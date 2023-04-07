// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ErrorModes
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;

namespace DistantWorlds
{
  [Flags]
  public enum ErrorModes : uint
  {
    SYSTEM_DEFAULT = 0,
    SEM_FAILCRITICALERRORS = 1,
    SEM_NOALIGNMENTFAULTEXCEPT = 4,
    SEM_NOGPFAULTERRORBOX = 2,
    SEM_NOOPENFILEERRORBOX = 32768, // 0x00008000
  }
}
