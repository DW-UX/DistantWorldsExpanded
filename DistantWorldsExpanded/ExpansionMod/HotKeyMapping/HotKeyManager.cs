using BaconDistantWorlds;
using DistantWorlds;
using DistantWorlds.Types;
using ExpansionMod.Controls;
using ExpansionMod.Objects;
using ExpansionMod.Objects.HotKeyMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.HotKeyMapping
{
    public class HotKeyManager : IHotKeyManager
    {
        private MissionQueueEditor _fEditor;
        private HotKeyModEditorControl _hotKeyControl;
        private Main _main;
        private readonly KeyMapper _hotKeyParser;
        private readonly string _folder;
        private readonly string _tabName;
        private readonly bool _canHandleKeysFromOverseer;

        public bool CanHandleKeysFromOverseer { get => _canHandleKeysFromOverseer; }

        public HotKeyManager(KeyMapper hotKeyParser, string folder, bool mainGame)
        {
            _hotKeyParser = hotKeyParser;
            _folder = folder;
            _tabName = mainGame ? "DistantWorlds" : "Expansion mod hotkeys";
            _canHandleKeysFromOverseer = !mainGame;
        }

        public void InitMain(Main gameMain)
        {
            this._main = gameMain;
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
            _hotKeyControl = new Controls.HotKeyModEditorControl(_hotKeyParser.GetAllTargets());
            return _hotKeyControl;
        }

        //public bool GetMappedKeys(KeyMappingTarget target, out List<Keys> keys)
        //{
        //    throw new NotImplementedException();
        //}

        public bool ResolveTargetFriendlyName(KeyMappingFriendlyNames name, out int id)
        { return _hotKeyParser.GetTargetMethodIdByFriendlyName(name.ToString(), out id); }
        public List<KeyMappingTarget> GetAllTargets()
        { return _hotKeyParser.GetAllTargets(); }
        public void KeyboardInput(KeyEventArgs e, List<Keys> keys)
        {
            if (!e.Handled)
            {
                Objects.MappedHotKey target;
                if (GetMappedTarget(keys, out target))
                {
                    if (ResolveTargetFriendlyName(KeyMappingFriendlyNames.OpenConstractionQueueEditor, out int id) &&
                        id == target.Parent.TargetMethodId)
                    {
                        OpenCounstrucionQueueEditor();
                        e.Handled = true;
                    }
                }
            }
        }
        public string GetTabPageName()
        {
            return _tabName;
        }

        public bool MapKeys(string filePath = null)
        {
            return _hotKeyParser.MapKeys(filePath);
        }

        public bool GetMappedTarget(List<Keys> key, out MappedHotKey target)
        {
            return _hotKeyParser.GetMappedTarget(key, out target);
        }

        public void RestoreDefaults()
        {
            _hotKeyParser.RestoreDefaults();
            SaveChanges();
        }
        private void OpenCounstrucionQueueEditor()
        {
            if (_main != null)
            {
                if (_main._Game.SelectedObject != null && _main._Game.SelectedObject is BuiltObject constrShip &&
                    constrShip.SubRole == BuiltObjectSubRole.ConstructionShip && _fEditor == null)
                {
                    bool flag = _main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                    if (!flag)
                    { _main._Game.Galaxy.Pause(); }
                    _fEditor = new MissionQueueEditor(constrShip, _main);
                    _fEditor.FormClosed += _fEditor_FormClosed;
                    _fEditor.ShowDialog(_main);
                    if (!flag)
                    { _main._Game.Galaxy.Resume(); }
                }
            }
        }

        private void _fEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            _fEditor = null;
        }
    }
    
    internal class HotKeyManagerBacon : IHotKeyManager
    {
        private KeyMapper _hotKeyParser;
        private string _folder;
        private HotKeyModEditorControl _hotKeyControl;
        private string _tabName;

        public bool CanHandleKeysFromOverseer => true;

        public HotKeyManagerBacon(KeyMapper hotKeyParser, string folder)
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
        public bool ResolveTargetFriendlyName(KeyMappingFriendlyNames name, out int id)
        { return _hotKeyParser.GetTargetMethodIdByFriendlyName(name.ToString(), out id); }
        public List<KeyMappingTarget> GetAllTargets()
        { return _hotKeyParser.GetAllTargets(); }
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
