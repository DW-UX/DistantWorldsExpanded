// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Ruin
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Ruin
  {
    private string _Name;
    private string _Description;
    private double _DevelopmentBonus;
    private int _PictureRef;
    private double _ParentX;
    private double _ParentY;
    private int _ResearchBonus;
    private int _MapSystemReveal;
    private double _MoneyBonus;
    public short GameEventId = short.MinValue;
    private bool _PlayerEmpireEncountered;
    private RuinType _Type = RuinType.Standard;
    private Race _HabitatNewRace;
    private int _SpecialGovernmentId;
    private Race _OriginsRace;
    private int _OriginsApprovalRatingBonus;
    private int _ResearchProjectId;
    private bool _RefugeesGenerated;
    private bool _LostBuiltObjectGenerated;
    private bool _LostColonyGenerated;
    private bool _CreatureSwarmGenerated;
    private bool _PirateAmbushGenerated;
    private double _BonusResearchEnergy;
    private double _BonusResearchHighTech;
    private double _BonusResearchWeapons;
    private double _BonusWealth;
    private double _BonusHappiness;
    private double _BonusDiplomacy;
    private double _BonusDefensive;
    private int _StoryEventData;
    private int _StoryClueLevel = -1;

    public int StoryEventData
    {
      get => this._StoryEventData;
      set => this._StoryEventData = value;
    }

    public int StoryClueLevel
    {
      get => this._StoryClueLevel;
      set => this._StoryClueLevel = value;
    }

    public bool CreatureSwarmGenerated
    {
      get => this._CreatureSwarmGenerated;
      set => this._CreatureSwarmGenerated = value;
    }

    public bool PirateAmbushGenerated
    {
      get => this._PirateAmbushGenerated;
      set => this._PirateAmbushGenerated = value;
    }

    public bool PlayerEmpireEncountered
    {
      get => this._PlayerEmpireEncountered;
      set => this._PlayerEmpireEncountered = value;
    }

    public bool RefugeesGenerated
    {
      get => this._RefugeesGenerated;
      set => this._RefugeesGenerated = value;
    }

    public bool LostBuiltObjectGenerated
    {
      get => this._LostBuiltObjectGenerated;
      set => this._LostBuiltObjectGenerated = value;
    }

    public bool LostColonyGenerated
    {
      get => this._LostColonyGenerated;
      set => this._LostColonyGenerated = value;
    }

    public Race HabitatNewRace
    {
      get => this._HabitatNewRace;
      set => this._HabitatNewRace = value;
    }

    public int SpecialGovernmentId
    {
      get => this._SpecialGovernmentId;
      set => this._SpecialGovernmentId = value;
    }

    public Race OriginsRace
    {
      get => this._OriginsRace;
      set => this._OriginsRace = value;
    }

    public int OriginsApprovalRatingBonus
    {
      get => this._OriginsApprovalRatingBonus;
      set => this._OriginsApprovalRatingBonus = value;
    }

    public double MoneyBonus
    {
      get => this._MoneyBonus;
      set => this._MoneyBonus = value;
    }

    public int ResearchProjectId
    {
      get => this._ResearchProjectId;
      set => this._ResearchProjectId = value;
    }

    public RuinType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    public string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    public double DevelopmentBonus
    {
      get => this._DevelopmentBonus;
      set => this._DevelopmentBonus = value;
    }

    public int PictureRef
    {
      get => this._PictureRef;
      set => this._PictureRef = value;
    }

    public double ParentX => this._ParentX;

    public double ParentY => this._ParentY;

    public int ResearchBonus
    {
      get => this._ResearchBonus;
      set => this._ResearchBonus = value;
    }

    public int MapSystemReveal
    {
      get => this._MapSystemReveal;
      set => this._MapSystemReveal = value;
    }

    public double BonusResearchEnergy
    {
      get => this._BonusResearchEnergy;
      set => this._BonusResearchEnergy = value;
    }

    public double BonusResearchHighTech
    {
      get => this._BonusResearchHighTech;
      set => this._BonusResearchHighTech = value;
    }

    public double BonusResearchWeapons
    {
      get => this._BonusResearchWeapons;
      set => this._BonusResearchWeapons = value;
    }

    public double BonusWealth
    {
      get => this._BonusWealth;
      set => this._BonusWealth = value;
    }

    public double BonusHappiness
    {
      get => this._BonusHappiness;
      set => this._BonusHappiness = value;
    }

    public double BonusDiplomacy
    {
      get => this._BonusDiplomacy;
      set => this._BonusDiplomacy = value;
    }

    public double BonusDefensive
    {
      get => this._BonusDefensive;
      set => this._BonusDefensive = value;
    }

    public void ClearBonuses()
    {
      this.MoneyBonus = 0.0;
      this.MapSystemReveal = 0;
      this.ResearchBonus = 0;
      this.HabitatNewRace = (Race) null;
      this.LostBuiltObjectGenerated = true;
      this.LostColonyGenerated = true;
      this.OriginsApprovalRatingBonus = 0;
      this.OriginsRace = (Race) null;
      this.RefugeesGenerated = true;
      this.CreatureSwarmGenerated = true;
      this.PirateAmbushGenerated = true;
      if (this._Type != RuinType.UnlockResearchProject)
        this.ResearchProjectId = -1;
      this.SpecialGovernmentId = -1;
      this.StoryEventData = 0;
    }

    public Ruin(
      string name,
      int pictureRef,
      double developmentBonus,
      double parentX,
      double parentY,
      int researchBonus,
      int mapSystemReveal,
      int moneyBonus)
    {
      this._Name = name;
      this._PictureRef = pictureRef;
      this._DevelopmentBonus = developmentBonus;
      this._ParentX = parentX;
      this._ParentY = parentY;
      this._ResearchBonus = researchBonus;
      this._MapSystemReveal = mapSystemReveal;
      this._MoneyBonus = (double) moneyBonus;
      this._PlayerEmpireEncountered = false;
    }
  }
}
