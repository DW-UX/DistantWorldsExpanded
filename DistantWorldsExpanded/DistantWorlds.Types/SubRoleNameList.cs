// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SubRoleNameList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SubRoleNameList
  {
    private BuiltObjectSubRole _SubRole;
    private List<string> _Names = new List<string>();

    public BuiltObjectSubRole SubRole
    {
      get => this._SubRole;
      set => this._SubRole = value;
    }

    public List<string> Names
    {
      get => this._Names;
      set => this._Names = value;
    }

    public SubRoleNameList(BuiltObjectSubRole subRole) => this._SubRole = subRole;
  }
}
