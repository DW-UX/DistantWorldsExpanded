// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EmpireStart
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EmpireStart : IComparable<EmpireStart>
  {
    private string _StartLocation;
    private string _HomeSystemFavourability;
    private int _Age;
    private double _TechLevel;
    private string _Name;
    private string _ProximityDistance;
    private string _GovernmentStyle;
    private string _Race;
    private int _RaceIndex = -1;
    private Color _PrimaryColor = Color.Empty;
    private Color _SecondaryColor = Color.Empty;
    private int _FlagShape = -1;
    private int _DesignPictureFamilyIndex = -1;
    private double _CorruptionMultiplier = 1.1;
    private DistantWorlds.Types.Race _ResolvedRace;
    private int _ProjectedColonyAmount;
    public double ColonyRevenueFactor = 1.0;
    public double DifficultyLevel = 1.0;
    public bool DifficultyScaling;
    public bool DestroyedPiratesDoNotRespawn;
    public double PirateShipMaintenanceFactor = 0.4;
    public bool AllowTechTrading = true;
    public PiratePlayStyle PiratePlayStyle;
    public int GalaxySectorX = 10;
    public int GalaxySectorY = 10;
    public float EmpireTerritoryColonyInfluenceRangeFactor = 1f;
    public bool ColonizationRangeEnforceLimit = true;
    public float ColonizationRange = 3000000f;
    public bool AllowGiantKaltorGeneration = true;

    public int DesignPictureFamilyIndex
    {
      get => this._DesignPictureFamilyIndex;
      set => this._DesignPictureFamilyIndex = value;
    }

    public string StartLocation
    {
      get => this._StartLocation;
      set => this._StartLocation = value;
    }

    public string HomeSystemFavourability
    {
      get => this._HomeSystemFavourability;
      set => this._HomeSystemFavourability = value;
    }

    public double CorruptionMultiplier
    {
      get => this._CorruptionMultiplier;
      set => this._CorruptionMultiplier = value;
    }

    public int Age
    {
      get => this._Age;
      set => this._Age = value;
    }

    public double TechLevel
    {
      get => this._TechLevel;
      set => this._TechLevel = value;
    }

    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    public string ProximityDistance
    {
      get => this._ProximityDistance;
      set => this._ProximityDistance = value;
    }

    public string GovernmentStyle
    {
      get => this._GovernmentStyle;
      set => this._GovernmentStyle = value;
    }

    public string Race
    {
      get => this._Race;
      set => this._Race = value;
    }

    public int RaceIndex
    {
      get => this._RaceIndex;
      set => this._RaceIndex = value;
    }

    public Color PrimaryColor
    {
      get => this._PrimaryColor;
      set => this._PrimaryColor = value;
    }

    public Color SecondaryColor
    {
      get => this._SecondaryColor;
      set => this._SecondaryColor = value;
    }

    public int FlagShape
    {
      get => this._FlagShape;
      set => this._FlagShape = value;
    }

    public DistantWorlds.Types.Race ResolvedRace
    {
      get => this._ResolvedRace;
      set => this._ResolvedRace = value;
    }

    public int ProjectedColonyAmount
    {
      get => this._ProjectedColonyAmount;
      set => this._ProjectedColonyAmount = value;
    }

    int IComparable<EmpireStart>.CompareTo(EmpireStart other) => this.ProjectedColonyAmount.CompareTo(other.ProjectedColonyAmount);
  }
}
