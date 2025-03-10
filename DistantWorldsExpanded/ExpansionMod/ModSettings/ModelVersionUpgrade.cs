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
            updatedSettings = null;
            if (model.Version == 1)
            {
                updatedSettings = new SettingsModel
                {
                    Version = 2,
                    PlayerEmprireDefaultRepairPriotityTemplate = model.PlayerEmprireDefaultRepairPriotityTemplate,
                    AIEmprireDefaultRepairPriotityTemplate = model.AIEmprireDefaultRepairPriotityTemplate,
                    TargetHappinessTaxColonyMaxed = 10,
                    TargetHappinessTaxColonyLarge = 10,
                    TargetHappinessTaxColonyMedium = 16,
                    TargetHappinessTaxColonySmall = 25,
                    EnableTargetHappinessTax = true
                };
                res = true;
            }
            return res;
        }
    }
}
