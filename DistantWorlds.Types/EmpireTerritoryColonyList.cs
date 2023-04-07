// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireTerritoryColonyList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  public class EmpireTerritoryColonyList : List<EmpireTerritoryColony>
  {
    public static EmpireTerritoryColonyList SortColoniesByInfluence(
      EmpireTerritoryColonyList colonies)
    {
      float[] keys = new float[colonies.Count];
      EmpireTerritoryColony[] array = colonies.ToArray();
      for (int index = 0; index < array.Length; ++index)
      {
        if (array[index].Colony != null)
          keys[index] = array[index].Colony.ColonyInfluenceRadius;
      }
      Array.Sort<float, EmpireTerritoryColony>(keys, array);
      Array.Reverse((Array) array);
      EmpireTerritoryColonyList territoryColonyList = new EmpireTerritoryColonyList();
      territoryColonyList.AddRange((IEnumerable<EmpireTerritoryColony>) array);
      return territoryColonyList;
    }
  }
}
