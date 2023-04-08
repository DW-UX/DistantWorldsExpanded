// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Properties.Settings
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace DistantWorlds.Properties
{
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    [CompilerGenerated]
    internal sealed class Settings : ApplicationSettingsBase
    {
        private static DistantWorlds.Properties.Settings defaultInstance;

        public static DistantWorlds.Properties.Settings Default => defaultInstance;

        public Settings():base()
        {
            
        }

        static Settings()
        {
            
            defaultInstance = (DistantWorlds.Properties.Settings)SettingsBase.Synchronized(new DistantWorlds.Properties.Settings());
        }
    }
}
