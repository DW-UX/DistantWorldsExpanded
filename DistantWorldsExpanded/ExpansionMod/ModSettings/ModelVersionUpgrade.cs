using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ModSettings
{
    internal static class ModelVersionUpgrade
    {
        public static bool IsUpgradeNeeded(SettingsModel model, out SettingsModel updatedSettings)
        {
            updatedSettings = null;
            return false;
        }
    }
}
