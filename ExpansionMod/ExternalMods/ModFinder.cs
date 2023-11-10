using ExpansionMod.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ExpansionMod.ExternalMods
{
    internal class ModFinder
    {
        public List<ModEntity> GetModEntities(ModConfigModel config, ModEntity myMod)
        {
            List<ModEntity> res = new List<ModEntity>();
            foreach (var item in config.ModEntries)
            {
                Assembly modAs = null; ;
                if (item.LoadType == (int)ModEnitityLoadType.File)
                { modAs = Assembly.LoadFrom(Path.Combine(Helper._ModsRootFolder, item.ModKey, item.ModEntryAssemblyName)); }
                else if (item.LoadType == (int)ModEnitityLoadType.Inbuilt)
                {
                    if (item.ModKey == ExpansionModMain._ModKey)
                    { 
                        //modAs = Assembly.GetAssembly(typeof(ExpansionModMain)); 
                        res.Add(myMod);
                        continue;
                    }
                    else {
                        //modAs = AppDomain.CurrentDomain.GetAssemblies().First(x => x.GetName().Name == "BaconDistantWorlds");
                        modAs = typeof(BaconDistantWorlds.BaconMain).Assembly;
                    }
                }
                else 
                {
                    continue;
                }

                bool classFound = false;
                foreach (var asClass in modAs.GetTypes())
                {
                    if (asClass.IsClass && asClass.GetInterfaces().Contains(typeof(IEntryPoint)))
                    {
                        classFound = true;
                        var instance = (IEntryPoint)Activator.CreateInstance(asClass);
                        res.Add(new ModEntity(Path.Combine(Helper._ModsRootFolder, item.ModKey), item.ModKey, instance));
                        break;
                    }
                }
                if (!classFound)
                { throw new ApplicationException($"No class that implements {nameof(IEntryPoint)} found in {item.ModEntryAssemblyName}"); }
            }
            return res;
        }
    }
}
