// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Creature
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Creature :
      StellarObject,
      IComparable,
      IComparable<StellarObject>,
      IComparable<Creature>,
      IComparable<BuiltObject>,
      ISerializable
    {
        public object _LockObject = new object();
        private Galaxy _Galaxy;
        private int _CreatureID;
        private int _AttackStrength;
        private int _PictureRef;
        private int _MaxSize;
        private double _Damage;
        private int _DamageKillThreshhold;
        private CreatureType _Type;
        private float _TurnRate;
        private float _AccelerationRate;
        private float _HealRate;
        private long _BirthDate;
        private Habitat _AnchorHabitat;
        private Point _AnchorPoint;
        private int _AnchorRange;
        private int _AttackRange;
        private Habitat _NearestSystemStar;
        private int _MovementSpeed;
        private int _MovementSpeedBase;
        private int _HyperSpeed;
        private long _HyperCountdown;
        private bool _CanHide;
        private bool _IsBenign;
        private int _LungeSpeed;
        private int _LungeSpeedBase;
        private double _LungeLength;
        private double _LungeAccelerationRate;
        private double _LungeInterval;
        private DateTime _LastLunge;
        private double _ParentOffsetX;
        private double _ParentOffsetY;
        private bool _LocationLocked;
        private double _LastPositionX;
        private double _LastPositionY;
        private double _ParentX;
        private double _ParentY;
        private float _CurrentHeading;
        private TurnDirection _TurnDirection;
        private float _TargetSpeed;
        private double _LastDistance;
        private double _DistanceToTarget;
        private bool _IsVisible;
        private bool _IsAttacking;
        private double _ReproductionCounter;
        private bool _MovementSlowedLocation;
        private bool _HyperjumpDisabledLocation;
        private float _CreaturePullAmountLocation;
        private float _CreaturePullAngleLocation;
        private float _CreatureDamageAmountLocation;
        private DateTime _LastTouch;
        private DateTime _LastShortTouch;
        private DateTime _LastPeriodicTouch;
        private DateTime _LastLongTouch;
        [NonSerialized]
        public bool PromptSystemCheck;
        [NonSerialized]
        private GalaxyLocationList _Locations = new GalaxyLocationList();

        public Creature()
        {
        }

        public Creature(SerializationInfo info, StreamingContext context)
          : base(info, context)
        {
            using (MemoryStream input = new MemoryStream((byte[])info.GetValue("Cr_D", typeof(byte[]))))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)input))
                {
                    this._CreatureID = binaryReader.ReadInt32();
                    this._AttackStrength = binaryReader.ReadInt32();
                    this._PictureRef = binaryReader.ReadInt32();
                    this._MaxSize = binaryReader.ReadInt32();
                    this._Damage = (double)binaryReader.ReadSingle();
                    this._DamageKillThreshhold = binaryReader.ReadInt32();
                    this._Type = (CreatureType)binaryReader.ReadByte();
                    this._TurnRate = binaryReader.ReadSingle();
                    this._AccelerationRate = binaryReader.ReadSingle();
                    this._HealRate = binaryReader.ReadSingle();
                    this._BirthDate = binaryReader.ReadInt64();
                    this._AnchorRange = binaryReader.ReadInt32();
                    this._AttackRange = binaryReader.ReadInt32();
                    this._MovementSpeed = (int)binaryReader.ReadInt16();
                    this._MovementSpeedBase = (int)binaryReader.ReadInt16();
                    this._HyperSpeed = binaryReader.ReadInt32();
                    this._HyperCountdown = binaryReader.ReadInt64();
                    this._CanHide = binaryReader.ReadBoolean();
                    this._IsBenign = binaryReader.ReadBoolean();
                    this._LungeSpeed = (int)binaryReader.ReadInt16();
                    this._LungeSpeedBase = (int)binaryReader.ReadInt16();
                    this._LungeLength = (double)binaryReader.ReadSingle();
                    this._LungeAccelerationRate = (double)binaryReader.ReadSingle();
                    this._LungeInterval = (double)binaryReader.ReadSingle();
                    this._ParentOffsetX = binaryReader.ReadDouble();
                    this._ParentOffsetY = binaryReader.ReadDouble();
                    this._LocationLocked = binaryReader.ReadBoolean();
                    this._LastPositionX = binaryReader.ReadDouble();
                    this._LastPositionY = binaryReader.ReadDouble();
                    this._ParentX = binaryReader.ReadDouble();
                    this._ParentY = binaryReader.ReadDouble();
                    this._CurrentHeading = binaryReader.ReadSingle();
                    this._TurnDirection = (TurnDirection)binaryReader.ReadByte();
                    this._TargetSpeed = binaryReader.ReadSingle();
                    this._LastDistance = binaryReader.ReadDouble();
                    this._DistanceToTarget = binaryReader.ReadDouble();
                    this._IsVisible = binaryReader.ReadBoolean();
                    this._IsAttacking = binaryReader.ReadBoolean();
                    this._ReproductionCounter = (double)binaryReader.ReadSingle();
                    this._MovementSlowedLocation = binaryReader.ReadBoolean();
                    this._HyperjumpDisabledLocation = binaryReader.ReadBoolean();
                    this._CreaturePullAmountLocation = binaryReader.ReadSingle();
                    this._CreaturePullAngleLocation = binaryReader.ReadSingle();
                    this._CreatureDamageAmountLocation = binaryReader.ReadSingle();
                    this._LastTouch = new DateTime(binaryReader.ReadInt64());
                    this._LastShortTouch = new DateTime(binaryReader.ReadInt64());
                    this._LastPeriodicTouch = new DateTime(binaryReader.ReadInt64());
                    this._LastLongTouch = new DateTime(binaryReader.ReadInt64());
                    this._LastLunge = new DateTime(binaryReader.ReadInt64());
                    binaryReader.Close();
                }
            }
            this._Galaxy = (Galaxy)info.GetValue("Gx", typeof(Galaxy));
            this._AnchorHabitat = (Habitat)info.GetValue("AnH", typeof(Habitat));
            this._AnchorPoint = (Point)info.GetValue("AnP", typeof(Point));
            this._NearestSystemStar = (Habitat)info.GetValue("NSS", typeof(Habitat));
        }

        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            using (MemoryStream output = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)output))
                {
                    binaryWriter.Write(this._CreatureID);
                    binaryWriter.Write(this._AttackStrength);
                    binaryWriter.Write(this._PictureRef);
                    binaryWriter.Write(this._MaxSize);
                    binaryWriter.Write((float)this._Damage);
                    binaryWriter.Write(this._DamageKillThreshhold);
                    binaryWriter.Write((byte)this._Type);
                    binaryWriter.Write(this._TurnRate);
                    binaryWriter.Write(this._AccelerationRate);
                    binaryWriter.Write(this._HealRate);
                    binaryWriter.Write(this._BirthDate);
                    binaryWriter.Write(this._AnchorRange);
                    binaryWriter.Write(this._AttackRange);
                    binaryWriter.Write((short)this._MovementSpeed);
                    binaryWriter.Write((short)this._MovementSpeedBase);
                    binaryWriter.Write(this._HyperSpeed);
                    binaryWriter.Write(this._HyperCountdown);
                    binaryWriter.Write(this._CanHide);
                    binaryWriter.Write(this._IsBenign);
                    binaryWriter.Write((short)this._LungeSpeed);
                    binaryWriter.Write((short)this._LungeSpeedBase);
                    binaryWriter.Write((float)this._LungeLength);
                    binaryWriter.Write((float)this._LungeAccelerationRate);
                    binaryWriter.Write((float)this._LungeInterval);
                    binaryWriter.Write(this._ParentOffsetX);
                    binaryWriter.Write(this._ParentOffsetY);
                    binaryWriter.Write(this._LocationLocked);
                    binaryWriter.Write(this._LastPositionX);
                    binaryWriter.Write(this._LastPositionY);
                    binaryWriter.Write(this._ParentX);
                    binaryWriter.Write(this._ParentY);
                    binaryWriter.Write(this._CurrentHeading);
                    binaryWriter.Write((byte)this._TurnDirection);
                    binaryWriter.Write(this._TargetSpeed);
                    binaryWriter.Write(this._LastDistance);
                    binaryWriter.Write(this._DistanceToTarget);
                    binaryWriter.Write(this._IsVisible);
                    binaryWriter.Write(this._IsAttacking);
                    binaryWriter.Write((float)this._ReproductionCounter);
                    binaryWriter.Write(this._MovementSlowedLocation);
                    binaryWriter.Write(this._HyperjumpDisabledLocation);
                    binaryWriter.Write(this._CreaturePullAmountLocation);
                    binaryWriter.Write(this._CreaturePullAngleLocation);
                    binaryWriter.Write(this._CreatureDamageAmountLocation);
                    binaryWriter.Write(this._LastTouch.Ticks);
                    binaryWriter.Write(this._LastShortTouch.Ticks);
                    binaryWriter.Write(this._LastPeriodicTouch.Ticks);
                    binaryWriter.Write(this._LastLongTouch.Ticks);
                    binaryWriter.Write(this._LastLunge.Ticks);
                    binaryWriter.Flush();
                    binaryWriter.Close();
                    info.AddValue("Cr_D", (object)output.ToArray());
                }
            }
            info.AddValue("Gx", (object)this._Galaxy);
            info.AddValue("AnH", (object)this._AnchorHabitat);
            info.AddValue("AnP", (object)this._AnchorPoint);
            info.AddValue("NSS", (object)this._NearestSystemStar);
        }

        public override int CargoSpace => 0;

        public override int TroopCapacityRemaining => 0;

        public bool IsVisible
        {
            get => this._IsVisible;
            set => this._IsVisible = value;
        }

        public int CreatureID => this._CreatureID;

        public CreatureType Type
        {
            get => this._Type;
            set => this._Type = value;
        }

        public int PictureRef
        {
            get => this._PictureRef;
            set => this._PictureRef = value;
        }

        public int AttackRange => this._AttackRange;

        public int MaxSize
        {
            get => this._MaxSize;
            set => this._MaxSize = value;
        }

        public double Damage
        {
            get => this._Damage;
            set => this._Damage = value;
        }

        public int DamageKillThreshhold
        {
            get => this._DamageKillThreshhold;
            set => this._DamageKillThreshhold = value;
        }

        public int AttackStrength
        {
            get => this._AttackStrength;
            set => this._AttackStrength = value;
        }

        public double DistanceToTarget => this._DistanceToTarget;

        public Habitat NearestSystemStar
        {
            get => this._NearestSystemStar;
            set => this._NearestSystemStar = value;
        }

        public void SetMovementSpeed(int speed) => this._MovementSpeed = speed;

        public int MovementSpeed => this._MovementSpeed;

        public float CurrentHeading
        {
            get => this._CurrentHeading;
            set => this._CurrentHeading = value;
        }

        public bool LocationLocked
        {
            get => this._LocationLocked;
            set => this._LocationLocked = value;
        }

        public bool CanHide => this._CanHide;

        public bool IsBenign => this._IsBenign;

        public Creature(Galaxy galaxy, CreatureType type, Habitat startingHabitat)
          : this(galaxy, type, startingHabitat, -2000000001, -2000000001, Point.Empty, 500)
        {
        }

        public Creature(
          Galaxy galaxy,
          CreatureType type,
          Habitat startingHabitat,
          int offsetX,
          int offsetY)
          : this(galaxy, type, startingHabitat, offsetX, offsetY, Point.Empty, 500)
        {
        }

        public Creature(
          Galaxy galaxy,
          CreatureType type,
          Habitat startingHabitat,
          int offsetX,
          int offsetY,
          Point startingPoint,
          int anchorRange)
        {
            this._Galaxy = galaxy;
            this._Type = type;
            this._CreatureID = this._Galaxy.GetNextCreatureID();
            this.Attackers = new StellarObjectList();
            this.Pursuers = new StellarObjectList();
            this._BirthDate = this._Galaxy.CurrentStarDate;
            this._CurrentHeading = this._Galaxy.SelectRandomHeading();
            this.TargetHeading = this._CurrentHeading;
            this.CurrentSpeed = 0.0f;
            this._TargetSpeed = 0.0f;
            this._TurnDirection = TurnDirection.StraightAhead;
            if (offsetX > -2000000001 && offsetY > -2000000001)
            {
                this._ParentOffsetX = (double)offsetX;
                this._ParentOffsetY = (double)offsetY;
            }
            else
                this._Galaxy.SelectRelativeHabitatSurfacePoint(startingHabitat, out this._ParentOffsetX, out this._ParentOffsetY);
            this._ParentX = this._ParentOffsetX;
            this._ParentY = this._ParentOffsetY;
            if (startingHabitat != null)
            {
                this.Xpos = startingHabitat.Xpos + this._ParentOffsetX;
                this.Ypos = startingHabitat.Ypos + this._ParentOffsetY;
            }
            else if (startingPoint != Point.Empty)
            {
                this.Xpos = (double)startingPoint.X + this._ParentOffsetX;
                this.Ypos = (double)startingPoint.Y + this._ParentOffsetY;
            }
            this._Damage = 0.0;
            this._IsAttacking = false;
            this._ReproductionCounter = 0.0;
            this._IsBenign = false;
            this._AnchorPoint = startingPoint;
            this._AnchorRange = anchorRange;
            int seconds = Galaxy.Rnd.Next(1, 30);
            this._LastLongTouch = this._Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, seconds));
            this._LastPeriodicTouch = this._Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, seconds));
            this._LastShortTouch = this._Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, seconds));
            this._LastTouch = this._Galaxy.CurrentDateTime.Subtract(new TimeSpan(0, 0, seconds));
            switch (this._Type)
            {
                case CreatureType.Kaltor:
                    this._AnchorHabitat = startingHabitat;
                    if (this._AnchorHabitat != null)
                    {
                        this._AnchorRange = this._AnchorHabitat.Category != HabitatCategoryType.Asteroid ? 600 : 1200;
                        this.ParentHabitat = this._AnchorHabitat;
                        this._ParentOffsetX = this.Xpos - this.ParentHabitat.Xpos;
                        this._ParentOffsetY = this.Ypos - this.ParentHabitat.Ypos;
                    }
                    this._AccelerationRate = 7f;
                    this._AttackRange = this._AnchorRange;
                    this.Size = Galaxy.Rnd.Next(80, 190);
                    this._MaxSize = 600;
                    this._AttackStrength = (int)((double)this.Size / 20.0);
                    this._TurnRate = 1.9f;
                    this._DamageKillThreshhold = (int)((double)this.Size * 1.4);
                    this._HealRate = 0.2f;
                    this._MovementSpeed = 34;
                    this._IsVisible = true;
                    this._CanHide = false;
                    this._LungeSpeed = 0;
                    this._LungeLength = 0.0;
                    this._LungeAccelerationRate = 0.0;
                    this._LungeInterval = 100.0;
                    this._LastLunge = this._LastTouch;
                    this.Name = this.SelectName(startingHabitat);
                    this._PictureRef = 2;
                    break;
                case CreatureType.RockSpaceSlug:
                case CreatureType.DesertSpaceSlug:
                    this._AnchorHabitat = startingHabitat;
                    if (this._AnchorHabitat != null)
                    {
                        this._AnchorRange = this._AnchorHabitat.Category != HabitatCategoryType.Asteroid ? (int)((double)this._AnchorHabitat.Diameter / 1.3) : 400;
                        this.ParentHabitat = this._AnchorHabitat;
                        this._ParentOffsetX = this.Xpos - this.ParentHabitat.Xpos;
                        this._ParentOffsetY = this.Ypos - this.ParentHabitat.Ypos;
                    }
                    this._AccelerationRate = 2f;
                    this._AttackRange = this._AnchorRange;
                    this.Size = Galaxy.Rnd.Next(120, 180);
                    this._MaxSize = 350;
                    this._AttackStrength = (int)((double)this.Size / 30.0);
                    this._TurnRate = 1.5f;
                    this._DamageKillThreshhold = (int)((double)this.Size * 1.1);
                    this._HealRate = 0.1f;
                    this._MovementSpeed = 7;
                    this._IsVisible = false;
                    this._CanHide = true;
                    this._LungeSpeed = 20;
                    this._LungeLength = 2.3;
                    this._LungeAccelerationRate = 25.0;
                    this._LungeInterval = 12.0;
                    this._LastLunge = this._LastTouch;
                    this.Name = this.SelectName(startingHabitat);
                    this._PictureRef = 0;
                    break;
                case CreatureType.Ardilus:
                    this._AnchorHabitat = startingHabitat;
                    if (this._AnchorHabitat != null)
                    {
                        this._AnchorRange = 800;
                        this.ParentHabitat = this._AnchorHabitat;
                        this._ParentOffsetX = this.Xpos - this.ParentHabitat.Xpos;
                        this._ParentOffsetY = this.Ypos - this.ParentHabitat.Ypos;
                    }
                    this._AccelerationRate = 2f;
                    this._AttackRange = 40000;
                    this.Size = Galaxy.Rnd.Next(210, 390);
                    this._MaxSize = 1500;
                    this._AttackStrength = (int)((double)this.Size / 40.0);
                    this._TurnRate = 1.4f;
                    this._DamageKillThreshhold = (int)((double)this.Size * 2.2);
                    this._HealRate = 0.3f;
                    this._MovementSpeed = 9;
                    this._HyperSpeed = 10000;
                    this._IsVisible = true;
                    this._CanHide = false;
                    this._LungeSpeed = 38;
                    this._LungeLength = 2.1;
                    this._LungeAccelerationRate = 82.0;
                    this._LungeInterval = 14.0;
                    this._LastLunge = this._LastTouch;
                    this.Name = this.SelectName(startingHabitat);
                    this._PictureRef = 3;
                    break;
                case CreatureType.SilverMist:
                    this._AnchorHabitat = startingHabitat;
                    this._AnchorRange = 1500;
                    if (this._AnchorHabitat != null)
                    {
                        this._AnchorRange = 1500;
                        this.ParentHabitat = this._AnchorHabitat;
                        this._ParentOffsetX = this.Xpos - this.ParentHabitat.Xpos;
                        this._ParentOffsetY = this.Ypos - this.ParentHabitat.Ypos;
                    }
                    this._AccelerationRate = 5f;
                    this._AttackRange = 50000;
                    this.Size = Galaxy.Rnd.Next(220, 280);
                    this._MaxSize = 1500;
                    this._AttackStrength = (int)((double)this.Size / 10.0);
                    this._TurnRate = 1.9f;
                    this._DamageKillThreshhold = (int)((double)this.Size * 3.0);
                    this._HealRate = 0.4f;
                    this._MovementSpeed = 27;
                    this._HyperSpeed = 5000;
                    this._IsVisible = true;
                    this._CanHide = false;
                    this._LungeSpeed = 72;
                    this._LungeLength = 6.5;
                    this._LungeAccelerationRate = 138.0;
                    this._LungeInterval = 16.0;
                    this._LastLunge = this._LastTouch;
                    this.Name = this.SelectName(startingHabitat);
                    this._PictureRef = 4;
                    break;
            }
            this._MovementSpeedBase = this._MovementSpeed;
            this._LungeSpeedBase = this._LungeSpeed;
            if (this._Type != CreatureType.DesertSpaceSlug)
                return;
            this._PictureRef = 1;
            this.Size = Galaxy.Rnd.Next(150, 220);
            this._MaxSize = 550;
            this._AttackStrength = (int)((double)this.Size / 12.0);
            this._TurnRate = 1.7f;
            this._DamageKillThreshhold = (int)((double)this.Size * 1.2);
        }

        private string SelectName(Habitat startingHabitat)
        {
            string str = Galaxy.ResolveDescription(this._Type);
            if (startingHabitat != null)
            {
                switch (this._Type)
                {
                    case CreatureType.Kaltor:
                        str = string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)Galaxy.DetermineHabitatSystemStar(startingHabitat).Name);
                        break;
                    case CreatureType.RockSpaceSlug:
                        str = startingHabitat.Category != HabitatCategoryType.Asteroid ? string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)Galaxy.DetermineHabitatSystemStar(startingHabitat).Name) : string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)(Galaxy.DetermineHabitatSystemStar(startingHabitat).Name + " " + TextResolver.GetText("Asteroid Field")));
                        break;
                    case CreatureType.DesertSpaceSlug:
                        str = string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)Galaxy.DetermineHabitatSystemStar(startingHabitat).Name);
                        break;
                    case CreatureType.Ardilus:
                        str = string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)Galaxy.DetermineHabitatSystemStar(startingHabitat).Name);
                        break;
                    case CreatureType.SilverMist:
                        str = string.Format(TextResolver.GetText("CREATURETYPE of LOCATION"), (object)str, (object)Galaxy.DetermineHabitatSystemStar(startingHabitat).Name);
                        break;
                }
            }
            return str;
        }

        private void Heal(double timePassed)
        {
            if (this._Damage > 0.0)
                this._Damage -= (double)this._HealRate * timePassed;
            if (this._Damage >= 0.0)
                return;
            this._Damage = 0.0;
        }

        public void DoTasks() => this.DoTasks(this._Galaxy.CurrentDateTime);

        public void DoTasks(DateTime time)
        {
            double timePassed = (double)time.Subtract(this._LastTouch).Ticks / 10000000.0;
            TimeSpan timeSpan1 = time.Subtract(this._LastShortTouch);
            double totalSeconds1 = timeSpan1.TotalSeconds;
            TimeSpan timeSpan2 = time.Subtract(this._LastPeriodicTouch);
            double totalSeconds2 = timeSpan2.TotalSeconds;
            TimeSpan timeSpan3 = time.Subtract(this._LastLongTouch);
            double totalSeconds3 = timeSpan3.TotalSeconds;
            this.Move(timePassed, time);
            this.DoLocationEffects(timePassed);
            if (timeSpan1.TotalSeconds >= 3.0)
            {
                this.CheckForAttackers();
                this.AttackTarget(totalSeconds1, time);
                this.Heal(totalSeconds1);
                this.ApplyLocationEffects(totalSeconds1);
                this._LastShortTouch = time;
            }
            if (timeSpan2.TotalSeconds >= 10.0)
            {
                if (this._IsVisible)
                    this.CheckForTargets();
                if (this.HasBeenDestroyed)
                    this.CompleteTeardown();
                this.CheckFixNotInSystem();
                this._LastPeriodicTouch = time;
            }
            if (timeSpan3.TotalSeconds >= 30.0)
            {
                this.Split();
                this.ChooseAction();
                this.Reproduce(totalSeconds3);
                this.Attackers.Clear();
                this._LastLongTouch = time;
            }
            this._LastTouch = time;
        }

        private void CheckFixNotInSystem()
        {
            if ((double)this.CurrentSpeed > (double)this.MovementSpeed)
                return;
            if (this.PromptSystemCheck)
            {
                Habitat nearestSystem = this._Galaxy.FastFindNearestSystem(this.Xpos, this.Ypos);
                if (nearestSystem != this._NearestSystemStar)
                {
                    if (this._NearestSystemStar != null)
                    {
                        SystemInfo bySystemIndex = this._Galaxy.Systems.GetBySystemIndex(this._NearestSystemStar.SystemIndex);
                        if (bySystemIndex != null && bySystemIndex.Creatures != null && bySystemIndex.Creatures.Contains(this))
                            bySystemIndex.Creatures.Remove(this);
                    }
                    if (this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, nearestSystem.Xpos, nearestSystem.Ypos) < 25000.0)
                        this._NearestSystemStar = nearestSystem;
                    if (this._NearestSystemStar != null)
                    {
                        SystemInfo bySystemIndex = this._Galaxy.Systems.GetBySystemIndex(this._NearestSystemStar.SystemIndex);
                        if (bySystemIndex != null && bySystemIndex.Creatures != null && !bySystemIndex.Creatures.Contains(this))
                            bySystemIndex.Creatures.Add(this);
                        if (this.ParentHabitat != null && this.ParentHabitat.SystemIndex != this._NearestSystemStar.SystemIndex)
                            this.ParentHabitat = (Habitat)null;
                        if (this._AnchorHabitat != null && this._AnchorHabitat.SystemIndex != this._NearestSystemStar.SystemIndex)
                            this._AnchorHabitat = (Habitat)null;
                    }
                }
                this.PromptSystemCheck = false;
            }
            if (this.NearestSystemStar != null)
            {
                SystemInfo bySystemIndex = this._Galaxy.Systems.GetBySystemIndex(this.NearestSystemStar.SystemIndex);
                if (bySystemIndex != null && !bySystemIndex.Creatures.Contains(this))
                {
                    bySystemIndex.Creatures.Add(this);
                    if (bySystemIndex.SystemStar != null)
                    {
                        this.NearestSystemStar = bySystemIndex.SystemStar;
                        if (this.ParentHabitat != null && this.ParentHabitat.SystemIndex != this._NearestSystemStar.SystemIndex)
                            this.ParentHabitat = (Habitat)null;
                        if (this._AnchorHabitat != null && this._AnchorHabitat.SystemIndex != this._NearestSystemStar.SystemIndex)
                            this._AnchorHabitat = (Habitat)null;
                    }
                }
            }
            if (this.ParentHabitat != null)
            {
                SystemInfo bySystemIndex = this._Galaxy.Systems.GetBySystemIndex(this.ParentHabitat.SystemIndex);
                if (bySystemIndex != null && !bySystemIndex.Creatures.Contains(this))
                {
                    bySystemIndex.Creatures.Add(this);
                    if (bySystemIndex.SystemStar != null)
                    {
                        this.NearestSystemStar = bySystemIndex.SystemStar;
                        if (this.ParentHabitat.SystemIndex != this.NearestSystemStar.SystemIndex)
                            this.ParentHabitat = (Habitat)null;
                        if (this._AnchorHabitat.SystemIndex != this.NearestSystemStar.SystemIndex)
                            this._AnchorHabitat = (Habitat)null;
                    }
                }
            }
            if (this._AnchorHabitat == null)
                return;
            SystemInfo bySystemIndex1 = this._Galaxy.Systems.GetBySystemIndex(this._AnchorHabitat.SystemIndex);
            if (bySystemIndex1 == null || bySystemIndex1.Creatures.Contains(this))
                return;
            bySystemIndex1.Creatures.Add(this);
            if (bySystemIndex1.SystemStar == null)
                return;
            this.NearestSystemStar = bySystemIndex1.SystemStar;
            if (this.ParentHabitat.SystemIndex != this.NearestSystemStar.SystemIndex)
                this.ParentHabitat = (Habitat)null;
            if (this._AnchorHabitat.SystemIndex == this.NearestSystemStar.SystemIndex)
                return;
            this._AnchorHabitat = (Habitat)null;
        }

        private void ChooseAction()
        {
            if (this.CurrentTarget != null || (double)this.CurrentSpeed > 0.0 || (double)this._TargetSpeed > 0.0)
                return;
            int num = Galaxy.Rnd.Next(0, 6);
            if (this._Type == CreatureType.SilverMist)
                num = Galaxy.Rnd.Next(0, 3);
            switch (num)
            {
                case 0:
                case 1:
                    this.InitiateWander();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    this.HideCreature();
                    break;
            }
        }

        private double ReduceAngle(double currentangle)
        {
            if (currentangle >= Math.PI)
                currentangle -= 2.0 * Math.PI;
            return currentangle;
        }

        private double IncreaseAngle(double currentangle)
        {
            if (currentangle <= -1.0 * Math.PI)
                currentangle += 2.0 * Math.PI;
            return currentangle;
        }

        private void CalculateCurrentHeading(double timePassed)
        {
            if ((double)this._CurrentHeading == (double)this.TargetHeading)
                return;
            double num1 = (double)this._TurnRate * timePassed;
            double num2 = (double)this.TargetHeading - (double)this._CurrentHeading;
            if (num2 < 0.0 && num2 > -1.0 * Math.PI || num2 >= Math.PI && num2 < 2.0 * Math.PI)
            {
                if (Math.Abs(num2) < Math.Abs(num1))
                {
                    this._CurrentHeading = this.TargetHeading;
                    this._TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    this._CurrentHeading -= (float)num1;
                    this._TurnDirection = TurnDirection.Left;
                }
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit((double)this._CurrentHeading <= -1.0 * Math.PI, 20, ref iterationCount))
                    this._CurrentHeading = (float)this.IncreaseAngle((double)this._CurrentHeading);
            }
            else
            {
                if (Math.Abs(num2) < Math.Abs(num1))
                {
                    this._CurrentHeading = this.TargetHeading;
                    this._TurnDirection = TurnDirection.StraightAhead;
                }
                else
                {
                    this._CurrentHeading += (float)num1;
                    this._TurnDirection = TurnDirection.Right;
                }
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit((double)this._CurrentHeading >= Math.PI, 20, ref iterationCount))
                    this._CurrentHeading = (float)this.ReduceAngle((double)this._CurrentHeading);
            }
        }

        private void AccelerateToTargetSpeed(double timePassed, double accelerationRate)
        {
            if ((double)this._TargetSpeed > (double)this.CurrentSpeed)
            {
                double num = accelerationRate * timePassed;
                if ((double)this.CurrentSpeed + num >= (double)this._TargetSpeed)
                    this.CurrentSpeed = this._TargetSpeed;
                else
                    this.CurrentSpeed += (float)num;
            }
            else if ((double)this._TargetSpeed < (double)this.CurrentSpeed)
            {
                double num = Math.Max(1.0, accelerationRate) * timePassed;
                if ((double)this.CurrentSpeed - num < (double)this._TargetSpeed)
                    this.CurrentSpeed = this._TargetSpeed;
                else
                    this.CurrentSpeed -= (float)num;
            }
            if ((double)this.CurrentSpeed >= 0.0)
                return;
            this.CurrentSpeed = 0.0f;
        }

        private void HideCreature()
        {
            if (!this._CanHide)
                return;
            this._IsVisible = false;
        }

        private void ShowCreature() => this._IsVisible = true;

        private void InitiateWander()
        {
            if (this._AnchorHabitat != null && this.CurrentTarget == null)
            {
                this.ShowCreature();
                if (this._Type == CreatureType.Kaltor && !this._LocationLocked)
                {
                    if (Galaxy.Rnd.Next(0, 2) == 1)
                    {
                        Habitat habitat = (Habitat)null;
                        double num = 536870911.0;
                        for (int index = 0; num > 8000.0 && index < 20; ++index)
                        {
                            if (this._Galaxy.Systems[this._AnchorHabitat.SystemIndex].Habitats.Count > 0)
                            {
                                habitat = this._Galaxy.Systems[this._AnchorHabitat.SystemIndex].Habitats[Galaxy.Rnd.Next(0, this._Galaxy.Systems[this._AnchorHabitat.SystemIndex].Habitats.Count)];
                                num = this._Galaxy.CalculateDistance(habitat.Xpos, habitat.Ypos, this._AnchorHabitat.Xpos, this._AnchorHabitat.Ypos);
                            }
                        }
                        if (habitat != null && habitat != this._AnchorHabitat)
                            this._AnchorHabitat = habitat;
                    }
                }
                else if (this._Type == CreatureType.SilverMist && !this._LocationLocked)
                {
                    Habitat habitat1 = (Habitat)null;
                    SystemInfoDistanceList orderedSystemList = this._Galaxy.GenerateDistanceOrderedSystemList(this.Xpos, this.Ypos);
                    for (int index1 = 0; habitat1 == null && index1 < 20; ++index1)
                    {
                        int index2 = Galaxy.Rnd.Next(1, Math.Min(5, orderedSystemList.Count));
                        habitat1 = orderedSystemList[index2].SystemInfo.SystemStar;
                        if (habitat1 != null && this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, habitat1.Xpos, habitat1.Ypos) > 1000000.0)
                            habitat1 = (Habitat)null;
                    }
                    if (habitat1 != null)
                    {
                        Habitat habitat2 = habitat1;
                        SystemInfo system = this._Galaxy.Systems[habitat1.SystemIndex];
                        if (system != null && system.Habitats != null && system.Habitats.Count > 0)
                        {
                            int index = Galaxy.Rnd.Next(0, system.Habitats.Count);
                            habitat2 = system.Habitats[index];
                        }
                        if (habitat2 != null)
                        {
                            this.ParentHabitat = habitat2;
                            this._AnchorHabitat = habitat2;
                            double num1 = (double)this.ParentHabitat.Diameter / 2.0 * Galaxy.Rnd.NextDouble();
                            double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                            this._ParentOffsetX = Math.Cos(num2) * num1;
                            this._ParentOffsetY = Math.Sin(num2) * num1;
                            this._ParentX = Math.Cos(num2) * num1;
                            this._ParentY = Math.Sin(num2) * num1;
                            this._TargetSpeed = (float)this._MovementSpeed;
                        }
                    }
                }
                else if (this._Type == CreatureType.Ardilus && !this._LocationLocked && Galaxy.Rnd.Next(0, 2) == 1)
                {
                    Habitat habitat = (Habitat)null;
                    int index = this._AnchorHabitat.HabitatIndex + 1;
                    int iterationCount = 0;
                    while (Galaxy.ConditionCheckLimit(habitat == null, 40000, ref iterationCount))
                    {
                        if (index >= this._Galaxy.Habitats.Count)
                            index = 0;
                        if (this._Galaxy.Habitats[index].Type == HabitatType.GasGiant && this._Galaxy.CalculateDistance(this._Galaxy.Habitats[index].Xpos, this._Galaxy.Habitats[index].Ypos, this.Xpos, this.Ypos) < 1000000.0)
                        {
                            habitat = this._Galaxy.Habitats[index];
                            bool flag = false;
                            Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                            if (this._Galaxy.Systems[habitatSystemStar].DominantEmpire != null && this._Galaxy.Systems[habitatSystemStar].DominantEmpire.Empire != null && !this._Galaxy.Systems[habitatSystemStar].DominantEmpire.Empire.CheckEmpireHasHyperDriveTech(this._Galaxy.Systems[habitatSystemStar].DominantEmpire.Empire))
                                flag = true;
                            if (flag)
                                habitat = (Habitat)null;
                        }
                        ++index;
                        if (index >= this._Galaxy.Habitats.Count)
                            index = 0;
                        if (index == this._AnchorHabitat.HabitatIndex)
                            break;
                    }
                    if (habitat != null && habitat != this._AnchorHabitat)
                        this._AnchorHabitat = habitat;
                }
                if (this._AnchorHabitat == null)
                    return;
                this.ParentHabitat = this._AnchorHabitat;
                double num3 = (double)this.ParentHabitat.Diameter;
                if (num3 < (double)(this._AnchorRange * 2))
                    num3 = (double)(this._AnchorRange * 2);
                double num4 = num3 - 20.0;
                if (num4 < 1.0)
                    num4 = 1.0;
                double num5 = num4 / 2.0 * Galaxy.Rnd.NextDouble();
                double num6 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                this._ParentOffsetX = Math.Cos(num6) * num5;
                this._ParentOffsetY = Math.Sin(num6) * num5;
                this._TargetSpeed = (float)this._MovementSpeed;
            }
            else
            {
                if (!(this._AnchorPoint != Point.Empty) || this.CurrentTarget != null || Galaxy.Rnd.Next(0, 2) != 1)
                    return;
                double num7 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double num8 = (double)this._AnchorRange * 0.9 * Galaxy.Rnd.NextDouble();
                this._ParentOffsetX = Math.Cos(num7) * num8;
                this._ParentOffsetY = Math.Sin(num7) * num8;
                this._TargetSpeed = (float)this._MovementSpeed;
            }
        }

        private void Split()
        {
            if (this._Galaxy.SilverMistCreatureCount >= (int)Math.Sqrt((double)this._Galaxy.StarCount) || this._Type != CreatureType.SilverMist || (double)this.CurrentSpeed > 0.0 || this.Damage > (double)(int)((double)this._DamageKillThreshhold * 0.2) || this.Size <= Galaxy.Rnd.Next(400, 550))
                return;
            int num = this.Size / 2;
            this.Size -= num;
            this._AttackStrength = (int)((double)this.Size / 10.0);
            this._DamageKillThreshhold = (int)((double)this.Size * 3.0);
            Creature creature = new Creature(this._Galaxy, CreatureType.SilverMist, this.ParentHabitat, (int)this._ParentX, (int)this._ParentY);
            creature.Size = num;
            creature.AttackStrength = (int)((double)creature.Size / 10.0);
            this._Galaxy.Creatures.Add(creature);
            if (this.NearestSystemStar != null)
            {
                this._Galaxy.Systems[this.NearestSystemStar.SystemIndex].Creatures.Add(creature);
                creature.NearestSystemStar = this.NearestSystemStar;
            }
            ++this._Galaxy.SilverMistCreatureCount;
        }

        private void Reproduce(double timePassed)
        {
            if (this._Galaxy.Creatures.Count >= this._Galaxy.StarCount / 2 || this._Type == CreatureType.SilverMist || this.NearestSystemStar == null)
                return;
            if (this.NearestSystemStar != null && this._Galaxy.Systems.Count > this.NearestSystemStar.SystemIndex)
            {
                SystemInfo system = this._Galaxy.Systems[this._NearestSystemStar.SystemIndex];
                if (system != null && system.Creatures != null)
                {
                    int num = 20;
                    if (system.DominantEmpire != null && system.DominantEmpire.Empire != null)
                        num = 4;
                    if (system.Creatures.Count > num)
                        return;
                }
            }
            if (this._Type == CreatureType.Kaltor)
                this._ReproductionCounter += timePassed / 10.0;
            if (this._ReproductionCounter <= 100.0 || this.CurrentTarget != null || this.ParentHabitat == null)
                return;
            this._ReproductionCounter = 0.0;
            Habitat parentHabitat = this.ParentHabitat;
            if (this._Type != CreatureType.Kaltor)
                return;
            int num1 = Galaxy.Rnd.Next(1, 3);
            for (int index = 0; index < num1; ++index)
            {
                Creature creature = new Creature(this._Galaxy, CreatureType.Kaltor, parentHabitat);
                this._Galaxy.Creatures.Add(creature);
                if (this.NearestSystemStar != null)
                {
                    this._Galaxy.Systems[this.NearestSystemStar.SystemIndex].Creatures.Add(creature);
                    creature.NearestSystemStar = this.NearestSystemStar;
                }
            }
        }

        public bool DamageCreature(StellarObject damager, int damage, Weapon weapon)
        {
            if (this._Type == CreatureType.SilverMist && (weapon == null || weapon.Component.Type != ComponentType.WeaponIonCannon && weapon.Component.Type != ComponentType.WeaponIonPulse))
                damage = Math.Max(1, (int)((double)damage / 10.0));
            this._Damage += (double)damage;
            if (this._Damage > (double)this._DamageKillThreshhold)
            {
                if (damager != null && damager.Empire != null && damager.Empire.Counters != null)
                    damager.Empire.Counters.ProcessCreatureDeath(this);
                this.HasBeenDestroyed = true;
                this._Galaxy.Creatures.Remove(this);
                if (this.ParentHabitat != null)
                    this._Galaxy.Systems[this.ParentHabitat.SystemIndex].Creatures.Remove(this);
                else if (this.NearestSystemStar != null)
                    this._Galaxy.Systems[this.NearestSystemStar.SystemIndex].Creatures.Remove(this);
                return true;
            }
            if (this._Damage < 0.0)
                this._Damage = 0.0;
            return false;
        }

        public void CompleteTeardown()
        {
            if (this.CurrentTarget != null)
            {
                if (this.CurrentTarget.Pursuers != null)
                    this.CurrentTarget.Pursuers.Remove((StellarObject)this);
                if (this.CurrentTarget.Attackers != null)
                    this.CurrentTarget.Attackers.Remove((StellarObject)this);
            }
            for (int index1 = 0; index1 < this._Galaxy.BuiltObjects.Count; ++index1)
            {
                if (this._Galaxy.BuiltObjects[index1] != null)
                {
                    int index2 = this._Galaxy.BuiltObjects[index1].Attackers.IndexOf((StellarObject)this);
                    if (index2 >= 0)
                        this._Galaxy.BuiltObjects[index1].Attackers.RemoveAt(index2);
                    int index3 = this._Galaxy.BuiltObjects[index1].Pursuers.IndexOf((StellarObject)this);
                    if (index3 >= 0)
                        this._Galaxy.BuiltObjects[index1].Pursuers.RemoveAt(index3);
                    if (this._Galaxy.BuiltObjects[index1].CurrentTarget == this)
                        this._Galaxy.BuiltObjects[index1].CurrentTarget = (StellarObject)null;
                    if (this._Galaxy.BuiltObjects[index1].Fighters != null && this._Galaxy.BuiltObjects[index1].Fighters.Count > 0)
                    {
                        for (int index4 = 0; index4 < this._Galaxy.BuiltObjects[index1].Fighters.Count; ++index4)
                        {
                            Fighter fighter = this._Galaxy.BuiltObjects[index1].Fighters[index4];
                            if (fighter.CurrentTarget == this)
                            {
                                fighter.AbandonAttackTarget();
                                fighter.EvaluateThreats(this._Galaxy);
                                if (fighter.MissionType == FighterMissionType.Undefined)
                                    fighter.MissionType = FighterMissionType.Patrol;
                            }
                        }
                    }
                }
            }
            this._Galaxy.Creatures.Remove(this);
            for (int index = 0; index < this._Galaxy.Systems.Count; ++index)
            {
                while (this._Galaxy.Systems[index].Creatures.Contains(this))
                    this._Galaxy.Systems[index].Creatures.Remove(this);
            }
            lock (Galaxy.GalaxyLocationRemoveLock)
            {
                for (int index = 0; index < this._Galaxy.GalaxyLocations.Count; ++index)
                {
                    if (this._Galaxy.GalaxyLocations[index].RelatedCreatures != null && this._Galaxy.GalaxyLocations[index].RelatedCreatures.Contains(this))
                        this._Galaxy.GalaxyLocations[index].RelatedCreatures.Remove(this);
                }
            }
        }

        private void Move(double timePassed, DateTime tempNow)
        {
            if ((double)this._TargetSpeed > 0.0 || (double)this.CurrentSpeed > 0.0)
            {
                double num1 = this.Xpos;
                double num2 = this.Ypos;
                if (!this._IsBenign && this.CurrentTarget != null)
                {
                    if (this.CheckTargetInRange(this.CurrentTarget))
                    {
                        num1 = this.CurrentTarget.Xpos;
                        num2 = this.CurrentTarget.Ypos;
                        this._TargetSpeed = (float)this._MovementSpeed;
                    }
                    else
                    {
                        this.CurrentTarget = (StellarObject)null;
                        this._DistanceToTarget = double.MaxValue;
                        this._TargetSpeed = 0.0f;
                    }
                }
                else if (this._IsBenign && this.Attackers.Count > 0)
                    this.FleeFromAttacker(this.Attackers[0]);
                else if (this.ParentHabitat != null)
                {
                    num1 = this.ParentHabitat.Xpos + this._ParentOffsetX;
                    num2 = this.ParentHabitat.Ypos + this._ParentOffsetY;
                }
                else if (this._AnchorPoint != Point.Empty)
                {
                    num1 = (double)this._AnchorPoint.X + this._ParentOffsetX;
                    num2 = (double)this._AnchorPoint.Y + this._ParentOffsetY;
                }
                else
                {
                    this._TargetSpeed = 0.0f;
                    this.AccelerateToTargetSpeed(timePassed, (double)this._AccelerationRate);
                    return;
                }
                if (num1 != this.Xpos && num2 != this.Ypos)
                    this.TargetHeading = (float)Galaxy.DetermineAngle(this.Xpos, this.Ypos, num1, num2);
                double accelerationRate = (double)this._AccelerationRate;
                if (this.CurrentTarget != null && this._LungeSpeed > 0)
                {
                    TimeSpan timeSpan = tempNow.Subtract(this._LastLunge);
                    if (timeSpan.TotalSeconds <= this._LungeLength)
                    {
                        this._TargetSpeed = (float)this._LungeSpeed;
                        accelerationRate = this._LungeAccelerationRate;
                    }
                    else if (timeSpan.TotalSeconds > this._LungeInterval)
                    {
                        if ((double)Math.Abs(this.TargetHeading - this._CurrentHeading) < 0.5 && this._Galaxy.CalculateDistance(this.CurrentTarget.Xpos, this.CurrentTarget.Ypos, this.Xpos, this.Ypos) < this._LungeLength * (double)this._LungeSpeed)
                        {
                            this._LastLunge = tempNow;
                            this._TargetSpeed = (float)this._LungeSpeed;
                            accelerationRate = this._LungeAccelerationRate;
                        }
                    }
                    else
                    {
                        this._TargetSpeed = (float)this._MovementSpeed;
                        this.CurrentSpeed = (float)this._MovementSpeed;
                        accelerationRate = (double)this._AccelerationRate;
                    }
                }
                this.CalculateCurrentHeading(timePassed);
                this.AccelerateToTargetSpeed(timePassed, accelerationRate);
                double num3 = (double)this.CurrentSpeed * timePassed;
                if (this.ParentHabitat != null)
                {
                    if (this.CurrentTarget == null)
                    {
                        this.CalculateCurrentParentOffset(out this._ParentX, out this._ParentY);
                        this._ParentX += Math.Cos((double)this._CurrentHeading) * num3;
                        this._ParentY += Math.Sin((double)this._CurrentHeading) * num3;
                        this.ApplyCurrentParentOffset();
                    }
                    else
                    {
                        this.Xpos += Math.Cos((double)this._CurrentHeading) * num3;
                        this.Ypos += Math.Sin((double)this._CurrentHeading) * num3;
                        this.CalculateCurrentParentOffset(out this._ParentX, out this._ParentY);
                    }
                }
                else
                {
                    this.Xpos += Math.Cos((double)this._CurrentHeading) * num3;
                    this.Ypos += Math.Sin((double)this._CurrentHeading) * num3;
                }
                double currentDistance = 0.0;
                if (this.CheckWhetherArrived(this.Xpos, this.Ypos, num1, num2, 30.0, out currentDistance))
                {
                    this._TargetSpeed = 0.0f;
                    if (this.CurrentTarget != null && !(this.CurrentTarget is Habitat))
                    {
                        this._TargetSpeed = this.CurrentTarget.CurrentSpeed;
                        if ((double)this._TargetSpeed > (double)this._MovementSpeed)
                            this._TargetSpeed = (float)this._MovementSpeed;
                    }
                    if ((double)this.CurrentSpeed <= (double)this._MovementSpeed || (double)this.CurrentSpeed <= (double)this._LungeSpeed)
                        return;
                    this.CurrentSpeed = (float)this._MovementSpeed;
                    double x;
                    double y;
                    this._Galaxy.SelectHyperJumpExitPoint(out x, out y, Galaxy.BaseHyperJumpAccuracy);
                    this.Xpos = num1 + x;
                    this.Ypos = num2 + y;
                    this._NearestSystemStar = this._Galaxy.FastFindNearestSystem(this.Xpos, this.Ypos);
                    this._Galaxy.Systems[this._NearestSystemStar.SystemIndex].Creatures.Add(this);
                }
                else
                {
                    this._TargetSpeed = (float)this._MovementSpeed;
                    if (this._HyperSpeed <= 0 || currentDistance <= (double)Galaxy.HyperJumpThreshhold || (double)this._TargetSpeed >= (double)this._HyperSpeed)
                        return;
                    if (this._HyperCountdown == 0L)
                        this._HyperCountdown = (long)Galaxy.Rnd.Next(8000, 14000);
                    this._HyperCountdown -= (long)(timePassed * 1000.0);
                    if (this._HyperCountdown > 0L)
                        return;
                    this._HyperCountdown = 0L;
                    this._TargetSpeed = (float)this._HyperSpeed;
                    this.CurrentSpeed = (float)this._HyperSpeed;
                    if (this._NearestSystemStar == null)
                        return;
                    this._Galaxy.Systems[this._NearestSystemStar.SystemIndex].Creatures.Remove(this);
                    this._NearestSystemStar = (Habitat)null;
                    this.ParentHabitat = (Habitat)null;
                    this._AnchorHabitat = (Habitat)null;
                }
            }
            else
            {
                if (this.CurrentTarget != null)
                {
                    if (!(this.CurrentTarget is Habitat))
                    {
                        this._TargetSpeed = this.CurrentTarget.CurrentSpeed;
                        if ((double)this._TargetSpeed > (double)this._MovementSpeed)
                            this._TargetSpeed = (float)this._MovementSpeed;
                    }
                    else
                        this._TargetSpeed = (float)this._MovementSpeed;
                }
                if (this.ParentHabitat == null)
                    return;
                this.Xpos = this.ParentHabitat.Xpos + this._ParentX;
                this.Ypos = this.ParentHabitat.Ypos + this._ParentY;
            }
        }

        private bool CheckWhetherArrived(
          double currentPositionX,
          double currentPositionY,
          double targetPositionX,
          double targetPositionY,
          double allowance,
          out double currentDistance)
        {
            currentDistance = this._Galaxy.CalculateDistance(currentPositionX, currentPositionY, targetPositionX, targetPositionY);
            if (currentDistance <= allowance)
            {
                this._LastPositionX = currentPositionX;
                this._LastPositionY = currentPositionY;
                return true;
            }
            double distance1 = this._Galaxy.CalculateDistance(this._LastPositionX, this._LastPositionY, targetPositionX, targetPositionY);
            double distance2 = this._Galaxy.CalculateDistance(this._LastPositionX, this._LastPositionY, currentPositionX, currentPositionY);
            this._LastPositionX = currentPositionX;
            this._LastPositionY = currentPositionY;
            return distance1 <= distance2;
        }

        private void CalculateCurrentParentOffset(out double parentX, out double parentY)
        {
            parentX = 0.0;
            parentY = 0.0;
            if (this.ParentHabitat == null)
                return;
            parentX = this.Xpos - this.ParentHabitat.Xpos;
            parentY = this.Ypos - this.ParentHabitat.Ypos;
        }

        private void ApplyCurrentParentOffset()
        {
            if (this.ParentHabitat == null)
                return;
            this.Xpos = this.ParentHabitat.Xpos + this._ParentX;
            this.Ypos = this.ParentHabitat.Ypos + this._ParentY;
        }

        private void FleeFromAttacker(StellarObject attacker)
        {
            this.TargetHeading = (float)Galaxy.DetermineAngle(attacker.Xpos, attacker.Ypos, this.Xpos, this.Ypos);
            this._TargetSpeed = (float)this._MovementSpeed;
        }

        private void CheckForAttackers()
        {
            if (this.Attackers.Count <= 0)
                return;
            this.CurrentTarget = this.Attackers[0];
            if (this.CurrentTarget.Pursuers != null && this.CurrentTarget.Pursuers.IndexOf((StellarObject)this) < 0)
                this.CurrentTarget.Pursuers.Add((StellarObject)this);
            this._TargetSpeed = (float)this._MovementSpeed;
        }

        private void CheckForTargets()
        {
            if (this.CurrentTarget != null || this._Galaxy.DeferEventsForGameStart)
                return;
            StellarObject stellarObject = this.ScanForTarget();
            if (stellarObject == null)
                return;
            if (this.CurrentTarget != null)
            {
                if (this.CurrentTarget.Pursuers != null)
                    this.CurrentTarget.Pursuers.Remove((StellarObject)this);
                if (this.CurrentTarget.Attackers != null)
                    this.CurrentTarget.Attackers.Remove((StellarObject)this);
            }
            this.CurrentTarget = stellarObject;
            if (stellarObject is Habitat)
            {
                double num1 = this.Xpos - stellarObject.Xpos;
                double num2 = this.Ypos - stellarObject.Ypos;
                this.ParentHabitat = (Habitat)stellarObject;
                this._AnchorHabitat = this.ParentHabitat;
                this._ParentX = num1;
                this._ParentY = num2;
                this._ParentOffsetX = num1;
                this._ParentOffsetY = num2;
            }
            if (this.CurrentTarget.Pursuers != null && this.CurrentTarget.Pursuers.IndexOf((StellarObject)this) < 0)
                this.CurrentTarget.Pursuers.Add((StellarObject)this);
            this._TargetSpeed = (float)this._MovementSpeed;
            switch (stellarObject)
            {
                case BuiltObject _:
                    this._Galaxy.NotifyOfAttack((StellarObject)this, (Empire)null, (BuiltObject)stellarObject, true);
                    break;
                case Habitat _:
                    this._Galaxy.NotifyOfAttack((StellarObject)this, (Empire)null, (Habitat)stellarObject, false, true, true);
                    break;
            }
        }

        private StellarObject ScanForTarget()
        {
            if ((double)this.CurrentSpeed > (double)this._MovementSpeed)
                return (StellarObject)null;
            StellarObject target = (StellarObject)null;
            if (this.NearestSystemStar == null)
                target = (StellarObject)this._Galaxy.FindNearestBuiltObject((int)this.Xpos, (int)this.Ypos, (Empire)null);
            else if (this._Type == CreatureType.SilverMist)
            {
                if (this.NearestSystemStar.SystemIndex >= 0 && this.NearestSystemStar.SystemIndex < this._Galaxy.Systems.Count)
                {
                    SystemInfo system = this._Galaxy.Systems[this.NearestSystemStar.SystemIndex];
                    if (system != null && (system.IndependentColonyCount > 0 || system.DominantEmpire != null && system.DominantEmpire.Empire != null))
                        target = (StellarObject)this._Galaxy.FindNearestColonyInSystem(system, this.Xpos, this.Ypos);
                }
                if (target == null)
                    target = (StellarObject)this._Galaxy.FastFindNearestShipInSystem(this.Xpos, this.Ypos, this.NearestSystemStar);
            }
            else
                target = (StellarObject)this._Galaxy.FastFindNearestShipInSystem(this.Xpos, this.Ypos, this.NearestSystemStar);
            return target != null && target.Empire != null && this.CheckTargetInRange(target) ? target : (StellarObject)null;
        }

        private bool CheckTargetInRange(StellarObject target)
        {
            int val2 = this._AnchorRange;
            double num1;
            if (target != null && (!this._LocationLocked || this._AnchorHabitat == null && this._AnchorPoint == Point.Empty))
                num1 = this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, target.Xpos, target.Ypos);
            else if (this._AnchorHabitat != null)
            {
                num1 = this._Galaxy.CalculateDistance(this._AnchorHabitat.Xpos, this._AnchorHabitat.Ypos, target.Xpos, target.Ypos);
                if (this._AnchorHabitat.Category == HabitatCategoryType.GasCloud)
                    val2 = (int)this._AnchorHabitat.Diameter / 2;
            }
            else
                num1 = !(this._AnchorPoint != Point.Empty) ? this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, target.Xpos, target.Ypos) : this._Galaxy.CalculateDistance((double)this._AnchorPoint.X, (double)this._AnchorPoint.Y, target.Xpos, target.Ypos);
            int num2 = Math.Max(this._AttackRange, val2);
            if (num1 < (double)num2)
            {
                this._TargetSpeed = num1 >= 30.0 ? (float)this._MovementSpeed : target.CurrentSpeed;
                return true;
            }
            if (this.CurrentTarget != null)
            {
                if (this.CurrentTarget.Pursuers != null)
                    this.CurrentTarget.Pursuers.Remove((StellarObject)this);
                if (this.CurrentTarget.Attackers != null)
                    this.CurrentTarget.Attackers.Remove((StellarObject)this);
            }
            return false;
        }

        private void AttackTarget(double timePassed, DateTime tempNow)
        {
            this._IsVisible = true;
            if (this.CurrentTarget == null || this._AttackStrength <= 0)
                return;
            this._DistanceToTarget = this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, this.CurrentTarget.Xpos, this.CurrentTarget.Ypos);
            if (this._DistanceToTarget <= 50.0)
            {
                this._TargetSpeed = this.CurrentTarget is Habitat ? (float)((Habitat)this.CurrentTarget).OrbitSpeed + 3f : this.CurrentTarget.CurrentSpeed;
                if (this.DamageTarget(this.CurrentTarget, Math.Max(1, (int)((double)this._AttackStrength * timePassed)), tempNow, timePassed))
                {
                    if (!(this.CurrentTarget is Habitat))
                    {
                        double size = (double)this.CurrentTarget.Size;
                        if (this.Size < this._MaxSize)
                        {
                            int num1 = Math.Min(4, (int)(size / 50.0));
                            int num2 = (int)(size / 10.0);
                            if (this._Type == CreatureType.SilverMist)
                            {
                                num1 = Math.Min(40, (int)(size / 25.0));
                                num2 = (int)(size / 5.0);
                            }
                            this._AttackStrength += num1;
                            this.Size += num2;
                            this.Size = Math.Min(this.Size, this._MaxSize);
                            this._DamageKillThreshhold += (int)(size / 10.0);
                            this._DamageKillThreshhold = Math.Min(this._DamageKillThreshhold, (int)((double)this.Size * 3.0));
                        }
                    }
                    this._Galaxy.CheckTriggerEvent(this.CurrentTarget.GameEventId, (Empire)null, EventTriggerType.Destroy, (object)null);
                    this._DistanceToTarget = double.MaxValue;
                    if (this.CurrentTarget.Attackers != null)
                    {
                        foreach (StellarObject attacker in (SyncList<StellarObject>)this.CurrentTarget.Attackers)
                            attacker.CurrentTarget = (StellarObject)null;
                    }
                    this.CurrentTarget = (StellarObject)null;
                    this.ParentHabitat = this._AnchorHabitat;
                }
                if (this.CurrentTarget == null || this.CurrentTarget.Attackers == null || this.CurrentTarget.Attackers.IndexOf((StellarObject)this) >= 0)
                    return;
                this.CurrentTarget.Attackers.Add((StellarObject)this);
            }
            else
                this._TargetSpeed = (float)this._MovementSpeed;
        }

        private bool DamageTarget(
          StellarObject abstractTarget,
          int damage,
          DateTime tempNow,
          double timePassed)
        {
            switch (abstractTarget)
            {
                case Creature _:
                    Creature creature = (Creature)abstractTarget;
                    if (creature.DamageCreature((StellarObject)null, damage, (Weapon)null))
                    {
                        creature.CompleteTeardown();
                        this.CurrentTarget = (StellarObject)null;
                        break;
                    }
                    break;
                case Habitat _:
                    Habitat habitat = (Habitat)abstractTarget;
                    if (this._Type == CreatureType.SilverMist && habitat.Population != null && habitat.Population.Count > 0)
                    {
                        if ((double)habitat.Quality > 0.0)
                        {
                            if ((double)habitat.Damage <= 0.0 && habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                            {
                                Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                                string description = string.Format(TextResolver.GetText("Colony Under Attack From Silvermist Description"), (object)habitat.Name, (object)habitatSystemStar.Name, (object)this.Name);
                                habitat.Empire.SendMessageToEmpire(habitat.Empire, EmpireMessageType.GeneralBadEvent, (object)habitat, description);
                            }
                            float num = (float)(timePassed * 0.01);
                            habitat.Damage += num;
                            habitat.Damage = Math.Min(habitat.Damage, habitat.BaseQuality);
                            habitat.RecalculateQuality();
                        }
                        long val2 = (long)(timePassed * 1000000.0 * (double)this._AttackStrength);
                        long num1 = Math.Min(habitat.Population.TotalAmount, val2);
                        long num2 = num1 / (long)habitat.Population.Count;
                        if (this.Size < this._MaxSize)
                        {
                            this.Size += Math.Max(1, Math.Min((int)((double)num1 / 1000000.0), 10));
                            this.Size = Math.Min(this.Size, this._MaxSize);
                            this._AttackStrength = (int)((double)this.Size / 10.0);
                            this._DamageKillThreshhold = (int)((double)this.Size * 3.0);
                        }
                        PopulationList populationList = new PopulationList();
                        for (int index = 0; index < habitat.Population.Count; ++index)
                        {
                            DistantWorlds.Types.Population population = habitat.Population[index];
                            population.Amount -= num2;
                            if (population.Amount <= 0L)
                                populationList.Add(population);
                        }
                        for (int index = 0; index < populationList.Count; ++index)
                            habitat.Population.Remove(populationList[index]);
                        if (habitat.Population.Count <= 0 && habitat.Empire != null)
                        {
                            if (habitat.Empire != null && habitat.Empire != this._Galaxy.IndependentEmpire)
                            {
                                Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                                string text = TextResolver.GetText("SilverMist Wipes Out Colony");
                                string description = string.Format(TextResolver.GetText("Colony Wiped Out From Silvermist Description"), (object)habitat.Name, (object)habitatSystemStar.Name, (object)this.Name);
                                habitat.Empire.SendMessageToEmpireWithTitle(habitat.Empire, EmpireMessageType.GeneralBadEvent, (object)habitat, description, text);
                            }
                            habitat.Empire.TakeOwnershipOfColony(habitat, (Empire)null, true);
                            for (int index = 0; index < habitat.Troops.Count; ++index)
                            {
                                habitat.Troops[index].BuiltObject = (BuiltObject)null;
                                habitat.Troops[index].Colony = (Habitat)null;
                                habitat.Troops[index].Empire = (Empire)null;
                                habitat.Troops[index].Race = (Race)null;
                            }
                            for (int index = 0; index < habitat.TroopsToRecruit.Count; ++index)
                            {
                                habitat.TroopsToRecruit[index].BuiltObject = (BuiltObject)null;
                                habitat.TroopsToRecruit[index].Colony = (Habitat)null;
                                habitat.TroopsToRecruit[index].Empire = (Empire)null;
                                habitat.TroopsToRecruit[index].Race = (Race)null;
                            }
                            for (int index = 0; index < habitat.InvadingTroops.Count; ++index)
                            {
                                habitat.InvadingTroops[index].BuiltObject = (BuiltObject)null;
                                habitat.InvadingTroops[index].Colony = (Habitat)null;
                                habitat.InvadingTroops[index].Empire = (Empire)null;
                                habitat.InvadingTroops[index].Race = (Race)null;
                            }
                            habitat.Troops.Clear();
                            habitat.TroopsToRecruit.Clear();
                            habitat.InvadingTroops.Clear();
                            Character[] arrayThreadSafe1 = ListHelper.ToArrayThreadSafe(habitat.Characters);
                            for (int index = 0; index < arrayThreadSafe1.Length; ++index)
                            {
                                arrayThreadSafe1[index].SendDeathMessage(CharacterDeathType.GenericDeath, this._Galaxy);
                                arrayThreadSafe1[index].Kill(this._Galaxy);
                            }
                            Character[] arrayThreadSafe2 = ListHelper.ToArrayThreadSafe(habitat.InvadingCharacters);
                            for (int index = 0; index < arrayThreadSafe2.Length; ++index)
                            {
                                arrayThreadSafe2[index].SendDeathMessage(CharacterDeathType.GenericDeath, this._Galaxy);
                                arrayThreadSafe2[index].Kill(this._Galaxy);
                            }
                            habitat.Empire = (Empire)null;
                            habitat.ManufacturingQueue = (ManufacturingQueue)null;
                            habitat.ConstructionQueue = (ConstructionQueue)null;
                        }
                        habitat.Population.RecalculateTotalAmount();
                        if (habitat.Population.TotalAmount <= 0L)
                            return true;
                        break;
                    }
                    break;
                case BuiltObject _:
                    BuiltObject excludeBuiltObject = (BuiltObject)abstractTarget;
                    int num3 = 0;
                    for (int index = 0; index < excludeBuiltObject.Components.Count; ++index)
                    {
                        if (excludeBuiltObject.Components[index].Status == ComponentStatus.Normal)
                            num3 += excludeBuiltObject.Components[index].Size;
                    }
                    if (num3 <= damage)
                    {
                        excludeBuiltObject.Explosions.Add(new Explosion()
                        {
                            ExplosionStart = tempNow,
                            ExplosionSize = (short)30,
                            ExplosionProgression = 0.0f,
                            ExplosionOffsetX = (short)0,
                            ExplosionOffsetY = (short)0,
                            ExplosionImageIndex = (short)Galaxy.Rnd.Next(0, 10),
                            ExplosionWillDestroy = true
                        });
                        excludeBuiltObject.HasBeenDestroyed = true;
                        if (excludeBuiltObject.Empire != null)
                            excludeBuiltObject.Empire.ResolveSystemVisibility(excludeBuiltObject.Xpos, excludeBuiltObject.Ypos, excludeBuiltObject, (Habitat)null);
                        if (excludeBuiltObject.ConstructionQueue != null && excludeBuiltObject.ConstructionQueue.ConstructionYards.CountUnderConstruction > 0)
                        {
                            foreach (ConstructionYard constructionYard in (SyncList<ConstructionYard>)excludeBuiltObject.ConstructionQueue.ConstructionYards)
                            {
                                BuiltObject underConstruction = constructionYard.ShipUnderConstruction;
                                if (underConstruction != null)
                                    this.DamageTarget((StellarObject)underConstruction, int.MaxValue, tempNow, timePassed);
                            }
                        }
                        excludeBuiltObject.ReDefine();
                        return true;
                    }
                    BuiltObjectComponent component = excludeBuiltObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                    int iterationCount1 = 0;
                    while (Galaxy.ConditionCheckLimit(component != null && damage > 0, 200, ref iterationCount1))
                    {
                        if (component.Value2 > 0 && damage <= component.Value2)
                            damage = 0;
                        if (damage > 0)
                        {
                            component.Status = ComponentStatus.Damaged;
                            damage -= component.Value1;
                            component = excludeBuiltObject.Components[ComponentCategoryType.Armor, ComponentStatus.Normal];
                        }
                    }
                    double damageReduction = excludeBuiltObject.DamageReduction;
                    if (excludeBuiltObject.ShipGroup != null)
                        damageReduction *= excludeBuiltObject.ShipGroup.DamageControlBonus;
                    double num4 = damageReduction * excludeBuiltObject.CaptainDamageControlBonus;
                    damage = (int)((double)damage + 0.49 - (double)damage * num4);
                    int index1;
                    for (int iterationCount2 = 0; Galaxy.ConditionCheckLimit(damage > 0, 500, ref iterationCount2); damage -= excludeBuiltObject.Components[index1].Size)
                    {
                        int num5 = 0;
                        do
                        {
                            index1 = Galaxy.Rnd.Next(0, excludeBuiltObject.Components.Count);
                            ++num5;
                        }
                        while (num5 <= 10 && excludeBuiltObject.Components[index1].Status == ComponentStatus.Damaged);
                        excludeBuiltObject.Components[index1].Status = ComponentStatus.Damaged;
                        if (excludeBuiltObject.Role != BuiltObjectRole.Base)
                        {
                            switch (excludeBuiltObject.Components[index1].Type)
                            {
                                case ComponentType.StorageFuel:
                                    int num6 = excludeBuiltObject.Components[index1].Value1;
                                    if (excludeBuiltObject.CurrentFuel > 0.0)
                                    {
                                        excludeBuiltObject.CurrentFuel -= (double)num6;
                                        if (excludeBuiltObject.CurrentFuel < 0.0)
                                        {
                                            excludeBuiltObject.CurrentFuel = 0.0;
                                            continue;
                                        }
                                        continue;
                                    }
                                    continue;
                                case ComponentType.StorageCargo:
                                    int num7 = excludeBuiltObject.Components[index1].Value1;
                                    if (excludeBuiltObject.Cargo != null && excludeBuiltObject.Cargo.TotalUnits > 0)
                                    {
                                        CargoList cargoList = new CargoList();
                                        foreach (DistantWorlds.Types.Cargo cargo in (SyncList<DistantWorlds.Types.Cargo>)excludeBuiltObject.Cargo)
                                        {
                                            if (num7 > 0)
                                            {
                                                if (cargo.Amount > num7)
                                                {
                                                    cargo.Amount -= num7;
                                                    break;
                                                }
                                                if (cargo.Amount > 0)
                                                {
                                                    num7 -= cargo.Amount;
                                                    cargoList.Add(cargo);
                                                }
                                            }
                                            else
                                                break;
                                        }
                                        using (IEnumerator<DistantWorlds.Types.Cargo> enumerator = cargoList.GetEnumerator())
                                        {
                                            while (enumerator.MoveNext())
                                            {
                                                DistantWorlds.Types.Cargo current = enumerator.Current;
                                                excludeBuiltObject.Cargo.Remove(current);
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                        continue;
                                case ComponentType.StorageTroop:
                                    int num8 = excludeBuiltObject.Components[index1].Value1;
                                    if (excludeBuiltObject.Troops != null && excludeBuiltObject.Troops.TotalSize > 0)
                                    {
                                        excludeBuiltObject.TroopCapacity -= num8;
                                        if (excludeBuiltObject.Troops.TotalSize > excludeBuiltObject.TroopCapacity)
                                        {
                                            int index2 = Galaxy.Rnd.Next(0, excludeBuiltObject.Troops.Count);
                                            if (index2 < excludeBuiltObject.Troops.Count)
                                            {
                                                if (excludeBuiltObject.Empire != null && excludeBuiltObject.Empire.Troops != null)
                                                    excludeBuiltObject.Empire.Troops.Remove(excludeBuiltObject.Troops[index2]);
                                                excludeBuiltObject.Troops.RemoveAt(index2);
                                                continue;
                                            }
                                            continue;
                                        }
                                        continue;
                                    }
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                    excludeBuiltObject.ReDefine();
                    if (excludeBuiltObject.Role != BuiltObjectRole.Base && excludeBuiltObject.DamagedComponentCount > 0)
                    {
                        excludeBuiltObject.RepairForNextMission = true;
                        break;
                    }
                    break;
            }
            return false;
        }

        private void ApplyLocationEffects(double timePassed)
        {
            if (this._Locations == null)
                this._Locations = new GalaxyLocationList();
            this._Galaxy.DetermineGalaxyLocationsAtPointSuppliedList(this.Xpos, this.Ypos, GalaxyLocationType.Undefined, ref this._Locations);
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            double num1 = 0.0;
            bool flag4 = false;
            double num2 = 0.0;
            double num3 = 0.0;
            for (int index = 0; index < this._Locations.Count; ++index)
            {
                GalaxyLocation location = this._Locations[index];
                switch (location.Effect)
                {
                    case GalaxyLocationEffectType.HyperjumpDisabled:
                        flag3 = true;
                        break;
                    case GalaxyLocationEffectType.MovementSlowed:
                        flag1 = true;
                        break;
                    case GalaxyLocationEffectType.ShipDamage:
                        flag2 = true;
                        num1 = location.EffectAmount;
                        break;
                    case GalaxyLocationEffectType.ShipPull:
                        flag4 = true;
                        double x2 = (double)location.Xpos + (double)location.Width / 2.0;
                        double y2 = (double)location.Ypos + (double)location.Height / 2.0;
                        double distance = this._Galaxy.CalculateDistance(this.Xpos, this.Ypos, x2, y2);
                        double num4 = (double)location.Width / 2.0 / distance;
                        num2 = location.EffectAmount * num4;
                        num3 = Galaxy.DetermineAngle(this.Xpos, this.Ypos, x2, y2);
                        break;
                }
            }
            this._CreatureDamageAmountLocation = !flag2 ? 0.0f : (float)num1;
            if (flag4)
            {
                this._CreaturePullAmountLocation = (float)num2;
                this._CreaturePullAngleLocation = (float)num3;
            }
            else
            {
                this._CreaturePullAmountLocation = 0.0f;
                this._CreaturePullAngleLocation = 0.0f;
            }
            this._HyperjumpDisabledLocation = flag3;
            if (flag1)
            {
                this._MovementSpeed = (int)((double)this._MovementSpeedBase * 0.75);
                this._LungeSpeed = (int)((double)this._LungeSpeedBase * 0.75);
            }
            else if (!flag1 && this._MovementSlowedLocation)
            {
                this._MovementSpeed = this._MovementSpeedBase;
                this._LungeSpeed = this._LungeSpeedBase;
            }
            this._MovementSlowedLocation = flag1;
        }

        private void DoLocationEffects(double timePassed)
        {
            if ((double)this._CreatureDamageAmountLocation > 0.0 && this.DamageCreature((StellarObject)null, (int)((double)this._CreatureDamageAmountLocation * timePassed), (Weapon)null))
                this.CompleteTeardown();
            if ((double)this._CreaturePullAmountLocation <= 0.0)
                return;
            double num = (double)this._CreaturePullAmountLocation * timePassed;
            this.Xpos += Math.Cos((double)this._CreaturePullAngleLocation) * num;
            this.Ypos += Math.Sin((double)this._CreaturePullAngleLocation) * num;
            if (this.ParentHabitat != null)
            {
                this._ParentOffsetX += Math.Cos((double)this._CreaturePullAngleLocation) * num;
                this._ParentOffsetY += Math.Sin((double)this._CreaturePullAngleLocation) * num;
            }
            this.TargetHeading = this._CreaturePullAngleLocation + 3.14159274f;
            this.CurrentSpeed = (float)this.TopSpeed;
        }

        int IComparable<StellarObject>.CompareTo(StellarObject other) => throw new NotImplementedException();// this.SortTag.CompareTo(other.SortTag);

        int IComparable<Creature>.CompareTo(Creature other) => throw new NotImplementedException();// this.SortTag.CompareTo(other.SortTag);

        int IComparable<BuiltObject>.CompareTo(BuiltObject other) => throw new NotImplementedException();// this.SortTag.CompareTo(other.SortTag);

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
            //switch (obj)
            //{
            //  case BuiltObject _:
            //    return this.SortTag.CompareTo(((StellarObject) obj).SortTag);
            //  case Habitat _:
            //    return this.SortTag.CompareTo(((StellarObject) obj).SortTag);
            //  case Creature _:
            //    return this.SortTag.CompareTo(((StellarObject) obj).SortTag);
            //  default:
            //    return 0;
            //}
        }
    }
}
