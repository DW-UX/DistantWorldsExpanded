using ExpansionMod.Controls;
using ExpansionMod.ExternalMods;
using ExpansionMod.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.HotKeyMapping
{
    class HotKeySettingsOverseer
    {
        private Dictionary<ModEntity, IHotKeyManager> _managerList = new Dictionary<ModEntity, IHotKeyManager>();
        public void AddHotKeyManager(IHotKeyManager manager, ModEntity mod)
        {
            _managerList[mod] = manager;
            manager.MapKeys(mod.RootFolder);
        }

        public void CanelChanges()
        {
            foreach (var item in _managerList)
            {
                item.Value.CancelChanges();
            }
        }

        public void SaveChanges()
        {
            foreach (var item in _managerList)
            {
                item.Value.SaveChanges();
            }
        }

        public void GenerateDefaultKes()
        {
            foreach (var item in _managerList)
            {
                item.Value.RestoreDefaults();
            }
        }

        public void ShowHotKeyForm(IWin32Window owner)
        {
            using fHotKeysMain hotKeyForm = new fHotKeysMain(_managerList.Values.ToList());
            {
                if (hotKeyForm.ShowDialog(owner) == DialogResult.OK)
                { SaveChanges(); }
                else
                { CanelChanges(); }
            }
        }

        public void HandleHotkeys(KeyEventArgs e, List<Keys> pressedKeys)
        {
            foreach (var item in _managerList)
            {
                if (item.Value.CanHandleKeysFromOverseer)
                {
                    item.Value.KeyboardInput(e, pressedKeys);
                    if (e.Handled)
                    { break; }
                }
            }
        }
    }
}
