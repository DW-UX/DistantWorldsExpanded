// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlanetaryFacilityDefinition
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PlanetaryFacilityDefinition : IComparable<PlanetaryFacilityDefinition>
  {
    public int PlanetaryFacilityDefinitionId;
    public PlanetaryFacilityType Type;
    public WonderType WonderType;
    public string Name;
    public short PictureRef;
    public string Description;
    public double Cost;
    [OptionalField]
    public double Maintenance;
    public int Value1;
    public int Value2;
    public int Value3;
    [NonSerialized]
    public float SortTag;

    public PlanetaryFacilityDefinition(
      int planetaryFacilityDefinitionId,
      string name,
      PlanetaryFacilityType type,
      short pictureRef,
      double cost,
      int value1,
      int value2,
      int value3)
      : this(planetaryFacilityDefinitionId, name, type, pictureRef, cost, value1, value2, value3, WonderType.Undefined)
    {
    }

    public PlanetaryFacilityDefinition(
      int planetaryFacilityDefinitionId,
      string name,
      PlanetaryFacilityType type,
      short pictureRef,
      double cost,
      double maintenance,
      int value1,
      int value2,
      int value3)
      : this(planetaryFacilityDefinitionId, name, type, pictureRef, cost, maintenance, value1, value2, value3, WonderType.Undefined)
    {
    }

    public PlanetaryFacilityDefinition(
      int planetaryFacilityDefinitionId,
      string name,
      PlanetaryFacilityType type,
      short pictureRef,
      double cost,
      int value1,
      int value2,
      int value3,
      WonderType wonderType)
      : this(planetaryFacilityDefinitionId, name, type, pictureRef, cost, 0.0, value1, value2, value3, wonderType)
    {
    }

    public PlanetaryFacilityDefinition(
      int planetaryFacilityDefinitionId,
      string name,
      PlanetaryFacilityType type,
      short pictureRef,
      double cost,
      double maintenance,
      int value1,
      int value2,
      int value3,
      WonderType wonderType)
    {
      this.PlanetaryFacilityDefinitionId = planetaryFacilityDefinitionId;
      this.Name = name;
      this.Type = type;
      this.WonderType = wonderType;
      this.PictureRef = pictureRef;
      this.Cost = cost;
      this.Maintenance = maintenance;
      this.Value1 = value1;
      this.Value2 = value2;
      this.Value3 = value3;
    }

    int IComparable<PlanetaryFacilityDefinition>.CompareTo(PlanetaryFacilityDefinition other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.PlanetaryFacilityDefinitionId.CompareTo(other.PlanetaryFacilityDefinitionId);
  }
}
