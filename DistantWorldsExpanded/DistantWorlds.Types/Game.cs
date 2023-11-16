// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Game
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Game
  {
    public Galaxy Galaxy;
    private Empire _PlayerEmpire;
    private string _PlayerName;
    private string _Version;
    private bool _GodMode;
    private string _EditorPassword;
    private double _ViewX;
    private double _ViewY;
    private double _ZoomFactor = 1.0;
    [OptionalField]
    private DateTime _LastSystemMapUpdate;
    [OptionalField]
    private DateTime _LastInfoPanelUpdate;
    [OptionalField]
    private object _SelectedObject;
    private double _SoundEffectsVolume;
    private double _MusicVolume;
    private int _DifficultyLevel;
    private int _ResearchSpeed;
    private int _MainViewScrollSpeed;
    private int _StarFieldSize;
    private bool _ShowSystemNebulae;
    [OptionalField]
    private int _MainViewZoomSpeed = 30;
    private bool _AutoPauseWhenInPopupWindow;
    [OptionalField]
    private int _MouseScrollWheelBehaviour;
    private bool _IsFinished;
    private Empire _Victor;
    [OptionalField]
    private string _CustomizationSetName;
    private List<int> _PlayerEmpireFirstDialogDoneEmpireIds = new List<int>();
    public object PlayerHotkey0;
    public object PlayerHotkey1;
    public object PlayerHotkey2;
    public object PlayerHotkey3;
    public object PlayerHotkey4;
    public object PlayerHotkey5;
    public object PlayerHotkey6;
    public object PlayerHotkey7;
    public object PlayerHotkey8;
    public object PlayerHotkey9;
    [OptionalField]
    public bool PlayAsAPirate;
    [OptionalField]
    public bool AgeOfShadows;
    private VictoryConditions _VictoryConditions;
    private EmpireVictoryConditions _PlayerVictoryConditionsToAchieve;
    private EmpireVictoryConditions _PlayerVictoryConditionsToPrevent;
    private bool _MainViewShowResources;
    private bool _MainViewShowShields;
    private bool _MainViewShowEmpireFlag;
    private bool _MainViewShowPopulationRaces;
    private bool _MainViewShowPopulationIndicator;
    [OptionalField]
    public bool DisplayMessageUnderAttackCivilianShips = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackCivilianBases = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackExplorationShips = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackColonyConstructionShips = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackMilitaryShips = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackOtherStateBases = true;
    [OptionalField]
    public bool DisplayMessageUnderAttackColoniesSpaceportsDefensiveBases = true;
    [OptionalField]
    public bool DisplayMessageConstructionResourceShortage = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackCivilianShips = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackCivilianBases = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackExplorationShips = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackColonyConstructionShips = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackMilitaryShips = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackOtherStateBases = true;
    [OptionalField]
    public bool DisplayPopupUnderAttackColoniesSpaceportsDefensiveBases = true;
    [OptionalField]
    public bool DisplayPopupConstructionResourceShortage = true;
    private bool _DisplayMessageBuiltObjectBuilt;
    private bool _DisplayMessageDiplomacyGift;
    private bool _DisplayMessageDiplomacyTreaty;
    private bool _DisplayMessageDiplomacyWarTradeSanctions;
    private bool _DisplayMessageDiplomacyEmpireMetDestroyed;
    private bool _DisplayMessageDiplomacyRequestWarning;
    private bool _DisplayMessageNewColony;
    private bool _DisplayMessageColonyInvaded;
    private bool _DisplayMessageResearchNewComponent;
    private bool _DisplayMessageIntelligenceMissions;
    private bool _DisplayMessageExploration;
    private bool _DisplayMessageShipMissionComplete;
    private bool _DisplayMessageShipNeedsRefuelling;
    private bool _DisplayPopupBuiltObjectBuilt;
    private bool _DisplayPopupDiplomacyGift;
    private bool _DisplayPopupDiplomacyTreaty;
    private bool _DisplayPopupDiplomacyWarTradeSanctions;
    private bool _DisplayPopupDiplomacyEmpireMetDestroyed;
    private bool _DisplayPopupDiplomacyRequestWarning;
    private bool _DisplayPopupNewColony;
    private bool _DisplayPopupColonyInvaded;
    private bool _DisplayPopupResearchNewComponent;
    private bool _DisplayPopupIntelligenceMissions;
    private bool _DisplayPopupExploration;
    private bool _DisplayPopupShipMissionComplete;
    private bool _DisplayPopupShipNeedsRefuelling;

    public string CustomizationSetName
    {
      get => this._CustomizationSetName;
      set => this._CustomizationSetName = value;
    }

    public List<int> PlayerEmpireFirstDialogDoneEmpireIds => this._PlayerEmpireFirstDialogDoneEmpireIds;

    public string EditorPassword
    {
      get => this._EditorPassword;
      set => this._EditorPassword = value;
    }

    public string Version
    {
      get => this._Version;
      set => this._Version = value;
    }

    public int StarFieldSize
    {
      get => this._StarFieldSize;
      set => this._StarFieldSize = value;
    }

    public bool ShowSystemNebulae
    {
      get => this._ShowSystemNebulae;
      set => this._ShowSystemNebulae = value;
    }

    public Empire Victor
    {
      get => this._Victor;
      set => this._Victor = value;
    }

    public bool IsFinished
    {
      get => this._IsFinished;
      set => this._IsFinished = value;
    }

    public double SoundEffectsVolume
    {
      get => this._SoundEffectsVolume;
      set => this._SoundEffectsVolume = value;
    }

    public double MusicVolume
    {
      get => this._MusicVolume;
      set => this._MusicVolume = value;
    }

    public int MainViewScrollSpeed
    {
      get => this._MainViewScrollSpeed;
      set => this._MainViewScrollSpeed = value;
    }

    public int MainViewZoomSpeed
    {
      get => this._MainViewZoomSpeed;
      set => this._MainViewZoomSpeed = value;
    }

    public bool AutoPauseWhenInPopupWindow
    {
      get => this._AutoPauseWhenInPopupWindow;
      set => this._AutoPauseWhenInPopupWindow = value;
    }

    public int MouseScrollWheelBehaviour
    {
      get => this._MouseScrollWheelBehaviour;
      set => this._MouseScrollWheelBehaviour = value;
    }

    public EmpireVictoryConditions PlayerVictoryConditionsToAchieve
    {
      get => this._PlayerVictoryConditionsToAchieve;
      set => this._PlayerVictoryConditionsToAchieve = value;
    }

    public EmpireVictoryConditions PlayerVictoryConditionsToPrevent
    {
      get => this._PlayerVictoryConditionsToPrevent;
      set => this._PlayerVictoryConditionsToPrevent = value;
    }

    public VictoryConditions GlobalVictoryConditions
    {
      get => this._VictoryConditions;
      set => this._VictoryConditions = value;
    }

    public bool DisplayMessageShipMissionComplete
    {
      get => this._DisplayMessageShipMissionComplete;
      set => this._DisplayMessageShipMissionComplete = value;
    }

    public bool DisplayMessageShipNeedsRefuelling
    {
      get => this._DisplayMessageShipNeedsRefuelling;
      set => this._DisplayMessageShipNeedsRefuelling = value;
    }

    public bool DisplayMessageExploration
    {
      get => this._DisplayMessageExploration;
      set => this._DisplayMessageExploration = value;
    }

    public bool DisplayMessageIntelligenceMissions
    {
      get => this._DisplayMessageIntelligenceMissions;
      set => this._DisplayMessageIntelligenceMissions = value;
    }

    public bool DisplayMessageDiplomacyTreaty
    {
      get => this._DisplayMessageDiplomacyTreaty;
      set => this._DisplayMessageDiplomacyTreaty = value;
    }

    public bool DisplayMessageDiplomacyWarTradeSanctions
    {
      get => this._DisplayMessageDiplomacyWarTradeSanctions;
      set => this._DisplayMessageDiplomacyWarTradeSanctions = value;
    }

    public bool DisplayMessageDiplomacyEmpireMetDestroyed
    {
      get => this._DisplayMessageDiplomacyEmpireMetDestroyed;
      set => this._DisplayMessageDiplomacyEmpireMetDestroyed = value;
    }

    public bool DisplayMessageDiplomacyRequestWarning
    {
      get => this._DisplayMessageDiplomacyRequestWarning;
      set => this._DisplayMessageDiplomacyRequestWarning = value;
    }

    public bool DisplayMessageNewColony
    {
      get => this._DisplayMessageNewColony;
      set => this._DisplayMessageNewColony = value;
    }

    public bool DisplayMessageColonyInvaded
    {
      get => this._DisplayMessageColonyInvaded;
      set => this._DisplayMessageColonyInvaded = value;
    }

    public bool DisplayMessageResearchNewComponent
    {
      get => this._DisplayMessageResearchNewComponent;
      set => this._DisplayMessageResearchNewComponent = value;
    }

    public bool DisplayMessageBuiltObjectBuilt
    {
      get => this._DisplayMessageBuiltObjectBuilt;
      set => this._DisplayMessageBuiltObjectBuilt = value;
    }

    public bool DisplayMessageDiplomacyGift
    {
      get => this._DisplayMessageDiplomacyGift;
      set => this._DisplayMessageDiplomacyGift = value;
    }

    public bool DisplayPopupShipMissionComplete
    {
      get => this._DisplayPopupShipMissionComplete;
      set => this._DisplayPopupShipMissionComplete = value;
    }

    public bool DisplayPopupShipNeedsRefuelling
    {
      get => this._DisplayPopupShipNeedsRefuelling;
      set => this._DisplayPopupShipNeedsRefuelling = value;
    }

    public bool DisplayPopupExploration
    {
      get => this._DisplayPopupExploration;
      set => this._DisplayPopupExploration = value;
    }

    public bool DisplayPopupIntelligenceMissions
    {
      get => this._DisplayPopupIntelligenceMissions;
      set => this._DisplayPopupIntelligenceMissions = value;
    }

    public bool DisplayPopupDiplomacyTreaty
    {
      get => this._DisplayPopupDiplomacyTreaty;
      set => this._DisplayPopupDiplomacyTreaty = value;
    }

    public bool DisplayPopupDiplomacyWarTradeSanctions
    {
      get => this._DisplayPopupDiplomacyWarTradeSanctions;
      set => this._DisplayPopupDiplomacyWarTradeSanctions = value;
    }

    public bool DisplayPopupDiplomacyEmpireMetDestroyed
    {
      get => this._DisplayPopupDiplomacyEmpireMetDestroyed;
      set => this._DisplayPopupDiplomacyEmpireMetDestroyed = value;
    }

    public bool DisplayPopupDiplomacyRequestWarning
    {
      get => this._DisplayPopupDiplomacyRequestWarning;
      set => this._DisplayPopupDiplomacyRequestWarning = value;
    }

    public bool DisplayPopupNewColony
    {
      get => this._DisplayPopupNewColony;
      set => this._DisplayPopupNewColony = value;
    }

    public bool DisplayPopupColonyInvaded
    {
      get => this._DisplayPopupColonyInvaded;
      set => this._DisplayPopupColonyInvaded = value;
    }

    public bool DisplayPopupResearchNewComponent
    {
      get => this._DisplayPopupResearchNewComponent;
      set => this._DisplayPopupResearchNewComponent = value;
    }

    public bool DisplayPopupBuiltObjectBuilt
    {
      get => this._DisplayPopupBuiltObjectBuilt;
      set => this._DisplayPopupBuiltObjectBuilt = value;
    }

    public bool DisplayPopupDiplomacyGift
    {
      get => this._DisplayPopupDiplomacyGift;
      set => this._DisplayPopupDiplomacyGift = value;
    }

    public Empire PlayerEmpire
    {
      get => this._PlayerEmpire;
      set => this._PlayerEmpire = value;
    }

    public bool GodMode
    {
      get => this._GodMode;
      set => this._GodMode = value;
    }

    public double ViewX
    {
      get => this._ViewX;
      set => this._ViewX = value;
    }

    public double ViewY
    {
      get => this._ViewY;
      set => this._ViewY = value;
    }

    public double ZoomFactor
    {
      get => this._ZoomFactor;
      set => this._ZoomFactor = value;
    }

    public DateTime LastSystemMapUpdate
    {
      get => this._LastSystemMapUpdate;
      set => this._LastSystemMapUpdate = value;
    }

    public DateTime LastInfoPanelUpdate
    {
      get => this._LastInfoPanelUpdate;
      set => this._LastInfoPanelUpdate = value;
    }

    public object SelectedObject
    {
      get => this._SelectedObject;
      set => this._SelectedObject = value;
    }
  }
}
