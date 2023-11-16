// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DiplomaticRelationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DiplomaticRelationList : SyncList<DiplomaticRelation>
  {
    private int[] _OtherEmpireIndexer;
    private bool _InvertEmpireIndexing;

    public bool InvertEmpireIndexing
    {
      get => this._InvertEmpireIndexing;
      set => this._InvertEmpireIndexing = value;
    }

    public DiplomaticRelationList()
    {
      this._OtherEmpireIndexer = new int[Galaxy.MaximumEmpireCount + 1];
      for (int index = 0; index < Galaxy.MaximumEmpireCount + 1; ++index)
        this._OtherEmpireIndexer[index] = -1;
    }

    public DiplomaticRelation this[Empire otherEmpire]
    {
      get
      {
        if (otherEmpire == null)
          return (DiplomaticRelation) null;
        int index = otherEmpire.EmpireId - 1;
        return index >= 0 && index < Galaxy.MaximumEmpireCount && this._OtherEmpireIndexer[index] >= 0 ? this[this._OtherEmpireIndexer[index]] : (DiplomaticRelation) null;
      }
    }

    private void DecrementIndexes(int aboveIndex)
    {
      if (aboveIndex < 0)
        return;
      for (int index = 0; index < this._OtherEmpireIndexer.Length; ++index)
      {
        if (this._OtherEmpireIndexer[index] > aboveIndex)
          --this._OtherEmpireIndexer[index];
      }
    }

    public string GetHighestAllianceName()
    {
      DiplomaticRelation diplomaticRelation1 = (DiplomaticRelation) null;
      for (int index = 0; index < this.Count; ++index)
      {
        DiplomaticRelation diplomaticRelation2 = this[index];
        if (diplomaticRelation2 != null && !string.IsNullOrEmpty(diplomaticRelation2.AllianceName))
        {
          if (diplomaticRelation1 == null)
          {
            diplomaticRelation1 = diplomaticRelation2;
          }
          else
          {
            switch (diplomaticRelation1.Type)
            {
              case DiplomaticRelationType.FreeTradeAgreement:
                if (diplomaticRelation2.Type == DiplomaticRelationType.Protectorate || diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact)
                {
                  diplomaticRelation1 = diplomaticRelation2;
                  continue;
                }
                continue;
              case DiplomaticRelationType.Protectorate:
                if (diplomaticRelation2.Type == DiplomaticRelationType.MutualDefensePact)
                {
                  diplomaticRelation1 = diplomaticRelation2;
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
      }
      return diplomaticRelation1 != null ? diplomaticRelation1.AllianceName : string.Empty;
    }

    public DiplomaticRelation FindOldestRelationByType(DiplomaticRelationType type)
    {
      DiplomaticRelation oldestRelationByType = (DiplomaticRelation) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type && (oldestRelationByType == null || this[index].StartDateOfLastChange < oldestRelationByType.StartDateOfLastChange))
          oldestRelationByType = this[index];
      }
      return oldestRelationByType;
    }

    public int CountSubjugatedDominions()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null && this[index].Type == DiplomaticRelationType.SubjugatedDominion && this[index].Initiator == this[index].ThisEmpire)
          ++num;
      }
      return num;
    }

    public int CountRelationsByType(DiplomaticRelationType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null && this[index].Type == type)
          ++num;
      }
      return num;
    }

    public int CountTreaties()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null && this[index].Type == DiplomaticRelationType.FreeTradeAgreement || this[index].Type == DiplomaticRelationType.MutualDefensePact || this[index].Type == DiplomaticRelationType.Protectorate)
          ++num;
      }
      return num;
    }

    public int CountMet()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index] != null && this[index].Type != DiplomaticRelationType.NotMet)
          ++num;
      }
      return num;
    }

    public new void RemoveAt(int index)
    {
      lock (this._LockObject)
      {
        DiplomaticRelation diplomaticRelation = this[index];
        if (this._InvertEmpireIndexing)
          this._OtherEmpireIndexer[diplomaticRelation.ThisEmpire.EmpireId - 1] = -1;
        else
          this._OtherEmpireIndexer[diplomaticRelation.OtherEmpire.EmpireId - 1] = -1;
        base.RemoveAt(index);
        this.DecrementIndexes(index);
      }
    }

    public new void Remove(DiplomaticRelation diplomaticRelation)
    {
      lock (this._LockObject)
      {
        int aboveIndex = -1;
        if (this._InvertEmpireIndexing)
        {
          if (diplomaticRelation.ThisEmpire != null && diplomaticRelation.ThisEmpire.EmpireId > 0 && diplomaticRelation.ThisEmpire.EmpireId < this._OtherEmpireIndexer.Length)
            aboveIndex = this._OtherEmpireIndexer[diplomaticRelation.ThisEmpire.EmpireId - 1];
        }
        else if (diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.EmpireId > 0 && diplomaticRelation.OtherEmpire.EmpireId < this._OtherEmpireIndexer.Length)
          aboveIndex = this._OtherEmpireIndexer[diplomaticRelation.OtherEmpire.EmpireId - 1];
        if (this._InvertEmpireIndexing)
        {
          if (diplomaticRelation.ThisEmpire != null && diplomaticRelation.ThisEmpire.EmpireId > 0 && diplomaticRelation.ThisEmpire.EmpireId < this._OtherEmpireIndexer.Length)
            this._OtherEmpireIndexer[diplomaticRelation.ThisEmpire.EmpireId - 1] = -1;
        }
        else if (diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire.EmpireId > 0 && diplomaticRelation.OtherEmpire.EmpireId < this._OtherEmpireIndexer.Length)
          this._OtherEmpireIndexer[diplomaticRelation.OtherEmpire.EmpireId - 1] = -1;
        base.Remove(diplomaticRelation);
        this.DecrementIndexes(aboveIndex);
      }
    }

    public new void Add(DiplomaticRelation diplomaticRelation)
    {
      lock (this._LockObject)
      {
        if (this._InvertEmpireIndexing)
        {
          int num = diplomaticRelation.ThisEmpire.EmpireId - 1;
          if (num >= 0 && num < Galaxy.MaximumEmpireCount)
            this._OtherEmpireIndexer[diplomaticRelation.ThisEmpire.EmpireId - 1] = this.Count;
        }
        else
        {
          int num = diplomaticRelation.OtherEmpire.EmpireId - 1;
          if (num >= 0 && num < Galaxy.MaximumEmpireCount)
            this._OtherEmpireIndexer[diplomaticRelation.OtherEmpire.EmpireId - 1] = this.Count;
        }
        base.Add(diplomaticRelation);
      }
    }
  }
}
