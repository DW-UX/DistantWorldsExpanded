// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PirateColonyControlList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PirateColonyControlList : List<PirateColonyControl>
  {
    public int IndexOf(int empireId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if ((int) this[index].EmpireId == empireId)
          return index;
      }
      return -1;
    }

    public bool CheckFactionHasControl(Empire pirateFaction) => pirateFaction != null && this.CheckFactionHasControl(pirateFaction.EmpireId);

    public bool CheckFactionHasControl(int pirateFactionId) => this.GetByFaction(pirateFactionId) != null;

    public PirateColonyControl GetHighestControl()
    {
      PirateColonyControl highestControl = (PirateColonyControl) null;
      for (int index = 0; index < this.Count; ++index)
      {
        PirateColonyControl pirateColonyControl = this[index];
        if (pirateColonyControl != null && (highestControl == null || (double) pirateColonyControl.ControlLevel > (double) highestControl.ControlLevel))
          highestControl = pirateColonyControl;
      }
      return highestControl;
    }

    public PirateColonyControl GetByFaction(Empire pirateFaction) => pirateFaction != null ? this.GetByFaction(pirateFaction.EmpireId) : (PirateColonyControl) null;

    public PirateColonyControl GetByFaction(int pirateFactionId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if ((int) this[index].EmpireId == pirateFactionId)
          return this[index];
      }
      return (PirateColonyControl) null;
    }

    public PirateColonyControl GetByFacilityControl()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].HasFacilityControl)
          return this[index];
      }
      return (PirateColonyControl) null;
    }

    public bool CheckEmpireHasRelationTypeWithAny(
      Galaxy galaxy,
      Empire empire,
      PirateRelationType relationType)
    {
      if (galaxy != null && empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          PirateColonyControl pirateColonyControl = this[index];
          if (pirateColonyControl != null && (int) pirateColonyControl.EmpireId != empire.EmpireId)
          {
            Empire empireById = galaxy.GetEmpireById((int) pirateColonyControl.EmpireId);
            if (empireById != null)
            {
              PirateRelation pirateRelation = empire.ObtainPirateRelation(empireById);
              if (pirateRelation != null && pirateRelation.Type == relationType)
                return true;
            }
          }
        }
      }
      return false;
    }
  }
}
