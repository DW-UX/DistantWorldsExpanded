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
    public partial class SelectCargoMissionTargets : Form
    {
        public StellarObject SelectedSource = null;
        public StellarObject SelectedTarget = null;
        public bool Repeatable;
        public SelectCargoMissionTargets(Main main)
        {
            InitializeComponent();

            bindingSource.DataSource = typeof(BuiltObjectView);
            bindingTarget.DataSource = typeof(BuiltObjectView);
            PopulateControls(main);
        }
        private void PopulateControls(Main main)
        {
            cmbTarget.DisplayMember = nameof(BuiltObjectView.Text);
            cmbSource.DisplayMember = nameof(BuiltObjectView.Text);

            List<BuiltObjectView> sourceList = main._Game.PlayerEmpire.Colonies.Select(x => new BuiltObjectView() { Item = x, Text = $"{x.Name} (Colony)"}).ToList();
            sourceList = main._Game.PlayerEmpire.BuiltObjects.Where(x=>x.Role == BuiltObjectRole.Base || x.SubRole == BuiltObjectSubRole.ConstructionShip).Select(x => new BuiltObjectView() { Item = x, Text = $"{x.Name} ({x.SubRole})" }).ToList();
            sourceList.AddRange(main._Game.PlayerEmpire.ResourceExtractors.Where(x => x.Role == BuiltObjectRole.Base || x.SubRole == BuiltObjectSubRole.ConstructionShip).Select(x => new BuiltObjectView() { Item = x, Text = $"{x.Name} ({x.SubRole})" }).ToList());
            sourceList.Sort((x1, x2) => x1.Text.CompareTo(x2.Text));

            bindingSource.DataSource = sourceList;
            cmbSource.DataSource = bindingSource;

            bindingTarget.DataSource = sourceList.ToList();
            cmbTarget.DataSource = bindingTarget;

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

        private void btnSelectTargets_Click(object sender, EventArgs e)
        {
            SelectedSource = ((BuiltObjectView)bindingSource.Current).Item;
            SelectedTarget = ((BuiltObjectView)bindingTarget.Current).Item;
            Repeatable = chkRepeat.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        class BuiltObjectView
        {
            public string Text { get; set; }
            public StellarObject Item { get; set; }
        }
    }

}
