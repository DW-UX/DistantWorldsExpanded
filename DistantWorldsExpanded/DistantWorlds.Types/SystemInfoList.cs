// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SystemInfoList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SystemInfoList : SyncList<SystemInfo>
  {
    public SystemInfo GetBySystemIndex(int systemIndex) => systemIndex >= 0 && systemIndex < this.Count ? this[systemIndex] : (SystemInfo) null;

    public SystemInfo this[Habitat systemStar]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].SystemStar == systemStar)
            return this[index];
        }
        return (SystemInfo) null;
      }
    }
  }
}
