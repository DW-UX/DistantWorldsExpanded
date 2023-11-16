using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.Objects
{
    public interface IHotKeyManager : IHotKeyParser, IHotKeyChanges
    {
        public bool CanHandleKeysFromOverseer { get; }
        public Control GetHotKeyControl();
        public string GetTabPageName();
    }
    public interface IHotKeyChanges
    {
        public void SaveChanges();
        public void CancelChanges();
        public void KeyboardInput(KeyEventArgs e, List<Keys> keys);
    }
    public interface IHotKeyParser
    {
        public bool MapKeys(string hotkeysDir);
        public bool GetMappedTarget(List<Keys> key, out MappedHotKey target);
        public void RestoreDefaults();
    }
    public interface IEntryPoint
    {
        public IHotKeyManager GetHotKeyManager();
        public string GetModKey();
    }   
}
