// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.Properties.Resources
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace BaconDistantWorlds.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
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
        if (BaconDistantWorlds.Properties.Resources.resourceMan == null)
          BaconDistantWorlds.Properties.Resources.resourceMan = new ResourceManager("BaconDistantWorlds.Properties.Resources", typeof (BaconDistantWorlds.Properties.Resources).Assembly);
        return BaconDistantWorlds.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => BaconDistantWorlds.Properties.Resources.resourceCulture;
      set => BaconDistantWorlds.Properties.Resources.resourceCulture = value;
    }

    internal static Bitmap AlienBattlefield01 => (Bitmap) BaconDistantWorlds.Properties.Resources.ResourceManager.GetObject(nameof (AlienBattlefield01), BaconDistantWorlds.Properties.Resources.resourceCulture);

    internal static Bitmap AlienBattlefield02 => (Bitmap) BaconDistantWorlds.Properties.Resources.ResourceManager.GetObject(nameof (AlienBattlefield02), BaconDistantWorlds.Properties.Resources.resourceCulture);

    internal static Bitmap AlienBattlefield03 => (Bitmap) BaconDistantWorlds.Properties.Resources.ResourceManager.GetObject(nameof (AlienBattlefield03), BaconDistantWorlds.Properties.Resources.resourceCulture);

    internal static Bitmap star___gold => (Bitmap) BaconDistantWorlds.Properties.Resources.ResourceManager.GetObject("star - gold", BaconDistantWorlds.Properties.Resources.resourceCulture);

    internal static Bitmap star_silver => (Bitmap) BaconDistantWorlds.Properties.Resources.ResourceManager.GetObject("star-silver", BaconDistantWorlds.Properties.Resources.resourceCulture);
  }
}
