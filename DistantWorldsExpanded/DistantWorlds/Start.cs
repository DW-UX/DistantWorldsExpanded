// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Start
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

//using AxWMPLib;
using BaconDistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using ExpansionMod;
using Ionic.Zlib;
using Microsoft.VisualBasic.Devices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.IO;
using System.Media;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
//using WMPLib;
using static System.Windows.Forms.AxHost;
using EmpireList = DistantWorlds.Types.EmpireList;
using LinearGradientMode = DistantWorlds.Controls.LinearGradientMode;

namespace DistantWorlds
{
    public partial class Start : Form, IFontCache
    {
        private delegate void Delegate7(Bitmap image);

        private delegate void Delegate8(string customizationSetName, bool changeDefault, bool updateControls);

        private delegate void Delegate9(string customizationSetName, bool updateControls);

        private delegate void Delegate10(string message);

        public delegate void RecoverFromLoadErrorDelegate();

        public struct SIZE
        {
            public int cx;

            public int cy;
        }

        private delegate void Delegate11();

        private IContainer icontainer_0;

        private LinkLabel lnkNewGame;

        private LinkLabel lnkLoadGame;

        private LinkLabel lnkExit;

        private Label lblCopyright;

        private Label lblVersion;

        private PictureBox picTitle;

        private GradientPanel pnlButtons;

        private BorderPanel pnlSaveLoadProgress;

        private PictureBox picSaveLoadGalaxy;

        private ScreenPanel pnlNewGame;

        private ComboBox cmbYourEmpireStartLocation;

        private SmoothLabel lblHelpDescription;

        private ComboBox cmbFlagShape;

        private CheckBox chkVictoryTimeLimit;

        private CheckBox chkVictoryEconomy;

        private CheckBox chkVictoryPopulation;

        private CheckBox chkVictoryTerritory;

        private CheckBox chkVictoryTimeStart;

        private NumericUpDown numVictoryTimeStartYears;

        private NumericUpDown numVictoryTimeLimitYears;

        private SmoothLabel lblHelpTitle;

        private LinkLabel lnkCheckForUpdates;

        private LinkLabel lnkPlayScenario;

        private LinkLabel lnkCopyright;

        private ScreenPanel pnlQuickStart;

        private GradientPanel pnlQuickStartDescription;

        private Label lblQuickStartDescriptionTitle;

        private GlassButton btnQuickStart;

        private GlassButton btnQuickStartCancel;

        private RadioButton radioClassicEmpire;

        private Label lblQuickStartDescriptionDetail;

        private RadioButton radioConflict;

        private RadioButton radioRingRace;

        private RadioButton radioEpic;

        private RadioButton radioClassicRebels;

        private RadioButton radioSmall;

        private RadioButton radioRandom;

        private NumericUpDown numVictoryPopulationPercent;

        private NumericUpDown numVictoryEconomyPercent;

        private NumericUpDown numVictoryTerritoryPercent;

        private EnabledLabel lblSaveLoadMessage;

        private RadioButton radioExpandingSettlements;

        private RadioButton radioExpandingFromTheCore;

        private RadioButton radioFullyDevelopedStandard;

        private RadioButton radioFullyDevelopedLarge;

        private RadioButton radioFullyDevelopedSmall;

        private RadioButton radioSovereignTerritoriesMinorFaction;

        private RadioButton radioSovereignTerritoriesRegionalRuler;

        private RadioButton radioGalacticRepublicWildFrontiers;

        private RadioButton radioGalacticRepublicSupremeRuler;

        private LinkLabel lnkTutorial;

        private BorderPanel borderPanel1;

        private BorderPanel borderPanel2;

        private ScreenPanel FtIzCrmve5;

        private GlassButton btnTutorialStartCancel;

        private LinkLabel lnkTutorialExpansionDiplomacy;

        private LinkLabel lnkTutorialFindingYourWayAround;

        private ScreenPanel pnlEncyclopedia;

        private EncyclopediaTopicTree pnlEncyclopediaTopics;

        private GlassButton btnEncyclopediaHome;

        private WebBrowser webEncyclopediaContent;

        private RelatedEncyclopediaItemsBox pnlEncyclopediaRelatedItems;

        private GlassButton btnEncyclopediaBack;

        private GlassButton btnEncyclopediaForward;

        private CheckBox chkEncyclopediaShowAtStart;

        private LinkLabel lnkGalactopedia;

        private LinkLabel lnkAbout;

        private BorderPanel pnlAbout;

        private Label upEzpZsgAK;

        private GlassButton btnAboutClose;

        private Label lblAboutTitle;

        private PictureBox picAbout;

        //internal AxWindowsMediaPlayer mediaPlayer;

        private Label lblMenuHints;

        private RaceDropDown cmbQuickStartRace;

        private Label lblQuickStartRace;

        private LinkLabel lnkQuickStartRaceHelp;

        private ScrollingCreditsPanel pnlAboutCredits;

        private GameOptionsScreenPanel pnlGameOptions;

        private GameOptionsMessagesScreenPanel pnlGameOptionsMessages;

        private LinkLabel lnkOptions;

        private GameOptionsAdvancedDisplaySettingsScreenPanel pnlGameOptionsAdvancedDisplaySettings;

        private System.Windows.Forms.Panel pnlStartNewGameVictoryConditions;

        private System.Windows.Forms.Panel pnlStartNewGameYourEmpire;

        private System.Windows.Forms.Panel pnlStartNewGameOtherEmpires;

        private GlassButton btnStartNewGameYourEmpirePrevious;

        private GlassButton btnStartNewGameYourEmpireNext;

        private GlassButton btnStartNewGameStart;

        private GlassButton btnStartNewGameVictoryConditionsPrevious;

        private GradientPanel pnlStartNewGameYourEmpireDetails;

        private GradientPanel pnlStartNewGameYourEmpireGovernment;

        private SmoothLabel lblStartNewGameYourEmpireGovernmentName;

        private LinkLabel lnkStartNewGameYourEmpireGovernment;

        private SmoothLabel lblStartNewGameYourEmpireGovernmentAttributes;

        private SmoothLabel lblStartNewGameYourEmpireGovernmentTitle;

        private ColorDropDown cmbSecondaryColor;

        private ColorDropDown cmbPrimaryColor;

        private SmoothLabel lblStartNewGameYourEmpireName;

        private TextBox txtYourEmpireName;

        private SmoothLabel lblStartNewGameYourEmpireMainColor;

        private SmoothLabel lblStartNewGameYourEmpireSecondaryColor;

        private GradientPanel pnlStartNewGameYourEmpireGalaxyLocation;

        private PictureBox picStartNewGameYourEmpireGalaxyLocation;

        private SmoothLabel lblStartNewGameYourEmpireGalaxyLocation;

        private LabelledTrackBar tbarStartNewGameYourEmpireSize;

        private LabelledTrackBar tbarStartNewGameYourEmpireTechLevel;

        private LabelledTrackBar tbarStartNewGameYourEmpireHomeSystem;

        private GlassButton btnStartNewGameOtherEmpiresNext;

        private GlassButton btnStartNewGameOtherEmpiresPrevious;

        private CheckBox chkGalaxyNewEmpiresDuringGame;

        private System.Windows.Forms.Panel pnlStartNewGameTheGalaxy;

        private LabelledTrackBar tbarStartNewGameTheGalaxyPirates;

        private LabelledTrackBar tbarStartNewGameTheGalaxySpaceCreatures;

        private LabelledTrackBar tbarStartNewGameTheGalaxyResearchSpeed;

        private LabelledTrackBar tbarStartNewGameTheGalaxyAggression;

        private LabelledTrackBar tbarStartNewGameTheGalaxyExpansion;

        private GlassButton btnStartNewGameTheGalaxyNext;

        private GradientPanel pnlStartNewGameGalaxyShapeSize;

        public LabelledTrackBar tbarStartNewGameTheGalaxyStarDensity;

        private SmoothLabel lblStartNewGameGalaxyShapeTitle;

        private SmoothLabel lblStartNewGameGalaxyShapeDescription;

        private RadioButton radStartNewGameGalaxyShapeSpiral;

        private RadioButton radStartNewGameGalaxyShapeRing;

        private RadioButton radStartNewGameGalaxyShapeIrregular;

        private RadioButton radStartNewGameGalaxyShapeElliptical;

        private PictureBox picStartNewGameTheGalaxyPreview;

        private SmoothLabel lblStartNewGameOtherEmpiresOR;

        private GradientPanel pnlStartNewGameOtherEmpiresAutoGen;

        private GradientPanel pnlStartNewGameOtherEmpiresList;

        private StartingEmpiresListView ctlStartingEmpiresList;

        private GlassButton btnAddNewEmpire;

        private SmoothLabel lblStartNewGameOtherEmpiresAutoGenNumberDescrip2;

        private SmoothLabel lblStartNewGameOtherEmpiresAutoGenNumberDescrip1;

        private CheckBox chkOtherEmpiresAutogenerate;

        private NumericUpDown numAutogenerateEmpiresAmount;

        private PictureBox picStartNewGameOtherEmpiresImageBottom;

        private PictureBox picStartNewGameTheGalaxyImage;

        private PictureBox picStartNewGameYourEmpireImage;

        private PictureBox picStartNewGameVictoryConditionsImage;

        private SmoothLabel lblVictorySandbox;

        private LinkLabel lnkThemes;

        private ThemesScreenPanel pnlThemes;

        
        private LabelledTrackBar tbarStartNewGameYourEmpireCorruption;

        private ScreenPanel pnlGameOptionsEmpireSettings;

        private LabelledTrackBar sldGameOptionsAttackOvermatch;

        private GroupBox grpGameOptionsDefaultEngagementStances;

        private ComboBox cmbGameOptionsEngagementStanceOther;

        private ComboBox cmbGameOptionsEngagementStancePatrol;

        private Label lblGameOptionsEngagementStanceOther;

        private Label lblGameOptionsEngagementStanceEscort;

        private ComboBox cmbGameOptionsEngagementStanceEscort;

        private Label lblGameOptionsEngagementStancePatrol;

        private CheckBox chkOptionsAllowSameSystemAsOtherEmpires;

        private ComboBox cmbGameOptionsEngagementStanceAttack;

        private Label lblGameOptionsEngagementStanceAttack;

        private GroupBox grpGameOptionsFleetAttackSettings;

        private NumericUpDown numGameOptionsFleetAttackGather;

        private NumericUpDown numGameOptionsFleetAttackRefuel;

        private Label lblGameOptionsFleetAttackGather;

        private Label lblGameOptionsFleetAttackRefuel;

        private RadioButton radStartNewGameGalaxyShapeClustersVaried;

        private RadioButton radStartNewGameGalaxyShapeClustersEven;

        private SmoothLabel lblStartNewGameTheGalaxyResearchBaseTechLabel;

        private NumericUpDown numStartNewGameTheGalaxyResearchBaseTech;

        private ComboBox cmbStartNewGameTheGalaxyPirateProximity;

        private SmoothLabel lblStartNewGameTheGalaxyPirateProximityLabel;

        private GroupBox grpGameOptionsDiscoveries;

        private ComboBox cmbGameOptionsEncounterAbandonedShipOrBase;

        private ComboBox cmbGameOptionsEncounterRuins;

        private Label lblGameOptionsEncounterAbandonedShipOrBase;

        private Label lblGameOptionsEncounterRuins;

        private CheckBox chkOptionsNewShipsAutomated;

        private CheckBox chkStoryReturnOfTheShakturi;

        private CheckBox chkQuickStartReturnOfTheShakturiStoryEvents;

        private CheckBox chkStoryDistantWorlds;

        private CheckBox chkQuickStartDistantWorldsStoryEvents;

        private LabelledTrackBar tbarStartNewGameTheGalaxyDifficulty;

        private CheckBox chkVictoryEnableDisasterEvents;

        private CheckBox chkVictoryEnableRaceSpecificConditions;

        private CheckBox chkOptionsSuppressAllPopups;

        private LabelledTrackBar tbarStartNewGameTheGalaxyDimensions;

        private CheckBox chkVictoryEnableRaceSpecificEvents;

        private SmoothLabel lblVictoryThresholdPercentage;

        private ComboBox cmbVictoryThresholdPercentage;

        private System.Windows.Forms.Panel pnlStartNewGameYourRace;

        private GradientPanel pnlStartNewGameYourEmpireRace;

        private System.Windows.Forms.Panel pnlStartNewGameYourEmpireRaceAttributesContainer;

        private SmoothLabel lblStartNewGameYourEmpireRaceName;

        private LinkLabel lnkStartNewGameYourEmpireRace;

        private PictureBox picStartNewGameYourEmpireRace;

        private RaceDropDown cmbStartNewGameYourEmpireRace;

        private SmoothLabel lblStartNewGameYourEmpireRaceTitle;

        private GlassButton btnStartNewGameYourRacePrevious;

        private GlassButton btnStartNewGameYourRaceNext;

        private RaceSummaryPanel pnlStartNewGameYourEmpireRaceAttributes;

        private System.Windows.Forms.Panel pnlStartNewGameVictoryConditionsGroup;

        private PictureBox picStartNewGameYourRaceImage;

        private System.Windows.Forms.Panel pnlStartNewGameColonizationTerritory;

        private LabelledTrackBar tbarStartNewGameTheGalaxyColonyPrevalence;

        private LabelledTrackBar tbarStartNewGameTheGalaxyAlienLife;

        private PictureBox picStartNewGameColonizationTerritoryImage;

        private GlassButton btnStartNewGameColonizationTerritoryPrevious;

        private GlassButton btnStartNewGameColonizationTerritoryNext;

        private GroupBox grpStartNewGameColonizationTerritoryColonizationRange;

        private Label lblStartNewGameColonizationTerritoryColonyInfluenceRangeTitle;

        private ColorSlider sldStartNewGameColonizationTerritoryColonyInfluenceRange;

        private Label lblStartNewGameColonizationTerritoryColonyInfluenceRangeValue;

        private Label lblStartNewGameColonizationTerritoryColonizationRangeValue;

        private Label lblStartNewGameColonizationTerritoryColonizationRangeTitle;

        private ColorSlider sldStartNewGameColonizationTerritoryColonizationRange;

        private CheckBox chkStartNewGameColonizationTerritoryEnforceColonizationRange;

        private Label lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion;

        private System.Windows.Forms.Panel pnlStartNewGameYourEmpireType;

        private GlassButton btnStartNewGameYourEmpireTypeNormalShadows;

        private GlassButton btnStartNewGameYourEmpireTypePirateShadows;

        private CheckBox chkStartNewGameTheGalaxyDifficultyScaling;

        private GlassButton btnStartNewGameYourEmpireTypeNormalClassic;

        private GlassButton btnStartNewGameYourEmpireTypePirateClassic;

        private GlassButton btnStartNewGameTheGalaxyPrevious;
        
        private CheckBox chkStoryShadows;

        private SmoothLabel lblVictoryPiratePlaystyle;

        private ComboBox cmbVictoryPiratePlayStyle;

        private SmoothLabel lblPiratePlaystyleDescription;

        private LinkLabel lnkTutorialPreWarpEmpire;

        private LinkLabel lnkTutorialPlayAsPirate;

        private PictureBox picStartNewGameYourEmpirePiratePlaystyle;

        private CheckBox chkStartNewGameTheGalaxyPiratesRespawn;

        private GroupBox grpGameOptionsDefaultEngagementStancesManual;

        private ComboBox cmbGameOptionsEngagementStanceAttackManual;

        private Label lblGameOptionsEngagementStanceAttackManual;

        private ComboBox cmbGameOptionsEngagementStanceOtherManual;

        private ComboBox cmbGameOptionsEngagementStancePatrolManual;

        private Label lblGameOptionsEngagementStanceOtherManual;

        private Label lblGameOptionsEngagementStanceEscortManual;

        private ComboBox cmbGameOptionsEngagementStanceEscortManual;

        private Label lblGameOptionsEngagementStancePatrolManual;

        private LabelledTrackBar tbarStartNewGameTheGalaxyPirateStrength;

        private CheckBox chkStartNewGameEnableTechTrading;

        private GovernmentStyleDropDown cmbStartNewGameYourEmpireGovernment;

        private System.Windows.Forms.Panel pnlStartNewGameGalaxyMaps;

        private GlassButton btnStartNewGameGalaxyMapsStart;

        private GlassButton btnStartNewGameGalaxyMapsCustom;

        private SmoothLabel lblStartNewGameGalaxyMapsExplanation;

        private EmpireSummaryPanel pnlStartNewGameGalaxyMapsEmpire;

        private EmpireSummaryListView ctlStartNewGameGalaxyMapsEmpires;

        private GalaxySummaryPanel pnlStartNewGameGalaxyMapsGalaxy;

        private GalaxySummaryListView ctlStartNewGameGalaxyMapsGalaxies;

        private SmoothLabel lblStartNewGameGalaxyMapsAvailableFactions;

        private SmoothLabel lblStartNewGameGalaxyMapsAvailableGalaxies;

        private GradientPanel pnlStartNewGameTheGalaxyLoadExisting;

        private GlassButton btnStartNewGameTheGalaxyLoadExistingClear;

        private GlassButton btnStartNewGameTheGalaxyLoadExistingBrowse;

        private SmoothLabel lblStartNewGameTheGalaxyLoadExistingFilepath;

        private SmoothLabel lblStartNewGameTheGalaxyLoadExistingTitle;

        private CheckBox chkStartNewGameTheGalaxyLoadExistingSpecialLocations;

        private CheckBox chkStartNewGameTheGalaxyLoadExistingRuins;

        private CheckBox chkStartNewGameTheGalaxyLoadExistingCreatures;

        private CheckBox chkStartNewGameTheGalaxyLoadExistingSceneryResearch;

        private CheckBox chkStartNewGameTheGalaxyLoadExistingResources;


        private CheckBox chkStartNewGameEnableGiantKaltors;

        private System.Windows.Forms.Panel pnlStartNewGameJumpStart;

        private GlassButton btnJumpStartTheGalaxyNext;

        private GlassButton btnJumpStartTheGalaxyPrevious;

        private LabelledTrackBar tbarJumpStartTheGalaxyDifficulty;

        private GradientPanel pnlJumpStartGalaxyShapeSize;

        private LabelledTrackBar tbarJumpStartTheGalaxyDimensions;

        private RadioButton radJumpStartGalaxyShapeVariedClusters;

        private RadioButton radJumpStartGalaxyShapeEvenClusters;

        private LabelledTrackBar tbarJumpStartTheGalaxyStarDensity;

        private SmoothLabel lblJumpStartGalaxyShapeTitle;

        private SmoothLabel lblJumpStartGalaxyShapeDescription;

        private RadioButton radJumpStartGalaxyShapeSpiral;

        private RadioButton radJumpStartGalaxyShapeRing;

        private RadioButton radJumpStartGalaxyShapeIrregular;

        private RadioButton radJumpStartGalaxyShapeElliptical;

        private PictureBox picJumpStartTheGalaxyPreview;

        private GradientPanel pnlJumpStartYourEmpireGovernment;

        private GovernmentStyleDropDown cmbJumpStartYourEmpireGovernment;

        private SmoothLabel lblJumpStartYourEmpireGovernmentName;

        private LinkLabel lnkJumpStartYourEmpireGovernment;

        private SmoothLabel lblJumpStartYourEmpireGovernmentAttributes;

        private SmoothLabel lblJumpStartYourEmpireGovernmentTitle;

        private GradientPanel pnlJumpStartYourEmpireRace;

        private System.Windows.Forms.Panel pnlJumpStartYourEmpireRaceAttributesContainer;

        private RaceSummaryPanel pnlJumpStartYourEmpireRaceAttributes;

        private SmoothLabel lblJumpStartYourEmpireRaceName;

        private LinkLabel lnkJumpStartYourEmpireRace;

        private PictureBox picJumpStartYourEmpireRace;

        private RaceDropDown cmbJumpStartYourEmpireRace;

        private SmoothLabel lblJumpStartYourEmpireRaceTitle;

        private CheckBox chkJumpStartTheGalaxyDifficultyScaling;

        private PictureBox picJumpStartYourEmpirePiratePlaystyle;

        private SmoothLabel lblJumpStartPiratePlaystyleDescription;

        private SmoothLabel lblJumpStartVictoryPiratePlaystyle;

        private ComboBox cmbJumpStartVictoryPiratePlayStyle;

        private GlassButton btnStartNewGameYourEmpireTypeLegends;

        private GlassButton btnStartNewGameYourEmpireTypeReturnOfTheShakturi;

        private GlassButton btnStartNewGameYourEmpireTypeQuickStarts;

        private GlassButton btnStartNewGameYourEmpireTypeClassicEra;

        private GlassButton btnStartNewGameYourEmpireTypeTheAncientGalaxy;

        private PictureBox picStartNewGameYourEmpireTypeTimeline;

        public ToolTip toolTip;

        private System.Windows.Forms.Panel pnlJumpStartPiratePlaystyleDescriptionContainer;

        private SmoothLabel lblActiveTheme;

        private SmoothLabel lblStartNewGameActiveTheme;

        private HoverMenuGroup menuGroup;

        private HoverMenuItem menuExit;

        private HoverMenuItem menuChangeTheme;

        private HoverMenuItem menuOptions;

        private HoverMenuItem menuLoadGame;

        private HoverMenuItem menuStartNewGame;

        private HoverMenuItem menuTutorials;

        private HoverMenuItem menuGalactopedia;

        private HoverMenuItem menuCheckForUpdates;

        private HoverMenuItem menuCredits;

        private System.Windows.Forms.Panel pnlTopLeftCorner;

        private System.Windows.Forms.Panel pnlBottomLeftCorner;

        private LinkLabel lnkTutorialDealingWithPirates;

        private LinkLabel lnkTutorialFleetsTroops;

        private LinkLabel lnkTutorialResearchDesign;

        private LinkLabel lnkTutorialShipsMissions;

        private LinkLabel lnkTutorialEmpireAndColonies;

        private GlassButton btnStartNewGameIntroductory;

        private RoundRectanglePanel pnlStartNewGameIntroductoryBorder;

        public Main main_0;

        private Game game_0;

        private bool bool_0;

        private bool bool_1;

        private Bitmap bitmap_0;

        private Bitmap bitmap_1;

        private Bitmap bitmap_2;

        private Bitmap bitmap_3;

        private Bitmap bitmap_4;

        private Bitmap bitmap_5;

        private Bitmap bitmap_6;

        private Bitmap bitmap_7;

        private Bitmap bitmap_8;

        private Bitmap bitmap_9;

        internal RaceList raceList_0;

        internal RaceList raceList_1;

        private Bitmap bitmap_10;

        private Bitmap bitmap_11;

        private Bitmap bitmap_12;

        private Bitmap bitmap_13;

        private Color color_0;

        private SolidBrush solidBrush_0;

        internal EncyclopediaItemList encyclopediaItemList_0;

        internal EncyclopediaItemList encyclopediaItemList_1;

        internal int int_0;

        private string[] string_0;

        private double double_0;

        private Thread thread_0;

        private BackgroundWorker oyxRtRyAwjg;

        private System.Timers.Timer timer_0;

        private System.Windows.Forms.Panel panel_0;

        private Size size_0;

        private float float_0;

        private Bitmap bitmap_14;

        private Delegate7 delegate7_0;

        private Delegate8 delegate8_0;

        private Delegate9 delegate9_0;

        private Delegate10 delegate10_0;

        private float float_1;

        private PrivateFontCollection privateFontCollection_0;

        private IntPtr intptr_0;

        private IntPtr intptr_1;

        private Font font_0;

        private Font font_1;

        private Font font_2;

        private Font font_3;

        private Font font_4;

        private Font font_5;

        private Font font_6;

        private Font font_7;

        private Font font_8;

        private Font font_9;

        private Font font_10;

        private List<Control> list_0;

        private bool bool_2;

        private bool bool_3;

        internal static ResourceSystem resourceSystem_0;

        private GalaxySummaryList galaxySummaryList_0;

        private Exception exception_0;

        protected static IntPtr m_HBitmap;

        //private Delegate11 delegate11_0;

        private string string_1;

        //private bool bool_4;

        private string wjhRtsSwmsa;

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


        [DllImport("kernel32.dll")]
        private static extern ErrorModes SetErrorMode(ErrorModes errorModes_0);

        [DllImport("kernel32.dll")]
        private static extern ErrorModes GetErrorMode();

        public Start() : base()
        {

            bool_0 = true;
            color_0 = Color.FromArgb(96, 255, 64, 64);
            oyxRtRyAwjg = new BackgroundWorker();
            timer_0 = new System.Timers.Timer();
            float_1 = 1f;
            list_0 = new List<Control>();
            galaxySummaryList_0 = new GalaxySummaryList();
            string_1 = string.Empty;
            //bool_4 = true;
            wjhRtsSwmsa = string.Empty;
            try
            {
                TextResolver.LoadText(Application.StartupPath + "\\GameText.txt");
                GameOptions gameOptions = Main.smethod_0();
                string customizationSetName = string.Empty;
                if (gameOptions != null && !string.IsNullOrEmpty(gameOptions.CustomizationSetName))
                {
                    customizationSetName = gameOptions.CustomizationSetName;
                    string text = Application.StartupPath + "\\Customization\\" + gameOptions.CustomizationSetName + "\\";
                    if (!Directory.Exists(text))
                    {
                        customizationSetName = string.Empty;
                    }
                    string text2 = text + "GameText.txt";
                    if (File.Exists(text2))
                    {
                        TextResolver.LoadText(text2);
                    }
                }
                Galaxy.InitializeData(Application.StartupPath, customizationSetName, out resourceSystem_0);
                InitializeComponent();
                BaconStart.InitializeMore(this, gameOptions);
                Main._ExpModMain.ModStartup(this);
                intptr_0 = method_150("DistantWorlds.Resources.Forgotte.ttf");
                intptr_1 = method_150("DistantWorlds.Resources.Forgottb.ttf");
                SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
                SetStyle(ControlStyles.UserPaint, value: true);
                UpdateStyles();
                timer_0.Elapsed += timer_0_Elapsed;
                delegate7_0 = method_18;
                delegate8_0 = method_2;
                delegate9_0 = method_4;
                delegate10_0 = method_8;
            }
            catch (Exception ex)
            {
                Main.CrashDump(ex);
                throw;
            }
        }

        [DllImport("user32")]
        private static extern int SystemParametersInfo(int int_1, int int_2, int int_3, int int_4);

        public bool ToggleScreenSaverActive(bool active)
        {
            int int_ = (active ? 1 : 0);
            int num = SystemParametersInfo(17, int_, 0, 0);
            return num > 0;
        }

        private Dictionary<string, double> method_0(string string_2)
        {
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            string text = string_2 + "\\startup.ini";
            int num = 0;
            try
            {
                if (File.Exists(text))
                {
                    FileStream fileStream = File.OpenRead(text);
                    StreamReader streamReader = new StreamReader(fileStream);
                    while (true)
                    {
                        if (!streamReader.EndOfStream)
                        {
                            num++;
                            string text2 = streamReader.ReadLine();
                            if (!string.IsNullOrEmpty(text2) && text2.Trim() != string.Empty && text2.Trim().Substring(0, 1) != "'")
                            {
                                int num2 = text2.IndexOf(" ");
                                if (num2 < 0)
                                {
                                    break;
                                }
                                string text3 = text2.Substring(0, num2);
                                string text4 = text2.Substring(num2, text2.Length - num2);
                                text4 = text4.Trim();
                                double result = -1.0;
                                if (!double.TryParse(text4, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                                {
                                    throw new ApplicationException(string.Format(TextResolver.GetText("Error reading value in file Startup.ini at line X"), num.ToString()));
                                }
                                dictionary.Add(text3.ToLower(CultureInfo.InvariantCulture), result);
                            }
                            continue;
                        }
                        streamReader.Close();
                        fileStream.Close();
                        return dictionary;
                    }
                    throw new ApplicationException(string.Format(TextResolver.GetText("Error reading name in file Startup.ini at line X"), num.ToString()));
                }
                return dictionary;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ApplicationException(string.Format(TextResolver.GetText("Error at line X reading file Y"), num.ToString(), text));
            }
        }

        private void Start_Load(object sender, EventArgs e)
        {
            //IL_1ba8: Unknown result type (might be due to invalid IL or missing references)
            //IL_1bb2: Expected O, but got Unknown
            //IL_1bbf: Unknown result type (might be due to invalid IL or missing references)
            //IL_1bc9: Expected O, but got Unknown
            //IL_1bd6: Unknown result type (might be due to invalid IL or missing references)
            //IL_1be0: Expected O, but got Unknown
            try
            {
                SetControlLocalizedLabels();
                font_0 = ((IFontCache)this).GenerateFont(15.33f, isBold: false);
                font_1 = ((IFontCache)this).GenerateFont(20.77f, isBold: false);
                font_2 = ((IFontCache)this).GenerateFont(15.33f, isBold: false);
                font_3 = ((IFontCache)this).GenerateFont(16.67f, isBold: false);
                font_4 = ((IFontCache)this).GenerateFont(16.67f, isBold: true);
                font_6 = ((IFontCache)this).GenerateFont(15.33f, isBold: true);
                font_5 = ((IFontCache)this).GenerateFont(18.67f, isBold: false);
                font_7 = ((IFontCache)this).GenerateFont(18.67f, isBold: true);
                font_8 = ((IFontCache)this).GenerateFont(18.67f, isBold: true);
                font_9 = ((IFontCache)this).GenerateFont(22.67f, isBold: true);
                font_10 = ((IFontCache)this).GenerateFont(32f, isBold: true);
                Font font = Font;
                method_151(font_0, this);
                method_152(font_0, this, typeof(BorderPanel));
                method_152(((IFontCache)this).GenerateFont(15.83f, isBold: true), this, typeof(GlassButton));
                Font = font;
                //Splash splash = new Splash();
                //method_151(font_7, splash.lblMessage);
                //splash.Show();
                //method_151(font_7, Class5._Splash.lblMessage);
                Class5._Splash.SetFont(font_7);
                //Class5._Splash.Show();
                Class5._Splash.Start();
                pnlAbout.SetFontCache(this);
                pnlEncyclopedia.SetFontCache(this);
                pnlNewGame.SetFontCache(this);
                pnlQuickStart.SetFontCache(this);
                pnlSaveLoadProgress.SetFontCache(this);
                FtIzCrmve5.SetFontCache(this);
                pnlGameOptions.SetFontCache(this);
                pnlGameOptionsAdvancedDisplaySettings.SetFontCache(this);
                pnlGameOptionsEmpireSettings.SetFontCache(this);
                pnlGameOptionsMessages.SetFontCache(this);
                pnlThemes.SetFontCache(this);
                pnlButtons.SetFontCache(this);
                pnlEncyclopediaRelatedItems.SetFontCache(this);
                pnlEncyclopediaTopics.SetFontCache(this);
                pnlStartNewGameYourEmpireDetails.SetFontCache(this);
                pnlStartNewGameYourEmpireGalaxyLocation.SetFontCache(this);
                pnlStartNewGameYourEmpireGovernment.SetFontCache(this);
                pnlStartNewGameYourEmpireRace.SetFontCache(this);
                pnlQuickStartDescription.SetFontCache(this);
                ctlStartingEmpiresList.SetFontCache(this);
                pnlThemes.pnlThemeDetail.SetFontCache(this);
                ctlStartNewGameGalaxyMapsGalaxies.SetFontCache(this);
                ctlStartNewGameGalaxyMapsEmpires.SetFontCache(this);
                Size size = Screen.GetBounds(this).Size;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                string text = Application.StartupPath + "\\images\\";
                string text2 = text + "ui\\chrome\\";
                bitmap_0 = (Bitmap)Image.FromFile(text2 + "galaxy.png");
                bitmap_1 = (Bitmap)Image.FromFile(text2 + "smallTitle.png");
                bitmap_2 = (Bitmap)Image.FromFile(text2 + "codeforce.png");
                bitmap_3 = (Bitmap)Image.FromFile(text2 + "matrix.png");
                bitmap_11 = (Bitmap)Image.FromFile(text2 + "remove.png");
                bitmap_4 = (Bitmap)Image.FromFile(text2 + "galaxyshape_elliptical.png");
                bitmap_5 = (Bitmap)Image.FromFile(text2 + "galaxyshape_spiral.png");
                bitmap_6 = (Bitmap)Image.FromFile(text2 + "galaxyshape_ring.png");
                bitmap_7 = (Bitmap)Image.FromFile(text2 + "galaxyshape_irregular.png");
                bitmap_8 = (Bitmap)Image.FromFile(text2 + "galaxyshape_clusterseven.png");
                bitmap_9 = (Bitmap)Image.FromFile(text2 + "galaxyshape_clustersvaried.png");
                bitmap_12 = (Bitmap)Image.FromFile(text2 + "pirateflag.png");
                bitmap_13 = (Bitmap)Image.FromFile(text2 + "pirateflag_small.png");
                solidBrush_0 = new SolidBrush(color_0);
                Dictionary<string, double> dictionary = method_0(Application.StartupPath);
                int num = 0;
                if (dictionary.ContainsKey("screenwidth"))
                {
                    num = (int)dictionary["screenwidth"];
                }
                int num2 = 0;
                if (dictionary.ContainsKey("screenheight"))
                {
                    num2 = (int)dictionary["screenheight"];
                }
                bool flag = true;
                if (num < 1024 || num2 < 768)
                {
                    num = size.Width;
                    num2 = size.Height;
                    flag = false;
                }
                main_0 = new Main(num, num2, flag);
                bool flag2 = true;
                if (dictionary.ContainsKey("playmovie") && (int)dictionary["playmovie"] == 0)
                {
                    flag2 = false;
                }
                double_0 = 1.0;
                if (dictionary.ContainsKey("hyperdrivespeed"))
                {
                    double_0 = dictionary["hyperdrivespeed"];
                    double_0 = Math.Max(1.0, Math.Min(3.0, double_0));
                }
                base.Visible = false;
                SuspendLayout();
                SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
                SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
                UpdateStyles();
                BringToFront();
                if (flag)
                {
                    base.FormBorderStyle = FormBorderStyle.Fixed3D;
                    base.MaximizeBox = false;
                    base.ClientSize = new Size(num, num2);
                    CenterToScreen();
                }
                else
                {
                    base.FormBorderStyle = FormBorderStyle.None;
                    base.ClientSize = new Size(num, num2);
                    base.Location = new Point(0, 0);
                    base.WindowState = FormWindowState.Minimized;
                    base.WindowState = FormWindowState.Maximized;
                }
                ToggleScreenSaverActive(active: false);
                lblActiveTheme.Font = font_7;
                int num3 = num / 2;
                lblActiveTheme.Location = new Point(num3, 20);
                lblActiveTheme.TextAlign = ContentAlignment.MiddleCenter;
                lblStartNewGameActiveTheme.Font = font_6;
                lblStartNewGameActiveTheme.TextAlign = ContentAlignment.MiddleCenter;
                int num4 = 340;
                int num5 = 500;
                int num6 = 300;
                int num7 = 111;
                int num8 = 76;
                if (num > 1920 && num2 > 1080)
                {
                    float_1 = (float)num2 / 1080f;
                    num4 = (int)((float)num4 * float_1);
                    num5 = (int)((float)num5 * float_1);
                    num6 = (int)((float)num6 * float_1);
                    num7 = (int)((float)num7 * float_1);
                    num8 = (int)((float)num8 * float_1);
                    font_2 = ((IFontCache)this).GenerateFont(15.33f * float_1, isBold: false);
                    font_8 = ((IFontCache)this).GenerateFont(18.67f * float_1, isBold: true);
                }
                pnlButtons.Size = new Size(num4, num5);
                int num9 = (num2 - pnlButtons.Height) / 2;
                pnlButtons.Location = new Point((num - pnlButtons.Width) / 2, num9);
                pnlButtons.BackColor = Color.FromArgb(208, 15, 15, 15);
                pnlButtons.BackColor2 = Color.FromArgb(208, 37, 35, 49);
                pnlButtons.BackColor3 = Color.FromArgb(208, 15, 15, 15);
                pnlButtons.BorderColor = Color.FromArgb(208, 31, 30, 41);
                pnlButtons.BorderWidth = 3;
                Color linkColor = Color.FromArgb(194, 194, 194);
                Color activeLinkColor = Color.FromArgb(255, 192, 0);
                lnkTutorial.LinkColor = linkColor;
                lnkPlayScenario.LinkColor = linkColor;
                lnkNewGame.LinkColor = linkColor;
                lnkLoadGame.LinkColor = linkColor;
                lnkGalactopedia.LinkColor = linkColor;
                lnkOptions.LinkColor = linkColor;
                lnkThemes.LinkColor = linkColor;
                lnkCheckForUpdates.LinkColor = linkColor;
                lnkAbout.LinkColor = linkColor;
                lnkExit.LinkColor = linkColor;
                lnkTutorial.ActiveLinkColor = activeLinkColor;
                lnkPlayScenario.ActiveLinkColor = activeLinkColor;
                lnkNewGame.ActiveLinkColor = activeLinkColor;
                lnkLoadGame.ActiveLinkColor = activeLinkColor;
                lnkGalactopedia.ActiveLinkColor = activeLinkColor;
                lnkOptions.ActiveLinkColor = activeLinkColor;
                lnkThemes.ActiveLinkColor = activeLinkColor;
                lnkCheckForUpdates.ActiveLinkColor = activeLinkColor;
                lnkAbout.ActiveLinkColor = activeLinkColor;
                lnkExit.ActiveLinkColor = activeLinkColor;
                lnkTutorial.Font = font_10;
                lnkPlayScenario.Font = font_10;
                lnkNewGame.Font = font_10;
                lnkLoadGame.Font = font_10;
                lnkGalactopedia.Font = font_10;
                lnkOptions.Font = font_10;
                lnkThemes.Font = font_10;
                lnkCheckForUpdates.Font = font_10;
                lnkAbout.Font = font_10;
                lnkExit.Font = font_10;
                lblVersion.Font = font_2;
                lnkCopyright.Font = font_7;
                int num10 = 36;
                lnkTutorial.Visible = false;
                lnkPlayScenario.Visible = false;
                lnkNewGame.Visible = false;
                lnkLoadGame.Visible = false;
                lnkGalactopedia.Visible = false;
                lnkOptions.Visible = false;
                lnkThemes.Visible = false;
                lnkCheckForUpdates.Visible = false;
                lnkAbout.Visible = false;
                lnkExit.Visible = false;
                menuGroup.Location = new Point(0, 0);
                menuGroup.Size = pnlButtons.Size;
                menuGroup.AddMenuItem(menuTutorials);
                menuGroup.AddMenuItem(menuStartNewGame);
                menuGroup.AddMenuItem(menuLoadGame);
                menuGroup.AddMenuItem(menuOptions);
                menuGroup.AddMenuItem(menuChangeTheme);
                menuGroup.AddMenuItem(menuExit);
                int num11 = num7;
                menuTutorials.Size = new Size(num6, num11);
                menuStartNewGame.Size = new Size(num6, num11);
                menuLoadGame.Size = new Size(num6, num11);
                menuOptions.Size = new Size(num6, num11);
                menuChangeTheme.Size = new Size(num6, num11);
                menuExit.Size = new Size(num6, num11);
                if (float_1 != 1f)
                {
                    main_0.bitmap_160 = GraphicsHelper.ScaleImage(main_0.bitmap_160, (int)((float)main_0.bitmap_160.Width * float_1), (int)((float)main_0.bitmap_160.Height * float_1), 1f);
                    main_0.bitmap_159 = GraphicsHelper.ScaleImage(main_0.bitmap_159, (int)((float)main_0.bitmap_159.Width * float_1), (int)((float)main_0.bitmap_159.Height * float_1), 1f);
                    main_0.bitmap_162 = GraphicsHelper.ScaleImage(main_0.bitmap_162, (int)((float)main_0.bitmap_162.Width * float_1), (int)((float)main_0.bitmap_162.Height * float_1), 1f);
                    main_0.bitmap_161 = GraphicsHelper.ScaleImage(main_0.bitmap_161, (int)((float)main_0.bitmap_161.Width * float_1), (int)((float)main_0.bitmap_161.Height * float_1), 1f);
                    main_0.bitmap_164 = GraphicsHelper.ScaleImage(main_0.bitmap_164, (int)((float)main_0.bitmap_164.Width * float_1), (int)((float)main_0.bitmap_164.Height * float_1), 1f);
                    main_0.bitmap_163 = GraphicsHelper.ScaleImage(main_0.bitmap_163, (int)((float)main_0.bitmap_163.Width * float_1), (int)((float)main_0.bitmap_163.Height * float_1), 1f);
                    main_0.bitmap_166 = GraphicsHelper.ScaleImage(main_0.bitmap_166, (int)((float)main_0.bitmap_166.Width * float_1), (int)((float)main_0.bitmap_166.Height * float_1), 1f);
                    main_0.bitmap_165 = GraphicsHelper.ScaleImage(main_0.bitmap_165, (int)((float)main_0.bitmap_165.Width * float_1), (int)((float)main_0.bitmap_165.Height * float_1), 1f);
                    main_0.bitmap_168 = GraphicsHelper.ScaleImage(main_0.bitmap_168, (int)((float)main_0.bitmap_168.Width * float_1), (int)((float)main_0.bitmap_168.Height * float_1), 1f);
                    main_0.bitmap_167 = GraphicsHelper.ScaleImage(main_0.bitmap_167, (int)((float)main_0.bitmap_167.Width * float_1), (int)((float)main_0.bitmap_167.Height * float_1), 1f);
                    main_0.bitmap_170 = GraphicsHelper.ScaleImage(main_0.bitmap_170, (int)((float)main_0.bitmap_170.Width * float_1), (int)((float)main_0.bitmap_170.Height * float_1), 1f);
                    main_0.bitmap_169 = GraphicsHelper.ScaleImage(main_0.bitmap_169, (int)((float)main_0.bitmap_169.Width * float_1), (int)((float)main_0.bitmap_169.Height * float_1), 1f);
                }
                menuTutorials.InitializeMenuItem(main_0.bitmap_160, main_0.bitmap_159);
                menuStartNewGame.InitializeMenuItem(main_0.bitmap_162, main_0.bitmap_161);
                menuLoadGame.InitializeMenuItem(main_0.bitmap_164, main_0.bitmap_163);
                menuOptions.InitializeMenuItem(main_0.bitmap_166, main_0.bitmap_165);
                menuChangeTheme.InitializeMenuItem(main_0.bitmap_168, main_0.bitmap_167);
                menuExit.InitializeMenuItem(main_0.bitmap_170, main_0.bitmap_169);
                num10 = 0;
                menuTutorials.Location = new Point((pnlButtons.Width - menuTutorials.Width) / 2, 0);
                menuStartNewGame.Location = new Point((pnlButtons.Width - menuStartNewGame.Width) / 2, 0 + num8);
                menuLoadGame.Location = new Point((pnlButtons.Width - menuLoadGame.Width) / 2, 0 + num8 * 2);
                menuOptions.Location = new Point((pnlButtons.Width - menuOptions.Width) / 2, 0 + num8 * 3);
                menuChangeTheme.Location = new Point((pnlButtons.Width - menuChangeTheme.Width) / 2, 0 + num8 * 4);
                menuExit.Location = new Point((pnlButtons.Width - menuExit.Width) / 2, 0 + num8 * 5);
                int num12 = (int)(115f * float_1);
                int num13 = (int)(97f * float_1);
                int num14 = (int)(135f * float_1);
                int num15 = (int)(117f * float_1);
                pnlTopLeftCorner.Size = new Size(num14, num15);
                pnlTopLeftCorner.Location = new Point(0, 0);
                pnlTopLeftCorner.BackColor = Color.FromArgb(144, 0, 0, 0);
                int num16 = (int)(108f * float_1);
                int num17 = (int)(56f * float_1);
                int num18 = (int)(135f * float_1);
                int num19 = (int)(112f * float_1);
                pnlBottomLeftCorner.Size = new Size(num18, num19);
                pnlBottomLeftCorner.Location = new Point(0, num2 - num19);
                pnlBottomLeftCorner.BackColor = Color.FromArgb(144, 0, 0, 0);
                menuGalactopedia.Size = new Size(num12, num13);
                menuGalactopedia.Location = new Point((num14 - num12) / 2, (num15 - num13) / 2);
                if (float_1 != 1f)
                {
                    main_0.bitmap_172 = GraphicsHelper.ScaleImage(main_0.bitmap_172, float_1, 1f);
                    main_0.bitmap_171 = GraphicsHelper.ScaleImage(main_0.bitmap_171, float_1, 1f);
                }
                menuGalactopedia.InitializeMenuItem(main_0.bitmap_172, main_0.bitmap_171);
                menuCheckForUpdates.Size = new Size(num16, num17);
                menuCheckForUpdates.Location = new Point((num18 - num16) / 2, (num19 - num17) / 2);
                if (float_1 != 1f)
                {
                    main_0.bitmap_175 = GraphicsHelper.ScaleImage(main_0.bitmap_175, float_1, 1f);
                    main_0.bitmap_174 = GraphicsHelper.ScaleImage(main_0.bitmap_174, float_1, 1f);
                }
                menuCheckForUpdates.InitializeMenuItem(main_0.bitmap_175, main_0.bitmap_174);
                int num20 = (int)(105f * float_1);
                int num21 = (int)(60f * float_1);
                menuCredits.Size = new Size(105, 60);
                menuCredits.Location = new Point(num - (num20 + 10), num2 - (num21 + 10));
                if (float_1 != 1f)
                {
                    main_0.bitmap_173 = GraphicsHelper.ScaleImage(main_0.bitmap_173, float_1, 1f);
                    main_0.iEycGdoMqb = GraphicsHelper.ScaleImage(main_0.iEycGdoMqb, float_1, 1f);
                }
                menuCredits.InitializeMenuItem(main_0.bitmap_173, main_0.iEycGdoMqb);
                lnkTutorial.Location = new Point((pnlButtons.Width - lnkTutorial.Width) / 2, num10);
                lnkPlayScenario.Location = new Point((pnlButtons.Width - lnkPlayScenario.Width) / 2, num10 + num8);
                lnkNewGame.Location = new Point((pnlButtons.Width - lnkNewGame.Width) / 2, num10 + num8 * 2);
                lnkLoadGame.Location = new Point((pnlButtons.Width - lnkLoadGame.Width) / 2, num10 + num8 * 3);
                lnkGalactopedia.Location = new Point((pnlButtons.Width - lnkGalactopedia.Width) / 2, num10 + num8 * 4);
                lnkOptions.Location = new Point((pnlButtons.Width - lnkOptions.Width) / 2, num10 + num8 * 5);
                lnkThemes.Location = new Point((pnlButtons.Width - lnkThemes.Width) / 2, num10 + num8 * 6);
                lnkCheckForUpdates.Location = new Point((pnlButtons.Width - lnkCheckForUpdates.Width) / 2, num10 + num8 * 7);
                lnkAbout.Location = new Point((pnlButtons.Width - lnkAbout.Width) / 2, num10 + num8 * 8);
                lnkExit.Location = new Point((pnlButtons.Width - lnkExit.Width) / 2, num10 + num8 * 9);
                int num22 = (int)(25f * float_1);
                lblVersion.Location = new Point(num22, pnlBottomLeftCorner.Height - 30);
                lblVersion.Text = TextResolver.GetText("Version") + " " + Application.ProductVersion;
                lblVersion.MaximumSize = new Size(pnlBottomLeftCorner.Width, 30);
                lblVersion.Size = new Size(pnlBottomLeftCorner.Width, 30);
                lblVersion.TextAlign = ContentAlignment.TopCenter;
                Color color = Color.FromArgb(255, 160, 0);
                lblVersion.ForeColor = color;
                lnkCopyright.ForeColor = color;
                lnkCopyright.LinkColor = color;
                lnkCopyright.ActiveLinkColor = color;
                int num23 = (int)(250f * float_1);
                lblMenuHints.Location = new Point(pnlButtons.Location.X - (num23 + 30), pnlButtons.Location.Y + pnlButtons.Height / 2 - 50);
                lblCopyright.Visible = false;
                string text3 = TextResolver.GetText("Copyright");
                Graphics graphics = CreateGraphics();
                int num24 = (int)graphics.MeasureString(text3, lnkCopyright.Font, 600, StringFormat.GenericTypographic).Width;
                lnkCopyright.Location = new Point((num - num24) / 2, num2 - 25);
                lnkCopyright.Text = text3;
                picTitle.Visible = false;
                picTitle.Size = new Size(482, 276);
                int val = (num2 - (10 + picTitle.Height + 10 + pnlButtons.Height + 10 + lnkCopyright.Height + 10)) / 2;
                val = Math.Max(10, val);
                picTitle.Location = new Point((num - picTitle.Width) / 2, 70);
                string text4 = string.Empty;
                if (main_0.gameOptions_0 != null)
                {
                    text4 = main_0.gameOptions_0.CustomizationSetName;
                }
                resourceSystem_0 = Galaxy.InitializeResourceDefinitions(Application.StartupPath, text4);
                Galaxy.ResourceSystemStatic = resourceSystem_0;
                List<string> list = new List<string>();
                list.Add("(" + TextResolver.GetText("Random") + ")");
                raceList_1 = Galaxy.LoadRaces(Application.StartupPath, text4);
                Galaxy.SetResearchRaceSpecialProjects(raceList_1);
                Galaxy.SetResearchComponentMaxTechPoints(main_0.gameOptions_0.StartGameOptions.GalaxyResearchSpeed * 1000);
                raceList_0 = raceList_1.ResolvePlayableRaces();
                foreach (Race item in raceList_0)
                {
                    list.Add(item.Name);
                }
                list.Sort();
                cmbStartNewGameYourEmpireRace.BindData(font_0, raceList_0, null, allowRandomRace: true);
                raceList_0.Sort();
                ctlStartingEmpiresList.SetRaces(text4);
                ctlStartingEmpiresList.SetProximityValues(10, 10);
                Galaxy.FlagShapes = Galaxy.LoadFlagShapes(Application.StartupPath, text4);
                Galaxy.FlagShapesPirates = Galaxy.LoadFlagShapesPirates(Application.StartupPath, text4);
                encyclopediaItemList_0 = main_0.method_465(null, Application.StartupPath, text4);
                encyclopediaItemList_1 = new EncyclopediaItemList();
                int_0 = 0;
                string folderPath = Application.StartupPath + "\\maps\\";
                if (!string.IsNullOrEmpty(text4))
                {
                    folderPath = Application.StartupPath + "\\Customization\\" + text4 + "\\maps\\";
                }
                galaxySummaryList_0.LoadFromFolder(folderPath);
                lblStartNewGameGalaxyMapsAvailableGalaxies.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableGalaxies"), "(" + TextResolver.GetText("Default") + ")");
                ctlStartNewGameGalaxyMapsGalaxies.SelectionChanged += ctlStartNewGameGalaxyMapsGalaxies_SelectionChanged;
                ctlStartNewGameGalaxyMapsEmpires.SelectionChanged += ctlStartNewGameGalaxyMapsEmpires_SelectionChanged;
                cmbPrimaryColor.Ignite();
                cmbSecondaryColor.Ignite(allowWhite: true, allowBlack: false, useDarkerPalette: false, Color.Empty);
                List<string> list2 = new List<string>();
                for (int i = 0; i < Galaxy.FlagShapes.Count; i++)
                {
                    list2.Add(" ");
                }
                cmbFlagShape.Items.AddRange(list2.ToArray());
                ResumeLayout();
                //splash.Close();
                //Class5._Splash.Close();
                Class5._Splash.Stop();
                method_142(bool_5: false);
                base.Visible = true;
                method_1(text4);
                Cursor.Current = Cursors.Default;
                string text5 = Application.StartupPath + "\\sounds\\effects\\";
                string text6 = string.Empty;
                if (!string.IsNullOrEmpty(text4))
                {
                    text6 = Application.StartupPath + "\\Customization\\" + text4 + "\\sounds\\effects\\";
                }
                if (!string.IsNullOrEmpty(text6) && File.Exists(text6 + "button1.wav"))
                {
                    CloseButton.SetSoundLocation(text6 + "button1.wav");
                }
                else
                {
                    CloseButton.SetSoundLocation(text5 + "button1.wav");
                }
                method_6(text5, text6, this);
                method_5(text5, text6);
                ctlStartingEmpiresList.SoundsEnabled = true;
                SetErrorMode(ErrorModes.SEM_FAILCRITICALERRORS);
                if (flag2)
                {
                    Cursor.Hide();
                    string_0 = new string[3]
                    {
                    Application.StartupPath + "\\movies\\matrixintro.wmv",
                    Application.StartupPath + "\\movies\\codeforceintro.wmv",
                    Application.StartupPath + "\\movies\\distantworldsintro.wmv"
                    };
                    method_141(string_0);
                    /*this.mediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.mediaPlayer_PlayStateChange);
                    this.mediaPlayer.MediaError += new AxWMPLib._WMPOCXEvents_MediaErrorEventHandler(this.mediaPlayer_MediaError);
                    this.mediaPlayer.MouseDownEvent += new AxWMPLib._WMPOCXEvents_MouseDownEventHandler(this.mediaPlayer_MouseDownEvent);*/
                    method_143();
                }
                oyxRtRyAwjg.DoWork += gameStartBackgroundWorker_DoWork;
                oyxRtRyAwjg.RunWorkerCompleted += oyxRtRyAwjg_RunWorkerCompleted;
                main_0.SendToBack();
                main_0.Visible = true;
                main_0.Visible = false;
                if (main_0 != null && main_0.gameOptions_0 != null && main_0.gameOptions_0.ShowEncyclopediaAtStart)
                {
                    method_127(TextResolver.GetText("Introduction"));
                }
                if (flag2)
                {
                    //((Control)(object)mediaPlayer).BringToFront();
                }
                else
                {
                    method_143();
                }
            }
            catch (Exception ex)
            {
                Main.CrashDump(ex);
                throw;
            }
        }

        private void method_1(string string_2)
        {
            string text = TextResolver.GetText("Current Theme") + ": " + string_2;
            if (string.IsNullOrEmpty(string_2))
            {
                text = string.Empty;
            }
            using (Graphics graphics = lblActiveTheme.CreateGraphics())
            {
                SizeF sizeF = graphics.MeasureString(text, font_7);
                int num = (base.Width - (int)sizeF.Width) / 2;
                lblActiveTheme.Location = new Point(num, lblActiveTheme.Location.Y);
                lblActiveTheme.Text = text;
            }
            using Graphics graphics2 = lblStartNewGameActiveTheme.CreateGraphics();
            int num2 = (900 - (int)graphics2.MeasureString(text, font_6).Width) / 2;
            lblStartNewGameActiveTheme.Location = new Point(num2, 620);
            lblStartNewGameActiveTheme.Text = text;
        }

        private void method_2(string string_2, bool bool_5, bool bool_6)
        {
            if (main_0 == null)
            {
                return;
            }
            bool flag = main_0.mainView.bool_0;
            main_0.mainView.bool_0 = false;
            if (bool_5)
            {
                if (string_2 == "(Default)")
                {
                    string_2 = string.Empty;
                }
                main_0.gameOptions_0.CustomizationSetName = string_2;
            }
            main_0.string_3 = string_2;
            if (string.IsNullOrEmpty(string_2))
            {
                string text = Application.StartupPath + "\\GameText.txt";
                if (File.Exists(text))
                {
                    TextResolver.LoadText(text);
                }
            }
            else
            {
                string text2 = Application.StartupPath + "\\Customization\\" + string_2 + "\\GameText.txt";
                if (File.Exists(text2))
                {
                    TextResolver.LoadText(text2);
                }
            }
            ResourceSystem resourceSystem = null;
            Galaxy.InitializeData(Application.StartupPath, string_2, out resourceSystem);
            resourceSystem_0 = Galaxy.ResourceSystemStatic;
            string string_3 = Application.StartupPath + "\\images\\";
            string string_4 = Application.StartupPath + "\\Customization\\" + string_2 + "\\images\\";
            main_0.bitmap_6 = new Bitmap[22];
            main_0.int_5 = new int[22];
            main_0.bitmap_7 = new Bitmap[22];
            main_0.list_2 = new List<Rectangle>[22];
            main_0.bitmap_23 = new Bitmap[20];
            main_0.bitmap_24 = new Bitmap[20];
            main_0.bitmap_25 = new Bitmap[20];
            main_0.bitmap_26 = new Bitmap[20];
            main_0.bitmap_27 = new Bitmap[20];
            main_0.habitatImageCache_0.Initialize(Application.StartupPath, string_2, initialLoad: false);
            main_0.builtObjectImageCache_0.Initialize(Application.StartupPath, string_2);
            Galaxy.FlagShapes = Galaxy.LoadFlagShapes(Application.StartupPath, string_2);
            Galaxy.FlagShapesPirates = Galaxy.LoadFlagShapesPirates(Application.StartupPath, string_2);
            Parallel.Invoke(() => main_0.LoadFighters(string_3, string_4, 0.5),
                () => main_0.LoadRacesImg(string_3, string_4, string_2),
                () => main_0.LoadTroops(string_3, string_4, string_2),
                () => main_0.LoadUiResources(string_3, string_4),
                () => main_0.LoadUiComponents(string_3, string_4),
                () => main_0.LoadUiChrome(string_3, string_2),
                () => main_0.LoadUiCursors(string_3, string_4),
                () => main_0.LoadEffectsWeapons(string_3, string_4),
                () => main_0.LoadPlanetaryFacilities(string_3, string_4),
                () => main_0.LoadRuins(string_3, string_4));
            main_0.string_27 = string.Empty;
            bool bool_7 = false;
            if (string.IsNullOrEmpty(string_2))
            {
                bool_7 = true;
            }
            double double_ = 0.5;
            Parallel.Invoke(() => main_0.LoadStars(string_3, string_4, double_, bool_7),
                () => main_0.LoadHyperEffects(string_3, string_4, double_),
                () => main_0.LoadEffects(string_3, string_4, double_),
                () => main_0.LoadEnvironmentOverlays(string_3, string_4),
                () => main_0.LoadEffectsExplosion(string_3, string_4, double_, bool_7),
                () => main_0.LoadUiMessages(string_3, string_4),
                () => main_0.LoadUiEvents(string_3, string_4),
                () => main_0.LoadUiPlagues(string_3, string_4),
                () => main_0.LoadEnvLandscapes(string_3, string_4),
                () => main_0.LoadUiAchievements(string_3, string_4),
                () => main_0.LoadUiShipsymbols(string_3, string_4),
                () => main_0.LoadEnvGalaxybackdrops(string_3, string_4)
            );
            main_0.mainView.ResetRendering();
            encyclopediaItemList_0 = main_0.method_465(null, Application.StartupPath, string_2);
            encyclopediaItemList_1 = new EncyclopediaItemList();
            int_0 = 0;
            main_0.encyclopediaItemList_0 = main_0.method_465(null, Application.StartupPath, string_2);
            main_0.vTtmruAejE = new EncyclopediaItemList();
            main_0.int_25 = 0;
            main_0.subRoleNameSet_0 = Galaxy.LoadShipNames(Application.StartupPath, string_2);
            main_0.list_1 = Galaxy.LoadColonyNames(Application.StartupPath, string_2);
            raceList_1 = Galaxy.LoadRaces(Application.StartupPath, string_2);
            raceList_0 = raceList_1.ResolvePlayableRaces();
            Galaxy.SetResearchRaceSpecialProjects(raceList_1);
            method_3(string_2);
            main_0.EffectsPlayer.Initialize(Application.StartupPath, string_2);
            string text3 = Application.StartupPath + "\\sounds\\effects\\";
            string text4 = string.Empty;
            if (!string.IsNullOrEmpty(string_2))
            {
                text4 = Application.StartupPath + "\\Customization\\" + string_2 + "\\sounds\\effects\\";
            }
            if (!string.IsNullOrEmpty(text4) && File.Exists(text4 + "button1.wav"))
            {
                CloseButton.SetSoundLocation(text4 + "button1.wav");
            }
            else
            {
                CloseButton.SetSoundLocation(text3 + "button1.wav");
            }
            method_6(text3, text4, this);
            method_5(text3, text4);
            Invoke(delegate9_0, string_2, bool_6);
            main_0.method_65(200);
            main_0.mainView.bool_0 = flag;
        }

        private void method_3(string string_2)
        {
            string folderPath = Application.StartupPath + "\\maps\\";
            if (!string.IsNullOrEmpty(string_2))
            {
                folderPath = Application.StartupPath + "\\Customization\\" + string_2 + "\\maps\\";
            }
            galaxySummaryList_0.Clear();
            galaxySummaryList_0.LoadFromFolder(folderPath);
        }

        private void method_4(string string_2, bool bool_5)
        {
            lblStartNewGameGalaxyMapsAvailableGalaxies.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableGalaxies"), string_2);
            pnlStartNewGameYourEmpireRaceAttributes.BindData(null, font_3, font_7);
            picStartNewGameYourEmpireRace.Image = null;
            if (bool_5)
            {
                cmbStartNewGameYourEmpireRace.BindData(font_0, raceList_0, null, allowRandomRace: true);
                cmbJumpStartYourEmpireRace.BindData(font_0, raceList_0, null, allowRandomRace: true);
                raceList_0.Sort();
                ctlStartingEmpiresList.SetRaces(string_2);
                ctlStartingEmpiresList.SetProximityValues(10, 10);
            }
            main_0.method_72(string_2);
            method_1(string_2);
            main_0.method_67(string_2);
        }

        private void method_5(string string_2, string string_3)
        {
            string text = string_2 + "button2.wav";
            if (!string.IsNullOrEmpty(string_3) && File.Exists(string_3 + "button2.wav"))
            {
                text = string_3 + "button2.wav";
            }
            string_1 = text;
        }

        private void method_6(string string_2, string string_3, Control control_0)
        {
            if (control_0 is GlassButton)
            {
                if (!string.IsNullOrEmpty(string_3) && File.Exists(string_3 + "button1.wav"))
                {
                    GlassButton.SetSoundLocation(string_3 + "button1.wav");
                }
                else
                {
                    GlassButton.SetSoundLocation(string_2 + "button1.wav");
                }
            }
            if (control_0 is HoverButton)
            {
                if (!string.IsNullOrEmpty(string_3) && File.Exists(string_3 + "button2.wav"))
                {
                    HoverButton.SetSoundLocation(string_3 + "button2.wav");
                }
                else
                {
                    HoverButton.SetSoundLocation(string_2 + "button2.wav");
                }
            }
            if (control_0 is ListViewBase)
            {
                if (!string.IsNullOrEmpty(string_3) && File.Exists(string_3 + "grid.wav"))
                {
                    ListViewBase.SetSoundLocation(string_3 + "grid.wav");
                }
                else
                {
                    ListViewBase.SetSoundLocation(string_2 + "grid.wav");
                }
            }
            if (control_0 is HoverMenuItem)
            {
                if (!string.IsNullOrEmpty(string_3) && File.Exists(string_3 + "button2.wav"))
                {
                    HoverMenuItem.SetSoundLocation(string_3 + "button2.wav");
                }
                else
                {
                    HoverMenuItem.SetSoundLocation(string_2 + "button2.wav");
                }
            }
            if (control_0.Controls == null)
            {
                return;
            }
            foreach (Control control in control_0.Controls)
            {
                method_6(string_2, string_3, control);
            }
        }

        private void lnkNewGame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_31("");
        }

        private ICryptoTransform CreateDecryptor(byte[] key, byte[] iv) {
            var rijndael = Aes.Create();
            rijndael.KeySize = 128;
            rijndael.BlockSize = 128;
            rijndael.Padding = PaddingMode.Zeros;
            rijndael.Mode = CipherMode.CBC;
            return rijndael.CreateDecryptor(key, iv);
        }

        private void method_8(string string_2)
        {
            pnlSaveLoadProgress.Size = new Size(320, 330);
            pnlSaveLoadProgress.Location = new Point((base.Width - pnlSaveLoadProgress.Width) / 2, (base.Height - pnlSaveLoadProgress.Height) / 2);
            pnlSaveLoadProgress.Font = font_7;
            Point location = new Point((pnlSaveLoadProgress.Width - bitmap_0.Width) / 2, (pnlSaveLoadProgress.Height - bitmap_0.Height) / 2 + 15);
            picSaveLoadGalaxy.Size = new Size(bitmap_0.Width, bitmap_0.Height);
            picSaveLoadGalaxy.Location = location;
            picSaveLoadGalaxy.BringToFront();
            Graphics graphics = lblSaveLoadMessage.CreateGraphics();
            int num = (int)graphics.MeasureString(string_2, pnlSaveLoadProgress.Font, 320, StringFormat.GenericTypographic).Width;
            int num2 = (int)graphics.MeasureString(string_2, pnlSaveLoadProgress.Font, 320, StringFormat.GenericTypographic).Height;
            lblSaveLoadMessage.Size = new Size(num + 2, num2 + 2);
            lblSaveLoadMessage.Location = new Point((pnlSaveLoadProgress.Width - num) / 2, 13);
            lblSaveLoadMessage.Font = font_7;
            lblSaveLoadMessage.ForeColor = Color.White;
            lblSaveLoadMessage.BackColor = Color.Transparent;
            lblSaveLoadMessage.Text = string_2;
            lblSaveLoadMessage.BringToFront();
            pnlSaveLoadProgress.BringToFront();
            pnlSaveLoadProgress.Visible = true;
            pnlSaveLoadProgress.Update();
            method_17(pnlSaveLoadProgress);
        }

        private void method_9()
        {
            method_19();
            pnlSaveLoadProgress.Visible = false;
        }

        private void method_10()
        {
            while (!bool_0)
            {
                Application.UseWaitCursor = true;
                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                Thread.Sleep(30);
            }
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
        }

        private void method_11(object object_0)
        {
            try
            {
                GC.Collect();
                string string_ = string.Empty;
                Stream stream_ = null;
                if (object_0 is List<object>)
                {
                    List<object> list = (List<object>)object_0;
                    if (list[0] is string)
                    {
                        string_ = (string)list[0];
                    }
                    if (list[1] is Stream)
                    {
                        stream_ = (Stream)list[1];
                    }
                }
                List<object> list2 = new List<object>();
                list2 = LoadFromFile(string_, stream_, bool_5: true);
                method_14(list2);
            }
            catch (SerializationException)
            {
                string text = TextResolver.GetText("This is not a valid Distant Worlds game file");
                MessageBox.Show(text, TextResolver.GetText("Cannot load file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_12();
            }
            catch (OutOfMemoryException)
            {
                string text2 = "There was not enough memory to load this Distant Worlds game. Please close all other open applications and try again.";
                text2 = text2 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                MessageBox.Show(text2, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_12();
            }
            catch (Exception ex3)
            {
                string text3 = "Distant Worlds could not load this game.";
                text3 += "\n\nError:\n\n";
                text3 += ex3.ToString();
                text3 = text3 + "\n\nWorking Set: " + Environment.WorkingSet + " bytes";
                MessageBox.Show(text3, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                method_12();
            }
            finally
            {
                bool_0 = true;
            }
        }

        private void method_12()
        {
            RecoverFromLoadErrorDelegate method = method_13;
            Invoke(method);
        }

        private void method_13()
        {
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
            method_9();
            base.Enabled = true;
            Show();
            main_0.Visible = false;
            bool_1 = true;
        }

        private List<object> LoadFromFile(string filePath, Stream fileStream, bool bool_5)
        {
            //IL_015e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0165: Expected O, but got Unknown
            List<object> list = new List<object>();
            GalaxySummary galaxySummary = GalaxySummary.ReadGalaxySummary(fileStream);
            string text = galaxySummary.ThemeName;
            string text2 = main_0.string_3;
            if (string.IsNullOrEmpty(text))
            {
                text = string.Empty;
            }
            if (string.IsNullOrEmpty(text2))
            {
                text2 = string.Empty;
            }
            if (bool_5 && text != text2 && delegate8_0 != null)
            {
                Application.DoEvents();
                string text3 = galaxySummary.ThemeName;
                if (string.IsNullOrEmpty(text3))
                {
                    text3 = "(" + TextResolver.GetText("Default") + ")";
                }
                Invoke(delegate10_0, string.Format(TextResolver.GetText("Switching to THEMENAME theme"), text3));
                Application.DoEvents();
                Invoke(delegate8_0, galaxySummary.ThemeName, true, true);
                Application.DoEvents();
                Invoke(delegate10_0, TextResolver.GetText("Loading the Galaxy..."));
                Application.DoEvents();
            }
            CompactSerializer compactSerializer = new CompactSerializer(typeof(Game), main_0.method_358());
            ICryptoTransform transform = CreateDecryptor(Main.byte_0, Main.byte_1);
            CryptoStream cryptoStream = new CryptoStream(fileStream, transform, CryptoStreamMode.Read);
            DeflateStream val = new DeflateStream(cryptoStream, (CompressionMode)1, (CompressionLevel)1, true);
            val.BufferSize = 4194304;
            XmlDictionaryReaderQuotas max = XmlDictionaryReaderQuotas.Max;
            XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateBinaryReader(val, max);
            Game item = (Game)compactSerializer.ReadObject(xmlDictionaryReader);
            Main._ExpModMain.FixAllDesignRepairTemplates(item, false);
            xmlDictionaryReader.Close();
            val.Close();
            cryptoStream.Close();
            fileStream.Close();
            list.Add(filePath);
            list.Add(item);
            return list;
        }

        private void method_14(object object_0)
        {
            string string_ = string.Empty;
            Game game = null;
            if (object_0 is List<object>)
            {
                List<object> list = (List<object>)object_0;
                if (list[0] is string)
                {
                    string_ = (string)list[0];
                }
                if (list[1] is Game)
                {
                    game = (Game)list[1];
                }
            }
            Galaxy.SetGalaxyPhysicalDimensions(game.Galaxy.SectorWidth, game.Galaxy.SectorHeight);
            Galaxy.AssignGalaxyDataToStatic(game.Galaxy.ResourceSystem, game.Galaxy.PlanetaryFacilityDefinitions, game.Galaxy.ComponentDefinitions, game.Galaxy.FighterSpecifications, game.Galaxy.ResearchNodeDefinitions, game.Galaxy.Governments, game.Galaxy.RaceFamilies, game.Galaxy.Plagues);
            Galaxy.SetResearchRaceSpecialProjects(game.Galaxy.Races);
            Galaxy.SetResearchComponentMaxTechPoints(game.Galaxy.BaseTechCost);
            game.Galaxy.RebuildIndexes();
            game.Galaxy.IndependentEmpire.SetPirateRelationEmpires(game.Galaxy);
            game.Galaxy.IndependentEmpire.UpdateEmpireRefuellingLocations();
            foreach (Empire empire in game.Galaxy.Empires)
            {
                empire.SetDefaultsForLists();
                empire.ExtendLatestDesignsWithNewSubRoles();
                empire.SetPirateRelationEmpires(game.Galaxy);
                empire.EvaluateSystemLinks();
                empire.UpdateSystemFuelSourceStatus();
                empire.UpdateEmpireRefuellingLocations();
                empire.ReviewBuiltObjectWeaponsComponentValues();
                empire.ReviewUnpersistedColonyData();
            }
            foreach (Empire pirateEmpire in game.Galaxy.PirateEmpires)
            {
                pirateEmpire.SetDefaultsForLists();
                pirateEmpire.ExtendLatestDesignsWithNewSubRoles();
                pirateEmpire.SetPirateRelationEmpires(game.Galaxy);
                pirateEmpire.UpdateSystemFuelSourceStatus();
                pirateEmpire.UpdateEmpireRefuellingLocations();
                pirateEmpire.ReviewBuiltObjectWeaponsComponentValues();
                pirateEmpire.ReviewUnpersistedColonyData();
            }
            Cursor.Current = Cursors.Default;
            main_0._Game = game;
            main_0.string_2 = string_;
            main_0._Game.Galaxy.ApplicationStartupPath = Application.StartupPath;
            main_0._Game.Galaxy.CustomizationSetPath = main_0.GetCustomizationPath();
        }

        private void method_15(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                if (e.Error is SerializationException)
                {
                    _ = (SerializationException)e.Error;
                    string text = TextResolver.GetText("This is not a valid Distant Worlds game file");
                    MessageBox.Show(text, TextResolver.GetText("Cannot load file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cursor.Current = Cursors.Default;
                    method_9();
                    base.Enabled = true;
                    Show();
                    main_0.Visible = false;
                }
                else
                {
                    _ = e.Error;
                    string text2 = "Distant Worlds could not load this game.";
                    MessageBox.Show(text2, "Cannot load file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Cursor.Current = Cursors.Default;
                    method_9();
                    base.Enabled = true;
                    Show();
                    main_0.Visible = false;
                }
                return;
            }
            string currentFileName = string.Empty;
            Game game = null;
            if (e.Result is List<object>)
            {
                List<object> list = (List<object>)e.Result;
                if (list[0] is string)
                {
                    currentFileName = (string)list[0];
                }
                if (list[1] is Game)
                {
                    game = (Game)list[1];
                }
            }
            Galaxy.SetGalaxyPhysicalDimensions(game.Galaxy.SectorWidth, game.Galaxy.SectorHeight);
            Galaxy.AssignGalaxyDataToStatic(game.Galaxy.ResourceSystem, game.Galaxy.PlanetaryFacilityDefinitions, game.Galaxy.ComponentDefinitions, game.Galaxy.FighterSpecifications, game.Galaxy.ResearchNodeDefinitions, game.Galaxy.Governments, game.Galaxy.RaceFamilies, game.Galaxy.Plagues);
            game.Galaxy.RebuildIndexes();
            game.Galaxy.IndependentEmpire.SetPirateRelationEmpires(game.Galaxy);
            game.Galaxy.IndependentEmpire.UpdateEmpireRefuellingLocations();
            foreach (Empire empire in game.Galaxy.Empires)
            {
                empire.SetDefaultsForLists();
                empire.ExtendLatestDesignsWithNewSubRoles();
                empire.SetPirateRelationEmpires(game.Galaxy);
                empire.EvaluateSystemLinks();
                empire.UpdateSystemFuelSourceStatus();
                empire.UpdateEmpireRefuellingLocations();
                empire.ReviewBuiltObjectWeaponsComponentValues();
                empire.ReviewUnpersistedColonyData();
            }
            foreach (Empire pirateEmpire in game.Galaxy.PirateEmpires)
            {
                pirateEmpire.SetDefaultsForLists();
                pirateEmpire.ExtendLatestDesignsWithNewSubRoles();
                pirateEmpire.SetPirateRelationEmpires(game.Galaxy);
                pirateEmpire.UpdateSystemFuelSourceStatus();
                pirateEmpire.UpdateEmpireRefuellingLocations();
                pirateEmpire.ReviewBuiltObjectWeaponsComponentValues();
                pirateEmpire.ReviewUnpersistedColonyData();
            }
            Cursor.Current = Cursors.Default;
            main_0.Ignite(game, currentFileName);
            method_9();
            base.Enabled = true;
            main_0.Visible = true;
            main_0.ProcessMain(game.Galaxy.CurrentDateTime, game.Galaxy.CurrentStarDate, null);
            main_0.mainView.Refresh();
            Hide();
            main_0.Launch(launchFromLoad: true);
            Galaxy.SetResearchRaceSpecialProjects(raceList_1);
            Galaxy.SetResearchComponentMaxTechPoints(game.Galaxy.BaseTechCost);
            method_1(main_0.string_3);
            Show();
            main_0.Visible = false;
            game_0 = null;
        }

        private void gameStartBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker_ = sender as BackgroundWorker;
            GalaxyShape galaxyShape_ = GalaxyShape.Elliptical;
            int int_ = 0;
            int int_2 = 0;
            bool bool_ = true;
            double double_ = 1.0;
            int int_3 = 500;
            double double_2 = 0.0;
            double double_3 = 0.0;
            int int_4 = 0;
            double double_4 = 1.0;
            double double_5 = 1.0;
            int int_5 = method_57(TextResolver.GetText("Starting"));
            double double_6 = 1.0;
            EmpireStart empireStart_ = null;
            EmpireStartList empireStartList_ = null;
            VictoryConditions victoryConditions_ = null;
            EmpireVictoryConditions empireVictoryConditions_ = null;
            EmpireVictoryConditions empireVictoryConditions_2 = null;
            bool bool_2 = false;
            bool bool_3 = true;
            GameStartResets gameStartResets_ = null;
            if (!(e.Argument is List<object>))
            {
                throw new ApplicationException("Invalid New Game worker event args");
            }
            List<object> list = (List<object>)e.Argument;
            if (list[0] is GalaxyShape)
            {
                galaxyShape_ = (GalaxyShape)list[0];
            }
            if (list[1] is int)
            {
                int_ = (int)list[1];
            }
            if (list[2] is int)
            {
                int_2 = (int)list[2];
            }
            if (list[3] is bool)
            {
                bool_ = (bool)list[3];
            }
            if (list[4] is double)
            {
                double_ = (double)list[4];
            }
            if (list[5] is int)
            {
                int_3 = (int)list[5];
            }
            if (list[6] is double)
            {
                double_2 = (double)list[6];
            }
            if (list[7] is double)
            {
                double_3 = (double)list[7];
            }
            if (list[8] is double)
            {
                double_4 = (double)list[8];
            }
            if (list[9] is int)
            {
                int_5 = (int)list[9];
            }
            if (list[10] is double)
            {
                double_6 = (double)list[10];
            }
            if (list[11] is EmpireStart)
            {
                empireStart_ = (EmpireStart)list[11];
            }
            if (list[12] is EmpireStartList)
            {
                empireStartList_ = (EmpireStartList)list[12];
            }
            if (list[13] is VictoryConditions)
            {
                victoryConditions_ = (VictoryConditions)list[13];
            }
            if (list[14] is EmpireVictoryConditions)
            {
                empireVictoryConditions_ = (EmpireVictoryConditions)list[14];
            }
            if (list[15] is EmpireVictoryConditions)
            {
                empireVictoryConditions_2 = (EmpireVictoryConditions)list[15];
            }
            if (list[16] is bool)
            {
                bool_2 = (bool)list[16];
            }
            if (list[17] is int)
            {
                int_4 = (int)list[17];
            }
            if (list[18] is double)
            {
                double_5 = (double)list[18];
            }
            if (list[19] is bool)
            {
                bool_3 = (bool)list[19];
            }
            if (list[20] is GameStartResets)
            {
                gameStartResets_ = (GameStartResets)list[20];
            }
            List<object> list2 = new List<object>();
            list2 = (List<object>)(e.Result = GetGame(backgroundWorker_, galaxyShape_, int_, int_2, bool_, double_, int_3, double_2, double_3, int_4, double_4, double_5, int_5, double_6, empireStart_, empireStartList_, victoryConditions_, empireVictoryConditions_, empireVictoryConditions_2, bool_2, bool_3, gameStartResets_));
        }

        private List<object> GetGame(BackgroundWorker backgroundWorker_0, GalaxyShape galaxyShape_0, int int_1, int int_2, bool bool_5, double double_1, int int_3, double double_2, double double_3, int int_4, double double_4, double double_5, int int_5, double double_6, EmpireStart empireStart_0, EmpireStartList empireStartList_0, VictoryConditions victoryConditions_0, EmpireVictoryConditions empireVictoryConditions_0, EmpireVictoryConditions empireVictoryConditions_1, bool bool_6, bool bool_7, GameStartResets gameStartResets_0)
        {
            if (!backgroundWorker_0.CancellationPending)
            {
                List<object> list = new List<object>();
                Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                Game game = CreateGameFromSettings(galaxyShape_0, int_1, int_2, bool_5, double_1, int_3, double_2, double_3, int_4, double_4, double_5, int_5, double_6, empireStart_0, empireStartList_0, victoryConditions_0, empireVictoryConditions_0, empireVictoryConditions_1, bool_6, bool_7, gameStartResets_0);
                list.Add(BaconMain.OverrideGalaxySetup(this, game));
                Main._ExpModMain.FixAllDesignRepairTemplates(game, false);
                return list;
            }
            return null;
        }

        private void method_17(System.Windows.Forms.Panel panel_1)
        {
            if (Environment.ProcessorCount > 1)
            {
                bitmap_14 = PrecacheScaledBitmap(bitmap_0, 270, 270);
                panel_0 = panel_1;
                size_0 = picSaveLoadGalaxy.Size;
                float_0 = (float)Math.PI * 2f;
                timer_0.AutoReset = false;
                timer_0.Interval = 75.0;
                timer_0.Start();
            }
            else
            {
                picSaveLoadGalaxy.Image = bitmap_0;
            }
        }

        private void timer_0_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer_0.Stop();
            if (panel_0 != null)
            {
                float_0 -= (float)Math.PI / 160f;
                Bitmap image = method_21(bitmap_14, float_0, bool_5: false);
                image = CropImageToSize(image, size_0);
                Invoke(delegate7_0, image);
                Application.DoEvents();
            }
            timer_0.Start();
        }

        private void method_18(Bitmap bitmap_15)
        {
            Image image = picSaveLoadGalaxy.Image;
            picSaveLoadGalaxy.SizeMode = PictureBoxSizeMode.CenterImage;
            picSaveLoadGalaxy.Image = bitmap_15;
            image?.Dispose();
        }

        private void method_19()
        {
            timer_0.Stop();
            panel_0 = null;
            picSaveLoadGalaxy.Image = null;
        }

        public Bitmap CropImageToSize(Bitmap image, Size size)
        {
            if (image.Width <= size.Width && image.Height <= size.Height)
            {
                return image;
            }
            int num = Math.Min(image.Width, size.Width);
            int num2 = Math.Min(image.Height, size.Height);
            Bitmap bitmap = new Bitmap(num, num2, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.Low;
            graphics.SmoothingMode = SmoothingMode.None;
            float num3 = (float)(num - image.Width) / 2f;
            float num4 = (float)(num2 - image.Height) / 2f;
            PointF point = new PointF(num3, num4);
            graphics.DrawImage(image, point);
            return bitmap;
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, double width, double height)
        {
            return PrecacheScaledBitmap(unscaledBitmap, (int)(width + 1.0), (int)(height + 1.0));
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height)
        {
            return PrecacheScaledBitmap(unscaledBitmap, width, height, InterpolationMode.HighQualityBilinear, CompositingQuality.HighQuality, SmoothingMode.AntiAlias);
        }

        public Bitmap PrecacheScaledBitmap(Bitmap unscaledBitmap, int width, int height, InterpolationMode interpolation, CompositingQuality compositing, SmoothingMode smoothing)
        {
            if (width < 1)
            {
                width = 1;
            }
            if (height < 1)
            {
                height = 1;
            }
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.InterpolationMode = interpolation;
            graphics.CompositingQuality = compositing;
            graphics.SmoothingMode = smoothing;
            graphics.DrawImage(unscaledBitmap, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        private Bitmap method_20(Image image_0, float float_2)
        {
            return method_21(image_0, float_2, bool_5: false);
        }

        private Bitmap method_21(Image image_0, float float_2, bool bool_5)
        {
            if (image_0 == null)
            {
                throw new ArgumentNullException("image");
            }
            float num = image_0.Width;
            float num2 = image_0.Height;
            float_2 *= -1f;
            float_2 *= 57.29578f;
            float_2 %= 360f;
            if ((double)float_2 < 0.0)
            {
                float_2 += 360f;
            }
            PointF[] array = new PointF[4];
            array[1].X = num;
            array[2].X = num;
            array[2].Y = num2;
            array[3].Y = num2;
            Matrix matrix = new Matrix();
            matrix.Rotate(float_2);
            matrix.TransformPoints(array);
            double num3 = double.MinValue;
            double num4 = double.MinValue;
            double num5 = double.MaxValue;
            double num6 = double.MaxValue;
            PointF[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                PointF pointF = array2[i];
                num3 = Math.Max(num3, pointF.X);
                num5 = Math.Min(num5, pointF.X);
                num4 = Math.Max(num4, pointF.Y);
                num6 = Math.Min(num6, pointF.Y);
            }
            double num7 = Math.Ceiling(num3 - num5);
            double num8 = Math.Ceiling(num4 - num6);
            Bitmap bitmap = new Bitmap((int)num7, (int)num8);
            using Graphics graphics = Graphics.FromImage(bitmap);
            if (bool_5)
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
            else
            {
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
            }
            PointF point = new PointF((float)(num7 / 2.0), (float)(num8 / 2.0));
            PointF point2 = new PointF(point.X - num / 2f, point.Y - num / 2f);
            matrix.Reset();
            matrix.RotateAt(float_2, point);
            graphics.Transform = matrix;
            graphics.DrawImage(image_0, point2);
            return bitmap;
        }

        private void oyxRtRyAwjg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            base.Enabled = true;
            if (e.Error != null)
            {
                _ = e.Error;
                string text = "Could not start this game.";
                MessageBox.Show(text, "Cannot start game", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Cursor.Current = Cursors.Default;
                method_9();
                base.Enabled = true;
                Show();
                main_0.Visible = false;
                game_0 = null;
                return;
            }
            Game game = null;
            if (e.Result is List<object>)
            {
                List<object> list = (List<object>)e.Result;
                if (list[0] is Game)
                {
                    game = (Game)list[0];
                }
            }
            game_0 = game;
            if (game == null && exception_0 != null)
            {
                string text2 = "There was an error while attempting to start a new game. ";
                text2 += "If you are using a theme, please revert to the default Distant Worlds theme and try again";
                text2 += "\n\nError Details:\n\n";
                text2 += exception_0.ToString();
                MessageBox.Show(text2, "Error while starting a new game", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void lnkLoadGame_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                BaconStart.LoadGame(this);
                method_149();
                new Game();
                string empty = string.Empty;
                bool flag = false;
                string text = Main.GetGameSavesFolderCreateIfNeeded();
                if (main_0 != null && main_0.gameOptions_0 != null && !string.IsNullOrEmpty(main_0.gameOptions_0.SaveGamePath))
                {
                    text = main_0.gameOptions_0.SaveGamePath;
                }
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = text;
                openFileDialog.Filter = TextResolver.GetText("Distant Worlds saved game files") + " (*.dwg)|*.dwg";
                openFileDialog.DefaultExt = "dwg";
                openFileDialog.Title = TextResolver.GetText("Load Distant Worlds game");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    flag = true;
                }
                if (flag)
                {
                    Stream stream_;
                    if ((stream_ = openFileDialog.OpenFile()) != null)
                    {
                        method_140();
                        empty = openFileDialog.FileName;
                        if (main_0 != null && main_0.gameOptions_0 != null)
                        {
                            DirectoryInfo directoryInfo = Directory.GetParent(empty);
                            if (directoryInfo != null)
                            {
                                main_0.gameOptions_0.SaveGamePath = directoryInfo.FullName;
                            }
                        }
                        openFileDialog.Dispose();
                        method_22(stream_, empty, TextResolver.GetText("Loading the Galaxy..."));
                    }
                    else
                    {
                        flag = false;
                        openFileDialog.Dispose();
                    }
                }
                else
                {
                    openFileDialog.Dispose();
                }
                if (!flag)
                {
                    Application.UseWaitCursor = false;
                    method_9();
                }
                else
                {
                    method_10();
                    Application.UseWaitCursor = false;
                    method_23();
                }
            }
            catch (Exception ex)
            {
                Main.CrashDump(ex);
                throw;
            }
        }

        private void method_22(Stream stream_0, string string_2, string string_3)
        {
            method_8(string_3);
            Application.DoEvents();
            base.Enabled = false;
            Application.UseWaitCursor = true;
            List<object> list = new List<object>();
            list.Add(string_2);
            list.Add(stream_0);
            bool_0 = false;
            thread_0 = new Thread(method_11, int.MaxValue);
            thread_0.Start(list);
            method_10();
            Application.UseWaitCursor = false;
        }

        private void method_23()
        {
            if (!bool_1)
            {
                if (!string.IsNullOrEmpty(main_0._Game.CustomizationSetName) && (string.IsNullOrEmpty(main_0.string_3) || main_0._Game.CustomizationSetName != main_0.string_3))
                {
                    main_0.method_66(main_0._Game.CustomizationSetName, bool_28: false);
                }
                else
                {
                    main_0.method_65(200);
                }
                main_0.method_56(Application.StartupPath + "\\images\\", main_0._Game.CustomizationSetName, main_0._Game.PlayerEmpire.DominantRace);
                main_0.Ignite();
                main_0.Visible = true;
                main_0.ProcessMain(main_0._Game.Galaxy.CurrentDateTime, main_0._Game.Galaxy.CurrentStarDate, null);
                main_0.mainView.Refresh();
                method_9();
                base.Enabled = true;
                Hide();
                main_0.Launch(launchFromLoad: true);
                Galaxy.SetResearchRaceSpecialProjects(raceList_1);
                if (main_0._Game != null && main_0.gameOptions_0 != null && main_0.gameOptions_0.StartGameOptions != null)
                {
                    Galaxy.SetResearchComponentMaxTechPoints(main_0.gameOptions_0.StartGameOptions.GalaxyResearchSpeed * 1000);
                }
                method_3(main_0.string_3);
                method_1(main_0.string_3);
                Show();
                main_0.Visible = false;
                game_0 = null;
            }
            bool_1 = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            lnkExit_LinkClicked(this, null);
            base.OnClosing(e);
        }

        private void lnkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method_149();
            method_140();
            if (main_0.musicPlayer_0 != null && main_0.musicPlayer_1 != null)
            {
                main_0.musicPlayer_0.Stop();
                main_0.musicPlayer_1.Stop();
            }
            string filename = Main.GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
            MessageBoxExManager.WriteSavedResponses(filename);
            if (main_0 != null)
            {
                main_0.method_257();
                main_0.method_255();
            }
            ToggleScreenSaverActive(active: true);
            if (!main_0.bool_2)
            {
                main_0.method_374();
            }
            Environment.Exit(-1);
        }

        private void method_24()
        {
            pnlQuickStart.Size = new Size(745, 470);
            pnlQuickStart.Location = new Point((base.Width - pnlQuickStart.Width) / 2, (base.Height - pnlQuickStart.Height) / 2);
            pnlQuickStart.DoLayout();
            pnlQuickStartDescription.Size = new Size(460, 350);
            pnlQuickStartDescription.Location = new Point(260, 10);
            lblQuickStartDescriptionTitle.Font = font_7;
            lblQuickStartDescriptionTitle.Location = new Point(10, 10);
            lblQuickStartDescriptionTitle.MaximumSize = new Size(440, 20);
            lblQuickStartDescriptionDetail.Font = font_3;
            lblQuickStartDescriptionDetail.Location = new Point(10, 40);
            lblQuickStartDescriptionDetail.MaximumSize = new Size(440, 290);
            lblQuickStartRace.Font = font_3;
            lblQuickStartRace.Location = new Point(10, 312);
            cmbQuickStartRace.Location = new Point(47, 305);
            cmbQuickStartRace.Size = new Size(135, 30);
            cmbQuickStartRace.ItemHeight = 30;
            cmbQuickStartRace.BindData(font_3, raceList_0, main_0.raceImageCache_0.GetRaceImages(), allowRandomRace: true);
            cmbQuickStartRace.SelectedIndex = 0;
            cmbQuickStartRace.BringToFront();
            lnkQuickStartRaceHelp.Location = new Point(185, 308);
            lnkQuickStartRaceHelp.MaximumSize = new Size(60, 70);
            lnkQuickStartRaceHelp.Text = TextResolver.GetText("About this Race") + "...";
            chkQuickStartDistantWorldsStoryEvents.Font = font_3;
            chkQuickStartDistantWorldsStoryEvents.Location = new Point(247, 302);
            chkQuickStartDistantWorldsStoryEvents.MaximumSize = new Size(210, 50);
            chkQuickStartDistantWorldsStoryEvents.CheckAlign = ContentAlignment.MiddleRight;
            chkQuickStartDistantWorldsStoryEvents.Text = TextResolver.GetText("Distant Worlds original storyline");
            chkQuickStartReturnOfTheShakturiStoryEvents.Font = font_3;
            chkQuickStartReturnOfTheShakturiStoryEvents.Location = new Point(250, 322);
            chkQuickStartReturnOfTheShakturiStoryEvents.MaximumSize = new Size(200, 50);
            chkQuickStartReturnOfTheShakturiStoryEvents.CheckAlign = ContentAlignment.MiddleRight;
            chkQuickStartReturnOfTheShakturiStoryEvents.Text = TextResolver.GetText("Return Of The Shakturi storyline");
            chkQuickStartDistantWorldsStoryEvents.Checked = true;
            chkQuickStartReturnOfTheShakturiStoryEvents.Checked = true;
            radioRandom.Location = new Point(10, 10);
            radioSmall.Location = new Point(10, 35);
            radioEpic.Location = new Point(10, 60);
            radioRingRace.Location = new Point(10, 85);
            radioConflict.Location = new Point(10, 110);
            radioExpandingSettlements.Location = new Point(10, 135);
            radioExpandingFromTheCore.Location = new Point(10, 160);
            radioFullyDevelopedSmall.Location = new Point(10, 185);
            radioFullyDevelopedStandard.Location = new Point(10, 210);
            radioFullyDevelopedLarge.Location = new Point(10, 235);
            radioGalacticRepublicSupremeRuler.Location = new Point(10, 260);
            radioGalacticRepublicWildFrontiers.Location = new Point(10, 285);
            radioSovereignTerritoriesRegionalRuler.Location = new Point(10, 310);
            radioSovereignTerritoriesMinorFaction.Location = new Point(10, 335);
            radioRandom.Font = font_3;
            radioSmall.Font = font_3;
            radioEpic.Font = font_3;
            radioRingRace.Font = font_3;
            radioConflict.Font = font_3;
            radioExpandingSettlements.Font = font_3;
            radioExpandingFromTheCore.Font = font_3;
            radioFullyDevelopedSmall.Font = font_3;
            radioFullyDevelopedStandard.Font = font_3;
            radioFullyDevelopedLarge.Font = font_3;
            radioGalacticRepublicSupremeRuler.Font = font_3;
            radioGalacticRepublicWildFrontiers.Font = font_3;
            radioSovereignTerritoriesRegionalRuler.Font = font_3;
            radioSovereignTerritoriesMinorFaction.Font = font_3;
            radioGalacticRepublicSupremeRuler.SendToBack();
            radioGalacticRepublicWildFrontiers.SendToBack();
            btnQuickStart.Size = new Size(280, 30);
            btnQuickStartCancel.Size = new Size(170, 30);
            btnQuickStartCancel.Location = new Point(260, 370);
            btnQuickStart.Location = new Point(440, 370);
            btnQuickStart.Font = font_7;
            btnQuickStartCancel.Font = font_7;
            pnlQuickStart.Visible = true;
            pnlQuickStart.BringToFront();
            radioRandom.Checked = true;
            radioRandom.Focus();
        }

        private void method_25()
        {
            pnlQuickStart.Visible = false;
        }

        private void method_26()
        {
            pnlThemes.Size = new Size(827, 681);
            pnlThemes.Location = new Point((base.Width - pnlThemes.Width) / 2, (base.Height - pnlThemes.Height) / 2);
            pnlThemes.DoLayout();
            pnlThemes.lblThemeTitle.Font = font_9;
            pnlThemes.lblThemeDescription.Font = font_5;
            pnlThemes.lblThemeGalaxyMaps.Font = font_7;
            string text = main_0.gameOptions_0.CustomizationSetName;
            if (string.IsNullOrEmpty(text))
            {
                text = "(Default)";
            }
            pnlThemes.lblCurrentTheme.Font = font_7;
            pnlThemes.lblCurrentTheme.Text = TextResolver.GetText("Current Theme") + ": " + text;
            method_28(text);
            pnlThemes.DoLayout();
            pnlThemes.btnThemeSwitch.Font = font_7;
            pnlThemes.btnThemeCancel.Font = font_7;            
            pnlThemes.Visible = true;
            pnlThemes.BringToFront();
        }

        private void method_27()
        {
            pnlThemes.Visible = false;
        }

        private void method_28(string string_2)
        {
            string path = Application.StartupPath + "\\Customization\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string[] array = Directory.GetDirectories(path);
            if (array == null)
            {
                array = new string[0];
            }
            List<string> list = new List<string>();
            string[] array2 = array;
            foreach (string path2 in array2)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path2);
                string name = directoryInfo.Name;
                list.Add(name);
            }
            list.Sort();
            List<string> list2 = new List<string>();
            list2.Add("(Default)");
            list2.AddRange(list);
            foreach (Control item in list_0)
            {
                if (item.Parent != null && item.Parent.Controls.Contains(item))
                {
                    item.Parent.Controls.Remove(item);
                }
                ((RadioButton)item).CheckedChanged -= method_29;
                item.Dispose();
            }
            list_0.Clear();
            for (int j = 0; j < list2.Count; j++)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = list2[j];
                radioButton.Font = new Font(font_3, FontStyle.Bold);
                radioButton.BackColor = Color.Transparent;
                radioButton.ForeColor = Color.FromArgb(170, 170, 170);
                radioButton.Location = new Point(10, 40 + j * 25);
                radioButton.Size = new Size(200, 25);
                radioButton.CheckedChanged += method_29;
                radioButton.Parent = pnlThemes;
                if (list2[j] == string_2)
                {
                    radioButton.Checked = true;
                }
                pnlThemes.Controls.Add(radioButton);
                list_0.Add(radioButton);
            }
        }

        private void method_29(object sender, EventArgs e)
        {
            if (!(sender is RadioButton))
            {
                return;
            }
            RadioButton radioButton = (RadioButton)sender;
            string text = radioButton.Text;
            string text2 = string.Empty;
            Bitmap image = null;
            bool enabled = true;
            if (text == "(Default)")
            {
                text2 = TextResolver.GetText("The default Distant Worlds theme, using the standard images, names and music.");
                image = bitmap_0;
                GalaxySummaryList galaxySummaryList = method_30(string.Empty);
                if (galaxySummaryList.Count > 0)
                {
                    pnlThemes.lblThemeGalaxyMaps.Text = string.Format(TextResolver.GetText("This theme has X galaxy maps available to play"), galaxySummaryList.Count.ToString("0"));
                }
                else
                {
                    pnlThemes.lblThemeGalaxyMaps.Text = string.Empty;
                }
                if (string.IsNullOrEmpty(main_0.gameOptions_0.CustomizationSetName))
                {
                    enabled = false;
                }
            }
            else
            {
                string text3 = Application.StartupPath + "\\Customization\\" + text + "\\";
                if (File.Exists(text3 + "about.txt"))
                {
                    text2 = File.ReadAllText(text3 + "about.txt");
                }
                if (File.Exists(text3 + "about.png"))
                {
                    image = (Bitmap)Image.FromFile(text3 + "about.png");
                }
                GalaxySummaryList galaxySummaryList2 = method_30(text);
                if (galaxySummaryList2.Count > 0)
                {
                    pnlThemes.lblThemeGalaxyMaps.Text = string.Format(TextResolver.GetText("This theme has X galaxy maps available to play"), galaxySummaryList2.Count.ToString("0"));
                }
                else
                {
                    pnlThemes.lblThemeGalaxyMaps.Text = string.Empty;
                }
                if (text == main_0.gameOptions_0.CustomizationSetName)
                {
                    enabled = false;
                }
            }
            pnlThemes.btnThemeSwitch.Enabled = enabled;
            pnlThemes.lblThemeTitle.Text = text;
            pnlThemes.lblThemeDescription.Text = text2;
            pnlThemes.picThemeImage.Image = image;
        }

        private GalaxySummaryList method_30(string string_2)
        {
            string folderPath = Application.StartupPath + "\\maps\\";
            if (!string.IsNullOrEmpty(string_2))
            {
                folderPath = Application.StartupPath + "\\Customization\\" + string_2 + "\\maps\\";
            }
            GalaxySummaryList galaxySummaryList = new GalaxySummaryList();
            galaxySummaryList.LoadFromFolder(folderPath);
            return galaxySummaryList;
        }

        private void method_31(string string_2)
        {
            pnlNewGame.SuspendLayout();
            pnlNewGame.pnlBody.SuspendLayout();
            pnlNewGame.Size = new Size(927, 768);
            pnlNewGame.Location = new Point((base.Width - pnlNewGame.Width) / 2, (base.Height - pnlNewGame.Height) / 2);
            pnlNewGame.DoLayout();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Playstyle");
            lblHelpTitle.Location = new Point(10, 7);
            lblHelpTitle.Font = font_6;
            lblHelpDescription.Location = new Point(175, 8);
            lblHelpDescription.TextAlign = ContentAlignment.TopLeft;
            lblHelpDescription.Font = font_3;
            lblHelpDescription.MaximumSize = new Size(720, 32);
            method_100("", "");
            Size size = new Size(900, 660);
            pnlStartNewGameGalaxyMaps.Size = size;
            pnlStartNewGameYourEmpireType.Size = size;
            pnlStartNewGameTheGalaxy.Size = size;
            pnlStartNewGameColonizationTerritory.Size = size;
            pnlStartNewGameVictoryConditions.Size = size;
            pnlStartNewGameOtherEmpires.Size = size;
            pnlStartNewGameYourEmpire.Size = size;
            pnlStartNewGameYourRace.Size = size;
            pnlStartNewGameJumpStart.Size = size;
            Point location = new Point(5, 40);
            pnlStartNewGameGalaxyMaps.Location = location;
            pnlStartNewGameYourEmpireType.Location = location;
            pnlStartNewGameTheGalaxy.Location = location;
            pnlStartNewGameColonizationTerritory.Location = location;
            pnlStartNewGameVictoryConditions.Location = location;
            pnlStartNewGameOtherEmpires.Location = location;
            pnlStartNewGameYourEmpire.Location = location;
            pnlStartNewGameYourRace.Location = location;
            pnlStartNewGameJumpStart.Location = location;
            string string_3 = "(" + TextResolver.GetText("Default") + ")";
            if (!string.IsNullOrEmpty(main_0.string_3))
            {
                string_3 = main_0.string_3;
            }
            method_33(string_3, string.Empty);
            method_41();
            method_37();
            method_34();
            method_42();
            method_39();
            method_43();
            method_44();
            method_36();
            if (main_0.gameOptions_0 == null)
            {
                main_0.method_260();
            }
            if (main_0.gameOptions_0.StartGameOptions == null)
            {
                main_0.gameOptions_0.StartGameOptions = main_0.method_259();
            }
            if (main_0.gameOptions_0.StartGameOptions.YourEmpireRace == 0)
            {
                int num = raceList_0.IndexOf("Human");
                if (num >= 0)
                {
                    main_0.gameOptions_0.StartGameOptions.YourEmpireRace = num + 1;
                }
            }
            method_196(main_0.gameOptions_0.StartGameOptions);
            if (galaxySummaryList_0.Count > 0)
            {
                method_32();
                pnlStartNewGameGalaxyMaps.Visible = true;
                pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Prebuilt Galaxies");
                ctlStartNewGameGalaxyMapsGalaxies.Focus();
                pnlStartNewGameGalaxyMaps.BringToFront();
            }
            else
            {
                pnlStartNewGameYourEmpireType.Visible = true;
                pnlStartNewGameYourEmpireType.BringToFront();
                method_100(TextResolver.GetText("Start New Game New Player Explanation Title UNIVERSE") + ":", TextResolver.GetText("Start New Game New Player Explanation Text UNIVERSE"));
            }
            pnlNewGame.pnlBody.ResumeLayout();
            pnlNewGame.ResumeLayout();
            pnlNewGame.Visible = true;
            pnlNewGame.BringToFront();
        }

        private void method_32()
        {
            if (galaxySummaryList_0 != null && galaxySummaryList_0.Count > 0)
            {
                ctlStartNewGameGalaxyMapsGalaxies.BindData(galaxySummaryList_0);
                pnlStartNewGameGalaxyMapsGalaxy.BindData(galaxySummaryList_0[0], font_9, font_3, font_7);
                string text = galaxySummaryList_0[0].Title;
                if (string.IsNullOrEmpty(text))
                {
                    text = galaxySummaryList_0[0].Filename;
                }
                lblStartNewGameGalaxyMapsAvailableFactions.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableEmpires"), text);
                ctlStartNewGameGalaxyMapsEmpires.BindData(galaxySummaryList_0[0].EmpireSummaries, raceList_1, main_0.raceImageCache_0);
                if (galaxySummaryList_0[0].EmpireSummaries.Count > 0)
                {
                    pnlStartNewGameGalaxyMapsEmpire.BindData(galaxySummaryList_0[0].EmpireSummaries[0], raceList_1, main_0.raceImageCache_0, font_9, font_3, font_7);
                }
            }
            else
            {
                ctlStartNewGameGalaxyMapsGalaxies.ClearData();
                pnlStartNewGameGalaxyMapsGalaxy.ClearData();
                ctlStartNewGameGalaxyMapsEmpires.ClearData();
                pnlStartNewGameGalaxyMapsEmpire.ClearData();
            }
        }

        private void method_33(string string_2, string string_3)
        {
            pnlStartNewGameGalaxyMaps.SuspendLayout();
            btnStartNewGameGalaxyMapsCustom.Size = new Size(300, 40);
            btnStartNewGameGalaxyMapsCustom.Location = new Point(pnlStartNewGameGalaxyMaps.Width - (btnStartNewGameGalaxyMapsCustom.Width + 10), 10);
            btnStartNewGameGalaxyMapsCustom.Text = TextResolver.GetText("StartNewGame GalaxyMaps CustomGame") + " >>";
            lblStartNewGameGalaxyMapsExplanation.Location = new Point(10, 10);
            lblStartNewGameGalaxyMapsExplanation.Font = font_4;
            lblStartNewGameGalaxyMapsExplanation.ForeColor = Color.Yellow;
            lblStartNewGameGalaxyMapsExplanation.MaximumSize = new Size(pnlStartNewGameGalaxyMaps.Width - (btnStartNewGameGalaxyMapsCustom.Width + 30), 35);
            lblStartNewGameGalaxyMapsExplanation.Text = TextResolver.GetText("StartNewGame GalaxyMaps Explanation");
            lblStartNewGameGalaxyMapsAvailableGalaxies.Location = new Point(10, 60);
            lblStartNewGameGalaxyMapsAvailableGalaxies.Font = font_7;
            lblStartNewGameGalaxyMapsAvailableGalaxies.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableGalaxies"), string_2);
            lblStartNewGameGalaxyMapsAvailableGalaxies.SendToBack();
            ctlStartNewGameGalaxyMapsGalaxies.Size = new Size(280, 200);
            ctlStartNewGameGalaxyMapsGalaxies.Location = new Point(10, 80);
            ctlStartNewGameGalaxyMapsGalaxies.Grid.Columns["Name"].Width = 280;
            pnlStartNewGameGalaxyMapsGalaxy.Size = new Size(pnlStartNewGameGalaxyMaps.Width - (ctlStartNewGameGalaxyMapsGalaxies.Width + 30), 200);
            pnlStartNewGameGalaxyMapsGalaxy.Location = new Point(300, 80);
            lblStartNewGameGalaxyMapsAvailableFactions.Location = new Point(10, 290);
            lblStartNewGameGalaxyMapsAvailableFactions.Font = font_7;
            lblStartNewGameGalaxyMapsAvailableFactions.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableEmpires"), string_3);
            lblStartNewGameGalaxyMapsAvailableFactions.SendToBack();
            ctlStartNewGameGalaxyMapsEmpires.Size = new Size(280, pnlStartNewGameGalaxyMaps.Height - 370);
            ctlStartNewGameGalaxyMapsEmpires.Location = new Point(10, 310);
            ctlStartNewGameGalaxyMapsEmpires.Grid.Columns["Race"].Width = 40;
            ctlStartNewGameGalaxyMapsEmpires.Grid.Columns["Name"].Width = 240;
            pnlStartNewGameGalaxyMapsEmpire.Size = new Size(pnlStartNewGameGalaxyMaps.Width - (ctlStartNewGameGalaxyMapsEmpires.Width + 30), ctlStartNewGameGalaxyMapsEmpires.Height);
            pnlStartNewGameGalaxyMapsEmpire.Location = new Point(ctlStartNewGameGalaxyMapsEmpires.Right + 10, ctlStartNewGameGalaxyMapsEmpires.Top);
            btnStartNewGameGalaxyMapsStart.Size = new Size(300, 40);
            btnStartNewGameGalaxyMapsStart.Location = new Point(pnlStartNewGameGalaxyMaps.Width - (btnStartNewGameGalaxyMapsStart.Width + 10), pnlStartNewGameGalaxyMaps.Height - (btnStartNewGameGalaxyMapsStart.Height + 10));
            btnStartNewGameGalaxyMapsStart.Text = TextResolver.GetText("StartNewGame GalaxyMaps StartGame") + " >>";
            btnStartNewGameGalaxyMapsStart.Visible = true;
            btnStartNewGameGalaxyMapsStart.BringToFront();
            pnlStartNewGameGalaxyMaps.ResumeLayout();
        }

        private void method_34()
        {
            pnlStartNewGameColonizationTerritory.SuspendLayout();
            tbarStartNewGameTheGalaxyColonyPrevalence.Setup();
            tbarStartNewGameTheGalaxyAlienLife.Setup();
            tbarStartNewGameTheGalaxyColonyPrevalence.LabelWidth = 90;
            tbarStartNewGameTheGalaxyAlienLife.LabelWidth = 90;
            tbarStartNewGameTheGalaxyColonyPrevalence.LinkWidth = 0;
            tbarStartNewGameTheGalaxyAlienLife.LinkWidth = 90;
            tbarStartNewGameTheGalaxyColonyPrevalence.Location = new Point(10, 10);
            tbarStartNewGameTheGalaxyAlienLife.Location = new Point(10, 80);
            tbarStartNewGameTheGalaxyColonyPrevalence.Size = new Size(880, 55);
            tbarStartNewGameTheGalaxyAlienLife.Size = new Size(880, 55);
            tbarStartNewGameTheGalaxyColonyPrevalence.SetLabels(new string[5]
            {
            TextResolver.GetText("Scarce"),
            TextResolver.GetText("Occasional"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Plentiful"),
            TextResolver.GetText("Abundant")
            });
            tbarStartNewGameTheGalaxyAlienLife.SetLabels(new string[5]
            {
            TextResolver.GetText("Rare"),
            TextResolver.GetText("Scattered"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Plentiful"),
            TextResolver.GetText("Teeming")
            });
            tbarStartNewGameTheGalaxyColonyPrevalence.LabelText = TextResolver.GetText("Colony Prevalence");
            tbarStartNewGameTheGalaxyAlienLife.LabelText = TextResolver.GetText("Independent Alien Life");
            tbarStartNewGameTheGalaxyAlienLife.LinkText = TextResolver.GetText("About Alien Life...");
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeTitle.Font = font_3;
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeTitle.Text = TextResolver.GetText("Colony Influence Range");
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeTitle.Location = new Point(10, 160);
            sldStartNewGameColonizationTerritoryColonyInfluenceRange.Size = new Size(680, 16);
            sldStartNewGameColonizationTerritoryColonyInfluenceRange.Location = new Point(150, 160);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeValue.Location = new Point(840, 160);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeValue.Font = font_3;
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.Location = new Point(425, 180);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.MaximumSize = new Size(450, 30);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.MinimumSize = new Size(450, 30);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.Size = new Size(450, 30);
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.TextAlign = ContentAlignment.TopRight;
            lblStartNewGameColonizationTerritoryColonyInfluenceRangeSuggestion.Font = font_3;
            grpStartNewGameColonizationTerritoryColonizationRange.Font = font_3;
            grpStartNewGameColonizationTerritoryColonizationRange.Text = "          " + TextResolver.GetText("Enforce Colonization Range Limits");
            grpStartNewGameColonizationTerritoryColonizationRange.Size = new Size(880, 60);
            grpStartNewGameColonizationTerritoryColonizationRange.Location = new Point(10, 215);
            chkStartNewGameColonizationTerritoryEnforceColonizationRange.Location = new Point(21, 217);
            chkStartNewGameColonizationTerritoryEnforceColonizationRange.BringToFront();
            lblStartNewGameColonizationTerritoryColonizationRangeTitle.Font = font_3;
            lblStartNewGameColonizationTerritoryColonizationRangeTitle.Text = TextResolver.GetText("Colonization Range");
            lblStartNewGameColonizationTerritoryColonizationRangeTitle.Location = new Point(10, 25);
            sldStartNewGameColonizationTerritoryColonizationRange.Size = new Size(645, 16);
            sldStartNewGameColonizationTerritoryColonizationRange.Location = new Point(140, 25);
            lblStartNewGameColonizationTerritoryColonizationRangeValue.Location = new Point(795, 25);
            lblStartNewGameColonizationTerritoryColonizationRangeValue.Font = font_3;
            picStartNewGameColonizationTerritoryImage.Size = new Size(880, 253);
            picStartNewGameColonizationTerritoryImage.Location = new Point(10, 315);
            btnStartNewGameColonizationTerritoryPrevious.Font = font_7;
            btnStartNewGameColonizationTerritoryNext.Font = font_7;
            btnStartNewGameColonizationTerritoryPrevious.Size = new Size(300, 40);
            btnStartNewGameColonizationTerritoryNext.Size = new Size(300, 40);
            btnStartNewGameColonizationTerritoryPrevious.Location = new Point(10, pnlStartNewGameColonizationTerritory.Height - (btnStartNewGameColonizationTerritoryPrevious.Height + 10));
            btnStartNewGameColonizationTerritoryNext.Location = new Point(pnlStartNewGameColonizationTerritory.Width - (btnStartNewGameColonizationTerritoryNext.Width + 10), pnlStartNewGameColonizationTerritory.Height - (btnStartNewGameColonizationTerritoryNext.Height + 10));
            pnlStartNewGameColonizationTerritory.ResumeLayout();
        }

        private void method_35()
        {
            if (bool_2)
            {
                pnlJumpStartYourEmpireGovernment.Visible = false;
                lblJumpStartVictoryPiratePlaystyle.Visible = true;
                cmbJumpStartVictoryPiratePlayStyle.Visible = true;
                pnlJumpStartPiratePlaystyleDescriptionContainer.Visible = true;
                picJumpStartYourEmpirePiratePlaystyle.Visible = true;
            }
            else
            {
                pnlJumpStartYourEmpireGovernment.Visible = true;
                lblJumpStartVictoryPiratePlaystyle.Visible = false;
                cmbJumpStartVictoryPiratePlayStyle.Visible = false;
                pnlJumpStartPiratePlaystyleDescriptionContainer.Visible = false;
                picJumpStartYourEmpirePiratePlaystyle.Visible = false;
            }
        }

        private void method_36()
        {
            pnlJumpStartGalaxyShapeSize.Size = new Size(880, 275);
            pnlJumpStartGalaxyShapeSize.Location = new Point(10, 10);
            radJumpStartGalaxyShapeElliptical.Location = new Point(10, 10);
            radJumpStartGalaxyShapeSpiral.Location = new Point(10, 30);
            radJumpStartGalaxyShapeRing.Location = new Point(10, 50);
            radJumpStartGalaxyShapeIrregular.Location = new Point(10, 70);
            radJumpStartGalaxyShapeEvenClusters.Location = new Point(10, 90);
            radJumpStartGalaxyShapeVariedClusters.Location = new Point(10, 110);
            radJumpStartGalaxyShapeElliptical.Font = font_3;
            radJumpStartGalaxyShapeSpiral.Font = font_3;
            radJumpStartGalaxyShapeRing.Font = font_3;
            radJumpStartGalaxyShapeIrregular.Font = font_3;
            radJumpStartGalaxyShapeEvenClusters.Font = font_3;
            radJumpStartGalaxyShapeVariedClusters.Font = font_3;
            picJumpStartTheGalaxyPreview.Size = new Size(120, 120);
            picJumpStartTheGalaxyPreview.Location = new Point(125, 10);
            picJumpStartTheGalaxyPreview.SizeMode = PictureBoxSizeMode.Zoom;
            lblJumpStartGalaxyShapeTitle.Font = font_7;
            lblJumpStartGalaxyShapeTitle.Location = new Point(255, 7);
            lblJumpStartGalaxyShapeDescription.Location = new Point(255, 32);
            lblJumpStartGalaxyShapeDescription.MaximumSize = new Size(615, 168);
            tbarJumpStartTheGalaxyStarDensity.Setup();
            tbarJumpStartTheGalaxyStarDensity.LinkWidth = 0;
            tbarJumpStartTheGalaxyStarDensity.Location = new Point(10, 145);
            tbarJumpStartTheGalaxyStarDensity.Size = new Size(860, 55);
            string text = TextResolver.GetText("stars");
            tbarJumpStartTheGalaxyStarDensity.SetLabels(new string[6]
            {
            TextResolver.GetText("Dwarf") + "\n100 " + text,
            TextResolver.GetText("Tiny") + "\n250 " + text,
            TextResolver.GetText("Small") + "\n400 " + text,
            TextResolver.GetText("Standard") + "\n700 " + text,
            TextResolver.GetText("Large") + "\n1000 " + text,
            TextResolver.GetText("Huge") + "\n1400 " + text
            });
            tbarJumpStartTheGalaxyStarDensity.LabelText = TextResolver.GetText("Star\\nAmount");
            tbarJumpStartTheGalaxyDimensions.Setup();
            tbarJumpStartTheGalaxyDimensions.LinkWidth = 0;
            tbarJumpStartTheGalaxyDimensions.Location = new Point(10, 210);
            tbarJumpStartTheGalaxyDimensions.Size = new Size(860, 55);
            string text2 = TextResolver.GetText("sectors");
            tbarJumpStartTheGalaxyDimensions.SetLabels(new string[5]
            {
            TextResolver.GetText("Tiny") + "\n4x4 " + text2,
            TextResolver.GetText("Small") + "\n6x6 " + text2,
            TextResolver.GetText("Medium") + "\n8x8 " + text2,
            TextResolver.GetText("Large") + "\n10x10 " + text2,
            TextResolver.GetText("Huge") + "\n15x15 " + text2
            });
            tbarJumpStartTheGalaxyDimensions.LabelText = TextResolver.GetText("Physical\\nSize");
            pnlJumpStartYourEmpireRace.Size = new Size(480, 240);
            pnlJumpStartYourEmpireRace.Location = new Point(10, 295);
            lblJumpStartYourEmpireRaceTitle.Visible = false;
            cmbJumpStartYourEmpireRace.Size = new Size(160, 26);
            cmbJumpStartYourEmpireRace.ItemHeight = 26;
            cmbJumpStartYourEmpireRace.Location = new Point(15, 15);
            picJumpStartYourEmpireRace.Size = new Size(160, 160);
            picJumpStartYourEmpireRace.Location = new Point(15, 52);
            picJumpStartYourEmpireRace.SizeMode = PictureBoxSizeMode.Zoom;
            picJumpStartYourEmpireRace.BorderStyle = BorderStyle.None;
            lnkJumpStartYourEmpireRace.Location = new Point(15, 218);
            lnkJumpStartYourEmpireRace.Font = font_3;
            lblJumpStartYourEmpireRaceName.Location = new Point(185, 14);
            lblJumpStartYourEmpireRaceName.Visible = true;
            lblJumpStartYourEmpireRaceName.BringToFront();
            pnlJumpStartYourEmpireRaceAttributesContainer.Location = new Point(190, 51);
            pnlJumpStartYourEmpireRaceAttributesContainer.Size = new Size(280, 179);
            pnlJumpStartYourEmpireRaceAttributesContainer.AutoScroll = true;
            pnlJumpStartYourEmpireRaceAttributesContainer.SetAutoScrollMargin(0, 0);
            pnlJumpStartYourEmpireRaceAttributesContainer.AutoScrollPosition = new Point(0, 0);
            pnlJumpStartYourEmpireRaceAttributes.Location = new Point(0, 0);
            pnlJumpStartYourEmpireRaceAttributes.Size = new Size(260, 0);
            pnlJumpStartYourEmpireRaceAttributes.MaximumSize = new Size(260, 2000);
            pnlJumpStartYourEmpireRaceAttributes.MinimumSize = new Size(260, 205);
            pnlJumpStartYourEmpireRaceAttributes.AutoSize = true;
            pnlJumpStartYourEmpireGovernment.Size = new Size(390, 240);
            pnlJumpStartYourEmpireGovernment.Location = new Point(500, 295);
            lblJumpStartYourEmpireGovernmentTitle.Font = font_7;
            lblJumpStartYourEmpireRaceTitle.Font = font_7;
            lblJumpStartYourEmpireGovernmentName.Visible = false;
            lblJumpStartYourEmpireRaceName.Visible = false;
            cmbJumpStartYourEmpireGovernment.Size = new Size(155, 21);
            cmbJumpStartYourEmpireGovernment.Location = new Point(15, 35);
            cmbJumpStartYourEmpireGovernment.AllowNullItem = true;
            cmbJumpStartYourEmpireGovernment.NullItemText = "(" + TextResolver.GetText("Random") + ")";
            cmbJumpStartYourEmpireGovernment.Font = font_3;
            lblJumpStartYourEmpireGovernmentAttributes.Location = new Point(180, 37);
            lblJumpStartYourEmpireGovernmentAttributes.Font = font_3;
            lblJumpStartYourEmpireGovernmentAttributes.MaximumSize = new Size(200, 170);
            lnkJumpStartYourEmpireGovernment.Size = new Size(200, 21);
            lnkJumpStartYourEmpireGovernment.MaximumSize = new Size(200, 40);
            lnkJumpStartYourEmpireGovernment.Location = new Point(180, 200);
            lnkJumpStartYourEmpireGovernment.Font = font_3;
            cmbJumpStartYourEmpireRace.BindData(font_3, raceList_0, main_0.raceImageCache_0.GetRaceImages(), allowRandomRace: true);
            lblJumpStartVictoryPiratePlaystyle.Font = font_7;
            lblJumpStartVictoryPiratePlaystyle.Text = TextResolver.GetText("Pirate Playstyle");
            lblJumpStartVictoryPiratePlaystyle.Location = new Point(500, 298);
            cmbJumpStartVictoryPiratePlayStyle.Font = font_3;
            cmbJumpStartVictoryPiratePlayStyle.Size = new Size(160, 21);
            cmbJumpStartVictoryPiratePlayStyle.Location = new Point(730, 295);
            pnlJumpStartPiratePlaystyleDescriptionContainer.Size = new Size(220, 205);
            pnlJumpStartPiratePlaystyleDescriptionContainer.Location = new Point(500, 331);
            pnlJumpStartPiratePlaystyleDescriptionContainer.AutoScroll = true;
            pnlJumpStartPiratePlaystyleDescriptionContainer.SetAutoScrollMargin(0, 0);
            pnlJumpStartPiratePlaystyleDescriptionContainer.AutoScrollPosition = new Point(0, 0);
            lblJumpStartPiratePlaystyleDescription.Font = font_3;
            lblJumpStartPiratePlaystyleDescription.Location = new Point(0, 0);
            lblJumpStartPiratePlaystyleDescription.MaximumSize = new Size(200, 500);
            lblJumpStartPiratePlaystyleDescription.Size = new Size(200, 205);
            picJumpStartYourEmpirePiratePlaystyle.Size = new Size(160, 160);
            picJumpStartYourEmpirePiratePlaystyle.Location = new Point(730, 331);
            method_35();
            tbarJumpStartTheGalaxyDifficulty.Setup();
            tbarJumpStartTheGalaxyDifficulty.LabelWidth = 80;
            tbarJumpStartTheGalaxyDifficulty.LinkWidth = 0;
            tbarJumpStartTheGalaxyDifficulty.Location = new Point(10, 545);
            tbarJumpStartTheGalaxyDifficulty.Size = new Size(630, 55);
            tbarJumpStartTheGalaxyDifficulty.SetLabels(new string[5]
            {
            TextResolver.GetText("Easy"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Hard"),
            TextResolver.GetText("Very Hard"),
            TextResolver.GetText("Extreme")
            });
            chkJumpStartTheGalaxyDifficultyScaling.Location = new Point(650, 560);
            chkJumpStartTheGalaxyDifficultyScaling.Text = TextResolver.GetText("Difficulty scales as player nears victory");
            tbarJumpStartTheGalaxyDifficulty.LabelText = TextResolver.GetText("Difficulty");
            btnJumpStartTheGalaxyPrevious.Font = font_7;
            btnJumpStartTheGalaxyNext.Font = font_7;
            btnJumpStartTheGalaxyPrevious.Size = new Size(300, 40);
            btnJumpStartTheGalaxyNext.Size = new Size(300, 40);
            btnJumpStartTheGalaxyPrevious.Location = new Point(10, pnlStartNewGameJumpStart.Height - (btnJumpStartTheGalaxyPrevious.Height + 10));
            btnJumpStartTheGalaxyNext.Location = new Point(pnlStartNewGameJumpStart.Width - (btnJumpStartTheGalaxyNext.Width + 10), pnlStartNewGameJumpStart.Height - (btnJumpStartTheGalaxyNext.Height + 10));
        }

        private void method_37()
        {
            pnlStartNewGameTheGalaxy.SuspendLayout();
            pnlStartNewGameGalaxyShapeSize.Size = new Size(650, 345);
            pnlStartNewGameGalaxyShapeSize.Location = new Point(10, 10);
            radStartNewGameGalaxyShapeElliptical.Location = new Point(10, 10);
            radStartNewGameGalaxyShapeSpiral.Location = new Point(10, 30);
            radStartNewGameGalaxyShapeRing.Location = new Point(10, 50);
            radStartNewGameGalaxyShapeIrregular.Location = new Point(10, 70);
            radStartNewGameGalaxyShapeClustersEven.Location = new Point(10, 90);
            radStartNewGameGalaxyShapeClustersVaried.Location = new Point(10, 110);
            radStartNewGameGalaxyShapeElliptical.Font = font_3;
            radStartNewGameGalaxyShapeSpiral.Font = font_3;
            radStartNewGameGalaxyShapeRing.Font = font_3;
            radStartNewGameGalaxyShapeIrregular.Font = font_3;
            radStartNewGameGalaxyShapeClustersEven.Font = font_3;
            radStartNewGameGalaxyShapeClustersVaried.Font = font_3;
            picStartNewGameTheGalaxyPreview.Size = new Size(190, 190);
            picStartNewGameTheGalaxyPreview.Location = new Point(120, 10);
            picStartNewGameTheGalaxyPreview.SizeMode = PictureBoxSizeMode.Zoom;
            lblStartNewGameGalaxyShapeTitle.Font = font_7;
            lblStartNewGameGalaxyShapeTitle.Location = new Point(335, 10);
            lblStartNewGameGalaxyShapeDescription.Location = new Point(335, 35);
            lblStartNewGameGalaxyShapeDescription.MaximumSize = new Size(305, 235);
            tbarStartNewGameTheGalaxyStarDensity.Setup();
            tbarStartNewGameTheGalaxyStarDensity.LinkWidth = 0;
            tbarStartNewGameTheGalaxyStarDensity.Location = new Point(10, 215);
            tbarStartNewGameTheGalaxyStarDensity.Size = new Size(630, 55);
            string text = TextResolver.GetText("stars");
            BaconStart.method_37(this);
            tbarStartNewGameTheGalaxyStarDensity.LabelText = TextResolver.GetText("Star\\nAmount");
            tbarStartNewGameTheGalaxyDimensions.Setup();
            tbarStartNewGameTheGalaxyDimensions.LinkWidth = 0;
            tbarStartNewGameTheGalaxyDimensions.Location = new Point(10, 280);
            tbarStartNewGameTheGalaxyDimensions.Size = new Size(630, 55);
            string text2 = TextResolver.GetText("sectors");
            tbarStartNewGameTheGalaxyDimensions.SetLabels(new string[5]
            {
            TextResolver.GetText("Tiny") + "\n4x4 " + text2,
            TextResolver.GetText("Small") + "\n6x6 " + text2,
            TextResolver.GetText("Medium") + "\n8x8 " + text2,
            TextResolver.GetText("Large") + "\n10x10 " + text2,
            TextResolver.GetText("Huge") + "\n15x15 " + text2
            });
            tbarStartNewGameTheGalaxyDimensions.LabelText = TextResolver.GetText("Physical\\nSize");
            tbarStartNewGameTheGalaxyExpansion.Setup(2);
            tbarStartNewGameTheGalaxyAggression.Setup();
            tbarStartNewGameTheGalaxyDifficulty.Setup();
            tbarStartNewGameTheGalaxyResearchSpeed.Setup();
            tbarStartNewGameTheGalaxySpaceCreatures.Setup();
            tbarStartNewGameTheGalaxyPirates.Setup();
            tbarStartNewGameTheGalaxyPirateStrength.Setup();
            tbarStartNewGameTheGalaxyExpansion.LabelWidth = 80;
            tbarStartNewGameTheGalaxyDifficulty.LabelWidth = 80;
            tbarStartNewGameTheGalaxyAggression.LabelWidth = 80;
            tbarStartNewGameTheGalaxyPirateStrength.LabelWidth = 80;
            tbarStartNewGameTheGalaxyResearchSpeed.LabelWidth = 70;
            tbarStartNewGameTheGalaxySpaceCreatures.LabelWidth = 70;
            tbarStartNewGameTheGalaxyPirates.LabelWidth = 60;
            tbarStartNewGameTheGalaxyExpansion.LinkWidth = 0;
            tbarStartNewGameTheGalaxyAggression.LinkWidth = 0;
            tbarStartNewGameTheGalaxyDifficulty.LinkWidth = 0;
            tbarStartNewGameTheGalaxyPirateStrength.LinkWidth = 0;
            tbarStartNewGameTheGalaxyResearchSpeed.LinkWidth = 65;
            tbarStartNewGameTheGalaxySpaceCreatures.LinkWidth = 65;
            tbarStartNewGameTheGalaxyPirates.LinkWidth = 65;
            tbarStartNewGameTheGalaxyExpansion.Location = new Point(10, 370);
            tbarStartNewGameTheGalaxyAggression.Location = new Point(10, 430);
            tbarStartNewGameTheGalaxyDifficulty.Location = new Point(10, 490);
            tbarStartNewGameTheGalaxyResearchSpeed.Location = new Point(400, 370);
            tbarStartNewGameTheGalaxySpaceCreatures.Location = new Point(400, 430);
            tbarStartNewGameTheGalaxyPirates.Location = new Point(400, 490);
            tbarStartNewGameTheGalaxyPirateStrength.Location = new Point(400, 550);
            tbarStartNewGameTheGalaxyExpansion.Size = new Size(380, 55);
            tbarStartNewGameTheGalaxyDifficulty.Size = new Size(380, 55);
            tbarStartNewGameTheGalaxyAggression.Size = new Size(380, 55);
            numStartNewGameTheGalaxyResearchBaseTech.Location = new Point(838, 384);
            numStartNewGameTheGalaxyResearchBaseTech.Size = new Size(40, 23);
            numStartNewGameTheGalaxyResearchBaseTech.Minimum = 1m;
            numStartNewGameTheGalaxyResearchBaseTech.Maximum = 999m;
            numStartNewGameTheGalaxyResearchBaseTech.Enabled = true;
            numStartNewGameTheGalaxyResearchBaseTech.TextAlign = HorizontalAlignment.Right;
            lblStartNewGameTheGalaxyResearchBaseTechLabel.Location = new Point(878, 384);
            lblStartNewGameTheGalaxyResearchBaseTechLabel.Font = font_6;
            tbarStartNewGameTheGalaxyResearchSpeed.Size = new Size(433, 55);
            tbarStartNewGameTheGalaxySpaceCreatures.Size = new Size(490, 55);
            tbarStartNewGameTheGalaxyPirates.Size = new Size(403, 55);
            tbarStartNewGameTheGalaxyPirateStrength.Size = new Size(380, 55);
            lblStartNewGameTheGalaxyPirateProximityLabel.Text = TextResolver.GetText("Pirate Proximity");
            lblStartNewGameTheGalaxyPirateProximityLabel.MaximumSize = new Size(90, 21);
            lblStartNewGameTheGalaxyPirateProximityLabel.Location = new Point(805, 493);
            cmbStartNewGameTheGalaxyPirateProximity.Size = new Size(80, 21);
            cmbStartNewGameTheGalaxyPirateProximity.Location = new Point(807, 508);
            if (cmbStartNewGameTheGalaxyPirateProximity.Items == null || cmbStartNewGameTheGalaxyPirateProximity.Items.Count <= 0)
            {
                cmbStartNewGameTheGalaxyPirateProximity.Items.AddRange(new string[3]
                {
                TextResolver.GetText("Nearby"),
                TextResolver.GetText("Average"),
                TextResolver.GetText("Distant")
                });
            }
            cmbStartNewGameTheGalaxyPirateProximity.BringToFront();
            tbarStartNewGameTheGalaxyExpansion.SliderOffset = 2;
            tbarStartNewGameTheGalaxyExpansion.SetLabels(new string[6]
            {
            TextResolver.GetText("PreWarp"),
            TextResolver.GetText("Starting"),
            TextResolver.GetText("Young"),
            TextResolver.GetText("Expanding"),
            TextResolver.GetText("Mature"),
            TextResolver.GetText("Old")
            });
            tbarStartNewGameTheGalaxyAggression.SetLabels(new string[5]
            {
            TextResolver.GetText("Peaceful"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Restless"),
            TextResolver.GetText("Unstable"),
            TextResolver.GetText("Chaos")
            });
            tbarStartNewGameTheGalaxyDifficulty.SetLabels(new string[5]
            {
            TextResolver.GetText("Easy"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Hard"),
            TextResolver.GetText("Very Hard"),
            TextResolver.GetText("Extreme")
            });
            chkStartNewGameTheGalaxyDifficultyScaling.Location = new Point(10, 550);
            chkStartNewGameTheGalaxyDifficultyScaling.Text = TextResolver.GetText("Difficulty scales as player nears victory");
            chkStartNewGameTheGalaxyPiratesRespawn.Location = new Point(785, 550);
            chkStartNewGameTheGalaxyPiratesRespawn.AutoSize = false;
            chkStartNewGameTheGalaxyPiratesRespawn.Size = new Size(105, 60);
            chkStartNewGameTheGalaxyPiratesRespawn.Padding = new Padding(0);
            chkStartNewGameTheGalaxyPiratesRespawn.Text = TextResolver.GetText("Destroyed Pirates do not respawn");
            chkStartNewGameTheGalaxyPiratesRespawn.CheckAlign = ContentAlignment.MiddleRight;
            chkStartNewGameTheGalaxyPiratesRespawn.TextAlign = ContentAlignment.MiddleRight;
            tbarStartNewGameTheGalaxyResearchSpeed.SetLabels(new string[5]
            {
            TextResolver.GetText("Very Expensive"),
            TextResolver.GetText("Expensive"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Cheap"),
            TextResolver.GetText("Very Cheap")
            });
            tbarStartNewGameTheGalaxySpaceCreatures.SetLabels(new string[4]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Few"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Many")
            });
            tbarStartNewGameTheGalaxyPirates.SetLabels(new string[6]
            {
            TextResolver.GetText("None"),
            TextResolver.GetText("Very Few"),
            TextResolver.GetText("Few"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Many"),
            TextResolver.GetText("Very Many")
            });
            tbarStartNewGameTheGalaxyPirateStrength.SetLabels(new string[4]
            {
            TextResolver.GetText("Very Weak"),
            TextResolver.GetText("Weak"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Strong")
            });
            tbarStartNewGameTheGalaxyExpansion.LabelText = TextResolver.GetText("Expansion");
            tbarStartNewGameTheGalaxyAggression.LabelText = TextResolver.GetText("Aggression");
            tbarStartNewGameTheGalaxyDifficulty.LabelText = TextResolver.GetText("Difficulty");
            tbarStartNewGameTheGalaxyResearchSpeed.LabelText = TextResolver.GetText("Research \\nCosts");
            tbarStartNewGameTheGalaxySpaceCreatures.LabelText = TextResolver.GetText("Space Creatures");
            tbarStartNewGameTheGalaxyPirates.LabelText = TextResolver.GetText("Pirates");
            tbarStartNewGameTheGalaxyPirateStrength.LabelText = TextResolver.GetText("Pirate Strength");
            tbarStartNewGameTheGalaxyResearchSpeed.LinkText = TextResolver.GetText("About Research...");
            tbarStartNewGameTheGalaxySpaceCreatures.LinkText = TextResolver.GetText("About Space Creatures...");
            tbarStartNewGameTheGalaxyPirates.LinkText = TextResolver.GetText("About Pirates...");
            picStartNewGameTheGalaxyImage.Size = new Size(220, 345);
            picStartNewGameTheGalaxyImage.Location = new Point(670, 10);
            picStartNewGameTheGalaxyImage.Visible = false;
            pnlStartNewGameTheGalaxyLoadExisting.Size = new Size(220, 345);
            pnlStartNewGameTheGalaxyLoadExisting.Location = new Point(670, 10);
            lblStartNewGameTheGalaxyLoadExistingTitle.Location = new Point(10, 10);
            lblStartNewGameTheGalaxyLoadExistingTitle.Font = font_7;
            lblStartNewGameTheGalaxyLoadExistingTitle.MaximumSize = new Size(200, 40);
            lblStartNewGameTheGalaxyLoadExistingTitle.Text = TextResolver.GetText("OR Load existing Galaxy as map");
            lblStartNewGameTheGalaxyLoadExistingFilepath.Location = new Point(10, 55);
            lblStartNewGameTheGalaxyLoadExistingFilepath.Font = font_3;
            lblStartNewGameTheGalaxyLoadExistingFilepath.MaximumSize = new Size(200, 102);
            lblStartNewGameTheGalaxyLoadExistingFilepath.Text = "(" + TextResolver.GetText("No Galaxy Map specified") + ")";
            lblStartNewGameTheGalaxyLoadExistingFilepath.BorderStyle = BorderStyle.FixedSingle;
            btnStartNewGameTheGalaxyLoadExistingBrowse.Size = new Size(98, 45);
            btnStartNewGameTheGalaxyLoadExistingBrowse.Location = new Point(10, 162);
            btnStartNewGameTheGalaxyLoadExistingBrowse.Text = TextResolver.GetText("Browse for Maps") + "...";
            btnStartNewGameTheGalaxyLoadExistingClear.Size = new Size(98, 45);
            btnStartNewGameTheGalaxyLoadExistingClear.Location = new Point(112, 162);
            btnStartNewGameTheGalaxyLoadExistingClear.Text = TextResolver.GetText("Clear Map");
            chkStartNewGameTheGalaxyLoadExistingResources.Location = new Point(10, 220);
            chkStartNewGameTheGalaxyLoadExistingResources.Font = font_3;
            chkStartNewGameTheGalaxyLoadExistingResources.Text = TextResolver.GetText("Regenerate Resources");
            chkStartNewGameTheGalaxyLoadExistingSceneryResearch.Location = new Point(10, 240);
            chkStartNewGameTheGalaxyLoadExistingSceneryResearch.Font = font_3;
            chkStartNewGameTheGalaxyLoadExistingSceneryResearch.Text = TextResolver.GetText("Regenerate Scenery and Research bonuses");
            chkStartNewGameTheGalaxyLoadExistingCreatures.Location = new Point(10, 260);
            chkStartNewGameTheGalaxyLoadExistingCreatures.Font = font_3;
            chkStartNewGameTheGalaxyLoadExistingCreatures.Text = TextResolver.GetText("Regenerate Space Creatures");
            chkStartNewGameTheGalaxyLoadExistingRuins.Location = new Point(10, 280);
            chkStartNewGameTheGalaxyLoadExistingRuins.Font = font_3;
            chkStartNewGameTheGalaxyLoadExistingRuins.Text = TextResolver.GetText("Regenerate Ruins");
            chkStartNewGameTheGalaxyLoadExistingSpecialLocations.Location = new Point(10, 300);
            chkStartNewGameTheGalaxyLoadExistingSpecialLocations.Font = font_3;
            chkStartNewGameTheGalaxyLoadExistingSpecialLocations.Text = TextResolver.GetText("Regenerate Special Locations");
            btnStartNewGameTheGalaxyPrevious.Font = font_7;
            btnStartNewGameTheGalaxyNext.Font = font_7;
            btnStartNewGameTheGalaxyPrevious.Size = new Size(300, 40);
            btnStartNewGameTheGalaxyNext.Size = new Size(300, 40);
            btnStartNewGameTheGalaxyPrevious.Location = new Point(10, pnlStartNewGameTheGalaxy.Height - (btnStartNewGameTheGalaxyPrevious.Height + 10));
            btnStartNewGameTheGalaxyNext.Location = new Point(pnlStartNewGameTheGalaxy.Width - (btnStartNewGameTheGalaxyNext.Width + 10), pnlStartNewGameTheGalaxy.Height - (btnStartNewGameTheGalaxyNext.Height + 10));
            pnlStartNewGameTheGalaxy.ResumeLayout();
        }

        private void method_38(bool bool_5, bool bool_6)
        {
            if (bool_5)
            {
                if (bool_6)
                {
                    tbarStartNewGameTheGalaxyExpansion.Value = 0;
                    tbarStartNewGameTheGalaxyPirates.Value = 4;
                    cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex = 1;
                    tbarStartNewGameTheGalaxyColonyPrevalence.Value = 3;
                    tbarStartNewGameTheGalaxyAlienLife.Value = 3;
                    tbarStartNewGameYourEmpireTechLevel.Value = 1;
                    tbarStartNewGameTheGalaxyAggression.Value = 2;
                }
                else
                {
                    tbarStartNewGameTheGalaxyExpansion.Value = 1;
                    tbarStartNewGameTheGalaxyPirates.Value = 3;
                    cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex = 1;
                    tbarStartNewGameTheGalaxyColonyPrevalence.Value = 2;
                    tbarStartNewGameTheGalaxyAlienLife.Value = 2;
                    tbarStartNewGameYourEmpireTechLevel.Value = 1;
                    tbarStartNewGameTheGalaxyAggression.Value = 1;
                }
            }
            else if (bool_6)
            {
                tbarStartNewGameTheGalaxyExpansion.Value = 0;
                tbarStartNewGameTheGalaxyPirates.Value = 4;
                cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex = 0;
                tbarStartNewGameTheGalaxyColonyPrevalence.Value = 3;
                tbarStartNewGameTheGalaxyAlienLife.Value = 3;
                tbarStartNewGameYourEmpireTechLevel.Value = 0;
                tbarStartNewGameTheGalaxyAggression.Value = 2;
            }
            else
            {
                tbarStartNewGameTheGalaxyExpansion.Value = 1;
                tbarStartNewGameTheGalaxyPirates.Value = 3;
                cmbStartNewGameTheGalaxyPirateProximity.SelectedIndex = 1;
                tbarStartNewGameTheGalaxyColonyPrevalence.Value = 2;
                tbarStartNewGameTheGalaxyAlienLife.Value = 2;
                tbarStartNewGameYourEmpireTechLevel.Value = 1;
                tbarStartNewGameTheGalaxyAggression.Value = 1;
            }
        }

        private void method_39()
        {
            method_40(bool_5: true);
        }

        private void method_40(bool bool_5)
        {
            pnlStartNewGameYourEmpire.SuspendLayout();
            pnlStartNewGameYourEmpireDetails.Size = new Size(340, 110);
            pnlStartNewGameYourEmpireDetails.Location = new Point(10, 8);
            lblStartNewGameYourEmpireName.Font = font_6;
            lblStartNewGameYourEmpireName.Location = new Point(10, 10);
            lblStartNewGameYourEmpireName.MaximumSize = new Size(90, 35);
            txtYourEmpireName.Location = new Point(105, 9);
            txtYourEmpireName.Size = new Size(220, 23);
            lblStartNewGameYourEmpireMainColor.Location = new Point(10, 47);
            lblStartNewGameYourEmpireSecondaryColor.Location = new Point(10, 82);
            cmbPrimaryColor.Location = new Point(105, 41);
            cmbSecondaryColor.Location = new Point(105, 76);
            cmbFlagShape.Location = new Point(196, 41);
            cmbFlagShape.Size = new Size(128, 56);
            cmbFlagShape.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFlagShape.ItemHeight = 63;
            cmbFlagShape.DropDownHeight = 700;
            pnlStartNewGameYourEmpireGalaxyLocation.Size = new Size(300, 110);
            pnlStartNewGameYourEmpireGalaxyLocation.Location = new Point(360, 8);
            lblStartNewGameYourEmpireGalaxyLocation.Location = new Point(10, 10);
            lblStartNewGameYourEmpireGalaxyLocation.Font = font_6;
            cmbYourEmpireStartLocation.Location = new Point(10, 35);
            cmbYourEmpireStartLocation.Size = new Size(160, 24);
            picStartNewGameYourEmpireGalaxyLocation.Size = new Size(98, 98);
            picStartNewGameYourEmpireGalaxyLocation.Location = new Point(180, 6);
            picStartNewGameYourEmpireGalaxyLocation.SizeMode = PictureBoxSizeMode.Zoom;
            pnlStartNewGameYourEmpireGovernment.Size = new Size(650, 285);
            lblStartNewGameYourEmpireGovernmentTitle.Font = font_7;
            lblStartNewGameYourEmpireRaceTitle.Font = font_7;
            lblStartNewGameYourEmpireGovernmentName.Visible = false;
            lblStartNewGameYourEmpireRaceName.Visible = false;
            cmbStartNewGameYourEmpireGovernment.Size = new Size(175, 21);
            cmbStartNewGameYourEmpireGovernment.Location = new Point(15, 35);
            cmbStartNewGameYourEmpireGovernment.AllowNullItem = true;
            cmbStartNewGameYourEmpireGovernment.NullItemText = "(" + TextResolver.GetText("Random") + ")";
            cmbStartNewGameYourEmpireGovernment.Font = font_3;
            lblStartNewGameYourEmpireGovernmentAttributes.Location = new Point(200, 37);
            lblStartNewGameYourEmpireGovernmentAttributes.Font = font_3;
            lblStartNewGameYourEmpireGovernmentAttributes.MaximumSize = new Size(430, 200);
            lnkStartNewGameYourEmpireGovernment.Size = new Size(360, 21);
            lnkStartNewGameYourEmpireGovernment.MaximumSize = new Size(360, 40);
            lnkStartNewGameYourEmpireGovernment.Location = new Point(200, 255);
            lnkStartNewGameYourEmpireGovernment.Font = font_3;
            if (bool_5)
            {
                cmbStartNewGameYourEmpireRace.BindData(font_3, raceList_0, main_0.raceImageCache_0.GetRaceImages(), allowRandomRace: true);
            }
            pnlStartNewGameYourEmpireGovernment.Location = new Point(10, 308);
            lblVictoryPiratePlaystyle.Font = font_7;
            lblVictoryPiratePlaystyle.Text = TextResolver.GetText("Pirate Playstyle");
            lblVictoryPiratePlaystyle.Location = new Point(10, 197);
            cmbVictoryPiratePlayStyle.Font = font_3;
            cmbVictoryPiratePlayStyle.Size = new Size(120, 21);
            cmbVictoryPiratePlayStyle.Location = new Point(140, 194);
            lblPiratePlaystyleDescription.Font = font_3;
            lblPiratePlaystyleDescription.Location = new Point(10, 230);
            lblPiratePlaystyleDescription.MaximumSize = new Size(340, 350);
            lblPiratePlaystyleDescription.Size = new Size(340, 350);
            picStartNewGameYourEmpirePiratePlaystyle.Size = new Size(300, 300);
            picStartNewGameYourEmpirePiratePlaystyle.Location = new Point(360, 230);
            method_101(PiratePlayStyle.Balanced, bool_5: false);
            if (bool_2)
            {
                pnlStartNewGameYourEmpireGovernment.Visible = false;
                tbarStartNewGameYourEmpireHomeSystem.Visible = false;
                tbarStartNewGameYourEmpireSize.Visible = false;
                tbarStartNewGameYourEmpireCorruption.Visible = false;
                tbarStartNewGameYourEmpireTechLevel.Location = new Point(10, 123);
                lblVictoryPiratePlaystyle.Visible = true;
                cmbVictoryPiratePlayStyle.Visible = true;
                lblPiratePlaystyleDescription.Visible = true;
                picStartNewGameYourEmpirePiratePlaystyle.Visible = true;
            }
            else
            {
                pnlStartNewGameYourEmpireGovernment.Visible = true;
                tbarStartNewGameYourEmpireHomeSystem.Visible = true;
                tbarStartNewGameYourEmpireSize.Visible = true;
                tbarStartNewGameYourEmpireCorruption.Visible = true;
                tbarStartNewGameYourEmpireTechLevel.Location = new Point(10, 213);
                lblVictoryPiratePlaystyle.Visible = false;
                cmbVictoryPiratePlayStyle.Visible = false;
                lblPiratePlaystyleDescription.Visible = false;
                picStartNewGameYourEmpirePiratePlaystyle.Visible = false;
            }
            tbarStartNewGameYourEmpireHomeSystem.Location = new Point(10, 123);
            tbarStartNewGameYourEmpireSize.Location = new Point(10, 168);
            tbarStartNewGameYourEmpireCorruption.Location = new Point(10, 258);
            tbarStartNewGameYourEmpireHomeSystem.Size = new Size(650, 42);
            tbarStartNewGameYourEmpireSize.Size = new Size(650, 42);
            tbarStartNewGameYourEmpireTechLevel.Size = new Size(650, 42);
            tbarStartNewGameYourEmpireCorruption.Size = new Size(650, 42);
            tbarStartNewGameYourEmpireHomeSystem.Setup();
            tbarStartNewGameYourEmpireSize.Setup();
            tbarStartNewGameYourEmpireTechLevel.Setup();
            tbarStartNewGameYourEmpireCorruption.Setup();
            tbarStartNewGameYourEmpireHomeSystem.LinkWidth = 0;
            tbarStartNewGameYourEmpireSize.LinkWidth = 0;
            tbarStartNewGameYourEmpireTechLevel.LinkWidth = 0;
            tbarStartNewGameYourEmpireCorruption.LinkWidth = 0;
            tbarStartNewGameYourEmpireHomeSystem.LinkText = string.Empty;
            tbarStartNewGameYourEmpireSize.LinkText = string.Empty;
            tbarStartNewGameYourEmpireTechLevel.LinkText = string.Empty;
            tbarStartNewGameYourEmpireCorruption.LinkText = string.Empty;
            tbarStartNewGameYourEmpireHomeSystem.SetLabels(new string[5]
            {
            TextResolver.GetText("Harsh"),
            TextResolver.GetText("Trying"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("Agreeable"),
            TextResolver.GetText("Excellent")
            });
            tbarStartNewGameYourEmpireSize.SetLabels(new string[6]
            {
            TextResolver.GetText("Random"),
            TextResolver.GetText("Starting"),
            TextResolver.GetText("Young"),
            TextResolver.GetText("Expanding"),
            TextResolver.GetText("Mature"),
            TextResolver.GetText("Old")
            });
            tbarStartNewGameYourEmpireTechLevel.SetLabels(new string[9]
            {
            TextResolver.GetText("PreWarp"),
            TextResolver.GetText("Normal"),
            string.Format(TextResolver.GetText("Level X"), "1"),
            string.Format(TextResolver.GetText("Level X"), "2"),
            string.Format(TextResolver.GetText("Level X"), "3"),
            string.Format(TextResolver.GetText("Level X"), "4"),
            string.Format(TextResolver.GetText("Level X"), "5"),
            string.Format(TextResolver.GetText("Level X"), "6"),
            string.Format(TextResolver.GetText("Level X"), "7")
            });
            tbarStartNewGameYourEmpireCorruption.SetLabels(new string[4]
            {
            TextResolver.GetText("Low"),
            TextResolver.GetText("Normal"),
            TextResolver.GetText("High"),
            TextResolver.GetText("Very High")
            });
            if (bool_5)
            {
                tbarStartNewGameYourEmpireHomeSystem.Value = 2;
                tbarStartNewGameYourEmpireCorruption.Value = 1;
            }
            picStartNewGameYourEmpireImage.Size = new Size(220, 585);
            picStartNewGameYourEmpireImage.Location = new Point(670, 8);
            picStartNewGameYourEmpireImage.SizeMode = PictureBoxSizeMode.Zoom;
            btnStartNewGameYourEmpirePrevious.Font = font_7;
            btnStartNewGameYourEmpireNext.Font = font_7;
            btnStartNewGameYourEmpirePrevious.Size = new Size(300, 40);
            btnStartNewGameYourEmpireNext.Size = new Size(300, 40);
            btnStartNewGameYourEmpirePrevious.Location = new Point(10, pnlStartNewGameYourEmpire.Height - (btnStartNewGameYourEmpirePrevious.Height + 10));
            btnStartNewGameYourEmpireNext.Location = new Point(pnlStartNewGameYourEmpire.Width - (btnStartNewGameYourEmpireNext.Width + 10), pnlStartNewGameYourEmpire.Height - (btnStartNewGameYourEmpireNext.Height + 10));
            pnlStartNewGameYourEmpire.ResumeLayout();
        }

        private void method_41()
        {
            pnlStartNewGameYourEmpireType.SuspendLayout();
            _ = (pnlStartNewGameYourEmpireType.Width - 876) / 2;
            int num = (pnlStartNewGameYourEmpireType.Height - 630) / 2;
            num -= 10;
            pnlStartNewGameIntroductoryBorder.Size = new Size(434, 104);
            pnlStartNewGameIntroductoryBorder.Location = new Point(233, 14);
            pnlStartNewGameIntroductoryBorder.BorderStyle = BorderStyle.None;
            pnlStartNewGameIntroductoryBorder.BorderWidth = 8;
            pnlStartNewGameIntroductoryBorder.CornerCurveRadius = 10;
            btnStartNewGameIntroductory.Size = new Size(420, 90);
            btnStartNewGameIntroductory.Location = new Point(7, 7);
            btnStartNewGameIntroductory.Font = font_9;
            toolTip.SetToolTip(btnStartNewGameIntroductory, TextResolver.GetText("Start New Game Description - Introductory Game"));
            Size size = new Size(140, 230);
            int num2 = 10;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Size = size;
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Location = new Point(10, 140);
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Text = TextResolver.GetText("Start New Game - Ancient Galaxy") + ">>";
            btnStartNewGameYourEmpireTypeTheAncientGalaxy.Font = font_7;
            num2 = 10 + (size.Width + 8);
            btnStartNewGameYourEmpireTypePirateShadows.Size = size;
            btnStartNewGameYourEmpireTypePirateShadows.Location = new Point(num2, 140);
            btnStartNewGameYourEmpireTypePirateShadows.Text = TextResolver.GetText("Start New Game - Shadows Pirate") + ">>";
            btnStartNewGameYourEmpireTypePirateShadows.Font = font_7;
            num2 += size.Width + 8;
            btnStartNewGameYourEmpireTypeNormalShadows.Size = size;
            btnStartNewGameYourEmpireTypeNormalShadows.Location = new Point(num2, 140);
            btnStartNewGameYourEmpireTypeNormalShadows.Text = TextResolver.GetText("Start New Game - Shadows Standard") + ">>";
            btnStartNewGameYourEmpireTypeNormalShadows.Font = font_7;
            num2 += size.Width + 8;
            btnStartNewGameYourEmpireTypeClassicEra.Size = size;
            btnStartNewGameYourEmpireTypeClassicEra.Location = new Point(num2, 140);
            btnStartNewGameYourEmpireTypeClassicEra.Text = TextResolver.GetText("Start New Game - Classic Era") + ">>";
            btnStartNewGameYourEmpireTypeClassicEra.Font = font_7;
            num2 += size.Width + 8;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Size = size;
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Location = new Point(num2, 140);
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Text = TextResolver.GetText("Start New Game - Return of the Shakturi") + ">>";
            btnStartNewGameYourEmpireTypeReturnOfTheShakturi.Font = font_7;
            num2 += size.Width + 8;
            btnStartNewGameYourEmpireTypeLegends.Size = size;
            btnStartNewGameYourEmpireTypeLegends.Location = new Point(num2, 140);
            btnStartNewGameYourEmpireTypeLegends.Text = TextResolver.GetText("Start New Game - Legends") + ">>";
            btnStartNewGameYourEmpireTypeLegends.Font = font_7;
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeTheAncientGalaxy, TextResolver.GetText("Start New Game Description - Ancient Galaxy"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypePirateShadows, TextResolver.GetText("Start New Game Description - Shadows Pirate"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeNormalShadows, TextResolver.GetText("Start New Game Description - Shadows Standard"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeClassicEra, TextResolver.GetText("Start New Game Description - Classic Era"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeReturnOfTheShakturi, TextResolver.GetText("Start New Game Description - Return of the Shakturi"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeLegends, TextResolver.GetText("Start New Game Description - Legends"));
            picStartNewGameYourEmpireTypeTimeline.Size = new Size(880, 150);
            picStartNewGameYourEmpireTypeTimeline.Location = new Point(10, 370);
            btnStartNewGameYourEmpireTypeNormalClassic.Size = new Size(340, 100);
            btnStartNewGameYourEmpireTypeNormalClassic.Location = new Point(70, 540);
            btnStartNewGameYourEmpireTypeNormalClassic.Text = TextResolver.GetText("Start New Game - Custom Standard") + ">>";
            btnStartNewGameYourEmpireTypeNormalClassic.Font = font_7;
            btnStartNewGameYourEmpireTypePirateClassic.Size = new Size(340, 100);
            btnStartNewGameYourEmpireTypePirateClassic.Location = new Point(490, 540);
            btnStartNewGameYourEmpireTypePirateClassic.Text = TextResolver.GetText("Start New Game - Custom Pirate") + ">>";
            btnStartNewGameYourEmpireTypePirateClassic.Font = font_7;
            lblStartNewGameActiveTheme.TextAlign = ContentAlignment.MiddleCenter;
            using (Graphics graphics = lblStartNewGameActiveTheme.CreateGraphics())
            {
                int num3 = (900 - (int)graphics.MeasureString(lblStartNewGameActiveTheme.Text, font_6).Width) / 2;
                lblStartNewGameActiveTheme.Location = new Point(num3, 640);
            }
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypeNormalClassic, TextResolver.GetText("Start New Game Description - Custom Standard"));
            toolTip.SetToolTip(btnStartNewGameYourEmpireTypePirateClassic, TextResolver.GetText("Start New Game Description - Custom Pirate"));
            btnStartNewGameYourEmpireTypeQuickStarts.Visible = false;
            pnlStartNewGameYourEmpireType.ResumeLayout();
        }

        private void method_42()
        {
            pnlStartNewGameYourRace.SuspendLayout();
            pnlStartNewGameYourEmpireRace.Location = new Point(5, 5);
            pnlStartNewGameYourEmpireRace.Size = new Size(750, 595);
            lblStartNewGameYourEmpireRaceTitle.Visible = false;
            cmbStartNewGameYourEmpireRace.Size = new Size(300, 26);
            cmbStartNewGameYourEmpireRace.ItemHeight = 26;
            cmbStartNewGameYourEmpireRace.Location = new Point(15, 20);
            picStartNewGameYourEmpireRace.Size = new Size(300, 300);
            picStartNewGameYourEmpireRace.Location = new Point(15, 57);
            picStartNewGameYourEmpireRace.SizeMode = PictureBoxSizeMode.Zoom;
            picStartNewGameYourEmpireRace.BorderStyle = BorderStyle.None;
            lnkStartNewGameYourEmpireRace.Location = new Point(15, 365);
            lnkStartNewGameYourEmpireRace.Font = font_3;
            lblStartNewGameYourEmpireRaceName.Location = new Point(325, 20);
            lblStartNewGameYourEmpireRaceName.Visible = true;
            lblStartNewGameYourEmpireRaceName.BringToFront();
            pnlStartNewGameYourEmpireRaceAttributesContainer.Location = new Point(330, 57);
            pnlStartNewGameYourEmpireRaceAttributesContainer.Size = new Size(410, 525);
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoScroll = true;
            pnlStartNewGameYourEmpireRaceAttributesContainer.SetAutoScrollMargin(0, 0);
            pnlStartNewGameYourEmpireRaceAttributesContainer.AutoScrollPosition = new Point(0, 0);
            pnlStartNewGameYourEmpireRaceAttributes.Location = new Point(0, 0);
            pnlStartNewGameYourEmpireRaceAttributes.Size = new Size(390, 0);
            pnlStartNewGameYourEmpireRaceAttributes.MaximumSize = new Size(390, 2000);
            pnlStartNewGameYourEmpireRaceAttributes.MinimumSize = new Size(390, 205);
            pnlStartNewGameYourEmpireRaceAttributes.AutoSize = true;
            picStartNewGameYourRaceImage.Size = new Size(130, 595);
            picStartNewGameYourRaceImage.Location = new Point(760, 5);
            picStartNewGameYourRaceImage.SizeMode = PictureBoxSizeMode.Zoom;
            cmbStartNewGameYourEmpireRace.BindData(font_3, raceList_0, main_0.raceImageCache_0.GetRaceImages(), allowRandomRace: true);
            btnStartNewGameYourRacePrevious.Font = font_7;
            btnStartNewGameYourRaceNext.Font = font_7;
            btnStartNewGameYourRacePrevious.Size = new Size(300, 40);
            btnStartNewGameYourRaceNext.Size = new Size(300, 40);
            btnStartNewGameYourRacePrevious.Location = new Point(10, pnlStartNewGameYourRace.Height - (btnStartNewGameYourRacePrevious.Height + 10));
            btnStartNewGameYourRaceNext.Location = new Point(pnlStartNewGameYourRace.Width - (btnStartNewGameYourRaceNext.Width + 10), pnlStartNewGameYourRace.Height - (btnStartNewGameYourRaceNext.Height + 10));
            btnStartNewGameYourRaceNext.Visible = true;
            btnStartNewGameYourRaceNext.BringToFront();
            btnStartNewGameYourRacePrevious.Visible = true;
            btnStartNewGameYourRacePrevious.BringToFront();
            pnlStartNewGameYourRace.ResumeLayout();
        }

        private void method_43()
        {
            pnlStartNewGameOtherEmpires.SuspendLayout();
            pnlStartNewGameOtherEmpiresAutoGen.Size = new Size(350, 75);
            pnlStartNewGameOtherEmpiresAutoGen.Location = new Point(10, 10);
            using (Graphics graphics = CreateGraphics())
            {
                string text = TextResolver.GetText("Generate");
                string text2 = TextResolver.GetText("starting empires");
                SizeF sizeF = graphics.MeasureString(text, font_3);
                graphics.MeasureString(text2, font_3);
                numAutogenerateEmpiresAmount.Location = new Point(142, 41);
                Point location = new Point(Math.Max(0, 142 - ((int)sizeF.Width + 5)), 43);
                Point location2 = new Point(142 + numAutogenerateEmpiresAmount.Width + 5, 43);
                lblStartNewGameOtherEmpiresAutoGenNumberDescrip1.Location = location;
                lblStartNewGameOtherEmpiresAutoGenNumberDescrip2.Location = location2;
            }
            lblStartNewGameOtherEmpiresAutoGenNumberDescrip1.Font = font_3;
            lblStartNewGameOtherEmpiresAutoGenNumberDescrip2.Font = font_3;
            lblStartNewGameOtherEmpiresOR.Font = font_7;
            lblStartNewGameOtherEmpiresOR.Location = new Point(20, 95);
            lblStartNewGameOtherEmpiresOR.Text = TextResolver.GetText("OR specify the starting empires below...");
            pnlStartNewGameOtherEmpiresList.Size = new Size(882, 307);
            pnlStartNewGameOtherEmpiresList.Location = new Point(10, 125);
            chkOtherEmpiresAutogenerate.Location = new Point(10, 10);
            chkOtherEmpiresAutogenerate.Font = font_3;
            ctlStartingEmpiresList.Grid.Rows.Clear();
            ctlStartingEmpiresList.Location = new Point(10, 35);
            ctlStartingEmpiresList.Size = new Size(860, 260);
            ctlStartingEmpiresList.Grid.Columns["Name"].Width = 170;
            ctlStartingEmpiresList.Grid.Columns["Race"].Width = 110;
            ctlStartingEmpiresList.Grid.Columns["Government"].Width = 135;
            ctlStartingEmpiresList.Grid.Columns["Size"].Width = 80;
            ctlStartingEmpiresList.Grid.Columns["TechLevel"].Width = 110;
            ctlStartingEmpiresList.Grid.Columns["HomeSystem"].Width = 95;
            ctlStartingEmpiresList.Grid.Columns["Proximity"].Width = 110;
            ctlStartingEmpiresList.Grid.Columns["Remove"].Width = 30;
            ctlStartingEmpiresList.RemoveImage = bitmap_11;
            btnAddNewEmpire.Size = new Size(200, 25);
            btnAddNewEmpire.Location = new Point(670, 10);
            chkGalaxyNewEmpiresDuringGame.Location = new Point(10, 440);
            chkGalaxyNewEmpiresDuringGame.Font = font_3;
            chkGalaxyNewEmpiresDuringGame.Text = TextResolver.GetText("Allow independent alien colonies to start new empires during the game");
            picStartNewGameOtherEmpiresImageBottom.Size = new Size(880, 130);
            picStartNewGameOtherEmpiresImageBottom.Location = new Point(10, 470);
            picStartNewGameOtherEmpiresImageBottom.SizeMode = PictureBoxSizeMode.CenterImage;
            btnStartNewGameOtherEmpiresPrevious.Font = font_7;
            btnStartNewGameOtherEmpiresNext.Font = font_7;
            btnStartNewGameOtherEmpiresPrevious.Size = new Size(300, 40);
            btnStartNewGameOtherEmpiresNext.Size = new Size(300, 40);
            btnStartNewGameOtherEmpiresPrevious.Location = new Point(10, pnlStartNewGameOtherEmpires.Height - (btnStartNewGameOtherEmpiresPrevious.Height + 10));
            btnStartNewGameOtherEmpiresNext.Location = new Point(pnlStartNewGameOtherEmpires.Width - (btnStartNewGameOtherEmpiresNext.Width + 10), pnlStartNewGameOtherEmpires.Height - (btnStartNewGameOtherEmpiresNext.Height + 10));
            pnlStartNewGameOtherEmpires.ResumeLayout();
        }

        private void method_44()
        {
            pnlStartNewGameVictoryConditions.SuspendLayout();
            chkVictoryTerritory.Font = font_3;
            chkVictoryPopulation.Font = font_3;
            chkVictoryEconomy.Font = font_3;
            chkVictoryTimeLimit.Font = font_3;
            chkVictoryTimeStart.Font = font_3;
            chkVictoryEnableRaceSpecificConditions.Font = font_3;
            pnlStartNewGameVictoryConditionsGroup.Size = new Size(880, 155);
            pnlStartNewGameVictoryConditionsGroup.Location = new Point(10, 10);
            pnlStartNewGameVictoryConditionsGroup.BackColor = Color.FromArgb(80, 0, 40);
            chkVictoryTerritory.Location = new Point(355, 12);
            chkVictoryPopulation.Location = new Point(355, 37);
            chkVictoryEconomy.Location = new Point(355, 62);
            chkVictoryEnableRaceSpecificConditions.Location = new Point(355, 87);
            cmbVictoryThresholdPercentage.Size = new Size(60, 21);
            cmbVictoryThresholdPercentage.Location = new Point(355, 117);
            lblVictoryThresholdPercentage.Location = new Point(421, 119);
            lblVictoryThresholdPercentage.Font = font_7;
            numVictoryTerritoryPercent.Location = new Point(830, 12);
            numVictoryPopulationPercent.Location = new Point(830, 37);
            numVictoryEconomyPercent.Location = new Point(830, 62);
            lblVictorySandbox.Font = font_6;
            lblVictorySandbox.Location = new Point(8, 8);
            lblVictorySandbox.Size = new Size(339, 139);
            lblVictorySandbox.MaximumSize = new Size(339, 139);
            chkVictoryTimeStart.Location = new Point(365, 175);
            chkVictoryTimeLimit.Location = new Point(365, 200);
            numVictoryTimeStartYears.Location = new Point(840, 175);
            numVictoryTimeLimitYears.Location = new Point(840, 200);
            chkVictoryEconomy.AutoSize = true;
            chkVictoryEconomy.MaximumSize = new Size(550, 48);
            chkVictoryTerritory.Text = string.Format(TextResolver.GetText("TERRITORY control X"), "XX");
            chkVictoryPopulation.Text = string.Format(TextResolver.GetText("POPULATION control X"), "XX");
            chkVictoryEconomy.Text = string.Format(TextResolver.GetText("ECONOMY control X"), "XX");
            chkVictoryTimeLimit.Text = string.Format(TextResolver.GetText("TIME LIMIT X"), "XX");
            chkVictoryTimeStart.Text = string.Format(TextResolver.GetText("Victory Conditions X"), "XX");
            chkVictoryEnableRaceSpecificConditions.Text = TextResolver.GetText("Enable Race-specific Victory Conditions");
            lblVictoryThresholdPercentage.Text = TextResolver.GetText("Victory Threshold Percent");
            chkVictoryTimeLimit.BringToFront();
            chkVictoryTimeStart.BringToFront();
            chkVictoryEnableRaceSpecificConditions.BringToFront();
            chkVictoryEnableRaceSpecificEvents.BringToFront();
            cmbVictoryThresholdPercentage.BringToFront();
            chkStoryDistantWorlds.Font = font_3;
            chkStoryDistantWorlds.Location = new Point(20, 245);
            chkStoryDistantWorlds.CheckAlign = ContentAlignment.TopLeft;
            chkStoryReturnOfTheShakturi.Font = font_3;
            chkStoryReturnOfTheShakturi.Location = new Point(20, 270);
            chkStoryReturnOfTheShakturi.AutoSize = false;
            chkStoryReturnOfTheShakturi.MaximumSize = new Size(800, 21);
            chkStoryReturnOfTheShakturi.Size = new Size(800, 21);
            chkStoryReturnOfTheShakturi.CheckAlign = ContentAlignment.TopLeft;
            chkStoryReturnOfTheShakturi.TextAlign = ContentAlignment.MiddleLeft;
            chkStoryReturnOfTheShakturi.BringToFront();
            chkVictoryEnableDisasterEvents.Text = TextResolver.GetText("Enable Disasters and other events");
            chkVictoryEnableDisasterEvents.Font = font_3;
            chkVictoryEnableDisasterEvents.Location = new Point(20, 295);
            chkVictoryEnableDisasterEvents.AutoSize = false;
            chkVictoryEnableDisasterEvents.MaximumSize = new Size(800, 21);
            chkVictoryEnableDisasterEvents.Size = new Size(800, 21);
            chkVictoryEnableDisasterEvents.CheckAlign = ContentAlignment.TopLeft;
            chkVictoryEnableDisasterEvents.TextAlign = ContentAlignment.MiddleLeft;
            chkVictoryEnableDisasterEvents.BringToFront();
            chkVictoryEnableRaceSpecificEvents.Text = TextResolver.GetText("Enable Race-specific events");
            chkVictoryEnableRaceSpecificEvents.Font = font_3;
            chkVictoryEnableRaceSpecificEvents.Location = new Point(20, 320);
            chkVictoryEnableRaceSpecificEvents.AutoSize = false;
            chkVictoryEnableRaceSpecificEvents.MaximumSize = new Size(800, 21);
            chkVictoryEnableRaceSpecificEvents.Size = new Size(800, 21);
            chkVictoryEnableRaceSpecificEvents.CheckAlign = ContentAlignment.TopLeft;
            chkVictoryEnableRaceSpecificEvents.TextAlign = ContentAlignment.MiddleLeft;
            chkVictoryEnableRaceSpecificEvents.BringToFront();
            chkStoryShadows.Font = font_3;
            chkStoryShadows.Location = new Point(20, 345);
            chkStoryShadows.AutoSize = false;
            chkStoryShadows.MaximumSize = new Size(800, 21);
            chkStoryShadows.Size = new Size(800, 21);
            chkStoryShadows.CheckAlign = ContentAlignment.TopLeft;
            chkStoryShadows.TextAlign = ContentAlignment.MiddleLeft;
            chkStoryShadows.BringToFront();
            chkStartNewGameEnableTechTrading.Font = font_3;
            chkStartNewGameEnableTechTrading.Location = new Point(pnlStartNewGameVictoryConditions.Width - (chkStartNewGameEnableTechTrading.Width + 20), 320);
            chkStartNewGameEnableTechTrading.CheckAlign = ContentAlignment.TopRight;
            chkStartNewGameEnableTechTrading.BringToFront();
            chkStartNewGameEnableGiantKaltors.Font = font_3;
            chkStartNewGameEnableGiantKaltors.Location = new Point(pnlStartNewGameVictoryConditions.Width - (chkStartNewGameEnableGiantKaltors.Width + 20), 345);
            chkStartNewGameEnableGiantKaltors.CheckAlign = ContentAlignment.TopRight;
            chkStartNewGameEnableGiantKaltors.BringToFront();
            picStartNewGameVictoryConditionsImage.Size = new Size(870, 220);
            picStartNewGameVictoryConditionsImage.Location = new Point(15, 375);
            btnStartNewGameVictoryConditionsPrevious.Font = font_7;
            btnStartNewGameStart.Font = font_7;
            btnStartNewGameVictoryConditionsPrevious.Size = new Size(300, 40);
            btnStartNewGameStart.Size = new Size(300, 40);
            btnStartNewGameVictoryConditionsPrevious.Location = new Point(10, pnlStartNewGameVictoryConditions.Height - (btnStartNewGameVictoryConditionsPrevious.Height + 10));
            btnStartNewGameStart.Location = new Point(pnlStartNewGameVictoryConditions.Width - (btnStartNewGameStart.Width + 10), pnlStartNewGameVictoryConditions.Height - (btnStartNewGameStart.Height + 10));
            pnlStartNewGameVictoryConditions.ResumeLayout();
        }

        private void method_45(bool bool_5)
        {
            if (bool_5)
            {
                lblVictoryPiratePlaystyle.Visible = true;
                cmbVictoryPiratePlayStyle.Visible = true;
                chkStoryReturnOfTheShakturi.Checked = false;
                chkStoryReturnOfTheShakturi.Enabled = false;
                lblVictorySandbox.Text = TextResolver.GetText("Victory Conditions Explanation Pirate");
                chkVictoryEnableRaceSpecificConditions.Text = TextResolver.GetText("Enable Pirate-specific Victory Conditions");
            }
            else
            {
                lblVictoryPiratePlaystyle.Visible = false;
                cmbVictoryPiratePlayStyle.Visible = false;
                chkStoryReturnOfTheShakturi.Enabled = true;
                lblVictorySandbox.Text = TextResolver.GetText("Victory Conditions Explanation");
                chkVictoryEnableRaceSpecificConditions.Text = TextResolver.GetText("Enable Race-specific Victory Conditions");
            }
        }

        private void method_46()
        {
            pnlNewGame.Visible = false;
        }

        private void method_47(object sender, EventArgs e)
        {
            main_0.gameOptions_0.StartGameOptions = method_195();
            main_0.YxwyUefOyQ();
            main_0.method_257();
            method_46();
        }

        private Race method_48(Galaxy galaxy_0, string string_2, DistantWorlds.Types.EmpireList empireList_0, bool bool_5)
        {
            //Race race = null;
            if (string_2 == "(" + TextResolver.GetText("Random") + ")")
            {
                RaceList raceList = new RaceList();
                if (empireList_0 != null)
                {
                    for (int i = 0; i < empireList_0.Count; i++)
                    {
                        if (empireList_0[i].DominantRace != null && !raceList.Contains(empireList_0[i].DominantRace))
                        {
                            raceList.Add(empireList_0[i].DominantRace);
                        }
                    }
                }
                RaceList raceList2 = new RaceList();
                for (int j = 0; j < galaxy_0.Races.Count; j++)
                {
                    if (!galaxy_0.Races[j].Playable || raceList.Contains(galaxy_0.Races[j]))
                    {
                        continue;
                    }
                    if (!bool_5)
                    {
                        if (galaxy_0.Races[j].CanBeNormalEmpire)
                        {
                            raceList2.Add(galaxy_0.Races[j]);
                        }
                    }
                    else
                    {
                        raceList2.Add(galaxy_0.Races[j]);
                    }
                }
                if (raceList2.Count > 0)
                {
                    return raceList2[Galaxy.Rnd.Next(0, raceList2.Count)];
                }
                return galaxy_0.SelectRandomRace(0);
            }
            int index = method_103(galaxy_0, string_2);
            return galaxy_0.Races[index];
        }

        private double method_49(Galaxy galaxy_0, int int_1)
        {
            double num = (double)galaxy_0.StarCount / 700.0;
            double num2 = Galaxy.DetermineEmpireExpansion(Galaxy.Rnd, int_1);
            num2 *= num;
            return Math.Sqrt(num2) * galaxy_0.TypicalDistanceBetweenColoniesAtMaximumFill / 2.0;
        }

        private bool method_50(Galaxy galaxy_0, Habitat habitat_0)
        {
            bool result = false;
            for (int i = 0; i < galaxy_0.Systems[habitat_0.SystemIndex].Habitats.Count; i++)
            {
                Habitat habitat = galaxy_0.Systems[habitat_0.SystemIndex].Habitats[i];
                if (habitat.Population != null && habitat.Population.Count > 0 && habitat.Empire != null && habitat.Empire != galaxy_0.IndependentEmpire)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private Habitat method_51(Galaxy galaxy_0, Race race_0, string string_2, Habitat habitat_0, HabitatType habitatType_0, bool bool_5, int int_1, Sector sector_0)
        {
            Habitat habitat = null;
            bool flag = false;
            if (string_2 == "(" + TextResolver.GetText("Random") + ")")
            {
                double num = (double)Galaxy.SectorSize * 2.0;
                num = 0.75 * ((double)Galaxy.SizeX / (Math.Sqrt(int_1) - 1.0));
                int num2 = 0;
                while (!flag && num2 < 200)
                {
                    double double_ = Math.Min(1.0, (double)num2 / 100.0);
                    double double_2;
                    double double_3;
                    if (bool_5)
                    {
                        method_93(galaxy_0, 0.0, 1.0, out double_2, out double_3);
                    }
                    else if (num2 == 0)
                    {
                        method_84(galaxy_0, race_0, 0.0, 1.0, out double_2, out double_3, bool_5: false, double_);
                    }
                    else
                    {
                        method_84(galaxy_0, race_0, 0.0, 1.0, out double_2, out double_3, bool_5: true, double_);
                    }
                    habitat = galaxy_0.FindNearestUncolonizedHabitat(double_2, double_3, habitatType_0);
                    if (habitat != null)
                    {
                        GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsAtPoint(habitat.Xpos, habitat.Ypos, GalaxyLocationType.NebulaCloud);
                        if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                        {
                            habitat = null;
                            flag = false;
                        }
                        if (habitat != null)
                        {
                            Habitat habitat2 = galaxy_0.FindNearestColony(habitat.Xpos, habitat.Ypos, null, 0, includeIndependentColonies: false);
                            double num3 = 0.0;
                            if (habitat2 != null)
                            {
                                num3 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, habitat2.Xpos, habitat2.Ypos);
                                if (num3 < num)
                                {
                                    habitat = null;
                                    flag = false;
                                }
                            }
                        }
                        if (habitat != null)
                        {
                            Habitat systemStar = Galaxy.DetermineHabitatSystemStar(habitat);
                            if (galaxy_0.Systems[systemStar].PlanetCount >= 4)
                            {
                                flag = true;
                            }
                        }
                        if (race_0 != null && habitat != null)
                        {
                            GalaxyLocation galaxyLocation = galaxy_0.DetermineRaceRegion(race_0);
                            if (galaxyLocation != null)
                            {
                                galaxyLocation.ResolveLocationCenter(out var x, out var y);
                                double num4 = galaxy_0.CalculateDistance(x, y, habitat.Xpos, habitat.Ypos);
                                double num5 = (double)galaxyLocation.Width * 0.4;
                                if (num4 > num5)
                                {
                                    habitat = null;
                                    flag = false;
                                }
                            }
                        }
                    }
                    num2++;
                }
                if (!flag && habitat == null)
                {
                    method_84(galaxy_0, race_0, 0.0, 1.0, out var double_4, out var double_5, bool_5: false, 0.0);
                    Habitat habitat3 = galaxy_0.FindNearestHabitat(double_4, double_5, HabitatType.MainSequence);
                    if (galaxy_0.Systems[habitat3].PlanetCount == 0 && habitat3.Name.Length <= 5)
                    {
                        galaxy_0.AssignSystemName(habitat3, 1);
                    }
                    galaxy_0.SetColonizableHabitatsInSystem(galaxy_0, habitat3, race_0, 1);
                    habitat = galaxy_0.FindNearestUncolonizedHabitat(habitat3.Xpos, habitat3.Ypos, habitatType_0);
                    if (habitat == null)
                    {
                        galaxy_0.SetColonizableHabitatsInSystem(galaxy_0, habitat3, race_0, 3);
                        habitat = galaxy_0.FindNearestUncolonizedHabitat(habitat3.Xpos, habitat3.Ypos, habitatType_0);
                    }
                }
            }
            else
            {
                int num6 = 0;
                double double_6 = 0.0;
                double double_7 = 0.0;
                double num7 = 0.7;
                double num8 = 0.0;
                double num9 = 0.0;
                double num10 = Galaxy.Rnd.NextDouble() * (double)Galaxy.SectorSize * 0.1;
                if (sector_0 != null)
                {
                    num8 = (double)sector_0.X * (double)Galaxy.SectorSize + (double)Galaxy.SectorSize * 0.5;
                    num9 = (double)sector_0.Y * (double)Galaxy.SectorSize + (double)Galaxy.SectorSize * 0.5;
                    double_6 = num8;
                    double_7 = num9;
                    num7 = 0.3;
                }
                while (!flag && num6 < 200)
                {
                    if (sector_0 != null)
                    {
                        double num11 = galaxy_0.SelectRandomHeading();
                        double num12 = num8 + Math.Cos(num11) * num10;
                        double num13 = num9 + Math.Sin(num11) * num10;
                        double num14 = num10;
                        while (!method_90(num12, num13))
                        {
                            num11 = galaxy_0.SelectRandomHeading();
                            num14 *= 0.9;
                            num12 = num8 + Math.Cos(num11) * num14;
                            num13 = num9 + Math.Sin(num11) * num14;
                        }
                        num10 *= 1.3;
                        double_6 = num12;
                        double_7 = num13;
                    }
                    else
                    {
                        double double_8 = galaxy_0.SelectRandomHeading();
                        double double_9 = method_52(galaxy_0, string_2);
                        method_92(galaxy_0, double_8, habitat_0.Xpos, habitat_0.Ypos, double_9, out double_6, out double_7);
                        while (!method_90(double_6, double_7))
                        {
                            double_8 = galaxy_0.SelectRandomHeading();
                            double_9 = method_52(galaxy_0, string_2);
                            method_92(galaxy_0, double_8, habitat_0.Xpos, habitat_0.Ypos, double_9, out double_6, out double_7);
                        }
                    }
                    List<HabitatType> list = new List<HabitatType>();
                    if (!list.Contains(habitatType_0))
                    {
                        list.Add(habitatType_0);
                    }
                    habitat = galaxy_0.FastFindNearestPlanetMoonOfTypesUnoccupiedSystem(double_6, double_7, null, list);
                    if (habitat != null)
                    {
                        Habitat habitat4 = Galaxy.DetermineHabitatSystemStar(habitat);
                        bool flag2;
                        if (!(flag2 = method_50(galaxy_0, habitat4)) && bool_5 && habitat_0 != null)
                        {
                            double num15 = (double)Galaxy.SectorSize * 0.5;
                            double num16 = galaxy_0.CalculateDistance(habitat_0.Xpos, habitat_0.Ypos, habitat.Xpos, habitat.Ypos);
                            if (num16 < num15)
                            {
                                flag2 = true;
                            }
                        }
                        Habitat habitat5 = galaxy_0.FindNearestColony(double_6, double_7, null, 0, includeIndependentColonies: false);
                        double num17 = (double)Galaxy.SectorSize * num7;
                        if (habitat5 != null)
                        {
                            double num18 = galaxy_0.CalculateDistance(double_6, double_7, habitat5.Xpos, habitat5.Ypos);
                            if (num18 < num17)
                            {
                                flag2 = true;
                            }
                        }
                        if (galaxy_0.Systems[habitat4].PlanetCount >= 3 && !flag2)
                        {
                            flag = true;
                        }
                    }
                    num6++;
                }
                if (!flag)
                {
                    double double_10 = galaxy_0.SelectRandomHeading();
                    double double_11 = method_52(galaxy_0, string_2);
                    method_92(galaxy_0, double_10, habitat_0.Xpos, habitat_0.Ypos, double_11, out double_6, out double_7);
                    while (!method_90(double_6, double_7))
                    {
                        double_10 = galaxy_0.SelectRandomHeading();
                        double_11 = method_52(galaxy_0, string_2);
                        method_92(galaxy_0, double_10, habitat_0.Xpos, habitat_0.Ypos, double_11, out double_6, out double_7);
                    }
                    List<HabitatType> list2 = new List<HabitatType>();
                    list2.Add(race_0.NativeHabitatType);
                    habitat = galaxy_0.FastFindNearestPlanetMoonOfTypesUnoccupiedSystem(double_6, double_7, null, list2);
                    if (habitat == null)
                    {
                        int index = Galaxy.Rnd.Next(0, galaxy_0.Systems.Count);
                        while (galaxy_0.Systems[index].SystemStar.Category != 0)
                        {
                            index = Galaxy.Rnd.Next(0, galaxy_0.Systems.Count);
                        }
                        Habitat systemStar2 = galaxy_0.Systems[index].SystemStar;
                        galaxy_0.SetColonizableHabitatsInSystem(galaxy_0, systemStar2, race_0, 1);
                        habitat = galaxy_0.FindNearestUncolonizedHabitat(galaxy_0.Systems[index].SystemStar.Xpos, galaxy_0.Systems[index].SystemStar.Ypos, race_0.NativeHabitatType);
                        if (habitat == null)
                        {
                            galaxy_0.SetColonizableHabitatsInSystem(galaxy_0, systemStar2, race_0, 3);
                            habitat = galaxy_0.FindNearestUncolonizedHabitat(systemStar2.Xpos, systemStar2.Ypos, race_0.NativeHabitatType);
                        }
                    }
                }
            }
            return habitat;
        }

        private double method_52(Galaxy galaxy_0, string string_2)
        {
            Sector sector_ = null;
            return method_53(galaxy_0, string_2, out sector_);
        }

        private double method_53(Galaxy galaxy_0, string string_2, out Sector sector_0)
        {
            sector_0 = null;
            string text = TextResolver.GetText("Sector");
            //double num = 0.0;
            double num2 = 0.05;
            double num3 = 1.0;
            if (string_2 == TextResolver.GetText("Random"))
            {
                num2 = 0.05;
                num3 = 1.0;
            }
            else if (string_2 == TextResolver.GetText("Same System"))
            {
                num2 = 0.0;
                num3 = (double)Galaxy.SizeX / 20000000.0 * 0.0024;
            }
            else if (string_2 == TextResolver.GetText("Nearby"))
            {
                num2 = 0.03;
                num3 = 0.11;
            }
            else if (string_2 == TextResolver.GetText("Average"))
            {
                num2 = 0.11;
                num3 = 0.4;
            }
            else if (string_2 == TextResolver.GetText("Distant"))
            {
                num2 = 0.4;
                num3 = 1.0;
            }
            else if (string_2 == TextResolver.GetText("Random - not too close"))
            {
                num2 = 0.1;
                num3 = 1.0;
            }
            else if (string_2.StartsWith(text))
            {
                string text2 = string_2.Substring(text.Length + 1, string_2.Length - (text.Length + 1));
                text2 = text2.Trim();
                if (text2.Length > 1)
                {
                    char c = text2[0];
                    int num4 = c - 65;
                    string s = text2.Substring(1, text2.Length - 1);
                    int result = 0;
                    if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
                    {
                        result--;
                        sector_0 = new Sector(num4, result);
                    }
                }
            }
            double num5 = (double)Galaxy.SizeX * (num3 - num2);
            double num6 = (double)Galaxy.SizeX * num2;
            return num6 + num5 * Galaxy.Rnd.NextDouble();
        }

        private double method_54(string string_2)
        {
            double result = 0.0;
            if (string_2 == "(" + TextResolver.GetText("Random") + ")")
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                result = 1.0 + random.NextDouble() * 6.0;
            }
            else if (string_2 == TextResolver.GetText("PreWarp"))
            {
                result = 0.0;
            }
            else if (string_2 == TextResolver.GetText("Normal"))
            {
                result = 0.5;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "1"))
            {
                result = 1.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "2"))
            {
                result = 2.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "3"))
            {
                result = 3.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "4"))
            {
                result = 4.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "5"))
            {
                result = 5.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "6"))
            {
                result = 6.0;
            }
            else if (string_2 == string.Format(TextResolver.GetText("Level X"), "7"))
            {
                result = 7.0;
            }
            return result;
        }

        private string method_55(double double_1)
        {
            string text = "(" + TextResolver.GetText("Random") + ")";
            if (double_1 < 0.0)
            {
                return "(" + TextResolver.GetText("Random") + ")";
            }
            if (double_1 == 0.0)
            {
                return TextResolver.GetText("PreWarp");
            }
            if (double_1 == 0.5)
            {
                return TextResolver.GetText("Starting");
            }
            if (double_1 > 0.5 && double_1 <= 1.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "1");
            }
            if (double_1 > 1.0 && double_1 <= 2.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "2");
            }
            if (double_1 > 2.0 && double_1 <= 3.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "3");
            }
            if (double_1 > 3.0 && double_1 <= 4.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "4");
            }
            if (double_1 > 4.0 && double_1 <= 5.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "5");
            }
            if (double_1 > 5.0 && double_1 <= 6.0)
            {
                return string.Format(TextResolver.GetText("Level X"), "6");
            }
            return string.Format(TextResolver.GetText("Level X"), "7");
        }

        private string method_56(string string_2, Random random_0)
        {
            string result = TextResolver.GetText("Starting");
            int num = 0;
            if (string_2 == TextResolver.GetText("PreWarp"))
            {
                num = 0;
            }
            else
            {
                num = 0;
                if (string_2 == TextResolver.GetText("Starting"))
                {
                    num = 1;
                    //int num2 = 1;
                    goto IL_00e8;
                }
                if (string_2 == TextResolver.GetText("Young"))
                {
                    num = random_0.Next(1, 3);
                }
                else if (string_2 == TextResolver.GetText("Expanding"))
                {
                    num = random_0.Next(1, 4);
                }
                else if (string_2 == TextResolver.GetText("Mature"))
                {
                    num = random_0.Next(1, 5);
                }
                else if (string_2 == TextResolver.GetText("Old"))
                {
                    num = random_0.Next(1, 6);
                }
                switch (num)
                {
                    case 0:
                        break;
                    case 1:
                        goto IL_00e8;
                    case 2:
                        result = TextResolver.GetText("Young");
                        goto IL_0127;
                    case 3:
                        result = TextResolver.GetText("Expanding");
                        goto IL_0127;
                    case 4:
                        result = TextResolver.GetText("Mature");
                        goto IL_0127;
                    case 5:
                        result = TextResolver.GetText("Old");
                        goto IL_0127;
                    default:
                        goto IL_0127;
                }
            }
            result = TextResolver.GetText("PreWarp");
            goto IL_0127;
        IL_00e8:
            result = TextResolver.GetText("Starting");
            goto IL_0127;
        IL_0127:
            return result;
        }

        private int method_57(string string_2)
        {
            int result = 0;
            if (string_2 == "(" + TextResolver.GetText("Random") + ")")
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                int num = random.Next(1, 6);
                result = num;
            }
            else if (string_2 == TextResolver.GetText("PreWarp"))
            {
                result = 0;
            }
            else if (string_2 == TextResolver.GetText("Starting"))
            {
                result = 1;
            }
            else if (string_2 == TextResolver.GetText("Young"))
            {
                result = 2;
            }
            else if (string_2 == TextResolver.GetText("Expanding"))
            {
                result = 3;
            }
            else if (string_2 == TextResolver.GetText("Mature"))
            {
                result = 4;
            }
            else if (string_2 == TextResolver.GetText("Old"))
            {
                result = 5;
            }
            else if (string_2 == TextResolver.GetText("Supersize"))
            {
                result = 6;
            }
            return result;
        }

        private string method_58(int int_1)
        {
            string result = string.Empty;
            switch (int_1)
            {
                case -1:
                    result = "(" + TextResolver.GetText("Random") + ")";
                    break;
                case 0:
                    result = TextResolver.GetText("PreWarp");
                    break;
                case 1:
                    result = TextResolver.GetText("Starting");
                    break;
                case 2:
                    result = TextResolver.GetText("Young");
                    break;
                case 3:
                    result = TextResolver.GetText("Expanding");
                    break;
                case 4:
                    result = TextResolver.GetText("Mature");
                    break;
                case 5:
                    result = TextResolver.GetText("Old");
                    break;
                case 6:
                    result = TextResolver.GetText("Supersize");
                    break;
            }
            return result;
        }

        private GalaxyShape method_59(string string_2)
        {
            GalaxyShape result = GalaxyShape.Elliptical;
            if (string_2 == TextResolver.GetText("Elliptical"))
            {
                result = GalaxyShape.Elliptical;
            }
            else if (string_2 == TextResolver.GetText("Spiral"))
            {
                result = GalaxyShape.Spiral;
            }
            else if (string_2 == TextResolver.GetText("Ring"))
            {
                result = GalaxyShape.Ring;
            }
            else if (string_2 == TextResolver.GetText("Irregular"))
            {
                result = GalaxyShape.Irregular;
            }
            else if (string_2 == TextResolver.GetText("Even Clusters"))
            {
                result = GalaxyShape.ClustersEven;
            }
            else if (string_2 == TextResolver.GetText("Varied Clusters"))
            {
                result = GalaxyShape.ClustersVaried;
            }
            return result;
        }

        private int method_60(int int_1)
        {
            return BaconStart.method_60(int_1);
        }

        private int method_61(int int_1, RaceList raceList_2)
        {
            return BaconStart.method_61(int_1, raceList_2);
        }

        private double method_62(int int_1)
        {
            double result = 0.0;
            switch (int_1)
            {
                case 0:
                    result = 0.0;
                    break;
                case 1:
                    result = 0.3;
                    break;
                case 2:
                    result = 0.6;
                    break;
                case 3:
                    result = 1.0;
                    break;
            }
            return result;
        }

        private double method_63(int int_1)
        {
            double result = 1.1;
            switch (int_1)
            {
                case 0:
                    result = 0.9;
                    break;
                case 1:
                    result = 1.1;
                    break;
                case 2:
                    result = 1.3;
                    break;
                case 3:
                    result = 1.5;
                    break;
            }
            return result;
        }

        private double method_64(int int_1)
        {
            double result = 0.75;
            switch (int_1)
            {
                case 0:
                    result = 0.35;
                    break;
                case 1:
                    result = 0.5;
                    break;
                case 2:
                    result = 0.65;
                    break;
                case 3:
                    result = 0.82;
                    break;
                case 4:
                    result = 1.0;
                    break;
            }
            return result;
        }

        private double method_65(int int_1)
        {
            double result = 1.0;
            switch (int_1)
            {
                case 0:
                    result = 0.7;
                    break;
                case 1:
                    result = 0.85;
                    break;
                case 2:
                    result = 1.0;
                    break;
                case 3:
                    result = 1.15;
                    break;
                case 4:
                    result = 1.3;
                    break;
            }
            return result;
        }

        private double method_66(int int_1)
        {
            double result = 0.0;
            switch (int_1)
            {
                case 0:
                    result = 0.0;
                    break;
                case 1:
                    result = 0.07;
                    break;
                case 2:
                    result = 0.2;
                    break;
                case 3:
                    result = 0.4;
                    break;
                case 4:
                    result = 0.7;
                    break;
                case 5:
                    result = 1.0;
                    break;
            }
            return result;
        }

        private int method_67(int int_1)
        {
            return BaconStart.OverrideLowIndependentLifeValue(int_1);
        }

        private double method_68(int int_1)
        {
            double result = 0.0;
            switch (int_1)
            {
                case 0:
                    result = 0.167;
                    break;
                case 1:
                    result = 0.3;
                    break;
                case 2:
                    result = 0.5;
                    break;
                case 3:
                    result = 0.875;
                    break;
                case 4:
                    result = 1.5;
                    break;
            }
            return result;
        }

        private Size method_69(int int_1)
        {
            Size result = new Size(10, 10);
            switch (int_1)
            {
                case 0:
                    result = new Size(4, 4);
                    break;
                case 1:
                    result = new Size(6, 6);
                    break;
                case 2:
                    result = new Size(8, 8);
                    break;
                case 3:
                    result = new Size(10, 10);
                    break;
                case 4:
                    result = new Size(15, 15);
                    break;
            }
            return result;
        }

        private double method_70(int int_1)
        {
            double result = 0.0;
            switch (int_1)
            {
                case 0:
                    result = 0.7;
                    break;
                case 1:
                    result = 0.9;
                    break;
                case 2:
                    result = 1.1;
                    break;
                case 3:
                    result = 1.4;
                    break;
                case 4:
                    result = 1.7;
                    break;
            }
            return result;
        }

        private double method_71(int int_1)
        {
            double result = 0.0;
            switch (int_1)
            {
                case 0:
                    result = 0.9;
                    break;
                case 1:
                    result = 1.1;
                    break;
                case 2:
                    result = 1.3;
                    break;
                case 3:
                    result = 1.5;
                    break;
                case 4:
                    result = 1.7;
                    break;
            }
            return result;
        }

        private string method_72()
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            int selectedGovernmentId = cmbStartNewGameYourEmpireGovernment.SelectedGovernmentId;
            if (selectedGovernmentId >= 0 && selectedGovernmentId < Galaxy.GovernmentsStatic.Count)
            {
                GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[selectedGovernmentId];
                if (governmentAttributes != null)
                {
                    result = governmentAttributes.Name;
                }
            }
            return result;
        }

        private string method_73()
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            int selectedGovernmentId = cmbJumpStartYourEmpireGovernment.SelectedGovernmentId;
            if (selectedGovernmentId >= 0 && selectedGovernmentId < Galaxy.GovernmentsStatic.Count)
            {
                GovernmentAttributes governmentAttributes = Galaxy.GovernmentsStatic[selectedGovernmentId];
                if (governmentAttributes != null)
                {
                    result = governmentAttributes.Name;
                }
            }
            return result;
        }

        private string ahrJhtHrDu()
        {
            string result = TextResolver.GetText("Normal");
            switch (tbarStartNewGameYourEmpireHomeSystem.Value)
            {
                case 0:
                    result = TextResolver.GetText("Harsh");
                    break;
                case 1:
                    result = TextResolver.GetText("Trying");
                    break;
                case 2:
                    result = TextResolver.GetText("Normal");
                    break;
                case 3:
                    result = TextResolver.GetText("Agreeable");
                    break;
                case 4:
                    result = TextResolver.GetText("Excellent");
                    break;
            }
            return result;
        }

        private string method_74()
        {
            string result = "(" + TextResolver.GetText("Random") + ")";
            switch (tbarStartNewGameYourEmpireSize.Value)
            {
                case 0:
                    result = "(" + TextResolver.GetText("Random") + ")";
                    break;
                case 1:
                    result = TextResolver.GetText("Starting");
                    break;
                case 2:
                    result = TextResolver.GetText("Young");
                    break;
                case 3:
                    result = TextResolver.GetText("Expanding");
                    break;
                case 4:
                    result = TextResolver.GetText("Mature");
                    break;
                case 5:
                    result = TextResolver.GetText("Old");
                    break;
            }
            return result;
        }

        private string method_75()
        {
            string result = TextResolver.GetText("Normal");
            switch (tbarStartNewGameYourEmpireTechLevel.Value)
            {
                case 0:
                    result = TextResolver.GetText("PreWarp");
                    break;
                case 1:
                    result = TextResolver.GetText("Normal");
                    break;
                case 2:
                    result = string.Format(TextResolver.GetText("Level X"), "1");
                    break;
                case 3:
                    result = string.Format(TextResolver.GetText("Level X"), "2");
                    break;
                case 4:
                    result = string.Format(TextResolver.GetText("Level X"), "3");
                    break;
                case 5:
                    result = string.Format(TextResolver.GetText("Level X"), "4");
                    break;
                case 6:
                    result = string.Format(TextResolver.GetText("Level X"), "5");
                    break;
                case 7:
                    result = string.Format(TextResolver.GetText("Level X"), "6");
                    break;
                case 8:
                    result = string.Format(TextResolver.GetText("Level X"), "7");
                    break;
            }
            return result;
        }



        private void btnStartNewGameGalaxyMapsCustom_Click(object sender, EventArgs e)
        {
            pnlStartNewGameGalaxyMaps.Visible = false;
            pnlStartNewGameGalaxyMaps.SendToBack();
            pnlStartNewGameYourEmpireType.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Playstyle");
            btnStartNewGameYourEmpireTypeNormalShadows.Focus();
            pnlStartNewGameYourEmpireType.BringToFront();
            lblHelpTitle.Text = string.Empty;
            lblHelpDescription.Text = string.Empty;
        }

        private void method_221(GalaxySummary galaxySummary_0, EmpireSummary empireSummary_0)
        {
            using FileStream stream_ = new FileStream(galaxySummary_0.Filepath, FileMode.Open, FileAccess.Read);
            method_22(stream_, galaxySummary_0.Filename, TextResolver.GetText("Starting Prebuilt Galaxy") + "...");
            method_10();
            if (empireSummary_0 == null && galaxySummary_0.EmpireSummaries != null && galaxySummary_0.EmpireSummaries.Count > 0)
            {
                empireSummary_0 = galaxySummary_0.EmpireSummaries[0];
            }
            if (empireSummary_0 == null)
            {
                return;
            }
            double viewX = 0.0;
            double viewY = 0.0;
            Empire empireById = main_0._Game.Galaxy.GetEmpireById(empireSummary_0.EmpireId);
            if (empireById != null)
            {
                main_0._Game.PlayerEmpire = empireById;
                main_0._Game.Galaxy.PlayerEmpire = empireById;
                if (empireById.PirateEmpireBaseHabitat != null)
                {
                    if (empireById.SpacePorts != null && empireById.SpacePorts.Count > 0)
                    {
                        viewX = empireById.SpacePorts[0].Xpos;
                        viewY = empireById.SpacePorts[0].Ypos;
                    }
                    else
                    {
                        viewX = empireById.PirateEmpireBaseHabitat.Xpos;
                        viewY = empireById.PirateEmpireBaseHabitat.Ypos;
                    }
                }
                else if (empireById.Capital != null)
                {
                    viewX = empireById.Capital.Xpos;
                    viewY = empireById.Capital.Ypos;
                }
            }
            main_0._Game.ViewX = viewX;
            main_0._Game.ViewY = viewY;
            main_0._Game.ZoomFactor = 1.0;
            for (int i = 0; i < main_0._Game.Galaxy.Empires.Count; i++)
            {
                Empire empire = main_0._Game.Galaxy.Empires[i];
                if (empire != null)
                {
                    if (empire == main_0._Game.PlayerEmpire)
                    {
                        empire.SetAutomationSettings(main_0.gameOptions_0);
                    }
                    else
                    {
                        empire.SetAutomationSettingsFullyAutomated();
                    }
                }
            }
            for (int j = 0; j < main_0._Game.Galaxy.PirateEmpires.Count; j++)
            {
                Empire empire2 = main_0._Game.Galaxy.PirateEmpires[j];
                if (empire2 != null)
                {
                    if (empire2 == main_0._Game.PlayerEmpire)
                    {
                        empire2.SetAutomationSettings(main_0.gameOptions_0);
                    }
                    else
                    {
                        empire2.SetAutomationSettingsFullyAutomated();
                    }
                }
            }
            Application.UseWaitCursor = false;
            method_77(main_0._Game);
        }

        private void btnStartNewGameGalaxyMapsStart_Click(object sender, EventArgs e)
        {
            GalaxySummary selectedGalaxySummary = ctlStartNewGameGalaxyMapsGalaxies.SelectedGalaxySummary;
            EmpireSummary selectedEmpireSummary = ctlStartNewGameGalaxyMapsEmpires.SelectedEmpireSummary;
            if (selectedGalaxySummary != null && !string.IsNullOrEmpty(selectedGalaxySummary.Filename) && !string.IsNullOrEmpty(selectedGalaxySummary.Filepath) && selectedEmpireSummary != null)
            {
                method_221(selectedGalaxySummary, selectedEmpireSummary);
            }
        }

        private void ctlStartNewGameGalaxyMapsGalaxies_SelectionChanged(object sender, EventArgs e)
        {
            GalaxySummary selectedGalaxySummary = ctlStartNewGameGalaxyMapsGalaxies.SelectedGalaxySummary;
            if (selectedGalaxySummary != null)
            {
                string text = selectedGalaxySummary.Title;
                if (string.IsNullOrEmpty(text))
                {
                    text = selectedGalaxySummary.Filename;
                }
                lblStartNewGameGalaxyMapsAvailableFactions.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableEmpires"), text);
                pnlStartNewGameGalaxyMapsGalaxy.BindData(selectedGalaxySummary, font_9, font_3, font_7);
                ctlStartNewGameGalaxyMapsEmpires.BindData(selectedGalaxySummary.EmpireSummaries, raceList_1, main_0.raceImageCache_0);
                if (selectedGalaxySummary.EmpireSummaries != null && selectedGalaxySummary.EmpireSummaries.Count > 0)
                {
                    pnlStartNewGameGalaxyMapsEmpire.BindData(selectedGalaxySummary.EmpireSummaries[0], raceList_1, main_0.raceImageCache_0, font_9, font_3, font_7);
                }
                else
                {
                    pnlStartNewGameGalaxyMapsEmpire.ClearData();
                }
            }
            else
            {
                lblStartNewGameGalaxyMapsAvailableFactions.Text = string.Format(TextResolver.GetText("StartNewGame GalaxyMaps AvailableEmpires"), string.Empty);
                pnlStartNewGameGalaxyMapsGalaxy.ClearData();
                ctlStartNewGameGalaxyMapsEmpires.ClearData();
            }
        }

        private void ctlStartNewGameGalaxyMapsEmpires_SelectionChanged(object sender, EventArgs e)
        {
            EmpireSummary selectedEmpireSummary = ctlStartNewGameGalaxyMapsEmpires.SelectedEmpireSummary;
            if (selectedEmpireSummary != null)
            {
                pnlStartNewGameGalaxyMapsEmpire.BindData(selectedEmpireSummary, raceList_1, main_0.raceImageCache_0, font_9, font_3, font_7);
            }
            else
            {
                pnlStartNewGameGalaxyMapsEmpire.ClearData();
            }
        }

        private void ctlStartNewGameGalaxyMapsGalaxies_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Prebuilt Galaxies") + ": " + TextResolver.GetText("Galaxies"), TextResolver.GetText("Select a galaxy to play"));
        }

        private void ctlStartNewGameGalaxyMapsEmpires_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Prebuilt Galaxies") + ": " + TextResolver.GetText("Factions"), TextResolver.GetText("Select a faction to play"));
        }

        private void btnStartNewGameTheGalaxyLoadExistingBrowse_Click(object sender, EventArgs e)
        {
            string value = string.Empty;
            string text = Application.StartupPath + "\\maps\\";
            if (!string.IsNullOrEmpty(main_0.string_3))
            {
                text = Application.StartupPath + "\\Customization\\" + main_0.string_3 + "\\maps\\";
                if (!Directory.Exists(text))
                {
                    text = Application.StartupPath + "\\maps\\";
                }
            }
            if (!Directory.Exists(text))
            {
                text = Main.GetGameSavesFolderCreateIfNeeded();
            }
            if (Directory.Exists(text))
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = text;
                openFileDialog.Filter = TextResolver.GetText("Distant Worlds saved game files") + " (*.dwg)|*.dwg";
                openFileDialog.DefaultExt = "dwg";
                openFileDialog.Title = TextResolver.GetText("Select Distant Worlds game as map");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    value = openFileDialog.FileName;
                }
                openFileDialog.Dispose();
            }
            if (!string.IsNullOrEmpty(value))
            {
                lblStartNewGameTheGalaxyLoadExistingFilepath.Text = value;
            }
            else
            {
                lblStartNewGameTheGalaxyLoadExistingFilepath.Text = "(" + TextResolver.GetText("No Galaxy Map specified") + ")";
            }
        }

        private void btnStartNewGameTheGalaxyLoadExistingClear_Click(object sender, EventArgs e)
        {
            lblStartNewGameTheGalaxyLoadExistingFilepath.Text = "(" + TextResolver.GetText("No Galaxy Map specified") + ")";
        }

        private void btnStartNewGameTheGalaxyLoadExistingBrowse_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Browse for Maps"), TextResolver.GetText("Browse for an existing saved game to use as a galaxy map in your new game"));
        }

        private void btnStartNewGameTheGalaxyLoadExistingClear_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Clear Map"), TextResolver.GetText("Clear saved game as galaxy map and instead use settings at left to generate a new galaxy"));
        }

        private void chkStartNewGameTheGalaxyLoadExistingResources_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Regenerate Resources"), TextResolver.GetText("Clear existing resources from the map and regenerate new resources when starting your new game"));
        }

        private void chkStartNewGameTheGalaxyLoadExistingSceneryResearch_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Regenerate Scenery and Research bonuses"), TextResolver.GetText("Clear existing Scenery and Research bonuses from the map and regenerate new bonuses when starting your new game"));
        }

        private void chkStartNewGameTheGalaxyLoadExistingCreatures_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Regenerate Space Creatures"), TextResolver.GetText("Clear existing Space Creatures from the map and regenerate new Space Creatures when starting your new game"));
        }

        private void IfEoxFyIkN(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Regenerate Ruins"), TextResolver.GetText("Clear existing Ruins from the map and regenerate new Ruins when starting your new game"));
        }

        private void BrXoYtPsoA(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Regenerate Special Locations"), TextResolver.GetText("Clear existing Special Locations from the map and regenerate new Special Locations when starting your new game"));
        }

        private void radJumpStartGalaxyShapeElliptical_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Elliptical"));
        }

        private void radJumpStartGalaxyShapeSpiral_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Spiral"));
        }

        private void radJumpStartGalaxyShapeRing_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Ring"));
        }

        private void radJumpStartGalaxyShapeIrregular_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Irregular"));
        }

        private void radJumpStartGalaxyShapeEvenClusters_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Even Clusters"));
        }

        private void radJumpStartGalaxyShapeVariedClusters_CheckedChanged(object sender, EventArgs e)
        {
            method_206(TextResolver.GetText("Varied Clusters"));
        }

        private void radJumpStartGalaxyShapeElliptical_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radJumpStartGalaxyShapeSpiral_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radJumpStartGalaxyShapeRing_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radJumpStartGalaxyShapeIrregular_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radJumpStartGalaxyShapeEvenClusters_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void radJumpStartGalaxyShapeVariedClusters_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Shape"), TextResolver.GetText("Determines the layout and distribution of stars within the galaxy"));
        }

        private void tbarJumpStartTheGalaxyDimensions_ValueChanged(object sender, EventArgs e)
        {
            method_211(bool_5: true);
        }

        private void tbarJumpStartTheGalaxyDimensions_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Physical Size"), TextResolver.GetText("Determines the physical dimensions of the galaxy"));
        }

        private void muloBoAqMA(object sender, EventArgs e)
        {
            method_211(bool_5: true);
        }

        private void tbarJumpStartTheGalaxyStarDensity_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Star Amount"), TextResolver.GetText("Determines how many stars are in the galaxy"));
        }

        private void tbarJumpStartTheGalaxyDifficulty_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Difficulty"), TextResolver.GetText("Determines difficulty and aggression of gameplay"));
        }

        private void chkJumpStartTheGalaxyDifficultyScaling_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("The Galaxy") + ": " + TextResolver.GetText("Difficulty Scaling"), TextResolver.GetText("Difficulty Scaling Description"));
        }

        private void cmbJumpStartYourEmpireRace_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Race") + ": " + TextResolver.GetText("Race"), TextResolver.GetText("The dominant race at your empire's home colony"));
        }

        private void lnkJumpStartYourEmpireRace_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Race selectedRace = cmbJumpStartYourEmpireRace.SelectedRace;
            if (selectedRace == null)
            {
                method_127(TextResolver.GetText("Alien Races"));
            }
            else
            {
                method_127(selectedRace.Name);
            }
        }

        private void cmbJumpStartYourEmpireGovernment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedGovernmentId = cmbJumpStartYourEmpireGovernment.SelectedGovernmentId;
            method_208(selectedGovernmentId, bool_5: true);
        }

        private void cmbJumpStartYourEmpireGovernment_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Government"), TextResolver.GetText("The form of government that your empire follows"));
        }

        private void lnkJumpStartYourEmpireGovernment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int selectedGovernmentId = cmbJumpStartYourEmpireGovernment.SelectedGovernmentId;
            GovernmentAttributes governmentAttributes = null;
            if (selectedGovernmentId >= 0 && selectedGovernmentId < Galaxy.GovernmentsStatic.Count)
            {
                governmentAttributes = Galaxy.GovernmentsStatic[selectedGovernmentId];
            }
            if (governmentAttributes == null)
            {
                method_127(TextResolver.GetText("Government Types"));
            }
            else
            {
                method_127(governmentAttributes.Name);
            }
        }

        private void cmbJumpStartYourEmpireRace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empty = string.Empty;
            Race selectedRace = cmbJumpStartYourEmpireRace.SelectedRace;
            RaceSummary summary = null;
            if (selectedRace == null)
            {
                empty = "(" + TextResolver.GetText("Random") + ")";
                _ = "(" + TextResolver.GetText("Race randomly selected") + ")";
                lnkJumpStartYourEmpireRace.Visible = false;
            }
            else
            {
                empty = selectedRace.Name;
                summary = Galaxy.GenerateRaceSummary(selectedRace);
                lnkJumpStartYourEmpireRace.Visible = true;
            }
            IdyEbrKpy3(selectedRace, bool_5: true);
            if (selectedRace != null && selectedRace.DefaultPiratePlaystyle != 0)
            {
                method_207(selectedRace.DefaultPiratePlaystyle, bool_5: true);
                method_101(selectedRace.DefaultPiratePlaystyle, bool_5: true);
            }
            else
            {
                method_207(PiratePlayStyle.Balanced, bool_5: true);
                method_101(PiratePlayStyle.Balanced, bool_5: true);
            }
            lblJumpStartYourEmpireRaceTitle.Text = TextResolver.GetText("Your Race") + ": " + empty;
            lblJumpStartYourEmpireRaceName.Visible = true;
            lblJumpStartYourEmpireRaceName.Font = font_9;
            lblJumpStartYourEmpireRaceName.Text = empty;
            Bitmap bitmap = (Bitmap)picJumpStartYourEmpireRace.Image;
            Bitmap image = main_0.method_118(null, selectedRace, picJumpStartYourEmpireRace.Width, picJumpStartYourEmpireRace.Height, main_0.bitmap_31, 6, bool_28: false);
            picJumpStartYourEmpireRace.Image = image;
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                bitmap.Dispose();
            }
            pnlJumpStartYourEmpireRaceAttributes.BindData(summary, font_3, font_7);
            pnlJumpStartYourEmpireRaceAttributesContainer.AutoScrollPosition = new Point(0, 0);
        }

        private void cmbJumpStartVictoryPiratePlayStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            PiratePlayStyle piratePlayStyle_ = PiratePlayStyle.Balanced;
            switch (cmbJumpStartVictoryPiratePlayStyle.SelectedIndex)
            {
                case 0:
                    piratePlayStyle_ = PiratePlayStyle.Balanced;
                    break;
                case 1:
                    piratePlayStyle_ = PiratePlayStyle.Pirate;
                    break;
                case 2:
                    piratePlayStyle_ = PiratePlayStyle.Mercenary;
                    break;
                case 3:
                    piratePlayStyle_ = PiratePlayStyle.Smuggler;
                    break;
            }
            method_101(piratePlayStyle_, bool_5: true);
        }

        private void cmbJumpStartVictoryPiratePlayStyle_Enter(object sender, EventArgs e)
        {
            method_100(TextResolver.GetText("Your Empire") + ": " + TextResolver.GetText("Pirate Playstyle"), TextResolver.GetText("Determines the play focus and pirate victory conditions for your pirate empire"));
        }

        private void btnStartNewGameYourEmpireTypeNormalClassic_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "CustomStandard";
            bool_2 = false;
            bool_3 = false;
            method_45(bool_2);
            method_40(bool_5: false);
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameTheGalaxy.Visible = true;
            pnlStartNewGameTheGalaxy.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: The Galaxy");
            if (radStartNewGameGalaxyShapeElliptical.Checked)
            {
                radStartNewGameGalaxyShapeElliptical.Focus();
            }
            else if (radStartNewGameGalaxyShapeSpiral.Checked)
            {
                radStartNewGameGalaxyShapeSpiral.Focus();
            }
            else if (radStartNewGameGalaxyShapeRing.Checked)
            {
                radStartNewGameGalaxyShapeRing.Focus();
            }
            else if (radStartNewGameGalaxyShapeIrregular.Checked)
            {
                radStartNewGameGalaxyShapeIrregular.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersEven.Checked)
            {
                radStartNewGameGalaxyShapeClustersEven.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersVaried.Checked)
            {
                radStartNewGameGalaxyShapeClustersVaried.Focus();
            }
        }

        private void nVkoJxpyvO(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "CustomPirate";
            bool_2 = true;
            bool_3 = false;
            method_45(bool_2);
            method_40(bool_5: false);
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: true, useDarkerPalette: true, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            if (bool_2 && tbarStartNewGameYourEmpireTechLevel.Value == 0)
            {
                tbarStartNewGameYourEmpireTechLevel.Value = 1;
            }
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameTheGalaxy.Visible = true;
            pnlStartNewGameTheGalaxy.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: The Galaxy");
            if (radStartNewGameGalaxyShapeElliptical.Checked)
            {
                radStartNewGameGalaxyShapeElliptical.Focus();
            }
            else if (radStartNewGameGalaxyShapeSpiral.Checked)
            {
                radStartNewGameGalaxyShapeSpiral.Focus();
            }
            else if (radStartNewGameGalaxyShapeRing.Checked)
            {
                radStartNewGameGalaxyShapeRing.Focus();
            }
            else if (radStartNewGameGalaxyShapeIrregular.Checked)
            {
                radStartNewGameGalaxyShapeIrregular.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersEven.Checked)
            {
                radStartNewGameGalaxyShapeClustersEven.Focus();
            }
            else if (radStartNewGameGalaxyShapeClustersVaried.Checked)
            {
                radStartNewGameGalaxyShapeClustersVaried.Focus();
            }
        }

        private void btnStartNewGameYourEmpireTypeNormalShadows_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "ShadowsStandard";
            bool_2 = false;
            bool_3 = true;
            method_35();
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameJumpStart.Visible = true;
            pnlStartNewGameJumpStart.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Galaxy, Race, Government, Difficulty");
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                radJumpStartGalaxyShapeElliptical.Focus();
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                radJumpStartGalaxyShapeSpiral.Focus();
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                radJumpStartGalaxyShapeRing.Focus();
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                radJumpStartGalaxyShapeIrregular.Focus();
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                radJumpStartGalaxyShapeEvenClusters.Focus();
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                radJumpStartGalaxyShapeVariedClusters.Focus();
            }
        }

        private void method_222(StartGameOptions startGameOptions_0)
        {
            tbarJumpStartTheGalaxyStarDensity.Value = startGameOptions_0.GalaxySize;
            tbarJumpStartTheGalaxyDimensions.Value = startGameOptions_0.GalaxyDimensions;
            tbarJumpStartTheGalaxyDifficulty.Value = startGameOptions_0.GalaxyDifficulty;
            chkJumpStartTheGalaxyDifficultyScaling.Checked = startGameOptions_0.GalaxyDifficultyScaling;
            if (startGameOptions_0.YourEmpireRace >= 0 && startGameOptions_0.YourEmpireRace < cmbJumpStartYourEmpireRace.Items.Count)
            {
                cmbJumpStartYourEmpireRace.SelectedIndex = startGameOptions_0.YourEmpireRace;
            }
            cmbJumpStartYourEmpireGovernment.SetSelectedGovernmentStyle(startGameOptions_0.YourEmpireGovernmentStyle);
            cmbJumpStartVictoryPiratePlayStyle.SelectedIndex = startGameOptions_0.PiratePlayStyle;
            tbarStartNewGameTheGalaxyStarDensity.Value = startGameOptions_0.GalaxySize;
            tbarStartNewGameTheGalaxyDimensions.Value = startGameOptions_0.GalaxyDimensions;
            tbarStartNewGameTheGalaxyDifficulty.Value = startGameOptions_0.GalaxyDifficulty;
            chkStartNewGameTheGalaxyDifficultyScaling.Checked = startGameOptions_0.GalaxyDifficultyScaling;
            if (startGameOptions_0.YourEmpireRace >= 0 && startGameOptions_0.YourEmpireRace < cmbStartNewGameYourEmpireRace.Items.Count)
            {
                cmbStartNewGameYourEmpireRace.SelectedIndex = startGameOptions_0.YourEmpireRace;
            }
            cmbStartNewGameYourEmpireGovernment.SetSelectedGovernmentStyle(startGameOptions_0.YourEmpireGovernmentStyle);
            cmbVictoryPiratePlayStyle.SelectedIndex = startGameOptions_0.PiratePlayStyle;
        }

        private void btnStartNewGameYourEmpireTypePirateShadows_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "ShadowsPirate";
            bool_2 = true;
            bool_3 = true;
            method_35();
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameJumpStart.Visible = true;
            pnlStartNewGameJumpStart.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Galaxy, Race, Government, Difficulty");
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                radJumpStartGalaxyShapeElliptical.Focus();
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                radJumpStartGalaxyShapeSpiral.Focus();
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                radJumpStartGalaxyShapeRing.Focus();
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                radJumpStartGalaxyShapeIrregular.Focus();
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                radJumpStartGalaxyShapeEvenClusters.Focus();
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                radJumpStartGalaxyShapeVariedClusters.Focus();
            }
        }

        private void btnStartNewGameIntroductory_Click(object sender, EventArgs e)
        {
            bool_2 = false;
            bool_3 = false;
            GalaxyShape galaxyShape = GalaxyShape.Elliptical;
            bool flag = true;
            EmpireVictoryConditions item = null;
            EmpireVictoryConditions item2 = null;
            bool flag2 = false;
            int num = 1;
            Random random = new Random((int)DateTime.Now.Ticks);
            int num2 = 700;
            int num3 = 20;
            int num4 = random.Next(15, 20);
            EmpireStartList empireStartList = new EmpireStartList();
            EmpireStart empireStart = method_112(null, empireStartList, TextResolver.GetText("Starting"), TextResolver.GetText("Normal"), random);
            empireStart.HomeSystemFavourability = TextResolver.GetText("Agreeable");
            empireStart.DifficultyLevel = method_201(0);
            for (int i = 0; i < num4; i++)
            {
                EmpireStart item3 = method_112(empireStart, empireStartList, TextResolver.GetText("Starting"), TextResolver.GetText("Normal"), random);
                empireStartList.Add(item3);
            }
            double num5 = meEawywtba(2);
            double num6 = 1.1;
            VictoryConditions victoryConditions = method_106(num3);
            victoryConditions.EnableStoryEvents = true;
            victoryConditions.EnableRaceSpecificEvents = true;
            victoryConditions.EnableRaceSpecificVictoryConditions = true;
            double num7 = 1.0;
            double num8 = 0.3;
            double num9 = 0.2;
            string string_ = TextResolver.GetText("Starting");
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num2);
            list.Add(num3);
            list.Add(flag);
            list.Add(num7);
            list.Add(700);
            list.Add(num8);
            list.Add(num9);
            list.Add(num5);
            list.Add(method_57(string_));
            list.Add(num6);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(victoryConditions);
            list.Add(item);
            list.Add(item2);
            list.Add(flag2);
            list.Add(num);
            list.Add(double_0);
            list.Add(true);
            list.Add(null);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            method_46();
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
            }
        }

        private void btnStartNewGameYourEmpireTypeClassicEra_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "ClassicEra";
            bool_2 = false;
            bool_3 = false;
            method_35();
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameJumpStart.Visible = true;
            pnlStartNewGameJumpStart.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Galaxy, Race, Government, Difficulty");
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                radJumpStartGalaxyShapeElliptical.Focus();
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                radJumpStartGalaxyShapeSpiral.Focus();
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                radJumpStartGalaxyShapeRing.Focus();
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                radJumpStartGalaxyShapeIrregular.Focus();
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                radJumpStartGalaxyShapeEvenClusters.Focus();
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                radJumpStartGalaxyShapeVariedClusters.Focus();
            }
        }

        private void btnStartNewGameYourEmpireTypeReturnOfTheShakturi_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "ReturnOfTheShakturi";
            bool_2 = false;
            bool_3 = false;
            method_35();
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameJumpStart.Visible = true;
            pnlStartNewGameJumpStart.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Galaxy, Race, Government, Difficulty");
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                radJumpStartGalaxyShapeElliptical.Focus();
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                radJumpStartGalaxyShapeSpiral.Focus();
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                radJumpStartGalaxyShapeRing.Focus();
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                radJumpStartGalaxyShapeIrregular.Focus();
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                radJumpStartGalaxyShapeEvenClusters.Focus();
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                radJumpStartGalaxyShapeVariedClusters.Focus();
            }
        }

        private void btnStartNewGameYourEmpireTypeLegends_Click(object sender, EventArgs e)
        {
            wjhRtsSwmsa = "Legends";
            bool_2 = false;
            bool_3 = false;
            method_35();
            cmbPrimaryColor.Ignite(allowWhite: false, allowBlack: false, useDarkerPalette: false, Color.Empty);
            method_204(bool_2);
            method_222(main_0.gameOptions_0.StartGameOptions);
            pnlStartNewGameYourEmpireType.Visible = false;
            pnlStartNewGameJumpStart.Visible = true;
            pnlStartNewGameJumpStart.BringToFront();
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Galaxy, Race, Government, Difficulty");
            if (radJumpStartGalaxyShapeElliptical.Checked)
            {
                radJumpStartGalaxyShapeElliptical.Focus();
            }
            else if (radJumpStartGalaxyShapeSpiral.Checked)
            {
                radJumpStartGalaxyShapeSpiral.Focus();
            }
            else if (radJumpStartGalaxyShapeRing.Checked)
            {
                radJumpStartGalaxyShapeRing.Focus();
            }
            else if (radJumpStartGalaxyShapeIrregular.Checked)
            {
                radJumpStartGalaxyShapeIrregular.Focus();
            }
            else if (radJumpStartGalaxyShapeEvenClusters.Checked)
            {
                radJumpStartGalaxyShapeEvenClusters.Focus();
            }
            else if (radJumpStartGalaxyShapeVariedClusters.Checked)
            {
                radJumpStartGalaxyShapeVariedClusters.Focus();
            }
        }

        private void method_223(object object_0)
        {
            if (object_0 is object[])
            {
                object[] array = (object[])object_0;
                if (array.Length == 3)
                {
                    string string_ = (string)array[0];
                    bool bool_ = (bool)array[1];
                    bool bool_2 = (bool)array[2];
                    bool_0 = false;
                    method_2(string_, bool_, bool_2);
                    bool_0 = true;
                }
            }
        }

        private void btnStartNewGameYourEmpireTypeTheAncientGalaxy_Click(object sender, EventArgs e)
        {
            if (main_0.string_3 != "The Ancient Galaxy")
            {
                base.Enabled = false;
                Application.UseWaitCursor = true;
                Thread thread = new Thread(method_223);
                thread.Start(new object[3] { "The Ancient Galaxy", true, true });
                Application.DoEvents();
                method_8(TextResolver.GetText("Switching to The Ancient Galaxy theme"));
                Application.DoEvents();
                method_10();
            }
            if (galaxySummaryList_0 != null && galaxySummaryList_0.Count > 0)
            {
                method_221(galaxySummaryList_0[0], null);
            }
        }

        private void btnJumpStartTheGalaxyPrevious_Click(object sender, EventArgs e)
        {
            pnlStartNewGameJumpStart.Visible = false;
            pnlStartNewGameYourEmpireType.Visible = true;
            pnlNewGame.HeaderTitle = TextResolver.GetText("Start a New Game: Playstyle");
            btnStartNewGameYourEmpireTypeNormalShadows.Focus();
            pnlStartNewGameYourEmpireType.BringToFront();
            lblHelpTitle.Text = string.Empty;
            lblHelpDescription.Text = string.Empty;
        }

        private void btnJumpStartTheGalaxyNext_Click(object sender, EventArgs e)
        {
            main_0.gameOptions_0.StartGameOptions = method_194();
            main_0.YxwyUefOyQ();
            main_0.method_257();
            method_46();
            Random random_ = new Random((int)DateTime.Now.Ticks);
            GalaxyShape galaxyShape = method_59(method_97());
            int num = method_60(tbarJumpStartTheGalaxyStarDensity.Value);
            int num2 = method_61(tbarJumpStartTheGalaxyStarDensity.Value, raceList_0);
            bool flag = true;
            double num3 = method_64(2);
            int num4 = method_67(2);
            double num5 = method_62(2);
            double num6 = method_66(3);
            int num7 = 1;
            double num8 = 120000.0;
            double num9 = method_71(1);
            int num10 = 1;
            if (bool_3)
            {
                num10 = 0;
            }
            string string_ = method_58(num10);
            EmpireStart empireStart = new EmpireStart();
            empireStart.Name = string.Empty;
            Race selectedRace = cmbJumpStartYourEmpireRace.SelectedRace;
            if (selectedRace != null)
            {
                empireStart.Race = selectedRace.Name;
            }
            else
            {
                empireStart.Race = "(" + TextResolver.GetText("Random") + ")";
            }
            empireStart.GovernmentStyle = method_73();
            empireStart.StartLocation = cmbYourEmpireStartLocation.SelectedItem.ToString();
            empireStart.HomeSystemFavourability = TextResolver.GetText("Normal");
            string text = TextResolver.GetText("Starting");
            if (text == TextResolver.GetText("Starting") && num10 == 0)
            {
                text = TextResolver.GetText("PreWarp");
            }
            empireStart.Age = method_57(text);
            empireStart.TechLevel = method_54(TextResolver.GetText("Normal"));
            if (bool_3 && !bool_2)
            {
                empireStart.TechLevel = method_54(TextResolver.GetText("PreWarp"));
            }
            if (selectedRace != null)
            {
                empireStart.PrimaryColor = selectedRace.DefaultMainColor;
                empireStart.SecondaryColor = selectedRace.DefaultSecondaryColor;
                empireStart.FlagShape = selectedRace.DefaultFlagShape;
            }
            empireStart.CorruptionMultiplier = method_63(1);
            empireStart.PiratePlayStyle = method_192();
            empireStart.PirateShipMaintenanceFactor = 0.4;
            empireStart.AllowTechTrading = true;
            empireStart.AllowGiantKaltorGeneration = true;
            empireStart.DifficultyLevel = method_201(tbarJumpStartTheGalaxyDifficulty.Value);
            empireStart.DifficultyScaling = chkJumpStartTheGalaxyDifficultyScaling.Checked;
            empireStart.DestroyedPiratesDoNotRespawn = false;
            Size size = method_69(tbarJumpStartTheGalaxyDimensions.Value);
            empireStart.GalaxySectorX = size.Width;
            empireStart.GalaxySectorY = size.Height;
            float num11 = (empireStart.EmpireTerritoryColonyInfluenceRangeFactor = (float)method_189(num, size.Width, size.Height));
            empireStart.ColonizationRangeEnforceLimit = true;
            empireStart.ColonizationRange = 2f * (float)Galaxy.SectorSize;
            EmpireStartList empireStartList = new EmpireStartList();
            int num12 = num2;
            for (int i = 0; i < num12; i++)
            {
                EmpireStart empireStart2 = new EmpireStart();
                empireStart2.Name = string.Empty;
                empireStart2.GovernmentStyle = "(" + TextResolver.GetText("Random") + ")";
                empireStart2.ProximityDistance = "(" + TextResolver.GetText("Random") + ")";
                empireStart2.HomeSystemFavourability = TextResolver.GetText("Normal");
                string string_2 = method_109(string_, random_);
                empireStart2.Age = method_57(string_2);
                empireStart2.TechLevel = method_89(num10);
                empireStart2.Race = "(" + TextResolver.GetText("Random") + ")";
                empireStartList.Add(empireStart2);
            }
            empireStartList = method_200(empireStartList, tbarJumpStartTheGalaxyDifficulty.Value, bool_5: false);
            long startStarDate = Galaxy.StartStarDate;
            startStarDate += num10 * 30000000;
            VictoryConditions victoryConditions = new VictoryConditions();
            victoryConditions.Economy = true;
            victoryConditions.EconomyPercent = 33.0;
            victoryConditions.Population = true;
            victoryConditions.PopulationPercent = 33.0;
            victoryConditions.Territory = true;
            victoryConditions.TerritoryPercent = 33.0;
            victoryConditions.TimeLimit = false;
            victoryConditions.TimeLimitDate = 0L;
            victoryConditions.StartDate = startStarDate + 10 * Galaxy.RealSecondsInGalacticYear * 1000;
            victoryConditions.VictoryThresholdPercentage = 0.8;
            bool flag2 = false;
            switch (wjhRtsSwmsa)
            {
                case "Legends":
                    victoryConditions.EnableStoryEventsShadows = false;
                    flag2 = true;
                    victoryConditions.EnableStoryEvents = true;
                    victoryConditions.EnableDisasterEvents = true;
                    victoryConditions.EnableRaceSpecificEvents = true;
                    victoryConditions.EnableRaceSpecificVictoryConditions = true;
                    break;
                case "ReturnOfTheShakturi":
                    victoryConditions.EnableStoryEventsShadows = false;
                    flag2 = true;
                    victoryConditions.EnableStoryEvents = true;
                    victoryConditions.EnableDisasterEvents = false;
                    victoryConditions.EnableRaceSpecificEvents = false;
                    victoryConditions.EnableRaceSpecificVictoryConditions = false;
                    break;
                case "ClassicEra":
                    victoryConditions.EnableStoryEventsShadows = false;
                    flag2 = true;
                    victoryConditions.EnableStoryEvents = false;
                    victoryConditions.EnableDisasterEvents = false;
                    victoryConditions.EnableRaceSpecificEvents = false;
                    victoryConditions.EnableRaceSpecificVictoryConditions = false;
                    break;
                case "ShadowsPirate":
                    victoryConditions.EnableStoryEventsShadows = true;
                    flag2 = false;
                    victoryConditions.EnableStoryEvents = false;
                    victoryConditions.EnableDisasterEvents = true;
                    victoryConditions.EnableRaceSpecificEvents = true;
                    victoryConditions.EnableRaceSpecificVictoryConditions = true;
                    num6 = method_66(4);
                    num7 = 1;
                    num3 = method_64(3);
                    num4 = method_67(3);
                    num9 = method_71(2);
                    break;
                case "ShadowsStandard":
                    victoryConditions.EnableStoryEventsShadows = true;
                    flag2 = false;
                    victoryConditions.EnableStoryEvents = true;
                    victoryConditions.EnableDisasterEvents = true;
                    victoryConditions.EnableRaceSpecificEvents = true;
                    victoryConditions.EnableRaceSpecificVictoryConditions = true;
                    num6 = method_66(4);
                    num7 = 0;
                    num3 = method_64(3);
                    num4 = method_67(3);
                    num9 = method_71(2);
                    break;
            }
            string customizationSetName = string.Empty;
            if (main_0.gameOptions_0 != null)
            {
                customizationSetName = main_0.gameOptions_0.CustomizationSetName;
            }
            RaceList raceList = Galaxy.LoadRaces(Application.StartupPath, customizationSetName);
            raceList = raceList.ResolvePlayableRaces();
            string string_3 = string.Empty;
            if (selectedRace != null)
            {
                string_3 = selectedRace.Name;
            }
            empireStartList = method_104(empireStartList, raceList, num9, string_3);
            GameStartResets item = new GameStartResets();
            List<object> list = new List<object>();
            list.Add(galaxyShape);
            list.Add(num);
            list.Add(num2);
            list.Add(flag);
            list.Add(num3);
            list.Add(num4);
            list.Add(num5);
            list.Add(num6);
            list.Add(num8);
            list.Add(num10);
            list.Add(num9);
            list.Add(empireStart);
            list.Add(empireStartList);
            list.Add(victoryConditions);
            list.Add(null);
            list.Add(null);
            list.Add(false);
            list.Add(num7);
            list.Add(double_0);
            list.Add(flag2);
            list.Add(item);
            method_8(TextResolver.GetText("Creating new Galaxy..."));
            base.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            oyxRtRyAwjg.RunWorkerAsync(list);
            Cursor.Current = Cursors.WaitCursor;
            while (oyxRtRyAwjg.IsBusy)
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;
                Thread.Sleep(30);
            }
            Cursor.Current = Cursors.Default;
            if (game_0 != null)
            {
                method_77(game_0);
                return;
            }
            Cursor.Current = Cursors.Default;
            method_9();
            method_46();
            method_25();
            base.Enabled = true;
            Show();
            main_0.Visible = false;
        }

        private void menuTutorials_Click(object sender, EventArgs e)
        {
            method_119();
        }

        private void menuStartNewGame_Click(object sender, EventArgs e)
        {
            method_31("");
        }

        private void menuLoadGame_Click(object sender, EventArgs e)
        {
            try
            {
                new Game();
                string empty = string.Empty;
                bool flag = false;
                string text = Main.GetGameSavesFolderCreateIfNeeded();
                if (main_0 != null && main_0.gameOptions_0 != null && !string.IsNullOrEmpty(main_0.gameOptions_0.SaveGamePath))
                {
                    text = main_0.gameOptions_0.SaveGamePath;
                }
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = text;
                openFileDialog.Filter = TextResolver.GetText("Distant Worlds saved game files") + " (*.dwg)|*.dwg";
                openFileDialog.DefaultExt = "dwg";
                openFileDialog.Title = TextResolver.GetText("Load Distant Worlds game");
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    flag = true;
                }
                if (flag)
                {
                    Stream stream_;
                    if ((stream_ = openFileDialog.OpenFile()) != null)
                    {
                        method_140();
                        empty = openFileDialog.FileName;
                        if (main_0 != null && main_0.gameOptions_0 != null)
                        {
                            DirectoryInfo directoryInfo = Directory.GetParent(empty);
                            if (directoryInfo != null)
                            {
                                main_0.gameOptions_0.SaveGamePath = directoryInfo.FullName;
                            }
                        }
                        openFileDialog.Dispose();
                        method_22(stream_, empty, TextResolver.GetText("Loading the Galaxy..."));
                    }
                    else
                    {
                        flag = false;
                        openFileDialog.Dispose();
                    }
                }
                else
                {
                    openFileDialog.Dispose();
                }
                if (!flag)
                {
                    Application.UseWaitCursor = false;
                    method_9();
                }
                else
                {
                    method_10();
                    Application.UseWaitCursor = false;
                    method_23();
                }
            }
            catch (Exception ex)
            {
                Main.CrashDump(ex);
                throw;
            }
        }

        private void menuOptions_Click(object sender, EventArgs e)
        {
            method_153();
        }

        private void menuChangeTheme_Click(object sender, EventArgs e)
        {
            if (!pnlThemes.Visible)
            {
                method_26();
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            method_140();
            if (main_0.musicPlayer_0 != null && main_0.musicPlayer_1 != null)
            {
                main_0.musicPlayer_0.Stop();
                main_0.musicPlayer_1.Stop();
            }
            string filename = Main.GetGameFilesFolderCreateIfNeeded() + "automationPrefs";
            MessageBoxExManager.WriteSavedResponses(filename);
            if (main_0 != null)
            {
                main_0.method_257();
                main_0.method_255();
            }
            ToggleScreenSaverActive(active: true);
            if (!main_0.bool_2)
            {
                main_0.method_374();
            }
            Environment.Exit(-1);
        }

        private void Start_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = PointToScreen(e.Location);
            method_224(point_);
        }

        private void method_224(Point point_0)
        {
            bool stateChanged = false;
            Point parentScreenLocation = menuGroup.PointToScreen(menuGroup.Location);
            menuGroup.CheckHoverState(point_0, parentScreenLocation, out stateChanged);
            if (stateChanged)
            {
                menuGroup.Invalidate();
            }
            menuGalactopedia.CheckHoverState(PointToClient(MouseHelper.GetCursorPosition()), pnlTopLeftCorner.Location);
            menuCheckForUpdates.CheckHoverState(PointToClient(MouseHelper.GetCursorPosition()), pnlBottomLeftCorner.Location);
            menuCredits.CheckHoverState(PointToClient(MouseHelper.GetCursorPosition()), new Point(0, 0));
        }

        private void menuGroup_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuGroup.PointToScreen(e.Location);
            method_224(point_);
        }

        private void pnlButtons_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = pnlButtons.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuTutorials_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuTutorials.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuStartNewGame_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuStartNewGame.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuLoadGame_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuLoadGame.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuOptions_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuOptions.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuChangeTheme_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuChangeTheme.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuExit_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuExit.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuGalactopedia_Click(object sender, EventArgs e)
        {
            method_127("");
        }

        private void menuCheckForUpdates_Click(object sender, EventArgs e)
        {
            string text = "http://www.codeforce.co.nz/dwuniverse_versioncheck.asp";
            text = text + "?version=" + Application.ProductVersion;
            Process.Start(text);
        }

        private void menuCredits_Click(object sender, EventArgs e)
        {
            menuCredits.Visible = false;
            Update();
            if (!pnlAboutCredits.Visible)
            {
                method_138();
            }
        }

        private void menuGalactopedia_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuGalactopedia.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuCheckForUpdates_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuCheckForUpdates.PointToScreen(e.Location);
            method_224(point_);
        }

        private void menuCredits_MouseMove(object sender, MouseEventArgs e)
        {
            Point point_ = menuCredits.PointToScreen(e.Location);
            method_224(point_);
        }

    }
}
