// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BlockadeList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class BlockadeList : SyncList<Blockade>
  {
    public Blockade this[Habitat colony]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Blockade blockade = this[index];
          if (blockade.TargetIsColony && blockade.Colony == colony)
            return blockade;
        }
        return (Blockade) null;
      }
    }

    public Blockade this[BuiltObject builtObject]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Blockade blockade = this[index];
          if (!blockade.TargetIsColony && blockade.BuiltObject == builtObject)
            return blockade;
        }
        return (Blockade) null;
      }
    }

    public BlockadeList GetBlockadesAgainstEmpire(Empire target)
    {
      BlockadeList blockadesAgainstEmpire = new BlockadeList();
      for (int index = 0; index < this.Count; ++index)
      {
        Blockade blockade = this[index];
        if (blockade.BlockadedEmpire == target)
          blockadesAgainstEmpire.Add(blockade);
      }
      return blockadesAgainstEmpire;
    }

    public BlockadeList GetBlockadesForEmpire(Empire initiator)
    {
      BlockadeList blockadesForEmpire = new BlockadeList();
      for (int index = 0; index < this.Count; ++index)
      {
        Blockade blockade = this[index];
        if (blockade.Initiator == initiator)
          blockadesForEmpire.Add(blockade);
      }
      return blockadesForEmpire;
    }
  }
}
