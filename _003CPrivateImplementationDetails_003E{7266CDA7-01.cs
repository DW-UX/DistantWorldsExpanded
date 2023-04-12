// Decompiled with JetBrains decompiler
// Type: <PrivateImplementationDetails>{7266CDA7-01D2-49E9-9919-A987E0EBA1B4}
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds;
using System;
using System.Diagnostics;

internal class main
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Class5.smethod_0();
        }
        else
        {
            if (args[0] == "GenerateMapFile")
            {
                KeyMapper.GenerateEmpyFile(Start._MappingFilePath);
            }
        }
    }
}
