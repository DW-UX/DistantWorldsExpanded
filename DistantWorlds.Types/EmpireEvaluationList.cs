// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireEvaluationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireEvaluationList : SyncList<EmpireEvaluation>
  {
    public EmpireEvaluation this[Empire empire]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          EmpireEvaluation empireEvaluation = this[index];
          if (empireEvaluation.Empire == empire)
            return empireEvaluation;
        }
        return (EmpireEvaluation) null;
      }
    }

    public EmpireEvaluation GetLowestEvaluation() => this.GetLowestEvaluation(new EmpireList());

    public EmpireEvaluation GetLowestEvaluation(EmpireList excludeEmpires)
    {
      EmpireEvaluation lowestEvaluation = (EmpireEvaluation) null;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireEvaluation empireEvaluation = this[index];
        if (empireEvaluation != null && !excludeEmpires.Contains(empireEvaluation.Empire) && (lowestEvaluation == null || lowestEvaluation.OverallAttitude > empireEvaluation.OverallAttitude))
          lowestEvaluation = empireEvaluation;
      }
      return lowestEvaluation;
    }

    public EmpireEvaluation GetLowestEvaluationKnownEmpire(Empire empire, EmpireList excludeEmpires)
    {
      EmpireEvaluation evaluationKnownEmpire = (EmpireEvaluation) null;
      for (int index = 0; index < this.Count; ++index)
      {
        EmpireEvaluation empireEvaluation = this[index];
        if (empireEvaluation != null && !excludeEmpires.Contains(empireEvaluation.Empire) && (evaluationKnownEmpire == null || evaluationKnownEmpire.OverallAttitude > empireEvaluation.OverallAttitude) && empire.PirateEmpireBaseHabitat == null && empireEvaluation.Empire.PirateEmpireBaseHabitat == null && empire.ObtainDiplomaticRelation(empireEvaluation.Empire).Type != DiplomaticRelationType.NotMet)
          evaluationKnownEmpire = empireEvaluation;
      }
      return evaluationKnownEmpire;
    }
  }
}
