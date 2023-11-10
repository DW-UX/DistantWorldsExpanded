// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchArea
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchArea
  {
    private ComponentCategoryType _Category;
    private float _TechPoints;
    private Component _LatestComponent;

    public ResearchArea(ComponentCategoryType componentCategory) => this._Category = componentCategory;

    public ResearchArea(ComponentCategoryType componentCategory, int techPoints)
    {
      this._Category = componentCategory;
      this._TechPoints = (float) techPoints;
    }

    public float TechPoints
    {
      get => this._TechPoints;
      set => this._TechPoints = value;
    }

    public Component LatestComponent
    {
      get => this._LatestComponent;
      set => this._LatestComponent = value;
    }

    public ComponentCategoryType Category => this._Category;
  }
}
