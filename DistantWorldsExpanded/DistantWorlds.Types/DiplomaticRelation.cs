// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DiplomaticRelation
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DiplomaticRelation : IComparable<DiplomaticRelation>
  {
    private DiplomaticRelationType _Type;
    private Empire _ThisEmpire;
    private Empire _OtherEmpire;
    private Empire _Initiator;
    private long _LastDiplomacyTradeOfferDate;
    private long _LastGiftDate;
    private long _StartDateOfLastChange;
    [OptionalField]
    private long _LastTradeDealOfferDate;
    private int _WarDamageBuiltObject;
    private int _WarDamageColony;
    [OptionalField]
    public bool Locked;
    public string AllianceName = string.Empty;
    private YearlyTradeValueList _TradeValues = new YearlyTradeValueList();
    private long _YearLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000);
    private int _YearsStored = 3;
    private double _TradeBonus;
    private bool _SupplyRestrictedResources;
    private bool _MilitaryRefuelingToOther;
    private bool _MiningRightsToOther;
    public DiplomaticStrategy Strategy;
    private WarObjective _WarObjective;
    private HabitatList _WarObjectiveColonies = new HabitatList();
    private BuiltObjectList _WarObjectiveBases = new BuiltObjectList();
    //[NonSerialized]
    //public double SortTag;

    public WarObjective WarObjective
    {
      get => this._WarObjective;
      set => this._WarObjective = value;
    }

    public HabitatList WarObjectiveColonies
    {
      get => this._WarObjectiveColonies;
      set => this._WarObjectiveColonies = value;
    }

    public BuiltObjectList WarObjectiveBases
    {
      get => this._WarObjectiveBases;
      set => this._WarObjectiveBases = value;
    }

    public bool MilitaryRefuelingToOther
    {
      get => this._MilitaryRefuelingToOther;
      set => this._MilitaryRefuelingToOther = value;
    }

    public bool MiningRightsToOther
    {
      get => this._MiningRightsToOther;
      set => this._MiningRightsToOther = value;
    }

    public bool SupplyRestrictedResources
    {
      get => this._SupplyRestrictedResources;
      set => this._SupplyRestrictedResources = value;
    }

    public double TradeBonus
    {
      get => this._TradeBonus;
      set => this._TradeBonus = value;
    }

    public int YearsStored => this._YearsStored;

    public int WarDamageTotal => this._WarDamageBuiltObject + this._WarDamageColony;

    public int WarDamageBuiltObject
    {
      get => this._WarDamageBuiltObject;
      set => this._WarDamageBuiltObject = value;
    }

    public int WarDamageColony
    {
      get => this._WarDamageColony;
      set => this._WarDamageColony = value;
    }

    public void PerformTradeTransaction(double value, long starDate)
    {
      if (this._ThisEmpire != null && this._ThisEmpire.Counters != null)
      {
        this._ThisEmpire.Counters.ProcessTradeBonus(this, value);
        if (this._ThisEmpire.Characters != null)
          this._ThisEmpire.Galaxy.DoCharacterEvent(CharacterEventType.TradeIncome, (object) null, this._ThisEmpire.Characters.GetAmbassadorsForEmpire(this._OtherEmpire), true, this._ThisEmpire);
      }
      starDate = this._ThisEmpire.Galaxy.CurrentStarDate;
      this.AgeTradeValues(starDate);
      YearlyTradeValue byYear = this._TradeValues.GetByYear(this.CalculateStartOfYear(starDate));
      if (byYear == null)
        return;
      byYear.Value += value;
    }

    private long CalculateStartOfYear(long date)
    {
      long num = date % this._YearLength;
      return date - num;
    }

    public void AgeTradeValues(long currentStarDate)
    {
      long startOfYear = this.CalculateStartOfYear(currentStarDate);
      long num = startOfYear - this._YearLength * (long) this._YearsStored;
      YearlyTradeValueList yearlyTradeValueList = new YearlyTradeValueList();
      for (int index = 0; index < this._TradeValues.Count; ++index)
      {
        YearlyTradeValue tradeValue = this._TradeValues[index];
        if (tradeValue != null && tradeValue.Year < num)
          yearlyTradeValueList.Add(tradeValue);
      }
      for (int index = 0; index < yearlyTradeValueList.Count; ++index)
        this._TradeValues.Remove(yearlyTradeValueList[index]);
      if (this._TradeValues.GetByYear(startOfYear) != null)
        return;
      this._TradeValues.Add(new YearlyTradeValue(startOfYear));
    }

    public double AnnualTradeBonus
    {
      get
      {
        double val1 = 0.0;
        switch (this.Type)
        {
          case DiplomaticRelationType.FreeTradeAgreement:
            double maximumFreeTrade = Galaxy.TradeBonusMaximumFreeTrade;
            val1 = Galaxy.TradeBonusMaximumFreeTradeAmount / maximumFreeTrade;
            break;
          case DiplomaticRelationType.MutualDefensePact:
          case DiplomaticRelationType.Protectorate:
            double maximumMutualDefense = Galaxy.TradeBonusMaximumMutualDefense;
            val1 = Galaxy.TradeBonusMaximumMutualDefenseAmount / maximumMutualDefense;
            break;
        }
        return this.TradeBonus * Math.Min(val1, this.NormalizedAnnualTradeValue);
      }
    }

    public double NormalizedAnnualTradeValue => this.TotalTradeValue / (double) Math.Max(1, this.YearsTradeVolumeStored);

    private int YearsTradeVolumeStored
    {
      get
      {
        int tradeVolumeStored = 0;
        if (this._TradeValues != null)
        {
          for (int index = 0; index < this._TradeValues.Count; ++index)
          {
            YearlyTradeValue tradeValue = this._TradeValues[index];
            if (tradeValue != null && tradeValue.Value >= 0.0)
              ++tradeVolumeStored;
          }
        }
        return tradeVolumeStored;
      }
    }

    public double TotalTradeValue
    {
      get
      {
        double totalTradeValue = 0.0;
        if (this._TradeValues != null)
        {
          for (int index = 0; index < this._TradeValues.Count; ++index)
          {
            YearlyTradeValue tradeValue = this._TradeValues[index];
            if (tradeValue != null && tradeValue.Value >= 0.0)
              totalTradeValue += tradeValue.Value;
          }
        }
        return totalTradeValue;
      }
    }

    public long LastDiplomacyTradeOfferDate
    {
      get => this._LastDiplomacyTradeOfferDate;
      set => this._LastDiplomacyTradeOfferDate = value;
    }

    public long LastGiftDate
    {
      get => this._LastGiftDate;
      set => this._LastGiftDate = value;
    }

    public long StartDateOfLastChange
    {
      get => this._StartDateOfLastChange;
      set => this._StartDateOfLastChange = value;
    }

    public long LastTradeDealOfferDate
    {
      get => this._LastTradeDealOfferDate;
      set => this._LastTradeDealOfferDate = value;
    }

    public DiplomaticRelation(
      DiplomaticRelationType type,
      Empire initiator,
      Empire thisEmpire,
      Empire otherEmpire,
      long proposalStarDate,
      bool tradeRestrictedResources)
    {
      this._Type = type;
      this._Initiator = initiator;
      this._ThisEmpire = thisEmpire;
      this._OtherEmpire = otherEmpire;
      this._LastDiplomacyTradeOfferDate = proposalStarDate;
      this._TradeBonus = 0.0;
      this._SupplyRestrictedResources = tradeRestrictedResources;
    }

    public DiplomaticRelation(
      DiplomaticRelationType type,
      Empire initiator,
      Empire thisEmpire,
      Empire otherEmpire,
      bool tradeRestrictedResources)
    {
      this._Type = type;
      this._Initiator = initiator;
      this._ThisEmpire = thisEmpire;
      this._OtherEmpire = otherEmpire;
      this._TradeBonus = 0.0;
      this._SupplyRestrictedResources = tradeRestrictedResources;
    }

    public DiplomaticRelation CloneLightWeight(DiplomaticRelationType newRelationType) => new DiplomaticRelation(newRelationType, this.Initiator, this.ThisEmpire, this.OtherEmpire, this._SupplyRestrictedResources);

    public DiplomaticRelationType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public Empire Initiator
    {
      get => this._Initiator;
      set => this._Initiator = value;
    }

    public Empire ThisEmpire
    {
      get => this._ThisEmpire;
      set => this._ThisEmpire = value;
    }

    public Empire OtherEmpire
    {
      get => this._OtherEmpire;
      set => this._OtherEmpire = value;
    }

    int IComparable<DiplomaticRelation>.CompareTo(DiplomaticRelation other) => throw new NotImplementedException();//  this.SortTag.CompareTo(other.SortTag);
    }
}
