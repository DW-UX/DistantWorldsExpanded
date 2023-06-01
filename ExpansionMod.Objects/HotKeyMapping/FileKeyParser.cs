using ExpansionMod.Objects;
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

namespace ExpansionMod.Objects.HotKeyMapping
{
    public class KeyMapper : IHotKeyParser
    {
        public const int _CurrentCodeFormatVersion = 2;

        protected string _mappingFileName;
        protected List<MappedHotKey> _keyMapping = new List<MappedHotKey>();
        protected MappedHotKey _notFound;

        public KeyMapper(string mappingFileName)
        {
            this._mappingFileName = mappingFileName;
            KeyMappingTarget notFoundTarget = new KeyMappingTarget() { FriendlyName = "NoHotkey", MappedHotKeys = new List<MappedHotKey>(), TargetMethodId = -1 };
            _notFound = new MappedHotKey(notFoundTarget);
        }

        public bool MapKeys(string hotkeysDir)
        {
            bool res = false;
            FileInfo fileInfo = new FileInfo(Path.Combine(hotkeysDir, _mappingFileName));
            if (fileInfo.Exists)
            {
                using StreamReader sR = new StreamReader(fileInfo.OpenRead());
                var mappingModel = JsonConvert.DeserializeObject<MappingJsonFileModel>(sR.ReadToEnd());
                if (ValidateNoDuplicates(mappingModel) & ValidateEscapeKeyMapped(mappingModel))
                {
                    res = true;
                    SetParrents(mappingModel);
                    FillTarget(mappingModel);
                }
            }
            return res;
        }
        public void Save(string hotkeysDir, MappingJsonFileModel newModel)
        {
            if (!ValidateNoDuplicates(newModel) || !ValidateEscapeKeyMapped(newModel))
            { throw new ApplicationException($"{nameof(KeyMapper)}{nameof(Save)}:HotKeys validation error"); }
            
            SetParrents(newModel);
            FillTarget(newModel);
            FileInfo fileInfo = new FileInfo(Path.Combine(hotkeysDir, _mappingFileName));
            using StreamWriter sW = new StreamWriter(fileInfo.OpenWrite());
            sW.Write(JsonConvert.SerializeObject(newModel, Formatting.Indented));
            sW.Flush();
        }
        public void GenerateDefaultFile(string hotkeysDir)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(Path.Combine(hotkeysDir, _mappingFileName));
                if (!fileInfo.Exists)
                {
                    MappingJsonFileModel model = GetDefaultModel();

                    using StreamWriter sW = new StreamWriter(fileInfo.OpenWrite());
                    sW.Write(JsonConvert.SerializeObject(model, Formatting.Indented));
                    sW.Flush();
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
        public bool GetMappedTarget(List<Keys> key, out MappedHotKey target)
        {
            target = FindTarget(key);
            return target != _notFound;
        }
        public List<KeyMappingTarget> GetAllTargets()
        {
            return _keyMapping.Select(x => x.Parent).Distinct().ToList();
        }
        public bool GetTargetMethodIdByFriendlyName(string name, out int id)
        {
            id = -1;
            id = _keyMapping.FirstOrDefault(x => x.Parent.FriendlyName == name).Parent.TargetMethodId;
            return id != 0;
        }
        public void RestoreDefaults()
        {
            FillTarget(GetDefaultModel());
        }
        protected virtual MappedHotKey FindTarget(List<Keys> keys)
        {
            MappedHotKey res = _notFound;
            foreach (var item in _keyMapping)
            {
                if (item.KeyCode.SequenceEqual(keys))
                {
                    res = item;
                    break;                    
                }
            }
            return res;
        }
        /// <summary>
        /// Check model for duplicate target id or same key sequense
        /// </summary>
        /// <param name="mappingModel"></param>
        /// <returns></returns>
        protected virtual bool ValidateNoDuplicates(MappingJsonFileModel mappingModel)
        {
            bool res = true;
            for (int i = 0; i < mappingModel.HotKeys.Count; i++)
            {
                if (res)
                {
                    for (int j = 0; j < mappingModel.HotKeys.Count; j++)
                    {
                        if (i != j && mappingModel.HotKeys[i].Equals(mappingModel.HotKeys[j]))
                        {
                            res = false;
                            break;
                        }
                    }
                }
            }
            return res;
        }
        protected virtual bool ValidateEscapeKeyMapped(MappingJsonFileModel mappingModel)
        {
            return true;
        }
        protected virtual void FillTarget(MappingJsonFileModel mappingModel)
        {
            _keyMapping = mappingModel.HotKeys.SelectMany(x => x.MappedHotKeys).ToList();
        }
        protected virtual MappingJsonFileModel GetDefaultModel()
        {
            var res = new MappingJsonFileModel()
            {
                FormatVersion = _CurrentCodeFormatVersion,
            };
            var item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup0.ToString(), TargetMethodId = 1 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup1.ToString(), TargetMethodId = 2 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup2.ToString(), TargetMethodId = 3 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup3.ToString(), TargetMethodId = 4 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup4.ToString(), TargetMethodId = 5 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup5.ToString(), TargetMethodId = 6 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup6.ToString(), TargetMethodId = 7 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup7.ToString(), TargetMethodId = 8 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup8.ToString(), TargetMethodId = 9 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup9.ToString(), TargetMethodId = 10 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup0.ToString(), TargetMethodId = 11 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup1.ToString(), TargetMethodId = 12 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup2.ToString(), TargetMethodId = 13 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup3.ToString(), TargetMethodId = 14 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup4.ToString(), TargetMethodId = 15 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup5.ToString(), TargetMethodId = 16 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup6.ToString(), TargetMethodId = 17 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup7.ToString(), TargetMethodId = 18 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup8.ToString(), TargetMethodId = 19 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SetControlGroup9.ToString(), TargetMethodId = 20 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup0WithFocus.ToString(), TargetMethodId = 21 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup1WithFocus.ToString(), TargetMethodId = 22 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup2WithFocus.ToString(), TargetMethodId = 23 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup3WithFocus.ToString(), TargetMethodId = 24 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup4WithFocus.ToString(), TargetMethodId = 25 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup5WithFocus.ToString(), TargetMethodId = 26 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup6WithFocus.ToString(), TargetMethodId = 27 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup7WithFocus.ToString(), TargetMethodId = 28 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup8WithFocus.ToString(), TargetMethodId = 29 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.SelectControlGroup9WithFocus.ToString(), TargetMethodId = 30 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleBasesForward.ToString(), TargetMethodId = 31 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleBasesBackward.ToString(), TargetMethodId = 32 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleBasesForwardWithFocus.ToString(), TargetMethodId = 33 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleBasesBackwardWithFocus.ToString(), TargetMethodId = 34 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonyOrExplorerForward.ToString(), TargetMethodId = 35 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonyOrExplorerBackward.ToString(), TargetMethodId = 36 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonyOrExplorerForwardWithFocus.ToString(), TargetMethodId = 37 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonyOrExplorerBackwardWithFocus.ToString(), TargetMethodId = 38 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonySelectionForward.ToString(), TargetMethodId = 39 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonySelectionBackward.ToString(), TargetMethodId = 40 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonySelectionForwardWithFocus.ToString(), TargetMethodId = 41 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleColonySelectionBackwardWithFocus.ToString(), TargetMethodId = 42 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleConstractionShipForward.ToString(), TargetMethodId = 43 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleConstractionShipBackward.ToString(), TargetMethodId = 44 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleConstractionShipForwardWithFocus.ToString(), TargetMethodId = 45 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleConstractionShipBackwardWithFocus.ToString(), TargetMethodId = 46 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleFleetForward.ToString(), TargetMethodId = 47 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleFleetBackward.ToString(), TargetMethodId = 48 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleFleetForwardWithFocus.ToString(), TargetMethodId = 49 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleFleetBackwardWithFocus.ToString(), TargetMethodId = 50 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleIdleShipForward.ToString(), TargetMethodId = 51 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleIdleShipBackward.ToString(), TargetMethodId = 52 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleIdleShipForwardWithFocus.ToString(), TargetMethodId = 53 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleIdleShipBackwardWithFocus.ToString(), TargetMethodId = 54 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleMainDisplayTypes.ToString(), TargetMethodId = 55 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleMilitaryShipForward.ToString(), TargetMethodId = 56 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleMilitaryShipBackward.ToString(), TargetMethodId = 57 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleMilitaryShipForwardWithFocus.ToString(), TargetMethodId = 58 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleMilitaryShipBackwardWithFocus.ToString(), TargetMethodId = 59 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CyclePanelVisibility.ToString(), TargetMethodId = 60 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.T } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleSelectionBackward.ToString(), TargetMethodId = 61 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.B } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleSelectionForward.ToString(), TargetMethodId = 62 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.N } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.CycleShipEngagmentRange.ToString(), TargetMethodId = 63 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Oemcomma } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.DecreaseGameSpeed.ToString(), TargetMethodId = 64 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.OemMinus } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.EnableAuto.ToString(), TargetMethodId = 65 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.A } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.FindNearestMilitaryShip.ToString(), TargetMethodId = 66 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Z } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.IncreaseGameSpeed.ToString(), TargetMethodId = 67 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Oemplus } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenBuildOrderScreen.ToString(), TargetMethodId = 68 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenColoniesScreen.ToString(), TargetMethodId = 69 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenConstructionYardsScreen.ToString(), TargetMethodId = 70 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F10 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenDesignsScreen.ToString(), TargetMethodId = 71 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenDiplomacyScreen.ToString(), TargetMethodId = 72 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenEmpireComparison.ToString(), TargetMethodId = 73 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.V } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenEmpireSummaryScreen.ToString(), TargetMethodId = 74 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenExpansionPlannerScreen.ToString(), TargetMethodId = 75 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenFleetScreen.ToString(), TargetMethodId = 76 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F12 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenGalaxyMap.ToString(), TargetMethodId = 77 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.G } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenGroundInvasionStatusScreen.ToString(), TargetMethodId = 78 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.OemOpenBrackets } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenHelp.ToString(), TargetMethodId = 79 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenIntelligenceAgentsScreen.ToString(), TargetMethodId = 80 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenMessageHistory.ToString(), TargetMethodId = 81 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.H } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenOptions.ToString(), TargetMethodId = 82 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.O } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenResearchScreen.ToString(), TargetMethodId = 83 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenShipAndBasesScreen.ToString(), TargetMethodId = 84 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F11 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.RefuelShip.ToString(), TargetMethodId = 85 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ShipEscapeCommand.ToString(), TargetMethodId = 86 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.E } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.StopShip.ToString(), TargetMethodId = 87 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.S } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ToggleViewLock.ToString(), TargetMethodId = 88 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.L } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ToglePause.ToString(), TargetMethodId = 89 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Space } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ZoomInMaximumLevel.ToString(), TargetMethodId = 90 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Home } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ZoomToGalaxyLevel.ToString(), TargetMethodId = 91 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.End } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ZoomToSectorLevel.ToString(), TargetMethodId = 92 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Delete } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ZoomToSelected.ToString(), TargetMethodId = 93 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Back } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.ZoomToSystemLevel.ToString(), TargetMethodId = 94 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Insert } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.Escape.ToString(), TargetMethodId = 95 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Escape } });
            res.HotKeys.Add(item);

            return res;
        }

        protected void SetParrents(MappingJsonFileModel mappingModel)
        {
            mappingModel.HotKeys.ForEach(x => x.MappedHotKeys.ForEach(y => y.Parent = x));
        }
    }

    public class ExpKeyMapper : KeyMapper
    {
        public ExpKeyMapper(string mappingFileName) : base(mappingFileName)
        {
        }

        protected override MappingJsonFileModel GetDefaultModel()
        {
            var res = new MappingJsonFileModel()
            {
                FormatVersion = _CurrentCodeFormatVersion,
            };
            var item = new KeyMappingTarget() { FriendlyName = KeyMappingFriendlyNames.OpenConstractionQueueEditor.ToString(), TargetMethodId = 1 };
            item.MappedHotKeys = new List<MappedHotKey>() { new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.J } } };
            res.HotKeys.Add(item);
            return res;
        }
        protected override bool ValidateEscapeKeyMapped(MappingJsonFileModel mappingModel)
        {
            return true;
        }
    }
}
