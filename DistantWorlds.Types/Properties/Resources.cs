// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Properties.Resources
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DistantWorlds.Types.Properties
{
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) DistantWorlds.Types.Properties.Resources.resourceMan, (object) null))
          DistantWorlds.Types.Properties.Resources.resourceMan = new ResourceManager("DistantWorlds.Types.Properties.Resources", typeof (DistantWorlds.Types.Properties.Resources).Assembly);
        return DistantWorlds.Types.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => DistantWorlds.Types.Properties.Resources.resourceCulture;
      set => DistantWorlds.Types.Properties.Resources.resourceCulture = value;
    }
  }
}
