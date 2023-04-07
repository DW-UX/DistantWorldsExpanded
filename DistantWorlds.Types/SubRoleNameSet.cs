// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SubRoleNameSet
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SubRoleNameSet
  {
    private List<SubRoleNameList> _SubRoleNames = new List<SubRoleNameList>();

    public List<SubRoleNameList> SubRoleNames
    {
      get => this._SubRoleNames;
      set => this._SubRoleNames = value;
    }

    public List<string> GetNames(BuiltObjectSubRole subRole)
    {
      foreach (SubRoleNameList subRoleName in this._SubRoleNames)
      {
        if (subRoleName.SubRole == subRole)
          return subRoleName.Names;
      }
      return (List<string>) null;
    }
  }
}
