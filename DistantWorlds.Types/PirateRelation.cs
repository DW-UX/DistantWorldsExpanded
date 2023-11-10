// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateRelation
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateRelation
  {
    public PirateRelationType Type;
    private Empire _ThisEmpire;
    private int _ThisEmpireId = -1;
    private Empire _OtherEmpire;
    private int _OtherEmpireId = -1;
    public float EvaluationGifts;
    public float EvaluationOffenseOverRequests;
    public float EvaluationDetectedIntelligenceMissions;
    public float EvaluationPirateMissionsSucceed;
    public float EvaluationPirateMissionsFail;
    public float EvaluationShipAttacks;
    public float EvaluationProtectionCancelled;
    public float EvaluationCovetedColonies;
    public float EvaluationLongRelationship;
    public float EvaluationRaidsAgainstOurColonies;
    public float DiplomacyFactor = 1f;
    public long LastChangeDate;
    public long LastOfferDate;
    public long LastInfoDate;
    public double MonthlyProtectionFeeToThisEmpire;
    public long LastProtectionFeePaymentDate;

    internal PirateRelation(int thisEmpireId, int otherEmpireId, PirateRelationType relationType)
    {
      this._ThisEmpireId = thisEmpireId;
      this._OtherEmpireId = otherEmpireId;
      this.Type = relationType;
    }

    public PirateRelation(Empire thisEmpire, Empire otherEmpire, PirateRelationType relationType)
    {
      this.ThisEmpire = thisEmpire;
      this.OtherEmpire = otherEmpire;
      this.Type = relationType;
    }

    public long RelationshipLength(long starDate) => starDate - this.LastChangeDate;

    public float CalculateOffenseOverCancellingProtection(long starDate)
    {
      float num1 = 5f;
      long num2 = this.RelationshipLength(starDate);
      long num3 = (long) ((double) Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.5);
      if (num2 < num3)
      {
        double num4 = 1.0 - (double) num2 / (double) num3;
        num1 = Math.Max(5f, Math.Min(20f, num1 + (float) ((double) num1 * num4 * 3.0)));
      }
      return num1 * -1f;
    }

    public float EvaluationCovetedColoniesFactored
    {
      get
      {
        float evaluationCovetedColonies = this.EvaluationCovetedColonies;
        return (double) evaluationCovetedColonies >= 0.0 ? evaluationCovetedColonies * this.DiplomacyFactor : evaluationCovetedColonies / this.DiplomacyFactor;
      }
    }

    public float EvaluationDetectedIntelligenceMissionsFactored
    {
      get
      {
        float intelligenceMissions = this.EvaluationDetectedIntelligenceMissions;
        return (double) intelligenceMissions >= 0.0 ? intelligenceMissions * this.DiplomacyFactor : intelligenceMissions / this.DiplomacyFactor;
      }
    }

    public float EvaluationGiftsFactored
    {
      get
      {
        float evaluationGifts = this.EvaluationGifts;
        return (double) evaluationGifts >= 0.0 ? evaluationGifts * this.DiplomacyFactor : evaluationGifts / this.DiplomacyFactor;
      }
    }

    public float EvaluationOffenseOverRequestsFactored
    {
      get
      {
        float offenseOverRequests = this.EvaluationOffenseOverRequests;
        return (double) offenseOverRequests >= 0.0 ? offenseOverRequests * this.DiplomacyFactor : offenseOverRequests / this.DiplomacyFactor;
      }
    }

    public float EvaluationPirateMissionsFailFactored
    {
      get
      {
        float pirateMissionsFail = this.EvaluationPirateMissionsFail;
        return (double) pirateMissionsFail >= 0.0 ? pirateMissionsFail * this.DiplomacyFactor : pirateMissionsFail / this.DiplomacyFactor;
      }
    }

    public float EvaluationPirateMissionsSucceedFactored
    {
      get
      {
        float pirateMissionsSucceed = this.EvaluationPirateMissionsSucceed;
        return (double) pirateMissionsSucceed >= 0.0 ? pirateMissionsSucceed * this.DiplomacyFactor : pirateMissionsSucceed / this.DiplomacyFactor;
      }
    }

    public float EvaluationProtectionCancelledFactored
    {
      get
      {
        float protectionCancelled = this.EvaluationProtectionCancelled;
        return (double) protectionCancelled >= 0.0 ? protectionCancelled * this.DiplomacyFactor : protectionCancelled / this.DiplomacyFactor;
      }
    }

    public float EvaluationShipAttacksFactored
    {
      get
      {
        float evaluationShipAttacks = this.EvaluationShipAttacks;
        return (double) evaluationShipAttacks >= 0.0 ? evaluationShipAttacks * this.DiplomacyFactor : evaluationShipAttacks / this.DiplomacyFactor;
      }
    }

    public float EvaluationLongRelationshipFactored
    {
      get
      {
        float longRelationship = this.EvaluationLongRelationship;
        return (double) longRelationship >= 0.0 ? longRelationship * this.DiplomacyFactor : longRelationship / this.DiplomacyFactor;
      }
    }

    public float EvaluationRaidsAgainstOurColoniesFactored
    {
      get
      {
        float againstOurColonies = this.EvaluationRaidsAgainstOurColonies;
        return (double) againstOurColonies >= 0.0 ? againstOurColonies * this.DiplomacyFactor : againstOurColonies / this.DiplomacyFactor;
      }
    }

    public void NeutralizeEvaluation(float neutralizationAmount)
    {
      int num1 = 0;
      if ((double) this.EvaluationDetectedIntelligenceMissions != 0.0)
        ++num1;
      if ((double) this.EvaluationGifts != 0.0)
        ++num1;
      if ((double) this.EvaluationOffenseOverRequests != 0.0)
        ++num1;
      if ((double) this.EvaluationPirateMissionsSucceed != 0.0)
        ++num1;
      if ((double) this.EvaluationPirateMissionsFail != 0.0)
        ++num1;
      if ((double) this.EvaluationProtectionCancelled != 0.0)
        ++num1;
      if ((double) this.EvaluationShipAttacks != 0.0)
        ++num1;
      if ((double) this.EvaluationCovetedColonies != 0.0)
        ++num1;
      if ((double) this.EvaluationRaidsAgainstOurColonies != 0.0)
        ++num1;
      float num2 = neutralizationAmount / (float) num1;
      if ((double) this.EvaluationDetectedIntelligenceMissions > 0.0)
      {
        this.EvaluationDetectedIntelligenceMissions -= num2;
        this.EvaluationDetectedIntelligenceMissions = Math.Max(0.0f, this.EvaluationDetectedIntelligenceMissions);
      }
      else if ((double) this.EvaluationDetectedIntelligenceMissions < 0.0)
      {
        this.EvaluationDetectedIntelligenceMissions += num2;
        this.EvaluationDetectedIntelligenceMissions = Math.Min(0.0f, this.EvaluationDetectedIntelligenceMissions);
      }
      if ((double) this.EvaluationGifts > 0.0)
      {
        this.EvaluationGifts -= num2;
        this.EvaluationGifts = Math.Max(0.0f, this.EvaluationGifts);
      }
      else if ((double) this.EvaluationGifts < 0.0)
      {
        this.EvaluationGifts += num2;
        this.EvaluationGifts = Math.Min(0.0f, this.EvaluationGifts);
      }
      if ((double) this.EvaluationOffenseOverRequests > 0.0)
      {
        this.EvaluationOffenseOverRequests -= num2;
        this.EvaluationOffenseOverRequests = Math.Max(0.0f, this.EvaluationOffenseOverRequests);
      }
      else if ((double) this.EvaluationOffenseOverRequests < 0.0)
      {
        this.EvaluationOffenseOverRequests += num2;
        this.EvaluationOffenseOverRequests = Math.Min(0.0f, this.EvaluationOffenseOverRequests);
      }
      if ((double) this.EvaluationPirateMissionsSucceed > 0.0)
      {
        this.EvaluationPirateMissionsSucceed -= num2;
        this.EvaluationPirateMissionsSucceed = Math.Max(0.0f, this.EvaluationPirateMissionsSucceed);
      }
      else if ((double) this.EvaluationPirateMissionsSucceed < 0.0)
      {
        this.EvaluationPirateMissionsSucceed += num2;
        this.EvaluationPirateMissionsSucceed = Math.Min(0.0f, this.EvaluationPirateMissionsSucceed);
      }
      if ((double) this.EvaluationPirateMissionsFail > 0.0)
      {
        this.EvaluationPirateMissionsFail -= num2;
        this.EvaluationPirateMissionsFail = Math.Max(0.0f, this.EvaluationPirateMissionsFail);
      }
      else if ((double) this.EvaluationPirateMissionsFail < 0.0)
      {
        this.EvaluationPirateMissionsFail += num2;
        this.EvaluationPirateMissionsFail = Math.Min(0.0f, this.EvaluationPirateMissionsFail);
      }
      if ((double) this.EvaluationProtectionCancelled > 0.0)
      {
        this.EvaluationProtectionCancelled -= num2;
        this.EvaluationProtectionCancelled = Math.Max(0.0f, this.EvaluationProtectionCancelled);
      }
      else if ((double) this.EvaluationProtectionCancelled < 0.0)
      {
        this.EvaluationProtectionCancelled += num2;
        this.EvaluationProtectionCancelled = Math.Min(0.0f, this.EvaluationProtectionCancelled);
      }
      if ((double) this.EvaluationShipAttacks > 0.0)
      {
        this.EvaluationShipAttacks -= num2;
        this.EvaluationShipAttacks = Math.Max(0.0f, this.EvaluationShipAttacks);
      }
      else if ((double) this.EvaluationShipAttacks < 0.0)
      {
        this.EvaluationShipAttacks += num2;
        this.EvaluationShipAttacks = Math.Min(0.0f, this.EvaluationShipAttacks);
      }
      if ((double) this.EvaluationCovetedColonies > 0.0)
      {
        this.EvaluationCovetedColonies -= num2;
        this.EvaluationCovetedColonies = Math.Max(0.0f, this.EvaluationCovetedColonies);
      }
      else if ((double) this.EvaluationCovetedColonies < 0.0)
      {
        this.EvaluationCovetedColonies += num2;
        this.EvaluationCovetedColonies = Math.Min(0.0f, this.EvaluationCovetedColonies);
      }
      if ((double) this.EvaluationRaidsAgainstOurColonies > 0.0)
      {
        this.EvaluationRaidsAgainstOurColonies -= num2;
        this.EvaluationRaidsAgainstOurColonies = Math.Max(0.0f, this.EvaluationRaidsAgainstOurColonies);
      }
      else
      {
        if ((double) this.EvaluationRaidsAgainstOurColonies >= 0.0)
          return;
        this.EvaluationRaidsAgainstOurColonies += num2;
        this.EvaluationRaidsAgainstOurColonies = Math.Min(0.0f, this.EvaluationRaidsAgainstOurColonies);
      }
    }

    public float Evaluation
    {
      get
      {
        float num = this.EvaluationGifts + this.EvaluationOffenseOverRequests + this.EvaluationDetectedIntelligenceMissions + this.EvaluationLongRelationship + this.EvaluationPirateMissionsSucceed + this.EvaluationPirateMissionsFail + this.EvaluationProtectionCancelled + this.EvaluationShipAttacks + this.EvaluationCovetedColonies + this.EvaluationRaidsAgainstOurColonies;
        return (double) num >= 0.0 ? num * this.DiplomacyFactor : num / this.DiplomacyFactor;
      }
    }

    public Empire ThisEmpire
    {
      get => this._ThisEmpire;
      set
      {
        this._ThisEmpire = value;
        if (this._ThisEmpire != null)
          this._ThisEmpireId = this._ThisEmpire.EmpireId;
        else
          this._ThisEmpireId = -1;
      }
    }

    internal int ThisEmpireId => this._ThisEmpireId;

    public Empire OtherEmpire
    {
      get => this._OtherEmpire;
      set
      {
        this._OtherEmpire = value;
        if (this._OtherEmpire != null)
          this._OtherEmpireId = this._OtherEmpire.EmpireId;
        else
          this._OtherEmpireId = -1;
      }
    }

    internal int OtherEmpireId => this._OtherEmpireId;

    public void FixupEmpires(Galaxy galaxy)
    {
      if (galaxy != null && galaxy.Empires != null && galaxy.PirateEmpires != null)
      {
        if (galaxy.IndependentEmpire != null)
        {
          if (galaxy.IndependentEmpire.EmpireId == this._ThisEmpireId)
            this._ThisEmpire = galaxy.IndependentEmpire;
          if (galaxy.IndependentEmpire.EmpireId == this._OtherEmpireId)
            this._OtherEmpire = galaxy.IndependentEmpire;
        }
        for (int index = 0; index < galaxy.Empires.Count; ++index)
        {
          Empire empire = galaxy.Empires[index];
          if (empire != null && empire.Active)
          {
            if (empire.EmpireId == this._ThisEmpireId)
              this._ThisEmpire = empire;
            if (empire.EmpireId == this._OtherEmpireId)
              this._OtherEmpire = empire;
          }
        }
        for (int index = 0; index < galaxy.PirateEmpires.Count; ++index)
        {
          Empire pirateEmpire = galaxy.PirateEmpires[index];
          if (pirateEmpire != null && pirateEmpire.Active)
          {
            if (pirateEmpire.EmpireId == this._ThisEmpireId)
              this._ThisEmpire = pirateEmpire;
            if (pirateEmpire.EmpireId == this._OtherEmpireId)
              this._OtherEmpire = pirateEmpire;
          }
        }
      }
      if (this._ThisEmpire == null)
        this._ThisEmpireId = -1;
      if (this._OtherEmpire != null)
        return;
      this._OtherEmpireId = -1;
    }
  }
}
