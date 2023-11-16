using ExpansionMod.Objects.HotKeyMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ExternalMods
{
    internal class ModConfigParser
    {
        public ModConfigModel ParseModConfig(string filePath)
        {
            List<ModEntity> res = new List<ModEntity>();
            using StreamReader sR = new StreamReader(filePath);
            return JsonConvert.DeserializeObject<ModConfigModel>(sR.ReadToEnd());
        }
    }
}
