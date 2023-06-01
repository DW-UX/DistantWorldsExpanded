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
        private readonly ExpansionModMain expMod;
        private readonly KeyMapper _hotKeyParser;
        private readonly string _folder;
        private readonly string _tabName;
        private readonly bool _canHandleKeysFromOverseer;

        public bool CanHandleKeysFromOverseer { get => _canHandleKeysFromOverseer; }

        public HotKeyManager(ExpansionModMain expMod , KeyMapper hotKeyParser, string folder, bool mainGame)
        {
            this.expMod = expMod;
            _hotKeyParser = hotKeyParser;
            _folder = folder;
            _tabName = mainGame ? "DistantWorlds" : "Expansion mod hotkeys";
            _canHandleKeysFromOverseer = !mainGame;
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
            if (expMod.GameMain != null)
            {
                if (expMod.GameMain._Game.SelectedObject != null && expMod.GameMain._Game.SelectedObject is BuiltObject constrShip &&
                    constrShip.SubRole == BuiltObjectSubRole.ConstructionShip)
                {
                    _fEditor = new MissionQueueEditor(constrShip);
                    _fEditor.Show(expMod.GameMain);
                }
            }
        }

    }
}
