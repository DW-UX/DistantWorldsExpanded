using DistantWorlds;
using DistantWorlds.Types;
using DistantWorlds.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
    public partial class SelectPassengerMissionTargets : Form
    {
        public Habitat SelectedSource = null;
        public Habitat SelectedTarget = null;
        public bool Repeatable;
        public SelectPassengerMissionTargets(Main main)
        {
            InitializeComponent();

            bindingSource.DataSource = typeof(HabitatView);
            bindingTarget.DataSource = typeof(HabitatView);
            PopulateControls(main);
            ResizeComboBox(cmbSource);
            ResizeComboBox(cmbTarget);
        }

        private void ResizeComboBox(ComboBox cmb)
        {
            if (cmb.Items.Count == 0) cmb.DropDownWidth = cmb.Width;
            else
            {
                int maxWidth = int.MinValue;
                for (int i = 0; i < cmb.Items.Count; i++)
                {
                    maxWidth = Math.Max(maxWidth, TextRenderer.MeasureText(cmb.GetItemText(cmb.Items[i]), cmb.Font).Width);
                }
                if (cmb.Items.Count > cmb.MaxDropDownItems)
                    maxWidth += SystemInformation.VerticalScrollBarWidth;
                cmb.DropDownWidth = maxWidth;
            }
        }

        private void PopulateControls(Main main)
        {
            cmbTarget.DisplayMember = nameof(HabitatView.Text);
            cmbSource.DisplayMember = nameof(HabitatView.Text);

            List<HabitatView> sourceList = main._Game.PlayerEmpire.Colonies.Select(x => new HabitatView() { Item = x, Text = x.Name }).ToList();

            bindingSource.DataSource = sourceList;
            cmbSource.DataSource = bindingSource;

            bindingTarget.DataSource = sourceList.ToList();
            cmbTarget.DataSource = bindingTarget;
        }

        private void btnSelectTargets_Click(object sender, EventArgs e)
        {
            SelectedSource = ((HabitatView)bindingSource.Current).Item;
            SelectedTarget = ((HabitatView)bindingTarget.Current).Item;
            Repeatable = chkRepeat.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        class HabitatView
        {
            public string Text { get; set; }
            public Habitat Item { get; set; }
        }
    }

}
