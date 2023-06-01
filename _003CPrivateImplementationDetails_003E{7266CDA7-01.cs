// Decompiled with JetBrains decompiler
// Type: <PrivateImplementationDetails>{7266CDA7-01D2-49E9-9919-A987E0EBA1B4}
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using BaconDistantWorlds.HotKeys;
using DistantWorlds;
using ExpansionMod;
using ExpansionMod.ExternalMods;
using Newtonsoft.Json;
//using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
//using System.Windows.Forms;

internal class main
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Class5.smethod_0();
        }
        else
        {
            if (args[0].ToUpperInvariant() == "/GenerateHotkeysMapFile".ToUpperInvariant())
            {
                ExpansionModMain.GenerateDefaultFiles();
                string _ModRootFolder = "AdvMods\\BaconMod";
                string _HotKeyFileName = "BaconModHotKeysMappingFile.json";
                BaconKeyMapper baconKeyMapper = new BaconKeyMapper(_HotKeyFileName);
                baconKeyMapper.GenerateDefaultFile(_ModRootFolder);
            }
        }
    }


    private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        if (args.Name.ToUpperInvariant().Contains("SlimDX".ToUpperInvariant()))
        {
            if (Environment.Is64BitProcess)
            {
                string path = @"x64\SlimDX.dll";
                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(Path.GetFullPath(path));
                }
                else
                { return null; }
            }
            else
            {
                string path = @"x86\SlimDX.dll";
                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(Path.GetFullPath(path));
                }
                else
                { return null; }
            }
        }
        else if (args.Name.ToUpperInvariant().Contains("Facepunch.Steamworks".ToUpperInvariant()))
        {
            if (Environment.Is64BitProcess)
            {
                string path = @"x64\Facepunch.Steamworks.Win64.dll";
                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(Path.GetFullPath(path));
                }
                else
                { return null; }
            }
            else
            {
                string path = @"x86\Facepunch.Steamworks.Win32.dll";
                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(Path.GetFullPath(path));
                }
                else
                { return null; }
            }
        }
        else
        {
            return null;
        }
    }
}
