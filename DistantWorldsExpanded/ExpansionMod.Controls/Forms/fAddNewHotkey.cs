using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.Controls
{
    public partial class fAddNewHotkey : Form
    {
        private static List<EnumView<Keys>> _availableKeys;
        public List<Keys> Result = new List<Keys>();
        public fAddNewHotkey()
        {
            InitializeComponent();
            bindingSourceSelectedHotKeys.DataSource = new List<Keys>();
            if (_availableKeys == null)
            { _availableKeys = GetAvailableKeys(); }
            bindingSourceHotKeyList.DataSource = _availableKeys;
            //bindingSourceHotKeyList.DataMember = nameof(EnumView<Keys>.Name);
            cmbHotKeyNames.DisplayMember = nameof(EnumView<Keys>.Name);
        }

        private void btnAddHotkey_Click(object sender, EventArgs e)
        {
            if (bindingSourceHotKeyList.Current != null)
            {
                bindingSourceSelectedHotKeys.Add((bindingSourceHotKeyList.Current as EnumView<Keys>).Key);
            }
        }      
        private void glassButton1_Click(object sender, EventArgs e)
        {
            if (bindingSourceHotKeyList.Current != null)
            {
                bindingSourceSelectedHotKeys.RemoveCurrent();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (bindingSourceSelectedHotKeys.Count > 0)
            {
                Result = (bindingSourceSelectedHotKeys.DataSource as List<Keys>).ToList();
            }
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (bindingSourceSelectedHotKeys.Position > 0)
            {
                int idx = bindingSourceSelectedHotKeys.Position;
                Keys temp = (Keys)bindingSourceSelectedHotKeys.Current;
                bindingSourceSelectedHotKeys.List.RemoveAt(idx);
                bindingSourceSelectedHotKeys.List.Insert(idx - 1, temp);
                bindingSourceSelectedHotKeys.Position--;
            }
        }
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (bindingSourceSelectedHotKeys.Position < bindingSourceSelectedHotKeys.Count)
            {
                int idx = bindingSourceSelectedHotKeys.Position;
                Keys temp = (Keys)bindingSourceSelectedHotKeys.Current;
                bindingSourceSelectedHotKeys.List.RemoveAt(idx);
                bindingSourceSelectedHotKeys.List.Insert(idx + 1, temp);
                bindingSourceSelectedHotKeys.Position++;
            }
        }

        //private List<EnumView<Keys>> GetAvailableKeys()
        //{
        //    List<EnumView<Keys>> res = HelperEnum<Keys>.GetViewEnum();
        //    res.RemoveAll(x => (int)x.Key < 0x8);
        //    res.RemoveAll(x => (int)x.Key > 0x14 && (int)x.Key < 0x1B);
        //    res.RemoveAll(x => (int)x.Key > 0x1B && (int)x.Key < 0x20);
        //    res.RemoveAll(x => (int)x.Key == 0x5F);
        //    res.RemoveAll(x => (int)x.Key > 0xDF);
        //    res.RemoveAll(x => (int)x.Key == -65536);

        //    var namesToRemove = DuplicateNames();
        //    res = res.Where(x => !namesToRemove.Contains(x.Name)).ToList();

        //    return res;
        //}

        //private List<string> DuplicateNames()
        //{
        //    return new List<string>()
        //    {
        //        "OemQuotes",
        //        "OemCloseBrackets",
        //        "OemPipe",
        //        "OemOpenBrackets",
        //        "Oemtilde",
        //        "OemQuestion",
        //        "OemSemicolon",
        //        "Snapshot",
        //        "Next",
        //        "Prior",
        //        "Return",
        //        "Capital",
        //    };
        //}
        private List<EnumView<Keys>> GetAvailableKeys()
        {
            List<EnumView<Keys>> allKeys = HelperEnum<Keys>.GetViewEnum();
            List<EnumView<Keys>> res = new List<EnumView<Keys>>();
            res.Add(new EnumView<Keys>() { Key = Keys.Back, Name = "Backspace" });
            res.Add(new EnumView<Keys>() { Key = Keys.Tab, Name = "Tab" });
            res.Add(new EnumView<Keys>() { Key = Keys.Enter, Name = "Enter" });
            res.Add(new EnumView<Keys>() { Key = Keys.Insert, Name = "Insert" });
            res.Add(new EnumView<Keys>() { Key = Keys.Delete, Name = "Delete" });
            res.Add(new EnumView<Keys>() { Key = Keys.Home, Name = "Home" });
            res.Add(new EnumView<Keys>() { Key = Keys.End, Name = "End" });
            res.Add(new EnumView<Keys>() { Key = Keys.ControlKey, Name = "Control" });
            res.Add(new EnumView<Keys>() { Key = Keys.Menu, Name = "Alt" });
            res.Add(new EnumView<Keys>() { Key = Keys.ShiftKey, Name = "Shift" });
            res.Add(new EnumView<Keys>() { Key = Keys.Left, Name = "Arrow Left" });
            res.Add(new EnumView<Keys>() { Key = Keys.Right, Name = "Arrow Right" });
            res.Add(new EnumView<Keys>() { Key = Keys.Up, Name = "Arrow Up" });
            res.Add(new EnumView<Keys>() { Key = Keys.Down, Name = "Arrow Down" });
            res.AddRange(allKeys.Where(x=>x.Key >= Keys.D0 && x.Key <= Keys.Z || 
            x.Key >= Keys.NumPad0 && x.Key <= Keys.F24));
            return res;
        }
    }
}
