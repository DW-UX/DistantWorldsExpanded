// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.Properties.Resources
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DistantWorlds.Controls.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  [DebuggerNonUserCode]
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
        if (object.ReferenceEquals((object) DistantWorlds.Controls.Properties.Resources.resourceMan, (object) null))
          DistantWorlds.Controls.Properties.Resources.resourceMan = new ResourceManager("DistantWorlds.Controls.Properties.Resources", typeof (DistantWorlds.Controls.Properties.Resources).Assembly);
        return DistantWorlds.Controls.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => DistantWorlds.Controls.Properties.Resources.resourceCulture;
      set => DistantWorlds.Controls.Properties.Resources.resourceCulture = value;
    }
  }
}
