// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ForceStructureProjectionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ForceStructureProjectionList : SyncList<ForceStructureProjection>
  {
    public ForceStructureProjectionList Diff(
      ForceStructureProjectionList forceStructureProjectionList)
    {
      ForceStructureProjectionList structureProjectionList = new ForceStructureProjectionList();
      foreach (BuiltObjectSubRole subRole in Enum.GetValues(typeof (BuiltObjectSubRole)))
      {
        int num1 = 0;
        int num2 = 0;
        ForceStructureProjection bySubRole1 = this.GetBySubRole(subRole);
        if (bySubRole1 != null)
          num1 = bySubRole1.Amount;
        ForceStructureProjection bySubRole2 = forceStructureProjectionList.GetBySubRole(subRole);
        if (bySubRole2 != null)
          num2 = bySubRole2.Amount;
        int amount = Math.Max(0, num1 - num2);
        ForceStructureProjection structureProjection = new ForceStructureProjection(subRole, amount, -1L);
        structureProjectionList.Add(structureProjection);
      }
      return structureProjectionList;
    }

    public ForceStructureProjection GetBySubRole(BuiltObjectSubRole subRole)
    {
      foreach (ForceStructureProjection bySubRole in (SyncList<ForceStructureProjection>) this)
      {
        if (bySubRole.SubRole == subRole)
          return bySubRole;
      }
      return (ForceStructureProjection) null;
    }

    public int TotalAmount
    {
      get
      {
        int totalAmount = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          ForceStructureProjection structureProjection = this[index];
          if (structureProjection != null)
            totalAmount += structureProjection.Amount;
        }
        return totalAmount;
      }
    }

    public ForceStructureProjectionList Clone()
    {
      ForceStructureProjectionList structureProjectionList = new ForceStructureProjectionList();
      for (int index = 0; index < this.Count; ++index)
      {
        ForceStructureProjection structureProjection1 = this[index];
        if (structureProjection1 != null)
        {
          ForceStructureProjection structureProjection2 = new ForceStructureProjection(structureProjection1.SubRole, structureProjection1.Amount, structureProjection1.ProjectionDate);
          structureProjectionList.Add(structureProjection2);
        }
      }
      return structureProjectionList;
    }
  }
}
