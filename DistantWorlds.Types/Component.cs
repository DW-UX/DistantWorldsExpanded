// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Component
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Component
  {
    public int ComponentID;

    public Component()
    {
    }

    public Component(int componentID) => this.ComponentID = componentID;

    public ComponentType Type => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Type;

    public string Name => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Name;

    public ComponentCategoryType Category => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Category;

    public int EnergyUsed => Galaxy.ComponentDefinitionsStatic[this.ComponentID].EnergyUsed;

    public IndustryType Industry => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Industry;

    public int PictureRef => Galaxy.ComponentDefinitionsStatic[this.ComponentID].PictureRef;

    public ComponentResourceList RequiredResources => Galaxy.ComponentDefinitionsStatic[this.ComponentID].RequiredResources;

    public int Size => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Size;

    public int TechLevel => Galaxy.ComponentDefinitionsStatic[this.ComponentID].TechLevel;

    public int SpecialImageIndex => Galaxy.ComponentDefinitionsStatic[this.ComponentID].SpecialImageIndex;

    public string SoundEffectFilename => Galaxy.ComponentDefinitionsStatic[this.ComponentID].SoundEffectFilename;

    public int Value1 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value1;

    public int Value2 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value2;

    public int Value3 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value3;

    public int Value4 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value4;

    public int Value5 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value5;

    public int Value6 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value6;

    public int Value7 => Galaxy.ComponentDefinitionsStatic[this.ComponentID].Value7;

    public static Component EvaluateLatest(ComponentType componentType, double techLevel)
    {
      int num = -1;
      Component latest = (Component) null;
      for (int index = 0; index < Galaxy.ComponentDefinitionsStatic.Length; ++index)
      {
        if (Galaxy.ComponentDefinitionsStatic[index].Type == componentType && (double) Galaxy.ComponentDefinitionsStatic[index].TechLevel <= techLevel && Galaxy.ComponentDefinitionsStatic[index].TechLevel > num)
        {
          latest = new Component(Galaxy.ComponentDefinitionsStatic[index].ComponentID);
          num = Galaxy.ComponentDefinitionsStatic[index].TechLevel;
        }
      }
      return latest;
    }

    public static Component EvaluateLatest(
      ComponentCategoryType componentCategoryType,
      double techLevel)
    {
      int num = -1;
      Component latest = (Component) null;
      for (int index = 0; index < Galaxy.ComponentDefinitionsStatic.Length; ++index)
      {
        if (Galaxy.CheckComponentMatchesCategoryStrict(Galaxy.ComponentDefinitionsStatic[index], componentCategoryType) && (double) Galaxy.ComponentDefinitionsStatic[index].TechLevel <= techLevel && Galaxy.ComponentDefinitionsStatic[index].TechLevel > num)
        {
          latest = new Component(Galaxy.ComponentDefinitionsStatic[index].ComponentID);
          num = Galaxy.ComponentDefinitionsStatic[index].TechLevel;
        }
      }
      return latest;
    }

    public static Component EvaluateNext(
      ComponentCategoryType componentCategoryType,
      double techLevel)
    {
      int num = int.MaxValue;
      Component next = (Component) null;
      for (int index = 0; index < Galaxy.ComponentDefinitionsStatic.Length; ++index)
      {
        if (Galaxy.ComponentDefinitionsStatic[index].Category == componentCategoryType && (double) Galaxy.ComponentDefinitionsStatic[index].TechLevel > techLevel && Galaxy.ComponentDefinitionsStatic[index].TechLevel <= num)
        {
          next = new Component(Galaxy.ComponentDefinitionsStatic[index].ComponentID);
          num = Galaxy.ComponentDefinitionsStatic[index].TechLevel;
        }
      }
      return next;
    }

    public static bool TypesIntersect(List<ComponentType> types1, List<ComponentType> types2)
    {
      for (int index = 0; index < types1.Count; ++index)
      {
        if (types2.Contains(types1[index]))
          return true;
      }
      return false;
    }

    public static bool CategoriesIntersect(
      List<ComponentCategoryType> categories1,
      List<ComponentCategoryType> categories2)
    {
      for (int index = 0; index < categories1.Count; ++index)
      {
        if (categories2.Contains(categories1[index]))
          return true;
      }
      return false;
    }
  }
}
