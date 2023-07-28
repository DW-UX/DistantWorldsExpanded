using DistantWorlds.Types;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ExpansionMod.Controls.Forms
{
    public partial class fRepairPriority : Form
    {
        private readonly RepairPriority _defaultTemplate;
        private readonly RepairPriority _originalTemplate;
        private readonly List<RepairPriority> _userTemplates;
        private readonly string _playerDefaultTemplate;
        private readonly string _aiDefaultTemplate;

        public List<RepairPriority> ResultList { get; set; }
        public RepairPriority SelectedTemplate { get; set; }

        public fRepairPriority(string currentSelected, List<RepairPriority> userTemplates, RepairPriority defaultTemplate,
            RepairPriority originalTemplate, string playerDefaultTemplate, string aiDefaultTemplate)
        {
            InitializeComponent();
            _defaultTemplate = defaultTemplate;
            _originalTemplate = originalTemplate;
            _playerDefaultTemplate = playerDefaultTemplate;
            _aiDefaultTemplate = aiDefaultTemplate;
            _userTemplates = userTemplates.ToList();
            _userTemplates.Insert(0, _originalTemplate);
            _userTemplates.Insert(1, _defaultTemplate);

            bindingSourceRepairPriorityTemplates.DataSource = _userTemplates;
            //cmbTempalteList.DisplayMember = nameof(RepairPriority.TemplateName);
            //cmbTempalteList.DataSource = bindingSourceRepairPriorityTemplates;
            //cmbTempalteList.DataBindings.Add("SelectedValue", bindingSourceRepairPriorityTemplates, nameof(RepairPriority.TemplateName));
            if (currentSelected != null)
            {
                int idx = userTemplates.FindIndex(x => x.TemplateName == currentSelected);
                if (idx == -1)
                { idx = 0; }
                bindingSourceRepairPriority.Position = idx;
            }

            //lstbRepairPriority.DisplayMember = nameof(RepairPriority.Priority);
            //lstbRepairPriority.DataSource = bindingSourceRepairPriority;
        }

        private void btnAddNewTemplate_Click(object sender, EventArgs e)
        {
            bindingSourceRepairPriorityTemplates.Add(new RepairPriority() { TemplateName = "New template", Priority = _defaultTemplate.Priority.ToList() });
            bindingSourceRepairPriorityTemplates.Position = bindingSourceRepairPriorityTemplates.Count - 1;
        }

        private void btnRemoveTemplate_Click(object sender, EventArgs e)
        {
            if (bindingSourceRepairPriorityTemplates.Count > 0 &&
                (bindingSourceRepairPriorityTemplates.Current != _defaultTemplate &&
                bindingSourceRepairPriorityTemplates.Current != _originalTemplate))
            { bindingSourceRepairPriorityTemplates.RemoveCurrent(); }
        }

        private void btnSaveSelect_Click(object sender, EventArgs e)
        {
            this.SelectedTemplate = bindingSourceRepairPriorityTemplates.Current as RepairPriority;
            this.ResultList = (bindingSourceRepairPriorityTemplates.DataSource as List<RepairPriority>).ToList();
            this.ResultList.Remove(_originalTemplate);
            this.ResultList.Remove(_defaultTemplate);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (bindingSourceRepairPriority.Position != 0)
            {
                ComponentCategoryType temp = (ComponentCategoryType)bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position - 1];
                bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position - 1] = bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position];
                bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position] = temp;
                bindingSourceRepairPriority.ResetBindings(false);
                lstbRepairPriority.SelectedIndex = bindingSourceRepairPriority.Position - 1;
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (bindingSourceRepairPriority.Position != bindingSourceRepairPriority.Count - 1)
            {
                ComponentCategoryType temp = (ComponentCategoryType)bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position + 1];
                bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position + 1] = bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position];
                bindingSourceRepairPriority.List[bindingSourceRepairPriority.Position] = temp;
                bindingSourceRepairPriority.ResetBindings(false);
                lstbRepairPriority.SelectedIndex = bindingSourceRepairPriority.Position + 1;
            }
        }

        private void cmbTempalteList_Validated(object sender, EventArgs e)
        {
            float maxSize = 0;
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                foreach (RepairPriority item in cmbTempalteList.Items)
                {
                    maxSize = Math.Max(maxSize, g.MeasureString(item.TemplateName, SystemFonts.DefaultFont).Width);
                }
            }
            cmbTempalteList.DropDownWidth = (int)maxSize;
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            var curItem = (RepairPriority)bindingSourceRepairPriorityTemplates.Current;
            using (fRepairPriorityRename frm = new fRepairPriorityRename(curItem.TemplateName))
            {
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    curItem.TemplateName = frm.Result;
                    bindingSourceRepairPriorityTemplates.ResetCurrentItem();
                    CheckUserDefaultTemplate();
                }
            }
        }

        private void CheckUserDefaultTemplate()
        {
            string msg = "";
            bool playerPressent = _userTemplates.Any(x => string.Equals(x.TemplateName, _playerDefaultTemplate, StringComparison.InvariantCultureIgnoreCase));
            bool aiPressent = _userTemplates.Any(x => string.Equals(x.TemplateName, _aiDefaultTemplate, StringComparison.InvariantCultureIgnoreCase));
            if (!playerPressent)
            {
                msg += $"Missing Default player repair template in user list, consider to create it as ships will use Original repair template instead. " +
                    $"Expected name is: {_playerDefaultTemplate}";
            }
            if (!aiPressent)
            {
                if (playerPressent) { msg += "\r\n"; }
                msg += $"Missing Default AI repair template in user list, consider to create it as ships will use Original repair template instead. " +
                    $"Expected name is: {_aiDefaultTemplate}";
            }
            if (!playerPressent || !aiPressent)
            {
                MessageBox.Show(this, msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void fRepairPriority_Load(object sender, EventArgs e)
        {
            CheckUserDefaultTemplate();
        }
    }
}
