using ExpansionMod.Objects;
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
    public partial class HotKeyModEditorControl : UserControl
    {

        public HotKeyModEditorControl(List<KeyMappingTarget> hotkeys)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            targetBindingSource.DataSource = ViewConvert.ConvertKeyMappingTarget(hotkeys);
        }

        public List<KeyMappingTarget> GetHotKeys()
        {
            return ViewConvert.ConvertKeyMappingTarget(targetBindingSource.DataSource as List<ViewKeyMappingTarget>);
        }

        private void btnAddHotkey_Click(object sender, EventArgs e)
        {
            using fAddNewHotkey addHotkey = new fAddNewHotkey();

            if (addHotkey.ShowDialog(this) == DialogResult.OK)
            {
                if (addHotkey.Result.Count > 0)
                { mappedHotKeysBindingSource.Add(new ViewMappedHotKey() { Key = addHotkey.Result }); }
            }
        }

        private void btnRemoveHotkey_Click(object sender, EventArgs e)
        {
            if (mappedHotKeysBindingSource.Count > 0)
            {
                mappedHotKeysBindingSource.RemoveCurrent();
            }
        }
    }
}
