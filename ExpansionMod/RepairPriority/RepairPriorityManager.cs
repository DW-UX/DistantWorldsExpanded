﻿using DistantWorlds;
using DistantWorlds.Types;
using ExpansionMod.Controls.Forms;
using ExpansionMod.ModSettings;
using ExpansionMod.Objects;
using ExpansionMod.Objects.HotKeyMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod
{
    internal class RepairPriorityManager
    {
        private const int _Version = 1;
        private const string _FileName = "RepairPriorityTemplates.json";
        public const string _OriginalTemplateName = "Original";
        public const string _DefaultTemplateName = "Default";

        private readonly string _modFolder;
        private readonly SettingsModel _settings;
        private Main _gameMain = null;
        private fRepairPriority _fRepairPriority = null;

        public static RepairPriority Default { get; set; }
        public static RepairPriority Original { get; set; }
        public List<RepairPriority> RepairPriorityTemplates { get; set; }

        static RepairPriorityManager()
        {
            Default = new RepairPriority()
            {
                TemplateName = _DefaultTemplateName,
                //UserGenerated = true,
                Priority = new List<DistantWorlds.Types.ComponentCategoryType>()
                {
                    DistantWorlds.Types.ComponentCategoryType.HyperDrive,
                    DistantWorlds.Types.ComponentCategoryType.Reactor,
                    DistantWorlds.Types.ComponentCategoryType.Shields,
                    DistantWorlds.Types.ComponentCategoryType.Engine,
                    DistantWorlds.Types.ComponentCategoryType.WeaponSuperBeam,
                    DistantWorlds.Types.ComponentCategoryType.WeaponTorpedo,
                    DistantWorlds.Types.ComponentCategoryType.WeaponSuperArea,
                    DistantWorlds.Types.ComponentCategoryType.WeaponBeam,
                    DistantWorlds.Types.ComponentCategoryType.WeaponGravity,
                    DistantWorlds.Types.ComponentCategoryType.WeaponIon,
                    DistantWorlds.Types.ComponentCategoryType.WeaponTorpedo,
                    DistantWorlds.Types.ComponentCategoryType.WeaponArea,
                    DistantWorlds.Types.ComponentCategoryType.WeaponPointDefense,
                    DistantWorlds.Types.ComponentCategoryType.Computer,
                    DistantWorlds.Types.ComponentCategoryType.Armor,
                    DistantWorlds.Types.ComponentCategoryType.HyperDisrupt,
                    DistantWorlds.Types.ComponentCategoryType.Sensor,
                    DistantWorlds.Types.ComponentCategoryType.AssaultPod,
                    DistantWorlds.Types.ComponentCategoryType.EnergyCollector,
                    DistantWorlds.Types.ComponentCategoryType.Construction,
                    DistantWorlds.Types.ComponentCategoryType.Extractor,
                    DistantWorlds.Types.ComponentCategoryType.Fighter,
                    DistantWorlds.Types.ComponentCategoryType.Habitation,
                    DistantWorlds.Types.ComponentCategoryType.Manufacturer,
                    DistantWorlds.Types.ComponentCategoryType.ShieldRecharge,
                    DistantWorlds.Types.ComponentCategoryType.Storage,
                    DistantWorlds.Types.ComponentCategoryType.Labs
                }
            };

            Original = new RepairPriority()
            {
                TemplateName = _OriginalTemplateName,
                //UserGenerated = true,
            };
        }
        public RepairPriorityManager(string modFolder, SettingsModel model)
        {
            _modFolder = modFolder;
            _settings = model;
        }

        public void InitMain(Main gameMain)
        { _gameMain = gameMain; }

        public string SelectRepairPriority(string current)
        {
            string res = current;
            if (this._gameMain != null)
            {
                bool flag = this._gameMain._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                { this._gameMain._Game.Galaxy.Pause(); }
                _fRepairPriority = new fRepairPriority(current, this.RepairPriorityTemplates, 
                    RepairPriorityManager.Default, RepairPriorityManager.Original, _settings.PlayerEmprireDefaultRepairPriotityTemplate,
                    _settings.AIEmprireDefaultRepairPriotityTemplate);
                if (_fRepairPriority.ShowDialog(this._gameMain) == DialogResult.OK)
                {
                    this.RepairPriorityTemplates = _fRepairPriority.ResultList;
                    if (!SaveRepairPriorityTemplates(out string errorMsg))
                    {
                        MessageBox.Show(_gameMain, $"Failed to save repair priority templates: {errorMsg}", "Error");
                    }
                    res = _fRepairPriority.SelectedTemplate.TemplateName;
                }
                _fRepairPriority = null;
                if (!flag)
                { this._gameMain._Game.Galaxy.Resume(); }
            }
            return res;
        }
        public List<ComponentCategoryType> GetRepairPriorityList(string current)
        {
            List<ComponentCategoryType> res = null; ;
            var item = this.RepairPriorityTemplates.FirstOrDefault(x => string.Equals(x.TemplateName, current, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            { res = item.Priority; }
            else if (string.Equals(RepairPriorityManager.Default.TemplateName, current, StringComparison.OrdinalIgnoreCase))
            { res = RepairPriorityManager.Default.Priority; }

            return res;
        }
        public bool CheckUserDefaultTemplatesExist()
        {
            bool res = true;
            if (string.Equals(_settings.PlayerEmprireDefaultRepairPriotityTemplate, _OriginalTemplateName,
                StringComparison.InvariantCultureIgnoreCase) || string.Equals(_settings.PlayerEmprireDefaultRepairPriotityTemplate,
                _DefaultTemplateName, StringComparison.InvariantCultureIgnoreCase))
            {
            }
            else
            { res &= this.RepairPriorityTemplates.Any(x => string.Equals(_settings.PlayerEmprireDefaultRepairPriotityTemplate, x.TemplateName, StringComparison.InvariantCultureIgnoreCase)); }

            if (res)
            {
                if (string.Equals(_settings.AIEmprireDefaultRepairPriotityTemplate, _OriginalTemplateName,
                    StringComparison.InvariantCultureIgnoreCase) || string.Equals(_settings.AIEmprireDefaultRepairPriotityTemplate,
                    _DefaultTemplateName, StringComparison.InvariantCultureIgnoreCase))
                {
                }
                else
                { res &= this.RepairPriorityTemplates.Any(x => string.Equals(_settings.AIEmprireDefaultRepairPriotityTemplate, x.TemplateName, StringComparison.InvariantCultureIgnoreCase)); }
            }
            return res;
        }

        public bool LoadRepairPriorityTemplates(out string errorMsg)
        {
            bool res = false;
            errorMsg = "";
            try
            {
                using StreamReader sR = new StreamReader(Path.Combine(_modFolder, _FileName));

                RepairPriorityModel mappingModel = JsonConvert.DeserializeObject<RepairPriorityModel>(sR.ReadToEnd());
                RepairPriorityTemplates = mappingModel.RepairPriority;
                res = true;

            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return res;
        }
        public bool SaveRepairPriorityTemplates(out string errorMsg)
        {
            bool res = false;
            errorMsg = "";
            try
            {
                using (FileStream fs = new FileStream(Path.Combine(_modFolder, _FileName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                {
                    fs.SetLength(0);
                    using (StreamWriter sW = new StreamWriter(fs))
                    {
                        RepairPriorityModel templateModel = new RepairPriorityModel()
                        {
                            Version = _Version,
                            RepairPriority = this.RepairPriorityTemplates,
                        };

                        sW.Write(JsonConvert.SerializeObject(templateModel, Formatting.Indented));
                        sW.Flush();
                    }
                }
                res = true;

            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
            return res;
        }

        public string GetDefaultPlayerTemplateName()
        {
            return _settings.PlayerEmprireDefaultRepairPriotityTemplate;
        }
        public string GetDefaultAITemplateName()
        {
            return _settings.AIEmprireDefaultRepairPriotityTemplate;
        }
    }
}

