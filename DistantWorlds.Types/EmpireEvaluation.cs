// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireEvaluation
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireEvaluation
  {
    private Empire _Empire;
    private double _IncidentEvaluation;
    private int _SystemCompetition;
    private int _TradeVolume;
    private int _RelationshipWithFriendsPositive;
    private int _RelationshipWithFriendsNegative;
    private int _Covetousness;
    private int _Blockades;
    private int _GovernmentStyleAffinity;
    private int _MilitaryForcesInSystems;
    private int _Envy;
    private double _Bias;
    private double _RestrictedResourceTrading;
    private int _MilitaryRefueling;
    private int _MiningRights;
    private double _RacialOffense;
    private double _SlaveryOffense;
    private double _DiplomacyFactor = 1.0;
    private long _LastSystemWarningDate;
    private int _LastSystemWarningIndex = -1;
    private double _CivilityRatingWeight = 0.5;
    public double FirstContactPenalty = EmpireEvaluation.FirstContactPenaltyStartAmount;
    private double _SystemCompetitionCumulative;
    private double _RelationshipWithFriendsPositiveCumulative;
    private double _RelationshipWithFriendsNegativeCumulative;
    private double _CovetousnessCumulative;
    private double _GovernmentStyleAffinityCumulative;
    public static double SystemCompetitionCap = 20.0;
    public static double SystemCompetitionCapExtended = 30.0;
    public static double RelationshipWithFriendsCap = 10.0;
    public static double CovetousnessCap = 25.0;
    public static double GovernmentStyleAffinityCap = 12.0;
    public static double IncidentEvaluationCap = 80.0;
    public static double IncidentEvaluationCapNegative = -150.0;
    public static double RestrictedResourceTradingCap = 10.0;
    public static double FirstContactPenaltyStartAmount = -15.0;
    public static double FirstContactPenaltyAnnualReductionAmount = 6.0;

    public double CivilityRatingWeight
    {
      get => this._CivilityRatingWeight;
      set => this._CivilityRatingWeight = value;
    }

    public void SetSlaveryOffense(double slaveryOffense) => this._SlaveryOffense = slaveryOffense;

    public double SlaveryOffense => this._SlaveryOffense;

    public double RacialOffense
    {
      get => this._RacialOffense;
      set => this._RacialOffense = value;
    }

    public void Clear() => this._Empire = (Empire) null;

    public EmpireEvaluation(Empire empire, Galaxy galaxy)
    {
      this._Empire = empire;
      this._IncidentEvaluation = 0.0;
      this._SystemCompetition = 0;
      this._TradeVolume = 0;
      this._RelationshipWithFriendsPositive = 0;
      this._RelationshipWithFriendsNegative = 0;
      this._Bias = 0.0;
      this._RestrictedResourceTrading = 0.0;
      this.FirstContactPenalty = EmpireEvaluation.FirstContactPenaltyStartAmount * galaxy.AggressionLevel;
    }

    public Empire Empire => this._Empire;

    public double DiplomacyFactor
    {
      get => this._DiplomacyFactor;
      set => this._DiplomacyFactor = value;
    }

    public int OverallAttitude
    {
      get
      {
        double num1 = 0.0;
        double incidentEvaluation = this._IncidentEvaluation;
        double num2 = incidentEvaluation <= 0.0 ? incidentEvaluation * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : incidentEvaluation / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num3 = num1 + num2;
        double competitionCumulative = this._SystemCompetitionCumulative;
        double num4 = competitionCumulative <= 0.0 ? competitionCumulative * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : competitionCumulative / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num5 = num3 + num4;
        double tradeVolume = (double) this._TradeVolume;
        double num6 = tradeVolume <= 0.0 ? tradeVolume * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : tradeVolume / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num7 = num5 + num6;
        double positiveCumulative = this._RelationshipWithFriendsPositiveCumulative;
        double num8 = positiveCumulative <= 0.0 ? positiveCumulative * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : positiveCumulative / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num9 = num7 + num8;
        double negativeCumulative = this._RelationshipWithFriendsNegativeCumulative;
        double num10 = negativeCumulative <= 0.0 ? negativeCumulative * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : negativeCumulative / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num11 = num9 + num10;
        double covetousnessCumulative = this._CovetousnessCumulative;
        double num12 = covetousnessCumulative <= 0.0 ? covetousnessCumulative * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : covetousnessCumulative / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num13 = num11 + num12;
        double blockades = (double) this._Blockades;
        double num14 = blockades <= 0.0 ? blockades * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : blockades / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num15 = num13 + num14;
        double affinityCumulative = this._GovernmentStyleAffinityCumulative;
        double num16 = affinityCumulative <= 0.0 ? affinityCumulative * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : affinityCumulative / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num17 = num15 + num16;
        double militaryForcesInSystems = (double) this._MilitaryForcesInSystems;
        double num18 = militaryForcesInSystems <= 0.0 ? militaryForcesInSystems * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : militaryForcesInSystems / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num19 = num17 + num18;
        double restrictedResourceTrading = this._RestrictedResourceTrading;
        double num20 = restrictedResourceTrading <= 0.0 ? restrictedResourceTrading * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : restrictedResourceTrading / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num21 = num19 + num20;
        double envy = (double) this._Envy;
        double num22 = envy <= 0.0 ? envy * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : envy / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num23 = num21 + num22;
        double militaryRefueling = (double) this._MilitaryRefueling;
        double num24 = militaryRefueling <= 0.0 ? militaryRefueling * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : militaryRefueling / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num25 = num23 + num24;
        double miningRights = (double) this._MiningRights;
        double num26 = miningRights <= 0.0 ? miningRights * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : miningRights / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num27 = num25 + num26;
        double racialOffense = this._RacialOffense;
        double num28 = racialOffense <= 0.0 ? racialOffense * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : racialOffense / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num29 = num27 + num28;
        double slaveryOffense = this._SlaveryOffense;
        double num30 = slaveryOffense <= 0.0 ? slaveryOffense * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : slaveryOffense / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num31 = num29 + num30;
        double firstContactPenalty = this.FirstContactPenalty;
        double num32 = firstContactPenalty <= 0.0 ? firstContactPenalty * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : firstContactPenalty / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num33 = num31 + num32;
        double reputationWeighted = this.ReputationWeighted;
        double num34 = reputationWeighted <= 0.0 ? reputationWeighted * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : reputationWeighted / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        double num35 = num33 + num34;
        double bias = this._Bias;
        double num36 = bias <= 0.0 ? bias * this._Empire.Galaxy.AggressionLevel / this._DiplomacyFactor : bias / this._Empire.Galaxy.AggressionLevel * this._DiplomacyFactor;
        return (int) (num35 + num36);
      }
    }

    public int OverallAttitudeWithoutSystemCompetition
    {
      get
      {
        double num1 = this._IncidentEvaluation + (double) this._TradeVolume + this._RelationshipWithFriendsPositiveCumulative + this._RelationshipWithFriendsNegativeCumulative + this._CovetousnessCumulative + (double) this._Blockades + this._GovernmentStyleAffinityCumulative + (double) this._MilitaryForcesInSystems + this._RestrictedResourceTrading + (double) this._Envy + (double) this._MilitaryRefueling + (double) this._MiningRights + this._RacialOffense + this.FirstContactPenalty + this.ReputationWeighted + this._Bias;
        double num2 = num1 <= 0.0 ? num1 * this._Empire.Galaxy.AggressionLevel : num1 / this._Empire.Galaxy.AggressionLevel;
        return num2 <= 0.0 ? (int) (num2 / this._DiplomacyFactor) : (int) (num2 * this._DiplomacyFactor);
      }
    }

    public double ReputationWeighted
    {
      get
      {
        double num = Math.Sqrt(Math.Min(4.0, this._Empire.RelativeEmpireSize));
        double reputationWeighted = this._Empire.CivilityRating * this._CivilityRatingWeight;
        if (reputationWeighted < 0.0)
          reputationWeighted *= num;
        return reputationWeighted;
      }
    }

    public int GovernmentStyleAffinity
    {
      get => this._GovernmentStyleAffinity;
      set => this._GovernmentStyleAffinity = value;
    }

    public int Blockades
    {
      get => this._Blockades;
      set => this._Blockades = value;
    }

    public int Covetousness
    {
      get => this._Covetousness;
      set => this._Covetousness = value;
    }

    public double IncidentEvaluationRaw => this._IncidentEvaluation;

    public double IncidentEvaluation
    {
      get
      {
        double incidentEvaluation = this._IncidentEvaluation;
        return incidentEvaluation <= 0.0 ? incidentEvaluation / this._DiplomacyFactor : incidentEvaluation * this._DiplomacyFactor;
      }
      set
      {
        this._IncidentEvaluation = value;
        if (this._IncidentEvaluation > EmpireEvaluation.IncidentEvaluationCap)
        {
          this._IncidentEvaluation = EmpireEvaluation.IncidentEvaluationCap;
        }
        else
        {
          if (this._IncidentEvaluation >= EmpireEvaluation.IncidentEvaluationCapNegative)
            return;
          this._IncidentEvaluation = EmpireEvaluation.IncidentEvaluationCapNegative;
        }
      }
    }

    public int RelationshipWithFriendsPositive
    {
      get => this._RelationshipWithFriendsPositive;
      set => this._RelationshipWithFriendsPositive = value;
    }

    public int RelationshipWithFriendsNegative
    {
      get => this._RelationshipWithFriendsNegative;
      set => this._RelationshipWithFriendsNegative = value;
    }

    public int TradeVolume
    {
      get => this._TradeVolume;
      set => this._TradeVolume = value;
    }

    public int SystemCompetition
    {
      get => this._SystemCompetition;
      set => this._SystemCompetition = value;
    }

    public double SystemCompetitionCumulative
    {
      get => this._SystemCompetitionCumulative;
      set => this._SystemCompetitionCumulative = value;
    }

    public int MilitaryRefueling
    {
      get => this._MilitaryRefueling;
      set => this._MilitaryRefueling = value;
    }

    public int MiningRights
    {
      get => this._MiningRights;
      set => this._MiningRights = value;
    }

    public long LastSystemWarningDate
    {
      get => this._LastSystemWarningDate;
      set => this._LastSystemWarningDate = value;
    }

    public int LastSystemWarningIndex
    {
      get => this._LastSystemWarningIndex;
      set => this._LastSystemWarningIndex = value;
    }

    public double RelationshipWithFriendsPositiveCumulative
    {
      get => this._RelationshipWithFriendsPositiveCumulative;
      set => this._RelationshipWithFriendsPositiveCumulative = value;
    }

    public double RelationshipWithFriendsNegativeCumulative
    {
      get => this._RelationshipWithFriendsNegativeCumulative;
      set => this._RelationshipWithFriendsNegativeCumulative = value;
    }

    public double CovetousnessCumulative
    {
      get => this._CovetousnessCumulative;
      set => this._CovetousnessCumulative = value;
    }

    public double GovernmentStyleAffinityCumulative
    {
      get => this._GovernmentStyleAffinityCumulative;
      set => this._GovernmentStyleAffinityCumulative = value;
    }

    public double BiasRaw => this._Bias;

    public double Bias
    {
      get
      {
        double bias = this._Bias;
        return bias <= 0.0 ? bias / this._DiplomacyFactor : bias * this._DiplomacyFactor;
      }
      set => this._Bias = value;
    }

    public int Envy
    {
      get => this._Envy;
      set => this._Envy = value;
    }

    public int MilitaryForcesInSystems
    {
      get => this._MilitaryForcesInSystems;
      set => this._MilitaryForcesInSystems = value;
    }

    public double RestrictedResourceTrading
    {
      get => this._RestrictedResourceTrading;
      set => this._RestrictedResourceTrading = value;
    }
  }
}
