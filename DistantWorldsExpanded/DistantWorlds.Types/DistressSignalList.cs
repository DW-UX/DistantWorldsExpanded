// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DistressSignalList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DistressSignalList : List<DistressSignal>
  {
    public DistressSignalList GetAttacksByEmpire(Empire attacker)
    {
      DistressSignalList attacksByEmpire = new DistressSignalList();
      foreach (DistressSignal distressSignal in (List<DistressSignal>) this)
      {
        if (distressSignal.Type == DistressSignalType.UnderAttack && distressSignal.Attacker == attacker)
          attacksByEmpire.Add(distressSignal);
      }
      return attacksByEmpire;
    }

    public DistressSignalList GetAttacksByColony(Habitat colony)
    {
      DistressSignalList attacksByColony = new DistressSignalList();
      foreach (DistressSignal distressSignal in (List<DistressSignal>) this)
      {
        if (distressSignal.Type == DistressSignalType.UnderAttack && distressSignal.Source is Habitat && (Habitat) distressSignal.Source == colony)
          attacksByColony.Add(distressSignal);
      }
      return attacksByColony;
    }

    public DistressSignalList GetAttacksByBuiltObject(BuiltObject builtObject)
    {
      DistressSignalList attacksByBuiltObject = new DistressSignalList();
      foreach (DistressSignal distressSignal in (List<DistressSignal>) this)
      {
        if (distressSignal.Type == DistressSignalType.UnderAttack && distressSignal.Source is BuiltObject && (BuiltObject) distressSignal.Source == builtObject)
          attacksByBuiltObject.Add(distressSignal);
      }
      return attacksByBuiltObject;
    }

    public DistressSignalList GetAttacksByEmpireAndColony(Empire attacker, Habitat colony)
    {
      DistressSignalList byEmpireAndColony = new DistressSignalList();
      foreach (DistressSignal distressSignal in (List<DistressSignal>) this)
      {
        if (distressSignal.Type == DistressSignalType.UnderAttack && distressSignal.Attacker == attacker && distressSignal.Source is Habitat && (Habitat) distressSignal.Source == colony)
          byEmpireAndColony.Add(distressSignal);
      }
      return byEmpireAndColony;
    }

    public DistressSignalList GetAttacksByEmpireAndBuiltObject(
      Empire attacker,
      BuiltObject builtObject)
    {
      DistressSignalList empireAndBuiltObject = new DistressSignalList();
      foreach (DistressSignal distressSignal in (List<DistressSignal>) this)
      {
        if (distressSignal.Type == DistressSignalType.UnderAttack && distressSignal.Attacker == attacker && distressSignal.Source is BuiltObject && (BuiltObject) distressSignal.Source == builtObject)
          empireAndBuiltObject.Add(distressSignal);
      }
      return empireAndBuiltObject;
    }
  }
}
