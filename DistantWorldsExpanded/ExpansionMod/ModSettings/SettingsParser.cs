using ExpansionMod.Objects.HotKeyMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpansionMod.ModSettings
{
    internal class SettingsParser
    {
        private const int _Version = 1;
        private const string _SettingsFileName = "ExapnsionModSettings.json";
        private string _modFolder;
        private ExpansionModMain _modMain;
        public SettingsParser(ExpansionModMain modMain, string modFolder)
        {
            _modMain = modMain;
            _modFolder = modFolder;
        }

        public SettingsModel LoadSettings(out string errorMsg)
        {
            SettingsModel res = null;
            errorMsg = "";
            FileInfo fileInfo = new FileInfo(Path.Combine(_modFolder, _SettingsFileName));
            if (fileInfo.Exists)
            {
                using (StreamReader sR = new StreamReader(fileInfo.OpenRead()))
                { res = GetModelFromFile(sR); }
                if (ModelVersionUpgrade.IsUpgradeNeeded(res, out SettingsModel temp))
                {
                    res = temp;
                    if (!SaveSettings(res, out string saveErrorMsg))
                    {
                        _modMain.ShowMessageBoxSimple($"Failed to save updated mod settings: {saveErrorMsg}", "Error",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                errorMsg = $"{_SettingsFileName} is missing";
            }
            return res;
        }

        private SettingsModel GetModelFromFile(StreamReader sR)
        {
            return JsonConvert.DeserializeObject<SettingsModel>(sR.ReadToEnd());
        }
        private bool SaveSettings(SettingsModel model, out string errorMsg)
        {
            bool res = true;
            errorMsg = "";
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(_modFolder, _SettingsFileName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    fs.SetLength(0);
                    using (StreamWriter sW = new StreamWriter(fs))
                    {
                        sW.Write(JsonConvert.SerializeObject(model, Formatting.Indented));
                        sW.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                errorMsg = ex.ToString();
            }
            return res;
        }
    }
}
