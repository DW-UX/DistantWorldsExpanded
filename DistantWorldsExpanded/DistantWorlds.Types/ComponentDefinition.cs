// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentDefinition
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ComponentDefinition : IComparable<ComponentDefinition>
  {
    private IndustryType _IndustryType;
    private ComponentCategoryType _ComponentCategoryType;
    private ComponentType _ComponentType;
    private ComponentResourceList _RequiredResources = new ComponentResourceList();
    public int ComponentID;
    public string Name;
    public int PictureRef;
    public int SpecialImageIndex;
    public string SoundEffectFilename;
    public int Size;
    public int TechLevel;
    public int EnergyUsed;
    public int Value1;
    public int Value2;
    public int Value3;
    public int Value4;
    public int Value5;
    public int Value6;
    public int Value7;
    [NonSerialized]
    public float SortTag;

    public ComponentDefinition(
      ComponentType componentType,
      int pictureRef,
      string name,
      int size,
      int energyUsed)
    {
      this.Type = componentType;
      this.PictureRef = pictureRef;
      this.Name = name;
      this.Size = size;
      this.EnergyUsed = energyUsed;
    }

    public ComponentDefinition(
      int componentId,
      string name,
      ComponentType componentType,
      int pictureRef,
      int specialImageIndex,
      string soundEffectFilename,
      int size,
      int energyUsed)
    {
      this.ComponentID = componentId;
      this.Name = name;
      this.Type = componentType;
      this.PictureRef = pictureRef;
      this.SpecialImageIndex = specialImageIndex;
      this.SoundEffectFilename = soundEffectFilename;
      this.Size = size;
      this.EnergyUsed = energyUsed;
    }

    public IndustryType Industry => this._IndustryType;

    public ComponentCategoryType Category => this._ComponentCategoryType;

    public ComponentType Type
    {
      get => this._ComponentType;
      set
      {
        this._ComponentType = value;
        this._ComponentCategoryType = ComponentDefinition.ResolveComponentCategory(this._ComponentType);
        this._IndustryType = ComponentDefinition.ResolveIndustry(this._ComponentCategoryType);
      }
    }

    public ComponentResourceList RequiredResources
    {
      get => this._RequiredResources;
      set => this._RequiredResources = value;
    }

    public static IndustryType ResolveIndustry(ComponentCategoryType componentCategoryType)
    {
      switch (componentCategoryType)
      {
        case ComponentCategoryType.WeaponBeam:
        case ComponentCategoryType.WeaponTorpedo:
        case ComponentCategoryType.WeaponArea:
        case ComponentCategoryType.WeaponPointDefense:
        case ComponentCategoryType.WeaponIon:
        case ComponentCategoryType.WeaponGravity:
        case ComponentCategoryType.Armor:
        case ComponentCategoryType.AssaultPod:
        case ComponentCategoryType.Fighter:
        case ComponentCategoryType.WeaponSuperBeam:
        case ComponentCategoryType.WeaponSuperArea:
        case ComponentCategoryType.WeaponSuperTorpedo:
          return IndustryType.Weapon;
        case ComponentCategoryType.Shields:
        case ComponentCategoryType.ShieldRecharge:
        case ComponentCategoryType.Engine:
        case ComponentCategoryType.HyperDrive:
        case ComponentCategoryType.HyperDisrupt:
        case ComponentCategoryType.Reactor:
        case ComponentCategoryType.EnergyCollector:
        case ComponentCategoryType.Extractor:
        case ComponentCategoryType.Manufacturer:
        case ComponentCategoryType.Construction:
          return IndustryType.Energy;
        case ComponentCategoryType.Storage:
        case ComponentCategoryType.Sensor:
        case ComponentCategoryType.Computer:
        case ComponentCategoryType.Labs:
        case ComponentCategoryType.Habitation:
          return IndustryType.HighTech;
        default:
          throw new ApplicationException("Unknown component category type.");
      }
    }

    public static ComponentType ResolveComponentTypeFromCode(int code)
    {
      switch (code)
      {
        case 0:
          return ComponentType.ShieldRecharge;
        case 1:
          return ComponentType.Armor;
        case 2:
          return ComponentType.AssaultPod;
        case 3:
          return ComponentType.StorageCargo;
        case 4:
          return ComponentType.HabitationColonization;
        case 5:
          return ComponentType.ComputerCommandCenter;
        case 6:
          return ComponentType.ComputerCommerceCenter;
        case 7:
          return ComponentType.ConstructionBuild;
        case 8:
          return ComponentType.ComputerCountermeasures;
        case 9:
          return ComponentType.ComputerCountermeasuresFleet;
        case 10:
          return ComponentType.DamageControl;
        case 11:
          return ComponentType.StorageDockingBay;
        case 12:
          return ComponentType.EnergyCollector;
        case 13:
          return ComponentType.EnergyToFuel;
        case 14:
          return ComponentType.EngineMainThrust;
        case 15:
          return ComponentType.EngineVectoring;
        case 16:
          return ComponentType.ExtractorGasExtractor;
        case 17:
          return ComponentType.ExtractorLuxury;
        case 18:
          return ComponentType.ExtractorMine;
        case 19:
          return ComponentType.FighterBay;
        case 20:
          return ComponentType.StorageFuel;
        case 21:
          return ComponentType.HabitationHabModule;
        case 22:
          return ComponentType.HyperDeny;
        case 23:
          return ComponentType.HyperDrive;
        case 24:
          return ComponentType.HyperStop;
        case 25:
          return ComponentType.HabitationLifeSupport;
        case 26:
          return ComponentType.SensorLongRange;
        case 27:
          return ComponentType.ManufacturerEnergyPlant;
        case 28:
          return ComponentType.ManufacturerHighTechPlant;
        case 29:
          return ComponentType.ManufacturerWeaponsPlant;
        case 30:
          return ComponentType.HabitationMedicalCenter;
        case 31:
          return ComponentType.StoragePassenger;
        case 32:
          return ComponentType.SensorProximityArray;
        case 33:
          return ComponentType.Reactor;
        case 34:
          return ComponentType.HabitationRecreationCenter;
        case 35:
          return ComponentType.LabsEnergyLab;
        case 36:
          return ComponentType.LabsHighTechLab;
        case 37:
          return ComponentType.LabsWeaponsLab;
        case 38:
          return ComponentType.SensorResourceProfileSensor;
        case 39:
          return ComponentType.SensorScannerJammer;
        case 40:
          return ComponentType.Shields;
        case 41:
          return ComponentType.SensorStealth;
        case 42:
          return ComponentType.ComputerTargetting;
        case 43:
          return ComponentType.ComputerTargettingFleet;
        case 44:
          return ComponentType.SensorTraceScanner;
        case 45:
          return ComponentType.StorageTroop;
        case 46:
          return ComponentType.WeaponAreaDestruction;
        case 47:
          return ComponentType.WeaponAreaGravity;
        case 48:
          return ComponentType.WeaponBeam;
        case 49:
          return ComponentType.WeaponBombard;
        case 50:
          return ComponentType.WeaponGravityBeam;
        case 51:
          return ComponentType.WeaponIonCannon;
        case 52:
          return ComponentType.WeaponIonDefense;
        case 53:
          return ComponentType.WeaponIonPulse;
        case 54:
          return ComponentType.WeaponMissile;
        case 55:
          return ComponentType.WeaponPhaser;
        case 56:
          return ComponentType.WeaponPointDefense;
        case 57:
          return ComponentType.WeaponRailGun;
        case 58:
          return ComponentType.WeaponSuperArea;
        case 59:
          return ComponentType.WeaponSuperBeam;
        case 60:
          return ComponentType.WeaponTorpedo;
        case 61:
          return ComponentType.WeaponTractorBeam;
        case 62:
          return ComponentType.WeaponSuperTorpedo;
        case 63:
          return ComponentType.WeaponSuperMissile;
        case 64:
          return ComponentType.WeaponSuperRailGun;
        case 65:
          return ComponentType.WeaponSuperPhaser;
        default:
          return ComponentType.Undefined;
      }
    }

    public static ComponentCategoryType ResolveComponentCategory(ComponentType componentType)
    {
      switch (componentType)
      {
        case ComponentType.WeaponBeam:
          return ComponentCategoryType.WeaponBeam;
        case ComponentType.WeaponTorpedo:
        case ComponentType.WeaponBombard:
        case ComponentType.WeaponMissile:
          return ComponentCategoryType.WeaponTorpedo;
        case ComponentType.WeaponPointDefense:
          return ComponentCategoryType.WeaponPointDefense;
        case ComponentType.WeaponIonCannon:
        case ComponentType.WeaponIonPulse:
        case ComponentType.WeaponIonDefense:
          return ComponentCategoryType.WeaponIon;
        case ComponentType.WeaponTractorBeam:
        case ComponentType.WeaponGravityBeam:
        case ComponentType.WeaponAreaGravity:
          return ComponentCategoryType.WeaponGravity;
        case ComponentType.AssaultPod:
          return ComponentCategoryType.AssaultPod;
        case ComponentType.HyperDeny:
        case ComponentType.HyperStop:
          return ComponentCategoryType.HyperDisrupt;
        case ComponentType.WeaponAreaDestruction:
          return ComponentCategoryType.WeaponArea;
        case ComponentType.WeaponSuperBeam:
        case ComponentType.WeaponSuperPhaser:
        case ComponentType.WeaponSuperRailGun:
          return ComponentCategoryType.WeaponSuperBeam;
        case ComponentType.WeaponSuperArea:
          return ComponentCategoryType.WeaponSuperArea;
        case ComponentType.FighterBay:
          return ComponentCategoryType.Fighter;
        case ComponentType.Armor:
          return ComponentCategoryType.Armor;
        case ComponentType.Shields:
          return ComponentCategoryType.Shields;
        case ComponentType.ShieldRecharge:
          return ComponentCategoryType.ShieldRecharge;
        case ComponentType.EngineMainThrust:
        case ComponentType.EngineVectoring:
          return ComponentCategoryType.Engine;
        case ComponentType.HyperDrive:
          return ComponentCategoryType.HyperDrive;
        case ComponentType.Reactor:
          return ComponentCategoryType.Reactor;
        case ComponentType.EnergyCollector:
          return ComponentCategoryType.EnergyCollector;
        case ComponentType.ExtractorMine:
        case ComponentType.ExtractorGasExtractor:
        case ComponentType.ExtractorLuxury:
          return ComponentCategoryType.Extractor;
        case ComponentType.ManufacturerWeaponsPlant:
        case ComponentType.ManufacturerEnergyPlant:
        case ComponentType.ManufacturerHighTechPlant:
          return ComponentCategoryType.Manufacturer;
        case ComponentType.StorageFuel:
        case ComponentType.StorageCargo:
        case ComponentType.StorageTroop:
        case ComponentType.StoragePassenger:
        case ComponentType.StorageDockingBay:
          return ComponentCategoryType.Storage;
        case ComponentType.SensorProximityArray:
        case ComponentType.SensorResourceProfileSensor:
        case ComponentType.SensorLongRange:
        case ComponentType.SensorTraceScanner:
        case ComponentType.SensorScannerJammer:
        case ComponentType.SensorStealth:
          return ComponentCategoryType.Sensor;
        case ComponentType.ComputerTargetting:
        case ComponentType.ComputerTargettingFleet:
        case ComponentType.ComputerCountermeasures:
        case ComponentType.ComputerCountermeasuresFleet:
        case ComponentType.ComputerCommandCenter:
        case ComponentType.ComputerCommerceCenter:
          return ComponentCategoryType.Computer;
        case ComponentType.LabsWeaponsLab:
        case ComponentType.LabsEnergyLab:
        case ComponentType.LabsHighTechLab:
          return ComponentCategoryType.Labs;
        case ComponentType.ConstructionBuild:
        case ComponentType.DamageControl:
          return ComponentCategoryType.Construction;
        case ComponentType.HabitationLifeSupport:
        case ComponentType.HabitationHabModule:
        case ComponentType.HabitationMedicalCenter:
        case ComponentType.HabitationRecreationCenter:
        case ComponentType.HabitationColonization:
          return ComponentCategoryType.Habitation;
        case ComponentType.WeaponPhaser:
          return ComponentCategoryType.WeaponBeam;
        case ComponentType.WeaponRailGun:
          return ComponentCategoryType.WeaponBeam;
        case ComponentType.EnergyToFuel:
          return ComponentCategoryType.EnergyCollector;
        case ComponentType.WeaponSuperTorpedo:
        case ComponentType.WeaponSuperMissile:
          return ComponentCategoryType.WeaponSuperTorpedo;
        default:
          throw new ApplicationException("Unknown component type.");
      }
    }

    public static ComponentDefinition GetLowestTechByType(
      ComponentType type,
      ComponentDefinition[] definitions)
    {
      ComponentDefinition lowestTechByType = (ComponentDefinition) null;
      for (int index = 0; index < definitions.Length; ++index)
      {
        ComponentDefinition definition = definitions[index];
        if (lowestTechByType != null && lowestTechByType.Type == type && (lowestTechByType == null || definition.TechLevel < lowestTechByType.TechLevel))
        {
          lowestTechByType = definition;
          int techLevel = definition.TechLevel;
        }
      }
      return lowestTechByType;
    }

    public static ComponentDefinition GetHighestTechByType(
      ComponentType type,
      ComponentDefinition[] definitions)
    {
      ComponentDefinition highestTechByType = (ComponentDefinition) null;
      for (int index = 0; index < definitions.Length; ++index)
      {
        ComponentDefinition definition = definitions[index];
        if (definition != null && definition.Type == type && (highestTechByType == null || definition.TechLevel > highestTechByType.TechLevel))
        {
          highestTechByType = definition;
          int techLevel = definition.TechLevel;
        }
      }
      return highestTechByType;
    }

    public static ComponentDefinitionList GetByType(
      ComponentType type,
      ComponentDefinition[] definitions)
    {
      ComponentDefinitionList byType = new ComponentDefinitionList();
      for (int index = 0; index < definitions.Length; ++index)
      {
        ComponentDefinition definition = definitions[index];
        if (definition != null && definition.Type == type)
          byType.Add(definition);
      }
      return byType;
    }

    int IComparable<ComponentDefinition>.CompareTo(ComponentDefinition other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.ComponentID.CompareTo(other.ComponentID);
  }
}
