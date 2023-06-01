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
    public partial class fHotKeysMain : Form
    {
        private List<IHotKeyManager> _hotkeyManagers;
        public fHotKeysMain(List<IHotKeyManager> hotkeyManagers)
        {
            InitializeComponent();

            _hotkeyManagers = hotkeyManagers;
            FillTabControl(_hotkeyManagers);
        }

        private void FillTabControl(List<IHotKeyManager> hotkeyManagers)
        {
            foreach(var item in hotkeyManagers) 
            {
                TabPage newPage = new TabPage(item.GetTabPageName());
                newPage.Controls.Add(item.GetHotKeyControl());
                newPage.Tag = item;
                this.tbCtrlHotKeysContainer.TabPages.Add(newPage);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
