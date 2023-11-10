// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.MessageBoxExResult
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Runtime.InteropServices;

namespace DistantWorlds.Controls
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct MessageBoxExResult
  {
    public const string Ok = "Ok";
    public const string Cancel = "Cancel";
    public const string Yes = "Yes";
    public const string No = "No";
    public const string Abort = "Abort";
    public const string Retry = "Retry";
    public const string Ignore = "Ignore";
    public const string Timeout = "Timeout";
  }
}
