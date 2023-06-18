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

namespace ExpansionMod
{
    public partial class MissionQueueEditor : Form
    {
        private BuiltObject _targetShip;
        private BuiltObjectMission _currentMission;
        private List<ListMissionObject> _queuedMissions = new List<ListMissionObject>();

        public MissionQueueEditor(BuiltObject targetShip, Control parent)
        {
            InitializeComponent();
            _targetShip = targetShip;

            bindingSourceQueuedMissions.DataSource = typeof(ExpansionMod.ListMissionObject);
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
            queuedList.AddRange(_queuedMissions.Select(x=>x.Mission));
            _targetShip._SubsequentMissions = queuedList;
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

        private void Restore()
        {
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
