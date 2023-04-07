// Decompiled with JetBrains decompiler
// Type: Class6
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Reflection;

internal class Class6
{
  internal static Module module_0;

  internal static void UliSJNssMbtjH(int typemdt)
  {
    Type type = Class6.module_0.ResolveType(33554432 + typemdt);
    foreach (FieldInfo field in type.GetFields())
    {
      MethodInfo method = (MethodInfo) Class6.module_0.ResolveMethod(field.MetadataToken + 100663296);
      field.SetValue((object) null, (object) (MulticastDelegate) Delegate.CreateDelegate(type, method));
    }
  }

  public Class6():base()
  {
    Class7.VEFSJNszvZKMZ();
  }

  static Class6()
  {
    Class7.VEFSJNszvZKMZ();
    Class6.module_0 = typeof (Class6).Assembly.ManifestModule;
  }

  internal delegate void Delegate12(object o);
}
