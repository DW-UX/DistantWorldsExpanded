using ExpansionMod.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ExternalMods
{
    internal class ModEntity
    {
        public string ModKey { get; private set; }
        public string RootFolder { get; private set; }
        public IEntryPoint EntryClass { get; set; }
        public ModEntity(string rootFolder, string modKey, IEntryPoint entryClass)
        {
            this.RootFolder = rootFolder;
            this.ModKey = modKey;
            this.EntryClass = entryClass;
        }
    }
}
