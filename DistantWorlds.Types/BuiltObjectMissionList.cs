// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectMissionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class BuiltObjectMissionList : SyncList<BuiltObjectMission>
  {
    public bool ContainsType(BuiltObjectMissionType missionType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        BuiltObjectMission builtObjectMission = this[index];
        if (builtObjectMission != null && builtObjectMission.Type == missionType)
          return true;
      }
      return false;
    }
  }
}
