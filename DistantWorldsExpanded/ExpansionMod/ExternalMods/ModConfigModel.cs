using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ExternalMods
{
    public class ModConfigModel
    {
        public int FormatVersion { get; set; }
        public List<ModEntry> ModEntries { get; set; }
    }
    public class ModEntry
    {
        public string ModKey { get; set; }
        public int LoadType { get; set; }
        public string ModEntryAssemblyName { get; set; }
    }
}
