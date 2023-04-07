// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ListHelper
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System.Collections.Generic;

namespace DistantWorlds.Types
{
  public static class ListHelper
  {
    public static ResearchNode[] ToArrayThreadSafe(ResearchNodeList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      ResearchNode[] arrayThreadSafe = new ResearchNode[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static HabitatResource[] ToArrayThreadSafe(HabitatResourceList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      HabitatResource[] arrayThreadSafe = new HabitatResource[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static BuiltObject[] ToArrayThreadSafe(BuiltObjectList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      BuiltObject[] arrayThreadSafe = new BuiltObject[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static EventActionExecutionPackage[] ToArrayThreadSafe(
      EventActionExecutionPackageList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      EventActionExecutionPackage[] arrayThreadSafe = new EventActionExecutionPackage[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Habitat[] ToArrayThreadSafe(HabitatList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Habitat[] arrayThreadSafe = new Habitat[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static object[] ToArrayThreadSafe(List<object> list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      object[] arrayThreadSafe = new object[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static StellarObject[] ToArrayThreadSafe(StellarObjectList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      StellarObject[] arrayThreadSafe = new StellarObject[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Creature[] ToArrayThreadSafe(CreatureList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Creature[] arrayThreadSafe = new Creature[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Character[] ToArrayThreadSafe(CharacterList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Character[] arrayThreadSafe = new Character[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Fighter[] ToArrayThreadSafe(FighterList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Fighter[] arrayThreadSafe = new Fighter[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Troop[] ToArrayThreadSafe(TroopList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Troop[] arrayThreadSafe = new Troop[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static ShipGroup[] ToArrayThreadSafe(ShipGroupList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      ShipGroup[] arrayThreadSafe = new ShipGroup[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static DiplomaticRelation[] ToArrayThreadSafe(DiplomaticRelationList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      DiplomaticRelation[] arrayThreadSafe = new DiplomaticRelation[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static Empire[] ToArrayThreadSafe(EmpireList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      Empire[] arrayThreadSafe = new Empire[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }

    public static EmpireMessage[] ToArrayThreadSafe(EmpireMessageList list)
    {
      int length = 0;
      if (list != null)
        length = list.Count;
      EmpireMessage[] arrayThreadSafe = new EmpireMessage[length];
      try
      {
        for (int index = 0; index < length; ++index)
          arrayThreadSafe[index] = list[index];
      }
      catch
      {
      }
      return arrayThreadSafe;
    }
  }
}
