// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ConstructionYardList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ConstructionYardList : SyncList<ConstructionYard>
  {
    public int CountUnderConstruction
    {
      get
      {
        int underConstruction = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ShipUnderConstruction != null)
            ++underConstruction;
        }
        return underConstruction;
      }
    }

    public int CountPlanetDestroyersUnderConstruction
    {
      get
      {
        int underConstruction = 0;
        for (int index = 0; index < this.Count; ++index)
        {
          ConstructionYard constructionYard = this[index];
          if (constructionYard != null && constructionYard.ShipUnderConstruction != null && constructionYard.ShipUnderConstruction.Design != null && constructionYard.ShipUnderConstruction.Design.IsPlanetDestroyer)
            ++underConstruction;
        }
        return underConstruction;
      }
    }

    public bool AddBuiltObjectToConstruct(BuiltObject builtObject, Empire buildingEmpire)
    {
      foreach (ConstructionYard constructionYard in (SyncList<ConstructionYard>) this)
      {
        if (constructionYard.ShipUnderConstruction == null)
        {
          double num = 1.0;
          if (builtObject.Empire == null && buildingEmpire != null)
            num = Galaxy.ResolveBuildSpeed(buildingEmpire, buildingEmpire.Galaxy, builtObject, false);
          constructionYard.BuildSpeedModifier = (float) num;
          constructionYard.ShipUnderConstruction = builtObject;
          constructionYard.IncrementalProgress = 0.0f;
          constructionYard.RetrofitComponentsToBeBuilt = (ComponentList) null;
          constructionYard.RetrofitComponentsToBeScrapped = (ComponentList) null;
          if (builtObject.RetrofitDesign != null)
          {
            constructionYard.RetrofitComponentsToBeBuilt = builtObject.Components.ResolveComponentList().Diff(builtObject.RetrofitDesign.Components);
            constructionYard.RetrofitComponentsToBeScrapped = builtObject.RetrofitDesign.Components.Diff(builtObject.Components.ResolveComponentList());
          }
          return true;
        }
      }
      return false;
    }

    public int IndexOf(BuiltObjectComponent builtObjectComponent)
    {
      lock (this._LockObject)
      {
        if (builtObjectComponent != null && builtObjectComponent.BuiltObjectComponentId >= (short) 0)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if ((int) builtObjectComponent.BuiltObjectComponentId == (int) this[index].BuiltObjectComponentId)
              return index;
          }
        }
        return -1;
      }
    }

    public int IndexOf(BuiltObject builtObject)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (builtObject == this[index].ShipUnderConstruction)
            return index;
        }
        return -1;
      }
    }
  }
}
