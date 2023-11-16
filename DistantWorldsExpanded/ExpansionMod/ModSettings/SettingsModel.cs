using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ModSettings
{
    internal class SettingsModel
    {
        public int Version { get; set; }
        public string PlayerEmprireDefaultRepairPriotityTemplate { get; set; } 
        public string AIEmprireDefaultRepairPriotityTemplate { get; set; }
    }
}
