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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using DistantWorlds.DBLoader;

namespace DistantWorlds
{

    public partial class Main
    {

        private void _useResourcePercentFilter_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void method_0(SoundEffectRequest soundEffectRequest_0)
        {
            lock (object_1)
            {
                if (soundEffectRequestList_0.Count < int_3)
                {
                    soundEffectRequestList_0.Add(soundEffectRequest_0);
                }
            }
        }

        internal void method_1()
        {
            if (soundEffectRequestList_0 != null && soundEffectRequestList_0.Count > 0)
            {
                ThreadPool.QueueUserWorkItem(method_2, null);
            }
        }

        internal void method_2(object object_7)
        {
            if (soundEffectRequestList_0 != null && soundEffectRequestList_0.Count > 0)
            {
                SoundEffectRequest[] array;
                lock (object_1)
                {
                    array = soundEffectRequestList_0.ToArray();
                    soundEffectRequestList_0.Clear();
                }
                foreach (SoundEffectRequest soundEffectRequest in array)
                {
                    effectsPlayer_0.PlayEffect(soundEffectRequest.Filename, soundEffectRequest.Balance, soundEffectRequest.Volume, soundEffectRequest.Frequency);
                }
                effectsPlayer_0.ClearFinishedBuffers();
            }
        }

        public Main(int width, int height, bool windowedMode) : base()
        {
            bool_0 = true;
            string_0 = string.Empty;
            galaxyTimeState_0 = GalaxyTimeState.Paused;
            string_2 = string.Empty;
            string_3 = string.Empty;
            ebnBxUfJs7 = new bool[7];
            timer_0 = new System.Timers.Timer();
            soundEffectRequestList_0 = new SoundEffectRequestList();
            object_1 = new object();
            int_3 = 10;
            timer_1 = new System.Timers.Timer();
            timer_2 = new System.Timers.Timer();
            string_4 = new string[111];
            string_5 = new string[24];
            string_6 = new string[16];
            string_7 = new string[120];
            string_8 = new string[4];
            string_9 = new string[4];
            string_10 = new string[10];
            string_11 = new string[100];
            string_12 = new string[179];
            string_13 = new string[49];
            string_14 = new string[20];
            string_15 = new string[20];
            habitatImageCache_0 = new HabitatImageCache();
            bitmap_1 = new Bitmap[15];
            bitmap_2 = new Bitmap[16];
            bitmap_3 = new Bitmap[9];
            characterImageCache_0 = new CharacterImageCache();
            bitmap_6 = new Bitmap[22];
            int_5 = new int[22];
            bitmap_7 = new Bitmap[22];
            list_2 = new List<Rectangle>[22];
            bitmap_8 = new Bitmap[33];
            bitmap_9 = new Bitmap[5];
            bitmap_10 = new Bitmap[10][];
            sveqhmNacy = new int[10];
            bitmap_11 = new Bitmap[10][];
            texture2D_0 = new Texture2D[10][];
            bitmap_12 = new Bitmap[8];
            bitmap_13 = new Bitmap[9];
            edLqkLkgAx = new Bitmap[4];
            bitmap_14 = new Bitmap[1];
            bitmap_15 = new Bitmap[1];
            bitmap_16 = new Bitmap[4];
            bitmap_18 = new Bitmap[2];
            bitmap_19 = new Bitmap[20][];
            bitmap_20 = new Bitmap[120];
            texture2D_1 = new Texture2D[120];
            bitmap_21 = new Bitmap[130];
            bitmap_22 = new Bitmap[2];
            _uiResourcesBitmaps = new Bitmap[41];
            bitmap_23 = new Bitmap[20];
            bitmap_24 = new Bitmap[20];
            bitmap_25 = new Bitmap[20];
            bitmap_26 = new Bitmap[20];
            bitmap_27 = new Bitmap[20];
            bitmap_28 = new Bitmap[62];
            bitmap_29 = new Bitmap[29];
            bitmap_30 = new Bitmap[8];
            UgecxqhvjP = 1f;
            float_1 = 1f;
            int_12 = 10;
            dateTime_0 = DateTime.MinValue;
            font_0 = new Font("Verdana", 15f, FontStyle.Bold);
            font_1 = new Font("Verdana", 12f, FontStyle.Bold);
            color_0 = Color.White;
            font_2 = new Font("Verdana", 9f, FontStyle.Bold);
            color_1 = Color.FromArgb(120, 120, 120);
            font_3 = new Font("Verdana", 8f, FontStyle.Regular);
            font_4 = new Font("Verdana", 8f, FontStyle.Regular);
            font_5 = new Font("Verdana", 8f, FontStyle.Bold);
            color_2 = Color.FromArgb(170, 170, 170);
            font_6 = new Font("Verdana", 9f, FontStyle.Regular);
            font_7 = new Font("Verdana", 9f, FontStyle.Bold);
            font_8 = new Font("Verdana", 9f, FontStyle.Regular);
            rectangle_1 = new Rectangle[2000];
            int_20 = 3;
            gyCmRgDujR = Point.Empty;
            list_5 = new List<object>();
            int_23 = 100;
            habitatList_0 = new HabitatList();
            dremNtuMsv = 100;
            double_0 = 1.0;
            int_27 = 10;
            int_28 = -1;
            bool_19 = true;
            bool_20 = true;
            double_1 = 2000.0;
            double_2 = 75.0;
            double_3 = 500.0;
            int_30 = 5;
            dateTime_1 = DateTime.MinValue;
            dateTime_2 = DateTime.MinValue;
            dateTime_3 = DateTime.MinValue;
            dateTime_4 = DateTime.MinValue;
            double_4 = 0.25;
            double_5 = 15000.0;
            string_21 = string.Empty;
            string_22 = string.Empty;
            string_23 = string.Empty;
            string_24 = string.Empty;
            string_25 = string.Empty;
            string_26 = string.Empty;
            //int_36 = 30;
            int_37 = 1;
            int_38 = 1;
            gameSummaryList_0 = new GameSummaryList();
            color_3 = Color.FromArgb(0, 0, 0);
            color_4 = Color.FromArgb(0, 0, 16);
            color_5 = Color.FromArgb(32, 32, 88);
            color_6 = Color.FromArgb(32, 32, 84);
            color_7 = Color.FromArgb(96, 96, 255);
            color_8 = Color.FromArgb(96, 96, 255);
            color_9 = Color.FromArgb(160, 32, 32);
            color_10 = Color.FromArgb(64, 255, 64);
            color_11 = Color.FromArgb(255, 64, 64);
            color_12 = Color.FromArgb(160, 255, 32, 64);
            color_13 = Color.FromArgb(160, 64, 224, 32);
            color_14 = Color.FromArgb(160, 96, 32, 255);
            color_15 = Color.FromArgb(160, 96, 96, 255);
            color_16 = Color.FromArgb(255, 72, 96);
            color_17 = Color.FromArgb(72, 255, 96);
            color_18 = Color.FromArgb(64, 64, 255);
            color_19 = Color.FromArgb(140, 96, 255);
            color_20 = Color.FromArgb(160, 255, 128, 0);
            color_21 = Color.FromArgb(160, 0, 0, 0);
            color_22 = Color.FromArgb(160, 160, 0, 96);
            color_23 = Color.FromArgb(160, 0, 0, 160);
            color_24 = Color.FromArgb(255, 144, 160);
            color_25 = Color.FromArgb(208, 255, 64);
            color_26 = Color.FromArgb(192, 176, 255);
            color_27 = Color.FromArgb(208, 224, 255);
            color_28 = Color.FromArgb(255, 168, 192);
            color_29 = Color.FromArgb(168, 255, 192);
            color_30 = Color.FromArgb(160, 160, 255);
            color_31 = Color.FromArgb(236, 192, 255);
            color_32 = Color.FromArgb(255, 224, 0);
            color_33 = Color.FromArgb(0, 0, 0);
            color_34 = Color.FromArgb(255, 32, 210);
            color_35 = Color.FromArgb(96, 96, 255);
            color_36 = Color.FromArgb(255, 255, 255);
            VuqPpUtdZU = Color.FromArgb(170, 170, 170);
            color_37 = Color.FromArgb(120, 120, 120);
            color_38 = Color.FromArgb(80, 80, 80);
            color_39 = Color.FromArgb(39, 40, 44);
            color_40 = Color.FromArgb(22, 21, 26);
            color_41 = Color.FromArgb(51, 54, 61);
            color_42 = Color.FromArgb(67, 67, 77);
            solidBrush_6 = new SolidBrush(Color.Yellow);
            solidBrush_7 = new SolidBrush(Color.FromArgb(64, 64, 64));
            solidBrush_8 = new SolidBrush(Color.OrangeRed);
            solidBrush_9 = new SolidBrush(Color.SandyBrown);
            solidBrush_10 = new SolidBrush(Color.Yellow);
            solidBrush_11 = new SolidBrush(Color.Green);
            solidBrush_12 = new SolidBrush(Color.Blue);
            solidBrush_13 = new SolidBrush(Color.Aqua);
            solidBrush_14 = new SolidBrush(Color.Red);
            solidBrush_15 = new SolidBrush(Color.DeepPink);
            solidBrush_16 = new SolidBrush(Color.Gray);
            solidBrush_17 = new SolidBrush(Color.Yellow);
            solidBrush_18 = new SolidBrush(Color.Red);
            solidBrush_19 = new SolidBrush(Color.Red);
            solidBrush_20 = new SolidBrush(Color.White);
            solidBrush_21 = new SolidBrush(Color.Aqua);
            solidBrush_22 = new SolidBrush(Color.Purple);
            solidBrush_23 = new SolidBrush(Color.FromArgb(0, 0, 176));
            solidBrush_24 = new SolidBrush(Color.Violet);
            solidBrush_25 = new SolidBrush(Color.FromArgb(48, 48, 64));
            solidBrush_26 = new SolidBrush(Color.White);
            solidBrush_27 = new SolidBrush(Color.Black);
            hatchBrush_0 = new HatchBrush(HatchStyle.Cross, Color.FromArgb(192, 192, 192), Color.FromArgb(1, 1, 1));
            pen_9 = new Pen(Color.Black);
            hatchBrush_1 = new HatchBrush(HatchStyle.Percent10, Color.FromArgb(40, 40, 96), Color.Transparent);
            hatchBrush_2 = new HatchBrush(HatchStyle.Percent25, Color.FromArgb(24, 24, 48), Color.Transparent);
            hatchBrush_3 = new HatchBrush(HatchStyle.DiagonalCross, Color.FromArgb(0, 0, 64), Color.Black);
            hatchBrush_4 = new HatchBrush(HatchStyle.ForwardDiagonal, Color.FromArgb(32, 32, 32), Color.Black);
            hatchBrush_5 = new HatchBrush(HatchStyle.ForwardDiagonal, Color.FromArgb(32, 32, 32), Color.Transparent);
            dateTime_5 = DateTime.MinValue;
            string_27 = string.Empty;
            list_6 = new List<object>();
            object_4 = new object();
            int_41 = 200;
            int_42 = 50;
            int_43 = 100;
            int_44 = 30;
            int_45 = 1;
            int_46 = 1;
            int_47 = 1;
            dateTime_6 = DateTime.MinValue;
            ProcessorCount = 1;
            bitmap_224 = new Bitmap[255];
            string_28 = string.Empty;
            string_29 = string.Empty;
            conversationOptionList_0 = new ConversationOptionList();
            object_5 = new object();
            int_60 = -1;
            int_62 = -1;
            int_63 = -1;
            double_6 = -1.0;
            try
            {
                InitializeComponent();
                //splash_0 = splashForm;
                SteamAPI.Initialize(Application.UserAppDataPath);
                MainInit(width, height, windowedMode);
            }
            catch (Exception ex)
            {
                CrashDump(ex);
                throw;
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowThreadProcessId(IntPtr intptr_2, out int int_64);

        public static bool ApplicationIsActivated()
        {
            IntPtr foregroundWindow = GetForegroundWindow();
            if (foregroundWindow == IntPtr.Zero)
            {
                return false;
            }
            int id = Process.GetCurrentProcess().Id;
            GetWindowThreadProcessId(foregroundWindow, out var int_);
            return int_ == id;
        }

        internal void method_3()
        {
            habitat_0 = null;
            builtObject_0 = null;
            builtObject_1 = null;
            builtObject_2 = null;
            builtObject_3 = null;
            shipGroup_0 = null;
            builtObject_4 = null;
            design_0 = null;
            bool_18 = false;
            builtObject_5 = null;
            shipGroup_2 = null;
            string_17 = string.Empty;
            builtObject_6 = null;
            habitat_1 = null;
            habitat_2 = null;
            creature_0 = null;
            shipAction_0 = null;
            builtObjectList_0 = null;
            habitat_3 = null;
            if (list_5 != null)
            {
                list_5.Clear();
            }
            if (habitatList_0 != null)
            {
                habitatList_0.Clear();
            }
            habitat_4 = null;
            creature_1 = null;
            builtObject_7 = null;
            empire_0 = null;
            empire_1 = null;
            object_2 = null;
            race_0 = null;
            habitat_5 = null;
            hOcmlqpCrp = null;
            object_3 = null;
            empire_2 = null;
            habitat_6 = null;
            habitat_7 = null;
            habitat_8 = null;
            if (habitatList_1 != null)
            {
                habitatList_1.Clear();
            }
            if (habitatList_2 != null)
            {
                habitatList_2.Clear();
            }
            tutorial_0 = null;
        }

        [DllImport("user32")]
        private static extern int SystemParametersInfo(int int_64, int int_65, int int_66, int int_67);

        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo", SetLastError = true)]
        private static extern bool SystemParametersInfo_1(uint uint_1, uint uint_2, ref int int_64, uint uint_3);

        public bool ToggleScreenSaverActive(bool active)
        {
            int int_ = (active ? 1 : 0);
            int num = SystemParametersInfo(17, int_, 0, 0);
            return num > 0;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //Discarded unreachable code: IL_0267
            base.OnMouseWheel(e);
            if (_Game != null)
            {
                if (itemListCollectionPanel_0.ScrollWheel(e.Location, e.Delta))
                {
                    return;
                }
                double num = double_0;
                double val = (double)(e.Delta * -1) / 120.0 * ((double)_Game.MainViewZoomSpeed / 100.0);
                val = Math.Max(-0.85, Math.Min(val, 0.85));
                num += num * val;
                if (num < 1.0)
                {
                    num = 1.0;
                }
                if (num > double_5)
                {
                    num = double_5;
                }
                Point point = PointToClient(MouseHelper.GetCursorPosition());
                int int_ = point.X;
                int int_2 = point.Y;
                method_151(ref int_, ref int_2);
                bool flag = true;
                if (num == double_0)
                {
                    flag = false;
                }
                bool uhvLmNjli = UhvLmNjli7;
                if (flag)
                {
                    switch (_Game.MouseScrollWheelBehaviour)
                    {
                        case 1:
                            if (_Game.SelectedObject != null)
                            {
                                method_157(_Game.SelectedObject);
                                if (uhvLmNjli)
                                {
                                    UhvLmNjli7 = true;
                                }
                            }
                            break;
                        case 2:
                            if (e.Delta > 0)
                            {
                                Point point2 = PointToClient(MouseHelper.GetCursorPosition());
                                int int_3 = point2.X;
                                int int_4 = point2.Y;
                                method_150(ref int_3, ref int_4, num);
                                int num2 = int_ - int_3;
                                int num3 = int_2 - int_4;
                                int num4 = int_13 + num2;
                                int num5 = int_14 + num3;
                                if (_Game.SelectedObject != null && UhvLmNjli7)
                                {
                                    UhvLmNjli7 = false;
                                }
                                int_13 = num4;
                                int_14 = num5;
                                Habitat habitat = habitat_6;
                                Habitat habitat2 = method_149();
                                if (habitat != habitat2)
                                {
                                    bool_20 = true;
                                }
                                else
                                {
                                    mainView.ClearBackdropImages();
                                }
                                if (uhvLmNjli)
                                {
                                    UhvLmNjli7 = true;
                                }
                            }
                            break;
                    }
                    if ((double_0 >= 500.0 && num < 500.0) || (double_0 <= 500.0 && num > 500.0))
                    {
                        itemListCollectionPanel_0.NeedsRefresh = true;
                    }
                    double_0 = num - 0.001;
                    mainView.double_14 = num;
                }
            }
            WuVtIlwpRt();
        }

        internal void method_4(double double_7)
        {
            mainView.double_14 = double_7;
            if (double_0 != double_7)
            {
                if ((double_0 >= 500.0 && double_7 < 500.0) || (double_0 <= 500.0 && double_7 > 500.0))
                {
                    itemListCollectionPanel_0.NeedsRefresh = true;
                }
                ZoomStatus = ZoomStatus.Zooming;
                double_0 = double_7;
                method_5();
                if (!mainView.bool_9)
                {
                    mainView.method_14(_Game.StarFieldSize);
                }
                picSystem.RegenNebulae(double_0);
                method_149();
                mainView.bool_0 = true;
            }
        }

        internal void method_5()
        {
            mainView.ClearNebulaeImagesScaled();
            mainView.bool_0 = false;
            if (!mainView.bool_11)
            {
                mainView.ClearPrecachedHabitatBitmaps();
                mainView.ClearPreprocessedBuiltObjectImages();
                mainView.ClearPreprocessedCreatureImages();
            }
            mainView.ClearPreprocessedFighterImages();
            mainView.FadeGalaxyBackground();
        }

        private Bitmap method_6(Bitmap bitmap_225, double double_7)
        {
            double num = bitmap_225.Width;
            double num2 = bitmap_225.Height;
            Point[] array = new Point[4]
            {
            new Point(0, 0),
            new Point((int)num, 0),
            new Point(0, (int)num2),
            new Point((int)num, (int)num2)
            };
            double num3 = num / 2.0;
            double num4 = num2 / 2.0;
            for (int i = 0; i <= 3; i++)
            {
                array[i].X -= (int)num3;
                array[i].Y -= (int)num4;
            }
            double num5 = Math.Sin(double_7);
            double num6 = Math.Cos(double_7);
            for (int j = 0; j <= 3; j++)
            {
                double num7 = array[j].X;
                double num8 = array[j].Y;
                array[j].X = (int)(num7 * num6 + num8 * num5);
                array[j].Y = (int)((0.0 - num7) * num5 + num8 * num6);
            }
            double num9 = array[0].X;
            double num10 = array[0].Y;
            for (int k = 1; k <= 3; k++)
            {
                if (num9 > (double)array[k].X)
                {
                    num9 = array[k].X;
                }
                if (num10 > (double)array[k].Y)
                {
                    num10 = array[k].Y;
                }
            }
            for (int l = 0; l <= 3; l++)
            {
                array[l].X -= (int)num9;
                array[l].Y -= (int)num10;
            }
            int num11 = Math.Max(1, (int)(-2.0 * num9));
            int num12 = Math.Max(1, (int)(-2.0 * num10));
            Bitmap bitmap = new Bitmap(num11, num12, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Point[] destPoints = new Point[3]
            {
            array[0],
            array[1],
            array[2]
            };
            graphics.DrawImage(bitmap_225, destPoints);
            return bitmap;
        }

        private void method_7(Bitmap bitmap_225, out int int_64, out int int_65, out int int_66)
        {
            int num = 3;
            int num2 = 0;
            int_64 = 10000;
            int_65 = -1;
            int_66 = bitmap_225.Height - 1;
            FastBitmap fastBitmap = new FastBitmap(bitmap_225);
            for (int Y = bitmap_225.Height - 1; Y >= 0; Y--)
            {
                for (int i = 0; i < bitmap_225.Width; i++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref Y);
                    if (pixel.ToArgb() == Color.Black.ToArgb() || pixel.ToArgb() == Color.FromArgb(0, 0, 0, 0).ToArgb())
                    {
                        continue;
                    }
                    if (i < int_64)
                    {
                        if (int_66 < bitmap_225.Height - 1)
                        {
                            int_66 = Y;
                        }
                        int_64 = i;
                    }
                    if (i > int_65)
                    {
                        int_65 = i;
                    }
                }
                if (int_64 < 10000 && int_65 >= 0)
                {
                    num2++;
                    if (num2 >= num)
                    {
                        break;
                    }
                }
            }
            fastBitmap.Release();
            if (int_64 < 0)
            {
                int_64 = 0;
            }
            if (int_65 < 0)
            {
                int_65 = bitmap_225.Width;
            }
        }

        private int method_8(Bitmap bitmap_225)
        {
            int num = 0;
            _ = bitmap_225.Width;
            _ = bitmap_225.Height;
            int num2 = Color.Black.ToArgb();
            int num3 = Color.FromArgb(0, 0, 0, 0).ToArgb();
            int num4 = Color.FromArgb(0, 255, 255, 255).ToArgb();
            FastBitmap fastBitmap = new FastBitmap(bitmap_225);
            for (int i = 0; i < bitmap_225.Width; i++)
            {
                for (int j = 0; j < bitmap_225.Height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.A > 0 && pixel.ToArgb() != num2 && pixel.ToArgb() != num3 && pixel.ToArgb() != num4)
                    {
                        num++;
                    }
                }
            }
            fastBitmap.Release();
            return num;
        }

        private Bitmap method_9(Bitmap bitmap_225, int int_64, Color color_43)
        {
            int num = 0;
            int num2 = bitmap_225.Width - 1;
            int num3 = 0;
            int num4 = bitmap_225.Height - 1;
            int num5 = Color.Black.ToArgb();
            int num6 = Color.FromArgb(0, 0, 0, 0).ToArgb();
            int num7 = Color.FromArgb(0, 255, 255, 255).ToArgb();
            FastBitmap fastBitmap = new FastBitmap(bitmap_225);
            for (int i = 0; i < bitmap_225.Width; i++)
            {
                for (int j = 0; j < bitmap_225.Height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.A > 0 && pixel.ToArgb() != num5 && pixel.ToArgb() != num6 && pixel.ToArgb() != num7)
                    {
                        num = i;
                        i = bitmap_225.Width;
                        break;
                    }
                }
            }
            for (int X = bitmap_225.Width - 1; X >= 0; X--)
            {
                for (int k = 0; k < bitmap_225.Height; k++)
                {
                    Color pixel2 = fastBitmap.GetPixel(ref X, ref k);
                    if (pixel2.A > 0 && pixel2.ToArgb() != num5 && pixel2.ToArgb() != num6 && pixel2.ToArgb() != num7)
                    {
                        num2 = X;
                        X = -1;
                        break;
                    }
                }
            }
            for (int l = 0; l < bitmap_225.Height; l++)
            {
                for (int m = 0; m < bitmap_225.Width; m++)
                {
                    Color pixel3 = fastBitmap.GetPixel(ref m, ref l);
                    if (pixel3.A > 0 && pixel3.ToArgb() != num5 && pixel3.ToArgb() != num6 && pixel3.ToArgb() != num7)
                    {
                        num3 = l;
                        l = bitmap_225.Height;
                        break;
                    }
                }
            }
            for (int Y = bitmap_225.Height - 1; Y >= 0; Y--)
            {
                for (int n = 0; n < bitmap_225.Width; n++)
                {
                    Color pixel4 = fastBitmap.GetPixel(ref n, ref Y);
                    if (pixel4.A > 0 && pixel4.ToArgb() != num5 && pixel4.ToArgb() != num6 && pixel4.ToArgb() != num7)
                    {
                        num4 = Y;
                        Y = -1;
                        break;
                    }
                }
            }
            fastBitmap.Release();
            num3 -= int_64;
            num4 += int_64;
            num -= int_64;
            num2 += int_64;
            int num8 = num4 - num3;
            int num9 = num2 - num;
            int num10;
            int num11;
            int num12;
            int num13;
            if (num8 > num9)
            {
                num10 = num + num9 / 2 - num8 / 2;
                num11 = num10 + num8;
                num12 = num3;
                num13 = num4;
            }
            else
            {
                num12 = num3 + num8 / 2 - num9 / 2;
                num13 = num12 + num9;
                num10 = num;
                num11 = num2;
            }
            Bitmap bitmap = new Bitmap(num11 - num10 + 1, num13 - num12 + 1, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(color_43);
            Rectangle rect = new Rectangle(num10 * -1, num12 * -1, bitmap_225.Width, bitmap_225.Height);
            graphics.DrawImage(bitmap_225, rect);
            return bitmap;
        }

        public Bitmap method_10(string string_30, string string_31, string string_32, bool bool_28)
        {
            if (!string.IsNullOrEmpty(string_30) && File.Exists(string_30 + string_32))
            {
                return method_12(string_30 + string_32, bool_28);
            }
            if (File.Exists(string_31 + string_32))
            {
                return method_12(string_31 + string_32, bool_28);
            }
            if (bool_28)
            {
                string text = string.Format(TextResolver.GetText("Could not load required image"), string_31 + string_32);
                ShowMessageBox(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(-1);
            }
            return null;
        }

        private Bitmap method_11(string string_30, string string_31, string string_32, string string_33, bool bool_28)
        {
            if (!string.IsNullOrEmpty(string_30) && File.Exists(string_30 + string_33))
            {
                return method_12(string_30 + string_33, bool_28);
            }
            if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + string_33))
            {
                return method_12(string_31 + string_33, bool_28);
            }
            if (File.Exists(string_32 + string_33))
            {
                return method_12(string_32 + string_33, bool_28);
            }
            if (bool_28)
            {
                string text = string.Format(TextResolver.GetText("Could not load required image"), string_32 + string_33);
                ShowMessageBox(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Environment.Exit(-1);
            }
            return null;
        }

        private Bitmap method_12(string string_30, bool bool_28)
        {
            return method_14(string_30, bool_28, 1.0, int.MaxValue);
        }

        private Bitmap method_13(string string_30, bool bool_28, double double_7)
        {
            return method_14(string_30, bool_28, double_7, int.MaxValue);
        }

        private Bitmap method_14(string string_30, bool bool_28, double double_7, int int_64)
        {
            return method_15(string_30, bool_28, double_7, int_64, 1);
        }

        private Bitmap method_15(string string_30, bool bool_28, double double_7, int int_64, int int_65)
        {
            Bitmap bitmap = null;
            if (!File.Exists(string_30))
            {
                if (!bool_28)
                {
                    return null;
                }
            }
            else
            {
                if (int_64 < int.MaxValue)
                {
                    FileInfo fileInfo = new FileInfo(string_30);
                    if (fileInfo.Length > int_64)
                    {
                        string text = string.Format(arg1: (int_64 / 1024).ToString(), format: TextResolver.GetText("Image too big"), arg0: string_30);
                        ShowMessageBox(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Environment.Exit(-1);
                    }
                }
                try
                {
                    bitmap = GraphicsHelper.LoadImageFromFilePath(string_30);
                    if (double_7 != 1.0)
                    {
                        int num = (int)((double)bitmap.Width * double_7);
                        int num2 = (int)((double)bitmap.Height * double_7);
                        if (num < int_65 || num2 < int_65)
                        {
                            double val = (double)int_65 / (double)bitmap.Width;
                            double val2 = (double)int_65 / (double)bitmap.Height;
                            double num3 = Math.Max(val, val2);
                            num = (int)((double)bitmap.Width * num3);
                            num2 = (int)((double)bitmap.Height * num3);
                        }
                        Bitmap bitmap2 = bitmap;
                        bitmap = PrecacheScaledBitmap(bitmap, num, num2, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                        bitmap2.Dispose();
                    }
                }
                catch (Exception)
                {
                    bitmap = null;
                }
            }
            if (bitmap != null)
            {
                return bitmap;
            }
            string text2 = string.Format(TextResolver.GetText("Could not load required image"), string_30);
            ShowMessageBox(text2, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(-1);
            return null;
        }

        private MemoryStream method_16(string string_30)
        {
            MemoryStream memoryStream = null;
            try
            {
                if (File.Exists(string_30))
                {
                    using FileStream fileStream = new FileStream(string_30, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    memoryStream = new MemoryStream(buffer);
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
                memoryStream = null;
            }
            if (memoryStream != null)
            {
                return memoryStream;
            }
            string text = string.Format(TextResolver.GetText("Could not load required sound"), string_30);
            ShowMessageBox(text, TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(-1);
            return new MemoryStream();
        }

        private void method_17(string string_30, string string_31)
        {
            string text = string_30 + "button2.wav";
            if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "button2.wav"))
            {
                text = string_31 + "button2.wav";
            }
            string_28 = text;
            string text2 = string_30 + "grid.wav";
            if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "grid.wav"))
            {
                text2 = string_31 + "grid.wav";
            }
            string_29 = text2;
        }

        private void method_18(string string_30, string string_31, Control control_1)
        {
            if (control_1 is GlassButton)
            {
                if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "button1.wav"))
                {
                    GlassButton.SetSoundLocation(string_31 + "button1.wav");
                }
                else
                {
                    GlassButton.SetSoundLocation(string_30 + "button1.wav");
                }
            }
            if (control_1 is HoverButton)
            {
                if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "button2.wav"))
                {
                    HoverButton.SetSoundLocation(string_31 + "button2.wav");
                }
                else
                {
                    HoverButton.SetSoundLocation(string_30 + "button2.wav");
                }
            }
            if (control_1 is ListViewBase)
            {
                if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "grid.wav"))
                {
                    ListViewBase.SetSoundLocation(string_31 + "grid.wav");
                }
                else
                {
                    ListViewBase.SetSoundLocation(string_30 + "grid.wav");
                }
            }
            if (control_1 is HoverMenuItem)
            {
                if (!string.IsNullOrEmpty(string_31) && File.Exists(string_31 + "button2.wav"))
                {
                    HoverMenuItem.SetSoundLocation(string_31 + "button2.wav");
                }
                else
                {
                    HoverMenuItem.SetSoundLocation(string_30 + "button2.wav");
                }
            }
            if (control_1.Controls == null)
            {
                return;
            }
            foreach (Control control in control_1.Controls)
            {
                method_18(string_30, string_31, control);
            }
        }

        internal Bitmap method_19(Bitmap bitmap_225, float float_2)
        {
            Bitmap bitmap = new Bitmap(bitmap_225.Width, bitmap_225.Height, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            ImageAttributes imageAttr = method_20(float_2);
            okQtJmsUqH(graphics);
            graphics.DrawImage(bitmap_225, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap_225.Width, bitmap_225.Height, GraphicsUnit.Pixel, imageAttr);
            return bitmap;
        }

        internal ImageAttributes method_20(float float_2)
        {
            float[][] newColorMatrix = new float[5][]
            {
            new float[5] { 1f, 0f, 0f, 0f, 0f },
            new float[5] { 0f, 1f, 0f, 0f, 0f },
            new float[5] { 0f, 0f, 1f, 0f, 0f },
            new float[5] { 0f, 0f, 0f, float_2, 0f },
            new float[5] { 0f, 0f, 0f, 0f, 1f }
            };
            ColorMatrix colorMatrix = new ColorMatrix(newColorMatrix);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix);
            return imageAttributes;
        }

        private Bitmap method_21(Bitmap bitmap_225)
        {
            Bitmap bitmap = new Bitmap(bitmap_225.Width + 1, bitmap_225.Height + 1, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            okQtJmsUqH(graphics);
            graphics.DrawImage(bitmap_225, new Rectangle(0, 0, bitmap_225.Width, bitmap_225.Height));
            graphics.Dispose();
            return bitmap;
        }

        private void LoadMapStars(string string_30)
        {
            //добавить загрузку битмапов через таски, может убрать в перенте ParralelFor ?
            string text = string_30 + "environment\\mapstars\\";
            List<Bitmap> list = new List<Bitmap>();
            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();
            string[] files = Directory.GetFiles(text + "mainsequence\\", "*.png");
            foreach (string string_31 in files)
            {
                //Bitmap item = method_12(string_31, bool_28: true);
                //list.Add(item);
                taskList.Add(Task.Run(() => method_12(string_31, bool_28: true)));
            }
            DiZcdsnvl0 = taskList.Count;
            files = Directory.GetFiles(text + "redgiant\\", "*.png");
            foreach (string string_32 in files)
            {
                //Bitmap item2 = method_12(string_32, bool_28: true);
                //list.Add(item2);
                taskList.Add(Task.Run(() => method_12(string_32, bool_28: true)));
            }
            int_7 = taskList.Count;
            files = Directory.GetFiles(text + "supergiant\\", "*.png");
            foreach (string string_33 in files)
            {
                //Bitmap item3 = method_12(string_33, bool_28: true);
                //list.Add(item3);
                taskList.Add(Task.Run(() => method_12(string_33, bool_28: true)));
            }
            int_8 = taskList.Count;
            files = Directory.GetFiles(text + "whitedwarf\\", "*.png");
            foreach (string string_34 in files)
            {
                //Bitmap item4 = method_12(string_34, bool_28: true);
                //list.Add(item4);
                taskList.Add(Task.Run(() => method_12(string_34, bool_28: true)));
            }
            int_9 = taskList.Count;
            files = Directory.GetFiles(text + "neutron\\", "*.png");
            foreach (string string_35 in files)
            {
                //Bitmap item5 = method_12(string_35, bool_28: true);
                //list.Add(item5);
                taskList.Add(Task.Run(() => method_12(string_35, bool_28: true)));
            }
            int_10 = taskList.Count;
            files = Directory.GetFiles(text + "blackhole\\", "*.png");
            foreach (string string_36 in files)
            {
                //Bitmap item6 = method_12(string_36, bool_28: true);
                //list.Add(item6);
                taskList.Add(Task.Run(() => method_12(string_36, bool_28: true)));
            }
            Task.WaitAll(taskList.ToArray());
            foreach (var item in taskList)
            {
                list.Add(item.Result);
            }
            bitmap_196 = list.ToArray();
            List<Bitmap> list2 = new List<Bitmap>();
            List<Bitmap> list3 = new List<Bitmap>();
            files = Directory.GetFiles(text + "flares\\", "*.png");
            object syncObj = new object();
            List<Task> taskList2 = new List<Task>();
            foreach (string string_37 in files)
            {
                Task localTask1 = Task.Run(() =>
                {
                    Bitmap res = method_12(string_37, bool_28: true);
                    Bitmap res1 = PrecacheScaledBitmap(res, 32, 32, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                    Bitmap res2 = PrecacheScaledBitmap(res, 16, 16, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                    lock (syncObj)
                    {
                        list2.Add(res1);
                        list3.Add(res2);
                    }
                    res.Dispose();
                });
                taskList2.Add(localTask1);
                //Bitmap bitmap = method_12(string_37, bool_28: true);
                //Bitmap item7 = PrecacheScaledBitmap(bitmap, 32, 32, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                //list2.Add(item7);
                //Bitmap item8 = PrecacheScaledBitmap(bitmap, 16, 16, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                //list3.Add(item8);
                //bitmap.Dispose();
            }
            Task.WaitAll(taskList2.ToArray());
            bitmap_197 = list2.ToArray();
            bitmap_198 = list3.ToArray();
        }

        private Bitmap method_23(Bitmap bitmap_225, int int_64)
        {
            int num = bitmap_225.Width;
            int num2 = bitmap_225.Height;
            FastBitmap fastBitmap = new FastBitmap(bitmap_225);
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.A < int_64)
                    {
                        fastBitmap.SetPixel(ref i, ref j, Color.FromArgb(0, pixel.R, pixel.G, pixel.B));
                    }
                }
            }
            fastBitmap.Release();
            return bitmap_225;
        }

        private Bitmap method_24(Bitmap bitmap_225, int int_64)
        {
            Rectangle rectangle_ = new Rectangle(int_64, int_64, bitmap_225.Width - int_64 * 2, bitmap_225.Height - int_64 * 2);
            return method_25(bitmap_225, rectangle_);
        }

        private Bitmap method_25(Bitmap bitmap_225, Rectangle rectangle_2)
        {
            Bitmap bitmap = new Bitmap(rectangle_2.Width, rectangle_2.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            Rectangle srcRect = new Rectangle(rectangle_2.X, rectangle_2.Y, rectangle_2.Width, rectangle_2.Height);
            graphics.DrawImage(bitmap_225, destRect, srcRect, GraphicsUnit.Pixel);
            return bitmap;
        }

        internal void LoadStars(string gameImages, string customizationPath, double double_7, bool bool_28)
        {
            string gameImageStars = gameImages + "environment\\stars\\";
            string customizationStars = customizationPath + "environment\\stars\\";
            if (!bool_28 && !Directory.Exists(customizationStars))
            {
                return;
            }
            GraphicsHelper.DisposeImageArray(bitmap_199);
            GraphicsHelper.DisposeImageArray(bitmap_200);
            GraphicsHelper.DisposeImageArray(bitmap_201);
            GraphicsHelper.DisposeImageArray(bitmap_202);
            GraphicsHelper.DisposeImageArray(bitmap_204);
            GraphicsHelper.DisposeImageArray(bitmap_206);

            List<Task> taskList = new List<Task>();
            bitmap_199 = new Bitmap[2];
            for (int i = 0; i < 2; i++)
            {
                string string_33 = "star_blackhole_" + i + ".png";
                int localI = i;
                //bitmap_199[i] = method_10(customizationStars, gameImageStars, string_33, bool_28: true);
                taskList.Add(Task.Run(() => bitmap_199[localI] = method_10(customizationStars, gameImageStars, string_33, bool_28: true)));
            }
            bitmap_200 = new Bitmap[100];
            for (int j = 0; j < 100; j++)
            {
                string string_34 = "blackhole\\BlkHole-" + (j + 1).ToString("0000") + ".png";
                //Bitmap bitmap = method_10(customizationStars, gameImageStars, string_34, bool_28: true);
                //int int_ = (int)((double)bitmap.Width * 0.1);
                //Bitmap bitmap2 = method_24(bitmap, int_);
                //bitmap_200[j] = PrecacheScaledBitmap(bitmap2, 100, 100, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                //bitmap.Dispose();
                //bitmap2.Dispose();
                int localJ = j;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap = method_10(customizationStars, gameImageStars, string_34, bool_28: true);
                    int int_ = (int)((double)bitmap.Width * 0.1);
                    Bitmap bitmap2 = method_24(bitmap, int_);
                    bitmap_200[localJ] = PrecacheScaledBitmap(bitmap2, 100, 100, InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
                    bitmap.Dispose();
                    bitmap2.Dispose();
                }));
            }
            bitmap_201 = new Bitmap[100];
            for (int k = 0; k < 100; k++)
            {
                string string_35 = "rays\\CoronaB-" + (k + 1).ToString("0000") + ".png";
                //bitmap_201[k] = method_10(customizationStars, gameImageStars, string_35, bool_28: true);
                int localK = k;
                taskList.Add(Task.Run(() => bitmap_201[localK] = method_10(customizationStars, gameImageStars, string_35, bool_28: true)));
            }
            bitmap_202 = new Bitmap[100];
            for (int l = 0; l < 100; l++)
            {
                string string_36 = "rays\\CoronaC-" + (l + 1).ToString("0000") + ".png";
                //bitmap_202[l] = method_10(customizationStars, gameImageStars, string_36, bool_28: true);
                int localL = l;
                taskList.Add(Task.Run(() => bitmap_202[localL] = method_10(customizationStars, gameImageStars, string_36, bool_28: true)));
            }
            bitmap_204 = new Bitmap[3];
            for (int m = 0; m < 3; m++)
            {
                string string_37 = "star_disc_" + m.ToString("0") + ".png";
                //bitmap_204[m] = method_10(customizationStars, gameImageStars, string_37, bool_28: true);
                int localM = m;
                taskList.Add(Task.Run(() => bitmap_204[localM] = method_10(customizationStars, gameImageStars, string_37, bool_28: true)));
            }
            string path = gameImages + "environment\\supernovae\\";
            if (!string.IsNullOrEmpty(customizationPath))
            {
                string text2 = customizationPath + "environment\\supernovae\\";
                if (Directory.Exists(text2))
                {
                    string[] files = Directory.GetFiles(text2, "*.png");
                    if (files.Length > 0)
                    {
                        path = text2;
                    }
                }
            }
            string[] files2 = Directory.GetFiles(path, "*.png");
            bitmap_206 = new Bitmap[files2.Length];
            for (int n = 0; n < files2.Length; n++)
            {
                string string_38 = files2[n];
                int localN = n;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap3 = method_13(string_38, bool_28: true, double_7);
                    Bitmap bitmap4 = bitmap3;
                    bitmap3 = method_21(bitmap3);
                    bitmap4.Dispose();
                    bitmap_206[localN] = bitmap3;
                }));
            }
            Task.WaitAll(taskList.ToArray());
        }

        private void LoadNebulae(string string_30, double double_7)
        {
            string path = string_30 + "environment\\nebulae\\";
            string[] files = Directory.GetFiles(path, "*.png");
            GraphicsHelper.DisposeImageArray(bitmap_205);
            bitmap_205 = new Bitmap[files.Length];
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < files.Length; i++)
            {
                string string_31 = files[i];
                int localI = i;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap = method_13(string_31, bool_28: true, double_7);
                    Bitmap bitmap2 = bitmap;
                    bitmap = method_21(bitmap);
                    bitmap2.Dispose();
                    bitmap_205[localI] = bitmap;
                }));
            }
            UpdateSplashProgress();
        }

        internal void LoadHyperEffects(string string_30, string string_31, double double_7)
        {
            string text = string_30 + "effects\\hyperenter\\";
            string text2 = string_31 + "effects\\hyperenter\\";
            string text3 = string_30 + "effects\\hyperexit\\";
            string text4 = string_31 + "effects\\hyperexit\\";
            if (Directory.Exists(text2))
            {
                string[] directories = Directory.GetDirectories(text2, "0");
                if (directories.Length > 0)
                {
                    text = text2;
                }
            }
            if (Directory.Exists(text4))
            {
                string[] directories2 = Directory.GetDirectories(text4, "0");
                if (directories2.Length > 0)
                {
                    text3 = text4;
                }
            }
            if (list_3 != null)
            {
                for (int i = 0; i < list_3.Count; i++)
                {
                    GraphicsHelper.DisposeImageArray(list_3[i]);
                }
            }
            if (list_4 != null)
            {
                for (int j = 0; j < list_4.Count; j++)
                {
                    GraphicsHelper.DisposeImageArray(list_4[j]);
                }
            }
            list_3 = new List<Bitmap[]>();
            list_4 = new List<Bitmap[]>();
            double double_8 = double_7 * 0.9;
            string[] directories3 = Directory.GetDirectories(text);
            string[] directories4 = Directory.GetDirectories(text3);
            if (directories3.Length <= 0)
            {
                throw new ApplicationException("Missing hyper enter animation folders at " + text);
            }
            if (directories4.Length <= 0)
            {
                throw new ApplicationException("Missing hyper exit animation folders at " + text3);
            }
            if (directories3.Length != directories4.Length)
            {
                throw new ApplicationException("Number of hyper enter animation folders does not match the number of hyper exit animation folders (" + text + ", " + text3 + ")");
            }
            List<Task> taskList = new List<Task>();
            for (int k = 0; k < directories3.Length; k++)
            {
                string text5 = k.ToString("0");
                if (!Directory.Exists(text + text5 + "\\"))
                {
                    break;
                }
                string[] files = Directory.GetFiles(text + text5 + "\\", "frame_*.png");
                list_3.Add(new Bitmap[files.Length]);
                for (int l = 0; l < files.Length; l++)
                {
                    string text6 = text + text5 + "\\frame_" + l + ".png";
                    if (!File.Exists(text6))
                    {
                        break;
                    }
                    int localK = k;
                    int localL = l;
                    taskList.Add(Task.Run(() =>
                    {
                        Bitmap bitmap = method_13(text6, bool_28: true, double_8);
                        Bitmap bitmap2 = bitmap;
                        bitmap = method_21(bitmap);
                        bitmap2.Dispose();
                        list_3[localK][localL] = bitmap;
                        list_3[localK][localL].RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }));
                }
                if (!Directory.Exists(text3 + text5 + "\\"))
                {
                    break;
                }
                string[] files2 = Directory.GetFiles(text3 + text5 + "\\", "frame_*.png");
                list_4.Add(new Bitmap[files2.Length]);
                for (int m = 0; m < files2.Length; m++)
                {
                    string text7 = text3 + text5 + "\\frame_" + m + ".png";
                    if (!File.Exists(text7))
                    {
                        break;
                    }
                    int localK = k;
                    int localM = m;
                    taskList.Add(Task.Run(() =>
                    {
                        Bitmap bitmap3 = method_13(text7, bool_28: true, double_8);
                        Bitmap bitmap4 = bitmap3;
                        bitmap3 = method_21(bitmap3);
                        bitmap4.Dispose();
                        list_4[localK][localM] = bitmap3;
                        list_4[localK][localM].RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }));
                }
            }
        }

        internal void LoadEffects(string gameImages, string customizationPath, double imageScale)
        {
            List<Task<Bitmap>> taskList = new List<Task<Bitmap>>();
            List<Task> taskList2 = new List<Task>();
            string gameImagesEffects = gameImages + "effects\\";
            string customizationEffects = customizationPath + "effects\\";
            string currentEnginesDir = gameImagesEffects + "enginethrusters\\";
            string custEngines = customizationEffects + "enginethrusters\\";

            GraphicsHelper.DisposeImageArray(bitmap_209);
            GraphicsHelper.DisposeImageArray(bitmap_18);
            GraphicsHelper.DisposeImageArray(bitmap_210);
            GraphicsHelper.DisposeImageArray(bitmap_215);
            if (Directory.Exists(custEngines))
            {
                string[] files = Directory.GetFiles(custEngines, "*.png");
                if (files.Length > 0)
                {
                    currentEnginesDir = custEngines;
                }
            }
            string[] files2 = Directory.GetFiles(currentEnginesDir, "*.png");
            List<Bitmap> list = new List<Bitmap>();
            for (int i = 0; i < files2.Length; i++)
            {
                string text5 = i + ".png";
                taskList.Add(Task.Run(() =>
                {
                    if (!File.Exists(currentEnginesDir + text5))
                    {
                        return null;
                    }
                    Bitmap bitmap = method_12(currentEnginesDir + text5, bool_28: true);
                    if (bitmap == null)
                    {
                        return null;
                    }
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    return bitmap;
                }));
                //list.Add(bitmap);
            }
            string string_32 = gameImagesEffects + "other\\shieldstrike.png";
            if (File.Exists(customizationEffects + "other\\shieldstrike.png"))
            {
                string_32 = customizationEffects + "other\\shieldstrike.png";
            }
            taskList2.Add(Task.Run(() => bitmap_17 = method_12(string_32, bool_28: true)));
            string text6 = gameImagesEffects + "other\\";
            if (File.Exists(customizationEffects + "other\\planetaryshield_0.png"))
            {
                taskList2.Add(Task.Run(() => bitmap_18[0] = method_12(customizationEffects + "other\\planetaryshield_0.png", bool_28: true)));
            }
            else
            {
                taskList2.Add(Task.Run(() => bitmap_18[0] = method_12(text6 + "planetaryshield_0.png", bool_28: true)));
            }
            if (File.Exists(customizationEffects + "other\\planetaryshield_1.png"))
            {
                taskList2.Add(Task.Run(() => bitmap_18[1] = method_12(customizationEffects + "other\\planetaryshield_1.png", bool_28: true)));
            }
            else
            {
                taskList2.Add(Task.Run(() => bitmap_18[1] = method_12(text6 + "planetaryshield_1.png", bool_28: true)));
            }
            string string_33 = gameImagesEffects + "lights\\light.png";
            if (File.Exists(customizationEffects + "lights\\light.png"))
            {
                string_33 = customizationEffects + "lights\\light.png";
            }
            taskList2.Add(Task.Run(() => bitmap_5 = method_12(string_33, bool_28: true)));
            string string_34 = gameImagesEffects + "longrangescanners\\lrs.png";
            if (File.Exists(customizationEffects + "longrangescanners\\lrs.png"))
            {
                string_34 = customizationEffects + "longrangescanners\\lrs.png";
            }
            taskList2.Add(Task.Run(() => bitmap_188 = method_12(string_34, bool_28: true)));
            string string_35 = gameImagesEffects + "systeminfluence\\systeminfluence.png";
            if (File.Exists(customizationEffects + "systeminfluence\\systeminfluence.png"))
            {
                string_35 = customizationEffects + "systeminfluence\\systeminfluence.png";
            }
            taskList2.Add(Task.Run(() => bitmap_187 = method_12(string_35, bool_28: true)));
            string text7 = gameImagesEffects + "mining\\";
            if (Directory.Exists(customizationEffects + "mining\\"))
            {
                string[] files3 = Directory.GetFiles(customizationEffects + "mining\\", "*.png");
                if (files3.Length > 0)
                {
                    text7 = customizationEffects + "mining\\";
                }
            }
            files2 = Directory.GetFiles(text7, "*.png");
            int num = files2.Length;
            bitmap_210 = new Bitmap[num];
            for (int j = 0; j < num; j++)
            {
                int localJ = j;
                taskList2.Add(Task.Run(() =>
                {
                    string string_36 = text7 + "Frame_" + (localJ + 1).ToString("000") + ".png";
                    Bitmap bitmap2 = method_13(string_36, bool_28: true, imageScale);
                    bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    bitmap_210[localJ] = bitmap2;
                }));
            }
            string text8 = gameImagesEffects + "gasmining\\";
            if (Directory.Exists(customizationEffects + "gasmining\\"))
            {
                string[] files4 = Directory.GetFiles(customizationEffects + "gasmining\\", "*.png");
                if (files4.Length > 0)
                {
                    text8 = customizationEffects + "gasmining\\";
                }
            }
            files2 = Directory.GetFiles(text8, "*.png");
            int num2 = files2.Length;
            GraphicsHelper.DisposeImageArray(bitmap_211);
            bitmap_211 = new Bitmap[num2];
            for (int k = 0; k < num2; k++)
            {
                int localK = k;
                taskList2.Add(Task.Run(() =>
                {
                    string string_37 = text8 + "Frame_" + (localK + 1).ToString("000") + ".png";
                    Bitmap bitmap2 = method_13(string_37, bool_28: true, imageScale);
                    bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    bitmap_211[localK] = bitmap2;
                }));
            }
            string text9 = gameImagesEffects + "construction\\";
            if (Directory.Exists(customizationEffects + "construction\\"))
            {
                string[] files5 = Directory.GetFiles(customizationEffects + "construction\\", "*.png");
                if (files5.Length > 0)
                {
                    text9 = customizationEffects + "construction\\";
                }
            }
            files2 = Directory.GetFiles(text9, "*.png");
            int num3 = files2.Length;
            GraphicsHelper.DisposeImageArray(bitmap_212);
            bitmap_212 = new Bitmap[num3];
            for (int l = 0; l < num3; l++)
            {
                int localL = l;
                taskList2.Add(Task.Run(() =>
                {
                    string string_38 = text9 + "Frame_" + (localL + 1).ToString("000") + ".png";
                    Bitmap bitmap2 = method_13(string_38, bool_28: true, imageScale);
                    bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    bitmap_212[localL] = bitmap2;
                }));
            }
            string text10 = gameImagesEffects + "tractorbeamstrike\\";
            if (Directory.Exists(customizationEffects + "tractorbeamstrike\\"))
            {
                string[] files6 = Directory.GetFiles(customizationEffects + "tractorbeamstrike\\", "*.png");
                if (files6.Length > 0)
                {
                    text10 = customizationEffects + "tractorbeamstrike\\";
                }
            }
            files2 = Directory.GetFiles(text10, "*.png");
            int num4 = files2.Length;
            bitmap_215 = new Bitmap[num4];
            for (int m = 0; m < num4; m++)
            {
                int localM = m;
                taskList2.Add(Task.Run(() =>
                {
                    string string_39 = text10 + (localM + 1).ToString("00") + ".png";
                    Bitmap bitmap2 = method_13(string_39, bool_28: true, imageScale);
                    bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    bitmap_215[localM] = bitmap2;
                }));
            }
            Task.WaitAll(taskList.ToArray());
            foreach (var item in taskList)
            {
                if (item.Result != null)
                { list.Add(item.Result); }
            }
            bitmap_209 = list.ToArray();
            Task.WaitAll(taskList2.ToArray());
        }

        internal void LoadEnvironmentOverlays(string gameImages, string customizationPath)
        {
            string gameOverlays = gameImages + "environment\\overlays\\";
            string custOverlays = customizationPath + "environment\\overlays\\";
            List<Task> taskList = new List<Task>();

            taskList.Add(Task.Run(() =>
            {
                string shadowsFilePath = "shadow\\shading.png";
                bitmap_191 = method_10(custOverlays, gameOverlays, shadowsFilePath, bool_28: true);
                FastBitmap fastBitmap = new FastBitmap(bitmap_191);
                for (int i = 0; i < bitmap_191.Width; i++)
                {
                    for (int j = 0; j < bitmap_191.Height; j++)
                    {
                        Color pixel = fastBitmap.GetPixel(ref i, ref j);
                        int alpha = (int)((double)(pixel.R + pixel.G + pixel.B) / 3.0);
                        if (pixel.R == 0 && pixel.G == 0 && pixel.B == 0)
                        {
                            alpha = 0;
                        }
                        fastBitmap.SetPixel(ref i, ref j, Color.FromArgb(alpha, 0, 0, 0));
                    }
                }
                fastBitmap.Release();
                bitmap_191.RotateFlip(RotateFlipType.Rotate270FlipNone);
            }));


            bitmap_192 = new Bitmap[GalaxyImages.CloudImageCount];
            for (int k = 0; k < GalaxyImages.CloudImageCount; k++)
            {
                string shadowsFilePath = "clouds\\cloudOverlay_" + k + ".png";

                int localK = k;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap = method_10(custOverlays, gameOverlays, shadowsFilePath, bool_28: true);
                    FastBitmap fastBitmap2 = new FastBitmap(bitmap);
                    for (int l = 0; l < bitmap.Width; l++)
                    {
                        for (int m = 0; m < bitmap.Height; m++)
                        {
                            Color pixel2 = fastBitmap2.GetPixel(ref l, ref m);
                            int num = (int)((double)(pixel2.R + pixel2.G + pixel2.B) / 3.0);
                            if (num <= 56)
                            {
                                num = 0;
                            }
                            fastBitmap2.SetPixel(ref l, ref m, Color.FromArgb(num, 255, 255, 255));
                        }
                    }
                    fastBitmap2.Release();
                    bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap_192[localK] = bitmap;
                }));
            }

            bitmap_193 = new Bitmap[GalaxyImages.CloudSparseImageCount];
            for (int n = 0; n < GalaxyImages.CloudSparseImageCount; n++)
            {
                string shadowsFilePath = "cloudssparse\\cloudOverlaySparse_" + n + ".png";
                int localN = n;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap2 = method_10(custOverlays, gameOverlays, shadowsFilePath, bool_28: true);
                    FastBitmap fastBitmap3 = new FastBitmap(bitmap2);
                    for (int X = 0; X < bitmap2.Width; X++)
                    {
                        for (int Y = 0; Y < bitmap2.Height; Y++)
                        {
                            Color pixel3 = fastBitmap3.GetPixel(ref X, ref Y);
                            int num2 = (int)((double)(pixel3.R + pixel3.G + pixel3.B) / 3.0);
                            if (num2 <= 56)
                            {
                                num2 = 0;
                            }
                            fastBitmap3.SetPixel(ref X, ref Y, Color.FromArgb(num2, 255, 255, 255));
                        }
                    }
                    fastBitmap3.Release();
                    bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    bitmap_193[localN] = bitmap2;
                }));
            }

            bitmap_194 = new Bitmap[GalaxyImages.HabitatQualityDamageImageCount];
            taskList.Add(Task.Run(() =>
            {

                string shadowsFilePath = "damage\\damage.png";
                int num3 = 60;
                int num4 = 27;
                Bitmap original = method_10(custOverlays, gameOverlays, shadowsFilePath, bool_28: true);
                for (int num5 = 0; num5 < GalaxyImages.HabitatQualityDamageImageCount; num5++)
                {
                    Bitmap bitmap3 = new Bitmap(original);
                    FastBitmap fastBitmap4 = new FastBitmap(bitmap3);
                    for (int X2 = 0; X2 < bitmap3.Width; X2++)
                    {
                        for (int Y2 = 0; Y2 < bitmap3.Height; Y2++)
                        {
                            Color pixel4 = fastBitmap4.GetPixel(ref X2, ref Y2);
                            int num6 = (int)((double)(pixel4.R + pixel4.G + pixel4.B) / 3.0);
                            if (pixel4.A == 0)
                            {
                                num6 = 0;
                            }
                            else if (num6 > num3)
                            {
                                num6 = 0;
                            }
                            else
                            {
                                num6 = (int)((double)(num3 - num6) * (255.0 / (double)num3));
                                num6 += 16;
                                num6 = Math.Max(0, Math.Min(num6, 255));
                            }
                            fastBitmap4.SetPixel(ref X2, ref Y2, Color.FromArgb(num6, 0, 0, 0));
                        }
                    }
                    fastBitmap4.Release();
                    bitmap_194[num5] = bitmap3;
                    num3 += num4;
                }
            }));
            string path = gameImages + "environment\\planets\\volcanic\\";
            string text = customizationPath + "environment\\planets\\volcanic\\";
            if (Directory.Exists(text))
            {
                string[] files = Directory.GetFiles(path, "VolcanicG*");
                if (files.Length > 0)
                {
                    path = text;
                }
            }
            bitmap_195 = new Bitmap[GalaxyImages.HabitatImageCountVolcanic];
            string[] files2 = Directory.GetFiles(path, "VolcanicG*");
            for (int num7 = 0; num7 < GalaxyImages.HabitatImageCountVolcanic; num7++)
            {
                int localNum7 = num7;
                taskList.Add(Task.Run(() =>
                {
                    Bitmap bitmap4 = method_12(files2[localNum7], bool_28: true);
                    bitmap4.MakeTransparent();
                    bitmap_195[localNum7] = bitmap4;
                }));
            }

            Task.WaitAll(taskList.ToArray());
        }

        private void LoadGenerateRings(string string_30, double double_7)
        {
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                int localI = i;
                taskList.Add(Task.Run(() =>
                {
                    PlanetaryRingsGenerator planetaryRingsGenerator = new PlanetaryRingsGenerator();
                    bitmap_1[localI] = planetaryRingsGenerator.GenerateRings(localI, 700);
                }));
            }
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadUiPlagues(string string_30, string string_31)
        {
            string text = string_30 + "ui\\plagues\\";
            string text2 = string_31 + "ui\\plagues\\";
            GraphicsHelper.DisposeImageArray(bitmap_9);
            bitmap_9 = new Bitmap[Galaxy.PlaguesStatic.Count];
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < Galaxy.PlaguesStatic.Count; i++)
            {
                string text3 = "plague_" + i + ".png";
                string text4 = text + text3;
                string text5 = text2 + text3;
                if (!string.IsNullOrEmpty(string_31) && File.Exists(text5))
                {
                    text4 = text5;
                }
                if (File.Exists(text4))
                {
                    int localI = i;
                    taskList.Add(Task.Run(() => bitmap_9[localI] = method_12(text4, bool_28: true)));
                }
                else
                {
                    bitmap_9[i] = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
                }
            }
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadPlanetaryFacilities(string string_30, string string_31)
        {
            string text = string_30 + "environment\\planetaryfacilities\\";
            string text2 = string_31 + "environment\\planetaryfacilities\\";
            List<Task> taskList = new List<Task>();

            GraphicsHelper.DisposeImageArray(bitmap_8);
            bitmap_8 = new Bitmap[Galaxy.PlanetaryFacilityDefinitionsStatic.Count];
            for (int i = 0; i < Galaxy.PlanetaryFacilityDefinitionsStatic.Count; i++)
            {
                string text3 = "facility_" + i + ".png";
                string text4 = text + text3;
                string text5 = text2 + text3;
                int localI = i;
                if (!string.IsNullOrEmpty(string_31) && File.Exists(text5))
                {
                    text4 = text5;
                }
                if (File.Exists(text4))
                {
                    taskList.Add(Task.Run(() => bitmap_8[localI] = method_12(text4, bool_28: true)));
                }
                else
                {
                    bitmap_8[i] = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
                }
            }
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadRuins(string gameImages, string customizationPath)
        {
            string gameRuins = gameImages + "environment\\ruins\\";
            string custRuins = customizationPath + "environment\\ruins\\";

            List<Task> taskList = new List<Task>();
            List<string> list = new List<string>();
            DirectoryInfo directoryInfo = new DirectoryInfo(gameRuins);
            FileInfo[] files = directoryInfo.GetFiles("ruin_*.png");
            foreach (FileInfo fileInfo in files)
            {
                if (fileInfo != null && !list.Contains(fileInfo.Name))
                {
                    list.Add(fileInfo.Name);
                }
            }
            if (!string.IsNullOrEmpty(customizationPath) && Directory.Exists(custRuins))
            {
                DirectoryInfo directoryInfo2 = new DirectoryInfo(custRuins);
                FileInfo[] files2 = directoryInfo2.GetFiles("ruin_*.png");
                foreach (FileInfo fileInfo2 in files2)
                {
                    if (fileInfo2 != null && !list.Contains(fileInfo2.Name))
                    {
                        list.Add(fileInfo2.Name);
                    }
                }
            }
            GraphicsHelper.DisposeImageArray(bitmap_2);
            bitmap_2 = new Bitmap[list.Count];
            for (int k = 0; k < list.Count; k++)
            {
                string text3 = "ruin_" + k + ".png";
                string text4 = gameRuins + text3;
                string text5 = custRuins + text3;

                int localK = k;
                if (!string.IsNullOrEmpty(customizationPath) && File.Exists(text5))
                {
                    text4 = text5;
                }
                if (File.Exists(text4))
                {
                    taskList.Add(Task.Run(() => bitmap_2[localK] = method_12(text4, bool_28: true)));
                }
                else
                {
                    bitmap_2[localK] = new Bitmap(1, 1, PixelFormat.Format32bppPArgb);
                }
            }
            Task.WaitAll(taskList.ToArray());
        }

        private Bitmap method_35(string string_30)
        {
            Bitmap result = bitmap_190;
            string text = Application.StartupPath + "\\images\\ui\\chrome\\";
            string text2 = text + string_30;
            if (File.Exists(text2))
            {
                result = method_12(text2, bool_28: false);
            }
            return result;
        }

        private void LoadFighterBomboer(string string_30, string string_31, double double_7, ref int int_64)
        {
            Bitmap bitmap = null;
            if (File.Exists(string_31 + ".png") || File.Exists(string_31 + ".bmp"))
            {
                string_30 = string_31;
            }
            Color color_ = Color.FromArgb(0, 0, 0, 0);
            bool flag = false;
            if (bitmap == null)
            {
                if (File.Exists(string_30 + ".png"))
                {
                    bitmap = method_12(string_30 + ".png", bool_28: true);
                    flag = true;
                }
                else
                {
                    bitmap = method_12(string_30 + ".bmp", bool_28: true);
                    color_ = Color.Black;
                }
            }
            Bitmap bitmap2 = bitmap;
            bitmap = method_9(bitmap, 4, color_);
            bitmap2.Dispose();
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
            List<Rectangle> list_ = method_101(bitmap, Color.Blue, 20);
            if (bitmap.Width >= 100)
            {
                method_37(double_7, ref bitmap, ref list_, 100);
            }
            Bitmap bitmap3 = method_105(bitmap, color_, Color.Black);
            if (!flag)
            {
                bitmap.MakeTransparent(Color.Black);
            }
            if (bitmap_6[int_64] != null)
            {
                bitmap_6[int_64].Dispose();
                bitmap_6[int_64] = null;
            }
            if (bitmap_7[int_64] != null)
            {
                bitmap_7[int_64].Dispose();
                bitmap_7[int_64] = null;
            }
            bitmap_6[int_64] = bitmap;
            int_5[int_64] = method_8(bitmap);
            bitmap_7[int_64] = bitmap3;
            list_2[int_64] = list_;
            int_64++;
        }

        private void method_37(double double_7, ref Bitmap bitmap_225, ref List<Rectangle> list_7, int int_64)
        {
            double num = double_7;
            if ((int)((double)bitmap_225.Width * double_7) < int_64)
            {
                num = (double)int_64 / (double)bitmap_225.Width;
            }
            else if ((int)((double)bitmap_225.Height * double_7) < int_64)
            {
                num = (double)int_64 / (double)bitmap_225.Height;
            }
            for (int i = 0; i < list_7.Count; i++)
            {
                Rectangle rectangle = list_7[i];
                Rectangle value = new Rectangle((int)((double)rectangle.X * num), (int)((double)rectangle.Y * num), (int)((double)rectangle.Width * num), (int)((double)rectangle.Height * num));
                list_7[i] = value;
            }
            Bitmap bitmap = bitmap_225;
            bitmap_225 = PrecacheScaledBitmap(bitmap_225, (int)((double)bitmap_225.Width * num), (int)((double)bitmap_225.Height * num), InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            bitmap.Dispose();
        }

        private void method_38(double double_7, ref Bitmap bitmap_225, ref List<Rectangle> list_7, ref List<Point> list_8, int int_64)
        {
            double num = double_7;
            if ((int)((double)bitmap_225.Width * double_7) < int_64)
            {
                num = (double)int_64 / (double)bitmap_225.Width;
            }
            else if ((int)((double)bitmap_225.Height * double_7) < int_64)
            {
                num = (double)int_64 / (double)bitmap_225.Height;
            }
            for (int i = 0; i < list_7.Count; i++)
            {
                Rectangle rectangle = list_7[i];
                Rectangle value = new Rectangle((int)((double)rectangle.X * num), (int)((double)rectangle.Y * num), (int)((double)rectangle.Width * num), (int)((double)rectangle.Height * num));
                list_7[i] = value;
            }
            for (int j = 0; j < list_8.Count; j++)
            {
                Point point = list_8[j];
                Point value2 = new Point((int)((double)point.X * num), (int)((double)point.Y * num));
                list_8[j] = value2;
            }
            Bitmap bitmap = bitmap_225;
            bitmap_225 = PrecacheScaledBitmap(bitmap_225, (int)((double)bitmap_225.Width * num), (int)((double)bitmap_225.Height * num), InterpolationMode.HighQualityBicubic, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
            bitmap.Dispose();
        }

        internal void LoadFighters(string string_30, string string_31, double double_7)
        {
            string origShipsFolder = string_30 + "units\\ships\\";
            string modShipsFolder = string_31 + "units\\ships\\";
            double double_8 = double_7 * 1.0;
            int int_ = 0;
            //int num = 0;
            //int num2 = 50;
            //while (true)
            List<DirectoryInfo> origDir = new DirectoryInfo(origShipsFolder).EnumerateDirectories("family*").ToList();
            List<int> familyNumbers = null;
            if (!string.IsNullOrEmpty(string_31))
            {
                DirectoryInfo modDir = new DirectoryInfo(modShipsFolder);
                List<DirectoryInfo> modDirFamilyList = new List<DirectoryInfo>();
                if (modDir.Exists)
                {
                    modDirFamilyList = modDir.EnumerateDirectories("family*").ToList();
                }
                familyNumbers = origDir.Union(modDirFamilyList).Select(x =>
                {
                    int.TryParse(x.Name.Substring(6), out int res);
                    return res;
                }).Distinct().ToList();
            }
            else
            {
                familyNumbers = origDir.Select(x =>
                {
                    int.TryParse(x.Name.Substring(6), out int res);
                    return res;
                }).Distinct().ToList();
            }
            familyNumbers.Sort();
            foreach (var item in familyNumbers)
            {
                string text3 = origShipsFolder + "family" + item + "\\";
                string text4 = modShipsFolder + "family" + item + "\\";
                //if (num < num2 && (Directory.Exists(text3) || Directory.Exists(text4)))
                if (Directory.Exists(text3) || Directory.Exists(text4))
                {
                    if (bitmap_6.Length <= int_)
                    {
                        method_40(24, 2);
                    }
                    int localI = int_;
                    LoadFighterBomboer(text3 + "Fighter", text4 + "Fighter", double_8, ref int_);
                    LoadFighterBomboer(text3 + "Bomber", text4 + "Bomber", double_8, ref int_);
                    //num++;
                    continue;
                }
                //break;
            }
            UpdateSplashProgress();
        }

        private void method_40(int int_64, int int_65)
        {
            Bitmap[] array = null;
            int[] array2 = null;
            List<Rectangle>[] array3 = null;
            array = new Bitmap[bitmap_6.Length];
            Array.Copy(bitmap_6, array, bitmap_6.Length);
            bitmap_6 = new Bitmap[bitmap_6.Length + int_65];
            Array.Copy(array, bitmap_6, array.Length);
            array = new Bitmap[bitmap_7.Length];
            Array.Copy(bitmap_7, array, bitmap_7.Length);
            bitmap_7 = new Bitmap[bitmap_7.Length + int_65];
            Array.Copy(array, bitmap_7, array.Length);
            array2 = new int[int_5.Length];
            Array.Copy(int_5, array2, int_5.Length);
            int_5 = new int[int_5.Length + int_65];
            Array.Copy(array2, int_5, array2.Length);
            array3 = new List<Rectangle>[list_2.Length];
            Array.Copy(list_2, array3, list_2.Length);
            list_2 = new List<Rectangle>[list_2.Length + int_65];
            Array.Copy(array3, list_2, array3.Length);
        }

        private void LoadCreaturesImpl(string string_30, string string_31, string string_32, int int_64, double double_7, int int_65)
        {
            bitmap_10[int_65] = new Bitmap[int_64];
            bitmap_11[int_65] = new Bitmap[int_64];
            texture2D_0[int_65] = new Texture2D[int_64];
            for (int i = 0; i < int_64; i++)
            {
                string string_33 = string_30 + string_32 + i.ToString("00000") + ".png";
                string text = string_31 + string_32 + i.ToString("00000") + ".png";
                if (File.Exists(text))
                {
                    string_33 = text;
                }
                bitmap_10[int_65][i] = method_13(string_33, bool_28: true, double_7);
                bitmap_10[int_65][i].RotateFlip(RotateFlipType.Rotate90FlipNone);
                bitmap_10[int_65][i].MakeTransparent();
                if (i == 0)
                {
                    sveqhmNacy[int_65] = method_8(bitmap_10[int_65][i]);
                }
                Bitmap bitmap = bitmap_10[int_65][i];
                bitmap_10[int_65][i] = method_9(bitmap_10[int_65][i], 0, Color.Transparent);
                bitmap.Dispose();
                bitmap_11[int_65][i] = method_104(bitmap_10[int_65][i], Color.Black);
            }
            //int_65++;
        }

        private void LoadCreatures(string string_30, string string_31, double double_7)
        {
            string text = string_30 + "units\\creatures\\";
            string text2 = string_31 + "units\\creatures\\";
            double double_8 = double_7 * 0.6;
            //int int_ = 0;
            List<Task> taskList = new List<Task>();

            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "spaceslug\\", text2 + "spaceslug\\", "Slug_", 12, double_8, 0)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "sandslug\\", text2 + "sandslug\\", "Sandworm_", 12, double_8, 1)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "kaltor\\", text2 + "kaltor\\", "Kaltor_", 12, double_8, 2)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "ardilus\\", text2 + "ardilus\\", "ArdillusMoving2_", 12, double_8, 3)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "silvermist\\", text2 + "silvermist\\", "SilverMist_", 12, double_8, 4)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "spaceslug\\", text2 + "spaceslug\\", "SlugAttack_", 0, double_8, 5)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "sandslug\\", text2 + "sandslug\\", "SandwormAttack_", 0, double_8, 6)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "kaltor\\", text2 + "kaltor\\", "KaltorAttack_", 8, double_8, 7)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "ardilus\\", text2 + "ardilus\\", "ArdillusMoving_", 12, double_8, 8)));
            taskList.Add(Task.Run(() => LoadCreaturesImpl(text + "silvermist\\", text2 + "silvermist\\", "SilverMist_", 12, double_8, 9)));

            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadTroops(string string_30, string string_31, string string_32)
        {
            string text = string_30 + "units\\troops\\";
            string text2 = string_31 + "units\\troops\\";
            int num = 20;
            RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, string_32);
            if (raceList.Count >= 20)
            {
                num = raceList.Count;
            }
            bitmap_23 = new Bitmap[num + 1];
            bitmap_24 = new Bitmap[num + 1];
            bitmap_25 = new Bitmap[num + 1];
            bitmap_26 = new Bitmap[num + 1];
            bitmap_27 = new Bitmap[num + 1];
            ReaderWriterLockSlim[] readerWriterLockSlim = new ReaderWriterLockSlim[num + 1];
            //string empty = string.Empty;
            //string empty2 = string.Empty;
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < num; i++)
            {
                int localI = i;
                string text3 = "Troop_" + i + ".png";
                string local1 = text + text3;
                string local2 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_23[localI] = method_12(local2, bool_28: false);
                    if (bitmap_23[localI] == null)
                    {
                        bitmap_23[localI] = method_12(local1, bool_28: true);
                        readerWriterLockSlim[localI] = new ReaderWriterLockSlim();
                    }
                    bitmap_23[localI].MakeTransparent();
                }));
                Task.WaitAll(taskList.ToArray());
                taskList.Clear();
                text3 = "Troop_" + i + "_Armored.png";
                string local3 = text + text3;
                string local4 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_24[localI] = method_12(local4, bool_28: false);
                    if (bitmap_24[localI] == null)
                    {
                        bitmap_24[localI] = method_12(local3, bool_28: false);
                    }
                    if (bitmap_24[localI] == null)
                    {
                        readerWriterLockSlim[localI].EnterWriteLock();
                        bitmap_24[localI] = new Bitmap(bitmap_23[localI]);
                        readerWriterLockSlim[localI].ExitWriteLock();
                    }
                    bitmap_24[localI].MakeTransparent();
                }));
                text3 = "Troop_" + i + "_Artillery.png";
                string local5 = text + text3;
                string local6 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_25[localI] = method_12(local6, bool_28: false);
                    if (bitmap_25[localI] == null)
                    {
                        bitmap_25[localI] = method_12(local5, bool_28: false);
                    }
                    if (bitmap_25[localI] == null)
                    {
                        readerWriterLockSlim[localI].EnterWriteLock();
                        bitmap_25[localI] = new Bitmap(bitmap_23[localI]);
                        readerWriterLockSlim[localI].ExitWriteLock();
                    }
                    bitmap_25[localI].MakeTransparent();
                }));
                text3 = "Troop_" + i + "_SpecialForces.png";
                string local7 = text + text3;
                string local8 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_26[localI] = method_12(local8, bool_28: false);
                    if (bitmap_26[localI] == null)
                    {
                        bitmap_26[localI] = method_12(local7, bool_28: false);
                    }
                    if (bitmap_26[localI] == null)
                    {
                        readerWriterLockSlim[localI].EnterWriteLock();
                        bitmap_26[localI] = new Bitmap(bitmap_23[localI]);
                        readerWriterLockSlim[localI].ExitWriteLock();   
                    }
                    bitmap_26[localI].MakeTransparent();
                }));
                text3 = "Troop_" + i + "_PirateRaider.png";
                string local9 = text + text3;
                string local10 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    bitmap_27[localI] = method_12(local10, bool_28: false);
                    if (bitmap_27[localI] == null)
                    {
                        bitmap_27[localI] = method_12(local9, bool_28: false);
                    }
                    if (bitmap_27[localI] == null)
                    {
                        readerWriterLockSlim[localI].EnterWriteLock();
                        bitmap_27[localI] = new Bitmap(bitmap_23[localI]);
                        readerWriterLockSlim[localI].ExitWriteLock();
                    }
                    bitmap_27[localI].MakeTransparent();
                }));
            }
            string local11 = text + "RoboticTroop.png";
            string local12 = text2 + "RoboticTroop.png";
            taskList.Add(Task.Run(() =>
            {
                bitmap_23[num] = method_12(local12, bool_28: false);
                if (bitmap_23[num] == null)
                {
                    bitmap_23[num] = method_12(local11, bool_28: true);
                }
                bitmap_23[num].MakeTransparent();
            }));
            Task.WaitAll(taskList.ToArray());
            bitmap_24[num] = new Bitmap(bitmap_23[num]);
            bitmap_25[num] = new Bitmap(bitmap_23[num]);
            bitmap_26[num] = new Bitmap(bitmap_23[num]);
            bitmap_27[num] = new Bitmap(bitmap_23[num]);
        }

        internal void LoadRacesImg(string string_30, string string_31, string string_32)
        {
            string text = string_30 + "units\\races\\";
            string text2 = string_31 + "units\\races\\";
            List<Task> taskList = new List<Task>();
            Bitmap[] array = new Bitmap[1];
            Bitmap[] array2 = new Bitmap[1];
            RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, string_32);
            if (raceList.Count >= array.Length)
            {
                array = new Bitmap[raceList.Count];
                array2 = new Bitmap[raceList.Count];
                _ = raceList.Count;
            }
            for (int i = 0; i < raceList.Count; i++)
            {
                string text3 = "race_" + i + ".png";
                string local1 = text + text3;
                string local2 = text2 + text3;
                int localI = i;
                taskList.Add(Task.Run(() =>
                {
                    array[localI] = method_12(local2, bool_28: false);
                    if (array[localI] == null)
                    {
                        array[localI] = method_12(local1, bool_28: true);
                    }
                }));
                text3 = "race_" + i + "a.png";
                string local3 = text + text3;
                string local4 = text2 + text3;
                taskList.Add(Task.Run(() =>
                {
                    array2[localI] = method_12(local4, bool_28: false);
                    if (array2[localI] == null)
                    {
                        array2[localI] = method_12(local3, bool_28: false);
                    }
                }));
            }
            string text4 = string_30 + "units\\races\\pirates\\";
            string text5 = string_31 + "units\\races\\pirates\\";
            Bitmap[] array3 = new Bitmap[5];
            Bitmap[] array4 = new Bitmap[5];
            string[] array5 = new string[5] { "balanced", "raider", "mercenary", "smuggler", "legendary" };
            for (int j = 0; j < 4; j++)
            {
                int localJ = j;
                string text6 = array5[j] + ".png";
                string local1 = text4 + text6;
                string local2 = text5 + text6;
                taskList.Add(Task.Run(() =>
                {
                    array3[localJ] = method_12(local2, bool_28: false);
                    if (array3[localJ] == null)
                    {
                        array3[localJ] = method_12(local1, bool_28: true);
                    }
                }));

                text6 = array5[j] + "_a.png";
                string local3 = text4 + text6;
                string local4 = text5 + text6;
                taskList.Add(Task.Run(() =>
                {
                    array4[localJ] = method_12(local4, bool_28: false);
                    if (array4[localJ] == null)
                    {
                        array4[localJ] = method_12(local3, bool_28: false);
                    }
                }));
            }
            taskList.Add(Task.Run(() =>
            {
                if (File.Exists(text5 + array5[4] + ".png"))
                {
                    array3[4] = method_12(text5 + array5[4] + ".png", bool_28: false);
                }
                else if (File.Exists(text4 + array5[4] + ".png"))
                {
                    array3[4] = method_12(text4 + array5[4] + ".png", bool_28: true);
                }
            }));
            taskList.Add(Task.Run(() =>
            {
                if (File.Exists(text5 + array5[4] + "_a.png"))
                {
                    array4[4] = method_12(text5 + array5[4] + "_a.png", bool_28: false);
                }
                else if (File.Exists(text4 + array5[4] + "_a.png"))
                {
                    array4[4] = method_12(text4 + array5[4] + "_a.png", bool_28: false);
                }
            }));
            Task.WaitAll(taskList.ToArray());
            if (array3[4] == null)
            {
                array3 = array3.Take(4).ToArray();
            }
            if (array4[4] == null)
            {
                array4 = array4.Take(4).ToArray();
            }
            if (raceImageCache_0 != null)
            {
                raceImageCache_0.Clear();
            }
            raceImageCache_0 = new RaceImageCache();
            raceImageCache_0.Initialize(array, array2, array3, array4);
        }

        internal void LoadUiComponents(string string_30, string customSet)
        {
            string originalPath = string_30 + "ui\\components\\";
            string custmomPath = customSet + "ui\\components\\";
            List<Task> taskList = new List<Task>();
            if (!File.Exists(custmomPath + "unbuilt.png"))
            {
                taskList.Add(Task.Run(() => bitmap_22[0] = method_12(originalPath + "unbuilt.png", bool_28: true)));
            }
            else
            {
                taskList.Add(Task.Run(() => bitmap_22[0] = method_12(custmomPath + "unbuilt.png", bool_28: true)));
            }
            if (!File.Exists(custmomPath + "damaged.png"))
            {
                taskList.Add(Task.Run(() => bitmap_22[1] = method_12(originalPath + "damaged.png", bool_28: true)));
            }
            else
            {
                taskList.Add(Task.Run(() => bitmap_22[1] = method_12(custmomPath + "damaged.png", bool_28: true)));
            }
            bitmap_21 = new Bitmap[Galaxy.ComponentDefinitionsStatic.Length];
            for (int i = 0; i < bitmap_21.Length; i++)
            {
                int localI = i;
                bool flag = true;
                string pngName = "Component_" + Galaxy.ComponentDefinitionsStatic[i].PictureRef + ".png";
                string bmpName = "Component_" + Galaxy.ComponentDefinitionsStatic[i].PictureRef + ".bmp";
                string imgPath = string.Empty;
                string customPngPath = custmomPath + pngName;
                string customBmpPath = custmomPath + bmpName;
                string originalPngPath = originalPath + pngName;
                string originalBmpPath = originalPath + bmpName;
                if (!string.IsNullOrEmpty(customSet))
                {
                    imgPath = customPngPath;
                    flag = true;
                    if (!File.Exists(customPngPath))
                    {
                        imgPath = customBmpPath;
                        flag = false;
                        if (!File.Exists(customBmpPath))
                        {
                            imgPath = originalPngPath;
                            flag = true;
                            if (!File.Exists(originalPngPath))
                            {
                                imgPath = originalBmpPath;
                                flag = false;
                            }
                        }
                    }
                }
                else
                {
                    imgPath = originalPngPath;
                    flag = true;
                    if (!File.Exists(originalPngPath))
                    {
                        imgPath = originalBmpPath;
                        flag = false;
                    }
                }
                taskList.Add(Task.Run(() =>
                {
                    bitmap_21[localI] = method_12(imgPath, bool_28: true);
                    if (!flag)
                    {
                        bitmap_21[localI].MakeTransparent(Color.FromArgb(21, 21, 28));
                    }
                }));
            }
            Task.WaitAll(taskList.ToArray());
        }

        internal void LoadUiResources(string string_30, string string_31)
        {
            string text = string_30 + "ui\\resources\\";
            string text2 = string_31 + "ui\\resources\\";
            _uiResourcesBitmaps = new Bitmap[Galaxy.ResourceSystemStatic.Resources.Count];
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < Galaxy.ResourceSystemStatic.Resources.Count; i++)
            {
                int localI = i;
                bool flag = true;
                string text3 = "Resource_" + i + ".png";
                string text4 = "Resource_" + i + ".bmp";
                string empty = string.Empty;
                string text5 = text2 + text3;
                string text6 = text2 + text4;
                string text7 = text + text3;
                string text8 = text + text4;
                if (!string.IsNullOrEmpty(string_31))
                {
                    empty = text5;
                    flag = true;
                    if (!File.Exists(text5))
                    {
                        empty = text6;
                        flag = false;
                        if (!File.Exists(text6))
                        {
                            empty = text7;
                            flag = true;
                            if (!File.Exists(text7))
                            {
                                empty = text8;
                                flag = false;
                            }
                        }
                    }
                }
                else
                {
                    empty = text7;
                    flag = true;
                    if (!File.Exists(text7))
                    {
                        empty = text8;
                        flag = false;
                    }
                }
                taskList.Add(Task.Run(() =>
                {
                    _uiResourcesBitmaps[localI] = method_12(empty, bool_28: true);
                    if (!flag)
                    {
                        _uiResourcesBitmaps[localI].MakeTransparent(Color.FromArgb(21, 21, 28));
                    }
                }));
            }
            Task.WaitAll(taskList.ToArray());
        }


    }

}