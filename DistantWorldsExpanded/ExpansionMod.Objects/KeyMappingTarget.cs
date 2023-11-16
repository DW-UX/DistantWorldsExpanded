using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpansionMod.Objects
{
    public enum KeyMappingFriendlyNames
    {
        Escape,
        ZoomToSelected,
        ToglePause,
        ZoomToGalaxyLevel,
        ZoomInMaximumLevel,
        ZoomToSystemLevel,
        ZoomToSectorLevel,
        SelectControlGroup0,
        SelectControlGroup1,
        SelectControlGroup2,
        SelectControlGroup3,
        SelectControlGroup4,
        SelectControlGroup5,
        SelectControlGroup6,
        SelectControlGroup7,
        SelectControlGroup8,
        SelectControlGroup9,
        SelectControlGroup0WithFocus,
        SelectControlGroup1WithFocus,
        SelectControlGroup2WithFocus,
        SelectControlGroup3WithFocus,
        SelectControlGroup4WithFocus,
        SelectControlGroup5WithFocus,
        SelectControlGroup6WithFocus,
        SelectControlGroup7WithFocus,
        SelectControlGroup8WithFocus,
        SelectControlGroup9WithFocus,
        SetControlGroup0,
        SetControlGroup1,
        SetControlGroup2,
        SetControlGroup3,
        SetControlGroup4,
        SetControlGroup5,
        SetControlGroup6,
        SetControlGroup7,
        SetControlGroup8,
        SetControlGroup9,
        EnableAuto,
        CycleSelectionBackward,
        CycleColonySelectionBackward,
        CycleColonySelectionForward,
        CycleColonySelectionBackwardWithFocus,
        CycleColonySelectionForwardWithFocus,
        CycleMainDisplayTypes,
        ShipEscapeCommand, 
        CycleFleetForward,
        CycleFleetBackward,
        CycleFleetForwardWithFocus,
        CycleFleetBackwardWithFocus,
        OpenGalaxyMap,
        OpenMessageHistory,
        CycleIdleShipForward,
        CycleIdleShipBackward,
        CycleIdleShipForwardWithFocus,
        CycleIdleShipBackwardWithFocus,
        ToggleViewLock,
        CycleMilitaryShipForward,
        CycleMilitaryShipBackward,
        CycleMilitaryShipForwardWithFocus,
        CycleMilitaryShipBackwardWithFocus,
        CycleSelectionForward,
        OpenOptions,
        CycleBasesForward,
        CycleBasesBackward,
        CycleBasesForwardWithFocus,
        CycleBasesBackwardWithFocus,
        RefuelShip,
        StopShip,
        CyclePanelVisibility,
        OpenEmpireComparison,
        CycleColonyOrExplorerForward,
        CycleColonyOrExplorerBackward,
        CycleColonyOrExplorerForwardWithFocus,
        CycleColonyOrExplorerBackwardWithFocus,
        CycleConstractionShipForward,
        CycleConstractionShipBackward,
        CycleConstractionShipForwardWithFocus,
        CycleConstractionShipBackwardWithFocus,
        FindNearestMilitaryShip,
        OpenHelp,
        OpenColoniesScreen,
        OpenExpansionPlannerScreen,
        OpenIntelligenceAgentsScreen,
        OpenDiplomacyScreen,
        OpenEmpireSummaryScreen,
        OpenResearchScreen,
        OpenDesignsScreen,
        OpenBuildOrderScreen,
        OpenConstructionYardsScreen,
        OpenShipAndBasesScreen,
        OpenFleetScreen,
        OpenGroundInvasionStatusScreen,
        IncreaseGameSpeed,
        CycleShipEngagmentRange,
        DecreaseGameSpeed,

        //ExtentedMod hotkeys
        OpenConstractionQueueEditor,
    }
    public class KeyMappingTarget : IComparable<KeyMappingTarget>
    {
        public string FriendlyName { get; set; }
        public int TargetMethodId { get; set; }
        public List<MappedHotKey> MappedHotKeys { get; set; } = new List<MappedHotKey>();

        public KeyMappingTarget Clone()
        {
            return new KeyMappingTarget()
            {
                FriendlyName = this.FriendlyName,
                MappedHotKeys = this.MappedHotKeys.Select(x => x.Clone()).ToList(),
                TargetMethodId = this.TargetMethodId,
            };
        }

        public int CompareTo(KeyMappingTarget other)
        {
            int res = 0;
            if ((res = this.TargetMethodId.CompareTo(other.TargetMethodId)) == 0)
            {
                for (int i = 0; i < this.MappedHotKeys.Count; i++)
                {
                    if ((res = this.MappedHotKeys[i].CompareTo(other.MappedHotKeys[i])) != 0)
                    { break; }
                }
            }
            return res;
        }
    }
    public class MappedHotKey : IComparable<MappedHotKey>
    {
        public List<Keys> KeyCode { get; set; } = new List<Keys>();
        [JsonIgnore]
        public KeyMappingTarget Parent { get; set; }

        public MappedHotKey(KeyMappingTarget parent)
        {
            Parent = parent;
        }
        public MappedHotKey Clone()
        {
            return new MappedHotKey(this.Parent)
            {
                KeyCode = this.KeyCode.ToList(),
            };
        }

        public int CompareTo(MappedHotKey other)
        {
            int res = 0;
            if ((res = this.KeyCode.Count.CompareTo(other.KeyCode.Count)) == 0)
            {
                for (int i = 0; i < this.KeyCode.Count; i++)
                {
                    if ((res = this.KeyCode[i].CompareTo(other.KeyCode[i])) != 0)
                    { break; }
                }
            }
            return res;
        }
    }

    public class KeyMappingTargetComparer : IEqualityComparer<KeyMappingTarget>
    {
        private static MappedHotKeyComparer _keyComparer = new MappedHotKeyComparer();
        public bool Equals(KeyMappingTarget x, KeyMappingTarget y)
        {
            bool res;
            if (x.TargetMethodId == y.TargetMethodId)
            {
                res = x.MappedHotKeys.SequenceEqual(y.MappedHotKeys, _keyComparer);
            }
            else
            { res = x.TargetMethodId.Equals(y.TargetMethodId); }
            return res;
        }

        public int GetHashCode(KeyMappingTarget obj)
        {
            throw new NotImplementedException();
        }
    }
    public class MappedHotKeyComparer : IEqualityComparer<MappedHotKey>
    {
        public bool Equals(MappedHotKey x, MappedHotKey y)
        {
            return x.KeyCode.SequenceEqual(y.KeyCode);
        }

        public int GetHashCode(MappedHotKey obj)
        {
            throw new NotImplementedException();
        }
    }
}
