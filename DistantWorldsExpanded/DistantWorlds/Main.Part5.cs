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


        private TutorialItemList method_454(string string_30)
        {
            TutorialItemList tutorialItemList = new TutorialItemList();
            int num = 0;
            string path = Application.StartupPath + "\\Tutorial\\" + string_30;
            if (!File.Exists(path))
            {
                path = Application.StartupPath + "\\Tutorial\\DE_" + string_30;
                if (!File.Exists(path))
                {
                    path = Application.StartupPath + "\\Tutorial\\FR_" + string_30;
                    if (!File.Exists(path))
                    {
                        path = Application.StartupPath + "\\Tutorial\\ES_" + string_30;
                    }
                }
            }
            try
            {
                if (!File.Exists(path))
                {
                    throw new ApplicationException("Missing file: " + string_30);
                }
                FileStream fileStream = File.OpenRead(path);
                StreamReader streamReader = new StreamReader(fileStream);
                TutorialItem tutorialItem = null;
                bool flag = true;
                while (!streamReader.EndOfStream)
                {
                    num++;
                    string text = streamReader.ReadLine();
                    if (flag)
                    {
                        if (tutorialItem != null)
                        {
                            tutorialItemList.Add(tutorialItem);
                        }
                        tutorialItem = new TutorialItem();
                        tutorialItem.Name = text.Trim();
                        if (!streamReader.EndOfStream)
                        {
                            text = streamReader.ReadLine();
                            tutorialItem.Title = text.Trim();
                        }
                        flag = false;
                    }
                    else if (text.Trim() == "~")
                    {
                        flag = true;
                    }
                    else
                    {
                        TutorialItem tutorialItem2 = tutorialItem;
                        tutorialItem2.Text = tutorialItem2.Text + text + "\n";
                    }
                }
                if (tutorialItem != null)
                {
                    tutorialItemList.Add(tutorialItem);
                }
                streamReader.Close();
                fileStream.Close();
                return tutorialItemList;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + string_30);
            }
        }

        private void method_455()
        {
            if (tutorial_0.LastStep)
            {
                method_155();
                btnTutorialContinue.Text = TextResolver.GetText("Play This Game");
                btnTutorialContinue.Location = new Point(btnTutorialContinue.Left + btnTutorialContinue.Width / 2 + 5, btnTutorialContinue.Top - 15);
                btnTutorialContinue.Size = new Size(btnTutorialContinue.Width / 2 - 5, btnTutorialContinue.Height + 15);
                btnTutorialExit.Location = new Point(btnTutorialExit.Left, btnTutorialExit.Top - 15);
                btnTutorialExit.Size = new Size(btnTutorialExit.Width, btnTutorialExit.Height + 15);
                btnTutorialExit.Visible = true;
            }
            else if (tutorial_0.CurrentStep.UnpauseGame)
            {
                method_155();
            }
            else
            {
                method_154();
            }
            lblTutorialTitle.Enabled = true;
            lblTutorialText.Enabled = true;
            lblTutorialTitle.Text = TextResolver.GetText("Tutorial") + ": " + tutorial_0.CurrentStep.Title;
            lblTutorialText.Text = tutorial_0.CurrentStep.Text;
            lblTutorialTitle.Enabled = false;
            lblTutorialText.Enabled = false;
            if (tutorial_0.PreviousStep != null && tutorial_0.PreviousStep.OpenScreen != null)
            {
                tutorial_0.PreviousStep.OpenScreen.HighlightControls = null;
            }
            if (tutorial_0.CurrentStep.ZoomScrollObject != null)
            {
                if (tutorial_0.CurrentStep.ZoomScrollObject is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)tutorial_0.CurrentStep.ZoomScrollObject;
                    mainView.method_2(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, tutorial_0.CurrentStep.ZoomLevel);
                }
                else if (tutorial_0.CurrentStep.ZoomScrollObject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)tutorial_0.CurrentStep.ZoomScrollObject;
                    mainView.method_2(builtObject.Xpos, builtObject.Ypos, tutorial_0.CurrentStep.ZoomLevel);
                }
                else if (tutorial_0.CurrentStep.ZoomScrollObject is Habitat)
                {
                    Habitat habitat = (Habitat)tutorial_0.CurrentStep.ZoomScrollObject;
                    mainView.method_2(habitat.Xpos, habitat.Ypos, tutorial_0.CurrentStep.ZoomLevel);
                }
                else if (tutorial_0.CurrentStep.ZoomScrollObject is SystemInfo)
                {
                    SystemInfo systemInfo = (SystemInfo)tutorial_0.CurrentStep.ZoomScrollObject;
                    mainView.method_2(systemInfo.SystemStar.Xpos, systemInfo.SystemStar.Ypos, tutorial_0.CurrentStep.ZoomLevel);
                }
            }
            if (tutorial_0.CurrentStep.SelectionObject != null)
            {
                if (tutorial_0.CurrentStep.SelectionObject is BuiltObject)
                {
                    BuiltObject object_ = (BuiltObject)tutorial_0.CurrentStep.SelectionObject;
                    method_208(object_);
                }
                else if (tutorial_0.CurrentStep.SelectionObject is ShipGroup)
                {
                    ShipGroup object_2 = (ShipGroup)tutorial_0.CurrentStep.SelectionObject;
                    method_208(object_2);
                }
                else if (tutorial_0.CurrentStep.SelectionObject is Habitat)
                {
                    Habitat object_3 = (Habitat)tutorial_0.CurrentStep.SelectionObject;
                    method_208(object_3);
                }
                else if (tutorial_0.CurrentStep.SelectionObject is SystemInfo)
                {
                    SystemInfo systemInfo2 = (SystemInfo)tutorial_0.CurrentStep.SelectionObject;
                    _Game.SelectedObject = systemInfo2;
                    Bitmap bitmap = null;
                    bitmap = ((systemInfo2.SystemStar.Category != HabitatCategoryType.GasCloud) ? bitmap_196[systemInfo2.SystemStar.MapPictureRef] : bitmap_4);
                    pnlDetailInfo.SetData(_Game, _Game.Galaxy, bitmap, systemInfo2);
                }
            }
            else
            {
                method_208(null);
            }
            if (tutorial_0.CurrentStep.OpenScreen != null)
            {
                if (tutorial_0.CurrentStep.ListSelection != null)
                {
                    switch (tutorial_0.CurrentStep.OpenScreen.Name)
                    {
                        case "pnlDesignDetail":
                            if (!pnlDesignDetail.Visible)
                            {
                                string_16 = "view";
                                if (tutorial_0.CurrentStep.ListSelection is Design)
                                {
                                    OpenDesignEditor((Design)tutorial_0.CurrentStep.ListSelection);
                                }
                                else
                                {
                                    OpenDesignEditor(null);
                                }
                            }
                            break;
                        case "pnlEmpireInfo":
                            if (!pnlEmpireInfo.Visible)
                            {
                                if (tutorial_0.CurrentStep.ListSelection is Empire)
                                {
                                    method_195((Empire)tutorial_0.CurrentStep.ListSelection);
                                }
                                else
                                {
                                    method_195(null);
                                }
                            }
                            break;
                        case "pnlDiplomacyTalk":
                            if (!pnlDiplomacyTalk.Visible)
                            {
                                if (tutorial_0.CurrentStep.ListSelection is Empire)
                                {
                                    method_295((Empire)tutorial_0.CurrentStep.ListSelection);
                                }
                                else
                                {
                                    method_295(null);
                                }
                            }
                            break;
                        case "pnlIntelligenceAgents":
                            if (!kYdDyYeMls.Visible)
                            {
                                if (tutorial_0.CurrentStep.ListSelection is Character)
                                {
                                    method_424((Character)tutorial_0.CurrentStep.ListSelection);
                                }
                                else
                                {
                                    method_424(null);
                                }
                            }
                            break;
                        default:
                            method_442(tutorial_0.CurrentStep.OpenScreen);
                            break;
                    }
                }
                else
                {
                    method_444(tutorial_0.CurrentStep.OpenScreen);
                    method_442(tutorial_0.CurrentStep.OpenScreen);
                }
                if (!string.IsNullOrEmpty(tutorial_0.CurrentStep.ScreenTabName))
                {
                    switch (tutorial_0.CurrentStep.OpenScreen.Name)
                    {
                        case "pnlEmpireGraphs":
                            tabEmpireComparisonGraphs.SelectTab(tutorial_0.CurrentStep.ScreenTabName);
                            break;
                        case "pnlColonyInfo":
                            tabColonyData.SelectTab(tutorial_0.CurrentStep.ScreenTabName);
                            break;
                        case "pnlBuiltObjectInfo":
                            tabBuiltObjectData.SelectTab(tutorial_0.CurrentStep.ScreenTabName);
                            break;
                    }
                }
            }
            else
            {
                method_441();
            }
            mainView.method_3();
            if (tutorial_0.CurrentStep.HighlightObject != null)
            {
                if (tutorial_0.CurrentStep.HighlightObject is ItemListCollectionPanel)
                {
                    if (tutorial_0.CurrentStep.HighlightOpenEmpireNavigationToolPanel)
                    {
                        List<Control> list = new List<Control>();
                        if (tutorial_0.CurrentStep.HighlightActionButtonNumber > 0)
                        {
                            switch (tutorial_0.CurrentStep.HighlightActionButtonNumber)
                            {
                                case 1:
                                    list.Add(btnSelectionAction1);
                                    break;
                                case 2:
                                    list.Add(btnSelectionAction2);
                                    break;
                                case 3:
                                    list.Add(btnSelectionAction3);
                                    break;
                                case 4:
                                    list.Add(btnSelectionAction4);
                                    break;
                                case 5:
                                    list.Add(btnSelectionAction5);
                                    break;
                                case 6:
                                    list.Add(btnSelectionAction6);
                                    break;
                                case 7:
                                    list.Add(btnSelectionAction7);
                                    break;
                                case 8:
                                    list.Add(btnSelectionAction8);
                                    break;
                            }
                        }
                        else if (tutorial_0.CurrentStep.HighlightActionButtonNumber == -1)
                        {
                            list.Add(btnSelectionAction1);
                            list.Add(btnSelectionAction2);
                            list.Add(btnSelectionAction3);
                            list.Add(btnSelectionAction4);
                            list.Add(btnSelectionAction5);
                            list.Add(btnSelectionAction6);
                            list.Add(btnSelectionAction7);
                            list.Add(btnSelectionAction8);
                        }
                        mainView.method_11(list);
                        if (!string.IsNullOrEmpty(tutorial_0.CurrentStep.HighlightEmpireNavigationToolPanelTitle))
                        {
                            method_76(itemListCollectionPanel_0.Panels[tutorial_0.CurrentStep.HighlightEmpireNavigationToolPanelTitle]);
                            itemListCollectionPanel_0.ActivePanel = itemListCollectionPanel_0.Panels[tutorial_0.CurrentStep.HighlightEmpireNavigationToolPanelTitle];
                        }
                        else
                        {
                            method_76(itemListCollectionPanel_0.Panels[0]);
                            itemListCollectionPanel_0.ActivePanel = itemListCollectionPanel_0.Panels[0];
                        }
                    }
                    else
                    {
                        mainView.method_11(null);
                        itemListCollectionPanel_0.ActivePanel = null;
                    }
                }
                else if (tutorial_0.CurrentStep.HighlightObject is ShipGroup)
                {
                    mainView.method_10((ShipGroup)tutorial_0.CurrentStep.HighlightObject);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is List<object>)
                {
                    mainView.method_6((List<object>)tutorial_0.CurrentStep.HighlightObject);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is Control)
                {
                    Control item = (Control)tutorial_0.CurrentStep.HighlightObject;
                    if (tutorial_0.CurrentStep.OpenScreen != null)
                    {
                        List<Control> list2 = new List<Control>();
                        list2.Add(item);
                        tutorial_0.CurrentStep.OpenScreen.HighlightControls = list2;
                    }
                    mainView.method_12(item);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is List<Control>)
                {
                    List<Control> list3 = (List<Control>)tutorial_0.CurrentStep.HighlightObject;
                    if (tutorial_0.CurrentStep.OpenScreen != null)
                    {
                        tutorial_0.CurrentStep.OpenScreen.HighlightControls = list3;
                    }
                    mainView.method_13(list3);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is BuiltObject)
                {
                    BuiltObject builtObject2 = (BuiltObject)tutorial_0.CurrentStep.HighlightObject;
                    mainView.method_7(builtObject2);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is Habitat)
                {
                    Habitat habitat2 = (Habitat)tutorial_0.CurrentStep.HighlightObject;
                    mainView.method_4(habitat2, tutorial_0.CurrentStep.HighlightHabitatEmpire, tutorial_0.CurrentStep.HighlightHabitatPopulationGraph, tutorial_0.CurrentStep.HighlightHabitatDominantRace, tutorial_0.CurrentStep.HighlightHabitatResources);
                }
                else if (tutorial_0.CurrentStep.HighlightObject is SystemInfo)
                {
                    SystemInfo systemInfo_ = (SystemInfo)tutorial_0.CurrentStep.HighlightObject;
                    mainView.method_9(systemInfo_);
                }
            }
            else
            {
                mainView.method_3();
            }
            pnlTutorial.BringToFront();
        }

        private void btnTutorialContinue_Click(object sender, EventArgs e)
        {
            if (tutorial_0 != null)
            {
                tutorial_0.Next();
                btnTutorialContinue.Text = TextResolver.GetText("Continue");
                if (tutorial_0.Finished)
                {
                    method_440();
                }
                else
                {
                    method_455();
                }
            }
            else
            {
                method_440();
            }
        }

        private void btnTutorialExit_Click(object sender, EventArgs e)
        {
            method_440();
            method_92();
            bool_4 = true;
            _Game.SelectedObject = null;
            int_28 = 0;
            int_29 = 0;
            pnlDetailInfo.ClearData();
            pnlHabitatInfo.ClearData();
            hoverPanel_0.ClearData();
            pnlBuiltObjectDetail.ClearData();
            pnlColonyHabitatInfo.ClearData();
            picSystem.Extinguish();
            picSystemMap.Extinguish();
            method_357();
        }

        private void btnIntroductionStart_Click(object sender, EventArgs e)
        {
            method_82();
            if (_Game.PlayerEmpire != null)
            {
                _ = _Game.PlayerEmpire.Capital;
            }
            WuVtIlwpRt();
        }

        internal void method_456(string string_30)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            vTtmruAejE.Clear();
            int_25 = -1;
            pnlEncyclopedia.Size = new Size(995, 700);
            pnlEncyclopedia.Location = new Point((mainView.Width - pnlEncyclopedia.Width) / 2, (mainView.Height - pnlEncyclopedia.Height) / 2);
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
            EncyclopediaItem encyclopediaItem = encyclopediaItemList_0[string_30];
            if (encyclopediaItem != null)
            {
                vTtmruAejE.Add(encyclopediaItem);
                int_25++;
            }
            method_458(encyclopediaItem);
            method_468();
            pnlEncyclopedia.Visible = true;
            pnlEncyclopedia.BringToFront();
        }

        private void method_457()
        {
            pnlEncyclopedia.Visible = false;
            pnlEncyclopedia.SendToBack();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_458(EncyclopediaItem encyclopediaItem_0)
        {
            encyclopediaItem_0 = method_459(encyclopediaItem_0);
            method_463(encyclopediaItem_0);
            method_464(encyclopediaItem_0);
        }

        private EncyclopediaItem method_459(EncyclopediaItem encyclopediaItem_0)
        {
            webEncyclopediaContent.AllowWebBrowserDrop = false;
            //webEncyclopediaContent.ContextMenu = null;
            webEncyclopediaContent.ContextMenuStrip = null;
            webEncyclopediaContent.IsWebBrowserContextMenuEnabled = false;
            webEncyclopediaContent.IsWebBrowserContextMenuEnabled = true;
            webEncyclopediaContent.WebBrowserShortcutsEnabled = false;
            if (encyclopediaItem_0 != null)
            {
                pnlEncyclopedia.HeaderTitle = TextResolver.GetText("Galactopedia") + ": " + encyclopediaItem_0.Title;
                string text = Application.StartupPath + "\\help\\";
                string text2 = string.Empty;
                if (!string.IsNullOrEmpty(_Game.CustomizationSetName) && _Game.CustomizationSetName.ToLower(CultureInfo.InvariantCulture) != "(default)")
                {
                    text2 = Application.StartupPath + "\\Customization\\" + _Game.CustomizationSetName + "\\help\\";
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
                        Race race_ = _Game.Galaxy.Races[encyclopediaItem_0.Title];
                        string value = method_460(race_);
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
                    else
                    {
                        webEncyclopediaContent.Navigate2(text3);
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
                if (!string.IsNullOrEmpty(_Game.CustomizationSetName))
                {
                    string title = string.Format(TextResolver.GetText("THEMENAME Theme"), _Game.CustomizationSetName);
                    encyclopediaItem_0 = encyclopediaItemList_0[title];
                    if (encyclopediaItem_0 != null)
                    {
                        encyclopediaItem_0 = method_459(encyclopediaItem_0);
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

        private string method_460(Race race_1)
        {
            string text = string.Empty;
            if (race_1 != null)
            {
                RaceSummary raceSummary = Galaxy.GenerateRaceSummary(race_1);
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

        private List<string> method_461(Race race_1)
        {
            List<string> list = new List<string>();
            string empty = string.Empty;
            empty = TextResolver.GetText("Default Reproduction Rate") + ": " + (race_1.ReproductiveRate - 1.0).ToString("#0%");
            list.Add(empty);
            string text = method_462(race_1.IntelligenceLevel);
            if (text == TextResolver.GetText("Slightly") && race_1.IntelligenceLevel >= 100)
            {
                text = TextResolver.GetText("Moderately");
            }
            empty = string.Format(arg1: (race_1.IntelligenceLevel < 100) ? TextResolver.GetText("Stupid") : TextResolver.GetText("Intelligent"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_1.IntelligenceLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_462(race_1.AggressionLevel);
            empty = string.Format(arg1: (race_1.AggressionLevel < 100) ? TextResolver.GetText("Passive") : TextResolver.GetText("Aggressive"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_1.AggressionLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_462(race_1.CautionLevel);
            empty = string.Format(arg1: (race_1.CautionLevel < 100) ? TextResolver.GetText("Reckless") : TextResolver.GetText("Cautious"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_1.CautionLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_462(race_1.FriendlinessLevel);
            empty = string.Format(arg1: (race_1.FriendlinessLevel < 100) ? TextResolver.GetText("Unfriendly") : TextResolver.GetText("Friendly"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_1.FriendlinessLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            text = method_462(race_1.LoyaltyLevel);
            empty = string.Format(arg1: (race_1.LoyaltyLevel < 100) ? TextResolver.GetText("Unreliable") : TextResolver.GetText("Dependable"), format: TextResolver.GetText("Racial Characteristic INTENSITY QUALITY"), arg0: text);
            empty = empty + " (" + (race_1.LoyaltyLevel - 100).ToString("+##0;-##0;0") + ")";
            list.Add(empty);
            return list;
        }

        private string method_462(int int_64)
        {
            string empty = string.Empty;
            int_64 -= 100;
            int_64 = Math.Abs(int_64);
            if (int_64 >= 30)
            {
                empty = TextResolver.GetText("Extremely");
            }
            else if (int_64 >= 17)
            {
                empty = TextResolver.GetText("Very");
            }
            else if (int_64 >= 6)
            {
                empty = TextResolver.GetText("Quite");
            }
            else if (int_64 < 6)
            {
                empty = TextResolver.GetText("Slightly");
            }
            return empty;
        }

        private void method_463(EncyclopediaItem encyclopediaItem_0)
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

        private void method_464(EncyclopediaItem encyclopediaItem_0)
        {
            if (encyclopediaItem_0 != null)
            {
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected -= method_467;
                pnlEncyclopediaTopics.SetSelectedItem(encyclopediaItem_0);
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected += method_467;
            }
            else
            {
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected -= method_467;
                pnlEncyclopediaTopics.SetSelectedItem(null);
                pnlEncyclopediaTopics.CollapseAll();
                pnlEncyclopediaTopics.OnEncyclopediaItemSelected += method_467;
            }
        }

        internal EncyclopediaItemList method_465(Galaxy galaxy_0, string string_30, string string_31)
        {
            EncyclopediaItemList encyclopediaItemList = new EncyclopediaItemList();
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Components"), "Component_Overview.mht", EncyclopediaCategory.Components, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Finding Your Way Around"), "UI_MouseActions.mht", EncyclopediaCategory.UserInterface, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Concepts"), "GameConcepts_YourEmpire.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Screens"), "Screen_MainScreenElements.mht", EncyclopediaCategory.Screens, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Planet and Star Types"), "Planet_Asteroid.mht", EncyclopediaCategory.PlanetsAndStars, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resources"), "Resource_Overview.mht", EncyclopediaCategory.Resources, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ships and Bases"), "Ship_SpacePort.mht", EncyclopediaCategory.Ships, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Alien Races"), "AlienRaces.mht", EncyclopediaCategory.Races, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Government Types"), "GameConcepts_GovernmentTypes.mht", EncyclopediaCategory.GovernmentTypes, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Space Creatures"), "Creatures.mht", EncyclopediaCategory.Creatures, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Editor"), "editor_overview.mht", EncyclopediaCategory.Editor, isCategoryRoot: true));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Bombard Weapons"), "Component_BombardWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fighter Bays"), "Component_FighterBay.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Gravity Well Projectors"), "Component_HyperBlock.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("HyperDeny Components"), "Component_HyperDeny.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ion Defense"), "Component_IonDefense.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ion Weapons"), "Component_IonWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Missile Weapons"), "Component_MissileWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Point Defense Weapons"), "Component_PointDefenseWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Scanner Jammers"), "Component_ScannerJammer.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Area Shield Recharge"), "Component_ShieldRecharge.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Trace Scanners"), "Component_TraceScanner.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Corruption"), "GameConcepts_Corruption.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fighters"), "GameConcepts_Fighters.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Planetary Facilities"), "GameConcepts_PlanetaryFacilities.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Build Order Screen"), "Screen_BuildOrder.mht", EncyclopediaCategory.Screens, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Policy Screen"), "Screen_EmpirePolicy.mht", EncyclopediaCategory.Screens, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Options - Your Empire Settings Screen"), "Screen_YourEmpireSettings.mht", EncyclopediaCategory.Screens, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Options - Message Settings Screen"), "Screen_MessageSettings.mht", EncyclopediaCategory.Screens, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Defensive Bases"), "Ship_DefensiveBase.mht", EncyclopediaCategory.Ships, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Monitoring Stations"), "Ship_MonitoringStation.mht", EncyclopediaCategory.Ships, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Research Stations"), "Ship_ResearchStation.mht", EncyclopediaCategory.Ships, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Navigation Tool"), "UI_EmpireNavigationTool.mht", EncyclopediaCategory.UserInterface, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Characters"), "GameConcepts_Characters.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Wonders"), "GameConcepts_Wonders.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colony Population Policies"), "GameConcepts_ColonyPopulationPolicies.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Carriers"), "Ship_Carrier.mht", EncyclopediaCategory.Ships, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Rail Guns"), "Component_RailGunWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Energy To Fuel Converter"), "Component_EnergyToFuelConverter.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Phased Weapons"), "Component_PhaserWeapons.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("SilverMist"), "Creature_4.mht", EncyclopediaCategory.Creatures, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Territory"), "GameConcepts_EmpireTerritory.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Assault Pods"), "Component_AssaultPod.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Tractor Beams"), "Component_TractorBeam.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Gravity Beam Weapons"), "Component_GravityBeamWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Gravity Area Weapons"), "Component_GravityAreaWeapon.mht", EncyclopediaCategory.Components, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Boarding And Capture"), "GameConcepts_BoardingAndCapture.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ground Report Screen"), "Screen_GroundCombat.mht", EncyclopediaCategory.Screens, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Introduction"), "default.mht", EncyclopediaCategory.GameConcepts, isCategoryRoot: false));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Continental Planets"), "Planet_Continental.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Marshy Swamp Planets"), "Planet_MarshySwamp.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Desert Planets"), "Planet_SandyDesert.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ocean Planets"), "Planet_Ocean.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ice Planets"), "Planet_IceGlacial.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Volcanic Planets"), "Planet_Volcanic.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Barren Rock Planets"), "Planet_BarrenRock.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Gas Giant Planets"), "Planet_GasGiant.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Frozen Gas Giant Planets"), "Planet_FrozenGasGiant.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Asteroids"), "Planet_Asteroid.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Gas Clouds"), "Planet_GasCloud.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Stars"), "Planet_Star.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Nebulae Clouds"), "Planet_Nebulae.mht", EncyclopediaCategory.PlanetsAndStars));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Asteroid"), "editor_editasteroid.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Space Creature"), "editor_editcreature.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Empire - Colonies"), "editor_editempirecolonies.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Empire - Details"), "editor_editempiredetails.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Empire - Research"), "editor_editempireresearch.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Empire - Characters"), "editor_editempirecharacters.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Empire - Ships and Bases"), "editor_editempireshipbase.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Galaxy"), "editor_editgalaxy.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Game Events"), "editor_editgameevents.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Gas Cloud"), "editor_editgascloud.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Planet or Moon"), "editor_editplanet.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Ship or Base"), "editor_editshipbase.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Edit Star"), "editor_editstar.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Exploration"), "editor_empireexploration.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Erase Independent Alien Race"), "editor_erasealienrace.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Erase Asteroid Field"), "editor_eraseasteroidfield.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Erase Colony"), "editor_erasecolony.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Erase Items"), "editor_eraseitems.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Erase Ruins"), "editor_eraseruins.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Independent Alien Race"), "editor_placealienrace.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Asteroid"), "editor_placeasteroid.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Asteroid Field"), "editor_placeasteroidfield.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Colony"), "editor_placecolony.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Space Creature"), "editor_placecreature.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Gas Cloud"), "editor_placegascloud.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Moon"), "editor_placemoon.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Pirate Faction"), "editor_placepirates.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Planet"), "editor_placeplanet.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Ruins"), "editor_placeruins.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Ship or Base"), "editor_placeshipbase.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place Star"), "editor_placestar.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Place System"), "editor_placesystem.mht", EncyclopediaCategory.Editor));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Exploration"), "GameConcepts_Exploration.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colonization"), "GameConcepts_Colonization.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colony Growth"), "GameConcepts_ColonyGrowth.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colony Approval"), "GameConcepts_ColonyApproval.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Your Empire - State vs Private"), "GameConcepts_YourEmpire.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Diplomacy"), "GameConcepts_Diplomacy.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Reputation"), "GameConcepts_Reputation.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Diplomatic Relation Types"), "GameConcepts_DiplomaticRelationTypes.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Blockades"), "GameConcepts_Blockades.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Construction"), "GameConcepts_Construction.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Research"), "GameConcepts_Research.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ship Designs"), "GameConcepts_Designs.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ship Costs"), "GameConcepts_ShipCosts.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fleets"), "GameConcepts_Fleets.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Intelligence Missions"), "GameConcepts_IntelligenceMissions.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Troops"), "GameConcepts_Troops.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Combat - Space Battles"), "GameConcepts_CombatSpace.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Combat - Colony Invasions"), "GameConcepts_CombatInvasion.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Combat - Ground Combat"), "GameConcepts_CombatGround.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Independent planets and Traders"), "GameConcepts_Independents.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Pirates"), "GameConcepts_Pirates.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Alternative Game play"), "GameConcepts_AlternativeGamePlay.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ancient Ruins"), "GameConcepts_Ruins.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Tourism"), "GameConcepts_Tourism.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Migration"), "GameConcepts_Migration.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Mining for Resources"), "GameConcepts_Mining.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fuel"), "GameConcepts_Fuel.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Economy Tips"), "GameConcepts_EconomyTips.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colonization Tips"), "GameConcepts_ColonizationTips.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colony Taxes"), "GameConcepts_Tax.mht", EncyclopediaCategory.GameConcepts));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Mouse Actions"), "UI_MouseActions.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Controlling your Ships"), "UI_ShipMissions.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ship Mission Types"), "UI_ShipMissionTypes.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Keyboard Commands"), "UI_KeyboardCommands.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Messages and Events"), "UI_Messages.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ship Symbols"), "UI_ShipSymbols.mht", EncyclopediaCategory.UserInterface));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colonies Screen"), "Screen_Colonies.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Design Detail Screen"), "Screen_DesignDetail.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Designs Screen"), "Screen_Designs.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Diplomacy Screen"), "Screen_Diplomacy.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Empire Comparisons and Victory Conditions"), "Screen_EmpireComparison.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Your Empire Summary Screen"), "Screen_EmpireSummary.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fleets Screen"), "Screen_Fleets.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Galaxy Map Screen"), "Screen_GalaxyMap.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Menu"), "Screen_GameMenu.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Options"), "Screen_GameOptions.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Selection Panel"), "Screen_InfoPanel.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Characters Screen"), "Screen_IntelligenceMissions.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Main Screen"), "Screen_MainScreenElements.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Research Screen"), "Screen_Research.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ships and Bases Screen"), "Screen_ShipsAndBases.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Speaking to Other Empires"), "Screen_TalkingWithEmpires.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Troops Screen"), "Screen_Troops.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Message History Screen"), "Screen_MessageHistory.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Expansion Planner Screen"), "Screen_ExpansionPlanner.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Start New Game Screen"), "Screen_StartNewGame.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Game Options - Advanced Display Settings Screen"), "Screen_AdvancedDisplaySettings.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Component Guide"), "Screen_ComponentGuide.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Change Theme Screen"), "Screen_ChangeTheme.mht", EncyclopediaCategory.Screens));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Capital Ships"), "Ship_CapitalShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colony Ships"), "Ship_ColonyShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Construction Ships"), "Ship_ConstructionShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Cruisers"), "Ship_Cruiser.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Destroyers"), "Ship_Destroyer.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Escorts"), "Ship_Escort.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Exploration Ships"), "Ship_ExplorationShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Freighters"), "Ship_Freighter.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Frigates"), "Ship_Frigate.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Star Bases"), "Ship_GenericBase.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Mining Ships"), "Ship_MiningShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Mining Stations"), "Ship_MiningStation.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Space Ports"), "Ship_SpacePort.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Troop Transports"), "Ship_TroopTransport.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resupply Ships"), "Ship_ResupplyShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resort Bases"), "Ship_ResortBase.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Passenger Ships"), "Ship_PassengerShip.mht", EncyclopediaCategory.Ships));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Area Weapons"), "Component_AreaWeapon.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Armor"), "Component_Armor.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Beam Weapons"), "Component_BeamWeapon.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Cargo Storage"), "Component_CargoStorage.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Colonization Modules"), "Component_ColonizationModule.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Combat Targetting"), "Component_CombatTargetting.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Command Centers"), "Component_CommandCenter.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Commerce Centers"), "Component_CommerceCenter.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Construction Yards"), "Component_ConstructionYard.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Countermeasures"), "Component_Countermeasures.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Docking Bays"), "Component_DockingBay.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Energy Collectors"), "Component_EnergyCollector.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Engines"), "Component_Engines.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Fuel Storage"), "Component_FuelStorage.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Habitation Modules"), "Component_HabitationModule.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Hyperdrives"), "Component_HyperDrive.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Life Support"), "Component_LifeSupport.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Long Range Scanners"), "Component_LongRangeScanner.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Manufacturers"), "Component_Manufacturer.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Medical Centers"), "Component_MedicalCenter.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Components"), "Component_Overview.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Proximity Arrays"), "Component_ProximityArray.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Reactors"), "Component_Reactor.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Recreation Centers"), "Component_RecreationCenter.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Research Laboratories"), "Component_ResearchLaboratory.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resource Extractors"), "Component_ResourceExtractor.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resource Profile Sensors"), "Component_ResourceProfileSensor.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Shields"), "Component_Shields.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Torpedo Weapons"), "Component_TorpedoWeapon.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Troop Storage"), "Component_TroopStorage.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Vectoring Engines"), "Component_VectoringEngines.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Damage Control"), "Component_DamageControl.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Stealth"), "Component_Stealth.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Passenger Storage"), "Component_PassengerStorage.mht", EncyclopediaCategory.Components));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Resources Visual Index"), "Resource_VisualIndex.mht", EncyclopediaCategory.Resources));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Space Creatures"), "Creatures.mht", EncyclopediaCategory.Creatures));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Ardilus"), "Creature_3.mht", EncyclopediaCategory.Creatures));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Giant Kaltor"), "Creature_2.mht", EncyclopediaCategory.Creatures));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Space Slug"), "Creature_0.mht", EncyclopediaCategory.Creatures));
            encyclopediaItemList.Add(new EncyclopediaItem(TextResolver.GetText("Sand Slug"), "Creature_1.mht", EncyclopediaCategory.Creatures));
            if (galaxy_0 != null)
            {
                encyclopediaItemList = Galaxy.AddResourceTopics(encyclopediaItemList, galaxy_0.ResourceSystem, string_30, string_31);
                encyclopediaItemList = Galaxy.AddRaceTopics(encyclopediaItemList, galaxy_0.Races, string_30, string_31);
                encyclopediaItemList = Galaxy.AddGovernmentTopics(encyclopediaItemList, galaxy_0.Governments, string_30, string_31);
            }
            else
            {
                encyclopediaItemList = Galaxy.AddResourceTopics(encyclopediaItemList, Galaxy.ResourceSystemStatic, string_30, string_31);
                RaceList races = Galaxy.LoadRaces(string_30, string_31);
                encyclopediaItemList = Galaxy.AddRaceTopics(encyclopediaItemList, races, string_30, string_31);
                encyclopediaItemList = Galaxy.AddGovernmentTopics(encyclopediaItemList, Galaxy.GovernmentsStatic, string_30, string_31);
            }
            encyclopediaItemList[TextResolver.GetText("Introduction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")]);
            encyclopediaItemList[TextResolver.GetText("Introduction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Keyboard Commands")]);
            encyclopediaItemList[TextResolver.GetText("Introduction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Approval")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")]);
            encyclopediaItemList[TextResolver.GetText("Colony Taxes")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Corruption")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Continental Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Marshy Swamp Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Desert Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ocean Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ice Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Volcanic Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Space Ports")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ancient Ruins")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization Tips")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Taxes")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Space Ports")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Corruption")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Taxes")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Colony Approval")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Taxes")]);
            encyclopediaItemList[TextResolver.GetText("Colony Approval")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Taxes")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Policy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization Tips")]);
            encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Alien Races")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization Tips")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Colony Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization Tips")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Economy Tips")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Economy Tips")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Economy Tips")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization Tips")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Economy Tips")]);
            encyclopediaItemList[TextResolver.GetText("Continental Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Marshy Swamp Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Desert Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Ocean Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Ice Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Volcanic Planets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Continental Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ocean Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Marshy Swamp Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Desert Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ice Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Volcanic Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Continental Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Marshy Swamp Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Desert Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ocean Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ice Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Volcanic Planets")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Modules")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Construction Yards")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Ancient Ruins")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Ancient Ruins")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Tourism")]);
            encyclopediaItemList[TextResolver.GetText("Exploration")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ancient Ruins")]);
            encyclopediaItemList[TextResolver.GetText("Space Slug")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Asteroids")]);
            encyclopediaItemList[TextResolver.GetText("Space Slug")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Stations")]);
            encyclopediaItemList[TextResolver.GetText("Sand Slug")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Desert Planets")]);
            encyclopediaItemList[TextResolver.GetText("Sand Slug")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Stations")]);
            encyclopediaItemList[TextResolver.GetText("Giant Kaltor")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Frozen Gas Giant Planets")]);
            encyclopediaItemList[TextResolver.GetText("Giant Kaltor")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Gas Clouds")]);
            encyclopediaItemList[TextResolver.GetText("Giant Kaltor")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Shields")]);
            encyclopediaItemList[TextResolver.GetText("Ardilus")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Gas Giant Planets")]);
            encyclopediaItemList[TextResolver.GetText("SilverMist")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ion Weapons")]);
            foreach (EncyclopediaItem item in encyclopediaItemList)
            {
                if (item.Category == EncyclopediaCategory.Creatures && item.Title != TextResolver.GetText("Space Creatures"))
                {
                    item.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Space Creatures")]);
                }
            }
            foreach (EncyclopediaItem item2 in encyclopediaItemList)
            {
                if (item2.Category == EncyclopediaCategory.Races && item2.Title != TextResolver.GetText("Alien Races"))
                {
                    item2.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Alien Races")]);
                }
            }
            foreach (EncyclopediaItem item3 in encyclopediaItemList)
            {
                if (item3.Category == EncyclopediaCategory.PlanetsAndStars)
                {
                    item3.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Galaxy Map Screen")]);
                    item3.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources Visual Index")]);
                }
            }
            foreach (EncyclopediaItem item4 in encyclopediaItemList)
            {
                if (item4.Category == EncyclopediaCategory.Resources && item4.Title != TextResolver.GetText("Resources"))
                {
                    item4.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
                }
                if (item4.Category == EncyclopediaCategory.Resources && item4.Title != TextResolver.GetText("Resources Visual Index"))
                {
                    item4.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources Visual Index")]);
                }
            }
            foreach (EncyclopediaItem item5 in encyclopediaItemList)
            {
                if (item5.Category == EncyclopediaCategory.Components && item5.Title != TextResolver.GetText("Components"))
                {
                    item5.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
                    item5.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
                }
            }
            foreach (EncyclopediaItem item6 in encyclopediaItemList)
            {
                if (item6.Category == EncyclopediaCategory.Ships)
                {
                    item6.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
                    item6.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
                    item6.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")]);
                }
            }
            foreach (EncyclopediaItem item7 in encyclopediaItemList)
            {
                if (item7.Category == EncyclopediaCategory.GovernmentTypes && !item7.IsCategoryRoot)
                {
                    item7.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
                    item7.RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
                }
            }
            encyclopediaItemList[TextResolver.GetText("Alien Races")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")]);
            encyclopediaItemList[TextResolver.GetText("Alien Races")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Alien Races")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Alien Races")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Alien Races")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Characters Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Characters")]);
            encyclopediaItemList[TextResolver.GetText("Characters")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Characters Screen")]);
            encyclopediaItemList[TextResolver.GetText("Characters")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Intelligence Missions")]);
            encyclopediaItemList[TextResolver.GetText("Characters Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Intelligence Missions")]);
            encyclopediaItemList[TextResolver.GetText("Intelligence Missions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Characters Screen")]);
            encyclopediaItemList[TextResolver.GetText("Galaxy Map Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Exploration")]);
            encyclopediaItemList[TextResolver.GetText("Exploration")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Galaxy Map Screen")]);
            encyclopediaItemList[TextResolver.GetText("Exploration")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Exploration Ships")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Corporate Nationalism")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Reputation")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Blockades")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Speaking to Other Empires")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")]);
            encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Empire Reputation")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Empire Reputation")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Empire Territory")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Government Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Government Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Reputation")]);
            encyclopediaItemList[TextResolver.GetText("Speaking to Other Empires")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Speaking to Other Empires")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Speaking to Other Empires")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")]);
            encyclopediaItemList[TextResolver.GetText("Speaking to Other Empires")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Approval")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops Screen")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Approval")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Migration")]);
            encyclopediaItemList[TextResolver.GetText("Colony Approval")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Colony Approval")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Ships")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fleets")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Mission Types")]);
            encyclopediaItemList[TextResolver.GetText("Fleets Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fleets")]);
            encyclopediaItemList[TextResolver.GetText("Fleets Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")]);
            encyclopediaItemList[TextResolver.GetText("Fleets Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Fleets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fleets Screen")]);
            encyclopediaItemList[TextResolver.GetText("Fleets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Troops")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops Screen")]);
            encyclopediaItemList[TextResolver.GetText("Troops Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops")]);
            encyclopediaItemList[TextResolver.GetText("Troops Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Troops")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Research")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research Screen")]);
            encyclopediaItemList[TextResolver.GetText("Research")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Research")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Research Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research")]);
            encyclopediaItemList[TextResolver.GetText("Research Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Research Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Research")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research Stations")]);
            encyclopediaItemList[TextResolver.GetText("Research Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research Stations")]);
            encyclopediaItemList[TextResolver.GetText("Designs Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Designs Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Design Detail Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Design Detail Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Design Detail Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
            encyclopediaItemList[TextResolver.GetText("Design Detail Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research")]);
            encyclopediaItemList[TextResolver.GetText("Design Detail Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Ship Designs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Designs Screen")]);
            encyclopediaItemList[TextResolver.GetText("Ship Designs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Design Detail Screen")]);
            encyclopediaItemList[TextResolver.GetText("Ship Designs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Ship Designs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research")]);
            encyclopediaItemList[TextResolver.GetText("Ship Designs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Selection Panel")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Symbols")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mouse Actions")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Messages and Events")]);
            encyclopediaItemList[TextResolver.GetText("Expansion Planner Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Expansion Planner Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining for Resources")]);
            encyclopediaItemList[TextResolver.GetText("Expansion Planner Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Expansion Planner Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Message History Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Messages and Events")]);
            encyclopediaItemList[TextResolver.GetText("Game Options - Advanced Display Settings Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Game Options")]);
            encyclopediaItemList[TextResolver.GetText("Game Options - Message Settings Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Game Options")]);
            encyclopediaItemList[TextResolver.GetText("Game Options - Message Settings Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Messages and Events")]);
            encyclopediaItemList[TextResolver.GetText("Game Options - Your Empire Settings Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Game Options")]);
            encyclopediaItemList[TextResolver.GetText("Component Guide")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Component Guide")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Navigation Tool")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Tourism")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Migration")]);
            encyclopediaItemList[TextResolver.GetText("Resort Bases")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Tourism")]);
            encyclopediaItemList[TextResolver.GetText("Resort Bases")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Passenger Ships")]);
            encyclopediaItemList[TextResolver.GetText("Resort Bases")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ancient Ruins")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Storage")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Passenger Ships")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Storage")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resort Bases")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Storage")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Tourism")]);
            encyclopediaItemList[TextResolver.GetText("Passenger Storage")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Migration")]);
            encyclopediaItemList[TextResolver.GetText("Colonization Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Migration")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fuel")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Stations")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Ships")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Expansion Planner Screen")]);
            encyclopediaItemList[TextResolver.GetText("Mining for Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Territory")]);
            encyclopediaItemList[TextResolver.GetText("Engines")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fuel")]);
            encyclopediaItemList[TextResolver.GetText("Reactors")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fuel")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fuel")]);
            encyclopediaItemList[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Energy To Fuel Converter")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining for Resources")]);
            encyclopediaItemList[TextResolver.GetText("Economy Tips")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining for Resources")]);
            encyclopediaItemList[TextResolver.GetText("Tourism")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resort Bases")]);
            encyclopediaItemList[TextResolver.GetText("Tourism")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Passenger Ships")]);
            encyclopediaItemList[TextResolver.GetText("Tourism")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ancient Ruins")]);
            encyclopediaItemList[TextResolver.GetText("Tourism")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Wonders")]);
            encyclopediaItemList[TextResolver.GetText("Migration")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Passenger Ships")]);
            encyclopediaItemList[TextResolver.GetText("Migration")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Population Policies")]);
            encyclopediaItemList[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining for Resources")]);
            encyclopediaItemList[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Stations")]);
            encyclopediaItemList[TextResolver.GetText("Fuel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Ships")]);
            encyclopediaItemList[TextResolver.GetText("Fleets")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resupply Ships")]);
            encyclopediaItemList[TextResolver.GetText("Resupply Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fleets")]);
            encyclopediaItemList[TextResolver.GetText("Ship Symbols")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Main Screen")]);
            encyclopediaItemList[TextResolver.GetText("Messages and Events")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Message History Screen")]);
            encyclopediaItemList[TextResolver.GetText("Messages and Events")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Game Options - Message Settings Screen")]);
            encyclopediaItemList[TextResolver.GetText("Fighter Bays")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Fighter Bays")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Carriers")]);
            encyclopediaItemList[TextResolver.GetText("Point Defense Weapons")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Selection Panel")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Main Screen")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Carriers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Capital Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Cruisers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Destroyers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Frigates")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Escorts")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Troop Transports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Troop Transports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")]);
            encyclopediaItemList[TextResolver.GetText("Troop Transports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops")]);
            encyclopediaItemList[TextResolver.GetText("Troop Transports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Colony Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Exploration Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Exploration")]);
            encyclopediaItemList[TextResolver.GetText("Space Ports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Construction Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Space Ports")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction Ships")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Costs")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ships and Bases Screen")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Blockades")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomacy")]);
            encyclopediaItemList[TextResolver.GetText("Blockades")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")]);
            encyclopediaItemList[TextResolver.GetText("Ship Costs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Ship Costs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Ship Costs")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Controlling your Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Mission Types")]);
            encyclopediaItemList[TextResolver.GetText("Ship Mission Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Mouse Actions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Main Screen")]);
            encyclopediaItemList[TextResolver.GetText("Main Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Keyboard Commands")]);
            encyclopediaItemList[TextResolver.GetText("Keyboard Commands")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Controlling your Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Keyboard Commands")]);
            encyclopediaItemList[TextResolver.GetText("Components")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Research")]);
            encyclopediaItemList[TextResolver.GetText("Components")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ship Designs")]);
            encyclopediaItemList[TextResolver.GetText("Components")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Components")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Colony Growth")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Empire Comparisons and Victory Conditions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")]);
            encyclopediaItemList[TextResolver.GetText("Freighters")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")]);
            encyclopediaItemList[TextResolver.GetText("Mining Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Your Empire - State vs Private")]);
            encyclopediaItemList[TextResolver.GetText("Colonization")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Independent planets and Traders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Carriers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Capital Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Cruisers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Destroyers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Frigates")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Escorts")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Troop Transports")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Construction Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Exploration Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Colony Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Controlling your Ships")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Ships")]);
            encyclopediaItemList[TextResolver.GetText("Resources")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining Stations")]);
            encyclopediaItemList[TextResolver.GetText("Messages and Events")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Main Screen")]);
            encyclopediaItemList[TextResolver.GetText("Mining Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Mining Stations")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Resources")]);
            encyclopediaItemList[TextResolver.GetText("Diplomatic Relation Types")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Blockades")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Carriers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Capital Ships")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Cruisers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Destroyers")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighters")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Build Order Screen")]);
            encyclopediaItemList[TextResolver.GetText("Construction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Navigation Tool")]);
            encyclopediaItemList[TextResolver.GetText("Game Options")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Empire Policy Screen")]);
            encyclopediaItemList[TextResolver.GetText("Empire Navigation Tool")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Selection Panel")]);
            encyclopediaItemList[TextResolver.GetText("Fighters")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Fighter Bays")]);
            encyclopediaItemList[TextResolver.GetText("Fighters")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Carriers")]);
            encyclopediaItemList[TextResolver.GetText("Build Order Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Construction")]);
            encyclopediaItemList[TextResolver.GetText("Empire Policy Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Game Options")]);
            encyclopediaItemList[TextResolver.GetText("Corruption")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Planetary Facilities")]);
            encyclopediaItemList[TextResolver.GetText("Corruption")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Government Types")]);
            encyclopediaItemList[TextResolver.GetText("Planetary Facilities")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Planetary Facilities")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Wonders")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Planetary Facilities")]);
            encyclopediaItemList[TextResolver.GetText("Colonies Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Wonders")]);
            encyclopediaItemList[TextResolver.GetText("Colony Population Policies")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Colony Population Policies")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Approval")]);
            encyclopediaItemList[TextResolver.GetText("Colony Population Policies")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colony Growth")]);
            encyclopediaItemList[TextResolver.GetText("Colony Population Policies")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Migration")]);
            encyclopediaItemList[TextResolver.GetText("Empire Territory")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonization")]);
            encyclopediaItemList[TextResolver.GetText("Empire Territory")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Mining for Resources")]);
            encyclopediaItemList[TextResolver.GetText("Intelligence Missions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Characters")]);
            encyclopediaItemList[TextResolver.GetText("Wonders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Colonies Screen")]);
            encyclopediaItemList[TextResolver.GetText("Wonders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Planetary Facilities")]);
            encyclopediaItemList[TextResolver.GetText("Wonders")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Tourism")]);
            encyclopediaItemList[TextResolver.GetText("Ion Weapons")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("SilverMist")]);
            encyclopediaItemList[TextResolver.GetText("Assault Pods")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Boarding And Capture")]);
            encyclopediaItemList[TextResolver.GetText("Assault Pods")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Tractor Beams")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Boarding And Capture")]);
            encyclopediaItemList[TextResolver.GetText("Boarding And Capture")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Assault Pods")]);
            encyclopediaItemList[TextResolver.GetText("Boarding And Capture")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Boarding And Capture")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")]);
            encyclopediaItemList[TextResolver.GetText("Pirates")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Boarding And Capture")]);
            encyclopediaItemList[TextResolver.GetText("Pirates")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Start New Game Screen")]);
            encyclopediaItemList[TextResolver.GetText("Introduction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Start New Game Screen")]);
            encyclopediaItemList[TextResolver.GetText("Introduction")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Colony Invasions")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ground Report Screen")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ground Report Screen")]);
            encyclopediaItemList[TextResolver.GetText("Combat - Space Battles")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Boarding And Capture")]);
            encyclopediaItemList[TextResolver.GetText("Ground Report Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Troops")]);
            encyclopediaItemList[TextResolver.GetText("Ground Report Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Combat - Ground Combat")]);
            encyclopediaItemList[TextResolver.GetText("Troops")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ground Report Screen")]);
            encyclopediaItemList[TextResolver.GetText("Troops Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Ground Report Screen")]);
            encyclopediaItemList[TextResolver.GetText("Your Empire Summary Screen")].RelatedItems.Add(encyclopediaItemList[TextResolver.GetText("Pirates")]);
            if (galaxy_0 != null)
            {
                encyclopediaItemList = Galaxy.AddThemeTopics(encyclopediaItemList, string_30, string_31);
                return Galaxy.AddGameInfoTopics(encyclopediaItemList, string_30, string_31);
            }
            encyclopediaItemList = Galaxy.AddThemeTopics(encyclopediaItemList, string_30, string_31);
            return Galaxy.AddGameInfoTopics(encyclopediaItemList, string_30, string_31);
        }

        private void btnEncyclopediaForward_Click(object sender, EventArgs e)
        {
            if (int_25 < vTtmruAejE.Count - 1)
            {
                int_25++;
                method_458(vTtmruAejE[int_25]);
            }
            method_468();
        }

        private void btnEncyclopediaBack_Click(object sender, EventArgs e)
        {
            if (int_25 > 0)
            {
                int_25--;
                method_458(vTtmruAejE[int_25]);
            }
            method_468();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            if (pnlEncyclopedia.Visible)
            {
                method_457();
                return;
            }
            string empty = string.Empty;
            if (pnlGameEditor.Visible)
            {
                if (!pnlEditGameEvents.Visible && !ynbOfkDbGY.Visible && !pnlEditEventAction.Visible)
                {
                    if (pnlEditGalaxy.Visible)
                    {
                        empty = TextResolver.GetText("Edit Galaxy");
                    }
                    else if (pnlEditBuiltObject.Visible)
                    {
                        empty = TextResolver.GetText("Edit Ship or Base");
                    }
                    else if (pnlEditCreature.Visible)
                    {
                        empty = TextResolver.GetText("Edit Space Creature");
                    }
                    else if (pnlEditEmpire.Visible)
                    {
                        if (tabEditEmpire.SelectedTab != null)
                        {
                            if (tabEditEmpire.SelectedTab.Name == "tabEditEmpireMain")
                            {
                                empty = TextResolver.GetText("Edit Empire - Details");
                            }
                            else if (tabEditEmpire.SelectedTab.Name == "tabEditEmpireResearch")
                            {
                                empty = TextResolver.GetText("Edit Empire - Research");
                            }
                            else if (tabEditEmpire.SelectedTab.Name == "tabEditEmpireColonies")
                            {
                                empty = TextResolver.GetText("Edit Empire - Colonies");
                            }
                            else if (tabEditEmpire.SelectedTab.Name == "tabEditEmpireCharacters")
                            {
                                empty = TextResolver.GetText("Edit Empire - Characters");
                            }
                            else if (tabEditEmpire.SelectedTab.Name == "tabEditEmpireBuiltObjects")
                            {
                                empty = TextResolver.GetText("Edit Empire - Ships and Bases");
                            }
                        }
                    }
                    else if (pnlEditHabitat.Visible)
                    {
                        if (habitat_4 != null)
                        {
                            switch (habitat_4.Category)
                            {
                                case HabitatCategoryType.Star:
                                    empty = TextResolver.GetText("Edit Star");
                                    break;
                                case HabitatCategoryType.Planet:
                                case HabitatCategoryType.Moon:
                                    empty = TextResolver.GetText("Edit Planet or Moon");
                                    break;
                                case HabitatCategoryType.Asteroid:
                                    empty = TextResolver.GetText("Edit Asteroid");
                                    break;
                                case HabitatCategoryType.GasCloud:
                                    empty = TextResolver.GetText("Edit Gas Cloud");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (gameEditorSelector.EditMode)
                        {
                            case EditorMode.System:
                                empty = TextResolver.GetText("Place System");
                                break;
                            case EditorMode.GasCloud:
                                empty = TextResolver.GetText("Place Gas Cloud");
                                break;
                            case EditorMode.Star:
                                empty = TextResolver.GetText("Place Star");
                                break;
                            case EditorMode.Planet:
                                empty = TextResolver.GetText("Place Planet");
                                break;
                            case EditorMode.Moon:
                                empty = TextResolver.GetText("Place Moon");
                                break;
                            case EditorMode.Asteroid:
                                empty = TextResolver.GetText("Place Asteroid");
                                break;
                            case EditorMode.AsteroidField:
                                empty = TextResolver.GetText("Place Asteroid Field");
                                break;
                            case EditorMode.BuiltObject:
                                empty = TextResolver.GetText("Place Ship or Base");
                                break;
                            case EditorMode.Colony:
                                empty = TextResolver.GetText("Place Colony");
                                break;
                            case EditorMode.AlienRace:
                                empty = TextResolver.GetText("Place Independent Alien Race");
                                break;
                            case EditorMode.Creature:
                                empty = TextResolver.GetText("Place Space Creature");
                                break;
                            case EditorMode.Pirates:
                                empty = TextResolver.GetText("Place Pirate Faction");
                                break;
                            case EditorMode.Ruins:
                            case EditorMode.RuinsSpecialGovernment:
                            case EditorMode.RuinsSuperWeapon:
                            case EditorMode.RuinsSleepingRace:
                            case EditorMode.RuinsRefugees:
                            case EditorMode.RuinsOrigins:
                            case EditorMode.RuinsLostShip:
                            case EditorMode.RuinsLostColony:
                                empty = TextResolver.GetText("Place Ruins");
                                break;
                            default:
                                {
                                    ExtendedPanel selectedPanel = gameEditorSelector.SelectedPanel;
                                    if (selectedPanel != null)
                                    {
                                        switch (selectedPanel.Name)
                                        {
                                            case "cpnSystem":
                                                empty = TextResolver.GetText("Place System");
                                                break;
                                            case "cpnGasCloud":
                                                empty = TextResolver.GetText("Place Gas Cloud");
                                                break;
                                            case "cpnStar":
                                                empty = TextResolver.GetText("Place Star");
                                                break;
                                            case "cpnPlanet":
                                                empty = TextResolver.GetText("Place Planet");
                                                break;
                                            case "cpnMoon":
                                                empty = TextResolver.GetText("Place Moon");
                                                break;
                                            case "cpnAsteroid":
                                                empty = TextResolver.GetText("Place Asteroid");
                                                break;
                                            case "cpnBuiltObject":
                                                empty = TextResolver.GetText("Place Ship or Base");
                                                break;
                                            case "cpnColony":
                                                empty = TextResolver.GetText("Place Colony");
                                                break;
                                            case "cpnAliens":
                                                empty = TextResolver.GetText("Place Independent Alien Race");
                                                break;
                                            case "cpnCreature":
                                                empty = TextResolver.GetText("Place Space Creature");
                                                break;
                                            case "cpnPirates":
                                                empty = TextResolver.GetText("Place Pirate Faction");
                                                break;
                                            case "cpnEmpireExploration":
                                                empty = TextResolver.GetText("Empire Exploration");
                                                break;
                                            case "cpnClearItems":
                                                empty = TextResolver.GetText("Erase Items");
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        empty = TextResolver.GetText("Game Editor");
                                    }
                                    break;
                                }
                            case EditorMode.EmpireExploration:
                                empty = TextResolver.GetText("Empire Exploration");
                                break;
                            case EditorMode.ClearItems:
                                empty = TextResolver.GetText("Erase Items");
                                break;
                            case EditorMode.ClearColony:
                                empty = TextResolver.GetText("Erase Colony");
                                break;
                            case EditorMode.ClearAlienRace:
                                empty = TextResolver.GetText("Erase Independent Alien Race");
                                break;
                            case EditorMode.ClearRuins:
                                empty = TextResolver.GetText("Erase Ruins");
                                break;
                            case EditorMode.ClearAsteroidField:
                                empty = TextResolver.GetText("Erase Asteroid Field");
                                break;
                        }
                    }
                }
                else
                {
                    empty = TextResolver.GetText("Edit Game Events");
                }
            }
            else
            {
                if (_Game.SelectedObject != null)
                {
                    if (_Game.SelectedObject is SystemInfo)
                    {
                        empty = TextResolver.GetText("Stars");
                    }
                    else if (_Game.SelectedObject is ShipGroup)
                    {
                        empty = TextResolver.GetText("Fleets");
                    }
                    else if (_Game.SelectedObject is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                        if (builtObject.IsBlockaded)
                        {
                            empty = TextResolver.GetText("Blockades");
                        }
                        else if (_Game.Galaxy.PirateEmpires.Contains(builtObject.Empire))
                        {
                            empty = TextResolver.GetText("Pirates");
                        }
                        else if (builtObject.Empire == _Game.Galaxy.IndependentEmpire)
                        {
                            empty = TextResolver.GetText("Independent planets and Traders");
                        }
                        else
                        {
                            switch (builtObject.SubRole)
                            {
                                case BuiltObjectSubRole.Escort:
                                    empty = TextResolver.GetText("Escorts");
                                    break;
                                case BuiltObjectSubRole.Frigate:
                                    empty = TextResolver.GetText("Frigates");
                                    break;
                                case BuiltObjectSubRole.Destroyer:
                                    empty = TextResolver.GetText("Destroyers");
                                    break;
                                case BuiltObjectSubRole.Cruiser:
                                    empty = TextResolver.GetText("Cruisers");
                                    break;
                                case BuiltObjectSubRole.CapitalShip:
                                    empty = TextResolver.GetText("Capital Ships");
                                    break;
                                case BuiltObjectSubRole.TroopTransport:
                                    empty = TextResolver.GetText("Troop Transports");
                                    break;
                                case BuiltObjectSubRole.Carrier:
                                    empty = TextResolver.GetText("Carriers");
                                    break;
                                case BuiltObjectSubRole.ResupplyShip:
                                    empty = TextResolver.GetText("Resupply Ships");
                                    break;
                                case BuiltObjectSubRole.ExplorationShip:
                                    empty = TextResolver.GetText("Exploration Ships");
                                    break;
                                case BuiltObjectSubRole.SmallFreighter:
                                case BuiltObjectSubRole.MediumFreighter:
                                case BuiltObjectSubRole.LargeFreighter:
                                    empty = TextResolver.GetText("Freighters");
                                    break;
                                case BuiltObjectSubRole.ColonyShip:
                                    empty = TextResolver.GetText("Colony Ships");
                                    break;
                                case BuiltObjectSubRole.PassengerShip:
                                    empty = TextResolver.GetText("Passenger Ships");
                                    break;
                                case BuiltObjectSubRole.ConstructionShip:
                                    empty = TextResolver.GetText("Construction Ships");
                                    break;
                                case BuiltObjectSubRole.GasMiningShip:
                                case BuiltObjectSubRole.MiningShip:
                                    empty = TextResolver.GetText("Mining Ships");
                                    break;
                                case BuiltObjectSubRole.GasMiningStation:
                                case BuiltObjectSubRole.MiningStation:
                                    empty = TextResolver.GetText("Mining Stations");
                                    break;
                                case BuiltObjectSubRole.SmallSpacePort:
                                case BuiltObjectSubRole.MediumSpacePort:
                                case BuiltObjectSubRole.LargeSpacePort:
                                    empty = TextResolver.GetText("Space Ports");
                                    break;
                                case BuiltObjectSubRole.ResortBase:
                                    empty = TextResolver.GetText("Resort Bases");
                                    break;
                                case BuiltObjectSubRole.GenericBase:
                                    empty = TextResolver.GetText("Star Bases");
                                    break;
                                case BuiltObjectSubRole.EnergyResearchStation:
                                case BuiltObjectSubRole.WeaponsResearchStation:
                                case BuiltObjectSubRole.HighTechResearchStation:
                                    empty = TextResolver.GetText("Research Stations");
                                    break;
                                case BuiltObjectSubRole.MonitoringStation:
                                    empty = TextResolver.GetText("Monitoring Stations");
                                    break;
                                case BuiltObjectSubRole.DefensiveBase:
                                    empty = TextResolver.GetText("Defensive Bases");
                                    break;
                            }
                        }
                    }
                    else if (_Game.SelectedObject is Fighter)
                    {
                        empty = TextResolver.GetText("Fighters");
                    }
                    else if (_Game.SelectedObject is Habitat)
                    {
                        Habitat habitat = (Habitat)_Game.SelectedObject;
                        if (habitat.IsBlockaded)
                        {
                            empty = TextResolver.GetText("Blockades");
                        }
                        else if (habitat.Empire == _Game.Galaxy.IndependentEmpire)
                        {
                            empty = TextResolver.GetText("Independent planets and Traders");
                        }
                        else if (habitat.Category == HabitatCategoryType.Asteroid)
                        {
                            empty = TextResolver.GetText("Asteroids");
                        }
                        else if (habitat.Category == HabitatCategoryType.GasCloud)
                        {
                            empty = TextResolver.GetText("Gas Clouds");
                        }
                        else if (habitat.Category == HabitatCategoryType.Star)
                        {
                            empty = TextResolver.GetText("Stars");
                        }
                        else
                        {
                            switch (habitat.Type)
                            {
                                case HabitatType.Volcanic:
                                    empty = TextResolver.GetText("Volcanic Planets");
                                    break;
                                case HabitatType.Desert:
                                    empty = TextResolver.GetText("Desert Planets");
                                    break;
                                case HabitatType.MarshySwamp:
                                    empty = TextResolver.GetText("Marshy Swamp Planets");
                                    break;
                                case HabitatType.Continental:
                                    empty = TextResolver.GetText("Continental Planets");
                                    break;
                                case HabitatType.Ocean:
                                    empty = TextResolver.GetText("Ocean Planets");
                                    break;
                                case HabitatType.BarrenRock:
                                    empty = TextResolver.GetText("Barren Rock Planets");
                                    break;
                                case HabitatType.Ice:
                                    empty = TextResolver.GetText("Ice Planets");
                                    break;
                                case HabitatType.GasGiant:
                                    empty = TextResolver.GetText("Gas Giant Planets");
                                    break;
                                case HabitatType.FrozenGasGiant:
                                    empty = TextResolver.GetText("Frozen Gas Giant Planets");
                                    break;
                            }
                        }
                    }
                    else if (_Game.SelectedObject is Creature)
                    {
                        Creature creature = (Creature)_Game.SelectedObject;
                        switch (creature.Type)
                        {
                            case CreatureType.Kaltor:
                                empty = TextResolver.GetText("Giant Kaltor");
                                break;
                            case CreatureType.RockSpaceSlug:
                                empty = TextResolver.GetText("Space Slug");
                                break;
                            case CreatureType.DesertSpaceSlug:
                                empty = TextResolver.GetText("Sand Slug");
                                break;
                            case CreatureType.Ardilus:
                                empty = TextResolver.GetText("Ardilus");
                                break;
                            case CreatureType.SilverMist:
                                empty = TextResolver.GetText("SilverMist");
                                break;
                        }
                    }
                }
                if (pnlGameOptionsMessages.Visible)
                {
                    empty = TextResolver.GetText("Game Options - Message Settings Screen");
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
                else if (pnlGameMenu.Visible)
                {
                    empty = TextResolver.GetText("Game Menu");
                }
                else if (pnlBuildOrder.Visible)
                {
                    empty = TextResolver.GetText("Build Order Screen");
                }
                else if (pnlCharacterEventHistory.Visible)
                {
                    empty = TextResolver.GetText("Character Event History Screen");
                }
                else if (pnlAdvisorSuggestion.Visible)
                {
                    empty = TextResolver.GetText("Advisor Suggestion Screen");
                }
                else if (pnlEmpirePolicy.Visible)
                {
                    empty = TextResolver.GetText("Empire Policy Screen");
                }
                else if (pnlDesignDetail.Visible)
                {
                    empty = TextResolver.GetText("Design Detail Screen");
                }
                else if (pnlDesigns.Visible)
                {
                    empty = TextResolver.GetText("Designs Screen");
                }
                else if (pnlDiplomacyTalk.Visible)
                {
                    empty = TextResolver.GetText("Speaking to Other Empires");
                }
                else if (CaLkaMyrMQ.Visible)
                {
                    empty = TextResolver.GetText("Galaxy Map Screen");
                }
                else if (pnlShipGroupInfo.Visible)
                {
                    empty = TextResolver.GetText("Fleets Screen");
                }
                else if (pnlBuiltObjectInfo.Visible)
                {
                    empty = TextResolver.GetText("Ships and Bases Screen");
                }
                else if (pnlColonyInfo.Visible)
                {
                    empty = TextResolver.GetText("Colonies Screen");
                }
                else if (vHfFsoqMev.Visible)
                {
                    empty = TextResolver.GetText("Empire Comparisons and Victory Conditions");
                }
                else if (pnlEmpireInfo.Visible)
                {
                    empty = TextResolver.GetText("Diplomacy Screen");
                }
                else if (pnlEmpireSummary.Visible)
                {
                    empty = TextResolver.GetText("Your Empire Summary Screen");
                }
                else if (kYdDyYeMls.Visible)
                {
                    empty = TextResolver.GetText("Intelligence Agents Screen");
                }
                else if (pnlComponentGuide.Visible)
                {
                    empty = TextResolver.GetText("Component Guide");
                }
                else if (pnlResearch.Visible)
                {
                    empty = TextResolver.GetText("Research Screen");
                }
                else if (pnlTroopInfo.Visible)
                {
                    empty = TextResolver.GetText("Troops Screen");
                }
                else if (pnlMessageHistory.Visible)
                {
                    empty = TextResolver.GetText("Message History Screen");
                }
                else if (pnlExpansionPlanner.Visible)
                {
                    empty = TextResolver.GetText("Expansion Planner Screen");
                }
            }
            if (string.IsNullOrEmpty(empty))
            {
                empty = TextResolver.GetText("Main Screen");
            }
            method_456(empty);
        }

        internal void method_466(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_226();
            object linkData = e.Link.LinkData;
            if (!(linkData is EncyclopediaItem))
            {
                return;
            }
            EncyclopediaItem encyclopediaItem = (EncyclopediaItem)linkData;
            method_458(encyclopediaItem);
            if (int_25 < vTtmruAejE.Count - 1)
            {
                int_25++;
                vTtmruAejE[int_25] = encyclopediaItem;
                if (int_25 < vTtmruAejE.Count - 1)
                {
                    vTtmruAejE.RemoveRange(int_25 + 1, vTtmruAejE.Count - (int_25 + 1));
                }
            }
            else
            {
                vTtmruAejE.Add(encyclopediaItem);
                int_25++;
            }
            method_468();
        }

        private void method_467(object sender, EncyclopediaItemChangedEventArgs e)
        {
            EncyclopediaItem item = e.Item;
            if (item == null)
            {
                return;
            }
            method_459(item);
            method_463(item);
            method_464(item);
            if (int_25 < vTtmruAejE.Count - 1)
            {
                int_25++;
                vTtmruAejE[int_25] = item;
                if (int_25 < vTtmruAejE.Count - 1)
                {
                    vTtmruAejE.RemoveRange(int_25 + 1, vTtmruAejE.Count - (int_25 + 1));
                }
            }
            else
            {
                vTtmruAejE.Add(item);
                int_25++;
            }
            method_468();
        }

        private void method_468()
        {
            if (int_25 < vTtmruAejE.Count - 1)
            {
                btnEncyclopediaForward.Enabled = true;
            }
            else
            {
                btnEncyclopediaForward.Enabled = false;
            }
            if (int_25 > 0)
            {
                btnEncyclopediaBack.Enabled = true;
            }
            else
            {
                btnEncyclopediaBack.Enabled = false;
            }
        }

        private void btnEncyclopediaHome_Click(object sender, EventArgs e)
        {
            method_458(null);
        }

        public static string GetGameFilesFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My Games\\Distant Worlds Universe\\";
        }

        public static string GetGameFilesFolderCreateIfNeeded()
        {
            string gameFilesFolder = GetGameFilesFolder();
            CheckCreateFolder(gameFilesFolder);
            return gameFilesFolder;
        }

        public static string GetGameSavesFolderCreateIfNeeded()
        {
            string gameFilesFolder = GetGameFilesFolder();
            gameFilesFolder += "SavedGames\\";
            CheckCreateFolder(gameFilesFolder);
            return gameFilesFolder;
        }

        public static void CheckCreateFolder(string folderPath)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnGameOptionsResetAutomationMessages_Click(object sender, EventArgs e)
        {
            string string_ = TextResolver.GetText("Reenable All Automation Prompts");
            MessageBoxEx messageBoxEx = method_372(string_, TextResolver.GetText("Reset Automation Messages?"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                MessageBoxExManager.ResetAllSavedResponses();
                string filename = GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
                MessageBoxExManager.WriteSavedResponses(filename);
            }
        }

        private void ctlDesignComponentToolbox_RowDoubleClick(object sender, EventArgs e)
        {
            if (btnAddComponentToDesign.Enabled)
            {
                btnAddComponentToDesign_Click(null, new EventArgs());
            }
        }

        private void ctlDesignComponents_RowDoubleClick(object sender, EventArgs e)
        {
            if (btnRemoveComponentFromDesign.Enabled)
            {
                btnRemoveComponentFromDesign_Click(null, new EventArgs());
            }
        }

        private void btnColonyConstructionYardMoveToTop_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            BuiltObject selectedBuiltObject = ctlColonyConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedHabitat != null && selectedBuiltObject != null)
            {
                int num = selectedHabitat.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0)
                {
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Insert(0, selectedBuiltObject);
                    ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlColonyConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnColonyConstructionYardMoveUp_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            BuiltObject selectedBuiltObject = ctlColonyConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedHabitat != null && selectedBuiltObject != null)
            {
                int num = selectedHabitat.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num > 0)
                {
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Insert(num - 1, selectedBuiltObject);
                    ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlColonyConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnColonyConstructionYardMoveDown_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            BuiltObject selectedBuiltObject = ctlColonyConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedHabitat != null && selectedBuiltObject != null)
            {
                int num = selectedHabitat.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0 && num < selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Count - 1)
                {
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Insert(num + 1, selectedBuiltObject);
                    ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlColonyConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnColonyConstructionYardMoveToBottom_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            BuiltObject selectedBuiltObject = ctlColonyConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedHabitat != null && selectedBuiltObject != null)
            {
                int num = selectedHabitat.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0)
                {
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Insert(selectedHabitat.ConstructionQueue.ConstructionWaitQueue.Count, selectedBuiltObject);
                    ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlColonyConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnBuiltObjectConstructionYardMoveToTop_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            BuiltObject selectedBuiltObject = ctlConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedStellarObject != null && selectedBuiltObject != null)
            {
                int num = selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0)
                {
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Insert(0, selectedBuiltObject);
                    ctlConstructionYardWaitQueue.BindData(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnBuiltObjectConstructionYardMoveUp_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            BuiltObject selectedBuiltObject = ctlConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedStellarObject != null && selectedBuiltObject != null)
            {
                int num = selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num > 0)
                {
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Insert(num - 1, selectedBuiltObject);
                    ctlConstructionYardWaitQueue.BindData(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void awqrtdiblI_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            BuiltObject selectedBuiltObject = ctlConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedStellarObject != null && selectedBuiltObject != null)
            {
                int num = selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0 && num < selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Count - 1)
                {
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Insert(num + 1, selectedBuiltObject);
                    ctlConstructionYardWaitQueue.BindData(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnBuiltObjectConstructionYardMoveToBottom_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            BuiltObject selectedBuiltObject = ctlConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedStellarObject != null && selectedBuiltObject != null)
            {
                int num = selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.IndexOf(selectedBuiltObject);
                if (num >= 0)
                {
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.RemoveAt(num);
                    selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Insert(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue.Count, selectedBuiltObject);
                    ctlConstructionYardWaitQueue.BindData(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                    ctlConstructionYardWaitQueue.SelectBuiltObject(selectedBuiltObject);
                }
            }
        }

        private void btnColonyMakeCapital_Click(object sender, EventArgs e)
        {
            BaconMain.OnChangeCapital(this);
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null)
            {
                _Game.PlayerEmpire.Capital = selectedHabitat;
                _Game.PlayerEmpire.RecalculateColonyDistancesFromCapital();
                UnlxwvByxj.BindData(_Game.PlayerEmpire.Colonies);
            }
        }

        private void btnGalaxyMapKey_Click(object sender, EventArgs e)
        {
            method_129();
        }

        private void btnGalaxyMapKeyClose_Click(object sender, EventArgs e)
        {
            method_130();
        }

        private string method_469(string string_30)
        {
            if (!string.IsNullOrEmpty(string_30))
            {
                string_30 = string_30.Substring(0, string_30.Length - 2);
            }
            return string_30;
        }

        private void btnGameEditorExit_Click(object sender, EventArgs e)
        {
            method_478(bool_28: false);
            method_504();
        }

        private void btnGameEditor_Click(object sender, EventArgs e)
        {
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

        private bool method_470()
        {
            if (!pnlEmpirePolicy.Visible && !pnlBuiltObjectInfo.Visible && !pnlColonyInfo.Visible && !pnlDesigns.Visible && !pnlDesignDetail.Visible && !pnlDiplomacyTalk.Visible && !vHfFsoqMev.Visible && !pnlEmpireInfo.Visible && !pnlEmpireSummary.Visible && !pnlEncyclopedia.Visible && !pnlEventMessage.Visible && !CaLkaMyrMQ.Visible && !pnlGameMenu.Visible && !pnlGameOptions.Visible && !pnlGameOptionsAdvancedDisplaySettings.Visible && !pnlGameOptionsEmpireSettings.Visible && !kYdDyYeMls.Visible && !pnlMessageHistory.Visible && !pnlExpansionPlanner.Visible && !pnlResearch.Visible && !pnlShipGroupInfo.Visible && !pnlTroopInfo.Visible && !pnlComponentGuide.Visible && !pnlRuinDetail.Visible && !pnlResourceComponents.Visible && !pnlConstructionSummary.Visible && !pnlStoryEvent.Visible && !pnlBuildOrder.Visible && !pnlAdvisorSuggestion.Visible && !pnlCharacterEventHistory.Visible)
            {
                bool_9 = false;
                return false;
            }
            bool_9 = true;
            return true;
        }

        private void method_471()
        {
            method_185();
            method_183();
            method_186();
            method_293();
            method_308();
            method_294();
            method_496();
            method_529();
            method_162();
            method_401();
            method_194();
            method_275();
            method_457();
            method_132();
            method_477();
            method_437();
            method_357();
            method_545();
            method_549();
            method_573();
            method_596();
            method_639();
            pnlGameOptions.SendToBack();
            pnlGameOptions.Visible = false;
            method_426();
            method_82();
            method_399();
            method_271();
            method_184();
            method_440();
            bool_9 = false;
        }

        private void method_472(bool bool_28)
        {
            if (!bool_28)
            {
                pnlSystemMap.Visible = false;
                pnlSystemMap.SendToBack();
                picSystem.Visible = false;
                picSystem.SendToBack();
                btnZoomColony.Visible = false;
                btnZoomIn.Visible = false;
                btnZoomOut.Visible = false;
                btnZoomRegion.Visible = false;
                jQaYpdpkDs.Visible = false;
                btnZoomSystem.Visible = false;
                btnZoomColony.SendToBack();
                btnZoomIn.SendToBack();
                btnZoomOut.SendToBack();
                btnZoomRegion.SendToBack();
                jQaYpdpkDs.SendToBack();
                btnZoomSystem.SendToBack();
                btnMapCivilianFade.Visible = false;
                btnMapOverlay1.Visible = false;
                btnMapOverlay2.Visible = false;
                btnMapOverlay3.Visible = false;
                btnMapOverlay4.Visible = false;
                btnMapOverlay5.Visible = false;
                btnMapOverlay6.Visible = false;
                btnMapOverlay7.Visible = false;
                btnMapOverlay8.Visible = false;
                btnMapCivilianFade.SendToBack();
                btnMapOverlay1.SendToBack();
                btnMapOverlay2.SendToBack();
                btnMapOverlay3.SendToBack();
                btnMapOverlay4.SendToBack();
                btnMapOverlay5.SendToBack();
                btnMapOverlay6.SendToBack();
                btnMapOverlay7.SendToBack();
                btnMapOverlay8.SendToBack();
                diplomaticMessageQueue_0.Visible = false;
            }
            pnlInfoPanel.SendToBack();
            pnlDetailInfo.SendToBack();
            btnSelectionBack.Visible = false;
            btnSelectionForward.Visible = false;
            pnlInfoPanel.Visible = false;
            pnlDetailInfo.Visible = false;
            btnSelectionBack.SendToBack();
            btnSelectionForward.SendToBack();
            btnGameSpeedIncrease.Visible = false;
            btnGameSpeedDecrease.Visible = false;
            btnGameSpeedIncrease.SendToBack();
            btnGameSpeedDecrease.SendToBack();
            btnSelectionPanelSize.Visible = false;
            btnSelectionPanelSize.SendToBack();
            btnZoomSelection.Visible = false;
            btnSelectNearestMilitary.Visible = false;
            btnCycleShipStance.Visible = false;
            btnHelp.Visible = false;
            btnZoomSelection.SendToBack();
            btnSelectNearestMilitary.SendToBack();
            btnCycleShipStance.SendToBack();
            btnHelp.SendToBack();
            lstMessages.Visible = false;
            btnPlayPause.Visible = false;
            btnEmpireSummary.Visible = false;
            btnExpansionPlanner.Visible = false;
            btnGameMenu.Visible = false;
            btnHistoryMessages.Visible = false;
            btnGalacticHistory.Visible = false;
            btnEmpireGraphs.Visible = false;
            btnGameEditor.Visible = false;
            btnEmpirePolicy.Visible = false;
            btnBuildOrder.Visible = false;
            tbtnBuiltObjects.Visible = false;
            tbtnColonies.Visible = false;
            tbtnConstructionYards.Visible = false;
            tbtnDesigns.Visible = false;
            tbtnEmpires.Visible = false;
            tbtnGalaxyMap.Visible = false;
            tbtnIntelligenceAgents.Visible = false;
            tbtnResearch.Visible = false;
            tbtnShipGroups.Visible = false;
            tbtnTroops.Visible = false;
            btnCycleBases.Visible = false;
            btnCycleColonies.Visible = false;
            btnCycleConstruction.Visible = false;
            btnCycleIdleShips.Visible = false;
            btnCycleMilitary.Visible = false;
            btnCycleOther.Visible = false;
            btnCycleShipGroups.Visible = false;
            btnCycleBasesBack.Visible = false;
            btnCycleColoniesBack.Visible = false;
            btnCycleConstructionBack.Visible = false;
            btnCycleIdleShipsBack.Visible = false;
            btnCycleMilitaryBack.Visible = false;
            btnCycleOtherBack.Visible = false;
            btnCycleShipGroupsBack.Visible = false;
            btnLockView.Visible = false;
            btnSelectNearestMilitary.Visible = false;
            btnCycleShipStance.Visible = false;
            btnSelectionAction1.Visible = false;
            btnSelectionAction2.Visible = false;
            btnSelectionAction3.Visible = false;
            btnSelectionAction4.Visible = false;
            btnSelectionAction5.Visible = false;
            btnSelectionAction6.Visible = false;
            btnSelectionAction7.Visible = false;
            btnSelectionAction8.Visible = false;
            lstMessages.SendToBack();
            btnPlayPause.SendToBack();
            btnEmpireSummary.SendToBack();
            btnExpansionPlanner.SendToBack();
            btnGameMenu.SendToBack();
            btnHistoryMessages.SendToBack();
            btnGalacticHistory.SendToBack();
            btnEmpireGraphs.SendToBack();
            btnGameEditor.SendToBack();
            btnEmpirePolicy.SendToBack();
            btnBuildOrder.SendToBack();
            tbtnBuiltObjects.SendToBack();
            tbtnColonies.SendToBack();
            tbtnConstructionYards.SendToBack();
            tbtnDesigns.SendToBack();
            tbtnEmpires.SendToBack();
            tbtnGalaxyMap.SendToBack();
            tbtnIntelligenceAgents.SendToBack();
            tbtnResearch.SendToBack();
            tbtnShipGroups.SendToBack();
            tbtnTroops.SendToBack();
            btnCycleBases.SendToBack();
            btnCycleColonies.SendToBack();
            btnCycleConstruction.SendToBack();
            btnCycleIdleShips.SendToBack();
            btnCycleMilitary.SendToBack();
            btnCycleOther.SendToBack();
            btnCycleShipGroups.SendToBack();
            btnCycleBasesBack.SendToBack();
            btnCycleColoniesBack.SendToBack();
            btnCycleConstructionBack.SendToBack();
            btnCycleIdleShipsBack.SendToBack();
            btnCycleMilitaryBack.SendToBack();
            btnCycleOtherBack.SendToBack();
            btnCycleShipGroupsBack.SendToBack();
            btnLockView.SendToBack();
            btnSelectNearestMilitary.SendToBack();
            btnCycleShipStance.SendToBack();
            btnSelectionAction1.SendToBack();
            btnSelectionAction2.SendToBack();
            btnSelectionAction3.SendToBack();
            btnSelectionAction4.SendToBack();
            btnSelectionAction5.SendToBack();
            btnSelectionAction6.SendToBack();
            btnSelectionAction7.SendToBack();
            btnSelectionAction8.SendToBack();
            itemListCollectionPanel_0.Visible = false;
        }

        private void method_473()
        {
            pnlInfoPanel.BringToFront();
            pnlDetailInfo.BringToFront();
            btnSelectionBack.BringToFront();
            btnSelectionForward.BringToFront();
            pnlSystemMap.BringToFront();
            picSystem.BringToFront();
            btnZoomColony.BringToFront();
            btnZoomIn.BringToFront();
            btnZoomOut.BringToFront();
            btnZoomRegion.BringToFront();
            jQaYpdpkDs.BringToFront();
            btnZoomSystem.BringToFront();
            btnZoomSelection.BringToFront();
            btnSelectNearestMilitary.BringToFront();
            btnCycleShipStance.BringToFront();
            btnHelp.BringToFront();
            btnSelectionPanelSize.BringToFront();
            btnMapCivilianFade.BringToFront();
            btnMapOverlay1.BringToFront();
            btnMapOverlay2.BringToFront();
            btnMapOverlay3.BringToFront();
            btnMapOverlay4.BringToFront();
            btnMapOverlay5.BringToFront();
            btnMapOverlay6.BringToFront();
            btnMapOverlay7.BringToFront();
            btnMapOverlay8.BringToFront();
            btnMapCivilianFade.Visible = true;
            btnMapOverlay1.Visible = true;
            btnMapOverlay2.Visible = true;
            btnMapOverlay3.Visible = true;
            btnMapOverlay4.Visible = true;
            btnMapOverlay5.Visible = true;
            btnMapOverlay6.Visible = true;
            btnMapOverlay7.Visible = true;
            btnMapOverlay8.Visible = true;
            diplomaticMessageQueue_0.Visible = true;
            itemListCollectionPanel_0.Visible = true;
            pnlInfoPanel.Visible = true;
            pnlDetailInfo.Visible = true;
            btnSelectionBack.Visible = true;
            btnSelectionForward.Visible = true;
            pnlSystemMap.Visible = true;
            picSystem.Visible = true;
            btnZoomColony.Visible = true;
            btnZoomIn.Visible = true;
            btnZoomOut.Visible = true;
            btnZoomRegion.Visible = true;
            jQaYpdpkDs.Visible = true;
            btnZoomSystem.Visible = true;
            btnZoomSelection.Visible = true;
            btnSelectNearestMilitary.Visible = true;
            btnHelp.Visible = true;
            btnSelectionPanelSize.Visible = true;
            btnGameSpeedIncrease.Visible = true;
            btnGameSpeedDecrease.Visible = true;
            btnGameSpeedIncrease.BringToFront();
            btnGameSpeedDecrease.BringToFront();
            lstMessages.BringToFront();
            btnPlayPause.BringToFront();
            btnEmpireSummary.BringToFront();
            btnExpansionPlanner.BringToFront();
            btnGameMenu.BringToFront();
            btnHistoryMessages.BringToFront();
            btnGalacticHistory.BringToFront();
            btnEmpireGraphs.BringToFront();
            btnGameEditor.BringToFront();
            btnEmpirePolicy.BringToFront();
            btnBuildOrder.BringToFront();
            tbtnBuiltObjects.BringToFront();
            tbtnColonies.BringToFront();
            tbtnConstructionYards.BringToFront();
            tbtnDesigns.BringToFront();
            tbtnEmpires.BringToFront();
            tbtnGalaxyMap.BringToFront();
            tbtnIntelligenceAgents.BringToFront();
            tbtnResearch.BringToFront();
            tbtnShipGroups.BringToFront();
            tbtnTroops.BringToFront();
            btnCycleBases.BringToFront();
            btnCycleColonies.BringToFront();
            btnCycleConstruction.BringToFront();
            btnCycleIdleShips.BringToFront();
            btnCycleMilitary.BringToFront();
            btnCycleOther.BringToFront();
            btnCycleShipGroups.BringToFront();
            btnCycleBasesBack.BringToFront();
            btnCycleColoniesBack.BringToFront();
            btnCycleConstructionBack.BringToFront();
            btnCycleIdleShipsBack.BringToFront();
            btnCycleMilitaryBack.BringToFront();
            btnCycleOtherBack.BringToFront();
            btnCycleShipGroupsBack.BringToFront();
            btnLockView.BringToFront();
            btnSelectionAction1.BringToFront();
            btnSelectionAction2.BringToFront();
            btnSelectionAction3.BringToFront();
            btnSelectionAction4.BringToFront();
            btnSelectionAction5.BringToFront();
            btnSelectionAction6.BringToFront();
            btnSelectionAction7.BringToFront();
            btnSelectionAction8.BringToFront();
            lstMessages.Visible = true;
            btnPlayPause.Visible = true;
            btnEmpireSummary.Visible = true;
            btnExpansionPlanner.Visible = true;
            btnGameMenu.Visible = true;
            btnHistoryMessages.Visible = true;
            btnGalacticHistory.Visible = true;
            btnEmpireGraphs.Visible = true;
            btnGameEditor.Visible = true;
            btnEmpirePolicy.Visible = true;
            btnBuildOrder.Visible = true;
            tbtnBuiltObjects.Visible = true;
            tbtnColonies.Visible = true;
            tbtnConstructionYards.Visible = true;
            tbtnDesigns.Visible = true;
            tbtnEmpires.Visible = true;
            tbtnGalaxyMap.Visible = true;
            tbtnIntelligenceAgents.Visible = true;
            tbtnResearch.Visible = true;
            tbtnShipGroups.Visible = true;
            tbtnTroops.Visible = true;
            btnCycleBases.Visible = true;
            btnCycleColonies.Visible = true;
            btnCycleConstruction.Visible = true;
            btnCycleIdleShips.Visible = true;
            btnCycleMilitary.Visible = true;
            btnCycleOther.Visible = true;
            btnCycleShipGroups.Visible = true;
            btnCycleBasesBack.Visible = true;
            btnCycleColoniesBack.Visible = true;
            btnCycleConstructionBack.Visible = true;
            btnCycleIdleShipsBack.Visible = true;
            btnCycleMilitaryBack.Visible = true;
            btnCycleOtherBack.Visible = true;
            btnCycleShipGroupsBack.Visible = true;
            btnLockView.Visible = true;
            btnSelectionAction1.Visible = true;
            btnSelectionAction2.Visible = true;
            btnSelectionAction3.Visible = true;
            btnSelectionAction4.Visible = true;
            btnSelectionAction5.Visible = true;
            btnSelectionAction6.Visible = true;
            btnSelectionAction7.Visible = true;
            btnSelectionAction8.Visible = true;
            itemListCollectionPanel_0.Visible = true;
        }

        private void method_474()
        {
            method_471();
            lstMessages.Visible = false;
            btnPlayPause.Visible = false;
            btnEmpireSummary.Visible = false;
            btnExpansionPlanner.Visible = false;
            btnGameMenu.Visible = false;
            btnHistoryMessages.Visible = false;
            btnGalacticHistory.Visible = false;
            btnEmpireGraphs.Visible = false;
            btnGameEditor.Visible = false;
            btnEmpirePolicy.Visible = false;
            btnBuildOrder.Visible = false;
            tbtnBuiltObjects.Visible = false;
            tbtnColonies.Visible = false;
            tbtnConstructionYards.Visible = false;
            tbtnDesigns.Visible = false;
            tbtnEmpires.Visible = false;
            tbtnGalaxyMap.Visible = false;
            tbtnIntelligenceAgents.Visible = false;
            tbtnResearch.Visible = false;
            tbtnShipGroups.Visible = false;
            tbtnTroops.Visible = false;
            btnSelectNearestMilitary.Visible = false;
            btnCycleShipStance.Visible = false;
            btnCycleBases.Visible = false;
            btnCycleColonies.Visible = false;
            btnCycleConstruction.Visible = false;
            btnCycleIdleShips.Visible = false;
            btnCycleMilitary.Visible = false;
            btnCycleOther.Visible = false;
            btnCycleShipGroups.Visible = false;
            btnCycleBasesBack.Visible = false;
            btnCycleColoniesBack.Visible = false;
            btnCycleConstructionBack.Visible = false;
            btnCycleIdleShipsBack.Visible = false;
            btnCycleMilitaryBack.Visible = false;
            btnCycleOtherBack.Visible = false;
            btnCycleShipGroupsBack.Visible = false;
            btnLockView.Visible = false;
            btnSelectionAction1.Visible = false;
            btnSelectionAction2.Visible = false;
            btnSelectionAction3.Visible = false;
            btnSelectionAction4.Visible = false;
            btnSelectionAction5.Visible = false;
            btnSelectionAction6.Visible = false;
            btnSelectionAction7.Visible = false;
            btnSelectionAction8.Visible = false;
            btnSelectionPanelSize.Visible = false;
            btnGameSpeedIncrease.Visible = false;
            btnGameSpeedDecrease.Visible = false;
            btnGameSpeedIncrease.SendToBack();
            btnGameSpeedDecrease.SendToBack();
            btnHelp.Visible = false;
            btnHelp.SendToBack();
            lstMessages.SendToBack();
            btnPlayPause.SendToBack();
            btnEmpireSummary.SendToBack();
            btnExpansionPlanner.SendToBack();
            btnGameMenu.SendToBack();
            btnHistoryMessages.SendToBack();
            btnGalacticHistory.SendToBack();
            btnEmpireGraphs.SendToBack();
            btnGameEditor.SendToBack();
            btnEmpirePolicy.SendToBack();
            btnBuildOrder.SendToBack();
            tbtnBuiltObjects.SendToBack();
            tbtnColonies.SendToBack();
            tbtnConstructionYards.SendToBack();
            tbtnDesigns.SendToBack();
            tbtnEmpires.SendToBack();
            tbtnGalaxyMap.SendToBack();
            tbtnIntelligenceAgents.SendToBack();
            tbtnResearch.SendToBack();
            tbtnShipGroups.SendToBack();
            tbtnTroops.SendToBack();
            btnSelectNearestMilitary.SendToBack();
            btnCycleShipStance.SendToBack();
            btnCycleBases.SendToBack();
            btnCycleColonies.SendToBack();
            btnCycleConstruction.SendToBack();
            btnCycleIdleShips.SendToBack();
            btnCycleMilitary.SendToBack();
            btnCycleOther.SendToBack();
            btnCycleShipGroups.SendToBack();
            btnCycleBasesBack.SendToBack();
            btnCycleColoniesBack.SendToBack();
            btnCycleConstructionBack.SendToBack();
            btnCycleIdleShipsBack.SendToBack();
            btnCycleMilitaryBack.SendToBack();
            btnCycleOtherBack.SendToBack();
            btnCycleShipGroupsBack.SendToBack();
            btnLockView.SendToBack();
            btnSelectionAction1.SendToBack();
            btnSelectionAction2.SendToBack();
            btnSelectionAction3.SendToBack();
            btnSelectionAction4.SendToBack();
            btnSelectionAction5.SendToBack();
            btnSelectionAction6.SendToBack();
            btnSelectionAction7.SendToBack();
            btnSelectionAction8.SendToBack();
            btnSelectionPanelSize.SendToBack();
            itemListCollectionPanel_0.Visible = false;
            diplomaticMessageQueue_0.Visible = false;
        }

        private void method_475()
        {
            lstMessages.BringToFront();
            btnPlayPause.BringToFront();
            btnEmpireSummary.BringToFront();
            btnExpansionPlanner.BringToFront();
            btnGameMenu.BringToFront();
            btnHistoryMessages.BringToFront();
            btnGalacticHistory.BringToFront();
            btnEmpireGraphs.BringToFront();
            btnGameEditor.BringToFront();
            btnEmpirePolicy.BringToFront();
            btnBuildOrder.BringToFront();
            tbtnBuiltObjects.BringToFront();
            tbtnColonies.BringToFront();
            tbtnConstructionYards.BringToFront();
            tbtnDesigns.BringToFront();
            tbtnEmpires.BringToFront();
            tbtnGalaxyMap.BringToFront();
            tbtnIntelligenceAgents.BringToFront();
            tbtnResearch.BringToFront();
            tbtnShipGroups.BringToFront();
            tbtnTroops.BringToFront();
            btnSelectNearestMilitary.BringToFront();
            btnCycleShipStance.BringToFront();
            btnCycleBases.BringToFront();
            btnCycleColonies.BringToFront();
            btnCycleConstruction.BringToFront();
            btnCycleIdleShips.BringToFront();
            btnCycleMilitary.BringToFront();
            btnCycleOther.BringToFront();
            btnCycleShipGroups.BringToFront();
            btnCycleBasesBack.BringToFront();
            btnCycleColoniesBack.BringToFront();
            btnCycleConstructionBack.BringToFront();
            btnCycleIdleShipsBack.BringToFront();
            btnCycleMilitaryBack.BringToFront();
            btnCycleOtherBack.BringToFront();
            btnCycleShipGroupsBack.BringToFront();
            btnLockView.BringToFront();
            btnSelectionAction1.BringToFront();
            btnSelectionAction2.BringToFront();
            btnSelectionAction3.BringToFront();
            btnSelectionAction4.BringToFront();
            btnSelectionAction5.BringToFront();
            btnSelectionAction6.BringToFront();
            btnSelectionAction7.BringToFront();
            btnSelectionAction8.BringToFront();
            btnSelectionPanelSize.BringToFront();
            btnGameSpeedIncrease.Visible = true;
            btnGameSpeedDecrease.Visible = true;
            btnGameSpeedIncrease.BringToFront();
            btnGameSpeedDecrease.BringToFront();
            btnHelp.Visible = true;
            btnHelp.BringToFront();
            lstMessages.Visible = true;
            btnPlayPause.Visible = true;
            btnEmpireSummary.Visible = true;
            btnExpansionPlanner.Visible = true;
            btnGameMenu.Visible = true;
            btnHistoryMessages.Visible = true;
            btnGalacticHistory.Visible = true;
            btnEmpireGraphs.Visible = true;
            btnGameEditor.Visible = true;
            btnEmpirePolicy.Visible = true;
            btnBuildOrder.Visible = true;
            tbtnBuiltObjects.Visible = true;
            tbtnColonies.Visible = true;
            tbtnConstructionYards.Visible = true;
            tbtnDesigns.Visible = true;
            tbtnEmpires.Visible = true;
            tbtnGalaxyMap.Visible = true;
            tbtnIntelligenceAgents.Visible = true;
            tbtnResearch.Visible = true;
            tbtnShipGroups.Visible = true;
            tbtnTroops.Visible = true;
            btnSelectNearestMilitary.Visible = true;
            btnCycleBases.Visible = true;
            btnCycleColonies.Visible = true;
            btnCycleConstruction.Visible = true;
            btnCycleIdleShips.Visible = true;
            btnCycleMilitary.Visible = true;
            btnCycleOther.Visible = true;
            btnCycleShipGroups.Visible = true;
            btnCycleBasesBack.Visible = true;
            btnCycleColoniesBack.Visible = true;
            btnCycleConstructionBack.Visible = true;
            btnCycleIdleShipsBack.Visible = true;
            btnCycleMilitaryBack.Visible = true;
            btnCycleOtherBack.Visible = true;
            btnCycleShipGroupsBack.Visible = true;
            btnLockView.Visible = true;
            btnSelectionAction1.Visible = true;
            btnSelectionAction2.Visible = true;
            btnSelectionAction3.Visible = true;
            btnSelectionAction4.Visible = true;
            btnSelectionAction5.Visible = true;
            btnSelectionAction6.Visible = true;
            btnSelectionAction7.Visible = true;
            btnSelectionAction8.Visible = true;
            btnSelectionPanelSize.Visible = true;
            itemListCollectionPanel_0.Visible = true;
            diplomaticMessageQueue_0.Visible = true;
        }

        private void method_476()
        {
            galaxyTimeState_0 = _Game.Galaxy.TimeState;
            method_474();
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            _Game.GodMode = true;
            pnlGameEditor.Size = new Size(340, 707);
            pnlGameEditor.Location = new Point(10, 10);
            lblGameEditorTitle.Font = font_1;
            lblGameEditorTitle.ForeColor = color_0;
            lblGameEditorTitle.BackColor = Color.Transparent;
            lblGameEditorTitle.Text = TextResolver.GetText("Game Editor");
            lblGameEditorTitle.Location = new Point(40, 8);
            picGameEditor.Location = new Point(10, 4);
            btnGameEditorExit.Font = new Font(font_3.FontFamily, 16.67f, FontStyle.Bold);
            btnGameEditorExit.Location = new Point(173, 8);
            btnGameEditorExit.Size = new Size(157, 25);
            gameEditorSelector.Size = new Size(320, 595);
            gameEditorSelector.Location = new Point(10, 38);
            mainView.nebulaCloudGenerator_1.GenerateNebulaBackdrop(1, out var cloudImages, out var _, 255, 26, 200, 200, transparentBackground: true, isGasCloud: true, useLowQuality: false);
            Bitmap gasCloudImage = PrecacheScaledBitmap(cloudImages[0], 40, 40, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            gameEditorSelector.BindData(mainView, _Game.Galaxy, _Game.Galaxy.Races, raceImageCache_0.GetRaceImages(), _Game.Galaxy.Empires, _Game.Galaxy.PirateEmpires, _Game.PlayerEmpire, _Game.Galaxy.IndependentEmpire, builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), bitmap_196, bitmap_206[0], gasCloudImage, bitmap_10, bitmap_2[0], bitmap_49, bitmap_135, bitmap_136, bitmap_137, bitmap_138, bitmap_139, bitmap_140, bitmap_141, bitmap_142, characterImageCache_0);
            btnGameEditorEditEmpires.Size = new Size(320, 25);
            btnGameEditorEditEmpires.Location = new Point(10, 640);
            btnGameEditorEditGalaxy.Size = new Size(320, 25);
            btnGameEditorEditGalaxy.Location = new Point(10, 672);
            btnGameEditorSave.Visible = false;
            btnGameEditorSaveAs.Visible = false;
            pnlGameEditor.Visible = true;
            pnlGameEditor.BringToFront();
        }

        private void method_477()
        {
            method_478(bool_28: true);
        }

        private void method_478(bool bool_28)
        {
            if (bool_28)
            {
                bool_11 = false;
                method_155();
            }
            gameEditorSelector.OpenPanel(gameEditorSelector.cpnSystem);
            method_488();
            method_494();
            method_482();
            method_501();
            method_496();
            empire_0 = null;
            _Game.GodMode = false;
            pnlGameEditor.SendToBack();
            pnlGameEditor.Visible = false;
            if (bool_28)
            {
                method_475();
            }
        }

        private void method_479()
        {
            pnlEditCreature.Size = new Size(300, 215);
            pnlEditCreature.Location = new Point(mainView.Width - pnlEditCreature.Width - 10, 120);
            lblEditCreatureTitle.Font = font_1;
            lblEditCreatureTitle.ForeColor = color_0;
            lblEditCreatureTitle.BackColor = Color.Transparent;
            lblEditCreatureTitle.Text = TextResolver.GetText("Edit Creature");
            lblEditCreatureTitle.Location = new Point(40, 10);
            picEditCreature.Location = new Point(10, 4);
            btnEditCreatureClose.Size = new Size(75, 25);
            btnEditCreatureClose.Location = new Point(pnlEditCreature.Width - 85, 10);
            btnEditCreatureClose.Text = TextResolver.GetText("Close");
            lblEditCreatureName.Location = new Point(10, 45);
            lblEditCreatureSize.Location = new Point(10, 75);
            lblEditCreatureDamage.Location = new Point(10, 105);
            lblEditCreatureAttackStrength.Location = new Point(10, 135);
            txtEditCreatureName.Size = new Size(220, 16);
            txtEditCreatureName.Location = new Point(70, 44);
            numEditCreatureSize.Location = new Point(70, 73);
            numEditCreatureDamage.Location = new Point(70, 103);
            numEditCreatureAttackStrength.Location = new Point(70, 133);
            chkEditCreatureVisible.Location = new Point(140, 73);
            chkEditCreatureAnchoredToParent.Location = new Point(140, 103);
            btnEditCreatureGameEvent.Size = new Size(100, 25);
            btnEditCreatureGameEvent.Location = new Point(10, 172);
            lblEditCreatureGameEvent.Location = new Point(115, 165);
            lblEditCreatureGameEvent.Font = font_7;
            lblEditCreatureGameEvent.MinimumSize = new Size(175, 40);
            lblEditCreatureGameEvent.MaximumSize = new Size(175, 40);
            lblEditCreatureGameEvent.Size = new Size(175, 40);
            lblEditCreatureGameEvent.TextAlign = ContentAlignment.MiddleLeft;
            method_480();
            txtEditCreatureName.Focus();
            txtEditCreatureName.SelectAll();
            pnlEditCreature.Visible = true;
            pnlEditCreature.BringToFront();
        }

        private void method_480()
        {
            if (_Game.SelectedObject is Creature)
            {
                Creature creature = (Creature)_Game.SelectedObject;
                txtEditCreatureName.MaxLength = 120;
                txtEditCreatureName.Text = creature.Name;
                numEditCreatureSize.Minimum = 10m;
                numEditCreatureSize.Maximum = creature.MaxSize;
                numEditCreatureSize.Value = creature.Size;
                numEditCreatureDamage.Minimum = 0m;
                numEditCreatureDamage.Maximum = creature.DamageKillThreshhold - 1;
                numEditCreatureDamage.Value = (decimal)creature.Damage;
                numEditCreatureAttackStrength.Minimum = 0m;
                numEditCreatureAttackStrength.Maximum = creature.MaxSize / 10;
                numEditCreatureAttackStrength.Value = creature.AttackStrength;
                chkEditCreatureAnchoredToParent.Checked = creature.LocationLocked;
                if (creature.CanHide)
                {
                    chkEditCreatureVisible.Visible = true;
                    chkEditCreatureVisible.Checked = creature.IsVisible;
                }
                else
                {
                    chkEditCreatureVisible.Visible = false;
                }
                creature_1 = creature;
            }
            method_677();
        }

        private void method_481()
        {
            if (creature_1 != null)
            {
                creature_1.Name = txtEditCreatureName.Text;
                creature_1.Size = (int)numEditCreatureSize.Value;
                creature_1.Damage = (double)numEditCreatureDamage.Value;
                creature_1.AttackStrength = (int)numEditCreatureAttackStrength.Value;
                creature_1.LocationLocked = chkEditCreatureAnchoredToParent.Checked;
                if (creature_1.CanHide)
                {
                    creature_1.IsVisible = chkEditCreatureVisible.Checked;
                }
                creature_1 = null;
            }
        }

        private void method_482()
        {
            method_481();
            pnlDetailInfo.Invalidate();
            bool_19 = true;
            method_149();
            pnlEditCreature.SendToBack();
            pnlEditCreature.Visible = false;
        }

        private void pnlEditCreature_Leave(object sender, EventArgs e)
        {
        }

        private void PilQidafZH()
        {
            pnlEditBuiltObject.Size = new Size(300, 450);
            pnlEditBuiltObject.Location = new Point(mainView.Width - pnlEditBuiltObject.Width - 10, 120);
            lblEditBuiltObjectTitle.Font = font_1;
            lblEditBuiltObjectTitle.ForeColor = color_0;
            lblEditBuiltObjectTitle.BackColor = Color.Transparent;
            lblEditBuiltObjectTitle.Text = TextResolver.GetText("Edit Ship or Base");
            lblEditBuiltObjectTitle.Location = new Point(44, 12);
            picEditBuiltObject.Location = new Point(10, 6);
            btnEditBuiltObjectClose.Size = new Size(75, 25);
            btnEditBuiltObjectClose.Location = new Point(pnlEditBuiltObject.Width - 85, 10);
            btnEditBuiltObjectClose.Text = TextResolver.GetText("Close");
            lblEditBuiltObjectName.Location = new Point(10, 45);
            lblEditBuiltObjectEmpire.Location = new Point(10, 75);
            lblEditBuiltObjectEncounterEvent.Location = new Point(10, 105);
            lblEditBuiltObjectEncounterEvent.Text = TextResolver.GetText("Encounter");
            lblEditBuiltObjectFleet.Location = new Point(10, 135);
            lblEditBuiltObjectFuel.Location = new Point(10, 165);
            lblEditBuiltObjectTroops.Location = new Point(10, 195);
            lblEditBuiltObjectTroops.SendToBack();
            txtEditBuiltObjectName.Size = new Size(220, 16);
            txtEditBuiltObjectName.Location = new Point(70, 44);
            cmbEditBuiltObjectEmpire.Size = new Size(220, 21);
            cmbEditBuiltObjectEmpire.Location = new Point(70, 73);
            cmbEditBuiltObjectEncounterEvent.Size = new Size(220, 21);
            cmbEditBuiltObjectEncounterEvent.Location = new Point(70, 103);
            cmbEditBuiltObjectFleet.Size = new Size(220, 21);
            cmbEditBuiltObjectFleet.Location = new Point(70, 133);
            numEditBuiltObjectFuel.Location = new Point(70, 163);
            chkEditBuiltObjectAutomated.Location = new Point(170, 163);
            ctlEditBuiltObjectTroops.Grid.Columns["Location"].Visible = false;
            ctlEditBuiltObjectTroops.Size = new Size(280, 175);
            ctlEditBuiltObjectTroops.Location = new Point(10, 190);
            ctlEditBuiltObjectTroops.Grid.Columns["Name"].Width = 110;
            ctlEditBuiltObjectTroops.Grid.Columns["Size"].Width = 50;
            ctlEditBuiltObjectTroops.Grid.Columns["AttackStrength"].Width = 60;
            ctlEditBuiltObjectTroops.Grid.Columns["DefendStrength"].Width = 60;
            cmbEditBuiltObjectAddTroop.Size = new Size(140, 21);
            cmbEditBuiltObjectAddTroop.Location = new Point(10, 370);
            cmbEditBuiltObjectAddTroop.DropDownWidth = 350;
            btnEditBuiltObjectAddTroop.Size = new Size(50, 25);
            btnEditBuiltObjectAddTroop.Location = new Point(153, 370);
            btnEditBuiltObjectAddTroop.Text = TextResolver.GetText("Add");
            btnEditBuiltObjectRemoveTroop.Size = new Size(75, 25);
            btnEditBuiltObjectRemoveTroop.Location = new Point(215, 370);
            btnEditBuiltObjectRemoveTroop.Text = TextResolver.GetText("Remove");
            btnEditBuiltObjectGameEvent.Size = new Size(100, 25);
            btnEditBuiltObjectGameEvent.Location = new Point(10, 412);
            lblEditBuiltObjectGameEvent.Location = new Point(115, 405);
            lblEditBuiltObjectGameEvent.MinimumSize = new Size(175, 40);
            lblEditBuiltObjectGameEvent.MaximumSize = new Size(175, 40);
            lblEditBuiltObjectGameEvent.Size = new Size(175, 40);
            lblEditBuiltObjectGameEvent.Font = font_7;
            lblEditBuiltObjectGameEvent.TextAlign = ContentAlignment.MiddleLeft;
            method_483();
            txtEditBuiltObjectName.Focus();
            txtEditBuiltObjectName.SelectAll();
            pnlEditBuiltObject.Visible = true;
            pnlEditBuiltObject.BringToFront();
        }

        private void method_483()
        {
            if (_Game.SelectedObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)_Game.SelectedObject;
                txtEditBuiltObjectName.MaxLength = 120;
                txtEditBuiltObjectName.Text = builtObject.Name;
                cmbEditBuiltObjectEmpire.BindData(_Game.PlayerEmpire, _Game.Galaxy.Empires, _Game.Galaxy.PirateEmpires, _Game.Galaxy.IndependentEmpire, includeNoEmpire: true);
                cmbEditBuiltObjectEmpire.SetSelectedEmpire(builtObject.Empire);
                if (builtObject.Empire != null)
                {
                    cmbEditBuiltObjectFleet.BindData(builtObject.Empire.ShipGroups, provideNullSelection: true, _Game.Galaxy);
                }
                else
                {
                    cmbEditBuiltObjectFleet.BindData(null, provideNullSelection: true, _Game.Galaxy);
                }
                method_485(builtObject.Empire, builtObject.EncounterEventType);
                cmbEditBuiltObjectFleet.SetSelectedFleet(builtObject.ShipGroup);
                if (builtObject.Role == BuiltObjectRole.Military)
                {
                    cmbEditBuiltObjectFleet.Enabled = true;
                }
                else
                {
                    cmbEditBuiltObjectFleet.Enabled = false;
                }
                numEditBuiltObjectFuel.Minimum = 0m;
                numEditBuiltObjectFuel.Maximum = (decimal)builtObject.FuelCapacity + 0.5m;
                if (builtObject.CurrentFuel > (double)builtObject.FuelCapacity)
                {
                    builtObject.CurrentFuel = builtObject.FuelCapacity;
                }
                numEditBuiltObjectFuel.Value = (decimal)builtObject.CurrentFuel;
                chkEditBuiltObjectAutomated.Checked = builtObject.IsAutoControlled;
                ctlEditBuiltObjectTroops.BindData(builtObject.Troops, editable: true);
                TroopList troopList = Galaxy.GenerateDefaultTroops(_Game.Galaxy);
                cmbEditBuiltObjectAddTroop.BindData(font_6, troopList, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27);
                if (builtObject.Empire != null && builtObject.Empire.DominantRace != null)
                {
                    Troop firstByTypeAndRace = troopList.GetFirstByTypeAndRace(TroopType.Infantry, builtObject.Empire.DominantRace);
                    if (firstByTypeAndRace != null)
                    {
                        cmbEditBuiltObjectAddTroop.SetSelectedTroop(firstByTypeAndRace);
                    }
                }
                builtObject_7 = builtObject;
            }
            method_677();
        }

        private void method_484()
        {
            if (builtObject_7 == null)
            {
                return;
            }
            builtObject_7.Name = txtEditBuiltObjectName.Text;
            if (ctlEditBuiltObjectTroops.Grid.EditingControl != null)
            {
                ctlEditBuiltObjectTroops.Grid.EndEdit();
            }
            builtObject_7.Troops = ctlEditBuiltObjectTroops.Troops;
            ShipGroup selectedFleet = cmbEditBuiltObjectFleet.SelectedFleet;
            if (selectedFleet != null)
            {
                selectedFleet.AddShipToFleet(builtObject_7);
            }
            else
            {
                builtObject_7.LeaveShipGroup();
            }
            builtObject_7.CurrentFuel = (double)numEditBuiltObjectFuel.Value;
            builtObject_7.IsAutoControlled = chkEditBuiltObjectAutomated.Checked;
            if (!builtObject_7.IsAutoControlled && builtObject_7.Empire != null && builtObject_7.Empire.NewBuiltObjectShouldBeAutomated(builtObject_7.SubRole))
            {
                builtObject_7.IsAutoControlled = true;
            }
            if (cmbEditBuiltObjectEmpire.SelectedEmpire != builtObject_7.Empire)
            {
                Empire selectedEmpire = cmbEditBuiltObjectEmpire.SelectedEmpire;
                if (selectedEmpire != null)
                {
                    selectedEmpire.TakeOwnershipOfBuiltObject(builtObject_7, selectedEmpire);
                }
                else if (builtObject_7.Empire != null)
                {
                    builtObject_7.Empire.TakeOwnershipOfBuiltObject(builtObject_7, null);
                }
            }
            if (builtObject_7.Empire == null)
            {
                builtObject_7.EncounterEventType = method_487();
            }
            else
            {
                builtObject_7.EncounterEventType = BuiltObjectEncounterEventType.Acquire;
            }
            builtObject_7 = null;
        }

        private void method_485(Empire empire_5, BuiltObjectEncounterEventType builtObjectEncounterEventType_0)
        {
            if (empire_5 != null)
            {
                lblEditBuiltObjectEncounterEvent.Visible = false;
                cmbEditBuiltObjectEncounterEvent.Visible = false;
                return;
            }
            lblEditBuiltObjectEncounterEvent.Visible = true;
            lblEditBuiltObjectEncounterEvent.BringToFront();
            cmbEditBuiltObjectEncounterEvent.Visible = true;
            cmbEditBuiltObjectEncounterEvent.BringToFront();
            cmbEditBuiltObjectEncounterEvent.Items.Clear();
            cmbEditBuiltObjectEncounterEvent.Items.AddRange(new string[3]
            {
            TextResolver.GetText("Acquire"),
            TextResolver.GetText("Explodes"),
            TextResolver.GetText("Pirate Ambush")
            });
            method_486(builtObjectEncounterEventType_0);
        }

        private void method_486(BuiltObjectEncounterEventType builtObjectEncounterEventType_0)
        {
            switch (builtObjectEncounterEventType_0)
            {
                case BuiltObjectEncounterEventType.Acquire:
                    cmbEditBuiltObjectEncounterEvent.SelectedIndex = 0;
                    break;
                case BuiltObjectEncounterEventType.PirateAmbush:
                    cmbEditBuiltObjectEncounterEvent.SelectedIndex = 2;
                    break;
                case BuiltObjectEncounterEventType.Explodes:
                    cmbEditBuiltObjectEncounterEvent.SelectedIndex = 1;
                    break;
            }
        }

        private BuiltObjectEncounterEventType method_487()
        {
            BuiltObjectEncounterEventType result = BuiltObjectEncounterEventType.Acquire;
            string text = cmbEditBuiltObjectEncounterEvent.SelectedItem.ToString();
            if (text == TextResolver.GetText("Acquire"))
            {
                result = BuiltObjectEncounterEventType.Acquire;
            }
            else if (text == TextResolver.GetText("Explodes"))
            {
                result = BuiltObjectEncounterEventType.Explodes;
            }
            else if (text == TextResolver.GetText("Pirate Ambush"))
            {
                result = BuiltObjectEncounterEventType.PirateAmbush;
            }
            return result;
        }

        private void method_488()
        {
            method_484();
            pnlDetailInfo.Invalidate();
            bool_19 = true;
            method_149();
            pnlEditBuiltObject.SendToBack();
            pnlEditBuiltObject.Visible = false;
        }

        private void cmbEditBuiltObjectEmpire_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empire selectedEmpire = cmbEditBuiltObjectEmpire.SelectedEmpire;
            if (selectedEmpire != null)
            {
                cmbEditBuiltObjectFleet.BindData(selectedEmpire.ShipGroups, provideNullSelection: true, _Game.Galaxy);
                if (selectedEmpire != _Game.Galaxy.IndependentEmpire && !_Game.Galaxy.PirateEmpires.Contains(selectedEmpire))
                {
                    ctlEditBuiltObjectTroops.Enabled = true;
                    btnEditBuiltObjectAddTroop.Enabled = true;
                    btnEditBuiltObjectRemoveTroop.Enabled = true;
                }
                else
                {
                    if (builtObject_7 != null)
                    {
                        if (builtObject_7.Troops != null)
                        {
                            builtObject_7.Troops.Clear();
                        }
                        ctlEditBuiltObjectTroops.BindData(builtObject_7.Troops, editable: true);
                    }
                    ctlEditBuiltObjectTroops.Enabled = false;
                    btnEditBuiltObjectAddTroop.Enabled = false;
                    btnEditBuiltObjectRemoveTroop.Enabled = false;
                }
            }
            else
            {
                cmbEditBuiltObjectFleet.BindData(null, provideNullSelection: true, _Game.Galaxy);
                if (builtObject_7 != null)
                {
                    if (builtObject_7.Troops != null)
                    {
                        builtObject_7.Troops.Clear();
                    }
                    ctlEditBuiltObjectTroops.BindData(builtObject_7.Troops, editable: true);
                }
                ctlEditBuiltObjectTroops.Enabled = false;
                btnEditBuiltObjectAddTroop.Enabled = false;
                btnEditBuiltObjectRemoveTroop.Enabled = false;
            }
            if (builtObject_7 != null)
            {
                method_485(selectedEmpire, builtObject_7.EncounterEventType);
            }
        }

        private void btnEditBuiltObjectAddTroop_Click(object sender, EventArgs e)
        {
            Empire selectedEmpire = cmbEditBuiltObjectEmpire.SelectedEmpire;
            if (builtObject_7 == null || selectedEmpire == null || selectedEmpire == _Game.Galaxy.IndependentEmpire || _Game.Galaxy.PirateEmpires.Contains(selectedEmpire))
            {
                return;
            }
            builtObject_7.Troops = ctlEditBuiltObjectTroops.Troops;
            if (builtObject_7.TroopCapacityRemaining >= 100)
            {
                Troop selectedTroop = cmbEditBuiltObjectAddTroop.SelectedTroop;
                if (selectedTroop != null && selectedTroop.Race != null)
                {
                    double num = selectedTroop.Race.TroopStrength;
                    string name = selectedEmpire.GenerateTroopDescription(selectedTroop.Name);
                    Troop troop = Galaxy.GenerateNewTroop(name, selectedTroop.Type, (int)num, selectedEmpire, selectedTroop.Race);
                    troop.BuiltObject = builtObject_7;
                    builtObject_7.Troops.Add(troop);
                    selectedEmpire.Troops.Add(troop);
                }
            }
            ctlEditBuiltObjectTroops.BindData(builtObject_7.Troops, editable: true);
        }

        private void btnEditBuiltObjectRemoveTroop_Click(object sender, EventArgs e)
        {
            if (builtObject_7 != null && ctlEditBuiltObjectTroops.SelectedTroop != null)
            {
                builtObject_7.Troops = ctlEditBuiltObjectTroops.Troops;
                builtObject_7.Troops.Remove(ctlEditBuiltObjectTroops.SelectedTroop);
                ctlEditBuiltObjectTroops.BindData(builtObject_7.Troops, editable: true);
            }
        }

        private void pnlEditBuiltObject_Leave(object sender, EventArgs e)
        {
        }

        private void method_489()
        {
            lblEditHabitatName.Visible = true;
            txtEditHabitatName.Visible = true;
            lblEditHabitatSize.Visible = true;
            numEditHabitatSize.Visible = true;
            lblEditHabitatDevelopmentLevel.Visible = true;
            numEditHabitatDevelopmentLevel.Visible = true;
            lblEditHabitatEmpire.Visible = true;
            cmbEditHabitatEmpire.Visible = true;
            chkEditHabitatCapital.Visible = true;
            lblEditHabitatSolarRadiation.Visible = true;
            numEditHabitatSolarRadiation.Visible = true;
            lblEditHabitatMicrowaveRadiation.Visible = true;
            numEditHabitatMicrowaveRadiation.Visible = true;
            lblEditHabitatXrayRadiation.Visible = true;
            numEditHabitatXrayRadiation.Visible = true;
            lblEditHabitatResources.Visible = true;
            ctlEditHabitatResources.Visible = true;
            lblEditHabitatPopulation.Visible = true;
            ctlEditHabitatPopulation.Visible = true;
            lblEditHabitatTroops.Visible = true;
            ctlEditHabitatTroops.Visible = true;
            cmbEditHabitatAddTroop.Visible = true;
            pnlEditRuinsContainer.Visible = true;
            picEditHabitatPicture.Visible = true;
            scrEditHabitatPicture.Visible = true;
            picEditHabitatPictureLandscape.Visible = true;
            scrEditHabitatPictureLandscape.Visible = true;
            cmbEditHabitatAddResource.Visible = true;
            btnEditHabitatAddResource.Visible = true;
            btnEditHabitatRemoveResource.Visible = true;
            lblEditHabitatQuality.Visible = true;
            numEditHabitatQuality.Visible = true;
            chkEditHabitatHasRings.Visible = true;
            lblEditHabitatScenery.Visible = true;
            numEditHabitatScenery.Visible = true;
            txtEditHabitatSceneryDescription.Visible = true;
            lblEditHabitatResearchBonus.Visible = true;
            ExUfgIlqCu.Visible = true;
            numEditHabitatResearchBonusAmount.Visible = true;
            lblEditHabitatPirateColonyControl.Visible = true;
            ctlEditHabitatPirateColonyControl.Visible = true;
            cmbEditHabitatAddPirateColonyControl.Visible = true;
            btnEditHabitatAddPirateColonyControl.Visible = true;
            btnEditHabitatRemovePirateColonyControl.Visible = true;
            lblEditHabitatPlanetaryFacilities.Visible = true;
            ctlEditHabitatPlanetaryFacilities.Visible = true;
            cmbEditHabitatAddPlanetaryFacility.Visible = true;
            btnEditHabitatAddPlanetaryFacility.Visible = true;
            btnEditHabitatRemovePlanetaryFacility.Visible = true;
        }

        private void method_490()
        {
            lblEditHabitatSolarRadiation.Visible = false;
            numEditHabitatSolarRadiation.Visible = false;
            lblEditHabitatMicrowaveRadiation.Visible = false;
            numEditHabitatMicrowaveRadiation.Visible = false;
            lblEditHabitatXrayRadiation.Visible = false;
            numEditHabitatXrayRadiation.Visible = false;
        }

        private void btnEditHabitatRemoveResource_Click(object sender, EventArgs e)
        {
            if (habitat_4 == null || habitat_4.Resources == null || habitat_4.Resources.Count <= 0)
            {
                return;
            }
            Resource selectedResource = ctlEditHabitatResources.SelectedResource;
            if (selectedResource == null)
            {
                return;
            }
            int num = -1;
            HabitatResourceList habitatResourceList = habitat_4.Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                if (habitatResourceList[i].ResourceID == selectedResource.ResourceID)
                {
                    num = i;
                    break;
                }
            }
            if (num < 0)
            {
                return;
            }
            HabitatResource[] array = habitat_4.Resources.ToArray();
            habitat_4.Resources.Clear();
            for (int j = 0; j < array.Length; j++)
            {
                if (j != num)
                {
                    habitat_4.Resources.Add(array[j]);
                }
            }
            ctlEditHabitatResources.BindData(habitat_4.Resources, _uiResourcesBitmaps, editable: true);
        }

        private void btnEditHabitatAddResource_Click(object sender, EventArgs e)
        {
            Resource selectedResource = cmbEditHabitatAddResource.SelectedResource;
            if (habitat_4 == null)
            {
                return;
            }
            if (habitat_4.Resources == null)
            {
                habitat_4.Resources = new HabitatResourceList();
            }
            if (selectedResource == null)
            {
                return;
            }
            bool flag = true;
            switch (selectedResource.Group)
            {
                case ResourceGroup.Mineral:
                    if (habitat_4.Resources.ContainsGroup(ResourceGroup.Gas))
                    {
                        flag = false;
                    }
                    break;
                case ResourceGroup.Gas:
                    if (habitat_4.Resources.ContainsGroup(ResourceGroup.Mineral))
                    {
                        flag = false;
                    }
                    break;
            }
            if (!flag)
            {
                string string_ = TextResolver.GetText("Cannot add both Mineral and Gas resources at same location");
                string string_2 = TextResolver.GetText("Invalid resource");
                MessageBoxEx messageBoxEx = method_371(string_, string_2, MessageBoxExIcon.Information);
                messageBoxEx.Show();
            }
            if (habitat_4.Resources.Count < 8 && flag)
            {
                habitat_4.Resources.Add(new HabitatResource(selectedResource.ResourceID, 500));
                ctlEditHabitatResources.BindData(habitat_4.Resources, _uiResourcesBitmaps, editable: true);
            }
        }

        private void btnEditHabitatAddPirateColonyControl_Click(object sender, EventArgs e)
        {
            if (habitat_4 != null && habitat_4.GetPirateControl() != null && habitat_4.GetPirateControl().Count < 3)
            {
                Empire selectedEmpire = cmbEditHabitatAddPirateColonyControl.SelectedEmpire;
                if (selectedEmpire != null && !habitat_4.GetPirateControl().CheckFactionHasControl(selectedEmpire.EmpireId))
                {
                    habitat_4.GetPirateControl().Add(new PirateColonyControl(selectedEmpire.EmpireId, 0.01f));
                    ctlEditHabitatPirateColonyControl.BindData(habitat_4.GetPirateControl(), _Game.Galaxy.PirateEmpires, habitat_4);
                }
            }
        }

        private void btnEditHabitatRemovePirateColonyControl_Click(object sender, EventArgs e)
        {
            if (habitat_4 == null || habitat_4.GetPirateControl() == null)
            {
                return;
            }
            PirateColonyControl selectedPirateColonyControl = ctlEditHabitatPirateColonyControl.SelectedPirateColonyControl;
            if (selectedPirateColonyControl != null)
            {
                PirateColonyControl byFaction = habitat_4.GetPirateControl().GetByFaction(selectedPirateColonyControl.EmpireId);
                if (byFaction != null)
                {
                    habitat_4.GetPirateControl().Remove(byFaction);
                    ctlEditHabitatPirateColonyControl.BindData(habitat_4.GetPirateControl(), _Game.Galaxy.PirateEmpires, habitat_4);
                }
            }
        }

        private void btnEditHabitatAddPlanetaryFacility_Click(object sender, EventArgs e)
        {
            if (habitat_4 == null)
            {
                return;
            }
            if (habitat_4.Facilities == null)
            {
                habitat_4.Facilities = new PlanetaryFacilityList();
            }
            PlanetaryFacilityDefinition selectedPlanetaryFacility = cmbEditHabitatAddPlanetaryFacility.SelectedPlanetaryFacility;
            if (selectedPlanetaryFacility == null)
            {
                return;
            }
            if (selectedPlanetaryFacility.Type == PlanetaryFacilityType.Wonder)
            {
                if (habitat_4.Facilities.CountWonderByType(selectedPlanetaryFacility.WonderType) <= 0)
                {
                    PlanetaryFacility planetaryFacility = new PlanetaryFacility(selectedPlanetaryFacility.PlanetaryFacilityDefinitionId, 1f);
                    habitat_4.Facilities.Add(planetaryFacility);
                    habitat_4.CheckAddFacilityTracking(planetaryFacility);
                    habitat_4.ReviewPlanetaryFacilities(habitat_4.Owner);
                    ctlEditHabitatPlanetaryFacilities.BindData(_Game.Galaxy, habitat_4.Facilities, habitat_4);
                }
            }
            else if (habitat_4.Facilities.CountByType(selectedPlanetaryFacility.Type) <= 0)
            {
                PlanetaryFacility planetaryFacility2 = new PlanetaryFacility(selectedPlanetaryFacility.PlanetaryFacilityDefinitionId, 1f);
                habitat_4.Facilities.Add(planetaryFacility2);
                habitat_4.CheckAddFacilityTracking(planetaryFacility2);
                habitat_4.ReviewPlanetaryFacilities(habitat_4.Owner);
                ctlEditHabitatPlanetaryFacilities.BindData(_Game.Galaxy, habitat_4.Facilities, habitat_4);
            }
        }

        private void btnEditHabitatRemovePlanetaryFacility_Click(object sender, EventArgs e)
        {
            if (habitat_4 == null || habitat_4.Facilities == null)
            {
                return;
            }
            PlanetaryFacility selectedFacility = ctlEditHabitatPlanetaryFacilities.SelectedFacility;
            if (selectedFacility == null)
            {
                return;
            }
            if (selectedFacility.Type == PlanetaryFacilityType.Wonder)
            {
                PlanetaryFacility planetaryFacility = habitat_4.Facilities.FindWonderByType(selectedFacility.WonderType);
                if (planetaryFacility != null)
                {
                    habitat_4.Facilities.Remove(planetaryFacility);
                    habitat_4.CheckRemoveFacilityTracking(planetaryFacility);
                    habitat_4.ReviewPlanetaryFacilities(habitat_4.Owner);
                    ctlEditHabitatPlanetaryFacilities.BindData(_Game.Galaxy, habitat_4.Facilities, habitat_4);
                }
            }
            else
            {
                PlanetaryFacility planetaryFacility2 = habitat_4.Facilities.FindByType(selectedFacility.Type);
                if (planetaryFacility2 != null)
                {
                    habitat_4.Facilities.Remove(planetaryFacility2);
                    habitat_4.CheckRemoveFacilityTracking(planetaryFacility2);
                    habitat_4.ReviewPlanetaryFacilities(habitat_4.Owner);
                    ctlEditHabitatPlanetaryFacilities.BindData(_Game.Galaxy, habitat_4.Facilities, habitat_4);
                }
            }
        }

        private void method_491(Habitat habitat_9)
        {
            method_489();
            int num = 190;
            int num2 = 345;
            int num3 = 475;
            int num4 = 605;
            int num5 = 0;
            int num6 = 805;
            lblEditHabitatTitle.Font = font_1;
            lblEditHabitatTitle.ForeColor = color_0;
            lblEditHabitatTitle.BackColor = Color.Transparent;
            lblEditHabitatTitle.Text = TextResolver.GetText("Edit Planet or Moon");
            switch (habitat_9.Category)
            {
                case HabitatCategoryType.Star:
                    lblEditHabitatTitle.Text = TextResolver.GetText("Edit Star");
                    chkEditHabitatHasRings.Visible = false;
                    lblEditHabitatQuality.Visible = false;
                    numEditHabitatQuality.Visible = false;
                    cmbEditHabitatAddResource.Visible = false;
                    btnEditHabitatAddResource.Visible = false;
                    btnEditHabitatRemoveResource.Visible = false;
                    lblEditHabitatDevelopmentLevel.Visible = false;
                    numEditHabitatDevelopmentLevel.Visible = false;
                    lblEditHabitatEmpire.Visible = false;
                    cmbEditHabitatEmpire.Visible = false;
                    chkEditHabitatCapital.Visible = false;
                    lblEditHabitatResources.Visible = false;
                    ctlEditHabitatResources.Visible = false;
                    lblEditHabitatPopulation.Visible = false;
                    ctlEditHabitatPopulation.Visible = false;
                    lblEditHabitatTroops.Visible = false;
                    ctlEditHabitatTroops.Visible = false;
                    cmbEditHabitatAddTroop.Visible = false;
                    pnlEditRuinsContainer.Visible = false;
                    lblEditHabitatPirateColonyControl.Visible = false;
                    ctlEditHabitatPirateColonyControl.Visible = false;
                    cmbEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatRemovePirateColonyControl.Visible = false;
                    lblEditHabitatPlanetaryFacilities.Visible = false;
                    ctlEditHabitatPlanetaryFacilities.Visible = false;
                    cmbEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatRemovePlanetaryFacility.Visible = false;
                    btnEditHabitatAddRuins.Visible = false;
                    btnEditHabitatRemoveRuins.Visible = false;
                    num5 = 200;
                    num6 = 300;
                    break;
                case HabitatCategoryType.Planet:
                case HabitatCategoryType.Moon:
                    if (habitat_9.Category == HabitatCategoryType.Planet)
                    {
                        lblEditHabitatTitle.Text = TextResolver.GetText("Edit Planet");
                    }
                    else if (habitat_9.Category == HabitatCategoryType.Moon)
                    {
                        lblEditHabitatTitle.Text = TextResolver.GetText("Edit Moon");
                        chkEditHabitatHasRings.Visible = false;
                    }
                    if (habitat_9.Type == HabitatType.BarrenRock || habitat_9.Type == HabitatType.GasGiant || habitat_9.Type == HabitatType.FrozenGasGiant)
                    {
                        lblEditHabitatQuality.Visible = false;
                        numEditHabitatQuality.Visible = false;
                    }
                    method_490();
                    if (habitat_9.Empire == null)
                    {
                        lblEditHabitatDevelopmentLevel.Visible = false;
                        numEditHabitatDevelopmentLevel.Visible = false;
                        lblEditHabitatEmpire.Visible = false;
                        cmbEditHabitatEmpire.Visible = false;
                        chkEditHabitatCapital.Visible = false;
                        lblEditHabitatPopulation.Visible = false;
                        ctlEditHabitatPopulation.Visible = false;
                        lblEditHabitatTroops.Visible = false;
                        ctlEditHabitatTroops.Visible = false;
                        cmbEditHabitatAddTroop.Visible = false;
                        lblEditHabitatPirateColonyControl.Visible = false;
                        ctlEditHabitatPirateColonyControl.Visible = false;
                        cmbEditHabitatAddPirateColonyControl.Visible = false;
                        btnEditHabitatAddPirateColonyControl.Visible = false;
                        btnEditHabitatRemovePirateColonyControl.Visible = false;
                        lblEditHabitatPlanetaryFacilities.Visible = false;
                        ctlEditHabitatPlanetaryFacilities.Visible = false;
                        cmbEditHabitatAddPlanetaryFacility.Visible = false;
                        btnEditHabitatAddPlanetaryFacility.Visible = false;
                        btnEditHabitatRemovePlanetaryFacility.Visible = false;
                        num = 200;
                        num4 = 375;
                        num6 = 375;
                    }
                    else if (habitat_9.Empire == _Game.Galaxy.IndependentEmpire)
                    {
                        num = 190;
                        num2 = 345;
                        num3 = 475;
                        num4 = 655;
                        num6 = 655;
                    }
                    else
                    {
                        chkEditHabitatCapital.Enabled = true;
                        ctlEditHabitatTroops.Enabled = true;
                        btnEditHabitatAddTroop.Enabled = true;
                        btnEditHabitatRemoveTroop.Enabled = true;
                        num = 190;
                        num2 = 345;
                        num3 = 475;
                        num4 = 655;
                        num6 = 655;
                    }
                    if (habitat_9.Ruin == null)
                    {
                        pnlEditRuinsContainer.Visible = false;
                        btnEditHabitatAddRuins.Visible = true;
                        btnEditHabitatRemoveRuins.Visible = false;
                    }
                    else
                    {
                        num6 += 200;
                        btnEditHabitatAddRuins.Visible = false;
                        btnEditHabitatRemoveRuins.Visible = true;
                    }
                    break;
                case HabitatCategoryType.Asteroid:
                    lblEditHabitatTitle.Text = TextResolver.GetText("Edit Asteroid");
                    chkEditHabitatHasRings.Visible = false;
                    lblEditHabitatQuality.Visible = false;
                    numEditHabitatQuality.Visible = false;
                    method_490();
                    lblEditHabitatDevelopmentLevel.Visible = false;
                    numEditHabitatDevelopmentLevel.Visible = false;
                    lblEditHabitatEmpire.Visible = false;
                    cmbEditHabitatEmpire.Visible = false;
                    chkEditHabitatCapital.Visible = false;
                    lblEditHabitatPopulation.Visible = false;
                    ctlEditHabitatPopulation.Visible = false;
                    lblEditHabitatTroops.Visible = false;
                    ctlEditHabitatTroops.Visible = false;
                    cmbEditHabitatAddTroop.Visible = false;
                    pnlEditRuinsContainer.Visible = false;
                    lblEditHabitatPirateColonyControl.Visible = false;
                    ctlEditHabitatPirateColonyControl.Visible = false;
                    cmbEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatRemovePirateColonyControl.Visible = false;
                    lblEditHabitatPlanetaryFacilities.Visible = false;
                    ctlEditHabitatPlanetaryFacilities.Visible = false;
                    cmbEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatRemovePlanetaryFacility.Visible = false;
                    btnEditHabitatAddRuins.Visible = false;
                    btnEditHabitatRemoveRuins.Visible = false;
                    num = 200;
                    num6 = 355;
                    break;
                case HabitatCategoryType.GasCloud:
                    lblEditHabitatTitle.Text = TextResolver.GetText("Edit Gas Cloud");
                    chkEditHabitatHasRings.Visible = false;
                    lblEditHabitatQuality.Visible = false;
                    numEditHabitatQuality.Visible = false;
                    lblEditHabitatDevelopmentLevel.Visible = false;
                    numEditHabitatDevelopmentLevel.Visible = false;
                    lblEditHabitatEmpire.Visible = false;
                    cmbEditHabitatEmpire.Visible = false;
                    chkEditHabitatCapital.Visible = false;
                    lblEditHabitatPopulation.Visible = false;
                    ctlEditHabitatPopulation.Visible = false;
                    lblEditHabitatTroops.Visible = false;
                    ctlEditHabitatTroops.Visible = false;
                    cmbEditHabitatAddTroop.Visible = false;
                    pnlEditRuinsContainer.Visible = false;
                    lblEditHabitatPirateColonyControl.Visible = false;
                    ctlEditHabitatPirateColonyControl.Visible = false;
                    cmbEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatAddPirateColonyControl.Visible = false;
                    btnEditHabitatRemovePirateColonyControl.Visible = false;
                    lblEditHabitatPlanetaryFacilities.Visible = false;
                    ctlEditHabitatPlanetaryFacilities.Visible = false;
                    cmbEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatAddPlanetaryFacility.Visible = false;
                    btnEditHabitatRemovePlanetaryFacility.Visible = false;
                    btnEditHabitatAddRuins.Visible = false;
                    btnEditHabitatRemoveRuins.Visible = false;
                    num5 = 205;
                    num = 300;
                    num6 = 455;
                    break;
            }
            ResourceDefinitionList resourceDefinitionList = new ResourceDefinitionList();
            resourceDefinitionList.AddRange(_Game.Galaxy.ResourceSystem.Resources);
            cmbEditHabitatAddResource.BindData(font_3, resourceDefinitionList, _uiResourcesBitmaps, allowNullResource: false, allowCriticalResources: false);
            pnlEditHabitat.Size = new Size(620, num6);
            pnlEditHabitat.Location = new Point(mainView.Width - pnlEditHabitat.Width - 10, 10);
            lblEditHabitatTitle.Location = new Point(44, 12);
            picEditHabitat.Location = new Point(10, 6);
            btnEditHabitatClose.Size = new Size(75, 25);
            btnEditHabitatClose.Location = new Point(pnlEditHabitat.Width - 85, 10);
            btnEditHabitatClose.Text = TextResolver.GetText("Close");
            picEditHabitatPicture.Size = new Size(130, 130);
            picEditHabitatPicture.Location = new Point(320, 25);
            picEditHabitatPicture.BackColor = Color.Transparent;
            picEditHabitatPicture.SizeMode = PictureBoxSizeMode.Zoom;
            scrEditHabitatPicture.Size = new Size(130, 18);
            scrEditHabitatPicture.Location = new Point(320, 163);
            scrEditHabitatPicture.Minimum = 0;
            scrEditHabitatPicture.Maximum = habitatImageCache_0.GetImagesSmall().Length - 1;
            picEditHabitatPictureLandscape.Size = new Size(130, 130);
            picEditHabitatPictureLandscape.Location = new Point(470, 25);
            picEditHabitatPictureLandscape.BackColor = Color.Transparent;
            picEditHabitatPictureLandscape.SizeMode = PictureBoxSizeMode.Zoom;
            scrEditHabitatPictureLandscape.Size = new Size(130, 18);
            scrEditHabitatPictureLandscape.Location = new Point(470, 163);
            scrEditHabitatPictureLandscape.Minimum = 0;
            scrEditHabitatPictureLandscape.Maximum = bitmap_29.Length - 1;
            lblEditHabitatName.Location = new Point(10, 45);
            txtEditHabitatName.Size = new Size(160, 16);
            txtEditHabitatName.Location = new Point(50, 44);
            chkEditHabitatHasRings.Location = new Point(220, 45);
            lblEditHabitatSize.Location = new Point(10, 75);
            numEditHabitatSize.Location = new Point(50, 73);
            lblEditHabitatDevelopmentLevel.Text = TextResolver.GetText("Development Abbreviation");
            lblEditHabitatDevelopmentLevel.Location = new Point(104, 75);
            numEditHabitatDevelopmentLevel.Location = new Point(148, 73);
            numEditHabitatDevelopmentLevel.BringToFront();
            lblEditHabitatQuality.Location = new Point(205, 75);
            numEditHabitatQuality.Location = new Point(243, 73);
            numEditHabitatQuality.BringToFront();
            lblEditHabitatEmpire.Location = new Point(10, 105);
            cmbEditHabitatEmpire.Size = new Size(145, 21);
            cmbEditHabitatEmpire.Location = new Point(50, 103);
            cmbEditHabitatEmpire.BringToFront();
            chkEditHabitatCapital.Location = new Point(200, 103);
            lblEditHabitatScenery.Location = new Point(10, 135);
            numEditHabitatScenery.Location = new Point(100, 135);
            txtEditHabitatSceneryDescription.Location = new Point(150, 135);
            txtEditHabitatSceneryDescription.Size = new Size(140, 21);
            lblEditHabitatResearchBonus.Location = new Point(10, 160);
            numEditHabitatResearchBonusAmount.Location = new Point(100, 160);
            ExUfgIlqCu.Items.Clear();
            ExUfgIlqCu.Items.AddRange(new string[4]
            {
            "(" + TextResolver.GetText("None") + ")",
            TextResolver.GetText("Weapons"),
            TextResolver.GetText("Energy"),
            TextResolver.GetText("HighTech")
            });
            ExUfgIlqCu.Location = new Point(150, 160);
            ExUfgIlqCu.Size = new Size(140, 21);
            lblEditHabitatResources.Location = new Point(10, num);
            ctlEditHabitatResources.Size = new Size(280, 100);
            ctlEditHabitatResources.Location = new Point(10, num + 20);
            ctlEditHabitatResources.Grid.Columns["Picture"].Width = 30;
            ctlEditHabitatResources.Grid.Columns["Type"].Width = 160;
            ctlEditHabitatResources.Grid.Columns["Abundance"].Width = 70;
            cmbEditHabitatAddResource.Location = new Point(10, num + 123);
            cmbEditHabitatAddResource.Size = new Size(140, 21);
            btnEditHabitatAddResource.Text = TextResolver.GetText("Add");
            btnEditHabitatAddResource.Location = new Point(153, num + 123);
            btnEditHabitatAddResource.Size = new Size(50, 25);
            btnEditHabitatRemoveResource.Text = TextResolver.GetText("Remove");
            btnEditHabitatRemoveResource.Location = new Point(215, num + 123);
            btnEditHabitatRemoveResource.Size = new Size(75, 25);
            lblEditHabitatPlanetaryFacilities.Location = new Point(310, num);
            ctlEditHabitatPlanetaryFacilities.Size = new Size(300, 185);
            ctlEditHabitatPlanetaryFacilities.Location = new Point(310, num + 20);
            cmbEditHabitatAddPlanetaryFacility.Size = new Size(160, 21);
            cmbEditHabitatAddPlanetaryFacility.Location = new Point(310, num + 208);
            btnEditHabitatAddPlanetaryFacility.Size = new Size(50, 25);
            btnEditHabitatAddPlanetaryFacility.Text = TextResolver.GetText("Add");
            btnEditHabitatAddPlanetaryFacility.Location = new Point(473, num + 208);
            btnEditHabitatRemovePlanetaryFacility.Size = new Size(75, 25);
            btnEditHabitatRemovePlanetaryFacility.Text = TextResolver.GetText("Remove");
            btnEditHabitatRemovePlanetaryFacility.Location = new Point(535, num + 208);
            int num7 = num3 - 45;
            lblEditHabitatPirateColonyControl.Location = new Point(310, num7);
            ctlEditHabitatPirateColonyControl.Size = new Size(300, 100);
            ctlEditHabitatPirateColonyControl.Location = new Point(310, num7 + 20);
            cmbEditHabitatAddPirateColonyControl.Size = new Size(160, 21);
            cmbEditHabitatAddPirateColonyControl.Location = new Point(310, num7 + 123);
            btnEditHabitatAddPirateColonyControl.Size = new Size(50, 25);
            btnEditHabitatAddPirateColonyControl.Text = TextResolver.GetText("Add");
            btnEditHabitatAddPirateColonyControl.Location = new Point(473, num7 + 123);
            btnEditHabitatRemovePirateColonyControl.Size = new Size(75, 25);
            btnEditHabitatRemovePirateColonyControl.Text = TextResolver.GetText("Remove");
            btnEditHabitatRemovePirateColonyControl.Location = new Point(535, num7 + 123);
            int num8 = num6 - 50;
            btnEditHabitatGameEvent.Size = new Size(100, 25);
            btnEditHabitatGameEvent.Location = new Point(310, num8 + 7);
            lblEditHabitatGameEvent.Location = new Point(415, num8);
            lblEditHabitatGameEvent.MinimumSize = new Size(175, 40);
            lblEditHabitatGameEvent.MaximumSize = new Size(175, 40);
            lblEditHabitatGameEvent.Size = new Size(175, 40);
            lblEditHabitatGameEvent.Font = font_7;
            lblEditHabitatGameEvent.TextAlign = ContentAlignment.MiddleLeft;
            lblEditHabitatPopulation.Location = new Point(10, num2);
            ctlEditHabitatPopulation.Size = new Size(280, 100);
            ctlEditHabitatPopulation.Location = new Point(10, num2 + 20);
            ctlEditHabitatPopulation.Grid.Columns["Picture"].Width = 40;
            ctlEditHabitatPopulation.Grid.Columns["Name"].Width = 95;
            ctlEditHabitatPopulation.Grid.Columns["Amount"].Width = 95;
            ctlEditHabitatPopulation.Grid.Columns["GrowthRate"].Width = 50;
            lblEditHabitatTroops.Location = new Point(10, num3);
            ctlEditHabitatTroops.Size = new Size(280, 100);
            ctlEditHabitatTroops.Location = new Point(10, num3 + 20);
            ctlEditHabitatTroops.Grid.Columns["Location"].Visible = false;
            ctlEditHabitatTroops.Grid.Columns["Empire"].Width = 25;
            ctlEditHabitatTroops.Grid.Columns["Name"].Width = 100;
            ctlEditHabitatTroops.Grid.Columns["Size"].Width = 55;
            ctlEditHabitatTroops.Grid.Columns["AttackStrength"].Width = 50;
            ctlEditHabitatTroops.Grid.Columns["DefendStrength"].Width = 50;
            btnEditHabitatAddRuins.Size = new Size(140, 20);
            btnEditHabitatAddRuins.Location = new Point(10, num4 - 20);
            btnEditHabitatAddRuins.Text = TextResolver.GetText("Add Ruins");
            btnEditHabitatRemoveRuins.Size = new Size(140, 20);
            btnEditHabitatRemoveRuins.Location = new Point(10, num4 - 20);
            btnEditHabitatRemoveRuins.Text = TextResolver.GetText("Remove Ruins");
            pnlEditRuinsContainer.Size = new Size(280, 190);
            pnlEditRuinsContainer.Location = new Point(10, num4);
            pnlEditRuins.Size = new Size(260, 170);
            pnlEditRuins.Location = new Point(10, 10);
            pnlEditSpecialRuins.Size = new Size(260, 170);
            pnlEditSpecialRuins.Location = new Point(10, 10);
            pnlEditRuins.Visible = true;
            pnlEditSpecialRuins.Visible = false;
            if (habitat_9.Ruin != null)
            {
                if (habitat_9.Ruin.Type != RuinType.Standard && habitat_9.Ruin.Type != RuinType.CreatureSwarm && habitat_9.Ruin.Type != RuinType.PirateAmbush)
                {
                    pnlEditRuins.Visible = false;
                    pnlEditSpecialRuins.Visible = true;
                }
                else
                {
                    pnlEditRuins.Visible = true;
                    pnlEditSpecialRuins.Visible = false;
                }
            }
            lblEditHabitatSolarRadiation.Location = new Point(10, num5);
            lblEditHabitatMicrowaveRadiation.Location = new Point(10, num5 + 30);
            lblEditHabitatXrayRadiation.Location = new Point(10, num5 + 60);
            numEditHabitatSolarRadiation.Location = new Point(150, num5 - 2);
            numEditHabitatMicrowaveRadiation.Location = new Point(150, num5 + 28);
            numEditHabitatXrayRadiation.Location = new Point(150, num5 + 58);
            cmbEditHabitatAddTroop.Location = new Point(10, num3 + 125);
            cmbEditHabitatAddTroop.Size = new Size(140, 21);
            cmbEditHabitatAddTroop.DropDownWidth = 350;
            btnEditHabitatAddTroop.Size = new Size(50, 25);
            btnEditHabitatAddTroop.Location = new Point(153, num3 + 125);
            btnEditHabitatAddTroop.Text = TextResolver.GetText("Add");
            btnEditHabitatRemoveTroop.Size = new Size(75, 25);
            btnEditHabitatRemoveTroop.Location = new Point(215, num3 + 125);
            btnEditHabitatRemoveTroop.Text = TextResolver.GetText("Remove");
            cmbEditHabitatAddTroop.Visible = true;
            btnEditHabitatAddTroop.Visible = true;
            btnEditHabitatRemoveTroop.Visible = true;
            method_492();
            txtEditHabitatName.Focus();
            txtEditHabitatName.SelectAll();
            pnlEditHabitat.Visible = true;
            pnlEditHabitat.BringToFront();
        }

        private void method_492()
        {
            if (_Game.SelectedObject is Habitat)
            {
                Habitat habitat = (Habitat)_Game.SelectedObject;
                decimal maximum = 1550m;
                switch (habitat.Category)
                {
                    case HabitatCategoryType.Star:
                        maximum = 2150m;
                        if (habitat.Type == HabitatType.BlackHole)
                        {
                            maximum = 6500m;
                        }
                        else if (habitat.Type == HabitatType.SuperNova)
                        {
                            maximum = 180000m;
                        }
                        break;
                    case HabitatCategoryType.Planet:
                        maximum = 400m;
                        if (habitat.Type == HabitatType.GasGiant)
                        {
                            maximum = 1000m;
                        }
                        else if (habitat.Type == HabitatType.FrozenGasGiant)
                        {
                            maximum = 900m;
                        }
                        break;
                    case HabitatCategoryType.Moon:
                        maximum = 400m;
                        break;
                    case HabitatCategoryType.Asteroid:
                        maximum = 50m;
                        break;
                    case HabitatCategoryType.GasCloud:
                        maximum = 32000m;
                        break;
                }
                txtEditHabitatName.MaxLength = 120;
                txtEditHabitatName.Text = habitat.Name;
                numEditHabitatSize.Minimum = 1m;
                numEditHabitatSize.Maximum = maximum;
                numEditHabitatSize.Increment = 10m;
                if (habitat.Type == HabitatType.SuperNova)
                {
                    numEditHabitatSize.Value = habitat.Diameter * 10;
                }
                else
                {
                    numEditHabitatSize.Value = habitat.Diameter;
                }
                if (picEditHabitatPicture.Image != null && picEditHabitatPicture.Image is Bitmap)
                {
                    Bitmap bitmap = (Bitmap)picEditHabitatPicture.Image;
                    picEditHabitatPicture.Image = null;
                    bitmap.Dispose();
                }
                picEditHabitatPicture.Image = null;
                picEditHabitatPictureLandscape.Image = null;
                picEditHabitatPicture.Refresh();
                picEditHabitatPictureLandscape.Refresh();
                if (habitat.Category != 0 && habitat.Category != HabitatCategoryType.GasCloud)
                {
                    scrEditHabitatPictureLandscape.Visible = true;
                    picEditHabitatPictureLandscape.Visible = true;
                    scrEditHabitatPicture.Visible = true;
                    picEditHabitatPicture.Visible = true;
                    scrEditHabitatPicture.Value = habitat.PictureRef;
                    RemIoawDot(habitat, habitat.PictureRef);
                    scrEditHabitatPictureLandscape.Value = habitat.LandscapePictureRef;
                    method_667(habitat, habitat.LandscapePictureRef);
                }
                else
                {
                    scrEditHabitatPicture.Visible = false;
                    picEditHabitatPicture.Visible = false;
                    scrEditHabitatPictureLandscape.Visible = false;
                    picEditHabitatPictureLandscape.Visible = false;
                }
                chkEditHabitatHasRings.Checked = habitat.HasRings;
                numEditHabitatQuality.Value = Math.Min(100, Math.Max(0, (int)(habitat.Quality * 100f + 0.5f)));
                numEditHabitatScenery.Value = Math.Min(100, Math.Max(0, (int)(habitat.ScenicFactor * 100f + 0.5f)));
                numEditHabitatResearchBonusAmount.Value = Math.Min(100, Math.Max(0, (int)habitat.ResearchBonus));
                txtEditHabitatSceneryDescription.Text = habitat.ScenicFeature;
                switch (habitat.ResearchBonusIndustry)
                {
                    case IndustryType.Undefined:
                        ExUfgIlqCu.SelectedIndex = 0;
                        break;
                    case IndustryType.Weapon:
                        ExUfgIlqCu.SelectedIndex = 1;
                        break;
                    case IndustryType.Energy:
                        ExUfgIlqCu.SelectedIndex = 2;
                        break;
                    case IndustryType.HighTech:
                        ExUfgIlqCu.SelectedIndex = 3;
                        break;
                }
                if (habitat.Empire == _Game.Galaxy.IndependentEmpire)
                {
                    chkEditHabitatCapital.Enabled = false;
                    ctlEditHabitatTroops.Enabled = false;
                    cmbEditHabitatAddTroop.Enabled = false;
                    btnEditHabitatAddTroop.Enabled = false;
                    btnEditHabitatRemoveTroop.Enabled = false;
                }
                numEditHabitatDevelopmentLevel.Minimum = 0m;
                if (habitat.Empire != null && habitat.Empire != _Game.Galaxy.IndependentEmpire && habitat.Population != null && habitat.Population.Count > 0)
                {
                    numEditHabitatDevelopmentLevel.Minimum = 0m;
                }
                numEditHabitatDevelopmentLevel.Maximum = 200m;
                numEditHabitatDevelopmentLevel.Value = habitat.DevelopmentLevel;
                cmbEditHabitatEmpire.BindData(_Game.PlayerEmpire, _Game.Galaxy.Empires, null, _Game.Galaxy.IndependentEmpire, includeNoEmpire: false);
                cmbEditHabitatEmpire.SetSelectedEmpire(habitat.Empire);
                bool @checked = false;
                if (habitat.Empire != null && habitat.Empire.Capital == habitat)
                {
                    @checked = true;
                }
                chkEditHabitatCapital.Checked = @checked;
                ctlEditHabitatResources.BindData(habitat.Resources, _uiResourcesBitmaps, editable: true);
                ctlEditHabitatPopulation.BindData(habitat.Population, editable: true);
                ctlEditHabitatTroops.BindData(habitat.Troops, editable: true);
                if (habitat.Ruin != null)
                {
                    if (habitat.Ruin.Type != RuinType.Standard && habitat.Ruin.Type != RuinType.PirateAmbush && habitat.Ruin.Type != RuinType.CreatureSwarm)
                    {
                        pnlEditSpecialRuins.BindData(habitat.Ruin, bitmap_2, _Game.Galaxy.Races, raceImageCache_0.GetRaceImages());
                    }
                    else
                    {
                        pnlEditRuins.BindData(habitat.Ruin, bitmap_2);
                    }
                }
                numEditHabitatSolarRadiation.Minimum = 0m;
                numEditHabitatSolarRadiation.Maximum = 100m;
                numEditHabitatSolarRadiation.Value = habitat.SolarRadiation;
                numEditHabitatMicrowaveRadiation.Minimum = 0m;
                numEditHabitatMicrowaveRadiation.Maximum = 110m;
                numEditHabitatMicrowaveRadiation.Value = habitat.MicrowaveRadiation;
                numEditHabitatXrayRadiation.Minimum = 0m;
                numEditHabitatXrayRadiation.Maximum = 220m;
                numEditHabitatXrayRadiation.Value = habitat.XrayRadiation;
                ctlEditHabitatPlanetaryFacilities.BindData(_Game.Galaxy, habitat.Facilities, habitat);
                cmbEditHabitatAddPlanetaryFacility.BindData(null, _Game.Galaxy.PlanetaryFacilityDefinitions, bitmap_8);
                ctlEditHabitatPirateColonyControl.BindData(habitat.GetPirateControl(), _Game.Galaxy.PirateEmpires, habitat);
                cmbEditHabitatAddPirateColonyControl.BindData(null, null, _Game.Galaxy.PirateEmpires, null, includeNoEmpire: false);
                TroopList troopList = Galaxy.GenerateDefaultTroops(_Game.Galaxy);
                cmbEditHabitatAddTroop.BindData(font_6, troopList, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27);
                if (habitat.Population != null && habitat.Population.DominantRace != null)
                {
                    Troop firstByTypeAndRace = troopList.GetFirstByTypeAndRace(TroopType.Infantry, habitat.Population.DominantRace);
                    if (firstByTypeAndRace != null)
                    {
                        cmbEditHabitatAddTroop.SetSelectedTroop(firstByTypeAndRace);
                    }
                }
                habitat_4 = habitat;
            }
            method_677();
        }

        private void method_493()
        {
            if (habitat_4 != null)
            {
                habitat_4.Name = txtEditHabitatName.Text;
                if (habitat_4.Type == HabitatType.SuperNova)
                {
                    habitat_4.Diameter = (short)(numEditHabitatSize.Value / 10m);
                }
                else
                {
                    habitat_4.Diameter = (short)numEditHabitatSize.Value;
                }
                habitat_4.SetDevelopmentLevel((int)numEditHabitatDevelopmentLevel.Value);
                habitat_4.SolarRadiation = (byte)numEditHabitatSolarRadiation.Value;
                habitat_4.MicrowaveRadiation = (byte)numEditHabitatMicrowaveRadiation.Value;
                habitat_4.XrayRadiation = (byte)numEditHabitatXrayRadiation.Value;
                habitat_4.HasRings = chkEditHabitatHasRings.Checked;
                habitat_4.BaseQuality = Math.Min(1f, Math.Max(0f, (float)numEditHabitatQuality.Value / 100f));
                habitat_4.ScenicFactor = Math.Min(1f, Math.Max(0f, (float)numEditHabitatScenery.Value / 100f));
                habitat_4.ResearchBonus = Math.Min((byte)100, Math.Max((byte)0, (byte)numEditHabitatResearchBonusAmount.Value));
                if (string.IsNullOrEmpty(txtEditHabitatSceneryDescription.Text))
                {
                    habitat_4.ScenicFeature = null;
                }
                else
                {
                    habitat_4.ScenicFeature = txtEditHabitatSceneryDescription.Text;
                }
                switch (ExUfgIlqCu.SelectedIndex)
                {
                    default:
                        habitat_4.ResearchBonusIndustry = IndustryType.Undefined;
                        break;
                    case 0:
                        if ((float)(int)habitat_4.ResearchBonus <= 0f)
                        {
                            habitat_4.ResearchBonusIndustry = IndustryType.Undefined;
                        }
                        else
                        {
                            habitat_4.ResearchBonusIndustry = IndustryType.Weapon;
                        }
                        break;
                    case 1:
                        habitat_4.ResearchBonusIndustry = IndustryType.Weapon;
                        break;
                    case 2:
                        habitat_4.ResearchBonusIndustry = IndustryType.Energy;
                        break;
                    case 3:
                        habitat_4.ResearchBonusIndustry = IndustryType.HighTech;
                        break;
                }
                if (pnlEditRuins.Visible)
                {
                    habitat_4.Ruin = pnlEditRuins.GetRuin();
                }
                else if (pnlEditSpecialRuins.Visible)
                {
                    habitat_4.Ruin = pnlEditSpecialRuins.GetRuin();
                }
                if (ctlEditHabitatResources.Grid.EditingControl != null)
                {
                    ctlEditHabitatResources.Grid.EndEdit();
                }
                if (ctlEditHabitatPopulation.Grid.EditingControl != null)
                {
                    ctlEditHabitatPopulation.Grid.EndEdit();
                }
                if (ctlEditHabitatTroops.Grid.EditingControl != null)
                {
                    ctlEditHabitatTroops.Grid.EndEdit();
                }
                habitat_4.Population = ctlEditHabitatPopulation.Population;
                habitat_4.Resources = ctlEditHabitatResources.Resources;
                habitat_4.Troops = ctlEditHabitatTroops.Troops;
                habitat_4.SetPirateControlRaw(ctlEditHabitatPirateColonyControl.PirateColonyControl);
                habitat_4.Population.RecalculateTotalAmount();
                if (cmbEditHabitatEmpire.SelectedEmpire != habitat_4.Empire)
                {
                    Empire selectedEmpire = cmbEditHabitatEmpire.SelectedEmpire;
                    if (selectedEmpire != null)
                    {
                        cmbEditHabitatEmpire.SelectedEmpire.TakeOwnershipOfColony(habitat_4, cmbEditHabitatEmpire.SelectedEmpire);
                    }
                }
                if (chkEditHabitatCapital.Checked && habitat_4.Empire.Capital != habitat_4)
                {
                    habitat_4.Empire.Capital = habitat_4;
                    habitat_4.Empire.RecalculateColonyDistancesFromCapital();
                }
                else if (!chkEditHabitatCapital.Checked && habitat_4.Empire != null && habitat_4.Empire.Capital == habitat_4)
                {
                    habitat_4.Empire.Capital = habitat_4.Empire.SelectBestCandidateForCapital();
                    habitat_4.Empire.RecalculateColonyDistancesFromCapital();
                }
                habitat_4 = null;
            }
            _Game.Galaxy.ReviewWondersBuilt();
        }

        private void method_494()
        {
            method_493();
            pnlDetailInfo.Invalidate();
            bool_19 = true;
            method_149();
            pnlEditHabitat.SendToBack();
            pnlEditHabitat.Visible = false;
        }

        private void pnlEditHabitat_Leave(object sender, EventArgs e)
        {
        }

        private void btnEditHabitatAddTroop_Click(object sender, EventArgs e)
        {
            if (habitat_4 == null)
            {
                return;
            }
            Empire empire = habitat_4.Empire;
            if (habitat_4 == null || empire == null || empire == _Game.Galaxy.IndependentEmpire || _Game.Galaxy.PirateEmpires.Contains(empire))
            {
                return;
            }
            habitat_4.Troops = ctlEditHabitatTroops.Troops;
            Troop selectedTroop = cmbEditHabitatAddTroop.SelectedTroop;
            if (selectedTroop != null && selectedTroop.Race != null)
            {
                double num = selectedTroop.Race.TroopStrength;
                string name = habitat_4.Empire.GenerateTroopDescription(selectedTroop.Name);
                Troop troop = Galaxy.GenerateNewTroop(name, selectedTroop.Type, (int)num, habitat_4.Empire, selectedTroop.Race);
                if (troop != null)
                {
                    troop.Readiness = 100f;
                    troop.Colony = habitat_4;
                    habitat_4.Troops.Add(troop);
                    empire.Troops.Add(troop);
                }
                ctlEditHabitatTroops.BindData(habitat_4.Troops, editable: true);
            }
        }

        private void btnEditHabitatRemoveTroop_Click(object sender, EventArgs e)
        {
            if (habitat_4 != null && ctlEditHabitatTroops.SelectedTroop != null)
            {
                habitat_4.Troops = ctlEditHabitatTroops.Troops;
                if (habitat_4.Empire != null)
                {
                    habitat_4.Empire.Troops.Remove(ctlEditHabitatTroops.SelectedTroop);
                }
                habitat_4.Troops.Remove(ctlEditHabitatTroops.SelectedTroop);
                ctlEditHabitatTroops.BindData(habitat_4.Troops, editable: true);
            }
        }

        private void method_495()
        {
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            pnlEditEmpireList.Size = new Size(460, 520);
            pnlEditEmpireList.Location = new Point((mainView.Width - pnlEditEmpireList.Width) / 2, (mainView.Height - pnlEditEmpireList.Height) / 2);
            lblEditEmpireListTitle.Font = font_1;
            lblEditEmpireListTitle.ForeColor = color_0;
            lblEditEmpireListTitle.BackColor = Color.Transparent;
            lblEditEmpireListTitle.Text = TextResolver.GetText("Edit Empires");
            lblEditEmpireListTitle.Location = new Point(42, 9);
            picEditEmpireList.Location = new Point(10, 4);
            btnEditEmpireListClose.Font = new Font(font_3.FontFamily, 16.67f, FontStyle.Bold);
            btnEditEmpireListClose.Location = new Point(250, 8);
            btnEditEmpireListClose.Size = new Size(200, 25);
            ctlEditEmpireList.Size = new Size(440, 420);
            ctlEditEmpireList.Location = new Point(10, 38);
            ctlEditEmpireList.Grid.Columns["Empire"].Width = 30;
            ctlEditEmpireList.Grid.Columns["Race"].Width = 40;
            ctlEditEmpireList.Grid.Columns["Name"].Width = 370;
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            empireList.AddRange(_Game.Galaxy.Empires);
            empireList.AddRange(_Game.Galaxy.PirateEmpires);
            ctlEditEmpireList.BindData(empireList, raceImageCache_0.GetRaceImages());
            btnEditEmpireListAdd.Size = new Size(140, 45);
            btnEditEmpireListAdd.Location = new Point(10, 468);
            btnEditEmpireListEdit.Size = new Size(140, 45);
            btnEditEmpireListEdit.Location = new Point(160, 468);
            btnEditEmpireListRemove.Size = new Size(140, 45);
            btnEditEmpireListRemove.Location = new Point(310, 468);
            pnlEditEmpireList.Visible = true;
            pnlEditEmpireList.BringToFront();
        }

        private void method_496()
        {
            pnlEditEmpireList.SendToBack();
            pnlEditEmpireList.Visible = false;
        }

        private void btnGameEditorEditEmpires_Click(object sender, EventArgs e)
        {
            method_495();
        }

        private void btnEditEmpireListClose_Click(object sender, EventArgs e)
        {
            method_496();
        }

        private void btnEditEmpireListAdd_Click(object sender, EventArgs e)
        {
            if (_Game.Galaxy.NextEmpireID < Galaxy.MaximumEmpireCount - 1)
            {
                int index = Galaxy.Rnd.Next(0, _Game.Galaxy.Races.Count);
                Race race = _Game.Galaxy.Races[index];
                int governmentId = Empire.SelectSuitableGovernment(race, -1, Empire.ResolveDefaultAllowableGovernmentTypes(race));
                EmpirePolicy policy = _Game.Galaxy.LoadEmpirePolicy(race, isPirate: false);
                empire_0 = new Empire(_Game.Galaxy, "", null, race, governmentId, 1.0, policy);
                empire_0.GenerateDesignSpecifications(_Game.Galaxy, race, isPirate: false, race.Name);
                _Game.Galaxy.Empires.Add(empire_0);
                method_500();
            }
            else
            {
                string string_ = TextResolver.GetText("Maximum Empires Message");
                MessageBoxEx messageBoxEx = method_370(string_, TextResolver.GetText("Maximum Empires in the Galaxy"));
                messageBoxEx.Show(this);
            }
        }

        private void btnEditEmpireListEdit_Click(object sender, EventArgs e)
        {
            empire_0 = ctlEditEmpireList.SelectedEmpire;
            if (empire_0 != null)
            {
                method_500();
            }
        }

        private void btnEditEmpireListRemove_Click(object sender, EventArgs e)
        {
            Empire selectedEmpire = ctlEditEmpireList.SelectedEmpire;
            string empty = string.Empty;
            if (selectedEmpire == _Game.PlayerEmpire)
            {
                empty = TextResolver.GetText("This empire is your player empire");
                MessageBoxEx messageBoxEx = method_370(empty, TextResolver.GetText("Cannot Remove Player"));
                messageBoxEx.Show(this);
                return;
            }
            empty = TextResolver.GetText("This will permanently remove the selected empire");
            MessageBoxEx messageBoxEx2 = method_372(empty, TextResolver.GetText("Remove Empire?"));
            if (!(messageBoxEx2.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes"))
            {
                return;
            }
            if (selectedEmpire != null)
            {
                if (selectedEmpire.PirateEmpireBaseHabitat == null && selectedEmpire.Colonies != null && selectedEmpire.Colonies.Count > 0)
                {
                    Habitat[] array = selectedEmpire.Colonies.ToArray();
                    for (int i = 0; i < array.Length; i++)
                    {
                        array[i]?.ClearColony(null, sendMessages: false, removeEmpireWhenNoColonies: true);
                    }
                }
                else
                {
                    selectedEmpire.CompleteTeardown(null, removeFromGalaxy: true, sendMessages: false);
                }
            }
            DistantWorlds.Types.EmpireList empireList = new DistantWorlds.Types.EmpireList();
            empireList.AddRange(_Game.Galaxy.Empires);
            empireList.AddRange(_Game.Galaxy.PirateEmpires);
            ctlEditEmpireList.BindData(empireList, raceImageCache_0.GetRaceImages());
        }

        private void method_497()
        {
            if (empire_0 == null)
            {
                return;
            }
            txtEditEmpireName.Text = empire_0.Name;
            cmbEditEmpireRace.SetSelectedRace(empire_0.DominantRace);
            if (empire_0.PirateEmpireBaseHabitat == null)
            {
                List<int> list = new List<int>();
                list.AddRange(empire_0.AllowableGovernmentTypes);
                if (!list.Contains(empire_0.GovernmentId))
                {
                    list.Add(empire_0.GovernmentId);
                }
                cmbEditEmpireGovernmentStyle.Ignite(list);
                cmbEditEmpireGovernmentStyle.SetSelectedGovernmentStyle(empire_0.GovernmentId);
                cmbEditEmpireGovernmentStyle.Visible = true;
                cmbEditEmpirePirateStyle.Visible = false;
            }
            else
            {
                cmbEditEmpirePirateStyle.BindData();
                cmbEditEmpirePirateStyle.SetSelectedPlaystyle(empire_0.PiratePlayStyle);
                cmbEditEmpireGovernmentStyle.Visible = false;
                cmbEditEmpirePirateStyle.Visible = true;
            }
            numEditEmpireMoney.Minimum = (decimal)Math.Min(-999999999.0, empire_0.StateMoney);
            numEditEmpireMoney.Maximum = (decimal)Math.Max(999999999.0, empire_0.StateMoney);
            numEditEmpireMoney.Value = (decimal)empire_0.StateMoney;
            if (empire_0.PirateEmpireBaseHabitat == null)
            {
                cmbEditEmpirePrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, empire_0.MainColor);
                cmbEditEmpirePrimaryColor.SetSelectedColor(empire_0.MainColor);
                cmbEditEmpireSecondaryColor.Ignite(allowWhite: true, allowBlack: false, useDarkerPalette: false, empire_0.SecondaryColor);
                cmbEditEmpireSecondaryColor.SetSelectedColor(empire_0.SecondaryColor);
                cmbEditEmpireFlagShape.BindData(empire_0.MainColor, empire_0.SecondaryColor, Galaxy.FlagShapes);
                cmbEditEmpireFlagShape.SetSelectedFlagShape(empire_0.FlagShape);
            }
            else
            {
                cmbEditEmpirePrimaryColor.Ignite(allowWhite: false, allowBlack: true, useDarkerPalette: true, empire_0.MainColor);
                cmbEditEmpirePrimaryColor.SetSelectedColor(empire_0.MainColor);
                cmbEditEmpireSecondaryColor.Ignite(allowWhite: true, allowBlack: false, useDarkerPalette: false, empire_0.SecondaryColor);
                cmbEditEmpireSecondaryColor.SetSelectedColor(empire_0.SecondaryColor);
                cmbEditEmpireFlagShape.BindData(empire_0.MainColor, empire_0.SecondaryColor, Galaxy.FlagShapesPirates);
                cmbEditEmpireFlagShape.SetSelectedFlagShape(empire_0.FlagShape);
            }
            if (empire_0.PirateEmpireBaseHabitat == null)
            {
                sldEditEmpireReputation.Visible = true;
                sldEditEmpireWarWeariness.Visible = true;
                lblEditEmpireReputation.Visible = true;
                lblEditEmpireWarWeariness.Visible = true;
                sldEditEmpireReputation.Value = (int)empire_0.CivilityRating;
                sldEditEmpireWarWeariness.Value = (int)Math.Max(sldEditEmpireWarWeariness.Minimum, Math.Min(sldEditEmpireWarWeariness.Maximum, empire_0.WarWearinessRaw));
                method_502();
                method_503();
            }
            else
            {
                sldEditEmpireReputation.Visible = false;
                sldEditEmpireWarWeariness.Visible = false;
                lblEditEmpireReputation.Visible = false;
                lblEditEmpireWarWeariness.Visible = false;
                sldEditEmpireReputation.Value = 0;
                sldEditEmpireWarWeariness.Value = 0;
                lblEditEmpireReputationSummary.Text = string.Empty;
                lblEditEmpireWarWearinessSummary.Text = string.Empty;
            }
            txtEditEmpireDescription.Text = empire_0.Description;
            chkEditEmpirePlayableInScenario.Checked = empire_0.PlayableInScenario;
            DiplomaticRelationList diplomaticRelationList = new DiplomaticRelationList();
            PirateRelationList pirateRelationList = new PirateRelationList();
            foreach (Empire empire in _Game.Galaxy.Empires)
            {
                if (empire_0.PirateEmpireBaseHabitat == null)
                {
                    DiplomaticRelation diplomaticRelation = empire_0.ObtainDiplomaticRelation(empire);
                    if (diplomaticRelation.OtherEmpire != empire_0)
                    {
                        diplomaticRelationList.Add(diplomaticRelation);
                    }
                }
                else
                {
                    PirateRelation pirateRelation = empire_0.ObtainPirateRelation(empire);
                    if (pirateRelation.OtherEmpire != empire_0)
                    {
                        pirateRelationList.Add(pirateRelation);
                    }
                }
            }
            foreach (Empire pirateEmpire in _Game.Galaxy.PirateEmpires)
            {
                PirateRelation pirateRelation2 = empire_0.ObtainPirateRelation(pirateEmpire);
                if (pirateRelation2.OtherEmpire != empire_0)
                {
                    pirateRelationList.Add(pirateRelation2);
                }
            }
            ctlEditEmpireRelationList.BindData(diplomaticRelationList, pirateRelationList, raceImageCache_0.GetRaceImages());
            ctlEditEmpireColonies.BindData(empire_0.Colonies);
            Habitat selectedHabitat = ctlEditEmpireColonies.SelectedHabitat;
            if (selectedHabitat != null)
            {
                Bitmap backgroundPicture = mainView.method_54(selectedHabitat, pnlEditEmpireColonyInfo.ClientSize.Width);
                pnlEditEmpireColonyInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, selectedHabitat);
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(empire_0.BuiltObjects);
            builtObjectList.AddRange(empire_0.PrivateBuiltObjects);
            ctlEditEmpireBuiltObjectList.BindData(builtObjectList, _Game.Galaxy);
            BuiltObject selectedBuiltObject = ctlEditEmpireBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(selectedBuiltObject);
                Bitmap image = new Bitmap(builtObjectImageData.Image);
                image = ((selectedBuiltObject.Empire == null) ? PrepareBuiltObjectImage(selectedBuiltObject, image, Color.Gray, Color.Gray, 1.0, 1) : PrepareBuiltObjectImage(selectedBuiltObject, image, selectedBuiltObject.Empire.MainColor, selectedBuiltObject.Empire.SecondaryColor, 1.0, 1));
                pnlEditEmpireBuiltObjectInfo.SetData(_Game, _Game.Galaxy, image, new Bitmap(builtObjectImageData.MaskImage), selectedBuiltObject);
            }
        }

        private void method_498()
        {
            empire_0.Name = txtEditEmpireName.Text;
            bool isPirate = false;
            if (empire_0.PirateEmpireBaseHabitat != null)
            {
                isPirate = true;
                if (empire_0.PiratePlayStyle != cmbEditEmpirePirateStyle.SelectedPlaystyle)
                {
                    empire_0.PiratePlayStyle = cmbEditEmpirePirateStyle.SelectedPlaystyle;
                }
            }
            else if (empire_0.GovernmentId != cmbEditEmpireGovernmentStyle.SelectedGovernmentId)
            {
                empire_0.ChangeGovernment(cmbEditEmpireGovernmentStyle.SelectedGovernmentId);
            }
            Race selectedRace = cmbEditEmpireRace.SelectedRace;
            if (empire_0.DominantRace != selectedRace)
            {
                empire_0.Policy = _Game.Galaxy.LoadEmpirePolicy(selectedRace, isPirate);
            }
            empire_0.DominantRace = selectedRace;
            empire_0.RefreshAllowableGovernmentTypes();
            if (_Game.PlayerEmpire.ControlDesigns)
            {
                empire_0.PerformResearch(0.0, allowResearchEvents: false);
                empire_0.CreateNewDesigns(_Game.Galaxy.CurrentStarDate, forceUpdate: true);
            }
            empire_0.StateMoney = (double)numEditEmpireMoney.Value;
            empire_0.MainColor = cmbEditEmpirePrimaryColor.SelectedColor;
            empire_0.SecondaryColor = cmbEditEmpireSecondaryColor.SelectedColor;
            empire_0.FlagShape = cmbEditEmpireFlagShape.SelectedFlagShapeIndex;
            Bitmap smallFlagPicture = null;
            Bitmap largeFlagPicture = null;
            if (empire_0.PirateEmpireBaseHabitat != null)
            {
                Galaxy.GenerateEmpireFlag(empire_0.MainColor, empire_0.SecondaryColor, empire_0.FlagShape, Galaxy.FlagShapesPirates, ref smallFlagPicture, ref largeFlagPicture);
            }
            else
            {
                Galaxy.GenerateEmpireFlag(empire_0.MainColor, empire_0.SecondaryColor, empire_0.FlagShape, Galaxy.FlagShapes, ref smallFlagPicture, ref largeFlagPicture);
            }
            if (empire_0.PirateEmpireBaseHabitat != null)
            {
                Bitmap mediumFlagPicture = GraphicsHelper.ScaleImage(largeFlagPicture, 35, 21, 1f, lowQuality: false);
                empire_0.MediumFlagPicture = mediumFlagPicture;
                if (!empire_0.PirateEmpireSuperPirates)
                {
                    using Graphics graphics = Graphics.FromImage(largeFlagPicture);
                    GraphicsHelper.SetGraphicsQualityToHigh(graphics);
                    graphics.DrawImage(srcRect: new Rectangle(0, 0, _Game.Galaxy.PirateFlagLarge.Width, _Game.Galaxy.PirateFlagLarge.Height), destRect: new Rectangle(2, 2, 35, 22), image: _Game.Galaxy.PirateFlagLarge, srcUnit: GraphicsUnit.Pixel);
                }
                mainView.DisposePirateFlagTextures();
            }
            empire_0.SmallFlagPicture = smallFlagPicture;
            empire_0.LargeFlagPicture = largeFlagPicture;
            empire_0.CivilityRating = sldEditEmpireReputation.Value;
            empire_0.WarWearinessRaw = sldEditEmpireWarWeariness.Value;
            if (empire_0 == _Game.PlayerEmpire)
            {
                btnEmpireSummary.Image = PrecacheScaledBitmap(_Game.PlayerEmpire.LargeFlagPicture, 50, 30);
            }
            empire_0.Description = txtEditEmpireDescription.Text;
            empire_0.PlayableInScenario = chkEditEmpirePlayableInScenario.Checked;
            ctlEditEmpireRelationList.FinalizeRelations(_Game.Galaxy);
        }

        private double method_499(int int_64)
        {
            double result = 0.0;
            switch (int_64)
            {
                case 0:
                    result = 0.0;
                    break;
                case 1:
                    result = 1.0;
                    break;
                case 2:
                    result = 2.0;
                    break;
                case 3:
                    result = 3.0;
                    break;
                case 4:
                    result = 4.0;
                    break;
                case 5:
                    result = 5.0;
                    break;
                case 6:
                    result = 6.0;
                    break;
                case 7:
                    result = 7.0;
                    break;
            }
            return result;
        }

        private void btnEditEmpireApplyTechLevel_Click(object sender, EventArgs e)
        {
            double techLevel = method_499(tbarEmpireTechLevel.Value);
            if (empire_0 != null && empire_0.Research != null && empire_0.Research.TechTree != null)
            {
                empire_0.Research.ResearchQueueEnergy.Clear();
                empire_0.Research.ResearchQueueHighTech.Clear();
                empire_0.Research.ResearchQueueWeapons.Clear();
                for (int i = 0; i < empire_0.Research.TechTree.Count; i++)
                {
                    empire_0.Research.TechTree[i].IsRushing = false;
                }
                empire_0.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeLevel(_Game.Galaxy, empire_0.Research.TechTree, empire_0.DominantRace, techLevel, isPirate: false);
                empire_0.Research.Update(empire_0.DominantRace);
                empire_0.ReviewResearchAbilities();
                empire_0.ReviewDesignsBuiltObjectsImprovedComponents();
            }
        }

        private void method_500()
        {
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                method_154();
            }
            pnlEditEmpire.Size = new Size(700, 760);
            pnlEditEmpire.Location = new Point((mainView.Width - pnlEditEmpire.Width) / 2, (mainView.Height - pnlEditEmpire.Height) / 2);
            lblEditEmpireTitle.Font = font_1;
            lblEditEmpireTitle.ForeColor = color_0;
            lblEditEmpireTitle.BackColor = Color.Transparent;
            lblEditEmpireTitle.Text = TextResolver.GetText("Edit Empire");
            lblEditEmpireTitle.Location = new Point(42, 9);
            lblEditEmpireTitle.SendToBack();
            picEditEmpire.Location = new Point(10, 4);
            btnEditEmpireClose.Font = new Font(font_3.FontFamily, 16.67f, FontStyle.Bold);
            btnEditEmpireClose.Location = new Point(540, 8);
            btnEditEmpireClose.Size = new Size(150, 25);
            tabEditEmpire.Size = new Size(680, 710);
            tabEditEmpire.Location = new Point(10, 38);
            lblEditEmpireName.Location = new Point(10, 10);
            lblEditEmpireRace.Location = new Point(10, 40);
            lblEditEmpireGovernmentStyle.Location = new Point(10, 70);
            lblEditEmpireMoney.Location = new Point(10, 100);
            lblEditEmpirePrimaryColor.Location = new Point(440, 10);
            lblEditEmpireSecondaryColor.Location = new Point(440, 40);
            lblEditEmpireFlagShape.Location = new Point(440, 70);
            lblEditEmpireReputation.Location = new Point(10, 143);
            lblEditEmpireWarWeariness.Location = new Point(10, 173);
            txtEditEmpireName.Size = new Size(280, 18);
            txtEditEmpireName.Location = new Point(110, 8);
            cmbEditEmpireRace.Size = new Size(200, 21);
            cmbEditEmpireRace.Location = new Point(110, 37);
            cmbEditEmpireRace.BindData(font_3, _Game.Galaxy.Races, raceImageCache_0.GetRaceImages());
            cmbEditEmpireGovernmentStyle.Size = new Size(200, 21);
            cmbEditEmpireGovernmentStyle.Location = new Point(110, 68);
            cmbEditEmpireGovernmentStyle.Ignite();
            cmbEditEmpireGovernmentStyle.BringToFront();
            numEditEmpireMoney.Size = new Size(100, 21);
            numEditEmpireMoney.Location = new Point(110, 98);
            cmbEditEmpirePirateStyle.Size = new Size(200, 21);
            cmbEditEmpirePirateStyle.Location = new Point(110, 68);
            cmbEditEmpirePirateStyle.BringToFront();
            cmbEditEmpirePrimaryColor.Size = new Size(100, 21);
            cmbEditEmpirePrimaryColor.Location = new Point(550, 8);
            cmbEditEmpirePrimaryColor.Ignite();
            cmbEditEmpireSecondaryColor.Size = new Size(100, 21);
            cmbEditEmpireSecondaryColor.Location = new Point(550, 38);
            cmbEditEmpireSecondaryColor.Ignite(allowWhite: true, allowBlack: false, useDarkerPalette: false, Color.Empty);
            cmbEditEmpireFlagShape.Size = new Size(60, 35);
            cmbEditEmpireFlagShape.Location = new Point(550, 68);
            cmbEditEmpireFlagShape.Ignite();
            sldEditEmpireReputation.Size = new Size(370, 16);
            sldEditEmpireReputation.Location = new Point(110, 143);
            sldEditEmpireReputation.Minimum = -100;
            sldEditEmpireReputation.Maximum = 30;
            lblEditEmpireReputationSummary.Location = new Point(500, 143);
            sldEditEmpireWarWeariness.Size = new Size(370, 16);
            sldEditEmpireWarWeariness.Location = new Point(110, 173);
            sldEditEmpireWarWeariness.Minimum = 0;
            sldEditEmpireWarWeariness.Maximum = 40;
            lblEditEmpireWarWearinessSummary.Location = new Point(500, 173);
            lblEditEmpireDescription.Location = new Point(10, 212);
            lblEditEmpireDescription.Text = TextResolver.GetText("Empire Description Label");
            txtEditEmpireDescription.Location = new Point(10, 230);
            txtEditEmpireDescription.Size = new Size(650, 100);
            txtEditEmpireDescription.ScrollBars = ScrollBars.Vertical;
            chkEditEmpirePlayableInScenario.Location = new Point(10, 331);
            chkEditEmpirePlayableInScenario.Font = font_3;
            chkEditEmpirePlayableInScenario.Text = TextResolver.GetText("Playable in Galaxy Map");
            txtEditEmpireDescription.BringToFront();
            lblEditEmpireRelationshipsTitle.Location = new Point(10, 368);
            lblEditEmpireRelationshipsTitle.Font = font_2;
            lblEditEmpireRelationshipsTitle.SendToBack();
            ctlEditEmpireRelationList.Size = new Size(650, 280);
            ctlEditEmpireRelationList.Location = new Point(10, 390);
            ctlEditEmpireRelationList.Grid.Columns["Empire"].Width = 30;
            ctlEditEmpireRelationList.Grid.Columns["Race"].Width = 40;
            ctlEditEmpireRelationList.Grid.Columns["Name"].Width = 320;
            ctlEditEmpireRelationList.Grid.Columns["Relation"].Width = 180;
            ctlEditEmpireRelationList.Grid.Columns["Bias"].Width = 60;
            ctlEditEmpireColonies.Size = new Size(400, 660);
            ctlEditEmpireColonies.Location = new Point(10, 10);
            ctlEditEmpireColonies.Grid.Columns["Quality"].Visible = false;
            ctlEditEmpireColonies.Grid.Columns["Approval"].Visible = false;
            ctlEditEmpireColonies.Grid.Columns["TaxRate"].Visible = false;
            ctlEditEmpireColonies.Grid.Columns["AnnualRevenue"].Visible = false;
            ctlEditEmpireColonies.Grid.Columns["Type"].Visible = false;
            ctlEditEmpireColonies.Grid.Columns["Facilities"].Visible = false;
            pnlEditEmpireColonyInfo.Size = new Size(240, 240);
            pnlEditEmpireColonyInfo.Location = new Point(420, 10);
            btnEditEmpireColonyGoto.Size = new Size(240, 45);
            btnEditEmpireColonyGoto.Location = new Point(420, 260);
            ctlEditEmpireBuiltObjectList.Size = new Size(400, 660);
            ctlEditEmpireBuiltObjectList.Location = new Point(10, 10);
            pnlEditEmpireBuiltObjectInfo.Size = new Size(240, 240);
            pnlEditEmpireBuiltObjectInfo.Location = new Point(420, 10);
            btnEditEmpireBuiltObjectGoto.Size = new Size(240, 45);
            btnEditEmpireBuiltObjectGoto.Location = new Point(420, 260);
            btnEditEmpireBuiltObjectAutoGen.Visible = false;
            btnEditEmpireCharacters.Location = new Point(235, 30);
            btnEditEmpireCharacters.Text = TextResolver.GetText("Edit Characters");
            tbarEmpireTechLevel.Location = new Point(10, 20);
            tbarEmpireTechLevel.Size = new Size(650, 40);
            tbarEmpireTechLevel.SetLabels(new string[8] { "Starting", "Level 1", "Level 2", "Level 3", "Level 4", "Level 5", "Level 6", "Level 7" });
            tbarEmpireTechLevel.Value = 0;
            btnEditEmpireApplyTechLevel.Size = new Size(200, 30);
            btnEditEmpireApplyTechLevel.Location = new Point(235, 70);
            btnEditEmpireSelectTechs.Size = new Size(200, 30);
            btnEditEmpireSelectTechs.Location = new Point(235, 130);
            method_497();
            txtEditEmpireName.Focus();
            txtEditEmpireName.SelectAll();
            pnlEditEmpire.Visible = true;
            pnlEditEmpire.BringToFront();
        }

        private void method_501()
        {
            if (empire_0 != null)
            {
                method_498();
                method_495();
            }
            empire_0 = null;
            pnlEditEmpire.SendToBack();
            pnlEditEmpire.Visible = false;
        }

        private void btnEditEmpireClose_Click(object sender, EventArgs e)
        {
            method_501();
        }

        private void cmbEditEmpireGovernmentStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbEditEmpirePrimaryColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (empire_0 != null && empire_0.PirateEmpireBaseHabitat != null)
            {
                cmbEditEmpireFlagShape.BindData(cmbEditEmpirePrimaryColor.SelectedColor, cmbEditEmpireSecondaryColor.SelectedColor, Galaxy.FlagShapesPirates);
            }
            else
            {
                cmbEditEmpireFlagShape.BindData(cmbEditEmpirePrimaryColor.SelectedColor, cmbEditEmpireSecondaryColor.SelectedColor, Galaxy.FlagShapes);
            }
            cmbEditEmpireFlagShape.Invalidate();
        }

        private void gDiCkjinNp(object sender, EventArgs e)
        {
            if (empire_0 != null && empire_0.PirateEmpireBaseHabitat != null)
            {
                cmbEditEmpireFlagShape.BindData(cmbEditEmpirePrimaryColor.SelectedColor, cmbEditEmpireSecondaryColor.SelectedColor, Galaxy.FlagShapesPirates);
            }
            else
            {
                cmbEditEmpireFlagShape.BindData(cmbEditEmpirePrimaryColor.SelectedColor, cmbEditEmpireSecondaryColor.SelectedColor, Galaxy.FlagShapes);
            }
            cmbEditEmpireFlagShape.Invalidate();
        }

        private void method_502()
        {
            string text = string.Empty;
            if (empire_0 != null)
            {
                text = empire_0.CivilityDescription();
                text = text + " (" + empire_0.CivilityRating.ToString("#0") + ")";
            }
            lblEditEmpireReputationSummary.Text = text;
        }

        private void method_503()
        {
            string text = string.Empty;
            if (empire_0 != null)
            {
                text = Galaxy.ResolveWarWearinessDescription(empire_0.WarWeariness);
                text = text + " (" + empire_0.WarWeariness.ToString("#0") + ")";
            }
            lblEditEmpireWarWearinessSummary.Text = text;
        }

        private void sldEditEmpireReputation_Scroll(object sender, ScrollEventArgs e)
        {
            empire_0.CivilityRating = sldEditEmpireReputation.Value;
            method_502();
        }

        private void sldEditEmpireWarWeariness_Scroll(object sender, ScrollEventArgs e)
        {
            empire_0.WarWearinessRaw = sldEditEmpireWarWeariness.Value;
            method_503();
        }

        private void cmbEditHabitatEmpire_SelectedIndexChanged(object sender, EventArgs e)
        {
            Empire selectedEmpire = cmbEditHabitatEmpire.SelectedEmpire;
            if (selectedEmpire != null)
            {
                if (selectedEmpire != _Game.Galaxy.IndependentEmpire && !_Game.Galaxy.PirateEmpires.Contains(selectedEmpire))
                {
                    ctlEditHabitatTroops.Enabled = true;
                    btnEditHabitatAddTroop.Enabled = true;
                    btnEditHabitatRemoveTroop.Enabled = true;
                    return;
                }
                if (habitat_4 != null)
                {
                    if (habitat_4.Troops != null)
                    {
                        habitat_4.Troops.Clear();
                    }
                    ctlEditHabitatTroops.BindData(habitat_4.Troops, editable: true);
                }
                ctlEditHabitatTroops.Enabled = false;
                btnEditHabitatAddTroop.Enabled = false;
                btnEditHabitatRemoveTroop.Enabled = false;
                return;
            }
            if (habitat_4 != null)
            {
                if (habitat_4.Troops != null)
                {
                    habitat_4.Troops.Clear();
                }
                ctlEditHabitatTroops.BindData(habitat_4.Troops, editable: true);
            }
            ctlEditHabitatTroops.Enabled = false;
            btnEditHabitatAddTroop.Enabled = false;
            btnEditHabitatRemoveTroop.Enabled = false;
        }

        private void ctlEditEmpireColonies_SelectionChanged(object sender, EventArgs e)
        {
            Habitat selectedHabitat = ctlEditEmpireColonies.SelectedHabitat;
            if (selectedHabitat != null)
            {
                Bitmap backgroundPicture = mainView.method_54(selectedHabitat, pnlEditEmpireColonyInfo.ClientSize.Width);
                pnlEditEmpireColonyInfo.SetData(_Game, _Game.Galaxy, backgroundPicture, selectedHabitat);
            }
        }

        private void ctlEditEmpireBuiltObjectList_SelectionChanged(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlEditEmpireBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(selectedBuiltObject);
                Bitmap image = new Bitmap(builtObjectImageData.Image);
                image = ((selectedBuiltObject.Empire == null) ? PrepareBuiltObjectImage(selectedBuiltObject, image, Color.Gray, Color.Gray, 1.0, 1) : PrepareBuiltObjectImage(selectedBuiltObject, image, selectedBuiltObject.Empire.MainColor, selectedBuiltObject.Empire.SecondaryColor, 1.0, 1));
                pnlEditEmpireBuiltObjectInfo.SetData(_Game, _Game.Galaxy, image, new Bitmap(builtObjectImageData.MaskImage), selectedBuiltObject);
            }
        }

        private void btnEditEmpireBuiltObjectGoto_Click(object sender, EventArgs e)
        {
            BuiltObject selectedBuiltObject = ctlEditEmpireBuiltObjectList.SelectedBuiltObject;
            if (selectedBuiltObject != null)
            {
                int_13 = (int)selectedBuiltObject.Xpos;
                int_14 = (int)selectedBuiltObject.Ypos;
                method_149();
                bool_20 = true;
            }
        }

        private void btnEditEmpireBuiltObjectAutoGen_Click(object sender, EventArgs e)
        {
        }

        private void btnEditEmpireColonyGoto_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = ctlEditEmpireColonies.SelectedHabitat;
            if (selectedHabitat != null)
            {
                int_13 = (int)selectedHabitat.Xpos;
                int_14 = (int)selectedHabitat.Ypos;
                method_149();
                bool_20 = true;
            }
        }

        private void method_504()
        {
            pnlGameEditorPassword.Size = new Size(360, 280);
            pnlGameEditorPassword.Location = new Point((mainView.Width - pnlGameEditorPassword.Width) / 2, (mainView.Height - pnlGameEditorPassword.Height) / 2);
            lblGameEditorPasswordTitle.Location = new Point(45, 10);
            lblGameEditorPasswordTitle.Font = font_2;
            picGameEditorPassword.Location = new Point(10, 4);
            lblGameEditorPasswordDescription.Location = new Point(10, 40);
            string text = TextResolver.GetText("You may enter a password below to protect this game from unauthorized editing");
            lblGameEditorPasswordDescription.MaximumSize = new Size(340, 50);
            lblGameEditorPasswordDescription.Text = text;
            lblGameEditorPassword.Location = new Point(10, 103);
            txtGameEditorPassword.Location = new Point(70, 100);
            txtGameEditorPassword.Size = new Size(270, 21);
            txtGameEditorPassword.Text = _Game.EditorPassword;
            lblGameEditorPasswordButtonDescription.Location = new Point(10, 130);
            text = TextResolver.GetText("Click 'Save' to save the password");
            lblGameEditorPasswordButtonDescription.MaximumSize = new Size(340, 105);
            lblGameEditorPasswordButtonDescription.Text = text;
            btnGameEditorPasswordOk.Size = new Size(165, 25);
            btnGameEditorPasswordOk.Location = new Point(10, 245);
            btnGameEditorPasswordCancel.Size = new Size(165, 25);
            btnGameEditorPasswordCancel.Location = new Point(185, 245);
            pnlGameEditorPassword.Visible = true;
            pnlGameEditorPassword.BringToFront();
            txtGameEditorPassword.SelectAll();
            txtGameEditorPassword.Focus();
        }

        private void method_505()
        {
            pnlGameEditorPassword.SendToBack();
            pnlGameEditorPassword.Visible = false;
        }

        private void btnGameEditorPasswordCancel_Click(object sender, EventArgs e)
        {
            _Game.EditorPassword = string.Empty;
            method_505();
            method_90();
            if (galaxyTimeState_0 == GalaxyTimeState.Running)
            {
                method_155();
            }
            method_475();
        }

        private void btnGameEditorPasswordOk_Click(object sender, EventArgs e)
        {
            string editorPassword = txtGameEditorPassword.Text;
            _Game.EditorPassword = editorPassword;
            method_505();
            method_90();
            if (galaxyTimeState_0 == GalaxyTimeState.Running)
            {
                method_155();
            }
            method_475();
        }

        private void method_506()
        {
            pnlGameEditorEnterPassword.Size = new Size(300, 160);
            pnlGameEditorEnterPassword.Location = new Point((mainView.Width - pnlGameEditorEnterPassword.Width) / 2, (mainView.Height - pnlGameEditorEnterPassword.Height) / 2);
            lblGameEditorEnterPasswordTitle.Location = new Point(45, 10);
            lblGameEditorEnterPasswordTitle.Font = font_2;
            picGameEditorEnterPassword.Location = new Point(10, 4);
            lblGameEditorEnterPasswordDescription.Location = new Point(10, 40);
            string text = TextResolver.GetText("Editing of this game is protected by a password");
            lblGameEditorEnterPasswordDescription.MaximumSize = new Size(280, 45);
            lblGameEditorEnterPasswordDescription.Text = text;
            lblGameEditorEnterPassword.Location = new Point(10, 98);
            txtGameEditorEnterPassword.Location = new Point(70, 95);
            txtGameEditorEnterPassword.Size = new Size(220, 21);
            txtGameEditorEnterPassword.Text = string.Empty;
            btnGameEditorEnterPasswordOk.Size = new Size(135, 25);
            btnGameEditorEnterPasswordOk.Location = new Point(10, 125);
            btnGameEditorEnterPasswordCancel.Size = new Size(135, 25);
            btnGameEditorEnterPasswordCancel.Location = new Point(155, 125);
            pnlGameEditorEnterPassword.Visible = true;
            pnlGameEditorEnterPassword.BringToFront();
            txtGameEditorEnterPassword.Focus();
        }

        private void method_507()
        {
            pnlGameEditorEnterPassword.SendToBack();
            pnlGameEditorEnterPassword.Visible = false;
        }

        private void btnGameEditorEnterPasswordCancel_Click(object sender, EventArgs e)
        {
            bool_11 = false;
            method_155();
            method_507();
        }

        private void btnGameEditorEnterPasswordOk_Click(object sender, EventArgs e)
        {
            if (txtGameEditorEnterPassword.Text == _Game.EditorPassword)
            {
                method_507();
                method_476();
            }
            else
            {
                MessageBoxEx messageBoxEx = method_370(TextResolver.GetText("The password you entered was incorrect"), TextResolver.GetText("Invalid password"));
                messageBoxEx.Show(this);
            }
        }

        private void txtGameEditorEnterPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnGameEditorEnterPasswordOk_Click(this, new EventArgs());
            }
        }

        private void txtGameEditorPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnGameEditorPasswordOk_Click(this, new EventArgs());
            }
        }

        private void method_508(Bitmap bitmap_225, string string_30, string string_31, bool bool_28, bool bool_29)
        {
            if (bool_29)
            {
                method_571(string_30, string_31);
                return;
            }
            method_154();
            lblEventMessageText.Text = string.Empty;
            lblEventMessageText.Size = new Size(0, 0);
            pnlEventMessageContainer.VerticalScroll.Value = 0;
            picEventMessage.Image = bitmap_225;
            lblEventMessageTitle.Text = string_30;
            lblEventMessageText.Text = string_31;
            btnEventMessageClose.Visible = true;
            btnEventMessageGoto.Visible = true;
            btnEventMessageInvestigate.Visible = false;
            btnEventMessageAvoid.Visible = false;
            btnEventMessageClose.BringToFront();
            btnEventMessageGoto.BringToFront();
            btnEventMessageInvestigate.BringToFront();
            btnEventMessageAvoid.BringToFront();
            method_512(bool_28);
        }


  }

}