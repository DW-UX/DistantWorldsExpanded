// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Design
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;
using ExpansionMod;
using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Design : IComparable<Design>, ISerializable
    {
        public BuiltObjectRole Role;

        public BuiltObjectSubRole SubRole;

        private string _Name;

        private long _DateCreated;

        private Empire _Empire;

        private int _TopSpeed;

        private int _CruiseSpeed;

        private int _WarpSpeed;

        private int _HyperjumpInitiate;

        private int _MaxFuel;

        private int _Firepower;

        private int _MaxShields;

        private BuiltObjectStance _Stance;

        private BuiltObjectFleeWhen _FleeWhen;

        private BattleTactics _TacticsStrongerShips;

        private BattleTactics _TacticsWeakerShips;

        private InvasionTactics _TacticsInvasion;

        private int _Price;

        private int _CargoCapacity;

        private int _Size;

        private int _PictureRef;

        private bool _IsSpacePort;

        private bool _IsRefuellingDepot;

        private bool _IsResearchLab;

        private bool _IsShipYard;

        private bool _IsResourceExtractor;

        private bool _IsManufacturer;

        private bool _IsObsolete;

        private int _BuildCount;

        public int StaticEnergyConsumption;

        public int ReactorPowerOutput;

        public int ReactorStorageCapacity;

        public int ReactorCycleFuelConsumption;

        public double CurrentReactorStorage;

        public int TargetSpeed;

        public double CurrentSpeed;

        public int TroopCapacity;

        public int FighterCapacity;

        public double Heading;

        public double TargetHeading;

        public double TurnRate;

        public int Armor;

        public int ArmorReactive;

        public int TargettingModifier;

        public short FleetTargettingModifier;

        public int CountermeasureModifier;

        public short FleetCountermeasureModifier;

        public double MaintenanceSavings;

        public double TradeBonuses;

        public int MedicalCapacity;

        public int RecreationCapacity;

        public int FirepowerRaw;

        public int OptimizedDesign;

        public int BombardPower;

        public int ShieldsCapacity;

        public double ShieldRechargeRate;

        public short ShieldAreaRechargeRange;

        public short ShieldAreaRechargeCapacity;

        public short ShieldAreaRechargeEnergyRequired;

        public int Population;

        public int SensorProximityArrayRange;

        public int SensorResourceProfileSensorRange;

        public int SensorLongRange;

        public int WeaponHyperDenyRange;

        public short HyperStopRange;

        public int MaximumWeaponsRange;

        public int MinimumWeaponsRange;

        public int PointDefenseWeaponsRange;

        public int IonDefense;

        public int IonWeaponPower;

        public int IonWeaponRange;

        public short TractorBeamRange;

        public short AssaultStrength;

        public short AssaultRange;

        public short AssaultShieldPenetration;

        public bool IsColony;

        public bool IsEnergyCollector;

        public bool AllowAutoRetrofit = true;

        public int ResearchWeapons;

        public int ResearchEnergy;

        public int ResearchHighTech;

        public int ExtractionMine;

        public int ExtractionGas;

        public int ExtractionLuxury;

        public int ManufactureWeapons;

        public int ManufactureEnergy;

        public int ManufactureHighTech;

        public int EnergyCollection;

        public Resource FuelType;

        public int FuelCapacity;

        public int TopSpeedFuelBurn;

        public int CruiseSpeedFuelBurn;

        public int WarpSpeedFuelBurn;

        public int ImpulseSpeedFuelBurn;

        public double AccelerationRate;

        public int DockingBayCount;

        public int ConstructionYardCount;

        private bool _IsManuallyCreated;

        private int _HyperDriveIndex;

        private double _DamageReduction;

        private int _DamageRepair;

        private double _Stealth = 1.0;

        public float ImageScalingFactor = 1f;

        public DesignImageScalingMode ImageScalingType;

        private WeaponList _Weapons = new WeaponList();

        public ComponentList Components;

        [OptionalField]
        public string RepaitPriorityTemplateName;

        public bool IsPlanetDestroyer
        {
            get
            {
                if (FirepowerRaw >= 10000)
                {
                    for (int i = 0; i < Weapons.Count; i++)
                    {
                        Weapon weapon = Weapons[i];
                        if (weapon != null && weapon.Component != null && (weapon.Component.Type == ComponentType.WeaponSuperBeam || weapon.Component.Type == ComponentType.WeaponSuperTorpedo || weapon.Component.Type == ComponentType.WeaponSuperMissile || weapon.Component.Type == ComponentType.WeaponSuperRailGun || weapon.Component.Type == ComponentType.WeaponSuperPhaser) && weapon.RawDamage >= 10000)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public double Stealth => _Stealth;

        public int DamageRepair => _DamageRepair;

        public double DamageReduction => _DamageReduction;

        public int BuildCount
        {
            get
            {
                return _BuildCount;
            }
            set
            {
                _BuildCount = value;
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public long DateCreated
        {
            get
            {
                return _DateCreated;
            }
            set
            {
                _DateCreated = value;
            }
        }

        public Empire Empire
        {
            get
            {
                return _Empire;
            }
            set
            {
                _Empire = value;
            }
        }

        public int TopSpeed
        {
            get
            {
                return _TopSpeed;
            }
            set
            {
                _TopSpeed = value;
            }
        }

        public int CruiseSpeed
        {
            get
            {
                return _CruiseSpeed;
            }
            set
            {
                _CruiseSpeed = value;
            }
        }

        public int WarpSpeed
        {
            get
            {
                return _WarpSpeed;
            }
            set
            {
                _WarpSpeed = value;
            }
        }

        public int HyperjumpInitiate
        {
            get
            {
                return _HyperjumpInitiate;
            }
            set
            {
                _HyperjumpInitiate = value;
            }
        }

        public int HyperDriveIndex
        {
            get
            {
                return _HyperDriveIndex;
            }
            set
            {
                _HyperDriveIndex = value;
            }
        }

        public int MaxFuel
        {
            get
            {
                return _MaxFuel;
            }
            set
            {
                _MaxFuel = value;
            }
        }

        public int MaxShields
        {
            get
            {
                return _MaxShields;
            }
            set
            {
                _MaxShields = value;
            }
        }

        public BuiltObjectStance Stance
        {
            get
            {
                return _Stance;
            }
            set
            {
                _Stance = value;
            }
        }

        public BuiltObjectFleeWhen FleeWhen
        {
            get
            {
                return _FleeWhen;
            }
            set
            {
                _FleeWhen = value;
            }
        }

        public BattleTactics TacticsStrongerShips
        {
            get
            {
                return _TacticsStrongerShips;
            }
            set
            {
                _TacticsStrongerShips = value;
            }
        }

        public BattleTactics TacticsWeakerShips
        {
            get
            {
                return _TacticsWeakerShips;
            }
            set
            {
                _TacticsWeakerShips = value;
            }
        }

        public InvasionTactics TacticsInvasion
        {
            get
            {
                return _TacticsInvasion;
            }
            set
            {
                _TacticsInvasion = value;
            }
        }

        public int Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }

        public int CargoCapacity
        {
            get
            {
                return _CargoCapacity;
            }
            set
            {
                _CargoCapacity = value;
            }
        }

        public int Size
        {
            get
            {
                return _Size;
            }
            set
            {
                _Size = value;
            }
        }

        public int PictureRef
        {
            get
            {
                return _PictureRef;
            }
            set
            {
                _PictureRef = BaconDesign.SetPictureRef(this, value);
            }
        }

        public bool IsSpacePort
        {
            get
            {
                return _IsSpacePort;
            }
            set
            {
                _IsSpacePort = value;
            }
        }

        public bool IsRefuellingDepot
        {
            get
            {
                return _IsRefuellingDepot;
            }
            set
            {
                _IsRefuellingDepot = value;
            }
        }

        public bool IsResearchLab
        {
            get
            {
                return _IsResearchLab;
            }
            set
            {
                _IsResearchLab = value;
            }
        }

        public bool IsShipYard
        {
            get
            {
                return _IsShipYard;
            }
            set
            {
                _IsShipYard = value;
            }
        }

        public bool IsResourceExtractor
        {
            get
            {
                return _IsResourceExtractor;
            }
            set
            {
                _IsResourceExtractor = value;
            }
        }

        public bool IsManufacturer
        {
            get
            {
                return _IsManufacturer;
            }
            set
            {
                _IsManufacturer = value;
            }
        }

        public bool IsObsolete
        {
            get
            {
                return _IsObsolete;
            }
            set
            {
                _IsObsolete = value;
            }
        }

        public bool IsManuallyCreated
        {
            get
            {
                return _IsManuallyCreated;
            }
            set
            {
                _IsManuallyCreated = value;
            }
        }

        public WeaponList Weapons
        {
            get
            {
                return _Weapons;
            }
            set
            {
                _Weapons = value;
            }
        }

        public Design(string name)
        {
            _Name = name;
            Components = new ComponentList();
        }

        public Design()
        {
        }

        public Design(SerializationInfo info, StreamingContext context)
            : this()
        {
            byte[] buffer = (byte[])info.GetValue("D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                Role = (BuiltObjectRole)binaryReader.ReadByte();
                SubRole = (BuiltObjectSubRole)binaryReader.ReadByte();
                _Name = binaryReader.ReadString();
                _DateCreated = binaryReader.ReadInt64();
                _TopSpeed = binaryReader.ReadInt32();
                _CruiseSpeed = binaryReader.ReadInt32();
                _WarpSpeed = binaryReader.ReadInt32();
                _HyperjumpInitiate = binaryReader.ReadInt32();
                _MaxFuel = binaryReader.ReadInt32();
                _Firepower = binaryReader.ReadInt32();
                _MaxShields = binaryReader.ReadInt32();
                _Stance = (BuiltObjectStance)binaryReader.ReadByte();
                _FleeWhen = (BuiltObjectFleeWhen)binaryReader.ReadByte();
                _TacticsStrongerShips = (BattleTactics)binaryReader.ReadByte();
                _TacticsWeakerShips = (BattleTactics)binaryReader.ReadByte();
                _TacticsInvasion = (InvasionTactics)binaryReader.ReadByte();
                _Price = binaryReader.ReadInt32();
                _CargoCapacity = binaryReader.ReadInt32();
                _Size = binaryReader.ReadInt32();
                _PictureRef = binaryReader.ReadInt32();
                _IsSpacePort = binaryReader.ReadBoolean();
                _IsRefuellingDepot = binaryReader.ReadBoolean();
                _IsResearchLab = binaryReader.ReadBoolean();
                _IsShipYard = binaryReader.ReadBoolean();
                _IsResourceExtractor = binaryReader.ReadBoolean();
                _IsManufacturer = binaryReader.ReadBoolean();
                _IsObsolete = binaryReader.ReadBoolean();
                _BuildCount = binaryReader.ReadInt32();
                StaticEnergyConsumption = binaryReader.ReadInt32();
                ReactorPowerOutput = binaryReader.ReadInt32();
                ReactorStorageCapacity = binaryReader.ReadInt32();
                ReactorCycleFuelConsumption = binaryReader.ReadInt32();
                CurrentReactorStorage = binaryReader.ReadDouble();
                TargetSpeed = binaryReader.ReadInt32();
                CurrentSpeed = binaryReader.ReadDouble();
                TroopCapacity = binaryReader.ReadInt32();
                FighterCapacity = binaryReader.ReadInt32();
                Heading = binaryReader.ReadDouble();
                TargetHeading = binaryReader.ReadDouble();
                TurnRate = binaryReader.ReadDouble();
                Armor = binaryReader.ReadInt32();
                ArmorReactive = binaryReader.ReadInt32();
                TargettingModifier = binaryReader.ReadInt32();
                CountermeasureModifier = binaryReader.ReadInt32();
                FleetTargettingModifier = binaryReader.ReadInt16();
                FleetCountermeasureModifier = binaryReader.ReadInt16();
                MaintenanceSavings = binaryReader.ReadDouble();
                TradeBonuses = binaryReader.ReadDouble();
                MedicalCapacity = binaryReader.ReadInt32();
                RecreationCapacity = binaryReader.ReadInt32();
                FirepowerRaw = binaryReader.ReadInt32();
                OptimizedDesign = binaryReader.ReadInt32();
                BombardPower = binaryReader.ReadInt32();
                PointDefenseWeaponsRange = binaryReader.ReadInt32();
                IonDefense = binaryReader.ReadInt32();
                IonWeaponPower = binaryReader.ReadInt32();
                IonWeaponRange = binaryReader.ReadInt32();
                TractorBeamRange = binaryReader.ReadInt16();
                AssaultStrength = binaryReader.ReadInt16();
                AssaultRange = binaryReader.ReadInt16();
                AssaultShieldPenetration = binaryReader.ReadInt16();
                ShieldsCapacity = binaryReader.ReadInt32();
                ShieldRechargeRate = binaryReader.ReadDouble();
                ShieldAreaRechargeRange = binaryReader.ReadInt16();
                ShieldAreaRechargeCapacity = binaryReader.ReadInt16();
                ShieldAreaRechargeEnergyRequired = binaryReader.ReadInt16();
                Population = binaryReader.ReadInt32();
                SensorProximityArrayRange = binaryReader.ReadInt32();
                SensorResourceProfileSensorRange = binaryReader.ReadInt32();
                SensorLongRange = binaryReader.ReadInt32();
                WeaponHyperDenyRange = binaryReader.ReadInt32();
                HyperStopRange = binaryReader.ReadInt16();
                MaximumWeaponsRange = binaryReader.ReadInt32();
                MinimumWeaponsRange = binaryReader.ReadInt32();
                IsColony = binaryReader.ReadBoolean();
                IsEnergyCollector = binaryReader.ReadBoolean();
                AllowAutoRetrofit = binaryReader.ReadBoolean();
                ResearchWeapons = binaryReader.ReadInt32();
                ResearchEnergy = binaryReader.ReadInt32();
                ResearchHighTech = binaryReader.ReadInt32();
                ExtractionMine = binaryReader.ReadInt32();
                ExtractionGas = binaryReader.ReadInt32();
                ExtractionLuxury = binaryReader.ReadInt32();
                ManufactureWeapons = binaryReader.ReadInt32();
                ManufactureEnergy = binaryReader.ReadInt32();
                ManufactureHighTech = binaryReader.ReadInt32();
                EnergyCollection = binaryReader.ReadInt32();
                byte b = binaryReader.ReadByte();
                if (b == 0)
                {
                    FuelType = null;
                }
                else
                {
                    FuelType = new Resource((byte)(b - 1));
                }
                FuelCapacity = binaryReader.ReadInt32();
                TopSpeedFuelBurn = binaryReader.ReadInt32();
                CruiseSpeedFuelBurn = binaryReader.ReadInt32();
                WarpSpeedFuelBurn = binaryReader.ReadInt32();
                ImpulseSpeedFuelBurn = binaryReader.ReadInt32();
                AccelerationRate = binaryReader.ReadDouble();
                DockingBayCount = binaryReader.ReadInt32();
                ConstructionYardCount = binaryReader.ReadInt32();
                _IsManuallyCreated = binaryReader.ReadBoolean();
                _HyperDriveIndex = binaryReader.ReadInt32();
                _DamageReduction = binaryReader.ReadDouble();
                _DamageRepair = binaryReader.ReadInt32();
                _Stealth = binaryReader.ReadDouble();
                ImageScalingFactor = binaryReader.ReadSingle();
                ImageScalingType = (DesignImageScalingMode)binaryReader.ReadByte();
                binaryReader.Close();
            }
            foreach (var item in info)
            {
                if (item.Name == "Em")
                { _Empire = (Empire)item.Value; }
                else if (item.Name == "Wps")
                { _Weapons = (WeaponList)item.Value; }
                else if (item.Name == "Cmps")
                { Components = (ComponentList)item.Value; }
                else if (item.Name == "RprTName")
                { RepaitPriorityTemplateName = (string)item.Value; }
            }
            //_Empire = (Empire)info.GetValue("Em", typeof(Empire));
            //_Weapons = (WeaponList)info.GetValue("Wps", typeof(WeaponList));
            //Components = (ComponentList)info.GetValue("Cmps", typeof(ComponentList));
            //RepaitPriorityTemplateName = info.GetString("RprTName");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write((byte)Role);
                binaryWriter.Write((byte)SubRole);
                binaryWriter.Write(_Name);
                binaryWriter.Write(_DateCreated);
                binaryWriter.Write(_TopSpeed);
                binaryWriter.Write(_CruiseSpeed);
                binaryWriter.Write(_WarpSpeed);
                binaryWriter.Write(_HyperjumpInitiate);
                binaryWriter.Write(_MaxFuel);
                binaryWriter.Write(_Firepower);
                binaryWriter.Write(_MaxShields);
                binaryWriter.Write((byte)_Stance);
                binaryWriter.Write((byte)_FleeWhen);
                binaryWriter.Write((byte)_TacticsStrongerShips);
                binaryWriter.Write((byte)_TacticsWeakerShips);
                binaryWriter.Write((byte)_TacticsInvasion);
                binaryWriter.Write(_Price);
                binaryWriter.Write(_CargoCapacity);
                binaryWriter.Write(_Size);
                binaryWriter.Write(_PictureRef);
                binaryWriter.Write(_IsSpacePort);
                binaryWriter.Write(_IsRefuellingDepot);
                binaryWriter.Write(_IsResearchLab);
                binaryWriter.Write(_IsShipYard);
                binaryWriter.Write(_IsResourceExtractor);
                binaryWriter.Write(_IsManufacturer);
                binaryWriter.Write(_IsObsolete);
                binaryWriter.Write(_BuildCount);
                binaryWriter.Write(StaticEnergyConsumption);
                binaryWriter.Write(ReactorPowerOutput);
                binaryWriter.Write(ReactorStorageCapacity);
                binaryWriter.Write(ReactorCycleFuelConsumption);
                binaryWriter.Write(CurrentReactorStorage);
                binaryWriter.Write(TargetSpeed);
                binaryWriter.Write(CurrentSpeed);
                binaryWriter.Write(TroopCapacity);
                binaryWriter.Write(FighterCapacity);
                binaryWriter.Write(Heading);
                binaryWriter.Write(TargetHeading);
                binaryWriter.Write(TurnRate);
                binaryWriter.Write(Armor);
                binaryWriter.Write(ArmorReactive);
                binaryWriter.Write(TargettingModifier);
                binaryWriter.Write(CountermeasureModifier);
                binaryWriter.Write(FleetTargettingModifier);
                binaryWriter.Write(FleetCountermeasureModifier);
                binaryWriter.Write(MaintenanceSavings);
                binaryWriter.Write(TradeBonuses);
                binaryWriter.Write(MedicalCapacity);
                binaryWriter.Write(RecreationCapacity);
                binaryWriter.Write(FirepowerRaw);
                binaryWriter.Write(OptimizedDesign);
                binaryWriter.Write(BombardPower);
                binaryWriter.Write(PointDefenseWeaponsRange);
                binaryWriter.Write(IonDefense);
                binaryWriter.Write(IonWeaponPower);
                binaryWriter.Write(IonWeaponRange);
                binaryWriter.Write(TractorBeamRange);
                binaryWriter.Write(AssaultStrength);
                binaryWriter.Write(AssaultRange);
                binaryWriter.Write(AssaultShieldPenetration);
                binaryWriter.Write(ShieldsCapacity);
                binaryWriter.Write(ShieldRechargeRate);
                binaryWriter.Write(ShieldAreaRechargeRange);
                binaryWriter.Write(ShieldAreaRechargeCapacity);
                binaryWriter.Write(ShieldAreaRechargeEnergyRequired);
                binaryWriter.Write(Population);
                binaryWriter.Write(SensorProximityArrayRange);
                binaryWriter.Write(SensorResourceProfileSensorRange);
                binaryWriter.Write(SensorLongRange);
                binaryWriter.Write(WeaponHyperDenyRange);
                binaryWriter.Write(HyperStopRange);
                binaryWriter.Write(MaximumWeaponsRange);
                binaryWriter.Write(MinimumWeaponsRange);
                binaryWriter.Write(IsColony);
                binaryWriter.Write(IsEnergyCollector);
                binaryWriter.Write(AllowAutoRetrofit);
                binaryWriter.Write(ResearchWeapons);
                binaryWriter.Write(ResearchEnergy);
                binaryWriter.Write(ResearchHighTech);
                binaryWriter.Write(ExtractionMine);
                binaryWriter.Write(ExtractionGas);
                binaryWriter.Write(ExtractionLuxury);
                binaryWriter.Write(ManufactureWeapons);
                binaryWriter.Write(ManufactureEnergy);
                binaryWriter.Write(ManufactureHighTech);
                binaryWriter.Write(EnergyCollection);
                if (FuelType != null)
                {
                    binaryWriter.Write((byte)(FuelType.ResourceID + 1));
                }
                else
                {
                    binaryWriter.Write((byte)0);
                }
                binaryWriter.Write(FuelCapacity);
                binaryWriter.Write(TopSpeedFuelBurn);
                binaryWriter.Write(CruiseSpeedFuelBurn);
                binaryWriter.Write(WarpSpeedFuelBurn);
                binaryWriter.Write(ImpulseSpeedFuelBurn);
                binaryWriter.Write(AccelerationRate);
                binaryWriter.Write(DockingBayCount);
                binaryWriter.Write(ConstructionYardCount);
                binaryWriter.Write(_IsManuallyCreated);
                binaryWriter.Write(_HyperDriveIndex);
                binaryWriter.Write(_DamageReduction);
                binaryWriter.Write(_DamageRepair);
                binaryWriter.Write(_Stealth);
                binaryWriter.Write(ImageScalingFactor);
                binaryWriter.Write((byte)ImageScalingType);
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("D", memoryStream.ToArray());
            }
            info.AddValue("Em", _Empire);
            info.AddValue("Wps", _Weapons);
            info.AddValue("Cmps", Components);
            info.AddValue("RprTName", RepaitPriorityTemplateName);
        }

        public bool IsEquivalent(Design designToCompare)
        {
            if (designToCompare == null)
            {
                return false;
            }
            if (designToCompare.Components.Count != Components.Count)
            {
                return false;
            }
            for (int i = 0; i < designToCompare.Components.Count; i++)
            {
                if (Components.Count > i && designToCompare.Components.Count > i)
                {
                    if (Components[i].ComponentID != designToCompare.Components[i].ComponentID)
                    {
                        return false;
                    }
                    continue;
                }
                return false;
            }
            return true;
        }

        public ComponentList DetermineUniqueUnbuildableComponents(Empire empire)
        {
            ComponentList componentList = new ComponentList();
            if (empire != null && empire.Research != null)
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    Component component = Components[i];
                    if (component != null && !empire.Research.CheckComponentResearched(component) && !componentList.Contains(component))
                    {
                        componentList.Add(component);
                    }
                }
            }
            return componentList;
        }

        public double CalculateTechLevel_OLD(Empire empire, Galaxy galaxy)
        {
            double result = 1.0;
            if (empire != null)
            {
                double num = 0.0;
                double num2 = 0.0;
                for (int i = 0; i < Components.Count; i++)
                {
                    int num3 = 0;
                    num3 = ((empire.Research == null || !empire.Research.CheckComponentResearched(Components[i])) ? 1 : empire.Research.CalculateCurrentTechPoints(Components[i], galaxy));
                    int num4 = num3;
                    if (empire.Research != null && !empire.Research.CheckComponentResearched(Components[i]))
                    {
                        num4 = ResearchSystem.ComponentMaxTechPoints[Components[i].ComponentID];
                    }
                    double val = (double)num4 / (double)num3;
                    val = Math.Max(1.0, val);
                    val -= 1.0;
                    val = Math.Min(val, 1000000.0);
                    if (val > 0.0)
                    {
                        num += (double)Components[i].Size * val;
                        if (val > num2)
                        {
                            num2 = val;
                        }
                    }
                }
                if (num > 0.0)
                {
                    result = 1.0 + num / (double)Size;
                }
            }
            return result;
        }

        public double CalculateTechLevel(Empire empire, Galaxy galaxy)
        {
            double result = 1.0;
            if (empire != null)
            {
                long num = 0L;
                for (int i = 0; i < Components.Count; i++)
                {
                    Component component = Components[i];
                    if (component != null && empire.Research != null)
                    {
                        int num2 = ResearchSystem.ComponentMaxTechPoints[component.ComponentID];
                        num += (long)num2 * (long)component.Size;
                    }
                }
                int num3 = Size;
                if (num3 <= 0)
                {
                    num3 = QuickCalculateSize();
                }
                result = (double)num / (double)num3;
            }
            return result;
        }

        public static double DetermineComponentEnergyRequirementsExcludeHyperdrive(ComponentImprovement component)
        {
            double result = 0.0;
            if (component.ImprovedComponent.Category != ComponentCategoryType.HyperDrive)
            {
                result = DetermineComponentEnergyRequirements(component);
            }
            return result;
        }

        public static double DetermineComponentEnergyRequirements(ComponentImprovement component)
        {
            //double num = 0.0;
            switch (component.ImprovedComponent.Type)
            {
                case ComponentType.WeaponBeam:
                case ComponentType.WeaponTorpedo:
                case ComponentType.WeaponBombard:
                case ComponentType.WeaponMissile:
                case ComponentType.WeaponPointDefense:
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                case ComponentType.WeaponTractorBeam:
                case ComponentType.WeaponGravityBeam:
                case ComponentType.WeaponAreaGravity:
                case ComponentType.HyperDeny:
                case ComponentType.WeaponAreaDestruction:
                case ComponentType.WeaponSuperBeam:
                case ComponentType.WeaponSuperArea:
                case ComponentType.WeaponPhaser:
                case ComponentType.WeaponRailGun:
                case ComponentType.WeaponSuperTorpedo:
                case ComponentType.WeaponSuperMissile:
                case ComponentType.WeaponSuperPhaser:
                case ComponentType.WeaponSuperRailGun:
                    if (component.Value6 > 0)
                    {
                        return CalculateWeaponEnergyUsePerSecond(component);
                    }
                    return component.ImprovedComponent.EnergyUsed;
                case ComponentType.EngineMainThrust:
                    return component.Value4;
                case ComponentType.EngineVectoring:
                    return component.Value2;
                case ComponentType.HyperDrive:
                    return component.Value2;
                default:
                    return component.ImprovedComponent.EnergyUsed;
            }
        }

        public static double CalculateStaticEnergyUsage(ComponentList components)
        {
            double num = 0.0;
            if (components != null)
            {
                for (int i = 0; i < components.Count; i++)
                {
                    Component component = components[i];
                    if (component != null)
                    {
                        num += (double)component.EnergyUsed;
                    }
                }
            }
            return num;
        }

        public static double DetermineComponentEnergyOutput(ComponentImprovement component)
        {
            double result = 0.0;
            ComponentCategoryType category = component.ImprovedComponent.Category;
            if (category == ComponentCategoryType.Reactor)
            {
                result = component.Value1;
            }
            return result;
        }

        public static double CalculateTotalWeaponsEnergyUsePerSecond(Design design, ResearchSystem research)
        {
            double num = 0.0;
            if (design != null && research != null && design.Weapons != null && design.Weapons.Count > 0)
            {
                for (int i = 0; i < design.Weapons.Count; i++)
                {
                    if (design.Weapons[i].Component != null)
                    {
                        ComponentImprovement weaponComponent = research.ResolveImprovedComponentValues(design.Weapons[i].Component);
                        num += CalculateWeaponEnergyUsePerSecond(weaponComponent);
                    }
                }
            }
            return num;
        }

        public static double CalculateWeaponEnergyUsePerSecond(ComponentImprovement weaponComponent)
        {
            double result = 0.0;
            if (weaponComponent.Value6 > 0)
            {
                result = (double)weaponComponent.Value3 / ((double)weaponComponent.Value6 / 1000.0);
            }
            return result;
        }

        public static int GetLeaderMaintenanceBonuses(Design design, Empire empire)
        {
            int num = 0;
            if (empire != null)
            {
                switch (design.Role)
                {
                    case BuiltObjectRole.Military:
                        if (empire.Leader != null)
                        {
                            num += empire.Leader.MilitaryShipMaintenance;
                        }
                        break;
                    case BuiltObjectRole.Base:
                        switch (design.SubRole)
                        {
                            case BuiltObjectSubRole.Outpost:
                            case BuiltObjectSubRole.SmallSpacePort:
                            case BuiltObjectSubRole.MediumSpacePort:
                            case BuiltObjectSubRole.LargeSpacePort:
                            case BuiltObjectSubRole.DefensiveBase:
                                if (empire.Leader != null)
                                {
                                    num += empire.Leader.MilitaryBaseMaintenance;
                                }
                                break;
                            default:
                                if (empire.Leader != null)
                                {
                                    num += empire.Leader.CivilianBaseMaintenance;
                                }
                                break;
                        }
                        break;
                    default:
                        if (empire.Leader != null)
                        {
                            num += empire.Leader.CivilianShipMaintenance;
                        }
                        break;
                }
            }
            return num;
        }

        public double CalculateMaintenanceCosts(Galaxy galaxy, Empire empire)
        {
            return BaconDesign.CalculateMaintenanceCosts(this, galaxy, empire);
        }

        public double CalculateCurrentPurchasePrice(Galaxy galaxy)
        {
            double num = 0.0;
            for (int i = 0; i < Components.Count; i++)
            {
                num += galaxy.ComponentCurrentPrices[Components[i].ComponentID];
            }
            if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
            {
                return num * Galaxy.ShipMarkupFactorPirates;
            }
            return num * Galaxy.ShipMarkupFactor;
        }

        public int QuickCalculateSize()
        {
            int num = 0;
            for (int i = 0; i < Components.Count; i++)
            {
                Component component = Components[i];
                if (component != null)
                {
                    num += component.Size;
                }
            }
            return num;
        }

        public double FuelUnitPerEnergyUnit()
        {
            return (double)ReactorCycleFuelConsumption / 1000.0 / (double)ReactorStorageCapacity;
        }

        public double MaximumRange()
        {
            double num = FuelUnitPerEnergyUnit();
            //double num2 = 0.0;
            if (WarpSpeed > 0)
            {
                return (double)FuelCapacity / (((double)WarpSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)WarpSpeed;
            }
            return (double)FuelCapacity / (((double)CruiseSpeedFuelBurn + (double)StaticEnergyConsumption) * num) * (double)CruiseSpeed;
        }

        public int CalculateBoardingAssaultValue(Race dominantEmpireRace)
        {
            int num = 0;
            double num2 = 1.0;
            if (dominantEmpireRace != null)
            {
                num2 = (double)dominantEmpireRace.TroopStrength / 100.0;
            }
            if (Weapons != null)
            {
                for (int i = 0; i < Weapons.Count; i++)
                {
                    Weapon weapon = Weapons[i];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod)
                    {
                        num += (int)((double)weapon.RawDamage * num2);
                    }
                }
            }
            return num;
        }

        public int CalculateBoardingDefenseValue(Race dominantEmpireRace)
        {
            int num = 0;
            double num2 = 1.0;
            if (dominantEmpireRace != null)
            {
                num2 = (double)dominantEmpireRace.TroopStrength / 100.0;
            }
            if (Components != null)
            {
                for (int i = 0; i < Components.Count; i++)
                {
                    Component component = Components[i];
                    if (component != null)
                    {
                        ComponentType type = component.Type;
                        if (type == ComponentType.HabitationHabModule)
                        {
                            num += (int)(20.0 * num2);
                        }
                    }
                }
            }
            if (Weapons != null)
            {
                for (int j = 0; j < Weapons.Count; j++)
                {
                    Weapon weapon = Weapons[j];
                    if (weapon != null && weapon.Component != null && weapon.Component.Type == ComponentType.AssaultPod)
                    {
                        num += (int)((double)weapon.RawDamage * num2);
                    }
                }
            }
            return num;
        }

        public void ReDefine()
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            ReactorPowerOutput = 0;
            ReactorStorageCapacity = 0;
            ReactorCycleFuelConsumption = 0;
            StaticEnergyConsumption = 0;
            SensorProximityArrayRange = 0;
            SensorResourceProfileSensorRange = 0;
            SensorLongRange = 0;
            WeaponHyperDenyRange = 0;
            HyperStopRange = 0;
            MaximumWeaponsRange = 0;
            MinimumWeaponsRange = 100000;
            PointDefenseWeaponsRange = 0;
            IonDefense = 0;
            IonWeaponPower = 0;
            IonWeaponRange = 0;
            TractorBeamRange = 0;
            AssaultStrength = 0;
            AssaultRange = 0;
            AssaultShieldPenetration = 0;
            Armor = 0;
            ArmorReactive = 0;
            DockingBayCount = 0;
            ConstructionYardCount = 0;
            _Stealth = 1.0;
            _DamageReduction = 0.0;
            IsColony = false;
            IsEnergyCollector = false;
            IsManufacturer = false;
            IsRefuellingDepot = false;
            IsResearchLab = false;
            IsResourceExtractor = false;
            IsShipYard = false;
            IsSpacePort = false;
            ResearchWeapons = 0;
            ResearchEnergy = 0;
            ResearchHighTech = 0;
            ExtractionMine = 0;
            ExtractionGas = 0;
            ExtractionLuxury = 0;
            ManufactureWeapons = 0;
            ManufactureEnergy = 0;
            ManufactureHighTech = 0;
            EnergyCollection = 0;
            MedicalCapacity = 0;
            RecreationCapacity = 0;
            FleetCountermeasureModifier = 0;
            CountermeasureModifier = 0;
            FleetTargettingModifier = 0;
            TargettingModifier = 0;
            MaintenanceSavings = 0.0;
            TradeBonuses = 0.0;
            ShieldAreaRechargeRange = 0;
            ShieldAreaRechargeCapacity = 0;
            ShieldAreaRechargeEnergyRequired = 0;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            double num10 = 0.0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            int num21 = 0;
            int num22 = int.MaxValue;
            int num23 = 0;
            int num24 = 0;
            int num25 = 0;
            int num26 = 0;
            int num27 = 0;
            Weapons.Clear();
            int num28 = 0;
            for (int i = 0; i < Components.Count; i++)
            {
                num23 += Components[i].Size;
                StaticEnergyConsumption += Components[i].EnergyUsed;
                ComponentImprovement componentImprovement = null;
                componentImprovement = ((Empire == null || Empire.Research == null) ? new ComponentImprovement(Components[i]) : Empire.Research.ResolveImprovedComponentValues(Components[i]));
                switch (componentImprovement.ImprovedComponent.Category)
                {
                    case ComponentCategoryType.AssaultPod:
                        {
                            if (componentImprovement.ImprovedComponent.Type != ComponentType.AssaultPod)
                            {
                                break;
                            }
                            AssaultStrength += (short)componentImprovement.Value1;
                            if (componentImprovement.Value2 > 0)
                            {
                                if (AssaultRange > 0)
                                {
                                    AssaultRange = Math.Min(AssaultRange, (short)componentImprovement.Value2);
                                }
                                else
                                {
                                    AssaultRange = (short)componentImprovement.Value2;
                                }
                            }
                            if (componentImprovement.Value5 > 0)
                            {
                                if (AssaultShieldPenetration > 0)
                                {
                                    AssaultShieldPenetration = Math.Max(AssaultShieldPenetration, (short)componentImprovement.Value5);
                                }
                                else
                                {
                                    AssaultShieldPenetration = (short)componentImprovement.Value5;
                                }
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentCategoryType.Fighter:
                        if (componentImprovement.ImprovedComponent.Type == ComponentType.FighterBay)
                        {
                            num3 += componentImprovement.Value1;
                        }
                        break;
                    case ComponentCategoryType.Engine:
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.EngineMainThrust:
                                num14 += componentImprovement.Value1;
                                num16 += componentImprovement.Value2;
                                num15 += componentImprovement.Value3;
                                num17 += componentImprovement.Value4;
                                break;
                            case ComponentType.EngineVectoring:
                                num18 += componentImprovement.Value1;
                                num19 += componentImprovement.Value2;
                                break;
                        }
                        break;
                    case ComponentCategoryType.HyperDrive:
                        if (componentImprovement.Value1 > num20)
                        {
                            num20 = componentImprovement.Value1;
                        }
                        if (componentImprovement.Value2 > num21)
                        {
                            num21 = componentImprovement.Value2;
                        }
                        if (componentImprovement.Value3 < num22)
                        {
                            num22 = componentImprovement.Value3;
                        }
                        _HyperDriveIndex = Galaxy.ResolveHyperDriveIndex(componentImprovement.ImprovedComponent);
                        break;
                    case ComponentCategoryType.HyperDisrupt:
                        if (componentImprovement.ImprovedComponent.Type == ComponentType.HyperDeny)
                        {
                            if (componentImprovement.Value2 > WeaponHyperDenyRange)
                            {
                                WeaponHyperDenyRange = componentImprovement.Value2;
                            }
                        }
                        else if (componentImprovement.ImprovedComponent.Type == ComponentType.HyperStop && componentImprovement.Value2 > HyperStopRange)
                        {
                            HyperStopRange = (short)componentImprovement.Value2;
                        }
                        break;
                    case ComponentCategoryType.ShieldRecharge:
                        if ((short)componentImprovement.Value1 > ShieldAreaRechargeRange)
                        {
                            ShieldAreaRechargeRange = (short)componentImprovement.Value1;
                            ShieldAreaRechargeCapacity = (short)componentImprovement.Value2;
                            ShieldAreaRechargeEnergyRequired = (short)componentImprovement.Value3;
                        }
                        break;
                    case ComponentCategoryType.Shields:
                        num9 += componentImprovement.Value1;
                        num10 += (double)componentImprovement.Value2 / 10.0;
                        break;
                    case ComponentCategoryType.Reactor:
                        ReactorPowerOutput += componentImprovement.Value1;
                        ReactorStorageCapacity += componentImprovement.Value2;
                        ReactorCycleFuelConsumption += componentImprovement.Value3;
                        FuelType = new Resource((byte)componentImprovement.Value4);
                        break;
                    case ComponentCategoryType.WeaponPointDefense:
                        {
                            if (componentImprovement.Value1 > 0 && componentImprovement.Value2 > PointDefenseWeaponsRange)
                            {
                                PointDefenseWeaponsRange = componentImprovement.Value2;
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentCategoryType.WeaponArea:
                    case ComponentCategoryType.WeaponSuperArea:
                        {
                            Weapon item = new Weapon(componentImprovement);
                            if (item.RawDamage > 0)
                            {
                                num5 += componentImprovement.Value1;
                                num6 += (int)(((double)componentImprovement.Value1 - (double)componentImprovement.Value2 / 100.0 * (double)componentImprovement.Value5 / 2.0) * (double)componentImprovement.Value2 * (1000.0 / (double)componentImprovement.Value6));
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            if (item.BombardDamage > 0)
                            {
                                num7 += item.BombardDamage;
                            }
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentCategoryType.WeaponBeam:
                    case ComponentCategoryType.WeaponSuperBeam:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                num5 += componentImprovement.Value1;
                                num6 += (int)(((double)componentImprovement.Value1 - (double)componentImprovement.Value2 / 100.0 * (double)componentImprovement.Value5 / 2.0) * (double)componentImprovement.Value2 * (1000.0 / (double)componentImprovement.Value6));
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            if (componentImprovement.Value7 > 0)
                            {
                                num7 += componentImprovement.Value7;
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentCategoryType.WeaponTorpedo:
                    case ComponentCategoryType.WeaponSuperTorpedo:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                num5 += componentImprovement.Value1;
                                num6 += (int)(((double)componentImprovement.Value1 - (double)componentImprovement.Value2 / 100.0 * (double)componentImprovement.Value5 / 2.0) * (double)componentImprovement.Value2 * (1000.0 / (double)componentImprovement.Value6));
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            if (componentImprovement.Value7 > 0)
                            {
                                num7 += componentImprovement.Value7;
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentCategoryType.Labs:
                        flag9 = true;
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.LabsEnergyLab:
                                ResearchEnergy += componentImprovement.Value1;
                                break;
                            case ComponentType.LabsHighTechLab:
                                ResearchHighTech += componentImprovement.Value1;
                                break;
                            case ComponentType.LabsWeaponsLab:
                                ResearchWeapons += componentImprovement.Value1;
                                break;
                        }
                        break;
                    case ComponentCategoryType.Extractor:
                        flag11 = true;
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.ExtractorGasExtractor:
                                ExtractionGas += componentImprovement.Value1;
                                break;
                            case ComponentType.ExtractorMine:
                                ExtractionMine += componentImprovement.Value1;
                                break;
                            case ComponentType.ExtractorLuxury:
                                ExtractionLuxury += componentImprovement.Value1;
                                break;
                        }
                        break;
                    case ComponentCategoryType.Manufacturer:
                        flag12 = true;
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.ManufacturerEnergyPlant:
                                ManufactureEnergy += componentImprovement.Value1;
                                break;
                            case ComponentType.ManufacturerHighTechPlant:
                                ManufactureHighTech += componentImprovement.Value1;
                                break;
                            case ComponentType.ManufacturerWeaponsPlant:
                                ManufactureWeapons += componentImprovement.Value1;
                                break;
                        }
                        break;
                    case ComponentCategoryType.EnergyCollector:
                        {
                            flag13 = true;
                            ComponentType type2 = componentImprovement.ImprovedComponent.Type;
                            if (type2 == ComponentType.EnergyCollector)
                            {
                                EnergyCollection += componentImprovement.Value1;
                            }
                            break;
                        }
                    case ComponentCategoryType.Armor:
                        {
                            ComponentType type = componentImprovement.ImprovedComponent.Type;
                            if (type == ComponentType.Armor)
                            {
                                Armor += componentImprovement.Value1;
                                if (componentImprovement.Value2 > ArmorReactive)
                                {
                                    ArmorReactive = componentImprovement.Value2;
                                }
                            }
                            break;
                        }
                    case ComponentCategoryType.Construction:
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.ConstructionBuild:
                                ConstructionYardCount++;
                                break;
                            case ComponentType.DamageControl:
                                if (componentImprovement.Value1 > num26)
                                {
                                    num26 = componentImprovement.Value1;
                                }
                                if (componentImprovement.Value2 > 0 && (num28 == 0 || componentImprovement.Value2 < num28))
                                {
                                    num28 = componentImprovement.Value2;
                                }
                                break;
                        }
                        break;
                    case ComponentCategoryType.Computer:
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.ComputerTargettingFleet:
                                if (componentImprovement.Value1 > TargettingModifier)
                                {
                                    TargettingModifier = (short)componentImprovement.Value1;
                                }
                                if (componentImprovement.Value2 > FleetTargettingModifier)
                                {
                                    FleetTargettingModifier = (short)componentImprovement.Value2;
                                }
                                break;
                            case ComponentType.ComputerTargetting:
                                if (componentImprovement.Value1 > TargettingModifier)
                                {
                                    TargettingModifier = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.ComputerCountermeasuresFleet:
                                if (componentImprovement.Value1 > CountermeasureModifier)
                                {
                                    CountermeasureModifier = (short)componentImprovement.Value1;
                                }
                                if (componentImprovement.Value2 > FleetCountermeasureModifier)
                                {
                                    FleetCountermeasureModifier = (short)componentImprovement.Value2;
                                }
                                break;
                            case ComponentType.ComputerCountermeasures:
                                if (componentImprovement.Value1 > CountermeasureModifier)
                                {
                                    CountermeasureModifier = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.ComputerCommandCenter:
                                {
                                    double num30 = (double)componentImprovement.Value1 / 100.0;
                                    if (num30 > MaintenanceSavings)
                                    {
                                        MaintenanceSavings = num30;
                                    }
                                    break;
                                }
                            case ComponentType.ComputerCommerceCenter:
                                {
                                    double num29 = (double)componentImprovement.Value1 / 1000.0;
                                    if (num29 > TradeBonuses)
                                    {
                                        TradeBonuses = num29;
                                    }
                                    break;
                                }
                        }
                        break;
                    case ComponentCategoryType.Sensor:
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.SensorStealth:
                                if (componentImprovement.Value1 > num27)
                                {
                                    num27 = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.SensorProximityArray:
                                if (componentImprovement.Value1 > SensorProximityArrayRange)
                                {
                                    SensorProximityArrayRange = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.SensorResourceProfileSensor:
                                if (componentImprovement.Value1 > SensorResourceProfileSensorRange)
                                {
                                    SensorResourceProfileSensorRange = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.SensorLongRange:
                                if (componentImprovement.Value1 > SensorLongRange)
                                {
                                    SensorLongRange = componentImprovement.Value1;
                                }
                                break;
                        }
                        break;
                    case ComponentCategoryType.Habitation:
                        switch (componentImprovement.ImprovedComponent.Type)
                        {
                            case ComponentType.HabitationHabModule:
                                num25 += componentImprovement.Value1;
                                break;
                            case ComponentType.HabitationLifeSupport:
                                num24 += componentImprovement.Value1;
                                break;
                            case ComponentType.HabitationMedicalCenter:
                                if (componentImprovement.Value1 > MedicalCapacity)
                                {
                                    MedicalCapacity = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.HabitationRecreationCenter:
                                if (componentImprovement.Value1 > RecreationCapacity)
                                {
                                    RecreationCapacity = componentImprovement.Value1;
                                }
                                break;
                            case ComponentType.HabitationColonization:
                                IsColony = true;
                                break;
                        }
                        break;
                }
                switch (componentImprovement.ImprovedComponent.Type)
                {
                    case ComponentType.WeaponTractorBeam:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 > TractorBeamRange)
                                {
                                    TractorBeamRange = (short)componentImprovement.Value2;
                                }
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentType.WeaponGravityBeam:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                num5 += componentImprovement.Value1;
                                num6 += (int)(((double)componentImprovement.Value1 - (double)componentImprovement.Value2 / 100.0 * (double)componentImprovement.Value5 / 2.0) * (double)componentImprovement.Value2 * (1000.0 / (double)componentImprovement.Value6));
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentType.WeaponAreaGravity:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                num5 += componentImprovement.Value1;
                                num6 += (int)(((double)componentImprovement.Value1 - (double)componentImprovement.Value2 / 100.0 * (double)componentImprovement.Value5 / 2.0) * (double)componentImprovement.Value2 * (1000.0 / (double)componentImprovement.Value6));
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentType.WeaponIonDefense:
                        if (componentImprovement.Value1 > IonDefense)
                        {
                            IonDefense = componentImprovement.Value1;
                        }
                        break;
                    case ComponentType.WeaponIonPulse:
                        {
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            if (item.RawDamage > 0)
                            {
                                num8 += componentImprovement.Value1;
                                num5 += componentImprovement.Value1;
                                if (componentImprovement.Value2 > IonWeaponRange)
                                {
                                    IonWeaponRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            break;
                        }
                    case ComponentType.WeaponIonCannon:
                        {
                            if (componentImprovement.Value1 > 0)
                            {
                                num8 += componentImprovement.Value1;
                                num5 += componentImprovement.Value1;
                                if (componentImprovement.Value2 > IonWeaponRange)
                                {
                                    IonWeaponRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 > MaximumWeaponsRange)
                                {
                                    MaximumWeaponsRange = componentImprovement.Value2;
                                }
                                if (componentImprovement.Value2 < MinimumWeaponsRange)
                                {
                                    MinimumWeaponsRange = componentImprovement.Value2;
                                }
                            }
                            Weapon item = new Weapon(componentImprovement);
                            Weapons.Add(item);
                            break;
                        }
                    case ComponentType.ConstructionBuild:
                        flag10 = true;
                        break;
                    case ComponentType.ComputerCommerceCenter:
                        flag2 = true;
                        break;
                    case ComponentType.StorageFuel:
                        flag3 = true;
                        num4 += componentImprovement.Value1;
                        break;
                    case ComponentType.StorageDockingBay:
                        flag4 = true;
                        DockingBayCount++;
                        break;
                    case ComponentType.StorageCargo:
                        flag5 = true;
                        num += componentImprovement.Value1;
                        break;
                    case ComponentType.StorageTroop:
                        num2 += componentImprovement.Value1;
                        break;
                    case ComponentType.ComputerCommandCenter:
                        flag6 = true;
                        break;
                    case ComponentType.HabitationLifeSupport:
                        flag7 = true;
                        break;
                    case ComponentType.HabitationHabModule:
                        flag8 = true;
                        break;
                }
            }
            if (flag6 && flag7 && flag8)
            {
                flag = true;
            }
            if (flag && flag3 && flag4)
            {
                IsRefuellingDepot = true;
            }
            if (IsRefuellingDepot && flag2 && flag5)
            {
                IsSpacePort = true;
            }
            if (flag && flag9)
            {
                IsResearchLab = true;
            }
            if (flag && flag11)
            {
                IsResourceExtractor = true;
            }
            if (flag && flag12)
            {
                IsManufacturer = true;
            }
            if (flag && flag10 && flag12)
            {
                IsShipYard = true;
            }
            if (flag && flag13)
            {
                IsEnergyCollector = true;
            }
            if (IsResourceExtractor)
            {
                _ = Role;
                _ = 8;
            }
            if (num27 > 0)
            {
                double num31 = (double)num27 / (double)num23;
                _Stealth = Math.Min(1.0, 0.5 / num31);
            }
            if (num26 > 0)
            {
                _DamageReduction = (double)num26 / 1000.0;
            }
            double num32 = (double)ReactorPowerOutput - (double)StaticEnergyConsumption;
            num13 = num20;
            num11 = num14 / Math.Max(1, num23);
            num12 = num15 / Math.Max(1, num23);
            double num33;
            double num34;
            double num35;
            if (ReactorPowerOutput > 0)
            {
                num33 = (double)num21 / num32;
                num34 = (double)num16 / num32;
                num35 = (double)num17 / num32;
            }
            else
            {
                num33 = 1000000.0;
                num34 = 1000000.0;
                num35 = 1000000.0;
            }
            if (num33 > 1.0)
            {
                num13 = (int)((double)num13 / num33);
                num21 = (int)((double)num21 / num33);
            }
            if (num34 > 1.0)
            {
                num11 = (int)((double)num11 / num34);
                num16 = (int)((double)num16 / num34);
            }
            if (num35 > 1.0)
            {
                num12 = (int)((double)num12 / num35);
                num17 = (int)((double)num17 / num35);
            }
            if (MinimumWeaponsRange >= 100000)
            {
                MinimumWeaponsRange = 0;
            }
            Size = num23;
            _CargoCapacity = num;
            TroopCapacity = num2;
            FighterCapacity = num3;
            FuelCapacity = num4;
            FirepowerRaw = num5;
            BombardPower = num7;
            IonWeaponPower = num8;
            ShieldsCapacity = num9;
            ShieldRechargeRate = num10;
            Population = Math.Min(num25, num24);
            TopSpeed = num11;
            TopSpeedFuelBurn = num16;
            CruiseSpeed = num12;
            CruiseSpeedFuelBurn = num17;
            WarpSpeed = num13;
            WarpSpeedFuelBurn = num21;
            ImpulseSpeedFuelBurn = CruiseSpeedFuelBurn / 4;
            HyperjumpInitiate = Math.Min(15, num22);
            TurnRate = 0.1 + (double)num18 * 2.0 / (double)num23;
            double num36 = (double)ReactorPowerOutput - (double)StaticEnergyConsumption;
            double num37 = num36 / (double)num16;
            if (num37 > 1.0)
            {
                num37 = Math.Sqrt(Math.Sqrt(num37));
                num37 = Math.Min(num37, 2.0);
            }
            AccelerationRate = ((double)num11 / 8.0 + 0.5) * num37;
            _DamageRepair = num28;
            BaconDesign.Redefine(this);
        }

        public Design Clone()
        {
            Design design = new Design(Name);
            design.ImageScalingType = ImageScalingType;
            design.ImageScalingFactor = ImageScalingFactor;
            design.DateCreated = DateCreated;
            design.Empire = Empire;
            design.Role = Role;
            design.SubRole = SubRole;
            design.Stance = Stance;
            design.FleeWhen = FleeWhen;
            design.TacticsStrongerShips = TacticsStrongerShips;
            design.TacticsWeakerShips = TacticsWeakerShips;
            design.TacticsInvasion = TacticsInvasion;
            design.PictureRef = PictureRef;
            design.BuildCount = BuildCount;
            design.Components = Components.Clone();
            design.IsObsolete = IsObsolete;
            design.IsManuallyCreated = IsManuallyCreated;
            design.AllowAutoRetrofit = AllowAutoRetrofit;
            design.RepaitPriorityTemplateName = RepaitPriorityTemplateName;
            design.ReDefine();
            return design;
        }

        int IComparable<Design>.CompareTo(Design other)
        {
            int num = SubRole.CompareTo(other.SubRole);
            if (num == 0)
            {
                return Name.CompareTo(other.Name);
            }
            return num;
        }
    }
}
