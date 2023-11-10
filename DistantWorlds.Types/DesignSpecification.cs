// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DesignSpecification
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DesignSpecification
  {
    private BuiltObjectSubRole _BuiltObjectSubRole;
    private BuiltObjectRole _BuiltObjectRole;
    private DesignSpecificationComponentRuleList _ComponentRules;
    private bool _Mobile;
    [OptionalField]
    public DesignImageScalingMode ImageScalingMode;
    [OptionalField]
    public float ImageScalingFactor = 1f;
    [OptionalField]
    public BattleTactics TacticsStronger;
    [OptionalField]
    public BattleTactics TacticsWeaker;
    [OptionalField]
    public InvasionTactics TacticsInvasion;
    [OptionalField]
    public BuiltObjectFleeWhen FleeWhen;

    public DesignSpecification Clone()
    {
      DesignSpecification designSpecification = new DesignSpecification(this._BuiltObjectSubRole, this._Mobile);
      designSpecification.ComponentRules = new DesignSpecificationComponentRuleList();
      foreach (DesignSpecificationComponentRule componentRule in (SyncList<DesignSpecificationComponentRule>) this._ComponentRules)
      {
        DesignSpecificationComponentRule specificationComponentRule = componentRule.ComponentType != ComponentType.Undefined ? new DesignSpecificationComponentRule(componentRule.ComponentRuleType, componentRule.ComponentType, componentRule.Amount) : new DesignSpecificationComponentRule(componentRule.ComponentRuleType, componentRule.ComponentCategory, componentRule.Amount);
        designSpecification.ComponentRules.Add(specificationComponentRule);
      }
      return designSpecification;
    }

    public bool Contains(ComponentType componentType)
    {
      if (this._ComponentRules != null)
      {
        foreach (DesignSpecificationComponentRule componentRule in (SyncList<DesignSpecificationComponentRule>) this._ComponentRules)
        {
          if (componentRule.ComponentType == componentType)
            return true;
        }
      }
      return false;
    }

    public DesignSpecification(BuiltObjectSubRole subRole, bool mobile)
    {
      this._ComponentRules = new DesignSpecificationComponentRuleList();
      this.SubRole = subRole;
      this.Mobile = mobile;
    }

    public BuiltObjectSubRole SubRole
    {
      get => this._BuiltObjectSubRole;
      set
      {
        this._BuiltObjectSubRole = value;
        this._BuiltObjectRole = DesignSpecification.ResolveRole(this._BuiltObjectSubRole);
      }
    }

    public BuiltObjectRole Role => this._BuiltObjectRole;

    public DesignSpecificationComponentRuleList ComponentRules
    {
      get => this._ComponentRules;
      set => this._ComponentRules = value;
    }

    public bool Mobile
    {
      get => this._Mobile;
      set => this._Mobile = value;
    }

    public static BuiltObjectRole ResolveRole(BuiltObjectSubRole subRole)
    {
      switch (subRole)
      {
        case BuiltObjectSubRole.Escort:
        case BuiltObjectSubRole.Frigate:
        case BuiltObjectSubRole.Destroyer:
        case BuiltObjectSubRole.Cruiser:
        case BuiltObjectSubRole.CapitalShip:
        case BuiltObjectSubRole.TroopTransport:
        case BuiltObjectSubRole.Carrier:
        case BuiltObjectSubRole.ResupplyShip:
          return BuiltObjectRole.Military;
        case BuiltObjectSubRole.ExplorationShip:
          return BuiltObjectRole.Exploration;
        case BuiltObjectSubRole.SmallFreighter:
        case BuiltObjectSubRole.MediumFreighter:
        case BuiltObjectSubRole.LargeFreighter:
          return BuiltObjectRole.Freight;
        case BuiltObjectSubRole.ColonyShip:
          return BuiltObjectRole.Colony;
        case BuiltObjectSubRole.PassengerShip:
          return BuiltObjectRole.Passenger;
        case BuiltObjectSubRole.ConstructionShip:
          return BuiltObjectRole.Build;
        case BuiltObjectSubRole.GasMiningShip:
        case BuiltObjectSubRole.MiningShip:
          return BuiltObjectRole.Resource;
        case BuiltObjectSubRole.GasMiningStation:
        case BuiltObjectSubRole.MiningStation:
        case BuiltObjectSubRole.SmallSpacePort:
        case BuiltObjectSubRole.MediumSpacePort:
        case BuiltObjectSubRole.LargeSpacePort:
        case BuiltObjectSubRole.ResortBase:
        case BuiltObjectSubRole.GenericBase:
          return BuiltObjectRole.Base;
        case BuiltObjectSubRole.EnergyResearchStation:
        case BuiltObjectSubRole.WeaponsResearchStation:
        case BuiltObjectSubRole.HighTechResearchStation:
        case BuiltObjectSubRole.MonitoringStation:
        case BuiltObjectSubRole.DefensiveBase:
          return BuiltObjectRole.Base;
        default:
          throw new ApplicationException("Unknown built object sub role type.");
      }
    }

    public static DesignSpecification LoadFromFile(
      Galaxy galaxy,
      string subRoleName,
      BuiltObjectSubRole subRole,
      bool isMobile,
      Race race,
      bool isPirate)
    {
      return DesignSpecification.LoadFromFile(galaxy.ApplicationStartupPath, galaxy.CustomizationSetPath, subRoleName, subRole, isMobile, race, isPirate);
    }

    public static DesignSpecification LoadFromFile(
      Galaxy galaxy,
      string subRoleName,
      BuiltObjectSubRole subRole,
      bool isMobile,
      Race race,
      bool isPirate,
      string raceNameOverride)
    {
      return DesignSpecification.LoadFromFile(galaxy.ApplicationStartupPath, galaxy.CustomizationSetPath, subRoleName, subRole, isMobile, race, isPirate, false, raceNameOverride);
    }

    public static DesignSpecification LoadFromFile(
      string applicationPath,
      string customPath,
      string subRoleName,
      BuiltObjectSubRole subRole,
      bool isMobile,
      Race race,
      bool isPirate)
    {
      return DesignSpecification.LoadFromFile(applicationPath, customPath, subRoleName, subRole, isMobile, race, isPirate, false);
    }

    public static DesignSpecification LoadFromFile(
      string applicationPath,
      string customPath,
      string subRoleName,
      BuiltObjectSubRole subRole,
      bool isMobile,
      Race race,
      bool isPirate,
      bool standAlone)
    {
      return DesignSpecification.LoadFromFile(applicationPath, customPath, subRoleName, subRole, isMobile, race, isPirate, standAlone, race.Name);
    }

    public static DesignSpecification LoadFromFile(
      string applicationPath,
      string customPath,
      string subRoleName,
      BuiltObjectSubRole subRole,
      bool isMobile,
      Race race,
      bool isPirate,
      bool standAlone,
      string raceNameOverride)
    {
      if (race != null)
      {
        string str1 = ";";
        int num = 0;
        string str2 = applicationPath + "\\designTemplates\\" + raceNameOverride + "\\" + subRoleName + ".txt";
        string path1 = customPath + "designTemplates\\" + raceNameOverride + "\\" + subRoleName + ".txt";
        string path2 = applicationPath + "\\designTemplates\\" + raceNameOverride + "\\pirate\\" + subRoleName + ".txt";
        string path3 = customPath + "designTemplates\\" + raceNameOverride + "\\pirate\\" + subRoleName + ".txt";
        string path4 = str2;
        if (isPirate)
        {
          path4 = path2;
          if (!string.IsNullOrEmpty(customPath) && File.Exists(path3))
            path4 = path3;
          else if (!File.Exists(path2))
            path4 = str2;
        }
        else if (!string.IsNullOrEmpty(customPath) && File.Exists(path1))
          path4 = path1;
        DesignSpecification designSpecification = new DesignSpecification(subRole, isMobile);
        if (File.Exists(path4))
        {
          designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentType.ComputerCommandCenter, 1));
          if (isMobile)
            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, ComponentCategoryType.HyperDrive, 1));
          using (FileStream fileStream = new FileStream(path4, FileMode.Open, FileAccess.Read))
          {
            using (StreamReader streamReader = new StreamReader((Stream) fileStream))
            {
              while (!streamReader.EndOfStream)
              {
                string str3 = streamReader.ReadLine();
                ++num;
                if (!string.IsNullOrEmpty(str3) && str3.Trim() != string.Empty && str3.Trim().Substring(0, 1) != "'")
                {
                  int length = str3.IndexOf(str1);
                  if (length >= 0)
                  {
                    string str4 = str3.Substring(0, length).Trim();
                    string s = str3.Substring(length + 1, str3.Length - (length + 1)).Trim();
                    if (str4.ToLower(CultureInfo.InvariantCulture) == "tacticsweaker")
                    {
                      BattleTactics battleTactics = BattleTactics.Undefined;
                      switch (s.Trim().ToLower(CultureInfo.InvariantCulture))
                      {
                        case "evade":
                          battleTactics = BattleTactics.Evade;
                          break;
                        case "standoff":
                          battleTactics = BattleTactics.Standoff;
                          break;
                        case "allweapons":
                          battleTactics = BattleTactics.AllWeapons;
                          break;
                        case "pointblank":
                          battleTactics = BattleTactics.PointBlank;
                          break;
                      }
                      designSpecification.TacticsWeaker = battleTactics;
                    }
                    else if (str4.ToLower(CultureInfo.InvariantCulture) == "tacticsstronger")
                    {
                      BattleTactics battleTactics = BattleTactics.Undefined;
                      switch (s.Trim().ToLower(CultureInfo.InvariantCulture))
                      {
                        case "evade":
                          battleTactics = BattleTactics.Evade;
                          break;
                        case "standoff":
                          battleTactics = BattleTactics.Standoff;
                          break;
                        case "allweapons":
                          battleTactics = BattleTactics.AllWeapons;
                          break;
                        case "pointblank":
                          battleTactics = BattleTactics.PointBlank;
                          break;
                      }
                      designSpecification.TacticsStronger = battleTactics;
                    }
                    else if (str4.ToLower(CultureInfo.InvariantCulture) == "tacticsinvasion")
                    {
                      InvasionTactics invasionTactics = InvasionTactics.Undefined;
                      switch (s.Trim().ToLower(CultureInfo.InvariantCulture))
                      {
                        case "donotinvade":
                          invasionTactics = InvasionTactics.DoNotInvade;
                          break;
                        case "invadewhenclear":
                          invasionTactics = InvasionTactics.InvadeWhenClear;
                          break;
                        case "invadeimmediately":
                          invasionTactics = InvasionTactics.InvadeImmediately;
                          break;
                      }
                      designSpecification.TacticsInvasion = invasionTactics;
                    }
                    else if (str4.ToLower(CultureInfo.InvariantCulture) == "fleewhen")
                    {
                      BuiltObjectFleeWhen builtObjectFleeWhen = BuiltObjectFleeWhen.Undefined;
                      switch (s.Trim().ToLower(CultureInfo.InvariantCulture))
                      {
                        case "enemymilitarysighted":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.EnemyMilitarySighted;
                          break;
                        case "attacked":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.Attacked;
                          break;
                        case "shields50":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.Shields50;
                          break;
                        case "shields20":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.Shields20;
                          break;
                        case "armor50":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.Armor50;
                          break;
                        case "never":
                          builtObjectFleeWhen = BuiltObjectFleeWhen.Never;
                          break;
                      }
                      designSpecification.FleeWhen = builtObjectFleeWhen;
                    }
                    else if (str4.ToLower(CultureInfo.InvariantCulture) == "imagescaling")
                    {
                      float result = 1f;
                      string str5 = "absolute";
                      string str6 = "scaled";
                      DesignImageScalingMode imageScalingMode;
                      if (s.ToLower(CultureInfo.InvariantCulture).StartsWith(str5))
                      {
                        imageScalingMode = DesignImageScalingMode.Absolute;
                        if (!float.TryParse(s.Substring(str5.Length, s.Length - str5.Length).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                          throw new ApplicationException("Error reading Image Scaling Factor in line " + num.ToString() + " of file " + path4);
                        if ((double) result < 10.0 || (double) result > 1000.0)
                          throw new ApplicationException("Invalid Image Scaling Factor (when mode is Absolute should be between 10 and 1000) in line " + num.ToString() + " of file " + path4);
                      }
                      else
                      {
                        if (!s.ToLower(CultureInfo.InvariantCulture).StartsWith(str6))
                          throw new ApplicationException("Invalid Image Scaling Mode (should be Absolute or Scaled) in line " + num.ToString() + " of file " + path4);
                        imageScalingMode = DesignImageScalingMode.Scaled;
                        if (!float.TryParse(s.Substring(str6.Length, s.Length - str6.Length).Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                          throw new ApplicationException("Error reading Image Scaling Factor in line " + num.ToString() + " of file " + path4);
                        if ((double) result < 0.05000000074505806 || (double) result > 10.0)
                          throw new ApplicationException("Invalid Image Scaling Factor (when mode is Scaled should be between 0.05 and 10.0) in line " + num.ToString() + " of file " + path4);
                      }
                      designSpecification.ImageScalingMode = imageScalingMode;
                      designSpecification.ImageScalingFactor = result;
                    }
                    else
                    {
                      int result = 0;
                      if (int.TryParse(s, out result) && result > 0)
                      {
                        ComponentType type = ComponentType.Undefined;
                        ComponentCategoryType category = ComponentCategoryType.Undefined;
                        DesignSpecification.ResolveComponentTypeFromName(str4.ToLower(CultureInfo.InvariantCulture), out type, out category);
                        if (type != ComponentType.ComputerCommandCenter && category != ComponentCategoryType.HyperDrive)
                        {
                          if (type != ComponentType.Undefined)
                            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, type, result));
                          else if (category != ComponentCategoryType.Undefined)
                            designSpecification.ComponentRules.Add(new DesignSpecificationComponentRule(DesignSpecificationComponentRuleType.MustHave, category, result));
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        else
          designSpecification = !standAlone ? Galaxy.DesignSpecifications.GetBySubRole(subRole) : (DesignSpecification) null;
        return designSpecification;
      }
      return standAlone ? (DesignSpecification) null : Galaxy.DesignSpecifications.GetBySubRole(subRole);
    }

    private static void ResolveComponentTypeFromName(
      string componentName,
      out ComponentType type,
      out ComponentCategoryType category)
    {
      type = ComponentType.Undefined;
      category = ComponentCategoryType.Undefined;
      switch (componentName)
      {
        case "weaponarea":
          type = ComponentType.WeaponAreaDestruction;
          break;
        case "weaponbeam":
          category = ComponentCategoryType.WeaponBeam;
          break;
        case "weapontorpedo":
          category = ComponentCategoryType.WeaponTorpedo;
          break;
        case "weaponmissile":
          type = ComponentType.WeaponMissile;
          break;
        case "weaponbombard":
          type = ComponentType.WeaponBombard;
          break;
        case "weaponphaser":
          type = ComponentType.WeaponPhaser;
          break;
        case "weaponrailgun":
          type = ComponentType.WeaponRailGun;
          break;
        case "weapontractorbeam":
          type = ComponentType.WeaponTractorBeam;
          break;
        case "weapongravitonbeam":
          type = ComponentType.WeaponGravityBeam;
          break;
        case "weaponareagravity":
          type = ComponentType.WeaponAreaGravity;
          break;
        case "assaultpod":
          category = ComponentCategoryType.AssaultPod;
          break;
        case "pointdefense":
          category = ComponentCategoryType.WeaponPointDefense;
          break;
        case "ioncannon":
          type = ComponentType.WeaponIonCannon;
          break;
        case "ionpulse":
          type = ComponentType.WeaponIonPulse;
          break;
        case "iondefense":
          type = ComponentType.WeaponIonDefense;
          break;
        case "hyperdeny":
          type = ComponentType.HyperDeny;
          break;
        case "gravitywellprojector":
          type = ComponentType.HyperStop;
          break;
        case "weaponsuperbeam":
          type = ComponentType.WeaponSuperBeam;
          break;
        case "weaponsupertorpedo":
          type = ComponentType.WeaponSuperTorpedo;
          break;
        case "weaponsuperrailgun":
          type = ComponentType.WeaponSuperRailGun;
          break;
        case "weaponsuperphaser":
          type = ComponentType.WeaponSuperPhaser;
          break;
        case "weaponsupermissile":
          type = ComponentType.WeaponSuperMissile;
          break;
        case "fighterbay":
          type = ComponentType.FighterBay;
          break;
        case "armor":
          type = ComponentType.Armor;
          break;
        case "shields":
          category = ComponentCategoryType.Shields;
          break;
        case "areashieldrecharge":
          category = ComponentCategoryType.ShieldRecharge;
          break;
        case "engine":
          type = ComponentType.EngineMainThrust;
          break;
        case "vectoringengine":
          type = ComponentType.EngineVectoring;
          break;
        case "hyperdrive":
          category = ComponentCategoryType.HyperDrive;
          break;
        case "reactor":
          category = ComponentCategoryType.Reactor;
          break;
        case "energycollector":
          type = ComponentType.EnergyCollector;
          break;
        case "energytofuelconverter":
          type = ComponentType.EnergyToFuel;
          break;
        case "miningengine":
          type = ComponentType.ExtractorMine;
          break;
        case "gasextractor":
          type = ComponentType.ExtractorGasExtractor;
          break;
        case "luxuryresourceextractor":
          type = ComponentType.ExtractorLuxury;
          break;
        case "weaponsmanufacturingplant":
          type = ComponentType.ManufacturerWeaponsPlant;
          break;
        case "energymanufacturingplant":
          type = ComponentType.ManufacturerEnergyPlant;
          break;
        case "hightechmanufacturingplant":
          type = ComponentType.ManufacturerHighTechPlant;
          break;
        case "fuelcell":
          type = ComponentType.StorageFuel;
          break;
        case "cargobay":
          type = ComponentType.StorageCargo;
          break;
        case "troopcompartment":
          type = ComponentType.StorageTroop;
          break;
        case "passengercompartment":
          type = ComponentType.StoragePassenger;
          break;
        case "dockingbay":
          type = ComponentType.StorageDockingBay;
          break;
        case "proximityarray":
          type = ComponentType.SensorProximityArray;
          break;
        case "resourceprofilesensor":
          type = ComponentType.SensorResourceProfileSensor;
          break;
        case "longrangescanner":
          type = ComponentType.SensorLongRange;
          break;
        case "tracescanner":
          type = ComponentType.SensorTraceScanner;
          break;
        case "scannerjammer":
          type = ComponentType.SensorScannerJammer;
          break;
        case "stealthcloak":
          type = ComponentType.SensorStealth;
          break;
        case "combattargettingsystem":
          type = ComponentType.ComputerTargetting;
          break;
        case "countermeasuressystem":
          type = ComponentType.ComputerCountermeasures;
          break;
        case "commandcenter":
          type = ComponentType.ComputerCommandCenter;
          break;
        case "commercecenter":
          type = ComponentType.ComputerCommerceCenter;
          break;
        case "fleettargettingsystem":
          type = ComponentType.ComputerTargettingFleet;
          break;
        case "fleetcountermeasuressystem":
          type = ComponentType.ComputerCountermeasuresFleet;
          break;
        case "weaponsresearchlab":
          type = ComponentType.LabsWeaponsLab;
          break;
        case "energyresearchlab":
          type = ComponentType.LabsEnergyLab;
          break;
        case "hightechresearchlab":
          type = ComponentType.LabsHighTechLab;
          break;
        case "constructionyard":
          type = ComponentType.ConstructionBuild;
          break;
        case "damagecontrol":
          type = ComponentType.DamageControl;
          break;
        case "lifesupport":
          type = ComponentType.HabitationLifeSupport;
          break;
        case "habmodule":
          type = ComponentType.HabitationHabModule;
          break;
        case "medicalcenter":
          type = ComponentType.HabitationMedicalCenter;
          break;
        case "recreationcenter":
          type = ComponentType.HabitationRecreationCenter;
          break;
        case "colonizationmodule":
          type = ComponentType.HabitationColonization;
          break;
      }
    }
  }
}
