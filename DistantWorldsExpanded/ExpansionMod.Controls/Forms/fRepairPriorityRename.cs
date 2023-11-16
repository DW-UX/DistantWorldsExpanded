using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.Controls.Forms
{
    public partial class fRepairPriorityRename : Form
    {
        public string Result { get; set; }
        public fRepairPriorityRename(string name)
        {
            InitializeComponent();
            txtNewName.Text = name;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewName.Text))
            {
                this.Result = txtNewName.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(this, "Name is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
