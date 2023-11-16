using DistantWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ExternalMods
{
    internal class ModsCaller
    {
        //class for transfering calls to mods
        private List<ModEntity> _mods;
        public ModsCaller(List<ModEntity> mods) 
        {
            _mods = mods;
        }

        //public InitMods(Main main) 
        //{
        //    foreach (var mod in _mods)
        //    {
                
        //    }
        //}
    }
}
