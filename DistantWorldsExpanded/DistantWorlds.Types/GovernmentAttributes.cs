// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GovernmentAttributes
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GovernmentAttributes : IComparable<GovernmentAttributes>
  {
    public int GovernmentId;
    public string Name;
    private double _WarWeariness;
    private double _MaintenanceCosts;
    private double _ApprovalRating;
    private double _PopulationGrowth;
    private double _ResearchSpeed;
    private double _TroopRecruitment;
    private double _Corruption;
    private double _TradeBonus;
    public double LeaderReplacementLikeliness = 1.0;
    public double LeaderReplacementDisruptionLevel = 1.0;
    public double LeaderReplacementBoost = 1.0;
    public int LeaderReplacementCharacterPool;
    public int LeaderReplacementTypicalManner;
    public double Stability = 1.0;
    public double ConcernForOwnReputation = 1.0;
    public double ImportanceOfOthersReputations = 1.0;
    public int SpecialFunctionCode;
    public int Availability;
    public List<string> EmpireNameAdjectives = new List<string>();
    public List<string> EmpireNameNouns = new List<string>();
    public GovernmentBiasList Biases = new GovernmentBiasList();
    [NonSerialized]
    public float SortTag;

    public GovernmentAttributes(
      int governmentId,
      string name,
      double corruption,
      double warWeariness,
      double maintenanceCosts,
      double approvalRating,
      double populationGrowth,
      double researchSpeed,
      double troopRecruitment,
      double tradeBonus,
      double leaderReplacementLikeliness,
      double leaderReplacementDisruptionLevel,
      double leaderReplacementBoost,
      int leaderReplacementCharacterPool,
      int leaderReplacementTypicalManner,
      double stability,
      double concernForOwnReputation,
      double importanceOfOthersReputations,
      int specialFunctionCode,
      int availability,
      List<string> empireNameAdjectives,
      List<string> empireNameNouns)
    {
      this.GovernmentId = governmentId;
      this.Name = name;
      this._Corruption = corruption;
      this._WarWeariness = warWeariness;
      this._MaintenanceCosts = maintenanceCosts;
      this._ApprovalRating = approvalRating;
      this._PopulationGrowth = populationGrowth;
      this._ResearchSpeed = researchSpeed;
      this._TroopRecruitment = troopRecruitment;
      this._TradeBonus = tradeBonus;
      this.LeaderReplacementLikeliness = leaderReplacementLikeliness;
      this.LeaderReplacementDisruptionLevel = leaderReplacementDisruptionLevel;
      this.LeaderReplacementBoost = leaderReplacementBoost;
      this.LeaderReplacementCharacterPool = leaderReplacementCharacterPool;
      this.LeaderReplacementTypicalManner = leaderReplacementTypicalManner;
      this.Stability = stability;
      this.ConcernForOwnReputation = concernForOwnReputation;
      this.ImportanceOfOthersReputations = importanceOfOthersReputations;
      this.SpecialFunctionCode = specialFunctionCode;
      this.Availability = availability;
      this.EmpireNameAdjectives = empireNameAdjectives;
      this.EmpireNameNouns = empireNameNouns;
    }

    public double WarWeariness => this._WarWeariness;

    public double MaintenanceCosts => this._MaintenanceCosts;

    public double ApprovalRating => this._ApprovalRating;

    public double PopulationGrowth => this._PopulationGrowth;

    public double ResearchSpeed => this._ResearchSpeed;

    public double TroopRecruitment => this._TroopRecruitment;

    public double Corruption => this._Corruption;

    public double TradeBonus => this._TradeBonus;

    public double NaturalAffinity(int governmentId) => (double) this.Biases.GetBias(governmentId);

    int IComparable<GovernmentAttributes>.CompareTo(GovernmentAttributes other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.GovernmentId.CompareTo(other.GovernmentId);
  }
}
