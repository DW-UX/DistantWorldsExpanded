using DistantWorlds.HotKeyMapping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace DistantWorlds
{
    public enum KeyMappingTarget
    {
        Unknown,
        Escape,
        ZoomToSelected,
        ToglePause,
        ZoomToGalaxyLevel,
        ZoomInMaximumLevel,
        ZoomToSystemLevel,
        ZoomToSectorLevel,
        ControlGroup0,
        ControlGroup1,
        ControlGroup2,
        ControlGroup3,
        ControlGroup4,
        ControlGroup5,
        ControlGroup6,
        ControlGroup7,
        ControlGroup8,
        ControlGroup9,
        EnableAuto,
        CycleSelectionBackward,
        CycleColonySelectionBackward,
        CycleMainDisplayTypes,
        ShipEscapeCommand, CycleFleet,
        OpenGalaxyMap,
        OpenMessageHistory,
        CycleIdleShip,
        ToggleViewLock,
        CycleMilitaryShip,
        CycleSelectionForward,
        OpenOptions,
        CycleBases,
        RefuelShip,
        StopShip,
        CyclePanelVisibility,
        OpenEmpireComparison,
        CycleColonyOrExplorer,
        CycleConstractionShip,
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
        DecreaseGameSpeed
    }
    public class KeyMapper
    {
        private readonly string _mappingFile;
        private const int _FormatVersion = 1;
        private Dictionary<KeyMappingTarget, List<Keys>> _mapping = new Dictionary<KeyMappingTarget, List<Keys>>();
        private Dictionary<Keys, KeyMappingTarget> _targetMapping = new Dictionary<Keys, KeyMappingTarget>();

        public KeyMapper(string mappingFile)
        {
            this._mappingFile = mappingFile;
        }

        public bool MapKeys()
        {
            bool res = false;
            FileInfo fileInfo = new FileInfo(_mappingFile);
            if (fileInfo.Exists)
            {
                using StreamReader sR = new StreamReader(fileInfo.OpenRead());
                var mappingModel = JsonConvert.DeserializeObject<MappingJsonFileModel>(sR.ReadToEnd());
                SetMappingKeys(mappingModel);
                if (ValidateNoDuplicates(mappingModel))
                {
                    res = true;
                }
            }
            return res;
        }
        public static void GenerateEmpyFile(string mappingFile)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(mappingFile);
                if (!fileInfo.Exists)
                {
                    MappingJsonFileModel model = GetDefaultModel();

                    using StreamWriter sW = new StreamWriter(fileInfo.OpenWrite());
                    sW.Write(JsonConvert.SerializeObject(model, Formatting.Indented));
                }
                else
                {
                    MessageBox.Show("MappingFile already exist, remove file before generating new file", "Mapping file generating error", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error", MessageBoxButtons.OK);
            }
        }

        public bool GetMappedKeys(KeyMappingTarget target, out List<Keys> keys)
        {
            bool res = false;
            if (_mapping.ContainsKey(target))
            {
                keys = _mapping[target];
                res = true;
            }
            else
            { keys = new List<Keys>(); }
            return res;
        }
        public bool GetMappedTarget(Keys key, out KeyMappingTarget target)
        {
            bool res = false;
            target = KeyMappingTarget.Unknown;
            if (_targetMapping.ContainsKey(key))
            {
                target = _targetMapping[key];
                res = true;
            }
            return res;
        }
        private bool ValidateNoDuplicates(MappingJsonFileModel model)
        {
            var keyList = _mapping.Values.SelectMany(y => y);
            return keyList.Count() == keyList.Distinct().Count();
        }

        private List<Keys> ConvertTextValueToKey(string values)
        {
            List<Keys> res = new List<Keys>();
            if (!string.IsNullOrWhiteSpace(values))
            {
                string[] splitValues = values.Split(';');

                foreach (var item in splitValues)
                {
                    if (Enum.TryParse(item, true, out Keys key))
                    {
                        res.Add(key);
                    }
                }
                if (res.Count == 0)
                { res.Add(Keys.None); }

            }
            else
            { res.Add(Keys.None); }
            return res;
        }
        private void SetMappingKeys(MappingJsonFileModel mappingModel)
        {
            AddDefaultMapping();
            //записать в словарь значения по таргету
            _mapping[KeyMappingTarget.ControlGroup0] = ConvertTextValueToKey(mappingModel.ControlGroup0);
            _mapping[KeyMappingTarget.ControlGroup1] = ConvertTextValueToKey(mappingModel.ControlGroup1);
            _mapping[KeyMappingTarget.ControlGroup2] = ConvertTextValueToKey(mappingModel.ControlGroup2);
            _mapping[KeyMappingTarget.ControlGroup3] = ConvertTextValueToKey(mappingModel.ControlGroup3);
            _mapping[KeyMappingTarget.ControlGroup4] = ConvertTextValueToKey(mappingModel.ControlGroup4);
            _mapping[KeyMappingTarget.ControlGroup5] = ConvertTextValueToKey(mappingModel.ControlGroup5);
            _mapping[KeyMappingTarget.ControlGroup6] = ConvertTextValueToKey(mappingModel.ControlGroup6);
            _mapping[KeyMappingTarget.ControlGroup7] = ConvertTextValueToKey(mappingModel.ControlGroup7);
            _mapping[KeyMappingTarget.ControlGroup8] = ConvertTextValueToKey(mappingModel.ControlGroup8);
            _mapping[KeyMappingTarget.ControlGroup9] = ConvertTextValueToKey(mappingModel.ControlGroup9);
            _mapping[KeyMappingTarget.CycleBases] = ConvertTextValueToKey(mappingModel.CycleBases);
            _mapping[KeyMappingTarget.RefuelShip] = ConvertTextValueToKey(mappingModel.RefuelShip);
            _mapping[KeyMappingTarget.CycleColonyOrExplorer] = ConvertTextValueToKey(mappingModel.CycleColonyOrExplorer);
            _mapping[KeyMappingTarget.CycleColonySelectionBackward] = ConvertTextValueToKey(mappingModel.CycleColonySelectionBackward);
            _mapping[KeyMappingTarget.CycleConstractionShip] = ConvertTextValueToKey(mappingModel.CycleConstractionShip);
            _mapping[KeyMappingTarget.CycleFleet] = ConvertTextValueToKey(mappingModel.CycleFleet);
            _mapping[KeyMappingTarget.CycleIdleShip] = ConvertTextValueToKey(mappingModel.CycleIdleShip);
            _mapping[KeyMappingTarget.CycleMainDisplayTypes] = ConvertTextValueToKey(mappingModel.CycleMainDisplayTypes);
            _mapping[KeyMappingTarget.CycleMilitaryShip] = ConvertTextValueToKey(mappingModel.CycleMilitaryShip);
            _mapping[KeyMappingTarget.CyclePanelVisibility] = ConvertTextValueToKey(mappingModel.CyclePanelVisibility);
            _mapping[KeyMappingTarget.CycleSelectionForward] = ConvertTextValueToKey(mappingModel.CycleSelectionForward);
            _mapping[KeyMappingTarget.CycleSelectionBackward] = ConvertTextValueToKey(mappingModel.CycleSelectionBackward);
            _mapping[KeyMappingTarget.CycleShipEngagmentRange] = ConvertTextValueToKey(mappingModel.CycleShipEngagmentRange);
            _mapping[KeyMappingTarget.DecreaseGameSpeed] = ConvertTextValueToKey(mappingModel.DecreaseGameSpeed);
            _mapping[KeyMappingTarget.EnableAuto] = ConvertTextValueToKey(mappingModel.EnableAuto);
            _mapping[KeyMappingTarget.FindNearestMilitaryShip] = ConvertTextValueToKey(mappingModel.FindNearestMilitaryShip);
            _mapping[KeyMappingTarget.IncreaseGameSpeed] = ConvertTextValueToKey(mappingModel.IncreaseGameSpeed);
            _mapping[KeyMappingTarget.OpenBuildOrderScreen] = ConvertTextValueToKey(mappingModel.OpenBuildOrderScreen);
            _mapping[KeyMappingTarget.OpenColoniesScreen] = ConvertTextValueToKey(mappingModel.OpenColoniesScreen);
            _mapping[KeyMappingTarget.OpenConstructionYardsScreen] = ConvertTextValueToKey(mappingModel.OpenConstructionYardsScreen);
            _mapping[KeyMappingTarget.OpenDesignsScreen] = ConvertTextValueToKey(mappingModel.OpenDesignsScreen);
            _mapping[KeyMappingTarget.OpenDiplomacyScreen] = ConvertTextValueToKey(mappingModel.OpenDiplomacyScreen);
            _mapping[KeyMappingTarget.OpenEmpireComparison] = ConvertTextValueToKey(mappingModel.OpenEmpireComparison);
            _mapping[KeyMappingTarget.OpenEmpireSummaryScreen] = ConvertTextValueToKey(mappingModel.OpenEmpireSummaryScreen);
            _mapping[KeyMappingTarget.OpenExpansionPlannerScreen] = ConvertTextValueToKey(mappingModel.OpenExpansionPlannerScreen);
            _mapping[KeyMappingTarget.OpenFleetScreen] = ConvertTextValueToKey(mappingModel.OpenFleetScreen);
            _mapping[KeyMappingTarget.OpenGalaxyMap] = ConvertTextValueToKey(mappingModel.OpenGalaxyMap);
            _mapping[KeyMappingTarget.OpenGroundInvasionStatusScreen] = ConvertTextValueToKey(mappingModel.OpenGroundInvasionStatusScreen);
            _mapping[KeyMappingTarget.OpenHelp] = ConvertTextValueToKey(mappingModel.OpenHelp);
            _mapping[KeyMappingTarget.OpenIntelligenceAgentsScreen] = ConvertTextValueToKey(mappingModel.OpenIntelligenceAgentsScreen);
            _mapping[KeyMappingTarget.OpenMessageHistory] = ConvertTextValueToKey(mappingModel.OpenMessageHistory);
            _mapping[KeyMappingTarget.OpenOptions] = ConvertTextValueToKey(mappingModel.OpenOptions);
            _mapping[KeyMappingTarget.OpenResearchScreen] = ConvertTextValueToKey(mappingModel.OpenResearchScreen);
            _mapping[KeyMappingTarget.OpenShipAndBasesScreen] = ConvertTextValueToKey(mappingModel.OpenShipAndBasesScreen);
            _mapping[KeyMappingTarget.ShipEscapeCommand] = ConvertTextValueToKey(mappingModel.ShipEscapeCommand);
            _mapping[KeyMappingTarget.StopShip] = ConvertTextValueToKey(mappingModel.StopShip);
            _mapping[KeyMappingTarget.ToggleViewLock] = ConvertTextValueToKey(mappingModel.ToggleViewLock);
            _mapping[KeyMappingTarget.ToglePause] = ConvertTextValueToKey(mappingModel.ToglePause);
            _mapping[KeyMappingTarget.ZoomInMaximumLevel] = ConvertTextValueToKey(mappingModel.ZoomInMaximumLevel);
            _mapping[KeyMappingTarget.ZoomToGalaxyLevel] = ConvertTextValueToKey(mappingModel.ZoomToGalaxyLevel);
            _mapping[KeyMappingTarget.ZoomToSectorLevel] = ConvertTextValueToKey(mappingModel.ZoomToSectorLevel);
            _mapping[KeyMappingTarget.ZoomToSelected] = ConvertTextValueToKey(mappingModel.ZoomToSelected);
            _mapping[KeyMappingTarget.ZoomToSystemLevel] = ConvertTextValueToKey(mappingModel.ZoomToSystemLevel);

            foreach (var item in _mapping)
            {
                foreach (var key in item.Value)
                {
                    _targetMapping[key] = item.Key;
                }
            }
        }
        private void AddDefaultMapping()
        {
            _mapping[KeyMappingTarget.Escape] = new List<Keys>() { Keys.Escape };
        }
        private static MappingJsonFileModel GetDefaultModel()
        {
            return new MappingJsonFileModel()
            {
                FormatVersion = _FormatVersion,
                ControlGroup0 = Keys.D0.ToString(),
                ControlGroup1 = Keys.D1.ToString(),
                ControlGroup2 = Keys.D2.ToString(),
                ControlGroup3 = Keys.D3.ToString(),
                ControlGroup4 = Keys.D4.ToString(),
                ControlGroup5 = Keys.D5.ToString(),
                ControlGroup6 = Keys.D6.ToString(),
                ControlGroup7 = Keys.D7.ToString(),
                ControlGroup8 = Keys.D8.ToString(),
                ControlGroup9 = Keys.D9.ToString(),
                CycleBases = Keys.P.ToString(),
                CycleColonyOrExplorer = Keys.X.ToString(),
                CycleColonySelectionBackward = Keys.C.ToString(),
                CycleConstractionShip = Keys.Y.ToString(),
                CycleFleet = Keys.F.ToString(),
                CycleIdleShip = Keys.I.ToString(),
                CycleMainDisplayTypes = Keys.D.ToString(),
                CycleMilitaryShip = Keys.M.ToString(),
                CyclePanelVisibility = Keys.T.ToString(),
                CycleSelectionBackward = Keys.B.ToString(),
                CycleSelectionForward = Keys.N.ToString(),
                CycleShipEngagmentRange = Keys.Oemcomma.ToString(),
                DecreaseGameSpeed = Keys.OemMinus.ToString() + ";" + Keys.Subtract.ToString(),
                EnableAuto = Keys.A.ToString(),
                FindNearestMilitaryShip = Keys.Z.ToString(),
                IncreaseGameSpeed = Keys.Oemplus.ToString() + ";" + Keys.Add.ToString(),
                OpenBuildOrderScreen = Keys.F8.ToString(),
                OpenColoniesScreen = Keys.F2.ToString(),
                OpenConstructionYardsScreen = Keys.F10.ToString(),
                OpenDesignsScreen = Keys.F9.ToString(),
                OpenDiplomacyScreen = Keys.F5.ToString(),
                OpenEmpireComparison = Keys.V.ToString(),
                OpenEmpireSummaryScreen = Keys.F6.ToString(),
                OpenExpansionPlannerScreen = Keys.F3.ToString(),
                OpenFleetScreen = Keys.F12.ToString(),
                OpenGalaxyMap = Keys.G.ToString(),
                OpenGroundInvasionStatusScreen = Keys.OemOpenBrackets.ToString(),
                OpenHelp = Keys.F1.ToString(),
                OpenIntelligenceAgentsScreen = Keys.F4.ToString(),
                OpenMessageHistory = Keys.H.ToString(),
                OpenOptions = Keys.O.ToString(),
                OpenResearchScreen = Keys.F7.ToString(),
                OpenShipAndBasesScreen = Keys.F11.ToString(),
                RefuelShip = Keys.R.ToString(),
                ShipEscapeCommand = Keys.E.ToString(),
                StopShip = Keys.S.ToString(),
                ToggleViewLock = Keys.L.ToString(),
                ToglePause = Keys.Space.ToString() + ";" + Keys.Pause.ToString(),
                ZoomInMaximumLevel = Keys.Home.ToString(),
                ZoomToGalaxyLevel = Keys.End.ToString(),
                ZoomToSectorLevel = Keys.Delete.ToString(),
                ZoomToSelected = Keys.Back.ToString(),
                ZoomToSystemLevel = Keys.Insert.ToString(),
            };

        }
    }
}
