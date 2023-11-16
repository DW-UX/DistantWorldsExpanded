// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateRelationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateRelationList : List<PirateRelation>, ISerializable
  {
    [NonSerialized]
    private object _LockObject = new object();
    [NonSerialized]
    private short[] _OtherEmpireIndexes = new short[(int) byte.MaxValue];

    public PirateRelationList() => this.ResetIndexes();

    public PirateRelationList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("PRL_D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num1 = (int) binaryReader.ReadInt16();
          for (int index = 0; index < num1; ++index)
          {
            PirateRelationType relationType = (PirateRelationType) binaryReader.ReadByte();
            int thisEmpireId = (int) binaryReader.ReadByte();
            int otherEmpireId = (int) binaryReader.ReadByte();
            float num2 = binaryReader.ReadSingle();
            float num3 = binaryReader.ReadSingle();
            float num4 = binaryReader.ReadSingle();
            float num5 = binaryReader.ReadSingle();
            float num6 = binaryReader.ReadSingle();
            float num7 = binaryReader.ReadSingle();
            float num8 = binaryReader.ReadSingle();
            float num9 = binaryReader.ReadSingle();
            float num10 = binaryReader.ReadSingle();
            float num11 = binaryReader.ReadSingle();
            double num12 = (double) binaryReader.ReadSingle();
            long num13 = binaryReader.ReadInt64();
            long num14 = binaryReader.ReadInt64();
            long num15 = binaryReader.ReadInt64();
            double num16 = binaryReader.ReadDouble();
            long num17 = binaryReader.ReadInt64();
            this.AddRaw(new PirateRelation(thisEmpireId, otherEmpireId, relationType)
            {
              EvaluationGifts = num2,
              EvaluationDetectedIntelligenceMissions = num3,
              EvaluationOffenseOverRequests = num4,
              EvaluationPirateMissionsSucceed = num5,
              EvaluationPirateMissionsFail = num6,
              EvaluationProtectionCancelled = num7,
              EvaluationShipAttacks = num8,
              EvaluationCovetedColonies = num9,
              EvaluationLongRelationship = num10,
              EvaluationRaidsAgainstOurColonies = num11,
              LastChangeDate = num13,
              LastOfferDate = num14,
              LastInfoDate = num15,
              MonthlyProtectionFeeToThisEmpire = num16,
              LastProtectionFeePaymentDate = num17
            });
          }
          binaryReader.Close();
        }
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this.Count > 0)
          {
            binaryWriter.Write((short) this.Count);
            for (int index = 0; index < this.Count; ++index)
            {
              PirateRelation pirateRelation = this[index];
              binaryWriter.Write((byte) pirateRelation.Type);
              binaryWriter.Write((byte) pirateRelation.ThisEmpireId);
              binaryWriter.Write((byte) pirateRelation.OtherEmpireId);
              binaryWriter.Write(pirateRelation.EvaluationGifts);
              binaryWriter.Write(pirateRelation.EvaluationDetectedIntelligenceMissions);
              binaryWriter.Write(pirateRelation.EvaluationOffenseOverRequests);
              binaryWriter.Write(pirateRelation.EvaluationPirateMissionsSucceed);
              binaryWriter.Write(pirateRelation.EvaluationPirateMissionsFail);
              binaryWriter.Write(pirateRelation.EvaluationProtectionCancelled);
              binaryWriter.Write(pirateRelation.EvaluationShipAttacks);
              binaryWriter.Write(pirateRelation.EvaluationCovetedColonies);
              binaryWriter.Write(pirateRelation.EvaluationLongRelationship);
              binaryWriter.Write(pirateRelation.EvaluationRaidsAgainstOurColonies);
              binaryWriter.Write(pirateRelation.DiplomacyFactor);
              binaryWriter.Write(pirateRelation.LastChangeDate);
              binaryWriter.Write(pirateRelation.LastOfferDate);
              binaryWriter.Write(pirateRelation.LastInfoDate);
              binaryWriter.Write(pirateRelation.MonthlyProtectionFeeToThisEmpire);
              binaryWriter.Write(pirateRelation.LastProtectionFeePaymentDate);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("PRL_D", (object) output.ToArray());
          }
          else
            info.AddValue("PRL_D", (object) new byte[0]);
        }
      }
    }

    public void FixupEmpires(Galaxy galaxy)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index] != null)
          {
            this[index].FixupEmpires(galaxy);
            if (this[index].OtherEmpireId >= 0 && this._OtherEmpireIndexes.Length > this[index].OtherEmpireId)
              this._OtherEmpireIndexes[(int) (byte) this[index].OtherEmpireId] = (short) index;
          }
        }
      }
    }

    private void RecalculateIndexes()
    {
      lock (this._LockObject)
      {
        this.ResetIndexes();
        for (int index = 0; index < this.Count; ++index)
        {
          PirateRelation pirateRelation = this[index];
          if (pirateRelation != null && pirateRelation.OtherEmpireId >= 0 && this._OtherEmpireIndexes.Length > pirateRelation.OtherEmpireId)
            this._OtherEmpireIndexes[(int) (byte) pirateRelation.OtherEmpireId] = (short) index;
        }
      }
    }

    public PirateRelation Add(PirateRelation pirateRelation)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation1 = this[index];
        if (pirateRelation1 != null && pirateRelation1.OtherEmpire == pirateRelation.OtherEmpire)
          return pirateRelation1;
      }
      this.AddRaw(pirateRelation);
      return pirateRelation;
    }

    public void AddRaw(PirateRelation pirateRelation)
    {
      lock (this._LockObject)
      {
        if (pirateRelation.OtherEmpireId < 0 || this._OtherEmpireIndexes.Length <= pirateRelation.OtherEmpireId)
          return;
        this._OtherEmpireIndexes[(int) (byte) pirateRelation.OtherEmpireId] = (short) this.Count;
        base.Add(pirateRelation);
      }
    }

    public void Remove(PirateRelation pirateRelation)
    {
      lock (this._LockObject)
      {
        if (pirateRelation.OtherEmpireId >= 0 && this._OtherEmpireIndexes.Length > pirateRelation.OtherEmpireId)
          this._OtherEmpireIndexes[(int) (byte) pirateRelation.OtherEmpireId] = (short) -1;
        base.Remove(pirateRelation);
        this.RecalculateIndexes();
      }
    }

    public PirateRelation GetRelationByOtherEmpire(Empire otherEmpire)
    {
      if (otherEmpire != null && otherEmpire.EmpireId >= 0 && otherEmpire.EmpireId < this._OtherEmpireIndexes.Length)
      {
        short otherEmpireIndex = this._OtherEmpireIndexes[(int) (byte) otherEmpire.EmpireId];
        if (otherEmpireIndex >= (short) 0 && this.Count > (int) otherEmpireIndex)
          return this[(int) otherEmpireIndex];
      }
      return (PirateRelation) null;
    }

    public PirateRelation GetRelationByOtherEmpireId(int otherEmpireId)
    {
      if (otherEmpireId >= 0)
      {
        short otherEmpireIndex = this._OtherEmpireIndexes[(int) (byte) otherEmpireId];
        if (otherEmpireIndex >= (short) 0 && this.Count > (int) otherEmpireIndex)
          return this[(int) otherEmpireIndex];
      }
      return (PirateRelation) null;
    }

    public PirateRelation GetRelationWithLowestEvaluation()
    {
      PirateRelation lowestEvaluation = (PirateRelation) null;
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet && (lowestEvaluation == null || (double) pirateRelation.Evaluation < (double) lowestEvaluation.Evaluation))
          lowestEvaluation = pirateRelation;
      }
      return lowestEvaluation;
    }

    public PirateRelationList GetRelationsAboveThreshold(float evaluationThreshold)
    {
      PirateRelationList relationsAboveThreshold = new PirateRelationList();
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet && (double) pirateRelation.Evaluation >= (double) evaluationThreshold)
          relationsAboveThreshold.Add(pirateRelation);
      }
      return relationsAboveThreshold;
    }

    public PirateRelationList GetRelationsAboveThresholdAndByType(
      float evaluationThreshold,
      PirateRelationType relationType)
    {
      PirateRelationList thresholdAndByType = new PirateRelationList();
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type == relationType && (double) pirateRelation.Evaluation >= (double) evaluationThreshold)
          thresholdAndByType.Add(pirateRelation);
      }
      return thresholdAndByType;
    }

    public PirateRelationList GetRelationsBelowThreshold(float evaluationThreshold)
    {
      PirateRelationList relationsBelowThreshold = new PirateRelationList();
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet && (double) pirateRelation.Evaluation <= (double) evaluationThreshold)
          relationsBelowThreshold.Add(pirateRelation);
      }
      return relationsBelowThreshold;
    }

    public int CountKnownPirateFactions()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type != PirateRelationType.NotMet && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire.PirateEmpireBaseHabitat != null)
          ++num;
      }
      return num;
    }

    public PirateRelationList GetRelationsByType(PirateRelationType relationType)
    {
      PirateRelationList relationsByType = new PirateRelationList();
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type == relationType)
          relationsByType.Add(pirateRelation);
      }
      return relationsByType;
    }

    public EmpireList ResolveEmpiresWithProtection()
    {
      EmpireList empireList = new EmpireList();
      for (int index = 0; index < this.Count; ++index)
      {
        PirateRelation pirateRelation = this[index];
        if (pirateRelation != null && pirateRelation.Type == PirateRelationType.Protection && pirateRelation.OtherEmpire != null)
          empireList.Add(pirateRelation.OtherEmpire);
      }
      return empireList;
    }

    private void ResetIndexes()
    {
      this._OtherEmpireIndexes = new short[(int) byte.MaxValue];
      for (int index = 0; index < this._OtherEmpireIndexes.Length; ++index)
        this._OtherEmpireIndexes[index] = (short) -1;
    }
  }
}
