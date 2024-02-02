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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ExpansionMod
{
    public partial class MissionQueueEditor : Form
    {
        private BuiltObject _targetShip;
        private List<Tuple<BuiltObject, ListMissionObject>> _shipsToAsignMissionOnSave = new();
        private Main _main;
        private BuiltObjectMission _currentMission;
        private List<ListMissionObject> _queuedMissions = new List<ListMissionObject>();

        public MissionQueueEditor(BuiltObject targetShip, Main main)
        {
            InitializeComponent();
            _targetShip = targetShip;
            _main = main;

            bindingSourceQueuedMissions.DataSource = typeof(ExpansionMod.ListMissionObject);
            bindingSourceAllConstrShips.DataSource = typeof(BuiltObject);

            bindingSourceAllConstrShips.DataSource = _main._Game.PlayerEmpire.BuiltObjects.Where(x => x.SubRole == BuiltObjectSubRole.ConstructionShip && x != _targetShip).ToList();
            SetDropDownListSize();
            Restore();
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 0)
            {
                ListMissionObject queuedTarget = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] as ListMissionObject;
                ListMissionObject currentMissionObj = new ListMissionObject(_targetShip, _currentMission);
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] = currentMissionObj;
                _currentMission = queuedTarget.Mission;
                lblCurrentMission.Text = Galaxy.ResolveDescription(_targetShip.Empire, _currentMission);
                bindingSourceQueuedMissions.ResetCurrentItem();
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 1 && bindingSourceQueuedMissions.Position > 0)
            {
                var itemToMove = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position];
                var targetItem = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position - 1];
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position - 1] = itemToMove;
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] = targetItem;
                bindingSourceQueuedMissions.ResetBindings(false);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 1 && bindingSourceQueuedMissions.Position < bindingSourceQueuedMissions.Count - 1)
            {
                var itemToMove = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position];
                var targetItem = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position - 1];
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position - 1] = itemToMove;
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] = targetItem;
                bindingSourceQueuedMissions.ResetBindings(false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            object target = null;
            if (_currentMission.TargetBuiltObject != null)
            { target = _currentMission.TargetBuiltObject; }
            else if (_currentMission.TargetHabitat != null)
            { target = _currentMission.TargetHabitat; }
            else if (_currentMission.TargetCreature != null)
            { target = _currentMission.TargetCreature; }
            else if (_currentMission.TargetShipGroup != null)
            { target = _currentMission.TargetShipGroup; }
            else if (_currentMission.TargetSector != null)
            { target = _currentMission.TargetSector; }
            object target2 = null;
            if (_currentMission.SecondaryTargetBuiltObject != null)
            { target2 = _currentMission.SecondaryTargetBuiltObject; }
            else if (_currentMission.SecondaryTargetHabitat != null)
            { target2 = _currentMission.SecondaryTargetHabitat; }
            else if (_currentMission.SecondaryTargetCreature != null)
            { target2 = _currentMission.SecondaryTargetCreature; }
            else if (_currentMission.SecondaryTargetShipGroup != null)
            { target2 = _currentMission.SecondaryTargetShipGroup; }
            if (!_targetShip.Mission.Equals(_currentMission))
            {
                _targetShip.AssignMission(_currentMission.Type,
                    target,
                    target2,
                     _currentMission.Design,
                     _currentMission.X,
                     _currentMission.Y,
                    _currentMission.Priority,
                     true);
            }
            var queuedList = new BuiltObjectMissionList();
            queuedList.AddRange(_queuedMissions.Select(x => x.Mission));
            _targetShip._SubsequentMissions = queuedList;

            foreach (var pair in _shipsToAsignMissionOnSave)
            {
                pair.Item1._SubsequentMissions.Add(pair.Item2.Mission);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 0)
            { bindingSourceQueuedMissions.RemoveCurrent(); }
        }

        private void btnRestoreMissions_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void btnAssignOtherShip_Click(object sender, EventArgs e)
        {
            if (bindingSourceAllConstrShips.Current != null && bindingSourceQueuedMissions.Current != null)
            {
                _shipsToAsignMissionOnSave.Add(new Tuple<BuiltObject, ListMissionObject>(
                bindingSourceAllConstrShips.Current as BuiltObject,
                bindingSourceQueuedMissions.Current as ListMissionObject));

                bindingSourceQueuedMissions.RemoveCurrent();
            }
        }
        private void btnMoveToTop_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 1 && bindingSourceQueuedMissions.Position > 0)
            {
                var itemToMove = bindingSourceQueuedMissions.Current;
                var targetItem = bindingSourceQueuedMissions[0];
                bindingSourceQueuedMissions[0] = itemToMove;
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] = targetItem;
                bindingSourceQueuedMissions.ResetBindings(false);
            }
        }

        private void btnMoveToBottom_Click(object sender, EventArgs e)
        {
            if (bindingSourceQueuedMissions.Count > 1 && bindingSourceQueuedMissions.Position < bindingSourceQueuedMissions.Count - 1)
            {
                var itemToMove = bindingSourceQueuedMissions.Current;
                var targetItem = bindingSourceQueuedMissions[bindingSourceQueuedMissions.Count - 1];
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Count - 1] = itemToMove;
                bindingSourceQueuedMissions[bindingSourceQueuedMissions.Position] = targetItem;
                bindingSourceQueuedMissions.ResetBindings(false);
            }
        }

        private void btnRetrofit_Click(object sender, EventArgs e)
        {
            string userMsg = null;
            StellarObject stellarObject2 = _main._Game.PlayerEmpire.FindNearestShipYard(_targetShip, canRepairOrBuild: true, includeVerySmallYards: true);

            Design design32 = _main._Game.PlayerEmpire.Designs.FindNewestCanBuildFullEvaluate(_targetShip.SubRole, _targetShip.ParentHabitat);

            _main._Game.PlayerEmpire.DetermineRetrofitAffordability(_targetShip, design32, out double cost2, out ComponentList componentsToProcure2);
            if (design32 != null && design32 != _targetShip.Design)
            {
                if (cost2 > _main._Game.PlayerEmpire.StateMoney)
                {
                    userMsg = TextResolver.GetText("Not Enough Money");
                }
                else if (_targetShip.RetrofitDesign != null || _targetShip.BuiltAt != null ||
                    (_currentMission != null && _currentMission.Type == BuiltObjectMissionType.Retrofit) ||
                    _queuedMissions.Any(x => x.Mission.Type == BuiltObjectMissionType.Retrofit))
                {
                    userMsg = TextResolver.GetText("Already Retrofitting");
                }
                else
                {
                    //_targetShip.QueueMission(BuiltObjectMissionType.Retrofit, stellarObject2, null, design32, BuiltObjectMissionPriority.VeryHigh);
                    bindingSourceQueuedMissions.Add(new ListMissionObject(_targetShip,
                                                                          new BuiltObjectMission(
                                                                              _main._Game.Galaxy,
                                                                              _targetShip,
                                                                              BuiltObjectMissionType.Retrofit,
                                                                              stellarObject2,
                                                                              null,
                                                                              null,
                                                                              null,
                                                                              null,
                                                                              design32,
                                                                              -2000000001.0,
                                                                              -2000000001.0,
                                                                              -1L,
                                                                              BuiltObjectMissionPriority.VeryHigh,
                                                                              allowReprocessing: true,
                                                                              allowBuiltObjectChanges: false)));
                    bindingSourceQueuedMissions.ResetBindings(false);
                }
            }
            else
            {
                userMsg = TextResolver.GetText("Already Latest Design");
            }
            if (!string.IsNullOrWhiteSpace(userMsg))
            {
                bool flag = _main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
                if (!flag)
                { _main._Game.Galaxy.Pause(); }
                MessageBox.Show(this, userMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (!flag)
                { _main._Game.Galaxy.Resume(); }
            }
        }

        private void Restore()
        {
            _shipsToAsignMissionOnSave.Clear();
            _queuedMissions.Clear();
            _currentMission = _targetShip.Mission;
            lblCurrentMission.Text = Galaxy.ResolveDescription(_targetShip.Empire, _currentMission);
            foreach (var item in _targetShip.SubsequentMissions)
            {
                _queuedMissions.Add(new ListMissionObject(_targetShip, item));
            }
            bindingSourceQueuedMissions.DataSource = _queuedMissions;
            bindingSourceQueuedMissions.ResetBindings(false);
        }

        private void SetDropDownListSize()
        {
            int maxWidth = cmbOtherConstructShips.DropDownWidth;
            for (int i = 0; i < cmbOtherConstructShips.Items.Count; i++)
            {
                maxWidth = Math.Max(maxWidth, TextRenderer.MeasureText(cmbOtherConstructShips.GetItemText(cmbOtherConstructShips.Items[i]),
                                                                       cmbOtherConstructShips.Font).Width);
            }
            if (cmbOtherConstructShips.Items.Count > cmbOtherConstructShips.MaxDropDownItems)
                maxWidth += SystemInformation.VerticalScrollBarWidth;
            cmbOtherConstructShips.DropDownWidth = maxWidth;
        }

    }
    public class ListMissionObject
    {
        public string Name { get; set; }
        public BuiltObjectMission Mission { get; set; }
        public ListMissionObject(BuiltObject targetShip, BuiltObjectMission mission)
        {
            this.Name = Galaxy.ResolveDescription(targetShip.Empire, mission);
            this.Mission = mission;
        }
    }


}