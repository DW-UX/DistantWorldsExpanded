using DistantWorlds.Types;
using DistantWorlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.IO;
using ExpansionMod.Objects.HotKeyMapping;
using ExpansionMod.ExternalMods;
using System.Drawing;
using DistantWorlds.Controls;
using System.Xml.Linq;
using ExpansionMod.ModSettings;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ExpansionMod
{
    public class ExpansionModMain : IEntryPoint
    {
        public const string _ModKey = "ExpansionMod";
        private const string _MainGameMappingFileName = "GameHotKeysMappingFile.json";
        private const string _ExpansionMappingFileName = "ExpansionHotKeysMappingFile.json";
        private static ModEntity _selfModEntity;

        private HotKeyManager _expModHotKeyManager;
        private HotKeyManager _gameHotKeyManager;
        private HotKeySettingsOverseer _hotKeyOverseer = new HotKeySettingsOverseer();
        private RepairPriorityManager _repairPriorityManager;

        private Main _gameMain = null;
        private SettingsModel _settings = null;

        public bool Inited { get; private set; }
        public bool StartupDone { get; private set; }
        public static ExpansionModMain ModMain { get; private set; }


        static ExpansionModMain()
        {
            _selfModEntity = new ModEntity(Helper.GetModPath(""), _ModKey, null);
        }
        public ExpansionModMain()
        {
            _selfModEntity.EntryClass = this;
        }

        public static void GenerateDefaultFiles()
        {
            KeyMapper gameKeyMapper = new KeyMapper(_MainGameMappingFileName);
            ExpKeyMapper expKeyMapper = new ExpKeyMapper(_ExpansionMappingFileName);
            gameKeyMapper.GenerateDefaultFile(_selfModEntity.RootFolder);
            expKeyMapper.GenerateDefaultFile(_selfModEntity.RootFolder);
        }


        public void ModStartup(Start start)
        {
            SettingsParser settParser = new SettingsParser(this, Helper.GetModPath(""));
            SettingsModel model;
            if ((model = settParser.LoadSettings(out string errorMsg)) != null && string.IsNullOrWhiteSpace(errorMsg))
            {
                HotKeysInitImpl(false);
                FindOtherMods();
                LoadRepairPriority(model);
                _settings = model;
                this.StartupDone = true;
            }
            else
            {
                throw new ApplicationException($"Failed to load mod settings: {errorMsg}");
            }
        }
        public void ModInitialize(Main main)
        {
            if (!this.Inited)
            {
                _gameMain = main;

                Inited = true;

                _gameHotKeyManager.InitMain(_gameMain);
                _expModHotKeyManager.InitMain(_gameMain);
                _repairPriorityManager.InitMain(_gameMain);
            }
        }


        public void ShowHotKeyEditor(IWin32Window owner)
        {
            _hotKeyOverseer.ShowHotKeyForm(owner);
        }
        public void ModKeyboardInput(KeyEventArgs e, List<Keys> pressedKeys)
        {
            _hotKeyOverseer.HandleHotkeys(e, pressedKeys);
        }
        public string SelectRepairPriority(string current)
        {
            return _repairPriorityManager.SelectRepairPriority(current);
        }
        public List<ComponentCategoryType> GetRepairPriorityList(string current)
        {
            return _repairPriorityManager.GetRepairPriorityList(current);
        }

        #region MainGameLegacy
        public bool GetMappedTarget(List<Keys> key, out MappedHotKey target)
        {
            return _gameHotKeyManager.GetMappedTarget(key, out target);
        }
        public bool ResolveTargetFriendlyName(KeyMappingFriendlyNames name, out int id)
        {
            return _gameHotKeyManager.ResolveTargetFriendlyName(name, out id);
        }
        #endregion

        public IHotKeyManager GetHotKeyManager()
        {
            return _expModHotKeyManager;
        }

        public string GetModKey()
        {
            return _ModKey;
        }

        public string GetDefaultPlayerRepairTemplateName()
        {
            return _repairPriorityManager.GetDefaultPlayerTemplateName();
        }
        public string GetDefaultAIRepairTemplateName()
        {
            return _repairPriorityManager.GetDefaultAITemplateName();
        }

        public void FixAllDesignRepairTemplates(Game game, bool reset)
        {
            FixPlayerDesignRepairTemplates(game, reset);
            FixAIDesignRepairTemplates(game, reset);
        }
        public void FixPlayerDesignRepairTemplates(Game game, bool reset)
        {
            if (game != null && game.Galaxy != null && game.Galaxy.Empires != null)
            {
                bool flag = game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                { game.Galaxy.Pause(); }
                foreach (var empire in game.Galaxy.Empires)
                {
                    if (empire == game.PlayerEmpire)
                    {
                        if (empire.Designs != null)
                        {
                            foreach (var design in empire.Designs)
                            {
                                if (design != null && (reset || string.IsNullOrWhiteSpace(design.RepaitPriorityTemplateName)))
                                {
                                    design.RepaitPriorityTemplateName = _repairPriorityManager.GetDefaultPlayerTemplateName();
                                }
                            }
                        }
                        if (empire.LatestDesigns != null)
                        {
                            foreach (var design in empire.LatestDesigns)
                            {
                                if (design != null && (reset || string.IsNullOrWhiteSpace(design.RepaitPriorityTemplateName)))
                                {
                                    design.RepaitPriorityTemplateName = _repairPriorityManager.GetDefaultPlayerTemplateName();
                                }
                            }
                        }
                    }
                }
                if (!flag)
                { game.Galaxy.Resume(); }
            }
        }
        public void FixAIDesignRepairTemplates(Game game, bool reset)
        {
            if (game != null && game.Galaxy != null && game.Galaxy.Empires != null)
            {
                bool flag = game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                { game.Galaxy.Pause(); }
                foreach (var empire in game.Galaxy.Empires)
                {
                    if (empire != game.PlayerEmpire)
                    {
                        if (empire.Designs != null)
                        {
                            foreach (var design in empire.Designs)
                            {
                                if (design != null && (reset || string.IsNullOrWhiteSpace(design.RepaitPriorityTemplateName)))
                                {
                                    design.RepaitPriorityTemplateName = _repairPriorityManager.GetDefaultAITemplateName();
                                }
                            }
                        }
                        if (empire.LatestDesigns != null)
                        {
                            foreach (var design in empire.LatestDesigns)
                            {
                                if (design != null && (reset || string.IsNullOrWhiteSpace(design.RepaitPriorityTemplateName)))
                                {
                                    design.RepaitPriorityTemplateName = _repairPriorityManager.GetDefaultAITemplateName();
                                }
                            }
                        }
                    }
                }
                if (!flag)
                { game.Galaxy.Resume(); }
            }
        }

        #region Internal

        internal void RegisterHotKeyManager(IHotKeyManager manager, ModEntity mod)
        {
            if (manager != null)
            { _hotKeyOverseer.AddHotKeyManager(manager, mod); }
        }

        internal void FindOtherMods()
        {
            ModConfigParser configParser = new ModConfigParser();
            ModFinder finder = new ModFinder();
            var config = configParser.ParseModConfig(Helper.GetModConfigPath());
            var extMods = finder.GetModEntities(config, _selfModEntity);
            foreach (var item in extMods)
            {
                this.RegisterHotKeyManager(item.EntryClass.GetHotKeyManager(), item);
            }
        }

        internal DialogResult ShowMessageBoxSimple(string message, string caption, MessageBoxButtons btn, MessageBoxIcon icon)
        {
            DialogResult res = DialogResult.None;
            bool flag = this._gameMain._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
            if (!flag)
            { this._gameMain._Game.Galaxy.Pause(); }
            res = MessageBox.Show(_gameMain, caption, message, btn, icon);
            if (!flag)
            { this._gameMain._Game.Galaxy.Resume(); }
            return res;
        }

        internal SettingsModel GetSettings()
        {
            if (this.StartupDone)
            {
                return _settings;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Private

        private void HotKeysInitImpl(bool generateDefaultKeys)
        {
            KeyMapper gameKeyMapper = new KeyMapper(_MainGameMappingFileName);
            ExpKeyMapper expKeyMapper = new ExpKeyMapper(_ExpansionMappingFileName);
            if (!gameKeyMapper.MapKeys(_selfModEntity.RootFolder))
            {
                throw new ApplicationException($"Failed to map keys, check {_MainGameMappingFileName} file");
            }
            if (!expKeyMapper.MapKeys(_selfModEntity.RootFolder))
            {
                throw new ApplicationException($"Failed to map keys, check {_ExpansionMappingFileName} file");
            }
            _gameHotKeyManager = new HotKeyManager(gameKeyMapper, _selfModEntity.RootFolder, true);
            _expModHotKeyManager = new HotKeyManager(expKeyMapper, _selfModEntity.RootFolder, false);
            if (!generateDefaultKeys)
            {
                _hotKeyOverseer.AddHotKeyManager(_gameHotKeyManager, new ModEntity(Helper.GetModPath(""), "DistantWorlds", null));
                //will self register later from FindOtherMods
                //_hotKeyOverseer.AddHotKeyManager(_expModHotKeyManager, new ModEntity(Helper.GetModPath(""), _ModKey, null));
            }
        }

        private void LoadRepairPriority(SettingsModel model)
        {
            _repairPriorityManager = new RepairPriorityManager(Helper.GetModPath(""), model);
            {
                //_repairPriorityManager.SaveRepairPriorityTemplates(out string Msg);
            }
            if (!_repairPriorityManager.LoadRepairPriorityTemplates(out string errorMsg))
            {
                throw new ApplicationException($"Failed to load repair priority templates: {errorMsg}");
            }
            if (!_repairPriorityManager.CheckUserDefaultTemplatesExist())
            {
                Main.ShowMessageBox("Default repair templates for Player and AI is missing in template list. " +
                    "Fix default templates names or create them in game. Until fixed ships with missing templates names" +
                    " will use original game template",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
