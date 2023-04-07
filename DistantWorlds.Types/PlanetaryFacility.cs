// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlanetaryFacility
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PlanetaryFacility
  {
    private int _PlanetaryFacilityDefinitionId;
    public float ConstructionProgress;

    public PlanetaryFacility(int planetaryFacilityDefinitionId)
      : this(planetaryFacilityDefinitionId, 0.0f)
    {
    }

    public PlanetaryFacility(int planetaryFacilityDefinitionId, float constructionProgress)
    {
      this._PlanetaryFacilityDefinitionId = planetaryFacilityDefinitionId;
      this.ConstructionProgress = constructionProgress;
    }

    public int PlanetaryFacilityDefinitionId => this._PlanetaryFacilityDefinitionId;

    public string Name => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Name;

    public PlanetaryFacilityType Type => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Type;

    public WonderType WonderType => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].WonderType;

    public string Description => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Description;

    public short PictureRef => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].PictureRef;

    public int Value1 => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Value1;

    public int Value2 => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Value2;

    public int Value3 => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Value3;

    public double Maintenance => Galaxy.PlanetaryFacilityDefinitionsStatic[this._PlanetaryFacilityDefinitionId].Maintenance;
  }
}
