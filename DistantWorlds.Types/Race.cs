// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Race
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Race : IComparable<Race>
    {
        private string _Name;

        private int _PictureRef;

        private double _ReproductiveRate;

        private int _IntelligenceLevel;

        private int _AggressionLevel;

        private int _CautionLevel;

        private int _FriendlinessLevel;

        private int _LoyaltyLevel;

        private int _DesignPictureFamilyIndex;

        private int _DesignNameIndex;

        private string _HomeSystemName;

        private string _TroopName;

        private string _TroopNameArmored;

        private string _TroopNameArtillery;

        private string _TroopNameSpecialForces;

        private Component _SpecialComponent;

        private int _SpecialGovernmentId = -1;

        private int _PreferredStartingGovernmentId = -1;

        [OptionalField]
        public int DesignPictureFamilyIndexPirates = -1;

        public byte FamilyId;

        private int _ShipMaintenanceSavings;

        private int _TroopMaintenanceSavings;

        private int _ResourceExtractionBonus;

        private int _WarWearinessAttenuation;

        private int _SatisfactionModifier;

        private int _ResearchBonus;

        private int _EspionageBonus;

        private int _TradeBonus;

        private ShipDesignFocus _ShipDesignFocus;

        private ComponentCategoryType _TechFocus1;

        private ComponentCategoryType _TechFocus2;

        private ComponentType _TechFocusType1;

        private ComponentType _TechFocusType2;

        private HabitatType _NativeHabitatType;

        private bool _Expanding = true;

        private bool _CanBePirate;

        [OptionalField]
        private bool _CanBeNormalEmpire = true;

        private bool _Playable = true;

        private Color _DefaultMainColor;

        private Color _DefaultSecondaryColor;

        private int _DefaultFlagShape;

        [OptionalField]
        public RaceBiasList Biases = new RaceBiasList();

        public CharacterList AvailableCharacters = new CharacterList();

        public ResourceBonusList CriticalResources = new ResourceBonusList();

        public double PeriodicGrowthRate = 1.0;

        public int PeriodicAggressionLevel = 100;

        public int PeriodicCautionLevel = 100;

        public int PeriodicFriendlinessLevel = 100;

        public RaceEventType PeriodicRaceEvent;

        public int ChangePeriodYearsInterval;

        public int ChangePeriodYearsLength;

        private bool _ChangePeriodActive;

        public double CivilianShipSizeFactor = 1.0;

        public double MilitaryShipSizeFactor = 1.0;

        public double ConstructionSpeedModifier = 1.0;

        public int IntelligenceAgentAdditional;

        public int TroopStrength = 100;

        public List<ComponentCategoryType> DisallowedResearchAreas = new List<ComponentCategoryType>();

        [OptionalField]
        public ComponentList DisallowedComponents = new ComponentList();

        public RaceVictoryConditionList VictoryConditions = new RaceVictoryConditionList();

        public RaceEventList RaceEvents = new RaceEventList();

        public double CharacterRandomAppearanceChanceLeader = 1.0;

        public double CharacterRandomAppearanceChanceAmbassador = 1.0;

        public double CharacterRandomAppearanceChanceGovernor = 1.0;

        public double CharacterRandomAppearanceChanceAdmiral = 1.0;

        public double CharacterRandomAppearanceChanceGeneral = 1.0;

        public double CharacterRandomAppearanceChanceScientist = 1.0;

        public double CharacterRandomAppearanceChanceIntelligenceAgent = 1.0;

        [OptionalField]
        public double CharacterRandomAppearanceChancePirateLeader = 1.0;

        [OptionalField]
        public double CharacterRandomAppearanceChanceShipCaptain = 1.0;

        public CharacterTraitType CharacterStartingTraitLeader;

        public CharacterTraitType CharacterStartingTraitAmbassador;

        public CharacterTraitType CharacterStartingTraitGovernor;

        public CharacterTraitType CharacterStartingTraitAdmiral;

        public CharacterTraitType CharacterStartingTraitGeneral;

        public CharacterTraitType CharacterStartingTraitScientist;

        public CharacterTraitType CharacterStartingTraitIntelligenceAgent;

        [OptionalField]
        public CharacterTraitType CharacterStartingTraitPirateLeader;

        [OptionalField]
        public CharacterTraitType CharacterStartingTraitShipCaptain;

        public double ResearchColonizationCostFactorContinental = 1.0;

        public double ResearchColonizationCostFactorMarshySwamp = 1.0;

        public double ResearchColonizationCostFactorOcean = 1.0;

        public double ResearchColonizationCostFactorDesert = 1.0;

        public double ResearchColonizationCostFactorIce = 1.0;

        public double ResearchColonizationCostFactorVolcanic = 1.0;

        public double ColonyConstructionSpeedFactorContinental = 1.0;

        public double ColonyConstructionSpeedFactorMarshySwamp = 1.0;

        public double ColonyConstructionSpeedFactorOcean = 1.0;

        public double ColonyConstructionSpeedFactorDesert = 1.0;

        public double ColonyConstructionSpeedFactorIce = 1.0;

        public double ColonyConstructionSpeedFactorVolcanic = 1.0;

        public double ColonyPopulationPolicyGrowthFactorExterminate = 1.0;

        public HabitatType ImmuneNaturalDisastersAtColonyType;

        public double SpaceportArmorStrengthFactor = 1.0;

        public int KnownStartingGalacticHistoryLocations;

        public double TourismIncomeFactor = 1.0;

        public double FreeTradeIncomeFactor = 1.0;

        public double MigrationFactor = 1.0;

        public double TroopRegenerationFactor = 1.0;

        public PiratePlayStyle DefaultPiratePlaystyle;

        [OptionalField]
        public List<int> DisallowedGovernmentIds = new List<int>();

        [OptionalField]
        public bool CanChangeGovernment = true;

        [OptionalField]
        public bool ImmuneToPlagues;

        [OptionalField]
        public List<int> ResearchPathWeapons = new List<int>();

        [OptionalField]
        public List<int> ResearchPathEnergy = new List<int>();

        [OptionalField]
        public List<int> ResearchPathHighTech = new List<int>();

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

        public Component SpecialComponent
        {
            get
            {
                return _SpecialComponent;
            }
            set
            {
                _SpecialComponent = value;
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
                _PictureRef = value;
            }
        }

        public int DesignPictureFamilyIndex
        {
            get
            {
                return _DesignPictureFamilyIndex;
            }
            set
            {
                _DesignPictureFamilyIndex = value;
            }
        }

        public int DesignNameIndex
        {
            get
            {
                return _DesignNameIndex;
            }
            set
            {
                _DesignNameIndex = value;
            }
        }

        public bool ChangePeriodActive
        {
            get
            {
                return _ChangePeriodActive;
            }
            set
            {
                _ChangePeriodActive = value;
            }
        }

        public double ReproductiveRateOriginal => _ReproductiveRate;

        public double ReproductiveRate
        {
            get
            {
                if (_ChangePeriodActive)
                {
                    return PeriodicGrowthRate;
                }
                return _ReproductiveRate;
            }
            set
            {
                _ReproductiveRate = value;
            }
        }

        public int IntelligenceLevel
        {
            get
            {
                return _IntelligenceLevel;
            }
            set
            {
                _IntelligenceLevel = value;
            }
        }

        public int AggressionLevelOriginal => _AggressionLevel;

        public int AggressionLevel
        {
            get
            {
                if (_ChangePeriodActive)
                {
                    return PeriodicAggressionLevel;
                }
                return _AggressionLevel;
            }
            set
            {
                _AggressionLevel = value;
            }
        }

        public int CautionLevelOriginal => _CautionLevel;

        public int CautionLevel
        {
            get
            {
                if (_ChangePeriodActive)
                {
                    return PeriodicCautionLevel;
                }
                return _CautionLevel;
            }
            set
            {
                _CautionLevel = value;
            }
        }

        public int FriendlinessLevelOriginal => _FriendlinessLevel;

        public int FriendlinessLevel
        {
            get
            {
                if (_ChangePeriodActive)
                {
                    return PeriodicFriendlinessLevel;
                }
                return _FriendlinessLevel;
            }
            set
            {
                _FriendlinessLevel = value;
            }
        }

        public int LoyaltyLevel
        {
            get
            {
                return _LoyaltyLevel;
            }
            set
            {
                _LoyaltyLevel = value;
            }
        }

        public string HomeSystemName
        {
            get
            {
                return _HomeSystemName;
            }
            set
            {
                _HomeSystemName = value;
            }
        }

        public string TroopName
        {
            get
            {
                return _TroopName;
            }
            set
            {
                _TroopName = value;
            }
        }

        public string TroopNameArmored
        {
            get
            {
                return _TroopNameArmored;
            }
            set
            {
                _TroopNameArmored = value;
            }
        }

        public string TroopNameArtillery
        {
            get
            {
                return _TroopNameArtillery;
            }
            set
            {
                _TroopNameArtillery = value;
            }
        }

        public string TroopNameSpecialForces
        {
            get
            {
                return _TroopNameSpecialForces;
            }
            set
            {
                _TroopNameSpecialForces = value;
            }
        }

        public int ShipMaintenanceSavings
        {
            get
            {
                return _ShipMaintenanceSavings;
            }
            set
            {
                _ShipMaintenanceSavings = value;
            }
        }

        public int TroopMaintenanceSavings
        {
            get
            {
                return _TroopMaintenanceSavings;
            }
            set
            {
                _TroopMaintenanceSavings = value;
            }
        }

        public int ResourceExtractionBonus
        {
            get
            {
                return _ResourceExtractionBonus;
            }
            set
            {
                _ResourceExtractionBonus = value;
            }
        }

        public int WarWearinessAttenuation
        {
            get
            {
                return _WarWearinessAttenuation;
            }
            set
            {
                _WarWearinessAttenuation = value;
            }
        }

        public int SatisfactionModifier
        {
            get
            {
                return _SatisfactionModifier;
            }
            set
            {
                _SatisfactionModifier = value;
            }
        }

        public int ResearchBonus
        {
            get
            {
                return _ResearchBonus;
            }
            set
            {
                _ResearchBonus = value;
            }
        }

        public int EspionageBonus
        {
            get
            {
                return _EspionageBonus;
            }
            set
            {
                _EspionageBonus = value;
            }
        }

        public int TradeBonus
        {
            get
            {
                return _TradeBonus;
            }
            set
            {
                _TradeBonus = value;
            }
        }

        public ShipDesignFocus ShipDesignFocus
        {
            get
            {
                return _ShipDesignFocus;
            }
            set
            {
                _ShipDesignFocus = value;
            }
        }

        public ComponentCategoryType TechFocus1
        {
            get
            {
                return _TechFocus1;
            }
            set
            {
                _TechFocus1 = value;
            }
        }

        public ComponentCategoryType TechFocus2
        {
            get
            {
                return _TechFocus2;
            }
            set
            {
                _TechFocus2 = value;
            }
        }

        public ComponentType TechFocusType1
        {
            get
            {
                return _TechFocusType1;
            }
            set
            {
                _TechFocusType1 = value;
            }
        }

        public ComponentType TechFocusType2
        {
            get
            {
                return _TechFocusType2;
            }
            set
            {
                _TechFocusType2 = value;
            }
        }

        public int SpecialGovernmentId
        {
            get
            {
                return _SpecialGovernmentId;
            }
            set
            {
                _SpecialGovernmentId = value;
            }
        }

        public int PreferredStartingGovernmentId
        {
            get
            {
                return _PreferredStartingGovernmentId;
            }
            set
            {
                _PreferredStartingGovernmentId = value;
            }
        }

        public HabitatType NativeHabitatType
        {
            get
            {
                return _NativeHabitatType;
            }
            set
            {
                _NativeHabitatType = value;
            }
        }

        public bool Expanding
        {
            get
            {
                return _Expanding;
            }
            set
            {
                _Expanding = value;
            }
        }

        public bool CanBePirate
        {
            get
            {
                return _CanBePirate;
            }
            set
            {
                _CanBePirate = value;
            }
        }

        public bool CanBeNormalEmpire
        {
            get
            {
                return _CanBeNormalEmpire;
            }
            set
            {
                _CanBeNormalEmpire = value;
            }
        }

        public bool Playable
        {
            get
            {
                return _Playable;
            }
            set
            {
                _Playable = value;
            }
        }

        public Color DefaultMainColor
        {
            get
            {
                return _DefaultMainColor;
            }
            set
            {
                _DefaultMainColor = value;
            }
        }

        public Color DefaultMainColorPirates => Color.FromArgb((int)_DefaultMainColor.R / 2, (int)_DefaultMainColor.G / 2, (int)_DefaultMainColor.B / 2);

        public Color DefaultSecondaryColor
        {
            get
            {
                return _DefaultSecondaryColor;
            }
            set
            {
                _DefaultSecondaryColor = value;
            }
        }

        public int DefaultFlagShape
        {
            get
            {
                return _DefaultFlagShape;
            }
            set
            {
                _DefaultFlagShape = value;
            }
        }

        public static Race LoadFromFile(string filePath)
        {
            int num = 0;
            Race race = null;
            try
            {
                string value = ";";
                byte resourceId = byte.MaxValue;
                ColonyResourceEffect colonyResourceEffect = ColonyResourceEffect.Undefined;
                double value2 = 0.0;
                bool appliesOnlyToSource = false;
                byte resourceId2 = byte.MaxValue;
                ColonyResourceEffect colonyResourceEffect2 = ColonyResourceEffect.Undefined;
                double value3 = 0.0;
                bool appliesOnlyToSource2 = false;
                byte resourceId3 = byte.MaxValue;
                ColonyResourceEffect colonyResourceEffect3 = ColonyResourceEffect.Undefined;
                double value4 = 0.0;
                bool appliesOnlyToSource3 = false;
                RaceVictoryConditionType raceVictoryConditionType = RaceVictoryConditionType.Undefined;
                double amount = 0.0;
                float proportion = 0f;
                int index = 0;
                RaceVictoryConditionType raceVictoryConditionType2 = RaceVictoryConditionType.Undefined;
                double amount2 = 0.0;
                float proportion2 = 0f;
                int index2 = 0;
                RaceVictoryConditionType raceVictoryConditionType3 = RaceVictoryConditionType.Undefined;
                double amount3 = 0.0;
                float proportion3 = 0f;
                int index3 = 0;
                RaceVictoryConditionType raceVictoryConditionType4 = RaceVictoryConditionType.Undefined;
                double amount4 = 0.0;
                float proportion4 = 0f;
                int index4 = 0;
                RaceVictoryConditionType raceVictoryConditionType5 = RaceVictoryConditionType.Undefined;
                double amount5 = 0.0;
                float proportion5 = 0f;
                int index5 = 0;
                RaceEventType raceEventType = RaceEventType.Undefined;
                double frequency = 1.0;
                RaceEventType raceEventType2 = RaceEventType.Undefined;
                double frequency2 = 1.0;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using StreamReader streamReader = new StreamReader(stream);
                    race = new Race();
                    while (!streamReader.EndOfStream)
                    {
                        num++;
                        string text = streamReader.ReadLine();
                        if (string.IsNullOrEmpty(text) || !(text.Trim() != string.Empty) || !(text.Trim().Substring(0, 1) != "'"))
                        {
                            continue;
                        }
                        int num2 = text.IndexOf(value);
                        if (num2 < 0)
                        {
                            continue;
                        }
                        string text2 = text.Substring(0, num2).Trim();
                        string text3 = text.Substring(num2 + 1, text.Length - (num2 + 1)).Trim();
                        byte b = 0;
                        switch (text2)
                        {
                            case "WeaponsResearchProjectOrder":
                                {
                                    string[] separator4 = new string[1] { "," };
                                    string[] array4 = text3.Split(separator4, StringSplitOptions.RemoveEmptyEntries);
                                    for (int l = 0; l < array4.Length; l++)
                                    {
                                        int result4 = -1;
                                        if (int.TryParse(array4[l], NumberStyles.Integer, CultureInfo.InvariantCulture, out result4))
                                        {
                                            race.ResearchPathWeapons.Add(result4);
                                        }
                                    }
                                    break;
                                }
                            case "EnergyResearchProjectOrder":
                                {
                                    string[] separator2 = new string[1] { "," };
                                    string[] array2 = text3.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
                                    for (int j = 0; j < array2.Length; j++)
                                    {
                                        int result2 = -1;
                                        if (int.TryParse(array2[j], NumberStyles.Integer, CultureInfo.InvariantCulture, out result2))
                                        {
                                            race.ResearchPathEnergy.Add(result2);
                                        }
                                    }
                                    break;
                                }
                            case "HighTechResearchProjectOrder":
                                {
                                    string[] separator3 = new string[1] { "," };
                                    string[] array3 = text3.Split(separator3, StringSplitOptions.RemoveEmptyEntries);
                                    for (int k = 0; k < array3.Length; k++)
                                    {
                                        int result3 = -1;
                                        if (int.TryParse(array3[k], NumberStyles.Integer, CultureInfo.InvariantCulture, out result3))
                                        {
                                            race.ResearchPathHighTech.Add(result3);
                                        }
                                    }
                                    break;
                                }
                            case "DisallowedComponentIds":
                                {
                                    string[] separator = new string[1] { "," };
                                    string[] array = text3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                                    for (int i = 0; i < array.Length; i++)
                                    {
                                        int result = -1;
                                        if (int.TryParse(array[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out result) && result >= 0 && result < Galaxy.ComponentDefinitionsStatic.Length)
                                        {
                                            Component component = new Component(result);
                                            if (component != null)
                                            {
                                                race.DisallowedComponents.Add(component);
                                            }
                                        }
                                    }
                                    break;
                                }
                            case "RaceEvent1Type":
                                {
                                    byte b11 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceEventType), b11))
                                    {
                                        raceEventType = (RaceEventType)b11;
                                    }
                                    break;
                                }
                            case "RaceEvent2Type":
                                {
                                    byte b8 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceEventType), b8))
                                    {
                                        raceEventType2 = (RaceEventType)b8;
                                    }
                                    break;
                                }
                            case "RaceEvent1Frequency":
                                frequency = ParseDoubleValue(text3);
                                break;
                            case "RaceEvent2Frequency":
                                frequency2 = ParseDoubleValue(text3);
                                break;
                            case "Resource1Type":
                                b = ParseByteValue(text3);
                                if (b >= 0)
                                {
                                    resourceId = b;
                                }
                                break;
                            case "Resource2Type":
                                b = ParseByteValue(text3);
                                if (b >= 0)
                                {
                                    resourceId2 = b;
                                }
                                break;
                            case "Resource3Type":
                                b = ParseByteValue(text3);
                                if (b >= 0)
                                {
                                    resourceId3 = b;
                                }
                                break;
                            case "Resource1Effect":
                                {
                                    byte b10 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(ColonyResourceEffect), b10))
                                    {
                                        colonyResourceEffect = (ColonyResourceEffect)b10;
                                    }
                                    break;
                                }
                            case "Resource2Effect":
                                {
                                    byte b9 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(ColonyResourceEffect), b9))
                                    {
                                        colonyResourceEffect2 = (ColonyResourceEffect)b9;
                                    }
                                    break;
                                }
                            case "Resource3Effect":
                                {
                                    byte b7 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(ColonyResourceEffect), b7))
                                    {
                                        colonyResourceEffect3 = (ColonyResourceEffect)b7;
                                    }
                                    break;
                                }
                            case "Resource1Amount":
                                value2 = ParseDoubleValue(text3);
                                break;
                            case "Resource2Amount":
                                value3 = ParseDoubleValue(text3);
                                break;
                            case "Resource3Amount":
                                value4 = ParseDoubleValue(text3);
                                break;
                            case "Resource1AppliesOnlyToSource":
                                appliesOnlyToSource = ParseBoolValue(text3);
                                break;
                            case "Resource2AppliesOnlyToSource":
                                appliesOnlyToSource2 = ParseBoolValue(text3);
                                break;
                            case "Resource3AppliesOnlyToSource":
                                appliesOnlyToSource3 = ParseBoolValue(text3);
                                break;
                            case "Condition1Type":
                                {
                                    byte b6 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceVictoryConditionType), b6))
                                    {
                                        raceVictoryConditionType = (RaceVictoryConditionType)b6;
                                    }
                                    break;
                                }
                            case "Condition2Type":
                                {
                                    byte b5 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceVictoryConditionType), b5))
                                    {
                                        raceVictoryConditionType2 = (RaceVictoryConditionType)b5;
                                    }
                                    break;
                                }
                            case "Condition3Type":
                                {
                                    byte b4 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceVictoryConditionType), b4))
                                    {
                                        raceVictoryConditionType3 = (RaceVictoryConditionType)b4;
                                    }
                                    break;
                                }
                            case "Condition4Type":
                                {
                                    byte b3 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceVictoryConditionType), b3))
                                    {
                                        raceVictoryConditionType4 = (RaceVictoryConditionType)b3;
                                    }
                                    break;
                                }
                            case "Condition5Type":
                                {
                                    byte b2 = (byte)ParseIntValue(text3);
                                    if (Enum.IsDefined(typeof(RaceVictoryConditionType), b2))
                                    {
                                        raceVictoryConditionType5 = (RaceVictoryConditionType)b2;
                                    }
                                    break;
                                }
                            case "Condition1Value":
                                amount = ParseDoubleValue(text3);
                                break;
                            case "Condition2Value":
                                amount2 = ParseDoubleValue(text3);
                                break;
                            case "Condition3Value":
                                amount3 = ParseDoubleValue(text3);
                                break;
                            case "Condition4Value":
                                amount4 = ParseDoubleValue(text3);
                                break;
                            case "Condition5Value":
                                amount5 = ParseDoubleValue(text3);
                                break;
                            case "Condition1Proportion":
                                proportion = ParseFloatValue(text3);
                                break;
                            case "Condition2Proportion":
                                proportion2 = ParseFloatValue(text3);
                                break;
                            case "Condition3Proportion":
                                proportion3 = ParseFloatValue(text3);
                                break;
                            case "Condition4Proportion":
                                proportion4 = ParseFloatValue(text3);
                                break;
                            case "Condition5Proportion":
                                proportion5 = ParseFloatValue(text3);
                                break;
                            case "Condition1AdditionalData":
                                index = ParseIntValue(text3);
                                break;
                            case "Condition2AdditionalData":
                                index2 = ParseIntValue(text3);
                                break;
                            case "Condition3AdditionalData":
                                index3 = ParseIntValue(text3);
                                break;
                            case "Condition4AdditionalData":
                                index4 = ParseIntValue(text3);
                                break;
                            case "Condition5AdditionalData":
                                index5 = ParseIntValue(text3);
                                break;
                            default:
                                race = SetNameValuePair(race, text2, text3);
                                break;
                        }
                    }
                }
                if (!string.IsNullOrEmpty(race.TroopName) && string.IsNullOrEmpty(race.TroopNameArmored))
                {
                    race.TroopNameArmored = race.TroopName;
                }
                if (!string.IsNullOrEmpty(race.TroopName) && string.IsNullOrEmpty(race.TroopNameArtillery))
                {
                    race.TroopNameArtillery = race.TroopName;
                }
                if (!string.IsNullOrEmpty(race.TroopName) && string.IsNullOrEmpty(race.TroopNameSpecialForces))
                {
                    race.TroopNameSpecialForces = race.TroopName;
                }
                if (raceEventType != 0)
                {
                    RaceEvent item = new RaceEvent(raceEventType, frequency);
                    race.RaceEvents.Add(item);
                }
                if (raceEventType2 != 0)
                {
                    RaceEvent item2 = new RaceEvent(raceEventType2, frequency2);
                    race.RaceEvents.Add(item2);
                }
                if (colonyResourceEffect != 0)
                {
                    ResourceBonus item3 = new ResourceBonus(resourceId, colonyResourceEffect, value2, appliesOnlyToSource);
                    race.CriticalResources.Add(item3);
                }
                if (colonyResourceEffect2 != 0)
                {
                    ResourceBonus item4 = new ResourceBonus(resourceId2, colonyResourceEffect2, value3, appliesOnlyToSource2);
                    race.CriticalResources.Add(item4);
                }
                if (colonyResourceEffect3 != 0)
                {
                    ResourceBonus item5 = new ResourceBonus(resourceId3, colonyResourceEffect3, value4, appliesOnlyToSource3);
                    race.CriticalResources.Add(item5);
                }
                if (raceVictoryConditionType != 0)
                {
                    object additionalData = ParseConditionAdditionalData(raceVictoryConditionType, index);
                    RaceVictoryCondition item6 = new RaceVictoryCondition(raceVictoryConditionType, amount, proportion, additionalData);
                    if (race.VictoryConditions == null)
                    {
                        race.VictoryConditions = new RaceVictoryConditionList();
                    }
                    race.VictoryConditions.Add(item6);
                }
                if (raceVictoryConditionType2 != 0)
                {
                    object additionalData2 = ParseConditionAdditionalData(raceVictoryConditionType2, index2);
                    RaceVictoryCondition item7 = new RaceVictoryCondition(raceVictoryConditionType2, amount2, proportion2, additionalData2);
                    if (race.VictoryConditions == null)
                    {
                        race.VictoryConditions = new RaceVictoryConditionList();
                    }
                    race.VictoryConditions.Add(item7);
                }
                if (raceVictoryConditionType3 != 0)
                {
                    object additionalData3 = ParseConditionAdditionalData(raceVictoryConditionType3, index3);
                    RaceVictoryCondition item8 = new RaceVictoryCondition(raceVictoryConditionType3, amount3, proportion3, additionalData3);
                    if (race.VictoryConditions == null)
                    {
                        race.VictoryConditions = new RaceVictoryConditionList();
                    }
                    race.VictoryConditions.Add(item8);
                }
                if (raceVictoryConditionType4 != 0)
                {
                    object additionalData4 = ParseConditionAdditionalData(raceVictoryConditionType4, index4);
                    RaceVictoryCondition item9 = new RaceVictoryCondition(raceVictoryConditionType4, amount4, proportion4, additionalData4);
                    if (race.VictoryConditions == null)
                    {
                        race.VictoryConditions = new RaceVictoryConditionList();
                    }
                    race.VictoryConditions.Add(item9);
                }
                if (raceVictoryConditionType5 != 0)
                {
                    object additionalData5 = ParseConditionAdditionalData(raceVictoryConditionType5, index5);
                    RaceVictoryCondition item10 = new RaceVictoryCondition(raceVictoryConditionType5, amount5, proportion5, additionalData5);
                    if (race.VictoryConditions == null)
                    {
                        race.VictoryConditions = new RaceVictoryConditionList();
                    }
                    race.VictoryConditions.Add(item10);
                }
                if (race.VictoryConditions != null)
                {
                    race.VictoryConditions.Sort();
                    race.VictoryConditions.Reverse();
                    return race;
                }
                return race;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException("Error at line " + num + " reading file " + filePath);
            }
        }

        private static object ParseConditionAdditionalData(RaceVictoryConditionType type, int index)
        {
            object result = null;
            switch (type)
            {
                case RaceVictoryConditionType.BuildWonder:
                    if (index >= 0 && index < Galaxy.PlanetaryFacilityDefinitionsStatic.Count)
                    {
                        result = Galaxy.PlanetaryFacilityDefinitionsStatic[index];
                    }
                    break;
                case RaceVictoryConditionType.ControlLargestColoniesByType:
                    result = Galaxy.ResolveColonyHabitatTypeByIndexIncludingUndefined(index);
                    break;
                case RaceVictoryConditionType.ControlPlanetTypePercentage:
                    result = Galaxy.ResolveColonyHabitatTypeByIndexIncludingUndefined(index);
                    break;
                case RaceVictoryConditionType.DestroyMostCreaturesByType:
                    result = Galaxy.ResolveCreatureTypeByIndex(index);
                    break;
                case RaceVictoryConditionType.ResearchMostCompletedBranchesByIndustry:
                    result = (IndustryType)index;
                    break;
            }
            return result;
        }

        private static Race SetNameValuePair(Race race, string name, string value)
        {
            if (race != null)
            {
                name = name.Trim();
                value = value.Trim();
                switch (name)
                {
                    case "Name":
                        race.Name = value;
                        break;
                    case "PictureIndex":
                        race.PictureRef = ParseIntValue(value);
                        break;
                    case "RaceFamily":
                        race.FamilyId = ParseRaceFamily(value);
                        break;
                    case "ReproductionRate":
                        race.ReproductiveRate = Math.Max(1.0, Math.Min(ParseDoubleValue(value), 1.5));
                        break;
                    case "Intelligence":
                        race.IntelligenceLevel = Math.Max(50, Math.Min(ParseIntValue(value), 150));
                        break;
                    case "Aggression":
                        race.AggressionLevel = Math.Max(50, Math.Min(ParseIntValue(value), 150));
                        break;
                    case "Caution":
                        race.CautionLevel = Math.Max(50, Math.Min(ParseIntValue(value), 150));
                        break;
                    case "Friendliness":
                        race.FriendlinessLevel = Math.Max(50, Math.Min(ParseIntValue(value), 150));
                        break;
                    case "Loyalty":
                        race.LoyaltyLevel = Math.Max(50, Math.Min(ParseIntValue(value), 150));
                        break;
                    case "DesignsPictureFamilyIndex":
                        race.DesignPictureFamilyIndex = Math.Max(0, Math.Min(ParseIntValue(value), 50));
                        break;
                    case "DesignsPictureFamilyIndexPirates":
                        race.DesignPictureFamilyIndexPirates = Math.Max(-1, Math.Min(ParseIntValue(value), 50));
                        break;
                    case "DesignNamesIndex":
                        race.DesignNameIndex = Math.Max(0, Math.Min(ParseIntValue(value), 50));
                        break;
                    case "ShipMaintenanceSavings":
                        race.ShipMaintenanceSavings = Math.Max(0, Math.Min(ParseIntValue(value), 100));
                        break;
                    case "TroopMaintenanceSavings":
                        race.TroopMaintenanceSavings = Math.Max(0, Math.Min(ParseIntValue(value), 100));
                        break;
                    case "ResourceExtractionBonus":
                        race.ResourceExtractionBonus = Math.Max(0, Math.Min(ParseIntValue(value), 500));
                        break;
                    case "WarWearinessAttenuation":
                        race.WarWearinessAttenuation = Math.Max(0, Math.Min(ParseIntValue(value), 100));
                        break;
                    case "SatisfactionModifier":
                        race.SatisfactionModifier = Math.Max(0, Math.Min(ParseIntValue(value), 505));
                        break;
                    case "ResearchBonus":
                        race.ResearchBonus = Math.Max(0, Math.Min(ParseIntValue(value), 510));
                        break;
                    case "EspionageBonus":
                        race.EspionageBonus = Math.Max(0, Math.Min(ParseIntValue(value), 515));
                        break;
                    case "TradeBonus":
                        race.TradeBonus = Math.Max(0, Math.Min(ParseIntValue(value), 520));
                        break;
                    case "OverallShipDesignFocus":
                        {
                            int num = ParseIntValue(value);
                            if (Enum.IsDefined(typeof(ShipDesignFocus), num))
                            {
                                race.ShipDesignFocus = (ShipDesignFocus)num;
                            }
                            break;
                        }
                    case "TechFocus1":
                        {
                            Galaxy.ResolveTechFocus(ParseIntValue(value), out var category2, out var type2);
                            race.TechFocus1 = category2;
                            race.TechFocusType1 = type2;
                            break;
                        }
                    case "TechFocus2":
                        {
                            Galaxy.ResolveTechFocus(ParseIntValue(value), out var category, out var type);
                            race.TechFocus2 = category;
                            race.TechFocusType2 = type;
                            break;
                        }
                    case "NativePlanetType":
                        race.NativeHabitatType = Galaxy.ResolveColonyHabitatTypeByIndexDesertBeforeOcean(ParseIntValue(value));
                        break;
                    case "SpecialComponent":
                        race.SpecialComponent = Galaxy.ResolveSpecialComponent(ParseIntValue(value));
                        break;
                    case "SpecialGovernment":
                        race.SpecialGovernmentId = ParseIntValue(value);
                        break;
                    case "PreferredStartingGovernment":
                        race.PreferredStartingGovernmentId = ParseIntValue(value);
                        break;
                    case "DisallowedGovernments":
                        {
                            string[] array = value.Split(',');
                            if (array == null || array.Length <= 0)
                            {
                                break;
                            }
                            for (int i = 0; i < array.Length; i++)
                            {
                                int result = -1;
                                if (int.TryParse(array[i], out result))
                                {
                                    race.DisallowedGovernmentIds.Add(result);
                                }
                            }
                            List<int> list = new List<int>();
                            for (int j = 0; j < Galaxy.GovernmentsStatic.Count; j++)
                            {
                                if (!race.DisallowedGovernmentIds.Contains(Galaxy.GovernmentsStatic[j].GovernmentId))
                                {
                                    list.Add(Galaxy.GovernmentsStatic[j].GovernmentId);
                                }
                            }
                            if (list.Count <= 0)
                            {
                                throw new ApplicationException("No allowable governments for race " + race.Name + ". Must not disallow all governments.");
                            }
                            break;
                        }
                    case "CanChangeGovernment":
                        race.CanChangeGovernment = ParseBoolValue(value);
                        break;
                    case "Expanding":
                        race.Expanding = ParseBoolValue(value);
                        break;
                    case "CanBePirate":
                        race.CanBePirate = ParseBoolValue(value);
                        break;
                    case "CanBeNormalEmpire":
                        race.CanBeNormalEmpire = ParseBoolValue(value);
                        break;
                    case "Playable":
                        race.Playable = ParseBoolValue(value);
                        break;
                    case "PeriodicChangeInterval":
                        race.ChangePeriodYearsInterval = ParseIntValue(value);
                        break;
                    case "PeriodicChangeLength":
                        race.ChangePeriodYearsLength = ParseIntValue(value);
                        break;
                    case "PeriodicFactorsGrowth":
                        race.PeriodicGrowthRate = Math.Max(1.0, Math.Min(ParseDoubleValue(value), 2.0));
                        break;
                    case "PeriodicFactorsAggression":
                        race.PeriodicAggressionLevel = Math.Max(50, Math.Min(ParseIntValue(value), 200));
                        break;
                    case "PeriodicFactorsCaution":
                        race.PeriodicCautionLevel = Math.Max(50, Math.Min(ParseIntValue(value), 200));
                        break;
                    case "PeriodicFactorsFriendliness":
                        race.PeriodicFriendlinessLevel = Math.Max(50, Math.Min(ParseIntValue(value), 200));
                        break;
                    case "PeriodicChangeCycleEvent":
                        {
                            byte b11 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(RaceEventType), b11))
                            {
                                race.PeriodicRaceEvent = (RaceEventType)b11;
                            }
                            break;
                        }
                    case "ShipSizeFactorCivilian":
                        race.CivilianShipSizeFactor = Math.Max(0.7, Math.Min(ParseDoubleValue(value), 5.1));
                        break;
                    case "ShipSizeFactorMilitary":
                        race.MilitaryShipSizeFactor = Math.Max(0.7, Math.Min(ParseDoubleValue(value), 5.1));
                        break;
                    case "DisallowedResearchArea1":
                        if (race.DisallowedResearchAreas == null)
                        {
                            race.DisallowedResearchAreas = new List<ComponentCategoryType>();
                        }
                        race.DisallowedResearchAreas.Add(Galaxy.ResolveTechDisallow(ParseIntValue(value)));
                        break;
                    case "DisallowedResearchArea2":
                        if (race.DisallowedResearchAreas == null)
                        {
                            race.DisallowedResearchAreas = new List<ComponentCategoryType>();
                        }
                        race.DisallowedResearchAreas.Add(Galaxy.ResolveTechDisallow(ParseIntValue(value)));
                        break;
                    case "DisallowedResearchArea3":
                        if (race.DisallowedResearchAreas == null)
                        {
                            race.DisallowedResearchAreas = new List<ComponentCategoryType>();
                        }
                        race.DisallowedResearchAreas.Add(Galaxy.ResolveTechDisallow(ParseIntValue(value)));
                        break;
                    case "AdditionalIntelligenceAgents":
                        race.IntelligenceAgentAdditional = Math.Min(5, Math.Max(0, ParseIntValue(value)));
                        break;
                    case "ConstructionSpeedFactor":
                        race.ConstructionSpeedModifier = Math.Max(0.3, Math.Min(ParseDoubleValue(value), 5.5));
                        break;
                    case "DefaultPrimaryColor":
                        race.DefaultMainColor = Galaxy.SelectColorFromKey(ParseIntValue(value));
                        break;
                    case "DefaultSecondaryColor":
                        race.DefaultSecondaryColor = Galaxy.SelectColorFromKey(ParseIntValue(value));
                        break;
                    case "DefaultFlagDesign":
                        race.DefaultFlagShape = ParseIntValue(value);
                        break;
                    case "HomeSystemName":
                        race.HomeSystemName = value;
                        break;
                    case "TroopStrength":
                        race.TroopStrength = Math.Min(400, Math.Max(50, ParseIntValue(value)));
                        break;
                    case "TroopName":
                        race.TroopName = value;
                        break;
                    case "TroopNameArmored":
                        race.TroopNameArmored = value;
                        break;
                    case "TroopNamePlanetaryDefense":
                        race.TroopNameArtillery = value;
                        break;
                    case "TroopNameSpecialForces":
                        race.TroopNameSpecialForces = value;
                        break;
                    case "CharacterRandomAppearanceChanceLeader":
                        race.CharacterRandomAppearanceChanceLeader = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceAmbassador":
                        race.CharacterRandomAppearanceChanceAmbassador = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceGovernor":
                        race.CharacterRandomAppearanceChanceGovernor = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceAdmiral":
                        race.CharacterRandomAppearanceChanceAdmiral = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceGeneral":
                        race.CharacterRandomAppearanceChanceGeneral = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceScientist":
                        race.CharacterRandomAppearanceChanceScientist = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceIntelligenceAgent":
                        race.CharacterRandomAppearanceChanceIntelligenceAgent = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChancePirateLeader":
                        race.CharacterRandomAppearanceChancePirateLeader = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "CharacterRandomAppearanceChanceShipCaptain":
                        race.CharacterRandomAppearanceChanceShipCaptain = Math.Min(5.0, Math.Max(0.0, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorContinental":
                        race.ResearchColonizationCostFactorContinental = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorMarshySwamp":
                        race.ResearchColonizationCostFactorMarshySwamp = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorOcean":
                        race.ResearchColonizationCostFactorOcean = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorDesert":
                        race.ResearchColonizationCostFactorDesert = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorIce":
                        race.ResearchColonizationCostFactorIce = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ResearchColonizationCostFactorVolcanic":
                        race.ResearchColonizationCostFactorVolcanic = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorContinental":
                        race.ColonyConstructionSpeedFactorContinental = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorMarshySwamp":
                        race.ColonyConstructionSpeedFactorMarshySwamp = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorOcean":
                        race.ColonyConstructionSpeedFactorOcean = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorDesert":
                        race.ColonyConstructionSpeedFactorDesert = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorIce":
                        race.ColonyConstructionSpeedFactorIce = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ColonyConstructionSpeedFactorVolcanic":
                        race.ColonyConstructionSpeedFactorVolcanic = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "CharacterStartingTraitLeader":
                        {
                            byte b10 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b10))
                            {
                                race.CharacterStartingTraitLeader = (CharacterTraitType)b10;
                            }
                            break;
                        }
                    case "CharacterStartingTraitAmbassador":
                        {
                            byte b9 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b9))
                            {
                                race.CharacterStartingTraitAmbassador = (CharacterTraitType)b9;
                            }
                            break;
                        }
                    case "CharacterStartingTraitGovernor":
                        {
                            byte b8 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b8))
                            {
                                race.CharacterStartingTraitGovernor = (CharacterTraitType)b8;
                            }
                            break;
                        }
                    case "CharacterStartingTraitAdmiral":
                        {
                            byte b7 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b7))
                            {
                                race.CharacterStartingTraitAdmiral = (CharacterTraitType)b7;
                            }
                            break;
                        }
                    case "CharacterStartingTraitGeneral":
                        {
                            byte b6 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b6))
                            {
                                race.CharacterStartingTraitGeneral = (CharacterTraitType)b6;
                            }
                            break;
                        }
                    case "CharacterStartingTraitScientist":
                        {
                            byte b5 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b5))
                            {
                                race.CharacterStartingTraitScientist = (CharacterTraitType)b5;
                            }
                            break;
                        }
                    case "CharacterStartingTraitIntelligenceAgent":
                        {
                            byte b4 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b4))
                            {
                                race.CharacterStartingTraitIntelligenceAgent = (CharacterTraitType)b4;
                            }
                            break;
                        }
                    case "CharacterStartingTraitPirateLeader":
                        {
                            byte b3 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b3))
                            {
                                race.CharacterStartingTraitPirateLeader = (CharacterTraitType)b3;
                            }
                            break;
                        }
                    case "CharacterStartingTraitShipCaptain":
                        {
                            byte b2 = (byte)ParseIntValue(value);
                            if (Enum.IsDefined(typeof(CharacterTraitType), b2))
                            {
                                race.CharacterStartingTraitShipCaptain = (CharacterTraitType)b2;
                            }
                            break;
                        }
                    case "ColonyPopulationPolicyGrowthFactorExterminate":
                        race.ColonyPopulationPolicyGrowthFactorExterminate = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "ImmuneNaturalDisastersAtColonyType":
                        race.ImmuneNaturalDisastersAtColonyType = Galaxy.ResolveColonyHabitatTypeByIndexIncludingUndefined(ParseIntValue(value));
                        break;
                    case "SpaceportArmorStrengthFactor":
                        race.SpaceportArmorStrengthFactor = Math.Min(3.0, Math.Max(0.3, ParseDoubleValue(value)));
                        break;
                    case "TourismIncomeFactor":
                        race.TourismIncomeFactor = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "FreeTradeIncomeFactor":
                        race.FreeTradeIncomeFactor = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "MigrationFactor":
                        race.MigrationFactor = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "TroopRegenerationFactor":
                        race.TroopRegenerationFactor = Math.Min(5.0, Math.Max(0.2, ParseDoubleValue(value)));
                        break;
                    case "KnownStartingGalacticHistoryLocations":
                        race.KnownStartingGalacticHistoryLocations = Math.Min(10, Math.Max(0, ParseIntValue(value)));
                        break;
                    case "PirateDefaultPlaystyle":
                        {
                            byte b = (byte)ParseIntValue(value);
                            b = (byte)(b + 1);
                            if (Enum.IsDefined(typeof(PiratePlayStyle), b))
                            {
                                race.DefaultPiratePlaystyle = (PiratePlayStyle)b;
                            }
                            break;
                        }
                    case "ImmuneToPlagues":
                        race.ImmuneToPlagues = ParseBoolValue(value);
                        break;
                }
            }
            return race;
        }

        private static byte ParseRaceFamily(string value)
        {
            int num = ParseIntValue(value);
            if (num >= 0 && num < Galaxy.RaceFamiliesStatic.Count)
            {
                return (byte)num;
            }
            return 0;
        }

        private static byte ParseByteValue(string value)
        {
            byte result = 0;
            byte.TryParse(value, out result);
            return result;
        }

        private static int ParseIntValue(string value)
        {
            int result = 0;
            int.TryParse(value, out result);
            return result;
        }

        private static float ParseFloatValue(string value)
        {
            float num = 0f;
            return float.Parse(value, NumberFormatInfo.InvariantInfo);
        }

        private static double ParseDoubleValue(string value)
        {
            double num = 0.0;
            return double.Parse(value, NumberFormatInfo.InvariantInfo);
        }

        private static bool ParseBoolValue(string value)
        {
            bool result = false;
            if (value.Trim().ToLower(CultureInfo.InvariantCulture) == "y")
            {
                result = true;
            }
            else if (value.Trim().ToLower(CultureInfo.InvariantCulture) == "n")
            {
                result = false;
            }
            return result;
        }

        public Race()
        {
        }

        public Race(string name, int pictureRef, double reproductiveRate, int intelligenceLevel, int aggressionLevel, int cautionLevel, int friendlinessLevel, int loyaltyLevel, int designPictureFamily, int designNameIndex, string troopName)
            : this()
        {
            _Name = name;
            _PictureRef = pictureRef;
            _ReproductiveRate = reproductiveRate;
            _IntelligenceLevel = intelligenceLevel;
            _AggressionLevel = aggressionLevel;
            _CautionLevel = cautionLevel;
            _FriendlinessLevel = friendlinessLevel;
            _LoyaltyLevel = loyaltyLevel;
            _DesignPictureFamilyIndex = designPictureFamily;
            _DesignNameIndex = designNameIndex;
            _TroopName = troopName;
        }

        public override string ToString()
        {
            return Name;
        }

        int IComparable<Race>.CompareTo(Race other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
