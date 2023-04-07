// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Properties.Resources
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace DistantWorlds.Properties
{
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal class Resources
    {
        private static ResourceManager resourceManager_0;

        private static CultureInfo cultureInfo_0;

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager_0
        {
            get
            {
                if (object.ReferenceEquals(resourceManager_0, null))
                {
                    ResourceManager resourceManager = (resourceManager_0 = new ResourceManager("DistantWorlds.Properties.Resources", typeof(DistantWorlds.Properties.Resources).Assembly));
                }
                return resourceManager_0;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo CultureInfo_0
        {
            set
            {
                cultureInfo_0 = value;
            }
        }

        internal Resources():base()
        {
            Class7.VEFSJNszvZKMZ();
        }

        [SpecialName]
        internal static byte[] smethod_0()
        {
            object @object = ResourceManager_0.GetObject("Forgottb", cultureInfo_0);
            return (byte[])@object;
        }

        [SpecialName]
        internal static byte[] smethod_1()
        {
            object @object = ResourceManager_0.GetObject("Forgotte", cultureInfo_0);
            return (byte[])@object;
        }
    }
}
