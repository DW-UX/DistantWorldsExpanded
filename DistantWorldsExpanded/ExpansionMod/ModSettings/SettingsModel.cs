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
        public int TargetHappinessTaxColonyMaxed { get; set; } = 10;
        public int TargetHappinessTaxColonyLarge { get; set; } = 10;
        public int TargetHappinessTaxColonyMedium { get; set; } = 16;
        public int TargetHappinessTaxColonySmall { get; set; } = 25;
        public bool EnableTargetHappinessTax { get; set; } = true;
    }
}
