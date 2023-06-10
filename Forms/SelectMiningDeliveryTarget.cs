using DistantWorlds.Types;
using DistantWorlds;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaconDistantWorlds.Forms
{
    public partial class SelectMiningDeliveryTarget : Form
    {
        public BuiltObject SelectedTarget = null;
        public bool DeliverToSpecificDestination = false;
        public SelectMiningDeliveryTarget(Main main)
        {
            InitializeComponent();

            bindingTarget.DataSource = typeof(BuiltObjectView);
            PopulateControls(main);
        }
        private void PopulateControls(Main main)
        {
            cmbTarget.DisplayMember = nameof(BuiltObjectView.Text);

            List<BuiltObjectView> sourceList = main._Game.PlayerEmpire.BuiltObjects.Where(x => x.Role == BuiltObjectRole.Base || x.SubRole == BuiltObjectSubRole.ConstructionShip).Select(x => new BuiltObjectView() { Item = x, Text = $"{x.Name} ({x.SubRole})" }).ToList();
            sourceList.Sort((x1, x2) => x1.Text.CompareTo(x2.Text));

            bindingTarget.DataSource = sourceList;
            cmbTarget.DataSource = bindingTarget;
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

        private void btnSelectTargets_Click(object sender, EventArgs e)
        {
            SelectedTarget = ((BuiltObjectView)bindingTarget.Current).Item;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void btnSetAutoTarget_Click(object sender, EventArgs e)
        {
            SelectedTarget = ((BuiltObjectView)bindingTarget.Current).Item;
            DeliverToSpecificDestination = true;
            this.DialogResult = DialogResult.OK;
        }

        class BuiltObjectView
        {
            public string Text { get; set; }
            public BuiltObject Item { get; set; }
        }

    }
}
