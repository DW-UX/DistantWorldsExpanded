// Decompiled with JetBrains decompiler
// Type: Class5
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

internal static class Class5
{
    //private static int int_0;
    //private static int int_1;
    //private static int int_2;
    //private static int int_3;
    //private static int int_4;
    //private static int int_5;
    //private static int int_6;
    //private static int int_7;
    //private static int FsaRtXdSbuq;
    //private static int int_8;
    //private static int int_9;
    internal static Splash _Splash;
    internal static void smethod_0()
    {
        try
        {
            Application.EnableVisualStyles();
            Size size = Screen.GetBounds(new Point(0, 0)).Size;
            if ((size.Width < 1024 || size.Height < 768) && MessageBox.Show("Distant Worlds requires a screen resolution of at least 1024 x 768.\n\nYou should change the resolution of your Windows desktop and restart.\n\nYou may choose to continue, but some aspects of Distant Worlds may not work properly.\n\nDo you wish to continue loading Distant Worlds with a low screen resolution?", "Screen resolution", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                Environment.Exit(-1);
            Task tr = Task.Factory.StartNew(() =>
            {
                _Splash = new Splash();
                Application.Run(_Splash);
            });
            var start = new Start();
            Application.Run(start);

        }
        catch (Exception ex)
        {
            Main.CrashDump(ex);
            if (_Splash != null)
            {
                _Splash.Stop();
                _Splash = null;
            }
            throw;
        }
    }
    private static void smethod_1()
    {
        string str = "DistWorldrez.exe";
        int num1 = 2404352;
        if (File.Exists(str))
        {
            if (new FileInfo(str).Length != (long)num1)
            {
                int num2 = (int)MessageBox.Show(string.Format("Incorrect version of the file {0}. Please reinstall Distant Worlds to correct this problem.", (object)str), "Incorrect File Version", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Environment.Exit(-1);
            }
            Class5.smethod_2(str);
        }
        else
        {
            int num3 = (int)MessageBox.Show(string.Format("Could not find file {0}. Please reinstall Distant Worlds to correct this problem.", (object)str), "File Missing", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Environment.Exit(-1);
        }
        Process process = new Process();
        process.StartInfo.FileName = str;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();
        process.WaitForExit();
        int exitCode = process.ExitCode;
        if (exitCode == 1)
            return;
        int num4 = (int)MessageBox.Show(string.Format("There is an error with your serial number as entered.  Please contact support@matrixgames.com for assistance.  Error Code: {0}", (object)exitCode.ToString()), "Registration Check", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Environment.Exit(-1);
    }

    private static void smethod_2(string string_0)
    {
        FileInfo fileInfo = new FileInfo(string_0);
        int length = (int)fileInfo.Length;
        FileStream fileStream = fileInfo.OpenRead();
        byte[] numArray = new byte[length];
        fileStream.Read(numArray, 0, length);
        bool flag = true;
        if (!Class5.smethod_3(numArray, 13, (byte[])new byte[4]
        {
      byte.MaxValue,
      (byte) 0,
      (byte) 0,
      (byte) 184
        }))
            flag = false;
        if (!Class5.smethod_3(numArray, 260, new byte[4]
        {
      (byte) 237,
      (byte) 4,
      (byte) 73,
      (byte) 245
        }))
            flag = false;
        if (!Class5.smethod_3(numArray, 4849, new byte[4] { (byte)129, (byte)227, byte.MaxValue, (byte)0 }))
            flag = false;
        if (!Class5.smethod_3(numArray, 232417, new byte[4] { (byte)120, (byte)8, (byte)133, byte.MaxValue }))
            flag = false;
        if (!Class5.smethod_3(numArray, 893909, new byte[4] { (byte)117, (byte)9, (byte)128, (byte)38 }))
            flag = false;
        fileStream.Close();
        if (flag)
            return;
        int num = (int)MessageBox.Show(string.Format("Incorrect version of the file {0}. Please reinstall Distant Worlds to correct this problem.", (object)string_0), "Incorrect File Version", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        Environment.Exit(-1);
    }

    private static bool smethod_3(byte[] object_0, int int_10, byte[] object_1)
    {
        for (int index = 0; index < object_1.Length; ++index)
        {
            if ((int)(byte)object_0[index + int_10] != (int)(byte)object_1[index])
                return false;
        }
        return true;
    }

    [DllImport("user32")]
    private static extern int SystemParametersInfo(int int_10, int int_11, int int_12, int int_13);

    public static bool smethod_4(bool bool_0) => Class5.SystemParametersInfo(17, !bool_0 ? 0 : 1, 0, 0) > 0;

    private static void smethod_5(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            Exception exceptionObject = (Exception)e.ExceptionObject;
            string str = "An error occurred: " + "\n\n" + exceptionObject.ToString();
            if (exceptionObject is ApplicationException)
                str = exceptionObject.Message;
            if (exceptionObject.InnerException != null && exceptionObject.InnerException is ApplicationException)
                str = exceptionObject.InnerException.Message;
            int num = (int)MessageBox.Show(str + "\n\nDistant Worlds will now exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Class5.smethod_4(true);
        }
        finally
        {
            Application.Exit();
        }
    }

    //static Class5()
    //{

    //Class5.int_0 = 74;
    //Class5.int_1 = 75;
    //Class5.int_2 = 1;
    //Class5.int_3 = 2;
    //Class5.int_4 = 1;
    //Class5.int_5 = 1;
    //Class5.int_6 = 2;
    //Class5.int_7 = 8202;
    //Class5.FsaRtXdSbuq = 8203;
    //Class5.int_8 = 8204;
    //Class5.int_9 = 8205;
    //}
}
