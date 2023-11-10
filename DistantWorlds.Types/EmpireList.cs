// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireList : SyncList<Empire>
  {
    public Empire GetByEmpireId(int empireId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].EmpireId == empireId)
          return this[index];
      }
      return (Empire) null;
    }

    public Empire this[string name]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Name == name)
            return this[index];
        }
        return (Empire) null;
      }
    }
  }
}
