using ExpansionMod.Objects.HotKeyMapping;
using ExpansionMod.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaconDistantWorlds.HotKeys
{
    public class BaconKeyMapper : KeyMapper
    {
        public BaconKeyMapper(string mappingFileName) : base(mappingFileName)
        {
        }
        protected override void RemoveKeysFromOlderVersions(MappingJsonFileModel mappingModel)
        {
            mappingModel.HotKeys.RemoveAll(x => x.TargetMethodId == 3 || x.TargetMethodId == 4);
        }
        protected override void RemapAndRenameOlderVersions(MappingJsonFileModel mappingModel)
        {
            var defaultModel = GetDefaultModel();
            Dictionary<int, string> oldNames = new Dictionary<int, string>();
            oldNames[24] = "OrderPassengershipMission";

            foreach (var item in oldNames)
            {
                var temp = mappingModel.HotKeys.FirstOrDefault(x => x.TargetMethodId == item.Key);
                if (temp != null)
                {
                    var defaultItem = defaultModel.HotKeys.FirstOrDefault(x => x.TargetMethodId == item.Key);
                    if (defaultItem != null)
                    { temp.FriendlyName = defaultItem.FriendlyName; }
                }
            }
            _reSaveNeeded = true;
        }
        protected override MappingJsonFileModel GetDefaultModel()
        {
            var res = new MappingJsonFileModel()
            {
                FormatVersion = _CurrentCodeFormatVersion,
                TargetCollectionVersion = _CurrentCodeFormatVersion,
            };
            var item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowDetailedInfo.ToString(), TargetMethodId = 1 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Return } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowMissionCommand.ToString(), TargetMethodId = 2 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.Return } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.AssignCargoMission.ToString(), TargetMethodId = 5 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.SetFighterTarget.ToString(), TargetMethodId = 6 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.OrderBombersToAttack.ToString(), TargetMethodId = 7 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.OrderBombersToAttackAll.ToString(), TargetMethodId = 8 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.TransferFighter.ToString(), TargetMethodId = 9 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ToggleAutomateCarrierOps.ToString(), TargetMethodId = 10 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.A } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowCustomBomberForm.ToString(), TargetMethodId = 11 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.B } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.CalculateDistance.ToString(), TargetMethodId = 12 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShipFinder.ToString(), TargetMethodId = 13 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.E } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.FixExplorerCurrentSystem.ToString(), TargetMethodId = 14 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.AssignMiningShipToTarget.ToString(), TargetMethodId = 15 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowPrisonForm.ToString(), TargetMethodId = 16 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.IncreaseDockingBayCapacity.ToString(), TargetMethodId = 17 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.Q } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowDamagedComponents.ToString(), TargetMethodId = 18 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.RevealIfPirate.ToString(), TargetMethodId = 19 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.RushStateShips.ToString(), TargetMethodId = 20 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ShowStats.ToString(), TargetMethodId = 21 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.S } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.AddShipToTradeList.ToString(), TargetMethodId = 22 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.T } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ForceUnloadAtDestination.ToString(), TargetMethodId = 23 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.U } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.AssignPassengershipMission.ToString(), TargetMethodId = 24 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.W } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.CycleSelectedByRoleBackward.ToString(), TargetMethodId = 25 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemSemicolon } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.GetParentCarrier.ToString(), TargetMethodId = 26 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemQuestion } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.CycleSelectedByRoleForward.ToString(), TargetMethodId = 27 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemQuotes } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = BaconKeyMappingFriendlyNames.ToggleShipAutoBaconImpl.ToString(), TargetMethodId = 28 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.A } });
            res.HotKeys.Add(item);
            return res;
        }
    }
}
