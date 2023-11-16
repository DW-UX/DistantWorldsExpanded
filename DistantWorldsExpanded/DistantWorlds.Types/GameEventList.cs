// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameEventList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GameEventList : List<GameEvent>
  {
    private short _NextId;

    public void ClearAndResetIdsToZero()
    {
      this.Clear();
      this._NextId = (short) 0;
    }

    public short GetNextId()
    {
      if (this._NextId >= short.MaxValue)
        return -1;
      ++this._NextId;
      return this._NextId;
    }

    public GameEvent GetById(short gameEventId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        GameEvent byId = this[index];
        if (byId != null && (int) byId.GameEventId == (int) gameEventId)
          return byId;
      }
      return (GameEvent) null;
    }
  }
}
