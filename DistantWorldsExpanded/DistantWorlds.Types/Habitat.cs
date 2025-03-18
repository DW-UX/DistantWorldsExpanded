// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Habitat
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;

namespace DistantWorlds.Types
{
    [Serializable]
    public class Habitat : StellarObject, IComparable<Habitat>, IComparable, IComparable<StellarObject>, IComparable<BuiltObject>, ISerializable
    {
        public object _LockObject = new object();

        public HabitatCategoryType Category;

        public HabitatType Type;

        private Galaxy _Galaxy;

        private byte _SolarRadiation;

        private byte _XrayRadiation;

        private byte _MicrowaveRadiation;

        private bool _OrbitDirection;

        private Habitat _Parent;

        private short _PictureRef;

        private byte _MapPictureRef;

        private short _LandscapePictureRef;

        public short _Diameter;

        private byte _OrbitSpeed;

        private short _OrbitDistance;

        private double _OrbitAngle;

        public long _MaxPopulation;

        public byte ResearchBonus;

        public IndustryType ResearchBonusIndustry;

        public PlanetaryFacilityList Facilities;

        public bool PlanetaryShieldPresent;

        public bool GiantIonCannonPresent;

        public byte DefensiveFortressBonus;

        private PirateColonyControlList _PirateColonyControl = new PirateColonyControlList();

        public Weapon GiantIonCannon;

        public InvasionStats InvasionStats;

        public float Damage;

        public ColonyPopulationPolicy ColonyPopulationPolicy;

        public ColonyPopulationPolicy ColonyPopulationPolicyRaceFamily;

        public float SlaveryBonusFactor = 1f;

        private float _BaseQuality = 1f;

        private float _Quality = 1f;

        private float _ColonyInfluenceRadius;

        private double _AnnualTaxRevenue;

        private bool _HasRings;

        public short PlagueId = -1;

        public float PlagueTimeRemaining;

        private string _ScenicFeature;

        public float ScenicFactor;

        private float _MigrationFactor;

        public int _DevelopmentLevel = 1;

        private bool _RestrictedResourcesPresent;

        private float _HappinessModifier;

        private float _CulturalDistressFactor;

        private bool _Rebelling;

        private float _TaxRate;

        private ManufacturingQueue _ManufacturingQueue;

        private BuiltObjectList _BasesAtHabitat;

        public float ConqueredFactor;

        public bool IsBlockaded;

        public float NovaProgression;

        public short NovaImageIndexMajor;

        public short NovaImageIndexMinor;

        public Explosion Explosion;

        private bool _HasBeenDestroyed;

        private bool _DestroyedAsteroidFieldGenerated;

        public ExplosionList Explosions;

        private TroopList _TroopsToRecruit;

        private TroopList _InvadingTroops;

        private CharacterList _InvadingCharacters;

        public HabitatResourceList Resources;

        private int _CurrentDefensiveForceAssigned;

        private int _EstimatedDefensiveForceRequired;

        private DateTime _LastTouch;

        private DateTime _LastIntermediateTouch;

        private DateTime _LastPeriodicTouch;

        private DateTime _LastLongTouch;

        private DateTime _LastHugeTouch;

        private DateTime _tempNow;

        private double _AnglePerSecond;

        private long _NextSoundTime;

        public int SystemIndex;

        public int HabitatIndex;

        [OptionalField]
        private float _DistanceFactor = 1f;

        private float _WarWithOurRace;

        [OptionalField]
        private int _DevelopmentLevelBaseline;

        public Ruin Ruin;

        [NonSerialized]
        public PlanetaryFacility WonderForDevelopment;

        [NonSerialized]
        private double _GrowthFactor = 1.0;

        [NonSerialized]
        private double _IncomeFactor = 1.0;

        [NonSerialized]
        private ResourceBonusList _ResourceBonuses = new ResourceBonusList();

        public RaceEventType RaceEventType;

        public bool HasSpacePort;

        [NonSerialized]
        public int InvasionSpaceControlStrengthDefenders = -1;

        [NonSerialized]
        public int InvasionSpaceControlStrengthAttackers = -1;

        [NonSerialized]
        public ColonyInvasion ColonyInvasion;

        [NonSerialized]
        public Empire TeardownEmpire;

        [NonSerialized]
        public volatile bool DoingTasks;

        [NonSerialized]
        public volatile bool DoingRemove;

        public Dictionary<string, object> BaconValues;

        public float BaseQuality
        {
            get
            {
                return _BaseQuality;
            }
            set
            {
                _BaseQuality = value;
                RecalculateQuality();
                RecalculateMaximumPopulation();
            }
        }

        public float Quality => _Quality;

        public float ColonyInfluenceRadius => _ColonyInfluenceRadius;

        public bool HasRings
        {
            get
            {
                return _HasRings;
            }
            set
            {
                _HasRings = value;
            }
        }

        public string ScenicFeature
        {
            get
            {
                return _ScenicFeature;
            }
            set
            {
                _ScenicFeature = value;
            }
        }

        public float MigrationFactor => _MigrationFactor;

        public long NextSoundTime
        {
            get
            {
                return _NextSoundTime;
            }
            set
            {
                _NextSoundTime = value;
            }
        }

        public bool Rebelling => _Rebelling;

        public long MaximumPopulation => _MaxPopulation;

        public float ResourceMultiplier
        {
            get
            {
                double num = 1.0;
                PopulationList population = Population;
                if (population != null && population.Count > 0)
                {
                    num = Math.Min(4.0, Math.Max(1.0, Galaxy.ColonyAnnualResourceConsumptionRate * ((double)population.TotalAmount / 20.0)));
                }
                return (float)num;
            }
        }

        public double IncomeFactor => _IncomeFactor;

        public double GrowthFactor => _GrowthFactor;

        public ResourceBonusList ResourceBonuses => _ResourceBonuses;

        public override int CargoSpace => 536870911;

        public override int TroopCapacityRemaining => 536870911;

        public BuiltObjectList BasesAtHabitat
        {
            get
            {
                return _BasesAtHabitat;
            }
            set
            {
                _BasesAtHabitat = value;
            }
        }

        public int StrategicValue
        {
            get
            {
                int val = DevelopmentLevel * (int)(Population.TotalAmount / 1000000);
                return Math.Max(10000, val);
            }
        }

        public int TroopLevelRequired
        {
            get
            {
                int num = (int)((double)EstimatedDefensiveForceRequired(atWar: false) * 0.5);
                int num2 = (int)(Math.Sqrt(_Galaxy.DifficultyLevel) * (double)(Galaxy.ColonyMaximumTroopStrength / 100));
                if (num > num2)
                {
                    num = num2;
                }
                if (Owner != null)
                {
                    double num3 = 1.0;
                    if (Owner.Capital == this || (Owner.Capitals != null && Owner.Capitals.Contains(this)))
                    {
                        num3 = 1.5;
                        if (Owner != _Galaxy.PlayerEmpire)
                        {
                            num3 *= Math.Sqrt(_Galaxy.DifficultyLevel);
                        }
                    }
                    else if (Owner.HomeWorld == this)
                    {
                        num3 = 1.5;
                        if (Owner != _Galaxy.PlayerEmpire)
                        {
                            num3 *= Math.Sqrt(_Galaxy.DifficultyLevel);
                        }
                    }
                    num = (int)((double)num * num3);
                }
                if (Empire != null && Empire.PenalColonies != null && Empire.PenalColonies.Count > 0 && Empire.PenalColonies.Contains(this))
                {
                    num = Math.Max(200, num);
                }
                if (Empire != null && Empire.Policy != null)
                {
                    num = (int)((double)num * Math.Max(Empire.Policy.TroopRecruitInfantryLevel, Empire.Policy.TroopGarrisonLevel));
                    num = Math.Max(num, Empire.Policy.TroopGarrisonMinimumPerColony * 100);
                }
                return num;
            }
        }

        public int TroopLevelMinimum
        {
            get
            {
                int num = TroopLevelRequired / 3;
                if (Empire != null && Empire.Policy != null)
                {
                    num = Math.Max(num, Empire.Policy.TroopGarrisonMinimumPerColony * 100);
                }
                return num;
            }
        }

        public int CurrentDefensiveForceAssigned
        {
            get
            {
                return _CurrentDefensiveForceAssigned;
            }
            set
            {
                _CurrentDefensiveForceAssigned = value;
            }
        }

        public TroopList TroopsToRecruit
        {
            get
            {
                return _TroopsToRecruit;
            }
            set
            {
                _TroopsToRecruit = value;
            }
        }

        public TroopList InvadingTroops
        {
            get
            {
                return _InvadingTroops;
            }
            set
            {
                _InvadingTroops = value;
            }
        }

        public CharacterList InvadingCharacters
        {
            get
            {
                return _InvadingCharacters;
            }
            set
            {
                _InvadingCharacters = value;
            }
        }

        public ManufacturingQueue ManufacturingQueue
        {
            get
            {
                return _ManufacturingQueue;
            }
            set
            {
                _ManufacturingQueue = value;
            }
        }

        public bool RestrictedResourcesPresent
        {
            get
            {
                return _RestrictedResourcesPresent;
            }
            set
            {
                _RestrictedResourcesPresent = value;
            }
        }

        public int DevelopmentLevelBaseline => _DevelopmentLevelBaseline;

        public int DevelopmentLevel
        {
            get
            {
                int val = 0;
                int val2 = 0;
                if (Ruin != null)
                {
                    val = (int)(Ruin.DevelopmentBonus * 100.0);
                }
                if (WonderForDevelopment != null)
                {
                    val2 = WonderForDevelopment.Value1;
                }
                int num = Math.Max(val, val2);
                if (_RestrictedResourcesPresent)
                {
                    num += 30;
                }
                num += (int)_ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.Development);
                if (RaceEventType == RaceEventType.TodashGalacticChampionships || RaceEventType == RaceEventType.PredictiveHistory)
                {
                    num += 5;
                }
                return _DevelopmentLevelBaseline + _DevelopmentLevel + num + BaconHabitat.GetDevelopmentLevel(this);
            }
        }

        public double TaxApproval
        {
            get
            {
                double num = (0.15 - (double)_TaxRate) * 100.0;
                if (num > 0.0)
                {
                    num *= 0.5;
                }
                if (Empire != null && Empire.GovernmentAttributes != null && Empire.GovernmentAttributes.SpecialFunctionCode == 1)
                {
                    num = -5.0;
                }
                return num;
            }
        }

        public double WarWithOurRace => _WarWithOurRace;

        public double RacialHappiness
        {
            get
            {
                if (Empire != null && Empire != _Galaxy.IndependentEmpire && Population != null && Population.Count > 0)
                {
                    Race dominantRace = Population.DominantRace;
                    if (dominantRace != null && Empire.DominantRace != dominantRace)
                    {
                        double num = 0.0;
                        EmpireList empireList = Empire.DetermineEmpiresWithDominantRace(dominantRace);
                        if (empireList != null && empireList.Count > 0)
                        {
                            EmpireEvaluation empireEvaluation = Empire.ObtainEmpireEvaluation(empireList[0]);
                            num = empireEvaluation.RacialOffense + empireEvaluation.SlaveryOffense;
                        }
                        ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy;
                        if (dominantRace.FamilyId == Empire.DominantRace.FamilyId)
                        {
                            colonyPopulationPolicy = ColonyPopulationPolicyRaceFamily;
                        }
                        switch (colonyPopulationPolicy)
                        {
                            case ColonyPopulationPolicy.Enslave:
                                num = Math.Min(num, -25.0);
                                break;
                            case ColonyPopulationPolicy.Exterminate:
                                num = Math.Min(num, -50.0);
                                break;
                        }
                        double num2 = Galaxy.ResolveStandardRaceBias(dominantRace, Empire.DominantRace);
                        return Math.Min(num2 * 0.5, num * 0.5);
                    }
                }
                return 0.0;
            }
        }

        public double EmpireApprovalRating
        {
            get
            {
                double taxApproval = TaxApproval;
                double inputValue = 0.0;
                if (Empire != null && Empire.LeaderChangeInfluence != 0.0)
                {
                    inputValue = Empire.LeaderChangeInfluence * 20.0;
                }
                double num = 0.0;
                double inputValue2 = 0.0;
                double num2 = 0.0;
                if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                {
                    num = Empire.WarWeariness;
                    if (Empire.GovernmentAttributes != null && Empire.GovernmentAttributes.WarWeariness != 0.0)
                    {
                        num *= Empire.GovernmentAttributes.WarWeariness;
                    }
                    if (Population != null && Population.DominantRace != null && Population.DominantRace.WarWearinessAttenuation > 0)
                    {
                        double num3 = (double)Population.DominantRace.WarWearinessAttenuation / 100.0;
                        num *= 1.0 - num3;
                    }
                    double num4 = _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.WarWearinessReduction) / 100.0;
                    if (num4 > 0.0)
                    {
                        num /= 1.0 + num4;
                    }
                    if (RaceEventType == RaceEventType.TodashGalacticChampionships)
                    {
                        num *= 0.9;
                    }
                    if (Empire != null && Empire.Leader != null)
                    {
                        double num5 = 1.0 + (double)Empire.Leader.WarWeariness / 100.0;
                        num /= num5;
                    }
                    if (Characters != null && Characters.Count > 0)
                    {
                        int highestSkillLevelExcludeLeaders = Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.WarWeariness);
                        double num6 = 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
                        num /= num6;
                    }
                    inputValue2 = Empire.CivilityRatingApprovalRaw;
                    double num7 = 1.0;
                    if (Population != null && Population.DominantRace != null)
                    {
                        num7 = Empire.CalculateRacialReputationConcern(Population.DominantRace);
                    }
                    inputValue2 /= num7;
                    num2 = CalculatePopulationPolicyConcern();
                }
                double racialHappiness = RacialHappiness;
                double warWithOurRace = WarWithOurRace;
                double inputValue3 = (double)Damage * -20.0;
                _ = (double)DevelopmentLevel / 5.0;
                _ = CulturalDistressFactor;
                double num8 = ModifyApprovalValueByEmpireAttributes(inputValue);
                double num9 = ModifyApprovalValueByEmpireAttributes((double)DevelopmentLevel / 5.0);
                double num10 = ModifyApprovalValueByEmpireAttributes(taxApproval);
                double num11 = ModifyApprovalValueByEmpireAttributes(num * -0.3);
                double num12 = ModifyApprovalValueByEmpireAttributes((double)CulturalDistressFactor * -1.0);
                double num13 = ModifyApprovalValueByEmpireAttributes(_HappinessModifier);
                double num14 = ModifyApprovalValueByEmpireAttributes(inputValue2);
                double num15 = ModifyApprovalValueByEmpireAttributes(racialHappiness);
                double num16 = ModifyApprovalValueByEmpireAttributes(warWithOurRace);
                double num17 = ModifyApprovalValueByEmpireAttributes(inputValue3);
                double num18 = ModifyApprovalValueByEmpireAttributes(ConqueredFactor);
                double inputValue4 = -15.0 * (1.0 - CalculateStrategicResourceSupplyGrowthFactor());
                double num19 = ModifyApprovalValueByEmpireAttributes(inputValue4);
                double inputValue5 = RaidEconomyDamageFactor * -20.0;
                double num20 = ModifyApprovalValueByEmpireAttributes(inputValue5);
                double plagueUnhappinessFactor = GetPlagueUnhappinessFactor();
                double num21 = ModifyApprovalValueByEmpireAttributes(plagueUnhappinessFactor);
                double num22 = num8 + num12 + num9 + num13 + num10 + num11 + num14 + num2 + num15 + num16 + num17 + num18 + num19 + num20 + num21;
                num22 += _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.Happiness);
                if (RaceEventType == RaceEventType.NepthysWineVintage)
                {
                    num22 += 5.0;
                }
                int num23 = 0;
                if (Characters != null && Characters.Count > 0)
                {
                    num23 += Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyHappiness);
                }
                if (Empire != null && Empire.Leader != null)
                {
                    num23 += Empire.Leader.ColonyHappiness;
                }
                double num24 = 1.0 + (double)num23 / 100.0;
                num22 = ((!(num22 > 0.0)) ? (num22 / num24) : (num22 * num24));
                if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.SpecialBonusHappiness > 0.0)
                {
                    if (num22 > 0.0)
                    {
                        num22 *= 1.0 + Empire.SpecialBonusHappiness;
                    }
                    else if (num22 < 0.0)
                    {
                        num22 /= 1.0 + Empire.SpecialBonusHappiness;
                    }
                }
                double num25 = 0.0;
                if (Facilities != null && Facilities.Count > 0)
                {
                    for (int i = 0; i < Facilities.Count; i++)
                    {
                        PlanetaryFacility planetaryFacility = Facilities[i];
                        if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == WonderType.ColonyHappiness)
                        {
                            num25 = (double)planetaryFacility.Value2 / 100.0;
                        }
                    }
                }
                if (num25 > 0.0)
                {
                    if (num22 > 0.0)
                    {
                        num22 *= 1.0 + num25;
                    }
                    else if (num22 < 0.0)
                    {
                        num22 /= 1.0 + num25;
                    }
                }
                return num22;
            }
        }

        public float HappinessModifier
        {
            get
            {
                return _HappinessModifier;
            }
            set
            {
                _HappinessModifier = value;
            }
        }

        public float CulturalDistressFactor
        {
            get
            {
                return _CulturalDistressFactor;
            }
            set
            {
                _CulturalDistressFactor = value;
            }
        }

        public float TaxRate
        {
            get
            {
                return _TaxRate;
            }
            set
            {
                _TaxRate = value;
            }
        }

        public double TaxComplianceRate
        {
            get
            {
                double num = (EmpireApprovalRating + 83.0) / 100.0;
                if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                {
                    num *= 0.75;
                }
                return Math.Max(0.0, Math.Min(1.0, num));
            }
        }

        public double AnnualTaxRevenue => _AnnualTaxRevenue;

        public double TaxResistance
        {
            get
            {
                double num = 1.0;
                if (Empire != null)
                {
                    long totalAmount = Population.TotalAmount;
                    long num2 = totalAmount * DevelopmentLevel;
                    double num3 = num2 - Empire.TaxResistanceThreshold * 100;
                    if (num3 > 0.0)
                    {
                        double num4 = 1.0 + num3 / (double)(Math.Max(1L, Empire.TaxResistanceThreshold) * 100);
                        num = num4 * Empire.TaxResistanceRate;
                        num = Math.Max(1.0, num);
                    }
                    if (double.IsNaN(num))
                    {
                        num = 1.0;
                    }
                }
                return num;
            }
        }

        public double AnnualRevenueAdjusted
        {
            get
            {
                double result = 0.0;
                if (Owner != null && Owner != _Galaxy.IndependentEmpire)
                {
                    double annualRevenue = AnnualRevenue;
                    if (annualRevenue >= 0.0)
                    {
                        double num = annualRevenue / Owner.PrivateAnnualRevenueUnadjusted;
                        result = Owner.PrivateAnnualRevenue * num;
                    }
                    else
                    {
                        result = annualRevenue;
                    }
                }
                return result;
            }
        }

        public double Corruption
        {
            get
            {
                double val = 0.0;
                if (Empire != null && Population != null)
                {
                    long num = Galaxy.ColonyCorruptionPopulationThreshhold;
                    if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                    {
                        num = 0L;
                    }
                    double num2 = 0.0;
                    long totalAmount = Population.TotalAmount;
                    if (Empire != null && Empire != _Galaxy.IndependentEmpire && totalAmount > num)
                    {
                        num2 = (double)totalAmount / 200000000.0 / 100.0;
                        if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
                        {
                            num2 = (double)totalAmount / 50000000.0 / 100.0;
                        }
                        num2 = Math.Max(0.0, Math.Min(num2, 0.7));
                        if (Empire.GovernmentAttributes != null)
                        {
                            num2 *= Empire.GovernmentAttributes.Corruption;
                        }
                        num2 *= Empire.ColonyCorruptionFactor;
                    }
                    val = num2 * (1.0 + Empire.Corruption) * Math.Sqrt(DistanceFactor);
                    if (_PirateColonyControl != null && _PirateColonyControl.Count > 0)
                    {
                        double num3 = 0.0;
                        PirateColonyControl highestControl = _PirateColonyControl.GetHighestControl();
                        if (highestControl != null)
                        {
                            num3 = highestControl.ControlLevel / 10f;
                        }
                        if (Facilities != null)
                        {
                            PlanetaryFacility planetaryFacility = Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                            if (planetaryFacility != null)
                            {
                                num3 += (double)planetaryFacility.Value3 / 100.0;
                            }
                        }
                        val += num3;
                    }
                    int num4 = 0;
                    if (Characters != null && Characters.Count > 0)
                    {
                        num4 += Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyCorruption);
                    }
                    if (Empire != null && Empire.Leader != null)
                    {
                        num4 += Empire.Leader.ColonyCorruption;
                    }
                    double num5 = 1.0 - (double)num4 / 100.0;
                    val *= num5;
                    val *= Empire.CorruptionMultiplier;
                    val = Math.Min(0.8, val);
                }
                return Math.Min(1.0, Math.Max(0.0, val));
            }
        }

        public double DistanceFactor => _DistanceFactor;

        public double AnnualRevenue
        {
            get
            {
                double num = 0.0;
                long num2 = 0L;
                if (Population != null)
                {
                    num2 = Population.TotalAmount;
                }
                double colonyRevenueDivisor = Galaxy.ColonyRevenueDivisor;
                if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.PirateEmpireBaseHabitat == null)
                {
                    colonyRevenueDivisor = Empire.ColonyRevenueDivisor;
                }
                double num3 = ((double)Quality - 0.5) * 2.0;
                if (Quality < 0.5f)
                {
                    num = (double)num2 * num3 * (100.0 - (double)DevelopmentLevel / 2.0) * (0.5 + Corruption) / colonyRevenueDivisor;
                }
                else
                {
                    num = (double)num2 * (double)DevelopmentLevel * (1.0 - Corruption) * num3 / colonyRevenueDivisor;
                    num *= _IncomeFactor;
                    if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                    {
                        num *= 1.0 + Empire.TradeBonus;
                        if (Empire.GovernmentAttributes != null)
                        {
                            num *= Empire.GovernmentAttributes.TradeBonus;
                        }
                        num *= (double)SlaveryBonusFactor;
                        num *= 1.0 + Empire.SpecialBonusWealth;
                        double num4 = 1.0 + _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.IncomeBoost) / 100.0;
                        num *= num4;
                    }
                }
                int num5 = 0;
                if (Characters != null && Characters.Count > 0)
                {
                    num5 += Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyIncome);
                }
                if (Empire != null && Empire.Leader != null)
                {
                    num5 += Empire.Leader.ColonyIncome;
                }
                double num6 = 1.0 + (double)num5 / 100.0;
                num *= num6;
                if (RaidCountdown > 0)
                {
                    double raidEconomyDamageFactor = RaidEconomyDamageFactor;
                    num *= 1.0 - raidEconomyDamageFactor;
                }
                if (Empire != null)
                {
                    num *= Empire.ColonyIncomeFactor;
                }
                return num;
            }
        }

        public double RaidEconomyDamageFactor
        {
            get
            {
                if (RaidCountdown > 0)
                {
                    double num = 1.0 - (double)(int)RaidCountdown / 60.0;
                    num *= 0.5;
                    num += 0.5;
                    num = Math.Min(1.0, Math.Max(0.5, num));
                    return 1.0 - num;
                }
                return 0.0;
            }
        }

        public double OrbitAngle
        {
            get
            {
                return _OrbitAngle;
            }
            set
            {
                if (value < Math.PI * 2.0 && value >= 0.0)
                {
                    _OrbitAngle = value;
                    return;
                }
                throw new ApplicationException("OrbitAngle out of valid range");
            }
        }

        public short OrbitDistance
        {
            get
            {
                return _OrbitDistance;
            }
            set
            {
                _OrbitDistance = value;
                double num = CalculateOrbitPathLength();
                _AnglePerSecond = Math.PI * 2.0 / (num / (double)(int)_OrbitSpeed);
            }
        }

        public short PictureRef
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

        public byte MapPictureRef
        {
            get
            {
                return _MapPictureRef;
            }
            set
            {
                _MapPictureRef = value;
            }
        }

        public short LandscapePictureRef
        {
            get
            {
                return _LandscapePictureRef;
            }
            set
            {
                _LandscapePictureRef = value;
            }
        }

        public short Diameter
        {
            get
            {
                return _Diameter;
            }
            set
            {
                _Diameter = value;
                RecalculateMaximumPopulation();
            }
        }

        public byte OrbitSpeed
        {
            get
            {
                return _OrbitSpeed;
            }
            set
            {
                _OrbitSpeed = value;
                double num = CalculateOrbitPathLength();
                _AnglePerSecond = Math.PI * 2.0 / (num / (double)(int)_OrbitSpeed);
            }
        }

        public bool OrbitDirection
        {
            get
            {
                return _OrbitDirection;
            }
            set
            {
                _OrbitDirection = value;
            }
        }

        public Habitat Parent
        {
            get
            {
                return _Parent;
            }
            set
            {
                _Parent = value;
            }
        }

        public byte SolarRadiation
        {
            get
            {
                return _SolarRadiation;
            }
            set
            {
                _SolarRadiation = value;
            }
        }

        public byte XrayRadiation
        {
            get
            {
                return _XrayRadiation;
            }
            set
            {
                _XrayRadiation = value;
            }
        }

        public byte MicrowaveRadiation
        {
            get
            {
                return _MicrowaveRadiation;
            }
            set
            {
                _MicrowaveRadiation = value;
            }
        }

        public void RecalculateQuality()
        {
            _Quality = _BaseQuality * (1f - Damage);
        }

        public void RecalculateColonyInfluenceRadius(bool empireHasWarptech)
        {
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Population != null)
            {
                RecalculateDevelopmentLevelBaseline();
                if (Population.TotalAmount > 0)
                {
                    double num = Math.Max(10, DevelopmentLevel) * (Population.TotalAmount / 1000000);
                    if (Empire.DominantRace != null && !Empire.DominantRace.Expanding)
                    {
                        _ColonyInfluenceRadius = 200000f + Math.Max(0f, Math.Min(150000f, (float)(Math.Sqrt(StrategicValue) * 700.0)));
                    }
                    else
                    {
                        int num2 = (int)num;
                        int num3 = 700000;
                        if (num2 > num3)
                        {
                            float num4 = 500000f + Math.Max(0f, Math.Min(1000000f, (float)(Math.Sqrt(num2) * 700.0)));
                            float num5 = (float)(Math.Sqrt(num2 - num3) * 100.0);
                            _ColonyInfluenceRadius = num4 + num5;
                        }
                        else
                        {
                            _ColonyInfluenceRadius = 500000f + Math.Max(0f, Math.Min(1000000f, (float)(Math.Sqrt(num2) * 700.0)));
                        }
                    }
                }
                else
                {
                    _ColonyInfluenceRadius = 0f;
                }
            }
            else
            {
                _ColonyInfluenceRadius = 0f;
            }
            if (_Galaxy.EmpireTerritoryColonyInfluenceRangeFactor <= 0f)
            {
                double val = Math.Sqrt((double)(_Galaxy.SectorWidth * _Galaxy.SectorHeight) / (double)_Galaxy.StarCount * 7.0);
                val = Math.Max(0.5, Math.Min(2.0, val));
                _Galaxy.EmpireTerritoryColonyInfluenceRangeFactor = (float)val;
            }
            _ColonyInfluenceRadius = _ColonyInfluenceRadius * _Galaxy.EmpireTerritoryColonyInfluenceRangeFactor * BaconHabitat.TerritoryMultipler(this);
            if (!empireHasWarptech)
            {
                _ColonyInfluenceRadius = Math.Min(100000f, _ColonyInfluenceRadius);
            }
        }

        public double CalculateScenicFactorIncludingRuinsWonders()
        {
            string scenicFeature = string.Empty;
            return CalculateScenicFactorIncludingRuinsWonders(out scenicFeature);
        }

        public double CalculateScenicFactorIncludingRuinsWonders(out string scenicFeature)
        {
            double num = 0.0;
            scenicFeature = string.Empty;
            if (ScenicFactor > 0f)
            {
                num = Math.Max(num, ScenicFactor);
                scenicFeature = ScenicFeature;
            }
            if (Ruin != null && Ruin.DevelopmentBonus > num)
            {
                num = Ruin.DevelopmentBonus;
                scenicFeature = Ruin.Name;
            }
            if (Facilities != null && Facilities.CountByType(PlanetaryFacilityType.Wonder) > 0)
            {
                int num2 = 0;
                PlanetaryFacility planetaryFacility = null;
                for (int i = 0; i < Facilities.Count; i++)
                {
                    PlanetaryFacility planetaryFacility2 = Facilities[i];
                    if (planetaryFacility2 != null && planetaryFacility2.ConstructionProgress >= 1f && planetaryFacility2.Type == PlanetaryFacilityType.Wonder)
                    {
                        num2 = Math.Max(num2, planetaryFacility2.Value1);
                        planetaryFacility = planetaryFacility2;
                    }
                }
                double num3 = (double)num2 / 100.0;
                if (num3 > num)
                {
                    num = num3;
                    if (planetaryFacility != null)
                    {
                        scenicFeature = planetaryFacility.Name;
                    }
                }
            }
            return num;
        }

        public void CalculateMigrationFactor()
        {
            if (Population != null && Population.TotalAmount > 0)
            {
                double num = 0.0;
                if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                {
                    num = EmpireApprovalRating / 100.0;
                }
                double num2 = Math.Min(1.0, Math.Max(-1.0, (double)(3000000000u - Population.TotalAmount) / 5000000000.0));
                double num3 = ((double)Quality - 0.5) / 5.0 + (double)ScenicFactor * 0.5;
                double num4 = 0.0;
                if ((double)_TaxRate < 0.15)
                {
                    num4 = (float)((0.15 - (double)_TaxRate) * Math.Max(0.0, num2) * 2.0);
                }
                _MigrationFactor = (float)(num4 + num + num2 + num3);
                if (Empire != null && Empire.DominantRace != null)
                {
                    _MigrationFactor = (float)((double)_MigrationFactor * Empire.DominantRace.MigrationFactor);
                }
                if (_MigrationFactor < 0f && Population.TotalAmount < 1000000000)
                {
                    _MigrationFactor = 0f;
                }
            }
            else
            {
                _MigrationFactor = 0f;
            }
        }

        public void StartRebelling()
        {
            if (!_Rebelling)
            {
                string description = string.Format(TextResolver.GetText("Colony Rebelling"), Name);
                Owner.SendMessageToEmpire(Owner, EmpireMessageType.ColonyRebelling, this, description);
                _Rebelling = true;
            }
        }

        public void StopRebelling()
        {
            _Rebelling = false;
        }

        public Habitat(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            byte[] buffer = (byte[])info.GetValue("H_D", typeof(byte[]));
            using (MemoryStream input = new MemoryStream(buffer))
            {
                using BinaryReader binaryReader = new BinaryReader(input);
                Category = (HabitatCategoryType)binaryReader.ReadByte();
                Type = (HabitatType)binaryReader.ReadByte();
                _SolarRadiation = binaryReader.ReadByte();
                _XrayRadiation = binaryReader.ReadByte();
                _MicrowaveRadiation = binaryReader.ReadByte();
                _OrbitDirection = binaryReader.ReadBoolean();
                _PictureRef = binaryReader.ReadInt16();
                _MapPictureRef = binaryReader.ReadByte();
                _LandscapePictureRef = binaryReader.ReadInt16();
                _Diameter = binaryReader.ReadInt16();
                _OrbitSpeed = binaryReader.ReadByte();
                _OrbitDistance = binaryReader.ReadInt16();
                _OrbitAngle = binaryReader.ReadDouble();
                _MaxPopulation = binaryReader.ReadInt64();
                Damage = binaryReader.ReadSingle();
                ColonyPopulationPolicy = (ColonyPopulationPolicy)binaryReader.ReadByte();
                ColonyPopulationPolicyRaceFamily = (ColonyPopulationPolicy)binaryReader.ReadByte();
                SlaveryBonusFactor = binaryReader.ReadSingle();
                PlagueId = binaryReader.ReadInt16();
                PlagueTimeRemaining = binaryReader.ReadSingle();
                _AnnualTaxRevenue = binaryReader.ReadDouble();
                _HasRings = binaryReader.ReadBoolean();
                ScenicFactor = binaryReader.ReadSingle();
                _MigrationFactor = binaryReader.ReadSingle();
                _DevelopmentLevel = binaryReader.ReadInt16();
                _DevelopmentLevelBaseline = binaryReader.ReadInt16();
                _WarWithOurRace = binaryReader.ReadSingle();
                _DistanceFactor = binaryReader.ReadSingle();
                _RestrictedResourcesPresent = binaryReader.ReadBoolean();
                _HappinessModifier = binaryReader.ReadSingle();
                _CulturalDistressFactor = binaryReader.ReadSingle();
                _Rebelling = binaryReader.ReadBoolean();
                _TaxRate = binaryReader.ReadSingle();
                ConqueredFactor = binaryReader.ReadSingle();
                IsBlockaded = binaryReader.ReadBoolean();
                NovaProgression = binaryReader.ReadSingle();
                NovaImageIndexMajor = binaryReader.ReadInt16();
                NovaImageIndexMinor = binaryReader.ReadInt16();
                _HasBeenDestroyed = binaryReader.ReadBoolean();
                _DestroyedAsteroidFieldGenerated = binaryReader.ReadBoolean();
                _CurrentDefensiveForceAssigned = binaryReader.ReadInt32();
                _EstimatedDefensiveForceRequired = binaryReader.ReadInt32();
                _AnglePerSecond = binaryReader.ReadDouble();
                _NextSoundTime = binaryReader.ReadInt64();
                SystemIndex = binaryReader.ReadInt32();
                HabitatIndex = binaryReader.ReadInt32();
                HasSpacePort = binaryReader.ReadBoolean();
                BaseQuality = binaryReader.ReadSingle();
                _LastTouch = new DateTime(binaryReader.ReadInt64());
                _LastIntermediateTouch = new DateTime(binaryReader.ReadInt64());
                _LastPeriodicTouch = new DateTime(binaryReader.ReadInt64());
                _LastLongTouch = new DateTime(binaryReader.ReadInt64());
                _LastHugeTouch = new DateTime(binaryReader.ReadInt64());
                _tempNow = new DateTime(binaryReader.ReadInt64());
                ResearchBonus = binaryReader.ReadByte();
                ResearchBonusIndustry = (IndustryType)binaryReader.ReadByte();
                RaceEventType = (RaceEventType)binaryReader.ReadByte();
                PlanetaryShieldPresent = binaryReader.ReadBoolean();
                GiantIonCannonPresent = binaryReader.ReadBoolean();
                DefensiveFortressBonus = binaryReader.ReadByte();
                int num = binaryReader.ReadByte();
                for (int i = 0; i < num; i++)
                {
                    int empireId = binaryReader.ReadByte();
                    float controlLevel = binaryReader.ReadSingle();
                    bool hasFacilityControl = binaryReader.ReadBoolean();
                    _PirateColonyControl.Add(new PirateColonyControl(empireId, controlLevel, hasFacilityControl));
                }
                _PirateColonyControl.Sort();
                _PirateColonyControl.Reverse();
                binaryReader.Close();
            }
            _Galaxy = (Galaxy)info.GetValue("Gx", typeof(Galaxy));
            _Parent = (Habitat)info.GetValue("Pa", typeof(Habitat));
            _ScenicFeature = info.GetString("Sc");
            _ManufacturingQueue = (ManufacturingQueue)info.GetValue("MQ", typeof(ManufacturingQueue));
            _BasesAtHabitat = (BuiltObjectList)info.GetValue("Bs", typeof(BuiltObjectList));
            _TroopsToRecruit = (TroopList)info.GetValue("TR", typeof(TroopList));
            _InvadingTroops = (TroopList)info.GetValue("TI", typeof(TroopList));
            _InvadingCharacters = (CharacterList)info.GetValue("CI", typeof(CharacterList));
            Ruin = (Ruin)info.GetValue("Rn", typeof(Ruin));
            Resources = (HabitatResourceList)info.GetValue("Res", typeof(HabitatResourceList));
            Facilities = (PlanetaryFacilityList)info.GetValue("Fac", typeof(PlanetaryFacilityList));
            GiantIonCannon = (Weapon)info.GetValue("GIC", typeof(Weapon));
            InvasionStats = (InvasionStats)info.GetValue("InSt", typeof(InvasionStats));
            RecalculateCriticalResourceSupplyBonuses();
            ReviewPlanetaryFacilities(Empire);
            BaconHabitat.DeserializeExtraFields(this, info);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
                binaryWriter.Write((byte)Category);
                binaryWriter.Write((byte)Type);
                binaryWriter.Write(_SolarRadiation);
                binaryWriter.Write(_XrayRadiation);
                binaryWriter.Write(_MicrowaveRadiation);
                binaryWriter.Write(_OrbitDirection);
                binaryWriter.Write(_PictureRef);
                binaryWriter.Write(_MapPictureRef);
                binaryWriter.Write(_LandscapePictureRef);
                binaryWriter.Write(_Diameter);
                binaryWriter.Write(_OrbitSpeed);
                binaryWriter.Write(_OrbitDistance);
                binaryWriter.Write(_OrbitAngle);
                binaryWriter.Write(_MaxPopulation);
                binaryWriter.Write(Damage);
                binaryWriter.Write((byte)ColonyPopulationPolicy);
                binaryWriter.Write((byte)ColonyPopulationPolicyRaceFamily);
                binaryWriter.Write(SlaveryBonusFactor);
                binaryWriter.Write(PlagueId);
                binaryWriter.Write(PlagueTimeRemaining);
                binaryWriter.Write(_AnnualTaxRevenue);
                binaryWriter.Write(_HasRings);
                binaryWriter.Write(ScenicFactor);
                binaryWriter.Write(_MigrationFactor);
                binaryWriter.Write((short)_DevelopmentLevel);
                binaryWriter.Write((short)_DevelopmentLevelBaseline);
                binaryWriter.Write(_WarWithOurRace);
                binaryWriter.Write(_DistanceFactor);
                binaryWriter.Write(_RestrictedResourcesPresent);
                binaryWriter.Write(_HappinessModifier);
                binaryWriter.Write(_CulturalDistressFactor);
                binaryWriter.Write(_Rebelling);
                binaryWriter.Write(_TaxRate);
                binaryWriter.Write(ConqueredFactor);
                binaryWriter.Write(IsBlockaded);
                binaryWriter.Write(NovaProgression);
                binaryWriter.Write(NovaImageIndexMajor);
                binaryWriter.Write(NovaImageIndexMinor);
                binaryWriter.Write(_HasBeenDestroyed);
                binaryWriter.Write(_DestroyedAsteroidFieldGenerated);
                binaryWriter.Write(_CurrentDefensiveForceAssigned);
                binaryWriter.Write(_EstimatedDefensiveForceRequired);
                binaryWriter.Write(_AnglePerSecond);
                binaryWriter.Write(_NextSoundTime);
                binaryWriter.Write(SystemIndex);
                binaryWriter.Write(HabitatIndex);
                binaryWriter.Write(HasSpacePort);
                binaryWriter.Write(BaseQuality);
                binaryWriter.Write(_LastTouch.Ticks);
                binaryWriter.Write(_LastIntermediateTouch.Ticks);
                binaryWriter.Write(_LastPeriodicTouch.Ticks);
                binaryWriter.Write(_LastLongTouch.Ticks);
                binaryWriter.Write(_LastHugeTouch.Ticks);
                binaryWriter.Write(_tempNow.Ticks);
                binaryWriter.Write(ResearchBonus);
                binaryWriter.Write((byte)ResearchBonusIndustry);
                binaryWriter.Write((byte)RaceEventType);
                binaryWriter.Write(PlanetaryShieldPresent);
                binaryWriter.Write(GiantIonCannonPresent);
                binaryWriter.Write(DefensiveFortressBonus);
                binaryWriter.Write((byte)_PirateColonyControl.Count);
                for (int i = 0; i < _PirateColonyControl.Count; i++)
                {
                    binaryWriter.Write(_PirateColonyControl[i].EmpireId);
                    binaryWriter.Write(_PirateColonyControl[i].ControlLevel);
                    binaryWriter.Write(_PirateColonyControl[i].HasFacilityControl);
                }
                binaryWriter.Flush();
                binaryWriter.Close();
                info.AddValue("H_D", memoryStream.ToArray());
            }
            info.AddValue("Gx", _Galaxy);
            info.AddValue("Pa", _Parent);
            info.AddValue("Sc", _ScenicFeature);
            info.AddValue("MQ", _ManufacturingQueue);
            info.AddValue("Bs", _BasesAtHabitat);
            info.AddValue("TR", _TroopsToRecruit);
            info.AddValue("TI", _InvadingTroops);
            info.AddValue("CI", _InvadingCharacters);
            info.AddValue("Rn", Ruin);
            info.AddValue("Res", Resources);
            info.AddValue("Fac", Facilities);
            info.AddValue("GIC", GiantIonCannon);
            info.AddValue("InSt", InvasionStats);
            BaconHabitat.SerializeExtraFields(this, info);
        }

        public bool DoTasks(DateTime time)
        {
            _tempNow = time;
            if (Explosion != null)
            {
                DoExplosion();
            }
            if (!HasBeenDestroyed)
            {
                DoingTasks = true;
                Move(_Galaxy);
                if (Explosions != null && Explosions.Count > 0)
                {
                    DoExplosions();
                }
                if (_tempNow < _LastIntermediateTouch)
                {
                    _LastIntermediateTouch = _tempNow;
                }
                if (_tempNow < _LastPeriodicTouch)
                {
                    _LastPeriodicTouch = _tempNow;
                }
                if (_tempNow < _LastLongTouch)
                {
                    _LastLongTouch = _tempNow;
                }
                if (_tempNow < _LastTouch)
                {
                    _LastTouch = _tempNow;
                }
                TimeSpan timeSpan = _tempNow - _LastIntermediateTouch;
                TimeSpan timeSpan2 = _tempNow - _LastPeriodicTouch;
                TimeSpan timeSpan3 = _tempNow - _LastTouch;
                double timePassed = (double)_tempNow.Subtract(_LastLongTouch).Ticks / 10000000.0;
                TimeSpan timeSpan4 = _tempNow.Subtract(_LastLongTouch);
                TimeSpan timeSpan5 = _tempNow.Subtract(_LastHugeTouch);
                HandleWeaponsFiring(timeSpan3.TotalSeconds, time, _Galaxy);
                if (timeSpan > Galaxy.IntermediateProcessingSpan)
                {
                    CheckForShipsDiscoveringRuins();
                    _LastIntermediateTouch = _tempNow;
                }
                if (timeSpan2 > Galaxy.PeriodicProcessingSpan)
                {
                    CalculateWarWithOurRace();
                    ScanForNewOwner();
                    if (!_Rebelling)
                    {
                        GrowPopulation(timeSpan2);
                    }
                    if (InvadingTroops != null && InvadingTroops.Count > 0)
                    {
                        ResolveInvasionEmpires(out var defender, out var invader);
                        int spaceControlStrengthDefenders = -1;
                        int spaceControlStrengthAttackers = -1;
                        CalculateSpaceControlStrengths(defender, invader, out spaceControlStrengthDefenders, out spaceControlStrengthAttackers);
                        InvasionSpaceControlStrengthDefenders = spaceControlStrengthDefenders;
                        InvasionSpaceControlStrengthAttackers = spaceControlStrengthAttackers;
                    }
                    else
                    {
                        InvasionSpaceControlStrengthDefenders = -1;
                        InvasionSpaceControlStrengthAttackers = -1;
                    }
                    if (ColonyInvasion == null)
                    {
                        ProcessColonyTroops(timeSpan2.TotalSeconds);
                        ResolveInvasionBattles(timeSpan2, _Galaxy);
                    }
                    if (!_Rebelling)
                    {
                        ExtractResources(timeSpan2.TotalSeconds);
                    }
                    if (!_Rebelling && Owner != null)
                    {
                        if (_ManufacturingQueue != null)
                        {
                            _ManufacturingQueue.DoManufacturing(_Galaxy, time, _Galaxy.CurrentStarDate);
                        }
                        if (ConstructionQueue != null)
                        {
                            ConstructionQueue.DoConstruction(_Galaxy, time);
                        }
                    }
                    ConstructFacilities(timeSpan2.TotalSeconds);
                    if (Owner != null && Owner != _Galaxy.IndependentEmpire)
                    {
                        IsShipYard = true;
                    }
                    else
                    {
                        IsShipYard = false;
                    }
                    CheckForShipsOfNewEmpiresInSystem(_Galaxy, time);
                    AttackEnemyTargets(time);
                    ProcessPlague(timeSpan2.TotalSeconds);
                    ReviewPirateControl(timeSpan2.TotalSeconds);
                    _LastPeriodicTouch = _tempNow;
                }
                if (timeSpan4 > Galaxy.LongProcessingSpan)
                {
                    ReviewWhetherRefuellingDepot();
                    if (_Galaxy.SpawnNewEmpires)
                    {
                        CheckHabitatIsEmpire(_Galaxy);
                    }
                    if (ConstructionQueue != null && Owner != null)
                    {
                        ConstructionQueue.ReviewConstructionSpeed();
                    }
                    RegenerateDamage(timeSpan4.TotalSeconds);
                    TerraformColony(timePassed);
                    IndependentColoniesRecruitAndTrainTroops(timePassed);
                    RecalculateDevelopmentLevelBaseline();
                    RecalculateAnnualTaxRevenue();
                    ConsumeResources(timePassed);
                    ConsumeAndOrderStrategicResourceSupply(timePassed);
                    CheckForShipsNoLongerDocking();
                    CheckForSpacePortFacilities(_Galaxy);
                    CheckSatisfaction();
                    UpdateConqueredFactor(timePassed);
                    CalculateMigrationFactor();
                    UpdateRaidCountdown(timePassed);
                    _LastLongTouch = _tempNow;
                }
                if (timeSpan5 > Galaxy.HugeProcessingSpan)
                {
                    BaconHabitat.HugeProcessingSpanActions(this);
                    ClearTroopsAwaitingPickup();
                    CheckForUnownedCargo();
                    bool empireHasWarptech = true;
                    if (Empire != null)
                    {
                        empireHasWarptech = Empire.CheckEmpireHasHyperDriveTech(Empire);
                    }
                    RecalculateColonyInfluenceRadius(empireHasWarptech);
                    if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                    {
                        _Galaxy.ChanceColonyGovernorPromotion(Empire, this);
                    }
                    SpawnCreatures();
                    ReviewManufacturedResources();
                    _Galaxy.CheckRemoveInvalidDockingShipsFromWaitQueue(this);
                    _LastHugeTouch = _tempNow;
                }
                _LastTouch = _tempNow;
                DoingTasks = false;
                return true;
            }
            if (Explosion == null && _Galaxy.Systems[SystemIndex].Habitats.Contains(this) && _Galaxy.Habitats.Contains(this) && !DoingRemove)
            {
                Thread thread = new Thread(DoPlanetRemove);
                thread.Start();
            }
            _LastTouch = _tempNow;
            return false;
        }

        private void CheckForUnownedCargo()
        {
            if (Cargo == null || Cargo.Count <= 0)
            {
                return;
            }
            Empire empire = Empire;
            if (empire == null)
            {
                return;
            }
            CargoList cargoList = new CargoList();
            CargoList cargoList2 = new CargoList();
            for (int i = 0; i < Cargo.Count; i++)
            {
                Cargo cargo = Cargo[i];
                if (cargo != null && cargo.EmpireId < 0)
                {
                    Cargo cargo2 = null;
                    if (cargo.CommodityIsComponent)
                    {
                        cargo2 = new Cargo(cargo.Component, cargo.Amount, empire, 0);
                    }
                    else if (cargo.CommodityIsResource)
                    {
                        cargo2 = new Cargo(cargo.Resource, cargo.Amount, empire, 0);
                    }
                    cargoList.Add(cargo2);
                    cargoList2.Add(cargo);
                }
            }
            for (int j = 0; j < cargoList2.Count; j++)
            {
                Cargo.Remove(cargoList2[j]);
            }
            foreach (Cargo item in cargoList)
            {
                Cargo.Add(item);
            }
        }

        private void UpdateConqueredFactor(double timePassed)
        {
            if (ConqueredFactor < 0f)
            {
                float num = (float)(20.0 * (timePassed / (double)Galaxy.RealSecondsInGalacticYear));
                float num2 = (ConqueredFactor = Math.Min(0f, ConqueredFactor + num));
            }
        }

        private void UpdateRaidCountdown(double timePassed)
        {
            if (RaidCountdown > 0)
            {
                int num = (int)(timePassed / 10.0);
                int val = RaidCountdown - num;
                val = Math.Min(255, Math.Max(0, val));
                RaidCountdown = (byte)val;
            }
        }

        private void SpawnCreatures()
        {
            if (!Resources.ContainsName("Korabbian Spice"))
            {
                return;
            }
            int num = 0;
            if (_Galaxy.Systems[SystemIndex].Creatures.Count > 0)
            {
                for (int i = 0; i < _Galaxy.Systems[SystemIndex].Creatures.Count; i++)
                {
                    Creature creature = _Galaxy.Systems[SystemIndex].Creatures[i];
                    if (creature != null && !creature.HasBeenDestroyed && creature.ParentHabitat == this && creature.Type == CreatureType.DesertSpaceSlug)
                    {
                        num++;
                    }
                }
            }
            if (num <= 0)
            {
                _Galaxy.GenerateCreatureAtHabitat(CreatureType.DesertSpaceSlug, this, lockLocation: true);
            }
        }

        public void ProcessColonyTroops(double timePassed)
        {
            if (Empire != null)
            {
                double troopStrengthNeutralizationAmount = (double)Galaxy.TroopStrengthAnnualNeutralizationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
                double troopSizeRegenerationAmount = (double)Galaxy.TroopSizeAnnualRegenerationAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
                double troopRecruitmentAmount = (double)Galaxy.TroopAnnualRecruitmentAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
                Empire.ProcessColonyTroops(this, null, troopStrengthNeutralizationAmount, troopSizeRegenerationAmount, troopRecruitmentAmount, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, atWar: false, null, performRecruitment: false);
            }
        }

        public PirateColonyControlList GetPirateControl()
        {
            return _PirateColonyControl;
        }

        public void SetPirateControlRaw(PirateColonyControlList pirateControl)
        {
            _PirateColonyControl = pirateControl;
        }

        public void SetPirateControl(Empire pirateFaction, float controlLevel)
        {
            if (pirateFaction != null && pirateFaction.PirateEmpireBaseHabitat != null && _PirateColonyControl.Count < 3 && !_PirateColonyControl.CheckFactionHasControl(pirateFaction.EmpireId))
            {
                PirateColonyControl item = new PirateColonyControl(pirateFaction.EmpireId, controlLevel);
                _PirateColonyControl.Add(item);
            }
        }

        private void ReviewPirateControl(double timePassed)
        {
            BaconHabitat.ReviewPirateControl(this, timePassed);
        }

        private void ProcessPlague(double timePassed)
        {
            if (PlagueId < 0 || !(PlagueTimeRemaining > 0f))
            {
                return;
            }
            double num = 0.0;
            int num2 = 1000;
            Plague plague = _Galaxy.Plagues[PlagueId];
            if (plague != null)
            {
                num = plague.MortalityRate;
                num2 = plague.InfectionChance;
                int num3 = num2;
                if (!string.IsNullOrEmpty(plague.ExceptionRaceName))
                {
                    Race dominantRace = Population.DominantRace;
                    if (dominantRace != null && dominantRace.Name == plague.ExceptionRaceName)
                    {
                        num3 = plague.ExceptionInfectionChance;
                    }
                }
                int num4 = Galaxy.Rnd.Next(0, 1000);
                if (num4 > 1000 - num3)
                {
                    Habitat habitat = _Galaxy.FindNearestInfectableColonyWithNoPlague(Xpos, Ypos);
                    if (habitat != null)
                    {
                        double num5 = _Galaxy.CalculateDistance(Xpos, Ypos, habitat.Xpos, habitat.Ypos);
                        double num6 = (double)Galaxy.SectorSize * 0.8;
                        num6 += (double)Galaxy.SectorSize * 2.0 * (Math.Sqrt(num3) / Math.Sqrt(1000.0));
                        if (num5 < num6 && habitat.Population != null && habitat.Empire != null && (habitat.Empire != _Galaxy.IndependentEmpire || plague.SpecialFunctionCode == 1))
                        {
                            habitat.InfectWithPlague(plague, this);
                            if (plague.SpecialFunctionCode == 1)
                            {
                                _Galaxy.AllowGiantKaltorGeneration = true;
                                int num7 = Galaxy.Rnd.Next(10, 16);
                                for (int i = 0; i < num7; i++)
                                {
                                    _Galaxy.GenerateCreatureAtHabitat(CreatureType.Kaltor, habitat, lockLocation: false);
                                }
                            }
                        }
                    }
                }
                if (Population != null)
                {
                    double num8 = num;
                    if (!string.IsNullOrEmpty(plague.ExceptionRaceName))
                    {
                        Race dominantRace2 = Population.DominantRace;
                        if (dominantRace2 != null && dominantRace2.Name == plague.ExceptionRaceName)
                        {
                            num8 = plague.ExceptionMortalityRate;
                        }
                    }
                    double num9 = Population.TotalAmount;
                    double val = num8 * (timePassed / (double)Galaxy.RealSecondsInGalacticYear) * num9;
                    double num10 = 10000000.0;
                    if (num8 > 1.0)
                    {
                        num10 *= num8;
                    }
                    val = Math.Max(num10, val);
                    if (num9 > num10 || plague.CanCompletelyEliminatePopulation)
                    {
                        PopulationList populationList = new PopulationList();
                        for (int j = 0; j < Population.Count; j++)
                        {
                            Population population = Population[j];
                            if (population == null)
                            {
                                continue;
                            }
                            double num11 = num;
                            double num12 = val;
                            if (!string.IsNullOrEmpty(plague.ExceptionRaceName) && population.Race != null && population.Race.Name == plague.ExceptionRaceName)
                            {
                                num11 = plague.ExceptionMortalityRate;
                                num12 = plague.ExceptionMortalityRate * (timePassed / (double)Galaxy.RealSecondsInGalacticYear) * num9;
                            }
                            if (!population.Race.ImmuneToPlagues)
                            {
                                double num13 = (double)population.Amount / num9;
                                double val2 = num12 * num13;
                                double num14 = 5000000.0;
                                if (num11 > 1.0)
                                {
                                    num14 *= num11;
                                }
                                val2 = Math.Max(num14, val2);
                                long num15 = population.Amount - (long)val2;
                                if (!plague.CanCompletelyEliminatePopulation)
                                {
                                    num15 = Math.Max(num15, 1000000L);
                                }
                                population.Amount = num15;
                                if (population.Amount <= 0)
                                {
                                    population.Amount = 0L;
                                    populationList.Add(population);
                                }
                            }
                        }
                        for (int k = 0; k < populationList.Count; k++)
                        {
                            Population.Remove(populationList[k]);
                        }
                        Population.RecalculateTotalAmount();
                        if (Population.TotalAmount <= 0 || Population.Count <= 0)
                        {
                            if (Empire != null)
                            {
                                string title = string.Format(TextResolver.GetText("COLONY wiped out by PLAGUE"), Name, plague.Name);
                                string description = string.Format(TextResolver.GetText("Our colony COLONY has been completely wiped out by PLAGUE"), Name, plague.Name);
                                Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyLost, this, description, new Point((int)Xpos, (int)Ypos), string.Empty, title);
                                Empire.SendNewsBroadcast(EventMessageType.DisasterEvent, this, DisasterEventType.Plague, warStartEnd: false, wonderBegun: false, EmpireMessageType.ColonyLost, plague);
                            }
                            BuiltObjectList builtObjectList = new BuiltObjectList();
                            if (Empire != null && Empire.BuiltObjects != null)
                            {
                                for (int l = 0; l < Empire.BuiltObjects.Count; l++)
                                {
                                    BuiltObject builtObject = Empire.BuiltObjects[l];
                                    if (builtObject != null && builtObject.Role == BuiltObjectRole.Base && builtObject.ParentHabitat == this)
                                    {
                                        builtObjectList.Add(builtObject);
                                    }
                                }
                            }
                            for (int m = 0; m < builtObjectList.Count; m++)
                            {
                                BuiltObject builtObject2 = builtObjectList[m];
                                builtObject2?.InflictDamage(builtObject2, null, 1000000.0, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                            }
                            for (int n = 0; n < BasesAtHabitat.Count; n++)
                            {
                                BuiltObject builtObject3 = BasesAtHabitat[n];
                                builtObject3?.InflictDamage(builtObject3, null, 1000000.0, _Galaxy.CurrentDateTime, _Galaxy, 0f, allowRecursion: false, 0.0, allowArmorInvulnerability: false);
                            }
                            ClearColony(null, sendMessages: true, removeEmpireWhenNoColonies: true);
                            PlagueId = -1;
                            PlagueTimeRemaining = 0f;
                        }
                    }
                }
            }
            float num16 = PlagueTimeRemaining - (float)timePassed;
            if (num16 <= 0f)
            {
                PlagueId = -1;
                PlagueTimeRemaining = 0f;
            }
            else
            {
                PlagueTimeRemaining = num16;
            }
        }

        public void InfectWithPlague(Plague plague, Habitat infectingColony)
        {
            bool flag = false;
            for (int i = 0; i < Population.Count; i++)
            {
                Population population = Population[i];
                if (population != null && population.Race != null && !population.Race.ImmuneToPlagues)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                PlagueId = plague.PlagueId;
                PlagueTimeRemaining = plague.Duration + (float)((Galaxy.Rnd.NextDouble() - 0.5) * ((double)plague.Duration * 0.3));
                Habitat habitat = Galaxy.DetermineHabitatSystemStar(this);
                _Galaxy.Systems[habitat].PlagueId = plague.PlagueId;
                string description = plague.Description;
                string title = TextResolver.GetText("Colony Disaster Plague Spreads") + "!";
                if (infectingColony == null)
                {
                    title = TextResolver.GetText("Colony Disaster Plague") + "!";
                }
                string empty = string.Empty;
                empty = ((infectingColony != null) ? string.Format(TextResolver.GetText("Colony Disaster Plague Spreads Description"), plague.Name, infectingColony.Name, Name, habitat.Name, description) : string.Format(TextResolver.GetText("Colony Disaster Plague Description"), plague.Name, Name, habitat.Name, description));
                Empire.SendEventMessageToEmpire(EventMessageType.DisasterEvent, title, empty, DisasterEventType.Plague, this);
                Empire.SendNewsBroadcast(EventMessageType.DisasterEvent, this, DisasterEventType.Plague, warStartEnd: false, wonderBegun: false);
            }
        }

        public double GetPlagueUnhappinessFactor()
        {
            Plague plague = null;
            return GetPlagueUnhappinessFactor(out plague);
        }

        public double GetPlagueUnhappinessFactor(out Plague plague)
        {
            double result = 0.0;
            plague = null;
            if (PlagueId >= 0 && PlagueId < Galaxy.PlaguesStatic.Count)
            {
                plague = Galaxy.PlaguesStatic[PlagueId];
                if (plague != null)
                {
                    double num = plague.MortalityRate;
                    if (!string.IsNullOrEmpty(plague.ExceptionRaceName) && Population != null)
                    {
                        Race dominantRace = Population.DominantRace;
                        if (dominantRace != null && dominantRace.Name == plague.ExceptionRaceName)
                        {
                            num = plague.ExceptionMortalityRate;
                        }
                    }
                    result = num * -20.0;
                }
            }
            return result;
        }

        private void ReviewManufacturedResources()
        {
            if (_Galaxy.ResourceSystem.ColonyManufacturedResources == null || _Galaxy.ResourceSystem.ColonyManufacturedResources.Count <= 0)
            {
                return;
            }
            int num = 0;
            if (Owner != null && Population != null)
            {
                int num2 = (int)(Population.TotalAmount / 1000000000);
                num = DevelopmentLevel * num2;
                if (Resources.Count < 8)
                {
                    int num3 = Math.Max(1, _Galaxy.ResourceSystem.ColonyManufacturedResources.Count / 2);
                    int num4 = Resources.CountColonyManufacturedResources();
                    if (num4 < num3)
                    {
                        ResourceDefinitionList resourceDefinitionList = new ResourceDefinitionList();
                        for (int i = 0; i < _Galaxy.ResourceSystem.ColonyManufacturedResources.Count; i++)
                        {
                            ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.ColonyManufacturedResources[i];
                            if (!Resources.ContainsId(resourceDefinition.ResourceID) && num >= resourceDefinition.ColonyManufacturingLevel)
                            {
                                ResourcePrevalanceList byPlanetOrMoonType = resourceDefinition.Prevalence.GetByPlanetOrMoonType(Type);
                                if (byPlanetOrMoonType != null && byPlanetOrMoonType.Count > 0)
                                {
                                    resourceDefinitionList.Add(resourceDefinition);
                                }
                            }
                        }
                        if (resourceDefinitionList.Count > 0)
                        {
                            int index = Galaxy.Rnd.Next(0, resourceDefinitionList.Count);
                            ResourceDefinition resourceDefinition2 = resourceDefinitionList[index];
                            if (resourceDefinition2 != null)
                            {
                                ResourcePrevalanceList byPlanetOrMoonType2 = resourceDefinition2.Prevalence.GetByPlanetOrMoonType(Type);
                                if (byPlanetOrMoonType2 != null && byPlanetOrMoonType2.Count > 0)
                                {
                                    for (int j = 0; j < byPlanetOrMoonType2.Count; j++)
                                    {
                                        ResourcePrevalence resourcePrevalence = byPlanetOrMoonType2[j];
                                        if (resourcePrevalence == null)
                                        {
                                            continue;
                                        }
                                        float num5 = (float)Galaxy.Rnd.NextDouble();
                                        if (!(resourcePrevalence.Prevalence >= num5))
                                        {
                                            continue;
                                        }
                                        int num6 = Resources.IndexOf(resourceDefinition2.ResourceID, 0);
                                        if (num6 < 0)
                                        {
                                            int abundance = Galaxy.Rnd.Next((int)(resourcePrevalence.AbundanceMinimum * 1000f), (int)(resourcePrevalence.AbundanceMaximum * 1000f));
                                            Resources.Add(new HabitatResource(resourceDefinition2.ResourceID, abundance));
                                            Habitat habitat = Galaxy.DetermineHabitatSystemStar(this);
                                            string title = string.Format(TextResolver.GetText("Colony Manufactured Resource Appearance"), resourceDefinition2.Name, Name);
                                            string message = string.Format(TextResolver.GetText("Colony Manufactured Resource Appearance Description"), resourceDefinition2.Name, Name, habitat.Name);
                                            if (Owner != null)
                                            {
                                                Owner.SendEventMessageToEmpire(EventMessageType.ResourceAppearance, title, message, resourceDefinition2, this);
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (Category != HabitatCategoryType.Planet && Category != HabitatCategoryType.Moon)
            {
                return;
            }
            HabitatResourceList colonyManufacturedResources = Resources.GetColonyManufacturedResources();
            if (colonyManufacturedResources.Count <= 0)
            {
                return;
            }
            for (int k = 0; k < colonyManufacturedResources.Count; k++)
            {
                HabitatResource habitatResource = colonyManufacturedResources[k];
                if (habitatResource == null)
                {
                    continue;
                }
                int num7 = (int)((double)habitatResource.ColonyManufacturingLevel * 0.67);
                if (num >= num7)
                {
                    continue;
                }
                int num8 = Resources.IndexOf(habitatResource.ResourceID, 0);
                if (num8 >= 0)
                {
                    Resources.RemoveAt(num8);
                    Habitat habitat2 = Galaxy.DetermineHabitatSystemStar(this);
                    string title2 = string.Format(TextResolver.GetText("Colony Manufactured Resource Stops"), habitatResource.Name, Name);
                    string message2 = string.Format(TextResolver.GetText("Colony Manufactured Resource Stops Description"), habitatResource.Name, Name, habitat2.Name);
                    if (Owner != null)
                    {
                        Owner.SendEventMessageToEmpire(EventMessageType.ResourceDepletion, title2, message2, habitatResource, this);
                    }
                }
            }
        }

        private void ReviewWhetherRefuellingDepot()
        {
            if (Population != null && Population.Count > 0 && Cargo != null)
            {
                IsRefuellingDepot = true;
            }
            else
            {
                IsRefuellingDepot = false;
            }
        }

        public void CheckAddFacilityTracking(PlanetaryFacility facility)
        {
            if (facility != null && facility.Type == PlanetaryFacilityType.Wonder && facility.WonderType == WonderType.RaceAchievement && Empire != null)
            {
                if (Empire.TrackedWonders == null)
                {
                    Empire.TrackedWonders = new PlanetaryFacilityBuildDateList();
                }
                Empire.TrackedWonders.AddUpdateBuildDate(this, facility.PlanetaryFacilityDefinitionId, _Galaxy.CurrentStarDate);
            }
        }

        public void CheckRemoveFacilityTracking(PlanetaryFacility facility)
        {
            if (facility != null && Empire != null && Empire.TrackedWonders != null)
            {
                Empire.TrackedWonders.RemoveBuildDate(this, facility.PlanetaryFacilityDefinitionId);
            }
        }

        private void ConstructFacilities(double timePassed)
        {
            if (Facilities == null || Facilities.Count <= 0)
            {
                return;
            }
            double val = 100000.0 * ((double)Galaxy.RealSecondsInGalacticYear / (double)StrategicValue);
            val = Math.Max(90.0, Math.Min(1800.0, val));
            float num = (float)(timePassed / val);
            float num2 = (float)(timePassed / (val * 10.0));
            bool flag = false;
            if (Empire != null && Empire.Leader != null)
            {
                float num3 = (float)(1.0 + (double)Empire.Leader.FacilityConstructionSpeed / 100.0);
                num *= num3;
                num2 *= num3;
            }
            if (Characters != null && Characters.Count > 0)
            {
                int highestSkillLevelExcludeLeaders = Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.FacilityConstructionSpeed);
                float num4 = (float)(1.0 + (double)highestSkillLevelExcludeLeaders / 100.0);
                num *= num4;
                num2 *= num4;
            }
            for (int i = 0; i < Facilities.Count; i++)
            {
                PlanetaryFacility planetaryFacility = Facilities[i];
                if (planetaryFacility == null || !(planetaryFacility.ConstructionProgress < 1f))
                {
                    continue;
                }
                float num5 = 1f;
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                    case PlanetaryFacilityType.PirateFortress:
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        {
                            PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
                            if (byFacilityControl != null)
                            {
                                Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
                                num5 = (float)Galaxy.CalculatePlanetaryFacilityBuildTimeFactor(planetaryFacility, empireById);
                            }
                            break;
                        }
                    default:
                        if (Empire != null)
                        {
                            num5 = (float)Galaxy.CalculatePlanetaryFacilityBuildTimeFactor(planetaryFacility, Empire);
                        }
                        break;
                }
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.Wonder:
                        planetaryFacility.ConstructionProgress += num2 * num5;
                        break;
                    case PlanetaryFacilityType.PirateBase:
                        planetaryFacility.ConstructionProgress += num * 1f * num5;
                        break;
                    case PlanetaryFacilityType.PirateFortress:
                        planetaryFacility.ConstructionProgress += num * 0.5f * num5;
                        break;
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        planetaryFacility.ConstructionProgress += num * 0.25f * num5;
                        break;
                    default:
                        planetaryFacility.ConstructionProgress += num * num5;
                        break;
                }
                if (planetaryFacility.ConstructionProgress > 1f)
                {
                    _Galaxy.CheckTriggerEvent(GameEventId, Empire, EventTriggerType.Build, planetaryFacility);
                    num = planetaryFacility.ConstructionProgress - 1f;
                    planetaryFacility.ConstructionProgress = 1f;
                    flag = true;
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(this);
                    Empire empire = Empire;
                    PirateColonyControl byFacilityControl2 = _PirateColonyControl.GetByFacilityControl();
                    if ((planetaryFacility.Type == PlanetaryFacilityType.PirateBase || planetaryFacility.Type == PlanetaryFacilityType.PirateFortress || planetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork) && byFacilityControl2 != null)
                    {
                        empire = _Galaxy.PirateEmpires.GetByEmpireId(byFacilityControl2.EmpireId);
                    }
                    if (empire != null)
                    {
                        if (planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                        {
                            string title = string.Format(TextResolver.GetText("Wonder Build Title"), planetaryFacility.Name) + "!";
                            string arg = _Galaxy.ResolveWonderDescription(Galaxy.PlanetaryFacilityDefinitionsStatic[planetaryFacility.PlanetaryFacilityDefinitionId]);
                            string message = string.Format(TextResolver.GetText("Wonder Build Description"), planetaryFacility.Name, Name, arg);
                            empire.SendEventMessageToEmpire(EventMessageType.WonderBuilt, title, message, planetaryFacility, this);
                            empire.SendNewsBroadcast(EventMessageType.WonderBuilt, planetaryFacility, DisasterEventType.Undefined, warStartEnd: false, wonderBegun: false, this);
                        }
                        else if (empire.PirateEmpireBaseHabitat != null)
                        {
                            if (planetaryFacility.Type == PlanetaryFacilityType.PirateCriminalNetwork)
                            {
                                if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                                {
                                    string description = string.Format(TextResolver.GetText("Colony Lost to Pirate Criminal Network"), Name, habitat.Name, empire.Name);
                                    Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyLost, this, description);
                                }
                                empire.TakeOwnershipOfColony(this, empire);
                            }
                            string title2 = string.Format(TextResolver.GetText("Pirate Facility Build Title"), planetaryFacility.Name) + "!";
                            string arg2 = _Galaxy.ResolvePirateFacilityDescription(Galaxy.PlanetaryFacilityDefinitionsStatic[planetaryFacility.PlanetaryFacilityDefinitionId]);
                            string message2 = string.Format(TextResolver.GetText("Pirate Facility Build Description"), planetaryFacility.Name, Name, arg2);
                            empire.SendEventMessageToEmpire(EventMessageType.WonderBuilt, title2, message2, planetaryFacility, this);
                        }
                        else
                        {
                            string description2 = string.Format(TextResolver.GetText("We have completed construction of a new FACILITY"), planetaryFacility.Name, Name, habitat.Name);
                            empire.SendMessageToEmpire(empire, EmpireMessageType.ColonyFacilityCompleted, this, description2);
                        }
                    }
                    if (planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == WonderType.RaceAchievement && planetaryFacility.Value2 == 1)
                    {
                        Race race = _Galaxy.Races["Mechanoid"];
                        if (race != null)
                        {
                            long num6 = 15000000000L + Galaxy.Rnd.Next(0, 3000000) * 1000;
                            _MaxPopulation = Math.Max(_MaxPopulation, Population.TotalAmount + num6);
                            Population population = new Population(race, num6);
                            Population.Add(population);
                            Name = "Utopia";
                        }
                    }
                    if (planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                    {
                        _Galaxy.CheckCancelWonderBuilding(planetaryFacility);
                        _Galaxy.ReviewWondersBuilt();
                        if (Empire != null && Empire.DiplomaticRelations != null)
                        {
                            for (int j = 0; j < Empire.DiplomaticRelations.Count; j++)
                            {
                                DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[j];
                                if (diplomaticRelation == null || diplomaticRelation.Type == DiplomaticRelationType.NotMet || diplomaticRelation.OtherEmpire == Empire)
                                {
                                    continue;
                                }
                                Empire otherEmpire = diplomaticRelation.OtherEmpire;
                                if (otherEmpire != null)
                                {
                                    PlanetaryFacilityDefinition byId = Galaxy.PlanetaryFacilityDefinitionsStatic.GetById(planetaryFacility.PlanetaryFacilityDefinitionId);
                                    if (otherEmpire.CheckSystemExplored(habitat))
                                    {
                                        string description3 = string.Format(TextResolver.GetText("EMPIRE has completed construction of a new WONDER at COLONY SYSTEM"), Empire.Name, planetaryFacility.Name, Name, habitat.Name);
                                        otherEmpire.SendMessageToEmpire(otherEmpire, EmpireMessageType.ColonyFacilityCompleted, byId, description3, new Point((int)Xpos, (int)Ypos), string.Empty);
                                    }
                                    else
                                    {
                                        string description4 = string.Format(TextResolver.GetText("EMPIRE has completed construction of a new WONDER"), Empire.Name, planetaryFacility.Name);
                                        otherEmpire.SendMessageToEmpire(otherEmpire, EmpireMessageType.ColonyFacilityCompleted, byId, description4);
                                    }
                                }
                            }
                        }
                        if (Empire.TrackedWonders == null)
                        {
                            Empire.TrackedWonders = new PlanetaryFacilityBuildDateList();
                        }
                        Empire.TrackedWonders.AddUpdateBuildDate(this, planetaryFacility.PlanetaryFacilityDefinitionId, _Galaxy.CurrentStarDate);
                        _Galaxy.DoCharacterEvent(CharacterEventType.BuildWonder, planetaryFacility, Characters, includeLeader: true, Empire);
                    }
                    else
                    {
                        _Galaxy.DoCharacterEvent(CharacterEventType.BuildFacility, planetaryFacility, Characters, includeLeader: true, Empire);
                    }
                }
                else
                {
                    num = 0f;
                }
                if (num <= 0f)
                {
                    break;
                }
            }
            if (flag && Empire != null)
            {
                ReviewPlanetaryFacilities(Empire);
                Empire.RefreshColonyFacilityInfo();
            }
        }

        private bool CheckIonCannonReadyToFire(DateTime time)
        {
            double totalMilliseconds = time.Subtract(GiantIonCannon.LastFired).TotalMilliseconds;
            if (totalMilliseconds >= (double)GiantIonCannon.FireRate)
            {
                return true;
            }
            return false;
        }

        private bool CheckTargetInRange(StellarObject target, out double distanceToTarget)
        {
            distanceToTarget = _Galaxy.CalculateDistance(Xpos, Ypos, target.Xpos, target.Ypos);
            if (distanceToTarget <= (double)GiantIonCannon.Range)
            {
                return true;
            }
            return false;
        }

        private bool CheckIonCannonCanFireAtTarget(StellarObject target, DateTime time, out double distanceToTarget)
        {
            distanceToTarget = 0.0;
            if (CheckIonCannonReadyToFire(time) && CheckTargetInRange(target, out distanceToTarget))
            {
                return true;
            }
            return false;
        }

        public void TargetInvadingShips(BuiltObject invader, DateTime time)
        {
            if (GiantIonCannonPresent && GiantIonCannon != null)
            {
                double distanceToTarget = 0.0;
                if (CheckIonCannonCanFireAtTarget(invader, time, out distanceToTarget))
                {
                    FireWeaponsAtTarget(distanceToTarget, invader, time);
                }
            }
        }

        private void HandleWeaponsFiring(double timePassed, DateTime time, Galaxy galaxy)
        {
            if (GiantIonCannon == null)
            {
                return;
            }
            Weapon giantIonCannon = GiantIonCannon;
            if (giantIonCannon.ResetNext)
            {
                giantIonCannon.Reset();
            }
            else if (giantIonCannon.Target == null && giantIonCannon.TargetWeapon == null)
            {
                giantIonCannon.ResetNext = true;
            }
            else if (giantIonCannon.Target != null && giantIonCannon.Target.HasBeenDestroyed)
            {
                giantIonCannon.ResetNext = true;
            }
            else if (giantIonCannon.TargetWeapon != null && giantIonCannon.TargetWeapon.DistanceTravelled <= 0f)
            {
                giantIonCannon.ResetNext = true;
            }
            else
            {
                if (!(giantIonCannon.DistanceTravelled >= 0f))
                {
                    return;
                }
                float val = (float)((double)_tempNow.Subtract(giantIonCannon.LastFired).Ticks / 10000000.0);
                val = Math.Min(val, (float)timePassed);
                bool flag = false;
                ComponentType type = giantIonCannon.Component.Type;
                if (type != ComponentType.WeaponIonCannon)
                {
                    return;
                }
                float num;
                if (giantIonCannon.DistanceTravelled <= 1f)
                {
                    flag = true;
                    num = 2f;
                }
                else
                {
                    num = (float)giantIonCannon.Speed * val;
                }
                giantIonCannon.DistanceTravelled += num;
                float distanceFromTarget = giantIonCannon.DistanceFromTarget;
                giantIonCannon.X += Math.Cos(giantIonCannon.Heading) * (double)num;
                giantIonCannon.Y += Math.Sin(giantIonCannon.Heading) * (double)num;
                giantIonCannon.DistanceFromTarget = (float)galaxy.CalculateDistance(giantIonCannon.X, giantIonCannon.Y, giantIonCannon.Target.Xpos, giantIonCannon.Target.Ypos);
                giantIonCannon.Power = (float)giantIonCannon.RawDamage - giantIonCannon.DistanceTravelled / 100f * (float)giantIonCannon.DamageLoss;
                if (giantIonCannon.WillHitTarget && !flag)
                {
                    bool flag2 = false;
                    if (distanceFromTarget < giantIonCannon.DistanceFromTarget)
                    {
                        flag2 = true;
                    }
                    if (flag2)
                    {
                        if (giantIonCannon.Component.Type == ComponentType.WeaponIonCannon)
                        {
                            if (giantIonCannon.Target != null && giantIonCannon.Target.HasBeenDestroyed)
                            {
                                giantIonCannon.Target = null;
                            }
                            else
                            {
                                InflictIonDamage(giantIonCannon.Target, giantIonCannon.Power, time, galaxy, giantIonCannon.Heading);
                            }
                        }
                        giantIonCannon.ResetNext = true;
                    }
                }
                if (giantIonCannon.DistanceTravelled > (float)giantIonCannon.Range)
                {
                    giantIonCannon.ResetNext = true;
                }
            }
        }

        private double InflictIonDamage(StellarObject target, double hitPower, DateTime time, Galaxy galaxy, double strikeAngle)
        {
            if (target is Creature)
            {
                Creature creature = (Creature)target;
                if (creature.DamageCreature(this, (int)hitPower, GiantIonCannon))
                {
                    _Galaxy.CheckTriggerEvent(creature.GameEventId, Empire, EventTriggerType.Destroy, null);
                    if (creature.Type == CreatureType.SilverMist && Empire != null)
                    {
                        Empire.CivilityRating += Galaxy.DestroySilverMistReputationBonus;
                    }
                    creature.CompleteTeardown();
                }
            }
            else if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                if (builtObject.DisabledComponentIndexes == null)
                {
                    builtObject.DisabledComponentIndexes = new List<short>();
                }
                if (builtObject.DisabledComponentDurations == null)
                {
                    builtObject.DisabledComponentDurations = new List<short>();
                }
                if (builtObject.IonDefense > 0)
                {
                    hitPower -= (double)builtObject.IonDefense;
                }
                if (hitPower > 0.0)
                {
                    for (int i = 0; i < builtObject.Components.Count; i++)
                    {
                        if (hitPower <= 0.0)
                        {
                            return hitPower;
                        }
                        switch (builtObject.Components[i].Category)
                        {
                            case ComponentCategoryType.WeaponBeam:
                            case ComponentCategoryType.WeaponTorpedo:
                            case ComponentCategoryType.WeaponArea:
                            case ComponentCategoryType.WeaponPointDefense:
                            case ComponentCategoryType.WeaponIon:
                            case ComponentCategoryType.WeaponGravity:
                            case ComponentCategoryType.Engine:
                            case ComponentCategoryType.WeaponSuperBeam:
                            case ComponentCategoryType.WeaponSuperArea:
                            case ComponentCategoryType.WeaponSuperTorpedo:
                                if (builtObject.Components[i].Status == ComponentStatus.Normal && !builtObject.DisabledComponentIndexes.Contains((short)i))
                                {
                                    builtObject.DisabledComponentIndexes.Add((short)i);
                                    builtObject.DisabledComponentDurations.Add((short)Galaxy.Rnd.Next(20000, 30000));
                                    builtObject.ReDefine();
                                    builtObject.LastIonStrike = time;
                                    hitPower -= 10.0;
                                }
                                break;
                        }
                    }
                }
            }
            return hitPower;
        }

        private void FireWeaponsAtTarget(double distanceToTarget, StellarObject target, DateTime time)
        {
            if (GiantIonCannonPresent && GiantIonCannon != null && !target.HasBeenDestroyed && GiantIonCannon.DistanceTravelled < 0f && (double)GiantIonCannon.Range >= distanceToTarget)
            {
                TimeSpan timeSpan = _tempNow.Subtract(GiantIonCannon.LastFired);
                double num = Galaxy.Rnd.NextDouble() * 500.0 - 250.0;
                if (timeSpan.TotalMilliseconds >= (double)GiantIonCannon.FireRate + num)
                {
                    double hitRangeChance = 0.0;
                    bool willHit = DetermineHitTarget(_Galaxy, GiantIonCannon, target, distanceToTarget, out hitRangeChance);
                    GiantIonCannon.Fire(_Galaxy, this, target, distanceToTarget, time, willHit, hitRangeChance);
                }
            }
        }

        private bool DetermineHitTarget(Galaxy galaxy, Weapon weapon, StellarObject target, double distanceToTarget, out double hitRangeChance)
        {
            hitRangeChance = Math.Max(0.0, ((double)weapon.Range - distanceToTarget) / (double)weapon.Range);
            double val = 20.0 / Math.Max(1.0, target.CurrentSpeed);
            val = Math.Max(0.5, Math.Min(val, 5.0));
            if (target is Fighter)
            {
                val = ((weapon.Component.Type != ComponentType.WeaponPointDefense) ? (val / 1.3) : (val * 1.1));
            }
            double num = 0.0;
            if (target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)target;
                num = builtObject.CountermeasureModifier + builtObject.FleetCountermeasureBonus;
            }
            else if (target is Fighter)
            {
                Fighter fighter = (Fighter)target;
                num = fighter.Specification.CountermeasureModifier;
                if (fighter.ParentBuiltObject != null && !fighter.ParentBuiltObject.HasBeenDestroyed)
                {
                    num += (double)fighter.ParentBuiltObject.FleetCountermeasureBonus;
                }
            }
            double num2 = num / 100.0;
            double num3 = val * (hitRangeChance + Galaxy.Rnd.NextDouble() + num2);
            if (num3 > 0.5)
            {
                return true;
            }
            return false;
        }

        private void CheckForShipsNoLongerDocking()
        {
            if (DockingBayWaitQueue == null || DockingBayWaitQueue.Count <= 0)
            {
                return;
            }
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int i = 0; i < DockingBayWaitQueue.Count; i++)
            {
                BuiltObject builtObject = DockingBayWaitQueue[i];
                if (builtObject.ParentHabitat != this)
                {
                    builtObjectList.Add(builtObject);
                }
            }
            for (int j = 0; j < builtObjectList.Count; j++)
            {
                DockingBayWaitQueue.Remove(builtObjectList[j]);
            }
        }

        private void RegenerateDamage(double timePassed)
        {
            if (Damage > 0f)
            {
                double num = Galaxy.HabitatDamageAnnualRegeneration * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
                if (Population != null && Population.Count > 0 && Population.TotalAmount > 0)
                {
                    double num2 = Math.Min(1.0, (double)Population.TotalAmount / 1000000000.0);
                    double val = 1.0 + num2 * 3.0;
                    val = Math.Min(3.0, Math.Max(val, 1.0));
                    num *= val;
                }
                Damage -= (float)num;
                Damage = Math.Max(0f, Damage);
                RecalculateQuality();
                RecalculateMaximumPopulation();
            }
        }

        private void CheckForShipsDiscoveringRuins()
        {
            if (Ruin == null || (!_Galaxy.CheckRuinsHaveBenefit(Ruin, null) && Ruin.StoryClueLevel < 0 && Ruin.PlayerEmpireEncountered))
            {
                return;
            }
            BuiltObjectList builtObjectsAtLocation = _Galaxy.GetBuiltObjectsAtLocation(Xpos, Ypos, 500);
            for (int i = 0; i < builtObjectsAtLocation.Count; i++)
            {
                BuiltObject builtObject = builtObjectsAtLocation[i];
                if (builtObject == null || builtObject.Empire == null || builtObject.Empire == _Galaxy.IndependentEmpire || builtObject.Empire.PirateEmpireBaseHabitat != null)
                {
                    continue;
                }
                double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
                if (!(num < 250000.0))
                {
                    continue;
                }
                bool flag = false;
                bool flag2 = false;
                BaconBuiltObject.AddScientificData(builtObject, this, "scientificData");
                if (builtObject.Empire == _Galaxy.PlayerEmpire)
                {
                    flag2 = true;
                    if (Ruin.PlayerEmpireEncountered)
                    {
                        flag = true;
                    }
                    else
                    {
                        Ruin.PlayerEmpireEncountered = true;
                    }
                }
                if (flag)
                {
                    continue;
                }
                bool flag3 = _Galaxy.CheckRuinsHaveBenefit(Ruin, builtObject.Empire);
                if (flag2)
                {
                    if (_Galaxy.PlayerEmpire.DiscoveryActionRuin > 0)
                    {
                        if (flag3)
                        {
                            _Galaxy.InvestigateRuins(builtObject.Empire, this);
                        }
                        continue;
                    }
                    Habitat habitat = Galaxy.DetermineHabitatSystemStar(this);
                    string text = string.Format(TextResolver.GetText("We have discovered ancient ruins from a lost civilization"), Ruin.Name, Galaxy.ResolveDescription(Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(Category).ToLower(CultureInfo.InvariantCulture), Name, habitat.Name);
                    text += "\n\n";
                    if (!string.IsNullOrEmpty(Ruin.Description))
                    {
                        text += Ruin.Description;
                        text += "\n\n";
                    }
                    text += TextResolver.GetText("Should we investigate the ruins?");
                    builtObject.Empire.SendEventMessageToEmpire(EventMessageType.EncounterRuins, Ruin.Name, text, this, this);
                }
                else if (flag3 && builtObject.Empire != null && !builtObject.Empire.Reclusive)
                {
                    _Galaxy.InvestigateRuins(builtObject.Empire, this);
                }
            }
        }

        private void ScanForNewOwner()
        {
            if (Population == null || Population.Count <= 0 || Population.TotalAmount <= 0 || Empire != null)
            {
                return;
            }
            BuiltObject builtObject = _Galaxy.FindNearestBuiltObject((int)Xpos, (int)Ypos, BuiltObjectRole.Undefined, includeIndependentBuiltObjects: false);
            if (builtObject == null || builtObject.Empire == null || builtObject.Empire.PirateEmpireBaseHabitat != null || builtObject.Empire == _Galaxy.IndependentEmpire || builtObject.Empire.Reclusive)
            {
                return;
            }
            double num = _Galaxy.CalculateDistanceSquared(Xpos, Ypos, builtObject.Xpos, builtObject.Ypos);
            if (num < 250000.0)
            {
                CheckForShipsDiscoveringRuins();
                builtObject.Empire.TakeOwnershipOfColony(this, builtObject.Empire);
                string text = string.Empty;
                if (builtObject.NearestSystemStar != null)
                {
                    text = builtObject.NearestSystemStar.Name;
                }
                string text2 = string.Format(TextResolver.GetText("We have discovered a lost colony of RACE"), Population.DominantRace.Name, Galaxy.ResolveDescription(Type).ToLower(CultureInfo.InvariantCulture), Galaxy.ResolveDescription(Category).ToLower(CultureInfo.InvariantCulture), Name, text);
                text2 = text2 + " " + TextResolver.GetText("The inhabitants have welcomed us and we have claimed the colony for our empire.");
                string text3 = TextResolver.GetText("Lost Colony Found");
                builtObject.Empire.SendEventMessageToEmpire(EventMessageType.LostColonyFound, text3, text2, this, this);
            }
        }

        private void ClearTroopsAwaitingPickup()
        {
            if (Troops == null || Troops.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < Troops.Count; i++)
            {
                Troop troop = Troops[i];
                if (troop.AwaitingPickup)
                {
                    troop.AwaitingPickup = false;
                }
            }
        }

        private void CheckForShipsOfNewEmpiresInSystem(Galaxy galaxy, DateTime time)
        {
            if (Owner != null && Owner != galaxy.IndependentEmpire)
            {
                PerformThreatEvaluation(time);
            }
        }

        private void PerformThreatEvaluation(DateTime time)
        {
            if (Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                DateTime dateTime = time.Subtract(new TimeSpan(0, 0, 5));
                if (Empire.SystemVisibility[SystemIndex].LatestThreatEvaluation < dateTime)
                {
                    Habitat systemStar = Galaxy.DetermineHabitatSystemStar(this);
                    List<int> threatLevels = new List<int>();
                    BuiltObjectList threats = _Galaxy.EvaluateSystemThreats(systemStar, Empire, out threatLevels);
                    Empire.SystemVisibility[SystemIndex].Threats = threats;
                    Empire.SystemVisibility[SystemIndex].ThreatLevels = threatLevels;
                    Empire.SystemVisibility[SystemIndex].LatestThreatEvaluation = time;
                }
            }
        }

        private void AttackEnemyTargets(DateTime time)
        {
            if (!GiantIonCannonPresent || GiantIonCannon == null || !CheckIonCannonReadyToFire(time))
            {
                return;
            }
            if (Empire != null && Empire.SystemVisibility != null)
            {
                BuiltObjectList threats = Empire.SystemVisibility[SystemIndex].Threats;
                if (threats != null)
                {
                    for (int i = 0; i < threats.Count; i++)
                    {
                        BuiltObject builtObject = threats[i];
                        if (builtObject != null && ShouldAttack(builtObject))
                        {
                            double distanceToTarget = 0.0;
                            if (CheckTargetInRange(builtObject, out distanceToTarget))
                            {
                                FireWeaponsAtTarget(distanceToTarget, builtObject, time);
                                break;
                            }
                        }
                    }
                }
            }
            CreatureList creatures = _Galaxy.Systems[SystemIndex].Creatures;
            if (creatures == null || creatures.Count <= 0)
            {
                return;
            }
            for (int j = 0; j < creatures.Count; j++)
            {
                Creature creature = creatures[j];
                if (creature != null)
                {
                    double distanceToTarget2 = 0.0;
                    if (CheckTargetInRange(creature, out distanceToTarget2))
                    {
                        FireWeaponsAtTarget(distanceToTarget2, creature, time);
                        break;
                    }
                }
            }
        }

        private bool ShouldAttack(BuiltObject target)
        {
            if (Empire == null || Population == null)
            {
                return false;
            }
            if (Empire == target.Empire || target.Empire == null)
            {
                return false;
            }
            if (target.Empire.PirateEmpireBaseHabitat != null && target.Empire.ObtainPirateRelation(Empire).Type == PirateRelationType.Protection)
            {
                return false;
            }
            if (Empire.Outlaws.Contains(target))
            {
                if (Empire == target.Empire)
                {
                    Empire.Outlaws.Remove(target);
                    return false;
                }
                return true;
            }
            if (Attackers != null && Attackers.Contains(target))
            {
                return true;
            }
            if (target.Empire.PirateEmpireBaseHabitat != null)
            {
                return true;
            }
            DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[target.Empire];
            if (diplomaticRelation != null)
            {
                if (diplomaticRelation.Type == DiplomaticRelationType.War)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CheckForSpacePortFacilities(Galaxy galaxy)
        {
            HasSpacePort = false;
            if (Population.TotalAmount > 0)
            {
                bool hasSpacePort = false;
                double happinessModifier = 0.0;
                galaxy.DetermineColonyBaseInfo(this, out hasSpacePort, out happinessModifier);
                HasSpacePort = hasSpacePort;
                HappinessModifier = (float)happinessModifier;
            }
            else
            {
                HappinessModifier = 0f;
            }
            if (BasesAtHabitat == null || BasesAtHabitat.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < BasesAtHabitat.Count; i++)
            {
                BuiltObject builtObject = BasesAtHabitat[i];
                if (builtObject != null && !builtObject.HasBeenDestroyed && (builtObject.SubRole == BuiltObjectSubRole.SmallSpacePort || builtObject.SubRole == BuiltObjectSubRole.MediumSpacePort || builtObject.SubRole == BuiltObjectSubRole.LargeSpacePort))
                {
                    HasSpacePort = true;
                }
            }
        }

        private void ConsumeResources(double timePassed)
        {
            if (Population == null || Population.Count <= 0 || Empire == null || Empire == _Galaxy.IndependentEmpire)
            {
                return;
            }
            ResourceList resourceList = DetermineCriticalResources();
            int num = (int)(Galaxy.ColonyAnnualLuxuryResourceConsumptionRate * (double)Population.TotalAmount * timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            if (num < 1)
            {
                num = 1;
            }
            int num2 = (int)(Galaxy.ColonyAnnualRestrictedResourceConsumptionRate * (double)Population.TotalAmount * timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            if (num2 < 1)
            {
                num2 = 1;
            }
            CargoList cargoList = new CargoList();
            if (Cargo == null)
            {
                return;
            }
            for (int i = 0; i < Cargo.Count; i++)
            {
                Cargo cargo = Cargo[i];
                if (cargo.EmpireId != Empire.EmpireId || cargo.CommodityResource == null || cargo.Available <= 0)
                {
                    continue;
                }
                Resource commodityResource = cargo.CommodityResource;
                if (!commodityResource.IsLuxuryResource && !resourceList.Contains(commodityResource))
                {
                    continue;
                }
                if (commodityResource.IsRestrictedResource)
                {
                    cargo.Amount -= num2;
                }
                else
                {
                    cargo.Amount -= num;
                }
                if (cargo.Amount <= 0)
                {
                    if (cargo.Reserved <= 0)
                    {
                        cargoList.Add(cargo);
                    }
                    else
                    {
                        cargo.Amount = 0;
                    }
                }
            }
            foreach (Cargo item in cargoList)
            {
                Cargo.Remove(item);
            }
        }

        private void LimitResourceCargoAmount(Resource resource, Empire empire)
        {
            int num = Cargo.IndexOf(resource, empire);
            if (num >= 0 && Cargo[num].Amount > Galaxy.ColonyResourceLimit)
            {
                Cargo[num].Amount = Galaxy.ColonyResourceLimit;
            }
        }

        private void ExtractResources(double timePassed)
        {
            if (Cargo == null)
            {
                return;
            }
            HabitatResourceList habitatResourceList = Resources.Clone();
            double num = 1.0;
            if (Owner != null && Owner != _Galaxy.IndependentEmpire)
            {
                num = 1.0 + Owner.ResourceExtractionBonus;
            }
            if (Empire != null && Empire.Leader != null)
            {
                num *= 1.0 + (double)Empire.Leader.MiningRate / 100.0;
            }
            if (Characters != null && Characters.Count > 0)
            {
                int highestSkillLevelExcludeLeaders = Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.MiningRate);
                num *= 1.0 + (double)highestSkillLevelExcludeLeaders / 100.0;
            }
            double num2 = Math.Min(9.0, Math.Max(3.0, (double)Population.TotalAmount / 100000000.0));
            num2 *= num;
            if (Owner == null || Owner == _Galaxy.IndependentEmpire)
            {
                num2 *= 1.0;
            }
            if (Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                num2 *= Empire.MiningRate;
            }
            int num3 = Math.Max(1, (int)(num2 * timePassed));
            if (Population.TotalAmount <= 0)
            {
                num3 = 0;
            }
            if (num3 <= 0)
            {
                return;
            }
            Empire empire = Empire;
            if (empire == null)
            {
                empire = _Galaxy.IndependentEmpire;
            }
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                HabitatResource habitatResource = habitatResourceList[i];
                if (habitatResource != null && habitatResource.Group == ResourceGroup.Luxury)
                {
                    Resource resource = new Resource(habitatResource.ResourceID);
                    int num4 = habitatResource.Extract(num3);
                    empire.Counters.MiningExtractionLuxury += num4;
                    Cargo cargo = new Cargo(resource, num4, empire);
                    Cargo.Add(cargo);
                }
            }
            for (int j = 0; j < habitatResourceList.Count; j++)
            {
                HabitatResource habitatResource2 = habitatResourceList[j];
                if (habitatResource2 != null && habitatResource2.Group == ResourceGroup.Gas)
                {
                    Resource resource2 = new Resource(habitatResource2.ResourceID);
                    int num5 = habitatResource2.Extract(num3);
                    empire.Counters.MiningExtractionGas += num5;
                    Cargo cargo = new Cargo(resource2, num5, empire);
                    Cargo.Add(cargo);
                }
            }
            for (int k = 0; k < habitatResourceList.Count; k++)
            {
                HabitatResource habitatResource3 = habitatResourceList[k];
                if (habitatResource3 != null && habitatResource3.Group == ResourceGroup.Mineral)
                {
                    Resource resource3 = new Resource(habitatResource3.ResourceID);
                    int num6 = habitatResource3.Extract(num3);
                    empire.Counters.MiningExtractionStrategic += num6;
                    if (resource3.ColonyManufacturingLevel > 0)
                    {
                        empire.Counters.MiningExtractionColonyManufactured += num6;
                    }
                    Cargo cargo = new Cargo(resource3, num6, empire);
                    Cargo.Add(cargo);
                }
            }
        }

        private void IndependentColoniesRecruitAndTrainTroops(double timePassed)
        {
            if (Empire != _Galaxy.IndependentEmpire || Population == null)
            {
                return;
            }
            if (Troops == null)
            {
                Troops = new TroopList();
            }
            if (TroopsToRecruit == null)
            {
                TroopsToRecruit = new TroopList();
            }
            if (InvadingTroops == null)
            {
                InvadingTroops = new TroopList();
            }
            if (Characters == null)
            {
                Characters = new CharacterList();
            }
            if (InvadingCharacters == null)
            {
                InvadingCharacters = new CharacterList();
            }
            int num = (int)((double)TroopLevelRequired / 1.0);
            int num2 = Troops.TotalDefendStrength / 100;
            num2 += TroopsToRecruit.Count * 100;
            if (num2 < num)
            {
                Race dominantRace = Population.DominantRace;
                float num3 = dominantRace.TroopStrength;
                if (Ruin != null)
                {
                    num3 = (int)((double)num3 * (1.0 + Ruin.BonusDefensive));
                }
                Troop troop = Galaxy.GenerateNewTroop(string.Format(TextResolver.GetText("RACENAME Militia"), dominantRace), TroopType.Infantry, (int)num3, _Galaxy.IndependentEmpire, dominantRace);
                troop.Readiness = 0f;
                troop.Colony = this;
                TroopsToRecruit.Add(troop);
            }
            double num4 = (double)Galaxy.TroopAnnualRecruitmentAmount * (timePassed / (double)Galaxy.RealSecondsInGalacticYear);
            if (Ruin != null)
            {
                num4 *= 1.0 + Ruin.BonusDefensive;
            }
            if (TroopsToRecruit.Count <= 0)
            {
                return;
            }
            int iterationCount = 0;
            while (Galaxy.ConditionCheckLimit(num4 > 0.0 && TroopsToRecruit.Count > 0, 50, ref iterationCount))
            {
                Troop troop2 = TroopsToRecruit[0];
                troop2.Readiness += (float)num4;
                num4 = 0.0;
                if (troop2.Readiness >= 100f)
                {
                    num4 = troop2.Readiness - 100f;
                    troop2.Readiness = 100f;
                    troop2.Colony = this;
                    Troops.Add(troop2);
                    TroopsToRecruit.RemoveAt(0);
                }
            }
        }

        private void CheckForReinforcements(int invasionForceStrength, int defendingForceStrength)
        {
            if (Empire == null || Empire == _Galaxy.IndependentEmpire || defendingForceStrength >= (int)((double)invasionForceStrength * 1.5))
            {
                return;
            }
            for (int i = 0; i < Empire.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = Empire.BuiltObjects[i];
                if (builtObject.TroopCapacity > 0 && builtObject.Troops != null && builtObject.Troops.Count > 0 && builtObject.TopSpeed > 0 && builtObject.Mission != null && builtObject.Mission.Type == BuiltObjectMissionType.UnloadTroops && builtObject.Mission.TargetHabitat == this)
                {
                    defendingForceStrength += builtObject.Troops.TotalDefendStrength;
                }
            }
            if (defendingForceStrength >= (int)((double)invasionForceStrength * 1.5))
            {
                return;
            }
            double num = (double)Galaxy.SectorSize * (double)Galaxy.SectorSize;
            for (int j = 0; j < Empire.BuiltObjects.Count; j++)
            {
                BuiltObject builtObject2 = Empire.BuiltObjects[j];
                if (builtObject2.TroopCapacity <= 0 || builtObject2.Troops == null || builtObject2.Troops.Count <= 0 || builtObject2.Troops.TotalDefendStrength <= 0 || builtObject2.TopSpeed <= 0 || builtObject2.WarpSpeed <= 0 || !builtObject2.IsAutoControlled || (builtObject2.Mission != null && builtObject2.Mission.Type != 0 && builtObject2.Mission.Priority != BuiltObjectMissionPriority.Low && builtObject2.Mission.Priority != BuiltObjectMissionPriority.Normal && (builtObject2.Mission.Type != BuiltObjectMissionType.Move || builtObject2.Mission.TargetHabitat != this) && (builtObject2.Mission.Type != BuiltObjectMissionType.Attack || builtObject2.Mission.TargetHabitat != null)))
                {
                    continue;
                }
                double num2 = _Galaxy.CalculateDistanceSquared(builtObject2.Xpos, builtObject2.Ypos, Xpos, Ypos);
                if (!(num2 < num))
                {
                    continue;
                }
                if (builtObject2.ShipGroup != null && builtObject2.ShipGroup.LeadShip != null)
                {
                    if (builtObject2.ShipGroup.LeadShip.IsAutoControlled && (builtObject2.ShipGroup.Mission == null || builtObject2.ShipGroup.Mission.Type == BuiltObjectMissionType.Undefined || builtObject2.ShipGroup.Mission.Priority == BuiltObjectMissionPriority.Low))
                    {
                        num2 = _Galaxy.CalculateDistanceSquared(builtObject2.ShipGroup.LeadShip.Xpos, builtObject2.ShipGroup.LeadShip.Ypos, Xpos, Ypos);
                        if (num2 < num && builtObject2.Empire != null)
                        {
                            builtObject2.Empire.AssignFleetUnloadTroops(builtObject2.ShipGroup, this, manuallyAssigned: false);
                        }
                    }
                }
                else
                {
                    builtObject2.ClearPreviousMissionRequirements();
                    builtObject2.AssignMission(BuiltObjectMissionType.UnloadTroops, this, null, builtObject2.Troops, BuiltObjectMissionPriority.High);
                }
            }
        }

        private void InvadingSpecialForcesAttack(double timePassed, Galaxy galaxy)
        {
            if (InvadingTroops == null || InvadingTroops.Count <= 0)
            {
                return;
            }
            TroopList byType = InvadingTroops.GetByType(TroopType.SpecialForces);
            if (byType == null || byType.Count <= 0)
            {
                return;
            }
            Empire invader = null;
            Empire defender = null;
            ResolveInvasionEmpires(out defender, out invader);
            if (defender == null)
            {
                defender = Empire;
            }
            bool flag = false;
            if (invader == Empire && defender.PirateEmpireBaseHabitat != null)
            {
                flag = true;
            }
            if (flag)
            {
                PlanetaryFacility facility = null;
                PlanetaryFacility facility2 = null;
                PlanetaryFacility facility3 = null;
                if (Facilities != null)
                {
                    facility = Facilities.FindByType(PlanetaryFacilityType.PirateBase);
                    facility2 = Facilities.FindByType(PlanetaryFacilityType.PirateFortress);
                    facility3 = Facilities.FindByType(PlanetaryFacilityType.PirateCriminalNetwork);
                }
                TroopList planetaryDefenseUnits = new TroopList();
                if (Troops != null)
                {
                    planetaryDefenseUnits = Troops.GetByType(TroopType.Artillery);
                }
                for (int i = 0; i < byType.Count; i++)
                {
                    Troop troop = byType[i];
                    if (troop != null && !AttemptToDestroyFacility(ref facility, troop, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility2, troop, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility3, troop, timePassed, galaxy))
                    {
                        AttemptToDamagePlanetaryDefenseUnits(planetaryDefenseUnits, troop, timePassed, galaxy);
                    }
                }
            }
            else
            {
                PlanetaryFacility facility4 = null;
                PlanetaryFacility facility5 = null;
                PlanetaryFacility facility6 = null;
                PlanetaryFacility facility7 = null;
                PlanetaryFacility facility8 = null;
                PlanetaryFacility facility9 = null;
                PlanetaryFacility facility10 = null;
                PlanetaryFacility facility11 = null;
                if (Facilities != null)
                {
                    facility4 = Facilities.FindByType(PlanetaryFacilityType.FortifiedBunker);
                    facility5 = Facilities.FindByType(PlanetaryFacilityType.PlanetaryShield);
                    facility6 = Facilities.FindByType(PlanetaryFacilityType.IonCannon);
                    facility7 = Facilities.FindByType(PlanetaryFacilityType.ArmoredFactory);
                    facility8 = Facilities.FindByType(PlanetaryFacilityType.CloningFacility);
                    facility9 = Facilities.FindByType(PlanetaryFacilityType.RoboticTroopFoundry);
                    facility10 = Facilities.FindByType(PlanetaryFacilityType.TroopTrainingCenter);
                    facility11 = Facilities.FindByType(PlanetaryFacilityType.MilitaryAcademy);
                }
                TroopList planetaryDefenseUnits2 = new TroopList();
                if (Troops != null)
                {
                    planetaryDefenseUnits2 = Troops.GetByType(TroopType.Artillery);
                }
                for (int j = 0; j < byType.Count; j++)
                {
                    Troop troop2 = byType[j];
                    if (troop2 != null && !AttemptToDestroyFacility(ref facility4, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility5, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility6, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility7, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility8, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility9, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility10, troop2, timePassed, galaxy) && !AttemptToDestroyFacility(ref facility11, troop2, timePassed, galaxy))
                    {
                        AttemptToDamagePlanetaryDefenseUnits(planetaryDefenseUnits2, troop2, timePassed, galaxy);
                    }
                }
            }
            TroopList byType2 = Troops.GetByType(TroopType.SpecialForces);
            if (byType2 == null || byType2.Count <= 0)
            {
                return;
            }
            int num = CalculateForceStrength(byType2, Characters, defending: false);
            int num2 = CalculateForceStrength(byType, InvadingCharacters, defending: true);
            double num3 = Math.Min(2.0, Math.Max(0.5, (double)num2 / ((double)num + 1.0)));
            double num4 = Math.Min(2.0, Math.Max(0.5, (double)num / ((double)num2 + 1.0)));
            double num5 = Math.Max(1.5, Math.Sqrt(((double)num + (double)num2) / 10000.0) / 2.0);
            double num6 = 0.8 + Galaxy.Rnd.NextDouble() * 0.4;
            double num7 = num6 * num3 * num5 * timePassed;
            double num8 = 0.8 + Galaxy.Rnd.NextDouble() * 0.4;
            double num9 = num8 * num4 * num5 * timePassed;
            double num10 = (double)num - num7;
            double num11 = (double)num2 - num9;
            if (num10 > num11)
            {
                double num12 = num10 / num11;
                double val = (double)num * 0.9;
                if (num11 <= 0.0)
                {
                    num7 += num11;
                    num7 = Math.Min(val, Math.Max(0.0, num7));
                }
                else if (num12 >= 10.0)
                {
                    num7 -= num11;
                    num7 = Math.Min(val, Math.Max(0.0, num7));
                }
            }
            else if (num10 <= 0.0)
            {
                num9 += num10;
                num9 = Math.Max(0.0, num9);
            }
            InflictTroopLosses(defender, defender, num7, byType, byType2, galaxy, specialForcesEvadeBetter: false);
            InflictTroopLosses(defender, invader, num9, byType2, byType, galaxy, specialForcesEvadeBetter: false);
        }

        private bool AttemptToDamagePlanetaryDefenseUnits(TroopList planetaryDefenseUnits, Troop specialForcesTroop, double timePassed, Galaxy galaxy)
        {
            if (specialForcesTroop != null && planetaryDefenseUnits != null && planetaryDefenseUnits.Count > 0)
            {
                double num = 0.8 + Galaxy.Rnd.NextDouble() * 0.4;
                double num2 = Math.Max(0.5, Math.Sqrt(specialForcesTroop.OverallAttackStrength / 10000.0));
                float num3 = (float)(num2 * timePassed * num);
                int index = Galaxy.Rnd.Next(0, planetaryDefenseUnits.Count);
                Troop troop = planetaryDefenseUnits[index];
                if (troop != null)
                {
                    if (troop.Readiness > num3)
                    {
                        troop.Readiness -= num3;
                        if (ColonyInvasion != null)
                        {
                            ColonyInvasion.AddExplosion(troop, isLarge: false, specialForcesTroop);
                        }
                    }
                    else
                    {
                        if (specialForcesTroop.Empire != null && specialForcesTroop.Empire.Counters != null)
                        {
                            specialForcesTroop.Empire.Counters.ProcessTroopDestruction(troop);
                        }
                        if (ColonyInvasion != null)
                        {
                            ColonyInvasion.AddExplosion(troop, isLarge: true, specialForcesTroop);
                        }
                        if (troop.Empire != null && troop.Empire.Troops != null)
                        {
                            troop.Empire.Troops.Remove(troop);
                        }
                        Troops.Remove(troop);
                    }
                    return true;
                }
            }
            return false;
        }

        private bool AttemptToDestroyFacility(ref PlanetaryFacility facility, Troop specialForcesTroop, double timePassed, Galaxy galaxy)
        {
            if (specialForcesTroop != null && facility != null)
            {
                double num = specialForcesTroop.OverallAttackStrength * timePassed / 10000.0;
                if (num > Galaxy.Rnd.NextDouble() * 20.0)
                {
                    bool flag = true;
                    if (Empire == specialForcesTroop.Empire && (facility.Type == PlanetaryFacilityType.PirateBase || facility.Type == PlanetaryFacilityType.PirateFortress || facility.Type == PlanetaryFacilityType.PirateCriminalNetwork))
                    {
                        flag = false;
                    }
                    if (!flag)
                    {
                        string description = string.Format(TextResolver.GetText("Special Forces Destroy Facility Description"), facility.Name, specialForcesTroop.Name, Name);
                        Empire.SendMessageToEmpire(Empire, EmpireMessageType.PlanetaryFacilityDestroyed, facility, description);
                    }
                    int num2 = Galaxy.Rnd.Next(5, 11);
                    specialForcesTroop.SetAttackStrength(specialForcesTroop.AttackStrength + num2);
                    Facilities.Remove(facility);
                    CheckRemoveFacilityTracking(facility);
                    ReviewPlanetaryFacilities(Empire);
                    facility = null;
                    if (ColonyInvasion != null)
                    {
                        ColonyInvasion.AddExplosion(facility, isLarge: true, specialForcesTroop);
                    }
                    return true;
                }
            }
            return false;
        }

        public void ResolveInvasionEmpires(out Empire defender, out Empire invader)
        {
            defender = null;
            Empire empire = null;
            if (Troops != null && Troops.Count > 0)
            {
                for (int i = 0; i < Troops.Count; i++)
                {
                    Troop troop = Troops[i];
                    if (troop != null)
                    {
                        TroopType type = troop.Type;
                        if (type == TroopType.PirateRaider)
                        {
                            empire = troop.Empire;
                        }
                        else
                        {
                            defender = troop.Empire;
                        }
                    }
                    if (defender != null)
                    {
                        break;
                    }
                }
            }
            if (defender == null && empire == null && Characters != null && Characters.Count > 0 && Characters[0] != null)
            {
                defender = Characters[0].Empire;
            }
            invader = null;
            Empire empire2 = null;
            if (InvadingTroops != null && InvadingTroops.Count > 0)
            {
                for (int j = 0; j < InvadingTroops.Count; j++)
                {
                    Troop troop2 = InvadingTroops[j];
                    if (troop2 != null)
                    {
                        TroopType type2 = troop2.Type;
                        if (type2 == TroopType.PirateRaider)
                        {
                            empire2 = troop2.Empire;
                        }
                        else
                        {
                            invader = troop2.Empire;
                        }
                    }
                    if (invader != null)
                    {
                        break;
                    }
                }
            }
            if (invader == null && empire2 == null && InvadingCharacters != null && InvadingCharacters.Count > 0 && InvadingCharacters[0] != null)
            {
                invader = InvadingCharacters[0].Empire;
            }
            if (defender == null && empire != null)
            {
                defender = empire;
            }
            if (invader == null && empire2 != null)
            {
                invader = empire2;
            }
        }

        public bool CheckCanInitiateAttackAgainstPirateFacilities(Empire attackingEmpire, PlanetaryFacility pirateFacility)
        {
            if (Empire == attackingEmpire && Troops != null && Troops.Count > 0 && InvadingTroops != null && InvadingTroops.Count <= 0)
            {
                Empire empire = CheckFacilityOwner(pirateFacility);
                if (empire != null && empire.DominantRace != null && empire != attackingEmpire)
                {
                    return true;
                }
            }
            return false;
        }

        public void InitiateAttackAgainstPirateFacilities(Empire attackingEmpire, PlanetaryFacility pirateFacility)
        {
            if (CheckCanInitiateAttackAgainstPirateFacilities(attackingEmpire, pirateFacility))
            {
                Empire empire = CheckFacilityOwner(pirateFacility);
                GenerateDefensivePirateRaiders(empire, currentDefendingTroopsInvade: true);
                if (InvasionStats == null)
                {
                    InvasionStats = new InvasionStats(this, attackingEmpire, empire);
                }
            }
        }

        public void PiratesDefendAgainstRaid(Empire raidingEmpire)
        {
            if (_PirateColonyControl == null || _PirateColonyControl.Count <= 0)
            {
                return;
            }
            PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
            if (byFacilityControl == null)
            {
                return;
            }
            Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
            if (empireById == null || empireById == raidingEmpire)
            {
                return;
            }
            bool flag = false;
            if (Troops != null && Troops.Count > 0)
            {
                Troop[] array = ListHelper.ToArrayThreadSafe(Troops);
                foreach (Troop troop in array)
                {
                    if (troop != null && troop.Type == TroopType.PirateRaider)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                GenerateDefensivePirateRaiders(empireById, currentDefendingTroopsInvade: false);
            }
        }

        private void GenerateDefensivePirateRaiders(Empire defendingPirateFaction, bool currentDefendingTroopsInvade)
        {
            BaconHabitat.GenerateDefensivePirateRaiders(this, defendingPirateFaction, currentDefendingTroopsInvade);
        }

        public void ResolveInvasionBattles(TimeSpan timePassed, Galaxy galaxy)
        {
            if ((InvadingTroops == null || InvadingTroops.Count <= 0) && (InvadingCharacters == null || InvadingCharacters.Count <= 0))
            {
                return;
            }
            Empire invader = null;
            Empire defender = null;
            ResolveInvasionEmpires(out defender, out invader);
            if (defender == null)
            {
                defender = Empire;
            }
            if (InvasionStats == null)
            {
                InvasionStats = new InvasionStats(this, invader, Empire);
            }
            bool flag = false;
            if (InvadingTroops.Count > 0 && InvadingTroops[0].Type == TroopType.PirateRaider)
            {
                flag = true;
                for (int i = 0; i < InvadingTroops.Count; i++)
                {
                    Troop troop = InvadingTroops[i];
                    if (troop != null && troop.Type != TroopType.PirateRaider)
                    {
                        flag = false;
                        break;
                    }
                }
            }
            InvadingSpecialForcesAttack(timePassed.TotalSeconds, galaxy);
            int attackingStrength = 0;
            int defendingStrength = 0;
            CalculateForceStrengths(defender, invader, Troops, Characters, InvadingTroops, InvadingCharacters, out defendingStrength, out attackingStrength);
            int num = 0;
            bool isDefending = false;
            if (Population != null)
            {
                num = CalculatePopulationStrength(out isDefending, invader, defender);
            }
            bool flag2 = false;
            bool flag3 = false;
            if (_Rebelling && InvadingTroops != null)
            {
                for (int j = 0; j < InvadingTroops.Count; j++)
                {
                    if (InvadingTroops != null && (InvadingTroops[j].Empire == _Galaxy.IndependentEmpire || InvadingTroops[j].Empire == null))
                    {
                        flag2 = true;
                        break;
                    }
                }
            }
            if (invader == Empire && defender.PirateEmpireBaseHabitat != null)
            {
                flag3 = true;
            }
            if (flag2 || flag3 || !isDefending)
            {
                if (!flag)
                {
                    attackingStrength += num;
                }
            }
            else if (!flag && Troops.Count > 0 && Troops.TotalDefendStrength > 0)
            {
                defendingStrength += num;
            }
            if (!flag3)
            {
                CheckForReinforcements(attackingStrength, defendingStrength);
            }
            new Random((int)DateTime.Now.Ticks);
            double num2 = Math.Min(2.0, Math.Max(0.5, (double)defendingStrength / ((double)attackingStrength + 1.0)));
            double num3 = Math.Min(2.0, Math.Max(0.5, (double)attackingStrength / ((double)defendingStrength + 1.0)));
            double num4 = Math.Max(0.5, Math.Sqrt(((double)attackingStrength + (double)defendingStrength) / 10000.0) / 2.0);
            double num5 = 0.8 + Galaxy.Rnd.NextDouble() * 0.4;
            double num6 = num5 * num2 * num4 * timePassed.TotalSeconds;
            double num7 = 0.8 + Galaxy.Rnd.NextDouble() * 0.4;
            double num8 = num7 * num3 * num4 * timePassed.TotalSeconds;
            double num9 = (double)attackingStrength - num6;
            double num10 = (double)defendingStrength - num8;
            if (num9 > num10)
            {
                double num11 = num9 / num10;
                double val = (double)attackingStrength * 0.9;
                if (num10 <= 0.0)
                {
                    num6 += num10;
                    num6 = Math.Min(val, Math.Max(0.0, num6));
                }
                else if (num11 >= 10.0)
                {
                    num6 -= num10;
                    num6 = Math.Min(val, Math.Max(0.0, num6));
                }
            }
            else if (num9 <= 0.0)
            {
                num8 += num9;
                num8 = Math.Max(0.0, num8);
            }
            InflictTroopLosses(defender, defender, num6, InvadingTroops, Troops, galaxy);
            InflictTroopLosses(defender, invader, num8, Troops, InvadingTroops, galaxy);
            if (Population != null)
            {
                double num12 = Math.Max(20000.0, Math.Min(1000000.0, (double)Population.TotalAmount / 4000.0));
                double val2 = Math.Min(10000000.0, (double)Population.TotalAmount / 2000.0);
                val2 = Math.Max(val2, num12);
                double num13 = timePassed.TotalSeconds * (num12 + (val2 - num12) * Galaxy.Rnd.NextDouble());
                if (flag)
                {
                    num13 /= 12.0;
                }
                if (Population.TotalAmount - (long)num13 > 0)
                {
                    double num14 = num13 / (double)Population.Count;
                    foreach (Population item in Population)
                    {
                        if (item == null)
                        {
                            continue;
                        }
                        long num15 = Math.Min(item.Amount / 2, (long)num14);
                        item.Amount -= num15;
                        if (ColonyInvasion != null)
                        {
                            Troop firer = null;
                            if (InvadingTroops != null && InvadingTroops.Count > 0)
                            {
                                firer = InvadingTroops[Galaxy.Rnd.Next(0, InvadingTroops.Count)];
                            }
                            ColonyInvasion.AddExplosion(item, isLarge: false, firer);
                        }
                        if (item.Amount < 0)
                        {
                            item.Amount = 0L;
                        }
                    }
                    Population.RecalculateTotalAmount();
                }
            }
            if (InvadingTroops == null || InvadingTroops.TotalAttackStrength <= 0)
            {
                bool flag4 = false;
                if (Troops.Count > 0 && Troops[0].Type == TroopType.PirateRaider)
                {
                    flag4 = true;
                    for (int k = 0; k < Troops.Count; k++)
                    {
                        Troop troop2 = Troops[k];
                        if (troop2 != null && troop2.Type != TroopType.PirateRaider)
                        {
                            flag4 = false;
                            break;
                        }
                    }
                }
                galaxy.InvasionFailures++;
                if (InvadingCharacters != null && InvadingCharacters.Count > 0)
                {
                    Character[] array = ListHelper.ToArrayThreadSafe(InvadingCharacters);
                    for (int l = 0; l < array.Length; l++)
                    {
                        if (array[l] != null)
                        {
                            if (Empire != null && Empire.Counters != null)
                            {
                                Empire.Counters.ProcessCharacterDeath(array[l]);
                            }
                            if (ColonyInvasion != null)
                            {
                                ColonyInvasion.AddExplosion(array[l], isLarge: true, null);
                            }
                            array[l].SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                            array[l].Kill(_Galaxy);
                        }
                    }
                }
                if (flag4)
                {
                    string arg = Galaxy.ResolveDescription(Category) + " " + Name;
                    defender.SendMessageToEmpire(defender, EmpireMessageType.ColonyDefended, this, string.Format(TextResolver.GetText("Our pirate forces have defended our hidden base on X"), arg));
                    _Rebelling = false;
                    if (invader != null)
                    {
                        string description = string.Format(TextResolver.GetText("Our attempted eradication of the EMPIRE pirate faction on COLONY has failed"), Name, defender.Name);
                        invader.SendMessageToEmpire(invader, EmpireMessageType.ColonyDefended, this, description);
                    }
                    if (InvasionStats != null)
                    {
                        InvasionStats.InvasionSucceeded = false;
                        _Galaxy.DoCharacterEvent(CharacterEventType.GroundInvasion, InvasionStats, Characters);
                    }
                    InvasionStats = null;
                    InvadingTroops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                    Troops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                    int num16 = Galaxy.Rnd.Next(6, 15);
                    int val3 = GetDevelopmentLevel() - num16;
                    val3 = Math.Max(0, val3);
                    SetDevelopmentLevel(val3);
                    return;
                }
                string arg2 = Galaxy.ResolveDescription(Category) + " " + Name;
                if (Empire != null)
                {
                    if (flag2)
                    {
                        Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyDefended, this, string.Format(TextResolver.GetText("We have put down a rebellion on X"), arg2));
                        _Rebelling = false;
                    }
                    else if (flag)
                    {
                        Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyDefended, this, string.Format(TextResolver.GetText("We have fended off a raid on X"), arg2));
                    }
                    else
                    {
                        Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyDefended, this, string.Format(TextResolver.GetText("We have fended off an invasion on X"), arg2));
                    }
                }
                if (invader != null)
                {
                    string empty = string.Empty;
                    empty = ((!flag) ? string.Format(TextResolver.GetText("Our attempted invasion of COLONY of EMPIRE has failed"), Name, Empire.Name) : string.Format(TextResolver.GetText("Our attempted raid on COLONY of EMPIRE has failed"), Name, Empire.Name));
                    invader.SendMessageToEmpire(invader, EmpireMessageType.ColonyDefended, this, empty);
                }
                if (InvasionStats != null)
                {
                    InvasionStats.InvasionSucceeded = false;
                    _Galaxy.DoCharacterEvent(CharacterEventType.GroundInvasion, InvasionStats, Characters);
                }
                InvasionStats = null;
                if (Troops != null)
                {
                    float num17 = 15f;
                    if (flag)
                    {
                        num17 = 5f;
                    }
                    if (Empire != null && Empire.Leader != null)
                    {
                        float num18 = (float)(1.0 + (double)Empire.Leader.TroopExperienceGain / 100.0);
                        num17 *= num18;
                    }
                    if (Characters != null && Characters.Count > 0)
                    {
                        int highestSkillLevelExcludeLeaders = Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.TroopExperienceGain);
                        float num19 = (float)(1.0 + (double)highestSkillLevelExcludeLeaders / 100.0);
                        num17 *= num19;
                    }
                    foreach (Troop troop5 in Troops)
                    {
                        if (troop5 != null)
                        {
                            troop5.SetAttackStrength(troop5.AttackStrength + (int)(num17 / 2f));
                            troop5.SetDefendStrength(troop5.DefendStrength + (int)num17);
                        }
                    }
                }
                InvadingTroops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                Troops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                int num20 = Galaxy.Rnd.Next(6, 15);
                int val4 = GetDevelopmentLevel() - num20;
                val4 = Math.Max(0, val4);
                SetDevelopmentLevel(val4);
                _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, this, Characters, includeLeader: true, Empire);
                _Galaxy.ChanceNewTroopGeneralFromInvasion(Empire, this, invading: false);
                return;
            }
            double num21 = (double)attackingStrength / (double)defendingStrength;
            bool flag5 = false;
            int num22 = Troops.CountByType(TroopType.PirateRaider);
            if (num22 > 0 && num22 == Troops.Count)
            {
                flag5 = true;
            }
            if (!flag5 && invader == Empire)
            {
                flag5 = true;
            }
            if (flag)
            {
                double num23 = Galaxy.Rnd.NextDouble() / timePassed.TotalSeconds;
                if (num23 < 0.005 && Facilities != null)
                {
                    PlanetaryFacility planetaryFacility = Facilities.FindBestPirateFacility(includeCriminalNetwork: false);
                    if (planetaryFacility != null && _PirateColonyControl != null)
                    {
                        PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
                        if (byFacilityControl != null)
                        {
                            Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
                            if (empireById != null && empireById != invader)
                            {
                                float num24 = 0.2f * ((float)attackingStrength / 10000f);
                                num24 *= (float)(0.7 + Galaxy.Rnd.NextDouble() * 0.6);
                                switch (planetaryFacility.Type)
                                {
                                    case PlanetaryFacilityType.PirateBase:
                                        num24 *= 1f;
                                        break;
                                    case PlanetaryFacilityType.PirateFortress:
                                        num24 *= 0.7f;
                                        break;
                                    case PlanetaryFacilityType.PirateCriminalNetwork:
                                        num24 *= 0.4f;
                                        break;
                                }
                                if (planetaryFacility.ConstructionProgress < 1f)
                                {
                                    num24 *= 1.5f;
                                }
                                num24 *= (float)empireById.PlanetaryFacilityEliminationFactor;
                                float num25 = planetaryFacility.ConstructionProgress - num24;
                                if (num25 <= 0f)
                                {
                                    Facilities.Remove(planetaryFacility);
                                    CheckRemoveFacilityTracking(planetaryFacility);
                                    PlanetaryFacility planetaryFacility2 = Facilities.FindBestPirateFacility(includeCriminalNetwork: true);
                                    if (planetaryFacility2 == null)
                                    {
                                        byFacilityControl.HasFacilityControl = false;
                                        float num26 = (byFacilityControl.ControlLevel = Math.Min(0.49f, Math.Max(0.01f, byFacilityControl.ControlLevel - 0.2f)));
                                    }
                                    string description2 = string.Format(TextResolver.GetText("Invasion Destroys Facility Description"), planetaryFacility.Name, Name);
                                    empireById.SendMessageToEmpire(empireById, EmpireMessageType.PlanetaryFacilityDestroyed, planetaryFacility, description2);
                                }
                                else
                                {
                                    planetaryFacility.ConstructionProgress = num25;
                                    string description3 = string.Format(TextResolver.GetText("Invasion Damages Facility Description"), planetaryFacility.Name, Name);
                                    empireById.SendMessageToEmpire(empireById, EmpireMessageType.PlanetaryFacilityDamaged, planetaryFacility, description3);
                                }
                            }
                        }
                    }
                }
                double num27 = 0.3;
                num27 = InvadingTroops.Count switch
                {
                    1 => 0.05,
                    2 => 0.1,
                    3 => 0.15,
                    4 => 0.2,
                    _ => 0.3,
                };
                if (InvadingTroops.Count > 1 && num21 < num27)
                {
                    Troop[] array2 = ListHelper.ToArrayThreadSafe(InvadingTroops);
                    foreach (Troop troop3 in array2)
                    {
                        if (troop3 != null && troop3.Type == TroopType.PirateRaider)
                        {
                            InvadingTroops.Remove(troop3);
                            if (troop3.Empire != null && troop3.Empire.Troops != null && troop3.Empire.Troops.Contains(troop3))
                            {
                                troop3.Empire.Troops.Remove(troop3);
                            }
                        }
                    }
                    if (InvadingCharacters != null && InvadingCharacters.Count > 0)
                    {
                        for (int n = 0; n < InvadingCharacters.Count; n++)
                        {
                            Character character = InvadingCharacters[n];
                            if (character != null && character.Empire != null && character.Empire.PirateEmpireBaseHabitat != null && character.Empire == invader)
                            {
                                BuiltObject builtObject = _Galaxy.FindNearestBuiltObject((int)Xpos, (int)Ypos, character.Empire);
                                if (builtObject != null)
                                {
                                    character.CompleteLocationTransfer(builtObject, _Galaxy);
                                }
                            }
                        }
                    }
                    string description4 = string.Format(TextResolver.GetText("Raid Withdrawal Description Invader"), Name);
                    invader.SendMessageToEmpire(invader, EmpireMessageType.ColonyDefended, this, description4);
                    string description5 = string.Format(TextResolver.GetText("Raid Withdrawal Description Defender"), Name, invader.Name);
                    defender.SendMessageToEmpire(defender, EmpireMessageType.ColonyDefended, this, description5);
                    InvadingTroops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                    Troops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                    if (Galaxy.Rnd.Next(0, 3) > 0)
                    {
                        double num28 = 0.0;
                        if (InvasionSpaceControlStrengthAttackers > 0)
                        {
                            num28 = ((InvasionSpaceControlStrengthDefenders > 0) ? Math.Max(0.1, Math.Min(1.0, InvasionSpaceControlStrengthAttackers / InvasionSpaceControlStrengthDefenders)) : 1.0);
                        }
                        if (num28 > 0.0)
                        {
                            double num29 = 0.1 + Galaxy.Rnd.NextDouble() * 0.4 * num28;
                            num29 *= invader.RaidBonusFactor;
                            _Galaxy.DoRaidBonuses(invader, this, num29);
                        }
                    }
                    return;
                }
            }
            double num30 = 20.0;
            if (flag)
            {
                num30 = 10.0;
            }
            if (!(num21 >= num30))
            {
                return;
            }
            galaxy.InvasionSuccesses++;
            Empire empire = null;
            if (InvadingTroops != null && InvadingTroops.Count > 0 && InvadingTroops[0] != null)
            {
                empire = InvadingTroops[0].Empire;
            }
            if (flag)
            {
                Empire empire2 = Empire;
                if (empire2 != null && empire2.PirateMissions != null)
                {
                    EmpireActivity firstByTargetAndTypeAssigned = empire2.PirateMissions.GetFirstByTargetAndTypeAssigned(this, EmpireActivityType.Defend, empire2);
                    if (firstByTargetAndTypeAssigned != null && firstByTargetAndTypeAssigned.AssignedEmpire != null && firstByTargetAndTypeAssigned.BidTimeRemaining == 0)
                    {
                        PirateRelation pirateRelation = empire2.ObtainPirateRelation(firstByTargetAndTypeAssigned.AssignedEmpire);
                        pirateRelation.EvaluationPirateMissionsFail -= 20f;
                        string description6 = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Pirate"), firstByTargetAndTypeAssigned.RequestingEmpire.Name, firstByTargetAndTypeAssigned.Target.Name, firstByTargetAndTypeAssigned.Price.ToString("0"));
                        firstByTargetAndTypeAssigned.AssignedEmpire.SendMessageToEmpire(firstByTargetAndTypeAssigned.AssignedEmpire, EmpireMessageType.PirateDefendMissionFailed, firstByTargetAndTypeAssigned.Target, description6);
                        description6 = string.Format(TextResolver.GetText("Pirate Defend Mission Failed Other"), firstByTargetAndTypeAssigned.AssignedEmpire.Name, firstByTargetAndTypeAssigned.Target.Name, firstByTargetAndTypeAssigned.Price.ToString("0"));
                        firstByTargetAndTypeAssigned.RequestingEmpire.SendMessageToEmpire(firstByTargetAndTypeAssigned.RequestingEmpire, EmpireMessageType.PirateDefendMissionFailed, firstByTargetAndTypeAssigned.Target, description6);
                        firstByTargetAndTypeAssigned.RequestingEmpire.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                        firstByTargetAndTypeAssigned.AssignedEmpire.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                        empire2.PirateMissions.RemoveEquivalent(firstByTargetAndTypeAssigned);
                    }
                }
                double raidBonusFactor = empire.RaidBonusFactor;
                _Galaxy.DoRaidBonuses(empire, this, raidBonusFactor);
                if (InvadingCharacters != null && InvadingCharacters.Count > 0)
                {
                    for (int num31 = 0; num31 < InvadingCharacters.Count; num31++)
                    {
                        Character character2 = InvadingCharacters[num31];
                        if (character2 != null && character2.Empire != null && character2.Empire.BuiltObjects != null)
                        {
                            BuiltObject nearestBuiltObject = character2.Empire.BuiltObjects.GetNearestBuiltObject(Xpos, Ypos, BuiltObjectRole.Military, null);
                            if (nearestBuiltObject != null)
                            {
                                character2.CompleteLocationTransfer(nearestBuiltObject, _Galaxy);
                            }
                        }
                    }
                }
                if (empire != null && empire.PirateEmpireBaseHabitat != null && _PirateColonyControl != null)
                {
                    PirateColonyControl byFaction = _PirateColonyControl.GetByFaction(empire.EmpireId);
                    if (byFaction != null)
                    {
                        float num32 = (float)(0.07 + Galaxy.Rnd.NextDouble() * 0.08);
                        num32 *= (float)empire.RaidBonusFactor;
                        float num33 = (byFaction.ControlLevel = Math.Min(1f, byFaction.ControlLevel + num32));
                    }
                }
                RaidCountdown = 60;
            }
            else
            {
                Troops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                InvadingTroops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
                TroopList troopList = new TroopList();
                if (flag2)
                {
                    if (Troops != null)
                    {
                        if (Empire != null && Empire.Troops != null)
                        {
                            foreach (Troop troop6 in Troops)
                            {
                                if (troop6 != null && (troop6.Empire == _Galaxy.IndependentEmpire || troop6.Empire == null))
                                {
                                    if (troop6.Empire != null && troop6.Empire.Troops != null)
                                    {
                                        troop6.Empire.Troops.Remove(troop6);
                                    }
                                    troopList.Add(troop6);
                                }
                            }
                        }
                        Troops.Clear();
                    }
                }
                else if (Troops != null)
                {
                    if (Empire != null && Empire.Troops != null)
                    {
                        foreach (Troop troop7 in Troops)
                        {
                            if (troop7 != null)
                            {
                                if (empire != null && empire.Counters != null)
                                {
                                    empire.Counters.ProcessTroopDestruction(troop7);
                                }
                                if (troop7.Empire != null && troop7.Empire.Troops != null)
                                {
                                    troop7.Empire.Troops.Remove(troop7);
                                }
                            }
                        }
                    }
                    Troops.Clear();
                }
                if (Characters != null && Characters.Count > 0)
                {
                    Character[] array3 = ListHelper.ToArrayThreadSafe(Characters);
                    foreach (Character character3 in array3)
                    {
                        if (character3 == null)
                        {
                            continue;
                        }
                        CharacterRole role = character3.Role;
                        if (role == CharacterRole.IntelligenceAgent)
                        {
                            if (Empire != null && Empire.Capital != null && this != Empire.Capital)
                            {
                                character3.CompleteLocationTransfer(Empire.Capital, _Galaxy);
                            }
                            else if (character3.Empire.PirateEmpireBaseHabitat != null)
                            {
                                BuiltObject builtObject2 = _Galaxy.IdentifyPirateSpaceport(character3.Empire);
                                if (builtObject2 != null)
                                {
                                    character3.CompleteLocationTransfer(builtObject2, _Galaxy);
                                    continue;
                                }
                                if (empire != null && empire.Counters != null)
                                {
                                    empire.Counters.ProcessCharacterDeath(character3);
                                }
                                if (ColonyInvasion != null)
                                {
                                    ColonyInvasion.AddExplosion(character3, isLarge: true, null);
                                }
                                character3.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                                character3.Kill(_Galaxy);
                            }
                            else
                            {
                                if (empire != null && empire.Counters != null)
                                {
                                    empire.Counters.ProcessCharacterDeath(character3);
                                }
                                if (ColonyInvasion != null)
                                {
                                    ColonyInvasion.AddExplosion(character3, isLarge: true, null);
                                }
                                character3.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                                character3.Kill(_Galaxy);
                            }
                        }
                        else if (character3.Empire != null && character3.Empire != Empire && character3.Empire.BuiltObjects != null)
                        {
                            BuiltObject nearestBuiltObject2 = character3.Empire.BuiltObjects.GetNearestBuiltObject(Xpos, Ypos, BuiltObjectRole.Military, null);
                            if (nearestBuiltObject2 != null)
                            {
                                character3.CompleteLocationTransfer(nearestBuiltObject2, _Galaxy);
                            }
                            else if (character3.Empire.Capital != null)
                            {
                                character3.CompleteLocationTransfer(character3.Empire.Capital, _Galaxy);
                            }
                            else
                            {
                                if (character3.Empire.PirateEmpireBaseHabitat == null)
                                {
                                    continue;
                                }
                                BuiltObject builtObject3 = _Galaxy.IdentifyPirateSpaceport(character3.Empire);
                                if (builtObject3 != null)
                                {
                                    character3.CompleteLocationTransfer(builtObject3, _Galaxy);
                                    continue;
                                }
                                if (empire != null && empire.Counters != null)
                                {
                                    empire.Counters.ProcessCharacterDeath(character3);
                                }
                                if (ColonyInvasion != null)
                                {
                                    ColonyInvasion.AddExplosion(character3, isLarge: true, null);
                                }
                                character3.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                                character3.Kill(_Galaxy);
                            }
                        }
                        else
                        {
                            if (empire != null && empire.Counters != null)
                            {
                                empire.Counters.ProcessCharacterDeath(character3);
                            }
                            if (ColonyInvasion != null)
                            {
                                ColonyInvasion.AddExplosion(character3, isLarge: true, null);
                            }
                            character3.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                            character3.Kill(_Galaxy);
                        }
                    }
                }
                EmpireList empireList = new EmpireList();
                List<double> list = new List<double>();
                if (InvadingTroops != null)
                {
                    float num35 = 25f;
                    if (empire != null && empire.Leader != null)
                    {
                        float num36 = (float)(1.0 + (double)empire.Leader.TroopExperienceGain / 100.0);
                        num35 *= num36;
                    }
                    if (InvadingCharacters != null && InvadingCharacters.Count > 0)
                    {
                        int highestSkillLevelExcludeRole = InvadingCharacters.GetHighestSkillLevelExcludeRole(CharacterSkillType.TroopExperienceGain, CharacterRole.Leader);
                        float num37 = (float)(1.0 + (double)highestSkillLevelExcludeRole / 100.0);
                        num35 *= num37;
                    }
                    TroopList troopList2 = new TroopList();
                    foreach (Troop invadingTroop in InvadingTroops)
                    {
                        if (invadingTroop == null)
                        {
                            continue;
                        }
                        int num38 = empireList.IndexOf(invadingTroop.Empire);
                        if (num38 >= 0)
                        {
                            list[num38] += invadingTroop.OverallAttackStrength;
                        }
                        else
                        {
                            empireList.Add(invadingTroop.Empire);
                            list.Add(invadingTroop.OverallAttackStrength);
                        }
                        invadingTroop.SetAttackStrength(invadingTroop.AttackStrength + (int)num35);
                        invadingTroop.SetDefendStrength(invadingTroop.DefendStrength + (int)(num35 / 2f));
                        if (invadingTroop.Empire != empire)
                        {
                            troopList2.Add(invadingTroop);
                            continue;
                        }
                        if (Troops == null)
                        {
                            Troops = new TroopList();
                        }
                        Troops.Add(invadingTroop);
                    }
                    InvadingTroops.Clear();
                    for (int num39 = 0; num39 < troopList2.Count; num39++)
                    {
                        Troop troop4 = troopList2[num39];
                        if (troop4 != null && troop4.Empire != null && troop4.Empire != Empire && troop4.Empire.BuiltObjects != null)
                        {
                            BuiltObject builtObject4 = troop4.Empire.FindNearestTroopShipWithSpace(Xpos, Ypos, troop4.Size);
                            if (builtObject4 != null && builtObject4.Troops != null)
                            {
                                builtObject4.Troops.Add(troop4);
                                troop4.BuiltObject = builtObject4;
                                continue;
                            }
                            Habitat habitat = _Galaxy.FastFindNearestColony(Xpos, Ypos, troop4.Empire, 0);
                            if (habitat != null && habitat.Troops != null)
                            {
                                troop4.Colony = habitat;
                                habitat.Troops.Add(troop4);
                            }
                            else
                            {
                                troop4.Empire.Troops.Remove(troop4);
                            }
                        }
                        else if (troop4 != null && troop4.Empire != null && troop4.Empire.Troops != null)
                        {
                            troop4.Empire.Troops.Remove(troop4);
                        }
                    }
                }
                Empire empire3 = null;
                if (flag2)
                {
                    empire3 = IdentifyLeavingEmpire();
                }
                else
                {
                    double num40 = 0.0;
                    for (int num41 = 0; num41 < empireList.Count; num41++)
                    {
                        if (list[num41] > num40)
                        {
                            num40 = list[num41];
                            empire3 = empireList[num41];
                        }
                    }
                }
                if (InvadingCharacters != null)
                {
                    CharacterList characterList = new CharacterList();
                    for (int num42 = 0; num42 < InvadingCharacters.Count; num42++)
                    {
                        Character character4 = InvadingCharacters[num42];
                        if (character4 != null)
                        {
                            if (character4.Empire == empire3)
                            {
                                Characters.Add(character4);
                            }
                            else
                            {
                                characterList.Add(character4);
                            }
                        }
                    }
                    InvadingCharacters.Clear();
                    for (int num43 = 0; num43 < characterList.Count; num43++)
                    {
                        Character character5 = characterList[num43];
                        if (character5 != null && character5.Empire != null && character5.Empire != Empire && character5.Empire.BuiltObjects != null)
                        {
                            BuiltObject nearestBuiltObject3 = character5.Empire.BuiltObjects.GetNearestBuiltObject(Xpos, Ypos, BuiltObjectRole.Military, null);
                            if (nearestBuiltObject3 != null)
                            {
                                character5.CompleteLocationTransfer(nearestBuiltObject3, _Galaxy);
                            }
                            else if (character5.Empire.Capital != null)
                            {
                                character5.CompleteLocationTransfer(character5.Empire.Capital, _Galaxy);
                            }
                            else
                            {
                                if (character5.Empire.PirateEmpireBaseHabitat == null)
                                {
                                    continue;
                                }
                                BuiltObject builtObject5 = _Galaxy.IdentifyPirateSpaceport(character5.Empire);
                                if (builtObject5 != null)
                                {
                                    character5.CompleteLocationTransfer(builtObject5, _Galaxy);
                                    continue;
                                }
                                if (empire != null && empire.Counters != null)
                                {
                                    empire.Counters.ProcessCharacterDeath(character5);
                                }
                                if (ColonyInvasion != null)
                                {
                                    ColonyInvasion.AddExplosion(character5, isLarge: true, null);
                                }
                                character5.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                                character5.Kill(_Galaxy);
                            }
                        }
                        else
                        {
                            if (empire != null && empire.Counters != null)
                            {
                                empire.Counters.ProcessCharacterDeath(character5);
                            }
                            if (ColonyInvasion != null)
                            {
                                ColonyInvasion.AddExplosion(character5, isLarge: true, null);
                            }
                            character5.SendDeathMessage(CharacterDeathType.ColonyInvasion, _Galaxy);
                            character5.Kill(_Galaxy);
                        }
                    }
                }
                if (flag5 && Facilities != null)
                {
                    PlanetaryFacilityList planetaryFacilityList = new PlanetaryFacilityList();
                    PlanetaryFacility planetaryFacility3 = null;
                    for (int num44 = 0; num44 < Facilities.Count; num44++)
                    {
                        PlanetaryFacility planetaryFacility4 = Facilities[num44];
                        if (planetaryFacility4 != null && (planetaryFacility4.Type == PlanetaryFacilityType.PirateBase || planetaryFacility4.Type == PlanetaryFacilityType.PirateFortress || planetaryFacility4.Type == PlanetaryFacilityType.PirateCriminalNetwork))
                        {
                            planetaryFacility3 = planetaryFacility4;
                            planetaryFacilityList.Add(planetaryFacility4);
                        }
                    }
                    for (int num45 = 0; num45 < planetaryFacilityList.Count; num45++)
                    {
                        Facilities.Remove(planetaryFacilityList[num45]);
                        CheckRemoveFacilityTracking(planetaryFacilityList[num45]);
                    }
                    if (planetaryFacility3 != null)
                    {
                        PirateColonyControl byFacilityControl2 = _PirateColonyControl.GetByFacilityControl();
                        if (byFacilityControl2 != null)
                        {
                            byFacilityControl2.HasFacilityControl = false;
                            float num46 = (byFacilityControl2.ControlLevel = Math.Min(0.49f, Math.Max(0.01f, byFacilityControl2.ControlLevel - 0.25f)));
                            Empire empireById2 = _Galaxy.GetEmpireById(byFacilityControl2.EmpireId);
                            if (empireById2 != null)
                            {
                                string description7 = string.Format(TextResolver.GetText("Invasion Destroys Facility Description"), planetaryFacility3.Name, Name);
                                empireById2.SendMessageToEmpire(empireById2, EmpireMessageType.PlanetaryFacilityDestroyed, planetaryFacility3, description7);
                            }
                        }
                    }
                }
                if (!flag5)
                {
                    if (Owner != null && Empire != _Galaxy.IndependentEmpire)
                    {
                        Empire.ResolveSystemVisibility(Xpos, Ypos, null, this);
                        if (Owner.Colonies != null)
                        {
                            Owner.Colonies.Remove(this);
                        }
                    }
                    _ = string.Empty;
                    if (Empire != null)
                    {
                        _ = Galaxy.ResolveDescription(Category) + " " + Name;
                        if (flag2)
                        {
                            string description8 = string.Empty;
                            if (empire3 == _Galaxy.IndependentEmpire)
                            {
                                description8 = string.Format(TextResolver.GetText("Our colony at the PLANETTYPE COLONYNAME has revolted and left our empire INDEPENDENT"), Galaxy.ResolveDescription(Category), Name);
                            }
                            else if (empire3 != null)
                            {
                                description8 = string.Format(TextResolver.GetText("Our colony at the PLANETTYPE COLONYNAME has revolted and left our empire JOIN OTHER"), Galaxy.ResolveDescription(Category), Name, empire3.Name);
                            }
                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyLost, this, description8);
                        }
                        else if (empire3 != null && !flag5)
                        {
                            string description9 = string.Format(TextResolver.GetText("The EMPIRE have invaded the PLANETTYPE COLONYNAME"), empire3.Name, Galaxy.ResolveDescription(Category), Name);
                            Empire.SendMessageToEmpire(Empire, EmpireMessageType.ColonyLost, this, description9);
                        }
                    }
                    if (troopList.Count > 0)
                    {
                        for (int num47 = 0; num47 < troopList.Count; num47++)
                        {
                            troopList[num47].Empire = empire3;
                            Troops.Add(troopList[num47]);
                        }
                    }
                    if (empire3 != null)
                    {
                        if (!flag2)
                        {
                            galaxy.InflictWarDamage(empire3, this);
                            empire3.CancelBlockade(this);
                            empire3.CancelAttacks(this);
                            if (Population != null && Population.DominantRace != null && empire3.DominantRace != null)
                            {
                                double num48 = Galaxy.ResolveStandardRaceBias(Population.DominantRace, empire3.DominantRace);
                                float num49 = (ConqueredFactor = (float)Math.Min(0.0, (num48 - 12.0) * 1.5));
                            }
                        }
                        if (empire3.Counters != null)
                        {
                            empire3.Counters.ProcessColonyConquest(this, Empire);
                        }
                        empire3.TakeOwnershipOfColony(this, empire3, flag2, destroyTroops: false);
                        TaxRate = 0f;
                    }
                    _Galaxy.ReviewEmpireTerritory(onlySystems: false);
                    int num50 = Galaxy.Rnd.Next(11, 24);
                    SetDevelopmentLevel(GetDevelopmentLevel() - num50);
                    _Galaxy.DoCharacterEvent(CharacterEventType.ColonyDevelopmentDecrease, this, Characters);
                    if (Facilities != null && Facilities.Count > 0)
                    {
                        PlanetaryFacility planetaryFacility5 = Facilities[Facilities.Count - 1];
                        if (planetaryFacility5 != null && planetaryFacility5.ConstructionProgress < 1f)
                        {
                            if (ColonyInvasion != null)
                            {
                                ColonyInvasion.AddExplosion(planetaryFacility5, isLarge: true, null);
                            }
                            Facilities.Remove(planetaryFacility5);
                            CheckRemoveFacilityTracking(planetaryFacility5);
                            ReviewPlanetaryFacilities(empire3);
                        }
                    }
                }
                if (InvasionStats != null)
                {
                    InvasionStats.InvasionSucceeded = true;
                    _Galaxy.DoCharacterEvent(CharacterEventType.GroundInvasion, InvasionStats, Characters);
                }
                InvasionStats = null;
                _ = Galaxy.ResolveDescription(Category) + " " + Name;
                if (empire3 != null)
                {
                    if (flag2)
                    {
                        string description10 = string.Format(TextResolver.GetText("Colony Left Empire Joined Us"), Galaxy.ResolveDescription(Category), Name);
                        empire3.SendMessageToEmpire(empire3, EmpireMessageType.ColonyGained, this, description10);
                        _Rebelling = false;
                    }
                    else if (flag5)
                    {
                        if (invader != null && defender != null)
                        {
                            string description11 = string.Format(TextResolver.GetText("Pirate Removed From Colony Description"), defender.Name, Name);
                            invader.SendMessageToEmpire(invader, EmpireMessageType.ColonyGained, this, description11);
                        }
                    }
                    else
                    {
                        string description12 = string.Format(TextResolver.GetText("Invaded Colony Taken Over"), Galaxy.ResolveDescription(Category), Name);
                        empire3.SendMessageToEmpire(empire3, EmpireMessageType.ColonyGained, this, description12);
                    }
                    if (Population != null && Population.DominantRace != null)
                    {
                        RaceList newAbilityRaces = new RaceList();
                        Race raceChanged = null;
                        List<string> list2 = empire3.ReviewEmpireAbilityBonuses(out newAbilityRaces, out raceChanged);
                        if (list2.Count > 0 && raceChanged != null)
                        {
                            string text = string.Format(TextResolver.GetText("Conquest New Race Ability"), Galaxy.ResolveDescription(Category).ToLower(CultureInfo.InvariantCulture), Name, raceChanged.Name);
                            if (flag2)
                            {
                                text = string.Format(TextResolver.GetText("Conquest New Race Ability Militia"), Galaxy.ResolveDescription(Category).ToLower(CultureInfo.InvariantCulture), Name, raceChanged.Name);
                            }
                            text += "\n";
                            foreach (string item2 in list2)
                            {
                                text = text + "\n" + item2;
                            }
                            string text2 = TextResolver.GetText("New Ability for our Empire");
                            empire3.SendEventMessageToEmpire(EventMessageType.NewEmpireRaceAbility, text2, text, raceChanged, this);
                        }
                    }
                    if (Population != null && Population.TotalAmount > 100000000 && Empire != null && Empire != _Galaxy.IndependentEmpire && Galaxy.Rnd.Next(0, 8) == 1 && empire3.CharactersCanGenerateAmountNonIntelligenceAgent() > 0)
                    {
                        Character character6 = empire3.GenerateNewCharacter(CharacterRole.TroopGeneral, this);
                        if (character6 != null)
                        {
                            string title = string.Format(TextResolver.GetText("New Character Event Title"), Galaxy.ResolveDescription(character6.Role));
                            string description13 = string.Format(TextResolver.GetText("New Character Event Troop General"), Name, character6.Name);
                            empire3.SendMessageToEmpireWithTitle(empire3, EmpireMessageType.CharacterAppearance, character6, description13, title);
                        }
                    }
                }
            }
            InvadingTroops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
            Troops.RemoveTroopsByType(TroopType.PirateRaider, alsoRemoveFromEmpire: true);
        }

        public int CalculatePopulationStrength(out bool isDefending)
        {
            return CalculatePopulationStrength(out isDefending, null, null);
        }

        public int CalculatePopulationStrength(out bool isDefending, Empire invader, Empire defender)
        {
            isDefending = true;
            int result = 0;
            if (Population != null && Population.DominantRace != null)
            {
                result = (int)(Population.TotalAmount / 5000000) * Population.DominantRace.AggressionLevel;
            }
            bool flag = false;
            if (_Rebelling && InvadingTroops != null)
            {
                for (int i = 0; i < InvadingTroops.Count; i++)
                {
                    Troop troop = InvadingTroops[i];
                    if (troop != null && (troop.Empire == _Galaxy.IndependentEmpire || troop.Empire == null))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            bool flag2 = false;
            if (invader != null && invader == Empire && defender != null && defender.PirateEmpireBaseHabitat != null)
            {
                flag2 = true;
            }
            if (flag || flag2)
            {
                isDefending = false;
            }
            return result;
        }

        public void DetermineTroopModifiers(Empire defendingEmpire, Empire attackingEmpire, TroopList defendingTroops, TroopList attackingTroops, CharacterList defendingCharacters, CharacterList attackingCharacters, out List<double> modifierAmountsDefend, out List<string> modifierReasonsDefend, out List<double> modifierAmountsAttack, out List<string> modifierReasonsAttack)
        {
            DetermineDefendBonuses(defendingTroops, defendingCharacters, out modifierAmountsDefend, out modifierReasonsDefend);
            DetermineAttackBonuses(attackingTroops, attackingCharacters, out modifierAmountsAttack, out modifierReasonsAttack);
            double infantryStrength = 0.0;
            double artilleryStrength = 0.0;
            double armorStrength = 0.0;
            double specialForcesStrength = 0.0;
            defendingTroops.GetTroopStrengthsByType(defending: true, out infantryStrength, out artilleryStrength, out armorStrength, out specialForcesStrength);
            _ = defendingTroops.TotalDefendStrength;
            double infantryStrength2 = 0.0;
            double artilleryStrength2 = 0.0;
            double armorStrength2 = 0.0;
            double specialForcesStrength2 = 0.0;
            attackingTroops.GetTroopStrengthsByType(defending: true, out infantryStrength2, out artilleryStrength2, out armorStrength2, out specialForcesStrength2);
            int totalAttackStrength = attackingTroops.TotalAttackStrength;
            if (totalAttackStrength > 0 && armorStrength > 0.0 && armorStrength >= armorStrength2 * 2.0)
            {
                modifierAmountsDefend.Add(0.25);
                modifierReasonsDefend.Add(TextResolver.GetText("Armored Reserve"));
            }
            else if (armorStrength2 > 0.0 && armorStrength2 >= armorStrength * 2.0)
            {
                modifierAmountsAttack.Add(0.25);
                modifierReasonsAttack.Add(TextResolver.GetText("Armored Breakthrough"));
            }
            if (totalAttackStrength > 0 && infantryStrength > 0.0 && infantryStrength >= infantryStrength2 * 2.0)
            {
                modifierAmountsDefend.Add(0.25);
                modifierReasonsDefend.Add(TextResolver.GetText("Fortified Line"));
            }
            else if (infantryStrength2 > 0.0 && infantryStrength2 >= infantryStrength * 2.0)
            {
                modifierAmountsAttack.Add(0.25);
                modifierReasonsAttack.Add(TextResolver.GetText("Overwhelming Odds"));
            }
            if (totalAttackStrength > 0 && artilleryStrength > specialForcesStrength2)
            {
                modifierAmountsDefend.Add(0.25);
                modifierReasonsDefend.Add(TextResolver.GetText("Defense Grid"));
            }
            else if (artilleryStrength2 > specialForcesStrength)
            {
                modifierAmountsAttack.Add(0.25);
                modifierReasonsAttack.Add(TextResolver.GetText("Defense Grid"));
            }
            if (totalAttackStrength > 0 && specialForcesStrength > artilleryStrength2)
            {
                modifierAmountsDefend.Add(0.25);
                modifierReasonsDefend.Add(TextResolver.GetText("Special Operations"));
            }
            else if (specialForcesStrength2 > artilleryStrength)
            {
                modifierAmountsAttack.Add(0.25);
                modifierReasonsAttack.Add(TextResolver.GetText("Special Operations"));
            }
            if (InvasionSpaceControlStrengthDefenders > InvasionSpaceControlStrengthAttackers)
            {
                modifierAmountsDefend.Add(0.25);
                modifierReasonsDefend.Add(TextResolver.GetText("Space Control"));
            }
            else if (InvasionSpaceControlStrengthAttackers > InvasionSpaceControlStrengthDefenders)
            {
                modifierAmountsAttack.Add(0.25);
                modifierReasonsAttack.Add(TextResolver.GetText("Space Control"));
            }
        }

        public void CalculateSpaceControlStrengths(Empire defendingEmpire, Empire attackingEmpire, out int spaceControlStrengthDefenders, out int spaceControlStrengthAttackers)
        {
            BaconHabitat.CalculateSpaceControlStrengths(this, defendingEmpire, defendingEmpire, out spaceControlStrengthDefenders, out spaceControlStrengthAttackers);
        }

        public void CalculateForceStrengths(Empire defender, Empire attacker, TroopList defendingTroops, CharacterList defendingCharacters, TroopList attackingTroops, CharacterList attackingCharacters, out int defendingStrength, out int attackingStrength)
        {
            double totalDefendModifier = 0.0;
            double totalAttackModifier = 0.0;
            CalculateForceStrengths(defender, attacker, defendingTroops, defendingCharacters, attackingTroops, attackingCharacters, out defendingStrength, out attackingStrength, out totalDefendModifier, out totalAttackModifier, out var _, out var _, out var _, out var _);
        }

        public void CalculateForceStrengths(Empire defender, Empire attacker, TroopList defendingTroops, CharacterList defendingCharacters, TroopList attackingTroops, CharacterList attackingCharacters, out int defendingStrength, out int attackingStrength, out double totalDefendModifier, out double totalAttackModifier, out List<double> modifierAmountsDefense, out List<string> modifierReasonsDefense, out List<double> modifierAmountsAttack, out List<string> modifierReasonsAttack)
        {
            BaconHabitat.CalculateForceStrengths(this, defender, attacker, defendingTroops, defendingCharacters, attackingTroops, attackingCharacters, out defendingStrength, out attackingStrength, out totalDefendModifier, out totalAttackModifier, out modifierAmountsDefense, out modifierReasonsDefense, out modifierAmountsAttack, out modifierReasonsAttack);
        }

        public int CalculateForceStrength(TroopList troops, CharacterList characters, bool defending)
        {
            List<double> modifierAmounts;
            List<string> modifierReasons;
            return CalculateForceStrength(troops, characters, defending, out modifierAmounts, out modifierReasons);
        }

        public int CalculateForceStrength(TroopList troops, CharacterList characters, bool defending, out List<double> modifierAmounts, out List<string> modifierReasons)
        {
            modifierAmounts = new List<double>();
            modifierReasons = new List<string>();
            int result = 0;
            if (troops != null)
            {
                if (defending)
                {
                    DetermineDefendBonuses(troops, characters, out modifierAmounts, out modifierReasons);
                }
                else
                {
                    DetermineAttackBonuses(troops, characters, out modifierAmounts, out modifierReasons);
                }
                double num = 1.0;
                for (int i = 0; i < modifierAmounts.Count; i++)
                {
                    num += modifierAmounts[i];
                }
                result = ((!defending) ? troops.TotalAttackStrength : troops.TotalDefendStrength);
                result = (int)((double)result * num);
            }
            return result;
        }

        public double DetermineDefendBonus(out bool fromNativePlanet, out bool fromUncolonizable, out bool fromTroopGenerals, out bool fromDefensiveFacilities)
        {
            double num = 1.0;
            fromNativePlanet = false;
            fromUncolonizable = false;
            fromTroopGenerals = false;
            fromDefensiveFacilities = false;
            if (Troops != null)
            {
                double num2 = 0.0;
                double num3 = 0.0;
                for (int i = 0; i < Troops.Count; i++)
                {
                    Troop troop = Troops[i];
                    num2 += troop.OverallDefendStrength;
                    num3 += troop.OverallDefendStrength;
                    if (troop != null && troop.Race != null && troop.Race.NativeHabitatType == Type)
                    {
                        num3 += troop.OverallDefendStrength * 0.1;
                        fromNativePlanet = true;
                        continue;
                    }
                    bool flag = false;
                    if (troop.Empire != null)
                    {
                        flag = Type switch
                        {
                            HabitatType.Continental => troop.Empire.CanColonizeContinental,
                            HabitatType.MarshySwamp => troop.Empire.CanColonizeMarshySwamp,
                            HabitatType.Desert => troop.Empire.CanColonizeDesert,
                            HabitatType.Ocean => troop.Empire.CanColonizeOcean,
                            HabitatType.Ice => troop.Empire.CanColonizeIce,
                            HabitatType.Volcanic => troop.Empire.CanColonizeVolcanic,
                            _ => false,
                        };
                    }
                    if (troop.Race != null && troop.Race.NativeHabitatType == Type)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        num3 -= troop.OverallDefendStrength * 0.1;
                        fromUncolonizable = true;
                    }
                }
                if (num2 > 0.0 && num3 > 0.0)
                {
                    num = num3 / num2;
                }
                double num4 = 0.0;
                if (Characters != null && Characters.Count > 0)
                {
                    int num5 = -100;
                    for (int j = 0; j < Characters.Count; j++)
                    {
                        Character character = Characters[j];
                        if (character != null && character.Role == CharacterRole.TroopGeneral)
                        {
                            num5 = Math.Max(num5, character.TroopGroundDefense);
                        }
                    }
                    if (num5 <= -100)
                    {
                        num5 = 0;
                    }
                    num4 = (double)(100 + num5) / 100.0;
                }
                if (num4 <= 0.0)
                {
                    num4 = 1.0;
                }
                if (num4 != 1.0)
                {
                    fromTroopGenerals = true;
                }
                num *= num4;
                if (DefensiveFortressBonus > 0)
                {
                    num *= 1.0 + (double)(int)DefensiveFortressBonus / 10.0;
                    fromDefensiveFacilities = true;
                }
            }
            return num - 1.0;
        }

        public void DetermineDefendBonuses(TroopList troops, CharacterList characters, out List<double> modifierAmounts, out List<string> modifierReasons)
        {
            modifierAmounts = new List<double>();
            modifierReasons = new List<string>();
            if (troops == null)
            {
                return;
            }
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            for (int i = 0; i < troops.Count; i++)
            {
                Troop troop = troops[i];
                if (troop == null)
                {
                    continue;
                }
                switch (troop.Type)
                {
                    case TroopType.Infantry:
                        num += troop.OverallDefendStrength;
                        break;
                    case TroopType.Armored:
                        num2 += troop.OverallDefendStrength;
                        break;
                    case TroopType.Artillery:
                        num3 += troop.OverallDefendStrength;
                        break;
                    case TroopType.SpecialForces:
                        num4 += troop.OverallDefendStrength;
                        break;
                }
                num5 += troop.OverallDefendStrength;
                num6 += troop.OverallDefendStrength;
                if (troop != null && troop.Race != null && troop.Race.NativeHabitatType == Type)
                {
                    num6 += troop.OverallDefendStrength * 0.1;
                    continue;
                }
                bool flag = false;
                if (troop.Empire != null)
                {
                    flag = Type switch
                    {
                        HabitatType.Continental => troop.Empire.CanColonizeContinental,
                        HabitatType.MarshySwamp => troop.Empire.CanColonizeMarshySwamp,
                        HabitatType.Desert => troop.Empire.CanColonizeDesert,
                        HabitatType.Ocean => troop.Empire.CanColonizeOcean,
                        HabitatType.Ice => troop.Empire.CanColonizeIce,
                        HabitatType.Volcanic => troop.Empire.CanColonizeVolcanic,
                        _ => false,
                    };
                }
                if (troop.Race != null && troop.Race.NativeHabitatType == Type)
                {
                    flag = true;
                }
                if (!flag)
                {
                    num6 -= troop.OverallDefendStrength * 0.1;
                }
            }
            if (num5 > 0.0 && num6 > 0.0)
            {
                double num7 = num6 / num5 - 1.0;
                if (num7 != 0.0)
                {
                    modifierAmounts.Add(num7);
                    modifierReasons.Add(TextResolver.GetText("Planet Type"));
                }
            }
            double num8 = num + num2 + num3 + num4;
            bool flag2 = false;
            double num9 = 0.0;
            if (characters != null && characters.Count > 0)
            {
                int num10 = -100;
                int num11 = -100;
                int num12 = -100;
                int num13 = -100;
                int num14 = -100;
                for (int j = 0; j < characters.Count; j++)
                {
                    Character character = characters[j];
                    if (character != null && character.Role == CharacterRole.TroopGeneral)
                    {
                        flag2 = true;
                        num10 = Math.Max(num10, character.TroopGroundDefense);
                        num11 = Math.Max(num11, character.TroopStrengthInfantry);
                        num12 = Math.Max(num12, character.TroopStrengthArmor);
                        num13 = Math.Max(num13, character.TroopStrengthPlanetaryDefense);
                        num14 = Math.Max(num14, character.TroopStrengthSpecialForces);
                    }
                }
                if (num10 <= -100)
                {
                    num10 = 0;
                }
                if (num11 <= -100)
                {
                    num11 = 0;
                }
                if (num12 <= -100)
                {
                    num12 = 0;
                }
                if (num13 <= -100)
                {
                    num13 = 0;
                }
                if (num14 <= -100)
                {
                    num14 = 0;
                }
                if (num > 0.0 && num11 != 0)
                {
                    double num15 = num / num8;
                    num9 += num15 * ((double)num11 / 100.0);
                }
                if (num2 > 0.0 && num12 != 0)
                {
                    double num16 = num2 / num8;
                    num9 += num16 * ((double)num12 / 100.0);
                }
                if (num3 > 0.0 && num13 != 0)
                {
                    double num17 = num3 / num8;
                    num9 += num17 * ((double)num13 / 100.0);
                }
                if (num4 > 0.0 && num14 != 0)
                {
                    double num18 = num4 / num8;
                    num9 += num18 * ((double)num14 / 100.0);
                }
                num9 += (double)num10 / 100.0;
            }
            if (flag2 && num9 != 0.0)
            {
                modifierAmounts.Add(num9);
                modifierReasons.Add(TextResolver.GetText("Colony Invasion Bonus Troop General"));
            }
            if (DefensiveFortressBonus > 0)
            {
                modifierAmounts.Add((double)(int)DefensiveFortressBonus / 10.0);
                modifierReasons.Add(TextResolver.GetText("Colony Invasion Bonus Defensive Facilities"));
            }
            if (_PirateColonyControl == null || _PirateColonyControl.Count <= 0 || troops.Count <= 0)
            {
                return;
            }
            Troop troop2 = troops[0];
            if (troop2 == null || troop2.Type != TroopType.PirateRaider || troop2.Empire == null || Facilities == null)
            {
                return;
            }
            PlanetaryFacility planetaryFacility = Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
            if (planetaryFacility == null)
            {
                return;
            }
            PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
            if (byFacilityControl == null)
            {
                return;
            }
            Empire empireById = _Galaxy.GetEmpireById(byFacilityControl.EmpireId);
            if (empireById != null && empireById == troop2.Empire)
            {
                double item = 0.25;
                switch (planetaryFacility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                        item = 0.25;
                        break;
                    case PlanetaryFacilityType.PirateFortress:
                        item = 0.5;
                        break;
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        item = 1.0;
                        break;
                }
                modifierAmounts.Add(item);
                modifierReasons.Add(TextResolver.GetText("Colony Invasion Bonus Pirate Facilities"));
            }
        }

        public void DetermineAttackBonuses(TroopList troops, CharacterList characters, out List<double> modifierAmounts, out List<string> modifierReasons)
        {
            modifierAmounts = new List<double>();
            modifierReasons = new List<string>();
            if (troops == null)
            {
                return;
            }
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            double num5 = 0.0;
            double num6 = 0.0;
            for (int i = 0; i < troops.Count; i++)
            {
                Troop troop = troops[i];
                if (troop == null)
                {
                    continue;
                }
                switch (troop.Type)
                {
                    case TroopType.Infantry:
                        num += troop.OverallAttackStrength;
                        break;
                    case TroopType.Armored:
                        num2 += troop.OverallAttackStrength;
                        break;
                    case TroopType.Artillery:
                        num3 += troop.OverallAttackStrength;
                        break;
                    case TroopType.SpecialForces:
                        num4 += troop.OverallAttackStrength;
                        break;
                }
                num5 += troop.OverallAttackStrength;
                num6 += troop.OverallAttackStrength;
                if (troop != null && troop.Race != null && troop.Race.NativeHabitatType == Type)
                {
                    num6 += troop.OverallAttackStrength * 0.1;
                    continue;
                }
                bool flag = false;
                if (troop.Empire != null)
                {
                    flag = Type switch
                    {
                        HabitatType.Continental => troop.Empire.CanColonizeContinental,
                        HabitatType.MarshySwamp => troop.Empire.CanColonizeMarshySwamp,
                        HabitatType.Desert => troop.Empire.CanColonizeDesert,
                        HabitatType.Ocean => troop.Empire.CanColonizeOcean,
                        HabitatType.Ice => troop.Empire.CanColonizeIce,
                        HabitatType.Volcanic => troop.Empire.CanColonizeVolcanic,
                        _ => false,
                    };
                }
                if (troop.Race != null && troop.Race.NativeHabitatType == Type)
                {
                    flag = true;
                }
                if (!flag)
                {
                    num6 -= troop.OverallAttackStrength * 0.1;
                }
            }
            if (num5 > 0.0 && num6 > 0.0)
            {
                double num7 = num6 / num5 - 1.0;
                if (num7 != 0.0)
                {
                    modifierAmounts.Add(num7);
                    modifierReasons.Add(TextResolver.GetText("Planet Type"));
                }
            }
            double num8 = num + num2 + num3 + num4;
            bool flag2 = false;
            double num9 = 0.0;
            if (characters != null && characters.Count > 0)
            {
                int num10 = -100;
                int num11 = -100;
                int num12 = -100;
                int num13 = -100;
                int num14 = -100;
                for (int j = 0; j < characters.Count; j++)
                {
                    Character character = characters[j];
                    if (character != null && character.Role == CharacterRole.TroopGeneral)
                    {
                        flag2 = true;
                        num10 = Math.Max(num10, character.TroopGroundAttack);
                        num11 = Math.Max(num11, character.TroopStrengthInfantry);
                        num12 = Math.Max(num12, character.TroopStrengthArmor);
                        num13 = Math.Max(num13, character.TroopStrengthPlanetaryDefense);
                        num14 = Math.Max(num14, character.TroopStrengthSpecialForces);
                    }
                }
                if (num10 <= -100)
                {
                    num10 = 0;
                }
                if (num11 <= -100)
                {
                    num11 = 0;
                }
                if (num12 <= -100)
                {
                    num12 = 0;
                }
                if (num13 <= -100)
                {
                    num13 = 0;
                }
                if (num14 <= -100)
                {
                    num14 = 0;
                }
                if (num > 0.0 && num11 != 0)
                {
                    double num15 = num / num8;
                    num9 += num15 * ((double)num11 / 100.0);
                }
                if (num2 > 0.0 && num12 != 0)
                {
                    double num16 = num2 / num8;
                    num9 += num16 * ((double)num12 / 100.0);
                }
                if (num3 > 0.0 && num13 != 0)
                {
                    double num17 = num3 / num8;
                    num9 += num17 * ((double)num13 / 100.0);
                }
                if (num4 > 0.0 && num14 != 0)
                {
                    double num18 = num4 / num8;
                    num9 += num18 * ((double)num14 / 100.0);
                }
                num9 += (double)num10 / 100.0;
            }
            if (flag2 && num9 != 0.0)
            {
                modifierAmounts.Add(num9);
                modifierReasons.Add(TextResolver.GetText("Colony Invasion Bonus Troop General"));
            }
        }

        public double DetermineAttackBonus(out bool fromNativePlanet, out bool fromUncolonizable, out bool fromTroopGenerals)
        {
            double num = 1.0;
            fromNativePlanet = false;
            fromUncolonizable = false;
            fromTroopGenerals = false;
            if (InvadingTroops != null)
            {
                double num2 = 0.0;
                double num3 = 0.0;
                for (int i = 0; i < InvadingTroops.Count; i++)
                {
                    Troop troop = InvadingTroops[i];
                    num2 += troop.OverallAttackStrength;
                    num3 += troop.OverallAttackStrength;
                    if (troop != null && troop.Race != null && troop.Race.NativeHabitatType == Type)
                    {
                        num3 += troop.OverallAttackStrength * 0.1;
                        fromNativePlanet = true;
                        continue;
                    }
                    bool flag = false;
                    if (troop.Empire != null)
                    {
                        flag = Type switch
                        {
                            HabitatType.Continental => troop.Empire.CanColonizeContinental,
                            HabitatType.MarshySwamp => troop.Empire.CanColonizeMarshySwamp,
                            HabitatType.Desert => troop.Empire.CanColonizeDesert,
                            HabitatType.Ocean => troop.Empire.CanColonizeOcean,
                            HabitatType.Ice => troop.Empire.CanColonizeIce,
                            HabitatType.Volcanic => troop.Empire.CanColonizeVolcanic,
                            _ => false,
                        };
                    }
                    if (troop.Race != null && troop.Race.NativeHabitatType == Type)
                    {
                        flag = true;
                    }
                    if (!flag)
                    {
                        num3 -= troop.OverallAttackStrength * 0.1;
                        fromUncolonizable = true;
                    }
                }
                num = num3 / num2;
                double num4 = 0.0;
                if (InvadingCharacters != null && InvadingCharacters.Count > 0)
                {
                    int num5 = -100;
                    for (int j = 0; j < InvadingCharacters.Count; j++)
                    {
                        Character character = InvadingCharacters[j];
                        if (character != null && character.Role == CharacterRole.TroopGeneral)
                        {
                            num5 = Math.Max(num5, character.TroopGroundAttack);
                        }
                    }
                    if (num5 <= -100)
                    {
                        num5 = 0;
                    }
                    num4 = (double)(100 + num5) / 100.0;
                }
                if (num4 <= 0.0)
                {
                    num4 = 1.0;
                }
                if (num4 != 1.0)
                {
                    fromTroopGenerals = true;
                }
                num *= num4;
            }
            return num - 1.0;
        }

        public void InflictTroopLosses(Empire defendingEmpire, Empire inflicter, double losses, TroopList troops, TroopList attackingTroops, Galaxy galaxy)
        {
            InflictTroopLosses(defendingEmpire, inflicter, losses, troops, attackingTroops, galaxy, specialForcesEvadeBetter: true);
        }

        public void InflictTroopLosses(Empire defendingEmpire, Empire inflicter, double losses, TroopList troops, TroopList attackingTroops, Galaxy galaxy, bool specialForcesEvadeBetter)
        {
            if (troops == null || troops.Count <= 0)
            {
                return;
            }
            Troop troop = troops[Galaxy.Rnd.Next(0, troops.Count)];
            if (troop == null)
            {
                return;
            }
            if (troop.Type == TroopType.SpecialForces && specialForcesEvadeBetter)
            {
                losses /= 3.0;
            }
            if ((double)troop.Readiness >= losses)
            {
                troop.Readiness -= (float)losses;
                if (ColonyInvasion != null)
                {
                    Troop firer = null;
                    if (attackingTroops != null && attackingTroops.Count > 0)
                    {
                        firer = attackingTroops[Galaxy.Rnd.Next(0, attackingTroops.Count)];
                    }
                    ColonyInvasion.AddExplosion(troop, isLarge: false, firer);
                }
                if (InvasionStats != null)
                {
                    if (inflicter == defendingEmpire)
                    {
                        InvasionStats.TroopsDamageToInvaders += (float)losses;
                    }
                    else
                    {
                        InvasionStats.TroopsDamageToDefenders += (float)losses;
                    }
                }
                return;
            }
            if (inflicter != null && inflicter.Counters != null)
            {
                inflicter.Counters.ProcessTroopDestruction(troop);
            }
            if (InvasionStats != null)
            {
                if (inflicter == defendingEmpire)
                {
                    InvasionStats.TroopsDamageToInvaders += troop.Readiness;
                    InvasionStats.DestroyedInvadingTroops++;
                }
                else
                {
                    InvasionStats.TroopsDamageToDefenders += troop.Readiness;
                    InvasionStats.DestroyedDefendingTroops++;
                }
            }
            if (ColonyInvasion != null)
            {
                Troop firer2 = null;
                if (attackingTroops != null && attackingTroops.Count > 0)
                {
                    firer2 = attackingTroops[Galaxy.Rnd.Next(0, attackingTroops.Count)];
                }
                ColonyInvasion.AddExplosion(troop, isLarge: true, firer2);
            }
            losses -= (double)troop.Readiness;
            troops.Remove(troop);
            if (troop.Empire != null && troop.Empire.Troops != null)
            {
                troop.Empire.Troops.Remove(troop);
            }
            InflictTroopLosses(defendingEmpire, inflicter, losses, troops, attackingTroops, galaxy);
        }

        private void CheckForNewCharacters(Galaxy galaxy)
        {
            if (Owner == null || Owner == galaxy.IndependentEmpire)
            {
                return;
            }
            int index = Galaxy.Rnd.Next(0, Population.Count);
            Character nextAppearanceCharacter = Galaxy.Characters.GetNextAppearanceCharacter(Population[index].Race);
            if (nextAppearanceCharacter != null)
            {
                int num = Galaxy.Rnd.Next(0, 11) - 5;
                long num2 = (galaxy.CurrentStarDate - galaxy._StartStarDate) / 6000;
                if (nextAppearanceCharacter.AppearanceOrder + num <= num2)
                {
                    nextAppearanceCharacter.Empire = Owner;
                    nextAppearanceCharacter.StartDate = galaxy.CurrentStarDate;
                    Owner.Characters.Add(nextAppearanceCharacter);
                    nextAppearanceCharacter.Active = true;
                    nextAppearanceCharacter.Location = this;
                    string description = nextAppearanceCharacter.Name + " has been born on " + Name;
                    Owner.SendMessageToEmpire(Owner, EmpireMessageType.CharacterAppearance, nextAppearanceCharacter, description);
                }
            }
        }

        public ResourceList DetermineCriticalResources()
        {
            if (Population != null && Population.TotalAmount > 0 && Population.DominantRace != null && Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                return Population.DominantRace.CriticalResources.ResolveResources();
            }
            return new ResourceList();
        }

        public void RecalculateCriticalResourceSupplyBonuses()
        {
            RecalculateCriticalResourceSupplyFactors();
            ResourceBonusList resourceBonusList = new ResourceBonusList();
            if (Population != null && Population.TotalAmount > 0 && Population.DominantRace != null && Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                Race dominantRace = Population.DominantRace;
                ResourceBonusList criticalResources = dominantRace.CriticalResources;
                for (int i = 0; i < criticalResources.Count; i++)
                {
                    ResourceBonus resourceBonus = criticalResources[i];
                    if (resourceBonus.AppliesOnlyToSources)
                    {
                        if (Resources != null)
                        {
                            int num = Resources.IndexOf(resourceBonus.ResourceId, 0);
                            if (num >= 0)
                            {
                                resourceBonusList.Add(new ResourceBonus(resourceBonus.ResourceId, resourceBonus.Effect, resourceBonus.Value, resourceBonus.AppliesOnlyToSources));
                            }
                        }
                        continue;
                    }
                    int num2 = 0;
                    if (Cargo != null)
                    {
                        num2 = Cargo.GetTotalResourceAvailable(new Resource(resourceBonus.ResourceId), Empire.EmpireId);
                    }
                    if (num2 > 0)
                    {
                        resourceBonusList.Add(new ResourceBonus(resourceBonus.ResourceId, resourceBonus.Effect, resourceBonus.Value, resourceBonus.AppliesOnlyToSources));
                    }
                }
            }
            _ResourceBonuses = resourceBonusList;
        }

        public void RecalculateCriticalResourceSupplyFactors()
        {
            double num = 1.0;
            double num2 = 1.0;
            if (Population != null && Population.TotalAmount > 0 && Population.DominantRace != null && Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                double num3 = 1.0;
                double num4 = 1.0;
                if (Facilities != null && Facilities.Count > 0)
                {
                    for (int i = 0; i < Facilities.Count; i++)
                    {
                        PlanetaryFacility planetaryFacility = Facilities[i];
                        if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder)
                        {
                            switch (planetaryFacility.WonderType)
                            {
                                case WonderType.ColonyIncome:
                                    num3 = Math.Max(num3, 1.0 + (double)planetaryFacility.Value2 / 100.0);
                                    break;
                                case WonderType.ColonyPopulationGrowth:
                                    num4 = Math.Max(num4, 1.0 + (double)planetaryFacility.Value2 / 100.0);
                                    break;
                            }
                        }
                    }
                }
                num *= num4;
                num2 *= num3;
            }
            _GrowthFactor = num;
            _IncomeFactor = num2;
        }

        public ColonyPopulationPolicy GetPolicyForPopulation(Race race, Race empireDominantRace)
        {
            ColonyPopulationPolicy result = ColonyPopulationPolicy.Assimilate;
            if (race != empireDominantRace && race != null && empireDominantRace != null)
            {
                result = ColonyPopulationPolicy;
                if (race.FamilyId == empireDominantRace.FamilyId)
                {
                    result = ColonyPopulationPolicyRaceFamily;
                }
            }
            return result;
        }

        public void GrowPopulation(TimeSpan timePassed)
        {
            if (PlagueId >= 0 || !(PlagueTimeRemaining <= 0f))
            {
                return;
            }
            long num = 0L;
            for (int i = 0; i < Population.Count; i++)
            {
                Population population = Population[i];
                num += population.Amount;
            }
            if (num < _MaxPopulation)
            {
                for (int j = 0; j < Population.Count; j++)
                {
                    Population population2 = Population[j];
                    if (Empire == _Galaxy.IndependentEmpire && population2.Race != null)
                    {
                        population2.GrowthRate = 1f + ((float)population2.Race.ReproductiveRate - 1f) / 3f;
                    }
                    double num2 = (double)population2.GrowthRate - 1.0;
                    if (num2 < 0.0)
                    {
                        num2 = 0.0;
                    }
                    long num3 = (long)((double)population2.Amount * (num2 * (timePassed.TotalSeconds / (double)Galaxy.RealSecondsInGalacticYear)));
                    population2.Amount += num3;
                    if (population2.Amount < Galaxy.MinimumHabitatPopulationAmount)
                    {
                        Race empireDominantRace = null;
                        if (Empire != null && Empire != _Galaxy.IndependentEmpire)
                        {
                            empireDominantRace = Empire.DominantRace;
                        }
                        switch (GetPolicyForPopulation(population2.Race, empireDominantRace))
                        {
                            case ColonyPopulationPolicy.Resettle:
                            case ColonyPopulationPolicy.Exterminate:
                                continue;
                        }
                        population2.Amount = Galaxy.MinimumHabitatPopulationAmount;
                    }
                }
                Population.RecalculateTotalAmount();
            }
            if (num > _MaxPopulation)
            {
                double num4 = (double)_MaxPopulation / (double)num;
                for (int k = 0; k < Population.Count; k++)
                {
                    Population population3 = Population[k];
                    population3.Amount = (long)((double)population3.Amount * num4);
                }
                Population.RecalculateTotalAmount();
            }
        }

        private void CheckHabitatIsEmpire(Galaxy galaxy)
        {
            if (galaxy.NextEmpireID >= Galaxy.MaximumEmpireCount || Owner != galaxy.IndependentEmpire)
            {
                return;
            }
            long num = 0L;
            if (Population == null || Population.Count <= 0 || Population.DominantRace == null || Population.DominantRace.IntelligenceLevel < Galaxy.HabitatToEmpireMinimumIntelligence)
            {
                return;
            }
            for (int i = 0; i < Population.Count; i++)
            {
                Population population = Population[i];
                if (population != null && population.Race != null)
                {
                    num += population.Race.IntelligenceLevel * population.Amount;
                }
            }
            if (num < Galaxy.HabitatToEmpireThreshhold || galaxy.Empires.Count >= galaxy.MaximumEmpireAmount || Galaxy.Rnd.Next(0, 5) != 2)
            {
                return;
            }
            GovernmentAttributesList governmentAttributesList = Empire.DetermineMostSuitableGovermentTypes(Population.DominantRace, Empire.ResolveDefaultAllowableGovernmentTypes(Population.DominantRace));
            EmpirePolicy policy = _Galaxy.LoadEmpirePolicy(Population.DominantRace, isPirate: false);
            Empire empire = new Empire(galaxy, "", this, Population.DominantRace, governmentAttributesList[0].GovernmentId, 1.0, policy);
            IsRefuellingDepot = true;
            int num2 = 1;
            int num3 = 2;
            int num4 = 1;
            int num5 = 5;
            int num6 = 0;
            int num7 = 1;
            EmpireList empiresToExclude = new EmpireList();
            Empire empire2 = galaxy.FindNearestEmpireCapital(Xpos, Ypos, empiresToExclude);
            if (empire2 != null && empire2.Research != null && !empire2.CheckEmpireHasHyperDriveTech(empire2))
            {
                empire.Research.TechTree = Galaxy.ResearchNodeDefinitionsStatic.SetTechTreeLevel(galaxy, empire.Research.TechTree, empire.DominantRace, 0.0, isPirate: false);
                ResearchNode researchNode = empire.Research.TechTree.FindNodeBySpecialFunctionCode(2);
                if (researchNode != null)
                {
                    researchNode.IsEnabled = true;
                }
                empire.Research.Update(empire.DominantRace);
                empire.ReviewResearchAbilities();
                empire.ReviewDesignsBuiltObjectsImprovedComponents();
                num2 = 0;
                num3 = 0;
                num4 = 0;
                num5 = 0;
                num6 = 0;
                num7 = 0;
            }
            empire.TakeOwnershipOfColony(this, empire);
            empire.ControlColonization = AutomationLevel.FullyAutomated;
            empire.ControlColonyDevelopment = true;
            empire.ControlColonyStockLevels = true;
            empire.ControlColonyTaxRates = true;
            empire.ControlDesigns = true;
            empire.ControlDiplomacyGifts = AutomationLevel.FullyAutomated;
            empire.ControlDiplomacyOffense = AutomationLevel.FullyAutomated;
            empire.ControlDiplomacyTreaties = AutomationLevel.FullyAutomated;
            empire.ControlMilitaryAttacks = AutomationLevel.FullyAutomated;
            empire.ControlMilitaryFleets = true;
            empire.ControlStateConstruction = AutomationLevel.FullyAutomated;
            empire.ControlTroopGeneration = true;
            empire.ControlAgentAssignment = AutomationLevel.FullyAutomated;
            empire.ControlResearch = true;
            empire.ControlPopulationPolicy = true;
            empire.ControlColonyFacilities = AutomationLevel.FullyAutomated;
            empire.ControlCharacterLocations = true;
            empire.ControlOfferPirateMissions = AutomationLevel.FullyAutomated;
            if (Population.DominantRace != null)
            {
                empire.DesignPictureFamilyIndex = Population.DominantRace.DesignPictureFamilyIndex;
            }
            empire.GenerateDesignSpecifications(galaxy, Population.DominantRace, isPirate: false, Population.DominantRace.Name);
            empire.InitiateConstruction = false;
            empire.DoTasks();
            empire.InitiateConstruction = true;
            Design design = null;
            BuiltObject builtObject = null;
            double x;
            double y;
            for (int j = 0; j < num2; j++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.GasMiningShip);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: false, Xpos + 100.0, Ypos - 50.0);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int k = 0; k < num2; k++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.MiningShip);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: false, Xpos + 50.0, Ypos + 100.0);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int l = 0; l < num3; l++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.ConstructionShip);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: true, Xpos, Ypos);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int m = 0; m < num4; m++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.ExplorationShip);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: true, Xpos, Ypos);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int n = 0; n < num5; n++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.SmallFreighter);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: false, Xpos - 50.0, Ypos - 100.0);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            for (int num8 = 0; num8 < num6; num8++)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.MediumFreighter);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, galaxy.GenerateBuiltObjectName(design, this), isState: false, Xpos - 50.0, Ypos - 100.0);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeParkingPoint(out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                }
            }
            if (num7 > 0)
            {
                design = empire.Designs.FindNewest(BuiltObjectSubRole.MediumSpacePort);
                if (design != null)
                {
                    design.BuildCount++;
                    builtObject = empire.GenerateBuiltObjectFromDesign(design, empire.Capital.Name + " " + TextResolver.GetText("Space Port"), isState: true, Xpos, Ypos);
                    builtObject.ParentHabitat = this;
                    builtObject.DateBuilt = galaxy.CurrentStarDate;
                    builtObject.DateRetrofit = galaxy.CurrentStarDate;
                    galaxy.SelectRelativeHabitatSurfacePoint(this, out x, out y);
                    builtObject.ParentOffsetX = x;
                    builtObject.ParentOffsetY = y;
                    builtObject.Heading = galaxy.SelectRandomHeading();
                    builtObject.ReDefine();
                    if (!empire.SpacePorts.Contains(builtObject))
                    {
                        empire.SpacePorts.Add(builtObject);
                    }
                    if (!empire.ConstructionYards.Contains(builtObject))
                    {
                        empire.ConstructionYards.Add(builtObject);
                    }
                    if (!empire.Manufacturers.Contains(builtObject))
                    {
                        empire.Manufacturers.Add(builtObject);
                    }
                    if (!empire.RefuellingDepots.Contains(builtObject))
                    {
                        empire.RefuellingDepots.Add(builtObject);
                    }
                    builtObject.ReDefine();
                }
            }
            empire.EnsureStrategicResourceSupply();
            empire.ResolveSystemVisibility(Xpos, Ypos, null, null);
            galaxy.Empires.Add(empire);
        }

        private void Move(Galaxy galaxy)
        {
            if (Parent == null)
            {
                return;
            }
            _ = Xpos;
            _ = Ypos;
            double xpos = Parent.Xpos;
            double ypos = Parent.Ypos;
            double totalSeconds = (_tempNow - _LastTouch).TotalSeconds;
            if (OrbitDirection)
            {
                _OrbitAngle += _AnglePerSecond * totalSeconds;
                int iterationCount = 0;
                while (Galaxy.ConditionCheckLimit(_OrbitAngle >= Math.PI * 2.0, 20, ref iterationCount))
                {
                    _OrbitAngle = Galaxy.ReduceAngle(_OrbitAngle);
                }
            }
            else
            {
                _OrbitAngle -= _AnglePerSecond * totalSeconds;
                int iterationCount2 = 0;
                while (Galaxy.ConditionCheckLimit(_OrbitAngle <= Math.PI * -2.0, 20, ref iterationCount2))
                {
                    _OrbitAngle = Galaxy.IncreaseAngle(_OrbitAngle);
                }
            }
            double num = Math.Sin(_OrbitAngle) * (double)OrbitDistance;
            double num2 = Math.Cos(_OrbitAngle) * (double)OrbitDistance;
            Ypos = ypos + num;
            Xpos = xpos + num2;
        }

        private void RecalculateEstimatedDefensiveForceRequired(bool atWar)
        {
            int strategicValue = StrategicValue;
            double num = 0.0;
            if (Owner != null && Owner != _Galaxy.IndependentEmpire)
            {
                num = Math.Pow((double)Owner.DominantRace.CautionLevel / 100.0, 2.0);
            }
            else if (Owner == _Galaxy.IndependentEmpire && Population != null && Population.DominantRace != null)
            {
                num = Math.Pow((double)Population.DominantRace.CautionLevel / 100.0, 2.0);
            }
            double num2 = 750.0;
            if (Owner != _Galaxy.PlayerEmpire && _Galaxy.DifficultyLevel > 1.0)
            {
                num2 /= Math.Sqrt(_Galaxy.DifficultyLevel);
            }
            _EstimatedDefensiveForceRequired = (int)((double)strategicValue / num2 * num);
            if (atWar && ((Owner != null && this == Owner.Capital) || strategicValue > 500000))
            {
                _EstimatedDefensiveForceRequired = (int)((double)_EstimatedDefensiveForceRequired * 1.3);
            }
            if (_Galaxy != null && _Galaxy.GlobalVictoryConditions != null && ((_Galaxy.GlobalVictoryConditions.DefendHabitat != null && _Galaxy.GlobalVictoryConditions.DefendHabitat == this) || (_Galaxy.GlobalVictoryConditions.TargetHabitat != null && _Galaxy.GlobalVictoryConditions.TargetHabitat == this)))
            {
                _EstimatedDefensiveForceRequired = (int)((double)_EstimatedDefensiveForceRequired * 2.0);
            }
            else if (Empire != null && Empire.Policy != null)
            {
                if (this == Empire.HomeWorld)
                {
                    _EstimatedDefensiveForceRequired = (int)((double)_EstimatedDefensiveForceRequired * Empire.Policy.HomeworldDefensePriority);
                }
                else if (Characters != null && Characters.CountCharactersByRole(CharacterRole.Leader) > 0 && Empire.Policy.ProtectLeaderAtAllCosts)
                {
                    _EstimatedDefensiveForceRequired = (int)((double)_EstimatedDefensiveForceRequired * 2.0);
                }
            }
        }

        public int EstimatedDefensiveForceRequired(bool atWar)
        {
            RecalculateEstimatedDefensiveForceRequired(atWar);
            return _EstimatedDefensiveForceRequired;
        }

        public double CalculateCurrentCompleteResourceValue(Galaxy galaxy)
        {
            double num = 0.0;
            int num2 = 0;
            HabitatResource[] array = ListHelper.ToArrayThreadSafe(Resources);
            for (int i = 0; i < array.Length; i++)
            {
                num2++;
                double val = galaxy.ResourceCurrentPrices[array[i].ResourceID];
                val = Math.Max(1.0, val);
                double num3 = val * val;
                double d = array[i].Abundance;
                d = Math.Sqrt(d) * 100.0;
                num += num3 / 100.0 * d;
            }
            if (Category == HabitatCategoryType.GasCloud)
            {
                num *= 6.0;
            }
            return num * 100.0;
        }

        public double CalculateCurrentStrategicResourceValue(Galaxy galaxy)
        {
            double num = 0.0;
            int num2 = 0;
            HabitatResourceList habitatResourceList = Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                if (!habitatResourceList[i].IsLuxuryResource)
                {
                    num2++;
                    double val = galaxy.ResourceCurrentPrices[habitatResourceList[i].ResourceID];
                    val = Math.Max(1.0, val);
                    double num3 = val * val * val;
                    double num4 = (double)habitatResourceList[i].Abundance * 100.0;
                    num += num3 / 100.0 * num4;
                }
            }
            if (Category == HabitatCategoryType.GasCloud)
            {
                num *= 6.0;
            }
            return num * 100.0;
        }

        public void RecalculateDevelopmentLevelBaseline()
        {
            int developmentLevelBaseline = 0;
            if (Population != null && Population.Count > 0)
            {
                Population.RecalculateTotalAmount();
                long totalAmount = Population.TotalAmount;
                double val = (double)totalAmount / 500000000.0;
                val = Math.Min(1.0, Math.Max(0.0, val));
                developmentLevelBaseline = (int)(50.0 * val);
            }
            _DevelopmentLevelBaseline = developmentLevelBaseline;
        }

        public void SetDevelopmentLevel(int developmentLevel)
        {
            _DevelopmentLevel = developmentLevel;
            if (_DevelopmentLevel > 50)
            {
                _DevelopmentLevel = 50;
            }
            else if (_DevelopmentLevel < 0)
            {
                _DevelopmentLevel = 0;
            }
        }

        public int GetDevelopmentLevel()
        {
            return _DevelopmentLevel;
        }

        public double CalculatePenalColonyValue()
        {
            if (Empire != null && Empire.Capital != null && Empire.Capital != this)
            {
                double num = _Galaxy.CalculateDistance(Empire.Capital.Xpos, Empire.Capital.Ypos, Xpos, Ypos);
                return num * Type switch
                {
                    HabitatType.Ice => 1000.0,
                    HabitatType.Volcanic => 100.0,
                    HabitatType.Desert => 10.0,
                    _ => 1.0,
                };
            }
            return 0.0;
        }

        public bool AcceptsPopulation(Empire empire, Race race)
        {
            if (empire != null && race != null)
            {
                if (Empire == null)
                {
                    return false;
                }
                if (Empire == _Galaxy.IndependentEmpire)
                {
                    return true;
                }
                Race dominantRace = Empire.DominantRace;
                if (dominantRace != null)
                {
                    if (race == dominantRace)
                    {
                        return true;
                    }
                    ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy;
                    if (race.FamilyId == dominantRace.FamilyId)
                    {
                        colonyPopulationPolicy = ColonyPopulationPolicyRaceFamily;
                    }
                    if (colonyPopulationPolicy == ColonyPopulationPolicy.Assimilate || (colonyPopulationPolicy == ColonyPopulationPolicy.Enslave && Empire == empire))
                    {
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }

        public bool HasPopulationToResettle(out Race race, out long amount)
        {
            race = null;
            amount = 0L;
            if (ColonyPopulationPolicyRaceFamily == ColonyPopulationPolicy.Resettle || ColonyPopulationPolicy == ColonyPopulationPolicy.Resettle)
            {
                Race race2 = null;
                if (Empire != null)
                {
                    race2 = Empire.DominantRace;
                }
                if (race2 != null && Population.TotalAmount > 30000000)
                {
                    for (int i = 0; i < Population.Count; i++)
                    {
                        Population population = Population[i];
                        if (population != null && population.Race != race2)
                        {
                            ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy;
                            if (population.Race.FamilyId == race2.FamilyId)
                            {
                                colonyPopulationPolicy = ColonyPopulationPolicyRaceFamily;
                            }
                            if (colonyPopulationPolicy == ColonyPopulationPolicy.Resettle)
                            {
                                race = population.Race;
                                amount = population.Amount;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public void CalculateWarWithOurRace()
        {
            _WarWithOurRace = 0f;
            if (Empire == null || Population == null || Population.DominantRace == null || Empire == _Galaxy.IndependentEmpire)
            {
                return;
            }
            double num = (double)(Population.DominantRace.LoyaltyLevel + Population.DominantRace.AggressionLevel) / 200.0;
            num *= num;
            num *= 10.0;
            double num2 = 1.0;
            for (int i = 0; i < Empire.DiplomaticRelations.Count; i++)
            {
                DiplomaticRelation diplomaticRelation = Empire.DiplomaticRelations[i];
                if (diplomaticRelation.Type == DiplomaticRelationType.War && diplomaticRelation.OtherEmpire.DominantRace == Population.DominantRace)
                {
                    num2 = 1.0 + Math.Sqrt(Math.Abs(diplomaticRelation.OtherEmpire.CivilityRating)) / 10.0 * (double)Math.Sign(diplomaticRelation.OtherEmpire.CivilityRating);
                    num2 = Math.Max(0.1, Math.Min(1.5, num2));
                    _WarWithOurRace -= (float)(num * num2);
                    break;
                }
            }
        }

        public double CalculateExterminationConcern()
        {
            double num = 0.0;
            if (Population != null && Population.Count > 0)
            {
                Race race = null;
                if (Empire != null && Empire.DominantRace != null)
                {
                    race = Empire.DominantRace;
                }
                if (race != null)
                {
                    for (int i = 0; i < Population.Count; i++)
                    {
                        Population population = Population[i];
                        if (population == null || population.Race == null)
                        {
                            continue;
                        }
                        ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                        if (population.Race != race)
                        {
                            colonyPopulationPolicy = ColonyPopulationPolicy;
                            if (population.Race.FamilyId == race.FamilyId)
                            {
                                colonyPopulationPolicy = ColonyPopulationPolicyRaceFamily;
                            }
                        }
                        ColonyPopulationPolicy colonyPopulationPolicy2 = colonyPopulationPolicy;
                        if (colonyPopulationPolicy2 == ColonyPopulationPolicy.Exterminate)
                        {
                            double num2 = (double)population.Amount / 10000000.0;
                            num -= num2;
                        }
                    }
                }
                num = Math.Min(0.0, Math.Max(-20.0, num));
            }
            return num;
        }

        public double CalculatePopulationPolicyConcern()
        {
            double exterminationConcern = 0.0;
            double slaveryConcern = 0.0;
            return CalculatePopulationPolicyConcern(out exterminationConcern, out slaveryConcern);
        }

        public double CalculatePopulationPolicyConcern(out double exterminationConcern, out double slaveryConcern)
        {
            double num = 0.0;
            exterminationConcern = 0.0;
            slaveryConcern = 0.0;
            if (Empire != null)
            {
                exterminationConcern = CalculateExterminationConcern();
                slaveryConcern = -1.0 * (((double)SlaveryBonusFactor - 1.0) * 20.0);
                if (slaveryConcern < 0.0)
                {
                    slaveryConcern = Math.Min(-5.0, Math.Max(-20.0, slaveryConcern));
                }
                num += exterminationConcern;
                num += slaveryConcern;
                num = Math.Min(0.0, Math.Max(-30.0, num));
            }
            return num;
        }

        public double CalculateAmountForTargetApproval(double targetApproval)
        {
            double num = CalculateUnmodifiedApproval(EmpireApprovalRating);
            double num2 = CalculateUnmodifiedApproval(targetApproval);
            return num2 - num;
        }

        public double CalculateUnmodifiedApproval(double value)
        {
            return CalculateUnmodifiedApproval(value, subtractAdditives: true);
        }

        public double CalculateUnmodifiedApproval(double value, bool subtractAdditives)
        {
            int num = 0;
            if (Characters != null && Characters.Count > 0)
            {
                num += Characters.GetHighestSkillLevelExcludeLeaders(CharacterSkillType.ColonyHappiness);
            }
            if (Empire != null && Empire.Leader != null)
            {
                num += Empire.Leader.ColonyHappiness;
            }
            double num2 = 1.0 + (double)num / 100.0;
            value = ((!(value > 0.0)) ? (value * num2) : (value / num2));
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.SpecialBonusHappiness > 0.0)
            {
                if (value > 0.0)
                {
                    value /= 1.0 + Empire.SpecialBonusHappiness;
                }
                else if (value < 0.0)
                {
                    value *= 1.0 + Empire.SpecialBonusHappiness;
                }
            }
            double num3 = 0.0;
            if (Facilities != null && Facilities.Count > 0)
            {
                for (int i = 0; i < Facilities.Count; i++)
                {
                    PlanetaryFacility planetaryFacility = Facilities[i];
                    if (planetaryFacility != null && planetaryFacility.ConstructionProgress >= 1f && planetaryFacility.Type == PlanetaryFacilityType.Wonder && planetaryFacility.WonderType == WonderType.ColonyHappiness)
                    {
                        num3 = (double)planetaryFacility.Value2 / 100.0;
                    }
                }
            }
            if (num3 > 0.0)
            {
                if (value > 0.0)
                {
                    value /= 1.0 + num3;
                }
                else if (value < 0.0)
                {
                    value *= 1.0 + num3;
                }
            }
            if (subtractAdditives)
            {
                value -= _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.Happiness);
                if (RaceEventType == RaceEventType.NepthysWineVintage)
                {
                    value -= 5.0;
                }
            }
            double num4 = ModifyApprovalValueByEmpireAttributes(value);
            value = num4;
            return value;
        }

        public double ModifyApprovalValueByEmpireAttributes(double inputValue)
        {
            double num = inputValue;
            if (Empire != null && Empire != _Galaxy.IndependentEmpire)
            {
                double num2 = 0.0;
                if (Population != null && Population.DominantRace != null)
                {
                    num2 = (double)Population.DominantRace.SatisfactionModifier / 100.0;
                }
                if (num < 0.0)
                {
                    if (Empire.GovernmentAttributes != null)
                    {
                        num /= Empire.GovernmentAttributes.ApprovalRating;
                    }
                    num /= 1.0 + num2;
                }
                else
                {
                    if (Empire.GovernmentAttributes != null)
                    {
                        num *= Empire.GovernmentAttributes.ApprovalRating;
                    }
                    num *= 1.0 + num2;
                }
            }
            return num;
        }

        private Empire IdentifyLeavingEmpire()
        {
            Empire result = _Galaxy.IndependentEmpire;
            if (Quality > 0.5f)
            {
                if (InvadingTroops != null && InvadingTroops.Count > 0)
                {
                    for (int i = 0; i < InvadingTroops.Count; i++)
                    {
                        if (InvadingTroops[i] != null && InvadingTroops[i].Empire != null && InvadingTroops[i].Empire != _Galaxy.IndependentEmpire)
                        {
                            result = InvadingTroops[i].Empire;
                            break;
                        }
                    }
                }
                else
                {
                    EmpireList empireList = new EmpireList();
                    foreach (DiplomaticRelation diplomaticRelation in Owner.DiplomaticRelations)
                    {
                        if (diplomaticRelation.Type == DiplomaticRelationType.FreeTradeAgreement || diplomaticRelation.Type == DiplomaticRelationType.Protectorate || diplomaticRelation.Type == DiplomaticRelationType.MutualDefensePact)
                        {
                            if (diplomaticRelation.OtherEmpire != Owner)
                            {
                                empireList.Add(diplomaticRelation.OtherEmpire);
                            }
                        }
                        else if (Population != null && Population.DominantRace != null && diplomaticRelation.OtherEmpire.DominantRace == Population.DominantRace && diplomaticRelation.OtherEmpire != Owner)
                        {
                            empireList.Add(diplomaticRelation.OtherEmpire);
                        }
                    }
                    if (empireList.Count > 0)
                    {
                        double num = double.MaxValue;
                        Habitat habitat = null;
                        foreach (Empire item in empireList)
                        {
                            Habitat habitat2 = _Galaxy.FastFindNearestColony((int)Xpos, (int)Ypos, item, 0);
                            if (habitat2 != null)
                            {
                                double num2 = _Galaxy.CalculateDistance(habitat2.Xpos, habitat2.Ypos, Xpos, Ypos);
                                if (num2 < num)
                                {
                                    num = num2;
                                    habitat = habitat2;
                                }
                            }
                        }
                        if (habitat != null && num < (double)Galaxy.SectorSize * 0.8)
                        {
                            result = habitat.Empire;
                        }
                    }
                }
            }
            return result;
        }

        public void LeaveEmpire()
        {
            if (Owner == null || Owner == _Galaxy.IndependentEmpire || (InvadingTroops != null && InvadingTroops.Count != 0))
            {
                return;
            }
            _CulturalDistressFactor = 0f;
            _Rebelling = false;
            Empire empire = IdentifyLeavingEmpire();
            if (empire != _Galaxy.IndependentEmpire)
            {
                string description = string.Format(TextResolver.GetText("Colony Leaves Empire"), Name, empire.Name);
                Owner.SendMessageToEmpire(Owner, EmpireMessageType.ColonyLost, this, description);
                empire.TakeOwnershipOfColony(this, empire);
                description = string.Format(TextResolver.GetText("Colony Leaves Empire Joins Us"), Name);
                empire.SendMessageToEmpire(empire, EmpireMessageType.ColonyGained, this, description);
                if (Population == null || Population.DominantRace == null)
                {
                    return;
                }
                RaceList newAbilityRaces = new RaceList();
                Race raceChanged = null;
                List<string> list = empire.ReviewEmpireAbilityBonuses(out newAbilityRaces, out raceChanged);
                if (list.Count <= 0 || raceChanged == null)
                {
                    return;
                }
                string text = string.Format(TextResolver.GetText("Revolt New Race Ability"), Galaxy.ResolveDescription(Category).ToLower(CultureInfo.InvariantCulture), Name, raceChanged.Name);
                text += "\n";
                foreach (string item in list)
                {
                    text = text + "\n" + item;
                }
                string text2 = TextResolver.GetText("New Ability for our Empire");
                empire.SendEventMessageToEmpire(EventMessageType.NewEmpireRaceAbility, text2, text, raceChanged, this);
            }
            else
            {
                string description2 = string.Format(TextResolver.GetText("Colony Leaves Empire Independent"), Name);
                Owner.SendMessageToEmpire(Owner, EmpireMessageType.ColonyLost, this, description2);
                _Galaxy.IndependentEmpire.TakeOwnershipOfColony(this, _Galaxy.IndependentEmpire, destroyAllBuiltObjectsAndTroopsAtColony: true);
            }
        }

        private void CheckSatisfaction()
        {
            if (Owner == null || Owner == _Galaxy.IndependentEmpire)
            {
                return;
            }
            int num = 0;
            if (Troops != null && Troops.Count > 0 && Troops.TotalDefendStrength > 0 && Population != null && Population.TotalAmount > 0)
            {
                int num2 = (int)(Population.TotalAmount / 50000);
                if (num2 > 0)
                {
                    num = Troops.TotalDefendStrength / num2;
                }
            }
            int num3 = (int)(-1.0 * Math.Sqrt(Math.Sqrt(Math.Sqrt(Population.TotalAmount))));
            int num4 = num3 * 3;
            int num5 = num4 - num;
            int num6 = num3 / 2;
            int num7 = num6 - num;
            double empireApprovalRating = EmpireApprovalRating;
            if (empireApprovalRating < (double)num5 && _Rebelling)
            {
                if (Troops == null || Troops.Count <= 0)
                {
                    LeaveEmpire();
                }
            }
            else if (empireApprovalRating < (double)num7)
            {
                if (_Rebelling || (InvadingTroops != null && InvadingTroops.Count > 0))
                {
                    return;
                }
                bool flag = false;
                if (Population != null && Population.DominantRace != null && Population.DominantRace != _Galaxy.ShakturiActualRace && ((Troops != null && Troops.Count > 0 && Galaxy.Rnd.Next(0, 3) > 0) || _WarWithOurRace != 0f))
                {
                    int num8 = 1;
                    long totalAmount = Population.TotalAmount;
                    num8 = (int)((totalAmount < 100000000) ? Math.Max(1L, totalAmount / 40000000) : ((totalAmount < 1000000000) ? Math.Max(2L, totalAmount / 150000000) : ((totalAmount >= 5000000000L) ? Math.Max(10L, totalAmount / 600000000) : Math.Max(5L, totalAmount / 400000000))));
                    if (_WarWithOurRace != 0f)
                    {
                        num8 = (int)((double)num8 * 1.6);
                    }
                    Race dominantRace = Population.DominantRace;
                    TroopList troopList = new TroopList();
                    for (int i = 0; i < num8; i++)
                    {
                        double num9 = dominantRace.TroopStrength;
                        if (Ruin != null)
                        {
                            num9 = (int)(num9 * (1.0 + Ruin.BonusDefensive));
                        }
                        float num10 = (float)(num9 *= 0.7);
                        Troop troop = Galaxy.GenerateNewTroop(string.Format(TextResolver.GetText("RACENAME Militia"), dominantRace), TroopType.Infantry, (int)num10, _Galaxy.IndependentEmpire, dominantRace);
                        troop.Colony = this;
                        troopList.Add(troop);
                    }
                    if (troopList.Count > 0)
                    {
                        flag = true;
                        InvadingTroops.AddRange(troopList);
                    }
                }
                string description = string.Format(TextResolver.GetText("Colony Rebelling"), Name);
                if (flag)
                {
                    description = string.Format(TextResolver.GetText("Colony Rebelling Militia"), Name);
                }
                Owner.SendMessageToEmpire(Owner, EmpireMessageType.ColonyRebelling, this, description);
                _Rebelling = true;
            }
            else
            {
                _Rebelling = false;
            }
        }

        public bool CheckColonyRevenueFromPirateControl(Empire revenueEmpire)
        {
            if (revenueEmpire != null && revenueEmpire.PirateEmpireBaseHabitat != null)
            {
                if (Empire == revenueEmpire)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void RecalculateAnnualTaxRevenue()
        {
            if (Empire != null)
            {
                RecalculateCriticalResourceSupplyBonuses();
                _AnnualTaxRevenue = AnnualRevenue * (double)TaxRate * TaxComplianceRate;
                _AnnualTaxRevenue = Math.Max(0.0, _AnnualTaxRevenue);
                double num = Galaxy.ColonyStateSupportCost;
                if (Population != null)
                {
                    double num2 = 100000000.0;
                    double num3 = Population.TotalAmount;
                    if (num3 < num2)
                    {
                        bool flag = false;
                        if (Empire != null && Empire.Capital != null && Empire.Capital == this)
                        {
                            flag = true;
                        }
                        if (!flag)
                        {
                            double num4 = 1.0 + (num2 - num3) / 30000000.0;
                            num *= num4;
                        }
                    }
                }
                if (Empire != null)
                {
                    _ = Empire.PirateEmpireBaseHabitat;
                }
                _AnnualTaxRevenue -= num;
            }
            else
            {
                _AnnualTaxRevenue = 0.0;
            }
        }

        public void RecalculateDistanceFactor()
        {
            HabitatList empireCapitals = Empire.IdentifyEmpireCapitals();
            RecalculateDistanceFactor(empireCapitals);
        }

        public void RecalculateDistanceFactor(HabitatList empireCapitals)
        {
            if (Empire != null && empireCapitals != null && empireCapitals.Count > 0)
            {
                double num = double.MaxValue;
                for (int i = 0; i < empireCapitals.Count; i++)
                {
                    double num2 = _Galaxy.CalculateDistance(Xpos, Ypos, empireCapitals[i].Xpos, empireCapitals[i].Ypos);
                    if (num2 < num)
                    {
                        num = num2;
                    }
                }
                double num3 = (double)Galaxy.SizeX * 0.6;
                _DistanceFactor = (float)Math.Max(0.0, Math.Min(1.0, num / num3));
            }
            else if (Empire != null && Empire.PirateEmpireBaseHabitat != null)
            {
                double num4 = _Galaxy.CalculateDistance(Xpos, Ypos, Empire.PirateEmpireBaseHabitat.Xpos, Empire.PirateEmpireBaseHabitat.Ypos);
                double num5 = (double)Galaxy.SizeX * 0.6;
                _DistanceFactor = (float)Math.Max(0.0, Math.Min(1.0, num4 / num5));
            }
            else
            {
                _DistanceFactor = 0f;
            }
        }

        public double CalculateOrbitPathLength()
        {
            return Math.PI * (double)_OrbitDistance * 2.0;
        }

        private void RecalculateMaximumPopulation()
        {
            BaconHabitat.RecalculateMaximumPopulation(this);
        }

        public Habitat(Galaxy galaxy, HabitatCategoryType habitatCategory, HabitatType habitatType, string name, double x, double y)
            : this(galaxy, habitatCategory, habitatType, name, null, 0.0, orbitdirection: true, 0, 0)
        {
            _LastHugeTouch = galaxy.CurrentDateTime;
            _LastLongTouch = galaxy.CurrentDateTime;
            _LastPeriodicTouch = galaxy.CurrentDateTime;
            _LastTouch = galaxy.CurrentDateTime;
            Xpos = x;
            Ypos = y;
        }

        public Habitat(Galaxy galaxy, HabitatCategoryType habitatCategory, HabitatType habitatType, string name, Habitat parent, double orbitangle, bool orbitdirection, int orbitdistance, int orbitspeed)
            : this(galaxy, habitatCategory, habitatType, name, parent, orbitangle, orbitdirection, orbitdistance, orbitspeed, doInitialMove: true)
        {
        }

        public Habitat(Galaxy galaxy, HabitatCategoryType habitatCategory, HabitatType habitatType, string name, Habitat parent, double orbitangle, bool orbitdirection, int orbitdistance, int orbitspeed, bool doInitialMove)
        {
            _Galaxy = galaxy;
            _LastHugeTouch = galaxy.CurrentDateTime;
            _LastLongTouch = galaxy.CurrentDateTime;
            _LastPeriodicTouch = galaxy.CurrentDateTime;
            _LastTouch = galaxy.CurrentDateTime;
            switch (habitatCategory)
            {
                case HabitatCategoryType.Asteroid:
                    switch (habitatType)
                    {
                        case HabitatType.BarrenRock:
                        case HabitatType.Ice:
                        case HabitatType.Metal:
                            break;
                        default:
                            throw new ApplicationException("Invalid habitat category/type combination.");
                    }
                    Category = habitatCategory;
                    Type = habitatType;
                    break;
                case HabitatCategoryType.GasCloud:
                    switch (habitatType)
                    {
                        case HabitatType.Hydrogen:
                        case HabitatType.Helium:
                        case HabitatType.Argon:
                        case HabitatType.Ammonia:
                        case HabitatType.CarbonDioxide:
                        case HabitatType.Oxygen:
                        case HabitatType.NitrogenOxygen:
                        case HabitatType.Chlorine:
                            break;
                        default:
                            throw new ApplicationException("Invalid habitat category/type combination.");
                    }
                    Category = habitatCategory;
                    Type = habitatType;
                    break;
                case HabitatCategoryType.Moon:
                    switch (habitatType)
                    {
                        case HabitatType.Volcanic:
                        case HabitatType.Desert:
                        case HabitatType.MarshySwamp:
                        case HabitatType.Continental:
                        case HabitatType.Ocean:
                        case HabitatType.BarrenRock:
                        case HabitatType.Ice:
                            break;
                        default:
                            throw new ApplicationException("Invalid habitat category/type combination.");
                    }
                    Category = habitatCategory;
                    Type = habitatType;
                    break;
                case HabitatCategoryType.Planet:
                    switch (habitatType)
                    {
                        case HabitatType.Volcanic:
                        case HabitatType.Desert:
                        case HabitatType.MarshySwamp:
                        case HabitatType.Continental:
                        case HabitatType.Ocean:
                        case HabitatType.BarrenRock:
                        case HabitatType.Ice:
                        case HabitatType.GasGiant:
                        case HabitatType.FrozenGasGiant:
                            break;
                        default:
                            throw new ApplicationException("Invalid habitat category/type combination.");
                    }
                    Category = habitatCategory;
                    Type = habitatType;
                    break;
                case HabitatCategoryType.Star:
                    switch (habitatType)
                    {
                        case HabitatType.MainSequence:
                        case HabitatType.RedGiant:
                        case HabitatType.SuperGiant:
                        case HabitatType.WhiteDwarf:
                        case HabitatType.Neutron:
                        case HabitatType.BlackHole:
                        case HabitatType.SuperNova:
                            break;
                        default:
                            throw new ApplicationException("Invalid habitat category/type combination.");
                    }
                    Category = habitatCategory;
                    Type = habitatType;
                    break;
            }
            Resources = new HabitatResourceList();
            ConstructionQueue = null;
            _ManufacturingQueue = null;
            Cargo = null;
            Population = new PopulationList();
            Troops = null;
            _TroopsToRecruit = null;
            _InvadingTroops = null;
            _InvadingCharacters = null;
            _BasesAtHabitat = new BuiltObjectList();
            Name = name;
            _Parent = parent;
            _OrbitAngle = orbitangle;
            _OrbitDirection = orbitdirection;
            _OrbitDistance = (short)orbitdistance;
            _OrbitSpeed = (byte)orbitspeed;
            double num = CalculateOrbitPathLength();
            if (parent != null)
            {
                _AnglePerSecond = Math.PI * 2.0 / (num / (double)(int)_OrbitSpeed);
            }
            _tempNow = galaxy.CurrentDateTime;
            if (doInitialMove)
            {
                _LastTouch = galaxy.CurrentDateTime.AddSeconds(-30.0);
                if (parent != null)
                {
                    _AnglePerSecond = Math.PI * 2.0 / (num / (double)(int)_OrbitSpeed);
                    Move(galaxy);
                }
            }
            RecalculateCriticalResourceSupplyBonuses();
        }

        private void DoExplosions()
        {
            if (Explosions == null || Explosions.Count <= 0)
            {
                return;
            }
            ExplosionList explosionList = new ExplosionList();
            for (int i = 0; i < Explosions.Count; i++)
            {
                Explosion explosion = Explosions[i];
                double num = (double)_tempNow.Subtract(explosion.ExplosionStart).Ticks / 10000000.0;
                explosion.ExplosionProgression = (float)Math.Max(0.0, num * 60.0);
                double num2 = Math.Min(100.0, Math.Max(50.0, explosion.ExplosionSize / 2));
                explosion.ExplosionCurrentImage = Math.Min((short)(Galaxy.ExplosionImageCount - 1), (short)((double)explosion.ExplosionProgression / num2 * (double)Galaxy.ExplosionImageCount));
                if ((double)explosion.ExplosionProgression > num2)
                {
                    explosion.ExplosionSize = 0;
                    explosion.ExplosionProgression = 0f;
                    explosion.ExplosionSoundPlayed = false;
                    explosionList.Add(explosion);
                }
            }
            foreach (Explosion item in explosionList)
            {
                Explosions.Remove(item);
            }
            if (Explosions.Count == 0)
            {
                Explosions = null;
            }
        }

        private void DoExplosion()
        {
            if (Explosion == null)
            {
                return;
            }
            double num = (double)_tempNow.Subtract(Explosion.ExplosionStart).Ticks / 10000000.0;
            Explosion.ExplosionProgression = (float)Math.Max(0.0, num * 50.0);
            double num2 = Math.Min(400.0, Math.Max(80.0, Explosion.ExplosionSize / 2));
            if (Diameter <= 50)
            {
                Explosion.ExplosionCurrentImage = Math.Min((short)(Galaxy.ExplosionImageCount - 1), (short)((double)Explosion.ExplosionProgression / num2 * (double)Galaxy.ExplosionImageCount));
            }
            else
            {
                Explosion.ExplosionCurrentImage = Math.Min((short)(Galaxy.ExplosionHabitatImageCount - 1), (short)((double)Explosion.ExplosionProgression / num2 * (double)Galaxy.ExplosionHabitatImageCount));
            }
            if (!_DestroyedAsteroidFieldGenerated && (double)Explosion.ExplosionProgression >= num2 * 0.7)
            {
                _DestroyedAsteroidFieldGenerated = true;
                Thread thread = new Thread(DoPlanetDestroyAsteroidField);
                thread.Start();
            }
            if ((double)Explosion.ExplosionProgression > num2)
            {
                HasBeenDestroyed = true;
                Explosion.ExplosionSize = 0;
                Explosion.ExplosionProgression = 0f;
                Explosion.ExplosionSoundPlayed = false;
                if (Explosion.ExplosionWillDestroy)
                {
                    Thread thread2 = new Thread(DoPlanetRemove);
                    thread2.Start();
                }
                Explosion = null;
            }
        }

        public void DoPlanetRemove()
        {
            lock (_LockObject)
            {
                DoingRemove = true;
                lock (_Galaxy._LockObject)
                {
                    while (DoingTasks)
                    {
                        Thread.Sleep(0);
                    }
                    if (_Galaxy.Habitats.Contains(this) && _Galaxy.Habitats.Count > HabitatIndex && _Galaxy.Habitats[HabitatIndex] == this)
                    {
                        _Galaxy.RemoveHabitat(this);
                    }
                }
                DoingRemove = false;
            }
        }

        public void DoPlanetDestroyAsteroidField()
        {
            lock (_Galaxy._LockObject)
            {
                if (Category != HabitatCategoryType.Asteroid)
                {
                    int num = Diameter / 2 + (int)(Galaxy.Rnd.NextDouble() * (double)Diameter * 0.125);
                    num = (num / 8 + 1) * 8;
                    Habitat nearestSystemStar = Galaxy.DetermineHabitatSystemStar(this);
                    ResourceDefinitionList randomOrderedResources = _Galaxy.ResourceSystem.GenerateRandomOrderedResources();
                    HabitatList habitatList = _Galaxy.GenerateAsteroidField(num, Xpos, Ypos, OrbitDirection, OrbitSpeed, 1.3, 0.8, randomOrderedResources);
                    _Galaxy.AddAsteroidField(habitatList, nearestSystemStar);
                    _Galaxy.OnRefreshView(new RefreshViewEventArgs(Xpos, Ypos, habitatList, onlyGalaxyBackdrops: false));
                }
            }
        }

        public PlanetaryFacilityDefinitionList ResolvePotentialFacilities()
        {
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = new PlanetaryFacilityDefinitionList();
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.Research != null && Population != null && Population.TotalAmount > 0)
            {
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = Empire.Research.BuildablePlanetaryFacilities.Clone();
                int num = planetaryFacilityDefinitionList2.CountByType(PlanetaryFacilityType.RegionalCapital);
                HabitatList habitatList = Empire.IdentifyEmpireRegionalCapitals(includeUnderConstruction: true);
                int num2 = 0;
                int num3 = 0;
                if (Facilities != null && Facilities.Count > 0)
                {
                    num3 += Facilities.CountByType(PlanetaryFacilityType.CloningFacility);
                    num3 += Facilities.CountByType(PlanetaryFacilityType.RoboticTroopFoundry);
                    num3 += Facilities.CountByType(PlanetaryFacilityType.TroopTrainingCenter);
                }
                for (int i = 0; i < planetaryFacilityDefinitionList2.Count; i++)
                {
                    switch (planetaryFacilityDefinitionList2[i].Type)
                    {
                        case PlanetaryFacilityType.TroopTrainingCenter:
                        case PlanetaryFacilityType.RoboticTroopFoundry:
                        case PlanetaryFacilityType.CloningFacility:
                            if (num3 <= 0)
                            {
                                planetaryFacilityDefinitionList.Add(planetaryFacilityDefinitionList2[i]);
                            }
                            break;
                        case PlanetaryFacilityType.RegionalCapital:
                            if (Empire.Capital != this && habitatList.Count < num && num2 == 0)
                            {
                                planetaryFacilityDefinitionList.Add(planetaryFacilityDefinitionList2[i]);
                                num2++;
                            }
                            break;
                        default:
                            planetaryFacilityDefinitionList.Add(planetaryFacilityDefinitionList2[i]);
                            break;
                    }
                }
            }
            return planetaryFacilityDefinitionList;
        }

        public bool CanBuildWonder(PlanetaryFacilityDefinition wonder)
        {
            if (wonder != null && wonder.Type == PlanetaryFacilityType.Wonder)
            {
                if (_Galaxy.CheckWonderBuilt(wonder))
                {
                    return false;
                }
                if (wonder.WonderType == WonderType.RaceAchievement && wonder.Value3 > 0)
                {
                    HabitatType habitatType = Galaxy.ResolveColonyHabitatTypeByIndexDesertBeforeOcean(wonder.Value3 - 1);
                    if (Type != habitatType)
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public PlanetaryFacilityDefinitionList ResolveBuildableWonders()
        {
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.Research != null && Population != null && Population.TotalAmount > 0)
            {
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = Empire.Research.BuildablePlanetaryFacilities.Clone();
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = new PlanetaryFacilityDefinitionList();
                for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
                {
                    if (planetaryFacilityDefinitionList[i].Type != PlanetaryFacilityType.Wonder)
                    {
                        planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                    }
                }
                for (int j = 0; j < planetaryFacilityDefinitionList.Count; j++)
                {
                    if (planetaryFacilityDefinitionList[j].Value3 > 0)
                    {
                        HabitatType habitatType = Galaxy.ResolveColonyHabitatTypeByIndexDesertBeforeOcean(planetaryFacilityDefinitionList[j].Value3 - 1);
                        if (Type != habitatType)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[j]);
                        }
                    }
                }
                if (Facilities != null && Facilities.Count > 0 && planetaryFacilityDefinitionList != null && planetaryFacilityDefinitionList.Count > 0)
                {
                    for (int k = 0; k < planetaryFacilityDefinitionList.Count; k++)
                    {
                        for (int l = 0; l < Facilities.Count; l++)
                        {
                            if (Facilities[l].PlanetaryFacilityDefinitionId == planetaryFacilityDefinitionList[k].PlanetaryFacilityDefinitionId)
                            {
                                planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[k]);
                                break;
                            }
                        }
                    }
                }
                for (int m = 0; m < planetaryFacilityDefinitionList.Count; m++)
                {
                    if (planetaryFacilityDefinitionList[m].Type == PlanetaryFacilityType.Wonder && !CanBuildWonder(planetaryFacilityDefinitionList[m]))
                    {
                        planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[m]);
                    }
                }
                if (planetaryFacilityDefinitionList2.Count > 0)
                {
                    for (int n = 0; n < planetaryFacilityDefinitionList2.Count; n++)
                    {
                        planetaryFacilityDefinitionList.Remove(planetaryFacilityDefinitionList2[n]);
                    }
                }
                return planetaryFacilityDefinitionList;
            }
            return new PlanetaryFacilityDefinitionList();
        }

        public Empire CheckFacilityOwner(PlanetaryFacility facility)
        {
            Empire result = null;
            if (facility != null && Facilities != null && Facilities.Contains(facility))
            {
                switch (facility.Type)
                {
                    case PlanetaryFacilityType.PirateBase:
                    case PlanetaryFacilityType.PirateFortress:
                    case PlanetaryFacilityType.PirateCriminalNetwork:
                        {
                            PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
                            result = ((byFacilityControl == null) ? Owner : _Galaxy.GetEmpireById(byFacilityControl.EmpireId));
                            break;
                        }
                    default:
                        result = Owner;
                        break;
                }
            }
            return result;
        }

        public PlanetaryFacility CheckPirateFacilityToAttack(out Empire pirateFaction)
        {
            pirateFaction = null;
            if (Facilities != null && Empire != null)
            {
                PlanetaryFacility planetaryFacility = Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                if (planetaryFacility != null)
                {
                    Empire empire = CheckFacilityOwner(planetaryFacility);
                    if (empire != Owner)
                    {
                        pirateFaction = empire;
                        return planetaryFacility;
                    }
                }
            }
            return null;
        }

        public bool CheckFacilityOwnedByColonyOwner(PlanetaryFacility facility)
        {
            Empire empire = CheckFacilityOwner(facility);
            if (empire == Owner)
            {
                return true;
            }
            return false;
        }

        public PlanetaryFacilityDefinitionList ResolveBuildableFacilitiesPirates(Empire pirateFaction)
        {
            PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = new PlanetaryFacilityDefinitionList();
            PlanetaryFacilityDefinition item = Galaxy.PlanetaryFacilityDefinitionsStatic[25];
            PlanetaryFacilityDefinition item2 = Galaxy.PlanetaryFacilityDefinitionsStatic[26];
            PlanetaryFacilityDefinition item3 = Galaxy.PlanetaryFacilityDefinitionsStatic[32];
            if (pirateFaction != null && pirateFaction.PirateEmpireBaseHabitat != null && Empire != null && Population != null && Population.TotalAmount > 0 && Facilities != null)
            {
                PirateColonyControl byFacilityControl = _PirateColonyControl.GetByFacilityControl();
                if (byFacilityControl != null)
                {
                    if (byFacilityControl.EmpireId == pirateFaction.EmpireId && byFacilityControl.ControlLevel >= 0.5f)
                    {
                        planetaryFacilityDefinitionList.Add(item);
                        if (byFacilityControl.ControlLevel >= 1f && Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase) > 0)
                        {
                            planetaryFacilityDefinitionList.Add(item2);
                            PlanetaryFacility planetaryFacility = Facilities.FindBestCompletedPirateFacility(includeCriminalNetwork: true);
                            if (planetaryFacility != null && planetaryFacility.Type == PlanetaryFacilityType.PirateFortress && byFacilityControl.HasFacilityControl && pirateFaction.CountPirateCriminalNetworks() <= 0)
                            {
                                planetaryFacilityDefinitionList.Add(item3);
                            }
                        }
                    }
                }
                else
                {
                    byFacilityControl = _PirateColonyControl.GetHighestControl();
                    if (byFacilityControl != null && byFacilityControl.EmpireId == pirateFaction.EmpireId && byFacilityControl.ControlLevel >= 0.5f)
                    {
                        planetaryFacilityDefinitionList.Add(item);
                        if (byFacilityControl.ControlLevel >= 1f && Facilities.CountCompletedByType(PlanetaryFacilityType.PirateBase) > 0)
                        {
                            planetaryFacilityDefinitionList.Add(item2);
                        }
                    }
                }
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = new PlanetaryFacilityDefinitionList();
                for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
                {
                    if (planetaryFacilityDefinitionList[i].Type == PlanetaryFacilityType.Wonder)
                    {
                        planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                    }
                }
                if (Facilities != null && Facilities.Count > 0 && planetaryFacilityDefinitionList != null && planetaryFacilityDefinitionList.Count > 0)
                {
                    for (int j = 0; j < planetaryFacilityDefinitionList.Count; j++)
                    {
                        for (int k = 0; k < Facilities.Count; k++)
                        {
                            if (Facilities[k].Type == planetaryFacilityDefinitionList[j].Type)
                            {
                                planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[j]);
                                break;
                            }
                        }
                    }
                }
                if (planetaryFacilityDefinitionList2.Count > 0)
                {
                    for (int l = 0; l < planetaryFacilityDefinitionList2.Count; l++)
                    {
                        planetaryFacilityDefinitionList.Remove(planetaryFacilityDefinitionList2[l]);
                    }
                }
            }
            return planetaryFacilityDefinitionList;
        }

        public PlanetaryFacilityDefinitionList ResolveBuildableFacilities()
        {
            if (Empire != null && Empire != _Galaxy.IndependentEmpire && Empire.Research != null && Population != null && Population.TotalAmount > 0)
            {
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList = Empire.Research.BuildablePlanetaryFacilities.Clone();
                PlanetaryFacilityDefinitionList planetaryFacilityDefinitionList2 = new PlanetaryFacilityDefinitionList();
                for (int i = 0; i < planetaryFacilityDefinitionList.Count; i++)
                {
                    if (planetaryFacilityDefinitionList[i].Type == PlanetaryFacilityType.Wonder)
                    {
                        planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[i]);
                    }
                }
                if (Facilities != null && Facilities.Count > 0 && planetaryFacilityDefinitionList != null && planetaryFacilityDefinitionList.Count > 0)
                {
                    for (int j = 0; j < planetaryFacilityDefinitionList.Count; j++)
                    {
                        for (int k = 0; k < Facilities.Count; k++)
                        {
                            if (Facilities[k].Type == planetaryFacilityDefinitionList[j].Type)
                            {
                                planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[j]);
                                break;
                            }
                        }
                    }
                    PlanetaryFacility planetaryFacility = Facilities.FindByType(PlanetaryFacilityType.CloningFacility);
                    PlanetaryFacility planetaryFacility2 = Facilities.FindByType(PlanetaryFacilityType.RoboticTroopFoundry);
                    PlanetaryFacility planetaryFacility3 = Facilities.FindByType(PlanetaryFacilityType.TroopTrainingCenter);
                    if (planetaryFacility != null || planetaryFacility2 != null || planetaryFacility3 != null)
                    {
                        PlanetaryFacilityDefinition item = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.CloningFacility);
                        PlanetaryFacilityDefinition item2 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.RoboticTroopFoundry);
                        PlanetaryFacilityDefinition item3 = planetaryFacilityDefinitionList.FindFacilityByType(PlanetaryFacilityType.TroopTrainingCenter);
                        planetaryFacilityDefinitionList2.Add(item);
                        planetaryFacilityDefinitionList2.Add(item2);
                        planetaryFacilityDefinitionList2.Add(item3);
                    }
                }
                int num = planetaryFacilityDefinitionList.CountByType(PlanetaryFacilityType.RegionalCapital);
                HabitatList habitatList = Empire.IdentifyEmpireRegionalCapitals(includeUnderConstruction: true);
                if (this == Empire.Capital || habitatList.Count >= num)
                {
                    for (int l = 0; l < planetaryFacilityDefinitionList.Count; l++)
                    {
                        if (planetaryFacilityDefinitionList[l].Type == PlanetaryFacilityType.RegionalCapital)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[l]);
                        }
                    }
                }
                else if (num > 1)
                {
                    int num2 = 0;
                    for (int num3 = planetaryFacilityDefinitionList.Count - 1; num3 >= 0; num3--)
                    {
                        if (planetaryFacilityDefinitionList[num3].Type == PlanetaryFacilityType.RegionalCapital && num2 < num - 1)
                        {
                            planetaryFacilityDefinitionList2.Add(planetaryFacilityDefinitionList[num3]);
                            num2++;
                        }
                    }
                }
                if (planetaryFacilityDefinitionList2.Count > 0)
                {
                    for (int m = 0; m < planetaryFacilityDefinitionList2.Count; m++)
                    {
                        planetaryFacilityDefinitionList.Remove(planetaryFacilityDefinitionList2[m]);
                    }
                }
                return planetaryFacilityDefinitionList;
            }
            return new PlanetaryFacilityDefinitionList();
        }

        public bool QueueWonderConstruction(PlanetaryFacilityDefinition wonder)
        {
            return QueueWonderConstruction(wonder, fullyConstructed: false);
        }

        public bool QueueWonderConstruction(PlanetaryFacilityDefinition wonder, bool fullyConstructed)
        {
            if (Facilities == null)
            {
                Facilities = new PlanetaryFacilityList();
            }
            if (_Galaxy.CheckWonderBuilt(wonder))
            {
                return false;
            }
            float constructionProgress = 0f;
            if (fullyConstructed)
            {
                constructionProgress = 1f;
            }
            Facilities.Add(new PlanetaryFacility(wonder.PlanetaryFacilityDefinitionId, constructionProgress));
            return true;
        }

        public bool QueueFacilityConstruction(PlanetaryFacilityType facilityType)
        {
            return QueueFacilityConstruction(facilityType, fullyConstructed: false);
        }

        public bool QueueFacilityConstruction(PlanetaryFacilityType facilityType, bool fullyConstructed)
        {
            if (Facilities == null)
            {
                Facilities = new PlanetaryFacilityList();
            }
            for (int i = 0; i < Facilities.Count; i++)
            {
                if (Facilities[i].Type == facilityType)
                {
                    return false;
                }
            }
            PlanetaryFacilityDefinition planetaryFacilityDefinition = Galaxy.PlanetaryFacilityDefinitionsStatic.FindFacilityByType(facilityType);
            if (planetaryFacilityDefinition != null)
            {
                float constructionProgress = 0f;
                if (fullyConstructed)
                {
                    constructionProgress = 1f;
                }
                Facilities.Add(new PlanetaryFacility(planetaryFacilityDefinition.PlanetaryFacilityDefinitionId, constructionProgress));
                return true;
            }
            return false;
        }

        public bool CheckTroopFacilitiesPresent()
        {
            bool result = false;
            if (Facilities != null && Facilities.Count > 0)
            {
                int val = 0;
                val = Math.Max(val, Facilities.CountByType(PlanetaryFacilityType.CloningFacility));
                val = Math.Max(val, Facilities.CountByType(PlanetaryFacilityType.RoboticTroopFoundry));
                val = Math.Max(val, Facilities.CountByType(PlanetaryFacilityType.TroopTrainingCenter));
                if (val > 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public Troop DetermineDefaultTroopTypeForColony()
        {
            Troop troop = null;
            if (Population != null)
            {
                Race dominantRace = Population.DominantRace;
                Empire empire = Empire;
                double num = 100.0;
                if (dominantRace != null)
                {
                    num = dominantRace.TroopStrength;
                }
                if (Ruin != null)
                {
                    num *= 1.0 + Ruin.BonusDefensive;
                }
                if (dominantRace != null)
                {
                    troop = Galaxy.GenerateNewTroop(dominantRace.TroopName, TroopType.Infantry, (int)num, empire, dominantRace);
                    troop.Readiness = 0f;
                    troop.Colony = this;
                }
            }
            return troop;
        }

        public Troop DetermineTroopTypeForColony()
        {
            Troop troop = null;
            if (Population != null)
            {
                Race dominantRace = Population.DominantRace;
                Empire empire = Empire;
                Troop troop2 = empire.IdentifyStrongestRaceAttackTroop();
                double num = 100.0;
                if (dominantRace != null)
                {
                    num = dominantRace.TroopStrength;
                }
                if (Ruin != null)
                {
                    num *= 1.0 + Ruin.BonusDefensive;
                }
                if (Facilities != null)
                {
                    for (int i = 0; i < Facilities.Count; i++)
                    {
                        if (!(Facilities[i].ConstructionProgress >= 1f))
                        {
                            continue;
                        }
                        switch (Facilities[i].Type)
                        {
                            case PlanetaryFacilityType.CloningFacility:
                                if (troop2 != null)
                                {
                                    troop = Galaxy.GenerateNewTroop(TextResolver.GetText("Clone Trooper Battalion"), TroopType.Infantry, troop2.AttackStrength, empire, troop2.Race, applyBonusFactors: false);
                                    troop.SetDefendStrength(troop2.DefendStrength);
                                    troop.Colony = this;
                                }
                                break;
                            case PlanetaryFacilityType.RoboticTroopFoundry:
                                troop = Galaxy.GenerateNewTroop(TextResolver.GetText("BattleBot Group"), TroopType.Infantry, 60, empire, null, applyBonusFactors: false);
                                troop.MaintenanceMultiplier = 0.25f;
                                troop.PictureRef = _Galaxy.Races.Count;
                                troop.Colony = this;
                                break;
                            case PlanetaryFacilityType.TroopTrainingCenter:
                                troop = Galaxy.GenerateNewTroop(string.Format(TextResolver.GetText("Elite TROOPNAME"), dominantRace.TroopName), TroopType.Infantry, (int)((float)num * 1.5f), empire, dominantRace);
                                troop.Colony = this;
                                break;
                        }
                    }
                }
                if (troop == null && dominantRace != null)
                {
                    troop = Galaxy.GenerateNewTroop(dominantRace.TroopName, TroopType.Infantry, (int)num, empire, dominantRace);
                    troop.Readiness = 0f;
                    troop.Colony = this;
                }
            }
            return troop;
        }

        public TroopList ResolveRecruitableTroopsForColony()
        {
            int cloneIndex = -1;
            int roboticIndex = -1;
            int eliteIndex = -1;
            return ResolveRecruitableTroopsForColony(out cloneIndex, out roboticIndex, out eliteIndex);
        }

        public TroopList ResolveRecruitableTroopsForColony(out int cloneIndex, out int roboticIndex, out int eliteIndex)
        {
            TroopList troopList = new TroopList();
            cloneIndex = -1;
            roboticIndex = -1;
            eliteIndex = -1;
            if (Population != null)
            {
                Race dominantRace = Population.DominantRace;
                Empire empire = Empire;
                Troop troop = empire.IdentifyStrongestRaceAttackTroop();
                double num = 100.0;
                if (dominantRace != null)
                {
                    num = dominantRace.TroopStrength;
                }
                if (Ruin != null)
                {
                    num *= 1.0 + Ruin.BonusDefensive;
                }
                double bonusTotalByEffectType = _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.RecruitedTroopStrength);
                num += bonusTotalByEffectType;
                if (empire != null && empire.Research != null && dominantRace != null)
                {
                    if (Empire.TroopCanRecruitInfantry)
                    {
                        Troop troop2 = Galaxy.GenerateNewTroop(dominantRace.TroopName, TroopType.Infantry, (int)num, empire, dominantRace);
                        troop2.Readiness = 0f;
                        troop2.Colony = this;
                        troopList.Add(troop2);
                    }
                    if (Empire.TroopCanRecruitArmored && Facilities.FindCompletedByType(PlanetaryFacilityType.ArmoredFactory) != null)
                    {
                        Troop troop3 = Galaxy.GenerateNewTroop(dominantRace.TroopNameArmored, TroopType.Armored, (int)num, empire, dominantRace);
                        troop3.Readiness = 0f;
                        troop3.Colony = this;
                        troopList.Add(troop3);
                    }
                    if (Empire.TroopCanRecruitArtillery)
                    {
                        Troop troop4 = Galaxy.GenerateNewTroop(dominantRace.TroopNameArtillery, TroopType.Artillery, (int)num, empire, dominantRace);
                        troop4.Readiness = 0f;
                        troop4.Colony = this;
                        troopList.Add(troop4);
                    }
                    if (Empire.TroopCanRecruitSpecialForces && Facilities.FindCompletedByType(PlanetaryFacilityType.MilitaryAcademy) != null)
                    {
                        Troop troop5 = Galaxy.GenerateNewTroop(dominantRace.TroopNameSpecialForces, TroopType.SpecialForces, (int)num, empire, dominantRace);
                        troop5.Readiness = 0f;
                        troop5.Colony = this;
                        troopList.Add(troop5);
                    }
                }
                if (Facilities != null)
                {
                    for (int i = 0; i < Facilities.Count; i++)
                    {
                        PlanetaryFacility planetaryFacility = Facilities[i];
                        if (planetaryFacility == null || !(planetaryFacility.ConstructionProgress >= 1f))
                        {
                            continue;
                        }
                        Troop troop6 = null;
                        switch (planetaryFacility.Type)
                        {
                            case PlanetaryFacilityType.CloningFacility:
                                if (troop != null)
                                {
                                    troop6 = Galaxy.GenerateNewTroop(TextResolver.GetText("Clone Trooper Battalion"), TroopType.Infantry, troop.AttackStrength, empire, troop.Race, applyBonusFactors: false);
                                    troop6.SetDefendStrength(troop.DefendStrength);
                                    troop6.Colony = this;
                                }
                                break;
                            case PlanetaryFacilityType.RoboticTroopFoundry:
                                troop6 = Galaxy.GenerateNewTroop(TextResolver.GetText("BattleBot Group"), TroopType.Infantry, 60, empire, null, applyBonusFactors: false);
                                troop6.MaintenanceMultiplier = 0.25f;
                                troop6.PictureRef = _Galaxy.Races.Count;
                                troop6.Colony = this;
                                break;
                            case PlanetaryFacilityType.TroopTrainingCenter:
                                troop6 = Galaxy.GenerateNewTroop(string.Format(TextResolver.GetText("Elite TROOPNAME"), dominantRace.TroopName), TroopType.Infantry, (int)((float)num * 1.5f), empire, dominantRace);
                                troop6.Colony = this;
                                break;
                        }
                        if (troop6 != null)
                        {
                            switch (planetaryFacility.Type)
                            {
                                case PlanetaryFacilityType.CloningFacility:
                                    cloneIndex = troopList.Count;
                                    break;
                                case PlanetaryFacilityType.RoboticTroopFoundry:
                                    roboticIndex = troopList.Count;
                                    break;
                                case PlanetaryFacilityType.TroopTrainingCenter:
                                    eliteIndex = troopList.Count;
                                    break;
                            }
                            troopList.Add(troop6);
                        }
                    }
                }
            }
            return troopList;
        }

        public Troop GenerateNewTroop()
        {
            return GenerateNewTroop(recruitDefaultTroops: false);
        }

        public Troop GenerateNewTroop(bool recruitDefaultTroops)
        {
            Troop empireBestTroop = null;
            if (Empire != null)
            {
                empireBestTroop = Empire.IdentifyStrongestRaceAttackTroop();
            }
            return GenerateNewTroop(empireBestTroop, recruitDefaultTroops);
        }

        public Troop GenerateNewTroop(Troop empireBestTroop, bool recruitDefaultTroops)
        {
            TroopType troopType = TroopType.Infantry;
            return GenerateNewTroop(troopType, empireBestTroop, recruitDefaultTroops);
        }

        public Troop GenerateNewTroop(TroopType troopType, Troop empireBestTroop, bool recruitDefaultTroops)
        {
            Troop troop = null;
            if (Population != null && Population.DominantRace != null && Empire != null)
            {
                Race dominantRace = Population.DominantRace;
                Empire empire = Empire;
                double num = dominantRace.TroopStrength;
                if (Ruin != null)
                {
                    num *= 1.0 + Ruin.BonusDefensive;
                }
                double bonusTotalByEffectType = _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.RecruitedTroopStrength);
                num += bonusTotalByEffectType;
                if (RaceEventType == RaceEventType.GreatHuntStrongTroops || RaceEventType == RaceEventType.WarriorWaveTroopRecruitment)
                {
                    num *= 1.1;
                }
                switch (troopType)
                {
                    case TroopType.Infantry:
                        if (Facilities != null)
                        {
                            for (int i = 0; i < Facilities.Count; i++)
                            {
                                PlanetaryFacility planetaryFacility = Facilities[i];
                                if (!(planetaryFacility.ConstructionProgress >= 1f))
                                {
                                    continue;
                                }
                                switch (planetaryFacility.Type)
                                {
                                    case PlanetaryFacilityType.CloningFacility:
                                        if (empireBestTroop == null && Empire != null)
                                        {
                                            empireBestTroop = Empire.IdentifyStrongestRaceAttackTroop();
                                        }
                                        if (empireBestTroop != null && empireBestTroop.Race != null)
                                        {
                                            troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(TextResolver.GetText("Clone Trooper Battalion")), TroopType.Infantry, empireBestTroop.AttackStrength, empire, empireBestTroop.Race, applyBonusFactors: false);
                                            troop.SetDefendStrength(empireBestTroop.DefendStrength);
                                            troop.Readiness = 0f;
                                            troop.Colony = this;
                                        }
                                        break;
                                    case PlanetaryFacilityType.RoboticTroopFoundry:
                                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(TextResolver.GetText("BattleBot Group")), TroopType.Infantry, 60, empire, null, applyBonusFactors: false);
                                        troop.MaintenanceMultiplier = 0.25f;
                                        troop.Readiness = 0f;
                                        troop.PictureRef = _Galaxy.Races.Count;
                                        troop.Colony = this;
                                        break;
                                    case PlanetaryFacilityType.TroopTrainingCenter:
                                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(string.Format(TextResolver.GetText("Elite TROOPNAME"), dominantRace.TroopName)), TroopType.Infantry, (int)((float)num * 1.5f), empire, dominantRace);
                                        troop.Readiness = 0f;
                                        troop.Colony = this;
                                        break;
                                }
                            }
                        }
                        if (troop == null || recruitDefaultTroops)
                        {
                            troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopName), TroopType.Infantry, (int)num, empire, dominantRace);
                            troop.Readiness = 0f;
                            troop.Colony = this;
                        }
                        break;
                    case TroopType.Armored:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameArmored), TroopType.Armored, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                    case TroopType.Artillery:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameArtillery), TroopType.Artillery, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                    case TroopType.SpecialForces:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameSpecialForces), TroopType.SpecialForces, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                }
            }
            return troop;
        }

        public Troop GenerateNewTroop(TroopType troopType, Troop empireBestTroop, bool isClone, bool isRobotic, bool isElite)
        {
            Troop troop = null;
            if (Population != null && Population.DominantRace != null && Empire != null)
            {
                Race dominantRace = Population.DominantRace;
                Empire empire = Empire;
                double num = dominantRace.TroopStrength;
                if (Ruin != null)
                {
                    num *= 1.0 + Ruin.BonusDefensive;
                }
                double bonusTotalByEffectType = _ResourceBonuses.GetBonusTotalByEffectType(ColonyResourceEffect.RecruitedTroopStrength);
                num += bonusTotalByEffectType;
                if (RaceEventType == RaceEventType.GreatHuntStrongTroops || RaceEventType == RaceEventType.WarriorWaveTroopRecruitment)
                {
                    num *= 1.1;
                }
                switch (troopType)
                {
                    case TroopType.Infantry:
                        if (isClone)
                        {
                            if (Facilities != null && Facilities.CountCompletedByType(PlanetaryFacilityType.CloningFacility) > 0)
                            {
                                if (empireBestTroop == null && Empire != null)
                                {
                                    empireBestTroop = Empire.IdentifyStrongestRaceAttackTroop();
                                }
                                if (empireBestTroop != null && empireBestTroop.Race != null)
                                {
                                    troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(TextResolver.GetText("Clone Trooper Battalion")), TroopType.Infantry, empireBestTroop.AttackStrength, empire, empireBestTroop.Race, applyBonusFactors: false);
                                    troop.SetDefendStrength(empireBestTroop.DefendStrength);
                                    troop.Readiness = 0f;
                                    troop.Colony = this;
                                }
                            }
                        }
                        else if (isRobotic)
                        {
                            if (Facilities != null && Facilities.CountCompletedByType(PlanetaryFacilityType.RoboticTroopFoundry) > 0)
                            {
                                troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(TextResolver.GetText("BattleBot Group")), TroopType.Infantry, 60, empire, null, applyBonusFactors: false);
                                troop.MaintenanceMultiplier = 0.25f;
                                troop.Readiness = 0f;
                                troop.PictureRef = _Galaxy.Races.Count;
                                troop.Colony = this;
                            }
                        }
                        else if (isElite)
                        {
                            if (Facilities != null && Facilities.CountCompletedByType(PlanetaryFacilityType.TroopTrainingCenter) > 0)
                            {
                                troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(string.Format(TextResolver.GetText("Elite TROOPNAME"), dominantRace.TroopName)), TroopType.Infantry, (int)((float)num * 1.5f), empire, dominantRace);
                                troop.Readiness = 0f;
                                troop.Colony = this;
                            }
                        }
                        else
                        {
                            troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopName), TroopType.Infantry, (int)num, empire, dominantRace);
                            troop.Readiness = 0f;
                            troop.Colony = this;
                        }
                        break;
                    case TroopType.Armored:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameArmored), TroopType.Armored, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                    case TroopType.Artillery:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameArtillery), TroopType.Artillery, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                    case TroopType.SpecialForces:
                        troop = Galaxy.GenerateNewTroop(empire.GenerateTroopDescription(dominantRace.TroopNameSpecialForces), TroopType.SpecialForces, (int)num, empire, dominantRace);
                        troop.Readiness = 0f;
                        troop.Colony = this;
                        break;
                }
            }
            return troop;
        }

        public Troop GenerateNewTroop(Troop troopToGenerate)
        {
            if (troopToGenerate != null && Population != null && Population.DominantRace != null && Empire != null)
            {
                int troopStrength = Population.DominantRace.TroopStrength;
                troopToGenerate = Galaxy.GenerateNewTroop(troopToGenerate.Name, troopToGenerate.Type, troopStrength, troopToGenerate.Empire, troopToGenerate.Race);
                troopToGenerate.Readiness = 0f;
                troopToGenerate.Colony = this;
            }
            return troopToGenerate;
        }

        private void TerraformColony(double timePassed)
        {
            BaconHabitat.TerraformColony(this, timePassed);
        }

        public void ReviewPlanetaryFacilities(Empire empire)
        {
            byte b = 0;
            bool planetaryShieldPresent = false;
            bool giantIonCannonPresent = false;
            Weapon giantIonCannon = null;
            byte b2 = 0;
            PlanetaryFacility planetaryFacility = null;
            if (Facilities != null)
            {
                for (int i = 0; i < Facilities.Count; i++)
                {
                    if (!(Facilities[i].ConstructionProgress >= 1f))
                    {
                        continue;
                    }
                    if (Facilities[i].Type == PlanetaryFacilityType.FortifiedBunker)
                    {
                        b = (byte)Facilities[i].Value1;
                    }
                    else if (Facilities[i].Type == PlanetaryFacilityType.IonCannon)
                    {
                        giantIonCannonPresent = true;
                        giantIonCannon = new Weapon(new BuiltObjectComponent(Facilities[i].Value1, ComponentStatus.Normal));
                    }
                    else if (Facilities[i].Type == PlanetaryFacilityType.PlanetaryShield)
                    {
                        planetaryShieldPresent = true;
                    }
                    else if (Facilities[i].Type == PlanetaryFacilityType.Wonder)
                    {
                        if (planetaryFacility == null || Facilities[i].Value1 > planetaryFacility.Value1)
                        {
                            planetaryFacility = Facilities[i];
                        }
                        if (Facilities[i].WonderType == WonderType.ColonyDefense)
                        {
                            b2 = (byte)Facilities[i].Value2;
                        }
                    }
                }
            }
            double val = 10.0 * ((1.0 + (double)(int)b2 / 10.0) * (1.0 + (double)(int)b / 10.0)) - 10.0;
            byte defensiveFortressBonus = (byte)Math.Min(255.0, val);
            PlanetaryShieldPresent = planetaryShieldPresent;
            GiantIonCannonPresent = giantIonCannonPresent;
            GiantIonCannon = giantIonCannon;
            DefensiveFortressBonus = defensiveFortressBonus;
            WonderForDevelopment = planetaryFacility;
            empire?.RecalculateColonyDistancesFromCapital();
        }

        public double CalculateStrategicResourceSupplyGrowthFactor()
        {
            double result = 1.0;
            if (Cargo != null && Empire != null)
            {
                int num = 0;
                int num2 = 0;
                for (int i = 0; i < _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance.Count; i++)
                {
                    ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.StrategicResourcesOrderedByRelativeImportance[i];
                    if (resourceDefinition != null && resourceDefinition.ColonyGrowthResourceLevel > 0f)
                    {
                        num++;
                        Cargo cargo = Cargo.GetCargo(new Resource(resourceDefinition.ResourceID), Empire);
                        if (cargo != null && cargo.Available > 0)
                        {
                            num2++;
                        }
                    }
                }
                result = (double)num2 / (double)num;
            }
            return result;
        }

        public void ConsumeAndOrderStrategicResourceSupply(double timePassed)
        {
            if (Population == null || Population.Count <= 0 || Population.TotalAmount <= 0 || Empire == null)
            {
                return;
            }
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            for (int i = 0; i < _Galaxy.ResourceSystem.Resources.Count; i++)
            {
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.Resources[i];
                if (resourceDefinition != null && resourceDefinition.ColonyGrowthResourceLevel > 0f)
                {
                    Resource resource = new Resource(resourceDefinition.ResourceID);
                    ConsumeStrategicResource(resource, timePassed);
                    CheckAndOrderResource(this, orders, resource);
                }
            }
        }

        private void ConsumeStrategicResource(Resource resource, double timePassed)
        {
            Cargo cargo = Cargo.GetCargo(resource, Empire);
            if (cargo == null)
            {
                return;
            }
            int num = (int)(0.5 + timePassed / (double)Galaxy.RealSecondsInGalacticYear * (double)CalculateStrategicResourceConsumptionPerYear(resource.ResourceID));
            if (cargo.Available > num)
            {
                cargo.Amount -= num;
                if (cargo.Amount <= 0 && cargo.Reserved <= 0)
                {
                    Cargo.Remove(cargo);
                }
            }
        }

        private void CheckAndOrderResource(Habitat colony, OrderList colonyOrders, Resource resource)
        {
            int amountToOrder = 0;
            int num = (int)(1.6 * (double)CalculateStrategicResourceConsumptionPerYear(resource.ResourceID));
            int minimumResourceLevel = (int)((double)num * 0.8);
            if (CheckResourceMeetsMinimumLevel(resource, minimumResourceLevel, num, colony, colonyOrders, out amountToOrder))
            {
                return;
            }
            double num2 = (double)amountToOrder * _Galaxy.ResourceCurrentPrices[resource.ResourceID];
            double num3 = 0.0;
            num3 = ((Empire != _Galaxy.IndependentEmpire) ? Empire.GetPrivateFunds() : double.MaxValue);
            if (num2 < num3)
            {
                BuiltObject builtObject = null;
                if (colony.HasSpacePort)
                {
                    builtObject = _Galaxy.DetermineSpacePortAtHabitat(colony);
                }
                if (builtObject != null && builtObject.IsSpacePort)
                {
                    Empire.CreateOrder(builtObject, resource, amountToOrder, isState: false, OrderType.Standard);
                }
                else
                {
                    Empire.CreateOrder(colony, resource, amountToOrder, isState: false, OrderType.Standard);
                }
            }
        }

        private bool CheckResourceMeetsMinimumLevel(Resource resource, int minimumResourceLevel, int maximumResourceLevel, Habitat colony, OrderList colonyOrders, out int amountToOrder)
        {
            bool result = false;
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = -1;
            if (colony.Cargo != null && colony.Cargo.GetExists(resource))
            {
                num4 = colony.Cargo.IndexOf(resource, colony.Owner);
            }
            if (num4 >= 0)
            {
                num = colony.Cargo[num4].Available;
                num2 = num;
            }
            int num5;
            for (num5 = colonyOrders.IndexOf(resource.ResourceID, 0); num5 >= 0; num5 = colonyOrders.IndexOf(resource.ResourceID, num5))
            {
                num3 = colonyOrders[num5].AmountRequested;
                num2 += num3;
                num5++;
            }
            amountToOrder = Math.Max(0, maximumResourceLevel - num2);
            if (amountToOrder > 0)
            {
                if (resource.IsRestrictedResource)
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumRestrictedResourceReorderAmount);
                }
                else
                {
                    amountToOrder = Math.Max(amountToOrder, Galaxy.MinimumContractSize);
                }
                amountToOrder = Math.Min(20000, amountToOrder);
            }
            if (num2 >= minimumResourceLevel)
            {
                result = true;
            }
            return result;
        }

        public int CalculateStrategicResourceConsumptionPerYear(byte resourceId)
        {
            double num = 0.0;
            if (Population != null)
            {
                num = (double)Population.TotalAmount / 1000000.0;
                num = Math.Sqrt(num);
                int num2 = 5;
                ResourceDefinition resourceDefinition = _Galaxy.ResourceSystem.Resources[resourceId];
                if (resourceDefinition != null && resourceDefinition.ColonyGrowthResourceLevel > 0f)
                {
                    return num2 + (int)(5.0 * Galaxy.ColonyStrategicResourceConsumptionPerMillionPerYear * num * (double)resourceDefinition.ColonyGrowthResourceLevel);
                }
                return 0;
            }
            return 0;
        }

        public int CalculateMinimumLuxuryResourceLevel()
        {
            int result = 0;
            if (Population != null)
            {
                long num = Math.Max(500000000L, Population.TotalAmount);
                result = (int)(Galaxy.ColonyAnnualLuxuryResourceConsumptionRate * (double)num);
                result = Math.Max(result * 3, Galaxy.MinimumLuxuryResourceReorderAmount);
            }
            return result;
        }

        public int CalculateMinimumLuxuryResourceLevelRestricted()
        {
            int result = 0;
            if (Population != null)
            {
                long num = Math.Max(500000000L, Population.TotalAmount);
                result = (int)(Galaxy.ColonyAnnualRestrictedResourceConsumptionRate * (double)num);
                result = Math.Max(result * 3, Galaxy.MinimumRestrictedResourceReorderAmount);
            }
            return result;
        }

        public void ClearColony(Empire clearingEmpire)
        {
            ClearColony(clearingEmpire, sendMessages: true, removeEmpireWhenNoColonies: true);
        }

        public void ClearColony(Empire clearingEmpire, bool sendMessages, bool removeEmpireWhenNoColonies)
        {
            int num = -1;
            if (Cargo != null)
            {
                Cargo.Clear();
            }
            Cargo = null;
            IsRefuellingDepot = false;
            if (Troops != null && Troops.Count > 0)
            {
                for (int i = 0; i < Troops.Count; i++)
                {
                    Troop troop = Troops[i];
                    if (troop.Empire != null && troop.Empire.Counters != null)
                    {
                        troop.Empire.Counters.ProcessTroopDestruction(troop);
                    }
                    if (troop.Empire != null && troop.Empire.Troops != null)
                    {
                        troop.Empire.Troops.Remove(troop);
                    }
                    troop.Empire = null;
                    troop.Colony = null;
                    troop.BuiltObject = null;
                }
                Troops.Clear();
            }
            if (TroopsToRecruit != null && TroopsToRecruit.Count > 0)
            {
                for (int j = 0; j < TroopsToRecruit.Count; j++)
                {
                    Troop troop2 = TroopsToRecruit[j];
                    if (troop2.Empire != null && troop2.Empire.Counters != null)
                    {
                        troop2.Empire.Counters.ProcessTroopDestruction(troop2);
                    }
                    if (troop2.Empire != null && troop2.Empire.Troops != null)
                    {
                        troop2.Empire.Troops.Remove(troop2);
                    }
                    troop2.Empire = null;
                    troop2.Colony = null;
                    troop2.BuiltObject = null;
                }
                TroopsToRecruit.Clear();
            }
            if (InvadingTroops != null && InvadingTroops.Count > 0)
            {
                for (int k = 0; k < InvadingTroops.Count; k++)
                {
                    Troop troop3 = InvadingTroops[k];
                    if (troop3.Empire != null && troop3.Empire.Troops != null)
                    {
                        troop3.Empire.Troops.Remove(troop3);
                    }
                    troop3.Empire = null;
                    troop3.Colony = null;
                    troop3.BuiltObject = null;
                }
                InvadingTroops.Clear();
            }
            if (Characters != null && Characters.Count > 0)
            {
                Character[] array = ListHelper.ToArrayThreadSafe(Characters);
                for (int l = 0; l < array.Length; l++)
                {
                    array[l].Kill(_Galaxy);
                }
            }
            if (InvadingCharacters != null && InvadingCharacters.Count > 0)
            {
                Character[] array2 = ListHelper.ToArrayThreadSafe(InvadingCharacters);
                for (int m = 0; m < array2.Length; m++)
                {
                    array2[m].Kill(_Galaxy);
                }
            }
            if (ConstructionQueue != null)
            {
                foreach (ConstructionYard constructionYard in ConstructionQueue.ConstructionYards)
                {
                    if (constructionYard.ShipUnderConstruction != null)
                    {
                        constructionYard.ShipUnderConstruction.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    }
                    constructionYard.ShipUnderConstruction = null;
                }
            }
            OrderList orders = _Galaxy.Orders.GetOrders(this);
            if (orders.Count > 0)
            {
                foreach (Order item in orders)
                {
                    _Galaxy.Orders.Remove(item);
                }
            }
            if (Population != null)
            {
                if (Population.Count > 0)
                {
                    for (int n = 0; n < Population.Count; n++)
                    {
                        Population population = Population[n];
                        population.Amount = 0L;
                    }
                    Population.Clear();
                }
                Population.RecalculateTotalAmount();
            }
            if (Facilities != null && Facilities.Count > 0)
            {
                for (int num2 = 0; num2 < Facilities.Count; num2++)
                {
                    CheckRemoveFacilityTracking(Facilities[num2]);
                }
                Facilities.Clear();
            }
            if (Empire != null)
            {
                num = Empire.Colonies.IndexOf(this);
                if (num >= 0)
                {
                    Empire.Colonies.RemoveAt(num);
                    if (Empire.Capital == this)
                    {
                        Habitat capital = Empire.SelectBestCandidateForCapital();
                        Empire.Capital = capital;
                        Empire.RecalculateColonyDistancesFromCapital();
                        if (Empire.Colonies.Count <= 0 && removeEmpireWhenNoColonies)
                        {
                            if (Empire.PirateEmpireBaseHabitat != null)
                            {
                                if (_Galaxy.CheckPirateEmpireTerminated(Empire))
                                {
                                    _Galaxy.EliminatePirateFaction(Empire, clearingEmpire);
                                }
                            }
                            else
                            {
                                if (sendMessages)
                                {
                                    Empire.SendMessageToEmpire(Empire, EmpireMessageType.EmpireDefeated, Empire, TextResolver.GetText("You have been defeated!"));
                                }
                                Empire.CompleteTeardown(clearingEmpire, removeFromGalaxy: true, sendMessages: false);
                            }
                        }
                    }
                }
                if (Empire != _Galaxy.IndependentEmpire)
                {
                    Empire.ResolveSystemVisibility(Xpos, Ypos, null, this);
                    Empire.EvaluateSystemLinks();
                }
                Empire empire = Empire;
                Owner = null;
                Empire = null;
                ReviewPlanetaryFacilities(empire);
            }
            SystemInfo other = _Galaxy.DetermineSystemInfo(_Galaxy.Systems[SystemIndex], _Galaxy.PlayerEmpire);
            _Galaxy.Systems[SystemIndex].CopyFromOther(other);
        }

        public void CompleteTeardown()
        {
            int num = -1;
            ClearColony(TeardownEmpire);
            _Galaxy.Orders.UpdateHabitatIndexes(HabitatIndex, -1);
            for (int i = 0; i < _Galaxy.BuiltObjects.Count; i++)
            {
                BuiltObject builtObject = _Galaxy.BuiltObjects[i];
                if (builtObject != null && builtObject.ParentHabitat == this)
                {
                    if (builtObject.Role == BuiltObjectRole.Base || builtObject.DockedAt == this || builtObject.BuiltAt == this)
                    {
                        builtObject.ClearPreviousMissionRequirements();
                        builtObject.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    }
                    else
                    {
                        builtObject.ParentHabitat = null;
                        builtObject.ParentOffsetX = -2000000001.0;
                        builtObject.ParentOffsetY = -2000000001.0;
                    }
                }
            }
            CreatureList creatureList = new CreatureList();
            for (int j = 0; j < _Galaxy.Creatures.Count; j++)
            {
                Creature creature = _Galaxy.Creatures[j];
                if (creature != null && creature.ParentHabitat == this)
                {
                    if (creature.Type == CreatureType.RockSpaceSlug || creature.Type == CreatureType.DesertSpaceSlug)
                    {
                        creatureList.Add(creature);
                    }
                    else
                    {
                        creature.ParentHabitat = null;
                    }
                }
            }
            foreach (Creature item in creatureList)
            {
                item.CompleteTeardown();
            }
            if (DockingBays != null)
            {
                foreach (DockingBay dockingBay in DockingBays)
                {
                    dockingBay.DockedShip?.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    dockingBay.DockedShip = null;
                }
            }
            for (int k = 0; k < _Galaxy.Empires.Count; k++)
            {
                Empire empire = _Galaxy.Empires[k];
                if (empire == null || empire.ShipGroups == null)
                {
                    continue;
                }
                for (int l = 0; l < empire.ShipGroups.Count; l++)
                {
                    ShipGroup shipGroup = empire.ShipGroups[l];
                    if (shipGroup == null)
                    {
                        continue;
                    }
                    BuiltObjectMission mission = shipGroup.Mission;
                    if (mission != null)
                    {
                        if (mission.TargetHabitat != null && mission.TargetHabitat == this)
                        {
                            shipGroup.CompleteMission();
                        }
                        if (mission.SecondaryTargetHabitat != null && mission.SecondaryTargetHabitat == this)
                        {
                            shipGroup.CompleteMission();
                        }
                    }
                }
            }
            GalaxyIndex galaxyIndex = _Galaxy.ResolveIndex(Xpos, Ypos);
            _ = galaxyIndex.X;
            _ = galaxyIndex.Y;
            for (int m = 0; m < _Galaxy.BuiltObjects.Count; m++)
            {
                BuiltObject builtObject2 = _Galaxy.BuiltObjects[m];
                if (builtObject2 == null)
                {
                    continue;
                }
                builtObject2.ClearAllMissionsForTarget(builtObject2, this, BuiltObjectMissionType.Undefined, dropOutOfHyperspace: true);
                if (builtObject2.NearestSystemStar == this)
                {
                    builtObject2.ClearPreviousMissionRequirements();
                    builtObject2.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    continue;
                }
                if (builtObject2.DockedAt == this)
                {
                    num = -1;
                    if (DockingBays != null)
                    {
                        num = DockingBays.IndexOf(builtObject2);
                    }
                    if (num >= 0)
                    {
                        DockingBays[num].DockedShip?.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                        continue;
                    }
                }
                if (builtObject2.BuiltAt == this && ConstructionQueue != null && ConstructionQueue.ConstructionYards != null)
                {
                    num = ConstructionQueue.ConstructionYards.IndexOf(builtObject2);
                    if (num >= 0)
                    {
                        ConstructionQueue.ConstructionYards[num].ShipUnderConstruction?.CompleteTeardown(_Galaxy, removeFromEmpire: true);
                    }
                }
            }
            if (_Galaxy.RuinsHabitats.Contains(this))
            {
                _Galaxy.RuinsHabitats.Remove(this);
            }
            if (_Galaxy.Systems != null && SystemIndex >= 0 && SystemIndex < _Galaxy.Systems.Count)
            {
                SystemInfo systemInfo = _Galaxy.Systems[SystemIndex];
                if (systemInfo != null && systemInfo.Habitats != null)
                {
                    num = systemInfo.Habitats.IndexOf(this);
                    if (num >= 0)
                    {
                        systemInfo.Habitats.RemoveAt(num);
                    }
                }
            }
            for (int n = 0; n < _Galaxy.Empires.Count; n++)
            {
                Empire empire2 = _Galaxy.Empires[n];
                if (empire2 == null)
                {
                    continue;
                }
                HabitatPrioritizationList habitatPrioritizationList = new HabitatPrioritizationList();
                PrioritizedTargetList prioritizedTargetList = new PrioritizedTargetList();
                HabitatList habitatList = new HabitatList();
                if (empire2.MonitoringHabitats != null)
                {
                    foreach (Habitat monitoringHabitat in empire2.MonitoringHabitats)
                    {
                        if (monitoringHabitat == this)
                        {
                            habitatList.Add(monitoringHabitat);
                        }
                    }
                    foreach (Habitat item2 in habitatList)
                    {
                        empire2.MonitoringHabitats.Remove(item2);
                    }
                }
                habitatList.Clear();
                if (empire2.DangerousHabitats != null)
                {
                    foreach (Habitat dangerousHabitat in empire2.DangerousHabitats)
                    {
                        if (dangerousHabitat == this)
                        {
                            habitatList.Add(dangerousHabitat);
                        }
                    }
                    foreach (Habitat item3 in habitatList)
                    {
                        empire2.DangerousHabitats.Remove(item3);
                    }
                }
                habitatList.Clear();
                if (empire2.ResortHabitats != null)
                {
                    foreach (PrioritizedTarget resortHabitat in empire2.ResortHabitats)
                    {
                        if (resortHabitat.Target is Habitat && (Habitat)resortHabitat.Target == this)
                        {
                            prioritizedTargetList.Add(resortHabitat);
                        }
                    }
                    PurgePrioritizedTargets(empire2.ResortHabitats, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.MigrationSources != null)
                {
                    foreach (PrioritizedTarget migrationSource in empire2.MigrationSources)
                    {
                        if (migrationSource.Target is Habitat && (Habitat)migrationSource.Target == this)
                        {
                            prioritizedTargetList.Add(migrationSource);
                        }
                    }
                    PurgePrioritizedTargets(empire2.MigrationSources, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.MigrationDestinations != null)
                {
                    foreach (PrioritizedTarget migrationDestination in empire2.MigrationDestinations)
                    {
                        if (migrationDestination.Target is Habitat && (Habitat)migrationDestination.Target == this)
                        {
                            prioritizedTargetList.Add(migrationDestination);
                        }
                    }
                    PurgePrioritizedTargets(empire2.MigrationDestinations, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.TourismSources != null)
                {
                    foreach (PrioritizedTarget tourismSource in empire2.TourismSources)
                    {
                        if (tourismSource.Target is Habitat && (Habitat)tourismSource.Target == this)
                        {
                            prioritizedTargetList.Add(tourismSource);
                        }
                    }
                    PurgePrioritizedTargets(empire2.TourismSources, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.TourismDestinations != null)
                {
                    foreach (PrioritizedTarget tourismDestination in empire2.TourismDestinations)
                    {
                        if (tourismDestination.Target is Habitat && (Habitat)tourismDestination.Target == this)
                        {
                            prioritizedTargetList.Add(tourismDestination);
                        }
                    }
                    PurgePrioritizedTargets(empire2.TourismDestinations, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.ResortBaseBuildLocations != null)
                {
                    foreach (PrioritizedTarget resortBaseBuildLocation in empire2.ResortBaseBuildLocations)
                    {
                        if (resortBaseBuildLocation.Target is Habitat && (Habitat)resortBaseBuildLocation.Target == this)
                        {
                            prioritizedTargetList.Add(resortBaseBuildLocation);
                        }
                    }
                    PurgePrioritizedTargets(empire2.ResortBaseBuildLocations, prioritizedTargetList);
                }
                prioritizedTargetList.Clear();
                if (empire2.ResourceTargets != null)
                {
                    foreach (HabitatPrioritization resourceTarget in empire2.ResourceTargets)
                    {
                        if (resourceTarget.Habitat == this)
                        {
                            habitatPrioritizationList.Add(resourceTarget);
                        }
                    }
                    empire2.ResourceTargets = PurgeHabitatPrioritizations(empire2.ResourceTargets, habitatPrioritizationList);
                }
                habitatPrioritizationList.Clear();
                if (empire2.ColonizationTargets != null)
                {
                    foreach (HabitatPrioritization colonizationTarget in empire2.ColonizationTargets)
                    {
                        if (colonizationTarget.Habitat == this)
                        {
                            habitatPrioritizationList.Add(colonizationTarget);
                        }
                    }
                    empire2.ColonizationTargets = PurgeHabitatPrioritizations(empire2.ColonizationTargets, habitatPrioritizationList);
                }
                habitatPrioritizationList.Clear();
                if (empire2.DesiredForeignColonies != null)
                {
                    foreach (HabitatPrioritization desiredForeignColony in empire2.DesiredForeignColonies)
                    {
                        if (desiredForeignColony.Habitat == this)
                        {
                            habitatPrioritizationList.Add(desiredForeignColony);
                        }
                    }
                    empire2.DesiredForeignColonies = PurgeHabitatPrioritizations(empire2.DesiredForeignColonies, habitatPrioritizationList);
                }
                habitatPrioritizationList.Clear();
                if (empire2.EmpireResourceTargets != null)
                {
                    foreach (HabitatPrioritization empireResourceTarget in empire2.EmpireResourceTargets)
                    {
                        if (empireResourceTarget.Habitat == this)
                        {
                            habitatPrioritizationList.Add(empireResourceTarget);
                        }
                    }
                    empire2.EmpireResourceTargets = PurgeHabitatPrioritizations(empire2.EmpireResourceTargets, habitatPrioritizationList);
                }
                habitatPrioritizationList.Clear();
            }
            if (SystemIndex >= 0 && _Galaxy.Systems.Count > SystemIndex)
            {
                SystemInfo systemInfo2 = _Galaxy.Systems[SystemIndex];
                if (systemInfo2 != null && systemInfo2.SystemStar == this)
                {
                    GalaxyIndex galaxyIndex2 = _Galaxy.ResolveIndex(Xpos, Ypos);
                    if (_Galaxy.SystemsIndex[galaxyIndex2.X][galaxyIndex2.Y].Contains(systemInfo2))
                    {
                        _Galaxy.SystemsIndex[galaxyIndex2.X][galaxyIndex2.Y].Remove(systemInfo2);
                    }
                    foreach (Empire empire3 in _Galaxy.Empires)
                    {
                        if (empire3 == null)
                        {
                            continue;
                        }
                        if (empire3.SystemVisibility != null)
                        {
                            for (int num2 = 0; num2 < empire3.SystemVisibility.Count; num2++)
                            {
                                if (empire3.SystemVisibility[num2].SystemStar == this)
                                {
                                    empire3.SystemVisibility.RemoveAt(num2);
                                    break;
                                }
                            }
                        }
                        if (empire3.SystemsVisible != null)
                        {
                            num = empire3.SystemsVisible.IndexOf(systemInfo2.SystemStar);
                            if (num >= 0)
                            {
                                empire3.SystemsVisible.RemoveAt(num);
                            }
                        }
                    }
                    foreach (Empire pirateEmpire in _Galaxy.PirateEmpires)
                    {
                        if (pirateEmpire == null)
                        {
                            continue;
                        }
                        if (pirateEmpire.SystemVisibility != null)
                        {
                            for (int num3 = 0; num3 < pirateEmpire.SystemVisibility.Count; num3++)
                            {
                                if (pirateEmpire.SystemVisibility[num3].SystemStar == this)
                                {
                                    pirateEmpire.SystemVisibility.RemoveAt(num3);
                                    break;
                                }
                            }
                        }
                        if (pirateEmpire.SystemsVisible != null)
                        {
                            num = pirateEmpire.SystemsVisible.IndexOf(systemInfo2.SystemStar);
                            if (num >= 0)
                            {
                                pirateEmpire.SystemsVisible.RemoveAt(num);
                            }
                        }
                    }
                    if (_Galaxy.IndependentEmpire != null && _Galaxy.IndependentEmpire.SystemVisibility != null)
                    {
                        for (int num4 = 0; num4 < _Galaxy.IndependentEmpire.SystemVisibility.Count; num4++)
                        {
                            if (_Galaxy.IndependentEmpire.SystemVisibility[num4].SystemStar == this)
                            {
                                _Galaxy.IndependentEmpire.SystemVisibility.RemoveAt(num4);
                                break;
                            }
                        }
                    }
                    if (_Galaxy.IndependentEmpire.SystemsVisible != null)
                    {
                        num = _Galaxy.IndependentEmpire.SystemsVisible.IndexOf(systemInfo2.SystemStar);
                        if (num >= 0)
                        {
                            _Galaxy.IndependentEmpire.SystemsVisible.RemoveAt(num);
                        }
                    }
                    systemInfo2.SystemStar = null;
                }
            }
            for (int num5 = 0; num5 < Galaxy.IndexMaxX; num5++)
            {
                for (int num6 = 0; num6 < Galaxy.IndexMaxY; num6++)
                {
                    num = _Galaxy.HabitatIndex[num5][num6].IndexOf(this);
                    if (num >= 0)
                    {
                        _Galaxy.HabitatIndex[num5][num6].RemoveAt(num);
                    }
                }
            }
            _Galaxy.Habitats.Remove(this);
        }

        private void PurgePrioritizedTargets(PrioritizedTargetList prioritizations, PrioritizedTargetList prioritizationsToRemove)
        {
            foreach (PrioritizedTarget item in prioritizationsToRemove)
            {
                prioritizations.Remove(item);
            }
        }

        private HabitatPrioritizationList PurgeHabitatPrioritizations(HabitatPrioritizationList prioritizations, HabitatPrioritizationList prioritizationsToRemove)
        {
            foreach (HabitatPrioritization item in prioritizationsToRemove)
            {
                prioritizations.Remove(item);
            }
            return prioritizations;
        }

        public int CompareTo(Habitat other)
        {
            if (StrategicValue == other.StrategicValue)
            {
                if (Population != null && Population.Count > 0 && other.Population != null && other.Population.Count > 0)
                {
                    return Population.TotalAmount.CompareTo(other.Population.TotalAmount);
                }
                return 0;
            }
            return StrategicValue.CompareTo(other.StrategicValue);
        }

        int IComparable<Habitat>.CompareTo(Habitat other)
        {
            if (StrategicValue == other.StrategicValue)
            {
                if (Population != null && Population.Count > 0 && other.Population != null && other.Population.Count > 0)
                {
                    return Population.TotalAmount.CompareTo(other.Population.TotalAmount);
                }
                return 0;
            }
            return StrategicValue.CompareTo(other.StrategicValue);
        }

        int IComparable<StellarObject>.CompareTo(StellarObject other)
        {
            throw new NotImplementedException();
            //return SortTag.CompareTo(other.SortTag);
        }

        int IComparable<BuiltObject>.CompareTo(BuiltObject other)
        {
            throw new NotImplementedException();
            //return SortTag.CompareTo(other.SortTag);
        }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
            //if (obj == this)
            //{
            //    return 0;
            //}
            //if (obj == null)
            //{
            //    return 1;
            //}
            //if (obj is BuiltObject)
            //{
            //    return SortTag.CompareTo(((BuiltObject)obj).SortTag);
            //}
            //if (obj is Habitat)
            //{
            //    return SortTag.CompareTo(((Habitat)obj).SortTag);
            //}
            //return 0;
        }
    }
}

