using ExpansionMod.Controls;
using ExpansionMod.Objects;
using ExpansionMod.Objects.HotKeyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaconDistantWorlds.HotKeys
{
    internal class HotKeyManager : IHotKeyManager
    {
        private KeyMapper _hotKeyParser;
        private string _folder;
        private HotKeyModEditorControl _hotKeyControl;
        private string _tabName;

        public bool CanHandleKeysFromOverseer => true;

        public HotKeyManager(KeyMapper hotKeyParser, string folder)
        {
            _hotKeyParser = hotKeyParser;
            _folder = folder;
            _tabName = "Bacon mod hotkeys";
        }

        public void SaveChanges()
        {
            //добавить сохранение в стиле апи. Обход всех модов
            if (_hotKeyControl != null)
            {
                MappingJsonFileModel model = new MappingJsonFileModel();
                model.FormatVersion = KeyMapper._CurrentCodeFormatVersion;
                model.HotKeys = _hotKeyControl.GetHotKeys();
                _hotKeyParser.Save(_folder, model);
            }
        }
        public void CancelChanges()
        {
        }

        public Control GetHotKeyControl()
        {
            if (_hotKeyControl != null)
            { _hotKeyControl.Dispose(); }
            _hotKeyControl = new ExpansionMod.Controls.HotKeyModEditorControl(_hotKeyParser.GetAllTargets());
            return _hotKeyControl;
        }
        
        public bool GetMappedTarget(List<Keys> key, out MappedHotKey target)
        {
            return _hotKeyParser.GetMappedTarget(key, out target);
        }
        public bool ResolveTargetFriendlyName(BaconKeyMappingFriendlyNames name, out int id)
        { return _hotKeyParser.GetTargetMethodIdByFriendlyName(name.ToString(), out id); }
        public string GetTabPageName()
        {
            return _tabName;
        }

        public bool MapKeys(string filePath = null)
        {
            return _hotKeyParser.MapKeys(filePath);
        }

        public void RestoreDefaults()
        {
            _hotKeyParser.RestoreDefaults();
            SaveChanges();
        }

        public void KeyboardInput(KeyEventArgs e, List<Keys> keys)
        {
            BaconBuiltObject.BaconKeyboardInput(e, keys);
        }
    }
}

