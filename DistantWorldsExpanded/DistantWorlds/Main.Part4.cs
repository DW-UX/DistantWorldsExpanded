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


        private void method_509(Bitmap bitmap_225, string string_30, string string_31, string string_32, string string_33)
        {
            method_154();
            lblEventMessageText.Text = string.Empty;
            lblEventMessageText.Size = new Size(0, 0);
            pnlEventMessageContainer.VerticalScroll.Value = 0;
            picEventMessage.Image = bitmap_225;
            lblEventMessageTitle.Text = string_30;
            lblEventMessageText.Text = string_31;
            btnEventMessageInvestigate.Visible = true;
            btnEventMessageInvestigate.Text = string_32;
            btnEventMessageAvoid.Visible = true;
            btnEventMessageAvoid.Text = string_33;
            btnEventMessageClose.Visible = false;
            btnEventMessageGoto.Visible = false;
            method_513(bool_28: false, 30);
        }

        private void method_510(Bitmap bitmap_225, string string_30, string string_31)
        {
            method_154();
            lblEventMessageText.Text = string.Empty;
            lblEventMessageText.Size = new Size(0, 0);
            pnlEventMessageContainer.VerticalScroll.Value = 0;
            picEventMessage.Image = bitmap_225;
            lblEventMessageTitle.Text = string_30;
            lblEventMessageText.Text = string_31;
            btnEventMessageInvestigate.Visible = true;
            btnEventMessageInvestigate.Text = TextResolver.GetText("Investigate Ruins");
            btnEventMessageAvoid.Visible = true;
            btnEventMessageAvoid.Text = TextResolver.GetText("Leave the Ruins alone");
            btnEventMessageClose.Visible = false;
            btnEventMessageGoto.Visible = false;
            btnEventMessageClose.BringToFront();
            btnEventMessageGoto.BringToFront();
            btnEventMessageInvestigate.BringToFront();
            btnEventMessageAvoid.BringToFront();
            method_512(bool_28: false);
        }

        private void method_511(Bitmap bitmap_225, string string_30, string string_31, BuiltObject builtObject_8)
        {
            method_154();
            lblEventMessageText.Text = string.Empty;
            lblEventMessageText.Size = new Size(0, 0);
            pnlEventMessageContainer.VerticalScroll.Value = 0;
            picEventMessage.Image = bitmap_225;
            lblEventMessageTitle.Text = string_30;
            lblEventMessageText.Text = string_31;
            string text = TextResolver.GetText("Investigate Ship");
            string text2 = TextResolver.GetText("Leave the Ship alone");
            if (builtObject_8.Role == BuiltObjectRole.Base)
            {
                text = TextResolver.GetText("Investigate Base");
                text2 = TextResolver.GetText("Leave the Base alone");
            }
            btnEventMessageInvestigate.Visible = true;
            btnEventMessageInvestigate.Text = text;
            btnEventMessageAvoid.Visible = true;
            btnEventMessageAvoid.Text = text2;
            btnEventMessageClose.Visible = false;
            btnEventMessageGoto.Visible = false;
            method_512(bool_28: false);
        }

        private void method_512(bool bool_28)
        {
            method_513(bool_28, 0);
        }

        private void method_513(bool bool_28, int int_64)
        {
            bool_9 = true;
            int num = 420;
            int num2 = 660;
            int num3 = 360;
            int num4 = 270;
            if (bool_28)
            {
                num3 = 200;
                num4 = 150;
            }
            pnlEventMessage.Size = new Size(num, num2);
            pnlEventMessage.Location = new Point((mainView.Width - pnlEventMessage.Width) / 2, (mainView.Height - pnlEventMessage.Height) / 2);
            pnlEventMessagePanel.Size = new Size(num - 20, num2 - 20);
            pnlEventMessagePanel.Location = new Point(10, 10);
            lnkEventMessageLink.Font = font_6;
            bool flag = false;
            if (object_2 is BuiltObject)
            {
                flag = true;
            }
            else if (object_2 is Habitat)
            {
                flag = true;
            }
            else if (object_2 is Creature)
            {
                flag = true;
            }
            else if (object_2 is Character)
            {
                flag = true;
            }
            else if (object_2 is Point)
            {
                flag = true;
            }
            lblEventMessageTitle.Font = font_2;
            lblEventMessageText.Font = font_6;
            picEventMessage.SizeMode = PictureBoxSizeMode.Zoom;
            if (picEventMessage.Image != null)
            {
                picEventMessage.Size = new Size(num3, num4);
                picEventMessage.Location = new Point((num - 20 - num3) / 2, 10);
                picEventMessage.Visible = true;
                SizeF empty = SizeF.Empty;
                using (Graphics graphics = lblEventMessageTitle.CreateGraphics())
                {
                    empty = graphics.MeasureString(lblEventMessageTitle.Text, lblEventMessageTitle.Font, num - 40);
                    lblEventMessageTitle.MaximumSize = new Size((int)empty.Width + 1, (int)empty.Height + 1);
                }
                int num5 = (pnlEventMessagePanel.Width - lblEventMessageTitle.Width) / 2;
                int num6 = (pnlEventMessagePanel.Width - lblEventMessageText.Width) / 2;
                lblEventMessageTitle.Location = new Point(num5, num4 + 20);
                lblEventMessageText.Location = new Point(num6, num4 + 50);
                lblEventMessageText.MaximumSize = new Size(num - 55, 2000);
                lblEventMessageText.Location = new Point(0, 0);
                lblEventMessageText.Size = lblEventMessageText.PreferredSize;
                if (lnkEventMessageLink.Enabled)
                {
                    lnkEventMessageLink.Location = new Point(0, lblEventMessageText.Size.Height + 10);
                }
                else
                {
                    lnkEventMessageLink.Location = new Point(0, 10);
                }
                pnlEventMessageContainer.Size = new Size(num - 35, pnlEventMessagePanel.Height - (num4 + 75 + lblEventMessageTitle.Height + int_64));
                pnlEventMessageContainer.Location = new Point(10, num4 + 20 + lblEventMessageTitle.Height + 5);
            }
            else
            {
                picEventMessage.Size = new Size(num - 40, 270);
                picEventMessage.Location = new Point(10, 10);
                picEventMessage.Visible = false;
                SizeF empty2 = SizeF.Empty;
                using (Graphics graphics2 = lblEventMessageTitle.CreateGraphics())
                {
                    empty2 = graphics2.MeasureString(lblEventMessageTitle.Text, lblEventMessageTitle.Font, num - 40);
                    lblEventMessageTitle.MaximumSize = new Size((int)empty2.Width + 1, (int)empty2.Height + 1);
                }
                lblEventMessageText.Size = new Size(num - 40, pnlEventMessagePanel.Height - (50 + int_64));
                int num7 = (pnlEventMessagePanel.Width - lblEventMessageTitle.Width) / 2;
                int num8 = (pnlEventMessagePanel.Width - lblEventMessageText.Width) / 2;
                lblEventMessageTitle.Location = new Point(num7, 10);
                lblEventMessageText.Location = new Point(num8, 10 + lblEventMessageTitle.Height + 5);
                lblEventMessageText.MaximumSize = new Size(num - 55, 2000);
                lblEventMessageText.Location = new Point(0, 0);
                lblEventMessageText.Size = lblEventMessageText.PreferredSize;
                if (lnkEventMessageLink.Enabled)
                {
                    lnkEventMessageLink.Location = new Point(0, lblEventMessageText.Size.Height + 10);
                }
                else
                {
                    lnkEventMessageLink.Location = new Point(0, 10);
                }
                pnlEventMessageContainer.Size = new Size(num - 35, pnlEventMessagePanel.Height - (75 + lblEventMessageTitle.Height + int_64));
                pnlEventMessageContainer.Location = new Point(10, 20 + lblEventMessageTitle.Height + 5);
            }
            btnEventMessageAvoid.Parent = pnlEventMessagePanel;
            btnEventMessageClose.Parent = pnlEventMessagePanel;
            btnEventMessageGoto.Parent = pnlEventMessagePanel;
            btnEventMessageInvestigate.Parent = pnlEventMessagePanel;
            if (!pnlEventMessagePanel.Controls.Contains(btnEventMessageAvoid))
            {
                pnlEventMessagePanel.Controls.Add(btnEventMessageAvoid);
            }
            if (!pnlEventMessagePanel.Controls.Contains(btnEventMessageClose))
            {
                pnlEventMessagePanel.Controls.Add(btnEventMessageClose);
            }
            if (!pnlEventMessagePanel.Controls.Contains(btnEventMessageGoto))
            {
                pnlEventMessagePanel.Controls.Add(btnEventMessageGoto);
            }
            if (!pnlEventMessagePanel.Controls.Contains(btnEventMessageInvestigate))
            {
                pnlEventMessagePanel.Controls.Add(btnEventMessageInvestigate);
            }
            int num9 = num - 20;
            int num10 = num2 - (10 + int_64);
            int num11 = (num9 - 360) / 2;
            btnEventMessageInvestigate.Size = new Size(175, 30 + int_64);
            btnEventMessageInvestigate.Location = new Point(num11, num10 - 50);
            btnEventMessageAvoid.Size = new Size(175, 30 + int_64);
            btnEventMessageAvoid.Location = new Point(num11 + 185, num10 - 50);
            if (flag)
            {
                btnEventMessageGoto.Size = new Size(175, 30);
                btnEventMessageGoto.Location = new Point(num11 + 185, num10 - 50);
                btnEventMessageGoto.Text = TextResolver.GetText("Go to Event Location");
                btnEventMessageGoto.Visible = true;
                btnEventMessageClose.Size = new Size(175, 30);
                btnEventMessageClose.Location = new Point(num11, num10 - 50);
            }
            else
            {
                btnEventMessageClose.Size = new Size(175, 30);
                btnEventMessageClose.Location = new Point(num11 + 92, num10 - 50);
                btnEventMessageGoto.Visible = false;
            }
            pnlEventMessage.Visible = true;
            pnlEventMessage.BringToFront();
        }

        private void method_514()
        {
            pnlEventMessage.SendToBack();
            pnlEventMessage.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void method_515()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\wonder.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\wonder.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_516()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\happyEvent.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\happyEvent.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_517()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\raceEvent.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\raceEvent.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_518()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\characterEvent.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\characterEvent.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_519()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\disaster.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\disaster.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_520()
        {
            musicPlayer_0.FadePause();
            string filePath = Application.StartupPath + "\\sounds\\effects\\dread.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\dread.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_521(Empire empire_5)
        {
            double num = 0.0;
            bool flag = false;
            bool flag2 = false;
            string text = string.Empty;
            string empty = string.Empty;
            if (empire_5 != null)
            {
                EmpireEvaluation empireEvaluation = empire_5.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                if (empireEvaluation != null)
                {
                    num = empireEvaluation.OverallAttitude;
                }
                if (empire_5.PirateEmpireBaseHabitat != null)
                {
                    flag = true;
                }
                if (empire_5.Reclusive)
                {
                    flag2 = true;
                }
                if (empire_5.DominantRace != null)
                {
                    text = empire_5.DominantRace.Name + "\\";
                }
            }
            musicPlayer_0.FadePause();
            string text2 = "discovery.mp3";
            string text3 = Application.StartupPath + "\\sounds\\effects\\discovery.mp3";
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            if (flag)
            {
                text3 = Application.StartupPath + "\\sounds\\effects\\diplomacyMoodMenacing.mp3";
                text2 = "diplomacyMoodMenacing.mp3";
            }
            else if (flag2)
            {
                text3 = Application.StartupPath + "\\sounds\\effects\\diplomacyMoodNeutral.mp3";
                text2 = "diplomacyMoodNeutral.mp3";
            }
            else if (num < -10.0)
            {
                text3 = Application.StartupPath + "\\sounds\\effects\\diplomacyMoodAngry.mp3";
                text2 = "diplomacyMoodAngry.mp3";
            }
            else if (num < 10.0)
            {
                text3 = Application.StartupPath + "\\sounds\\effects\\diplomacyMoodNeutral.mp3";
                text2 = "diplomacyMoodNeutral.mp3";
            }
            else
            {
                text3 = Application.StartupPath + "\\sounds\\effects\\diplomacyMoodHappy.mp3";
                text2 = "diplomacyMoodHappy.mp3";
            }
            if (!string.IsNullOrEmpty(text))
            {
                empty = Application.StartupPath + "\\sounds\\effects\\" + text + text2;
                if (File.Exists(empty))
                {
                    text3 = empty;
                }
            }
            if (!string.IsNullOrEmpty(string_3))
            {
                string text4 = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\" + text2;
                if (File.Exists(text4))
                {
                    text3 = text4;
                }
                if (!string.IsNullOrEmpty(text))
                {
                    empty = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\" + text + text2;
                    if (File.Exists(empty))
                    {
                        text3 = empty;
                    }
                }
            }
            musicPlayer_1.PlayMusicFileMethodDelegate(text3);
        }

        private void ArhCaEfBkk()
        {
            musicPlayer_0.FadePause();
            string filePath =  Application.StartupPath + "sounds\\effects\\discovery.mp3";
            if (!string.IsNullOrEmpty(string_3))
            {
                string text = Application.StartupPath + "\\Customization\\" + string_3 + "\\sounds\\effects\\discovery.mp3";
                if (File.Exists(text))
                {
                    filePath = text;
                }
            }
            musicPlayer_1.SetVolume(_Game.MusicVolume);
            musicPlayer_1.PlayMusicFileMethodDelegate(filePath);
        }

        private void method_522()
        {
            if (musicPlayer_1.IsPlaying)
            {
                musicPlayer_1.FadeStop();
                musicPlayer_0.FadeResume();
            }
            else if (!musicPlayer_0.IsPlaying)
            {
                musicPlayer_0.ForceSwitch();
            }
        }

        void IEventMessageRecipient.ReceiveEventMessage(EventMessageType eventMessageType, string title, string message, object additionalData, object location)
        {
            Delegate6 method = method_523;
            BeginInvoke(method, eventMessageType, title, message, additionalData, location);
        }

        private void method_523(EventMessageType eventMessageType_1, string string_30, string string_31, object object_7, object object_8)
        {
            bool flag = true;
            if (gameOptions_0 != null && gameOptions_0.SuppressAllPopups)
            {
                flag = false;
            }
            bool flag2 = false;
            Bitmap bitmap = null;
            bool bool_ = false;
            bool bool_2 = false;
            race_0 = null;
            lnkEventMessageLink.Enabled = false;
            lnkEventMessageLink.Visible = false;
            lnkEventMessageLink.SendToBack();
            EmpireMessageType empireMessageType = EmpireMessageType.Informational;
            if (eventMessageType_1 != EventMessageType.RogueFleetDefectsToUs && eventMessageType_1 != EventMessageType.UncoverPirateAttackFundingAnotherEmpire && eventMessageType_1 != EventMessageType.UncoverPlanetDestroyerConstruction)
            {
                object_3 = null;
                eventMessageType_0 = EventMessageType.Undefined;
                switch (eventMessageType_1)
                {
                    case EventMessageType.EncounterBuiltObject:
                        empireMessageType = EmpireMessageType.ExplorationBuiltObject;
                        if (object_7 is BuiltObject)
                        {
                            BuiltObject builtObject = (hOcmlqpCrp = (BuiltObject)object_7);
                            bitmap = PrepareBuiltObjectImage(builtObject, builtObjectImageCache_0.ObtainImage(builtObject), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            bitmap = method_6(bitmap, (double)builtObject.Heading * -1.0);
                            if (_Game.DisplayMessageExploration)
                            {
                                EmpireMessage empireMessage = new EmpireMessage(_Game.PlayerEmpire, empireMessageType, object_8);
                                empireMessage.Description = string_31;
                                empireMessage.Title = string_30;
                                empireMessage.SupressPopup = true;
                                _Game.PlayerEmpire.SendMessageToEmpire(empireMessage, _Game.PlayerEmpire);
                            }
                            if (flag)
                            {
                                ArhCaEfBkk();
                                method_511(bitmap, string_30, string_31, builtObject);
                            }
                        }
                        return;
                    case EventMessageType.EncounterRuins:
                        empireMessageType = EmpireMessageType.ExplorationRuins;
                        if (object_7 is Habitat)
                        {
                            Habitat habitat = (habitat_5 = (Habitat)object_7);
                            if (habitat.Ruin != null)
                            {
                                bitmap = this.bitmap_2[habitat.Ruin.PictureRef];
                            }
                            if (_Game.DisplayMessageExploration)
                            {
                                EmpireMessage empireMessage2 = new EmpireMessage(_Game.PlayerEmpire, empireMessageType, object_8);
                                empireMessage2.Description = string_31;
                                empireMessage2.Title = string_30;
                                empireMessage2.SupressPopup = true;
                                _Game.PlayerEmpire.SendMessageToEmpire(empireMessage2, _Game.PlayerEmpire);
                            }
                            if (flag)
                            {
                                ArhCaEfBkk();
                                method_510(bitmap, string_30, string_31);
                            }
                        }
                        return;
                }
                int num = 0;
                switch (eventMessageType_1)
                {
                    case EventMessageType.NewEmpireRaceAbility:
                        {
                            Race race3 = null;
                            if (object_7 is Race)
                            {
                                race3 = (Race)object_7;
                            }
                            if (race3 != null)
                            {
                                bitmap = raceImageCache_0.GetRaceImage(race3.PictureRef);
                            }
                            empireMessageType = EmpireMessageType.ExplorationHabitat;
                            num = 2;
                            break;
                        }
                    case EventMessageType.ExoticTechDiscovered:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin9 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin9.PictureRef];
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.SpecialGovernmentType:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin3 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin3.PictureRef];
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.CreatureOutbreak:
                        bitmap = (Bitmap)bitmap_10[2][0].Clone();
                        num = 1;
                        if (object_7 != null && object_7 is Creature)
                        {
                            Creature creature2 = (Creature)object_7;
                            switch (creature2.Type)
                            {
                                case CreatureType.Kaltor:
                                    bitmap = (Bitmap)bitmap_10[2][0].Clone();
                                    break;
                                case CreatureType.RockSpaceSlug:
                                    bitmap = (Bitmap)bitmap_10[0][0].Clone();
                                    break;
                                case CreatureType.DesertSpaceSlug:
                                    bitmap = (Bitmap)bitmap_10[1][0].Clone();
                                    break;
                                case CreatureType.Ardilus:
                                    bitmap = (Bitmap)bitmap_10[3][0].Clone();
                                    break;
                                case CreatureType.SilverMist:
                                    bitmap = (Bitmap)bitmap_10[4][0].Clone();
                                    num = 3;
                                    break;
                            }
                        }
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        empireMessageType = EmpireMessageType.BattleUnderAttack;
                        break;
                    case EventMessageType.GalacticRefugees:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin6 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin6.PictureRef];
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.SleepersAwake:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin10 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin10.PictureRef];
                        }
                        else if (object_7 is Race)
                        {
                            Race race4 = (Race)object_7;
                            bitmap = raceImageCache_0.GetRaceImage(race4.PictureRef);
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.NewEmpireEmerges:
                        bitmap = null;
                        num = 3;
                        break;
                    case EventMessageType.LostBuiltObjectCoordinates:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin2 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin2.PictureRef];
                        }
                        else if (object_7 is BuiltObject)
                        {
                            BuiltObject builtObject2 = (BuiltObject)object_7;
                            bitmap = PrepareBuiltObjectImage(builtObject2, builtObjectImageCache_0.ObtainImage(builtObject2), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            bitmap = method_6(bitmap, (double)builtObject2.Heading * -1.0);
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.LostColonyCoordinates:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin8 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin8.PictureRef];
                        }
                        else if (object_7 is Habitat)
                        {
                            Habitat habitat8 = (Habitat)object_7;
                            bitmap = habitatImageCache_0.ObtainImage(habitat8);
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.FreeSuperShip:
                        if (object_7 is Habitat)
                        {
                            Habitat habitat12 = (Habitat)object_7;
                            bitmap = bitmap_29[habitat12.LandscapePictureRef];
                        }
                        else if (object_7 is Character)
                        {
                            Character character5 = (Character)object_7;
                            bitmap = characterImageCache_0.ObtainCharacterImage(character5);
                        }
                        else if (object_7 is BuiltObject)
                        {
                            BuiltObject builtObject5 = (BuiltObject)object_7;
                            bitmap = PrepareBuiltObjectImage(builtObject5, builtObjectImageCache_0.ObtainImage(builtObject5.PictureRef), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            if (builtObject5.UnbuiltComponentCount > 0)
                            {
                                bitmap = method_116(builtObject5, bitmap, 0.4f);
                            }
                            bitmap = method_6(bitmap, (double)builtObject5.Heading * -1.0);
                        }
                        empireMessageType = EmpireMessageType.ExplorationBuiltObject;
                        num = 2;
                        break;
                    case EventMessageType.PirateFactionJoinsYou:
                        bitmap = bitmap_49;
                        empireMessageType = EmpireMessageType.ExplorationBuiltObject;
                        num = 3;
                        break;
                    case EventMessageType.TreasureFound:
                        bitmap = bitmap_52;
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 1;
                        break;
                    case EventMessageType.LostColonyFound:
                        if (object_7 is Habitat)
                        {
                            Habitat habitat6 = (Habitat)object_7;
                            bitmap = bitmap_29[habitat6.LandscapePictureRef];
                        }
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.IndependentPopulation:
                        if (object_7 is Race)
                        {
                            Race race2 = (race_0 = (Race)object_7);
                            bitmap = raceImageCache_0.GetRaceImage(race2.PictureRef);
                        }
                        lnkEventMessageLink.Enabled = true;
                        lnkEventMessageLink.BringToFront();
                        lnkEventMessageLink.Visible = true;
                        empireMessageType = EmpireMessageType.ExplorationHabitat;
                        num = 2;
                        break;
                    case EventMessageType.GeneralRuinsDiscovery:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin.PictureRef];
                        }
                        empireMessageType = EmpireMessageType.ExplorationRuins;
                        num = 1;
                        break;
                    case EventMessageType.BuiltObjectExplodes:
                        bitmap = bitmap_28[0];
                        empireMessageType = EmpireMessageType.BattleUnderAttack;
                        num = 1;
                        break;
                    case EventMessageType.PirateAmbush:
                        bitmap = bitmap_49;
                        empireMessageType = EmpireMessageType.BattleUnderAttack;
                        num = 1;
                        break;
                    case EventMessageType.OriginsDiscovery:
                    case EventMessageType.StoryClue:
                        if (object_7 is Habitat)
                        {
                            Habitat habitat7 = (Habitat)object_7;
                            bitmap = bitmap_29[habitat7.LandscapePictureRef];
                        }
                        else if (object_7 is Ruin)
                        {
                            Ruin ruin7 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin7.PictureRef];
                        }
                        else if (object_7 is BuiltObject)
                        {
                            BuiltObject builtObject4 = (BuiltObject)object_7;
                            bitmap = PrepareBuiltObjectImage(builtObject4, builtObjectImageCache_0.ObtainImage(builtObject4), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            bitmap = method_6(bitmap, (double)builtObject4.Heading * -1.0);
                        }
                        bool_ = true;
                        bool_2 = true;
                        empireMessageType = EmpireMessageType.GalacticHistory;
                        num = 2;
                        break;
                    case EventMessageType.RestrictedResourceDiscovered:
                        if (object_7 is Resource)
                        {
                            Resource resource4 = (Resource)object_7;
                            bitmap = _uiResourcesBitmaps[resource4.PictureRef];
                        }
                        else if (object_7 is Habitat)
                        {
                            Habitat habitat9 = (Habitat)object_7;
                            bitmap = bitmap_29[habitat9.LandscapePictureRef];
                        }
                        empireMessageType = EmpireMessageType.RestrictedResourceDiscovered;
                        num = 2;
                        break;
                    case EventMessageType.RuinsEmpireBonus:
                        if (object_7 is Ruin)
                        {
                            Ruin ruin5 = (Ruin)object_7;
                            bitmap = this.bitmap_2[ruin5.PictureRef];
                        }
                        else if (object_7 is Habitat)
                        {
                            Habitat habitat3 = (Habitat)object_7;
                            bitmap = habitatImageCache_0.ObtainImage(habitat3);
                        }
                        empireMessageType = EmpireMessageType.ExplorationRuins;
                        num = 2;
                        break;
                    case EventMessageType.RogueFleetDefectsFromUs:
                        if (object_7 is Empire)
                        {
                            Empire empire = (Empire)object_7;
                            bitmap = empire.LargeFlagPicture;
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.EmpireSplits:
                        if (object_7 is Empire)
                        {
                            Empire empire4 = (Empire)object_7;
                            bitmap = empire4.LargeFlagPicture;
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.UncoverPirateAttackFundingYourEmpire:
                        if (object_7 is EmpireActivity)
                        {
                            EmpireActivity empireActivity = (EmpireActivity)object_7;
                            bitmap = empireActivity.RequestingEmpire.LargeFlagPicture;
                        }
                        else
                        {
                            bitmap = bitmap_49;
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.UncoverKnownLocation:
                        {
                            if (!(object_7 is GalaxyLocation))
                            {
                                break;
                            }
                            GalaxyLocation galaxyLocation2 = (GalaxyLocation)object_7;
                            num = 3;
                            if (galaxyLocation2.Type != GalaxyLocationType.DebrisField)
                            {
                                if (galaxyLocation2.Type != GalaxyLocationType.RestrictedArea)
                                {
                                    break;
                                }
                                goto case EventMessageType.SpecialArea;
                            }
                            goto case EventMessageType.AncientBattleDebrisField;
                        }
                    case EventMessageType.SpecialArea:
                        if (object_7 is GalaxyLocation)
                        {
                            bitmap = bitmap_28[22];
                        }
                        empireMessageType = EmpireMessageType.ExplorationLocation;
                        num = 2;
                        break;
                    case EventMessageType.AncientBattleDebrisField:
                        if (object_7 is GalaxyLocation)
                        {
                            GalaxyLocation galaxyLocation3 = (GalaxyLocation)object_7;
                            if (galaxyLocation3.Type == GalaxyLocationType.DebrisField)
                            {
                                BuiltObjectList builtObjectList2 = _Game.Galaxy.FindAbandonedShipsInDebrisField(galaxyLocation3);
                                if (builtObjectList2 != null && builtObjectList2.Count > 0)
                                {
                                    BuiltObjectImageData builtObjectImageData2 = builtObjectImageCache_0.ObtainImageData(builtObjectList2[0]);
                                    bitmap = PrepareBuiltObjectImage(builtObjectList2[0], builtObjectImageData2.Image, Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                                    bitmap = method_106(builtObjectList2[0], bitmap, builtObjectImageData2.MaskImage);
                                    bitmap = method_6(bitmap, (double)builtObjectList2[0].Heading * -1.0);
                                }
                            }
                        }
                        empireMessageType = EmpireMessageType.ExplorationLocation;
                        num = 2;
                        break;
                    case EventMessageType.RareResourceIntercepted:
                        if (object_7 is Resource)
                        {
                            Resource resource = (Resource)object_7;
                            bitmap = _uiResourcesBitmaps[resource.PictureRef];
                        }
                        empireMessageType = EmpireMessageType.GeneralGoodEvent;
                        num = 3;
                        break;
                    case EventMessageType.GeneralDiscovery:
                        {
                            empireMessageType = EmpireMessageType.ExplorationRuins;
                            num = 1;
                            if (object_7 is Resource)
                            {
                                Resource resource2 = (Resource)object_7;
                                bitmap = _uiResourcesBitmaps[resource2.PictureRef];
                                break;
                            }
                            if (object_7 is PlanetaryFacility)
                            {
                                PlanetaryFacility planetaryFacility = (PlanetaryFacility)object_7;
                                if (planetaryFacility != null)
                                {
                                    bitmap = bitmap_8[planetaryFacility.PictureRef];
                                }
                                break;
                            }
                            if (object_7 is PlanetaryFacilityDefinition)
                            {
                                PlanetaryFacilityDefinition planetaryFacilityDefinition = (PlanetaryFacilityDefinition)object_7;
                                if (planetaryFacilityDefinition != null)
                                {
                                    bitmap = bitmap_8[planetaryFacilityDefinition.PictureRef];
                                }
                                break;
                            }
                            if (object_7 is GalaxyLocation)
                            {
                                GalaxyLocation galaxyLocation = (GalaxyLocation)object_7;
                                if (galaxyLocation.Type == GalaxyLocationType.DebrisField)
                                {
                                    BuiltObjectList builtObjectList = _Game.Galaxy.FindAbandonedShipsInDebrisField(galaxyLocation);
                                    if (builtObjectList != null && builtObjectList.Count > 0)
                                    {
                                        BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(builtObjectList[0]);
                                        bitmap = PrepareBuiltObjectImage(builtObjectList[0], builtObjectImageData.Image, Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                                        bitmap = method_106(builtObjectList[0], bitmap, builtObjectImageData.MaskImage);
                                        bitmap = method_6(bitmap, (double)builtObjectList[0].Heading * -1.0);
                                    }
                                }
                                else if (galaxyLocation.Type == GalaxyLocationType.RestrictedArea)
                                {
                                    bitmap = bitmap_28[22];
                                }
                                break;
                            }
                            if (object_7 is Ruin)
                            {
                                Ruin ruin4 = (Ruin)object_7;
                                bitmap = this.bitmap_2[ruin4.PictureRef];
                                break;
                            }
                            if (object_7 is BuiltObject)
                            {
                                BuiltObject builtObject3 = (BuiltObject)object_7;
                                bitmap = PrepareBuiltObjectImage(builtObject3, builtObjectImageCache_0.ObtainImage(builtObject3), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                                bitmap = method_6(bitmap, (double)builtObject3.Heading * -1.0);
                                empireMessageType = EmpireMessageType.ExplorationBuiltObject;
                                break;
                            }
                            if (object_7 is Habitat)
                            {
                                Habitat habitat2 = (Habitat)object_7;
                                bitmap = habitatImageCache_0.ObtainImage(habitat2.PictureRef);
                                empireMessageType = EmpireMessageType.ExplorationHabitat;
                                break;
                            }
                            if (object_7 is Race)
                            {
                                Race race = (Race)object_7;
                                bitmap = raceImageCache_0.GetRaceImage(race.PictureRef);
                                break;
                            }
                            if (object_7 is Creature)
                            {
                                Creature creature = (Creature)object_7;
                                bitmap = (Bitmap)bitmap_10[creature.PictureRef][0].Clone();
                                break;
                            }
                            if (object_7 is Character)
                            {
                                Character character2 = (Character)object_7;
                                bitmap = characterImageCache_0.ObtainCharacterImage(character2);
                                break;
                            }
                            if (object_7 is DistantWorlds.Types.Component)
                            {
                                DistantWorlds.Types.Component component = (DistantWorlds.Types.Component)object_7;
                                bitmap = bitmap_21[component.PictureRef];
                                if (component.Type == ComponentType.HyperDrive && component.Value1 < 5000)
                                {
                                    bitmap = bitmap_89;
                                }
                                else if (component.Type == ComponentType.HabitationColonization && component.Value1 <= 30000000)
                                {
                                    bitmap = bitmap_90;
                                }
                                break;
                            }
                            if (object_7 is ResearchNode)
                            {
                                ResearchNode researchNode = (ResearchNode)object_7;
                                if (researchNode.Components != null && researchNode.Components.Count > 0)
                                {
                                    bitmap = bitmap_21[researchNode.Components[0].PictureRef];
                                }
                                else if (researchNode.ComponentImprovements != null && researchNode.ComponentImprovements.Count > 0 && researchNode.ComponentImprovements[0].ImprovedComponent != null)
                                {
                                    bitmap = bitmap_21[researchNode.ComponentImprovements[0].ImprovedComponent.PictureRef];
                                }
                                else if (researchNode.PlanetaryFacility != null)
                                {
                                    bitmap = bitmap_8[researchNode.PlanetaryFacility.PictureRef];
                                }
                                break;
                            }
                            if (object_7 is ResearchNodeDefinition)
                            {
                                ResearchNodeDefinition researchNodeDefinition = (ResearchNodeDefinition)object_7;
                                if (researchNodeDefinition.Components != null && researchNodeDefinition.Components.Count > 0)
                                {
                                    bitmap = bitmap_21[researchNodeDefinition.Components[0].PictureRef];
                                }
                                else if (researchNodeDefinition.ComponentImprovements != null && researchNodeDefinition.ComponentImprovements.Count > 0 && researchNodeDefinition.ComponentImprovements[0].ImprovedComponent != null)
                                {
                                    bitmap = bitmap_21[researchNodeDefinition.ComponentImprovements[0].ImprovedComponent.PictureRef];
                                }
                                else if (researchNodeDefinition.PlanetaryFacility != null)
                                {
                                    bitmap = bitmap_8[researchNodeDefinition.PlanetaryFacility.PictureRef];
                                }
                                break;
                            }
                            if (object_7 is Empire)
                            {
                                Empire empire3 = (Empire)object_7;
                                bitmap = empire3.LargeFlagPicture;
                                break;
                            }
                            if (!(object_7 is EventAction))
                            {
                                break;
                            }
                            EventAction eventAction = (EventAction)object_7;
                            switch (eventAction.Type)
                            {
                                case EventActionType.GeneralMessageToEmpire:
                                    if (!string.IsNullOrEmpty(eventAction.ImageFilename))
                                    {
                                        string string_34 = Application.StartupPath + "\\images\\ui\\story\\";
                                        string string_35 = string.Empty;
                                        if (!string.IsNullOrEmpty(string_3))
                                        {
                                            string_35 = Application.StartupPath + "\\customization\\" + string_3 + "\\images\\ui\\story\\";
                                        }
                                        bitmap = method_10(string_35, string_34, eventAction.ImageFilename, bool_28: false);
                                    }
                                    if (bitmap == null)
                                    {
                                        bitmap = bitmap_189;
                                    }
                                    flag2 = true;
                                    break;
                                case EventActionType.EmpireMessageToEmpire:
                                    if (!string.IsNullOrEmpty(eventAction.ImageFilename))
                                    {
                                        string string_32 = Application.StartupPath + "\\images\\ui\\story\\";
                                        string string_33 = string.Empty;
                                        if (!string.IsNullOrEmpty(string_3))
                                        {
                                            string_33 = Application.StartupPath + "\\customization\\" + string_3 + "\\images\\ui\\story\\";
                                        }
                                        bitmap = method_10(string_33, string_32, eventAction.ImageFilename, bool_28: false);
                                    }
                                    if (bitmap == null)
                                    {
                                        Bitmap bitmap2 = method_652(eventAction.Empire, 200);
                                        Bitmap bitmap3 = new Bitmap(bitmap_189);
                                        using (Graphics graphics = Graphics.FromImage(bitmap3))
                                        {
                                            graphics.DrawImage(bitmap_189, new Point(0, 0));
                                            int num2 = (bitmap_189.Width - bitmap2.Width) / 2;
                                            int num3 = (bitmap_189.Height - bitmap2.Height) / 2;
                                            graphics.DrawImage(bitmap2, new Point(num2, num3));
                                        }
                                        bitmap2.Dispose();
                                        bitmap = bitmap3;
                                    }
                                    flag2 = true;
                                    break;
                            }
                            num = 3;
                            break;
                        }
                    case EventMessageType.DisasterEvent:
                        if (object_7 is DisasterEventType disasterEventType)
                        {
                            switch (disasterEventType)
                            {
                                case DisasterEventType.Earthquake:
                                    bitmap = bitmap_30[0];
                                    break;
                                case DisasterEventType.Sinkhole:
                                    bitmap = bitmap_30[1];
                                    break;
                                case DisasterEventType.Tsunami:
                                    bitmap = bitmap_30[2];
                                    break;
                                case DisasterEventType.Sandstorm:
                                    bitmap = bitmap_30[3];
                                    break;
                                case DisasterEventType.Blizzard:
                                    bitmap = bitmap_30[4];
                                    break;
                                case DisasterEventType.Eruption:
                                    bitmap = bitmap_30[5];
                                    break;
                                case DisasterEventType.Plague:
                                    bitmap = bitmap_30[6];
                                    break;
                                case DisasterEventType.EconomicCrisis:
                                    bitmap = bitmap_30[7];
                                    break;
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.ResourceAppearance:
                        if (object_7 is Resource)
                        {
                            Resource resource5 = (Resource)object_7;
                            bitmap = _uiResourcesBitmaps[resource5.PictureRef];
                            if (object_8 is Habitat)
                            {
                                Habitat habitat10 = (Habitat)object_8;
                                if (habitat10 != null)
                                {
                                    Bitmap bitmap_4 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat10), 150, 150, 1f);
                                    bitmap = method_654(bitmap_4, _uiResourcesBitmaps[resource5.PictureRef], 150, 75);
                                }
                            }
                        }
                        else if (object_7 is ResourceDefinition)
                        {
                            ResourceDefinition resourceDefinition2 = (ResourceDefinition)object_7;
                            bitmap = _uiResourcesBitmaps[resourceDefinition2.PictureRef];
                            if (object_8 is Habitat)
                            {
                                Habitat habitat11 = (Habitat)object_8;
                                if (habitat11 != null)
                                {
                                    Bitmap bitmap_5 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat11), 150, 150, 1f);
                                    bitmap = method_654(bitmap_5, _uiResourcesBitmaps[resourceDefinition2.PictureRef], 150, 75);
                                }
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralGoodEvent;
                        num = 3;
                        break;
                    case EventMessageType.ResourceDepletion:
                        if (object_7 is Resource)
                        {
                            Resource resource3 = (Resource)object_7;
                            bitmap = _uiResourcesBitmaps[resource3.PictureRef];
                            if (object_8 is Habitat)
                            {
                                Habitat habitat4 = (Habitat)object_8;
                                if (habitat4 != null)
                                {
                                    Bitmap bitmap_2 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat4), 150, 150, 1f);
                                    bitmap = method_654(bitmap_2, _uiResourcesBitmaps[resource3.PictureRef], 150, 75);
                                }
                            }
                        }
                        else if (object_7 is ResourceDefinition)
                        {
                            ResourceDefinition resourceDefinition = (ResourceDefinition)object_7;
                            bitmap = _uiResourcesBitmaps[resourceDefinition.PictureRef];
                            if (object_8 is Habitat)
                            {
                                Habitat habitat5 = (Habitat)object_8;
                                if (habitat5 != null)
                                {
                                    Bitmap bitmap_3 = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(habitat5), 150, 150, 1f);
                                    bitmap = method_654(bitmap_3, _uiResourcesBitmaps[resourceDefinition.PictureRef], 150, 75);
                                }
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.RaceEvent:
                        if (object_7 is RaceEventType raceEventType)
                        {
                            switch (raceEventType)
                            {
                                case RaceEventType.NepthysWineVintage:
                                    {
                                        Bitmap bitmap_ = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(GalaxyImages.HabitatImageOffsetOcean + 1), 150, 150, 1f);
                                        ResourceDefinition byName = _Game.Galaxy.ResourceSystem.Resources.GetByName("Nepthys Wine");
                                        if (byName != null)
                                        {
                                            bitmap = method_654(bitmap_, _uiResourcesBitmaps[byName.PictureRef], 150, 75);
                                        }
                                        break;
                                    }
                                case RaceEventType.UnderwaterLeviathan:
                                    bitmap = GraphicsHelper.ScaleLimitImage(bitmap_29[GalaxyImages.LandscapeImageOffsetOcean + 1], 200, 200, 1f);
                                    break;
                                case RaceEventType.StrengthInNumbersMaintenanceLowerForSmallShips:
                                    {
                                        int pictureRef = ShipImageHelper.ResolveStandardShipImageByFamilyAndSubRole(_Game.PlayerEmpire.DominantRace.DesignPictureFamilyIndex, BuiltObjectSubRole.Frigate);
                                        Bitmap bitmap4 = new Bitmap(builtObjectImageCache_0.ObtainImage(pictureRef));
                                        bitmap4.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        bitmap = GraphicsHelper.ScaleLimitImage(bitmap4, 200, 200, 1f);
                                        break;
                                    }
                                case RaceEventType.NaturalHarmonyColonyQualityIncreased:
                                    bitmap = GraphicsHelper.ScaleLimitImage(habitatImageCache_0.ObtainImage(GalaxyImages.HabitatImageOffsetContinental + 1), 150, 150, 1f);
                                    break;
                                case RaceEventType.ForcedRetirementLeaderReplaced:
                                    {
                                        if (_Game.PlayerEmpire.RaceEventData != null && _Game.PlayerEmpire.RaceEventData is Character)
                                        {
                                            Character character4 = (Character)_Game.PlayerEmpire.RaceEventData;
                                            bitmap = characterImageCache_0.ObtainCharacterImage(character4);
                                            _Game.PlayerEmpire.RaceEventData = null;
                                            break;
                                        }
                                        Character leader = _Game.PlayerEmpire.Leader;
                                        if (leader != null)
                                        {
                                            bitmap = characterImageCache_0.ObtainCharacterImage(leader);
                                        }
                                        break;
                                    }
                                case RaceEventType.DestinyCharacterTraits:
                                case RaceEventType.SecurityConcernsCharacterReplaced:
                                case RaceEventType.SupremeWarriorNewGeneral:
                                    if (_Game.PlayerEmpire.RaceEventData != null && _Game.PlayerEmpire.RaceEventData is Character)
                                    {
                                        Character character3 = (Character)_Game.PlayerEmpire.RaceEventData;
                                        bitmap = characterImageCache_0.ObtainCharacterImage(character3);
                                        _Game.PlayerEmpire.RaceEventData = null;
                                    }
                                    else
                                    {
                                        bitmap = raceImageCache_0.GetRaceImage(_Game.PlayerEmpire.DominantRace.PictureRef);
                                    }
                                    break;
                                case RaceEventType.GreatHuntStrongTroops:
                                case RaceEventType.WarriorWaveTroopRecruitment:
                                case RaceEventType.SwarmsFullTroopTransport:
                                case RaceEventType.CannibalismPopulationShrinks:
                                case RaceEventType.MetamorphosisCharacterChange:
                                case RaceEventType.AntiXenoRiotsExterminate:
                                case RaceEventType.XenophobiaNoAssimilate:
                                case RaceEventType.NeverSurrenderWarWearinessReset:
                                case RaceEventType.TodashGalacticChampionships:
                                case RaceEventType.IsolationistsResetFirstContactPenalty:
                                case RaceEventType.GrandPerformanceDiplomacyBonus:
                                case RaceEventType.FriendsInManyPlacesRevealTerritory:
                                case RaceEventType.LuckyAvertColonyDisaster:
                                case RaceEventType.DeathCultExterminate:
                                    bitmap = raceImageCache_0.GetRaceImage(_Game.PlayerEmpire.DominantRace.PictureRef);
                                    break;
                                case RaceEventType.HistoricalKnowledgeUncoverHiddenLocation:
                                case RaceEventType.PredictiveHistory:
                                    bitmap = bitmap_28[25];
                                    break;
                                case RaceEventType.SuppressedKnowledgeLoseResearch:
                                case RaceEventType.ShakturiArtifactWeaponResearch:
                                case RaceEventType.ScientificBreakthroughResearchProgress:
                                case RaceEventType.CreativeReengineeringFreeCrashResearch:
                                case RaceEventType.HistoricalDiscoveryExploreRuinsForResearchBoost:
                                    bitmap = bitmap_83;
                                    break;
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralNeutralEvent;
                        num = 3;
                        break;
                    case EventMessageType.WonderBuilt:
                        if (object_7 is PlanetaryFacility)
                        {
                            PlanetaryFacility planetaryFacility2 = (PlanetaryFacility)object_7;
                            if (planetaryFacility2 != null)
                            {
                                bitmap = bitmap_8[planetaryFacility2.PictureRef];
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralGoodEvent;
                        num = 3;
                        break;
                    case EventMessageType.PhantomPirates:
                        if (object_7 is Empire)
                        {
                            Empire empire2 = (Empire)object_7;
                            if (empire2 != null)
                            {
                                bitmap = ((empire2.DominantRace == null) ? empire2.LargeFlagPicture : method_652(empire2, 200));
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralBadEvent;
                        num = 3;
                        break;
                    case EventMessageType.CharacterEvent:
                    case EventMessageType.LeaderChange:
                        if (object_7 is Character)
                        {
                            Character character = (Character)object_7;
                            if (character != null)
                            {
                                bitmap = characterImageCache_0.ObtainCharacterImage(character);
                            }
                        }
                        empireMessageType = EmpireMessageType.GeneralNeutralEvent;
                        num = 3;
                        break;
                }
                object_2 = object_8;
                if (_Game.DisplayMessageExploration && empireMessageType != EmpireMessageType.Informational)
                {
                    EmpireMessage empireMessage3 = new EmpireMessage(_Game.PlayerEmpire, empireMessageType, object_8);
                    empireMessage3.Description = string_31;
                    empireMessage3.Title = string_30;
                    empireMessage3.SupressPopup = true;
                    _Game.PlayerEmpire.SendMessageToEmpire(empireMessage3, _Game.PlayerEmpire);
                }
                bool flag3 = true;
                flag3 = _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase switch
                {
                    1 => true,
                    2 => false,
                    _ => true,
                };
                bool flag4 = true;
                bool flag5 = true;
                bool flag6 = true;
                switch (_Game.PlayerEmpire.DiscoveryActionRuin)
                {
                    default:
                        flag4 = true;
                        flag5 = true;
                        flag6 = true;
                        break;
                    case 1:
                        flag4 = true;
                        flag5 = true;
                        flag6 = true;
                        break;
                    case 2:
                        flag4 = false;
                        flag5 = true;
                        flag6 = true;
                        break;
                    case 3:
                        flag4 = false;
                        flag5 = false;
                        flag6 = true;
                        break;
                    case 4:
                        flag4 = false;
                        flag5 = false;
                        flag6 = false;
                        break;
                }
                bool flag7 = true;
                if (object_7 is BuiltObject)
                {
                    if (!flag3)
                    {
                        flag7 = false;
                    }
                }
                else
                {
                    switch (num)
                    {
                        default:
                            if (!flag6)
                            {
                                flag7 = false;
                            }
                            break;
                        case 0:
                            if (!flag4)
                            {
                                flag7 = false;
                            }
                            break;
                        case 1:
                            if (!flag5)
                            {
                                flag7 = false;
                            }
                            break;
                        case 2:
                            if (!flag6)
                            {
                                flag7 = false;
                            }
                            break;
                    }
                }
                if (!flag)
                {
                    flag7 = false;
                }
                if (flag7 && !musicPlayer_1.IsPlaying)
                {
                    switch (eventMessageType_1)
                    {
                        case EventMessageType.CreatureOutbreak:
                            if (object_7 != null && object_7 is Creature)
                            {
                                Creature creature3 = (Creature)object_7;
                                if (creature3.Type == CreatureType.SilverMist)
                                {
                                    method_520();
                                }
                            }
                            break;
                        case EventMessageType.FreeSuperShip:
                            if (eventMessageType_1 == EventMessageType.FreeSuperShip && (object_7 is BuiltObject || object_7 is Character))
                            {
                                ArhCaEfBkk();
                            }
                            break;
                        case EventMessageType.LostColonyFound:
                        case EventMessageType.IndependentPopulation:
                            method_516();
                            break;
                        case EventMessageType.OriginsDiscovery:
                        case EventMessageType.AncientBattleDebrisField:
                        case EventMessageType.StoryClue:
                        case EventMessageType.SpecialArea:
                        case EventMessageType.UncoverPirateAttackFundingAnotherEmpire:
                        case EventMessageType.UncoverPirateAttackFundingYourEmpire:
                        case EventMessageType.UncoverPlanetDestroyerConstruction:
                        case EventMessageType.RareResourceIntercepted:
                            ArhCaEfBkk();
                            break;
                        case EventMessageType.RogueFleetDefectsFromUs:
                        case EventMessageType.EmpireSplits:
                        case EventMessageType.DisasterEvent:
                            method_519();
                            break;
                        case EventMessageType.RaceEvent:
                            method_517();
                            break;
                        case EventMessageType.WonderBuilt:
                            method_515();
                            break;
                        case EventMessageType.PirateFactionJoinsYou:
                        case EventMessageType.PhantomPirates:
                            method_520();
                            break;
                        case EventMessageType.CharacterEvent:
                        case EventMessageType.LeaderChange:
                            method_518();
                            break;
                    }
                }
                if (flag7)
                {
                    if (flag2)
                    {
                        method_570(string_30, string_31, bitmap);
                    }
                    else
                    {
                        method_508(bitmap, string_30, string_31, bool_, bool_2);
                    }
                }
            }
            else
            {
                object_3 = object_7;
                eventMessageType_0 = eventMessageType_1;
                string string_36 = "";
                string string_37 = "";
                switch (eventMessageType_1)
                {
                    case EventMessageType.UncoverPirateAttackFundingAnotherEmpire:
                        bitmap = bitmap_49;
                        string_36 = TextResolver.GetText("Warn target empire about this secret deal");
                        string_37 = TextResolver.GetText("Keep this information to ourselves");
                        break;
                    case EventMessageType.UncoverPlanetDestroyerConstruction:
                        {
                            BuiltObject builtObject6 = (BuiltObject)object_7;
                            bitmap = PrepareBuiltObjectImage(builtObject6, builtObjectImageCache_0.ObtainImage(builtObject6), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            bitmap = method_116(builtObject6, bitmap, 0.4f);
                            bitmap = method_6(bitmap, (double)builtObject6.Heading * -1.0);
                            string_36 = TextResolver.GetText("Expose this secret project to all empires");
                            string_37 = TextResolver.GetText("Keep this information to ourselves");
                            break;
                        }
                    case EventMessageType.RogueFleetDefectsToUs:
                        {
                            ShipGroup shipGroup = (ShipGroup)object_7;
                            bitmap = PrepareBuiltObjectImage(shipGroup.LeadShip, builtObjectImageCache_0.ObtainImage(shipGroup.LeadShip), Color.Gray, Color.Gray, 1.0, 1, allowPreRotate: false);
                            bitmap = method_6(bitmap, (double)shipGroup.LeadShip.Heading * -1.0);
                            string_36 = TextResolver.GetText("Accept rogue fleet into our empire");
                            string_37 = TextResolver.GetText("Reject offer from rogue fleet");
                            break;
                        }
                }
                if (_Game.DisplayMessageExploration)
                {
                    EmpireMessage empireMessage4 = new EmpireMessage(_Game.PlayerEmpire, empireMessageType, object_8);
                    empireMessage4.Description = string_31;
                    empireMessage4.Title = string_30;
                    empireMessage4.SupressPopup = true;
                    _Game.PlayerEmpire.SendMessageToEmpire(empireMessage4, _Game.PlayerEmpire);
                }
                if (flag)
                {
                    ArhCaEfBkk();
                    method_509(bitmap, string_30, string_31, string_36, string_37);
                }
            }
        }

        private void btnEventMessageClose_Click(object sender, EventArgs e)
        {
            method_522();
            method_514();
            method_155();
        }

        private void btnEventMessageGoto_Click(object sender, EventArgs e)
        {
            if (this.object_2 is BuiltObject)
            {
                BuiltObject object_ = (BuiltObject)this.object_2;
                method_157(object_);
            }
            else if (this.object_2 is Habitat)
            {
                Habitat object_2 = (Habitat)this.object_2;
                method_157(object_2);
            }
            else if (this.object_2 is Creature)
            {
                Creature object_3 = (Creature)this.object_2;
                method_157(object_3);
            }
            else if (this.object_2 is Character)
            {
                Character object_4 = (Character)this.object_2;
                method_157(object_4);
            }
            else if (this.object_2 is Point)
            {
                Point point = (Point)this.object_2;
                method_156(point.X, point.Y);
            }
            method_4(1.0);
            method_522();
            method_514();
            method_155();
        }

        private void btnBuiltObjectSelect_Click(object sender, EventArgs e)
        {
            method_208(ctlBuiltObjectList.SelectedBuiltObject);
        }

        private void btnShipGroupSelect_Click(object sender, EventArgs e)
        {
            method_208(ctlShipGroupListView.SelectedShipGroup);
        }

        private void btnColonySelect_Click(object sender, EventArgs e)
        {
            method_208(UnlxwvByxj.SelectedHabitat);
        }

        private void pnlMessagePopup_MouseEnter(object sender, EventArgs e)
        {
            EmpireMessage message = pnlMessagePopup.Message;
            if (message != null)
            {
                EmpireMessage empireMessage_ = message;
                method_242(empireMessage_);
            }
        }

        private void pnlMessagePopup_MouseLeave(object sender, EventArgs e)
        {
            EmpireMessage message = pnlMessagePopup.Message;
            if (message != null)
            {
                EmpireMessage empireMessage_ = message;
                method_244(empireMessage_);
            }
        }

        private void method_524(string string_30)
        {
            if (!Directory.Exists(string_30))
            {
                Directory.CreateDirectory(string_30);
            }
        }

        private void btnDesignsSave_Click(object sender, EventArgs e)
        {
            if (_Game == null || _Game.PlayerEmpire == null || _Game.PlayerEmpire.Designs == null)
            {
                return;
            }
            DesignList selectedDesigns = ctlDesignsList.SelectedDesigns;
            if (selectedDesigns != null && selectedDesigns.Count > 0)
            {
                bool flag = false;
                string text = GetGameFilesFolderCreateIfNeeded() + "Settings\\";
                if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.DesignsPath))
                {
                    text = gameOptions_0.DesignsPath;
                }
                Stream stream = null;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = text;
                method_524(text);
                saveFileDialog.Filter = TextResolver.GetText("Distant Worlds ship designs files") + " (*.dwd)|*.dwd";
                saveFileDialog.DefaultExt = "dwd";
                saveFileDialog.Title = TextResolver.GetText("Save Distant Worlds designs");
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = saveFileDialog.OpenFile()) != null)
                    {
                        if (gameOptions_0 != null && !string.IsNullOrEmpty(saveFileDialog.FileName))
                        {
                            DirectoryInfo directoryInfo = Directory.GetParent(saveFileDialog.FileName);
                            if (directoryInfo != null)
                            {
                                gameOptions_0.DesignsPath = directoryInfo.FullName;
                            }
                        }
                        saveFileDialog.Dispose();
                        if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                        {
                            flag = true;
                            method_154();
                        }
                        method_92();
                        Cursor.Current = Cursors.WaitCursor;
                        method_525(selectedDesigns, stream);
                        stream.Close();
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
            else
            {
                string string_ = TextResolver.GetText("No designs have been selected");
                method_371(string_, TextResolver.GetText("No designs to save"), MessageBoxExIcon.Information);
            }
        }

        private void method_525(DesignList designList_0, Stream stream_0)
        {
            if (designList_0 == null || stream_0 == null || !stream_0.CanWrite)
            {
                return;
            }
            DesignList designList = new DesignList();
            foreach (Design item in designList_0)
            {
                Design design = item.Clone();
                design.Empire = null;
                designList.Add(design);
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream_0, designList);
        }

        private void mgohAuJwBE(object sender, EventArgs e)
        {
            bool flag = false;
            string text = GetGameFilesFolderCreateIfNeeded() + "Settings\\";
            if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.DesignsPath))
            {
                text = gameOptions_0.DesignsPath;
            }
            Stream stream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = text;
            method_524(text);
            openFileDialog.Filter = TextResolver.GetText("Distant Worlds ship designs files") + " (*.dwd)|*.dwd";
            openFileDialog.DefaultExt = "dwd";
            openFileDialog.Title = TextResolver.GetText("Load Distant Worlds designs");
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = openFileDialog.OpenFile()) != null)
                {
                    if (gameOptions_0 != null && !string.IsNullOrEmpty(openFileDialog.FileName))
                    {
                        DirectoryInfo directoryInfo = Directory.GetParent(openFileDialog.FileName);
                        if (directoryInfo != null)
                        {
                            gameOptions_0.DesignsPath = directoryInfo.FullName;
                        }
                    }
                    openFileDialog.Dispose();
                    if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                    {
                        flag = true;
                        method_154();
                    }
                    method_92();
                    Cursor.Current = Cursors.WaitCursor;
                    Galaxy.LoadDesigns(stream, _Game.PlayerEmpire, markLoadedDesignsAsOptimized: false, _Game.Galaxy.CurrentStarDate);
                    stream.Close();
                    ctlDesignsList.BindData(_Game.Galaxy, _Game.PlayerEmpire.Designs, builtObjectImageCache_0.GetImagesSmall(), allowMultiSelect: true);
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
            _Game.PlayerEmpire.ReviewLatestDesigns();
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused && flag)
            {
                method_155();
            }
            method_90();
        }

        private IntPtr method_526(string string_30)
        {
            if (privateFontCollection_0 == null)
            {
                privateFontCollection_0 = new PrivateFontCollection();
            }
            Stream manifestResourceStream = GetType().Assembly.GetManifestResourceStream(string_30);
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

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Gdi32.RemoveFontMemResourceEx(intptr_0);
            Gdi32.RemoveFontMemResourceEx(intptr_1);
            SteamAPI.Shutdown();
        }

        private void btnEventMessageInvestigate_Click(object sender, EventArgs e)
        {
            method_514();
            if (eventMessageType_0 != 0)
            {
                switch (eventMessageType_0)
                {
                    case EventMessageType.UncoverPirateAttackFundingAnotherEmpire:
                        {
                            if (!(object_3 is EmpireActivity))
                            {
                                break;
                            }
                            EmpireActivity empireActivity = (EmpireActivity)object_3;
                            if (empireActivity.TargetEmpire.PirateEmpireBaseHabitat == null && empireActivity.RequestingEmpire.PirateEmpireBaseHabitat == null)
                            {
                                EmpireEvaluation empireEvaluation = empireActivity.TargetEmpire.ObtainEmpireEvaluation(empireActivity.RequestingEmpire);
                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw - 15.0;
                                empireActivity.RequestingEmpire.CivilityRating -= 3.0;
                                empireEvaluation = empireActivity.TargetEmpire.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                                empireEvaluation.IncidentEvaluation = empireEvaluation.IncidentEvaluationRaw + 12.0;
                                break;
                            }
                            empireActivity.TargetEmpire.ChangePirateEvaluation(empireActivity.RequestingEmpire, -15f, PirateRelationEvaluationType.DetectedIntelligenceMissions);
                            empireActivity.RequestingEmpire.CivilityRating -= 3.0;
                            if (empireActivity.TargetEmpire.PirateEmpireBaseHabitat == null && _Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                            {
                                EmpireEvaluation empireEvaluation2 = empireActivity.TargetEmpire.ObtainEmpireEvaluation(_Game.PlayerEmpire);
                                empireEvaluation2.IncidentEvaluation = empireEvaluation2.IncidentEvaluationRaw + 12.0;
                            }
                            else
                            {
                                empireActivity.TargetEmpire.ChangePirateEvaluation(_Game.PlayerEmpire, 12f, PirateRelationEvaluationType.Gifts);
                            }
                            break;
                        }
                    case EventMessageType.UncoverPlanetDestroyerConstruction:
                        if (object_3 is object[])
                        {
                            object[] array = (object[])object_3;
                            Empire planetDestroyerBuilder = (Empire)array[0];
                            GalaxyLocation planetDestroyerLocation = (GalaxyLocation)array[1];
                            _Game.PlayerEmpire.ExposePlanetDestroyerConstruction(planetDestroyerBuilder, planetDestroyerLocation, _Game.PlayerEmpire);
                        }
                        break;
                    case EventMessageType.RogueFleetDefectsToUs:
                        if (object_3 is ShipGroup)
                        {
                            ShipGroup fleet = (ShipGroup)object_3;
                            _Game.PlayerEmpire.DefectFleet(fleet, _Game.PlayerEmpire);
                        }
                        break;
                }
            }
            else if (habitat_5 != null)
            {
                _Game.Galaxy.InvestigateRuins(_Game.PlayerEmpire, habitat_5);
                habitat_5 = null;
            }
            else if (hOcmlqpCrp != null)
            {
                hOcmlqpCrp.PlayerEmpireEncounterAction = BuiltObjectEncounterAction.Prompt;
                _Game.Galaxy.InvestigateAbandonedBuiltObject(_Game.PlayerEmpire, hOcmlqpCrp);
                hOcmlqpCrp = null;
            }
        }

        private void btnEventMessageAvoid_Click(object sender, EventArgs e)
        {
            method_522();
            method_514();
            method_155();
        }

        private void method_527()
        {
            method_528(string.Empty);
        }

        private void method_528(string string_30)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlMessageHistory.Size = new Size(965, 580);
            pnlMessageHistory.Location = new Point((mainView.Width - pnlMessageHistory.Width) / 2, (mainView.Height - pnlMessageHistory.Height) / 2);
            pnlMessageHistory.DoLayout();
            method_531(null);
            cmbMessageHistoryFilter.Location = new Point(10, 9);
            cmbMessageHistoryFilter.Size = new Size(310, 21);
            cmbMessageHistoryFilter.BringToFront();
            cmbMessageHistoryFilter.Visible = true;
            if (cmbMessageHistoryFilter.Items == null || cmbMessageHistoryFilter.Items.Count == 0)
            {
                cmbMessageHistoryFilter.SelectedIndexChanged -= cmbMessageHistoryFilter_SelectedIndexChanged;
                cmbMessageHistoryFilter.Items.AddRange(new object[3]
                {
                TextResolver.GetText("Show All Messages"),
                TextResolver.GetText("Show Non-Battle Messages"),
                TextResolver.GetText("Show Galactic History Messages")
                });
                cmbMessageHistoryFilter.SelectedIndex = 0;
                cmbMessageHistoryFilter.SelectedIndexChanged += cmbMessageHistoryFilter_SelectedIndexChanged;
            }
            if (!string.IsNullOrEmpty(string_30))
            {
                cmbMessageHistoryFilter.SelectedIndexChanged -= cmbMessageHistoryFilter_SelectedIndexChanged;
                switch (string_30.ToLower(CultureInfo.InvariantCulture))
                {
                    case "galactichistory":
                        cmbMessageHistoryFilter.SelectedIndex = 2;
                        break;
                    case "nonbattle":
                        cmbMessageHistoryFilter.SelectedIndex = 1;
                        break;
                    case "all":
                        cmbMessageHistoryFilter.SelectedIndex = 0;
                        break;
                    case "either":
                        if (cmbMessageHistoryFilter.SelectedIndex == 2)
                        {
                            cmbMessageHistoryFilter.SelectedIndex = 0;
                        }
                        break;
                }
                cmbMessageHistoryFilter.SelectedIndexChanged += cmbMessageHistoryFilter_SelectedIndexChanged;
            }
            ctlMessageHistoryMessages.Height = 475;
            ctlMessageHistoryMessages.Width = 310;
            ctlMessageHistoryMessages.Location = new Point(10, 35);
            method_542();
            ctlMessageHistoryMessages.BringToFront();
            ctlMessageHistoryMessages.Grid.Columns["Icon"].Width = 35;
            ctlMessageHistoryMessages.Grid.Columns["Title"].Width = 195;
            ctlMessageHistoryMessages.Grid.Columns["StarDate"].Width = 80;
            lblMessageHistoryHeading.Font = font_2;
            lblMessageHistoryHeading.ForeColor = color_1;
            lblMessageHistoryHeading.BackColor = Color.Transparent;
            lblMessageHistoryHeading.Location = new Point(330, 10);
            txtMessageHistoryText.Font = font_6;
            txtMessageHistoryText.BackColor = Color.FromArgb(48, 48, 64);
            txtMessageHistoryText.ForeColor = Color.FromArgb(170, 170, 170);
            txtMessageHistoryText.Size = new Size(350, 475);
            txtMessageHistoryText.Location = new Point(330, 35);
            txtMessageHistoryText.BringToFront();
            gmapMessageHistory.BringToFront();
            gmapMessageHistory.Size = new Size(250, 250);
            gmapMessageHistory.Location = new Point(690, 35);
            method_531(ctlMessageHistoryMessages.SelectedMessage);
            btnMessageHistoryGoto.Location = new Point(690, 295);
            btnMessageHistoryGoto.Size = new Size(250, 25);
            pnlMessageHistory.Visible = true;
            pnlMessageHistory.BringToFront();
            ctlMessageHistoryMessages.Focus();
        }

        private void method_529()
        {
            pnlMessageHistory.SendToBack();
            pnlMessageHistory.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private Point method_530(EmpireMessage empireMessage_1)
        {
            Point result = empireMessage_1.Location;
            _ = empireMessage_1.Location;
            if (empireMessage_1.Location.IsEmpty && empireMessage_1.Subject != null)
            {
                if (empireMessage_1.Subject is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)empireMessage_1.Subject;
                    result = new Point((int)builtObject.Xpos, (int)builtObject.Ypos);
                }
                else if (empireMessage_1.Subject is Habitat)
                {
                    Habitat habitat = (Habitat)empireMessage_1.Subject;
                    result = new Point((int)habitat.Xpos, (int)habitat.Ypos);
                }
            }
            return result;
        }

        private void btnMessageHistoryGoto_Click(object sender, EventArgs e)
        {
            EmpireMessage selectedMessage = ctlMessageHistoryMessages.SelectedMessage;
            if (selectedMessage != null)
            {
                Point point = method_530(selectedMessage);
                if (!point.IsEmpty)
                {
                    method_156(point.X, point.Y);
                    method_4(1.0);
                    method_529();
                }
            }
        }

        private void method_531(EmpireMessage empireMessage_1)
        {
            if (empireMessage_1 != null)
            {
                Point point = method_530(empireMessage_1);
                if (point.IsEmpty)
                {
                    btnMessageHistoryGoto.Enabled = false;
                }
                else
                {
                    btnMessageHistoryGoto.Enabled = true;
                }
                lblMessageHistoryHeading.Text = empireMessage_1.Title;
                txtMessageHistoryText.Text = empireMessage_1.Description;
                gmapMessageHistory.SetPosition(point.X, point.Y);
            }
            else
            {
                lblMessageHistoryHeading.Text = string.Empty;
                txtMessageHistoryText.Text = string.Empty;
                gmapMessageHistory.SetSystems(null);
                gmapMessageHistory.ClearLocations();
                btnMessageHistoryGoto.Enabled = false;
            }
            gmapMessageHistory.Invalidate();
        }

        private void ctlMessageHistoryMessages_SelectionChanged(object sender, EventArgs e)
        {
            EmpireMessage selectedMessage = ctlMessageHistoryMessages.SelectedMessage;
            method_531(selectedMessage);
        }

        private void btnHistoryMessages_Click(object sender, EventArgs e)
        {
            if (pnlMessageHistory.Visible)
            {
                method_529();
            }
            else
            {
                method_528("either");
            }
        }

        private void lnkEventMessageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_226();
            if (race_0 != null)
            {
                method_456(race_0.Name);
            }
        }

        private void HxbhkchesK(object sender, EventArgs e)
        {
            method_194();
        }

        private void pnlColonyInfo_CloseButtonClicked(object sender, EventArgs e)
        {
            method_186();
        }

        private void CaLkaMyrMQ_CloseButtonClicked(object sender, EventArgs e)
        {
            method_132();
        }

        private void pnlBuiltObjectInfo_CloseButtonClicked(object sender, EventArgs e)
        {
            method_185();
        }

        private void pnlShipGroupInfo_CloseButtonClicked(object sender, EventArgs e)
        {
            method_271();
        }

        private void pnlTroopInfo_CloseButtonClicked(object sender, EventArgs e)
        {
            method_184();
        }

        private void pnlResearch_CloseButtonClicked(object sender, EventArgs e)
        {
            method_399();
        }

        private void pnlDesigns_CloseButtonClicked(object sender, EventArgs e)
        {
            method_308();
        }

        private void vHfFsoqMev_CloseButtonClicked(object sender, EventArgs e)
        {
            method_401();
        }

        private void pnlMessageHistory_CloseButtonClicked(object sender, EventArgs e)
        {
            method_529();
        }

        private void pnlEmpireSummary_CloseButtonClicked(object sender, EventArgs e)
        {
            method_275();
        }

        private void pnlEncyclopedia_CloseButtonClicked(object sender, EventArgs e)
        {
            method_457();
        }

        private void pnlGameOptions_CloseButtonClicked(object sender, EventArgs e)
        {
            method_413();
        }

        private void lnkEmpireSummaryGovernmentType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string name = TextResolver.GetText("Government Types");
            int num = method_422();
            GovernmentAttributes governmentAttributes = null;
            if (num >= 0 && num < _Game.Galaxy.Governments.Count)
            {
                governmentAttributes = _Game.Galaxy.Governments[num];
                if (governmentAttributes != null)
                {
                    name = governmentAttributes.Name;
                }
            }
            method_456(name);
        }

        private void NuSppwjfQh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Diplomatic Relation Types"));
        }

        private void lnkColonyGrowth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Colony Growth"));
        }

        private void lnkColonyApproval_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Colony Approval"));
        }

        private void lnkBuiltObjectConstruction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Construction"));
        }

        private void lnkTroops_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Troops"));
        }

        private void lnkResearch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Research"));
        }

        private void pnlCharacterMission_MissionTypeHelp(object sender, EventArgs e)
        {
            method_456(TextResolver.GetText("Intelligence Missions"));
        }

        private void btnZoomSelection_Click(object sender, EventArgs e)
        {
            if (_Game.SelectedObject != null)
            {
                if (!UhvLmNjli7)
                {
                    method_157(_Game.SelectedObject);
                }
                if (double_0 != 1.0)
                {
                    method_4(1.0);
                }
            }
        }

        private void btnBuiltObjectConstructionScrap_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            if (selectedStellarObject == null || selectedStellarObject.ConstructionQueue == null)
            {
                return;
            }
            BuiltObject shipUnderConstruction = ctlConstructionYards.SelectedConstructionYard.ShipUnderConstruction;
            if (shipUnderConstruction == null)
            {
                return;
            }
            MessageBoxEx messageBoxEx = null;
            string empty = string.Empty;
            if (shipUnderConstruction.Owner == null)
            {
                empty = ((shipUnderConstruction.Empire != null) ? TextResolver.GetText("This ship is privately owned - it cannot be scrapped") : TextResolver.GetText("This ship is not owned by your empire - it cannot be scrapped"));
                messageBoxEx = method_371(empty, TextResolver.GetText("Cannot scrap ship"), MessageBoxExIcon.Information);
                return;
            }
            empty = TextResolver.GetText("The purchase cost will not be refunded if you scrap this ship");
            empty = empty + " (" + shipUnderConstruction.Name + ")";
            messageBoxEx = method_372(empty, TextResolver.GetText("Scrap Ship under Construction?"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                selectedStellarObject.ConstructionQueue.RemoveBuiltObject(shipUnderConstruction);
                ctlConstructionYards.BindData(_Game.Galaxy, selectedStellarObject.ConstructionQueue.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                shipUnderConstruction.CompleteTeardown(_Game.Galaxy);
            }
        }

        private void btnBuiltObjectConstructionRemoveFromQueue_Click(object sender, EventArgs e)
        {
            StellarObject selectedStellarObject = ctlBuiltObjectList.SelectedStellarObject;
            if (selectedStellarObject == null || selectedStellarObject.ConstructionQueue == null)
            {
                return;
            }
            BuiltObject selectedBuiltObject = ctlConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedBuiltObject == null)
            {
                return;
            }
            MessageBoxEx messageBoxEx = null;
            string empty = string.Empty;
            if (selectedBuiltObject.Owner == null)
            {
                empty = ((selectedBuiltObject.Empire != null) ? TextResolver.GetText("This ship is privately owned - it cannot be removed") : TextResolver.GetText("This ship is not owned by your empire - it cannot be removed"));
                messageBoxEx = method_371(empty, TextResolver.GetText("Cannot remove ship from queue"), MessageBoxExIcon.Information);
                return;
            }
            double num = selectedBuiltObject.PurchasePrice * 0.5;
            empty = string.Format(TextResolver.GetText("Removing this ship from the construction queue will refund half the purchase cost"), num.ToString("0"));
            empty = empty + " (" + selectedBuiltObject.Name + ")";
            messageBoxEx = method_372(empty, TextResolver.GetText("Remove Ship from Construction Queue?"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                if (selectedBuiltObject.Empire != null)
                {
                    selectedBuiltObject.Empire.StateMoney += num;
                    selectedBuiltObject.Empire.PirateEconomy.PerformIncome(num, PirateIncomeType.Undefined, _Game.Galaxy.CurrentStarDate);
                }
                selectedStellarObject.ConstructionQueue.RemoveBuiltObject(selectedBuiltObject);
                ctlConstructionYardWaitQueue.BindData(selectedStellarObject.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                if (selectedBuiltObject.RetrofitDesign != null)
                {
                    selectedBuiltObject.RetrofitDesign = null;
                    selectedBuiltObject.RetrofitForNextMission = false;
                }
                else
                {
                    selectedBuiltObject.CompleteTeardown(_Game.Galaxy);
                }
            }
        }

        private void btnColonyConstructionScrap_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null || selectedHabitat.ConstructionQueue == null)
            {
                return;
            }
            BuiltObject shipUnderConstruction = ctlColonyConstructionYard.SelectedConstructionYard.ShipUnderConstruction;
            if (shipUnderConstruction == null)
            {
                return;
            }
            MessageBoxEx messageBoxEx = null;
            string empty = string.Empty;
            if (shipUnderConstruction.Owner == null)
            {
                empty = ((shipUnderConstruction.Empire != null) ? "This ship is privately owned - it cannot be scrapped" : "This ship is not owned by your empire - it cannot be scrapped");
                messageBoxEx = method_371(empty, "Cannot scrap ship", MessageBoxExIcon.Information);
                return;
            }
            empty = "The purchase cost will not be refunded if you scrap this ship.";
            empty = empty + "\n\nAre you sure you want to scrap this ship? (" + shipUnderConstruction.Name + ")";
            messageBoxEx = method_372(empty, "Scrap Ship under Construction?");
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                selectedHabitat.ConstructionQueue.RemoveBuiltObject(shipUnderConstruction);
                ctlColonyConstructionYard.BindData(_Game.Galaxy, selectedHabitat.ConstructionQueue.ConstructionYards, bitmap_21, builtObjectImageCache_0.GetImagesSmall());
                shipUnderConstruction.CompleteTeardown(_Game.Galaxy);
            }
        }

        private void btnColonyConstructionRemoveFromQueue_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat == null || selectedHabitat.ConstructionQueue == null)
            {
                return;
            }
            BuiltObject selectedBuiltObject = ctlColonyConstructionYardWaitQueue.SelectedBuiltObject;
            if (selectedBuiltObject == null)
            {
                return;
            }
            MessageBoxEx messageBoxEx = null;
            string empty = string.Empty;
            if (selectedBuiltObject.Owner == null)
            {
                empty = ((selectedBuiltObject.Empire != null) ? TextResolver.GetText("This ship is privately owned - it cannot be removed") : TextResolver.GetText("This ship is not owned by your empire - it cannot be removed"));
                messageBoxEx = method_371(empty, TextResolver.GetText("Cannot remove ship from queue"), MessageBoxExIcon.Information);
                return;
            }
            double num = selectedBuiltObject.PurchasePrice * 0.5;
            empty = string.Format(TextResolver.GetText("Removing this ship from the construction queue will refund half the purchase cost"), num.ToString("0"));
            empty = empty + " (" + selectedBuiltObject.Name + ")";
            messageBoxEx = method_372(empty, TextResolver.GetText("Remove Ship from Construction Queue?"));
            if (messageBoxEx.Show(this).ToLower(CultureInfo.InvariantCulture) == "yes")
            {
                if (selectedBuiltObject.Empire != null)
                {
                    selectedBuiltObject.Empire.StateMoney += num;
                    selectedBuiltObject.Empire.PirateEconomy.PerformIncome(num, PirateIncomeType.Undefined, _Game.Galaxy.CurrentStarDate);
                }
                selectedHabitat.ConstructionQueue.RemoveBuiltObject(selectedBuiltObject);
                ctlColonyConstructionYardWaitQueue.BindData(selectedHabitat.ConstructionQueue.ConstructionWaitQueue, _Game.Galaxy);
                if (selectedBuiltObject.RetrofitDesign != null)
                {
                    selectedBuiltObject.RetrofitDesign = null;
                    selectedBuiltObject.RetrofitForNextMission = false;
                }
                else
                {
                    selectedBuiltObject.CompleteTeardown(_Game.Galaxy);
                }
            }
        }

        private void lnkColonyConstruction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Construction"));
        }

        private void lnkFleets_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Fleets"));
        }

        private void kouhXgMiyR(object sender, EventArgs e)
        {
            BuiltObject builtObject = _Game.Galaxy.FastFindNearestAvailableMilitaryShip(int_13, int_14, _Game.PlayerEmpire);
            if (builtObject != null)
            {
                method_208(builtObject);
            }
        }

        private void pnlExpansionPlanner_CloseButtonClicked(object sender, EventArgs e)
        {
            method_162();
        }

        private void LrufvZylIl_CheckedChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            method_532(filter);
        }

        private void chkExpansionPlannerToggleAsteroids_CheckedChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            method_532(filter);
        }

        private void method_532(HabitatPrioritizationListFilter filter)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            Resource selectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            bool criticalResourcesSelected = cmbExpansionPlannerResourceFilter.CriticalResourcesSelected;
            string text = method_538().ToLower(CultureInfo.InvariantCulture);
            string text2;
            if ((text2 = text) != null)
            {
                BaconMain.method_532_SetExpansionPlanner(text2);
                switch (text2)
                {
                    case "resourcessupply":
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Resource Supply");
                        btnExpansionPlannerSelectTarget.Text = TextResolver.GetText("Select Resource Location");
                        btnExpansionPlannerGotoTarget.Text = TextResolver.GetText("Go to Resource Location");
                        btnExpansionPlannerBuildColonyShip.Enabled = false;
                        chkExpansionPlannerToggleAsteroids.Visible = false;
                        LrufvZylIl.Visible = false;
                        habitatPrioritizationList = _Game.PlayerEmpire.ResolveResourceSupplyLocations();
                        break;
                    case "resourcesgalaxy":
                        lblExpansionPlannerAvailableBuiltObjects.Text = TextResolver.GetText("Available Construction ships");
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Resource Target");
                        btnExpansionPlannerSelectTarget.Text = TextResolver.GetText("Select Resource Target");
                        btnExpansionPlannerGotoTarget.Text = TextResolver.GetText("Go to Resource Target");
                        method_161();
                        chkExpansionPlannerToggleAsteroids.Visible = true;
                        LrufvZylIl.Visible = false;
                        habitatPrioritizationList = _Game.PlayerEmpire.IdentifyResourceCentres(_Game.Galaxy, filterOutAssignedHabitats: false, filterOutDangerousTargets: false, chkExpansionPlannerToggleAsteroids.Checked);
                        break;
                    case "resourcesyou":
                        lblExpansionPlannerAvailableBuiltObjects.Text = TextResolver.GetText("Available Construction ships");
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Resource Target");
                        btnExpansionPlannerSelectTarget.Text = TextResolver.GetText("Select Resource Target");
                        btnExpansionPlannerGotoTarget.Text = TextResolver.GetText("Go to Resource Target");
                        method_161();
                        chkExpansionPlannerToggleAsteroids.Visible = true;
                        LrufvZylIl.Visible = false;
                        habitatPrioritizationList = _Game.PlayerEmpire.PrioritizeEmpireResourceNeeds(includeLuxuryResources: true, 49, 0.001, filterOutHabitatsWithMiningStationsUnderConstruction: false, chkExpansionPlannerToggleAsteroids.Checked);
                        break;
                    case "colonies":
                        lblExpansionPlannerAvailableBuiltObjects.Text = TextResolver.GetText("Available Colony ships");
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Potential Colony");
                        btnExpansionPlannerSelectTarget.Text = TextResolver.GetText("Select Potential Colony");
                        btnExpansionPlannerGotoTarget.Text = TextResolver.GetText("Go to Potential Colony");
                        chkExpansionPlannerToggleAsteroids.Visible = false;
                        LrufvZylIl.Visible = true;
                        habitatPrioritizationList = _Game.PlayerEmpire.IdentifyColonizationTargets(_Game.Galaxy, filterOutDangerousTargets: false, 0, 5000, LrufvZylIl.Checked, includeDistantTargets: true);
                        method_161();
                        if (habitatPrioritizationList.Count > 0)
                        {
                            btnExpansionPlannerBuildColonyShip.Enabled = true;
                        }
                        break;
                }
            }
            gmapExpansionPlanner.BringToFront();
            if (criticalResourcesSelected)
            {
                habitatPrioritizationList = method_534(habitatPrioritizationList, _Game.PlayerEmpire);
            }
            else if (selectedResource != null)
            {
                habitatPrioritizationList = method_535(habitatPrioritizationList, selectedResource.ResourceID, _Game.PlayerEmpire);
            }
            bool bindingForColonization = false;
            if (text == "colonies")
            {
                bindingForColonization = true;
            }
            FilterOutHabitatPrioritizationList(habitatPrioritizationList, filter);
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            ctlExpansionPlannerTargets.BindData(_Game.Galaxy, habitatPrioritizationList, bindingForColonization);
            ctlExpansionPlannerTargets.SelectHabitatPrioritization(selectedHabitatPrioritization);
            HabitatPrioritization selectedHabitatPrioritization2 = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            if (selectedHabitatPrioritization2 != null && selectedHabitatPrioritization2.Habitat != null)
            {
                gmapExpansionPlanner.SetPosition(selectedHabitatPrioritization2.Habitat.Xpos, selectedHabitatPrioritization2.Habitat.Ypos);
                gmapExpansionPlanner.Invalidate();
                btnExpansionPlannerGotoTarget.Enabled = true;
                btnExpansionPlannerSelectTarget.Enabled = true;
            }
            else
            {
                gmapExpansionPlanner.SetPosition(0.0, 0.0);
                gmapExpansionPlanner.Invalidate();
                btnExpansionPlannerGotoTarget.Enabled = false;
                btnExpansionPlannerSelectTarget.Enabled = false;
            }
        }

        private void FilterOutHabitatPrioritizationList(HabitatPrioritizationList habitatPrioritizationList, HabitatPrioritizationListFilter filter)
        {
            if (filter.Enabled)
            {
                for (int i = 0; i < habitatPrioritizationList.Count;)
                {
                    if (filter.FilterType == FilterType.TotalResource)
                    {
                        if (habitatPrioritizationList[i].Habitat.Resources.Count == 0 || habitatPrioritizationList[i].Habitat.Resources.Sum(x => x.Abundance) >= filter.Percentage)
                        {
                            i++;
                        }
                        else
                        { habitatPrioritizationList.RemoveAt(i); }
                    }
                    else if (filter.FilterType == FilterType.SelectedResource && filter.SelectedResource != null)
                    {
                        if (habitatPrioritizationList[i].Habitat.Resources.ContainsId(filter.SelectedResource.ResourceID))
                        {
                            if (habitatPrioritizationList[i].Habitat.Resources[habitatPrioritizationList[i].Habitat.Resources.IndexOf(filter.SelectedResource.ResourceID, 0)].Abundance >= filter.Percentage)
                            {
                                i++;
                            }
                            else
                            { habitatPrioritizationList.RemoveAt(i); }
                        }
                    }
                    else
                    { i++; }
                }
            }
        }

        private void method_533()
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            BuiltObjectList builtObjectList2 = new BuiltObjectList();
            builtObjectList2.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList2.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            switch (method_538().ToLower(CultureInfo.InvariantCulture))
            {
                case "resourcesyou":
                case "resourcesgalaxy":
                    foreach (BuiltObject item in builtObjectList2)
                    {
                        if (item.SubRole == BuiltObjectSubRole.ConstructionShip && item.BuiltAt == null && (item.Mission == null || item.Mission.Type == BuiltObjectMissionType.Undefined || item.Mission.Priority == BuiltObjectMissionPriority.Low))
                        {
                            builtObjectList.Add(item);
                        }
                    }
                    break;
                case "colonies":
                    if (selectedHabitatPrioritization == null || selectedHabitatPrioritization.Habitat == null)
                    {
                        break;
                    }
                    foreach (BuiltObject item2 in builtObjectList2)
                    {
                        if (item2.SubRole == BuiltObjectSubRole.ColonyShip && (item2.Mission == null || item2.Mission.Type == BuiltObjectMissionType.Undefined || item2.Mission.Priority == BuiltObjectMissionPriority.Low))
                        {
                            int newPopulationAmount = 0;
                            if (_Game.PlayerEmpire.CanBuiltObjectColonizeHabitat(item2, selectedHabitatPrioritization.Habitat, out newPopulationAmount))
                            {
                                builtObjectList.Add(item2);
                            }
                        }
                    }
                    break;
            }
            BuiltObject selectedBuiltObject = cmbExpansionPlannerAvailableBuiltObjects.SelectedBuiltObject;
            cmbExpansionPlannerAvailableBuiltObjects.BindData(builtObjectList, _Game.Galaxy, normalHeight: false);
            if (selectedBuiltObject != null)
            {
                cmbExpansionPlannerAvailableBuiltObjects.SetSelectedBuiltObject(selectedBuiltObject);
            }
            if (cmbExpansionPlannerAvailableBuiltObjects.SelectedBuiltObject == null && cmbExpansionPlannerAvailableBuiltObjects.Items.Count > 0)
            {
                cmbExpansionPlannerAvailableBuiltObjects.SelectedIndex = 0;
            }
            if (cmbExpansionPlannerAvailableBuiltObjects.Items != null && cmbExpansionPlannerAvailableBuiltObjects.Items.Count > 0)
            {
                switch (method_538().ToLower(CultureInfo.InvariantCulture))
                {
                    case "resourcesyou":
                    case "resourcesgalaxy":
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Resource Target and Construction Ship");
                        break;
                    case "colonies":
                        lblExpansionPlannerGalaxyMap.Text = TextResolver.GetText("Location of Potential Colony and Colony Ship");
                        break;
                    case "resourcessupply":
                        break;
                }
            }
        }

        private void RyphEufuaW()
        {
            string text = string.Empty;
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            BuiltObject selectedBuiltObject = cmbExpansionPlannerAvailableBuiltObjects.SelectedBuiltObject;
            bool enabled = true;
            if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
            {
                gmapExpansionPlanner.SetPosition(selectedHabitatPrioritization.Habitat.Xpos, selectedHabitatPrioritization.Habitat.Ypos);
                if (selectedBuiltObject != null)
                {
                    gmapExpansionPlanner.SetPositionAlt(selectedBuiltObject.Xpos, selectedBuiltObject.Ypos);
                }
                btnExpansionPlannerGotoTarget.Enabled = true;
                btnExpansionPlannerSelectTarget.Enabled = true;
            }
            else
            {
                gmapExpansionPlanner.SetPosition(0.0, 0.0);
                gmapExpansionPlanner.SetPositionAlt(0.0, 0.0);
                btnExpansionPlannerGotoTarget.Enabled = false;
                btnExpansionPlannerSelectTarget.Enabled = false;
            }
            gmapExpansionPlanner.Invalidate();
            if (method_538().ToLower(CultureInfo.InvariantCulture) == "resourcessupply")
            {
                enabled = false;
            }
            else if (selectedBuiltObject != null && selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null && selectedHabitatPrioritization.AssignedShip == null)
            {
                switch (method_538().ToLower(CultureInfo.InvariantCulture))
                {
                    case "resourcesyou":
                    case "resourcesgalaxy":
                        if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null && _Game.Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(_Game.PlayerEmpire, selectedHabitatPrioritization.Habitat))
                        {
                            text = string.Format(TextResolver.GetText("Send X to build a mining station at Y"), selectedBuiltObject.Name, selectedHabitatPrioritization.Habitat.Name);
                            break;
                        }
                        text = "(" + TextResolver.GetText("Cannot build here") + ")";
                        enabled = false;
                        break;
                    case "colonies":
                        if (_Game.PlayerEmpire.CanEmpireColonizeHabitatRange(_Game.PlayerEmpire, selectedHabitatPrioritization.Habitat))
                        {
                            text = string.Format(TextResolver.GetText("Send X to colonize Y"), selectedBuiltObject.Name, selectedHabitatPrioritization.Habitat.Name);
                            break;
                        }
                        text = TextResolver.GetText("Cannot Colonize");
                        enabled = false;
                        break;
                }
            }
            else
            {
                enabled = false;
                switch (method_538().ToLower(CultureInfo.InvariantCulture))
                {
                    case "resourcesyou":
                    case "resourcesgalaxy":
                        if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null && _Game.Galaxy.CheckEmpireTerritoryCanBuildAtHabitat(_Game.PlayerEmpire, selectedHabitatPrioritization.Habitat))
                        {
                            text = ((cmbExpansionPlannerAvailableBuiltObjects.Items.Count > 0 && selectedHabitatPrioritization != null && selectedHabitatPrioritization.AssignedShip == null) ? ("(" + TextResolver.GetText("No Construction ship selected") + ")") : ((selectedHabitatPrioritization == null || selectedHabitatPrioritization.Habitat == null) ? ("(" + TextResolver.GetText("No resource target selected") + ")") : ((selectedHabitatPrioritization.AssignedShip == null) ? ("(" + TextResolver.GetText("No Construction ships available") + ")") : ("(" + TextResolver.GetText("Construction Ship already assigned") + ")"))));
                            break;
                        }
                        text = "(" + TextResolver.GetText("Cannot build here") + ")";
                        enabled = false;
                        break;
                    case "colonies":
                        text = ((cmbExpansionPlannerAvailableBuiltObjects.Items.Count > 0 && selectedHabitatPrioritization != null && selectedHabitatPrioritization.AssignedShip == null) ? ("(" + TextResolver.GetText("No Colony ship selected") + ")") : ((selectedHabitatPrioritization == null || selectedHabitatPrioritization.Habitat == null) ? ("(" + TextResolver.GetText("No colony target selected") + ")") : ((selectedHabitatPrioritization.AssignedShip == null) ? ("(" + TextResolver.GetText("No Colony ships available") + ")") : ("(" + TextResolver.GetText("Colony Ship already assigned") + ")"))));
                        break;
                }
            }
            btnExpansionPlannerAction.Text = text;
            btnExpansionPlannerAction.Enabled = enabled;
            method_161();
        }

        private HabitatPrioritizationList method_534(HabitatPrioritizationList habitatPrioritizationList_0, Empire empire_5)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            if (empire_5 != null && empire_5.ResourceMap != null)
            {
                ResourceBonusList resourceBonusList = new ResourceBonusList();
                if (empire_5.DominantRace != null)
                {
                    resourceBonusList = empire_5.DominantRace.CriticalResources;
                }
                {
                    foreach (HabitatPrioritization item in habitatPrioritizationList_0)
                    {
                        if (item.Habitat == null || item.Habitat.Resources == null || item.Habitat.Resources.Count <= 0 || empire_5.ResourceMap == null || !empire_5.ResourceMap.CheckResourcesKnown(item.Habitat))
                        {
                            continue;
                        }
                        for (int i = 0; i < item.Habitat.Resources.Count; i++)
                        {
                            HabitatResource habitatResource = item.Habitat.Resources[i];
                            if (habitatResource != null && resourceBonusList.GetBonusByResourceType(habitatResource.ResourceID) != null)
                            {
                                habitatPrioritizationList.Add(item);
                                break;
                            }
                        }
                    }
                    return habitatPrioritizationList;
                }
            }
            return habitatPrioritizationList;
        }

        private HabitatPrioritizationList method_535(HabitatPrioritizationList habitatPrioritizationList_0, byte byte_2, Empire empire_5)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            if (empire_5 != null && empire_5.ResourceMap != null)
            {
                foreach (HabitatPrioritization item in habitatPrioritizationList_0)
                {
                    if (item.Habitat != null && item.Habitat.Resources != null && item.Habitat.Resources.Count > 0 && empire_5.ResourceMap != null && empire_5.ResourceMap.CheckResourcesKnown(item.Habitat))
                    {
                        int num = item.Habitat.Resources.IndexOf(byte_2, 0);
                        if (num >= 0)
                        {
                            habitatPrioritizationList.Add(item);
                        }
                    }
                }
                return habitatPrioritizationList;
            }
            return habitatPrioritizationList;
        }

        private HabitatPrioritizationList method_536(HabitatPrioritizationList habitatPrioritizationList_0)
        {
            HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
            foreach (HabitatPrioritization item in habitatPrioritizationList_0)
            {
                if (item.AssignedShip != null)
                {
                    habitatPrioritizationList.Add(item);
                }
            }
            foreach (HabitatPrioritization item2 in habitatPrioritizationList)
            {
                habitatPrioritizationList_0.Remove(item2);
            }
            return habitatPrioritizationList_0;
        }

        private void method_537(string string_30)
        {
            switch (string_30.ToLower(CultureInfo.InvariantCulture))
            {
                case "resourcessupply":
                    cmbExpansionPlannerMode.SelectedIndex = 3;
                    break;
                case "resourcesgalaxy":
                    cmbExpansionPlannerMode.SelectedIndex = 2;
                    break;
                case "resourcesyou":
                    cmbExpansionPlannerMode.SelectedIndex = 1;
                    break;
                case "colonies":
                    cmbExpansionPlannerMode.SelectedIndex = 0;
                    break;
            }
        }

        private string method_538()
        {
            string result = string.Empty;
            switch (cmbExpansionPlannerMode.SelectedIndex)
            {
                case 0:
                    result = "colonies";
                    break;
                case 1:
                    result = "resourcesyou";
                    break;
                case 2:
                    result = "resourcesgalaxy";
                    break;
                case 3:
                    result = "resourcessupply";
                    break;
            }
            return result;
        }

        private void cmbExpansionPlannerMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            PlannerNeedRefresh(filter);
        }

        private void cmbExpansionPlannerResourceFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            PlannerNeedRefresh(filter);
        }

        private void useResourcePercentFilter_CheckedChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            PlannerNeedRefresh(filter);
        }
        private void numResourcePercentFilter_ValueChanged(object sender, EventArgs e)
        {
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            if (filter.Enabled)
            { PlannerNeedRefresh(filter); }
        }
        private void PlannerNeedRefresh(HabitatPrioritizationListFilter filter)
        {
            method_532(filter);
            method_533();
            RyphEufuaW();
        }

        private void cmbExpansionPlannerAvailableBuiltObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            RyphEufuaW();
        }

        private void btnExpansionPlannerBuildColonyShip_Click(object sender, EventArgs e)
        {
            switch (method_538().ToLower(CultureInfo.InvariantCulture))
            {
                case "colonies":
                    {
                        HabitatPrioritization selectedHabitatPrioritization2 = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
                        if (selectedHabitatPrioritization2 != null && selectedHabitatPrioritization2.Habitat != null)
                        {
                            method_539(selectedHabitatPrioritization2.Habitat);
                        }
                        break;
                    }
                case "resourcesyou":
                case "resourcesgalaxy":
                    {
                        HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
                        if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
                        {
                            method_540(null, selectedHabitatPrioritization.Habitat);
                        }
                        break;
                    }
            }
            HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
            filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
            filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
            filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
            filter.Enabled = _chkUseResourcePercentFilter.Checked;
            method_532(filter);
            method_533();
        }

        private void method_539(Habitat habitat_9)
        {
            if (habitat_9 == null)
            {
                return;
            }
            List<HabitatType> colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpire(_Game.PlayerEmpire);
            Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuild(BuiltObjectSubRole.ColonyShip);
            if (design == null || !_Game.PlayerEmpire.CanEmpireColonizeHabitat(_Game.PlayerEmpire, habitat_9, colonizableHabitatTypes, design))
            {
                return;
            }
            colonizableHabitatTypes = _Game.PlayerEmpire.ColonizableHabitatTypesForEmpireTechOnly(_Game.PlayerEmpire);
            Habitat habitat = null;
            double num = double.MaxValue;
            foreach (Habitat colony in _Game.PlayerEmpire.Colonies)
            {
                if ((habitat_9.Empire == _Game.Galaxy.IndependentEmpire || colonizableHabitatTypes.Contains(habitat_9.Type) || colony.Population.DominantRace.NativeHabitatType == habitat_9.Type) && colony.Population.TotalAmount >= Galaxy.BuildColonyShipPopulationRequirement)
                {
                    double num2 = _Game.Galaxy.CalculateDistance(colony.Xpos, colony.Ypos, habitat_9.Xpos, habitat_9.Ypos);
                    double num3 = 600.0;
                    if (colony.ConstructionQueue != null)
                    {
                        num3 = colony.ConstructionQueue.EstimateCurrentWaitQueueTime();
                        num3 += 1.0;
                    }
                    double num4 = num3 * num2;
                    if (num2 > 0.0)
                    {
                        num4 = num3 * Math.Sqrt(num2);
                    }
                    if (num4 < num)
                    {
                        num = num4;
                        habitat = colony;
                    }
                }
            }
            if (habitat != null)
            {
                bool isAutoControlled = _Game.PlayerEmpire.NewBuiltObjectShouldBeAutomated(BuiltObjectSubRole.ColonyShip);
                _Game.PlayerEmpire.PurchaseNewBuiltObject(design, habitat, isStateOwned: true, isAutoControlled)?.AssignMission(BuiltObjectMissionType.Colonize, habitat_9, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
            }
        }

        private void method_540(BuiltObject builtObject_8, Habitat habitat_9)
        {
            BuiltObjectSubRole subRole = BuiltObjectSubRole.MiningStation;
            if (habitat_9.Resources.CountGasResources() > 0)
            {
                subRole = BuiltObjectSubRole.GasMiningStation;
            }
            Design design = _Game.PlayerEmpire.Designs.FindNewestCanBuild(subRole);
            if (design == null)
            {
                return;
            }
            double num = 0.0;
            double num2 = 0.0;
            _Game.Galaxy.SelectRelativeHabitatSurfacePoint(habitat_9, out num, out num2);
            if (builtObject_8 == null)
            {
                builtObject_8 = _Game.Galaxy.FastFindBestConstructionShip(habitat_9.Xpos, habitat_9.Ypos, _Game.PlayerEmpire);
                if (builtObject_8 != null)
                {
                    if (builtObject_8.Mission != null && builtObject_8.Mission.Type != 0)
                    {
                        builtObject_8.QueueMission(BuiltObjectMissionType.Build, habitat_9, null, design, num, num2, BuiltObjectMissionPriority.Normal);
                    }
                    else
                    {
                        builtObject_8.AssignMission(BuiltObjectMissionType.Build, habitat_9, null, design, num, num2, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                    }
                }
            }
            else
            {
                builtObject_8.AssignMission(BuiltObjectMissionType.Build, habitat_9, null, design, num, num2, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
            }
        }

        private void btnExpansionPlannerAction_Click(object sender, EventArgs e)
        {
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            BuiltObject selectedBuiltObject = cmbExpansionPlannerAvailableBuiltObjects.SelectedBuiltObject;
            if (selectedBuiltObject != null && selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
            {
                switch (method_538())
                {
                    case "resourcesyou":
                    case "resourcesgalaxy":
                        method_540(selectedBuiltObject, selectedHabitatPrioritization.Habitat);
                        break;
                    case "colonies":
                        selectedBuiltObject.AssignMission(BuiltObjectMissionType.Colonize, selectedHabitatPrioritization.Habitat, null, BuiltObjectMissionPriority.Normal, manuallyAssigned: true);
                        break;
                }
                HabitatPrioritizationListFilter filter = new HabitatPrioritizationListFilter();
                filter.FilterType = cmbExpansionPlannerResourceFilter.SelectedResource != null ? FilterType.SelectedResource : FilterType.TotalResource;
                filter.Percentage = (int)_numResourcePercentFilter.Value * 10;
                filter.SelectedResource = cmbExpansionPlannerResourceFilter.SelectedResource;
                filter.Enabled = _chkUseResourcePercentFilter.Checked;
                method_532(filter);
                method_533();
            }
        }

        private void ctlExpansionPlannerTargets_SelectionChanged(object sender, EventArgs e)
        {
            method_533();
            RyphEufuaW();
        }

        private void btnExpansionPlannerSortResources_Click(object sender, EventArgs e)
        {
            ResourceList resources = _Game.PlayerEmpire.IdentifyDeficientEmpireResources(includeLuxuryResources: true, 0.001);
            ctlExpansionPlannerResources.BindData(_Game.Galaxy, resources, _uiResourcesBitmaps);
        }

        private void method_541(object object_7, int int_64)
        {
            if (_Game.Galaxy.ResourceSystem.Resources[int_64].Group == ResourceGroup.Luxury)
            {
                string name = _Game.Galaxy.ResourceSystem.Resources[int_64].Name;
                method_456(name);
            }
            else
            {
                Resource resource_ = new Resource((byte)int_64);
                method_552(resource_);
            }
        }

        private void btnColonyShowExpansionPlanner_Click(object sender, EventArgs e)
        {
            method_160("colonies");
        }

        private void btnBuiltObjectShowMiningPlanner_Click(object sender, EventArgs e)
        {
            method_160("resourcesyou");
        }

        private void btnEmpireSummaryShowExpansionPlanner_Click(object sender, EventArgs e)
        {
            method_159();
        }

        private void btnExpansionPlanner_Click(object sender, EventArgs e)
        {
            if (pnlExpansionPlanner.Visible)
            {
                method_162();
            }
            else
            {
                method_159();
            }
        }

        private void method_542()
        {
            _Game.PlayerEmpire.RemoveOldHistoryMessages();
            _Game.PlayerEmpire.MessageHistory.Sort();
            _Game.PlayerEmpire.MessageHistory.Reverse();
            int selectedIndex = cmbMessageHistoryFilter.SelectedIndex;
            bool flag = true;
            bool flag2 = true;
            if (selectedIndex > 0)
            {
                flag2 = false;
            }
            if (selectedIndex == 2)
            {
                flag = false;
                pnlMessageHistory.HeaderTitle = TextResolver.GetText("Galactic History");
            }
            else
            {
                pnlMessageHistory.HeaderTitle = TextResolver.GetText("Messages");
            }
            EmpireMessageList empireMessageList = new EmpireMessageList();
            foreach (EmpireMessage item in _Game.PlayerEmpire.MessageHistory)
            {
                bool flag3 = true;
                if (!flag2 && (item.MessageType == EmpireMessageType.BattleAttacking || item.MessageType == EmpireMessageType.BattleUnderAttack || item.MessageType == EmpireMessageType.IncomingEnemyFleet))
                {
                    flag3 = false;
                }
                if (!flag && item.MessageType != EmpireMessageType.GalacticHistory)
                {
                    flag3 = false;
                }
                if (flag3)
                {
                    empireMessageList.Add(item);
                }
            }
            ctlMessageHistoryMessages.BindData(empireMessageList, bitmap_28, bitmap_8, characterImageCache_0, bitmap_82);
        }

        private void btnExpansionPlannerGotoTarget_Click(object sender, EventArgs e)
        {
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
            {
                method_157(selectedHabitatPrioritization.Habitat);
                method_4(1.0);
                method_162();
            }
        }

        private void btnExpansionPlannerSelectTarget_Click(object sender, EventArgs e)
        {
            HabitatPrioritization selectedHabitatPrioritization = ctlExpansionPlannerTargets.SelectedHabitatPrioritization;
            if (selectedHabitatPrioritization != null && selectedHabitatPrioritization.Habitat != null)
            {
                method_208(selectedHabitatPrioritization.Habitat);
            }
        }

        private void lnkDiplomacyReputation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Empire Reputation"));
        }

        private void lnkDiplomacyPirates_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_456(TextResolver.GetText("Pirates"));
        }

        private void method_543(DistantWorlds.Types.Component component_0)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlComponentGuide.Size = new Size(360, 530);
            pnlComponentGuide.Location = new Point((mainView.Width - pnlComponentGuide.Width) / 2, (mainView.Height - pnlComponentGuide.Height) / 2);
            pnlComponentGuide.DoLayout();
            pnlComponentGuideGroup.Size = new Size(325, 445);
            pnlComponentGuideGroup.Location = new Point(10, 10);
            pnlComponentGuideGroup.CurveMode = CornerCurveMode.All;
            pnlComponentGuideDetail.Size = new Size(305, 275);
            pnlComponentGuideDetail.Location = new Point(10, 10);
            pnlComponentGuideDetail.SetTextPositions(10, 140, 160, 115);
            lnkComponentGuideType.Location = new Point(10, 290);
            lblComponentGuideResources.Location = new Point(10, 315);
            lblComponentGuideResources.Text = TextResolver.GetText("Resources required to manufacture");
            ctlComponentGuideResources.Size = new Size(305, 105);
            ctlComponentGuideResources.Location = new Point(10, 330);
            ctlComponentGuideResources.Grid.Columns["Picture"].Width = 50;
            ctlComponentGuideResources.Grid.Columns["Type"].Width = 200;
            ctlComponentGuideResources.Grid.Columns["Quantity"].Width = 55;
            ctlComponentGuideResources.BringToFront();
            method_544(component_0);
            pnlComponentGuide.BringToFront();
            pnlComponentGuide.Visible = true;
        }

        private void method_544(DistantWorlds.Types.Component component_0)
        {
            if (component_0 != null)
            {
                string arg = method_546(component_0);
                lnkComponentGuideType.Text = string.Format(TextResolver.GetText("Learn about X"), arg) + "...";
            }
            int num = 0;
            ComponentResourceList componentResources = null;
            if (component_0 != null)
            {
                num = component_0.PictureRef;
                componentResources = component_0.RequiredResources;
            }
            pnlComponentGuideDetail.Ignite(_Game.Galaxy, component_0, bitmap_21[num], largeFont: true);
            ctlComponentGuideResources.BindData(componentResources, _uiResourcesBitmaps);
        }

        private void method_545()
        {
            pnlComponentGuide.SendToBack();
            pnlComponentGuide.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private string method_546(DistantWorlds.Types.Component component_0)
        {
            string result = TextResolver.GetText("Components");
            if (component_0 != null)
            {
                switch (component_0.Category)
                {
                    case ComponentCategoryType.WeaponArea:
                        result = TextResolver.GetText("Area Weapons");
                        break;
                    case ComponentCategoryType.WeaponPointDefense:
                        result = TextResolver.GetText("Point Defense Weapons");
                        break;
                    case ComponentCategoryType.WeaponIon:
                        switch (component_0.Type)
                        {
                            case ComponentType.WeaponIonCannon:
                            case ComponentType.WeaponIonPulse:
                                result = TextResolver.GetText("Ion Weapons");
                                break;
                            case ComponentType.WeaponIonDefense:
                                result = TextResolver.GetText("Component Type Ion Defense");
                                break;
                        }
                        break;
                    case ComponentCategoryType.WeaponGravity:
                        result = component_0.Type switch
                        {
                            ComponentType.WeaponTractorBeam => TextResolver.GetText("Tractor Beams"),
                            ComponentType.WeaponGravityBeam => TextResolver.GetText("Gravity Beam Weapons"),
                            ComponentType.WeaponAreaGravity => TextResolver.GetText("Gravity Area Weapons"),
                            _ => (component_0.Value7 <= 0) ? TextResolver.GetText("Torpedo Weapons") : TextResolver.GetText("Bombard Weapons"),
                        };
                        break;
                    case ComponentCategoryType.Armor:
                        result = TextResolver.GetText("Armor");
                        break;
                    case ComponentCategoryType.AssaultPod:
                        result = TextResolver.GetText("Assault Pods");
                        break;
                    case ComponentCategoryType.Fighter:
                        {
                            ComponentType type3 = component_0.Type;
                            if (type3 == ComponentType.FighterBay)
                            {
                                result = TextResolver.GetText("Fighter Bays");
                            }
                            break;
                        }
                    case ComponentCategoryType.Shields:
                        result = TextResolver.GetText("Shields");
                        break;
                    case ComponentCategoryType.ShieldRecharge:
                        result = TextResolver.GetText("Area Shield Recharge");
                        break;
                    case ComponentCategoryType.Engine:
                        switch (component_0.Type)
                        {
                            case ComponentType.EngineMainThrust:
                                result = TextResolver.GetText("Engines");
                                break;
                            case ComponentType.EngineVectoring:
                                result = TextResolver.GetText("Vectoring Engines");
                                break;
                        }
                        break;
                    case ComponentCategoryType.HyperDrive:
                        result = TextResolver.GetText("Hyperdrives");
                        break;
                    case ComponentCategoryType.HyperDisrupt:
                        switch (component_0.Type)
                        {
                            case ComponentType.HyperDeny:
                                result = TextResolver.GetText("HyperDeny Components");
                                break;
                            case ComponentType.HyperStop:
                                result = TextResolver.GetText("Gravity Well Projectors");
                                break;
                        }
                        break;
                    case ComponentCategoryType.Reactor:
                        result = TextResolver.GetText("Reactors");
                        break;
                    case ComponentCategoryType.EnergyCollector:
                        {
                            result = TextResolver.GetText("Energy Collectors");
                            ComponentType type2 = component_0.Type;
                            if (type2 == ComponentType.EnergyToFuel)
                            {
                                result = TextResolver.GetText("Energy To Fuel Converter");
                            }
                            break;
                        }
                    case ComponentCategoryType.Extractor:
                        result = TextResolver.GetText("Resource Extractors");
                        break;
                    case ComponentCategoryType.Manufacturer:
                        result = TextResolver.GetText("Manufacturers");
                        break;
                    case ComponentCategoryType.Storage:
                        switch (component_0.Type)
                        {
                            case ComponentType.StorageFuel:
                                result = TextResolver.GetText("Fuel Storage");
                                break;
                            case ComponentType.StorageCargo:
                                result = TextResolver.GetText("Cargo Storage");
                                break;
                            case ComponentType.StorageTroop:
                                result = TextResolver.GetText("Troop Storage");
                                break;
                            case ComponentType.StoragePassenger:
                                result = TextResolver.GetText("Passenger Storage");
                                break;
                            case ComponentType.StorageDockingBay:
                                result = TextResolver.GetText("Docking Bays");
                                break;
                        }
                        break;
                    case ComponentCategoryType.Sensor:
                        switch (component_0.Type)
                        {
                            case ComponentType.SensorProximityArray:
                                result = TextResolver.GetText("Proximity Arrays");
                                break;
                            case ComponentType.SensorResourceProfileSensor:
                                result = TextResolver.GetText("Resource Profile Sensors");
                                break;
                            case ComponentType.SensorLongRange:
                                result = TextResolver.GetText("Long Range Scanners");
                                break;
                            case ComponentType.SensorTraceScanner:
                                result = TextResolver.GetText("Trace Scanners");
                                break;
                            case ComponentType.SensorScannerJammer:
                                result = TextResolver.GetText("Scanner Jammers");
                                break;
                            case ComponentType.SensorStealth:
                                result = TextResolver.GetText("Stealth");
                                break;
                        }
                        break;
                    case ComponentCategoryType.Computer:
                        switch (component_0.Type)
                        {
                            case ComponentType.ComputerTargetting:
                            case ComponentType.ComputerTargettingFleet:
                                result = TextResolver.GetText("Combat Targetting");
                                break;
                            case ComponentType.ComputerCountermeasures:
                            case ComponentType.ComputerCountermeasuresFleet:
                                result = TextResolver.GetText("Countermeasures");
                                break;
                            case ComponentType.ComputerCommandCenter:
                                result = TextResolver.GetText("Command Centers");
                                break;
                            case ComponentType.ComputerCommerceCenter:
                                result = TextResolver.GetText("Commerce Centers");
                                break;
                        }
                        break;
                    case ComponentCategoryType.Labs:
                        result = TextResolver.GetText("Research Laboratories");
                        break;
                    case ComponentCategoryType.Construction:
                        switch (component_0.Type)
                        {
                            case ComponentType.DamageControl:
                                result = TextResolver.GetText("Damage Control");
                                break;
                            case ComponentType.ConstructionBuild:
                                result = TextResolver.GetText("Construction Yards");
                                break;
                        }
                        break;
                    case ComponentCategoryType.Habitation:
                        switch (component_0.Type)
                        {
                            case ComponentType.HabitationLifeSupport:
                                result = TextResolver.GetText("Life Support");
                                break;
                            case ComponentType.HabitationHabModule:
                                result = TextResolver.GetText("Habitation Modules");
                                break;
                            case ComponentType.HabitationMedicalCenter:
                                result = TextResolver.GetText("Medical Centers");
                                break;
                            case ComponentType.HabitationRecreationCenter:
                                result = TextResolver.GetText("Recreation Centers");
                                break;
                            case ComponentType.HabitationColonization:
                                result = TextResolver.GetText("Colonization Modules");
                                break;
                        }
                        break;
                    case ComponentCategoryType.WeaponBeam:
                    case ComponentCategoryType.WeaponSuperBeam:
                        result = TextResolver.GetText("Beam Weapons");
                        switch (component_0.Type)
                        {
                            case ComponentType.WeaponPhaser:
                            case ComponentType.WeaponSuperPhaser:
                                result = TextResolver.GetText("Phased Weapons");
                                break;
                            case ComponentType.WeaponRailGun:
                            case ComponentType.WeaponSuperRailGun:
                                result = TextResolver.GetText("Rail Guns");
                                break;
                        }
                        break;
                    case ComponentCategoryType.WeaponSuperArea:
                        result = TextResolver.GetText("Area Weapons");
                        break;
                    case ComponentCategoryType.WeaponTorpedo:
                    case ComponentCategoryType.WeaponSuperTorpedo:
                        {
                            ComponentType type = component_0.Type;
                            result = ((type == ComponentType.WeaponMissile || type == ComponentType.WeaponSuperMissile) ? TextResolver.GetText("Missile Weapons") : ((component_0.Value7 <= 0) ? TextResolver.GetText("Torpedo Weapons") : TextResolver.GetText("Bombard Weapons")));
                            break;
                        }
                }
            }
            return result;
        }

        private void pnlComponentGuide_CloseButtonClicked(object sender, EventArgs e)
        {
            method_545();
        }

        private void method_547(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DistantWorlds.Types.Component component_ = null;
            if (e.Link.LinkData != null && e.Link.LinkData is DistantWorlds.Types.Component)
            {
                component_ = (DistantWorlds.Types.Component)e.Link.LinkData;
            }
            method_543(component_);
        }

        private void lnkComponentGuideType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string string_ = method_546(pnlComponentGuideDetail.Component);
            method_456(string_);
        }

        private void method_548(Design design_3)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlConstructionSummary.Size = new Size(418, 622);
            pnlConstructionSummary.Location = new Point((mainView.Width - pnlConstructionSummary.Width) / 2, (mainView.Height - pnlConstructionSummary.Height) / 2);
            pnlConstructionSummary.DoLayout();
            lblConstructionSummaryOverview.Location = new Point(10, 10);
            lblConstructionSummaryOverview.Font = font_7;
            string text = string.Format(TextResolver.GetText("Requirements for building ship design X"), design_3.Name);
            if (design_3.Role == BuiltObjectRole.Base)
            {
                text = string.Format(TextResolver.GetText("Requirements for building base design X"), design_3.Name);
            }
            lblConstructionSummaryOverview.Text = text;
            lblConstructionSummaryOverview.MaximumSize = new Size(380, 40);
            lblConstructionSummaryOverview.BringToFront();
            lblConstructionSummaryComponents.Location = new Point(10, 50);
            lblConstructionSummaryComponents.Text = TextResolver.GetText("Components Required");
            ctlConstructionSummaryComponents.Size = new Size(380, 200);
            ctlConstructionSummaryComponents.Location = new Point(10, 65);
            ctlConstructionSummaryComponents.Grid.Columns["TechPoints"].Visible = false;
            ctlConstructionSummaryComponents.Grid.Columns["Category"].Width = 65;
            ctlConstructionSummaryComponents.Grid.Columns["Picture"].Width = 35;
            ctlConstructionSummaryComponents.Grid.Columns["Name"].Width = 230;
            ctlConstructionSummaryComponents.Grid.Columns["Size"].Width = 50;
            ctlConstructionSummaryComponents.BringToFront();
            lblConstructionSummaryResources.Location = new Point(10, 280);
            lblConstructionSummaryResources.Text = TextResolver.GetText("Resources Required");
            ctlConstructionSummaryConstructionResources.Size = new Size(380, 250);
            ctlConstructionSummaryConstructionResources.Location = new Point(10, 295);
            ctlConstructionSummaryConstructionResources.Grid.Columns["Picture"].Width = 40;
            ctlConstructionSummaryConstructionResources.Grid.Columns["Type"].Width = 290;
            ctlConstructionSummaryConstructionResources.Grid.Columns["Quantity"].Width = 50;
            ctlConstructionSummaryConstructionResources.BringToFront();
            ctlConstructionSummaryComponents.BindData(design_3.Components, bitmap_21, _Game.Galaxy);
            ComponentResourceList componentResources = _Game.PlayerEmpire.ResolveResourcesFromComponents(design_3.Components);
            ctlConstructionSummaryConstructionResources.BindData(componentResources, _uiResourcesBitmaps);
            pnlConstructionSummary.BringToFront();
            pnlConstructionSummary.Visible = true;
        }

        private void method_549()
        {
            pnlConstructionSummary.SendToBack();
            pnlConstructionSummary.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void pnlConstructionSummary_CloseButtonClicked(object sender, EventArgs e)
        {
            method_549();
        }

        private void btnDesignsShowComponentGuide_Click(object sender, EventArgs e)
        {
            DistantWorlds.Types.Component component = pnlDesignComponentDetail.Component;
            if (component == null)
            {
                component = ctlDesignComponentToolbox.SelectedComponent;
            }
            method_543(component);
        }

        private void btnDesignsShowConstructionSummary_Click(object sender, EventArgs e)
        {
            PrepareDesignForEditor();
            method_548(design_0);
        }

        private void btnBuiltObjectConstructionShowSummary_Click(object sender, EventArgs e)
        {
            Design design = null;
            if (ctlConstructionYards.SelectedConstructionYard == null)
            {
                return;
            }
            BuiltObject shipUnderConstruction = ctlConstructionYards.SelectedConstructionYard.ShipUnderConstruction;
            if (shipUnderConstruction != null)
            {
                design = shipUnderConstruction.Design;
                if (shipUnderConstruction.RetrofitDesign != null)
                {
                    design = shipUnderConstruction.RetrofitDesign;
                }
                method_548(design);
            }
        }

        private void btnColonyConstructionShowSummary_Click(object sender, EventArgs e)
        {
            Design design = null;
            if (ctlColonyConstructionYard.SelectedConstructionYard == null)
            {
                return;
            }
            BuiltObject shipUnderConstruction = ctlColonyConstructionYard.SelectedConstructionYard.ShipUnderConstruction;
            if (shipUnderConstruction != null)
            {
                design = shipUnderConstruction.Design;
                if (shipUnderConstruction.RetrofitDesign != null)
                {
                    design = shipUnderConstruction.RetrofitDesign;
                }
                method_548(design);
            }
        }

        private void pnlDetailInfoShipGroup_MouseClick(object sender, MouseEventArgs e)
        {
            pnlDetailInfoShipGroup.Invalidate();
            Hotspot hotspot = pnlDetailInfoShipGroup.Hotspots.ResolveHotspotAtPoint(e.X, e.Y);
            if (hotspot == null || hotspot.RelatedObject == null)
            {
                return;
            }
            if (hotspot.RelatedObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)hotspot.RelatedObject;
                BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(builtObject);
                Bitmap image = new Bitmap(builtObjectImageData.Image);
                image = ((builtObject.Empire == null) ? PrepareBuiltObjectImage(builtObject, image, Color.Gray, Color.Gray, 1.0, 1) : PrepareBuiltObjectImage(builtObject, image, builtObject.Empire.MainColor, builtObject.Empire.SecondaryColor, 1.0, 1));
                pnlDetailInfoShipGroup.SetData(_Game, _Game.Galaxy, image, new Bitmap(builtObjectImageData.MaskImage), builtObject);
            }
            else if (hotspot.RelatedObject is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)hotspot.RelatedObject;
                pnlDetailInfoShipGroup.SetData(_Game, _Game.Galaxy, shipGroup);
            }
            else if (hotspot.RelatedObject is Troop)
            {
                Troop troop_ = (Troop)hotspot.RelatedObject;
                method_172(troop_);
            }
            else if (hotspot.RelatedObject is Design)
            {
                Design design = (Design)hotspot.RelatedObject;
                design_1 = design.Clone();
                if (design.BuildCount > 0)
                {
                    string_16 = "view";
                }
                else if (design.Empire != null && design.Empire.CheckDesignInUseForConstructionOrRetrofits(design))
                {
                    string_16 = "view";
                }
                else
                {
                    string_16 = "edit";
                }
                OpenDesignEditor(design);
            }
        }

        private void pnlDetailInfo_MouseClick(object sender, MouseEventArgs e)
        {
            Hotspot hotspot = pnlDetailInfo.Hotspots.ResolveHotspotAtPoint(e.X, e.Y);
            if (hotspot != null)
            {
                if (hotspot.RelatedObject == null)
                {
                    return;
                }
                if (hotspot.RelatedObject is object[])
                {
                    object[] array = (object[])hotspot.RelatedObject;
                    if (array.Length > 0 && array[0] is Habitat)
                    {
                        Habitat habitat_ = (Habitat)array[0];
                        method_164(habitat_);
                    }
                }
                else if (hotspot.RelatedObject is BuiltObject)
                {
                    method_208(hotspot.RelatedObject);
                }
                else if (hotspot.RelatedObject is Fighter)
                {
                    method_208(hotspot.RelatedObject);
                }
                else if (hotspot.RelatedObject is Habitat)
                {
                    method_208(hotspot.RelatedObject);
                }
                else if (hotspot.RelatedObject is Creature)
                {
                    method_208(hotspot.RelatedObject);
                }
                else if (hotspot.RelatedObject is ShipGroup)
                {
                    method_208(hotspot.RelatedObject);
                }
                else if (hotspot.RelatedObject is Design)
                {
                    Design design = (Design)hotspot.RelatedObject;
                    design_1 = design.Clone();
                    if (design.BuildCount > 0)
                    {
                        string_16 = "view";
                    }
                    else if (design.Empire != null && design.Empire.CheckDesignInUseForConstructionOrRetrofits(design))
                    {
                        string_16 = "view";
                    }
                    else
                    {
                        string_16 = "edit";
                    }
                    OpenDesignEditor(design);
                }
                else if (hotspot.RelatedObject is Ruin)
                {
                    Ruin ruin_ = (Ruin)hotspot.RelatedObject;
                    method_550(ruin_);
                }
                else if (hotspot.RelatedObject is PlanetaryFacility)
                {
                    PlanetaryFacility planetaryFacility = (PlanetaryFacility)hotspot.RelatedObject;
                    if (planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                    {
                        method_456(TextResolver.GetText("Wonders"));
                    }
                    else
                    {
                        method_456(TextResolver.GetText("Planetary Facilities"));
                    }
                }
                else if (hotspot.RelatedObject is Resource)
                {
                    Resource resource = (Resource)hotspot.RelatedObject;
                    method_456(resource.Name);
                }
                else if (hotspot.RelatedObject is Race)
                {
                    Race race = (Race)hotspot.RelatedObject;
                    method_456(race.Name);
                }
                else if (hotspot.RelatedObject is Empire)
                {
                    Empire empire_ = (Empire)hotspot.RelatedObject;
                    method_195(empire_);
                }
                else if (hotspot.RelatedObject is Troop)
                {
                    Troop troop_ = (Troop)hotspot.RelatedObject;
                    method_172(troop_);
                }
                else
                {
                    if (!(hotspot.RelatedObject is Character))
                    {
                        return;
                    }
                    if (pnlGameEditor.Visible)
                    {
                        Character character = (Character)hotspot.RelatedObject;
                        if (character != null && character.Empire != null)
                        {
                            method_425(character, character.Empire, bool_28: true);
                        }
                    }
                    else
                    {
                        Character character2 = (Character)hotspot.RelatedObject;
                        if (character2 != null)
                        {
                            method_424(character2);
                        }
                    }
                }
            }
            else
            {
                method_157(_Game.SelectedObject);
            }
        }

        private void pnlDetailInfoShipGroup_MouseMove(object sender, MouseEventArgs e)
        {
            Point point = pnlDetailInfoShipGroup.PointToClient(MouseHelper.GetCursorPosition());
            Hotspot hotspot = pnlDetailInfoShipGroup.Hotspots.ResolveHotspotAtPoint(point.X, point.Y);
            if (hotspot != null)
            {
                hotspot.Hovered = true;
                pnlDetailInfoShipGroup.Invalidate();
            }
        }

        private void pnlDetailInfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (_Game.SelectedObject is SystemInfo)
            {
                pnlDetailInfo.Invalidate();
            }
        }

        private void btnCycleBasesBack_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectSubRole> list = new List<BuiltObjectSubRole>();
            list.Add(BuiltObjectSubRole.SmallSpacePort);
            list.Add(BuiltObjectSubRole.MediumSpacePort);
            list.Add(BuiltObjectSubRole.LargeSpacePort);
            list.Add(BuiltObjectSubRole.GenericBase);
            list.Add(BuiltObjectSubRole.EnergyResearchStation);
            list.Add(BuiltObjectSubRole.WeaponsResearchStation);
            list.Add(BuiltObjectSubRole.HighTechResearchStation);
            list.Add(BuiltObjectSubRole.MonitoringStation);
            list.Add(BuiltObjectSubRole.DefensiveBase);
            BuiltObjectList builtObjectsBySubRole = builtObjectList.GetBuiltObjectsBySubRole(list);
            int num = 0;
            if (builtObject_0 != null)
            {
                num = builtObjectsBySubRole.IndexOf(builtObject_0);
                num--;
                if (num < 0)
                {
                    num = builtObjectsBySubRole.Count - 1;
                }
            }
            if (builtObjectsBySubRole.Count > num)
            {
                builtObject_0 = builtObjectsBySubRole[num];
            }
            else
            {
                builtObject_0 = null;
            }
            if (builtObject_0 != null)
            {
                method_208(builtObject_0);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleMilitaryBack_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Military);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            int num = 0;
            if (builtObject_1 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_1);
                num--;
                if (num < 0)
                {
                    num = builtObjectsByRole.Count - 1;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_1 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_1 = null;
            }
            if (builtObject_1 != null)
            {
                method_208(builtObject_1);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleColoniesBack_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (habitat_0 != null)
            {
                num = _Game.PlayerEmpire.Colonies.IndexOf(habitat_0);
                num--;
                if (num < 0)
                {
                    num = _Game.PlayerEmpire.Colonies.Count - 1;
                }
            }
            if (_Game.PlayerEmpire.Colonies.Count > num)
            {
                habitat_0 = _Game.PlayerEmpire.Colonies[num];
            }
            else
            {
                habitat_0 = null;
            }
            if (habitat_0 != null)
            {
                method_208(habitat_0);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleConstructionBack_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Build);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            BuiltObjectList builtObjectsBySubRole = builtObjectList.GetBuiltObjectsBySubRole(BuiltObjectSubRole.ResupplyShip);
            if (builtObjectsBySubRole.Count > 0)
            {
                builtObjectsByRole.AddRange(builtObjectsBySubRole);
            }
            int num = 0;
            if (builtObject_2 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_2);
                num--;
                if (num < 0)
                {
                    num = builtObjectsByRole.Count - 1;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_2 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_2 = null;
            }
            if (builtObject_2 != null)
            {
                method_208(builtObject_2);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleOtherBack_Click(object sender, EventArgs e)
        {
            BuiltObjectList builtObjectList = new BuiltObjectList();
            builtObjectList.AddRange(_Game.PlayerEmpire.BuiltObjects);
            builtObjectList.AddRange(_Game.PlayerEmpire.PrivateBuiltObjects);
            List<BuiltObjectRole> list = new List<BuiltObjectRole>();
            list.Add(BuiltObjectRole.Colony);
            list.Add(BuiltObjectRole.Exploration);
            BuiltObjectList builtObjectsByRole = builtObjectList.GetBuiltObjectsByRole(list);
            int num = 0;
            if (builtObject_3 != null)
            {
                num = builtObjectsByRole.IndexOf(builtObject_3);
                num--;
                if (num < 0)
                {
                    num = builtObjectsByRole.Count - 1;
                }
            }
            if (builtObjectsByRole.Count > num)
            {
                builtObject_3 = builtObjectsByRole[num];
            }
            else
            {
                builtObject_3 = null;
            }
            if (builtObject_3 != null)
            {
                method_208(builtObject_3);
                if (UhvLmNjli7)
                {
                    method_157(builtObject_0);
                    UhvLmNjli7 = true;
                }
            }
            Focus();
        }

        private void btnCycleShipGroupsBack_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (shipGroup_0 != null)
            {
                num = _Game.PlayerEmpire.ShipGroups.IndexOf(shipGroup_0);
                num--;
                if (num < 0)
                {
                    num = _Game.PlayerEmpire.ShipGroups.Count - 1;
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

        private void btnCycleIdleShipsBack_Click(object sender, EventArgs e)
        {
            BuiltObject builtObject = null;
            ShipGroup shipGroup = null;
            int num = 0;
            int num2 = 0;
            if (builtObject_4 != null)
            {
                num = _Game.PlayerEmpire.BuiltObjects.IndexOf(builtObject_4);
                builtObject = method_350(num, -1);
                if (builtObject == null)
                {
                    shipGroup = method_349(_Game.PlayerEmpire.ShipGroups.Count, -1);
                    if (shipGroup == null)
                    {
                        builtObject = method_350(_Game.PlayerEmpire.BuiltObjects.Count, -1);
                    }
                }
            }
            else if (shipGroup_1 != null)
            {
                num2 = _Game.PlayerEmpire.ShipGroups.IndexOf(shipGroup_1);
                shipGroup = method_349(num2, -1);
                if (shipGroup == null)
                {
                    builtObject = method_350(_Game.PlayerEmpire.BuiltObjects.Count, -1);
                    if (builtObject == null)
                    {
                        shipGroup = method_349(_Game.PlayerEmpire.ShipGroups.Count, -1);
                    }
                }
            }
            else
            {
                shipGroup = method_349(_Game.PlayerEmpire.ShipGroups.Count, -1);
                if (shipGroup == null)
                {
                    builtObject = method_350(_Game.PlayerEmpire.BuiltObjects.Count, -1);
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

        private void pnlRuinDetail_CloseButtonClicked(object sender, EventArgs e)
        {
            method_551();
        }

        private void method_550(Ruin ruin_0)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            if (ruin_0 != null)
            {
                pnlRuinDetail.Size = new Size(360, 500);
                pnlRuinDetail.Location = new Point((mainView.Width - pnlRuinDetail.Width) / 2, (mainView.Height - pnlRuinDetail.Height) / 2);
                pnlRuinDetail.DoLayout();
                picRuinDetailImage.Size = new Size(320, 160);
                picRuinDetailImage.Location = new Point((pnlRuinDetail.Width - 15 - picRuinDetailImage.Width) / 2, 10);
                picRuinDetailImage.SizeMode = PictureBoxSizeMode.Zoom;
                picRuinDetailImage.Image = bitmap_2[ruin_0.PictureRef];
                lblRuinDetailName.Font = font_2;
                lblRuinDetailName.Text = ruin_0.Name;
                lblRuinDetailName.Location = new Point((pnlRuinDetail.Width - 15 - lblRuinDetailName.Width) / 2, 190);
                lblRuinDetailAbilities.Font = font_6;
                lblRuinDetailAbilities.Text = _Game.Galaxy.GenerateRuinAbilitiesSummary(ruin_0);
                lblRuinDetailAbilities.MaximumSize = new Size(320, 200);
                lblRuinDetailAbilities.Location = new Point((pnlRuinDetail.Width - 15 - lblRuinDetailAbilities.Width) / 2, 225);
                lblRuinDetailDescription.Font = font_6;
                lblRuinDetailDescription.Location = new Point(10, 225 + lblRuinDetailAbilities.Height + 15);
                lblRuinDetailDescription.MaximumSize = new Size(320, pnlRuinDetail.Height - (lblRuinDetailDescription.Location.Y + 20));
                if (ruin_0.PlayerEmpireEncountered && ruin_0.Type == RuinType.UnlockResearchProject)
                {
                    lblRuinDetailDescription.Text = ruin_0.Description;
                }
                else if (ruin_0.PlayerEmpireEncountered && !_Game.Galaxy.CheckRuinsHaveBenefit(ruin_0, _Game.PlayerEmpire))
                {
                    lblRuinDetailDescription.Text = ruin_0.Description;
                }
                else
                {
                    lblRuinDetailDescription.Text = string.Empty;
                }
            }
            pnlRuinDetail.BringToFront();
            pnlRuinDetail.Visible = true;
        }

        private void method_551()
        {
            pnlRuinDetail.SendToBack();
            pnlRuinDetail.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnColonyShowRuin_Click(object sender, EventArgs e)
        {
            Habitat selectedHabitat = UnlxwvByxj.SelectedHabitat;
            if (selectedHabitat != null && selectedHabitat.Ruin != null)
            {
                method_550(selectedHabitat.Ruin);
            }
        }

        private void method_552(Resource resource_0)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            if (resource_0 != null && !resource_0.IsLuxuryResource)
            {
                pnlResourceComponents.Size = new Size(660, 570);
                pnlResourceComponents.Location = new Point((mainView.Width - pnlResourceComponents.Width) / 2, (mainView.Height - pnlResourceComponents.Height) / 2);
                pnlResourceComponents.HeaderIcon = _uiResourcesBitmaps[resource_0.PictureRef];
                pnlResourceComponents.HeaderTitle = string.Format(TextResolver.GetText("Components that use RESOURCE"), resource_0.Name);
                pnlResourceComponents.DoLayout();
                lblResourceComponentsUsedBy.Visible = false;
                lnkResourceComponentsAboutResource.Text = string.Format(TextResolver.GetText("Learn about X in the Galactopedia"), resource_0.Name) + "...";
                lnkResourceComponentsAboutResource.Tag = resource_0;
                lnkResourceComponentsAboutResource.Location = new Point(10, 10);
                ctlResourceComponents.BringToFront();
                ctlResourceComponents.Size = new Size(623, 463);
                ctlResourceComponents.Location = new Point(10, 30);
                ComponentList components = _Game.Galaxy.ResolveComponentsThatUseResource(resource_0);
                ctlResourceComponents.BindData(components, bitmap_21);
                ctlResourceComponents.Grid.Columns["Picture"].Width = 30;
                ctlResourceComponents.Grid.Columns["Name"].Width = 353;
                ctlResourceComponents.Grid.Columns["Category"].Width = 80;
                ctlResourceComponents.Grid.Columns["Size"].Width = 70;
                ctlResourceComponents.Grid.Columns["TechPoints"].Width = 70;
            }
            pnlResourceComponents.BringToFront();
            pnlResourceComponents.Visible = true;
        }

        private void method_553()
        {
            pnlResourceComponents.SendToBack();
            pnlResourceComponents.Visible = false;
            method_545();
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void pnlResourceComponents_CloseButtonClicked(object sender, EventArgs e)
        {
            method_553();
        }

        private void method_554(object object_7, int int_64)
        {
            DistantWorlds.Types.Component component_ = new DistantWorlds.Types.Component(int_64);
            method_543(component_);
        }

        private void lnkResourceComponentsAboutResource_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Resource resource = (Resource)lnkResourceComponentsAboutResource.Tag;
            string name = resource.Name;
            method_456(name);
        }

        private void method_555(object object_7, int int_64)
        {
            if (int_64 >= 0)
            {
                BuiltObject builtObject = _Game.PlayerEmpire.BuiltObjects.FindBuiltObjectById(int_64);
                if (builtObject == null)
                {
                    _Game.PlayerEmpire.PrivateBuiltObjects.FindBuiltObjectById(int_64);
                }
                if (builtObject != null)
                {
                    method_208(builtObject);
                }
            }
        }

        private void pnlTutorial_MouseMove(object sender, MouseEventArgs e)
        {
            if (bool_17)
            {
                int num = e.X - gyCmRgDujR.X;
                int num2 = e.Y - gyCmRgDujR.Y;
                Point point = PointToClient(pnlTutorial.PointToScreen(new Point(num, num2)));
                int num3 = Math.Max(0, Math.Min(point.X, base.Width - pnlTutorial.Width));
                int num4 = Math.Max(0, Math.Min(point.Y, base.Height - pnlTutorial.Height));
                pnlTutorial.Location = new Point(num3, num4);
            }
        }

        private void pnlTutorial_MouseDown(object sender, MouseEventArgs e)
        {
            if (pnlTutorial.Visible && pnlTutorial.Enabled)
            {
                bool_17 = true;
                gyCmRgDujR = new Point(e.X, e.Y);
            }
        }

        private void pnlTutorial_MouseUp(object sender, MouseEventArgs e)
        {
            bool_17 = false;
        }

        private void btnGameOptionsEmpireSettings_Click(object sender, EventArgs e)
        {
            method_556();
        }

        private void pnlGameOptionsEmpireSettings_CloseButtonClicked(object sender, EventArgs e)
        {
            method_565();
        }

        private void method_556()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
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
            lblGameOptionsEngagementStancePatrol.Font = font_4;
            lblGameOptionsEngagementStanceEscort.Font = font_4;
            lblGameOptionsEngagementStanceAttack.Font = font_4;
            lblGameOptionsEngagementStanceOther.Font = font_4;
            cmbGameOptionsEngagementStancePatrol.Font = font_4;
            cmbGameOptionsEngagementStanceEscort.Font = font_4;
            cmbGameOptionsEngagementStanceAttack.Font = font_4;
            cmbGameOptionsEngagementStanceOther.Font = font_4;
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
            lblGameOptionsEngagementStancePatrolManual.Font = font_4;
            lblGameOptionsEngagementStanceEscortManual.Font = font_4;
            lblGameOptionsEngagementStanceAttackManual.Font = font_4;
            lblGameOptionsEngagementStanceOtherManual.Font = font_4;
            cmbGameOptionsEngagementStancePatrolManual.Font = font_4;
            cmbGameOptionsEngagementStanceEscortManual.Font = font_4;
            cmbGameOptionsEngagementStanceAttackManual.Font = font_4;
            cmbGameOptionsEngagementStanceOtherManual.Font = font_4;
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
            lblGameOptionsFleetAttackRefuel.Font = font_4;
            lblGameOptionsFleetAttackGather.Location = new Point(60, 58);
            lblGameOptionsFleetAttackGather.Font = font_4;
            sldGameOptionsAttackOvermatch.Size = new Size(465, 62);
            sldGameOptionsAttackOvermatch.Font = font_4;
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
            lblGameOptionsEncounterRuins.Font = font_4;
            cmbGameOptionsEncounterRuins.Size = new Size(260, 21);
            cmbGameOptionsEncounterRuins.Font = font_4;
            cmbGameOptionsEncounterRuins.Location = new Point(198, 18);
            lblGameOptionsEncounterAbandonedShipOrBase.Location = new Point(7, 50);
            lblGameOptionsEncounterAbandonedShipOrBase.MaximumSize = new Size(188, 40);
            lblGameOptionsEncounterAbandonedShipOrBase.Font = font_4;
            cmbGameOptionsEncounterAbandonedShipOrBase.Size = new Size(260, 21);
            cmbGameOptionsEncounterAbandonedShipOrBase.Location = new Point(198, 58);
            cmbGameOptionsEncounterAbandonedShipOrBase.Font = font_4;
            chkOptionsNewShipsAutomated.Location = new Point(10, 645);
            chkOptionsNewShipsAutomated.Font = font_4;
            chkOptionsNewShipsAutomated.CheckAlign = ContentAlignment.TopLeft;
            chkOptionsSuppressAllPopups.Location = new Point(10, 670);
            chkOptionsSuppressAllPopups.BringToFront();
            chkOptionsSuppressAllPopups.Font = font_4;
            chkOptionsSuppressAllPopups.CheckAlign = ContentAlignment.TopLeft;
            method_557();
            pnlGameOptionsEmpireSettings.Visible = true;
            pnlGameOptionsEmpireSettings.BringToFront();
        }

        private void method_557()
        {
            if (gameOptions_0 == null)
            {
                return;
            }
            cmbGameOptionsEngagementStancePatrol.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangePatrol);
            cmbGameOptionsEngagementStanceEscort.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeEscort);
            cmbGameOptionsEngagementStanceAttack.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeAttack);
            cmbGameOptionsEngagementStanceOther.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeOther);
            cmbGameOptionsEngagementStancePatrolManual.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangePatrolManual);
            cmbGameOptionsEngagementStanceEscortManual.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeEscortManual);
            cmbGameOptionsEngagementStanceAttackManual.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeAttackManual);
            cmbGameOptionsEngagementStanceOtherManual.SelectedIndex = method_564(_Game.PlayerEmpire.AttackRangeOtherManual);
            sldGameOptionsAttackOvermatch.Value = method_562(_Game.PlayerEmpire.AttackOvermatchFactor);
            numGameOptionsFleetAttackRefuel.Value = method_559(_Game.PlayerEmpire.FleetAttackRefuelPortion);
            numGameOptionsFleetAttackGather.Value = method_559(_Game.PlayerEmpire.FleetAttackGatherPortion);
            cmbGameOptionsEncounterRuins.SelectedIndex = _Game.PlayerEmpire.DiscoveryActionRuin;
            cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex = _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase;
            chkOptionsNewShipsAutomated.Checked = _Game.PlayerEmpire.NewShipsAutomated;
            chkOptionsSuppressAllPopups.Checked = gameOptions_0.SuppressAllPopups;
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

        private void method_558()
        {
            if (gameOptions_0 != null)
            {
                _Game.PlayerEmpire.AttackRangePatrol = method_563(cmbGameOptionsEngagementStancePatrol.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeEscort = method_563(cmbGameOptionsEngagementStanceEscort.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeAttack = method_563(cmbGameOptionsEngagementStanceAttack.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeOther = method_563(cmbGameOptionsEngagementStanceOther.SelectedIndex);
                _Game.PlayerEmpire.AttackRangePatrolManual = method_563(cmbGameOptionsEngagementStancePatrolManual.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeEscortManual = method_563(cmbGameOptionsEngagementStanceEscortManual.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeAttackManual = method_563(cmbGameOptionsEngagementStanceAttackManual.SelectedIndex);
                _Game.PlayerEmpire.AttackRangeOtherManual = method_563(cmbGameOptionsEngagementStanceOtherManual.SelectedIndex);
                _Game.PlayerEmpire.AttackOvermatchFactor = method_561(sldGameOptionsAttackOvermatch.Value);
                _Game.PlayerEmpire.FleetAttackRefuelPortion = method_560(numGameOptionsFleetAttackRefuel.Value);
                _Game.PlayerEmpire.FleetAttackGatherPortion = method_560(numGameOptionsFleetAttackGather.Value);
                gameOptions_0.FleetAttackRefuelPortion = method_560(numGameOptionsFleetAttackRefuel.Value);
                gameOptions_0.FleetAttackGatherPortion = method_560(numGameOptionsFleetAttackGather.Value);
                _Game.PlayerEmpire.DiscoveryActionRuin = cmbGameOptionsEncounterRuins.SelectedIndex;
                _Game.PlayerEmpire.DiscoveryActionAbandonedShipBase = cmbGameOptionsEncounterAbandonedShipOrBase.SelectedIndex;
                _Game.PlayerEmpire.NewShipsAutomated = chkOptionsNewShipsAutomated.Checked;
                gameOptions_0.SuppressAllPopups = chkOptionsSuppressAllPopups.Checked;
            }
        }

        internal decimal method_559(float float_2)
        {
            decimal val = (decimal)(float_2 * 100f);
            return Math.Max(0m, Math.Min(100m, val));
        }

        internal float method_560(decimal decimal_0)
        {
            float val = (float)((double)decimal_0 / 100.0);
            return Math.Max(0f, Math.Min(1f, val));
        }

        private float method_561(int int_64)
        {
            float result = 2f;
            switch (int_64)
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

        private int method_562(float float_2)
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

        private int method_563(int int_64)
        {
            int result = 0;
            switch (int_64)
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

        private int method_564(int int_64)
        {
            int result = -1;
            if (int_64 < 0)
            {
                result = 0;
            }
            else if (int_64 == 0)
            {
                result = 1;
            }
            else if (int_64 >= 0 && int_64 <= 2000)
            {
                result = 2;
            }
            else if (int_64 > 2000 && int_64 <= 48000)
            {
                result = 3;
            }
            return result;
        }

        private void method_565()
        {
            method_558();
            pnlGameOptionsEmpireSettings.SendToBack();
            pnlGameOptionsEmpireSettings.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnGameOptionsShowMessages_Click(object sender, EventArgs e)
        {
            method_566();
        }

        private void pnlGameOptionsMessages_CloseButtonClicked(object sender, EventArgs e)
        {
            method_567();
        }

        private void method_566()
        {
            pnlGameOptionsMessages.Size = new Size(735, 502);
            pnlGameOptionsMessages.Location = new Point((base.Width - pnlGameOptionsMessages.Width) / 2, (base.Height - pnlGameOptionsMessages.Height) / 2);
            pnlGameOptionsMessages.DoLayout();
            grpOptionsPopupMessages.Visible = true;
            grpOptionsScrollingMessages.Visible = true;
            grpOptionsPopupMessages.Font = font_2;
            grpOptionsScrollingMessages.Font = font_2;
            grpOptionsPopupMessages.BringToFront();
            grpOptionsScrollingMessages.BringToFront();
            grpOptionsScrollingMessages.Location = new Point(12, 10);
            grpOptionsPopupMessages.Location = new Point(367, 10);
            grpOptionsScrollingMessages.Size = new Size(340, 412);
            grpOptionsPopupMessages.Size = new Size(340, 412);
            chkOptionsScrollingMessageNewShipBuilt.Font = font_4;
            chkOptionsScrollingMessageRequestWarning.Font = font_4;
            chkOptionsScrollingMessageDiplomacyTreaties.Font = font_4;
            chkOptionsScrollingMessageWarTradeSanctions.Font = font_4;
            chkOptionsScrollingMessageColonyGainLoss.Font = font_4;
            chkOptionsScrollingMessageEmpireMetDestroyed.Font = font_4;
            chkOptionsScrollingMessageResearchBreakthrough.Font = font_4;
            chkOptionsScrollingMessageIntelligenceMissions.Font = font_4;
            chkOptionsScrollingMessageExploration.Font = font_4;
            chkOptionsScrollingMessageShipMissionComplete.Font = font_4;
            chkOptionsScrollingMessageShipNeedsRefuelling.Font = font_4;
            chkOptionsScrollingMessageUnderAttackCivilianShips.Font = font_4;
            chkOptionsScrollingMessageUnderAttackCivilianBases.Font = font_4;
            chkOptionsScrollingMessageUnderAttackExplorationShips.Font = font_4;
            chkOptionsScrollingMessageUnderAttackColonyConstructionShips.Font = font_4;
            chkOptionsScrollingMessageUnderAttackMilitaryShips.Font = font_4;
            chkOptionsScrollingMessageUnderAttackOtherStateBases.Font = font_4;
            chkOptionsScrollingMessageUnderAttackColoniesSpaceports.Font = font_4;
            chkOptionsScrollingMessageConstructionResourceShortage.Font = font_4;
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
            chkOptionsPopupMessageShipBuilt.Font = font_4;
            chkOptionsPopupMessageRequestWarning.Font = font_4;
            chkOptionsPopupMessageDiplomacyTreaties.Font = font_4;
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Font = font_4;
            chkOptionsPopupMessageColonyGainLoss.Font = font_4;
            XwwejKaRdv.Font = font_4;
            chkOptionsPopupMessageResearchBreakthrough.Font = font_4;
            KpfeuWqjpj.Font = font_4;
            chkOptionsPopupMessageExploration.Font = font_4;
            chkOptionsPopupMessageShipMissionComplete.Font = font_4;
            chkOptionsPopupMessageShipNeedsRefuelling.Font = font_4;
            chkOptionsPopupMessageUnderAttackCivilianShips.Font = font_4;
            gcbeGaamXG.Font = font_4;
            chkOptionsPopupMessageUnderAttackExplorationShips.Font = font_4;
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Font = font_4;
            chkOptionsPopupMessageUnderAttackMilitaryShips.Font = font_4;
            ltFewaOdau.Font = font_4;
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Font = font_4;
            chkOptionsPopupMessageConstructionResourceShortage.Font = font_4;
            chkOptionsPopupMessageShipBuilt.Location = new Point(7, 22);
            chkOptionsPopupMessageRequestWarning.Location = new Point(7, 42);
            chkOptionsPopupMessageDiplomacyTreaties.Location = new Point(7, 62);
            chkOptionsPopupMessageDiplomacyWarTradeSanctions.Location = new Point(7, 82);
            chkOptionsPopupMessageColonyGainLoss.Location = new Point(7, 102);
            XwwejKaRdv.Location = new Point(7, 122);
            chkOptionsPopupMessageResearchBreakthrough.Location = new Point(7, 142);
            KpfeuWqjpj.Location = new Point(7, 162);
            chkOptionsPopupMessageExploration.Location = new Point(7, 182);
            chkOptionsPopupMessageShipMissionComplete.Location = new Point(7, 202);
            chkOptionsPopupMessageShipNeedsRefuelling.Location = new Point(7, 222);
            chkOptionsPopupMessageUnderAttackCivilianShips.Location = new Point(7, 242);
            gcbeGaamXG.Location = new Point(7, 262);
            chkOptionsPopupMessageUnderAttackExplorationShips.Location = new Point(7, 282);
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.Location = new Point(7, 302);
            chkOptionsPopupMessageUnderAttackMilitaryShips.Location = new Point(7, 322);
            ltFewaOdau.Location = new Point(7, 342);
            chkOptionsPopupMessageUnderAttackColoniesSpaceports.Location = new Point(7, 362);
            chkOptionsPopupMessageConstructionResourceShortage.Location = new Point(7, 382);
            chkOptionsPopupMessageUnderAttackCivilianShips.BringToFront();
            gcbeGaamXG.BringToFront();
            chkOptionsPopupMessageUnderAttackExplorationShips.BringToFront();
            chkOptionsPopupMessageUnderAttackColonyConstructionShips.BringToFront();
            chkOptionsPopupMessageUnderAttackMilitaryShips.BringToFront();
            ltFewaOdau.BringToFront();
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
            method_417();
            pnlGameOptionsMessages.Visible = true;
            pnlGameOptionsMessages.BringToFront();
        }

        private void method_567()
        {
            method_416();
            pnlGameOptionsMessages.SendToBack();
            pnlGameOptionsMessages.Visible = false;
        }

        private void method_568()
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
            {
                bool_11 = true;
                method_154();
            }
            pnlGameOptionsAdvancedDisplaySettings.Size = new Size(440, 500);
            pnlGameOptionsAdvancedDisplaySettings.Location = new Point(
                (base.Width - pnlGameOptionsAdvancedDisplaySettings.Width) / 2,
                (base.Height - pnlGameOptionsAdvancedDisplaySettings.Height) / 2);
            pnlGameOptionsAdvancedDisplaySettings.DoLayout();
            grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Size = new Size(400, 60);
            grpGameOptionsAdvancedDisplaySettingsMaximumFramerate.Font = font_2;
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Location = new Point(177, 22);
            numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Location = new Point(127, 23);
            lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS.Font = font_4;
            chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Font = font_4;
            chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Location = new Point(8, 22);
            chkOptionsShowSystemNebulae.Location = new Point(15, 87);
            chkOptionsShowSystemNebulae.Font = font_4;
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Font = font_4;
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Size = new Size(400, 52);
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Location = new Point(12, 115);
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.LabelWidth = 160;
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Setup();
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.LinkWidth = 0;
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.LinkText = string.Empty;
            tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.SetLabels(new string[3]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Medium"),
            TextResolver.GetText("High")
            });
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Location = new Point(12, 184);
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Width = 400;
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Height = 231;
            grpGameOptionsAdvancedDisplaySettingsGalaxyIcons.Font = font_2;
            chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Font = font_4;
            chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Font = font_4;
            chkGameOptionsGalaxyDisplayAlwaysPirates.Font = font_4;
            chkGameOptionsGalaxyDisplayCivilianShips.Font = font_4;
            chkGameOptionsGalaxyDisplayColonyShips.Font = font_4;
            chkGameOptionsGalaxyDisplayConstructionShips.Font = font_4;
            chkGameOptionsGalaxyDisplayExplorationShips.Font = font_4;
            chkGameOptionsGalaxyDisplayFleets.Font = font_4;
            chkGameOptionsGalaxyDisplayMilitaryShips.Font = font_4;
            chkGameOptionsGalaxyDisplayOtherBases.Font = font_4;
            chkGameOptionsGalaxyDisplayResupplyShips.Font = font_4;
            chkGameOptionsGalaxyDisplaySpacePorts.Font = font_4;
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
            if (gameOptions_0 != null)
            {
                chkOptionsShowSystemNebulae.Checked = gameOptions_0.ShowSystemNebulae;
                int systemNebulaeDetail = gameOptions_0.SystemNebulaeDetail;
                systemNebulaeDetail = Math.Max(0, Math.Min(2, systemNebulaeDetail));
                tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Value = systemNebulaeDetail;
                if (gameOptions_0.MaximumFramerate <= 0)
                {
                    chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked = true;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = false;
                }
                else
                {
                    chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked = false;
                    int maximumFramerate = gameOptions_0.MaximumFramerate;
                    maximumFramerate = Math.Max((int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Minimum, maximumFramerate);
                    maximumFramerate = Math.Min((int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Maximum, maximumFramerate);
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value = maximumFramerate;
                }
                chkGameOptionsGalaxyDisplayFleets.Checked = gameOptions_0.GalaxyViewDisplayFleets;
                chkGameOptionsGalaxyDisplayResupplyShips.Checked = gameOptions_0.GalaxyViewDisplayResupplyShips;
                chkGameOptionsGalaxyDisplayMilitaryShips.Checked = gameOptions_0.GalaxyViewDisplayMilitaryShips;
                chkGameOptionsGalaxyDisplaySpacePorts.Checked = gameOptions_0.GalaxyViewDisplaySpacePorts;
                chkGameOptionsGalaxyDisplayOtherBases.Checked = gameOptions_0.GalaxyViewDisplayOtherBases;
                chkGameOptionsGalaxyDisplayExplorationShips.Checked = gameOptions_0.GalaxyViewDisplayExplorationShips;
                chkGameOptionsGalaxyDisplayColonyShips.Checked = gameOptions_0.GalaxyViewDisplayColonyShips;
                chkGameOptionsGalaxyDisplayConstructionShips.Checked = gameOptions_0.GalaxyViewDisplayConstructionShips;
                chkGameOptionsGalaxyDisplayCivilianShips.Checked = gameOptions_0.GalaxyViewDisplayCivilianShips;
                chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Checked = gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets;
                chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Checked = gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips;
                chkGameOptionsGalaxyDisplayAlwaysPirates.Checked = gameOptions_0.GalaxyViewDisplayAlwaysPirates;
            }
            pnlGameOptionsAdvancedDisplaySettings.Visible = true;
            pnlGameOptionsAdvancedDisplaySettings.BringToFront();
        }

        private void method_569()
        {
            if (gameOptions_0 != null)
            {
                gameOptions_0.ShowSystemNebulae = chkOptionsShowSystemNebulae.Checked;
                gameOptions_0.SystemNebulaeDetail = tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail.Value;
                SastWuBaXc(gameOptions_0);
                if (chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked)
                {
                    gameOptions_0.MaximumFramerate = -1;
                }
                else
                {
                    gameOptions_0.MaximumFramerate = (int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value;
                }
                gameOptions_0.GalaxyViewDisplayFleets = chkGameOptionsGalaxyDisplayFleets.Checked;
                gameOptions_0.GalaxyViewDisplayResupplyShips = chkGameOptionsGalaxyDisplayResupplyShips.Checked;
                gameOptions_0.GalaxyViewDisplayMilitaryShips = chkGameOptionsGalaxyDisplayMilitaryShips.Checked;
                gameOptions_0.GalaxyViewDisplaySpacePorts = chkGameOptionsGalaxyDisplaySpacePorts.Checked;
                gameOptions_0.GalaxyViewDisplayOtherBases = chkGameOptionsGalaxyDisplayOtherBases.Checked;
                gameOptions_0.GalaxyViewDisplayExplorationShips = chkGameOptionsGalaxyDisplayExplorationShips.Checked;
                gameOptions_0.GalaxyViewDisplayColonyShips = chkGameOptionsGalaxyDisplayColonyShips.Checked;
                gameOptions_0.GalaxyViewDisplayConstructionShips = chkGameOptionsGalaxyDisplayConstructionShips.Checked;
                gameOptions_0.GalaxyViewDisplayCivilianShips = chkGameOptionsGalaxyDisplayCivilianShips.Checked;
                gameOptions_0.GalaxyViewDisplayAlwaysEnemyFleets = chkGameOptionsGalaxyDisplayAlwaysEnemyFleets.Checked;
                gameOptions_0.GalaxyViewDisplayAlwaysEnemyMilitaryShips = chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips.Checked;
                gameOptions_0.GalaxyViewDisplayAlwaysPirates = chkGameOptionsGalaxyDisplayAlwaysPirates.Checked;
            }
            pnlGameOptionsAdvancedDisplaySettings.SendToBack();
            pnlGameOptionsAdvancedDisplaySettings.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void pnlGameOptionsAdvancedDisplaySettings_CloseButtonClicked(object sender, EventArgs e)
        {
            method_569();
        }

        private void chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited_CheckedChanged(object sender, EventArgs e)
        {
            if (gameOptions_0 != null)
            {
                if (chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited.Checked)
                {
                    gameOptions_0.MaximumFramerate = -1;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = false;
                }
                else
                {
                    gameOptions_0.MaximumFramerate = (int)numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Value;
                    numGameOptionsAdvancedDisplaySettingsMaximumFramerate.Enabled = true;
                }
            }
        }

        private void btnGameOptionsAdvancedDisplaySettings_Click(object sender, EventArgs e)
        {
            method_568();
        }
        private void btnHotKeys_Click(object sender, EventArgs e)
        {
            ShowHotKeys();
        }

        private void cmbEmpireSummaryChangeGovernmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_Game != null && _Game.PlayerEmpire != null)
            {
                GovernmentAttributes byName = _Game.Galaxy.Governments.GetByName(cmbEmpireSummaryChangeGovernmentType.SelectedItem.ToString());
                int num = -1;
                if (byName != null)
                {
                    num = byName.GovernmentId;
                }
                if (num != _Game.PlayerEmpire.GovernmentId && num != -1)
                {
                    btnEmpireSummaryChangeGovernment.Text = string.Format(TextResolver.GetText("Have Revolution and switch to GOVERNMENT"), byName.Name);
                    btnEmpireSummaryChangeGovernment.Enabled = true;
                }
                else
                {
                    btnEmpireSummaryChangeGovernment.Text = TextResolver.GetText("Have Revolution and switch government");
                    btnEmpireSummaryChangeGovernment.Enabled = false;
                }
                if (_Game.PlayerEmpire.DominantRace != null && !_Game.PlayerEmpire.DominantRace.CanChangeGovernment)
                {
                    btnEmpireSummaryChangeGovernment.Text = "(" + string.Format(TextResolver.GetText("Race cannot change government"), _Game.PlayerEmpire.DominantRace.Name) + ")";
                    btnEmpireSummaryChangeGovernment.Enabled = false;
                }
                pnlEmpireSummaryColony.Ignite(this, _Game.Galaxy, _Game.PlayerEmpire, num);
            }
            else
            {
                btnEmpireSummaryChangeGovernment.Enabled = true;
                btnEmpireSummaryChangeGovernment.Text = TextResolver.GetText("Have Revolution and switch government");
            }
        }

        private void ctlDesignsList_RowDoubleClick(object sender, EventArgs e)
        {
            btnDesignsEdit_Click(this, new EventArgs());
        }

        private void btnGameSpeedIncrease_Click(object sender, EventArgs e)
        {
            if (_Game != null && _Game.Galaxy != null)
            {
                double timeSpeed = _Game.Galaxy.TimeSpeed;
                timeSpeed *= 2.0;
                timeSpeed = Math.Min(4.0, timeSpeed);
                _Game.Galaxy.ChangeTimeSpeed(timeSpeed);
                btnGameSpeedDecrease.Enabled = true;
                if (timeSpeed >= 4.0)
                {
                    btnGameSpeedIncrease.Enabled = false;
                }
                else
                {
                    btnGameSpeedIncrease.Enabled = true;
                }
                mainView.Focus();
            }
        }

        private void btnGameSpeedDecrease_Click(object sender, EventArgs e)
        {
            if (_Game != null && _Game.Galaxy != null)
            {
                double timeSpeed = _Game.Galaxy.TimeSpeed;
                timeSpeed /= 2.0;
                timeSpeed = Math.Max(0.25, timeSpeed);
                _Game.Galaxy.ChangeTimeSpeed(timeSpeed);
                btnGameSpeedIncrease.Enabled = true;
                if (timeSpeed <= 0.25)
                {
                    btnGameSpeedDecrease.Enabled = false;
                }
                else
                {
                    btnGameSpeedDecrease.Enabled = true;
                }
                mainView.Focus();
            }
        }

        private void method_570(string string_30, string string_31, Bitmap bitmap_225)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow)
            {
                bool_11 = true;
                method_154();
            }
            if (!musicPlayer_1.IsPlaying)
            {
                ArhCaEfBkk();
            }
            pnlStoryEvent.Size = mainView.Size;
            pnlStoryEvent.Location = new Point(0, 0);
            pnlStoryEvent.BackColor = Color.Black;
            pnlStoryEvent.BackgroundImageLayout = ImageLayout.Zoom;
            pnlStoryEvent.BackgroundImage = bitmap_225;
            int num = 20;
            btnStoryEventAction.Visible = false;
            btnStoryEventClose.Size = new Size(260, 33);
            btnStoryEventClose.Location = new Point((mainView.Width - btnStoryEventClose.Width) / 2, mainView.Height - (btnStoryEventClose.Height + 20));
            btnStoryEventClose.Font = font_2;
            btnStoryEventClose.Text = TextResolver.GetText("Close");
            using (Graphics graphics = Graphics.FromImage(bitmap_225))
            {
                Font font = font_1;
                SizeF sizeF = graphics.MeasureString(string_30, font, 800, StringFormat.GenericTypographic);
                lblStoryEventTitle.Size = new Size((int)sizeF.Width + 5, (int)sizeF.Height + 10);
                font = font_7;
                SizeF sizeF2 = graphics.MeasureString(string_31, font, 800, StringFormat.GenericTypographic);
                lblStoryEventText.Size = new Size((int)sizeF2.Width + 5, (int)sizeF2.Height + 10);
                int val = (mainView.Height - ((int)sizeF.Height + 12 + (int)sizeF2.Height + btnStoryEventClose.Height + num)) / 2;
                val = Math.Max(5, val);
                int num2 = val + ((int)sizeF.Height + 12);
                lblStoryEventTitle.Location = new Point((mainView.Width - (int)sizeF.Width) / 2, val);
                lblStoryEventTitle.ForeColor = Color.White;
                lblStoryEventTitle.Font = font_1;
                lblStoryEventTitle.BackColor = Color.Transparent;
                lblStoryEventTitle.Parent = pnlStoryEvent;
                lblStoryEventTitle.BringToFront();
                lblStoryEventText.Location = new Point((mainView.Width - 800) / 2, num2);
                lblStoryEventText.ForeColor = Color.White;
                lblStoryEventText.Font = font;
                lblStoryEventText.BackColor = Color.Transparent;
                lblStoryEventText.Parent = pnlStoryEvent;
                lblStoryEventText.BringToFront();
                lblStoryEventTitle.Text = string_30;
                lblStoryEventText.Text = string_31;
            }
            btnStoryEventClose.BringToFront();
            btnStoryEventAction.BringToFront();
            pnlStoryEvent.Visible = true;
            pnlStoryEvent.BringToFront();
        }

        private void method_571(string string_30, string string_31)
        {
            method_572(string_30, string_31, -1);
        }

        private void method_572(string string_30, string string_31, int int_64)
        {
            bool_9 = true;
            if (_Game.AutoPauseWhenInPopupWindow)
            {
                bool_11 = true;
                method_154();
            }
            if (!musicPlayer_1.IsPlaying)
            {
                ArhCaEfBkk();
            }
            int_61 = int_64;
            pnlStoryEvent.Size = mainView.Size;
            pnlStoryEvent.Location = new Point(0, 0);
            pnlStoryEvent.BackColor = Color.Black;
            pnlStoryEvent.BackgroundImageLayout = ImageLayout.Zoom;
            Bitmap bitmap = bitmap_189;
            if (int_61 >= 2)
            {
                bitmap = int_61 switch
                {
                    0 => method_35("guardians.jpg"),
                    1 => method_35("guardians.jpg"),
                    2 => method_35("guardians.jpg"),
                    3 => method_35("shakturi.jpg"),
                    4 => method_35("guardians.jpg"),
                    _ => bitmap_190,
                };
            }
            pnlStoryEvent.BackgroundImage = bitmap;
            int num = 20;
            int num2 = 260;
            if (int_61 == 4)
            {
                num2 = 360;
                btnStoryEventClose.Size = new Size(360, 50);
                btnStoryEventClose.Location = new Point((mainView.Width - (720 + num)) / 2, mainView.Height - (btnStoryEventClose.Height + num));
                btnStoryEventClose.Font = font_2;
                btnStoryEventClose.Text = TextResolver.GetText("No, we do not need any further help");
                btnStoryEventAction.Visible = true;
                btnStoryEventAction.Size = new Size(360, 50);
                btnStoryEventAction.Location = new Point((mainView.Width - (720 + num)) / 2 + 360 + num, mainView.Height - (btnStoryEventAction.Height + num));
                btnStoryEventAction.Font = font_2;
                btnStoryEventAction.Text = TextResolver.GetText("Yes, our dire situation calls for the use of this superweapon!");
            }
            else if (int_61 == 2)
            {
                num2 = 360;
                btnStoryEventClose.Size = new Size(360, 50);
                btnStoryEventClose.Location = new Point((mainView.Width - (720 + num)) / 2, mainView.Height - (btnStoryEventClose.Height + num));
                btnStoryEventClose.Font = font_2;
                btnStoryEventClose.Text = TextResolver.GetText("No, we do not care about Utopia, and we will not join this alliance");
                btnStoryEventAction.Visible = true;
                btnStoryEventAction.Size = new Size(360, 50);
                btnStoryEventAction.Location = new Point((mainView.Width - (720 + num)) / 2 + 360 + num, mainView.Height - (btnStoryEventAction.Height + num));
                btnStoryEventAction.Font = font_2;
                btnStoryEventAction.Text = TextResolver.GetText("Yes, we will unite to fight the Shakturi!");
            }
            else
            {
                btnStoryEventAction.Visible = false;
                btnStoryEventClose.Size = new Size(num2, 33);
                btnStoryEventClose.Location = new Point((mainView.Width - btnStoryEventClose.Width) / 2, mainView.Height - (btnStoryEventClose.Height + num));
                btnStoryEventClose.Font = font_2;
                btnStoryEventClose.Text = TextResolver.GetText("Close");
            }
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font font = font_1;
                SizeF sizeF = graphics.MeasureString(string_30, font, 800, StringFormat.GenericTypographic);
                lblStoryEventTitle.Size = new Size((int)sizeF.Width + 5, (int)sizeF.Height + 10);
                font = font_7;
                SizeF sizeF2 = graphics.MeasureString(string_31, font, 800, StringFormat.GenericTypographic);
                lblStoryEventText.Size = new Size((int)sizeF2.Width + 5, (int)sizeF2.Height + 10);
                int val = (mainView.Height - ((int)sizeF.Height + 12 + (int)sizeF2.Height + btnStoryEventClose.Height + num)) / 2;
                val = Math.Max(5, val);
                int num3 = val + ((int)sizeF.Height + 12);
                lblStoryEventTitle.Location = new Point((mainView.Width - (int)sizeF.Width) / 2, val);
                lblStoryEventTitle.ForeColor = Color.White;
                lblStoryEventTitle.Font = font_1;
                lblStoryEventTitle.BackColor = Color.Transparent;
                lblStoryEventTitle.Parent = pnlStoryEvent;
                lblStoryEventTitle.BringToFront();
                lblStoryEventText.Location = new Point((mainView.Width - 800) / 2, num3);
                lblStoryEventText.ForeColor = Color.White;
                lblStoryEventText.Font = font;
                lblStoryEventText.BackColor = Color.Transparent;
                lblStoryEventText.Parent = pnlStoryEvent;
                lblStoryEventText.BringToFront();
                lblStoryEventTitle.Text = string_30;
                lblStoryEventText.Text = string_31;
            }
            btnStoryEventClose.BringToFront();
            btnStoryEventAction.BringToFront();
            pnlStoryEvent.Visible = true;
            pnlStoryEvent.BringToFront();
        }

        private void method_573()
        {
            pnlStoryEvent.SendToBack();
            pnlStoryEvent.Visible = false;
            if (!method_470() && bool_11)
            {
                bool_11 = false;
                method_155();
            }
        }

        private void btnStoryEventAction_Click(object sender, EventArgs e)
        {
            if (int_61 == 4)
            {
                BuiltObject object_ = _Game.Galaxy.GenerateDeliverancePlanetDestroyer();
                method_208(object_);
                method_157(object_);
                method_4(1.0);
            }
            else if (int_61 == 2)
            {
                ShipGroup object_2 = _Game.Galaxy.GenerateFreedomAlliance(includePlayer: true, ref _Game);
                method_208(object_2);
                method_157(object_2);
                method_4(1.0);
            }
            method_522();
            method_573();
            Focus();
        }

        private void btnStoryEventClose_Click(object sender, EventArgs e)
        {
            if (int_61 == 2)
            {
                _Game.Galaxy.GenerateFreedomAlliance(includePlayer: false, ref _Game);
                Empire empire = null;
                Empire empire2 = null;
                for (int i = 0; i < _Game.Galaxy.Empires.Count; i++)
                {
                    if (_Game.Galaxy.Empires[i].DominantRace != null && _Game.Galaxy.Empires[i].DominantRace == _Game.Galaxy.ShakturiActualRace)
                    {
                        empire2 = _Game.Galaxy.Empires[i];
                    }
                    if (_Game.Galaxy.Empires[i].DominantRace != null && _Game.Galaxy.Empires[i].DominantRace.Name.ToLower(CultureInfo.InvariantCulture) == "mechanoid")
                    {
                        empire = _Game.Galaxy.Empires[i];
                    }
                }
                if (empire != null && empire2 != null)
                {
                    _Game.Galaxy.GenerateShakturiInvasion(empire2, empire);
                }
                _Game.Galaxy.StoryReturnOfTheShakturiEventLevel = 3;
            }
            method_522();
            method_573();
            Focus();
        }


  }

}