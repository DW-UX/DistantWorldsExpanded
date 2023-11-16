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


        internal void LoadEffectsWeapons(string string_30, string string_31)
        {
            string text = string_30 + "effects\\weapons\\";
            string text2 = string_31 + "effects\\weapons\\";
            List<Task> taskList = new List<Task>();
            GraphicsHelper.DisposeImageArray(bitmap_12);
            GraphicsHelper.DisposeImageArray(bitmap_13);
            GraphicsHelper.DisposeImageArray(edLqkLkgAx);
            GraphicsHelper.DisposeImageArray(bitmap_16);
            if (Directory.Exists(text2))
            {
                string[] files = Directory.GetFiles(text2, "*.png");
                if (files.Length > 0)
                {
                    text = text2;
                }
            }
            string[] files2 = Directory.GetFiles(text, "torpedo_*.png");

            bitmap_12 = new Bitmap[files2.Length];
            for (int i = 0; i < files2.Length; i++)
            {
                int localI = i;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_12[localI] = method_12(text + "torpedo_" + localI + ".png", bool_28: true);
                    bitmap_12[localI].RotateFlip(RotateFlipType.Rotate90FlipNone);
                }));
            }
            string[] files3 = Directory.GetFiles(text, "beam_*.png");
            bitmap_13 = new Bitmap[files3.Length];
            for (int j = 0; j < files3.Length; j++)
            {
                int localJ = j;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_13[localJ] = method_12(text + "beam_" + localJ + ".png", bool_28: true);
                    bitmap_13[localJ].RotateFlip(RotateFlipType.Rotate90FlipNone);
                }));
            }
            string[] files4 = Directory.GetFiles(text, "area_*.png");
            edLqkLkgAx = new Bitmap[files4.Length];
            for (int k = 0; k < files4.Length; k++)
            {
                int localK = k;
                taskList.Add(Task.Run(() =>
                {
                    edLqkLkgAx[localK] = method_12(text + "area_" + localK + ".png", bool_28: true);
                    edLqkLkgAx[localK].RotateFlip(RotateFlipType.Rotate90FlipNone);
                }));
            }
            for (int l = 0; l < 1; l++)
            {
                int localL = l;
                taskList.Add(Task.Run(() => bitmap_14[localL] = method_12(text + "HyperDeny_" + localL + ".png", bool_28: true)));
            }
            for (int m = 0; m < 1; m++)
            {
                int localM = m;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_15[localM] = method_12(text + "AssaultPod_" + localM + ".png", bool_28: true);
                    bitmap_15[localM].RotateFlip(RotateFlipType.Rotate90FlipNone);
                }));
            }
            taskList.Add(Task.Run(() =>
            {
                bitmap_16[0] = method_12(text + "Troop_Infantry_0.png", bool_28: true);
                bitmap_16[0].RotateFlip(RotateFlipType.Rotate90FlipNone);
                bitmap_16[1] = method_12(text + "Troop_Armored_0.png", bool_28: true);
                bitmap_16[1].RotateFlip(RotateFlipType.Rotate90FlipNone);
                bitmap_16[2] = method_12(text + "Troop_Artillery_0.png", bool_28: true);
                bitmap_16[2].RotateFlip(RotateFlipType.Rotate90FlipNone);
                bitmap_16[3] = method_12(text + "Troop_SpecialForces_0.png", bool_28: true);
                bitmap_16[3].RotateFlip(RotateFlipType.Rotate90FlipNone);
            }));
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadEffectsExplosion(string string_30, string string_31, double double_7, bool bool_28)
        {
            string path = string_30 + "effects\\explosions\\";
            string text = string_31 + "effects\\explosions\\";
            if (!bool_28 && !Directory.Exists(text))
            {
                return;
            }
            List<Task> taskList = new List<Task>();
            if (Directory.Exists(text))
            {
                string[] directories = Directory.GetDirectories(text);
                if (directories.Length > 0)
                {
                    path = text;
                }
            }
            double double_8 = double_7 * 1.0;
            double double_9 = double_7 * 0.7;
            string[] directories2 = Directory.GetDirectories(path);
            for (int i = 0; i < directories2.Length; i++)
            {
                string[] files = Directory.GetFiles(directories2[i], "*.png");
                GraphicsHelper.DisposeImageArray(bitmap_19[i]);
                bitmap_19[i] = new Bitmap[files.Length];
                List<string> list = new List<string>();
                list.AddRange(files);
                list.Sort();
                for (int j = 0; j < list.Count; j++)
                {
                    int localI = i;
                    int localJ = j;
                    taskList.Add(Task.Run(() => bitmap_19[localI][localJ] = method_13(list[localJ], bool_28: true, double_8))); ;
                }
            }
            bool flag = true;
            ComputerInfo computerInfo = new ComputerInfo();
            ulong totalPhysicalMemory = computerInfo.TotalPhysicalMemory;
            if (totalPhysicalMemory >= 1610612736L)
            {
                flag = false;
            }
            string path2 = string_30 + "effects\\planetdestroy\\";
            string text2 = string_31 + "effects\\planetdestroy\\";
            if (Directory.Exists(text2))
            {
                string[] files2 = Directory.GetFiles(text2, "*.png");
                if (files2.Length > 0)
                {
                    path2 = text2;
                }
            }
            string[] files3 = Directory.GetFiles(path2, "*.png");
            GraphicsHelper.DisposeImageArray(bitmap_20);
            bitmap_20 = new Bitmap[files3.Length];
            List<string> list2 = new List<string>();
            list2.AddRange(files3);
            list2.Sort();
            for (int k = 0; k < list2.Count; k++)
            {
                int localK = k;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_20[localK] = method_13(list2[localK], bool_28: true, double_9);
                    if (flag && double_7 >= 1.0)
                    {
                        Bitmap bitmap = bitmap_20[localK];
                        bitmap_20[localK] = PrecacheScaledBitmap(bitmap_20[localK], bitmap_20[localK].Width / 2, bitmap_20[localK].Height / 2);
                        bitmap.Dispose();
                    }
                }));
            }
            Task.WaitAll(taskList.ToArray());
            UpdateSplashProgress();
        }

        internal void LoadUiMessages(string string_30, string string_31)
        {
            BaconMain.LoadUiMessages(this, string_30, string_31);
        }

        internal void LoadUiEvents(string string_30, string string_31)
        {
            string string_32 = string_30 + "ui\\events\\";
            string string_33 = string_31 + "ui\\events\\";
            Parallel.Invoke(
                () => bitmap_30[0] = method_10(string_33, string_32, "earthquake.png", bool_28: true),
                () => bitmap_30[1] = method_10(string_33, string_32, "sinkhole.png", bool_28: true),
                () => bitmap_30[2] = method_10(string_33, string_32, "tsunami.png", bool_28: true),
                () => bitmap_30[3] = method_10(string_33, string_32, "sandstorm.png", bool_28: true),
                () => bitmap_30[4] = method_10(string_33, string_32, "blizzard.png", bool_28: true),
                () => bitmap_30[5] = method_10(string_33, string_32, "eruption.png", bool_28: true),
                () => bitmap_30[6] = method_10(string_33, string_32, "plague.png", bool_28: true),
                () => bitmap_30[7] = method_10(string_33, string_32, "economiccrisis.png", bool_28: true)
            );
        }

        internal void LoadEnvLandscapes(string string_30, string string_31)
        {
            string text = string_30 + "environment\\landscapes\\";
            string text2 = string_31 + "environment\\landscapes\\";
            //List<Bitmap> list = new List<Bitmap>();
            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();
            for (int i = 0; i < GalaxyImages.LandscapeImageCountBarrenRock; i++)
            {
                string string_32 = "barrenrock\\landscape_" + i + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_32, bool_28: true)));
            }
            for (int j = 0; j < GalaxyImages.LandscapeImageCountContinental; j++)
            {
                string string_33 = "continental\\landscape_" + j + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_33, bool_28: true)));

            }
            for (int k = 0; k < GalaxyImages.LandscapeImageCountForest; k++)
            {
                string string_34 = "forest\\landscape_" + k + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_34, bool_28: true)));

            }
            for (int l = 0; l < GalaxyImages.LandscapeImageCountFrozenGasGiant; l++)
            {
                string string_35 = "frozengasgiant\\landscape_" + l + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_35, bool_28: true)));

            }
            for (int m = 0; m < GalaxyImages.LandscapeImageCountGasGiant; m++)
            {
                string string_36 = "gasgiant\\landscape_" + m + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_36, bool_28: true)));

            }
            for (int n = 0; n < GalaxyImages.LandscapeImageCountIce; n++)
            {
                string string_37 = "iceglacial\\landscape_" + n + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_37, bool_28: true)));

            }
            for (int num2 = 0; num2 < GalaxyImages.LandscapeImageCountMarshySwamp; num2++)
            {
                string string_38 = "marshyswamp\\landscape_" + num2 + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_38, bool_28: true)));

            }
            for (int num3 = 0; num3 < GalaxyImages.LandscapeImageCountOcean; num3++)
            {
                string string_39 = "ocean\\landscape_" + num3 + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_39, bool_28: true)));

            }
            for (int num4 = 0; num4 < GalaxyImages.LandscapeImageCountDesert; num4++)
            {
                string string_40 = "sandydesert\\landscape_" + num4 + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_40, bool_28: true)));

            }
            for (int num5 = 0; num5 < GalaxyImages.LandscapeImageCountVolcanic; num5++)
            {
                string string_41 = "volcanic\\landscape_" + num5 + ".png";
                taskList.Add(Task.Run(() => method_10(text2, text, string_41, bool_28: true)));

            }
            if (Directory.Exists(text2 + "other\\"))
            {
                string[] files = Directory.GetFiles(text2 + "other", "*.png");
                if (files.Length > 0)
                {
                    for (int num6 = 0; num6 < files.Length; num6++)
                    {
                        int localNum6 = num6;
                        taskList.Add(Task.Run(() => method_12(files[localNum6], bool_28: true)));
                    }
                }
            }
            else if (Directory.Exists(text + "other\\"))
            {
                string[] files2 = Directory.GetFiles(text + "other", "*.png");
                if (files2.Length > 0)
                {
                    for (int num7 = 0; num7 < files2.Length; num7++)
                    {
                        int localNum7 = num7;
                        taskList.Add(Task.Run(() => method_12(files2[localNum7], bool_28: true)));
                    }
                }
            }
            Task.WaitAll(taskList.ToArray());
            bitmap_29 = taskList.Where(x => x.Result != null).Select(x => x.Result).ToArray();
        }

        internal void LoadUiShipsymbols(string string_30, string string_31)
        {
            string string_32 = string_30 + "ui\\shipsymbols\\";
            string string_33 = string_31 + "ui\\shipsymbols\\";
            Parallel.Invoke(
                () => bitmap_110 = method_10(string_33, string_32, "base_galaxy.png", bool_28: true),
                () => bitmap_117 = method_10(string_33, string_32, "base.png", bool_28: true),
                () => bitmap_112 = method_10(string_33, string_32, "construction_galaxy.png", bool_28: true),
                () => bitmap_119 = method_10(string_33, string_32, "construction.png", bool_28: true),
                () => bitmap_113 = method_10(string_33, string_32, "exploration_galaxy.png", bool_28: true),
                () => bitmap_120 = method_10(string_33, string_32, "exploration.png", bool_28: true),
                () => bitmap_125 = method_10(string_33, string_32, "fleet.png", bool_28: true),
                () => VnciZycUss = method_10(string_33, string_32, "freighter_galaxy.png", bool_28: true),
                () => bitmap_122 = method_10(string_33, string_32, "freighter.png", bool_28: true),
                () => bitmap_114 = method_10(string_33, string_32, "military_galaxy.png", bool_28: true),
                () => bitmap_121 = method_10(string_33, string_32, "military.png", bool_28: true),
                () => bitmap_115 = method_10(string_33, string_32, "miningship_galaxy.png", bool_28: true),
                () => bitmap_123 = method_10(string_33, string_32, "miningship.png", bool_28: true),
                () => bitmap_111 = method_10(string_33, string_32, "miningstation_galaxy.png", bool_28: true),
                () => bitmap_118 = method_10(string_33, string_32, "miningstation.png", bool_28: true),
                () => bitmap_116 = method_10(string_33, string_32, "passengership_galaxy.png", bool_28: true),
                () => bitmap_124 = method_10(string_33, string_32, "passengership.png", bool_28: true)
            );
        }

        internal void LoadUiAchievements(string string_30, string string_31)
        {
            string string_32 = string_30 + "ui\\achievements\\";
            string string_33 = string_31 + "ui\\achievements\\";
            bitmap_217 = new Bitmap[55];
            bitmap_218 = new Bitmap[55];
            bitmap_219 = new Bitmap[55];
            bitmap_216 = new Bitmap[1];
            string text = ".png";
            Parallel.Invoke(
                () => bitmap_216[0] = method_10(string_33, string_32, "galaxy.png", bool_28: true),
                () => bitmap_217[0] = method_10(string_33, string_32, "AchieveAllRaceVictoryConditions" + text, bool_28: true),
                () => bitmap_217[1] = method_10(string_33, string_32, "DestroyEnemyMilitaryShips_1" + text, bool_28: true),
                () => bitmap_217[2] = method_10(string_33, string_32, "DestroyEnemyMilitaryShips_2" + text, bool_28: true),
                () => bitmap_217[3] = method_10(string_33, string_32, "DestroyEnemyMilitaryShips_3" + text, bool_28: true),
                () => bitmap_217[4] = method_10(string_33, string_32, "DestroyEnemyCivilianShips_1" + text, bool_28: true),
                () => bitmap_217[5] = method_10(string_33, string_32, "DestroyEnemyCivilianShips_2" + text, bool_28: true),
                () => bitmap_217[6] = method_10(string_33, string_32, "DestroyEnemyCivilianShips_3" + text, bool_28: true),
                () => bitmap_217[7] = method_10(string_33, string_32, "DestroyEnemyTroops_1" + text, bool_28: true),
                () => bitmap_217[8] = method_10(string_33, string_32, "DestroyEnemyTroops_2" + text, bool_28: true),
                () => bitmap_217[9] = method_10(string_33, string_32, "DestroyEnemyTroops_3" + text, bool_28: true),
                () => bitmap_217[10] = method_10(string_33, string_32, "DestroySpaceMonsters_1" + text, bool_28: true),
                () => bitmap_217[11] = method_10(string_33, string_32, "DestroySpaceMonsters_2" + text, bool_28: true),
                () => bitmap_217[12] = method_10(string_33, string_32, "DestroySpaceMonsters_3" + text, bool_28: true),
                () => bitmap_217[13] = method_10(string_33, string_32, "DestroySilverMists_1" + text, bool_28: true),
                () => bitmap_217[14] = method_10(string_33, string_32, "DestroySilverMists_2" + text, bool_28: true),
                () => bitmap_217[15] = method_10(string_33, string_32, "DestroySilverMists_3" + text, bool_28: true),
                () => bitmap_217[16] = method_10(string_33, string_32, "ConquerEnemyColonies_1" + text, bool_28: true),
                () => bitmap_217[17] = method_10(string_33, string_32, "ConquerEnemyColonies_2" + text, bool_28: true),
                () => bitmap_217[18] = method_10(string_33, string_32, "ConquerEnemyColonies_3" + text, bool_28: true),
                () => bitmap_217[19] = method_10(string_33, string_32, "StartWars" + text, bool_28: true),
                () => bitmap_217[20] = method_10(string_33, string_32, "BreakTreaties" + text, bool_28: true),
                () => bitmap_217[21] = method_10(string_33, string_32, "EliminateEnemyCharacters_1" + text, bool_28: true),
                () => bitmap_217[22] = method_10(string_33, string_32, "EliminateEnemyCharacters_2" + text, bool_28: true),
                () => bitmap_217[23] = method_10(string_33, string_32, "EliminateEnemyCharacters_3" + text, bool_28: true),
                () => bitmap_217[24] = method_10(string_33, string_32, "EliminateEnemyEmpires_1" + text, bool_28: true),
                () => bitmap_217[25] = method_10(string_33, string_32, "EliminateEnemyEmpires_2" + text, bool_28: true),
                () => bitmap_217[26] = method_10(string_33, string_32, "EliminateEnemyEmpires_3" + text, bool_28: true),
                () => bitmap_217[27] = method_10(string_33, string_32, "TradeIncome" + text, bool_28: true),
                () => bitmap_217[28] = method_10(string_33, string_32, "MineResources" + text, bool_28: true),
                () => bitmap_217[29] = method_10(string_33, string_32, "CaptureEnemyShips_1" + text, bool_28: true),
                () => bitmap_217[30] = method_10(string_33, string_32, "CaptureEnemyShips_2" + text, bool_28: true),
                () => bitmap_217[31] = method_10(string_33, string_32, "CaptureEnemyShips_3" + text, bool_28: true),
                () => bitmap_217[32] = method_10(string_33, string_32, "EliminatePirateFactions_1" + text, bool_28: true),
                () => bitmap_217[33] = method_10(string_33, string_32, "EliminatePirateFactions_2" + text, bool_28: true),
                () => bitmap_217[34] = method_10(string_33, string_32, "EliminatePirateFactions_3" + text, bool_28: true),
                () => bitmap_217[35] = method_10(string_33, string_32, "TimeAtWar" + text, bool_28: true),
                () => bitmap_217[36] = method_10(string_33, string_32, "TimeAtPeace" + text, bool_28: true),
                () => bitmap_217[37] = method_10(string_33, string_32, "SuccessfulRaids_1" + text, bool_28: true),
                () => bitmap_217[38] = method_10(string_33, string_32, "SuccessfulRaids_2" + text, bool_28: true),
                () => bitmap_217[39] = method_10(string_33, string_32, "SuccessfulRaids_3" + text, bool_28: true),
                () => bitmap_217[40] = method_10(string_33, string_32, "GovernmentWayOfDarkness" + text, bool_28: true),
                () => bitmap_217[41] = method_10(string_33, string_32, "GovernmentWayOfAncients" + text, bool_28: true),
                () => bitmap_217[42] = method_10(string_33, string_32, "EmpireSplits" + text, bool_28: true),
                () => bitmap_217[43] = method_10(string_33, string_32, "BuildWonders_1" + text, bool_28: true),
                () => bitmap_217[44] = method_10(string_33, string_32, "BuildWonders_2" + text, bool_28: true),
                () => bitmap_217[45] = method_10(string_33, string_32, "BuildWonders_3" + text, bool_28: true),
                () => bitmap_217[46] = method_10(string_33, string_32, "OwnAnOperationalPlanetDestroyer" + text, bool_28: true),
                () => bitmap_217[47] = method_10(string_33, string_32, "JoinTheFreedomAlliance" + text, bool_28: true),
                () => bitmap_217[48] = method_10(string_33, string_32, "JoinTheShakturi" + text, bool_28: true),
                () => bitmap_217[49] = method_10(string_33, string_32, "DefeatAncients" + text, bool_28: true),
                () => bitmap_217[50] = method_10(string_33, string_32, "DefeatShakturi" + text, bool_28: true),
                () => bitmap_217[51] = method_10(string_33, string_32, "DefeatLegendaryPirates" + text, bool_28: true),
                () => bitmap_217[52] = method_10(string_33, string_32, "SuccessfulIntelligenceMissions_1" + text, bool_28: true),
                () => bitmap_217[53] = method_10(string_33, string_32, "SuccessfulIntelligenceMissions_2" + text, bool_28: true),
                () => bitmap_217[54] = method_10(string_33, string_32, "SuccessfulIntelligenceMissions_3" + text, bool_28: true)
            );
            bitmap_218 = new Bitmap[bitmap_217.Length];
            bitmap_219 = new Bitmap[bitmap_217.Length];
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < bitmap_217.Length; i++)
            {
                int localI = i;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_218[localI] = GraphicsHelper.ScaleImage(bitmap_217[localI], GameSummaryPanel.MedalSmallSize.Width, GameSummaryPanel.MedalSmallSize.Height, 1f);
                    bitmap_219[localI] = GraphicsHelper.ScaleImage(bitmap_217[localI], GameSummaryPanel.MedalVerySmallSize.Width, GameSummaryPanel.MedalVerySmallSize.Height, 1f);
                    Bitmap bitmap = bitmap_217[localI];
                    bitmap_217[localI] = GraphicsHelper.ScaleImage(bitmap_217[localI], GameSummaryPanel.MedalLargeSize.Width, GameSummaryPanel.MedalLargeSize.Height, 1f);
                    bitmap.Dispose();
                }));
            }
            Task.WaitAll(taskList.ToArray());
            Parallel.Invoke(
                () => bitmap_220 = method_10(string_33, string_32, "left.png", bool_28: true),
                () => bitmap_221 = method_10(string_33, string_32, "right.png", bool_28: true),
                () => bitmap_222 = method_10(string_33, string_32, "up.png", bool_28: true),
                () => bitmap_223 = method_10(string_33, string_32, "down.png", bool_28: true)
            );
        }

        internal void LoadUiChromeButtons(string string_30, string string_31)
        {
            string string_32 = Application.StartupPath + "\\images\\ui\\chrome\\";
            string text = Application.StartupPath + "\\Customization\\" + string_30 + "\\images\\ui\\chrome\\";
            string string_33 = string.Empty;
            if (!string.IsNullOrEmpty(string_31))
            {
                string_33 = text + string_31 + "\\";
            }
            picGameEditor.Image = method_55(string_32, text, string_33, "gameEditorButton.png");
            picGameEditorEnterPassword.Image = bitmap_145;
            tbtnColonies.Image = method_55(string_32, text, string_33, "coloniesButton.png");
            tbtnBuiltObjects.Image = method_55(string_32, text, string_33, "shipsAndBasesButton.png");
            tbtnEmpires.Image = method_55(string_32, text, string_33, "diplomacyButton.png");
            tbtnTroops.Image = method_55(string_32, text, string_33, "troopsButton.png");
            tbtnGalaxyMap.Image = method_55(string_32, text, string_33, "galaxyMapButton.png");
            tbtnShipGroups.Image = method_55(string_32, text, string_33, "fleetsButton.png");
            tbtnConstructionYards.Image = method_55(string_32, text, string_33, "constructionYardsButton.png");
            tbtnIntelligenceAgents.Image = method_55(string_32, text, string_33, "charactersButton.png");
            tbtnDesigns.Image = method_55(string_32, text, string_33, "designsButton.png");
            btnExpansionPlanner.Image = method_55(string_32, text, string_33, "expansionPlannerButton.png");
            btnEmpirePolicy.Image = method_55(string_32, text, string_33, "empirePolicyButton.png");
            btnEmpireGraphs.Image = method_55(string_32, text, string_33, "empireGraphsButton.png");
            btnGameEditor.Image = method_55(string_32, text, string_33, "gameEditorButton.png");
            btnBuildOrder.Image = method_55(string_32, text, string_33, "buildButton.png");
            btnGalacticHistory.Image = method_55(string_32, text, string_33, "galacticHistoryButton.png");
            btnHistoryMessages.Image = method_55(string_32, text, string_33, "messagesButton.png");
            btnGameMenu.Image = method_55(string_32, text, string_33, "gameOptionsButton.png");
            if (_Game != null && _Game.Galaxy != null && _Game.Galaxy.TimeState == GalaxyTimeState.Paused)
            {
                btnPlayPause.Image = bitmap_46;
            }
            else
            {
                btnPlayPause.Image = bitmap_45;
            }
            btnHelp.Image = method_55(string_32, text, string_33, "galactopediaButton.png");
            btnEncyclopediaHome.Image = method_55(string_32, text, string_33, "galactopediaHome.png");
            btnEncyclopediaForward.Image = bitmap_156;
            btnEncyclopediaBack.Image = bitmap_157;
            pnlColonyInfo.HeaderIcon = bitmap_37;
            pnlBuiltObjectInfo.HeaderIcon = bitmap_146;
            pnlEmpireInfo.HeaderIcon = bitmap_148;
            pnlTroopInfo.HeaderIcon = bitmap_149;
            CaLkaMyrMQ.HeaderIcon = bitmap_150;
            pnlShipGroupInfo.HeaderIcon = bitmap_147;
            kYdDyYeMls.HeaderIcon = bitmap_51;
            pnlDesigns.HeaderIcon = bitmap_151;
            pnlExpansionPlanner.HeaderIcon = bitmap_143;
            pnlEmpirePolicy.HeaderIcon = bitmap_152;
            vHfFsoqMev.HeaderIcon = bitmap_153;
            pnlMessageHistory.HeaderIcon = bitmap_154;
            pnlEncyclopedia.HeaderIcon = eqliPoFqeq;
            pnlBuildOrder.HeaderIcon = bitmap_72;
            pnlColonyInfo.DoLayout();
            pnlBuiltObjectInfo.DoLayout();
            pnlEmpireInfo.DoLayout();
            pnlTroopInfo.DoLayout();
            CaLkaMyrMQ.DoLayout();
            pnlShipGroupInfo.DoLayout();
            kYdDyYeMls.DoLayout();
            pnlDesigns.DoLayout();
            pnlExpansionPlanner.DoLayout();
            pnlEmpirePolicy.DoLayout();
            vHfFsoqMev.DoLayout();
            pnlMessageHistory.DoLayout();
            pnlEncyclopedia.DoLayout();
            pnlBuildOrder.DoLayout();
            btnCycleBases.Image = method_55(string_32, text, string_33, "cycleBases.png");
            btnCycleBasesBack.Image = method_55(string_32, text, string_33, "cycleBasesBack.png");
            btnCycleColonies.Image = method_55(string_32, text, string_33, "cycleColonies.png");
            btnCycleColoniesBack.Image = method_55(string_32, text, string_33, "cycleColoniesBack.png");
            btnCycleConstruction.Image = method_55(string_32, text, string_33, "cycleConstruction.png");
            btnCycleConstructionBack.Image = method_55(string_32, text, string_33, "cycleConstructionBack.png");
            btnCycleIdleShips.Image = method_55(string_32, text, string_33, "cycleIdleShips.png");
            btnCycleIdleShipsBack.Image = method_55(string_32, text, string_33, "cycleIdleShipsBack.png");
            btnCycleMilitary.Image = method_55(string_32, text, string_33, "cycleMilitary.png");
            btnCycleMilitaryBack.Image = method_55(string_32, text, string_33, "cycleMilitaryBack.png");
            btnCycleOther.Image = method_55(string_32, text, string_33, "cycleOther.png");
            btnCycleOtherBack.Image = method_55(string_32, text, string_33, "cycleOtherBack.png");
            btnCycleShipGroups.Image = method_55(string_32, text, string_33, "cycleFleets.png");
            btnCycleShipGroupsBack.Image = method_55(string_32, text, string_33, "cycleFleetsBack.png");
            btnCycleShipStance.Image = method_55(string_32, text, string_33, "shipStance.png");
            btnLockView.Image = method_55(string_32, text, string_33, "lockView.png");
            btnSelectNearestMilitary.Image = method_55(string_32, text, string_33, "nearestMilitary.png");
            btnSelectionBack.Image = bitmap_157;
            btnSelectionForward.Image = bitmap_156;
            btnSelectionPanelSize.Image = method_55(string_32, text, string_33, "selectionPanelSize.png");
            btnZoomColony.Image = method_55(string_32, text, string_33, "zoomColony.png");
            btnZoomIn.Image = method_55(string_32, text, string_33, "zoomIn.png");
            btnZoomOut.Image = method_55(string_32, text, string_33, "zoomOut.png");
            btnZoomRegion.Image = method_55(string_32, text, string_33, "zoomRegion.png");
            jQaYpdpkDs.Image = method_55(string_32, text, string_33, "zoomSector.png");
            btnZoomSelection.Image = method_55(string_32, text, string_33, "zoomSelection.png");
            btnZoomSystem.Image = method_55(string_32, text, string_33, "zoomSystem.png");
            btnEncyclopediaBack.Image = bitmap_157;
            btnEncyclopediaForward.Image = bitmap_156;
            btnEncyclopediaHome.Image = method_55(string_32, text, string_33, "galactopediaHome.png");
        }

        private Bitmap method_54(string string_30, string string_31, string string_32)
        {
            Bitmap result = null;
            if (File.Exists(string_31 + string_32))
            {
                result = method_12(string_31 + string_32, bool_28: true);
            }
            else if (File.Exists(string_30 + string_32))
            {
                result = method_12(string_30 + string_32, bool_28: true);
            }
            return result;
        }

        private Bitmap method_55(string string_30, string string_31, string string_32, string string_33)
        {
            Bitmap result = null;
            if (File.Exists(string_32 + string_33))
            {
                result = method_12(string_32 + string_33, bool_28: true);
            }
            else if (File.Exists(string_31 + string_33))
            {
                result = method_12(string_31 + string_33, bool_28: true);
            }
            else if (File.Exists(string_30 + string_33))
            {
                result = method_12(string_30 + string_33, bool_28: true);
            }
            return result;
        }

        internal void method_56(string string_30, string string_31, Race race_1)
        {
            string text = string.Empty;
            if (race_1 != null)
            {
                text = race_1.Name;
            }
            bool flag = false;
            if (string.IsNullOrEmpty(string_27))
            {
                if (!string.IsNullOrEmpty(text))
                {
                    string path = Application.StartupPath + "\\Customization\\" + string_31 + "\\images\\ui\\chrome\\" + text + "\\";
                    if (Directory.Exists(path))
                    {
                        flag = true;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(text))
            {
                if (text != string_27)
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                LoadUiChromeForRace(string_30, string_31, text);
                mainView.ResetRendering();
                method_73(string_31, text);
                string_27 = text;
            }
        }

        internal void LoadUiChrome(string string_30, string string_31)
        {
            LoadUiChromeForRace(string_30, string_31, string.Empty);
        }

        internal void LoadUiChromeForRace(string string_30, string string_31, string string_32)
        {
            string text = string_30 + "ui\\chrome\\";
            string text2 = string.Empty;
            string string_33 = string.Empty;
            if (!string.IsNullOrEmpty(string_31))
            {
                text2 = Application.StartupPath + "\\customization\\" + string_31 + "\\images\\ui\\chrome\\";
                if (!string.IsNullOrEmpty(string_32))
                {
                    string_33 = text2 + string_32 + "\\";
                }
            }
            Parallel.Invoke(
                () => bitmap_33 = method_11(string_33, text2, text, "happy.png", bool_28: true),
                () => bitmap_34 = method_11(string_33, text2, text, "neutral.png", bool_28: true),
                () => bitmap_35 = method_11(string_33, text2, text, "sad.png", bool_28: true),
                () => bitmap_36 = method_11(string_33, text2, text, "angry.png", bool_28: true),
                () => bitmap_32 = method_11(string_33, text2, text, "developmentLevel.png", bool_28: true),
                () => bitmap_37 = method_11(string_33, text2, text, "colony.png", bool_28: true),
                () => bitmap_38 = method_11(string_33, text2, text, "firepower.png", bool_28: true),
                () => bitmap_43 = method_11(string_33, text2, text, "fleetLeader.png", bool_28: true),
                () => bitmap_44 = method_11(string_33, text2, text, "capital.png", bool_28: true),
                () => bitmap_55 = method_11(string_33, text2, text, "refuel.png", bool_28: true),
                () => bitmap_56 = method_11(string_33, text2, text, "scrapbase.png", bool_28: true),
                () => bitmap_45 = method_11(string_33, text2, text, "pauseresume_Play.png", bool_28: true),
                () => bitmap_46 = method_11(string_33, text2, text, "pauseresume_Pause.png", bool_28: true),
                () => bitmap_47 = method_10(text2, text, "galaxy.png", bool_28: true),
                () => bitmap_48 = method_11(string_33, text2, text, "treaty.png", bool_28: true),
                () => bitmap_189 = method_10(text2, text, "storyEvent.jpg", bool_28: true),
                () => bitmap_190 = method_10(text2, text, "storyMessage.jpg", bool_28: true),
                () => bitmap_49 = method_11(string_33, text2, text, "pirateflag.png", bool_28: true),
                () => bitmap_50 = method_11(string_33, text2, text, "pirateflag_small.png", bool_28: true),
                () => bitmap_51 = method_11(string_33, text2, text, "characters.png", bool_28: true),
                () => bitmap_52 = method_11(string_33, text2, text, "money.png", bool_28: true),
                () => bitmap_53 = method_11(string_33, text2, text, "automate.png", bool_28: true),
                () => bitmap_54 = method_11(string_33, text2, text, "unautomate.png", bool_28: true),
                () => bitmap_57 = method_11(string_33, text2, text, "emergency.png", bool_28: true),
                () => bitmap_58 = method_11(string_33, text2, text, "fighters.png", bool_28: true),
                () => bitmap_59 = method_11(string_33, text2, text, "launchfighters.png", bool_28: true),
                () => bitmap_60 = method_11(string_33, text2, text, "launchbombers.png", bool_28: true),
                () => bitmap_61 = method_11(string_33, text2, text, "retrievefighters.png", bool_28: true),
                () => bitmap_62 = method_11(string_33, text2, text, "retrievebombers.png", bool_28: true),
                () => bitmap_63 = method_11(string_33, text2, text, "upgradefighters.png", bool_28: true),
                () => bitmap_64 = method_11(string_33, text2, text, "joinfleet.png", bool_28: true),
                () => bitmap_65 = method_11(string_33, text2, text, "leavefleet.png", bool_28: true),
                () => bitmap_66 = method_11(string_33, text2, text, "retrofitship.png", bool_28: true),
                () => bitmap_67 = method_11(string_33, text2, text, "retrofitbase.png", bool_28: true),
                () => bitmap_68 = method_11(string_33, text2, text, "returntotop.png", bool_28: true),
                () => bitmap_69 = method_11(string_33, text2, text, "buildfighter.png", bool_28: true),
                () => bitmap_70 = method_11(string_33, text2, text, "buildbomber.png", bool_28: true),
                () => bitmap_71 = method_11(string_33, text2, text, "colonize.png", bool_28: true),
                () => bitmap_72 = method_11(string_33, text2, text, "build.png", bool_28: true),
                () => bitmap_73 = method_11(string_33, text2, text, "construction.png", bool_28: true),
                () => bitmap_74 = method_11(string_33, text2, text, "loadtroops.png", bool_28: true),
                () => bitmap_75 = method_11(string_33, text2, text, "stop.png", bool_28: true),
                () => bitmap_76 = method_11(string_33, text2, text, "newfleet.png", bool_28: true),
                () => bitmap_77 = method_11(string_33, text2, text, "scrapfighter.png", bool_28: true),
                () => bitmap_83 = method_11(string_33, text2, text, "research.png", bool_28: true),
                () => bitmap_84 = method_11(string_33, text2, text, "advisorsuggestion.png", bool_28: true),
                () => bitmap_85 = method_11(string_33, text2, text, "bombard.png", bool_28: true),
                () => bitmap_86 = method_11(string_33, text2, text, "exclamation.png", bool_28: true),
                () => bitmap_87 = method_11(string_33, text2, text, "raid.png", bool_28: true),
                () => bitmap_88 = method_11(string_33, text2, text, "assault.png", bool_28: true),
                () => bitmap_78 = method_11(string_33, text2, text, "UpArrow.png", bool_28: true),
                () => bitmap_79 = method_11(string_33, text2, text, "ScrollUpArrow.png", bool_28: true),
                () => bitmap_80 = method_11(string_33, text2, text, "ScrollDownArrow.png", bool_28: true),
                () => bitmap_91 = method_10(text2, text, "blank.png", bool_28: true),
                () => bitmap_31 = method_11(string_33, text2, text, "panelframe.png", bool_28: true),
                () => bitmap_136 = method_11(string_33, text2, text, "asteroid.png", bool_28: true),
                () => bitmap_137 = method_11(string_33, text2, text, "asteroidField.png", bool_28: true),
                () => bitmap_135 = method_11(string_33, text2, text, "debrisField.png", bool_28: true),
                () => bitmap_138 = method_11(string_33, text2, text, "eraseitem.png", bool_28: true),
                () => bitmap_139 = method_11(string_33, text2, text, "erasecolony.png", bool_28: true),
                () => bitmap_140 = method_11(string_33, text2, text, "erasepopulation.png", bool_28: true),
                () => bitmap_141 = method_11(string_33, text2, text, "eraseruins.png", bool_28: true),
                () => bitmap_142 = method_11(string_33, text2, text, "eraseasteroidfield.png", bool_28: true),
                () => bitmap_81 = method_11(string_33, text2, text, "mine.png", bool_28: true),
                () => bitmap_82 = method_11(string_33, text2, text, "attack.png", bool_28: true),
                () => bitmap_126 = method_11(string_33, text2, text, "fleetAttackPosture.png", bool_28: true),
                () => bitmap_127 = method_11(string_33, text2, text, "fleetDefendPosture.png", bool_28: true),
                () => bitmap_128 = method_11(string_33, text2, text, "fleetRangeTarget.png", bool_28: true),
                () => bitmap_129 = method_11(string_33, text2, text, "fleetRangeSystem.png", bool_28: true),
                () => bitmap_130 = method_11(string_33, text2, text, "fleetRangeArea.png", bool_28: true),
                () => bitmap_131 = method_11(string_33, text2, text, "fleetRangeSector.png", bool_28: true),
                () => bitmap_132 = method_11(string_33, text2, text, "fleetRangeAny.png", bool_28: true),
                () => bitmap_133 = method_11(string_33, text2, text, "fleetAttackPoint.png", bool_28: true),
                () => bitmap_134 = method_11(string_33, text2, text, "fleetHomeBase.png", bool_28: true),
                () => bitmap_92 = method_11(string_33, text2, text, "research_small.png", bool_28: true),
                () => bitmap_93 = method_11(string_33, text2, text, "scenery.png", bool_28: true),
                () => bitmap_94 = method_11(string_33, text2, text, "territory.png", bool_28: true),
                () => bitmap_95 = method_11(string_33, text2, text, "longrangescanner.png", bool_28: true),
                () => bitmap_96 = method_11(string_33, text2, text, "travelvectorMilitary.png", bool_28: true),
                () => bitmap_97 = method_11(string_33, text2, text, "travelvectorCivilian.png", bool_28: true),
                () => bitmap_98 = method_11(string_33, text2, text, "fleetposture.png", bool_28: true),
                () => bitmap_99 = method_11(string_33, text2, text, "civilianfade.png", bool_28: true),
                () => bitmap_89 = method_11(string_33, text2, text, "warpjump_large.png", bool_28: true),
                () => bitmap_90 = method_11(string_33, text2, text, "colonization_large.png", bool_28: true),
                () => bitmap_100 = method_11(string_33, text2, text, "pirateMissionAttack.png", bool_28: true),
                () => bitmap_101 = method_11(string_33, text2, text, "pirateMissionDefend.png", bool_28: true),
                () => bitmap_102 = method_11(string_33, text2, text, "pirateMissionSmuggle.png", bool_28: true),
                () => bitmap_103 = method_11(string_33, text2, text, "Space.png", bool_28: true),
                () => bitmap_105 = method_10(text2, text, "arrowhead.png", bool_28: true),
                () => bitmap_104 = method_11(string_33, text2, text, "assaultpod_landing.png", bool_28: true),
                () => bitmap_106 = method_10(text2, text, "playstyle_pirateshadows.png", bool_28: true),
                () => bitmap_107 = method_10(text2, text, "playstyle_normalshadows.png", bool_28: true),
                () => bitmap_108 = method_10(text2, text, "playstyle_pirateclassic.png", bool_28: true),
                () => bitmap_109 = method_10(text2, text, "playstyle_normalclassic.png", bool_28: true),
                () => bitmap_3[0] = method_11(string_33, text2, text, "characterRole_Leader.png", bool_28: true),
                () => bitmap_3[1] = method_11(string_33, text2, text, "characterRole_Ambassador.png", bool_28: true),
                () => bitmap_3[2] = method_11(string_33, text2, text, "characterRole_ColonyGovernor.png", bool_28: true),
                () => bitmap_3[3] = method_11(string_33, text2, text, "characterRole_FleetAdmiral.png", bool_28: true),
                () => bitmap_3[4] = method_11(string_33, text2, text, "characterRole_TroopGeneral.png", bool_28: true),
                () => bitmap_3[5] = method_11(string_33, text2, text, "characterRole_IntelligenceAgent.png", bool_28: true),
                () => bitmap_3[6] = method_11(string_33, text2, text, "characterRole_Scientist.png", bool_28: true),
                () => bitmap_3[7] = method_11(string_33, text2, text, "characterRole_PirateLeader.png", bool_28: true),
                () => bitmap_3[8] = method_11(string_33, text2, text, "characterRole_ShipCaptain.png", bool_28: true),
                () => bitmap_143 = method_11(string_33, text2, text, "expansionPlanner.png", bool_28: true),
                () => bitmap_144 = method_11(string_33, text2, text, "gameEditor.png", bool_28: true),
                () => bitmap_145 = method_11(string_33, text2, text, "key.png", bool_28: true),
                () => bitmap_146 = method_11(string_33, text2, text, "shipsAndBases.png", bool_28: true),
                () => bitmap_147 = method_11(string_33, text2, text, "fleets.png", bool_28: true),
                () => bitmap_148 = method_11(string_33, text2, text, "diplomacy.png", bool_28: true),
                () => bitmap_149 = method_11(string_33, text2, text, "troops.png", bool_28: true),
                () => bitmap_150 = method_11(string_33, text2, text, "galaxyMap.png", bool_28: true),
                () => bitmap_151 = method_11(string_33, text2, text, "designs.png", bool_28: true),
                () => bitmap_152 = method_11(string_33, text2, text, "empirePolicy.png", bool_28: true),
                () => bitmap_153 = method_11(string_33, text2, text, "empireGraphs.png", bool_28: true),
                () => bitmap_154 = method_11(string_33, text2, text, "messages.png", bool_28: true),
                () => bitmap_155 = method_11(string_33, text2, text, "galacticHistory.png", bool_28: true),
                () => bitmap_156 = method_11(string_33, text2, text, "forward.png", bool_28: true),
                () => bitmap_157 = method_11(string_33, text2, text, "back.png", bool_28: true),
                () => eqliPoFqeq = method_11(string_33, text2, text, "galactopedia.png", bool_28: true),
                () => bitmap_158 = method_11(string_33, text2, text, "gameOptions.png", bool_28: true),
                () => bitmap_159 = method_10(text2, text, "Menu_Tutorials_Active.png", bool_28: true),
                () => bitmap_160 = method_10(text2, text, "Menu_Tutorials_Inactive.png", bool_28: true),
                () => bitmap_161 = method_10(text2, text, "Menu_StartNewGame_Active.png", bool_28: true),
                () => bitmap_162 = method_10(text2, text, "Menu_StartNewGame_Inactive.png", bool_28: true),
                () => bitmap_163 = method_10(text2, text, "Menu_LoadGame_Active.png", bool_28: true),
                () => bitmap_164 = method_10(text2, text, "Menu_LoadGame_Inactive.png", bool_28: true),
                () => bitmap_165 = method_10(text2, text, "Menu_Options_Active.png", bool_28: true),
                () => bitmap_166 = method_10(text2, text, "Menu_Options_Inactive.png", bool_28: true),
                () => bitmap_167 = method_10(text2, text, "Menu_ChangeTheme_Active.png", bool_28: true),
                () => bitmap_168 = method_10(text2, text, "Menu_ChangeTheme_Inactive.png", bool_28: true),
                () => bitmap_169 = method_10(text2, text, "Menu_Exit_Active.png", bool_28: true),
                () => bitmap_170 = method_10(text2, text, "Menu_Exit_Inactive.png", bool_28: true),
                () => bitmap_171 = method_10(text2, text, "Menu_Galactopedia_Active.png", bool_28: true),
                () => bitmap_172 = method_10(text2, text, "Menu_Galactopedia_Inactive.png", bool_28: true),
                () => iEycGdoMqb = method_10(text2, text, "Menu_Credits_Active.png", bool_28: true),
                () => bitmap_173 = method_10(text2, text, "Menu_Credits_Inactive.png", bool_28: true),
                () => bitmap_174 = method_10(text2, text, "Menu_CheckForUpdates_Active.png", bool_28: true),
                () => bitmap_175 = method_10(text2, text, "Menu_CheckForUpdates_Inactive.png", bool_28: true)
            );
        }

        internal void LoadUiCursors(string string_30, string string_31)
        {
            string string_32 = string_30 + "ui\\cursors\\";
            string string_33 = string_31 + "ui\\cursors\\";
            List<Task> taskList = new List<Task>();
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap = method_10(string_33, string_32, "default.png", bool_28: true);
                cursor_0 = new Cursor(bitmap.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap2 = method_10(string_33, string_32, "moveToOrbit.png", bool_28: true);
                cursor_1 = new Cursor(bitmap2.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap3 = method_10(string_33, string_32, "attack.png", bool_28: true);
                cursor_2 = new Cursor(bitmap3.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap4 = method_10(string_33, string_32, "bombard.png", bool_28: true);
                cursor_3 = new Cursor(bitmap4.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap5 = method_10(string_33, string_32, "colonize.png", bool_28: true);
                cursor_4 = new Cursor(bitmap5.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap6 = method_10(string_33, string_32, "patrol.png", bool_28: true);
                cursor_5 = new Cursor(bitmap6.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap7 = method_10(string_33, string_32, "mine.png", bool_28: true);
                cursor_6 = new Cursor(bitmap7.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap8 = method_10(string_33, string_32, "loadtroops.png", bool_28: true);
                cursor_7 = new Cursor(bitmap8.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap9 = method_10(string_33, string_32, "unloadtroops.png", bool_28: true);
                cursor_8 = new Cursor(bitmap9.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap10 = method_10(string_33, string_32, "blockade.png", bool_28: true);
                cursor_9 = new Cursor(bitmap10.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap11 = method_10(string_33, string_32, "escort.png", bool_28: true);
                cursor_10 = new Cursor(bitmap11.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap12 = method_10(string_33, string_32, "build.png", bool_28: true);
                cursor_11 = new Cursor(bitmap12.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap13 = method_10(string_33, string_32, "fleetAttackPoint.png", bool_28: true);
                cursor_12 = new Cursor(bitmap13.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap14 = method_10(string_33, string_32, "fleetHomeBase.png", bool_28: true);
                cursor_13 = new Cursor(bitmap14.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap15 = method_10(string_33, string_32, "capture.png", bool_28: true);
                cursor_14 = new Cursor(bitmap15.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap16 = method_10(string_33, string_32, "raid.png", bool_28: true);
                cursor_15 = new Cursor(bitmap16.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap17 = method_10(string_33, string_32, "setEventActionTarget.png", bool_28: true);
                cursor_16 = new Cursor(bitmap17.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap18 = method_10(string_33, string_32, "setEventActionTargetInvalid.png", bool_28: true);
                cursor_17 = new Cursor(bitmap18.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placesystem.png", bool_28: true);
                cursor_18 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placegascloud.png", bool_28: true);
                cursor_19 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placestar.png", bool_28: true);
                cursor_20 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placeplanet.png", bool_28: true);
                cursor_21 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placemoon.png", bool_28: true);
                cursor_22 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placeasteroid.png", bool_28: true);
                cursor_23 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placeasteroidfield.png", bool_28: true);
                cursor_24 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placeshipbase.png", bool_28: true);
                cursor_25 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placecolony.png", bool_28: true);
                cursor_26 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placecharacter.png", bool_28: true);
                cursor_28 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placepopulation.png", bool_28: true);
                cursor_27 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placecreature.png", bool_28: true);
                cursor_29 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placepirate.png", bool_28: true);
                cursor_30 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "placeruins.png", bool_28: true);
                cursor_31 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "explorationMarker.png", bool_28: true);
                cursor_32 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "eraseitem.png", bool_28: true);
                cursor_33 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "erasecolony.png", bool_28: true);
                cursor_34 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "erasepopulation.png", bool_28: true);
                cursor_35 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "eraseruins.png", bool_28: true);
                cursor_36 = new Cursor(bitmap19.GetHicon());
            }));
            taskList.Add(Task.Run(() =>
            {
                Bitmap bitmap19 = method_10(string_33, string_32, "eraseasteroidfield.png", bool_28: true);
                cursor_37 = new Cursor(bitmap19.GetHicon());
            }));
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadEnvGalaxybackdrops(string string_30, string string_31)
        {
            string string_32 = string_30 + "environment\\galaxybackdrops\\";
            string string_33 = string_31 + "environment\\galaxybackdrops\\";
            if (bitmap_176 != null && bitmap_176.PixelFormat != 0)
            {
                bitmap_176.Dispose();
            }
            bitmap_176 = method_10(string_33, string_32, "galaxy_backdrop.jpg", bool_28: true);
        }

        internal string CustomizationSetName()
        {
            string result = string.Empty;
            if (_Game != null && !string.IsNullOrEmpty(_Game.CustomizationSetName) && _Game.CustomizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                result = _Game.CustomizationSetName;
            }
            else if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.CustomizationSetName) && gameOptions_0.CustomizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                result = gameOptions_0.CustomizationSetName;
            }
            return result;
        }

        internal string GetCustomizationPath()
        {
            string result = string.Empty;
            if (_Game != null && !string.IsNullOrEmpty(_Game.CustomizationSetName) && _Game.CustomizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                result = Application.StartupPath + "\\Customization\\" + _Game.CustomizationSetName + "\\";
            }
            else if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.CustomizationSetName) && gameOptions_0.CustomizationSetName.ToLower(CultureInfo.InvariantCulture) != "default")
            {
                result = Application.StartupPath + "\\Customization\\" + gameOptions_0.CustomizationSetName + "\\";
            }
            return result;
        }

        private void MainInitImages()
        {
            List<Task> taskList = new List<Task>();
            double imageScale = 0.5;
            string gameImages = Application.StartupPath + "\\images\\";
            string customizationPath = GetCustomizationPath();
            string customizationSetName = CustomizationSetName();
            customizationPath += "images\\";
            habitatImageCache_0 = new HabitatImageCache();
            habitatImageCache_0.Initialize(Application.StartupPath, customizationSetName, initialLoad: true);
            UpdateSplashProgress();
            if (builtObjectImageCache_0 == null)
            {
                builtObjectImageCache_0 = new BuiltObjectImageCache();
                builtObjectImageCache_0.LoadProgress += builtObjectImageCache_0_LoadProgress;
            }
            builtObjectImageCache_0.Initialize(Application.StartupPath, CustomizationSetName());


            Parallel.Invoke(() => LoadMapStars(gameImages),
            () => LoadStars(gameImages, customizationPath, imageScale, bool_28: true),
            () => LoadNebulae(gameImages, imageScale),
            //method_71();

             () => LoadHyperEffects(gameImages, customizationPath, imageScale),
            () => LoadEffects(gameImages, customizationPath, imageScale),
            () => LoadEnvironmentOverlays(gameImages, customizationPath),
            () => LoadRuins(gameImages, customizationPath),
            () => LoadPlanetaryFacilities(gameImages, customizationPath),
            () => LoadGenerateRings(gameImages, imageScale),
            () => LoadFighters(gameImages, customizationPath, imageScale),
            //method_71();

            () => LoadCreatures(gameImages, customizationPath, imageScale),
            () => LoadTroops(gameImages, customizationPath, CustomizationSetName()),
            () => LoadRacesImg(gameImages, customizationPath, CustomizationSetName()),
            () => LoadUiComponents(gameImages, customizationPath),
            () => LoadUiResources(gameImages, customizationPath),
            () => LoadEffectsWeapons(gameImages, customizationPath),
            () => LoadEffectsExplosion(gameImages, customizationPath, imageScale, bool_28: true),
            //method_71();

            () => LoadUiMessages(gameImages, customizationPath),
            () => LoadUiEvents(gameImages, customizationPath),
            () => LoadUiPlagues(gameImages, customizationPath),
            () => LoadEnvLandscapes(gameImages, customizationPath),
            () => LoadUiChrome(gameImages, customizationSetName),
            () => LoadUiAchievements(gameImages, customizationPath),
            () => LoadUiShipsymbols(gameImages, customizationPath),
            () => LoadUiCursors(gameImages, customizationPath),
            () => LoadEnvGalaxybackdrops(gameImages, customizationPath));

            //LoadMapStars(gameImages);
            // LoadStars(gameImages, customizationPath, imageScale, bool_28: true);
            // LoadNebulae(gameImages, imageScale);
            ////method_71();

            //  LoadHyperEffects(gameImages, customizationPath, imageScale);
            // LoadEffects(gameImages, customizationPath, imageScale);
            // LoadEnvironmentOverlays(gameImages, customizationPath);
            // LoadRuins(gameImages, customizationPath);
            // LoadPlanetaryFacilities(gameImages, customizationPath);
            // LoadGenerateRings(gameImages, imageScale);
            // LoadFighters(gameImages, customizationPath, imageScale);
            ////method_71();

            // LoadCreatures(gameImages, customizationPath, imageScale);
            // LoadTroops(gameImages, customizationPath, method_61());
            // LoadRacesImg(gameImages, customizationPath, method_61());
            // LoadUiComponents(gameImages, customizationPath);
            // LoadUiResources(gameImages, customizationPath);
            // LoadEffectsWeapons(gameImages, customizationPath);
            // LoadEffectsExplosion(gameImages, customizationPath, imageScale, bool_28: true);
            ////method_71();

            // LoadUiMessages(gameImages, customizationPath);
            // LoadUiEvents(gameImages, customizationPath);
            // LoadUiPlagues(gameImages, customizationPath);
            // LoadEnvLandscapes(gameImages, customizationPath);
            // LoadUiChrome(gameImages, customizationSetName);
            // LoadUiAchievements(gameImages, customizationPath);
            // LoadUiShipsymbols(gameImages, customizationPath);
            // LoadUiCursors(gameImages, customizationPath);
            // LoadEnvGalaxybackdrops(gameImages, customizationPath);

        }

        private void builtObjectImageCache_0_LoadProgress(object sender, EventArgs e)
        {
            if (_Game == null)
            {
                UpdateSplashProgress();
            }
        }

        private void method_64()
        {
            gmapMain.ClearData();
            gmapColony.ClearData();
            gmapBuiltObject.ClearData();
            gmapShipGroupInfo.ClearData();
            dboYnQplv3.ClearData();
            gmapEmpireDetail.ClearData();
            gmapMessageHistory.ClearData();
            gmapExpansionPlanner.ClearData();
            mainView.bool_0 = false;
            mainView.list_34 = null;
            mainView.ClearPrecachedHabitatBitmaps();
            mainView.ClearPreprocessedBuiltObjectImages();
            mainView.ClearPreprocessedFighterImages();
            mainView.ClearPreprocessedCreatureImages();
            mainView.EventLocations.Clear();
            mainView.Extinguish();
            picSystem.Extinguish();
            picSystemMap.Extinguish();
            pnlDetailInfo.ClearData();
            pnlDetailInfoShipGroup.ClearData();
            pnlHabitatInfo.ClearData();
            pnlColonyHabitatInfo.ClearData();
            pnlBuiltObjectDetail.ClearData();
            ctlEditHabitatPopulation.ClearData();
            ctlBuiltObjectList.ClearData();
            ctlConstructionYardWaitQueue.ClearData();
            ctlDockingWaitQueue.ClearData();
            ctlColonyDockingBayWaitQueue.ClearData();
            ctlColonyConstructionYardWaitQueue.ClearData();
            ctlEditEmpireBuiltObjectList.ClearData();
            hoverPanel_0.ClearData();
            itemListCollectionPanel_0.Reset();
            tbtnResearch.Reset();
            ctlColonyFacilities.ClearData();
            ctlColonyCharacterTroops.ClearData();
            ctlBuiltObjectCharactersTroops.ClearData();
            UnlxwvByxj.ClearData();
            pnlEmpireDetailInfo.ClearData();
            ctlBuiltObjectComponents.ClearData();
            ctlColonyPopulation.ClearData();
            ctlEditEmpireColonies.ClearData();
            pnlEditEmpireBuiltObjectInfo.ClearData();
            pnlEditEmpireColonyInfo.ClearData();
            ctlBuiltObjectCargo.ClearData();
            ctlBuiltObjectComponentsResources.ClearData();
            ctlCharacterList.ClearData();
            ctlColonyCargo.ClearData();
            ctlColonyConstructionYard.ClearData();
            ctlColonyDockingBay.ClearData();
            ctlColonyResources.ClearData();
            ctlComponentGuideResources.ClearData();
            ctlConstructionSummaryComponents.ClearData();
            ctlConstructionSummaryConstructionResources.ClearData();
            duExoPvEoA.ClearData();
            ctlConstructionYardManufacturerWaitQueue.ClearData();
            ctlConstructionYards.ClearData();
            ctlDesignComponents.ClearData();
            ctlDesignComponentToolbox.ClearData();
            ctlDesignsList.ClearData();
            ctlDesignWeapons.ClearData();
            ctlDiplomacyConversation.ClearData();
            ctlDockingBays.ClearData();
            ctlEditBuiltObjectTroops.ClearData();
            ctlEditEmpireList.ClearData();
            ctlEditEmpireRelationList.ClearData();
            ctlEditHabitatResources.ClearData();
            ctlEditHabitatTroops.ClearData();
            ctlEmpireDiplomaticRelationList.ClearData();
            ctlExpansionPlannerResources.ClearData();
            ctlExpansionPlannerTargets.ClearData();
            ctlIntelligenceAgents.ClearData();
            pnlCharacterMission.ClearData();
            ctlCharacterList.ClearData();
            ctlCharacterEvents.ClearData();
            ctlCharacterSummary.ClearData();
            ctlMessageHistoryMessages.ClearData();
            ctlResearchFacilities.ClearData();
            ctlResourceComponents.ClearData();
            ctlShipGroupListView.ClearData();
            ctlTroopList.ClearData();
            ctlWeapons.ClearData();
            pnlEmpireSummaryBuiltObject.ClearData();
            pnlEmpireSummaryColony.ClearData();
            pnlEmpireSummaryEconomy.ClearData();
            pnlEmpireSummaryBonus.ClearData();
            pnlBuiltObjectConstructionYardPurchaser.ClearData();
            pnlCharacterMission.ClearData();
            pnlColonyConstructionYardPurchaser.ClearData();
            pnlColonyPopulationAttitudeSummary.ClearData();
            pnlComponentGuideDetail.ClearData();
            pnlDesignComponentDetail.ClearData();
            pnlDesignDefense.ClearData();
            pnlDesignEnergy.ClearData();
            pnlDesignIndustry.ClearData();
            pnlDesignMovement.ClearData();
            pnlDesignWarnings.ClearData();
            pnlDiplomacyEmpireSummary.ClearData();
            pnlEditRuins.ClearData();
            pnlEditSpecialRuins.ClearData();
            pnlEmpireComparisonEconomy.ClearData();
            pnlEmpireComparisonMilitaryStrength.ClearData();
            pnlEmpireComparisonPopulation.ClearData();
            pnlEmpireComparisonStrategicValue.ClearData();
            pnlEmpireComparisonTerritory.ClearData();
            pnlEmpireComparisonTopColonies.ClearData();
            pnlEmpireDetailInfo.ClearData();
            pnlGameVictoryConditions.ClearData();
            pnlGameRaceVictoryConditions.ClearData();
            pnlMessagePopup.ClearData();
            pnlResearchSummary.ClearData();
            cmbEditBuiltObjectEmpire.ClearData();
            cmbEditBuiltObjectFleet.ClearData();
            cmbEditEmpireRace.ClearData();
            cmbEditHabitatEmpire.ClearData();
            cmbExpansionPlannerAvailableBuiltObjects.ClearData();
            cmbExpansionPlannerResourceFilter.ClearData();
            _numResourcePercentFilter.Value = 0;
            lstMessages.ClearData();
            if (tutorial_0 != null)
            {
                tutorial_0.ClearData();
                tutorial_0 = null;
            }
            btnEmpireSummary.Image = null;
        }

        internal void method_65(int int_64)
        {
            GC.AddMemoryPressure(int_64 * 1024 * 1024);
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            GC.RemoveMemoryPressure(int_64 * 1024 * 1024);
        }

        internal void method_66(string string_30, bool bool_28)
        {
            bool flag = mainView.bool_0;
            mainView.bool_0 = false;
            if (bool_28)
            {
                if (string_30 == "(Default)")
                {
                    string_30 = string.Empty;
                }
                gameOptions_0.CustomizationSetName = string_30;
            }
            string_3 = string_30;
            if (string.IsNullOrEmpty(string_30))
            {
                string text = Application.StartupPath + "\\GameText.txt";
                if (File.Exists(text))
                {
                    TextResolver.LoadText(text);
                }
            }
            else
            {
                string text2 = Application.StartupPath + "\\Customization\\" + string_30 + "\\GameText.txt";
                if (File.Exists(text2))
                {
                    TextResolver.LoadText(text2);
                }
            }
            ResourceSystem resourceSystem = null;
            Galaxy.InitializeData(Application.StartupPath, string_30, out resourceSystem);
            Start.resourceSystem_0 = Galaxy.ResourceSystemStatic;
            string string_31 = Application.StartupPath + "\\images\\";
            string string_32 = Application.StartupPath + "\\Customization\\" + string_30 + "\\images\\";
            bitmap_6 = new Bitmap[22];
            int_5 = new int[22];
            bitmap_7 = new Bitmap[22];
            list_2 = new List<Rectangle>[22];
            bitmap_23 = new Bitmap[20];
            bitmap_24 = new Bitmap[20];
            bitmap_25 = new Bitmap[20];
            bitmap_26 = new Bitmap[20];
            bitmap_27 = new Bitmap[20];
            habitatImageCache_0.Initialize(Application.StartupPath, string_30, initialLoad: false);
            builtObjectImageCache_0.Initialize(Application.StartupPath, string_30);
            Parallel.Invoke(
                () => LoadFighters(string_31, string_32, 0.5),
                () => LoadRacesImg(string_31, string_32, string_30),
                () => LoadTroops(string_31, string_32, string_30));
            Galaxy.FlagShapes = Galaxy.LoadFlagShapes(Application.StartupPath, string_30);
            Galaxy.FlagShapesPirates = Galaxy.LoadFlagShapesPirates(Application.StartupPath, string_30);
            Parallel.Invoke(() => LoadUiResources(string_31, string_32),
                () => LoadUiComponents(string_31, string_32),
                () => LoadUiChrome(string_31, string_30),
                () => LoadUiCursors(string_31, string_32),
                () => LoadEffectsWeapons(string_31, string_32),
                () => LoadPlanetaryFacilities(string_31, string_32),
                () => LoadRuins(string_31, string_32));
            string_27 = string.Empty;
            bool bool_29 = false;
            if (string.IsNullOrEmpty(string_30))
            {
                bool_29 = true;
            }
            double double_ = 0.5;
            Parallel.Invoke(() => LoadStars(string_31, string_32, double_, bool_29),
               () => LoadHyperEffects(string_31, string_32, double_),
               () => LoadEffects(string_31, string_32, double_),
               () => LoadEnvironmentOverlays(string_31, string_32),
               () => LoadEffectsExplosion(string_31, string_32, double_, bool_29),
               () => LoadUiMessages(string_31, string_32),
               () => LoadUiEvents(string_31, string_32),
               () => LoadUiPlagues(string_31, string_32),
               () => LoadEnvLandscapes(string_31, string_32),
               () => LoadUiAchievements(string_31, string_32),
               () => LoadUiShipsymbols(string_31, string_32),
               () => LoadEnvGalaxybackdrops(string_31, string_32));
            mainView.ResetRendering();
            encyclopediaItemList_0 = method_465(null, Application.StartupPath, string_30);
            vTtmruAejE = new EncyclopediaItemList();
            int_25 = 0;
            subRoleNameSet_0 = Galaxy.LoadShipNames(Application.StartupPath, string_30);
            list_1 = Galaxy.LoadColonyNames(Application.StartupPath, string_30);
            dialogSet_0 = null;
            EffectsPlayer.Initialize(Application.StartupPath, string_30);
            string text3 = Application.StartupPath + "\\sounds\\effects\\";
            string text4 = string.Empty;
            if (!string.IsNullOrEmpty(string_30))
            {
                text4 = Application.StartupPath + "\\Customization\\" + string_30 + "\\sounds\\effects\\";
            }
            if (!string.IsNullOrEmpty(text4) && File.Exists(text4 + "button1.wav"))
            {
                CloseButton.SetSoundLocation(text4 + "button1.wav");
            }
            else
            {
                CloseButton.SetSoundLocation(text3 + "button1.wav");
            }
            method_18(text3, text4, this);
            method_17(text3, text4);
            method_72(string_30);
            method_67(string_30);
            method_65(200);
            mainView.bool_0 = flag;
        }

        internal void method_67(string string_30)
        {
            method_68(string_30, bool_28: true);
        }

        internal void method_68(string string_30, bool bool_28)
        {
            string folder = Application.StartupPath + "\\sounds\\music\\";
            string text = Application.StartupPath + "\\Customization\\" + string_30 + "\\sounds\\music\\";
            string text2 = "DistantWorldsTheme.mp3";
            bool flag = false;
            if (Directory.Exists(text))
            {
                string[] files = Directory.GetFiles(text, "*.mp3");
                if (files != null && files.Length > 0)
                {
                    if (!File.Exists(text + text2))
                    {
                        FileInfo fileInfo = new FileInfo(files[0]);
                        text2 = fileInfo.Name;
                    }
                    if (musicPlayer_0 != null)
                    {
                        musicPlayer_0.Stop();
                    }
                    musicPlayer_0 = new MusicPlayer(this, text, text2);
                    flag = true;
                }
            }
            if (!flag)
            {
                if (musicPlayer_0 != null)
                {
                    musicPlayer_0.Stop();
                }
                musicPlayer_0 = new MusicPlayer(this, folder, text2);
            }
            musicPlayer_0.SetVolume(gameOptions_0.MusicVolume);
            if (bool_28)
            {
                musicPlayer_0.StartTheme();
            }
        }

        private void method_69(Font font_9, Control control_1)
        {
            method_70(font_9, control_1, null);
        }

        private void method_70(Font font_9, Control control_1, Type type_0)
        {
            if (type_0 == null || control_1.GetType() == type_0)
            {
                control_1.Font = font_9;
            }
            if (control_1.Controls == null)
            {
                return;
            }
            foreach (Control control in control_1.Controls)
            {
                method_70(font_9, control, type_0);
            }
        }

        private void UpdateSplashProgress()
        {
            Class5._Splash.UpdateState(int_26);
            int_26++;
            //if (splash_0 == null)
            //{
            //    return;
            //}
            //string[] array = new string[14]
            //{
            //TextResolver.GetText("Forming nebulae clouds..."),
            //TextResolver.GetText("Igniting stellar cores..."),
            //TextResolver.GetText("Emptying Black Holes..."),
            //TextResolver.GetText("Tidying up asteroid fields..."),
            //TextResolver.GetText("Initiating orbital motion..."),
            //TextResolver.GetText("Building mining stations..."),
            //TextResolver.GetText("Fueling pirate ships..."),
            //TextResolver.GetText("Repairing battle damage..."),
            //TextResolver.GetText("Recharging reactors..."),
            //TextResolver.GetText("Recalibrating hyperdrives..."),
            //TextResolver.GetText("Feeding the Giant Kaltors..."),
            //TextResolver.GetText("Starting the Game"),
            //"",
            //""
            //};
            //if (int_26 < array.Length && splash_0.Visible)
            //{
            //    string text = array[int_26];
            //    int_26++;
            //    int_26 = Math.Min(int_26, array.Length - 1);
            //    splash_0.SuspendLayout();
            //    splash_0.lblMessage.MaximumSize = new Size(450, 30);
            //    splash_0.lblMessage.Text = text;
            //    splash_0.lblMessage.ForeColor = Color.FromArgb(255, 192, 0);
            //    SizeF sizeF = splash_0.lblMessage.Size;
            //    Point point = new Point((splash_0.Width - (int)sizeF.Width) / 2, 195);
            //    using (Graphics graphics = CreateGraphics())
            //    {
            //        sizeF = graphics.MeasureString(text, splash_0.lblMessage.Font, 450);
            //        sizeF = new SizeF(sizeF.Width + 10f, sizeF.Height + 5f);
            //        splash_0.lblMessage.Size = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
            //        splash_0.lblMessage.MaximumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
            //        splash_0.lblMessage.MinimumSize = new Size((int)sizeF.Width + 1, (int)sizeF.Height + 1);
            //        point = new Point((splash_0.Width - (int)sizeF.Width) / 2, 195);
            //        splash_0.lblMessage.Location = point;
            //        splash_0.lblMessage.Text = text;
            //    }
            //    splash_0.lblMessage.Update();
            //    splash_0.ResumeLayout();
            //    Application.DoEvents();
            //}
        }

        internal void method_72(string string_30)
        {
            method_73(string_30, string.Empty);
        }

        internal void method_73(string string_30, string string_31)
        {
            InfoPanel.InitializeImages(characterImageCache_0, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27, _uiResourcesBitmaps, raceImageCache_0, builtObjectImageCache_0.GetImagesSmall(), bitmap_6, bitmap_2, habitatImageCache_0.GetImagesSmall(), bitmap_8, bitmap_33, bitmap_34, bitmap_35, bitmap_36, bitmap_32, bitmap_37, bitmap_38, bitmap_43, bitmap_44, bitmap_43, bitmap_53, bitmap_28[20], bitmap_28, bitmap_9);
            ctlEditHabitatPopulation.InitializeImages(raceImageCache_0.GetRaceImages());
            hoverPanel_0.InitializeImages(this, builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), bitmap_10, raceImageCache_0.GetRaceImages(), _uiResourcesBitmaps);
            pnlGameRaceVictoryConditions.InitializeImages(raceImageCache_0);
            pnlGameSummary.InitializeImages(bitmap_216, bitmap_217, bitmap_218, bitmap_219, bitmap_220, bitmap_221, bitmap_222, bitmap_223);
            diplomaticMessageQueue_0.InitializeImages(raceImageCache_0, bitmap_72, bitmap_51, bitmap_37, bitmap_52, bitmap_73, bitmap_55, bitmap_81, bitmap_71, bitmap_82, bitmap_85, bitmap_28[28], bitmap_84, bitmap_49, bitmap_87);
            itemListCollectionPanel_0.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), raceImageCache_0, _uiResourcesBitmaps, bitmap_8, bitmap_79, bitmap_80, bitmap_128, bitmap_129, bitmap_130, bitmap_131, bitmap_132, bitmap_126, bitmap_127, bitmap_28[30]);
            List<Bitmap> list = method_396();
            tbtnResearch.InitializeImages(bitmap_21, bitmap_6, list.ToArray(), bitmap_8, bitmap_78, bitmap_83, bitmap_23, bitmap_24, bitmap_25, bitmap_26);
            pnlResearchTree.InitializeImages(bitmap_21, bitmap_6, list.ToArray(), raceImageCache_0.GetRaceImages(), bitmap_8, bitmap_78, bitmap_38, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_9);
            ctlExpansionPlannerTargets.InitializeImages(habitatImageCache_0.GetImagesSmall(), builtObjectImageCache_0.GetImagesSmall(), raceImageCache_0.GetRaceImages(), _uiResourcesBitmaps, bitmap_2, bitmap_4);
            BuiltObjectListView.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), bitmap_53);
            ctlBuiltObjectList.Kickstart(allowMultiselect: true);
            ctlConstructionYardWaitQueue.Kickstart(allowMultiselect: false);
            ctlDockingWaitQueue.Kickstart(allowMultiselect: false);
            ctlColonyDockingBayWaitQueue.Kickstart(allowMultiselect: false);
            ctlColonyConstructionYardWaitQueue.Kickstart(allowMultiselect: false);
            ctlEditEmpireBuiltObjectList.Kickstart(allowMultiselect: false);
            cmbExpansionPlannerAvailableBuiltObjects.InitializeImages(builtObjectImageCache_0.GetImagesSmall());
            ctlCharacterSummary.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall());
            cmbEditBuiltObjectFleet.InitializeImages(builtObjectImageCache_0.GetImagesSmall());
            cmbTroopFilter.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall());
            ctlColonyFacilities.InitializeImages(bitmap_8);
            ctlEditHabitatPlanetaryFacilities.InitializeImages(bitmap_8);
            CharacterTroopListIconView.InitializeImages(bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27);
            ctlBuiltObjectCharactersTroops.Kickstart();
            ctlColonyCharacterTroops.Kickstart();
            HabitatListView.InitializeImages(habitatImageCache_0.GetImagesSmall(), bitmap_8, bitmap_44, bitmap_43, bitmap_33, bitmap_34, bitmap_35, bitmap_36);
            pnlEmpireDetailInfo.InitializeImages(raceImageCache_0);
            pnlEmpireDetailInfo.Kickstart(base.ClientSize);
            ctlBuiltObjectComponents.InitializeImages(bitmap_21, bitmap_22);
            ctlColonyPopulation.InitializeImages(raceImageCache_0.GetRaceImages());
            pnlColonyInvasionContainer.InitializeImages(bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27, bitmap_8, builtObjectImageCache_0, characterImageCache_0, raceImageCache_0.GetRaceImages(), bitmap_19, bitmap_103, bitmap_104, bitmap_16[0], bitmap_16[1], bitmap_16[2], bitmap_16[3], Application.StartupPath, gameOptions_0.CustomizationSetName);
            LoadUiChromeButtons(string_30, string_31);
        }

        private void MainInit(int width, int height, bool windowedMode)
        {
            SetControlLocalizedLabels();
            ComputerInfo computerInfo = new ComputerInfo();
            long_0 = (long)computerInfo.TotalPhysicalMemory;
            bool_6 = Environment.Is64BitOperatingSystem;
            int_1 = Environment.OSVersion.Version.Major;
            int_2 = Environment.OSVersion.Version.Minor;
            ToggleScreenSaverActive(active: false);
            bool_2 = method_373();
            if (!bool_2)
            {
                method_375();
            }
            UpdateSplashProgress();
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.Opaque, value: true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            SetStyle(ControlStyles.Selectable, value: false);
            UpdateStyles();
            intptr_0 = method_526("DistantWorlds.Resources.Forgotte.ttf");
            intptr_1 = method_526("DistantWorlds.Resources.Forgottb.ttf");
            font_0 = ((IFontCache)this).GenerateFont(32f, isBold: true);
            font_1 = ((IFontCache)this).GenerateFont(22.67f, isBold: true);
            font_2 = ((IFontCache)this).GenerateFont(18.67f, isBold: true);
            font_6 = ((IFontCache)this).GenerateFont(16.67f, isBold: false);
            font_7 = ((IFontCache)this).GenerateFont(16.67f, isBold: true);
            font_3 = ((IFontCache)this).GenerateFont(15.33f, isBold: false);
            font_4 = ((IFontCache)this).GenerateFont(20.77f, isBold: false);
            font_5 = ((IFontCache)this).GenerateFont(15.33f, isBold: true);
            font_8 = ((IFontCache)this).GenerateFont(18.67f, isBold: false);
            Font font = Font;
            method_69(font_3, this);
            method_70(font_3, this, typeof(BorderPanel));
            method_70(((IFontCache)this).GenerateFont(15.83f, isBold: true), this, typeof(GlassButton));
            Font = font;
            pnlBuiltObjectInfo.SetFontCache(this);
            pnlCharacterInfo.SetFontCache(this);
            pnlColonyInfo.SetFontCache(this);
            pnlColonyInvasion.SetFontCache(this);
            pnlDesignDetail.SetFontCache(this);
            pnlDesigns.SetFontCache(this);
            pnlDiplomacyTalk.SetFontCache(this);
            pnlEditBuiltObject.SetFontCache(this);
            pnlEditCreature.SetFontCache(this);
            pnlEditEmpire.SetFontCache(this);
            pnlEditEmpireList.SetFontCache(this);
            pnlEditHabitat.SetFontCache(this);
            vHfFsoqMev.SetFontCache(this);
            pnlEmpirePolicy.SetFontCache(this);
            pnlBuildOrder.SetFontCache(this);
            pnlAdvisorSuggestion.SetFontCache(this);
            pnlCharacterEventHistory.SetFontCache(this);
            pnlEmpireInfo.SetFontCache(this);
            pnlEmpireSummary.SetFontCache(this);
            pnlEncyclopedia.SetFontCache(this);
            pnlEventMessage.SetFontCache(this);
            CaLkaMyrMQ.SetFontCache(this);
            pnlGalaxyMapKey.SetFontCache(this);
            pnlGameEditor.SetFontCache(this);
            pnlGameEditorEnterPassword.SetFontCache(this);
            pnlGameEditorPassword.SetFontCache(this);
            pnlGameEnd.SetFontCache(this);
            pnlGameMenu.SetFontCache(this);
            pnlGameOptions.SetFontCache(this);
            pnlGameOptionsAdvancedDisplaySettings.SetFontCache(this);
            pnlGameOptionsEmpireSettings.SetFontCache(this);
            pnlGameOptionsMessages.SetFontCache(this);
            pnlInfoPanel.SetFontCache(this);
            kYdDyYeMls.SetFontCache(this);
            pnlIntroduction.SetFontCache(this);
            pnlResearch.SetFontCache(this);
            pnlSaveLoadProgress.SetFontCache(this);
            pnlShipGroupInfo.SetFontCache(this);
            pnlSystemMap.SetFontCache(this);
            pnlTroopInfo.SetFontCache(this);
            pnlTutorial.SetFontCache(this);
            pnlMessageHistory.SetFontCache(this);
            pnlExpansionPlanner.SetFontCache(this);
            pnlComponentGuide.SetFontCache(this);
            pnlConstructionSummary.SetFontCache(this);
            pnlRuinDetail.SetFontCache(this);
            pnlResourceComponents.SetFontCache(this);
            pnlRetrofit.SetFontCache(this);
            ynbOfkDbGY.SetFontCache(this);
            pnlEditEventAction.SetFontCache(this);
            ctlGameEvents.SetFontCache(this);
            ctlGameEvent.SetFontCache(this);
            pnlCharacterEditSkillsTraits.SetFontCache(this);
            gmapBuiltObject.SetFontCache(this);
            gmapColony.SetFontCache(this);
            gmapEmpireDetail.SetFontCache(this);
            gmapMain.SetFontCache(this);
            gmapShipGroupInfo.SetFontCache(this);
            dboYnQplv3.SetFontCache(this);
            gmapMessageHistory.SetFontCache(this);
            gmapExpansionPlanner.SetFontCache(this);
            pnlBuiltObjectConstructionYardPurchaser.SetFontCache(this);
            pnlBuiltObjectDetail.SetFontCache(this);
            pnlCharacterMission.SetFontCache(this);
            pnlColonyConstructionYardPurchaser.SetFontCache(this);
            pnlColonyHabitatInfo.SetFontCache(this);
            pnlColonyPopulationAttitudeSummary.SetFontCache(this);
            pnlDesignBasics.SetFontCache(this);
            pnlDesignComponentDetail.SetFontCache(this);
            pnlDesignDefense.SetFontCache(this);
            pnlDesignEnergy.SetFontCache(this);
            pnlDesignIndustry.SetFontCache(this);
            pnlDesignMovement.SetFontCache(this);
            pnlDesignWarnings.SetFontCache(this);
            pnlDesignWeapons.SetFontCache(this);
            pnlDetailInfo.SetFontCache(this);
            pnlDetailInfoShipGroup.SetFontCache(this);
            pnlDiplomacyEmpireSummary.SetFontCache(this);
            pnlDiplomaticConversationResponse.SetFontCache(this);
            pnlDiplomaticRelationColorKey.SetFontCache(this);
            pnlEditEmpireBuiltObjectInfo.SetFontCache(this);
            pnlEditEmpireColonyInfo.SetFontCache(this);
            pnlEditRuinsContainer.SetFontCache(this);
            pnlEmpireComparisonMilitaryStrength.SetFontCache(this);
            pnlEmpireComparisonPopulation.SetFontCache(this);
            pnlEmpireComparisonStrategicValue.SetFontCache(this);
            pnlEmpireComparisonTerritory.SetFontCache(this);
            pnlEmpireComparisonTopColonies.SetFontCache(this);
            pnlEmpireDetailInfo.SetFontCache(this);
            pnlEmpireSummaryBuiltObject.SetFontCache(this);
            pnlEmpireSummaryColony.SetFontCache(this);
            pnlEmpireSummaryEconomy.SetFontCache(this);
            pnlEmpireSummaryBonus.SetFontCache(this);
            pnlEncyclopediaRelatedItems.SetFontCache(this);
            pnlEventMessagePanel.SetFontCache(this);
            pnlGalaxyMapKeyActual.SetFontCache(this);
            pnlGameVictoryConditions.SetFontCache(this);
            pnlHabitatInfo.SetFontCache(this);
            pnlIntroductionBackground.SetFontCache(this);
            pnlMessagePopup.SetFontCache(this);
            pnlResearchSummary.SetFontCache(this);
            ctlBuiltObjectCargo.SetFontCache(this);
            ctlBuiltObjectCharactersTroops.SetFontCache(this);
            ctlBuiltObjectComponents.SetFontCache(this);
            ctlBuiltObjectComponentsResources.SetFontCache(this);
            ctlBuiltObjectList.SetFontCache(this);
            ctlCharacterList.SetFontCache(this);
            ctlColonyCargo.SetFontCache(this);
            ctlColonyCharacterTroops.SetFontCache(this);
            ctlColonyConstructionYard.SetFontCache(this);
            ctlColonyConstructionYardWaitQueue.SetFontCache(this);
            ctlColonyDockingBay.SetFontCache(this);
            ctlColonyDockingBayWaitQueue.SetFontCache(this);
            UnlxwvByxj.SetFontCache(this);
            ctlColonyPopulation.SetFontCache(this);
            ctlColonyResources.SetFontCache(this);
            duExoPvEoA.SetFontCache(this);
            ctlConstructionYardManufacturerWaitQueue.SetFontCache(this);
            ctlConstructionYards.SetFontCache(this);
            ctlConstructionYardWaitQueue.SetFontCache(this);
            ctlDesignComponents.SetFontCache(this);
            ctlDesignComponentToolbox.SetFontCache(this);
            ctlDesignsList.SetFontCache(this);
            ctlDesignWeapons.SetFontCache(this);
            ctlDockingBays.SetFontCache(this);
            ctlDockingWaitQueue.SetFontCache(this);
            ctlEditBuiltObjectTroops.SetFontCache(this);
            ctlEditEmpireBuiltObjectList.SetFontCache(this);
            ctlEditEmpireColonies.SetFontCache(this);
            ctlEditEmpireList.SetFontCache(this);
            ctlEditEmpireRelationList.SetFontCache(this);
            ctlEditHabitatPopulation.SetFontCache(this);
            ctlEditHabitatResources.SetFontCache(this);
            ctlEditHabitatTroops.SetFontCache(this);
            ctlEditHabitatPirateColonyControl.SetFontCache(this);
            ctlEmpireDiplomaticRelationList.SetFontCache(this);
            ctlIntelligenceAgents.SetFontCache(this);
            ctlCharacterEvents.SetFontCache(this);
            ctlResearchFacilities.SetFontCache(this);
            ctlShipGroupListView.SetFontCache(this);
            ctlTroopList.SetFontCache(this);
            ctlWeapons.SetFontCache(this);
            ctlMessageHistoryMessages.SetFontCache(this);
            ctlExpansionPlannerResources.SetFontCache(this);
            ctlExpansionPlannerTargets.SetFontCache(this);
            ctlComponentGuideResources.SetFontCache(this);
            ctlConstructionSummaryComponents.SetFontCache(this);
            ctlConstructionSummaryConstructionResources.SetFontCache(this);
            ctlResourceComponents.SetFontCache(this);
            pnlEditRuins.SetFontCache(this);
            pnlEditSpecialRuins.SetFontCache(this);
            pnlEncyclopediaTopics.SetFontCache(this);
            ctlDiplomacyConversation.SetFontCache(this);
            ctlDiplomacyTradeUs.SetFontCache(this);
            ctlDiplomacyTradeThem.SetFontCache(this);
            lstMessages.SetFontCache(this);
            actionMenu.Font = font_3;
            selectionMenu.Font = font_3;
            mainView.Font = font_3;
            gameEditorSelector.Font = font_3;
            BringToFront();
            if (windowedMode)
            {
                base.FormBorderStyle = FormBorderStyle.Fixed3D;
                base.ClientSize = new Size(width, height);
                CenterToScreen();
            }
            else
            {
                base.FormBorderStyle = FormBorderStyle.None;
                base.Location = new Point(0, 0);
                base.Height = height;
                base.Width = width;
                base.WindowState = FormWindowState.Minimized;
                base.WindowState = FormWindowState.Maximized;
            }
            mainView.Location = new Point(0, 0);
            mainView.Size = base.ClientSize;
            mainView.SendToBack();
            Rectangle rectangle = new Rectangle(0, 0, width, height);
            int num = 64;
            int num2 = 80;
            int num3 = (rectangle.Width - 700) / 2;
            btnHistoryMessages.Size = new Size(32, 48);
            btnGalacticHistory.Size = new Size(32, 32);
            btnHistoryMessages.SetCornerCurves(topLeft: false, topRight: true, bottomRight: false, bottomLeft: false);
            btnGalacticHistory.SetCornerCurves(topLeft: false, topRight: false, bottomRight: true, bottomLeft: false);
            btnHistoryMessages.Location = new Point(num3 + 668, 10);
            btnGalacticHistory.Location = new Point(num3 + 668, 58);
            lstMessages.CurveMode = CornerCurveMode.TopLeft_BottomLeft;
            lstMessages.Size = new Size(668, 80);
            lstMessages.Location = new Point(num3, 10);
            lstMessages.BringToFront();
            int num4 = (rectangle.Width - 624) / 2;
            tbtnColonies.Size = new Size(32, 32);
            btnExpansionPlanner.Size = new Size(32, 32);
            btnEmpireGraphs.Size = new Size(32, 32);
            btnEmpirePolicy.Size = new Size(32, 32);
            btnGameEditor.Size = new Size(32, 32);
            btnBuildOrder.Size = new Size(32, 32);
            tbtnConstructionYards.Size = new Size(32, 32);
            tbtnBuiltObjects.Size = new Size(32, 32);
            tbtnShipGroups.Size = new Size(32, 32);
            tbtnTroops.Size = new Size(32, 32);
            tbtnColonies.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: true);
            btnExpansionPlanner.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnEmpireGraphs.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnEmpirePolicy.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnGameEditor.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnBuildOrder.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnConstructionYards.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnBuiltObjects.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnShipGroups.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnTroops.SetCornerCurves(topLeft: false, topRight: false, bottomRight: true, bottomLeft: false);
            tbtnIntelligenceAgents.Size = new Size(32, 64);
            tbtnEmpires.Size = new Size(80, 64);
            btnEmpireSummary.Size = new Size(80, 64);
            tbtnResearch.Size = new Size(80, 64);
            tbtnDesigns.Size = new Size(32, 64);
            tbtnIntelligenceAgents.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: true);
            tbtnEmpires.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnEmpireSummary.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnResearch.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            tbtnDesigns.SetCornerCurves(topLeft: false, topRight: false, bottomRight: true, bottomLeft: false);
            int num5 = num4;
            tbtnColonies.Location = new Point(num5, 90);
            num5 += 32;
            btnExpansionPlanner.Location = new Point(num5, 90);
            num5 += 32;
            btnEmpireGraphs.Location = new Point(num5, 90);
            num5 += 32;
            btnEmpirePolicy.Location = new Point(num5, 90);
            num5 += 32;
            btnGameEditor.Location = new Point(num5, 90);
            num5 += 32;
            tbtnIntelligenceAgents.Location = new Point(num5, 90);
            num5 += 32;
            tbtnEmpires.Location = new Point(num5, 90);
            num5 += 80;
            btnEmpireSummary.Location = new Point(num5, 90);
            num5 += 80;
            tbtnResearch.Location = new Point(num5, 90);
            num5 += 80;
            tbtnDesigns.Location = new Point(num5, 90);
            num5 += 32;
            btnBuildOrder.Location = new Point(num5, 90);
            num5 += 32;
            tbtnConstructionYards.Location = new Point(num5, 90);
            num5 += 32;
            tbtnBuiltObjects.Location = new Point(num5, 90);
            num5 += 32;
            tbtnShipGroups.Location = new Point(num5, 90);
            num5 += 32;
            tbtnTroops.Location = new Point(num5, 90);
            num5 += 32;
            btnGameMenu.Size = new Size(40, 40);
            btnGameMenu.Location = new Point(10, 10);
            btnGameMenu.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnHelp.Size = new Size(40, 40);
            btnHelp.Location = new Point(50, 10);
            btnHelp.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnPlayPause.Size = new Size(80, 34);
            btnPlayPause.Location = new Point(10, 62);
            btnPlayPause.SetCornerCurves(topLeft: true, topRight: true, bottomRight: false, bottomLeft: false);
            btnGameSpeedDecrease.Size = new Size(40, 20);
            btnGameSpeedDecrease.Location = new Point(10, 96);
            btnGameSpeedDecrease.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: true);
            btnGameSpeedIncrease.Size = new Size(40, 20);
            btnGameSpeedIncrease.Location = new Point(50, 96);
            btnGameSpeedIncrease.SetCornerCurves(topLeft: false, topRight: false, bottomRight: true, bottomLeft: false);
            tbtnBuiltObjects.ClipBackground = true;
            tbtnBuiltObjects.IntensifyColors = true;
            tbtnColonies.ClipBackground = true;
            tbtnColonies.IntensifyColors = true;
            tbtnConstructionYards.ClipBackground = true;
            tbtnConstructionYards.IntensifyColors = true;
            tbtnDesigns.ClipBackground = true;
            tbtnDesigns.IntensifyColors = true;
            tbtnEmpires.ClipBackground = true;
            tbtnEmpires.IntensifyColors = true;
            tbtnGalaxyMap.ClipBackground = true;
            tbtnGalaxyMap.IntensifyColors = true;
            tbtnIntelligenceAgents.ClipBackground = true;
            tbtnIntelligenceAgents.IntensifyColors = true;
            tbtnResearch.ClipBackground = true;
            tbtnResearch.IntensifyColors = true;
            tbtnShipGroups.ClipBackground = true;
            tbtnShipGroups.IntensifyColors = true;
            tbtnTroops.ClipBackground = true;
            tbtnTroops.IntensifyColors = true;
            btnEmpireGraphs.ClipBackground = true;
            btnEmpireGraphs.IntensifyColors = true;
            btnGameEditor.ClipBackground = true;
            btnGameEditor.IntensifyColors = true;
            btnEmpirePolicy.ClipBackground = true;
            btnEmpirePolicy.IntensifyColors = true;
            btnBuildOrder.ClipBackground = true;
            btnBuildOrder.IntensifyColors = true;
            btnGameMenu.ClipBackground = true;
            btnGameMenu.IntensifyColors = true;
            btnPlayPause.ClipBackground = true;
            btnPlayPause.IntensifyColors = true;
            btnEmpireSummary.ClipBackground = true;
            btnEmpireSummary.IntensifyColors = true;
            btnExpansionPlanner.ClipBackground = true;
            btnExpansionPlanner.IntensifyColors = true;
            btnHistoryMessages.ClipBackground = true;
            btnHistoryMessages.IntensifyColors = true;
            btnGalacticHistory.ClipBackground = true;
            btnGalacticHistory.IntensifyColors = true;
            btnGameSpeedIncrease.ClipBackground = true;
            btnGameSpeedIncrease.IntensifyColors = true;
            btnGameSpeedDecrease.ClipBackground = true;
            btnGameSpeedDecrease.IntensifyColors = true;
            btnGameSpeedDecrease.TabStop = false;
            btnGameSpeedIncrease.TabStop = false;
            btnSelectionPanelSize.ClipBackground = true;
            btnSelectionPanelSize.IntensifyColors = true;
            btnZoomColony.ClipBackground = true;
            btnZoomColony.IntensifyColors = true;
            btnZoomIn.ClipBackground = true;
            btnZoomIn.IntensifyColors = true;
            btnZoomOut.ClipBackground = true;
            btnZoomOut.IntensifyColors = true;
            btnZoomRegion.ClipBackground = true;
            btnZoomRegion.IntensifyColors = true;
            jQaYpdpkDs.ClipBackground = true;
            jQaYpdpkDs.IntensifyColors = true;
            btnZoomSelection.ClipBackground = true;
            btnZoomSelection.IntensifyColors = true;
            btnSelectNearestMilitary.ClipBackground = true;
            btnSelectNearestMilitary.IntensifyColors = true;
            btnCycleShipStance.ClipBackground = true;
            btnCycleShipStance.IntensifyColors = true;
            btnZoomSystem.ClipBackground = true;
            btnZoomSystem.IntensifyColors = true;
            btnHelp.ClipBackground = true;
            btnHelp.IntensifyColors = true;
            btnCycleBases.ClipBackground = true;
            btnCycleBases.IntensifyColors = true;
            btnCycleColonies.ClipBackground = true;
            btnCycleColonies.IntensifyColors = true;
            btnCycleConstruction.ClipBackground = true;
            btnCycleConstruction.IntensifyColors = true;
            btnCycleIdleShips.ClipBackground = true;
            btnCycleIdleShips.IntensifyColors = true;
            btnCycleMilitary.ClipBackground = true;
            btnCycleMilitary.IntensifyColors = true;
            btnCycleOther.ClipBackground = true;
            btnCycleOther.IntensifyColors = true;
            btnCycleShipGroups.ClipBackground = true;
            btnCycleShipGroups.IntensifyColors = true;
            btnCycleBasesBack.ClipBackground = true;
            btnCycleBasesBack.IntensifyColors = true;
            btnCycleColoniesBack.ClipBackground = true;
            btnCycleColoniesBack.IntensifyColors = true;
            btnCycleConstructionBack.ClipBackground = true;
            btnCycleConstructionBack.IntensifyColors = true;
            btnCycleIdleShipsBack.ClipBackground = true;
            btnCycleIdleShipsBack.IntensifyColors = true;
            btnCycleMilitaryBack.ClipBackground = true;
            btnCycleMilitaryBack.IntensifyColors = true;
            btnCycleOtherBack.ClipBackground = true;
            btnCycleOtherBack.IntensifyColors = true;
            btnCycleShipGroupsBack.ClipBackground = true;
            btnCycleShipGroupsBack.IntensifyColors = true;
            btnLockView.ClipBackground = true;
            btnLockView.IntensifyColors = true;
            btnSelectionBack.ClipBackground = true;
            btnSelectionBack.IntensifyColors = true;
            btnSelectionForward.ClipBackground = true;
            btnSelectionForward.IntensifyColors = true;
            btnSelectionAction1.ClipBackground = false;
            btnSelectionAction1.IntensifyColors = true;
            btnSelectionAction1.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction2.ClipBackground = false;
            btnSelectionAction2.IntensifyColors = true;
            btnSelectionAction2.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction3.ClipBackground = false;
            btnSelectionAction3.IntensifyColors = true;
            btnSelectionAction3.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction4.ClipBackground = false;
            btnSelectionAction4.IntensifyColors = true;
            btnSelectionAction4.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction5.ClipBackground = false;
            btnSelectionAction5.IntensifyColors = true;
            btnSelectionAction5.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction6.ClipBackground = false;
            btnSelectionAction6.IntensifyColors = true;
            btnSelectionAction6.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction7.ClipBackground = false;
            btnSelectionAction7.IntensifyColors = true;
            btnSelectionAction7.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnSelectionAction8.ClipBackground = false;
            btnSelectionAction8.IntensifyColors = true;
            btnSelectionAction8.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            method_263();
            if (!string.IsNullOrEmpty(gameOptions_0.CustomizationSetName))
            {
                string path = Application.StartupPath + "\\Customization\\" + gameOptions_0.CustomizationSetName + "\\";
                if (!Directory.Exists(path))
                {
                    gameOptions_0.CustomizationSetName = string.Empty;
                }
            }
            method_256();
            //Stopwatch sw = Stopwatch.StartNew();
            MainInitImages();
            //sw.Stop();
            //ShowMessageBox(sw.Elapsed.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.None);
            string text = string.Empty;
            if (gameOptions_0 != null)
            {
                text = gameOptions_0.CustomizationSetName;
            }
            subRoleNameSet_0 = Galaxy.LoadShipNames(Application.StartupPath, text);
            list_1 = Galaxy.LoadColonyNames(Application.StartupPath, text);
            string_3 = text;
            Galaxy.FlagShapes = Galaxy.LoadFlagShapes(Application.StartupPath, text);
            Galaxy.FlagShapesPirates = Galaxy.LoadFlagShapesPirates(Application.StartupPath, text);
            encyclopediaItemList_0 = method_465(null, Application.StartupPath, text);
            vTtmruAejE = new EncyclopediaItemList();
            int_25 = 0;
            mainView.Font = new Font("Verdana", 8f, FontStyle.Regular);
            InfoPanel.InitializeImages(characterImageCache_0, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27, _uiResourcesBitmaps, raceImageCache_0,
                builtObjectImageCache_0.GetImagesSmall(), bitmap_6, bitmap_2, habitatImageCache_0.GetImagesSmall(), bitmap_8, bitmap_33, bitmap_34,
                bitmap_35, bitmap_36, bitmap_32, bitmap_37, bitmap_38, bitmap_43, bitmap_44, bitmap_43, bitmap_53, bitmap_28[20], bitmap_28, bitmap_9);
            pnlDetailInfo.Size = new Size(280, 240);
            pnlDetailInfo.Location = new Point(44, 5);
            pnlDetailInfo.Font = new Font("Verdana", 8f, FontStyle.Regular);
            pnlDetailInfo.Reset();
            pnlDetailInfo.BringToFront();
            pnlInfoPanel.Size = new Size(370, 250);
            pnlInfoPanel.Location = new Point(26, mainView.Size.Height - (pnlDetailInfo.Size.Height + 50));
            pnlInfoPanel.BringToFront();
            pnlDetailInfoShipGroup.Size = new Size(240, 240);
            pnlDetailInfoShipGroup.Location = new Point(20, mainView.Size.Height - (pnlDetailInfo.Size.Height + 20));
            pnlDetailInfoShipGroup.Font = new Font("Verdana", 8f, FontStyle.Regular);
            pnlDetailInfoShipGroup.ShowExtendedInfo = true;
            pnlDetailInfoShipGroup.Reset();
            pnlHabitatInfo.Size = new Size(240, 240);
            pnlHabitatInfo.Location = new Point(20, mainView.Size.Height - (pnlDetailInfo.Size.Height + 20));
            pnlHabitatInfo.Font = new Font("Verdana", 8f, FontStyle.Regular);
            pnlHabitatInfo.Reset();
            pnlColonyHabitatInfo.Size = new Size(240, 240);
            pnlColonyHabitatInfo.Location = new Point(20, mainView.Size.Height - (pnlDetailInfo.Size.Height + 20));
            pnlColonyHabitatInfo.Font = new Font("Verdana", 8f, FontStyle.Regular);
            pnlColonyHabitatInfo.Reset();
            UpdateSplashProgress();
            pnlBuiltObjectDetail.Size = new Size(240, 240);
            pnlBuiltObjectDetail.Location = new Point(20, mainView.Size.Height - (pnlDetailInfo.Size.Height + 20));
            pnlBuiltObjectDetail.Font = new Font("Verdana", 8f, FontStyle.Regular);
            pnlBuiltObjectDetail.ShowExtendedInfo = true;
            pnlBuiltObjectDetail.Reset();
            ctlEditHabitatPopulation.InitializeImages(raceImageCache_0.GetRaceImages());
            BuiltObjectListView.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), bitmap_53);
            ctlBuiltObjectList.Kickstart(allowMultiselect: true);
            ctlConstructionYardWaitQueue.Kickstart(allowMultiselect: false);
            ctlDockingWaitQueue.Kickstart(allowMultiselect: false);
            ctlColonyDockingBayWaitQueue.Kickstart(allowMultiselect: false);
            ctlColonyConstructionYardWaitQueue.Kickstart(allowMultiselect: false);
            ctlEditEmpireBuiltObjectList.Kickstart(allowMultiselect: false);
            cmbExpansionPlannerAvailableBuiltObjects.InitializeImages(builtObjectImageCache_0.GetImagesSmall());
            ctlCharacterSummary.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall());
            cmbEditBuiltObjectFleet.InitializeImages(builtObjectImageCache_0.GetImagesSmall());
            cmbTroopFilter.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall());
            pnlColonyInvasionContainer.InitializeImages(bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27, bitmap_8, builtObjectImageCache_0, characterImageCache_0, raceImageCache_0.GetRaceImages(), bitmap_19, bitmap_103, bitmap_104, bitmap_16[0], bitmap_16[1], bitmap_16[2], bitmap_16[3], Application.StartupPath, string.Empty);
            int num6 = mainView.Size.Height - (pnlDetailInfo.Height + 45) + 1;
            btnCycleColoniesBack.Location = new Point(10, num6 + 30);
            btnCycleColoniesBack.BringToFront();
            btnCycleBasesBack.Location = new Point(10, num6 + 60);
            btnCycleBasesBack.BringToFront();
            btnCycleMilitaryBack.Location = new Point(10, num6 + 90);
            btnCycleMilitaryBack.BringToFront();
            btnCycleConstructionBack.Location = new Point(10, num6 + 120);
            btnCycleConstructionBack.BringToFront();
            btnCycleOtherBack.Location = new Point(10, num6 + 150);
            btnCycleOtherBack.BringToFront();
            btnCycleShipGroupsBack.Location = new Point(10, num6 + 180);
            btnCycleShipGroupsBack.BringToFront();
            btnCycleIdleShipsBack.Location = new Point(10, num6 + 210);
            btnCycleIdleShipsBack.BringToFront();
            btnSelectionPanelSize.Location = new Point(353, num6 - 36);
            btnSelectionPanelSize.BringToFront();
            btnCycleShipStance.BringToFront();
            btnSelectNearestMilitary.Location = new Point(353, num6);
            btnSelectNearestMilitary.BringToFront();
            btnCycleColonies.Location = new Point(353, num6 + 30);
            btnCycleColonies.BringToFront();
            btnCycleBases.Location = new Point(353, num6 + 60);
            btnCycleBases.BringToFront();
            btnCycleMilitary.Location = new Point(353, num6 + 90);
            btnCycleMilitary.BringToFront();
            btnCycleConstruction.Location = new Point(353, num6 + 120);
            btnCycleConstruction.BringToFront();
            btnCycleOther.Location = new Point(353, num6 + 150);
            btnCycleOther.BringToFront();
            btnCycleShipGroups.Location = new Point(353, num6 + 180);
            btnCycleShipGroups.BringToFront();
            btnCycleIdleShips.Location = new Point(353, num6 + 210);
            btnCycleIdleShips.BringToFront();
            btnCycleShipStance.Location = new Point(353, num6 + 240);
            btnCycleShipStance.Size = new Size(56, 28);
            btnLockView.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleColoniesBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleBasesBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleMilitaryBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleConstructionBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleOtherBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleShipGroupsBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnCycleIdleShipsBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnSelectNearestMilitary.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleShipStance.SetCornerCurves(topLeft: true, topRight: true, bottomRight: true, bottomLeft: true);
            btnCycleColonies.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleBases.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleMilitary.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleConstruction.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleOther.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleShipGroups.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnCycleIdleShips.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnSelectionAction1.Size = new Size(35, 28);
            btnSelectionAction2.Size = new Size(35, 28);
            btnSelectionAction3.Size = new Size(35, 28);
            btnSelectionAction4.Size = new Size(35, 28);
            btnSelectionAction5.Size = new Size(35, 28);
            btnSelectionAction6.Size = new Size(35, 28);
            btnSelectionAction7.Size = new Size(35, 28);
            btnSelectionAction8.Size = new Size(35, 28);
            int num7 = pnlInfoPanel.Bottom + 2;
            btnSelectionAction1.Location = new Point(70, num7);
            btnSelectionAction2.Location = new Point(105, num7);
            btnSelectionAction3.Location = new Point(140, num7);
            btnSelectionAction4.Location = new Point(175, num7);
            btnSelectionAction5.Location = new Point(210, num7);
            btnSelectionAction6.Location = new Point(245, num7);
            btnSelectionAction7.Location = new Point(280, num7);
            btnSelectionAction8.Location = new Point(315, num7);
            btnSelectionAction1.BringToFront();
            btnSelectionAction2.BringToFront();
            btnSelectionAction3.BringToFront();
            btnSelectionAction4.BringToFront();
            btnSelectionAction5.BringToFront();
            btnSelectionAction6.BringToFront();
            btnSelectionAction7.BringToFront();
            btnSelectionAction8.BringToFront();
            btnSelectionAction1.Visible = true;
            btnSelectionAction2.Visible = true;
            btnSelectionAction3.Visible = true;
            btnSelectionAction4.Visible = true;
            btnSelectionAction5.Visible = true;
            btnSelectionAction6.Visible = true;
            btnSelectionAction7.Visible = true;
            btnSelectionAction8.Visible = true;
            btnSelectionBack.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnSelectionForward.SetCornerCurves(topLeft: false, topRight: true, bottomRight: true, bottomLeft: false);
            btnSelectionBack.Size = new Size(138, 28);
            btnSelectionForward.Size = new Size(138, 28);
            btnSelectionBack.Location = new Point(71, num6 - 36);
            btnSelectionBack.BringToFront();
            btnSelectionForward.Location = new Point(213, num6 - 36);
            btnSelectionForward.BringToFront();
            method_212();
            btnLockView.Location = new Point(10, num6);
            btnLockView.BringToFront();
            pnlSystemMap.Width = 330;
            pnlSystemMap.Height = 290;
            picSystem.Width = 280;
            picSystem.Height = 280;
            mainView.HoverMessageLocation = new Point(10, mainView.Size.Height - (pnlInfoPanel.Height + btnSelectionForward.Height + btnSelectionAction1.Height + 4 + 35));
            mainView.HoverMessageLocationMap = new Point(mainView.Size.Width - (pnlSystemMap.Width + 10), mainView.Height - (pnlSystemMap.Height + 64));
            mainView.HoverMessageLocationButtons = new Point(mainView.Size.Width / 2, num2 + num + 10 + 5);
            num6 = mainView.Size.Height - (pnlSystemMap.Height + 7) + 1;
            btnZoomSelection.Location = new Point(mainView.Width - (picSystem.Width + btnZoomSelection.Width + 17), num6);
            btnZoomSelection.BringToFront();
            btnZoomIn.Location = new Point(mainView.Width - (picSystem.Width + btnZoomIn.Width + 17), num6 + 30);
            btnZoomIn.BringToFront();
            btnZoomOut.Location = new Point(mainView.Width - (picSystem.Width + btnZoomOut.Width + 17), num6 + 60);
            btnZoomOut.BringToFront();
            btnZoomColony.Location = new Point(mainView.Width - (picSystem.Width + btnZoomColony.Width + 17), num6 + 90);
            btnZoomColony.BringToFront();
            btnZoomSystem.Location = new Point(mainView.Width - (picSystem.Width + btnZoomSystem.Width + 17), num6 + 120);
            btnZoomSystem.BringToFront();
            jQaYpdpkDs.Location = new Point(mainView.Width - (picSystem.Width + jQaYpdpkDs.Width + 17), num6 + 150);
            jQaYpdpkDs.BringToFront();
            btnZoomRegion.Location = new Point(mainView.Width - (picSystem.Width + btnZoomRegion.Width + 17), num6 + 180);
            btnZoomRegion.BringToFront();
            tbtnGalaxyMap.Size = new Size(btnZoomColony.Width, 40);
            tbtnGalaxyMap.Location = new Point(mainView.Width - (picSystem.Width + btnZoomRegion.Width + 17), num6 + 240);
            tbtnGalaxyMap.BringToFront();
            btnZoomSelection.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnZoomIn.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnZoomOut.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnZoomColony.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnZoomSystem.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            jQaYpdpkDs.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            btnZoomRegion.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            tbtnGalaxyMap.SetCornerCurves(topLeft: true, topRight: false, bottomRight: false, bottomLeft: true);
            pnlSystemMap.Location = new Point(mainView.Size.Width - (pnlSystemMap.Size.Width + 10), mainView.Size.Height - (pnlSystemMap.Size.Height + 10));
            picSystem.Location = new Point(45, 5);
            picSystem.BringToFront();
            btnMapCivilianFade.Size = new Size(35, 28);
            btnMapCivilianFade.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay1.Size = new Size(35, 28);
            btnMapOverlay1.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay2.Size = new Size(35, 28);
            btnMapOverlay2.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay3.Size = new Size(35, 28);
            btnMapOverlay3.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay4.Size = new Size(35, 28);
            btnMapOverlay4.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay5.Size = new Size(35, 28);
            btnMapOverlay5.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay6.Size = new Size(35, 28);
            btnMapOverlay6.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay7.Size = new Size(35, 28);
            btnMapOverlay7.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapOverlay8.Size = new Size(35, 28);
            btnMapOverlay8.SetCornerCurves(topLeft: false, topRight: false, bottomRight: false, bottomLeft: false);
            btnMapCivilianFade.ClipBackground = false;
            btnMapCivilianFade.IntensifyColors = true;
            btnMapOverlay1.ClipBackground = false;
            btnMapOverlay1.IntensifyColors = true;
            btnMapOverlay2.ClipBackground = false;
            btnMapOverlay2.IntensifyColors = true;
            btnMapOverlay3.ClipBackground = false;
            btnMapOverlay3.IntensifyColors = true;
            btnMapOverlay4.ClipBackground = false;
            btnMapOverlay4.IntensifyColors = true;
            btnMapOverlay5.ClipBackground = false;
            btnMapOverlay5.IntensifyColors = true;
            btnMapOverlay6.ClipBackground = false;
            btnMapOverlay6.IntensifyColors = true;
            btnMapOverlay7.ClipBackground = false;
            btnMapOverlay7.IntensifyColors = true;
            btnMapOverlay8.ClipBackground = false;
            btnMapOverlay8.IntensifyColors = true;
            int num8 = mainView.Size.Width - (pnlSystemMap.Size.Width + 10) + 11;
            int num9 = mainView.Size.Height - (pnlSystemMap.Size.Height + 10) - 29;
            btnMapCivilianFade.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay1.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay2.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay3.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay4.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay5.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay6.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay7.Location = new Point(num8, num9);
            num8 += 35;
            btnMapOverlay8.Location = new Point(num8, num9);
            num8 += 35;
            btnMapCivilianFade.Image = bitmap_99;
            btnMapOverlay1.Image = bitmap_98;
            btnMapOverlay2.Image = bitmap_96;
            btnMapOverlay3.Image = bitmap_97;
            btnMapOverlay4.Image = bitmap_37;
            btnMapOverlay5.Image = bitmap_93;
            btnMapOverlay6.Image = bitmap_92;
            btnMapOverlay7.Image = bitmap_95;
            btnMapOverlay8.Image = bitmap_94;
            _ = (lstMessages.Size.Width - 336) / 2;
            _ = (mainView.Size.Width - lstMessages.Size.Width) / 2;
            _ = mainView.Size.Width / 2;
            lblStarDate.Location = new Point(10, 10);
            lblStarDate.BackColor = Color.Black;
            lblSystemName.Location = new Point(10, 35);
            lblSystemName.BackColor = Color.Black;
            lblStateMoney.Location = new Point(mainView.Size.Width - 95, 10);
            lblStateMoney.BackColor = Color.Black;
            lblPrivateMoney.Location = new Point(mainView.Size.Width - 95, 35);
            lblPrivateMoney.BackColor = Color.Black;
            lblStarDate.Visible = false;
            lblSystemName.Visible = false;
            lblStateMoney.Visible = false;
            lblPrivateMoney.Visible = false;
            lblGodData.Location = new Point(10, 180);
            if (_Game != null)
            {
                if (_Game.GodMode)
                {
                    method_272();
                }
                else
                {
                    method_273();
                }
            }
            else
            {
                method_273();
            }
            graphics_1 = picSystem.CreateGraphics();
            graphics_1.InterpolationMode = InterpolationMode.High;
            graphics_2 = picSystemMap.CreateGraphics();
            graphics_2.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_2.InterpolationMode = InterpolationMode.High;
            int_35 = Galaxy.MaxSolarSystemSize / picSystem.ClientRectangle.Width;
            mainView.nebulaCloudGenerator_1.GenerateNebulaBackdrop(1, out var cloudImages, out var _, 255, 26, 200, 200, transparentBackground: true, isGasCloud: true, useLowQuality: false);
            bitmap_4 = PrecacheScaledBitmap(cloudImages[0], 40, 40, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            for (int i = 0; i < cloudImages.Count; i++)
            {
                cloudImages[i].Dispose();
            }
            hoverPanel_0 = new HoverPanel();
            hoverPanel_0.SetFontCache(this);
            hoverPanel_0.InitializeImages(this, builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), bitmap_10, raceImageCache_0.GetRaceImages(), _uiResourcesBitmaps);
            int num10 = 260;
            if (rectangle.Width <= 1024)
            {
                rectangle_0 = new Rectangle((rectangle.Width - num10) / 2 + 28, rectangle.Height - 135, num10, 130);
            }
            else
            {
                rectangle_0 = new Rectangle((rectangle.Width - num10) / 2, rectangle.Height - 135, num10, 130);
            }
            pnlGameRaceVictoryConditions.InitializeImages(raceImageCache_0);
            pnlGameSummary.InitializeImages(bitmap_216, bitmap_217, bitmap_218, bitmap_219, bitmap_220, bitmap_221, bitmap_222, bitmap_223);
            UpdateSplashProgress();
            diplomaticMessageQueue_0 = new DiplomaticMessageQueue();
            diplomaticMessageQueue_0.SetFontCache(this);
            Rectangle area = new Rectangle(rectangle.Width - 308, 90, 300, 200);
            if (rectangle.Height > 768)
            {
                int num11 = Math.Min(7, 4 + (rectangle.Height - 768) / 50);
                area = new Rectangle(rectangle.Width - 308, 90, 300, num11 * 50);
            }
            diplomaticMessageQueue_0.Initialize(this, area);
            diplomaticMessageQueue_0.InitializeImages(raceImageCache_0, bitmap_72, bitmap_51, bitmap_37, bitmap_52, bitmap_73, bitmap_55, bitmap_81, bitmap_71, bitmap_82, bitmap_85, bitmap_28[28], bitmap_84, bitmap_49, bitmap_87);
            itemListCollectionPanel_0 = new ItemListCollectionPanel();
            itemListCollectionPanel_0.SetFontCache(this);
            Rectangle area2 = new Rectangle(8, 150, 300, 265);
            if (rectangle.Height > 768)
            {
                int num12 = Math.Min(10, 5 + (rectangle.Height - 793) / 43);
                area2 = new Rectangle(8, 175, 300, 50 + num12 * 43);
            }
            if (rectangle.Height > 800)
            {
                itemListCollectionPanel_0.SelectionButtonHeight = 26;
                itemListCollectionPanel_0.SelectionButtonWidth = 26;
            }
            else
            {
                itemListCollectionPanel_0.SelectionButtonHeight = 20;
                itemListCollectionPanel_0.SelectionButtonWidth = 20;
            }
            itemListCollectionPanel_0.Initialize(this, area2);
            itemListCollectionPanel_0.InitializeImages(builtObjectImageCache_0.GetImagesSmall(), habitatImageCache_0.GetImagesSmall(), raceImageCache_0, _uiResourcesBitmaps, bitmap_8, bitmap_79, bitmap_80, bitmap_128, bitmap_129, bitmap_130, bitmap_131, bitmap_132, bitmap_126, bitmap_127, bitmap_28[30]);
            method_666();
            List<Bitmap> list = method_396();
            tbtnResearch.InitializeImages(bitmap_21, bitmap_6, list.ToArray(), bitmap_8, bitmap_78, bitmap_83, bitmap_23, bitmap_24, bitmap_25, bitmap_26);
            pnlResearchTree.InitializeImages(bitmap_21, bitmap_6, list.ToArray(), raceImageCache_0.GetRaceImages(), bitmap_8, bitmap_78, bitmap_38, bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_9);
            solidBrush_0 = new SolidBrush(Color.Transparent);
            solidBrush_1 = new SolidBrush(color_3);
            pen_0 = new Pen(color_4, 1f);
            pen_1 = new Pen(color_5, 1f);
            pen_2 = new Pen(color_7, 1f);
            pen_3 = new Pen(color_9, 3f);
            pen_4 = new Pen(color_10, 2f);
            pen_5 = new Pen(color_11, 2f);
            pen_6 = new Pen(color_7, 2f);
            pen_6.DashStyle = DashStyle.Dash;
            solidBrush_2 = new SolidBrush(color_6);
            solidBrush_3 = new SolidBrush(color_8);
            ctlColonyFacilities.InitializeImages(bitmap_8);
            ctlEditHabitatPlanetaryFacilities.InitializeImages(bitmap_8);
            CharacterTroopListIconView.InitializeImages(bitmap_23, bitmap_24, bitmap_25, bitmap_26, bitmap_27);
            ctlBuiltObjectCharactersTroops.Kickstart();
            ctlColonyCharacterTroops.Kickstart();
            HabitatListView.InitializeImages(habitatImageCache_0.GetImagesSmall(), bitmap_8, bitmap_44, bitmap_43, bitmap_33, bitmap_34, bitmap_35, bitmap_36);
            pnlEmpireDetailInfo.InitializeImages(raceImageCache_0);
            pnlEmpireDetailInfo.Kickstart(base.ClientSize);
            ctlBuiltObjectComponents.InitializeImages(bitmap_21, bitmap_22);
            ctlColonyPopulation.InitializeImages(raceImageCache_0.GetRaceImages());
            pnlEditEmpireBuiltObjectInfo.Reset();
            pnlEditEmpireColonyInfo.Reset();
            ctlExpansionPlannerTargets.InitializeImages(habitatImageCache_0.GetImagesSmall(), builtObjectImageCache_0.GetImagesSmall(), raceImageCache_0.GetRaceImages(), _uiResourcesBitmaps, bitmap_2, bitmap_4);
            LoadUiChromeButtons(text, string.Empty);
            UnlxwvByxj.SelectionChanged += UnlxwvByxj_SelectionChanged;
            ctlBuiltObjectList.SelectionChanged += ctlBuiltObjectList_SelectionChanged;
            ctlBuiltObjectComponents.SelectionChanged += ctlBuiltObjectComponents_SelectionChanged;
            ctlEmpireDiplomaticRelationList.SelectionChanged += ctlEmpireDiplomaticRelationList_SelectionChanged;
            ctlShipGroupListView.SelectionChanged += ctlShipGroupListView_SelectionChanged;
            ctlTroopList.SelectionChanged += ctlTroopList_SelectionChanged;
            ctlDesignsList.SelectionChanged += ctlDesignsList_SelectionChanged;
            ctlCharacterSummary.CharacterNameChanged += ctlCharacterSummary_CharacterNameChanged;
            ctlCharacterSummary.CharacterTransferInitiated += ctlCharacterSummary_CharacterTransferInitiated;
            pnlCharacterMission.MissionAssigned += pnlCharacterMission_MissionCancelled;
            pnlCharacterMission.MissionCancelled += pnlCharacterMission_MissionCancelled;
            pnlCharacterMission.MissionTypeHelp += pnlCharacterMission_MissionTypeHelp;
            diplomaticMessageQueue_0.MessageClicked += method_79;
            itemListCollectionPanel_0.ItemClicked += method_78;
            itemListCollectionPanel_0.BindItemPanel += method_75;
            itemListCollectionPanel_0.ToggleButtonClicked += method_77;
            pnlResearchTree.NodeClicked += method_397;
            CaLkaMyrMQ.Visible = false;
            CaLkaMyrMQ.SendToBack();
            pnlEncyclopedia.Visible = false;
            pnlEncyclopedia.SendToBack();
            List<KeyValuePair<string, int>> list2 = new List<KeyValuePair<string, int>>();
            ResourceList resourceList = new ResourceList();
            foreach (ResourceDefinition resource in Galaxy.ResourceSystemStatic.Resources)
            {
                if (!resourceList.Contains(new Resource(resource.ResourceID)))
                {
                    list2.Add(new KeyValuePair<string, int>(resource.Name, resource.ResourceID));
                }
            }
            list2.Sort((KeyValuePair<string, int> obj1, KeyValuePair<string, int> obj2) => obj1.Key.CompareTo(obj2.Key));
            omjYcxcvXH.DisplayMember = "Key";
            omjYcxcvXH.ValueMember = "Value";
            foreach (KeyValuePair<string, int> item in list2)
            {
                omjYcxcvXH.Items.Add(item);
            }
            cmbGalaxyMapHabitatType.Items.Add("(" + TextResolver.GetText("All Types") + ")");
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.Continental));
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.MarshySwamp));
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.Desert));
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.Ocean));
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.Ice));
            cmbGalaxyMapHabitatType.Items.Add(Galaxy.ResolveDescription(HabitatType.Volcanic));
            method_80(this);
            string text2 = Application.StartupPath + "\\sounds\\effects\\";
            string text3 = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                text3 = Application.StartupPath + "\\Customization\\" + text + "\\sounds\\effects\\";
            }
            if (!string.IsNullOrEmpty(text3) && File.Exists(text3 + "button1.wav"))
            {
                CloseButton.SetSoundLocation(text3 + "button1.wav");
            }
            else
            {
                CloseButton.SetSoundLocation(text2 + "button1.wav");
            }
            method_18(text2, text3, this);
            method_17(text2, text3);
            ctlEmpireDiplomaticRelationList.EmpireListView.SoundsEnabled = true;
            ctlBuiltObjectList.SoundsEnabled = true;
            ctlCharacterList.SoundsEnabled = true;
            UnlxwvByxj.SoundsEnabled = true;
            ctlDesignComponents.SoundsEnabled = true;
            ctlDesignComponentToolbox.SoundsEnabled = true;
            ctlDesignsList.SoundsEnabled = true;
            ctlIntelligenceAgents.SoundsEnabled = true;
            ctlCharacterEvents.SoundsEnabled = true;
            ctlMessageHistoryMessages.SoundsEnabled = true;
            ctlResearchFacilities.SoundsEnabled = true;
            ctlShipGroupListView.SoundsEnabled = true;
            ctlTroopList.SoundsEnabled = true;
            GenerateAutomationMessageBox(TextResolver.GetText("Troop Recruitment"));
            GenerateAutomationMessageBox(TextResolver.GetText("Agent Recruitment"));
            GenerateAutomationMessageBox(TextResolver.GetText("Agent Assignment"));
            GenerateAutomationMessageBox(TextResolver.GetText("Colony Tax Rates"));
            GenerateAutomationMessageBox(TextResolver.GetText("Colonization"));
            GenerateAutomationMessageBox(TextResolver.GetText("Ship Design"));
            GenerateAutomationMessageBox(TextResolver.GetText("Ship Building"));
            GenerateAutomationMessageBox(TextResolver.GetText("Fleet Formation"));
            GenerateAutomationMessageBox(TextResolver.GetText("Diplomatic Gifts"));
            GenerateAutomationMessageBox(TextResolver.GetText("Treaty Negotiation"));
            GenerateAutomationMessageBox(TextResolver.GetText("Wars and Trade Sanctions"));
            GenerateAutomationMessageBox(TextResolver.GetText("Attacks on Enemies"));
            bool flag = false;
            if (gameOptions_0 == null)
            {
                method_260();
                flag = true;
            }
            if (gameOptions_0.StartGameOptions == null)
            {
                gameOptions_0.StartGameOptions = method_259();
                flag = true;
            }
            string path2 = GetGameFilesFolderCreateIfNeeded() + "defaultOptions";
            if (!File.Exists(path2))
            {
                flag = true;
            }
            if (flag)
            {
                method_257();
            }
            BiEtkpwdtg(gameOptions_0);
            string filename = GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
            MessageBoxExManager.ReadSavedResponses(filename);
            SastWuBaXc(gameOptions_0);
            string folder = Application.StartupPath + "\\sounds\\music\\";
            string themeMusic = "DistantWorldsTheme.mp3";
            method_68(gameOptions_0.CustomizationSetName, bool_28: false);
            musicPlayer_1 = new MusicPlayer(this, folder, themeMusic);
            //Device device = new Device();
            //DirectSound device = new DirectSound();
            //device.SetCooperativeLevel(base.Handle, CooperativeLevel.Priority);
            //effectsPlayer_0 = new EffectsPlayer(this, Application.StartupPath, text, device);
            effectsPlayer_0 = new EffectsPlayer(this, Application.StartupPath, text);
            SetMessagePopupPosition = pnlMessagePopup.SetPosition;
            timer_1.Elapsed += timer_1_Elapsed;
            SetMainFocus = WuVtIlwpRt;
            delegate1_0 = method_66;
            delegate2_0 = method_383;
            GameEndMessage = method_431;
            timer_2.Elapsed += timer_2_Elapsed;
            timer_0.Elapsed += timer_0_Elapsed;
            delegate0_0 = method_378;
            delegate3_0 = DoGameEnd;
        }

        private void method_75(object sender, ItemListCollectionPanel.BindItemPanelEventArgs e)
        {
            method_76(e.Panel);
        }

        private void method_76(ItemListPanel itemListPanel_0)
        {
            BaconMain.PopulateListsOnLefthandSide(this, itemListPanel_0);
        }

        private void method_77(object sender, ItemListCollectionPanel.BindItemPanelEventArgs e)
        {
            if (e.Panel != null)
            {
                method_76(e.Panel);
            }
        }

        private void method_78(object sender, ItemListCollectionPanel.ItemClickedEventArgs e)
        {
            if (e.Item is PrioritizedTarget)
            {
                PrioritizedTarget prioritizedTarget = (PrioritizedTarget)e.Item;
                ShipGroup shipGroup = null;
                if (_Game.SelectedObject != null && _Game.SelectedObject is ShipGroup)
                {
                    shipGroup = (ShipGroup)_Game.SelectedObject;
                }
                int missionQueueIndex = -1;
                ShipGroup shipGroup2 = itemListCollectionPanel_0.ResolveAssignedFleet(prioritizedTarget, out missionQueueIndex);
                if (e.ButtonClicked == MouseButtons.Left)
                {
                    if (shipGroup2 != null)
                    {
                        method_208(shipGroup2);
                        return;
                    }
                    if (shipGroup != null && shipGroup.Empire == _Game.PlayerEmpire)
                    {
                        if (shipGroup.Mission == null || shipGroup.Mission.Target != prioritizedTarget.Target || (shipGroup.Mission.Type != BuiltObjectMissionType.Attack && shipGroup.Mission.Type != BuiltObjectMissionType.Bombard && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndAttack && shipGroup.Mission.Type != BuiltObjectMissionType.WaitAndBombard))
                        {
                            shipGroup.AssignMission(BuiltObjectMissionType.Attack, prioritizedTarget.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                        }
                        return;
                    }
                    double num = 0.0;
                    double num2 = 0.0;
                    prioritizedTarget.ResolveTargetCoordinates(out num, out num2);
                    ShipGroup shipGroup3 = _Game.PlayerEmpire.IdentifyNearestAvailableFleet(num, num2, mustBeAutomated: false, mustBeWithinFuelRange: true, 0.0);
                    if (shipGroup3 == null)
                    {
                        shipGroup3 = _Game.PlayerEmpire.IdentifyNearestAvailableFleet(num, num2, mustBeAutomated: false, mustBeWithinFuelRange: false, 0.0);
                    }
                    shipGroup3?.AssignMission(BuiltObjectMissionType.Attack, prioritizedTarget.Target, null, BuiltObjectMissionPriority.High, manuallyAssigned: true);
                }
                else if (e.ButtonClicked == MouseButtons.Right && shipGroup2 != null && missionQueueIndex == 0)
                {
                    shipGroup2.ForceCompleteMission();
                }
                return;
            }
            if (e.Item is BuiltObject)
            {
                Keyboard keyboard = new Keyboard();
                if (keyboard.ShiftKeyDown)
                {
                    BuiltObject builtObject = (BuiltObject)e.Item;
                    BuiltObjectList builtObjectList = new BuiltObjectList();
                    if (builtObject != null && method_140(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                    if (builtObjectList.Count > 0)
                    {
                        if (_Game.SelectedObject != null && _Game.SelectedObject is BuiltObjectList)
                        {
                            BuiltObjectList builtObjectList2 = (BuiltObjectList)_Game.SelectedObject;
                            for (int i = 0; i < builtObjectList.Count; i++)
                            {
                                BuiltObject item = builtObjectList[i];
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
                            BuiltObject builtObject2 = (BuiltObject)_Game.SelectedObject;
                            if (method_140(builtObject2))
                            {
                                for (int j = 0; j < builtObjectList.Count; j++)
                                {
                                    BuiltObject builtObject3 = builtObjectList[j];
                                    if (builtObject2 == builtObject3)
                                    {
                                        builtObject2 = null;
                                    }
                                    else
                                    {
                                        builtObjectList3.Add(builtObject3);
                                    }
                                }
                                if (builtObject2 != null)
                                {
                                    builtObjectList3.Insert(0, builtObject2);
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
                }
            }
            if (e.Item is EmpireActivity)
            {
                EmpireActivity empireActivity = (EmpireActivity)e.Item;
                if (e.IsDoubleClick)
                {
                    method_157(empireActivity.Target);
                    method_4(1.0);
                }
                method_208(empireActivity.Target);
                if (e.BidButtonClicked)
                {
                    bool flag = false;
                    if (empireActivity.RequestingEmpire == _Game.PlayerEmpire)
                    {
                        switch (empireActivity.Type)
                        {
                            case EmpireActivityType.Attack:
                            case EmpireActivityType.Defend:
                                if (_Game.Galaxy.PirateMissions.ContainsEquivalent(empireActivity))
                                {
                                    flag = true;
                                }
                                break;
                            case EmpireActivityType.Smuggle:
                                flag = true;
                                break;
                        }
                    }
                    if (flag)
                    {
                        switch (empireActivity.Type)
                        {
                            case EmpireActivityType.Attack:
                            case EmpireActivityType.Defend:
                                if (empireActivity.AssignedEmpire != null)
                                {
                                    empireActivity.AssignedEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                                }
                                _Game.Galaxy.PirateMissions.RemoveEquivalent(empireActivity);
                                _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                                empireActivity.AssignedEmpire = null;
                                break;
                            case EmpireActivityType.Smuggle:
                                {
                                    _Game.Galaxy.RemovePirateSmugglingMissionFromAllEmpires(empireActivity);
                                    _Game.PlayerEmpire.PirateMissions.RemoveEquivalent(empireActivity);
                                    EmpireActivity firstByTargetAndType = _Game.Galaxy.PirateMissions.GetFirstByTargetAndType(empireActivity.Target, EmpireActivityType.Smuggle);
                                    if (firstByTargetAndType != null)
                                    {
                                        _Game.Galaxy.PirateMissions.Remove(firstByTargetAndType);
                                        if (firstByTargetAndType.RelatedOrder != null)
                                        {
                                            firstByTargetAndType.RelatedOrder.ExpiryDate = _Game.Galaxy.CurrentStarDate;
                                        }
                                    }
                                    break;
                                }
                        }
                    }
                    else
                    {
                        switch (empireActivity.Type)
                        {
                            case EmpireActivityType.Attack:
                            case EmpireActivityType.Defend:
                                empireActivity.AssignedEmpire = _Game.PlayerEmpire;
                                if (empireActivity.BidTimeRemaining < 0L)
                                {
                                    empireActivity.BidTimeRemaining = 60000L;
                                    break;
                                }
                                empireActivity.Price *= 0.9;
                                if (empireActivity.BidTimeRemaining < 10000L)
                                {
                                    empireActivity.BidTimeRemaining += 10000L;
                                }
                                break;
                            case EmpireActivityType.Smuggle:
                                if (!_Game.PlayerEmpire.PirateMissions.ContainsEquivalent(empireActivity))
                                {
                                    _Game.PlayerEmpire.PirateMissions.Add(empireActivity);
                                }
                                break;
                        }
                    }
                }
            }
            else
            {
                if (e.IsDoubleClick)
                {
                    method_157(e.Item);
                    method_4(1.0);
                }
                method_208(e.Item);
            }
            if (e.Item is GalaxyLocation)
            {
                GalaxyLocation galaxyLocation = (GalaxyLocation)e.Item;
                if (galaxyLocation.RelatedBuiltObject != null)
                {
                    method_208(galaxyLocation.RelatedBuiltObject);
                }
            }
            else if (e.Item is Character)
            {
                Character character = (Character)e.Item;
                if (character.Location != null)
                {
                    method_208(character.Location);
                }
            }
        }

        private void method_79(object sender, DiplomaticMessageQueue.MessageClickedEventArgs e)
        {
            if (_Game.Galaxy.TimeState != GalaxyTimeState.Paused)
            {
                method_154();
                bool_11 = true;
            }
            if (e.Message.MessageType == EmpireMessageType.AdvisorSuggestion)
            {
                if (e.Message.AdvisorMessageType == AdvisorMessageType.BuildOrder)
                {
                    method_628();
                }
                else
                {
                    method_649(e.Message);
                }
            }
            else
            {
                method_296(e.Message.Sender, e.ConversationOption);
            }
        }

        internal void SastWuBaXc(GameOptions gameOptions_1)
        {
            if (gameOptions_1 != null && mainView != null)
            {
                switch (gameOptions_1.SystemNebulaeDetail)
                {
                    case 0:
                        mainView.double_2 = 4.5;
                        break;
                    case 1:
                        mainView.double_2 = 2.9;
                        break;
                    case 2:
                        mainView.double_2 = 1.8;
                        break;
                    case 3:
                        mainView.double_2 = 1.0;
                        break;
                }
                mainView.ClearNebulaeImages();
            }
        }

        private void method_80(Control control_1)
        {
            control_1.TabStop = false;
            foreach (Control control in control_1.Controls)
            {
                method_80(control);
            }
        }

        private void WuVtIlwpRt()
        {
            Focus();
        }

        private void BiEtkpwdtg(GameOptions gameOptions_1)
        {
            if (gameOptions_1 != null)
            {
                btnMapCivilianFade.ToggledOn = gameOptions_1.MapOverlayFadeCivilianShips;
                btnMapOverlay1.ToggledOn = gameOptions_1.MapOverlayFleetPostures;
                btnMapOverlay2.ToggledOn = gameOptions_1.MapOverlayTravelVectorsState;
                btnMapOverlay3.ToggledOn = gameOptions_1.MapOverlayTravelVectorsPrivate;
                btnMapOverlay4.ToggledOn = gameOptions_1.MapOverlayPotentialColonies;
                btnMapOverlay5.ToggledOn = gameOptions_1.MapOverlayScenicLocations;
                btnMapOverlay6.ToggledOn = gameOptions_1.MapOverlayResearchLocations;
                btnMapOverlay7.ToggledOn = gameOptions_1.MapOverlayLongRangeScanners;
                btnMapOverlay8.ToggledOn = gameOptions_1.MapOverlayEmpireTerritory;
                if (gameOptions_0.MapOverlayFadeCivilianShips)
                {
                    btnMapCivilianFade.Hint = TextResolver.GetText("Fade civilian ships and bases") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapCivilianFade.Hint = TextResolver.GetText("Fade civilian ships and bases") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayFleetPostures)
                {
                    btnMapOverlay1.Hint = TextResolver.GetText("Show Fleet Postures") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay1.Hint = TextResolver.GetText("Show Fleet Postures") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayTravelVectorsState)
                {
                    btnMapOverlay2.Hint = TextResolver.GetText("Show Travel Vectors State") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay2.Hint = TextResolver.GetText("Show Travel Vectors State") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayTravelVectorsPrivate)
                {
                    btnMapOverlay3.Hint = TextResolver.GetText("Show Travel Vectors Private") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay3.Hint = TextResolver.GetText("Show Travel Vectors Private") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayPotentialColonies)
                {
                    btnMapOverlay4.Hint = TextResolver.GetText("Show Potential Colonies") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay4.Hint = TextResolver.GetText("Show Potential Colonies") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayScenicLocations)
                {
                    btnMapOverlay5.Hint = TextResolver.GetText("Show Scenic Locations") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay5.Hint = TextResolver.GetText("Show Scenic Locations") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayResearchLocations)
                {
                    btnMapOverlay6.Hint = TextResolver.GetText("Show Research Locations") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay6.Hint = TextResolver.GetText("Show Research Locations") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayLongRangeScanners)
                {
                    btnMapOverlay7.Hint = TextResolver.GetText("Show Long Range Scanners") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay7.Hint = TextResolver.GetText("Show Long Range Scanners") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                if (gameOptions_0.MapOverlayEmpireTerritory)
                {
                    btnMapOverlay8.Hint = TextResolver.GetText("Show Empire Territory") + "  (" + TextResolver.GetText("on").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
                else
                {
                    btnMapOverlay8.Hint = TextResolver.GetText("Show Empire Territory") + "  (" + TextResolver.GetText("off").ToUpper(CultureInfo.InvariantCulture) + ")";
                }
            }
        }

        public void Ignite()
        {
            Ignite(_Game, string_2);
        }

        public void Ignite(Game game, string currentFileName)
        {
            mainView.DisposePirateFlagTextures();
            method_3();
            _Game = game;
            string_2 = currentFileName;
            if (_Game != null && _Game.Galaxy != null)
            {
                Galaxy.SetGalaxyPhysicalDimensions(_Game.Galaxy.SectorWidth, _Game.Galaxy.SectorHeight);
                Galaxy.SetResearchCosts(_Game.Galaxy.BaseTechCost, _Game.Galaxy.ResearchNodeDefinitions);
                Galaxy.SetHyperDriveSpeeds(_Game.Galaxy.ResourceSystem, _Game.Galaxy.HyperdriveSpeedMultiplier, _Game.Galaxy.BaseTechCost, Application.StartupPath, _Game.CustomizationSetName);
                Galaxy.SetResearchRaceSpecialProjects(_Game.Galaxy.Races);
                Galaxy.SetResearchComponentMaxTechPoints(_Game.Galaxy.BaseTechCost);
                _Game.Galaxy.UpdateEmpireResearch();
            }
            bool_4 = false;
            method_90();
            if (_Game.PlayerEmpire != null)
            {
                _Game.PlayerEmpire.MessageRecipient = this;
                _Game.PlayerEmpire.AutomationAuthorizer = this;
                _Game.PlayerEmpire.EventMessageRecipient = this;
            }
            lstMessages.ClearItems();
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
            if (_Game.GodMode)
            {
                method_272();
            }
            else
            {
                method_273();
            }
            if (_Game.PlayerEmpire != null)
            {
                btnEmpireSummary.Image = PrecacheScaledBitmap(_Game.PlayerEmpire.LargeFlagPicture, 50, 30);
            }
            if (_Game.Galaxy != null)
            {
                _Game.Galaxy.SystemsUpdated += _Galaxy_SystemsUpdated;
                _Game.Galaxy.LocationPinged += method_365;
                _Game.Galaxy.GameEnd += Galaxy_GameEnd;
                _Game.Galaxy.RefreshView += _Galaxy_RefreshView;
                _Game.Galaxy.CharacterImageChanged += method_364;
            }
            Cursor.Current = cursor_0;
            Cursor = cursor_0;
            method_83();
            method_208(_Game.SelectedObject);
            musicPlayer_0.ForceSwitch();
        }

        private void method_81()
        {
            bool_9 = true;
            pnlIntroduction.Size = new Size(800, 665);
            pnlIntroduction.Location = new Point((mainView.Width - pnlIntroduction.Width) / 2, (mainView.Height - pnlIntroduction.Height) / 2);
            SizeF sizeF = SizeF.Empty;
            string text = TextResolver.GetText("Welcome to Your Empire");
            using (Graphics graphics = CreateGraphics())
            {
                sizeF = graphics.MeasureString(text, font_1);
            }
            lblIntroductionTitle.Location = new Point((pnlIntroduction.Width - (int)sizeF.Width) / 2, 10);
            lblIntroductionTitle.Text = text;
            lblIntroductionTitle.Font = font_1;
            pnlIntroductionBackground.BringToFront();
            pnlIntroductionBackground.Size = new Size(780, 565);
            pnlIntroductionBackground.Location = new Point(10, 45);
            picIntroductionFlag.Visible = false;
            picIntroductionRace.Size = new Size(280, 200);
            picIntroductionRace.Location = new Point(40, 15);
            lblIntroductionEmpireName.Location = new Point(350, 20);
            lblIntroductionEmpireDetails.Location = new Point(355, 42);
            lblIntroductionEmpireName.BringToFront();
            lblIntroductionEmpireDetails.BringToFront();
            lblIntroductionVictoryConditions.Location = new Point(350, 100);
            lblIntroductionVictoryConditions.MaximumSize = new Size(420, 115);
            lblIntroductionVictoryConditions.BringToFront();
            lblIntroductionConclusion.Location = new Point(272, 475);
            lblIntroductionConclusion.Visible = false;
            lblIntroductionPlaystyleIntro.Font = font_7;
            lblIntroductionWhatToDoPointsTitle.Font = font_7;
            lblIntroductionWhatToDoPoints.Font = font_6;
            picIntroductionPlaystyle.Location = new Point(130, 225);
            picIntroductionPlaystyle.Size = new Size(520, 210);
            lblIntroductionPlaystyleIntro.Location = new Point(0, 0);
            lblIntroductionPlaystyleIntro.Size = new Size(520, 210);
            lblIntroductionPlaystyleIntro.MaximumSize = new Size(520, 210);
            lblIntroductionPlaystyleIntro.TextAlign = ContentAlignment.MiddleCenter;
            lblIntroductionWhatToDoPointsTitle.Location = new Point(10, 415);
            lblIntroductionWhatToDoPoints.Location = new Point(20, 435);
            lblIntroductionWhatToDoPoints.Size = new Size(740, 100);
            lblIntroductionWhatToDoPoints.MaximumSize = new Size(740, 100);
            pnlIntroductionBackground.BackgroundImageLayout = ImageLayout.Stretch;
            lblIntroductionWhatToDoPointsTitle.Text = TextResolver.GetText("What You Should Do") + ":";
            picIntroductionPlaystyle.BackgroundImageLayout = ImageLayout.Center;
            if (pnlIntroductionBackground.BackgroundImage != null)
            {
                Image backgroundImage = pnlIntroductionBackground.BackgroundImage;
                pnlIntroductionBackground.BackgroundImage = null;
                backgroundImage.Dispose();
            }
            if (!string.IsNullOrEmpty(_Game.PlayerEmpire.Description))
            {
                lblIntroductionPlaystyleIntro.Text = _Game.PlayerEmpire.Description;
                lblIntroductionWhatToDoPointsTitle.Visible = false;
                lblIntroductionWhatToDoPoints.Text = string.Empty;
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat != null)
                {
                    pnlIntroductionBackground.BackgroundImage = method_376(bitmap_108, 0.1f);
                }
                else
                {
                    pnlIntroductionBackground.BackgroundImage = method_376(bitmap_109, 0.1f);
                }
            }
            else
            {
                lblIntroductionWhatToDoPointsTitle.Visible = true;
                if (_Game.PlayAsAPirate)
                {
                    if (_Game.AgeOfShadows)
                    {
                        lblIntroductionPlaystyleIntro.Text = TextResolver.GetText("Game Type Intro Pirate Shadows");
                        lblIntroductionWhatToDoPoints.Text = TextResolver.GetText("Game WhatToDo Pirate Shadows");
                        pnlIntroductionBackground.BackgroundImage = method_376(bitmap_106, 0.1f);
                    }
                    else
                    {
                        lblIntroductionPlaystyleIntro.Text = TextResolver.GetText("Game Type Intro Pirate Classic");
                        lblIntroductionWhatToDoPoints.Text = TextResolver.GetText("Game WhatToDo Pirate Classic");
                        pnlIntroductionBackground.BackgroundImage = method_376(bitmap_108, 0.1f);
                    }
                }
                else if (_Game.AgeOfShadows)
                {
                    lblIntroductionPlaystyleIntro.Text = TextResolver.GetText("Game Type Intro Normal Shadows");
                    lblIntroductionWhatToDoPoints.Text = TextResolver.GetText("Game WhatToDo Normal Shadows");
                    pnlIntroductionBackground.BackgroundImage = method_376(bitmap_107, 0.1f);
                }
                else
                {
                    lblIntroductionPlaystyleIntro.Text = TextResolver.GetText("Game Type Intro Normal Classic");
                    lblIntroductionWhatToDoPoints.Text = TextResolver.GetText("Game WhatToDo Normal Classic");
                    pnlIntroductionBackground.BackgroundImage = method_376(bitmap_109, 0.1f);
                }
            }
            lblIntroductionWhatToDoPoints.BringToFront();
            int num = (picIntroductionPlaystyle.Height - lblIntroductionPlaystyleIntro.Size.Height) / 2;
            lblIntroductionPlaystyleIntro.Location = new Point(0, num);
            btnIntroductionStart.Size = new Size(240, 35);
            btnIntroductionStart.Location = new Point(280, 620);
            btnIntroductionStart.Font = new Font(btnIntroductionStart.Font.FontFamily, 22.67f, FontStyle.Bold, GraphicsUnit.Pixel);
            btnIntroductionStart.Text = TextResolver.GetText("Start Playing");
            if (_Game != null && _Game.PlayerEmpire != null)
            {
                Bitmap image = new Bitmap(280, 200, PixelFormat.Format32bppPArgb);
                using (Graphics graphics2 = Graphics.FromImage(image))
                {
                    okQtJmsUqH(graphics2);
                    graphics2.DrawImage(destRect: new Rectangle(0, 65, 150, 90), srcRect: new Rectangle(0, 0, _Game.PlayerEmpire.LargeFlagPicture.Width, _Game.PlayerEmpire.LargeFlagPicture.Height), image: _Game.PlayerEmpire.LargeFlagPicture, srcUnit: GraphicsUnit.Pixel);
                    Bitmap empireDominantRaceImage = raceImageCache_0.GetEmpireDominantRaceImage(_Game.PlayerEmpire);
                    Rectangle destRect2 = new Rectangle(80, 0, 200, 200);
                    Rectangle srcRect2 = new Rectangle(0, 0, empireDominantRaceImage.Width, empireDominantRaceImage.Height);
                    graphics2.DrawImage(empireDominantRaceImage, destRect2, srcRect2, GraphicsUnit.Pixel);
                }
                picIntroductionRace.Image = image;
                lblIntroductionEmpireName.Text = _Game.PlayerEmpire.Name;
                lblIntroductionEmpireName.Font = font_2;
                lblIntroductionEmpireDetails.Font = font_6;
                string text2 = _Game.PlayerEmpire.DominantRace.Name;
                if (!text2.ToLower(CultureInfo.InvariantCulture).EndsWith("s"))
                {
                    text2 += "s";
                }
                HabitatList colonies = _Game.PlayerEmpire.Colonies;
                HabitatList habitatList = _Game.PlayerEmpire.DetermineEmpireSystems(_Game.PlayerEmpire);
                string empty = string.Empty;
                if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                {
                    empty = string.Format(TextResolver.GetText("You are ruler of"), text2, _Game.PlayerEmpire.GovernmentAttributes.Name, colonies.Count.ToString(), habitatList.Count.ToString());
                    lblIntroductionEmpireDetails.Text = empty;
                }
                else
                {
                    empty = string.Format(TextResolver.GetText("You are ruler of PIRATE"), text2, Galaxy.ResolveDescription(_Game.PlayerEmpire.PiratePlayStyle));
                    lblIntroductionEmpireDetails.Text = empty;
                }
                lblIntroductionVictoryConditions.Font = font_6;
                lblIntroductionVictoryConditions.Text = string.Format(TextResolver.GetText("Game Intro Victory"), _Game.GlobalVictoryConditions.VictoryThresholdPercentage.ToString("0%"));
                string text3 = string.Empty;
                if (_Game.GlobalVictoryConditions.EnableRaceSpecificVictoryConditions)
                {
                    if (_Game.PlayerEmpire.PirateEmpireBaseHabitat == null)
                    {
                        string text4 = " - " + string.Format(TextResolver.GetText("Game Intro Victory Race"), _Game.PlayerEmpire.DominantRace.Name);
                        text3 = text3 + text4 + "\n";
                    }
                    else
                    {
                        string text5 = " - " + string.Format(TextResolver.GetText("Game Intro Victory Pirate"), Galaxy.ResolveDescription(_Game.PlayerEmpire.PiratePlayStyle));
                        text3 = text3 + text5 + "\n";
                    }
                }
                if (_Game.GlobalVictoryConditions.Economy)
                {
                    string text6 = " - " + string.Format(TextResolver.GetText("Game Intro Victory Economy"), _Game.GlobalVictoryConditions.EconomyPercent.ToString("#0"));
                    text3 = text3 + text6 + "\n";
                }
                if (_Game.GlobalVictoryConditions.Population)
                {
                    string text7 = " - " + string.Format(TextResolver.GetText("Game Intro Victory Population"), _Game.GlobalVictoryConditions.PopulationPercent.ToString("#0"));
                    text3 = text3 + text7 + "\n";
                }
                if (_Game.GlobalVictoryConditions.Territory)
                {
                    string text8 = " - " + string.Format(TextResolver.GetText("Game Intro Victory Territory"), _Game.GlobalVictoryConditions.TerritoryPercent.ToString("#0"));
                    text3 = text3 + text8 + "\n";
                }
                if (_Game.GlobalVictoryConditions.TimeLimit)
                {
                    string text9 = string.Format(TextResolver.GetText("Game Intro Victory Time Limit"), Galaxy.ResolveStarDateDescription(_Game.GlobalVictoryConditions.TimeLimitDate));
                    text3 = text3 + text9 + "\n";
                    text3 = text3 + "    (" + TextResolver.GetText("Winner is the empire with the greatest strategic value at this time") + ")\n";
                }
                if (_Game.GlobalVictoryConditions.StartDate > 0L)
                {
                    string text10 = string.Format(TextResolver.GetText("To win - Start Date"), Galaxy.ResolveStarDateDescription(_Game.GlobalVictoryConditions.StartDate));
                    text3 = text3 + text10 + "\n";
                }
                if (string.IsNullOrEmpty(text3))
                {
                    text3 = TextResolver.GetText("SANDBOX MODE");
                }
                Label label = lblIntroductionVictoryConditions;
                label.Text = label.Text + "\n" + text3;
            }
            string text11 = TextResolver.GetText("Introduction Conclusion");
            SizeF sizeF2 = SizeF.Empty;
            using (Graphics graphics3 = CreateGraphics())
            {
                sizeF2 = graphics3.MeasureString(text11, font_7);
            }
            lblIntroductionConclusion.Location = new Point((pnlIntroductionBackground.Width - (int)sizeF2.Width) / 2, 475);
            lblIntroductionConclusion.Font = font_7;
            lblIntroductionConclusion.Text = text11;
            pnlIntroduction.Visible = true;
            pnlIntroduction.BringToFront();
        }

        private void method_82()
        {
            if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused)
            {
                btnPlayPause.Image = bitmap_45;
                method_155();
            }
            method_90();
            bool_9 = false;
            pnlIntroduction.Visible = false;
            pnlIntroduction.SendToBack();
        }

        public void Launch(bool launchFromLoad)
        {
            if (launchFromLoad)
            {
                if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused && !gameOptions_0.LoadedGamesPaused)
                {
                    method_155();
                }
                else if (gameOptions_0.LoadedGamesPaused && _Game.Galaxy.TimeState == GalaxyTimeState.Running)
                {
                    method_154();
                }
            }
            else if (_Game.Galaxy.TimeState == GalaxyTimeState.Paused)
            {
                btnPlayPause.Image = bitmap_45;
                method_155();
            }
            BaconMain.BaconInitialize(this);
            _ExpModMain.ModInitialize(this);
            method_90();
            method_262(gameOptions_0);
            pnlBuiltObjectInfo.Visible = false;
            pnlColonyInfo.Visible = false;
            pnlDesignDetail.Visible = false;
            pnlDesigns.Visible = false;
            pnlDiplomacyTalk.Visible = false;
            pnlEmpirePolicy.Visible = false;
            pnlBuildOrder.Visible = false;
            vHfFsoqMev.Visible = false;
            pnlEmpireInfo.Visible = false;
            pnlEmpireSummary.Visible = false;
            CaLkaMyrMQ.Visible = false;
            pnlGameEnd.Visible = false;
            pnlGameMenu.Visible = false;
            pnlGameOptions.Visible = false;
            kYdDyYeMls.Visible = false;
            pnlResearch.Visible = false;
            pnlSaveLoadProgress.Visible = false;
            pnlShipGroupInfo.Visible = false;
            pnlTroopInfo.Visible = false;
            pnlIntroduction.Visible = false;
            pnlTutorial.Visible = false;
            BringToFront();
            base.Visible = true;
            Focus();
            method_473();
            ProgramLoop();
            if ((!string.IsNullOrEmpty(string_3) && string.IsNullOrEmpty(gameOptions_0.CustomizationSetName)) || (!string.IsNullOrEmpty(string_3) && string_3 != gameOptions_0.CustomizationSetName) || (string.IsNullOrEmpty(string_3) && !string.IsNullOrEmpty(gameOptions_0.CustomizationSetName)))
            {
                method_66(gameOptions_0.CustomizationSetName, bool_28: false);
            }
            Hide();
            if (_Game != null && _Game.Galaxy != null)
            {
                _Game.Galaxy.SystemsUpdated -= _Galaxy_SystemsUpdated;
                _Game.Galaxy.LocationPinged -= method_365;
                _Game.Galaxy.GameEnd -= Galaxy_GameEnd;
                _Game.Galaxy.RefreshView -= _Galaxy_RefreshView;
                _Game.Galaxy.CharacterImageChanged -= method_364;
                _Game.PlayerEmpire.MessageRecipient = null;
                _Game.PlayerEmpire.AutomationAuthorizer = null;
                _Game.PlayerEmpire.EventMessageRecipient = null;
                method_3();
                method_64();
            }
            _Game = null;
        }

        public void ResetGalaxyBackdropsBackgroundThread()
        {
            ResetGalaxyBackdrops();
        }

        public void ResetGalaxyBackdropsInvoked(object state)
        {
            ResetGalaxyBackdrops();
        }

        public void ResetGalaxyBackdrops()
        {
            try
            {
                lock (object_4)
                {
                    bool_23 = true;
                    if (bitmap_177 == null || bitmap_177.PixelFormat == PixelFormat.Undefined || bitmap_177.Width != bitmap_178.Width || bitmap_177.Height != bitmap_178.Height)
                    {
                        bitmap_177 = PrecacheScaledBitmap(bitmap_178, bitmap_178.Width, bitmap_178.Height, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                    }
                    try
                    {
                        using Graphics graphics = Graphics.FromImage(bitmap_177);
                        GraphicsHelper.SetGraphicsQualityToLow(graphics);
                        graphics.DrawImageUnscaled(bitmap_178, 0, 0);
                    }
                    catch (ExternalException)
                    {
                    }
                    if (gameOptions_0.MapOverlayLongRangeScanners)
                    {
                        mainView.method_149(bitmap_177, ref bitmap_180, bitmap_188);
                    }
                    double num = Math.Max(10000.0, double_0);
                    double double_ = 1.0 + num / 10000.0;
                    if (!gameOptions_0.CleanGalaxyView)
                    {
                        mainView.method_148(ref bitmap_177, ref bitmap_181, _Game.Galaxy, double_);
                    }
                    if (mainView.bool_11 && mainView.bool_12)
                    {
                        if (texture2D_3 == null || texture2D_3.IsDisposed)
                        {
                            texture2D_3 = new Texture2D(mainView.GraphicsDevice, bitmap_177.Width, bitmap_177.Height, false, SurfaceFormat.Color);
                        }
                        if (texture2D_3.Width != bitmap_177.Width || texture2D_3.Height != bitmap_177.Height)
                        {
                            mainView.method_22(texture2D_3);
                            texture2D_3 = new Texture2D(mainView.GraphicsDevice, bitmap_177.Width, bitmap_177.Height, false, SurfaceFormat.Color);
                        }
                        if (uint_0 == null)
                        {
                            int num2 = bitmap_177.Width * bitmap_177.Height;
                            uint_0 = new uint[num2];
                        }
                        texture2D_5 = texture2D_3;
                        bool_24 = true;
                        XnaDrawingHelper.FastBitmapToTexture(mainView.GraphicsDevice, bitmap_177, ref texture2D_3, uint_0);
                        bool_24 = false;
                        mainView.method_22(texture2D_5);
                    }
                    if (bitmap_179 != null)
                    {
                        mainView.method_21(bitmap_179);
                    }
                    bitmap_179 = null;
                    if (FyKcynWgNv != null)
                    {
                        mainView.method_21(FyKcynWgNv);
                    }
                    FyKcynWgNv = null;
                    if (bitmap_185 != null)
                    {
                        mainView.method_21(bitmap_185);
                    }
                    bitmap_185 = null;
                    if (bitmap_186 != null)
                    {
                        mainView.method_21(bitmap_186);
                    }
                    bitmap_186 = null;
                    bool_23 = false;
                }
            }
            catch (Exception)
            {
            }
        }

        private void method_83()
        {
            dialogSet_0 = new DialogSet();
            string baseDialogPath = Application.StartupPath + "\\dialog\\";
            string text = Application.StartupPath + "\\Customization\\";
            if (!string.IsNullOrEmpty(_Game.CustomizationSetName) || !string.IsNullOrEmpty(gameOptions_0.CustomizationSetName))
            {
                string customizationSetName = gameOptions_0.CustomizationSetName;
                if (!string.IsNullOrEmpty(_Game.CustomizationSetName))
                {
                    customizationSetName = _Game.CustomizationSetName;
                }
                text = text + customizationSetName + "\\dialog\\";
            }
            dialogSet_0.Initialize(baseDialogPath, text, _Game.Galaxy.Races);
            if (bitmap_224 != null)
            {
                for (int i = 0; i < bitmap_224.Length; i++)
                {
                    if (bitmap_224[i] != null)
                    {
                        bitmap_224[i].Dispose();
                        bitmap_224[i] = null;
                    }
                }
            }
            characterImageCache_0.ClearAll();
            characterImageCache_0.Initialize(Application.StartupPath, _Game.CustomizationSetName, _Game.Galaxy.Races, raceImageCache_0.GetRaceImages(), bitmap_3);
            hoverPanel_0.Game = _Game;
            diplomaticMessageQueue_0.Reset();
            itemListCollectionPanel_0.Reset();
            tbtnResearch.Reset();
            tbtnResearch.BindData(_Game.PlayerEmpire);
            method_163();
            ChangeItemPanelSize(gameOptions_0.EmpireNavigationToolSize);
            switch (gameOptions_0.SelectionPanelSize)
            {
                case 0:
                    QxrIvWcaOp();
                    break;
                case 1:
                    method_665();
                    break;
            }
            mainView.Kickstart(this, _Game.Galaxy);
            lstMessages.KickStart(this, _Game.Galaxy, cursor_0);
            if (_Game.Galaxy.TimeSpeed <= 0.25)
            {
                btnGameSpeedDecrease.Enabled = false;
            }
            else
            {
                btnGameSpeedDecrease.Enabled = true;
            }
            if (_Game.Galaxy.TimeSpeed >= 4.0)
            {
                btnGameSpeedIncrease.Enabled = false;
            }
            else
            {
                btnGameSpeedIncrease.Enabled = true;
            }
            _Game.Galaxy.UpdateSystemInfo(_Game.PlayerEmpire);
            _Game.Galaxy.ReviewEmpireTerritory(onlySystems: false);
            GalaxyLocationList locations = null;
            if (bitmap_182 != null)
            {
                bitmap_182.Dispose();
            }
            bitmap_182 = _Game.Galaxy.GenerateNebulae(generateImage: true, bitmap_176, bitmap_205, out locations);
            if (bitmap_178 != null)
            {
                bitmap_178.Dispose();
            }
            bitmap_178 = new Bitmap(bitmap_176.Width, bitmap_176.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap_178);
            GraphicsHelper.SetGraphicsQualityToHigh(graphics);
            graphics.PageUnit = GraphicsUnit.Pixel;
            graphics.DrawImage(bitmap_176, new Rectangle(0, 0, bitmap_176.Width, bitmap_176.Height), new Rectangle(0, 0, bitmap_176.Width, bitmap_176.Height), GraphicsUnit.Pixel);
            graphics.DrawImage(bitmap_182, 0, 0);
            if (bitmap_177 != null)
            {
                bitmap_177.Dispose();
            }
            if (texture2D_3 != null)
            {
                texture2D_3.Dispose();
            }
            ResetGalaxyBackdrops();
            gmapMain.Ignite(this, _Game.Galaxy, bitmap_187, 0, 0, showNebulae: true);
            gmapColony.Ignite(this, _Game.Galaxy, bitmap_187, 0, 0);
            gmapBuiltObject.Ignite(this, _Game.Galaxy, bitmap_187, 1, 1);
            gmapShipGroupInfo.Ignite(this, _Game.Galaxy, bitmap_187, 1, 1);
            dboYnQplv3.Ignite(this, _Game.Galaxy, bitmap_187, 1, 1);
            gmapEmpireDetail.Ignite(this, _Game.Galaxy, bitmap_187, 0, 1);
            gmapMessageHistory.Ignite(this, _Game.Galaxy, bitmap_187, 1, 1);
            gmapExpansionPlanner.Ignite(this, _Game.Galaxy, bitmap_187, 1, 1);
            if (mainView.list_34 != null)
            {
                for (int j = 0; j < mainView.list_34.Count; j++)
                {
                    mainView.list_34[j]?.Dispose();
                }
            }
            mainView.list_34 = null;
            mainView.bool_0 = false;
            mainView.ClearPrecachedHabitatBitmaps();
            mainView.ClearPreprocessedBuiltObjectImages();
            mainView.ClearPreprocessedFighterImages();
            mainView.ClearPreprocessedCreatureImages();
            mainView.FadeGalaxyBackground();
            mainView.method_14(_Game.StarFieldSize);
            method_149();
            picSystem.GradientMode = DistantWorlds.Controls.LinearGradientMode.None;
            picSystem.Ignite(this, bitmap_182, bitmap_176, bitmap_187, _Game.Galaxy, relativeToView: true, int_28, int_35, drawViewIndicator: true, erasePrevious: true, clearFirst: false, showIndicatorLines: false, string.Empty);
            picSystem.ShowFleetPostures = true;
            picSystemMap.Extinguish();
            mainView.EventLocations.Clear();
            mainView.bool_0 = true;
            picSystem.ClearNebulae();
        }

        public void _Galaxy_SystemsUpdated(object sender, EventArgs e)
        {
        }

        public void Galaxy_GameEnd(object sender, GameEndEventArgs e)
        {
            Invoke(delegate3_0, e);
        }

        public void DoGameEnd(GameEndEventArgs e)
        {
            method_154();
            _Game.IsFinished = true;
            _Game.Victor = e.VictorEmpire;
            musicPlayer_0.StartTheme();
            method_436(e);
            if (e.Code == 1)
            {
                string string_ = TextResolver.GetText("You have Defeated the Shakturi!");
                string string_2 = _Game.Galaxy.GenerateMajorStoryVictoryMessage(e.OutcomeForPlayer);
                if (e.OutcomeForPlayer == GameEndOutcome.Defeat)
                {
                    string_ = TextResolver.GetText("The Shakturi have defeated the Freedom Alliance!");
                }
                method_571(string_, string_2);
            }
        }

        public void _Galaxy_RefreshView(object sender, RefreshViewEventArgs e)
        {
            if (!bool_5)
            {
                bool_5 = true;
                refreshViewEventArgs_0 = e;
            }
        }

        private void method_84()
        {
            if (bool_5 && refreshViewEventArgs_0 != null)
            {
                if (ProcessorCount > 1)
                {
                    ThreadPool.QueueUserWorkItem(method_85, null);
                }
                else
                {
                    method_85(null);
                }
            }
        }

        private void method_85(object object_7)
        {
            if (!bool_5 || refreshViewEventArgs_0 == null)
            {
                return;
            }
            RefreshViewEventArgs refreshViewEventArgs = refreshViewEventArgs_0;
            bool_25 = true;
            bool_5 = false;
            if (_Game != null && _Game.Galaxy != null)
            {
                try
                {
                    if (!refreshViewEventArgs.OnlyGalaxyBackdrops)
                    {
                        Habitat habitat = _Game.Galaxy.FastFindNearestSystem(refreshViewEventArgs.Xpos, refreshViewEventArgs.Ypos);
                        Habitat habitat2 = _Game.Galaxy.FastFindNearestSystem(int_13, int_14);
                        if (habitat2 != habitat)
                        {
                            lock (_Game.Galaxy._LockObject)
                            {
                                method_149();
                                mainView.ClearPrecachedHabitatBitmaps();
                                bool_20 = true;
                            }
                        }
                        else if (refreshViewEventArgs != null && refreshViewEventArgs.NewHabitats != null && refreshViewEventArgs.NewHabitats.Count > 0)
                        {
                            for (int i = 0; i < refreshViewEventArgs.NewHabitats.Count; i++)
                            {
                                mainView.list_5.Add(null);
                                mainView.list_6.Add(null);
                                mainView.list_7.Add(DateTime.MinValue);
                                mainView.list_8.Add(null);
                            }
                            int_29 += refreshViewEventArgs.NewHabitats.Count;
                        }
                        else
                        {
                            mainView.ClearPrecachedHabitatBitmaps();
                        }
                    }
                    ResetGalaxyBackdropsBackgroundThread();
                }
                catch (Exception)
                {
                }
            }
            bool_25 = false;
        }

        private void method_86(DateTime dateTime_7, long long_1, BuiltObjectList builtObjectList_1, bool bool_28)
        {
            if (bool_21)
            {
                return;
            }
            if (bool_28)
            {
                int_41 = 3000;
                int_41 = 1000;
                int_42 = 150;
                int_43 = 3000;
                int_43 = 1000;
                int_44 = 50;
                int_45 = 1;
                int_46 = 1;
                int_41 = Math.Min(int_41, _Game.Galaxy.Habitats.Count);
                int_43 = Math.Min(int_43, _Game.Galaxy.BuiltObjects.Count);
            }
            else
            {
                int_41 = 200;
                int_42 = 50;
                int_43 = 100;
                int_44 = 30;
                int_45 = 1;
                int_46 = 1;
            }
            _Game.Galaxy.DoTasksTimeSensitive(long_1, dateTime_7);
            if (int_54 % 100 == 0 && method_94(_Game.Galaxy))
            {
                int_54 = 0;
            }
            int_54++;
            if (_Game.Galaxy.GlobalVictoryConditions == null && _Game.GlobalVictoryConditions != null)
            {
                _Game.Galaxy.GlobalVictoryConditions = _Game.GlobalVictoryConditions;
            }
            Empire empire = _Game.Galaxy.IdentifyMechanoidEmpire();
            for (int i = 0; i < _Game.Galaxy.Empires.Count; i++)
            {
                Empire empire2 = _Game.Galaxy.Empires[i];
                if (empire2 != null)
                {
                    if (empire2 != _Game.PlayerEmpire)
                    {
                        empire2.WarnOfIncomingEnemyFleetsAndPlanetDestroyers(_Game.PlayerEmpire);
                    }
                    if (empire != null && empire2 != empire)
                    {
                        empire2.WarnOfIncomingEnemyFleetsAndPlanetDestroyers(empire);
                    }
                }
            }
            string_0 = "GxHab";
            if (_Game.Galaxy.Habitats.Count > 0)
            {
                HabitatList habitatList = new HabitatList();
                int num = int_48;
                for (int j = 0; j < int_41; j++)
                {
                    if (num >= _Game.Galaxy.Habitats.Count)
                    {
                        num = 0;
                    }
                    habitatList.Add(_Game.Galaxy.Habitats[num]);
                    num++;
                }
                if (habitatList.Count > 0)
                {
                    for (int k = 0; k < habitatList.Count; k++)
                    {
                        habitatList[k]?.DoTasks(dateTime_7);
                    }
                    int_48 = num;
                }
                else
                {
                    int_48 = num;
                }
            }
            string_0 = "GxFlt";
            if (_Game.Galaxy.Empires.Count > 0)
            {
                for (int l = 0; l < int_46; l++)
                {
                    if (int_55 >= _Game.Galaxy.Empires.Count)
                    {
                        int_55 = 0;
                    }
                    if (int_56 % 5 == 0)
                    {
                        Empire empire3 = _Game.Galaxy.Empires[int_55];
                        if (empire3 != null && empire3.ShipGroups != null && empire3.ShipGroups.Count > 0)
                        {
                            for (int m = 0; m < empire3.ShipGroups.Count; m++)
                            {
                                empire3.ShipGroups[m]?.DoTasks(dateTime_7);
                            }
                        }
                        int_56 = 0;
                        int_55++;
                    }
                    int_56++;
                }
            }
            string_0 = "GxBO";
            if (_Game.Galaxy.BuiltObjects.Count > 0)
            {
                BuiltObjectList builtObjectList = new BuiltObjectList();
                int num2 = int_49;
                int num3 = int_49;
                for (int n = 0; n < int_42; n++)
                {
                    if (num2 >= _Game.Galaxy.BuiltObjects.Count)
                    {
                        num2 = 0;
                    }
                    bool flag = false;
                    BuiltObject builtObject = null;
                    while (builtObject == null || !builtObject.InBattle)
                    {
                        num2++;
                        if (num2 >= _Game.Galaxy.BuiltObjects.Count)
                        {
                            num2 = 0;
                        }
                        if (num2 == num3)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                    {
                        break;
                    }
                    builtObject = _Game.Galaxy.BuiltObjects[num2];
                    if (builtObject != null && !builtObjectList_1.Contains(builtObject) && !builtObjectList.Contains(builtObject))
                    {
                        builtObjectList.Add(builtObject);
                    }
                    num2++;
                    if (num2 == num3)
                    {
                        break;
                    }
                }
                BuiltObject builtObject2 = null;
                int num4 = int_50;
                for (int num5 = 0; num5 < int_43; num5++)
                {
                    if (num4 >= _Game.Galaxy.BuiltObjects.Count)
                    {
                        num4 = 0;
                    }
                    builtObject2 = _Game.Galaxy.BuiltObjects[num4];
                    if (builtObject2 != null && !builtObjectList_1.Contains(builtObject2))
                    {
                        builtObjectList.Add(builtObject2);
                    }
                    num4++;
                }
                if (builtObjectList.Count > 0)
                {
                    for (int num6 = 0; num6 < builtObjectList.Count; num6++)
                    {
                        builtObjectList[num6]?.DoTasks(dateTime_7, long_1, inView: false);
                    }
                    int_49 = num2;
                    int_50 = num4;
                }
                else
                {
                    int_49 = num2;
                    int_50 = num4;
                }
            }
            string_0 = "GxCr";
            if (_Game.Galaxy.Creatures.Count > 0)
            {
                for (int num7 = 0; num7 < int_44; num7++)
                {
                    if (int_51 >= _Game.Galaxy.Creatures.Count)
                    {
                        int_51 = 0;
                    }
                    _Game.Galaxy.Creatures[int_51]?.DoTasks(dateTime_7);
                    int_51++;
                }
            }
            string_0 = "GxEm";
            if (_Game.Galaxy.Empires.Count > 0)
            {
                for (int num8 = 0; num8 < int_45; num8++)
                {
                    if (int_52 >= _Game.Galaxy.Empires.Count)
                    {
                        int_52 = 0;
                    }
                    if (int_53 % 10 == 0 && method_94(_Game.Galaxy.Empires[int_52]))
                    {
                        int_53 = 0;
                        int_52++;
                    }
                    int_53++;
                }
            }
            string_0 = "GxEmP";
            if (_Game.Galaxy.PirateEmpires.Count <= 0)
            {
                return;
            }
            for (int num9 = 0; num9 < int_47; num9++)
            {
                if (int_57 >= _Game.Galaxy.PirateEmpires.Count)
                {
                    int_57 = 0;
                }
                if (int_58 % 10 == 0 && method_94(_Game.Galaxy.PirateEmpires[int_57]))
                {
                    int_58 = 0;
                    int_57++;
                }
                int_58++;
            }
        }

        private void method_87()
        {
            Point pt = PointToClient(MouseHelper.GetCursorPosition());
            Control childAtPoint = GetChildAtPoint(pt, GetChildAtPointSkip.None);
            if (childAtPoint != null && childAtPoint.Name != "mainView")
            {
                Cursor = cursor_0;
            }
        }

        private void method_88()
        {
            bool flag = false;
            if (vqleEgFcoS.Length != int_0)
            {
                flag = true;
            }
            Thread[] array = vqleEgFcoS;
            foreach (Thread thread in array)
            {
                if (thread == null || !thread.IsAlive || thread.ThreadState == System.Threading.ThreadState.Aborted || thread.ThreadState == System.Threading.ThreadState.AbortRequested || thread.ThreadState == System.Threading.ThreadState.Stopped || thread.ThreadState == System.Threading.ThreadState.StopRequested)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                method_93(bool_28: true, 0);
                method_91(bool_28: true);
            }
        }

        private int method_89()
        {
            int num = 0;
            /*try
            {
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_Processor");
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                foreach (ManagementObject item in managementObjectCollection)
                {
                    int result = 0;
                    if (int.TryParse(item["NumberOfCores"].ToString(), out result))
                    {
                        num += result;
                    }
                }
                return num;
            }
            catch
            {
                return Environment.ProcessorCount;
            }*/
            return Environment.ProcessorCount;
        }

        private void method_90()
        {
            method_91(bool_28: false);
        }

        private void method_91(bool bool_28)
        {
            if (!bool_3)
            {
                bool_3 = true;
                int_0 = Math.Max(1, Environment.ProcessorCount - 1);
                vqleEgFcoS = new Thread[int_0];
                list_0 = new List<object>[int_0];
                object_0 = new object[int_0];
                for (int i = 0; i < int_0; i++)
                {
                    list_0[i] = new List<object>();
                    object_0[i] = new object();
                    Thread thread = new Thread(method_96);
                    thread.Priority = ThreadPriority.Normal;
                    vqleEgFcoS[i] = thread;
                    thread.Start(new object[2] { i, 10 });
                }
            }
        }

        private void method_92()
        {
            method_93(bool_28: false, 400);
        }

        private void method_93(bool bool_28, int int_64)
        {
            if (!bool_3 && !bool_28)
            {
                return;
            }
            bool_3 = false;
            Thread.Sleep(int_64);
            int millisecondsTimeout = 20000;
            if (int_64 > 0)
            {
                millisecondsTimeout = 30000;
            }
            Thread[] array = vqleEgFcoS;
            foreach (Thread thread in array)
            {
                if (thread != null && thread.IsAlive && thread.ThreadState != System.Threading.ThreadState.Aborted && thread.ThreadState != System.Threading.ThreadState.AbortRequested && thread.ThreadState != System.Threading.ThreadState.Stopped && thread.ThreadState != System.Threading.ThreadState.StopRequested && !thread.Join(millisecondsTimeout))
                {
                    thread.Abort();
                }
            }
            vqleEgFcoS = new Thread[1];
        }

        private bool method_94(object object_7)
        {
            int num = -1;
            int num2 = int.MaxValue;
            bool flag = false;
            for (int i = 0; i < int_0; i++)
            {
                object[] array;
                lock (object_0[i])
                {
                    array = ListHelper.ToArrayThreadSafe(list_0[i]);
                }
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] == object_7)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    flag = false;
                }
                else if (list_0[i].Count < num2)
                {
                    num2 = list_0[i].Count;
                    num = i;
                }
            }
            if (num >= 0)
            {
                lock (object_0[num])
                {
                    list_0[num].Add(object_7);
                    return true;
                }
            }
            return false;
        }

        private void method_95(object object_7)
        {
            int num = -1;
            int num2 = int.MaxValue;
            for (int i = 0; i < int_0; i++)
            {
                if (list_0[i].Count < num2)
                {
                    num2 = list_0[i].Count;
                    num = i;
                }
            }
            if (num >= 0)
            {
                lock (object_0[num])
                {
                    list_0[num].Add(object_7);
                }
            }
        }

        private void method_96(object object_7)
        {
            try
            {
                if (object_7 == null || !(object_7 is object[]))
                {
                    return;
                }
                object[] array = (object[])object_7;
                if (array.Length <= 0)
                {
                    return;
                }
                int num = (int)array[0];
                int millisecondsTimeout = (int)array[1];
                while (bool_3)
                {
                    if (list_0.Length <= num || object_0.Length <= num)
                    {
                        continue;
                    }
                    if (list_0[num].Count > 0)
                    {
                        object[][] array2 = new object[int_0][];
                        lock (object_0[num])
                        {
                            for (int i = 0; i < list_0.Length; i++)
                            {
                                List<object> list = list_0[i];
                                if (list != null)
                                {
                                    array2[i] = ListHelper.ToArrayThreadSafe(list);
                                }
                            }
                            list_0[num].Clear();
                        }
                        for (int j = 0; j < array2[num].Length; j++)
                        {
                            object obj = array2[num][j];
                            if (obj == null)
                            {
                                continue;
                            }
                            if (obj is string)
                            {
                                string text = (string)obj;
                                if (text == "ResetGalaxyBackdrops")
                                {
                                    ResetGalaxyBackdrops();
                                }
                            }
                            if (obj is Empire)
                            {
                                Empire empire = (Empire)obj;
                                if (empire.Active)
                                {
                                    empire.DoTasks();
                                }
                            }
                            else if (obj is Galaxy)
                            {
                                ((Galaxy)obj).DoTasks(_Game.IsFinished, _Game.PlayerEmpire, _Game.GlobalVictoryConditions, _Game.PlayerVictoryConditionsToAchieve, _Game.PlayerVictoryConditionsToPrevent);
                            }
                            else if (obj is HabitatList)
                            {
                                HabitatList habitatList = (HabitatList)obj;
                                DateTime currentDateTime = _Game.Galaxy.CurrentDateTime;
                                for (int k = 0; k < habitatList.Count; k++)
                                {
                                    habitatList[k]?.DoTasks(currentDateTime);
                                }
                            }
                            else if (obj is BuiltObjectList)
                            {
                                BuiltObjectList builtObjectList = (BuiltObjectList)obj;
                                DateTime currentDateTime2 = _Game.Galaxy.CurrentDateTime;
                                long currentStarDate = _Game.Galaxy.CurrentStarDate;
                                for (int l = 0; l < builtObjectList.Count; l++)
                                {
                                    builtObjectList[l]?.DoTasks(currentDateTime2, currentStarDate, inView: false);
                                }
                            }
                        }
                    }
                    Thread.Sleep(millisecondsTimeout);
                }
            }
            catch (Exception ex)
            {
                CrashDump(ex);
                throw;
            }
        }

        private void method_97()
        {
            if (gameOptions_0 == null || gameOptions_0.AutoSaveInterval <= 0 || pnlGameEditor.Visible || pnlGameEditorPassword.Visible || pnlGameOptions.Visible)
            {
                return;
            }
            if (dateTime_6 <= DateTime.MinValue)
            {
                dateTime_6 = DateTime.Now;
            }
            DateTime dateTime = dateTime_6.AddMinutes(Math.Max(1, gameOptions_0.AutoSaveInterval));
            if (!(DateTime.Now > dateTime))
            {
                return;
            }
            if (actionMenu.Visible)
            {
                actionMenu.Hide();
            }
            if (selectionMenu.Visible)
            {
                selectionMenu.Hide();
            }
            int_59++;
            if (int_59 > 5)
            {
                int_59 = 1;
            }
            string text = "Autosave" + int_59 + ".dwg";
            string text2 = GetGameSavesFolderCreateIfNeeded();
            if (gameOptions_0 != null && !string.IsNullOrEmpty(gameOptions_0.SaveGamePath))
            {
                text2 = gameOptions_0.SaveGamePath;
            }
            if (!Directory.Exists(text2))
            {
                try
                {
                    Directory.CreateDirectory(text2);
                }
                catch (Exception)
                {
                    string string_ = string.Format(TextResolver.GetText("Could not create save game folder"), text2);
                    MessageBoxEx messageBoxEx = method_371(string_, TextResolver.GetText("Could not set save game folder"), MessageBoxExIcon.Exclamation);
                    messageBoxEx.Show(this);
                    text2 = Application.ExecutablePath;
                }
            }
            string path = text2 + "\\" + text;
            bool flag = false;
            FileStream fileStream = new FileStream(path, FileMode.Create);
            if (fileStream != null)
            {
                Cursor.Current = Cursors.WaitCursor;
                galaxyTimeState_0 = _Game.Galaxy.TimeState;
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
            dateTime_6 = DateTime.Now;
        }

        public void ProgramLoop()
        {
            int_48 = 0;
            int_49 = 0;
            int_50 = 0;
            int_52 = 0;
            int_53 = 0;
            ProcessorCount = Environment.ProcessorCount;
            int num = 0;
            DateTime dateTime = DateTime.MinValue;
            double num2 = 5.0;
            int num3 = 0;
            DateTime value = DateTime.Now;
            DateTime value2 = DateTime.Now;
            bool bool_ = false;
            if (Environment.ProcessorCount > 1)
            {
                bool_ = true;
            }
            method_90();
            try
            {
                int upsCounter = 0;
                int ups = 0;
                DateTime upsLastTime = DateTime.Now;
                while (!bool_4)
                {
                    if (bool_7)
                    {
                        TutorialItemList tutorialItemList_ = new TutorialItemList();
                        switch (string_1)
                        {
                            case "PreWarpEmpire":
                                tutorialItemList_ = method_454("PreWarpEmpire.txt");
                                tutorialItemList_ = method_446(tutorialItemList_);
                                string_0 = "TutPreWarpEmpire";
                                break;
                            case "FindingYourWayAround":
                                tutorialItemList_ = method_454("FindingYourWayAround.txt");
                                tutorialItemList_ = method_451(tutorialItemList_);
                                string_0 = "TutFindingYourWayAround";
                                break;
                            case "EmpireAndColonies":
                                tutorialItemList_ = method_454("EmpireAndColonies.txt");
                                tutorialItemList_ = method_452(tutorialItemList_);
                                string_0 = "TutEmpireAndColonies";
                                break;
                            case "ShipsAndBases":
                                tutorialItemList_ = method_454("ShipsAndMissions.txt");
                                tutorialItemList_ = method_453(tutorialItemList_);
                                string_0 = "TutShipsAndBases";
                                break;
                            case "ExpansionDiplomacy":
                                tutorialItemList_ = method_454("ExpansionDiplomacy.txt");
                                tutorialItemList_ = method_448(tutorialItemList_);
                                string_0 = "TutExpDip";
                                break;
                            case "ResearchDesign":
                                tutorialItemList_ = method_454("ResearchDesign.txt");
                                tutorialItemList_ = method_449(tutorialItemList_);
                                string_0 = "TutResearchDesign";
                                break;
                            case "FleetsTroops":
                                tutorialItemList_ = method_454("FleetsTroops.txt");
                                tutorialItemList_ = method_450(tutorialItemList_);
                                string_0 = "TutFleetsTroops";
                                break;
                            case "DealingWithPirates":
                                tutorialItemList_ = method_454("DealingWithPirates.txt");
                                tutorialItemList_ = method_445(tutorialItemList_);
                                string_0 = "TutDealingWithPirates";
                                break;
                            case "PlayAsPirate":
                                tutorialItemList_ = method_454("PlayAsPirate.txt");
                                tutorialItemList_ = method_447(tutorialItemList_);
                                string_0 = "TutPirates";
                                break;
                        }
                        method_443(tutorialItemList_);
                        bool_7 = false;
                    }
                    DateTime now = DateTime.Now;
                    if (tutorial_0 != null && !tutorial_0.Finished && tutorial_0.CurrentStep != null && tutorial_0.CurrentStep.OpenScreen != null && now.Subtract(dateTime_5).TotalMilliseconds > 10000.0)
                    {
                        tutorial_0.CurrentStep.OpenScreen.Invalidate();
                        dateTime_5 = now;
                    }
                    string_0 = "HovMsg";
                    method_206();
                    method_87();
                    if (bool_3)
                    {
                        string_0 = "PQSFX";
                        method_1();
                        DateTime currentDateTime = _Game.Galaxy.CurrentDateTime;
                        long currentStarDate = _Game.Galaxy.CurrentStarDate;
                        BuiltObjectList builtObjectList = method_123();
                        string_0 = "ProcMn";
                        ProcessMain(currentDateTime, currentStarDate, builtObjectList);
                        string_0 = "ProcMp";
                        method_124(currentDateTime);
                        string_0 = "ProcGx";
                        method_86(currentDateTime, currentStarDate, builtObjectList, bool_);
                        method_253();
                        lstMessages.Invalidate();
                    }
                    else if ((thread_0 != null && thread_0.IsAlive) || (thread_1 != null && thread_1.IsAlive))
                    {
                        Thread.Sleep(500);
                        Cursor.Current = Cursors.WaitCursor;
                    }
                    if (mainView.bool_11)
                    {
                        mainView.DrawMainViewXna(ups);
                    }
                    else
                    {
                        mainView.Invalidate();
                    }
                    Application.DoEvents();
                    bool flag = method_98();
                    method_352(!flag);
                    method_99(ups);
                    if ((thread_0 != null && thread_0.IsAlive) || (thread_1 != null && thread_1.IsAlive))
                    {
                        Cursor.Current = Cursors.WaitCursor;
                    }
                    if (bool_8 && mainView != null && mainView.bool_0)
                    {
                        if (_Game.Galaxy.TimeState == GalaxyTimeState.Running)
                        {
                            btnPlayPause.Image = bitmap_46;
                            method_154();
                        }
                        method_92();
                        string_0 = "ShwInt";
                        method_81();
                        bool_8 = false;
                    }
                    if (pnlIntroduction.Visible)
                    {
                        if (mainView.bool_11)
                        {
                            mainView.DrawMainViewXna(ups);
                        }
                        else
                        {
                            mainView.Invalidate();
                        }
                    }
                    method_84();
                    if (now.Subtract(value).TotalSeconds > 15.0)
                    {
                        int maximumAgeInSeconds = 15;
                        if (bool_6 && long_0 >= 4080218929L && (int_1 > 6 || (int_1 == 6 && int_2 >= 1)))
                        {
                            maximumAgeInSeconds = 120;
                        }
                        habitatImageCache_0.ClearOldImages(maximumAgeInSeconds);
                        builtObjectImageCache_0.ClearOldImages(maximumAgeInSeconds);
                        characterImageCache_0.ClearOldImages();
                        value = now;
                    }
                    if (now.Subtract(value2).TotalSeconds > 240.0)
                    {
                        method_88();
                        value2 = now;
                    }
                    if (!mainView.bool_11 && gameOptions_0 != null && gameOptions_0.MaximumFramerate > 0)
                    {
                        num++;
                        DateTime now2 = DateTime.Now;
                        if (now2 > dateTime)
                        {
                            TimeSpan timeSpan = now2.Subtract(dateTime).Add(new TimeSpan(0, 0, (int)num2));
                            dateTime = now2.AddSeconds(num2);
                            double num4 = Math.Max(0.1, timeSpan.TotalSeconds - (double)(num3 * num) / 1000.0);
                            double num5 = (double)num / num4;
                            double num6 = gameOptions_0.MaximumFramerate;
                            if (num5 > num6)
                            {
                                double num7 = 1000.0 / num5;
                                num3 = (int)((1000.0 - num7 * num6) / num2);
                                num3 = Math.Min(100, Math.Max(0, num3));
                            }
                            else
                            {
                                num3 = 0;
                            }
                            num = 0;
                        }
                        if (num3 > 0)
                        {
                            Thread.Sleep(num3);
                        }
                    }
                    if (bool_22)
                    {
                        if (int_39 != 0)
                        {
                            int_37 = -int_37;
                        }
                        if (int_40 != 0)
                        {
                            int_38 = -int_38;
                        }
                        bool_22 = false;
                        mainView_MouseMove(this, new MouseEventArgs(Control.MouseButtons, 0, MouseHelper.GetCursorPosition().X, MouseHelper.GetCursorPosition().Y, 0));
                    }
                    method_97();
                    ResetMouseEventArgs();
                    upsCounter++;
                    if ((DateTime.Now - upsLastTime).TotalSeconds > 1)
                    {
                        //Debug.WriteLine(upsCounter);
                        ups = upsCounter;
                        upsCounter = 0;
                        upsLastTime = DateTime.Now;
                    }
                }
                method_92();
            }
            catch (Exception ex)
            {
                CrashDump(ex);
                string text = "An error has occurred. Distant Worlds will now exit.";
                text += "\n\n";
                text += "Details of the error are below:";
                text += "\n\n";
                text = text + "Error Code: " + string_0;
                text += "\n\n";
                if (ex is ApplicationException)
                {
                    text += ex.Message;
                    text += "\n\n";
                }
                if (ex.InnerException != null && ex.InnerException is ApplicationException)
                {
                    text += ex.InnerException.Message;
                    text += "\n\n";
                }
                text += ex.ToString();
                if (text.Length > 1800)
                {
                    text = text.Substring(0, 1800) + "...";
                }
                ShowMessageBox(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Class5.smethod_4(bool_0: true);
                Application.Exit();
            }
        }

        private bool method_98()
        {
            Point point = PointToClient(MouseHelper.GetCursorPosition());
            int int_ = 0;
            int int_2 = 0;
            if (point.X == 0 || point.X == 1)
            {
                int_ = (int)((double)_Game.MainViewScrollSpeed * double_0) * -1;
            }
            if (point.Y == 0 || point.Y == 1)
            {
                int_2 = (int)((double)_Game.MainViewScrollSpeed * double_0) * -1;
            }
            if (point.X == base.Width - 1 || point.X == base.Width - 2)
            {
                int_ = (int)((double)_Game.MainViewScrollSpeed * double_0);
            }
            if (point.Y == base.Height - 1 || point.Y == base.Height - 2)
            {
                int_2 = (int)((double)_Game.MainViewScrollSpeed * double_0);
            }
            return method_204(int_, int_2);
        }

        private void method_99(int upsCounter)
        {
            if (ebnBxUfJs7 == null || ebnBxUfJs7.Length != 7)
            {
                return;
            }
            if (method_100(0) && !method_470())
            {
                method_204(0, (int)((double)_Game.MainViewScrollSpeed * double_0) * -1);
            }
            if (method_100(1) && !method_470())
            {
                method_204(0, (int)((double)_Game.MainViewScrollSpeed * double_0));
            }
            if (method_100(2) && !method_470())
            {
                method_204((int)((double)_Game.MainViewScrollSpeed * double_0) * -1, 0);
            }
            if (method_100(3) && !method_470())
            {
                method_204((int)((double)_Game.MainViewScrollSpeed * double_0), 0);
            }
            if (method_100(4) && !method_470())
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
                mainView.double_14 = num;
            }
            if (method_100(5) && !method_470())
            {
                double num2 = double_0;
                num2 -= num2 * ((double)_Game.MainViewZoomSpeed / 100.0);
                if (num2 < double_4)
                {
                    num2 = double_4;
                }
                if (num2 > double_5)
                {
                    num2 = double_5;
                }
                mainView.double_14 = num2;
            }
            if (method_100(6))
            {
                method_353(upsCounter);
            }
        }

        private bool method_100(int int_64)
        {
            if (ebnBxUfJs7[int_64])
            {
                ebnBxUfJs7[int_64] = false;
                return true;
            }
            return false;
        }

        public MessageBoxEx GenerateAutomationMessageBox(string automationTask)
        {
            MessageBoxEx messageBoxEx = MessageBoxExManager.GetMessageBox(automationTask);
            if (messageBoxEx == null)
            {
                messageBoxEx = MessageBoxExManager.CreateMessageBox(automationTask, font_6);
                messageBoxEx.AddButton(TextResolver.GetText("Leave automation on"), "On");
                messageBoxEx.AddButton(TextResolver.GetText("Turn off automation"), "Off");
                messageBoxEx.Caption = string.Format(TextResolver.GetText("Turn Off TASKNAME Automation?"), automationTask);
                messageBoxEx.Icon = MessageBoxExIcon.Question;
                messageBoxEx.AllowSaveResponse = true;
                messageBoxEx.SaveResponseText = TextResolver.GetText("Don't ask me again");
                string text = string.Format(TextResolver.GetText("Would you like to turn off automation"), automationTask);
                text = (messageBoxEx.Text = text.Replace("\n", Environment.NewLine));
                messageBoxEx.UseSavedResponse = true;
            }
            return messageBoxEx;
        }

        public MessageBoxEx GenerateAutomationPromptShow(string taskDescription)
        {
            MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, font_6);
            messageBoxEx.AddButton(MessageBoxExButtons.Yes);
            messageBoxEx.AddButton(MessageBoxExButtons.Show);
            messageBoxEx.AddButton(MessageBoxExButtons.No);
            messageBoxEx.Caption = TextResolver.GetText("Advisor Suggestion");
            messageBoxEx.Icon = MessageBoxExIcon.Question;
            messageBoxEx.Font = font_6;
            messageBoxEx.AllowSaveResponse = false;
            taskDescription = taskDescription.Replace("\n", Environment.NewLine);
            messageBoxEx.Text = taskDescription;
            messageBoxEx.UseSavedResponse = false;
            return messageBoxEx;
        }

        public MessageBoxEx GenerateAutomationPrompt(string taskDescription)
        {
            MessageBoxEx messageBoxEx = MessageBoxExManager.CreateMessageBox(null, font_6);
            messageBoxEx.AddButtons(MessageBoxButtons.YesNo);
            messageBoxEx.Caption = TextResolver.GetText("Advisor Suggestion");
            messageBoxEx.Icon = MessageBoxExIcon.Question;
            messageBoxEx.Font = font_6;
            messageBoxEx.AllowSaveResponse = false;
            taskDescription = taskDescription.Replace("\n", Environment.NewLine);
            messageBoxEx.Text = taskDescription;
            messageBoxEx.UseSavedResponse = false;
            return messageBoxEx;
        }

        public Bitmap PrepareBuiltObjectImage(BuiltObject builtObject, Bitmap image, Color mainColor, Color secondaryColor, double size, int targetSize)
        {
            return PrepareBuiltObjectImage(builtObject, image, mainColor, secondaryColor, size, targetSize, allowPreRotate: false, 1.0);
        }

        public Bitmap PrepareBuiltObjectImage(BuiltObject builtObject, Bitmap image, Color mainColor, Color secondaryColor, double size, int targetSize, bool allowPreRotate)
        {
            return PrepareBuiltObjectImage(builtObject, image, mainColor, secondaryColor, size, targetSize, allowPreRotate, 1.0);
        }

        public Bitmap PrepareBuiltObjectImage(BuiltObject builtObject, Bitmap image, Color mainColor, Color secondaryColor, double size, int targetSize, bool allowPreRotate, double zoom)
        {
            targetSize = (int)((double)targetSize / (zoom * zoom));
            Size size2 = DetermineBuiltObjectSize(image, size, targetSize);
            Bitmap bitmap = new Bitmap(size2.Width, size2.Height, PixelFormat.Format32bppPArgb);
            if (image != null && image.PixelFormat != 0)
            {
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using Graphics graphics = Graphics.FromImage(bitmap);
                okQtJmsUqH(graphics);
                graphics.DrawImage(image, new RectangleF(0f, 0f, size2.Width, size2.Height));
            }
            if (allowPreRotate && builtObject.Role == BuiltObjectRole.Base)
            {
                Bitmap bitmap2 = bitmap;
                bitmap = mainView.method_218(bitmap, builtObject.Heading * -1f, GraphicsQuality.High);
                bitmap2.Dispose();
            }
            return bitmap;
        }

        public Bitmap PrepareBuiltObjectImageNEW(BuiltObject builtObject, BuiltObjectImageData imageData, Color mainColor, Color secondaryColor, bool allowPreRotate, double zoom)
        {
            Bitmap bitmap = null;
            if (builtObject != null)
            {
                Size size = DetermineBuiltObjectSizeNEW(builtObject, imageData, zoom);
                bitmap = Galaxy.CreateBitmapSafely(size.Width, size.Height, PixelFormat.Format32bppPArgb);
                if (bitmap != null)
                {
                    bitmap.SetResolution(imageData.Image.HorizontalResolution, imageData.Image.VerticalResolution);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        okQtJmsUqH(graphics);
                        graphics.DrawImage(imageData.Image, new RectangleF(0f, 0f, size.Width, size.Height));
                    }
                    if (allowPreRotate && builtObject.Role == BuiltObjectRole.Base)
                    {
                        Bitmap bitmap2 = bitmap;
                        bitmap = mainView.method_218(bitmap, builtObject.Heading * -1f, GraphicsQuality.High);
                        bitmap2.Dispose();
                    }
                }
            }
            return bitmap;
        }

        public Bitmap PrepareFighterImage(Fighter fighter, Bitmap image, Color mainColor, Color secondaryColor, double size, int targetSize, double zoom)
        {
            targetSize = (int)((double)targetSize / (zoom * zoom));
            Size size2 = DetermineBuiltObjectSize(image, size, targetSize);
            Bitmap bitmap = new Bitmap(size2.Width, size2.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            okQtJmsUqH(graphics);
            graphics.DrawImage(image, new RectangleF(0f, 0f, size2.Width, size2.Height));
            return bitmap;
        }

        public Bitmap PrepareBuiltObjectImage_OLD(BuiltObject builtObject, Bitmap image, Color mainColor, Color secondaryColor, double size, int targetSize, bool allowPreRotate, double zoom)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            okQtJmsUqH(graphics);
            SolidBrush brush = new SolidBrush(mainColor);
            graphics.FillRectangle(brush, 0, 0, bitmap.Width, bitmap.Height);
            image.MakeTransparent(Color.FromArgb(255, 0, 0));
            graphics.DrawImageUnscaled(image, 0, 0);
            Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format32bppPArgb);
            graphics = Graphics.FromImage(bitmap2);
            okQtJmsUqH(graphics);
            brush = new SolidBrush(secondaryColor);
            graphics.FillRectangle(brush, 0, 0, bitmap2.Width, bitmap2.Height);
            bitmap.MakeTransparent(Color.FromArgb(0, 255, 0));
            graphics.DrawImageUnscaled(bitmap, 0, 0);
            bitmap2.MakeTransparent(Color.Black);
            targetSize = (int)((double)targetSize / zoom);
            Size size2 = DetermineBuiltObjectSize(image, size, targetSize);
            Bitmap bitmap3 = new Bitmap(size2.Width, size2.Height, PixelFormat.Format32bppPArgb);
            graphics = Graphics.FromImage(bitmap3);
            okQtJmsUqH(graphics);
            graphics.DrawImage(bitmap2, new RectangleF(0f, 0f, size2.Width, size2.Height));
            if (allowPreRotate && builtObject.Role == BuiltObjectRole.Base)
            {
                bitmap3 = mainView.method_218(bitmap3, builtObject.Heading * -1f, GraphicsQuality.High);
            }
            bitmap.Dispose();
            bitmap2.Dispose();
            graphics.Dispose();
            brush.Dispose();
            return bitmap3;
        }

        public Size DetermineBuiltObjectSizeNEW(BuiltObject builtObject, BuiltObjectImageData imageData, double zoom)
        {
            double num = 1.0;
            if (builtObject != null && imageData != null && imageData.Image != null && imageData.Image.PixelFormat != 0)
            {
                int num2 = imageData.Image.Size.Width * imageData.Image.Size.Height;
                double num3 = (double)num2 / (double)imageData.Size;
                num = Math.Sqrt((double)builtObject.Size * num3 * Galaxy.BuiltObjectDrawResizeFactor / (zoom * zoom));
                if (builtObject.Design != null && builtObject.Design.ImageScalingType != 0)
                {
                    switch (builtObject.Design.ImageScalingType)
                    {
                        case DesignImageScalingMode.Absolute:
                            num = builtObject.Design.ImageScalingFactor / (float)zoom;
                            break;
                        case DesignImageScalingMode.Scaled:
                            num *= (double)builtObject.Design.ImageScalingFactor;
                            break;
                    }
                }
            }
            return new Size((int)num, (int)num);
        }

        public Size DetermineBuiltObjectSize(Bitmap image, double size, int targetSize)
        {
            double num = (double)targetSize / size;
            int num2 = 10000;
            if (image != null && image.PixelFormat != 0)
            {
                num2 = image.Width * image.Height;
            }
            double d = (double)num2 * num;
            double num3 = Math.Sqrt(d);
            double val = num3;
            num3 = Math.Max(1.0, num3);
            val = Math.Max(1.0, val);
            return new Size((int)num3, (int)val);
        }

        public Bitmap PrepareEngineExhaust(Fighter fighter, Bitmap fighterImage)
        {
            List<Rectangle> list = list_2[fighter.PictureRef];
            if (fighter.TargetSpeed > 0f && list.Count > 0)
            {
                Bitmap bitmap = null;
                bitmap = ((fighter.Specification.EngineExhaustImageIndex < 0 || fighter.Specification.EngineExhaustImageIndex >= bitmap_209.Length) ? bitmap_209[0] : bitmap_209[fighter.Specification.EngineExhaustImageIndex]);
                float num = (float)fighterImage.Width / (float)bitmap_6[fighter.PictureRef].Width;
                float num2 = 0.25f;
                float num3 = (float)bitmap.Height * ((float)fighterImage.Width / (float)bitmap.Height * num2);
                float num4 = 0f;
                if (fighter.TargetSpeed > 0f)
                {
                    if (fighter.TargetSpeed <= (float)fighter.TopSpeed * 0.25f)
                    {
                        num4 = 1f;
                    }
                    else if (fighter.TargetSpeed <= (float)fighter.TopSpeed * 0.5f)
                    {
                        num4 = 1.7f;
                    }
                    else if (fighter.TargetSpeed <= (float)fighter.TopSpeed)
                    {
                        num4 = 2.5f;
                    }
                }
                float num5 = num4 * num3;
                num5 = (int)num5;
                if (num5 % 2f == 1f)
                {
                    num5 += 1f;
                }
                int left = fighterImage.Width;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Left < left)
                    {
                        left = list[i].Left;
                    }
                }
                float num6 = (float)left * num;
                float num7 = num6 - num5;
                float num8 = num7 * -1f;
                float val = (float)fighterImage.Width + num8 * 2f + 2f;
                float val2 = (float)fighterImage.Height + num8 * 2f + 2f;
                val = Math.Max(2f, val);
                val2 = Math.Max(2f, val2);
                Bitmap bitmap2 = new Bitmap((int)val, (int)val2, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap2);
                method_109(graphics);
                graphics.Clear(Color.Transparent);
                RectangleF srcRect = new RectangleF(0f, 0f, bitmap.Width, bitmap.Height);
                for (int j = 0; j < list.Count; j++)
                {
                    float num9 = (float)list[j].Left * num - num5 + num8 + 3f;
                    float num10 = 1.8f;
                    float num11 = (float)list[j].Height * num;
                    float num12 = num11 * num10 / 2f;
                    float num13 = num11 + num12 * 2f;
                    float num14 = (float)list[j].Top * num + num8 - num12;
                    RectangleF destRect = new RectangleF(num9, num14, num5, num13);
                    graphics.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
                }
                return bitmap2;
            }
            return new Bitmap(fighterImage.Width, fighterImage.Height, PixelFormat.Format32bppPArgb);
        }

        public Bitmap PrepareEngineExhaust(BuiltObject ship, Bitmap shipImage)
        {
            BuiltObjectImageData builtObjectImageData = builtObjectImageCache_0.ObtainImageData(ship);
            List<Rectangle> thrusterLocations = builtObjectImageData.ThrusterLocations;
            if (ship.TargetSpeed > 0 && thrusterLocations.Count > 0)
            {
                Bitmap bitmap = null;
                switch (ship.EngineType)
                {
                    case EngineType.Proton:
                        bitmap = bitmap_209[0];
                        break;
                    case EngineType.Quantum:
                        bitmap = bitmap_209[1];
                        break;
                    case EngineType.Acceleros:
                        bitmap = bitmap_209[2];
                        break;
                    case EngineType.Vortex:
                        bitmap = bitmap_209[3];
                        break;
                    case EngineType.StarBurner:
                        bitmap = bitmap_209[4];
                        break;
                    case EngineType.TurboThruster:
                        bitmap = bitmap_209[5];
                        break;
                }
                float num = (float)shipImage.Width / (float)builtObjectImageData.Image.Width;
                float num2 = 0.15f;
                float num3 = (float)bitmap.Height * ((float)shipImage.Width / (float)bitmap.Height * num2);
                float num4 = 0f;
                if (ship.TargetSpeed > 0)
                {
                    if (ship.TargetSpeed <= Galaxy.MovementImpulseSpeed)
                    {
                        num4 = 1f;
                    }
                    else if (ship.TargetSpeed <= ship.CruiseSpeed)
                    {
                        num4 = 1.7f;
                    }
                    else if (ship.TargetSpeed <= ship.TopSpeed)
                    {
                        num4 = 2.5f;
                    }
                }
                float num5 = num4 * num3;
                num5 = (int)num5;
                if (num5 % 2f == 1f)
                {
                    num5 += 1f;
                }
                int left = shipImage.Width;
                for (int i = 0; i < thrusterLocations.Count; i++)
                {
                    if (thrusterLocations[i].Left < left)
                    {
                        left = thrusterLocations[i].Left;
                    }
                }
                float num6 = (float)left * num;
                float num7 = num6 - num5;
                float num8 = num7 * -1f;
                float val = (float)shipImage.Width + num8 * 2f + 2f;
                float val2 = (float)shipImage.Height + num8 * 2f + 2f;
                val = Math.Max(2f, val);
                val2 = Math.Max(2f, val2);
                Bitmap bitmap2 = new Bitmap((int)val, (int)val2, PixelFormat.Format32bppPArgb);
                bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                using Graphics graphics = Graphics.FromImage(bitmap2);
                method_109(graphics);
                graphics.Clear(Color.Transparent);
                Bitmap bitmap3 = new Bitmap(bitmap);
                RectangleF srcRect = new RectangleF(0f, 0f, bitmap3.Width, bitmap3.Height);
                for (int j = 0; j < thrusterLocations.Count; j++)
                {
                    float num9 = (float)thrusterLocations[j].Left * num - num5 + num8 + 3f;
                    float num10 = 1.8f;
                    float num11 = (float)thrusterLocations[j].Height * num;
                    float num12 = num11 * num10 / 2f;
                    float num13 = num11 + num12 * 2f;
                    float num14 = (float)thrusterLocations[j].Top * num + num8 - num12;
                    RectangleF destRect = new RectangleF(num9, num14, num5, num13);
                    graphics.DrawImage(bitmap3, destRect, srcRect, GraphicsUnit.Pixel);
                }
                bitmap3.Dispose();
                return bitmap2;
            }
            return new Bitmap(shipImage.Width, shipImage.Height, PixelFormat.Format32bppPArgb);
        }

        public Bitmap PrepareCreatureImage(Creature creature, Bitmap image, double size, int targetSize, bool allowPreRotate)
        {
            double d = (double)targetSize / size;
            d = Math.Sqrt(d);
            int num = (int)((double)image.Width * d);
            int num2 = (int)((double)image.Height * d);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage(image, new Rectangle(0, 0, num, num2));
            }
            if (allowPreRotate)
            {
                Bitmap bitmap2 = bitmap;
                bitmap = mainView.method_217(bitmap, creature.CurrentHeading * -1f);
                bitmap2.Dispose();
            }
            return bitmap;
        }

        internal List<Rectangle> method_101(Bitmap bitmap_225, Color color_43, int int_64)
        {
            List<Rectangle> list = new List<Rectangle>();
            List<Point> list2 = new List<Point>();
            List<Color> list3 = new List<Color>();
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_225);
            Color transparent = Color.Transparent;
            for (int i = 0; i < bitmap_225.Width; i++)
            {
                for (int j = 0; j < bitmap_225.Height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.B != color_43.B || pixel.R != color_43.R || pixel.G != color_43.G)
                    {
                        continue;
                    }
                    int num = j;
                    int num2 = j;
                    transparent = method_102(fastBitmap, i, j);
                    list3.Add(transparent);
                    list2.Add(new Point(i, j));
                    if (j < bitmap_225.Height - 1)
                    {
                        j++;
                        num2 = bitmap_225.Height - 1;
                        for (; j < bitmap_225.Height; j++)
                        {
                            pixel = fastBitmap.GetPixel(ref i, ref j);
                            if (pixel.R == color_43.R && pixel.G == color_43.G && pixel.B == color_43.B)
                            {
                                transparent = method_102(fastBitmap, i, j);
                                list3.Add(transparent);
                                list2.Add(new Point(i, j));
                                continue;
                            }
                            num2 = j - 1;
                            break;
                        }
                    }
                    list.Add(new Rectangle(i, num, 0, num2 - num + 1));
                    if (list.Count >= int_64)
                    {
                        fastBitmap.Release();
                        return list;
                    }
                }
            }
            fastBitmap.Release();
            for (int k = 0; k < list2.Count; k++)
            {
                bitmap_225.SetPixel(list2[k].X, list2[k].Y, list3[k]);
            }
            return list;
        }

        private Color method_102(DistantWorlds.FastBitmap fastBitmap_0, int int_64, int int_65)
        {
            int X = Math.Max(0, int_64 - 1);
            if (int_64 == 0)
            {
                X = 1;
            }
            int X2 = Math.Min(fastBitmap_0.Bitmap.Width - 1, int_64 + 1);
            if (int_64 == fastBitmap_0.Bitmap.Width - 1)
            {
                X2 = fastBitmap_0.Bitmap.Width - 2;
            }
            Color pixel = fastBitmap_0.GetPixel(ref X, ref int_65);
            Color pixel2 = fastBitmap_0.GetPixel(ref X2, ref int_65);
            int alpha = (pixel.A + pixel2.A) / 2;
            int red = (pixel.R + pixel2.R) / 2;
            int green = (pixel.G + pixel2.G) / 2;
            int blue = (pixel.B + pixel2.B) / 2;
            return Color.FromArgb(alpha, red, green, blue);
        }

        internal List<Point> method_103(Bitmap bitmap_225, Color color_43, int int_64)
        {
            List<Point> list = new List<Point>();
            List<Point> list2 = new List<Point>();
            List<Color> list3 = new List<Color>();
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_225);
            Color transparent = Color.Transparent;
            for (int i = 0; i < bitmap_225.Height; i++)
            {
                for (int j = 0; j < bitmap_225.Width; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.R == color_43.R && pixel.G == color_43.G && pixel.B == color_43.B)
                    {
                        list.Add(new Point(j, i));
                        transparent = method_102(fastBitmap, j, i);
                        list3.Add(transparent);
                        list2.Add(new Point(j, i));
                        if (list.Count >= int_64)
                        {
                            fastBitmap.Release();
                            return list;
                        }
                    }
                }
            }
            fastBitmap.Release();
            for (int k = 0; k < list2.Count; k++)
            {
                bitmap_225.SetPixel(list2[k].X, list2[k].Y, list3[k]);
            }
            return list;
        }

        internal Bitmap method_104(Bitmap bitmap_225, Color color_43)
        {
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_225);
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            DistantWorlds.FastBitmap fastBitmap2 = new DistantWorlds.FastBitmap(bitmap);
            int num = bitmap_225.Width;
            int num2 = bitmap_225.Height;
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (fastBitmap.GetPixel(ref j, ref i).A > 192)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, Color.Transparent);
                    }
                    else
                    {
                        fastBitmap2.SetPixel(ref j, ref i, color_43);
                    }
                }
            }
            fastBitmap2.Release();
            fastBitmap.Release();
            return bitmap;
        }

        internal Bitmap method_105(Bitmap bitmap_225, Color color_43, Color color_44)
        {
            DistantWorlds.FastBitmap fastBitmap = new DistantWorlds.FastBitmap(bitmap_225);
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            DistantWorlds.FastBitmap fastBitmap2 = new DistantWorlds.FastBitmap(bitmap);
            int num = color_43.ToArgb();
            for (int i = 0; i < bitmap_225.Height; i++)
            {
                for (int j = 0; j < bitmap_225.Width; j++)
                {
                    if (fastBitmap.GetPixel(ref j, ref i).ToArgb() != num)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, Color.Transparent);
                    }
                    else
                    {
                        fastBitmap2.SetPixel(ref j, ref i, color_44);
                    }
                }
            }
            fastBitmap2.Release();
            fastBitmap.Release();
            return bitmap;
        }

        internal Bitmap method_106(BuiltObject builtObject_8, Bitmap bitmap_225, Bitmap bitmap_226)
        {
            if (builtObject_8.DamagedComponentCount > 0)
            {
                double num = (double)builtObject_8.DamagedComponentCount / (double)builtObject_8.Components.Count;
                int num2 = bitmap_225.Width * bitmap_225.Height;
                int int_ = (int)((double)num2 * 0.7 * num);
                Random random_ = new Random(builtObject_8.BuiltObjectID);
                HatchBrush brush_ = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1));
                return method_113(bitmap_225, bitmap_226, brush_, int_, random_);
            }
            return bitmap_225;
        }

        internal Bitmap method_107(Fighter fighter_0, Bitmap bitmap_225, Bitmap bitmap_226)
        {
            if (fighter_0.Health < 1f)
            {
                double num = 1f - fighter_0.Health;
                int num2 = bitmap_225.Width * bitmap_225.Height;
                int int_ = (int)((double)num2 * 0.7 * num);
                Random random_ = new Random(fighter_0.FighterID);
                HatchBrush brush_ = new HatchBrush(HatchStyle.Cross, Color.FromArgb(160, 160, 160), Color.FromArgb(1, 1, 1));
                return method_113(bitmap_225, bitmap_226, brush_, int_, random_);
            }
            return bitmap_225;
        }

        internal Bitmap method_108(Creature creature_2, Bitmap bitmap_225, Bitmap bitmap_226)
        {
            if (creature_2.Damage > 0.0)
            {
                double num = creature_2.Damage / (double)creature_2.DamageKillThreshhold;
                int num2 = bitmap_225.Width * bitmap_225.Height;
                int int_ = (int)((double)num2 * 0.3 * num);
                Random random_ = new Random(creature_2.CreatureID);
                SolidBrush brush_ = new SolidBrush(Color.FromArgb(1, 1, 1));
                switch (creature_2.Type)
                {
                    case CreatureType.Kaltor:
                        brush_ = new SolidBrush(Color.FromArgb(110, 32, 80));
                        goto default;
                    case CreatureType.RockSpaceSlug:
                        brush_ = new SolidBrush(Color.FromArgb(64, 32, 36));
                        goto default;
                    case CreatureType.DesertSpaceSlug:
                        brush_ = new SolidBrush(Color.FromArgb(160, 56, 0));
                        goto default;
                    case CreatureType.Ardilus:
                        brush_ = new SolidBrush(Color.FromArgb(48, 8, 20));
                        goto default;
                    default:
                        return method_113(bitmap_225, bitmap_226, brush_, int_, random_);
                    case CreatureType.SilverMist:
                        {
                            float val = 1f - 0.95f * ((float)creature_2.Damage / (float)creature_2.DamageKillThreshhold);
                            val = Math.Min(1f, Math.Max(0f, val));
                            return method_376(bitmap_225, val);
                        }
                }
            }
            return bitmap_225;
        }

        private void method_109(Graphics graphics_3)
        {
            if (graphics_3 != null)
            {
                graphics_3.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_3.InterpolationMode = InterpolationMode.Low;
                graphics_3.SmoothingMode = SmoothingMode.None;
                graphics_3.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                graphics_3.PixelOffsetMode = PixelOffsetMode.None;
            }
        }

        private void okQtJmsUqH(Graphics graphics_3)
        {
            if (graphics_3 != null)
            {
                graphics_3.CompositingQuality = CompositingQuality.HighQuality;
                graphics_3.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics_3.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_3.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics_3.PixelOffsetMode = PixelOffsetMode.HighQuality;
            }
        }

        internal Bitmap method_110(int int_64, Color color_43)
        {
            string string_ = Application.StartupPath + "\\images\\units\\ships\\family0\\light.png";
            return method_12(string_, bool_28: true);
        }

        internal Bitmap method_111(BuiltObject builtObject_8)
        {
            Bitmap bitmap = bitmap_5;
            if (builtObject_8 != null)
            {
                int num = 0;
                if (builtObject_8.Empire != null && builtObject_8.Empire != _Game.Galaxy.IndependentEmpire && builtObject_8.Empire.PirateEmpireBaseHabitat == null)
                {
                    num = builtObject_8.Empire.EmpireId;
                }
                if (bitmap_224[num] == null)
                {
                    Color color = Color.Red;
                    if (num > 0)
                    {
                        color = builtObject_8.Empire.MainColor;
                        color = ControlPaint.Light(color);
                    }
                    bitmap = mainView.method_224(bitmap, color, bool_13: true);
                    bitmap_224[num] = bitmap;
                }
                else
                {
                    bitmap = bitmap_224[num];
                }
            }
            return bitmap;
        }


  }

}