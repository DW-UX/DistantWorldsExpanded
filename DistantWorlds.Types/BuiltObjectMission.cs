// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectMission
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class BuiltObjectMission : ISerializable
    {
        [NonSerialized]
        public object _LockObject = new object();

        public BuiltObject _BuiltObject;

        public Galaxy _Galaxy;

        private CommandQueue _Commands = new CommandQueue();

        private StellarObject _MissionTargetStellarObject;

        private ShipGroup _MissionTargetShipGroup;

        private StellarObject _MissionTargetStellarObject2;

        private ShipGroup _MissionTargetShipGroup2;

        [NonSerialized]
        private bool _MissionTargetIsBuiltObject;

        [NonSerialized]
        private bool _MissionTarget2IsBuiltObject;

        private CargoList _MissionCargo;

        private Design _MissionDesign;

        private TroopList _MissionTroopList;

        private PopulationList _MissionPopulationList;

        private float _MissionXCoord = -2.00001E+09f;

        private float _MissionYCoord = -2.00001E+09f;

        private BuiltObjectMissionPriority _MissionPriority;

        private long _StarDate;

        private bool _ManuallyAssigned;

        private bool _RepeatCommands;

        private bool _IsShipGroupMission;

        public BuiltObjectMissionType Type;

        public BuiltObjectMissionType PreviousType;

        private Sector _TargetSector;

        public bool IsShipGroupMission
        {
            get
            {
                return _IsShipGroupMission;
            }
            set
            {
                _IsShipGroupMission = value;
            }
        }

        public bool ManuallyAssigned
        {
            get
            {
                return _ManuallyAssigned;
            }
            set
            {
                _ManuallyAssigned = value;
            }
        }

        public bool RepeatCommands
        {
            get
            {
                return _RepeatCommands;
            }
            set
            {
                _RepeatCommands = value;
            }
        }

        public CargoList Cargo => _MissionCargo;

        public TroopList Troops => _MissionTroopList;

        public PopulationList Population => _MissionPopulationList;

        public Design Design => _MissionDesign;

        public float X => _MissionXCoord;

        public float Y => _MissionYCoord;

        public long StarDate => _StarDate;

        public BuiltObjectMissionPriority Priority => _MissionPriority;

        public object Target
        {
            get
            {
                if (_MissionTargetStellarObject != null)
                {
                    return _MissionTargetStellarObject;
                }
                if (_MissionTargetShipGroup != null)
                {
                    return _MissionTargetShipGroup;
                }
                return null;
            }
        }

        public object SecondaryTarget
        {
            get
            {
                if (_MissionTargetStellarObject2 != null)
                {
                    return _MissionTargetStellarObject2;
                }
                if (_MissionTargetShipGroup2 != null)
                {
                    return _MissionTargetShipGroup2;
                }
                return null;
            }
        }

        public Sector TargetSector => _TargetSector;

        public BuiltObject TargetBuiltObject
        {
            get
            {
                if (_MissionTargetIsBuiltObject)
                {
                    return (BuiltObject)_MissionTargetStellarObject;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject = value;
                _MissionTargetIsBuiltObject = true;
                _MissionTargetShipGroup = null;
                _MissionXCoord = -2.00001E+09f;
                _MissionYCoord = -2.00001E+09f;
            }
        }

        public Habitat TargetHabitat
        {
            get
            {
                if (_MissionTargetStellarObject is Habitat)
                {
                    return (Habitat)_MissionTargetStellarObject;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject = value;
                _MissionTargetIsBuiltObject = false;
                _MissionTargetShipGroup = null;
                _MissionXCoord = -2.00001E+09f;
                _MissionYCoord = -2.00001E+09f;
            }
        }

        public Creature TargetCreature
        {
            get
            {
                if (_MissionTargetStellarObject is Creature)
                {
                    return (Creature)_MissionTargetStellarObject;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject = value;
                _MissionTargetIsBuiltObject = false;
                _MissionTargetShipGroup = null;
                _MissionXCoord = -2.00001E+09f;
                _MissionYCoord = -2.00001E+09f;
            }
        }

        public ShipGroup TargetShipGroup
        {
            get
            {
                return _MissionTargetShipGroup;
            }
            set
            {
                _MissionTargetStellarObject = null;
                _MissionTargetIsBuiltObject = false;
                _MissionTargetShipGroup = value;
                _MissionXCoord = -2.00001E+09f;
                _MissionYCoord = -2.00001E+09f;
            }
        }

        public BuiltObject SecondaryTargetBuiltObject
        {
            get
            {
                if (_MissionTarget2IsBuiltObject)
                {
                    return (BuiltObject)_MissionTargetStellarObject2;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject2 = value;
                _MissionTarget2IsBuiltObject = true;
                _MissionTargetShipGroup2 = null;
            }
        }

        public Habitat SecondaryTargetHabitat
        {
            get
            {
                if (_MissionTargetStellarObject2 is Habitat)
                {
                    return (Habitat)_MissionTargetStellarObject2;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject2 = value;
                _MissionTarget2IsBuiltObject = false;
                _MissionTargetShipGroup2 = null;
            }
        }

        public Creature SecondaryTargetCreature
        {
            get
            {
                if (_MissionTargetStellarObject2 is Creature)
                {
                    return (Creature)_MissionTargetStellarObject2;
                }
                return null;
            }
            set
            {
                _MissionTargetStellarObject2 = value;
                _MissionTarget2IsBuiltObject = false;
                _MissionTargetShipGroup2 = null;
            }
        }

        public ShipGroup SecondaryTargetShipGroup
        {
            get
            {
                return _MissionTargetShipGroup2;
            }
            set
            {
                _MissionTargetStellarObject2 = null;
                _MissionTarget2IsBuiltObject = false;
                _MissionTargetShipGroup2 = value;
            }
        }

        public BuiltObjectMission(SerializationInfo info, StreamingContext context)
        {
            byte[] buffer = (byte[])info.GetValue("D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                _MissionXCoord = binaryReader.ReadSingle();
                _MissionYCoord = binaryReader.ReadSingle();
                _MissionPriority = (BuiltObjectMissionPriority)binaryReader.ReadByte();
                _StarDate = binaryReader.ReadInt64();
                _ManuallyAssigned = binaryReader.ReadBoolean();
                _RepeatCommands = binaryReader.ReadBoolean();
                _IsShipGroupMission = binaryReader.ReadBoolean();
                Type = (BuiltObjectMissionType)binaryReader.ReadByte();
                PreviousType = (BuiltObjectMissionType)binaryReader.ReadByte();
                int num = binaryReader.ReadInt16();
                int num2 = binaryReader.ReadInt16();
                if (num >= 0 && num2 >= 0)
                {
                    _TargetSector = new Sector(num, num2);
                }
                binaryReader.Close();
            }
            _BuiltObject = (BuiltObject)info.GetValue("BO", typeof(BuiltObject));
            _Galaxy = (Galaxy)info.GetValue("Gx", typeof(Galaxy));
            _Commands = (CommandQueue)info.GetValue("Cmds", typeof(CommandQueue));
            _MissionTargetStellarObject = (StellarObject)info.GetValue("TStO", typeof(StellarObject));
            if (_MissionTargetStellarObject != null && _MissionTargetStellarObject is BuiltObject)
            {
                _MissionTargetIsBuiltObject = true;
            }
            _MissionTargetShipGroup = (ShipGroup)info.GetValue("TSG", typeof(ShipGroup));
            _MissionTargetStellarObject2 = (StellarObject)info.GetValue("TStO2", typeof(StellarObject));
            if (_MissionTargetStellarObject2 != null && _MissionTargetStellarObject2 is BuiltObject)
            {
                _MissionTarget2IsBuiltObject = true;
            }
            _MissionTargetShipGroup2 = (ShipGroup)info.GetValue("TSG2", typeof(ShipGroup));
            _MissionCargo = (CargoList)info.GetValue("Cg", typeof(CargoList));
            _MissionDesign = (Design)info.GetValue("Ds", typeof(Design));
            _MissionTroopList = (TroopList)info.GetValue("TL", typeof(TroopList));
            _MissionPopulationList = (PopulationList)info.GetValue("PL", typeof(PopulationList));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(_MissionXCoord);
                binaryWriter.Write(_MissionYCoord);
                binaryWriter.Write((byte)_MissionPriority);
                binaryWriter.Write(_StarDate);
                binaryWriter.Write(_ManuallyAssigned);
                binaryWriter.Write(_RepeatCommands);
                binaryWriter.Write(_IsShipGroupMission);
                binaryWriter.Write((byte)Type);
                binaryWriter.Write((byte)PreviousType);
                if (_TargetSector != null)
                {
                    binaryWriter.Write((short)_TargetSector.X);
                    binaryWriter.Write((short)_TargetSector.Y);
                }
                else
                {
                    binaryWriter.Write((short)(-1));
                    binaryWriter.Write((short)(-1));
                }
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("D", memoryStream.ToArray());
            }
            info.AddValue("BO", _BuiltObject);
            info.AddValue("Gx", _Galaxy);
            info.AddValue("Cmds", _Commands);
            info.AddValue("TStO", _MissionTargetStellarObject);
            info.AddValue("TSG", _MissionTargetShipGroup);
            info.AddValue("TStO2", _MissionTargetStellarObject2);
            info.AddValue("TSG2", _MissionTargetShipGroup2);
            info.AddValue("Cg", _MissionCargo);
            info.AddValue("Ds", _MissionDesign);
            info.AddValue("TL", _MissionTroopList);
            info.AddValue("PL", _MissionPopulationList);
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, long starDate, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, null, null, -2000000001.0, -2000000001.0, starDate, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, Design design, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, null, design, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, Design design, double x, double y, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, null, design, x, y, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, cargo, null, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, TroopList troops, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, troops, null, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, double x, double y, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, null, null, x, y, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, Design design, double x, double y, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, cargo, null, null, design, x, y, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, PopulationList population, BuiltObjectMissionPriority priority)
            : this(galaxy, builtObject, missionType, target, target2, null, null, population, null, -2000000001.0, -2000000001.0, -1L, priority, allowReprocessing: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing)
            : this(galaxy, builtObject, missionType, target, target2, cargo, troops, population, design, x, y, starDate, priority, allowReprocessing, allowBuiltObjectChanges: true)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing, bool allowBuiltObjectChanges)
            : this(galaxy, builtObject, missionType, target, target2, cargo, troops, population, design, x, y, starDate, priority, allowReprocessing, allowBuiltObjectChanges, specifiedAsFleetMission: false)
        {
        }

        public BuiltObjectMission(Galaxy galaxy, BuiltObject builtObject, BuiltObjectMissionType missionType, object target, object target2, CargoList cargo, TroopList troops, PopulationList population, Design design, double x, double y, long starDate, BuiltObjectMissionPriority priority, bool allowReprocessing, bool allowBuiltObjectChanges, bool specifiedAsFleetMission)
        {
            _Galaxy = galaxy;
            _BuiltObject = builtObject;
            if (target != null)
            {
                if (target is BuiltObject)
                {
                    TargetBuiltObject = (BuiltObject)target;
                }
                else if (target is Habitat)
                {
                    TargetHabitat = (Habitat)target;
                }
                else if (target is Creature)
                {
                    TargetCreature = (Creature)target;
                }
                else if (target is ShipGroup)
                {
                    TargetShipGroup = (ShipGroup)target;
                }
                else if (target is Sector)
                {
                    _TargetSector = (Sector)target;
                }
            }
            else
            {
                _MissionTargetStellarObject = null;
                _MissionTargetShipGroup = null;
            }
            if (target2 != null)
            {
                if (target2 is BuiltObject)
                {
                    SecondaryTargetBuiltObject = (BuiltObject)target2;
                }
                else if (target2 is Habitat)
                {
                    SecondaryTargetHabitat = (Habitat)target2;
                }
                else if (target2 is Creature)
                {
                    SecondaryTargetCreature = (Creature)target2;
                }
                else if (target2 is ShipGroup)
                {
                    SecondaryTargetShipGroup = (ShipGroup)target2;
                }
            }
            else
            {
                _MissionTargetStellarObject2 = null;
                _MissionTargetShipGroup2 = null;
            }
            _MissionCargo = cargo;
            _MissionTroopList = troops;
            _MissionPopulationList = population;
            if (_MissionPopulationList != null)
            {
                _MissionPopulationList.RecalculateTotalAmount();
            }
            _MissionDesign = design;
            _MissionXCoord = (float)x;
            _MissionYCoord = (float)y;
            if (x <= -2000000000.0)
            {
                _MissionXCoord = -2.00001E+09f;
            }
            if (y <= -2000000000.0)
            {
                _MissionYCoord = -2.00001E+09f;
            }
            _StarDate = starDate;
            _MissionPriority = priority;
            Type = missionType;
            bool couldResolveCommands = false;
            _Commands = ResolveCommandsForMission(this, allowReprocessing, specifiedAsFleetMission, out couldResolveCommands);
            BaconBuiltObjectMission.LoadMoreCargo(this, builtObject);
            if (!couldResolveCommands)
            {
                if (_BuiltObject != null && allowBuiltObjectChanges)
                {
                    _BuiltObject.ClearPreviousMissionRequirements();
                }
                Clear();
                return;
            }
            if (builtObject.Role != BuiltObjectRole.Base && allowBuiltObjectChanges)
            {
                _BuiltObject.ParentBuiltObject = null;
                _BuiltObject.ParentHabitat = null;
                _BuiltObject.ParentOffsetX = -2000000001.0;
                _BuiltObject.ParentOffsetY = -2000000001.0;
            }
            _RepeatCommands = false;
        }

        public static Empire ResolveMissionTargetEmpire(BuiltObjectMission mission)
        {
            Empire result = null;
            if (mission.TargetBuiltObject != null)
            {
                BuiltObject targetBuiltObject = mission.TargetBuiltObject;
                result = targetBuiltObject.Empire;
            }
            else if (mission.TargetHabitat != null)
            {
                Habitat targetHabitat = mission.TargetHabitat;
                result = targetHabitat.Empire;
            }
            else if (mission.TargetShipGroup != null)
            {
                ShipGroup targetShipGroup = mission.TargetShipGroup;
                result = targetShipGroup.Empire;
            }
            return result;
        }

        public static Empire ResolveMissionSecondaryTargetEmpire(BuiltObjectMission mission)
        {
            Empire result = null;
            if (mission.SecondaryTargetBuiltObject != null)
            {
                BuiltObject secondaryTargetBuiltObject = mission.SecondaryTargetBuiltObject;
                result = secondaryTargetBuiltObject.Empire;
            }
            else if (mission.SecondaryTargetHabitat != null)
            {
                Habitat secondaryTargetHabitat = mission.SecondaryTargetHabitat;
                result = secondaryTargetHabitat.Empire;
            }
            else if (mission.SecondaryTargetShipGroup != null)
            {
                ShipGroup secondaryTargetShipGroup = mission.SecondaryTargetShipGroup;
                result = secondaryTargetShipGroup.Empire;
            }
            return result;
        }

        public BuiltObjectMission Clone()
        {
            BuiltObjectMission builtObjectMission = new BuiltObjectMission(_Galaxy, _BuiltObject, Type, Target, SecondaryTarget, Cargo, Troops, Population, Design, X, Y, StarDate, Priority, allowReprocessing: true, allowBuiltObjectChanges: false);
            builtObjectMission.SetTargetSector(TargetSector);
            builtObjectMission.ReplaceCommandStack(_Commands);
            builtObjectMission.RepeatCommands = _RepeatCommands;
            return builtObjectMission;
        }

        public void Clear()
        {
            if (_Commands != null)
            {
                lock (_LockObject)
                {
                    _Commands.Clear();
                }
            }
            Type = BuiltObjectMissionType.Undefined;
            _MissionPriority = BuiltObjectMissionPriority.Undefined;
            TargetBuiltObject = null;
            SecondaryTargetBuiltObject = null;
            _MissionDesign = null;
            _MissionCargo = null;
            _MissionTroopList = new TroopList();
            _MissionPopulationList = new PopulationList();
            _RepeatCommands = false;
            _StarDate = 0L;
            _MissionXCoord = -2.00001E+09f;
            _MissionYCoord = -2.00001E+09f;
            _ManuallyAssigned = false;
            _RepeatCommands = false;
        }

        public bool CompleteCommandIfMatchesAction(CommandAction matchAction)
        {
            lock (_LockObject)
            {
                bool result = false;
                if (_Commands.Count > 0)
                {
                    Command[] array = _Commands.ToArray();
                    Command command = null;
                    if (array.Length > 0)
                    {
                        command = array[0];
                    }
                    if (command != null && command.Action == matchAction)
                    {
                        _Commands.Dequeue();
                        result = true;
                    }
                }
                if (_Commands.Count == 0)
                {
                    PreviousType = Type;
                    Type = BuiltObjectMissionType.Undefined;
                }
                return result;
            }
        }

        public void CompleteCommand()
        {
            CompleteCommand(ignoreRepeatCommands: false);
        }

        public void CompleteCommand(bool ignoreRepeatCommands)
        {
            lock (_LockObject)
            {
                if (_Commands.Count > 0)
                {
                    Command item = _Commands.Dequeue();
                    if (!ignoreRepeatCommands && _RepeatCommands)
                    {
                        _Commands.Enqueue(item);
                    }
                }
                if (_Commands.Count == 0)
                {
                    PreviousType = Type;
                    Type = BuiltObjectMissionType.Undefined;
                }
            }
        }

        public Command FastPeekCurrentCommand()
        {
            lock (_LockObject)
            {
                if (_Commands != null && _Commands.Count > 0)
                {
                    return _Commands.Peek();
                }
            }
            return null;
        }

        public Command ShowCurrentCommand()
        {
            lock (_LockObject)
            {
                if (_Commands == null)
                {
                    return null;
                }
                if (_Commands.Count <= 0)
                {
                    return null;
                }
                Command[] array = _Commands.ToArray();
                if (array.Length > 0)
                {
                    return array[0];
                }
            }
            return null;
        }

        public Command ShowNextCommand()
        {
            lock (_LockObject)
            {
                Command[] array = new Command[0];
                if (_Commands != null)
                {
                    array = _Commands.ToArray();
                }
                if (array.Length > 1)
                {
                    return array[1];
                }
                return null;
            }
        }

        public Command[] ShowAllCommands()
        {
            lock (_LockObject)
            {
                Command[] result = new Command[0];
                if (_Commands != null)
                {
                    result = _Commands.ToArray();
                }
                return result;
            }
        }

        public void InsertCommandAtTop(Command command)
        {
            lock (_LockObject)
            {
                if (_Commands == null)
                {
                    _Commands = new CommandQueue();
                }
                lock (_LockObject)
                {
                    Command[] array = _Commands.ToArray();
                    _Commands.Clear();
                    _Commands.Enqueue(command);
                    for (int i = 0; i < array.Length; i++)
                    {
                        _Commands.Enqueue(array[i]);
                    }
                }
            }
        }

        public void AddCommandToEnd(Command command)
        {
            lock (_LockObject)
            {
                if (_Commands == null)
                {
                    _Commands = new CommandQueue();
                }
                _Commands.Enqueue(command);
            }
        }

        public void ReplaceCommandStack(CommandQueue commands)
        {
            lock (_LockObject)
            {
                _Commands = commands;
            }
        }

        public bool CheckCommandsPastPrimaryTarget(BuiltObject primaryTarget)
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if (command != null && command.TargetBuiltObject == primaryTarget)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool CheckCommandsForAction(CommandAction action)
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if (command != null && command.Action == action)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckCommandsForAction(CommandAction action, int maxSteps)
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    int num = 0;
                    foreach (Command command in _Commands)
                    {
                        num++;
                        if (num <= maxSteps)
                        {
                            if (command != null && command.Action == action)
                            {
                                return true;
                            }
                            continue;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        public Command GetNextDockCommand()
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if (command != null && command.Action == CommandAction.Undock)
                        {
                            return command;
                        }
                    }
                }
            }
            return null;
        }

        public bool CheckCommandsForUndock()
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if (command != null && command.Action == CommandAction.Undock)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckCommandsForHyperjump()
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if (command != null && command.Action == CommandAction.HyperTo)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public bool CheckCommandsForHyperjumpOrConditionalJump()
        {
            lock (_LockObject)
            {
                if (_Commands != null)
                {
                    foreach (Command command in _Commands)
                    {
                        if ((command != null && command.Action == CommandAction.HyperTo) || command.Action == CommandAction.ConditionalHyperTo)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void EnsureCoordsInGalaxy(ref double x, ref double y)
        {
            x = Math.Max(0.0, Math.Min((double)Galaxy.SizeX - 1.0, x));
            y = Math.Max(0.0, Math.Min((double)Galaxy.SizeY - 1.0, y));
        }

        public Point ResolveTargetCoordinatesCurrentCommand()
        {
            Command command = FastPeekCurrentCommand();
            if (command != null)
            {
                double x = -1.0;
                double y = -1.0;
                if (command.TargetBuiltObject != null)
                {
                    x = command.TargetBuiltObject.Xpos;
                    y = command.TargetBuiltObject.Ypos;
                }
                else if (command.TargetHabitat != null)
                {
                    x = command.TargetHabitat.Xpos;
                    y = command.TargetHabitat.Ypos;
                }
                else if (command.TargetCreature != null)
                {
                    x = command.TargetCreature.Xpos;
                    y = command.TargetCreature.Ypos;
                }
                else if (command.TargetShipGroup != null)
                {
                    if (command.TargetShipGroup.LeadShip != null)
                    {
                        x = command.TargetShipGroup.LeadShip.Xpos;
                        y = command.TargetShipGroup.LeadShip.Ypos;
                    }
                }
                else if ((double)command.Xpos > -2000000001.0 && (double)command.Ypos > -2000000001.0)
                {
                    x = command.Xpos;
                    y = command.Ypos;
                }
                EnsureCoordsInGalaxy(ref x, ref y);
                return new Point((int)x, (int)y);
            }
            return Point.Empty;
        }

        public StellarObject ResolveMissionTargetHabitatIfPossible()
        {
            StellarObject result = null;
            Habitat targetHabitat = TargetHabitat;
            BuiltObject targetBuiltObject = TargetBuiltObject;
            Creature targetCreature = TargetCreature;
            ShipGroup targetShipGroup = TargetShipGroup;
            if (targetHabitat != null)
            {
                result = targetHabitat;
            }
            else if (targetBuiltObject != null)
            {
                result = ((targetBuiltObject.ParentHabitat != null) ? targetBuiltObject.ParentHabitat : ((targetBuiltObject.NearestSystemStar == null) ? ((StellarObject)targetBuiltObject) : ((StellarObject)targetBuiltObject.NearestSystemStar)));
            }
            else if (targetCreature != null)
            {
                result = ((targetCreature.ParentHabitat != null) ? targetCreature.ParentHabitat : ((targetCreature.NearestSystemStar == null) ? ((StellarObject)targetCreature) : ((StellarObject)targetCreature.NearestSystemStar)));
            }
            else if (targetShipGroup != null)
            {
                BuiltObject leadShip = targetShipGroup.LeadShip;
                if (leadShip != null && leadShip.ParentHabitat != null)
                {
                    result = leadShip.ParentHabitat;
                }
                else if (leadShip != null && leadShip.NearestSystemStar != null)
                {
                    result = leadShip.NearestSystemStar;
                }
            }
            return result;
        }

        public Point ResolveTargetCoordinates(BuiltObjectMission mission)
        {
            double x = -1.0;
            double y = -1.0;
            if (_MissionTargetStellarObject != null)
            {
                x = _MissionTargetStellarObject.Xpos;
                y = _MissionTargetStellarObject.Ypos;
            }
            else if (_MissionTargetShipGroup != null)
            {
                if (_MissionTargetShipGroup.LeadShip != null)
                {
                    x = _MissionTargetShipGroup.LeadShip.Xpos;
                    y = _MissionTargetShipGroup.LeadShip.Ypos;
                }
            }
            else if (mission.X > -2E+09f && mission.Y > -2E+09f)
            {
                x = mission.X;
                y = mission.Y;
            }
            EnsureCoordsInGalaxy(ref x, ref y);
            return new Point((int)x, (int)y);
        }

        public Command GenerateParkCommand()
        {
            Command command = new Command(CommandAction.MoveTo);
            _Galaxy.SelectRelativeParkingPoint(out var x, out var y);
            command.TargetRelativeXpos = (float)x;
            command.TargetRelativeYpos = (float)y;
            return command;
        }

        private CommandQueue ResolveCommandsForMission(BuiltObjectMission mission, bool allowReprocessing, bool specifiedAsFleetMission, out bool couldResolveCommands)
        {
            return BaconBuiltObjectMission.ResolveCommandsForMission(this, mission, allowReprocessing, specifiedAsFleetMission, out couldResolveCommands);
        }

        public void SetTargetSector(Sector sector)
        {
            _TargetSector = sector;
        }
    }

}