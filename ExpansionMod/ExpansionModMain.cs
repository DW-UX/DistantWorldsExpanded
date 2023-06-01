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

namespace ExpansionMod
{
    public class ExpansionModMain : IEntryPoint
    {
        public const string _ModKey = "ExpansionMod";
        private const string _MainGameMappingFileName = "GameHotKeysMappingFile.json";
        private const string _ExpansionMappingFileName = "ExpansionHotKeysMappingFile.json";
        private static ModEntity _selfModEntity;

        public bool Inited { get; private set; }

        internal Main GameMain = null;

        static ExpansionModMain()
        {
            _selfModEntity = new ModEntity(Helper.GetModPath(""), _ModKey, null);
        }
        public ExpansionModMain()
        {
            _selfModEntity.EntryClass = this;
        }

        private HotKeyManager _expModHotKeyManager;
        private HotKeyManager _gameHotKeyManager;
        private HotKeySettingsOverseer _hotKeyOverseer = new HotKeySettingsOverseer();

        public static void GenerateDefaultFiles()
        {
            KeyMapper gameKeyMapper = new KeyMapper(_MainGameMappingFileName);
            ExpKeyMapper expKeyMapper = new ExpKeyMapper(_ExpansionMappingFileName);
            gameKeyMapper.GenerateDefaultFile(_selfModEntity.RootFolder);
            expKeyMapper.GenerateDefaultFile(_selfModEntity.RootFolder);
        }


        public void ModInitialize(Main main)
        {
            if (!this.Inited)
            {
                GameMain = main;
               
                Inited = true;
                HotKeysInitImpl(false);
                FindOtherMods();
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
        internal void RegisterHotKeyManager(IHotKeyManager manager, ModEntity mod)
        {
            if (manager != null)
            { _hotKeyOverseer.AddHotKeyManager(manager, mod); }
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
            _gameHotKeyManager = new HotKeyManager(this, gameKeyMapper, _selfModEntity.RootFolder, true);
            _expModHotKeyManager = new HotKeyManager(this, expKeyMapper, _selfModEntity.RootFolder, false);
            if (!generateDefaultKeys)
            {
                _hotKeyOverseer.AddHotKeyManager(_gameHotKeyManager, new ModEntity(Helper.GetModPath(""), "DistantWorlds", null));
                //will self register later from FindOtherMods
                //_hotKeyOverseer.AddHotKeyManager(_expModHotKeyManager, new ModEntity(Helper.GetModPath(""), _ModKey, null));
            }
        }
        #endregion
    }
}
