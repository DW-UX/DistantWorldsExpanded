using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DistantWorlds.DBLoader
{
    internal class ModData
    {
        private readonly string _modFolder;

        public ModData(string modFolder)
        {
            _modFolder = modFolder;
        }

        public List<string> ParseChanges()
        {
            var changes = new List<string>();
            DirectoryInfo directory = new DirectoryInfo(_modFolder);
            var files = directory.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            foreach (var file in files.Where(x => ModDbGlobals.Files.Any(y => y.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase))))
            {
                XDocument doc = XDocument.Load(file.FullName);
                changes.AddRange(doc.Root.Descendants().Select(x => x.Value).ToList());
            }
            return changes;
        }
    }
}
