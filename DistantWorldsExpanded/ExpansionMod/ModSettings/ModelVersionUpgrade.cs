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
            bool res = false;
            updatedSettings = model.Clone();
            if (model.Version == 1)
            {
                updatedSettings.Version = 3;
                updatedSettings.TargetHappinessTaxColonyMaxed = 10;
                updatedSettings.TargetHappinessTaxColonyLarge = 10;
                updatedSettings.TargetHappinessTaxColonyMedium = 16;
                updatedSettings.TargetHappinessTaxColonySmall = 25;
                updatedSettings.EnableTargetHappinessTax = true;
                updatedSettings.UseDbFiles = true;
                res = true;
            }
            else if (model.Version == 2)
            {
                updatedSettings.Version = 3;
                updatedSettings.UseDbFiles = true;
                res = true;
            }
            return res;
        }
    }
}
