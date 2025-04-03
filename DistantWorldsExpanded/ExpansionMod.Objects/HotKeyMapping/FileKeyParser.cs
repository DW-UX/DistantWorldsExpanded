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
        public const int _CurrentCodeFormatVersion = 3;
        private const int _TargetCollectionVersion = 1;

        protected string _mappingFileName;
        protected List<MappedHotKey> _keyMapping = new List<MappedHotKey>();
        protected MappedHotKey _notFound;
        protected bool _reSaveNeeded = false;

        public KeyMapper(string mappingFileName)
        {
            this._mappingFileName = mappingFileName;
            KeyMappingTarget notFoundTarget = new KeyMappingTarget() { KeyTarget =  (KeyMappingFriendlyNames)(-1), MappedHotKeys = new List<MappedHotKey>(), TargetMethodId = -1 };
            _notFound = new MappedHotKey(notFoundTarget);
        }

        public virtual bool MapKeys(string hotkeysDir)
        {
            bool res = false;
            FileInfo fileInfo = new FileInfo(Path.Combine(hotkeysDir, _mappingFileName));
            if (fileInfo.Exists)
            {
                MappingJsonFileModel mappingModel;
                using (StreamReader sR = new StreamReader(fileInfo.OpenRead()))
                { mappingModel = GetModelFromFile(sR); }
                RemoveKeysFromOlderVersions(mappingModel);
                RemapAndRenameOlderVersions(mappingModel);
                if (_reSaveNeeded)
                {
                    Save(hotkeysDir, mappingModel);
                }
                if (ValidateNoDuplicates(mappingModel) & ValidateEscapeKeyMapped(mappingModel))
                {
                    res = true;
                    SetParrents(mappingModel);
                    FillTarget(mappingModel);
                }
            }
            return res;
        }
        public virtual void Save(string hotkeysDir, MappingJsonFileModel newModel)
        {
            if (!ValidateNoDuplicates(newModel) || !ValidateEscapeKeyMapped(newModel))
            { throw new ApplicationException($"{nameof(KeyMapper)}{nameof(Save)}:HotKeys validation error"); }

            SetParrents(newModel);
            FillTarget(newModel);
            using (FileStream fs = new FileStream(Path.Combine(hotkeysDir, _mappingFileName), FileMode.Truncate, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter sW = new StreamWriter(fs))
                {
                    sW.Write(JsonConvert.SerializeObject(newModel, Formatting.Indented));
                    sW.Flush();
                }
            }
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
                MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK);
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
            var res = _keyMapping.FirstOrDefault(x => x.Parent.FriendlyName == name)?.Parent.TargetMethodId;
            if (res.HasValue)
            { id = res.Value; }
            return id != 0;
        }
        public void RestoreDefaults()
        {
            FillTarget(GetDefaultModel());
        }
        protected virtual MappingJsonFileModel GetModelFromFile(StreamReader sR)
        {
            return JsonConvert.DeserializeObject<MappingJsonFileModel>(sR.ReadToEnd());
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
            //_keyMapping = mappingModel.HotKeys.SelectMany(x => x.MappedHotKeys).ToList();
            _keyMapping.Clear();
            foreach (var item in mappingModel.HotKeys)
            {
                if (item.MappedHotKeys.Count != 0)
                { _keyMapping.AddRange(item.MappedHotKeys); }
                else
                {
                    _keyMapping.Add(new MappedHotKey(item));
                }
            }
        }
        protected virtual void RemoveKeysFromOlderVersions(MappingJsonFileModel mappingModel)
        {
            var defaultModel = GetDefaultModel();
            if (mappingModel.HotKeys.RemoveAll(x => !defaultModel.HotKeys.Any(y => y.TargetMethodId == x.TargetMethodId)) > 0)
            { _reSaveNeeded = true; }
        }
        protected virtual void RemapAndRenameOlderVersions(MappingJsonFileModel mappingModel)
        {
            
        }
        protected virtual MappingJsonFileModel GetDefaultModel()
        {
            var res = new MappingJsonFileModel()
            {
                FormatVersion = _CurrentCodeFormatVersion,
                TargetCollectionVersion = _TargetCollectionVersion,
            };
            var item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup0, TargetMethodId = 1 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup1, TargetMethodId = 2 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup2, TargetMethodId = 3 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup3, TargetMethodId = 4 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup4, TargetMethodId = 5 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup5, TargetMethodId = 6 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup6, TargetMethodId = 7 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup7, TargetMethodId = 8 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup8, TargetMethodId = 9 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup9, TargetMethodId = 10 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup0, TargetMethodId = 11 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup1, TargetMethodId = 12 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup2, TargetMethodId = 13 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup3, TargetMethodId = 14 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup4, TargetMethodId = 15 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup5, TargetMethodId = 16 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup6, TargetMethodId = 17 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup7, TargetMethodId = 18 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup8, TargetMethodId = 19 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetControlGroup9, TargetMethodId = 20 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup0WithFocus, TargetMethodId = 21 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D0 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup1WithFocus, TargetMethodId = 22 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup2WithFocus, TargetMethodId = 23 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup3WithFocus, TargetMethodId = 24 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup4WithFocus, TargetMethodId = 25 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup5WithFocus, TargetMethodId = 26 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup6WithFocus, TargetMethodId = 27 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup7WithFocus, TargetMethodId = 28 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup8WithFocus, TargetMethodId = 29 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SelectControlGroup9WithFocus, TargetMethodId = 30 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.D9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleBasesForward, TargetMethodId = 31 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleBasesBackward, TargetMethodId = 32 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleBasesForwardWithFocus, TargetMethodId = 33 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleBasesBackwardWithFocus, TargetMethodId = 34 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonyOrExplorerForward, TargetMethodId = 35 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonyOrExplorerBackward, TargetMethodId = 36 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonyOrExplorerForwardWithFocus, TargetMethodId = 37 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonyOrExplorerBackwardWithFocus, TargetMethodId = 38 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.X } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonySelectionForward, TargetMethodId = 39 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonySelectionBackward, TargetMethodId = 40 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonySelectionForwardWithFocus, TargetMethodId = 41 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleColonySelectionBackwardWithFocus, TargetMethodId = 42 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.C } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleConstractionShipForward, TargetMethodId = 43 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleConstractionShipBackward, TargetMethodId = 44 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleConstractionShipForwardWithFocus, TargetMethodId = 45 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleConstractionShipBackwardWithFocus, TargetMethodId = 46 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.Y } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleFleetForward, TargetMethodId = 47 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleFleetBackward, TargetMethodId = 48 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleFleetForwardWithFocus, TargetMethodId = 49 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleFleetBackwardWithFocus, TargetMethodId = 50 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleIdleShipForward, TargetMethodId = 51 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleIdleShipBackward, TargetMethodId = 52 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleIdleShipForwardWithFocus, TargetMethodId = 53 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleIdleShipBackwardWithFocus, TargetMethodId = 54 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.I } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleMainDisplayTypes, TargetMethodId = 55 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.D } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleMilitaryShipForward, TargetMethodId = 56 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleMilitaryShipBackward, TargetMethodId = 57 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleMilitaryShipForwardWithFocus, TargetMethodId = 58 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleMilitaryShipBackwardWithFocus, TargetMethodId = 59 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.ShiftKey, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CyclePanelVisibility, TargetMethodId = 60 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.T } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleSelectionBackward, TargetMethodId = 61 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.B } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleSelectionForward, TargetMethodId = 62 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.N } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleShipEngagmentRange, TargetMethodId = 63 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Oemcomma } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.DecreaseGameSpeed, TargetMethodId = 64 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.OemMinus } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.EnableAuto, TargetMethodId = 65 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.A } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.FindNearestMilitaryShip, TargetMethodId = 66 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Z } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.IncreaseGameSpeed, TargetMethodId = 67 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Oemplus } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenBuildOrderScreen, TargetMethodId = 68 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F8 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenColoniesScreen, TargetMethodId = 69 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F2 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenConstructionYardsScreen, TargetMethodId = 70 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F10 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenDesignsScreen, TargetMethodId = 71 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F9 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenDiplomacyScreen, TargetMethodId = 72 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenEmpireComparison, TargetMethodId = 73 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.V } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenEmpireSummaryScreen, TargetMethodId = 74 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenExpansionPlannerScreen, TargetMethodId = 75 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenFleetScreen, TargetMethodId = 76 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F12 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenGalaxyMap, TargetMethodId = 77 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.G } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenGroundInvasionStatusScreen, TargetMethodId = 78 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.OemOpenBrackets } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenHelp, TargetMethodId = 79 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F1 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenIntelligenceAgentsScreen, TargetMethodId = 80 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenMessageHistory, TargetMethodId = 81 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.H } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenOptions, TargetMethodId = 82 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.O } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenResearchScreen, TargetMethodId = 83 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenShipAndBasesScreen, TargetMethodId = 84 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.F11 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.RefuelShip, TargetMethodId = 85 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShipEscapeCommand, TargetMethodId = 86 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.E } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.StopShip, TargetMethodId = 87 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.S } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ToggleViewLock, TargetMethodId = 88 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.L } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ToglePause, TargetMethodId = 89 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Space } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ZoomInMaximumLevel, TargetMethodId = 90 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Home } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ZoomToGalaxyLevel, TargetMethodId = 91 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.End } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ZoomToSectorLevel, TargetMethodId = 92 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Delete } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ZoomToSelected, TargetMethodId = 93 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Back } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ZoomToSystemLevel, TargetMethodId = 94 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Insert } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.Escape, TargetMethodId = 95 };
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
            var item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OpenConstractionQueueEditor, TargetMethodId = 1 };
            item.MappedHotKeys = new List<MappedHotKey>() { new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.J } } };
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewUp, TargetMethodId = 2 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Up } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewDown, TargetMethodId = 3 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Down } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewLeft, TargetMethodId = 4 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Left } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewRight, TargetMethodId = 5 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Right } });
            res.HotKeys.Add(item);
            return res;
        }
        protected override void RemapAndRenameOlderVersions(MappingJsonFileModel mappingModel)
        {
            if(mappingModel.FormatVersion == 2)
            {
                mappingModel.FormatVersion = 3;

                var item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewUp, TargetMethodId = 2 };
                item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Up } });
                mappingModel.HotKeys.Add(item);
                item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewDown, TargetMethodId = 3 };
                item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Down } });
                mappingModel.HotKeys.Add(item);
                item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewLeft, TargetMethodId = 4 };
                item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Left } });
                mappingModel.HotKeys.Add(item);
                item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.MoveViewRight, TargetMethodId = 5 };
                item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Right } });
                mappingModel.HotKeys.Add(item);
                _reSaveNeeded = true;
            }    
        }
        protected override bool ValidateEscapeKeyMapped(MappingJsonFileModel mappingModel)
        {
            return true;
        }
    }
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
                    { temp.KeyTarget = defaultItem.KeyTarget; }
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
            var item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowDetailedInfo, TargetMethodId = 1 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Return } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowMissionCommand, TargetMethodId = 2 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.Return } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.AssignCargoMission, TargetMethodId = 5 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D3 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.SetFighterTarget, TargetMethodId = 6 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D4 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OrderBombersToAttack, TargetMethodId = 7 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D5 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.OrderBombersToAttackAll, TargetMethodId = 8 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D6 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.TransferFighter, TargetMethodId = 9 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.D7 } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ToggleAutomateCarrierOps, TargetMethodId = 10 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.A } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowCustomBomberForm, TargetMethodId = 11 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.B } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CalculateDistance, TargetMethodId = 12 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.D } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShipFinder, TargetMethodId = 13 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.E } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.FixExplorerCurrentSystem, TargetMethodId = 14 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.F } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.AssignMiningShipToTarget, TargetMethodId = 15 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.M } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowPrisonForm, TargetMethodId = 16 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.P } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.IncreaseDockingBayCapacity, TargetMethodId = 17 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.Q } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowDamagedComponents, TargetMethodId = 18 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.RevealIfPirate, TargetMethodId = 19 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.RushStateShips, TargetMethodId = 20 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ShiftKey, Keys.R } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ShowStats, TargetMethodId = 21 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.S } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.AddShipToTradeList, TargetMethodId = 22 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.T } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ForceUnloadAtDestination, TargetMethodId = 23 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.U } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.AssignPassengershipMission, TargetMethodId = 24 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.ControlKey, Keys.W } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleSelectedByRoleBackward, TargetMethodId = 25 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemSemicolon } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.GetParentCarrier, TargetMethodId = 26 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemQuestion } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.CycleSelectedByRoleForward, TargetMethodId = 27 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.Menu, Keys.OemQuotes } });
            res.HotKeys.Add(item);
            item = new KeyMappingTarget() { KeyTarget = KeyMappingFriendlyNames.ToggleShipAutoBaconImpl, TargetMethodId = 28 };
            item.MappedHotKeys.Add(new MappedHotKey(item) { KeyCode = new List<Keys>() { Keys.A } });
            res.HotKeys.Add(item);
            return res;
        }
    }

}
