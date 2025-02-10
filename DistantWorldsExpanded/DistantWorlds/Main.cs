// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.
// DistantWorlds.Main
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Linq;
//using System.Management;
using System.Media;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using BaconDistantWorlds;
using ExpansionMod;
using DistantWorlds.Controls;
using DistantWorlds.Types;
//using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework.Graphics;
//using SlimDX.DirectSound;
using ExpansionMod.HotKeyMapping;
using ExpansionMod.Objects;
using System.Collections.Concurrent;

namespace DistantWorlds
{
    public partial class Main : Form, IFontCache, IMessageRecipient, IAutomationAuthorizer, IEventMessageRecipient
    {
        private delegate void Delegate0(Bitmap image);

        public delegate void SetMessagePopupPositionDelegate(int x, int y);

        public delegate void SetMainFocusDelegate();

        private delegate void Delegate1(string customizationSetName, bool changeDefault);

        private delegate void Delegate2(string message);

        public delegate void GameEndMessageDelegate();

        private delegate void Delegate3(GameEndEventArgs e);

        private delegate void Delegate4(string message, object target, EmpireMessage empireMessage);

        private delegate void Delegate5(EmpireMessage message);

        public delegate void RecoverFromLoadSaveErrorDelegate();

        private delegate void Delegate6(EventMessageType eventMessageType, string title, string message, object additionalData, object location);

        private IContainer icontainer_0;

        private ScreenPanel CaLkaMyrMQ;

        private SystemView picSystemMap;

        private SystemView picSystem;

        private ListView lvwHabitats;

        private ColumnHeader columnHeader_0;

        private TextBox txtHabitatSearch;

        private Label lblSystemName;

        private Label lblStarDate;

        private ScreenPanel pnlColonyInfo;

        public HabitatListView UnlxwvByxj;

        private Label lblColonyHabitats;

        private HabitatResourceListView GalaxyHabitatResourceListView;

        public ScreenPanel pnlBuiltObjectInfo;

        private BuiltObjectListView ctlBuiltObjectList;

        private Label lblBuiltObjectDockingWaitQueue;

        private GlassButton btnColonyShowOnGalaxyMap;

        private GlassButton btnColonyGotoHabitat;

        private GlassButton btnBuiltObjectViewDesign;

        private GlassButton btnBuiltObjectViewShipGroup;

        private ScreenPanel pnlTroopInfo;

        private Label lblTroopList;

        private TroopListView ctlTroopList;

        private BorderPanel pnlCharacterInfo;

        private Label lblCharacterList;

        private Label lblCharacterInfoTitle;

        private CharacterListView ctlCharacterList;

        private GlassButton btnCharacterInfoClose;

        private GlassButton btnBuiltObjectGoto;

        private TextBox hvhxxedjqS;

        private Label lblBuiltObjectName;

        private TextBox txtColonyName;

        private Label lblColonyName;

        private Label lblColonyTaxRate;

        private NumericUpDown numColonyTaxRate;

        private ScreenPanel pnlEmpireInfo;

        private EmpireDetailView pnlEmpireDetailInfo;

        private DiplomaticRelationListView ctlEmpireDiplomaticRelationList;

        private GlassButton btnEmpireTalk;

        public MainView mainView;

        private ScrollingLinkList lstMessages;

        private GlassButton btnCycleBases;

        private GlassButton btnCycleMilitary;

        private GlassButton btnCycleConstruction;

        private GlassButton btnCycleColonies;

        private GlassButton btnCycleOther;

        private GlassButton tbtnColonies;

        private GlassButton tbtnBuiltObjects;

        private GlassButton tbtnEmpires;

        private GlassButton tbtnTroops;

        private GlassButton tbtnGalaxyMap;

        private GlassButton tbtnShipGroups;

        private GlassButton btnLockView;

        public EnhancedTabControl tabBuiltObjectData;

        private TabPage tabBuiltObject_Cargo;

        private TabPage kuvxnccAc2;

        private TabPage tabBuiltObject_ConstructionYards;

        private TabPage tabBuiltObject_DockingBays;

        private TabPage tabBuiltObject_Troops;

        private TabPage tabBuiltObject_Weapons;

        private CargoListView ctlBuiltObjectCargo;

        private ComponentResourceListView ctlBuiltObjectComponentsResources;

        private Label lblBuiltObjectComponentsResources;

        private BuiltObjectComponentListView ctlBuiltObjectComponents;

        private ManufacturerListView duExoPvEoA;

        private Label lblConstructionYardManufacturers;

        private ConstructionYardListView ctlConstructionYards;

        private Label lblConstructionYardManufacturerWaitQueue;

        private ComponentListView ctlConstructionYardManufacturerWaitQueue;

        private Label lblConstructionYardWaitQueue;

        private BuiltObjectListView ctlConstructionYardWaitQueue;

        private Label lblDockingBayWaitQueue;

        private BuiltObjectListView ctlDockingWaitQueue;

        private DockingBayListView ctlDockingBays;

        private CharacterTroopListIconView ctlBuiltObjectCharactersTroops;

        private WeaponListView ctlWeapons;

        private EnhancedTabControl tabColonyData;

        private TabPage tabColony_Cargo;

        private TabPage tabColony_Resources;

        private TabPage tabColony_Population;

        private HabitatResourceListView ctlColonyResources;

        private PopulationListView ctlColonyPopulation;

        private TabPage tabColony_Troops;

        private CharacterTroopListIconView ctlColonyCharacterTroops;

        private TabPage tabColony_ConstructionYard;

        private ConstructionYardListView ctlColonyConstructionYard;

        private TabPage tabColony_DockingBay;

        private CargoListView ctlColonyCargo;

        private DockingBayListView ctlColonyDockingBay;

        private BuiltObjectListView ctlColonyDockingBayWaitQueue;

        private BuiltObjectListView ctlColonyConstructionYardWaitQueue;

        private Label lblColonyConstructionYardWaitQueue;

        private Label lblColonyDockingBayWaitQueue;

        private Label lblStateMoney;

        private Label lblPrivateMoney;

        private Label lblGodData;

        private InfoPanel pnlDetailInfo;

        private GlassButton btnZoomColony;

        private GlassButton btnZoomSystem;

        private GlassButton jQaYpdpkDs;

        private GlassButton btnZoomRegion;

        private ScreenPanel pnlShipGroupInfo;

        private GalaxyMap gmapShipGroupInfo;

        private InfoPanel pnlDetailInfoShipGroup;

        private ShipGroupListView ctlShipGroupListView;

        private GlassButton btnShipGroupGoto;

        private TextBox txtShipGroupName;

        private Label lblShipGroupName;

        private GalaxyMap gmapMain;

        private Label lblGalaxyMapViewModeLabel;

        private ComboBox cmbGalaxyMapViewMode;

        private ComboBox omjYcxcvXH;

        private GalaxyMap gmapColony;

        private GalaxyMap gmapBuiltObject;

        private InfoPanel pnlHabitatInfo;

        private InfoPanel pnlBuiltObjectDetail;

        private InfoPanel pnlColonyHabitatInfo;

        private GalaxyMap dboYnQplv3;

        private GlassButton btnTroopDisband;

        private Label lblTroopSummary;

        private Label lblTroopInfoName;

        private TextBox txtTroopInfoName;

        private GlassButton btnTroopGoto;

        private ScreenPanel pnlEmpireSummary;

        private EmpireSummaryEconomy pnlEmpireSummaryEconomy;

        private TextBox txtEmpireSummaryName;

        private Label lblEmpireSummaryName;

        private EmpireSummaryBuiltObject pnlEmpireSummaryBuiltObject;

        private EmpireSummaryColony pnlEmpireSummaryColony;

        private GlassButton btnGalaxyMapGoto;

        private ResearchButton tbtnResearch;

        private GlassButton tbtnDesigns;

        private GlassButton btnCycleShipGroups;

        private GlassButton btnZoomOut;

        private GlassButton btnZoomIn;

        private GlassButton btnCycleIdleShips;

        private GlassButton btnEmpireGraphs;

        private GlassButton btnPlayPause;

        private GlassButton btnGameMenu;

        private GlassButton btnEmpireSummary;

        public ContextMenuStrip actionMenu;

        internal ContextMenuStrip selectionMenu;

        private BorderPanel pnlGameMenu;

        private Label lblGameMenuTitle;

        private GlassButton btnGameMenuSave;

        private GlassButton btnGameMenuLoad;

        private GlassButton btnGameMenuQuit;

        private GlassButton btnGameMenuOptions;

        private GlassButton btnGameMenuSaveAs;

        private GlassButton btnGameMenuStartMenu;

        private BorderPanel pnlSaveLoadProgress;

        private PictureBox picSaveLoadGalaxy;

        private GlassButton btnGameMenuCancel;

        private PictureBox picGameMenuHeader;

        private ScreenPanel pnlDesigns;

        public DesignListView ctlDesignsList;

        private GlassButton btnDesignsDelete;

        private GlassButton btnDesignsCopyAsNew;

        private GlassButton btnDesignsAddNew;

        private BorderPanel pnlDesignDetail;

        private GradientPanel pnlDesignWeapons;

        private Label lblDesignWeaponHyperDenyValue;

        private Label lblDesignWeaponHyperDeny;

        private Label lblDesignWeaponTroopCapacityValue;

        private Label lblDesignWeaponTroopCapacity;

        private Label lblDesignWeaponTargettingValue;

        private Label lblDesignWeaponTargetting;

        private Label lblDesignWeaponFirepowerValue;

        private Label lblDesignWeaponFirepower;

        private Label lblDesignsWeaponsTitle;

        private WeaponListView ctlDesignWeapons;

        private GradientPanel pnlDesignBasics;

        private Label lblDesignsSizeValue;

        private Label lblDesignsFleeWhen;

        private ComboBox cmbDesignsFleeWhen;

        private Label lblDesignsStance;

        private ComboBox cmbDesignsStance;

        private PictureBox picDesignPicture;

        private Label lblDesignsPicture;

        private ComboBox cmbDesignsPicture;

        private Label lblDesignsSize;

        private Label lblDesignsBasicsSubRole;

        private ComboBox cmbDesignsSubRole;

        private Label lblDesignDetailTitle;

        private GlassButton btnDesignsCancel;

        private GlassButton btnDesignsSaveDesign;

        private DesignDefense pnlDesignDefense;

        private DesignWarnings pnlDesignWarnings;

        private DesignIndustry pnlDesignIndustry;

        private DesignEnergy pnlDesignEnergy;

        private DesignMovement pnlDesignMovement;

        private Label lblDesignName;

        private TextBox txtDesignName;

        private GlassButton btnRemoveComponentFromDesign;

        private GlassButton btnAddComponentToDesign;

        private CheckBox chkDesignComponentsShowLatest;

        private GlassButton _btnRepairPrioritySelect;

        private Label _lblCurrentRepairPriorityTemplate;

        public ComponentListView ctlDesignComponents;

        public ComponentListView ctlDesignComponentToolbox;

        private GlassButton btnDesignsEdit;

        private ComponentDetail pnlDesignComponentDetail;

        private Label lblDesignTacticsInvasion;

        private ComboBox cmbDesignTacticsInvasion;

        private Label lblDesignTacticsWeakerShips;

        private ComboBox cmbDesignTacticsWeakerShips;

        private Label lblDesignTacticsStrongerShips;

        private ComboBox cmbDesignTacticsStrongerShips;

        private Label lblDesignWeaponRangeMinimumValue;

        private Label lblDesignWeaponRangeMinimum;

        private Label lblDesignWeaponRangeMaximumValue;

        private Label lblDesignWeaponRangeMaximum;

        private System.Windows.Forms.Panel pnlDesignComponentsHighlight;

        private BorderPanel pnlDiplomacyTalk;

        private DiplomacyTradeTree ctlDiplomacyTradeUs;

        private DiplomacyTradeTree ctlDiplomacyTradeThem;

        private PictureBox picRace;

        private PictureBox picFlag;

        private HyperlinkOptionsBox ctlDiplomacyConversation;

        private MessagePanel pnlDiplomaticConversationResponse;

        private ScreenPanel pnlResearch;

        private GlassButton btnResearchGotoFacility;

        private ResearchFacilities ctlResearchFacilities;

        private ResearchSummary pnlResearchSummary;

        private Label lblResearchEmpireLabel;

        private Label lblResearchEmpireGovernmentModifier;

        private Label lblResearchEmpireGovernment;

        private Label lblResearchEmpirePotential;

        private ScreenPanel vHfFsoqMev;

        private EnhancedTabControl tabEmpireComparisonGraphs;

        private TabPage tabPopulation;

        private TabPage tabTerritory;

        private TabPage tabEconomy;

        private TabPage tabStrategicValue;

        private TabPage tabMilitaryStrength;

        private TabPage tabTopColonies;

        private EmpireComparison pnlEmpireComparisonTerritory;

        private EmpireComparison pnlEmpireComparisonEconomy;

        private EmpireComparison pnlEmpireComparisonStrategicValue;

        private EmpireComparison pnlEmpireComparisonMilitaryStrength;

        private EmpireComparison pnlEmpireComparisonPopulation;

        private TopColonies pnlEmpireComparisonTopColonies;

        private ScreenPanel pnlGameOptions;

        private GroupBox grpOptionsControl;

        private CheckBox chkOptionsControlTroops;

        private CheckBox chkOptionsControlDesigns;

        private CheckBox chkOptionsControlFleets;

        private CheckBox chkOptionsControlColonyTaxRates;

        private GroupBox grpOptionsVolume;

        private Label lblOptionsSoundEffectsVolume;

        private Label lblOptionsMusicVolume;

        private ColorSlider sldOptionsSoundEffectsVolume;

        private ColorSlider sldOptionsMusicVolume;

        private CheckBox chkOptionsAutoPauseInPopup;

        private MessagePopup pnlMessagePopup;

        private GalaxyMap gmapEmpireDetail;

        private PictureBox pnlGalaxyMapHabitatPicture;

        private Label lblColonyCount;

        private ComboBox cmbBuiltObjectSetFleet;

        private CheckBox chkDesignObsolete;

        private GlassButton btnEmpireSummaryChangeGovernment;

        private ComboBox cmbEmpireSummaryChangeGovernmentType;

        public ComboBox cmbBuiltObjectFilter;

        private GlassButton btnMainViewDisplayToggle;

        private DiplomaticRelationColorKey pnlDiplomaticRelationColorKey;

        private ScreenPanel kYdDyYeMls;

        private CharacterMission pnlCharacterMission;

        private CharacterListView ctlIntelligenceAgents;

        private Label lblIntelligenceAgentSummary;

        private GlassButton tbtnIntelligenceAgents;

        private GlassButton tbtnConstructionYards;

        private Label lblIntelligenceAgentHeader;

        private GlassButton btnIntelligenceAgentsDisband;

        private GlassButton btnIntelligenceAgentsRecruit;

        private ConstructionYardPurchaser pnlBuiltObjectConstructionYardPurchaser;

        private ConstructionYardPurchaser pnlColonyConstructionYardPurchaser;

        private GlassButton btnColonyTroopsDisband;

        private GlassButton btnColonyTroopsRecruit;

        private ComboBox cmbShipGroupInfoHomeColony;

        private GlassButton btnShipGroupInfoSetHomeColony;

        private Label lblDesignsMaximumSize;

        private Label lblResearchMaximumSize;

        private Label lblColonyMaximumSize;

        private Label lblBuiltObjectMaximumSize;

        private GlassButton btnDesignsUpgrade;

        private Label lblIntelligenceAgentMax;

        private TabPage tabVictoryConditions;

        private GameVictoryConditions pnlGameVictoryConditions;

        private BorderPanel pnlGameEnd;

        private GlassButton btnGameEndExit;

        private GlassButton btnGameEndContinue;

        private PictureBox picGameEndPicture;

        private EnabledLabel lblSaveLoadMessage;

        private Label lblDesignDetailMaintenanceCost;

        private Label lblDesignDetailPurchaseCost;

        private BorderPanel pnlTutorial;

        private EnabledLabel lblTutorialText;

        private EnabledLabel lblTutorialTitle;

        private GlassButton btnTutorialContinue;

        private GlassButton btnTutorialExit;

        private BorderPanel pnlIntroduction;

        private Label lblIntroductionTitle;

        private GlassButton btnIntroductionStart;

        private GradientPanel pnlIntroductionBackground;

        private Label lblIntroductionConclusion;

        private Label lblIntroductionVictoryConditions;

        private Label lblIntroductionEmpireDetails;

        private Label lblIntroductionEmpireName;

        private PictureBox picIntroductionFlag;

        private Label lblColonyGalaxyMapTitle;

        private Label lblBuiltObjectGalaxyMapTitle;

        private Label lblDiplomacyGalaxyMapTitle;

        private Label lblShipGroupGalaxyMapTitle;

        private Label lblTroopsGalaxyMapTitle;

        private ScreenPanel pnlEncyclopedia;

        private GlassButton btnEncyclopediaBack;

        private GlassButton btnEncyclopediaForward;

        private GlassButton btnHelp;

        private RelatedEncyclopediaItemsBox pnlEncyclopediaRelatedItems;

        private WebBrowser webEncyclopediaContent;

        private GlassButton btnEncyclopediaHome;

        private GlassButton btnGameOptionsResetAutomationMessages;

        private GlassButton btnBuiltObjectConstructionYardMoveToBottom;

        private GlassButton awqrtdiblI;

        private GlassButton btnBuiltObjectConstructionYardMoveUp;

        private GlassButton btnBuiltObjectConstructionYardMoveToTop;

        private GlassButton btnColonyConstructionYardMoveToBottom;

        private GlassButton btnColonyConstructionYardMoveDown;

        private GlassButton btnColonyConstructionYardMoveUp;

        private GlassButton btnColonyConstructionYardMoveToTop;

        private GlassButton btnColonyMakeCapital;

        private HabitatAttitudeSummary pnlColonyPopulationAttitudeSummary;

        private BorderPanel pnlGalaxyMapKey;

        private GlassButton btnGalaxyMapKey;

        private Label lblGalaxyMapKeyTitle;

        private MapKey pnlGalaxyMapKeyActual;

        private GlassButton btnGalaxyMapKeyClose;

        internal BorderPanel pnlGameEditor;

        private PictureBox picGameEditor;

        private Label lblGameEditorTitle;

        private GlassButton btnGameEditor;

        private GlassButton btnGameEditorEditGalaxy;

        private GlassButton btnGameEditorEditEmpires;

        private GlassButton btnGameEditorSaveAs;

        private GlassButton btnGameEditorSave;

        private GlassButton btnGameEditorExit;

        private CollapsingHabitatTypeSelector gameEditorSelector;

        private BorderPanel pnlEditCreature;

        private PictureBox picEditCreature;

        private Label lblEditCreatureTitle;

        private Label lblEditCreatureName;

        private TextBox txtEditCreatureName;

        private Label lblEditCreatureDamage;

        private NumericUpDown numEditCreatureDamage;

        private Label lblEditCreatureSize;

        private NumericUpDown numEditCreatureSize;

        private Label lblEditCreatureAttackStrength;

        private NumericUpDown numEditCreatureAttackStrength;

        private CheckBox chkEditCreatureAnchoredToParent;

        private CheckBox chkEditCreatureVisible;

        private BorderPanel pnlEditBuiltObject;

        private CheckBox chkEditBuiltObjectAutomated;

        private Label lblEditBuiltObjectFuel;

        private NumericUpDown numEditBuiltObjectFuel;

        private Label lblEditBuiltObjectName;

        private TextBox txtEditBuiltObjectName;

        private PictureBox picEditBuiltObject;

        private Label lblEditBuiltObjectTitle;

        private EmpireDropDown cmbEditBuiltObjectEmpire;

        private Label lblEditBuiltObjectEmpire;

        private Label lblEditBuiltObjectTroops;

        private Label lblEditBuiltObjectFleet;

        private TroopListView ctlEditBuiltObjectTroops;

        private GlassButton btnEditBuiltObjectRemoveTroop;

        private GlassButton btnEditBuiltObjectAddTroop;

        private FleetDropDown cmbEditBuiltObjectFleet;

        private BorderPanel pnlEditHabitat;

        private GradientPanel pnlEditRuinsContainer;

        private RuinPanel pnlEditRuins;

        private CheckBox chkEditHabitatCapital;

        private Label lblEditHabitatSize;

        private NumericUpDown numEditHabitatSize;

        private Label lblEditHabitatName;

        private TextBox txtEditHabitatName;

        private PictureBox picEditHabitat;

        private Label lblEditHabitatTitle;

        private HabitatResourceListView ctlEditHabitatResources;

        private Label lblEditHabitatResources;

        private Label lblEditHabitatPopulation;

        private PopulationListView ctlEditHabitatPopulation;

        private Label lblEditHabitatEmpire;

        private EmpireDropDown cmbEditHabitatEmpire;

        private Label lblEditHabitatTroops;

        private TroopListView ctlEditHabitatTroops;

        private Label lblEditHabitatDevelopmentLevel;

        private NumericUpDown numEditHabitatDevelopmentLevel;

        private Label lblEditHabitatMicrowaveRadiation;

        private NumericUpDown numEditHabitatMicrowaveRadiation;

        private Label lblEditHabitatXrayRadiation;

        private NumericUpDown numEditHabitatXrayRadiation;

        private Label lblEditHabitatSolarRadiation;

        private NumericUpDown numEditHabitatSolarRadiation;

        private GlassButton btnEditHabitatRemoveTroop;

        private GlassButton btnEditHabitatAddTroop;

        private GlassButton btnEditHabitatAddResource;

        private GlassButton btnEditHabitatRemoveResource;

        private BorderPanel pnlEditEmpireList;

        private PictureBox picEditEmpireList;

        private Label lblEditEmpireListTitle;

        private GlassButton btnEditEmpireListClose;

        private GlassButton btnEditEmpireListRemove;

        private GlassButton btnEditEmpireListEdit;

        private GlassButton btnEditEmpireListAdd;

        private EmpireListViewBasic ctlEditEmpireList;

        private BorderPanel pnlEditEmpire;

        private GlassButton btnEditEmpireClose;

        private PictureBox picEditEmpire;

        private Label lblEditEmpireTitle;

        private EnhancedTabControl tabEditEmpire;

        private TabPage tabEditEmpireMain;

        private TabPage tabEditEmpireResearch;

        private TabPage tabEditEmpireColonies;

        private TabPage tabEditEmpireBuiltObjects;

        private Label lblEditEmpireRace;

        private RaceDropDown cmbEditEmpireRace;

        private Label lblEditEmpireName;

        private TextBox txtEditEmpireName;

        private Label lblEditEmpireWarWeariness;

        private Label lblEditEmpireReputation;

        private ColorSlider sldEditEmpireReputation;

        private ColorSlider sldEditEmpireWarWeariness;

        private ColorDropDown cmbEditEmpireSecondaryColor;

        private ColorDropDown cmbEditEmpirePrimaryColor;

        private Label lblEditEmpireFlagShape;

        private Label lblEditEmpirePrimaryColor;

        private Label lblEditEmpireSecondaryColor;

        private FlagShapeDropDown cmbEditEmpireFlagShape;

        private Label lblEditEmpireGovernmentStyle;

        private GovernmentStyleDropDown cmbEditEmpireGovernmentStyle;

        private Label lblEditEmpireReputationSummary;

        private Label lblEditEmpireWarWearinessSummary;

        private EmpireRelationList ctlEditEmpireRelationList;

        private GlassButton btnEditEmpireColonyGoto;

        private InfoPanel pnlEditEmpireColonyInfo;

        private HabitatListView ctlEditEmpireColonies;

        private GlassButton btnEditEmpireBuiltObjectAutoGen;

        private GlassButton btnEditEmpireBuiltObjectGoto;

        private InfoPanel pnlEditEmpireBuiltObjectInfo;

        private BuiltObjectListView ctlEditEmpireBuiltObjectList;

        private Label lblEditEmpireRelationshipsTitle;

        private EncyclopediaTopicTree pnlEncyclopediaTopics;

        private Label lblEditEmpireMoney;

        private NumericUpDown numEditEmpireMoney;

        private BorderPanel pnlGameEditorPassword;

        private GlassButton btnGameEditorPasswordCancel;

        private GlassButton btnGameEditorPasswordOk;

        private Label lblGameEditorPasswordDescription;

        private Label lblGameEditorPassword;

        private TextBox txtGameEditorPassword;

        private Label lblGameEditorPasswordTitle;

        private PictureBox picGameEditorPassword;

        private BorderPanel pnlGameEditorEnterPassword;

        private PictureBox picGameEditorEnterPassword;

        private GlassButton btnGameEditorEnterPasswordCancel;

        private GlassButton btnGameEditorEnterPasswordOk;

        private Label lblGameEditorEnterPasswordDescription;

        private Label lblGameEditorEnterPassword;

        private TextBox txtGameEditorEnterPassword;

        private Label lblGameEditorEnterPasswordTitle;

        private Label lblGameEditorPasswordButtonDescription;

        private GlassButton btnSelectionForward;

        private GlassButton btnSelectionBack;

        private GlassButton btnGalaxyMapBack;

        private GlassButton btnGalaxyMapForward;

        private Label lblOptionsControlAttacks;

        private ComboBox fYpVlWkAfp;

        private Label lblOptionsControlDiplomacyTreaties;

        private ComboBox cmbOptionsControlDiplomacyTreaties;

        private Label lblOptionsControlDiplomacyGifts;

        private ComboBox cmbOptionsControlDiplomacyGifts;

        private Label lblOptionsControlColonization;

        private ComboBox cmbOptionsControlColonization;

        private Label lblOptionsControlConstruction;

        private ComboBox cmbOptionsControlConstruction;

        private Label lblOptionsControlAgentMissions;

        private ComboBox cmbOptionsControlAgentMissions;

        private Label lblOptionsControlDiplomacyOffense;

        private ComboBox cmbOptionsControlDiplomacyOffense;

        internal BorderPanel pnlSystemMap;

        private BorderPanel pnlInfoPanel;

        private BorderPanel pnlEventMessage;

        private GradientPanel pnlEventMessagePanel;

        private Label lblEventMessageTitle;

        private PictureBox picEventMessage;

        private GlassButton btnEventMessageClose;

        private GlassButton btnEventMessageGoto;

        private SpecialRuin pnlEditSpecialRuins;

        private GlassButton btnBuiltObjectSelect;

        private GlassButton btnShipGroupSelect;

        private GlassButton btnColonySelect;

        private GlassButton btnEventMessageAvoid;

        private GlassButton btnEventMessageInvestigate;

        private Label lblEditBuiltObjectEncounterEvent;

        private ComboBox cmbEditBuiltObjectEncounterEvent;

        private ScreenPanel pnlMessageHistory;

        private TextBox txtMessageHistoryText;

        private GalaxyMap gmapMessageHistory;

        private EmpireMessageListView ctlMessageHistoryMessages;

        private GlassButton btnMessageHistoryGoto;

        private Label lblMessageHistoryHeading;

        private GlassButton btnHistoryMessages;

        private Label lblGameEndComment;

        private System.Windows.Forms.Panel pnlEventMessageContainer;

        private Label lblEventMessageText;

        private LinkLabel lnkEventMessageLink;

        private GroupBox grpOptionsDisplaySettings;

        private CheckBox chkOptionsShowSystemNebulae;

        private Label lblOptionsMainViewZoomSpeed;

        private ColorSlider sldOptionsMainViewZoomSpeed;

        private Label lblOptionsMainViewStarFieldSize;

        private ColorSlider sldOptionsMainViewStarFieldSize;

        private Label lblOptionsMainViewGuiScale;

        private ColorSlider sldOptionsMainViewGuiScale;

        private Label lblOptionsMainViewScrollSpeed;

        private ColorSlider sldOptionsMainViewScrollSpeed;

        private Label lblResearchEmpireRaceBonusModifier;

        private Label lblResearchEmpireRaceBonus;

        private ComboBox cmbGalaxyMapHabitatType;

        private Label lblGameEndCommentDropShadow;

        private ComboBox cmbOptionsAutomationMode;

        private System.Windows.Forms.Panel pnlOptionsAutomationMode;

        private Label lblOptionsAutomationMode;

        private PictureBox picIntroductionRace;

        private GradientPanel pnlDiplomacyTalkPanel;

        private DiplomacyEmpireSummary pnlDiplomacyEmpireSummary;

        private Label lblDiplomacyTalkTitle;

        private LinkLabel lnkEmpireSummaryGovernmentType;

        private LinkLabel NuSppwjfQh;

        private LinkLabel lnkColonyGrowth;

        private LinkLabel lnkColonyApproval;

        private LinkLabel lnkBuiltObjectConstruction;

        private LinkLabel lnkTroops;

        private LinkLabel lnkResearch;

        private GlassButton btnZoomSelection;

        private GlassButton btnBuiltObjectConstructionScrap;

        private GlassButton btnBuiltObjectConstructionRemoveFromQueue;

        private GlassButton btnColonyConstructionRemoveFromQueue;

        private GlassButton btnColonyConstructionScrap;

        private LinkLabel lnkColonyConstruction;

        private LinkLabel lnkFleets;

        private GlassButton btnSelectNearestMilitary;

        private GlassButton btnDesignsLoad;

        private GlassButton btnDesignsSave;

        private ScreenPanel pnlExpansionPlanner;

        private GalaxyMap gmapExpansionPlanner;

        private ResourceListView ctlExpansionPlannerResources;

        private Label lblExpansionPlannerGalaxyMap;

        private Label lblExpansionPlannerCurrentlyShowing;

        private Label lblExpansionPlannerResources;

        private ComboBox cmbExpansionPlannerMode;

        private GradientPanel pnlExpansionPlannerTargetGroup;

        private Label lblExpansionPlannerAvailableBuiltObjects;

        private GlassButton btnExpansionPlannerAction;

        private BuiltObjectDropDown cmbExpansionPlannerAvailableBuiltObjects;

        private Label lblExpansionPlannerResourceFilter;

        private ResourceDropDown cmbExpansionPlannerResourceFilter;

        private NumericUpDownNoArrows _numResourcePercentFilter;
        private CheckBox _chkUseResourcePercentFilter;

        private HabitatPrioritizationListView ctlExpansionPlannerTargets;

        private GlassButton btnExpansionPlannerSortResources;

        private GlassButton btnColonyShowExpansionPlanner;

        private GlassButton btnBuiltObjectShowMiningPlanner;

        private GlassButton btnEmpireSummaryShowExpansionPlanner;

        private GlassButton btnExpansionPlanner;

        private GlassButton btnExpansionPlannerSelectTarget;

        private GlassButton btnExpansionPlannerGotoTarget;

        private LinkLabel lnkDiplomacyReputation;

        private ScreenPanel pnlComponentGuide;

        private ComponentDetail pnlComponentGuideDetail;

        private ComponentResourceListView ctlComponentGuideResources;

        private LinkLabel lnkComponentGuideType;

        private Label lblComponentGuideResources;

        private GradientPanel pnlComponentGuideGroup;

        private ScreenPanel pnlConstructionSummary;

        private Label lblConstructionSummaryResources;

        private ComponentResourceListView ctlConstructionSummaryConstructionResources;

        private Label lblConstructionSummaryOverview;

        private Label lblConstructionSummaryComponents;

        private GlassButton btnDesignsShowConstructionSummary;

        private GlassButton btnDesignsShowComponentGuide;

        private GlassButton btnBuiltObjectConstructionShowSummary;

        private GlassButton btnColonyConstructionShowSummary;

        private ComponentListView ctlConstructionSummaryComponents;

        private GlassButton btnCycleBasesBack;

        private GlassButton btnCycleMilitaryBack;

        private GlassButton btnCycleColoniesBack;

        private GlassButton btnCycleConstructionBack;

        private GlassButton btnCycleOtherBack;

        private GlassButton btnCycleShipGroupsBack;

        private GlassButton btnCycleIdleShipsBack;

        private GlassButton btnExpansionPlannerBuildColonyShip;

        private Label lblResearchEmpireSpecialBonuses;

        private ScreenPanel pnlRuinDetail;

        private PictureBox picRuinDetailImage;

        private Label lblRuinDetailAbilities;

        private Label lblRuinDetailDescription;

        private Label lblRuinDetailName;

        private GlassButton btnColonyShowRuin;

        private ScreenPanel pnlResourceComponents;

        private ComponentListView ctlResourceComponents;

        private Label lblResourceComponentsUsedBy;

        private LinkLabel lnkResourceComponentsAboutResource;

        private Label lblOptionsMouseScrollMode;

        private ComboBox cmbOptionsMouseScrollWheelBehaviour;

        private GlassButton btnGameOptionsAdvancedDisplaySettings;
        private GlassButton btnHotKeys;

        private ScreenPanel pnlGameOptionsAdvancedDisplaySettings;

        private GroupBox grpGameOptionsAdvancedDisplaySettingsMaximumFramerate;

        private NumericUpDown numGameOptionsAdvancedDisplaySettingsMaximumFramerate;

        private CheckBox chkGameOptionsAdvancedDisplaySettingsMaximumFramerateUnlimited;

        private Label lblGameOptionsAdvancedDisplaySettingsMaximumFramerateFPS;

        private LabelledTrackBar tbarGameOptionsAdvancedDisplaySettingsSystemNebulaeDetail;

        private GlassButton btnGameSpeedIncrease;

        private GlassButton btnGameSpeedDecrease;

        private LinkLabel lnkDiplomacyPirates;

        private System.Windows.Forms.Panel pnlStoryEvent;

        private LabelDropshadow lblStoryEventText;

        private GlassButton btnStoryEventClose;

        private LabelDropshadow lblStoryEventTitle;

        private PictureBox picExpansionPlannerImage;

        private Label lblDesignWeaponBombardPowerValue;

        private Label lblDesignWeaponBombardPower;

        private ComboBox cmbMessageHistoryFilter;

        private GlassButton btnGalacticHistory;

        private ComboBox cmbDesignsFilter;

        private ScreenPanel pnlRetrofit;

        private Label lblRetrofitCost;

        private GlassButton btnRetrofitGo;

        private Label lblRetrofitDesignLabel;

        private DesignDropDown cmbRetrofitDesign;

        private Label lblRetrofitMessage;

        private GlassButton btnBuiltObjectRepairSelected;

        private GlassButton btnShipGroupRepairAndRefuel;

        private GlassButton btnShipGroupRetrofit;

        private GroupBox grpOptionsAutoSave;

        private NumericUpDown numOptionsAutoSaveMinutes;

        private CheckBox chkOptionsAutoSave;

        private GroupBox grpGameOptionsAdvancedDisplaySettingsGalaxyIcons;

        private CheckBox chkGameOptionsGalaxyDisplaySpacePorts;

        private CheckBox chkGameOptionsGalaxyDisplayExplorationShips;

        private CheckBox chkGameOptionsGalaxyDisplayResupplyShips;

        private CheckBox chkGameOptionsGalaxyDisplayOtherBases;

        private CheckBox chkGameOptionsGalaxyDisplayCivilianShips;

        private CheckBox chkGameOptionsGalaxyDisplayFleets;

        private CheckBox chkGameOptionsGalaxyDisplayMilitaryShips;

        private CheckBox chkGameOptionsGalaxyDisplayColonyShips;

        private CheckBox chkGameOptionsGalaxyDisplayAlwaysEnemyMilitaryShips;

        private CheckBox chkGameOptionsGalaxyDisplayAlwaysEnemyFleets;

        private CheckBox chkGameOptionsGalaxyDisplayConstructionShips;

        private CheckBox chkGameOptionsGalaxyDisplayAlwaysPirates;

        private Label lblDesignWeaponTotalEnergyValue;

        private Label FqNlrHsjje;

        private GlassButton btnBuiltObjectRetrofitSelected;

        private ScreenPanel pnlGameOptionsEmpireSettings;

        private LabelledTrackBar sldGameOptionsAttackOvermatch;

        private GroupBox grpGameOptionsDefaultEngagementStances;

        private Label lblGameOptionsEngagementStancePatrol;

        private ComboBox cmbGameOptionsEngagementStanceOther;

        private ComboBox cmbGameOptionsEngagementStancePatrol;

        private Label lblGameOptionsEngagementStanceOther;

        private Label lblGameOptionsEngagementStanceEscort;

        private ComboBox cmbGameOptionsEngagementStanceEscort;

        private GlassButton btnGameOptionsEmpireSettings;

        private CheckBox chkOptionsAllowSameSystemAsOtherEmpires;

        private GlassButton btnBuiltObjectRetireSelected;

        private GlassButton btnBuiltObjectRefuelSelected;

        private GlassButton XxYlcNpSu4;

        private GlassButton btnCycleShipStance;

        private GlassButton btnBuiltObjectScrapSelected;

        private ComboBox cmbGameOptionsEngagementStanceAttack;

        private Label lblGameOptionsEngagementStanceAttack;

        private GroupBox grpGameOptionsFleetAttackSettings;

        private NumericUpDown numGameOptionsFleetAttackGather;

        private NumericUpDown numGameOptionsFleetAttackRefuel;

        private Label lblGameOptionsFleetAttackGather;

        private Label lblGameOptionsFleetAttackRefuel;

        private GlassButton btnSelectionAction1;

        private GlassButton btnSelectionAction6;

        private GlassButton btnSelectionAction2;

        private GlassButton btnSelectionAction7;

        private GlassButton btnSelectionAction3;

        private GlassButton btnSelectionAction8;

        private GlassButton btnSelectionAction4;

        private GlassButton btnSelectionAction5;

        private ResearchTree pnlResearchTree;

        private System.Windows.Forms.Panel WdosRcovZt;

        private GlassButton btnResearchFacilities;

        private GlassButton btnResearchTreeHighTech;

        private GlassButton btnResearchTreeEnergy;

        private GlassButton btnResearchTreeWeapons;

        private GradientPanel TwUstMvCeX;

        private GlassButton btnEditEmpireApplyTechLevel;

        private LabelledTrackBar tbarEmpireTechLevel;

        private CheckBox chkOptionsControlResearch;

        private Label lblDesignWeaponFleetTargettingValue;

        private Label lblDesignWeaponFleetTargetting;

        private Label lblDesignWeaponHyperStopValue;

        private Label lblDesignWeaponHyperStop;

        private Label lblDesignWeaponPointDefenseValue;

        private Label lblDesignWeaponPointDefense;

        private Label lblDesignWeaponFightersValue;

        private Label lblDesignWeaponFighters;

        private TabPage tabColony_Facilities;

        private PlanetaryFacilityListIconView ctlColonyFacilities;

        private GlassButton btnColonyFacilityBuild;

        private GlassButton btnColonyFacilityScrap;

        private PlanetaryFacilityDefinitionDropDown cmbColonyFacilitiesToBuild;

        private GlassButton btnGameMenuEditor;

        private Label lblOptionsControlColonyFacilities;

        private ComboBox cmbOptionsControlColonyFacilities;

        private EmpireSummaryBonusesTitle pnlEmpireSummaryBonus;

        private HoverDetail pnlHoverDetail;

        private Label lblResearchInstructions;

        private GroupBox grpGameOptionsDiscoveries;

        private ComboBox cmbGameOptionsEncounterAbandonedShipOrBase;

        private ComboBox cmbGameOptionsEncounterRuins;

        private Label lblGameOptionsEncounterAbandonedShipOrBase;

        private Label lblGameOptionsEncounterRuins;

        private CheckBox chkOptionsNewShipsAutomated;

        private CheckBox chkOptionsLoadedGamesPaused;

        private GlassButton btnStoryEventAction;

        private ScreenPanel pnlEmpirePolicy;

        private GlassButton XgxsOtuAmD;

        private GlassButton WqesexberY;

        private System.Windows.Forms.Panel pnlEmpirePolicyContainer;

        private System.Windows.Forms.Panel nDrsqatloR;

        private GlassButton btnEmpireSummaryShowEmpirePolicy;

        private ScreenPanel pnlBuildOrder;

        private GlassButton btnBuildOrderPurchase;

        private GlassButton btnBuildOrderCancel;

        private Label lblBuildOrderAdvisorRecommendation;

        private Label FfJsLkoYvX;

        private Label lblBuildOrderCurrentAmount;

        private Label lblBuildOrderCost;

        private Label lblBuildOrderDesign;

        private System.Windows.Forms.Panel pnlBuildOrderCostColumn;

        private System.Windows.Forms.Panel pnlBuildOrderPurchaseAmountColumn;

        private Label lblBuildOrderPurchaseAmount;

        private System.Windows.Forms.Panel pnlBuildOrderCurrentAmountColumn;

        private System.Windows.Forms.Panel pnlBuildOrderContainer;

        private Label lblBuildOrderTotalMaintenance;

        private Label lblBuildOrderMaintenance;

        private Label iqosaeoiKu;

        private Label lblBuildOrderAvailableFunds;

        private Label lblBuildOrderAvailableFundsLabel;

        private Label lblBuildOrderExplanation;

        private Label lblBuildOrderAvailableCashflowLabel;

        private GlassButton btnEmpirePolicy;

        private GlassButton btnBuildOrder;

        private Label lblEmpireSummaryTESTINGSilvermistTriggerLocation;

        private Label lblEditHabitatScenery;

        private NumericUpDown numEditHabitatScenery;

        private Label lblEditHabitatResearchBonus;

        private NumericUpDown numEditHabitatResearchBonusAmount;

        private Label lblEditHabitatQuality;

        private NumericUpDown numEditHabitatQuality;

        private CheckBox chkEditHabitatHasRings;

        private ComboBox ExUfgIlqCu;

        private ResourceDropDown cmbEditHabitatAddResource;

        private TextBox txtEditHabitatSceneryDescription;

        private Label lblColonyPopulationPolicyAllOthers;

        private ColonyPopulationPolicyDropDown cmbColonyPopulationPolicyAllOthers;

        private Label lblColonyPopulationPolicyRaceFamily;

        private ColonyPopulationPolicyDropDown cmbColonyPopulationPolicyRaceFamily;

        private GlassButton btnEmpirePolicySave;

        private GlassButton btnEmpirePolicyLoad;

        private CharacterSummary ctlCharacterSummary;

        private LinkLabel lnkCharactersLearnAbout;

        private Label lblCharacterSummary;

        private GlassButton btnMapOverlay1;

        private GlassButton btnMapOverlay2;

        private GlassButton btnMapOverlay3;

        private GlassButton btnMapOverlay4;

        private GlassButton btnMapOverlay8;

        private GlassButton btnMapOverlay7;

        private GlassButton btnMapOverlay6;

        private GlassButton btnMapOverlay5;

        private ScreenPanel pnlAdvisorSuggestion;

        private GlassButton btnAdvisorSuggestionShow;

        private GlassButton btnAdvisorSuggestionApprove;

        private GlassButton btnAdvisorSuggestionDecline;

        private Label lblAdvisorSuggestionDescription;

        private PictureBox picAdvisorSuggestionImage;

        private System.Windows.Forms.Panel pnlAdvisorSuggestionDescriptionContainer;

        private Label lblAdvisorSuggestionTitle;

        private CheckBox chkOptionsSuppressAllPopups;

        private GlassButton btnColonyPopulationApplyPolicyToAll;

        private ToolTip toolTip_0;

        private RaceVictoryConditionsPanel pnlGameRaceVictoryConditions;

        private GradientPanel pnlColonyPopulationAttitudeSummaryBackground;

        private System.Windows.Forms.Panel pnlColonyPopulationAttitudeSummaryContainer;

        private System.Windows.Forms.Panel pnlGameRaceVictoryConditionsContainer;

        private ScreenPanel pnlCharacterEventHistory;

        private TextBox txtCharacterEventHistoryDescription;

        private CharacterEventListView ctlCharacterEvents;

        private Label lblCharacterEventHistoryTitle;

        private GlassButton btnCharacterShowEventHistory;

        private Label lblColonyTaxPercent;

        private CheckBox LrufvZylIl;

        private CheckBox chkExpansionPlannerToggleAsteroids;

        private CheckBox chkOptionsControlPopulationPolicy;

        private CheckBox chkOptionsControlCharacterLocations;

        private Label lblDesignDetailUpgradeRolesExplanation;

        private Label lblDesignDetailAutoRetrofit;

        private ComboBox cmbDesignDetailAutoRetrofit;

        private Label lblBuiltObjectAutoRetrofit;

        private ComboBox cmbBuiltObjectAutoRetrofit;

        private ComboBox cmbDesignsFilterTypes;

        private GlassButton btnDesignsUpgradeManual;

        private GlassButton btnDesignsShowEmpirePolicy;

        private Label lblDesignsUpgradeRolesExplanation;

        private GlassButton btnRemoveComponentFromDesignMultiple;

        private GlassButton btnAddComponentToDesignMultiple;

        private Label lblDesignWeaponBoardingAssaultValue;

        private Label lblDesignWeaponBoardingAssault;

        private GlassButton btnColonyTroopsUngarrison;

        private GlassButton btnColonyTroopGarrison;

        private ComboBox cmbColonyTroopsRecruitType;

        private GlassButton btnColonyTroopTransferTransport;

        private ComboBox cmbColonyTroopTransferTransport;

        private CheckBox chkUseTroopLoadouts;

        private GroupBox grpUseTroopLoadouts;

        private Label lblTroopLoadoutSpecialForces;

        private NumericUpDown numTroopLoadoutSpecialForces;

        private Label lblTroopLoadoutArtillery;

        private NumericUpDown numTroopLoadoutArtillery;

        private Label lblTroopLoadoutArmored;

        private NumericUpDown numTroopLoadoutArmored;

        private Label lblTroopLoadoutInfantry;

        private NumericUpDown numTroopLoadoutInfantry;

        private Label lblTroopLoadoutTotal;

        private ColonyInvasionPanel pnlColonyInvasionContainer;

        private BorderPanel pnlPirateSmugglingMissionResourceSelection;

        private SmoothLabel lblPirateSmugglingMissionResourcePriceDescription;

        private ResourceDropDown cmbPirateSmugglingMissionResourceSelection;

        private GlassButton btnPirateSmugglingMissionCancel;

        private GlassButton btnPirateSmugglingMissionAssign;

        private Label lblColonyCargoStrategicResourcesLow;

        private CheckBox chkShipGroupUseTroopLoadouts;

        private GroupBox grpShipGroupUseTroopLoadouts;

        private Label lblShipGroupTroopLoadoutDescription;

        private Label lblShipGroupTroopLoadoutSpecialForces;

        private NumericUpDown numShipGroupTroopLoadoutSpecialForces;

        private Label lblShipGroupTroopLoadoutArtillery;

        private NumericUpDown numShipGroupTroopLoadoutArtillery;

        private Label lblShipGroupTroopLoadoutArmored;

        private NumericUpDown numShipGroupTroopLoadoutArmored;

        private Label lblShipGroupTroopLoadoutInfantry;

        private NumericUpDown numShipGroupTroopLoadoutInfantry;

        private Label lblBuiltObjectTroopLoadoutPartOfFleet;

        private Label lblIntroductionWhatToDoPointsTitle;

        private Label lblIntroductionWhatToDoPoints;

        private System.Windows.Forms.Panel picIntroductionPlaystyle;

        private Label lblIntroductionPlaystyleIntro;

        private Label lblBuiltObjectCargoConstructionResourceShortage;

        private Label lblColonyCargoConstructionResourceShortage;

        private Label lblTroopFilter;

        private FleetHabitatDropDown cmbTroopFilter;

        private GlassButton btnTroopUngarrison;

        private GlassButton btnTroopGarrison;

        private Label lblShipGroupUngarrisonedTroopReport;

        private Label lblOptionsControlOfferPirateMissions;

        private ComboBox cmbOptionsControlOfferPirateMissions;

        private GlassButton btnSelectionPanelSize;

        private GlassButton btnMapCivilianFade;

        private ScreenPanel pnlColonyInvasion;

        private TabPage tabAchievements;

        private GameSummaryPanel pnlGameSummary;

        private GradientPanel pnlDesignWarningsBackground;

        private System.Windows.Forms.Panel pnlDesignWarningsContainer;

        private GroupBox grpGameOptionsDefaultEngagementStancesManual;

        private ComboBox cmbGameOptionsEngagementStanceAttackManual;

        private Label lblGameOptionsEngagementStanceAttackManual;

        private ComboBox cmbGameOptionsEngagementStanceOtherManual;

        private ComboBox cmbGameOptionsEngagementStancePatrolManual;

        private Label lblGameOptionsEngagementStanceOtherManual;

        private Label lblGameOptionsEngagementStanceEscortManual;

        private ComboBox cmbGameOptionsEngagementStanceEscortManual;

        private Label lblGameOptionsEngagementStancePatrolManual;

        private Label lblEditHabitatPlanetaryFacilities;

        private Label lblEditHabitatPirateColonyControl;

        private GlassButton btnEditHabitatAddPlanetaryFacility;

        private GlassButton btnEditHabitatRemovePlanetaryFacility;

        private PlanetaryFacilityDefinitionDropDown cmbEditHabitatAddPlanetaryFacility;

        private GlassButton btnEditHabitatAddPirateColonyControl;

        private GlassButton btnEditHabitatRemovePirateColonyControl;

        private EmpireDropDown cmbEditHabitatAddPirateColonyControl;

        private PlanetaryFacilityListIconView ctlEditHabitatPlanetaryFacilities;

        private PirateColonyControlListView ctlEditHabitatPirateColonyControl;

        private TroopDropDown cmbEditHabitatAddTroop;

        private TroopDropDown cmbEditBuiltObjectAddTroop;

        private PictureBox picEditHabitatPictureLandscape;

        private HScrollBar scrEditHabitatPictureLandscape;

        private PictureBox picEditHabitatPicture;

        private HScrollBar scrEditHabitatPicture;

        private PiratePlaystyleDropDown cmbEditEmpirePirateStyle;

        private TextBox txtEditEmpireDescription;

        private Label lblEditEmpireDescription;

        private BorderPanel pnlEditGalaxy;

        private GlassButton btnEditGalaxyClose;

        private Label lblEditGalaxyExplanation;

        private Label lblEditGalaxyHeading;

        private TextBox txtEditGalaxyTitle;

        private Label lblEditGalaxyTitle;

        private TextBox txtEditGalaxyDescription;

        private Label lblEditGalaxyDescription;

        private BorderPanel ynbOfkDbGY;

        private BorderPanel pnlEditEventAction;

        private EventActionPanel ctlEventAction;

        private GameEventPanel ctlGameEvent;

        private GlassButton btnEditBuiltObjectGameEvent;

        private SmoothLabel lblEditBuiltObjectGameEvent;

        private GlassButton btnEditHabitatGameEvent;

        private SmoothLabel lblEditHabitatGameEvent;

        private GlassButton btnEditCreatureGameEvent;

        private SmoothLabel lblEditCreatureGameEvent;

        private GlassButton btnEditBuiltObjectClose;

        private GlassButton btnEditCreatureClose;

        private GlassButton btnEditHabitatClose;

        private GlassButton btnEditGalaxyShowEvents;

        private BorderPanel pnlEditGameEvents;

        private GlassButton btnEditGameEventsEdit;

        private SmoothLabel EfcOvcsSlw;

        private GameEventListView ctlGameEvents;

        private GlassButton btnEditGameEventsGoto;

        private GlassButton btnEditGameEventsDelete;

        private GlassButton btnEditGameEventsClose;

        private Label lblDesignImageScalingAmount;

        private Label lblDesignImageScalingMode;

        private DesignImageScalingModeDropDown cmbDesignImageScalingMode;

        private NumericUpDown numDesignImageScalingAmount;

        private GlassButton btnGameOptionsShowMessages;

        private GameOptionsMessagesScreenPanel pnlGameOptionsMessages;

        private GroupBox grpOptionsPopupMessages;

        private CheckBox gcbeGaamXG;

        private CheckBox chkOptionsPopupMessageUnderAttackMilitaryShips;

        private CheckBox chkOptionsPopupMessageUnderAttackExplorationShips;

        private CheckBox ltFewaOdau;

        private CheckBox chkOptionsPopupMessageUnderAttackColonyConstructionShips;

        private CheckBox chkOptionsPopupMessageUnderAttackColoniesSpaceports;

        private CheckBox chkOptionsPopupMessageUnderAttackCivilianShips;

        private CheckBox chkOptionsPopupMessageShipNeedsRefuelling;

        private CheckBox chkOptionsPopupMessageShipMissionComplete;

        private CheckBox chkOptionsPopupMessageExploration;

        private CheckBox KpfeuWqjpj;

        private CheckBox chkOptionsPopupMessageResearchBreakthrough;

        private CheckBox XwwejKaRdv;

        private CheckBox chkOptionsPopupMessageColonyGainLoss;

        private CheckBox chkOptionsPopupMessageDiplomacyWarTradeSanctions;

        private CheckBox chkOptionsPopupMessageDiplomacyTreaties;

        private CheckBox chkOptionsPopupMessageRequestWarning;

        private CheckBox chkOptionsPopupMessageShipBuilt;

        private GroupBox grpOptionsScrollingMessages;

        private CheckBox chkOptionsScrollingMessageUnderAttackCivilianBases;

        private CheckBox chkOptionsScrollingMessageUnderAttackMilitaryShips;

        private CheckBox chkOptionsScrollingMessageUnderAttackExplorationShips;

        private CheckBox chkOptionsScrollingMessageUnderAttackOtherStateBases;

        private CheckBox chkOptionsScrollingMessageUnderAttackColonyConstructionShips;

        private CheckBox chkOptionsScrollingMessageUnderAttackColoniesSpaceports;

        private CheckBox chkOptionsScrollingMessageShipNeedsRefuelling;

        private CheckBox chkOptionsScrollingMessageShipMissionComplete;

        private CheckBox chkOptionsScrollingMessageExploration;

        private CheckBox chkOptionsScrollingMessageIntelligenceMissions;

        private CheckBox chkOptionsScrollingMessageResearchBreakthrough;

        private CheckBox chkOptionsScrollingMessageEmpireMetDestroyed;

        private CheckBox chkOptionsScrollingMessageColonyGainLoss;

        private CheckBox chkOptionsScrollingMessageUnderAttackCivilianShips;

        private CheckBox chkOptionsScrollingMessageWarTradeSanctions;

        private CheckBox chkOptionsScrollingMessageDiplomacyTreaties;

        private CheckBox chkOptionsScrollingMessageRequestWarning;

        private CheckBox chkOptionsScrollingMessageNewShipBuilt;

        private GlassButton btnEditGameEventsAddNew;

        private CheckBox chkEditEmpirePlayableInScenario;

        private GlassButton btnEditEmpireSelectTechs;

        private BorderPanel pnlRelationAllianceName;

        private TextBox txtRelationAllianceName;

        private GlassButton btnRelationAllianceNameApply;

        private SmoothLabel lblRelationAllianceName;

        private GlassButton btnEditHabitatAddRuins;

        private GlassButton btnEditHabitatRemoveRuins;

        private ScreenPanel pnlCharacterEditSkillsTraits;

        private GlassButton btnCharacterEditSkillsTraitsApply;

        private CharacterSkillTraitEditPanel pnlCharacterSkillTraitEditor;

        private TabPage tabEditEmpireCharacters;

        private GlassButton btnEditEmpireCharacters;

        private CheckBox chkOptionsPopupMessageConstructionResourceShortage;

        private CheckBox chkOptionsScrollingMessageConstructionResourceShortage;

        private CheckBox chkRelationAllianceNameLocked;

        public Game _Game;

        private int int_0;

        private Thread[] vqleEgFcoS;

        private List<object>[] list_0;

        private object[] object_0;

        private bool bool_0;

        private bool VkjezoZtWa;

        private bool bool_1;

        internal bool bool_2;

        internal static byte[] byte_0;

        internal static byte[] byte_1;

        private volatile bool bool_3;

        private volatile bool bool_4;

        internal ZoomStatus ZoomStatus;

        private volatile bool bool_5;

        private RefreshViewEventArgs refreshViewEventArgs_0;

        private string string_0;

        internal long long_0;

        internal bool bool_6;

        internal int int_1;

        internal int int_2;

        internal bool bool_7;

        internal string string_1;

        internal bool bool_8;

        internal bool bool_9;

        internal bool bool_10;

        private MouseHoverMode mouseHoverMode_0;

        private bool bool_11;

        private GalaxyTimeState galaxyTimeState_0;

        internal string string_2;

        internal string string_3;

        private bool[] ebnBxUfJs7;

        private Thread thread_0;

        public Thread thread_1;

        private System.Timers.Timer timer_0;

        private System.Windows.Forms.Panel panel_0;

        private Size size_0;

        private float float_0;

        private Bitmap bitmap_0;

        private Delegate0 delegate0_0;

        internal DialogSet dialogSet_0;

        internal SubRoleNameSet subRoleNameSet_0;

        internal List<string> list_1;

        internal MusicPlayer musicPlayer_0;

        internal MusicPlayer musicPlayer_1;

        private EffectsPlayer effectsPlayer_0;

        private SoundPlayer soundPlayer_0;

        private SoundEffectRequestList soundEffectRequestList_0;

        private object object_1;

        private int int_3;

        private System.Timers.Timer timer_1;

        private int int_4;

        public SetMessagePopupPositionDelegate SetMessagePopupPosition;

        public SetMainFocusDelegate SetMainFocus;

        private Delegate1 delegate1_0;

        private Delegate2 delegate2_0;

        private System.Timers.Timer timer_2;

        public GameEndMessageDelegate GameEndMessage;

        private DistantWorlds.Types.EmpireList empireList_0;

        private GameEndOutcome gameEndOutcome_0;

        private PrivateFontCollection privateFontCollection_0;

        private IntPtr intptr_0;

        private IntPtr intptr_1;

        private string[] string_4;

        private string[] string_5;

        private string[] string_6;

        private string[] string_7;

        private string[] string_8;

        private string[] string_9;

        private string[] string_10;

        private string[] string_11;

        private string[] string_12;

        private string[] string_13;

        private string[] string_14;

        private string[] string_15;

        public HabitatImageCache habitatImageCache_0;

        internal Bitmap[] bitmap_1;

        internal Bitmap[] bitmap_2;

        internal Bitmap[] bitmap_3;

        internal CharacterImageCache characterImageCache_0;

        internal Bitmap bitmap_4;

        internal Bitmap bitmap_5;

        public BuiltObjectImageCache builtObjectImageCache_0;

        public Bitmap[] bitmap_6;

        internal int[] int_5;

        internal Bitmap[] bitmap_7;

        internal List<Rectangle>[] list_2;

        internal Bitmap[] bitmap_8;

        internal Bitmap[] bitmap_9;

        internal Bitmap[][] bitmap_10;

        internal int[] sveqhmNacy;

        internal Bitmap[][] bitmap_11;

        internal Texture2D[][] texture2D_0;

        internal Bitmap[] bitmap_12;

        internal Bitmap[] bitmap_13;

        internal Bitmap[] edLqkLkgAx;

        internal Bitmap[] bitmap_14;

        internal Bitmap[] bitmap_15;

        internal Bitmap[] bitmap_16;

        internal Bitmap bitmap_17;

        internal Bitmap[] bitmap_18;

        internal Bitmap[][] bitmap_19;

        internal Bitmap[] bitmap_20;

        internal Texture2D[] texture2D_1;

        internal Bitmap[] bitmap_21;

        internal Bitmap[] bitmap_22;

        internal Bitmap[] _uiResourcesBitmaps;

        internal RaceImageCache raceImageCache_0;

        internal Bitmap[] bitmap_23;

        internal Bitmap[] bitmap_24;

        internal Bitmap[] bitmap_25;

        internal Bitmap[] bitmap_26;

        internal Bitmap[] bitmap_27;

        public Bitmap[] bitmap_28;

        internal Bitmap[] bitmap_29;

        internal Bitmap[] bitmap_30;

        internal Bitmap bitmap_31;

        internal Bitmap bitmap_32;

        internal Bitmap bitmap_33;

        internal Bitmap bitmap_34;

        internal Bitmap bitmap_35;

        internal Bitmap bitmap_36;

        internal Bitmap bitmap_37;

        internal Bitmap bitmap_38;

        //internal Bitmap bitmap_39;

        //internal Bitmap bitmap_40;

        //internal Bitmap bitmap_41;

        //internal Bitmap bitmap_42;

        internal Bitmap bitmap_43;

        internal Bitmap bitmap_44;

        internal Bitmap bitmap_45;

        internal Bitmap bitmap_46;

        internal Bitmap bitmap_47;

        internal Bitmap bitmap_48;

        internal Bitmap bitmap_49;

        internal Bitmap bitmap_50;

        internal Bitmap bitmap_51;

        internal Bitmap bitmap_52;

        internal Bitmap bitmap_53;

        internal Bitmap bitmap_54;

        internal Bitmap bitmap_55;

        internal Bitmap bitmap_56;

        internal Bitmap bitmap_57;

        internal Bitmap bitmap_58;

        internal Bitmap bitmap_59;

        internal Bitmap bitmap_60;

        internal Bitmap bitmap_61;

        internal Bitmap bitmap_62;

        internal Bitmap bitmap_63;

        internal Bitmap bitmap_64;

        internal Bitmap bitmap_65;

        internal Bitmap bitmap_66;

        internal Bitmap bitmap_67;

        internal Bitmap bitmap_68;

        internal Bitmap bitmap_69;

        internal Bitmap bitmap_70;

        internal Bitmap bitmap_71;

        internal Bitmap bitmap_72;

        internal Bitmap bitmap_73;

        internal Bitmap bitmap_74;

        internal Bitmap bitmap_75;

        internal Bitmap bitmap_76;

        internal Bitmap bitmap_77;

        internal Bitmap bitmap_78;

        internal Bitmap bitmap_79;

        internal Bitmap bitmap_80;

        internal Bitmap bitmap_81;

        internal Bitmap bitmap_82;

        internal Bitmap bitmap_83;

        internal Bitmap bitmap_84;

        internal Bitmap bitmap_85;

        internal Bitmap bitmap_86;

        internal Bitmap bitmap_87;

        internal Bitmap bitmap_88;

        internal Bitmap bitmap_89;

        internal Bitmap bitmap_90;

        internal Bitmap bitmap_91;

        internal Bitmap bitmap_92;

        internal Bitmap bitmap_93;

        internal Bitmap bitmap_94;

        internal Bitmap bitmap_95;

        internal Bitmap bitmap_96;

        internal Bitmap bitmap_97;

        internal Bitmap bitmap_98;

        internal Bitmap bitmap_99;

        internal Bitmap bitmap_100;

        internal Bitmap bitmap_101;

        internal Bitmap bitmap_102;

        internal Bitmap bitmap_103;

        internal Bitmap bitmap_104;

        internal Bitmap bitmap_105;

        internal Bitmap bitmap_106;

        internal Bitmap bitmap_107;

        internal Bitmap bitmap_108;

        internal Bitmap bitmap_109;

        internal Bitmap bitmap_110;

        internal Bitmap bitmap_111;

        internal Bitmap bitmap_112;

        internal Bitmap bitmap_113;

        internal Bitmap bitmap_114;

        internal Bitmap VnciZycUss;

        internal Bitmap bitmap_115;

        internal Bitmap bitmap_116;

        internal Bitmap bitmap_117;

        internal Bitmap bitmap_118;

        internal Bitmap bitmap_119;

        internal Bitmap bitmap_120;

        internal Bitmap bitmap_121;

        internal Bitmap bitmap_122;

        internal Bitmap bitmap_123;

        internal Bitmap bitmap_124;

        internal Bitmap bitmap_125;

        internal Bitmap bitmap_126;

        internal Bitmap bitmap_127;

        internal Bitmap bitmap_128;

        internal Bitmap bitmap_129;

        internal Bitmap bitmap_130;

        internal Bitmap bitmap_131;

        internal Bitmap bitmap_132;

        internal Bitmap bitmap_133;

        internal Bitmap bitmap_134;

        internal Bitmap bitmap_135;

        internal Bitmap bitmap_136;

        internal Bitmap bitmap_137;

        internal Bitmap bitmap_138;

        internal Bitmap bitmap_139;

        internal Bitmap bitmap_140;

        internal Bitmap bitmap_141;

        internal Bitmap bitmap_142;

        internal Bitmap bitmap_143;

        internal Bitmap bitmap_144;

        internal Bitmap bitmap_145;

        internal Bitmap bitmap_146;

        internal Bitmap bitmap_147;

        internal Bitmap bitmap_148;

        internal Bitmap bitmap_149;

        internal Bitmap bitmap_150;

        internal Bitmap bitmap_151;

        internal Bitmap bitmap_152;

        internal Bitmap bitmap_153;

        internal Bitmap bitmap_154;

        internal Bitmap bitmap_155;

        internal Bitmap bitmap_156;

        internal Bitmap bitmap_157;

        internal Bitmap eqliPoFqeq;

        internal Bitmap bitmap_158;

        internal Bitmap bitmap_159;

        internal Bitmap bitmap_160;

        internal Bitmap bitmap_161;

        internal Bitmap bitmap_162;

        internal Bitmap bitmap_163;

        internal Bitmap bitmap_164;

        internal Bitmap bitmap_165;

        internal Bitmap bitmap_166;

        internal Bitmap bitmap_167;

        internal Bitmap bitmap_168;

        internal Bitmap bitmap_169;

        internal Bitmap bitmap_170;

        internal Bitmap bitmap_171;

        internal Bitmap bitmap_172;

        internal Bitmap iEycGdoMqb;

        internal Bitmap bitmap_173;

        internal Bitmap bitmap_174;

        internal Bitmap bitmap_175;

        internal Bitmap bitmap_176;

        internal Bitmap bitmap_177;

        internal Bitmap bitmap_178;

        internal Bitmap bitmap_179;

        internal Bitmap FyKcynWgNv;

        internal RectangleF rectangleF_0;

        internal bool bool_12;

        internal Bitmap bitmap_180;

        internal Texture2D texture2D_2;

        internal Texture2D texture2D_3;

        internal Texture2D texture2D_4;

        internal Texture2D texture2D_5;

        internal uint[] uint_0;

        internal Bitmap bitmap_181;

        internal Bitmap bitmap_182;

        internal Bitmap bitmap_183;

        internal Bitmap bitmap_184;

        internal RectangleF rectangleF_1;

        internal float UgecxqhvjP;

        internal bool bool_13;

        //internal bool bool_14;

        internal Bitmap bitmap_185;

        internal Bitmap bitmap_186;

        internal RectangleF rectangleF_2;

        internal float float_1;

        internal bool bool_15;

        internal Bitmap bitmap_187;

        internal Bitmap bitmap_188;

        internal Bitmap bitmap_189;

        internal Bitmap bitmap_190;

        internal Bitmap bitmap_191;

        internal Bitmap[] bitmap_192;

        internal Bitmap[] bitmap_193;

        internal Bitmap[] bitmap_194;

        internal Bitmap[] bitmap_195;

        internal Bitmap[] bitmap_196;

        internal Bitmap[] bitmap_197;

        internal Bitmap[] bitmap_198;

        //internal int int_6;

        internal int DiZcdsnvl0;

        internal int int_7;

        internal int int_8;

        internal int int_9;

        internal int int_10;

        internal Bitmap[] bitmap_199;

        internal Bitmap[] bitmap_200;

        internal Bitmap[] bitmap_201;

        internal Bitmap[] bitmap_202;

        //internal Bitmap[] bitmap_203;

        internal Bitmap[] bitmap_204;

        internal Bitmap[] bitmap_205;

        internal Bitmap[] bitmap_206;

        //internal Bitmap[] bitmap_207;

        //internal Bitmap[] bitmap_208;

        public List<Bitmap[]> list_3;

        public List<Bitmap[]> list_4;

        internal Bitmap[] bitmap_209;

        internal Bitmap[] bitmap_210;

        internal Bitmap[] bitmap_211;

        internal Bitmap[] bitmap_212;

        //internal Bitmap[] bitmap_213;

        //internal Bitmap[] bitmap_214;

        internal Bitmap[] bitmap_215;

        internal Bitmap[] bitmap_216;

        internal Bitmap[] bitmap_217;

        internal Bitmap[] bitmap_218;

        internal Bitmap[] bitmap_219;

        internal Bitmap bitmap_220;

        internal Bitmap bitmap_221;

        internal Bitmap bitmap_222;

        internal Bitmap bitmap_223;

        internal HoverPanel hoverPanel_0;

        internal Rectangle rectangle_0;

        private int int_11;

        private int int_12;

        internal DiplomaticMessageQueue diplomaticMessageQueue_0;

        internal ItemListCollectionPanel itemListCollectionPanel_0;

        private Keys keys_0;

        private DateTime dateTime_0;

        internal Font font_0;

        internal Font font_1;

        internal Color color_0;

        internal Font font_2;

        internal Color color_1;

        public Font font_3;

        internal Font font_4;

        internal Font font_5;

        internal Color color_2;

        internal Font font_6;

        internal Font font_7;

        internal Font font_8;

        //private Graphics graphics_0;

        private Graphics graphics_1;

        private Graphics graphics_2;

        private Habitat habitat_0;

        private BuiltObject builtObject_0;

        private BuiltObject builtObject_1;

        private BuiltObject builtObject_2;

        private BuiltObject builtObject_3;

        private ShipGroup shipGroup_0;

        private BuiltObject builtObject_4;

        private ShipGroup shipGroup_1;

        private Design design_0;

        private Design design_1;

        private string string_16;

        private Design design_2;

        internal Rectangle[] rectangle_1;

        internal bool bool_16;

        internal int int_13;

        internal int int_14;

        public int int_15;

        public int int_16;

        internal int int_17;

        internal int int_18;

        internal int int_19;

        internal int int_20;

        internal int int_21;

        internal int vhadzRiecM;

        private bool bool_17;

        private Point gyCmRgDujR;

        internal bool bool_18;

        internal BuiltObject builtObject_5;

        internal ShipGroup shipGroup_2;

        internal string string_17;

        internal string string_18;

        internal string string_19;

        internal BuiltObject builtObject_6;

        internal Habitat habitat_1;

        internal Habitat habitat_2;

        internal Creature creature_0;

        internal ShipAction shipAction_0;

        internal string string_20;

        internal BuiltObjectList builtObjectList_0;

        internal Habitat habitat_3;

        private List<object> list_5;

        private int int_22;

        private int int_23;

        private HabitatList habitatList_0;

        private int int_24;

        private int dremNtuMsv;

        private Habitat habitat_4;

        private Creature creature_1;

        private BuiltObject builtObject_7;

        private Empire empire_0;

        internal Empire empire_1;

        private GameEvent gameEvent_0;

        internal EncyclopediaItemList encyclopediaItemList_0;

        internal EncyclopediaItemList vTtmruAejE;

        internal int int_25;

        private object object_2;

        private Race race_0;

        private Habitat habitat_5;

        private BuiltObject hOcmlqpCrp;

        private object object_3;

        private EventMessageType eventMessageType_0;

        internal Cursor cursor_0;

        internal Cursor cursor_1;

        internal Cursor cursor_2;

        internal Cursor cursor_3;

        internal Cursor cursor_4;

        internal Cursor cursor_5;

        internal Cursor cursor_6;

        internal Cursor cursor_7;

        internal Cursor cursor_8;

        internal Cursor cursor_9;

        internal Cursor cursor_10;

        internal Cursor cursor_11;

        internal Cursor cursor_12;

        internal Cursor cursor_13;

        internal Cursor cursor_14;

        internal Cursor cursor_15;

        internal Cursor cursor_16;

        internal Cursor cursor_17;

        internal Cursor cursor_18;

        internal Cursor cursor_19;

        internal Cursor cursor_20;

        internal Cursor cursor_21;

        internal Cursor cursor_22;

        internal Cursor cursor_23;

        internal Cursor cursor_24;

        internal Cursor cursor_25;

        internal Cursor cursor_26;

        internal Cursor cursor_27;

        internal Cursor cursor_28;

        internal Cursor cursor_29;

        internal Cursor cursor_30;

        internal Cursor cursor_31;

        internal Cursor cursor_32;

        internal Cursor cursor_33;

        internal Cursor cursor_34;

        internal Cursor cursor_35;

        internal Cursor cursor_36;

        internal Cursor cursor_37;

        //private Splash splash_0;

        private int int_26;

        internal double double_0;

        internal int int_27;

        internal int int_28;

        internal int int_29;

        internal bool bool_19;

        internal bool bool_20;

        internal double double_1;

        internal double double_2;

        internal double double_3;

        internal int int_30;

        //internal int int_31;

        internal DateTime dateTime_1;

        internal DateTime dateTime_2;

        internal DateTime dateTime_3;

        internal DateTime dateTime_4;

        internal double double_4;

        internal double double_5;

        internal int int_32;

        internal int int_33;

        private Empire empire_2;

        internal string string_21;

        internal Habitat habitat_6;

        internal string string_22;

        internal string string_23;

        internal double AheLexjQsu;

        internal string string_24;

        internal string string_25;

        internal string string_26;

        internal int int_34;

        //private Thread QeeLcRcmdt;

        private volatile bool bool_21;

        internal bool UhvLmNjli7;

        private int int_35;

        //private int int_36;

        internal bool bool_22;

        internal int int_37;

        internal int int_38;

        internal int int_39;

        internal int int_40;

        internal Habitat habitat_7;

        internal Habitat habitat_8;

        internal HabitatList habitatList_1;

        internal HabitatList habitatList_2;

        internal GameOptions gameOptions_0;

        internal GameSummaryList gameSummaryList_0;

        internal Color color_3;

        internal Color color_4;

        internal Color color_5;

        internal Color color_6;

        internal Color color_7;

        internal Color color_8;

        internal Color color_9;

        internal Color color_10;

        internal Color color_11;

        internal Color color_12;

        internal Color color_13;

        internal Color color_14;

        internal Color color_15;

        internal Color color_16;

        internal Color color_17;

        internal Color color_18;

        internal Color color_19;

        internal Color color_20;

        internal Color color_21;

        internal Color color_22;

        internal Color color_23;

        internal Color color_24;

        internal Color color_25;

        internal Color color_26;

        internal Color color_27;

        internal Color color_28;

        internal Color color_29;

        internal Color color_30;

        internal Color color_31;

        internal Color color_32;

        internal Color color_33;

        internal Color color_34;

        internal Color color_35;

        internal Color color_36;

        internal Color VuqPpUtdZU;

        internal Color color_37;

        internal Color color_38;

        internal Color color_39;

        internal Color color_40;

        internal Color color_41;

        internal Color color_42;

        internal SolidBrush solidBrush_0;

        internal SolidBrush solidBrush_1;

        internal Pen pen_0;

        internal Pen pen_1;

        internal SolidBrush solidBrush_2;

        internal SolidBrush solidBrush_3;

        internal Pen pen_2;

        internal Pen pen_3;

        internal Pen pen_4;

        internal Pen pen_5;

        internal Pen pen_6;

        //internal Pen pen_7;

        //internal Pen pen_8;

        //internal SolidBrush solidBrush_4;

        //internal SolidBrush solidBrush_5;

        internal SolidBrush solidBrush_6;

        internal SolidBrush solidBrush_7;

        internal SolidBrush solidBrush_8;

        internal SolidBrush solidBrush_9;

        internal SolidBrush solidBrush_10;

        internal SolidBrush solidBrush_11;

        internal SolidBrush solidBrush_12;

        internal SolidBrush solidBrush_13;

        internal SolidBrush solidBrush_14;

        internal SolidBrush solidBrush_15;

        internal SolidBrush solidBrush_16;

        internal SolidBrush solidBrush_17;

        internal SolidBrush solidBrush_18;

        internal SolidBrush solidBrush_19;

        internal SolidBrush solidBrush_20;

        internal SolidBrush solidBrush_21;

        internal SolidBrush solidBrush_22;

        internal SolidBrush solidBrush_23;

        internal SolidBrush solidBrush_24;

        internal SolidBrush solidBrush_25;

        internal SolidBrush solidBrush_26;

        internal SolidBrush solidBrush_27;

        internal HatchBrush hatchBrush_0;

        internal Pen pen_9;

        internal HatchBrush hatchBrush_1;

        internal HatchBrush hatchBrush_2;

        internal HatchBrush hatchBrush_3;

        internal HatchBrush hatchBrush_4;

        internal HatchBrush hatchBrush_5;

        private Tutorial tutorial_0;

        private DateTime dateTime_5;

        internal string string_27;

        public List<object> list_6;

        internal bool bool_23;

        internal bool bool_24;

        internal object object_4;

        private Delegate3 delegate3_0;

        internal bool bool_25;

        private int int_41;

        private int int_42;

        private int int_43;

        private int int_44;

        private int int_45;

        private int int_46;

        private int int_47;

        private int int_48;

        private int int_49;

        private int int_50;

        private int int_51;

        private int int_52;

        private int int_53;

        private int int_54;

        private int int_55;

        private int int_56;

        private int int_57;

        private int int_58;

        private DateTime dateTime_6;

        private int int_59;

        public int ProcessorCount;

        private Bitmap[] bitmap_224;

        private string string_28;

        private string string_29;

        private ConversationOptionList conversationOptionList_0;

        private object object_5;

        private Delegate5 delegate5_0;

        private TradeableItemList tradeableItemList_0;

        private TradeableItemList tradeableItemList_1;

        private TradeableItemList tradeableItemList_2;

        private TradeableItemList tradeableItemList_3;

        private TradeableItemList tradeableItemList_4;

        private TradeableItemList tradeableItemList_5;

        private Empire empire_3;

        private int int_60;

        private Empire empire_4;

        private bool bool_26;

        private int int_61;

        private int int_62;

        private int int_63;

        private double double_6;

        private object object_6;

        private bool UeqnAxbxfb;

        private bool bool_27;

        private EmpireMessage empireMessage_0;

        private Control control_0;

        private DiplomaticRelation diplomaticRelation_0;

        //[CompilerGenerated]
        //private static Comparison<KeyValuePair<string, int>> comparison_0;
        [IgnoreDataMember]
        public static ExpansionModMain _ExpModMain = new ExpansionModMain();

        public MusicPlayer MusicPlayer => musicPlayer_0;

        public EffectsPlayer EffectsPlayer => effectsPlayer_0;

        private List<Keys> _pressedKeys = new List<Keys>();
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle |= 512;
                return createParams;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && icontainer_0 != null)
            {
                icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

    }

}