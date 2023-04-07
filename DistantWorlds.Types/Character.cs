// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Character
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Character : IComparable<Character>, ISerializable
    {
        public string Name;

        public Race Race;

        private string _PictureFilename;

        private long _StartDate;

        private long _EndDate;

        private bool _Active;

        private Empire _Empire;

        private IntelligenceMission _Mission;

        private int _AppearanceOrder;

        private StellarObject _Location;

        private StellarObject _TransferDestination;

        private float _TransferTimeRemaining;

        private long _TransferArrivalDate = long.MaxValue;

        private DateTime _LastTouch;

        public CharacterRole Role;

        public CharacterSkillList Skills = new CharacterSkillList();

        public CharacterSkillList TraitSkills = new CharacterSkillList();

        public List<CharacterTraitType> Traits = new List<CharacterTraitType>();

        public CharacterEventList EventHistory = new CharacterEventList();

        public bool BonusesKnown;

        private sbyte _Diplomacy;

        private sbyte _ColonyIncome;

        private sbyte _TradeIncome;

        private sbyte _TourismIncome;

        private sbyte _ColonyCorruption;

        private sbyte _ColonyHappiness;

        private sbyte _PopulationGrowth;

        private sbyte _MiningRate;

        private sbyte _TroopRecruitmentRate;

        private sbyte _MilitaryShipConstructionSpeed;

        private sbyte _CivilianShipConstructionSpeed;

        private sbyte _ColonyShipConstructionSpeed;

        private sbyte _FacilityConstructionSpeed;

        private sbyte _ResearchWeapons;

        private sbyte _ResearchEnergy;

        private sbyte _ResearchHighTech;

        private sbyte _Espionage;

        private sbyte _CounterEspionage;

        private sbyte _Sabotage;

        private sbyte _Concealment;

        private sbyte _PsyOps;

        private sbyte _Assassination;

        private sbyte _MilitaryShipMaintenance;

        private sbyte _CivilianShipMaintenance;

        private sbyte _MilitaryBaseMaintenance;

        private sbyte _CivilianBaseMaintenance;

        private sbyte _TroopMaintenance;

        private sbyte _WarWeariness;

        private sbyte _Targeting;

        private sbyte _Countermeasures;

        private sbyte _ShipManeuvering;

        private sbyte _Fighters;

        private sbyte _ShipEnergyUsage;

        private sbyte _WeaponsDamage;

        private sbyte _WeaponsRange;

        private sbyte _ShieldRechargeRate;

        private sbyte _DamageControl;

        private sbyte _RepairBonus;

        private sbyte _HyperjumpSpeed;

        private sbyte _TroopGroundAttack;

        private sbyte _TroopGroundDefense;

        private sbyte _TroopExperienceGain;

        private sbyte _TroopRecoveryRate;

        private sbyte _TroopStrengthArmor;

        private sbyte _TroopStrengthInfantry;

        private sbyte _TroopStrengthSpecialForces;

        private sbyte _TroopStrengthPlanetaryDefense;

        private sbyte _SmugglingIncome;

        private sbyte _SmugglingEvasion;

        private sbyte _BoardingAssault;

        private sbyte _DiplomacyTraits;

        private sbyte _ColonyIncomeTraits;

        private sbyte _TradeIncomeTraits;

        private sbyte _TourismIncomeTraits;

        private sbyte _ColonyCorruptionTraits;

        private sbyte _ColonyHappinessTraits;

        private sbyte _PopulationGrowthTraits;

        private sbyte _MiningRateTraits;

        private sbyte _TroopRecruitmentRateTraits;

        private sbyte _MilitaryShipConstructionSpeedTraits;

        private sbyte _CivilianShipConstructionSpeedTraits;

        private sbyte _ColonyShipConstructionSpeedTraits;

        private sbyte _FacilityConstructionSpeedTraits;

        private sbyte _ResearchWeaponsTraits;

        private sbyte _ResearchEnergyTraits;

        private sbyte _ResearchHighTechTraits;

        private sbyte _EspionageTraits;

        private sbyte _CounterEspionageTraits;

        private sbyte _SabotageTraits;

        private sbyte _ConcealmentTraits;

        private sbyte _PsyOpsTraits;

        private sbyte _AssassinationTraits;

        private sbyte _MilitaryShipMaintenanceTraits;

        private sbyte _CivilianShipMaintenanceTraits;

        private sbyte _MilitaryBaseMaintenanceTraits;

        private sbyte _CivilianBaseMaintenanceTraits;

        private sbyte _TroopMaintenanceTraits;

        private sbyte _WarWearinessTraits;

        private sbyte _TargetingTraits;

        private sbyte _CountermeasuresTraits;

        private sbyte _ShipManeuveringTraits;

        private sbyte _FightersTraits;

        private sbyte _ShipEnergyUsageTraits;

        private sbyte _WeaponsDamageTraits;

        private sbyte _WeaponsRangeTraits;

        private sbyte _ShieldRechargeRateTraits;

        private sbyte _DamageControlTraits;

        private sbyte _RepairBonusTraits;

        private sbyte _HyperjumpSpeedTraits;

        private sbyte _TroopGroundAttackTraits;

        private sbyte _TroopGroundDefenseTraits;

        private sbyte _TroopExperienceGainTraits;

        private sbyte _TroopRecoveryRateTraits;

        private sbyte _TroopStrengthArmorTraits;

        private sbyte _TroopStrengthInfantryTraits;

        private sbyte _TroopStrengthSpecialForcesTraits;

        private sbyte _TroopStrengthPlanetaryDefenseTraits;

        private sbyte _SmugglingIncomeTraits;

        private sbyte _SmugglingEvasionTraits;

        private sbyte _BoardingAssaultTraits;

        public long TransferArrivalDate => _TransferArrivalDate;

        public int Diplomacy => _Diplomacy + _DiplomacyTraits;

        public int ColonyIncome => _ColonyIncome + _ColonyIncomeTraits;

        public int TradeIncome => _TradeIncome + _TradeIncomeTraits;

        public int TourismIncome => _TourismIncome + _TourismIncomeTraits;

        public int ColonyCorruption => _ColonyCorruption + _ColonyCorruptionTraits;

        public int ColonyHappiness => _ColonyHappiness + _ColonyHappinessTraits;

        public int PopulationGrowth => _PopulationGrowth + _PopulationGrowthTraits;

        public int MiningRate => _MiningRate + _MiningRateTraits;

        public int TroopRecruitmentRate => _TroopRecruitmentRate + _TroopRecruitmentRateTraits;

        public int MilitaryShipConstructionSpeed => _MilitaryShipConstructionSpeed + _MilitaryShipConstructionSpeedTraits;

        public int CivilianShipConstructionSpeed => _CivilianShipConstructionSpeed + _CivilianShipConstructionSpeedTraits;

        public int ColonyShipConstructionSpeed => _ColonyShipConstructionSpeed + _ColonyShipConstructionSpeedTraits;

        public int FacilityConstructionSpeed => _FacilityConstructionSpeed + _FacilityConstructionSpeedTraits;

        public int ResearchWeapons => _ResearchWeapons + _ResearchWeaponsTraits;

        public int ResearchEnergy => _ResearchEnergy + _ResearchEnergyTraits;

        public int ResearchHighTech => _ResearchHighTech + _ResearchHighTechTraits;

        public int Espionage => _Espionage + _EspionageTraits;

        public int EspionageFactored => (int)(25.0 * (1.0 + (double)Espionage / 100.0 * 3.0));

        public int CounterEspionage => _CounterEspionage + _CounterEspionageTraits;

        public int CounterEspionageFactored => (int)(25.0 * (1.0 + (double)CounterEspionage / 100.0 * 3.0));

        public int Sabotage => _Sabotage + _SabotageTraits;

        public int SabotageFactored => (int)(25.0 * (1.0 + (double)Sabotage / 100.0 * 3.0));

        public int Concealment => _Concealment + _ConcealmentTraits;

        public int ConcealmentFactored => (int)(25.0 * (1.0 + (double)Concealment / 100.0 * 3.0));

        public int PsyOps => _PsyOps + _PsyOpsTraits;

        public int PsyOpsFactored => (int)(25.0 * (1.0 + (double)PsyOps / 100.0 * 3.0));

        public int Assassination => _Assassination + _AssassinationTraits;

        public int AssassinationFactored => (int)(25.0 * (1.0 + (double)Assassination / 100.0 * 3.0));

        public int MilitaryShipMaintenance => _MilitaryShipMaintenance + _MilitaryShipMaintenanceTraits;

        public int CivilianShipMaintenance => _CivilianShipMaintenance + _CivilianShipMaintenanceTraits;

        public int MilitaryBaseMaintenance => _MilitaryBaseMaintenance + _MilitaryBaseMaintenanceTraits;

        public int CivilianBaseMaintenance => _CivilianBaseMaintenance + _CivilianBaseMaintenanceTraits;

        public int TroopMaintenance => _TroopMaintenance + _TroopMaintenanceTraits;

        public int WarWeariness => _WarWeariness + _WarWearinessTraits;

        public int Targeting => _Targeting + _TargetingTraits;

        public int Countermeasures => _Countermeasures + _CountermeasuresTraits;

        public int ShipManeuvering => _ShipManeuvering + _ShipManeuveringTraits;

        public int Fighters => _Fighters + _FightersTraits;

        public int ShipEnergyUsage => _ShipEnergyUsage + _ShipEnergyUsageTraits;

        public int WeaponsDamage => _WeaponsDamage + _WeaponsDamageTraits;

        public int WeaponsRange => _WeaponsRange + _WeaponsRangeTraits;

        public int ShieldRechargeRate => _ShieldRechargeRate + _ShieldRechargeRateTraits;

        public int DamageControl => _DamageControl + _DamageControlTraits;

        public int RepairBonus => _RepairBonus + _RepairBonusTraits;

        public int HyperjumpSpeed => _HyperjumpSpeed + _HyperjumpSpeedTraits;

        public int TroopGroundAttack => _TroopGroundAttack + _TroopGroundAttackTraits;

        public int TroopGroundDefense => _TroopGroundDefense + _TroopGroundDefenseTraits;

        public int TroopExperienceGain => _TroopExperienceGain + _TroopExperienceGainTraits;

        public int TroopRecoveryRate => _TroopRecoveryRate + _TroopRecoveryRateTraits;

        public int TroopStrengthArmor => _TroopStrengthArmor + _TroopStrengthArmorTraits;

        public int TroopStrengthInfantry => _TroopStrengthInfantry + _TroopStrengthInfantryTraits;

        public int TroopStrengthSpecialForces => _TroopStrengthSpecialForces + _TroopStrengthSpecialForcesTraits;

        public int TroopStrengthPlanetaryDefense => _TroopStrengthPlanetaryDefense + _TroopStrengthPlanetaryDefenseTraits;

        public int SmugglingIncome => _SmugglingIncome + _SmugglingIncomeTraits;

        public int SmugglingEvasion => _SmugglingEvasion + _SmugglingEvasionTraits;

        public int BoardingAssault => _BoardingAssault + _BoardingAssaultTraits;

        public string PictureFilename
        {
            get
            {
                return _PictureFilename;
            }
            set
            {
                _PictureFilename = value;
            }
        }

        public StellarObject Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }

        public StellarObject TransferDestination => _TransferDestination;

        public float TransferTimeRemaining => _TransferTimeRemaining;

        public IntelligenceMission Mission
        {
            get
            {
                return _Mission;
            }
            set
            {
                _Mission = value;
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

        public long StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }

        public long EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }

        public int AppearanceOrder
        {
            get
            {
                return _AppearanceOrder;
            }
            set
            {
                _AppearanceOrder = value;
            }
        }

        public bool Active
        {
            get
            {
                return _Active;
            }
            set
            {
                _Active = value;
            }
        }

        public Character(SerializationInfo info, StreamingContext context)
        {
            byte[] buffer = (byte[])info.GetValue("Chr_D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                _StartDate = binaryReader.ReadInt64();
                _EndDate = binaryReader.ReadInt64();
                _Active = binaryReader.ReadBoolean();
                _AppearanceOrder = binaryReader.ReadInt32();
                _TransferTimeRemaining = binaryReader.ReadSingle();
                _TransferArrivalDate = binaryReader.ReadInt64();
                _LastTouch = new DateTime(binaryReader.ReadInt64());
                Role = (CharacterRole)binaryReader.ReadByte();
                BonusesKnown = binaryReader.ReadBoolean();
                byte b = binaryReader.ReadByte();
                for (byte b2 = 0; b2 < b; b2 = (byte)(b2 + 1))
                {
                    CharacterSkillType skillType = (CharacterSkillType)binaryReader.ReadByte();
                    sbyte level = binaryReader.ReadSByte();
                    float progress = binaryReader.ReadSingle();
                    float nextProgressThreshold = binaryReader.ReadSingle();
                    CharacterSkill item = new CharacterSkill(skillType, level)
                    {
                        Progress = progress,
                        NextProgressThreshold = nextProgressThreshold
                    };
                    Skills.Add(item);
                }
                byte b3 = binaryReader.ReadByte();
                for (byte b4 = 0; b4 < b3; b4 = (byte)(b4 + 1))
                {
                    CharacterTraitType item2 = (CharacterTraitType)binaryReader.ReadByte();
                    Traits.Add(item2);
                }
                binaryReader.Close();
            }
            Name = (string)info.GetValue("Na", typeof(string));
            Race = (Race)info.GetValue("Ra", typeof(Race));
            _PictureFilename = (string)info.GetValue("PF", typeof(string));
            Empire = (Empire)info.GetValue("Em", typeof(Empire));
            _Mission = (IntelligenceMission)info.GetValue("Ms", typeof(IntelligenceMission));
            _Location = (StellarObject)info.GetValue("Lc", typeof(StellarObject));
            _TransferDestination = (StellarObject)info.GetValue("TD", typeof(StellarObject));
            EventHistory = (CharacterEventList)info.GetValue("EH", typeof(CharacterEventList));
            RebuildCachedSkillValues();
            ReviewTraitSkills();
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write(_StartDate);
                binaryWriter.Write(_EndDate);
                binaryWriter.Write(_Active);
                binaryWriter.Write(_AppearanceOrder);
                binaryWriter.Write(_TransferTimeRemaining);
                binaryWriter.Write(_TransferArrivalDate);
                binaryWriter.Write(_LastTouch.Ticks);
                binaryWriter.Write((byte)Role);
                binaryWriter.Write(BonusesKnown);
                binaryWriter.Write((byte)Skills.Count);
                for (int i = 0; i < Skills.Count; i++)
                {
                    CharacterSkill characterSkill = Skills[i];
                    binaryWriter.Write((byte)characterSkill.Type);
                    binaryWriter.Write((sbyte)characterSkill.Level);
                    binaryWriter.Write(characterSkill.Progress);
                    binaryWriter.Write(characterSkill.NextProgressThreshold);
                }
                binaryWriter.Write((byte)Traits.Count);
                for (int j = 0; j < Traits.Count; j++)
                {
                    binaryWriter.Write((byte)Traits[j]);
                }
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("Chr_D", memoryStream.ToArray());
            }
            info.AddValue("Na", Name);
            info.AddValue("Ra", Race);
            info.AddValue("PF", _PictureFilename);
            info.AddValue("Em", Empire);
            info.AddValue("Ms", _Mission);
            info.AddValue("Lc", _Location);
            info.AddValue("TD", _TransferDestination);
            info.AddValue("EH", EventHistory);
        }

        public bool IncrementSkillProgress(CharacterSkillType skillType, float progressAmount, Galaxy galaxy)
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                CharacterSkill characterSkill = Skills[i];
                if (characterSkill == null || characterSkill.Type != skillType)
                {
                    continue;
                }
                characterSkill.Progress += progressAmount;
                if (characterSkill.Progress >= characterSkill.NextProgressThreshold)
                {
                    int num = CalculateSkillLevelIncrement(skillType, galaxy) * BaconCharacter.IncrementSkillProgress(this);
                    if (Role == CharacterRole.Leader)
                    {
                        num /= 2;
                        num = Math.Max(1, num);
                    }
                    int val = characterSkill.Level + num;
                    UpdateSkillLevel(level: characterSkill.Level = Math.Max(-100, Math.Min(val, 125)), skill: characterSkill.Type);
                    characterSkill.Progress = 0f;
                    characterSkill.NextProgressThreshold *= 1.5f;
                    if (galaxy != null)
                    {
                        long currentStarDate = galaxy.CurrentStarDate;
                        CharacterEvent characterEvent = new CharacterEvent(CharacterEventType.CharacterSkillProgress, characterSkill, currentStarDate);
                        EventHistory.Add(characterEvent);
                    }
                    return true;
                }
                if (characterSkill.Progress < 0f)
                {
                    characterSkill.Progress = 0f;
                }
            }
            return false;
        }

        private int CalculateSkillLevelIncrement(CharacterSkillType skillType, Galaxy galaxy)
        {
            int result = 0;
            switch (skillType)
            {
                case CharacterSkillType.MilitaryShipMaintenance:
                case CharacterSkillType.MilitaryBaseMaintenance:
                case CharacterSkillType.CivilianShipMaintenance:
                case CharacterSkillType.CivilianBaseMaintenance:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.MilitaryShipConstructionSpeed:
                case CharacterSkillType.CivilianShipConstructionSpeed:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.ColonyIncome:
                case CharacterSkillType.ColonyCorruption:
                case CharacterSkillType.ColonyHappiness:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.Espionage:
                case CharacterSkillType.CounterEspionage:
                case CharacterSkillType.Sabotage:
                case CharacterSkillType.Concealment:
                case CharacterSkillType.PsyOps:
                case CharacterSkillType.Assassination:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.Targeting:
                case CharacterSkillType.Countermeasures:
                case CharacterSkillType.ShipManeuvering:
                case CharacterSkillType.Fighters:
                case CharacterSkillType.ShipEnergyUsage:
                case CharacterSkillType.ShieldRechargeRate:
                case CharacterSkillType.DamageControl:
                case CharacterSkillType.RepairBonus:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.WeaponsDamage:
                case CharacterSkillType.WeaponsRange:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.HyperjumpSpeed:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.Diplomacy:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.FacilityConstructionSpeed:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.MiningRate:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.PopulationGrowth:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.ResearchWeapons:
                case CharacterSkillType.ResearchEnergy:
                case CharacterSkillType.ResearchHighTech:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.TradeIncome:
                case CharacterSkillType.TourismIncome:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.TroopRecruitment:
                case CharacterSkillType.TroopGroundAttack:
                case CharacterSkillType.TroopGroundDefense:
                case CharacterSkillType.TroopRecoveryRate:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.TroopMaintenance:
                case CharacterSkillType.TroopExperienceGain:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.WarWeariness:
                    result = Galaxy.Rnd.Next(4, 9);
                    break;
                case CharacterSkillType.TroopStrengthArmor:
                case CharacterSkillType.TroopStrengthInfantry:
                case CharacterSkillType.TroopStrengthSpecialForces:
                case CharacterSkillType.TroopStrengthPlanetaryDefense:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.SmugglingIncome:
                case CharacterSkillType.SmugglingEvasion:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
                case CharacterSkillType.BoardingAssault:
                    result = Galaxy.Rnd.Next(5, 16);
                    break;
            }
            return result;
        }

        private void UpdateSkillLevel(CharacterSkillType skill, int level)
        {
            switch (skill)
            {
                case CharacterSkillType.Assassination:
                    _Assassination = (sbyte)level;
                    break;
                case CharacterSkillType.CivilianBaseMaintenance:
                    _CivilianBaseMaintenance = (sbyte)level;
                    break;
                case CharacterSkillType.CivilianShipConstructionSpeed:
                    _CivilianShipConstructionSpeed = (sbyte)level;
                    break;
                case CharacterSkillType.CivilianShipMaintenance:
                    _CivilianShipMaintenance = (sbyte)level;
                    break;
                case CharacterSkillType.ColonyCorruption:
                    _ColonyCorruption = (sbyte)level;
                    break;
                case CharacterSkillType.ColonyHappiness:
                    _ColonyHappiness = (sbyte)level;
                    break;
                case CharacterSkillType.ColonyIncome:
                    _ColonyIncome = (sbyte)level;
                    break;
                case CharacterSkillType.ColonyShipConstructionSpeed:
                    _ColonyShipConstructionSpeed = (sbyte)level;
                    break;
                case CharacterSkillType.Concealment:
                    _Concealment = (sbyte)level;
                    break;
                case CharacterSkillType.CounterEspionage:
                    _CounterEspionage = (sbyte)level;
                    break;
                case CharacterSkillType.Countermeasures:
                    _Countermeasures = (sbyte)level;
                    break;
                case CharacterSkillType.DamageControl:
                    _DamageControl = (sbyte)level;
                    break;
                case CharacterSkillType.Diplomacy:
                    _Diplomacy = (sbyte)level;
                    break;
                case CharacterSkillType.Espionage:
                    _Espionage = (sbyte)level;
                    break;
                case CharacterSkillType.FacilityConstructionSpeed:
                    _FacilityConstructionSpeed = (sbyte)level;
                    break;
                case CharacterSkillType.Fighters:
                    _Fighters = (sbyte)level;
                    break;
                case CharacterSkillType.HyperjumpSpeed:
                    _HyperjumpSpeed = (sbyte)level;
                    break;
                case CharacterSkillType.MilitaryBaseMaintenance:
                    _MilitaryBaseMaintenance = (sbyte)level;
                    break;
                case CharacterSkillType.MilitaryShipConstructionSpeed:
                    _MilitaryShipConstructionSpeed = (sbyte)level;
                    break;
                case CharacterSkillType.MilitaryShipMaintenance:
                    _MilitaryShipMaintenance = (sbyte)level;
                    break;
                case CharacterSkillType.MiningRate:
                    _MiningRate = (sbyte)level;
                    break;
                case CharacterSkillType.PopulationGrowth:
                    _PopulationGrowth = (sbyte)level;
                    break;
                case CharacterSkillType.PsyOps:
                    _PsyOps = (sbyte)level;
                    break;
                case CharacterSkillType.RepairBonus:
                    _RepairBonus = (sbyte)level;
                    break;
                case CharacterSkillType.ResearchEnergy:
                    _ResearchEnergy = (sbyte)level;
                    break;
                case CharacterSkillType.ResearchHighTech:
                    _ResearchHighTech = (sbyte)level;
                    break;
                case CharacterSkillType.ResearchWeapons:
                    _ResearchWeapons = (sbyte)level;
                    break;
                case CharacterSkillType.Sabotage:
                    _Sabotage = (sbyte)level;
                    break;
                case CharacterSkillType.ShieldRechargeRate:
                    _ShieldRechargeRate = (sbyte)level;
                    break;
                case CharacterSkillType.ShipEnergyUsage:
                    _ShipEnergyUsage = (sbyte)level;
                    break;
                case CharacterSkillType.ShipManeuvering:
                    _ShipManeuvering = (sbyte)level;
                    break;
                case CharacterSkillType.Targeting:
                    _Targeting = (sbyte)level;
                    break;
                case CharacterSkillType.TourismIncome:
                    _TourismIncome = (sbyte)level;
                    break;
                case CharacterSkillType.TradeIncome:
                    _TradeIncome = (sbyte)level;
                    break;
                case CharacterSkillType.TroopExperienceGain:
                    _TroopExperienceGain = (sbyte)level;
                    break;
                case CharacterSkillType.TroopGroundAttack:
                    _TroopGroundAttack = (sbyte)level;
                    break;
                case CharacterSkillType.TroopGroundDefense:
                    _TroopGroundDefense = (sbyte)level;
                    break;
                case CharacterSkillType.TroopMaintenance:
                    _TroopMaintenance = (sbyte)level;
                    break;
                case CharacterSkillType.TroopRecoveryRate:
                    _TroopRecoveryRate = (sbyte)level;
                    break;
                case CharacterSkillType.TroopRecruitment:
                    _TroopRecruitmentRate = (sbyte)level;
                    break;
                case CharacterSkillType.WarWeariness:
                    _WarWeariness = (sbyte)level;
                    break;
                case CharacterSkillType.WeaponsDamage:
                    _WeaponsDamage = (sbyte)level;
                    break;
                case CharacterSkillType.WeaponsRange:
                    _WeaponsRange = (sbyte)level;
                    break;
                case CharacterSkillType.TroopStrengthArmor:
                    _TroopStrengthArmor = (sbyte)level;
                    break;
                case CharacterSkillType.TroopStrengthInfantry:
                    _TroopStrengthInfantry = (sbyte)level;
                    break;
                case CharacterSkillType.TroopStrengthSpecialForces:
                    _TroopStrengthSpecialForces = (sbyte)level;
                    break;
                case CharacterSkillType.TroopStrengthPlanetaryDefense:
                    _TroopStrengthPlanetaryDefense = (sbyte)level;
                    break;
                case CharacterSkillType.SmugglingIncome:
                    _SmugglingIncome = (sbyte)level;
                    break;
                case CharacterSkillType.SmugglingEvasion:
                    _SmugglingEvasion = (sbyte)level;
                    break;
                case CharacterSkillType.BoardingAssault:
                    _BoardingAssault = (sbyte)level;
                    break;
            }
        }

        public int GetSkillLevelTotal()
        {
            int num = 0;
            List<CharacterSkillType> list = ResolveCharacterSkillTypes(includeUnknownBonuses: true);
            for (int i = 0; i < list.Count; i++)
            {
                num += GetSkillLevel(list[i]);
            }
            return num;
        }

        public List<CharacterSkillType> ResolveCharacterSkillTypes(bool includeUnknownBonuses)
        {
            List<CharacterSkillType> list = new List<CharacterSkillType>();
            for (int i = 0; i < Skills.Count; i++)
            {
                list.Add(Skills[i].Type);
            }
            if (includeUnknownBonuses || BonusesKnown)
            {
                for (int j = 0; j < TraitSkills.Count; j++)
                {
                    if (!list.Contains(TraitSkills[j].Type))
                    {
                        list.Add(TraitSkills[j].Type);
                    }
                }
            }
            return list;
        }

        public int GetSkillLevel(CharacterSkillType skill)
        {
            return skill switch
            {
                CharacterSkillType.Assassination => _AssassinationTraits + _Assassination,
                CharacterSkillType.CivilianBaseMaintenance => _CivilianBaseMaintenanceTraits + _CivilianBaseMaintenance,
                CharacterSkillType.CivilianShipConstructionSpeed => _CivilianShipConstructionSpeedTraits + _CivilianShipConstructionSpeed,
                CharacterSkillType.CivilianShipMaintenance => _CivilianShipMaintenanceTraits + _CivilianShipMaintenance,
                CharacterSkillType.ColonyCorruption => _ColonyCorruptionTraits + _ColonyCorruption,
                CharacterSkillType.ColonyHappiness => _ColonyHappinessTraits + _ColonyHappiness,
                CharacterSkillType.ColonyIncome => _ColonyIncomeTraits + _ColonyIncome,
                CharacterSkillType.ColonyShipConstructionSpeed => _ColonyShipConstructionSpeedTraits + _ColonyShipConstructionSpeed,
                CharacterSkillType.Concealment => _ConcealmentTraits + _Concealment,
                CharacterSkillType.CounterEspionage => _CounterEspionageTraits + _CounterEspionage,
                CharacterSkillType.Countermeasures => _CountermeasuresTraits + _Countermeasures,
                CharacterSkillType.DamageControl => _DamageControlTraits + _DamageControl,
                CharacterSkillType.Diplomacy => _DiplomacyTraits + _Diplomacy,
                CharacterSkillType.Espionage => _EspionageTraits + _Espionage,
                CharacterSkillType.FacilityConstructionSpeed => _FacilityConstructionSpeedTraits + _FacilityConstructionSpeed,
                CharacterSkillType.Fighters => _FightersTraits + _Fighters,
                CharacterSkillType.HyperjumpSpeed => _HyperjumpSpeedTraits + _HyperjumpSpeed,
                CharacterSkillType.MilitaryBaseMaintenance => _MilitaryBaseMaintenanceTraits + _MilitaryBaseMaintenance,
                CharacterSkillType.MilitaryShipConstructionSpeed => _MilitaryShipConstructionSpeedTraits + _MilitaryShipConstructionSpeed,
                CharacterSkillType.MilitaryShipMaintenance => _MilitaryShipMaintenanceTraits + _MilitaryShipMaintenance,
                CharacterSkillType.MiningRate => _MiningRateTraits + _MiningRate,
                CharacterSkillType.PopulationGrowth => _PopulationGrowthTraits + _PopulationGrowth,
                CharacterSkillType.PsyOps => _PsyOpsTraits + _PsyOps,
                CharacterSkillType.RepairBonus => _RepairBonusTraits + _RepairBonus,
                CharacterSkillType.ResearchEnergy => _ResearchEnergyTraits + _ResearchEnergy,
                CharacterSkillType.ResearchHighTech => _ResearchHighTechTraits + _ResearchHighTech,
                CharacterSkillType.ResearchWeapons => _ResearchWeaponsTraits + _ResearchWeapons,
                CharacterSkillType.Sabotage => _SabotageTraits + _Sabotage,
                CharacterSkillType.ShieldRechargeRate => _ShieldRechargeRateTraits + _ShieldRechargeRate,
                CharacterSkillType.ShipEnergyUsage => _ShipEnergyUsageTraits + _ShipEnergyUsage,
                CharacterSkillType.ShipManeuvering => _ShipManeuveringTraits + _ShipManeuvering,
                CharacterSkillType.Targeting => _TargetingTraits + _Targeting,
                CharacterSkillType.TourismIncome => _TourismIncomeTraits + _TourismIncome,
                CharacterSkillType.TradeIncome => _TradeIncomeTraits + _TradeIncome,
                CharacterSkillType.TroopExperienceGain => _TroopExperienceGainTraits + _TroopExperienceGain,
                CharacterSkillType.TroopGroundAttack => _TroopGroundAttackTraits + _TroopGroundAttack,
                CharacterSkillType.TroopGroundDefense => _TroopGroundDefenseTraits + _TroopGroundDefense,
                CharacterSkillType.TroopMaintenance => _TroopMaintenanceTraits + _TroopMaintenance,
                CharacterSkillType.TroopRecoveryRate => _TroopRecoveryRateTraits + _TroopRecoveryRate,
                CharacterSkillType.TroopRecruitment => _TroopRecruitmentRateTraits + _TroopRecruitmentRate,
                CharacterSkillType.WarWeariness => _WarWearinessTraits + _WarWeariness,
                CharacterSkillType.WeaponsDamage => _WeaponsDamageTraits + _WeaponsDamage,
                CharacterSkillType.WeaponsRange => _WeaponsRangeTraits + _WeaponsRange,
                CharacterSkillType.TroopStrengthArmor => _TroopStrengthArmorTraits + _TroopStrengthArmor,
                CharacterSkillType.TroopStrengthInfantry => _TroopStrengthInfantryTraits + _TroopStrengthInfantry,
                CharacterSkillType.TroopStrengthSpecialForces => _TroopStrengthSpecialForcesTraits + _TroopStrengthSpecialForces,
                CharacterSkillType.TroopStrengthPlanetaryDefense => _TroopStrengthPlanetaryDefenseTraits + _TroopStrengthPlanetaryDefense,
                CharacterSkillType.SmugglingIncome => _SmugglingIncomeTraits + _SmugglingIncome,
                CharacterSkillType.SmugglingEvasion => _SmugglingEvasionTraits + _SmugglingEvasion,
                CharacterSkillType.BoardingAssault => _BoardingAssaultTraits + _BoardingAssault,
                _ => 0,
            };
        }

        public bool AddSkill(CharacterSkillType skillType, int level, Galaxy galaxy)
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                CharacterSkill characterSkill = Skills[i];
                if (characterSkill != null && characterSkill.Type == skillType)
                {
                    return false;
                }
            }
            int num = 4;
            if (Role == CharacterRole.IntelligenceAgent)
            {
                num = 4;
            }
            if (Skills.Count < num * BaconCharacter.IncrementSkillProgress(this))
            {
                CharacterSkill characterSkill2 = new CharacterSkill(skillType, level);
                Skills.Add(characterSkill2);
                RebuildCachedSkillValues();
                if (galaxy != null)
                {
                    long currentStarDate = galaxy.CurrentStarDate;
                    CharacterEvent characterEvent = (characterEvent = new CharacterEvent(CharacterEventType.CharacterSkillGain, characterSkill2, currentStarDate));
                    EventHistory.Add(characterEvent);
                }
                return true;
            }
            return false;
        }

        public CharacterSkill GetSkill(CharacterSkillType skillType)
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                CharacterSkill characterSkill = Skills[i];
                if (characterSkill != null && characterSkill.Type == skillType)
                {
                    return characterSkill;
                }
            }
            return null;
        }

        public bool CheckHasSkill(CharacterSkillType skill)
        {
            for (int i = 0; i < Skills.Count; i++)
            {
                if (Skills[i].Type == skill)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<CharacterSkillType> DetermineValidSkillsForRole(CharacterRole role)
        {
            return DetermineValidSkillsForRole(role, primarySkills: true, secondarySkills: true);
        }

        public static List<CharacterSkillType> DetermineValidSkillsForRole(CharacterRole role, bool primarySkills, bool secondarySkills)
        {
            List<CharacterSkillType> list = new List<CharacterSkillType>();
            switch (role)
            {
                case CharacterRole.Ambassador:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Diplomacy);
                        list.Add(CharacterSkillType.CounterEspionage);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.TradeIncome);
                        list.Add(CharacterSkillType.TourismIncome);
                        list.Add(CharacterSkillType.Espionage);
                    }
                    break;
                case CharacterRole.ColonyGovernor:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.ColonyIncome);
                        list.Add(CharacterSkillType.ColonyHappiness);
                        list.Add(CharacterSkillType.PopulationGrowth);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.TradeIncome);
                        list.Add(CharacterSkillType.TourismIncome);
                        list.Add(CharacterSkillType.ColonyCorruption);
                        list.Add(CharacterSkillType.MiningRate);
                        list.Add(CharacterSkillType.TroopRecruitment);
                        list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                        list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                        list.Add(CharacterSkillType.ColonyShipConstructionSpeed);
                        list.Add(CharacterSkillType.FacilityConstructionSpeed);
                        list.Add(CharacterSkillType.MilitaryBaseMaintenance);
                        list.Add(CharacterSkillType.CivilianBaseMaintenance);
                        list.Add(CharacterSkillType.TroopMaintenance);
                        list.Add(CharacterSkillType.WarWeariness);
                    }
                    break;
                case CharacterRole.FleetAdmiral:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Targeting);
                        list.Add(CharacterSkillType.Countermeasures);
                        list.Add(CharacterSkillType.ShipManeuvering);
                        list.Add(CharacterSkillType.ShipEnergyUsage);
                        list.Add(CharacterSkillType.DamageControl);
                        list.Add(CharacterSkillType.RepairBonus);
                        list.Add(CharacterSkillType.HyperjumpSpeed);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.MilitaryShipMaintenance);
                        list.Add(CharacterSkillType.Fighters);
                        list.Add(CharacterSkillType.WeaponsDamage);
                        list.Add(CharacterSkillType.WeaponsRange);
                        list.Add(CharacterSkillType.ShieldRechargeRate);
                        list.Add(CharacterSkillType.BoardingAssault);
                    }
                    break;
                case CharacterRole.IntelligenceAgent:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Espionage);
                        list.Add(CharacterSkillType.CounterEspionage);
                        list.Add(CharacterSkillType.Sabotage);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.PsyOps);
                        list.Add(CharacterSkillType.Concealment);
                        list.Add(CharacterSkillType.Assassination);
                    }
                    break;
                case CharacterRole.Leader:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Diplomacy);
                        list.Add(CharacterSkillType.ColonyIncome);
                        list.Add(CharacterSkillType.ColonyHappiness);
                        list.Add(CharacterSkillType.PopulationGrowth);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.TradeIncome);
                        list.Add(CharacterSkillType.TourismIncome);
                        list.Add(CharacterSkillType.ColonyCorruption);
                        list.Add(CharacterSkillType.MiningRate);
                        list.Add(CharacterSkillType.TroopRecruitment);
                        list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                        list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                        list.Add(CharacterSkillType.ColonyShipConstructionSpeed);
                        list.Add(CharacterSkillType.FacilityConstructionSpeed);
                        list.Add(CharacterSkillType.ResearchWeapons);
                        list.Add(CharacterSkillType.ResearchEnergy);
                        list.Add(CharacterSkillType.ResearchHighTech);
                        list.Add(CharacterSkillType.Espionage);
                        list.Add(CharacterSkillType.CounterEspionage);
                        list.Add(CharacterSkillType.MilitaryShipMaintenance);
                        list.Add(CharacterSkillType.MilitaryBaseMaintenance);
                        list.Add(CharacterSkillType.CivilianShipMaintenance);
                        list.Add(CharacterSkillType.CivilianBaseMaintenance);
                        list.Add(CharacterSkillType.TroopMaintenance);
                        list.Add(CharacterSkillType.WarWeariness);
                    }
                    break;
                case CharacterRole.PirateLeader:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Diplomacy);
                        list.Add(CharacterSkillType.Espionage);
                        list.Add(CharacterSkillType.MilitaryShipConstructionSpeed);
                        list.Add(CharacterSkillType.CivilianShipConstructionSpeed);
                        list.Add(CharacterSkillType.MilitaryShipMaintenance);
                        list.Add(CharacterSkillType.MilitaryBaseMaintenance);
                        list.Add(CharacterSkillType.CivilianShipMaintenance);
                        list.Add(CharacterSkillType.CivilianBaseMaintenance);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.TradeIncome);
                        list.Add(CharacterSkillType.TourismIncome);
                        list.Add(CharacterSkillType.MiningRate);
                        list.Add(CharacterSkillType.FacilityConstructionSpeed);
                        list.Add(CharacterSkillType.ResearchWeapons);
                        list.Add(CharacterSkillType.ResearchEnergy);
                        list.Add(CharacterSkillType.ResearchHighTech);
                        list.Add(CharacterSkillType.CounterEspionage);
                        list.Add(CharacterSkillType.Targeting);
                        list.Add(CharacterSkillType.Countermeasures);
                        list.Add(CharacterSkillType.ShipManeuvering);
                        list.Add(CharacterSkillType.ShipEnergyUsage);
                        list.Add(CharacterSkillType.DamageControl);
                        list.Add(CharacterSkillType.RepairBonus);
                        list.Add(CharacterSkillType.HyperjumpSpeed);
                        list.Add(CharacterSkillType.Fighters);
                        list.Add(CharacterSkillType.WeaponsDamage);
                        list.Add(CharacterSkillType.WeaponsRange);
                        list.Add(CharacterSkillType.ShieldRechargeRate);
                        list.Add(CharacterSkillType.SmugglingIncome);
                        list.Add(CharacterSkillType.SmugglingEvasion);
                        list.Add(CharacterSkillType.BoardingAssault);
                    }
                    break;
                case CharacterRole.ShipCaptain:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.Targeting);
                        list.Add(CharacterSkillType.Countermeasures);
                        list.Add(CharacterSkillType.ShipManeuvering);
                        list.Add(CharacterSkillType.ShipEnergyUsage);
                        list.Add(CharacterSkillType.DamageControl);
                        list.Add(CharacterSkillType.RepairBonus);
                        list.Add(CharacterSkillType.HyperjumpSpeed);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.MilitaryShipMaintenance);
                        list.Add(CharacterSkillType.Fighters);
                        list.Add(CharacterSkillType.WeaponsDamage);
                        list.Add(CharacterSkillType.WeaponsRange);
                        list.Add(CharacterSkillType.ShieldRechargeRate);
                        list.Add(CharacterSkillType.SmugglingIncome);
                        list.Add(CharacterSkillType.SmugglingEvasion);
                        list.Add(CharacterSkillType.BoardingAssault);
                    }
                    break;
                case CharacterRole.Scientist:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.ResearchWeapons);
                        list.Add(CharacterSkillType.ResearchEnergy);
                        list.Add(CharacterSkillType.ResearchHighTech);
                    }
                    break;
                case CharacterRole.TroopGeneral:
                    if (primarySkills)
                    {
                        list.Add(CharacterSkillType.TroopGroundAttack);
                        list.Add(CharacterSkillType.TroopGroundDefense);
                        list.Add(CharacterSkillType.TroopRecoveryRate);
                    }
                    if (secondarySkills)
                    {
                        list.Add(CharacterSkillType.TroopMaintenance);
                        list.Add(CharacterSkillType.TroopRecruitment);
                        list.Add(CharacterSkillType.TroopExperienceGain);
                        list.Add(CharacterSkillType.TroopStrengthArmor);
                        list.Add(CharacterSkillType.TroopStrengthInfantry);
                        list.Add(CharacterSkillType.TroopStrengthSpecialForces);
                        list.Add(CharacterSkillType.TroopStrengthPlanetaryDefense);
                    }
                    break;
            }
            return list;
        }

        public int TotalSkillValuesIfPresent(List<CharacterSkillType> skills)
        {
            int num = 0;
            for (int i = 0; i < skills.Count; i++)
            {
                CharacterSkill skillByType = Skills.GetSkillByType(skills[i]);
                if (skillByType != null)
                {
                    num += skillByType.Level;
                }
            }
            return num;
        }

        public static bool CheckSkillValid(CharacterSkillType skillType, CharacterRole role)
        {
            List<CharacterSkillType> list = DetermineValidSkillsForRole(role);
            if (list.Contains(skillType))
            {
                return true;
            }
            return false;
        }

        public bool AddTrait(CharacterTraitType trait, bool starting, Galaxy galaxy)
        {
            if (CheckNewTraitValid(trait))
            {
                List<CharacterTraitType> list = DetermineValidTraitsForRole(Role, starting);
                if (list.Contains(trait))
                {
                    int num = 4;
                    if (Traits.Count < num)
                    {
                        Traits.Add(trait);
                        ReviewTraitSkills();
                        if (galaxy != null)
                        {
                            long currentStarDate = galaxy.CurrentStarDate;
                            CharacterEvent characterEvent = (characterEvent = new CharacterEvent(CharacterEventType.CharacterTraitGain, trait, currentStarDate));
                            EventHistory.Add(characterEvent);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public void RemoveAllSkillsAndTraits()
        {
            Skills.Clear();
            Traits.Clear();
            RebuildCachedSkillValues();
            ReviewTraitSkills();
        }

        public bool RemoveTrait(CharacterTraitType trait)
        {
            if (Traits.Contains(trait))
            {
                Traits.Remove(trait);
                ReviewTraitSkills();
                return true;
            }
            return false;
        }

        public bool CheckNewTraitValid(CharacterTraitType trait)
        {
            for (int i = 0; i < Traits.Count; i++)
            {
                switch (Traits[i])
                {
                    case CharacterTraitType.IntelligenceUninhibited:
                    case CharacterTraitType.IntelligenceMeasured:
                        if (trait == CharacterTraitType.IntelligenceUninhibited || trait == CharacterTraitType.IntelligenceMeasured)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.IntelligenceAddict:
                    case CharacterTraitType.IntelligenceSober:
                        if (trait == CharacterTraitType.IntelligenceAddict || trait == CharacterTraitType.IntelligenceSober)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.IntelligenceCourageous:
                    case CharacterTraitType.IntelligenceWeak:
                        if (trait == CharacterTraitType.IntelligenceCourageous || trait == CharacterTraitType.IntelligenceWeak)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.IntelligenceTolerant:
                    case CharacterTraitType.IntelligenceXenophobic:
                        if (trait == CharacterTraitType.IntelligenceTolerant || trait == CharacterTraitType.IntelligenceXenophobic)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.IntelligenceEloquentSpeaker:
                    case CharacterTraitType.IntelligencePoorSpeaker:
                        if (trait == CharacterTraitType.IntelligenceEloquentSpeaker || trait == CharacterTraitType.IntelligencePoorSpeaker)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.IntelligenceCorrupt:
                    case CharacterTraitType.IntelligenceLawful:
                        if (trait == CharacterTraitType.IntelligenceCorrupt || trait == CharacterTraitType.IntelligenceLawful)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Paranoid:
                    case CharacterTraitType.Trusting:
                        if (trait == CharacterTraitType.Paranoid || trait == CharacterTraitType.Trusting)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.PeaceThroughStrength:
                    case CharacterTraitType.Pacifist:
                        if (trait == CharacterTraitType.PeaceThroughStrength || trait == CharacterTraitType.Pacifist)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Expansionist:
                    case CharacterTraitType.Isolationist:
                        if (trait == CharacterTraitType.Expansionist || trait == CharacterTraitType.Isolationist)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Diplomat:
                    case CharacterTraitType.Obnoxious:
                        if (trait == CharacterTraitType.Diplomat || trait == CharacterTraitType.Obnoxious)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Famous:
                    case CharacterTraitType.Disliked:
                        if (trait == CharacterTraitType.Famous || trait == CharacterTraitType.Disliked)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodAdministrator:
                    case CharacterTraitType.PoorAdministrator:
                        if (trait == CharacterTraitType.GoodAdministrator || trait == CharacterTraitType.PoorAdministrator)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.BeanCounter:
                    case CharacterTraitType.Generous:
                        if (trait == CharacterTraitType.BeanCounter || trait == CharacterTraitType.Generous)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Engineer:
                    case CharacterTraitType.Luddite:
                        if (trait == CharacterTraitType.Engineer || trait == CharacterTraitType.Luddite)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.FreeTrader:
                    case CharacterTraitType.Protectionist:
                        if (trait == CharacterTraitType.FreeTrader || trait == CharacterTraitType.Protectionist)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Environmentalist:
                    case CharacterTraitType.Industrialist:
                        if (trait == CharacterTraitType.Environmentalist || trait == CharacterTraitType.Industrialist)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.InspiringPresence:
                    case CharacterTraitType.Demoralizing:
                        if (trait == CharacterTraitType.InspiringPresence || trait == CharacterTraitType.Demoralizing)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Organized:
                    case CharacterTraitType.Disorganized:
                        if (trait == CharacterTraitType.Organized || trait == CharacterTraitType.Disorganized)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.HealthOriented:
                    case CharacterTraitType.LaborOriented:
                        if (trait == CharacterTraitType.HealthOriented || trait == CharacterTraitType.LaborOriented)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Spiritual:
                    case CharacterTraitType.Logical:
                        if (trait == CharacterTraitType.Logical || trait == CharacterTraitType.Spiritual)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodStrategist:
                    case CharacterTraitType.PoorStrategist:
                        if (trait == CharacterTraitType.GoodStrategist || trait == CharacterTraitType.PoorStrategist)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Uninhibited:
                    case CharacterTraitType.Measured:
                        if (trait == CharacterTraitType.Uninhibited || trait == CharacterTraitType.Measured)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Addict:
                    case CharacterTraitType.Sober:
                        if (trait == CharacterTraitType.Addict || trait == CharacterTraitType.Sober)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Courageous:
                    case CharacterTraitType.Weak:
                        if (trait == CharacterTraitType.Courageous || trait == CharacterTraitType.Weak)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Tolerant:
                    case CharacterTraitType.Xenophobic:
                        if (trait == CharacterTraitType.Tolerant || trait == CharacterTraitType.Xenophobic)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.EloquentSpeaker:
                    case CharacterTraitType.PoorSpeaker:
                        if (trait == CharacterTraitType.EloquentSpeaker || trait == CharacterTraitType.PoorSpeaker)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Corrupt:
                    case CharacterTraitType.Lawful:
                        if (trait == CharacterTraitType.Corrupt || trait == CharacterTraitType.Lawful)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Lazy:
                    case CharacterTraitType.Energetic:
                        if (trait == CharacterTraitType.Lazy || trait == CharacterTraitType.Energetic)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Linguist:
                    case CharacterTraitType.TongueTied:
                        if (trait == CharacterTraitType.Linguist || trait == CharacterTraitType.TongueTied)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Technical:
                    case CharacterTraitType.NonTechnical:
                        if (trait == CharacterTraitType.Technical || trait == CharacterTraitType.NonTechnical)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodTactician:
                    case CharacterTraitType.PoorTactician:
                        if (trait == CharacterTraitType.GoodTactician || trait == CharacterTraitType.PoorTactician)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.StrongSpaceAttacker:
                    case CharacterTraitType.PoorSpaceAttacker:
                        if (trait == CharacterTraitType.StrongSpaceAttacker || trait == CharacterTraitType.PoorSpaceAttacker)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.StrongSpaceDefender:
                    case CharacterTraitType.PoorSpaceDefender:
                        if (trait == CharacterTraitType.StrongSpaceDefender || trait == CharacterTraitType.PoorSpaceDefender)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Drunk:
                        if (trait == CharacterTraitType.Drunk)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.ToughDiscipline:
                    case CharacterTraitType.LaxDiscipline:
                        if (trait == CharacterTraitType.ToughDiscipline || trait == CharacterTraitType.LaxDiscipline)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.LocalDefenseTactics:
                        if (trait == CharacterTraitType.LocalDefenseTactics)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.PlanetarySupport:
                        if (trait == CharacterTraitType.PlanetarySupport)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodSpaceLogistician:
                    case CharacterTraitType.PoorSpaceLogistician:
                        if (trait == CharacterTraitType.GoodSpaceLogistician || trait == CharacterTraitType.PoorSpaceLogistician)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.NaturalSpaceLeader:
                        if (trait == CharacterTraitType.NaturalSpaceLeader)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.SkilledNavigator:
                    case CharacterTraitType.PoorNavigator:
                        if (trait == CharacterTraitType.SkilledNavigator || trait == CharacterTraitType.PoorNavigator)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.StrongGroundAttacker:
                    case CharacterTraitType.PoorGroundAttacker:
                        if (trait == CharacterTraitType.StrongGroundAttacker || trait == CharacterTraitType.PoorGroundAttacker)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.StrongGroundDefender:
                    case CharacterTraitType.PoorGroundDefender:
                        if (trait == CharacterTraitType.StrongGroundAttacker || trait == CharacterTraitType.PoorGroundDefender)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodGroundLogistician:
                    case CharacterTraitType.PoorGroundLogistician:
                        if (trait == CharacterTraitType.GoodGroundLogistician || trait == CharacterTraitType.PoorGroundLogistician)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.NaturalGroundLeader:
                        if (trait == CharacterTraitType.NaturalGroundLeader)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.GoodRecruiter:
                    case CharacterTraitType.PoorRecruiter:
                        if (trait == CharacterTraitType.GoodRecruiter || trait == CharacterTraitType.PoorRecruiter)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.CarefulAttacker:
                    case CharacterTraitType.RecklessAttacker:
                        if (trait == CharacterTraitType.CarefulAttacker || trait == CharacterTraitType.RecklessAttacker)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.DoubleAgent:
                        if (trait == CharacterTraitType.DoubleAgent)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Creative:
                    case CharacterTraitType.Methodical:
                        if (trait == CharacterTraitType.Creative || trait == CharacterTraitType.Methodical)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.ForeignSpy:
                    case CharacterTraitType.Patriot:
                        if (trait == CharacterTraitType.ForeignSpy || trait == CharacterTraitType.Patriot)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.UltraGenius:
                        if (trait == CharacterTraitType.UltraGenius)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.Smuggler:
                        if (trait == CharacterTraitType.Smuggler)
                        {
                            return false;
                        }
                        break;
                    case CharacterTraitType.BountyHunter:
                        if (trait == CharacterTraitType.BountyHunter)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        public void RebuildCachedSkillValues()
        {
            _Diplomacy = 0;
            _ColonyIncome = 0;
            _TradeIncome = 0;
            _TourismIncome = 0;
            _ColonyCorruption = 0;
            _ColonyHappiness = 0;
            _PopulationGrowth = 0;
            _MiningRate = 0;
            _TroopRecruitmentRate = 0;
            _MilitaryShipConstructionSpeed = 0;
            _CivilianShipConstructionSpeed = 0;
            _ColonyShipConstructionSpeed = 0;
            _FacilityConstructionSpeed = 0;
            _ResearchWeapons = 0;
            _ResearchEnergy = 0;
            _ResearchHighTech = 0;
            _Espionage = 0;
            _CounterEspionage = 0;
            _Sabotage = 0;
            _Concealment = 0;
            _PsyOps = 0;
            _Assassination = 0;
            _MilitaryShipMaintenance = 0;
            _CivilianShipMaintenance = 0;
            _MilitaryBaseMaintenance = 0;
            _CivilianBaseMaintenance = 0;
            _TroopMaintenance = 0;
            _WarWeariness = 0;
            _Targeting = 0;
            _Countermeasures = 0;
            _ShipManeuvering = 0;
            _Fighters = 0;
            _ShipEnergyUsage = 0;
            _WeaponsDamage = 0;
            _WeaponsRange = 0;
            _ShieldRechargeRate = 0;
            _DamageControl = 0;
            _RepairBonus = 0;
            _HyperjumpSpeed = 0;
            _TroopGroundAttack = 0;
            _TroopGroundDefense = 0;
            _TroopExperienceGain = 0;
            _TroopRecoveryRate = 0;
            _TroopStrengthArmor = 0;
            _TroopStrengthInfantry = 0;
            _TroopStrengthSpecialForces = 0;
            _TroopStrengthPlanetaryDefense = 0;
            _SmugglingIncome = 0;
            _SmugglingEvasion = 0;
            _BoardingAssault = 0;
            for (int i = 0; i < Skills.Count; i++)
            {
                CharacterSkill characterSkill = Skills[i];
                if (characterSkill != null)
                {
                    sbyte b = (sbyte)Math.Min(100, Math.Max(-100, characterSkill.Level));
                    switch (characterSkill.Type)
                    {
                        case CharacterSkillType.Assassination:
                            _Assassination = b;
                            break;
                        case CharacterSkillType.CivilianBaseMaintenance:
                            _CivilianBaseMaintenance = b;
                            break;
                        case CharacterSkillType.CivilianShipConstructionSpeed:
                            _CivilianShipConstructionSpeed = b;
                            break;
                        case CharacterSkillType.CivilianShipMaintenance:
                            _CivilianShipMaintenance = b;
                            break;
                        case CharacterSkillType.ColonyCorruption:
                            _ColonyCorruption = b;
                            break;
                        case CharacterSkillType.ColonyHappiness:
                            _ColonyHappiness = b;
                            break;
                        case CharacterSkillType.ColonyIncome:
                            _ColonyIncome = b;
                            break;
                        case CharacterSkillType.ColonyShipConstructionSpeed:
                            _ColonyShipConstructionSpeed = b;
                            break;
                        case CharacterSkillType.Concealment:
                            _Concealment = b;
                            break;
                        case CharacterSkillType.CounterEspionage:
                            _CounterEspionage = b;
                            break;
                        case CharacterSkillType.Countermeasures:
                            _Countermeasures = b;
                            break;
                        case CharacterSkillType.DamageControl:
                            _DamageControl = b;
                            break;
                        case CharacterSkillType.Diplomacy:
                            _Diplomacy = b;
                            break;
                        case CharacterSkillType.Espionage:
                            _Espionage = b;
                            break;
                        case CharacterSkillType.FacilityConstructionSpeed:
                            _FacilityConstructionSpeed = b;
                            break;
                        case CharacterSkillType.Fighters:
                            _Fighters = b;
                            break;
                        case CharacterSkillType.HyperjumpSpeed:
                            _HyperjumpSpeed = b;
                            break;
                        case CharacterSkillType.MilitaryBaseMaintenance:
                            _MilitaryBaseMaintenance = b;
                            break;
                        case CharacterSkillType.MilitaryShipConstructionSpeed:
                            _MilitaryShipConstructionSpeed = b;
                            break;
                        case CharacterSkillType.MilitaryShipMaintenance:
                            _MilitaryShipMaintenance = b;
                            break;
                        case CharacterSkillType.MiningRate:
                            _MiningRate = b;
                            break;
                        case CharacterSkillType.PopulationGrowth:
                            _PopulationGrowth = b;
                            break;
                        case CharacterSkillType.PsyOps:
                            _PsyOps = b;
                            break;
                        case CharacterSkillType.RepairBonus:
                            _RepairBonus = b;
                            break;
                        case CharacterSkillType.ResearchEnergy:
                            _ResearchEnergy = b;
                            break;
                        case CharacterSkillType.ResearchHighTech:
                            _ResearchHighTech = b;
                            break;
                        case CharacterSkillType.ResearchWeapons:
                            _ResearchWeapons = b;
                            break;
                        case CharacterSkillType.Sabotage:
                            _Sabotage = b;
                            break;
                        case CharacterSkillType.ShieldRechargeRate:
                            _ShieldRechargeRate = b;
                            break;
                        case CharacterSkillType.ShipEnergyUsage:
                            _ShipEnergyUsage = b;
                            break;
                        case CharacterSkillType.ShipManeuvering:
                            _ShipManeuvering = b;
                            break;
                        case CharacterSkillType.Targeting:
                            _Targeting = b;
                            break;
                        case CharacterSkillType.TourismIncome:
                            _TourismIncome = b;
                            break;
                        case CharacterSkillType.TradeIncome:
                            _TradeIncome = b;
                            break;
                        case CharacterSkillType.TroopExperienceGain:
                            _TroopExperienceGain = b;
                            break;
                        case CharacterSkillType.TroopGroundAttack:
                            _TroopGroundAttack = b;
                            break;
                        case CharacterSkillType.TroopGroundDefense:
                            _TroopGroundDefense = b;
                            break;
                        case CharacterSkillType.TroopMaintenance:
                            _TroopMaintenance = b;
                            break;
                        case CharacterSkillType.TroopRecoveryRate:
                            _TroopRecoveryRate = b;
                            break;
                        case CharacterSkillType.TroopRecruitment:
                            _TroopRecruitmentRate = b;
                            break;
                        case CharacterSkillType.WarWeariness:
                            _WarWeariness = b;
                            break;
                        case CharacterSkillType.WeaponsDamage:
                            _WeaponsDamage = b;
                            break;
                        case CharacterSkillType.WeaponsRange:
                            _WeaponsRange = b;
                            break;
                        case CharacterSkillType.TroopStrengthArmor:
                            _TroopStrengthArmor = b;
                            break;
                        case CharacterSkillType.TroopStrengthInfantry:
                            _TroopStrengthInfantry = b;
                            break;
                        case CharacterSkillType.TroopStrengthSpecialForces:
                            _TroopStrengthSpecialForces = b;
                            break;
                        case CharacterSkillType.TroopStrengthPlanetaryDefense:
                            _TroopStrengthPlanetaryDefense = b;
                            break;
                        case CharacterSkillType.SmugglingIncome:
                            _SmugglingIncome = b;
                            break;
                        case CharacterSkillType.SmugglingEvasion:
                            _SmugglingEvasion = b;
                            break;
                        case CharacterSkillType.BoardingAssault:
                            _BoardingAssault = b;
                            break;
                    }
                }
            }
        }

        public void ReviewTraitSkills()
        {
            _DiplomacyTraits = 0;
            _ColonyIncomeTraits = 0;
            _TradeIncomeTraits = 0;
            _TourismIncomeTraits = 0;
            _ColonyCorruptionTraits = 0;
            _ColonyHappinessTraits = 0;
            _PopulationGrowthTraits = 0;
            _MiningRateTraits = 0;
            _TroopRecruitmentRateTraits = 0;
            _MilitaryShipConstructionSpeedTraits = 0;
            _CivilianShipConstructionSpeedTraits = 0;
            _ColonyShipConstructionSpeedTraits = 0;
            _FacilityConstructionSpeedTraits = 0;
            _ResearchWeaponsTraits = 0;
            _ResearchEnergyTraits = 0;
            _ResearchHighTechTraits = 0;
            _EspionageTraits = 0;
            _CounterEspionageTraits = 0;
            _SabotageTraits = 0;
            _ConcealmentTraits = 0;
            _PsyOpsTraits = 0;
            _AssassinationTraits = 0;
            _MilitaryShipMaintenanceTraits = 0;
            _CivilianShipMaintenanceTraits = 0;
            _MilitaryBaseMaintenanceTraits = 0;
            _CivilianBaseMaintenanceTraits = 0;
            _TroopMaintenanceTraits = 0;
            _WarWearinessTraits = 0;
            _TargetingTraits = 0;
            _CountermeasuresTraits = 0;
            _ShipManeuveringTraits = 0;
            _FightersTraits = 0;
            _ShipEnergyUsageTraits = 0;
            _WeaponsDamageTraits = 0;
            _WeaponsRangeTraits = 0;
            _ShieldRechargeRateTraits = 0;
            _DamageControlTraits = 0;
            _RepairBonusTraits = 0;
            _HyperjumpSpeedTraits = 0;
            _TroopGroundAttackTraits = 0;
            _TroopGroundDefenseTraits = 0;
            _TroopExperienceGainTraits = 0;
            _TroopRecoveryRateTraits = 0;
            _TroopStrengthArmorTraits = 0;
            _TroopStrengthInfantryTraits = 0;
            _TroopStrengthSpecialForcesTraits = 0;
            _TroopStrengthPlanetaryDefenseTraits = 0;
            _SmugglingIncomeTraits = 0;
            _SmugglingEvasionTraits = 0;
            _BoardingAssaultTraits = 0;
            TraitSkills.Clear();
            sbyte b = 10;
            sbyte b2 = 5;
            sbyte b3 = 20;
            for (int i = 0; i < Traits.Count; i++)
            {
                CharacterTraitType characterTraitType = Traits[i];
                if (!Galaxy.CheckCharacterTraitAppliesOnlyToExistingSkills(characterTraitType))
                {
                    CharacterSkillList newSkills = Galaxy.DetermineEffectsOfCharacterTrait(characterTraitType, Role);
                    TraitSkills.CombineSkillList(newSkills);
                    switch (characterTraitType)
                    {
                        case CharacterTraitType.Smuggler:
                            _SmugglingIncomeTraits += b;
                            _SmugglingEvasionTraits += b;
                            _DamageControlTraits += b;
                            _CountermeasuresTraits += b;
                            break;
                        case CharacterTraitType.BountyHunter:
                            _BoardingAssaultTraits += b;
                            _WeaponsDamageTraits += b;
                            _TargetingTraits += b;
                            break;
                        case CharacterTraitType.IntelligenceCorrupt:
                            _EspionageTraits -= b;
                            _CounterEspionageTraits -= b;
                            _SabotageTraits -= b;
                            _PsyOpsTraits -= b;
                            _ConcealmentTraits -= b;
                            _AssassinationTraits -= b;
                            break;
                        case CharacterTraitType.IntelligenceCourageous:
                            _EspionageTraits += b;
                            _CounterEspionageTraits += b;
                            _SabotageTraits += b;
                            _PsyOpsTraits += b;
                            _ConcealmentTraits += b;
                            _AssassinationTraits += b;
                            break;
                        case CharacterTraitType.IntelligenceEloquentSpeaker:
                            _PsyOpsTraits += b;
                            break;
                        case CharacterTraitType.IntelligenceLawful:
                            _EspionageTraits -= b;
                            _CounterEspionageTraits += b;
                            _SabotageTraits -= b;
                            _PsyOpsTraits -= b;
                            _ConcealmentTraits -= b;
                            _AssassinationTraits -= b;
                            break;
                        case CharacterTraitType.IntelligenceMeasured:
                            _ConcealmentTraits += b;
                            _PsyOpsTraits += b;
                            break;
                        case CharacterTraitType.IntelligencePoorSpeaker:
                            _PsyOpsTraits -= b;
                            break;
                        case CharacterTraitType.IntelligenceTolerant:
                            _CounterEspionageTraits -= b;
                            _ConcealmentTraits += b;
                            break;
                        case CharacterTraitType.IntelligenceUninhibited:
                            _ConcealmentTraits -= b;
                            _PsyOpsTraits -= b;
                            break;
                        case CharacterTraitType.IntelligenceWeak:
                            _EspionageTraits -= b;
                            _CounterEspionageTraits -= b;
                            _SabotageTraits -= b;
                            _PsyOpsTraits -= b;
                            _ConcealmentTraits -= b;
                            _AssassinationTraits -= b;
                            break;
                        case CharacterTraitType.IntelligenceXenophobic:
                            _CounterEspionageTraits += b;
                            _ConcealmentTraits -= b;
                            break;
                        case CharacterTraitType.Paranoid:
                            _CounterEspionageTraits += b;
                            _DiplomacyTraits -= b;
                            _ColonyHappinessTraits -= b;
                            break;
                        case CharacterTraitType.Trusting:
                            _CounterEspionageTraits -= b;
                            _DiplomacyTraits += b;
                            _ColonyHappinessTraits += b;
                            break;
                        case CharacterTraitType.PeaceThroughStrength:
                            _TroopRecruitmentRateTraits += b;
                            _MilitaryShipConstructionSpeedTraits += b;
                            _ColonyHappinessTraits -= b;
                            break;
                        case CharacterTraitType.Pacifist:
                            _TroopRecruitmentRateTraits -= b;
                            _MilitaryShipConstructionSpeedTraits -= b;
                            _ColonyHappinessTraits += b;
                            break;
                        case CharacterTraitType.Expansionist:
                            _ResearchHighTechTraits += b;
                            _ColonyShipConstructionSpeedTraits += b;
                            break;
                        case CharacterTraitType.Isolationist:
                            _ResearchWeaponsTraits += b;
                            _ColonyShipConstructionSpeedTraits -= b;
                            break;
                        case CharacterTraitType.Diplomat:
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.Obnoxious:
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.Famous:
                            _ColonyHappinessTraits += b;
                            _TourismIncomeTraits += b;
                            break;
                        case CharacterTraitType.Disliked:
                            _ColonyHappinessTraits -= b;
                            _TourismIncomeTraits -= b;
                            break;
                        case CharacterTraitType.GoodAdministrator:
                            _ColonyIncomeTraits += b;
                            break;
                        case CharacterTraitType.PoorAdministrator:
                            _ColonyIncomeTraits -= b;
                            break;
                        case CharacterTraitType.BeanCounter:
                            _ColonyCorruptionTraits += b;
                            _ColonyHappinessTraits -= b;
                            break;
                        case CharacterTraitType.Generous:
                            _ColonyCorruptionTraits -= b;
                            _ColonyHappinessTraits += b;
                            break;
                        case CharacterTraitType.Engineer:
                            _ResearchHighTechTraits += b;
                            _CivilianShipConstructionSpeedTraits += b;
                            break;
                        case CharacterTraitType.Luddite:
                            _ResearchHighTechTraits -= b;
                            _CivilianShipConstructionSpeedTraits -= b;
                            break;
                        case CharacterTraitType.FreeTrader:
                            _TradeIncomeTraits += b;
                            _CivilianShipConstructionSpeedTraits += b;
                            break;
                        case CharacterTraitType.Protectionist:
                            _TradeIncomeTraits -= b;
                            _CivilianShipConstructionSpeedTraits -= b;
                            break;
                        case CharacterTraitType.Environmentalist:
                            _PopulationGrowthTraits += b;
                            _MiningRateTraits -= b;
                            break;
                        case CharacterTraitType.Industrialist:
                            _PopulationGrowthTraits -= b;
                            _MiningRateTraits += b;
                            break;
                        case CharacterTraitType.Organized:
                            _MilitaryShipConstructionSpeedTraits += b;
                            _CivilianShipConstructionSpeedTraits += b;
                            _ColonyShipConstructionSpeedTraits += b;
                            break;
                        case CharacterTraitType.Disorganized:
                            _MilitaryShipConstructionSpeedTraits -= b;
                            _CivilianShipConstructionSpeedTraits -= b;
                            _ColonyShipConstructionSpeedTraits -= b;
                            break;
                        case CharacterTraitType.HealthOriented:
                            _PopulationGrowthTraits += b;
                            _ColonyHappinessTraits += b;
                            _ColonyIncomeTraits -= b;
                            break;
                        case CharacterTraitType.LaborOriented:
                            _PopulationGrowthTraits -= b;
                            _ColonyHappinessTraits -= b;
                            _ColonyIncomeTraits += b;
                            break;
                        case CharacterTraitType.Spiritual:
                            _ColonyHappinessTraits += b;
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.Logical:
                            _ColonyHappinessTraits -= b;
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.GoodStrategist:
                            _TroopMaintenanceTraits += b;
                            _MilitaryShipMaintenanceTraits += b;
                            break;
                        case CharacterTraitType.PoorStrategist:
                            _TroopMaintenanceTraits -= b;
                            _MilitaryShipMaintenanceTraits -= b;
                            break;
                        case CharacterTraitType.Uninhibited:
                            _ColonyHappinessTraits += b;
                            _ColonyCorruptionTraits -= b;
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.Measured:
                            _ColonyHappinessTraits -= b;
                            _ColonyCorruptionTraits += b;
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.Addict:
                            _ColonyCorruptionTraits -= b;
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.Sober:
                            _ColonyCorruptionTraits += b;
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.Courageous:
                            _WarWearinessTraits += b;
                            _TroopRecruitmentRateTraits += b;
                            break;
                        case CharacterTraitType.Weak:
                            _WarWearinessTraits -= b;
                            _TroopRecruitmentRateTraits -= b;
                            break;
                        case CharacterTraitType.Tolerant:
                            _TradeIncomeTraits += b;
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.Xenophobic:
                            _TradeIncomeTraits -= b;
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.EloquentSpeaker:
                            _ColonyHappinessTraits += b;
                            _DiplomacyTraits += b;
                            break;
                        case CharacterTraitType.PoorSpeaker:
                            _ColonyHappinessTraits -= b;
                            _DiplomacyTraits -= b;
                            break;
                        case CharacterTraitType.Corrupt:
                            _ColonyCorruptionTraits -= b;
                            _TradeIncomeTraits -= b;
                            _TourismIncomeTraits -= b;
                            break;
                        case CharacterTraitType.Lawful:
                            _ColonyCorruptionTraits += b;
                            _TradeIncomeTraits += b;
                            _TourismIncomeTraits += b;
                            break;
                        case CharacterTraitType.Linguist:
                            _DiplomacyTraits += b;
                            _TourismIncomeTraits += b;
                            break;
                        case CharacterTraitType.TongueTied:
                            _DiplomacyTraits -= b;
                            _TourismIncomeTraits -= b;
                            break;
                        case CharacterTraitType.Technical:
                            _MilitaryShipConstructionSpeedTraits += b;
                            _CivilianShipConstructionSpeedTraits += b;
                            _ColonyShipConstructionSpeedTraits += b;
                            _FacilityConstructionSpeedTraits += b;
                            break;
                        case CharacterTraitType.NonTechnical:
                            _MilitaryShipConstructionSpeedTraits -= b;
                            _CivilianShipConstructionSpeedTraits -= b;
                            _ColonyShipConstructionSpeedTraits -= b;
                            _FacilityConstructionSpeedTraits -= b;
                            break;
                        case CharacterTraitType.StrongSpaceAttacker:
                            _TargetingTraits += b;
                            _ShipManeuveringTraits += b;
                            _WeaponsDamageTraits += b;
                            break;
                        case CharacterTraitType.PoorSpaceAttacker:
                            _TargetingTraits -= b;
                            _ShipManeuveringTraits -= b;
                            _WeaponsDamageTraits -= b;
                            break;
                        case CharacterTraitType.StrongSpaceDefender:
                            _CountermeasuresTraits += b;
                            _ShipManeuveringTraits += b;
                            _ShieldRechargeRateTraits += b;
                            break;
                        case CharacterTraitType.PoorSpaceDefender:
                            _CountermeasuresTraits -= b;
                            _ShipManeuveringTraits -= b;
                            _ShieldRechargeRateTraits -= b;
                            break;
                        case CharacterTraitType.GoodSpaceLogistician:
                            _ShipEnergyUsageTraits += b;
                            break;
                        case CharacterTraitType.PoorSpaceLogistician:
                            _ShipEnergyUsageTraits -= b;
                            break;
                        case CharacterTraitType.NaturalSpaceLeader:
                            _WeaponsDamageTraits += b;
                            _DamageControlTraits += b;
                            _TargetingTraits += b;
                            _CountermeasuresTraits += b;
                            break;
                        case CharacterTraitType.SkilledNavigator:
                            _HyperjumpSpeedTraits += b;
                            break;
                        case CharacterTraitType.PoorNavigator:
                            _HyperjumpSpeedTraits -= b;
                            break;
                        case CharacterTraitType.StrongGroundAttacker:
                            _TroopGroundAttackTraits += b;
                            break;
                        case CharacterTraitType.PoorGroundAttacker:
                            _TroopGroundAttackTraits -= b;
                            break;
                        case CharacterTraitType.StrongGroundDefender:
                            _TroopGroundDefenseTraits += b;
                            break;
                        case CharacterTraitType.PoorGroundDefender:
                            _TroopGroundDefenseTraits -= b;
                            break;
                        case CharacterTraitType.GoodGroundLogistician:
                            _TroopMaintenanceTraits += b;
                            break;
                        case CharacterTraitType.PoorGroundLogistician:
                            _TroopMaintenanceTraits -= b;
                            break;
                        case CharacterTraitType.NaturalGroundLeader:
                            _TroopGroundAttackTraits += b;
                            _TroopGroundDefenseTraits += b;
                            _TroopRecruitmentRateTraits += b;
                            _TroopExperienceGainTraits += b;
                            break;
                        case CharacterTraitType.GoodRecruiter:
                            _TroopRecruitmentRateTraits += b;
                            break;
                        case CharacterTraitType.PoorRecruiter:
                            _TroopRecruitmentRateTraits -= b;
                            break;
                        case CharacterTraitType.CarefulAttacker:
                            _TroopGroundAttackTraits -= b;
                            _TroopGroundDefenseTraits += b;
                            _TroopRecoveryRateTraits += b;
                            break;
                        case CharacterTraitType.RecklessAttacker:
                            _TroopGroundAttackTraits += b;
                            _TroopGroundDefenseTraits -= b;
                            _TroopRecoveryRateTraits -= b;
                            break;
                        case CharacterTraitType.DoubleAgent:
                            _EspionageTraits -= b3;
                            _CounterEspionageTraits -= b3;
                            _SabotageTraits -= b3;
                            _ConcealmentTraits -= b3;
                            _PsyOpsTraits -= b3;
                            _AssassinationTraits -= b3;
                            break;
                    }
                }
            }
            for (int j = 0; j < Traits.Count; j++)
            {
                CharacterTraitType characterTraitType2 = Traits[j];
                if (!Galaxy.CheckCharacterTraitAppliesOnlyToExistingSkills(characterTraitType2))
                {
                    continue;
                }
                switch (characterTraitType2)
                {
                    case CharacterTraitType.Lazy:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits -= b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits -= b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits -= b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits -= b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits -= b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits -= b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits -= b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits -= b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits -= b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits -= b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits -= b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits -= b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits -= b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits -= b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits -= b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits -= b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits -= b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits -= b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits -= b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits -= b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits -= b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits -= b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits -= b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits -= b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits -= b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits -= b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits -= b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits -= b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits -= b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits -= b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits -= b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits -= b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits -= b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits -= b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits -= b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits -= b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits -= b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits -= b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits -= b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits -= b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits -= b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits -= b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits -= b2;
                        }
                        break;
                    case CharacterTraitType.Energetic:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits += b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits += b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits += b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits += b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits += b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits += b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits += b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits += b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits += b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits += b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits += b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits += b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits += b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits += b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits += b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits += b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits += b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits += b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits += b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits += b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits += b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits += b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits += b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits += b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits += b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits += b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits += b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits += b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits += b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits += b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits += b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits += b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits += b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits += b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits += b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits += b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits += b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits += b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits += b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits += b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits += b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits += b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits += b2;
                        }
                        break;
                    case CharacterTraitType.Drunk:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits -= b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits -= b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits -= b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits -= b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits -= b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits -= b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits -= b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits -= b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits -= b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits -= b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits -= b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits -= b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits -= b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits -= b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits -= b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits -= b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits -= b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits -= b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits -= b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits -= b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits -= b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits -= b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits -= b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits -= b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits -= b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits -= b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits -= b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits -= b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits -= b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits -= b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits -= b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits -= b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits -= b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits -= b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits -= b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits -= b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits -= b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits -= b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits -= b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits -= b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits -= b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits -= b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits -= b2;
                        }
                        break;
                    case CharacterTraitType.ToughDiscipline:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits += b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits += b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits += b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits += b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits += b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits += b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits += b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits += b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits += b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits += b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits += b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits += b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits += b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits += b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits += b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits += b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits += b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits += b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits += b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits += b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits += b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits += b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits += b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits += b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits += b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits += b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits += b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits += b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits += b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits += b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits += b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits += b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits += b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits += b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits += b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits += b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits += b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits += b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits += b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits += b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits += b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits += b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits += b2;
                        }
                        break;
                    case CharacterTraitType.LaxDiscipline:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits -= b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits -= b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits -= b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits -= b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits -= b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits -= b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits -= b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits -= b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits -= b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits -= b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits -= b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits -= b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits -= b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits -= b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits -= b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits -= b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits -= b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits -= b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits -= b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits -= b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits -= b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits -= b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits -= b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits -= b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits -= b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits -= b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits -= b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits -= b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits -= b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits -= b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits -= b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits -= b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits -= b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits -= b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits -= b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits -= b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits -= b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits -= b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits -= b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits -= b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits -= b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits -= b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits -= b2;
                        }
                        break;
                    case CharacterTraitType.IntelligenceAddict:
                        if (_Espionage != 0)
                        {
                            _EspionageTraits -= b;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits -= b;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits -= b;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits -= b;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits -= b;
                        }
                        break;
                    case CharacterTraitType.IntelligenceSober:
                        if (_Espionage != 0)
                        {
                            _EspionageTraits += b;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits += b;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits += b;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits += b;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits += b;
                        }
                        break;
                    case CharacterTraitType.GoodTactician:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits += b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits += b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits += b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits += b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits += b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits += b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits += b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits += b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits += b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits += b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits += b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits += b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits += b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits += b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits += b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits += b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits += b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits += b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits += b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits += b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits += b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits += b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits += b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits += b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits += b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits += b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits += b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits += b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits += b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits += b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits += b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits += b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits += b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits += b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits += b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits += b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits += b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits += b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits += b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits += b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits += b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits += b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits += b2;
                        }
                        break;
                    case CharacterTraitType.PoorTactician:
                        if (_Diplomacy != 0)
                        {
                            _DiplomacyTraits -= b2;
                        }
                        if (_ColonyIncome != 0)
                        {
                            _ColonyIncomeTraits -= b2;
                        }
                        if (_TradeIncome != 0)
                        {
                            _TradeIncomeTraits -= b2;
                        }
                        if (_TourismIncome != 0)
                        {
                            _TourismIncomeTraits -= b2;
                        }
                        if (_ColonyCorruption != 0)
                        {
                            _ColonyCorruptionTraits -= b2;
                        }
                        if (_ColonyHappiness != 0)
                        {
                            _ColonyHappinessTraits -= b2;
                        }
                        if (_PopulationGrowth != 0)
                        {
                            _PopulationGrowthTraits -= b2;
                        }
                        if (_MiningRate != 0)
                        {
                            _MiningRateTraits -= b2;
                        }
                        if (_TroopRecruitmentRate != 0)
                        {
                            _TroopRecruitmentRateTraits -= b2;
                        }
                        if (_MilitaryShipConstructionSpeed != 0)
                        {
                            _MilitaryShipConstructionSpeedTraits -= b2;
                        }
                        if (_CivilianShipConstructionSpeed != 0)
                        {
                            _CivilianShipConstructionSpeedTraits -= b2;
                        }
                        if (_ColonyShipConstructionSpeed != 0)
                        {
                            _ColonyShipConstructionSpeedTraits -= b2;
                        }
                        if (_FacilityConstructionSpeed != 0)
                        {
                            _FacilityConstructionSpeedTraits -= b2;
                        }
                        if (_ResearchWeapons != 0)
                        {
                            _ResearchWeaponsTraits -= b2;
                        }
                        if (_ResearchEnergy != 0)
                        {
                            _ResearchEnergyTraits -= b2;
                        }
                        if (_ResearchHighTech != 0)
                        {
                            _ResearchHighTechTraits -= b2;
                        }
                        if (_Espionage != 0)
                        {
                            _EspionageTraits -= b2;
                        }
                        if (_CounterEspionage != 0)
                        {
                            _CounterEspionageTraits -= b2;
                        }
                        if (_Sabotage != 0)
                        {
                            _SabotageTraits -= b2;
                        }
                        if (_Concealment != 0)
                        {
                            _ConcealmentTraits -= b2;
                        }
                        if (_PsyOps != 0)
                        {
                            _PsyOpsTraits -= b2;
                        }
                        if (_Assassination != 0)
                        {
                            _AssassinationTraits -= b2;
                        }
                        if (_MilitaryShipMaintenance != 0)
                        {
                            _MilitaryShipMaintenanceTraits -= b2;
                        }
                        if (_CivilianShipMaintenance != 0)
                        {
                            _CivilianShipMaintenanceTraits -= b2;
                        }
                        if (_MilitaryBaseMaintenance != 0)
                        {
                            _MilitaryBaseMaintenanceTraits -= b2;
                        }
                        if (_CivilianBaseMaintenance != 0)
                        {
                            _CivilianBaseMaintenanceTraits -= b2;
                        }
                        if (_TroopMaintenance != 0)
                        {
                            _TroopMaintenanceTraits -= b2;
                        }
                        if (_WarWeariness != 0)
                        {
                            _WarWearinessTraits -= b2;
                        }
                        if (_Targeting != 0)
                        {
                            _TargetingTraits -= b2;
                        }
                        if (_Countermeasures != 0)
                        {
                            _CountermeasuresTraits -= b2;
                        }
                        if (_ShipManeuvering != 0)
                        {
                            _ShipManeuveringTraits -= b2;
                        }
                        if (_Fighters != 0)
                        {
                            _FightersTraits -= b2;
                        }
                        if (_ShipEnergyUsage != 0)
                        {
                            _ShipEnergyUsageTraits -= b2;
                        }
                        if (_WeaponsDamage != 0)
                        {
                            _WeaponsDamageTraits -= b2;
                        }
                        if (_WeaponsRange != 0)
                        {
                            _WeaponsRangeTraits -= b2;
                        }
                        if (_ShieldRechargeRate != 0)
                        {
                            _ShieldRechargeRateTraits -= b2;
                        }
                        if (_DamageControl != 0)
                        {
                            _DamageControlTraits -= b2;
                        }
                        if (_RepairBonus != 0)
                        {
                            _RepairBonusTraits -= b2;
                        }
                        if (_HyperjumpSpeed != 0)
                        {
                            _HyperjumpSpeedTraits -= b2;
                        }
                        if (_TroopGroundAttack != 0)
                        {
                            _TroopGroundAttackTraits -= b2;
                        }
                        if (_TroopGroundDefense != 0)
                        {
                            _TroopGroundDefenseTraits -= b2;
                        }
                        if (_TroopExperienceGain != 0)
                        {
                            _TroopExperienceGainTraits -= b2;
                        }
                        if (_TroopRecoveryRate != 0)
                        {
                            _TroopRecoveryRateTraits -= b2;
                        }
                        break;
                }
            }
            ClearIrrelevantTraitSkills();
        }

        public static List<CharacterTraitType> DetermineValidTraitsForRole(CharacterRole role)
        {
            return DetermineValidTraitsForRole(role, includeStartingTraits: false);
        }

        public static List<CharacterTraitType> DetermineValidTraitsForRole(CharacterRole role, bool includeStartingTraits)
        {
            return DetermineValidTraitsForRole(role, includeStartingTraits, includeHighlyNegativeTraits: true);
        }

        public static List<CharacterTraitType> DetermineValidTraitsForRole(CharacterRole role, bool includeStartingTraits, bool includeHighlyNegativeTraits)
        {
            List<CharacterTraitType> list = new List<CharacterTraitType>();
            switch (role)
            {
                case CharacterRole.Ambassador:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.Trusting);
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Spiritual);
                        list.Add(CharacterTraitType.Logical);
                        list.Add(CharacterTraitType.Energetic);
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Addict);
                        list.Add(CharacterTraitType.Lazy);
                    }
                    list.Add(CharacterTraitType.Linguist);
                    list.Add(CharacterTraitType.TongueTied);
                    list.Add(CharacterTraitType.Diplomat);
                    list.Add(CharacterTraitType.Obnoxious);
                    list.Add(CharacterTraitType.Paranoid);
                    list.Add(CharacterTraitType.Famous);
                    list.Add(CharacterTraitType.Disliked);
                    list.Add(CharacterTraitType.FreeTrader);
                    list.Add(CharacterTraitType.Protectionist);
                    list.Add(CharacterTraitType.Uninhibited);
                    list.Add(CharacterTraitType.Measured);
                    list.Add(CharacterTraitType.Sober);
                    list.Add(CharacterTraitType.Tolerant);
                    list.Add(CharacterTraitType.Xenophobic);
                    list.Add(CharacterTraitType.EloquentSpeaker);
                    list.Add(CharacterTraitType.PoorSpeaker);
                    list.Add(CharacterTraitType.Corrupt);
                    list.Add(CharacterTraitType.Lawful);
                    break;
                case CharacterRole.ColonyGovernor:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.Trusting);
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Spiritual);
                        list.Add(CharacterTraitType.Logical);
                        list.Add(CharacterTraitType.Energetic);
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Luddite);
                        list.Add(CharacterTraitType.Addict);
                        list.Add(CharacterTraitType.Lazy);
                    }
                    list.Add(CharacterTraitType.Paranoid);
                    list.Add(CharacterTraitType.PeaceThroughStrength);
                    list.Add(CharacterTraitType.Pacifist);
                    list.Add(CharacterTraitType.Expansionist);
                    list.Add(CharacterTraitType.Isolationist);
                    list.Add(CharacterTraitType.Famous);
                    list.Add(CharacterTraitType.Disliked);
                    list.Add(CharacterTraitType.GoodAdministrator);
                    list.Add(CharacterTraitType.PoorAdministrator);
                    list.Add(CharacterTraitType.BeanCounter);
                    list.Add(CharacterTraitType.Generous);
                    list.Add(CharacterTraitType.Engineer);
                    list.Add(CharacterTraitType.FreeTrader);
                    list.Add(CharacterTraitType.Protectionist);
                    list.Add(CharacterTraitType.Environmentalist);
                    list.Add(CharacterTraitType.Industrialist);
                    list.Add(CharacterTraitType.Organized);
                    list.Add(CharacterTraitType.Disorganized);
                    list.Add(CharacterTraitType.HealthOriented);
                    list.Add(CharacterTraitType.LaborOriented);
                    list.Add(CharacterTraitType.GoodStrategist);
                    list.Add(CharacterTraitType.PoorStrategist);
                    list.Add(CharacterTraitType.Uninhibited);
                    list.Add(CharacterTraitType.Measured);
                    list.Add(CharacterTraitType.Sober);
                    list.Add(CharacterTraitType.Courageous);
                    list.Add(CharacterTraitType.Weak);
                    list.Add(CharacterTraitType.Tolerant);
                    list.Add(CharacterTraitType.Xenophobic);
                    list.Add(CharacterTraitType.EloquentSpeaker);
                    list.Add(CharacterTraitType.PoorSpeaker);
                    list.Add(CharacterTraitType.Corrupt);
                    list.Add(CharacterTraitType.Lawful);
                    list.Add(CharacterTraitType.Technical);
                    list.Add(CharacterTraitType.NonTechnical);
                    break;
                case CharacterRole.FleetAdmiral:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Energetic);
                        list.Add(CharacterTraitType.ToughDiscipline);
                        list.Add(CharacterTraitType.NaturalSpaceLeader);
                        if (includeHighlyNegativeTraits)
                        {
                            list.Add(CharacterTraitType.LaxDiscipline);
                        }
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Lazy);
                        list.Add(CharacterTraitType.Drunk);
                    }
                    list.Add(CharacterTraitType.GoodTactician);
                    list.Add(CharacterTraitType.PoorTactician);
                    list.Add(CharacterTraitType.StrongSpaceAttacker);
                    list.Add(CharacterTraitType.PoorSpaceAttacker);
                    list.Add(CharacterTraitType.StrongSpaceDefender);
                    list.Add(CharacterTraitType.PoorSpaceDefender);
                    list.Add(CharacterTraitType.LocalDefenseTactics);
                    list.Add(CharacterTraitType.GoodSpaceLogistician);
                    list.Add(CharacterTraitType.PoorSpaceLogistician);
                    list.Add(CharacterTraitType.SkilledNavigator);
                    list.Add(CharacterTraitType.PoorNavigator);
                    break;
                case CharacterRole.ShipCaptain:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Energetic);
                        list.Add(CharacterTraitType.ToughDiscipline);
                        list.Add(CharacterTraitType.NaturalSpaceLeader);
                        if (includeHighlyNegativeTraits)
                        {
                            list.Add(CharacterTraitType.LaxDiscipline);
                        }
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Lazy);
                        list.Add(CharacterTraitType.Drunk);
                    }
                    list.Add(CharacterTraitType.GoodTactician);
                    list.Add(CharacterTraitType.PoorTactician);
                    list.Add(CharacterTraitType.StrongSpaceAttacker);
                    list.Add(CharacterTraitType.PoorSpaceAttacker);
                    list.Add(CharacterTraitType.StrongSpaceDefender);
                    list.Add(CharacterTraitType.PoorSpaceDefender);
                    list.Add(CharacterTraitType.LocalDefenseTactics);
                    list.Add(CharacterTraitType.GoodSpaceLogistician);
                    list.Add(CharacterTraitType.PoorSpaceLogistician);
                    list.Add(CharacterTraitType.SkilledNavigator);
                    list.Add(CharacterTraitType.PoorNavigator);
                    list.Add(CharacterTraitType.Smuggler);
                    list.Add(CharacterTraitType.BountyHunter);
                    break;
                case CharacterRole.IntelligenceAgent:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.IntelligenceCourageous);
                        list.Add(CharacterTraitType.Energetic);
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Lazy);
                        list.Add(CharacterTraitType.IntelligenceAddict);
                        list.Add(CharacterTraitType.DoubleAgent);
                    }
                    list.Add(CharacterTraitType.IntelligenceUninhibited);
                    list.Add(CharacterTraitType.IntelligenceMeasured);
                    list.Add(CharacterTraitType.IntelligenceSober);
                    list.Add(CharacterTraitType.IntelligenceWeak);
                    list.Add(CharacterTraitType.IntelligenceTolerant);
                    list.Add(CharacterTraitType.IntelligenceXenophobic);
                    list.Add(CharacterTraitType.IntelligenceEloquentSpeaker);
                    list.Add(CharacterTraitType.IntelligencePoorSpeaker);
                    list.Add(CharacterTraitType.IntelligenceCorrupt);
                    list.Add(CharacterTraitType.IntelligenceLawful);
                    break;
                case CharacterRole.Leader:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.Trusting);
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Spiritual);
                        list.Add(CharacterTraitType.Logical);
                        list.Add(CharacterTraitType.Courageous);
                        list.Add(CharacterTraitType.Energetic);
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Luddite);
                        list.Add(CharacterTraitType.Addict);
                        list.Add(CharacterTraitType.Lazy);
                    }
                    list.Add(CharacterTraitType.Paranoid);
                    list.Add(CharacterTraitType.PeaceThroughStrength);
                    list.Add(CharacterTraitType.Pacifist);
                    list.Add(CharacterTraitType.Expansionist);
                    list.Add(CharacterTraitType.Isolationist);
                    list.Add(CharacterTraitType.Diplomat);
                    list.Add(CharacterTraitType.Obnoxious);
                    list.Add(CharacterTraitType.Famous);
                    list.Add(CharacterTraitType.Disliked);
                    list.Add(CharacterTraitType.GoodAdministrator);
                    list.Add(CharacterTraitType.PoorAdministrator);
                    list.Add(CharacterTraitType.BeanCounter);
                    list.Add(CharacterTraitType.Generous);
                    list.Add(CharacterTraitType.Engineer);
                    list.Add(CharacterTraitType.FreeTrader);
                    list.Add(CharacterTraitType.Protectionist);
                    list.Add(CharacterTraitType.Environmentalist);
                    list.Add(CharacterTraitType.Industrialist);
                    list.Add(CharacterTraitType.Organized);
                    list.Add(CharacterTraitType.Disorganized);
                    list.Add(CharacterTraitType.HealthOriented);
                    list.Add(CharacterTraitType.LaborOriented);
                    list.Add(CharacterTraitType.GoodStrategist);
                    list.Add(CharacterTraitType.PoorStrategist);
                    list.Add(CharacterTraitType.Uninhibited);
                    list.Add(CharacterTraitType.Measured);
                    list.Add(CharacterTraitType.Sober);
                    list.Add(CharacterTraitType.Weak);
                    list.Add(CharacterTraitType.Tolerant);
                    list.Add(CharacterTraitType.Xenophobic);
                    list.Add(CharacterTraitType.EloquentSpeaker);
                    list.Add(CharacterTraitType.PoorSpeaker);
                    list.Add(CharacterTraitType.Corrupt);
                    list.Add(CharacterTraitType.Lawful);
                    break;
                case CharacterRole.PirateLeader:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.Trusting);
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Spiritual);
                        list.Add(CharacterTraitType.Logical);
                        list.Add(CharacterTraitType.Energetic);
                        list.Add(CharacterTraitType.ToughDiscipline);
                        list.Add(CharacterTraitType.NaturalSpaceLeader);
                        if (includeHighlyNegativeTraits)
                        {
                            list.Add(CharacterTraitType.LaxDiscipline);
                        }
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Luddite);
                        list.Add(CharacterTraitType.Addict);
                        list.Add(CharacterTraitType.Lazy);
                        list.Add(CharacterTraitType.Drunk);
                    }
                    list.Add(CharacterTraitType.Paranoid);
                    list.Add(CharacterTraitType.PeaceThroughStrength);
                    list.Add(CharacterTraitType.Pacifist);
                    list.Add(CharacterTraitType.Expansionist);
                    list.Add(CharacterTraitType.Isolationist);
                    list.Add(CharacterTraitType.Diplomat);
                    list.Add(CharacterTraitType.Obnoxious);
                    list.Add(CharacterTraitType.Famous);
                    list.Add(CharacterTraitType.Disliked);
                    list.Add(CharacterTraitType.Engineer);
                    list.Add(CharacterTraitType.FreeTrader);
                    list.Add(CharacterTraitType.Protectionist);
                    list.Add(CharacterTraitType.Environmentalist);
                    list.Add(CharacterTraitType.Industrialist);
                    list.Add(CharacterTraitType.Organized);
                    list.Add(CharacterTraitType.Disorganized);
                    list.Add(CharacterTraitType.GoodStrategist);
                    list.Add(CharacterTraitType.PoorStrategist);
                    list.Add(CharacterTraitType.Uninhibited);
                    list.Add(CharacterTraitType.Measured);
                    list.Add(CharacterTraitType.Sober);
                    list.Add(CharacterTraitType.Tolerant);
                    list.Add(CharacterTraitType.Xenophobic);
                    list.Add(CharacterTraitType.EloquentSpeaker);
                    list.Add(CharacterTraitType.PoorSpeaker);
                    list.Add(CharacterTraitType.Corrupt);
                    list.Add(CharacterTraitType.Lawful);
                    list.Add(CharacterTraitType.GoodTactician);
                    list.Add(CharacterTraitType.PoorTactician);
                    list.Add(CharacterTraitType.StrongSpaceAttacker);
                    list.Add(CharacterTraitType.PoorSpaceAttacker);
                    list.Add(CharacterTraitType.StrongSpaceDefender);
                    list.Add(CharacterTraitType.PoorSpaceDefender);
                    list.Add(CharacterTraitType.LocalDefenseTactics);
                    list.Add(CharacterTraitType.GoodSpaceLogistician);
                    list.Add(CharacterTraitType.PoorSpaceLogistician);
                    list.Add(CharacterTraitType.SkilledNavigator);
                    list.Add(CharacterTraitType.PoorNavigator);
                    break;
                case CharacterRole.Scientist:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Energetic);
                        list.Add(CharacterTraitType.UltraGenius);
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Lazy);
                    }
                    list.Add(CharacterTraitType.Creative);
                    list.Add(CharacterTraitType.Methodical);
                    list.Add(CharacterTraitType.ForeignSpy);
                    list.Add(CharacterTraitType.Patriot);
                    break;
                case CharacterRole.TroopGeneral:
                    if (includeStartingTraits)
                    {
                        list.Add(CharacterTraitType.InspiringPresence);
                        list.Add(CharacterTraitType.Demoralizing);
                        list.Add(CharacterTraitType.Energetic);
                        list.Add(CharacterTraitType.ToughDiscipline);
                        list.Add(CharacterTraitType.NaturalGroundLeader);
                        if (includeHighlyNegativeTraits)
                        {
                            list.Add(CharacterTraitType.LaxDiscipline);
                        }
                    }
                    if (includeHighlyNegativeTraits)
                    {
                        list.Add(CharacterTraitType.Lazy);
                        list.Add(CharacterTraitType.Drunk);
                    }
                    list.Add(CharacterTraitType.GoodTactician);
                    list.Add(CharacterTraitType.PoorTactician);
                    list.Add(CharacterTraitType.StrongGroundAttacker);
                    list.Add(CharacterTraitType.PoorGroundAttacker);
                    list.Add(CharacterTraitType.StrongGroundDefender);
                    list.Add(CharacterTraitType.PoorGroundDefender);
                    list.Add(CharacterTraitType.GoodGroundLogistician);
                    list.Add(CharacterTraitType.PoorGroundLogistician);
                    list.Add(CharacterTraitType.GoodRecruiter);
                    list.Add(CharacterTraitType.PoorRecruiter);
                    list.Add(CharacterTraitType.CarefulAttacker);
                    list.Add(CharacterTraitType.RecklessAttacker);
                    break;
            }
            return list;
        }

        private void ClearIrrelevantTraitSkills()
        {
            List<CharacterSkillType> list = DetermineValidSkillsForRole(Role);
            CharacterSkillList characterSkillList = new CharacterSkillList();
            for (int i = 0; i < TraitSkills.Count; i++)
            {
                CharacterSkill characterSkill = TraitSkills[i];
                if (characterSkill != null && !list.Contains(characterSkill.Type))
                {
                    characterSkillList.Add(characterSkill);
                }
            }
            for (int j = 0; j < characterSkillList.Count; j++)
            {
                TraitSkills.Remove(characterSkillList[j]);
            }
            switch (Role)
            {
                case CharacterRole.Ambassador:
                    _ColonyIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _MilitaryShipConstructionSpeedTraits = 0;
                    _CivilianShipConstructionSpeedTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _MilitaryShipMaintenanceTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _MilitaryBaseMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _TroopMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
                case CharacterRole.ColonyGovernor:
                    _DiplomacyTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _EspionageTraits = 0;
                    _CounterEspionageTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _MilitaryShipMaintenanceTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
                case CharacterRole.FleetAdmiral:
                    _DiplomacyTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _TradeIncomeTraits = 0;
                    _TourismIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _EspionageTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    break;
                case CharacterRole.ShipCaptain:
                    _DiplomacyTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _TradeIncomeTraits = 0;
                    _TourismIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _EspionageTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    break;
                case CharacterRole.IntelligenceAgent:
                    _DiplomacyTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _TradeIncomeTraits = 0;
                    _TourismIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _MilitaryShipConstructionSpeedTraits = 0;
                    _CivilianShipConstructionSpeedTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _MilitaryShipMaintenanceTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _MilitaryBaseMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _TroopMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
                case CharacterRole.Leader:
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
                case CharacterRole.PirateLeader:
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _TroopMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    break;
                case CharacterRole.Scientist:
                    _DiplomacyTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _TradeIncomeTraits = 0;
                    _TourismIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _TroopRecruitmentRateTraits = 0;
                    _MilitaryShipConstructionSpeedTraits = 0;
                    _CivilianShipConstructionSpeedTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _EspionageTraits = 0;
                    _CounterEspionageTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _MilitaryShipMaintenanceTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _MilitaryBaseMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _TroopMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _TroopGroundAttackTraits = 0;
                    _TroopGroundDefenseTraits = 0;
                    _TroopExperienceGainTraits = 0;
                    _TroopRecoveryRateTraits = 0;
                    _TroopStrengthArmorTraits = 0;
                    _TroopStrengthInfantryTraits = 0;
                    _TroopStrengthSpecialForcesTraits = 0;
                    _TroopStrengthPlanetaryDefenseTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
                case CharacterRole.TroopGeneral:
                    _DiplomacyTraits = 0;
                    _ColonyIncomeTraits = 0;
                    _TradeIncomeTraits = 0;
                    _TourismIncomeTraits = 0;
                    _ColonyCorruptionTraits = 0;
                    _ColonyHappinessTraits = 0;
                    _PopulationGrowthTraits = 0;
                    _MiningRateTraits = 0;
                    _MilitaryShipConstructionSpeedTraits = 0;
                    _CivilianShipConstructionSpeedTraits = 0;
                    _ColonyShipConstructionSpeedTraits = 0;
                    _FacilityConstructionSpeedTraits = 0;
                    _ResearchWeaponsTraits = 0;
                    _ResearchEnergyTraits = 0;
                    _ResearchHighTechTraits = 0;
                    _EspionageTraits = 0;
                    _CounterEspionageTraits = 0;
                    _SabotageTraits = 0;
                    _ConcealmentTraits = 0;
                    _PsyOpsTraits = 0;
                    _AssassinationTraits = 0;
                    _MilitaryShipMaintenanceTraits = 0;
                    _CivilianShipMaintenanceTraits = 0;
                    _MilitaryBaseMaintenanceTraits = 0;
                    _CivilianBaseMaintenanceTraits = 0;
                    _WarWearinessTraits = 0;
                    _TargetingTraits = 0;
                    _CountermeasuresTraits = 0;
                    _ShipManeuveringTraits = 0;
                    _FightersTraits = 0;
                    _ShipEnergyUsageTraits = 0;
                    _WeaponsDamageTraits = 0;
                    _WeaponsRangeTraits = 0;
                    _ShieldRechargeRateTraits = 0;
                    _DamageControlTraits = 0;
                    _RepairBonusTraits = 0;
                    _HyperjumpSpeedTraits = 0;
                    _SmugglingIncomeTraits = 0;
                    _SmugglingEvasionTraits = 0;
                    _BoardingAssaultTraits = 0;
                    break;
            }
        }

        public Character(string name, CharacterRole role, string pictureFilename, Race race, Empire empire, StellarObject location, int appearanceOrder)
        {
            Name = name;
            Race = race;
            Role = role;
            _PictureFilename = pictureFilename;
            _Empire = empire;
            if (_Empire != null && !_Empire.Characters.Contains(this))
            {
                _Empire.Characters.Add(this);
            }
            if (_Location != null && !_Location.Characters.Contains(this))
            {
                _Location.Characters.Add(this);
            }
            _AppearanceOrder = appearanceOrder;
            _Active = false;
        }

        public long TransferExpectedArrivalDate(Galaxy galaxy)
        {
            long result = 0L;
            if (galaxy != null)
            {
                result = galaxy.CurrentStarDate + (long)(_TransferTimeRemaining * 1000f);
            }
            return result;
        }

        public void DoTasks(Galaxy galaxy)
        {
            DateTime currentDateTime = galaxy.CurrentDateTime;
            double totalSeconds = currentDateTime.Subtract(_LastTouch).TotalSeconds;
            ProcessTransfer(totalSeconds, galaxy);
            _LastTouch = currentDateTime;
        }

        private void ProcessTransfer(double timePassed, Galaxy galaxy)
        {
            if (_TransferDestination != null)
            {
                _TransferTimeRemaining -= (float)timePassed;
                if (_TransferTimeRemaining <= 0f)
                {
                    CompleteLocationTransfer(_TransferDestination, galaxy);
                    _TransferDestination = null;
                    _TransferTimeRemaining = 0f;
                }
            }
        }

        public void ResetTransfer()
        {
            _TransferDestination = null;
            _TransferTimeRemaining = 0f;
        }

        public void CompleteLocationTransfer(StellarObject destination, Galaxy galaxy)
        {
            CompleteLocationTransfer(destination, galaxy, invadingDestination: false);
        }

        public void CompleteLocationTransfer(StellarObject destination, Galaxy galaxy, bool invadingDestination)
        {
            if (_Location != null)
            {
                if (_Location.Characters == null)
                {
                    _Location.Characters = new CharacterList();
                }
                if (_Location.Characters.Contains(this))
                {
                    _Location.Characters.Remove(this);
                }
                if (_Location is Habitat)
                {
                    Habitat habitat = (Habitat)_Location;
                    if (habitat.InvadingCharacters != null && habitat.InvadingCharacters.Contains(this))
                    {
                        habitat.InvadingCharacters.Remove(this);
                    }
                }
                else if (_Location is BuiltObject)
                {
                    BuiltObject builtObject = (BuiltObject)_Location;
                    builtObject.ReviewCaptainBonuses();
                }
            }
            if (destination != null)
            {
                if (invadingDestination)
                {
                    if (destination is Habitat)
                    {
                        Habitat habitat2 = (Habitat)destination;
                        if (habitat2.InvadingCharacters == null)
                        {
                            habitat2.InvadingCharacters = new CharacterList();
                        }
                        if (!habitat2.InvadingCharacters.Contains(this))
                        {
                            habitat2.InvadingCharacters.Add(this);
                        }
                    }
                }
                else
                {
                    if (destination.Characters == null)
                    {
                        destination.Characters = new CharacterList();
                    }
                    if (!destination.Characters.Contains(this))
                    {
                        destination.Characters.Add(this);
                    }
                }
            }
            _Location = destination;
            if (_Location is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)_Location;
                builtObject2.ReviewCaptainBonuses();
            }
            if (galaxy != null)
            {
                _TransferArrivalDate = galaxy.CurrentStarDate;
            }
            bool flag = false;
            if (Role == CharacterRole.Ambassador && _Location != null && _Location is Habitat)
            {
                Habitat habitat3 = (Habitat)_Location;
                if (habitat3 != null && habitat3.Empire != null && habitat3.Empire != Empire && habitat3.Empire.Capital != null && habitat3.Empire.Capital == habitat3)
                {
                    galaxy?.DoCharacterEvent(CharacterEventType.AmbassadorAssignedToEmpire, habitat3.Empire, this);
                    flag = true;
                }
            }
            if (!flag)
            {
                galaxy?.DoCharacterEvent(CharacterEventType.CharacterTransferLocation, this, this);
            }
        }

        public void CompleteEmpireChange(Empire newEmpire)
        {
            if (_Empire != null)
            {
                if (_Empire.Characters.Contains(this))
                {
                    _Empire.Characters.Remove(this);
                }
                if (_Empire.Leader == this)
                {
                    _Empire.Leader = null;
                }
            }
            if (newEmpire != null)
            {
                if (!newEmpire.Characters.Contains(this))
                {
                    newEmpire.Characters.Add(this);
                }
                if (newEmpire.Leader == null && (Role == CharacterRole.Leader || Role == CharacterRole.PirateLeader))
                {
                    newEmpire.Leader = this;
                }
            }
            _Empire = newEmpire;
        }

        public void TransferToNewLocation(StellarObject destination, Galaxy galaxy)
        {
            if (destination == null)
            {
                return;
            }
            float num = 30f;
            float num2 = 90f;
            double num3 = 0.0;
            if (_Location != null)
            {
                num3 = Galaxy.CalculateDistanceStatic(_Location.Xpos, _Location.Ypos, destination.Xpos, destination.Ypos);
            }
            float num4 = (float)(num3 / (double)Galaxy.SectorSize * (double)num2);
            float num5 = (_TransferTimeRemaining = num + num4);
            _TransferDestination = destination;
            if ((Role != CharacterRole.FleetAdmiral && Role != CharacterRole.TroopGeneral && Role != CharacterRole.ShipCaptain && Role != CharacterRole.PirateLeader) || _Location == null || !(_Location is BuiltObject))
            {
                return;
            }
            BuiltObject builtObject = (BuiltObject)_Location;
            if (builtObject != null && builtObject.ShipGroup != null && destination is BuiltObject)
            {
                BuiltObject builtObject2 = (BuiltObject)destination;
                if (builtObject2 != null && builtObject2.ShipGroup != null && builtObject.ShipGroup == builtObject2.ShipGroup)
                {
                    CompleteLocationTransfer(destination, galaxy);
                    _TransferDestination = null;
                    _TransferTimeRemaining = 0f;
                }
            }
        }

        public void DefectToEmpire(Empire defectEmpire, StellarObject destination)
        {
            if (defectEmpire != null)
            {
                CompleteLocationTransfer(destination, defectEmpire.Galaxy);
                CompleteEmpireChange(defectEmpire);
            }
        }

        public void Activate(Galaxy galaxy, Empire empire, StellarObject location)
        {
            _StartDate = galaxy.CurrentStarDate;
            _LastTouch = galaxy.CurrentDateTime;
            _TransferArrivalDate = galaxy.CurrentStarDate;
            CompleteEmpireChange(empire);
            CompleteLocationTransfer(location, galaxy);
            _TransferDestination = null;
            _TransferTimeRemaining = 0f;
            _Active = true;
        }

        public void SendDeathMessage(CharacterDeathType deathType, Galaxy galaxy)
        {
            if (_Empire == null || galaxy == null)
            {
                return;
            }
            string text = string.Empty;
            string text2 = string.Empty;
            if (_Location != null)
            {
                text = _Location.Name;
            }
            string title = string.Format(TextResolver.GetText("Character Death Title"), Name);
            string text3 = string.Format(TextResolver.GetText("Character Death Description"), Galaxy.ResolveDescription(Role), Name, text);
            switch (deathType)
            {
                case CharacterDeathType.Assassination:
                    title = string.Format(TextResolver.GetText("Character Death Assassination Title"), Name);
                    text3 = string.Format(TextResolver.GetText("Character Death Assassination Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.ColonyBombardment:
                    text3 = string.Format(TextResolver.GetText("Character Death Bombardment Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.ColonyInvasion:
                    text3 = string.Format(TextResolver.GetText("Character Death Invasion Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.Disaster:
                    text3 = string.Format(TextResolver.GetText("Character Death Disaster Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.GenericDeath:
                    text3 = string.Format(TextResolver.GetText("Character Death Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.ShipDestroyed:
                case CharacterDeathType.ShipCaptured:
                    if (_Location != null && _Location is BuiltObject)
                    {
                        BuiltObject builtObject = (BuiltObject)_Location;
                        if (builtObject.ParentHabitat != null)
                        {
                            text2 = builtObject.ParentHabitat.Name;
                        }
                        else if (builtObject.NearestSystemStar != null)
                        {
                            text2 = builtObject.NearestSystemStar.Name;
                        }
                        else
                        {
                            Habitat habitat = galaxy.FastFindNearestSystem(builtObject.Xpos, builtObject.Ypos);
                            if (habitat != null)
                            {
                                text2 = habitat.Name;
                            }
                        }
                    }
                    text3 = ((deathType != CharacterDeathType.ShipCaptured) ? string.Format(TextResolver.GetText("Character Death Ship Destroyed Description"), Galaxy.ResolveDescription(Role), Name, text, text2) : string.Format(TextResolver.GetText("Character Death Ship Captured Description"), Galaxy.ResolveDescription(Role), Name, text, text2));
                    break;
                case CharacterDeathType.BaseDestroyed:
                    text3 = string.Format(TextResolver.GetText("Character Death Base Destroyed Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.BaseCaptured:
                    text3 = string.Format(TextResolver.GetText("Character Death Base Captured Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
                case CharacterDeathType.Dismissed:
                    text3 = string.Format(TextResolver.GetText("Character Death Dismiss Description"), Galaxy.ResolveDescription(Role), Name, text);
                    break;
            }
            if (deathType != CharacterDeathType.Dismissed)
            {
                text3 = text3 + "\n\n" + string.Format(TextResolver.GetText("Character Death Great Loss"), Galaxy.ResolveDescription(Role));
            }
            _Empire.SendEventMessageToEmpire(EventMessageType.CharacterEvent, title, text3, this, _Location);
            _Empire.SendNewsBroadcast(EventMessageType.CharacterEvent, this);
        }

        public void Kill(Galaxy galaxy)
        {
            if (BaconCharacter.Kill(this))
            {
                if (galaxy != null)
                {
                    short matchingGameEventIdCharacterKilled = galaxy.GetMatchingGameEventIdCharacterKilled(this);
                    galaxy.CheckTriggerEvent(matchingGameEventIdCharacterKilled, Empire, EventTriggerType.CharacterKilled, this);
                }
                CompleteEmpireChange(null);
                CompleteLocationTransfer(null, null);
                _TransferDestination = null;
                _TransferTimeRemaining = 0f;
                _Mission = null;
                _Active = false;
            }
        }

        public ShipGroup DetermineFleet()
        {
            StellarObject stellarObject = DetermineLocationWithDestination();
            if (stellarObject != null && stellarObject is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)stellarObject;
                return builtObject.ShipGroup;
            }
            return null;
        }

        public StellarObject DetermineLocationWithDestination()
        {
            if (_Location != null)
            {
                if (_TransferDestination == null)
                {
                    return _Location;
                }
                return _TransferDestination;
            }
            if (_TransferDestination != null)
            {
                return _TransferDestination;
            }
            return null;
        }

        public Empire DetermineLocationEmpire()
        {
            if (_Location != null)
            {
                return _Location.Empire;
            }
            return null;
        }

        public Empire DetermineLocationEmpireWithTransfer()
        {
            if (_Location != null)
            {
                if (_TransferDestination == null)
                {
                    return _Location.Empire;
                }
                return _TransferDestination.Empire;
            }
            return null;
        }

        int IComparable<Character>.CompareTo(Character other)
        {
            return AppearanceOrder.CompareTo(other.AppearanceOrder);
        }
    }
}
