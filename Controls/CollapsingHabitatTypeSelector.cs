// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CollapsingHabitatTypeSelector
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CollapsingHabitatTypeSelector : UserControl
  {
    private IContainer components;
    private Panel MainPanel;
    private ExtendedPanel cpnStar;
    private ExtendedPanel cpnGasCloud;
    private ExtendedPanel cpnPlanet;
    private ExtendedPanel cpnBuiltObject;
    private ExtendedPanel cpnAsteroid;
    private ExtendedPanel cpnMoon;
    public HabitatTypeIconView vwSystem;
    public ExtendedPanel cpnSystem;
    public HabitatTypeIconView vwGasCloud;
    public HabitatTypeIconView vwMoon;
    public HabitatTypeIconView vwPlanet;
    public HabitatTypeIconView vwStar;
    private ExtendedPanel cpnCreature;
    private ExtendedPanel cpnAliens;
    private ExtendedPanel cpnColony;
    private Label label2;
    private Label label1;
    private Label label3;
    private Label label4;
    private ExtendedPanel cpnEmpireExploration;
    private ExtendedPanel cpnPirates;
    private ExtendedPanel cpnClearItems;
    private Label label5;
    public EmpireDropDown cmbPlaceShipEmpire;
    public DesignDropDown cmbPlaceShipDesign;
    public EmpireDropDown cmbPlaceColonyEmpire;
    public RaceDropDown cmbPlaceAlienRace;
    public CreatureTypeIconView vwCreature;
    public GenericIconView vwPirates;
    public GenericIconView vwAsteroids;
    public EmpireDropDown cmbSetEmpireExploration;
    private GenericIconView vwClearItems;
    private ExtendedPanel cpnCharacters;
    private Label label7;
    public CharacterDropDown cmbCharacters;
    private Label label6;
    public EmpireDropDown cmbCharacterEmpire;
    private ExtendedPanel cpnRuins;
    public GenericIconView vwRuins;
    private Galaxy _Galaxy;
    private RaceList _Races;
    private Bitmap[] _RaceImages;
    private DistantWorlds.Types.EmpireList _Empires;
    private DistantWorlds.Types.EmpireList _PirateFactions;
    private Empire _PlayerEmpire;
    private Empire _IndependentEmpire;
    private Bitmap[] _BuiltObjectImages;
    private CharacterImageCache _CharacterImageCache;
    public string EditModeExtra = string.Empty;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CollapsingHabitatTypeSelector));
      this.MainPanel = new Panel();
      this.cpnClearItems = new ExtendedPanel();
      this.vwClearItems = new GenericIconView();
      this.cpnEmpireExploration = new ExtendedPanel();
      this.label5 = new Label();
      this.cmbSetEmpireExploration = new EmpireDropDown();
      this.cpnRuins = new ExtendedPanel();
      this.vwRuins = new GenericIconView();
      this.cpnPirates = new ExtendedPanel();
      this.vwPirates = new GenericIconView();
      this.cpnCreature = new ExtendedPanel();
      this.vwCreature = new CreatureTypeIconView();
      this.cpnCharacters = new ExtendedPanel();
      this.label7 = new Label();
      this.cmbCharacters = new CharacterDropDown();
      this.label6 = new Label();
      this.cmbCharacterEmpire = new EmpireDropDown();
      this.cpnAliens = new ExtendedPanel();
      this.label4 = new Label();
      this.cmbPlaceAlienRace = new RaceDropDown();
      this.cpnColony = new ExtendedPanel();
      this.label3 = new Label();
      this.cmbPlaceColonyEmpire = new EmpireDropDown();
      this.cpnBuiltObject = new ExtendedPanel();
      this.label2 = new Label();
      this.label1 = new Label();
      this.cmbPlaceShipDesign = new DesignDropDown();
      this.cmbPlaceShipEmpire = new EmpireDropDown();
      this.cpnAsteroid = new ExtendedPanel();
      this.vwAsteroids = new GenericIconView();
      this.cpnMoon = new ExtendedPanel();
      this.vwMoon = new HabitatTypeIconView();
      this.cpnPlanet = new ExtendedPanel();
      this.vwPlanet = new HabitatTypeIconView();
      this.cpnStar = new ExtendedPanel();
      this.vwStar = new HabitatTypeIconView();
      this.cpnGasCloud = new ExtendedPanel();
      this.vwGasCloud = new HabitatTypeIconView();
      this.cpnSystem = new ExtendedPanel();
      this.vwSystem = new HabitatTypeIconView();
      this.MainPanel.SuspendLayout();
      this.cpnClearItems.SuspendLayout();
      this.cpnEmpireExploration.SuspendLayout();
      this.cpnRuins.SuspendLayout();
      this.cpnPirates.SuspendLayout();
      this.cpnCreature.SuspendLayout();
      this.cpnCharacters.SuspendLayout();
      this.cpnAliens.SuspendLayout();
      this.cpnColony.SuspendLayout();
      this.cpnBuiltObject.SuspendLayout();
      this.cpnAsteroid.SuspendLayout();
      this.cpnMoon.SuspendLayout();
      this.cpnPlanet.SuspendLayout();
      this.cpnStar.SuspendLayout();
      this.cpnGasCloud.SuspendLayout();
      this.cpnSystem.SuspendLayout();
      this.SuspendLayout();
      this.MainPanel.BackColor = Color.FromArgb(32, 32, 48);
      this.MainPanel.Controls.Add((Control) this.cpnClearItems);
      this.MainPanel.Controls.Add((Control) this.cpnEmpireExploration);
      this.MainPanel.Controls.Add((Control) this.cpnRuins);
      this.MainPanel.Controls.Add((Control) this.cpnPirates);
      this.MainPanel.Controls.Add((Control) this.cpnCreature);
      this.MainPanel.Controls.Add((Control) this.cpnCharacters);
      this.MainPanel.Controls.Add((Control) this.cpnAliens);
      this.MainPanel.Controls.Add((Control) this.cpnColony);
      this.MainPanel.Controls.Add((Control) this.cpnBuiltObject);
      this.MainPanel.Controls.Add((Control) this.cpnAsteroid);
      this.MainPanel.Controls.Add((Control) this.cpnMoon);
      this.MainPanel.Controls.Add((Control) this.cpnPlanet);
      this.MainPanel.Controls.Add((Control) this.cpnStar);
      this.MainPanel.Controls.Add((Control) this.cpnGasCloud);
      this.MainPanel.Controls.Add((Control) this.cpnSystem);
      this.MainPanel.Location = new Point(0, 0);
      this.MainPanel.Name = "MainPanel";
      this.MainPanel.Size = new Size(320, 1640);
      this.MainPanel.TabIndex = 0;
      this.cpnClearItems.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnClearItems.BackupSize = new Size(0, 0);
      this.cpnClearItems.BorderColor = Color.Gray;
      this.cpnClearItems.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnClearItems.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnClearItems.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnClearItems.CaptionImage = (Image) componentResourceManager.GetObject("cpnClearItems.CaptionImage");
      this.cpnClearItems.CaptionSize = 20;
      this.cpnClearItems.CaptionText = "Clear Items";
      this.cpnClearItems.CaptionTextColor = Color.White;
      this.cpnClearItems.Controls.Add((Control) this.vwClearItems);
      this.cpnClearItems.CornerStyle = CornerStyle.Normal;
      this.cpnClearItems.DirectionCtrlColor = Color.LightGray;
      this.cpnClearItems.DirectionCtrlHoverColor = Color.White;
      this.cpnClearItems.Dock = DockStyle.Top;
      this.cpnClearItems.ForeColor = Color.White;
      this.cpnClearItems.Location = new Point(0, 1465);
      this.cpnClearItems.Name = "cpnClearItems";
      this.cpnClearItems.Size = new Size(320, 90);
      this.cpnClearItems.State = ExtendedPanelState.Collapsed;
      this.cpnClearItems.TabIndex = 13;
      this.vwClearItems.BackColor = Color.FromArgb(32, 32, 48);
      this.vwClearItems.BorderStyle = BorderStyle.None;
      this.vwClearItems.Dock = DockStyle.Fill;
      this.vwClearItems.Font = new Font("Verdana", 6.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.vwClearItems.ForeColor = Color.White;
      this.vwClearItems.Location = new Point(0, 0);
      this.vwClearItems.MultiSelect = false;
      this.vwClearItems.Name = "vwClearItems";
      this.vwClearItems.Scrollable = false;
      this.vwClearItems.ShowItemToolTips = true;
      this.vwClearItems.Size = new Size(320, 90);
      this.vwClearItems.TabIndex = 1;
      this.vwClearItems.UseCompatibleStateImageBehavior = false;
      this.cpnEmpireExploration.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnEmpireExploration.BackupSize = new Size(0, 0);
      this.cpnEmpireExploration.BorderColor = Color.Gray;
      this.cpnEmpireExploration.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnEmpireExploration.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnEmpireExploration.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnEmpireExploration.CaptionImage = (Image) componentResourceManager.GetObject("cpnEmpireExploration.CaptionImage");
      this.cpnEmpireExploration.CaptionSize = 20;
      this.cpnEmpireExploration.CaptionText = "Empire Exploration";
      this.cpnEmpireExploration.CaptionTextColor = Color.White;
      this.cpnEmpireExploration.Controls.Add((Control) this.label5);
      this.cpnEmpireExploration.Controls.Add((Control) this.cmbSetEmpireExploration);
      this.cpnEmpireExploration.CornerStyle = CornerStyle.Normal;
      this.cpnEmpireExploration.DirectionCtrlColor = Color.LightGray;
      this.cpnEmpireExploration.DirectionCtrlHoverColor = Color.White;
      this.cpnEmpireExploration.Dock = DockStyle.Top;
      this.cpnEmpireExploration.ForeColor = Color.White;
      this.cpnEmpireExploration.Location = new Point(0, 1395);
      this.cpnEmpireExploration.Name = "cpnEmpireExploration";
      this.cpnEmpireExploration.Size = new Size(320, 70);
      this.cpnEmpireExploration.State = ExtendedPanelState.Collapsed;
      this.cpnEmpireExploration.TabIndex = 12;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(4, 33);
      this.label5.Name = "label5";
      this.label5.Size = new Size(50, 14);
      this.label5.TabIndex = 7;
      this.label5.Text = "Empire";
      this.cmbSetEmpireExploration.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbSetEmpireExploration.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSetEmpireExploration.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSetEmpireExploration.FlatStyle = FlatStyle.Popup;
      this.cmbSetEmpireExploration.Font = new Font("Verdana", 8.25f);
      this.cmbSetEmpireExploration.ForeColor = Color.White;
      this.cmbSetEmpireExploration.FormattingEnabled = true;
      this.cmbSetEmpireExploration.Location = new Point(59, 30);
      this.cmbSetEmpireExploration.Name = "cmbSetEmpireExploration";
      this.cmbSetEmpireExploration.Size = new Size(250, 22);
      this.cmbSetEmpireExploration.TabIndex = 1;
      this.cpnRuins.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnRuins.BackupSize = new Size(0, 0);
      this.cpnRuins.BorderColor = Color.Gray;
      this.cpnRuins.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnRuins.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnRuins.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnRuins.CaptionImage = (Image) componentResourceManager.GetObject("cpnRuins.CaptionImage");
      this.cpnRuins.CaptionSize = 20;
      this.cpnRuins.CaptionText = "Ruins";
      this.cpnRuins.CaptionTextColor = Color.White;
      this.cpnRuins.Controls.Add((Control) this.vwRuins);
      this.cpnRuins.CornerStyle = CornerStyle.Normal;
      this.cpnRuins.DirectionCtrlColor = Color.LightGray;
      this.cpnRuins.DirectionCtrlHoverColor = Color.White;
      this.cpnRuins.Dock = DockStyle.Top;
      this.cpnRuins.ForeColor = Color.FromArgb(170, 170, 170);
      this.cpnRuins.Location = new Point(0, 1275);
      this.cpnRuins.Name = "cpnRuins";
      this.cpnRuins.Size = new Size(320, 120);
      this.cpnRuins.State = ExtendedPanelState.Collapsed;
      this.cpnRuins.TabIndex = 11;
      this.vwRuins.BackColor = Color.FromArgb(32, 32, 48);
      this.vwRuins.BorderStyle = BorderStyle.None;
      this.vwRuins.Dock = DockStyle.Fill;
      this.vwRuins.Font = new Font("Verdana", 8.25f);
      this.vwRuins.ForeColor = Color.White;
      this.vwRuins.Location = new Point(0, 0);
      this.vwRuins.MultiSelect = false;
      this.vwRuins.Name = "vwRuins";
      this.vwRuins.Scrollable = false;
      this.vwRuins.ShowItemToolTips = true;
      this.vwRuins.Size = new Size(320, 120);
      this.vwRuins.TabIndex = 2;
      this.vwRuins.UseCompatibleStateImageBehavior = false;
      this.cpnPirates.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnPirates.BackupSize = new Size(0, 0);
      this.cpnPirates.BorderColor = Color.Gray;
      this.cpnPirates.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnPirates.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnPirates.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnPirates.CaptionImage = (Image) componentResourceManager.GetObject("cpnPirates.CaptionImage");
      this.cpnPirates.CaptionSize = 20;
      this.cpnPirates.CaptionText = "Pirates and Ruins";
      this.cpnPirates.CaptionTextColor = Color.White;
      this.cpnPirates.Controls.Add((Control) this.vwPirates);
      this.cpnPirates.CornerStyle = CornerStyle.Normal;
      this.cpnPirates.DirectionCtrlColor = Color.LightGray;
      this.cpnPirates.DirectionCtrlHoverColor = Color.White;
      this.cpnPirates.Dock = DockStyle.Top;
      this.cpnPirates.ForeColor = Color.White;
      this.cpnPirates.Location = new Point(0, 1130);
      this.cpnPirates.Name = "cpnPirates";
      this.cpnPirates.Size = new Size(320, 145);
      this.cpnPirates.State = ExtendedPanelState.Collapsed;
      this.cpnPirates.TabIndex = 10;
      this.vwPirates.BackColor = Color.FromArgb(32, 32, 48);
      this.vwPirates.BorderStyle = BorderStyle.None;
      this.vwPirates.Dock = DockStyle.Fill;
      this.vwPirates.Font = new Font("Verdana", 8.25f);
      this.vwPirates.ForeColor = Color.White;
      this.vwPirates.Location = new Point(0, 0);
      this.vwPirates.MultiSelect = false;
      this.vwPirates.Name = "vwPirates";
      this.vwPirates.Scrollable = false;
      this.vwPirates.ShowItemToolTips = true;
      this.vwPirates.Size = new Size(320, 145);
      this.vwPirates.TabIndex = 1;
      this.vwPirates.UseCompatibleStateImageBehavior = false;
      this.cpnCreature.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnCreature.BackupSize = new Size(0, 0);
      this.cpnCreature.BorderColor = Color.Gray;
      this.cpnCreature.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnCreature.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnCreature.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnCreature.CaptionImage = (Image) componentResourceManager.GetObject("cpnCreature.CaptionImage");
      this.cpnCreature.CaptionSize = 20;
      this.cpnCreature.CaptionText = "Space Creatures";
      this.cpnCreature.CaptionTextColor = Color.White;
      this.cpnCreature.Controls.Add((Control) this.vwCreature);
      this.cpnCreature.CornerStyle = CornerStyle.Normal;
      this.cpnCreature.DirectionCtrlColor = Color.LightGray;
      this.cpnCreature.DirectionCtrlHoverColor = Color.White;
      this.cpnCreature.Dock = DockStyle.Top;
      this.cpnCreature.ForeColor = Color.White;
      this.cpnCreature.Location = new Point(0, 1030);
      this.cpnCreature.Name = "cpnCreature";
      this.cpnCreature.Size = new Size(320, 100);
      this.cpnCreature.State = ExtendedPanelState.Collapsed;
      this.cpnCreature.TabIndex = 9;
      this.vwCreature.BackColor = Color.FromArgb(32, 32, 48);
      this.vwCreature.BorderStyle = BorderStyle.None;
      this.vwCreature.Dock = DockStyle.Fill;
      this.vwCreature.Font = new Font("Verdana", 6.75f);
      this.vwCreature.ForeColor = Color.White;
      this.vwCreature.Location = new Point(0, 0);
      this.vwCreature.MultiSelect = false;
      this.vwCreature.Name = "vwCreature";
      this.vwCreature.Scrollable = false;
      this.vwCreature.ShowItemToolTips = true;
      this.vwCreature.Size = new Size(320, 100);
      this.vwCreature.TabIndex = 1;
      this.vwCreature.UseCompatibleStateImageBehavior = false;
      this.cpnCharacters.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnCharacters.BackupSize = new Size(0, 0);
      this.cpnCharacters.BorderColor = Color.Gray;
      this.cpnCharacters.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnCharacters.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnCharacters.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnCharacters.CaptionImage = (Image) componentResourceManager.GetObject("cpnCharacters.CaptionImage");
      this.cpnCharacters.CaptionSize = 20;
      this.cpnCharacters.CaptionText = "Characters";
      this.cpnCharacters.CaptionTextColor = Color.White;
      this.cpnCharacters.Controls.Add((Control) this.label7);
      this.cpnCharacters.Controls.Add((Control) this.cmbCharacters);
      this.cpnCharacters.Controls.Add((Control) this.label6);
      this.cpnCharacters.Controls.Add((Control) this.cmbCharacterEmpire);
      this.cpnCharacters.CornerStyle = CornerStyle.Normal;
      this.cpnCharacters.DirectionCtrlColor = Color.LightGray;
      this.cpnCharacters.DirectionCtrlHoverColor = Color.White;
      this.cpnCharacters.Dock = DockStyle.Top;
      this.cpnCharacters.ForeColor = Color.FromArgb(170, 170, 170);
      this.cpnCharacters.Location = new Point(0, 930);
      this.cpnCharacters.Name = "cpnCharacters";
      this.cpnCharacters.Size = new Size(320, 100);
      this.cpnCharacters.TabIndex = 14;
      this.label7.AutoSize = true;
      this.label7.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7.Location = new Point(4, 61);
      this.label7.Name = "label7";
      this.label7.Size = new Size(34, 14);
      this.label7.TabIndex = 10;
      this.label7.Text = "Role";
      this.cmbCharacters.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbCharacters.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCharacters.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharacters.FlatStyle = FlatStyle.Popup;
      this.cmbCharacters.Font = new Font("Verdana", 8.25f);
      this.cmbCharacters.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbCharacters.FormattingEnabled = true;
      this.cmbCharacters.Location = new Point(59, 59);
      this.cmbCharacters.Name = "cmbCharacters";
      this.cmbCharacters.Size = new Size(250, 22);
      this.cmbCharacters.TabIndex = 9;
      this.label6.AutoSize = true;
      this.label6.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6.Location = new Point(4, 30);
      this.label6.Name = "label6";
      this.label6.Size = new Size(50, 14);
      this.label6.TabIndex = 8;
      this.label6.Text = "Empire";
      this.cmbCharacterEmpire.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbCharacterEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbCharacterEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbCharacterEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbCharacterEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbCharacterEmpire.ForeColor = Color.White;
      this.cmbCharacterEmpire.FormattingEnabled = true;
      this.cmbCharacterEmpire.Location = new Point(59, 28);
      this.cmbCharacterEmpire.Name = "cmbCharacterEmpire";
      this.cmbCharacterEmpire.Size = new Size(250, 22);
      this.cmbCharacterEmpire.TabIndex = 2;
      this.cmbCharacterEmpire.SelectedIndexChanged += new EventHandler(this.cmbCharacterEmpire_SelectedIndexChanged);
      this.cpnAliens.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnAliens.BackupSize = new Size(0, 0);
      this.cpnAliens.BorderColor = Color.Gray;
      this.cpnAliens.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnAliens.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnAliens.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnAliens.CaptionImage = (Image) componentResourceManager.GetObject("cpnAliens.CaptionImage");
      this.cpnAliens.CaptionSize = 20;
      this.cpnAliens.CaptionText = "Independent Alien Races";
      this.cpnAliens.CaptionTextColor = Color.White;
      this.cpnAliens.Controls.Add((Control) this.label4);
      this.cpnAliens.Controls.Add((Control) this.cmbPlaceAlienRace);
      this.cpnAliens.CornerStyle = CornerStyle.Normal;
      this.cpnAliens.DirectionCtrlColor = Color.LightGray;
      this.cpnAliens.DirectionCtrlHoverColor = Color.White;
      this.cpnAliens.Dock = DockStyle.Top;
      this.cpnAliens.ForeColor = Color.White;
      this.cpnAliens.Location = new Point(0, 860);
      this.cpnAliens.Name = "cpnAliens";
      this.cpnAliens.Size = new Size(320, 70);
      this.cpnAliens.State = ExtendedPanelState.Collapsed;
      this.cpnAliens.TabIndex = 8;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(4, 33);
      this.label4.Name = "label4";
      this.label4.Size = new Size(37, 14);
      this.label4.TabIndex = 6;
      this.label4.Text = "Race";
      this.cmbPlaceAlienRace.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbPlaceAlienRace.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPlaceAlienRace.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPlaceAlienRace.FlatStyle = FlatStyle.Popup;
      this.cmbPlaceAlienRace.Font = new Font("Verdana", 8.25f);
      this.cmbPlaceAlienRace.ForeColor = Color.White;
      this.cmbPlaceAlienRace.FormattingEnabled = true;
      this.cmbPlaceAlienRace.Location = new Point(59, 30);
      this.cmbPlaceAlienRace.Name = "cmbPlaceAlienRace";
      this.cmbPlaceAlienRace.Size = new Size(250, 22);
      this.cmbPlaceAlienRace.TabIndex = 1;
      this.cpnColony.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnColony.BackupSize = new Size(0, 0);
      this.cpnColony.BorderColor = Color.Gray;
      this.cpnColony.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnColony.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnColony.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnColony.CaptionImage = (Image) componentResourceManager.GetObject("cpnColony.CaptionImage");
      this.cpnColony.CaptionSize = 20;
      this.cpnColony.CaptionText = "Colonies";
      this.cpnColony.CaptionTextColor = Color.White;
      this.cpnColony.Controls.Add((Control) this.label3);
      this.cpnColony.Controls.Add((Control) this.cmbPlaceColonyEmpire);
      this.cpnColony.CornerStyle = CornerStyle.Normal;
      this.cpnColony.DirectionCtrlColor = Color.LightGray;
      this.cpnColony.DirectionCtrlHoverColor = Color.White;
      this.cpnColony.Dock = DockStyle.Top;
      this.cpnColony.ForeColor = Color.White;
      this.cpnColony.Location = new Point(0, 790);
      this.cpnColony.Name = "cpnColony";
      this.cpnColony.Size = new Size(320, 70);
      this.cpnColony.State = ExtendedPanelState.Collapsed;
      this.cpnColony.TabIndex = 7;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(4, 33);
      this.label3.Name = "label3";
      this.label3.Size = new Size(50, 14);
      this.label3.TabIndex = 5;
      this.label3.Text = "Empire";
      this.cmbPlaceColonyEmpire.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbPlaceColonyEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPlaceColonyEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPlaceColonyEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbPlaceColonyEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbPlaceColonyEmpire.ForeColor = Color.White;
      this.cmbPlaceColonyEmpire.FormattingEnabled = true;
      this.cmbPlaceColonyEmpire.Location = new Point(59, 30);
      this.cmbPlaceColonyEmpire.Name = "cmbPlaceColonyEmpire";
      this.cmbPlaceColonyEmpire.Size = new Size(250, 22);
      this.cmbPlaceColonyEmpire.TabIndex = 4;
      this.cpnBuiltObject.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnBuiltObject.BackupSize = new Size(0, 0);
      this.cpnBuiltObject.BorderColor = Color.Gray;
      this.cpnBuiltObject.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnBuiltObject.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnBuiltObject.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnBuiltObject.CaptionImage = (Image) componentResourceManager.GetObject("cpnBuiltObject.CaptionImage");
      this.cpnBuiltObject.CaptionSize = 20;
      this.cpnBuiltObject.CaptionText = "Ships and Bases";
      this.cpnBuiltObject.CaptionTextColor = Color.White;
      this.cpnBuiltObject.Controls.Add((Control) this.label2);
      this.cpnBuiltObject.Controls.Add((Control) this.label1);
      this.cpnBuiltObject.Controls.Add((Control) this.cmbPlaceShipDesign);
      this.cpnBuiltObject.Controls.Add((Control) this.cmbPlaceShipEmpire);
      this.cpnBuiltObject.CornerStyle = CornerStyle.Normal;
      this.cpnBuiltObject.DirectionCtrlColor = Color.LightGray;
      this.cpnBuiltObject.DirectionCtrlHoverColor = Color.White;
      this.cpnBuiltObject.Dock = DockStyle.Top;
      this.cpnBuiltObject.ForeColor = Color.White;
      this.cpnBuiltObject.Location = new Point(0, 690);
      this.cpnBuiltObject.Name = "cpnBuiltObject";
      this.cpnBuiltObject.Size = new Size(320, 100);
      this.cpnBuiltObject.State = ExtendedPanelState.Collapsed;
      this.cpnBuiltObject.TabIndex = 6;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(4, 64);
      this.label2.Name = "label2";
      this.label2.Size = new Size(50, 14);
      this.label2.TabIndex = 4;
      this.label2.Text = "Design";
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Verdana", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(4, 33);
      this.label1.Name = "label1";
      this.label1.Size = new Size(50, 14);
      this.label1.TabIndex = 3;
      this.label1.Text = "Empire";
      this.cmbPlaceShipDesign.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbPlaceShipDesign.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPlaceShipDesign.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPlaceShipDesign.FlatStyle = FlatStyle.Popup;
      this.cmbPlaceShipDesign.Font = new Font("Verdana", 8.25f);
      this.cmbPlaceShipDesign.ForeColor = Color.White;
      this.cmbPlaceShipDesign.FormattingEnabled = true;
      this.cmbPlaceShipDesign.Location = new Point(59, 61);
      this.cmbPlaceShipDesign.Name = "cmbPlaceShipDesign";
      this.cmbPlaceShipDesign.Size = new Size(250, 22);
      this.cmbPlaceShipDesign.TabIndex = 2;
      this.cmbPlaceShipEmpire.BackColor = Color.FromArgb(24, 24, 32);
      this.cmbPlaceShipEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbPlaceShipEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbPlaceShipEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbPlaceShipEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbPlaceShipEmpire.ForeColor = Color.White;
      this.cmbPlaceShipEmpire.FormattingEnabled = true;
      this.cmbPlaceShipEmpire.Location = new Point(59, 30);
      this.cmbPlaceShipEmpire.Name = "cmbPlaceShipEmpire";
      this.cmbPlaceShipEmpire.Size = new Size(250, 22);
      this.cmbPlaceShipEmpire.TabIndex = 1;
      this.cmbPlaceShipEmpire.SelectedIndexChanged += new EventHandler(this.cmbPlaceShipEmpire_SelectedIndexChanged);
      this.cpnAsteroid.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnAsteroid.BackupSize = new Size(0, 0);
      this.cpnAsteroid.BorderColor = Color.Gray;
      this.cpnAsteroid.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnAsteroid.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnAsteroid.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnAsteroid.CaptionImage = (Image) componentResourceManager.GetObject("cpnAsteroid.CaptionImage");
      this.cpnAsteroid.CaptionSize = 20;
      this.cpnAsteroid.CaptionText = "Asteroids";
      this.cpnAsteroid.CaptionTextColor = Color.White;
      this.cpnAsteroid.Controls.Add((Control) this.vwAsteroids);
      this.cpnAsteroid.CornerStyle = CornerStyle.Normal;
      this.cpnAsteroid.DirectionCtrlColor = Color.LightGray;
      this.cpnAsteroid.DirectionCtrlHoverColor = Color.White;
      this.cpnAsteroid.Dock = DockStyle.Top;
      this.cpnAsteroid.ForeColor = Color.White;
      this.cpnAsteroid.Location = new Point(0, 605);
      this.cpnAsteroid.Name = "cpnAsteroid";
      this.cpnAsteroid.Size = new Size(320, 85);
      this.cpnAsteroid.State = ExtendedPanelState.Collapsed;
      this.cpnAsteroid.TabIndex = 5;
      this.vwAsteroids.BackColor = Color.FromArgb(32, 32, 48);
      this.vwAsteroids.BorderStyle = BorderStyle.None;
      this.vwAsteroids.Dock = DockStyle.Fill;
      this.vwAsteroids.Font = new Font("Verdana", 8.25f);
      this.vwAsteroids.ForeColor = Color.White;
      this.vwAsteroids.Location = new Point(0, 0);
      this.vwAsteroids.MultiSelect = false;
      this.vwAsteroids.Name = "vwAsteroids";
      this.vwAsteroids.Scrollable = false;
      this.vwAsteroids.ShowItemToolTips = true;
      this.vwAsteroids.Size = new Size(320, 85);
      this.vwAsteroids.TabIndex = 1;
      this.vwAsteroids.UseCompatibleStateImageBehavior = false;
      this.cpnMoon.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnMoon.BackupSize = new Size(0, 0);
      this.cpnMoon.BorderColor = Color.Gray;
      this.cpnMoon.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnMoon.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnMoon.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnMoon.CaptionImage = (Image) componentResourceManager.GetObject("cpnMoon.CaptionImage");
      this.cpnMoon.CaptionSize = 20;
      this.cpnMoon.CaptionText = "Moons";
      this.cpnMoon.CaptionTextColor = Color.White;
      this.cpnMoon.Controls.Add((Control) this.vwMoon);
      this.cpnMoon.CornerStyle = CornerStyle.Normal;
      this.cpnMoon.DirectionCtrlColor = Color.LightGray;
      this.cpnMoon.DirectionCtrlHoverColor = Color.White;
      this.cpnMoon.Dock = DockStyle.Top;
      this.cpnMoon.ForeColor = Color.White;
      this.cpnMoon.Location = new Point(0, 460);
      this.cpnMoon.Name = "cpnMoon";
      this.cpnMoon.Size = new Size(320, 145);
      this.cpnMoon.State = ExtendedPanelState.Collapsed;
      this.cpnMoon.TabIndex = 4;
      this.vwMoon.BackColor = Color.FromArgb(32, 32, 48);
      this.vwMoon.BorderStyle = BorderStyle.None;
      this.vwMoon.Dock = DockStyle.Fill;
      this.vwMoon.Font = new Font("Verdana", 6.75f);
      this.vwMoon.ForeColor = Color.White;
      this.vwMoon.Location = new Point(0, 0);
      this.vwMoon.Margin = new Padding(5);
      this.vwMoon.MultiSelect = false;
      this.vwMoon.Name = "vwMoon";
      this.vwMoon.Scrollable = false;
      this.vwMoon.ShowItemToolTips = true;
      this.vwMoon.Size = new Size(320, 145);
      this.vwMoon.TabIndex = 1;
      this.vwMoon.UseCompatibleStateImageBehavior = false;
      this.cpnPlanet.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnPlanet.BackupSize = new Size(0, 0);
      this.cpnPlanet.BorderColor = Color.Gray;
      this.cpnPlanet.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnPlanet.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnPlanet.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnPlanet.CaptionImage = (Image) componentResourceManager.GetObject("cpnPlanet.CaptionImage");
      this.cpnPlanet.CaptionSize = 20;
      this.cpnPlanet.CaptionText = "Planets";
      this.cpnPlanet.CaptionTextColor = Color.White;
      this.cpnPlanet.Controls.Add((Control) this.vwPlanet);
      this.cpnPlanet.CornerStyle = CornerStyle.Normal;
      this.cpnPlanet.DirectionCtrlColor = Color.LightGray;
      this.cpnPlanet.DirectionCtrlHoverColor = Color.White;
      this.cpnPlanet.Dock = DockStyle.Top;
      this.cpnPlanet.ForeColor = Color.White;
      this.cpnPlanet.Location = new Point(0, 315);
      this.cpnPlanet.Name = "cpnPlanet";
      this.cpnPlanet.Size = new Size(320, 145);
      this.cpnPlanet.State = ExtendedPanelState.Collapsed;
      this.cpnPlanet.TabIndex = 3;
      this.vwPlanet.BackColor = Color.FromArgb(32, 32, 48);
      this.vwPlanet.BorderStyle = BorderStyle.None;
      this.vwPlanet.Dock = DockStyle.Fill;
      this.vwPlanet.Font = new Font("Verdana", 6.75f);
      this.vwPlanet.ForeColor = Color.White;
      this.vwPlanet.Location = new Point(0, 0);
      this.vwPlanet.Margin = new Padding(5);
      this.vwPlanet.MultiSelect = false;
      this.vwPlanet.Name = "vwPlanet";
      this.vwPlanet.Scrollable = false;
      this.vwPlanet.ShowItemToolTips = true;
      this.vwPlanet.Size = new Size(320, 145);
      this.vwPlanet.TabIndex = 1;
      this.vwPlanet.UseCompatibleStateImageBehavior = false;
      this.cpnStar.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnStar.BackupSize = new Size(0, 0);
      this.cpnStar.BorderColor = Color.Gray;
      this.cpnStar.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnStar.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnStar.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnStar.CaptionImage = (Image) componentResourceManager.GetObject("cpnStar.CaptionImage");
      this.cpnStar.CaptionSize = 20;
      this.cpnStar.CaptionText = "Stars";
      this.cpnStar.CaptionTextColor = Color.White;
      this.cpnStar.Controls.Add((Control) this.vwStar);
      this.cpnStar.CornerStyle = CornerStyle.Normal;
      this.cpnStar.DirectionCtrlColor = Color.LightGray;
      this.cpnStar.DirectionCtrlHoverColor = Color.White;
      this.cpnStar.Dock = DockStyle.Top;
      this.cpnStar.ForeColor = Color.White;
      this.cpnStar.Location = new Point(0, 230);
      this.cpnStar.Name = "cpnStar";
      this.cpnStar.Size = new Size(320, 85);
      this.cpnStar.State = ExtendedPanelState.Collapsed;
      this.cpnStar.TabIndex = 2;
      this.vwStar.BackColor = Color.FromArgb(32, 32, 48);
      this.vwStar.BorderStyle = BorderStyle.None;
      this.vwStar.Dock = DockStyle.Fill;
      this.vwStar.Font = new Font("Verdana", 6.75f);
      this.vwStar.ForeColor = Color.White;
      this.vwStar.Location = new Point(0, 0);
      this.vwStar.Margin = new Padding(5);
      this.vwStar.MultiSelect = false;
      this.vwStar.Name = "vwStar";
      this.vwStar.Scrollable = false;
      this.vwStar.ShowItemToolTips = true;
      this.vwStar.Size = new Size(320, 85);
      this.vwStar.TabIndex = 1;
      this.vwStar.UseCompatibleStateImageBehavior = false;
      this.cpnGasCloud.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnGasCloud.BackupSize = new Size(0, 0);
      this.cpnGasCloud.BorderColor = Color.Gray;
      this.cpnGasCloud.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnGasCloud.CaptionColorTwo = Color.FromArgb(48, 48, 64);
      this.cpnGasCloud.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnGasCloud.CaptionImage = (Image) componentResourceManager.GetObject("cpnGasCloud.CaptionImage");
      this.cpnGasCloud.CaptionSize = 20;
      this.cpnGasCloud.CaptionText = "Gas Clouds";
      this.cpnGasCloud.CaptionTextColor = Color.White;
      this.cpnGasCloud.Controls.Add((Control) this.vwGasCloud);
      this.cpnGasCloud.CornerStyle = CornerStyle.Normal;
      this.cpnGasCloud.DirectionCtrlColor = Color.LightGray;
      this.cpnGasCloud.DirectionCtrlHoverColor = Color.White;
      this.cpnGasCloud.Dock = DockStyle.Top;
      this.cpnGasCloud.ForeColor = Color.White;
      this.cpnGasCloud.Location = new Point(0, 85);
      this.cpnGasCloud.Name = "cpnGasCloud";
      this.cpnGasCloud.Size = new Size(320, 145);
      this.cpnGasCloud.State = ExtendedPanelState.Collapsed;
      this.cpnGasCloud.TabIndex = 1;
      this.vwGasCloud.BackColor = Color.FromArgb(32, 32, 48);
      this.vwGasCloud.BorderStyle = BorderStyle.None;
      this.vwGasCloud.Dock = DockStyle.Fill;
      this.vwGasCloud.Font = new Font("Verdana", 6.75f);
      this.vwGasCloud.ForeColor = Color.White;
      this.vwGasCloud.Location = new Point(0, 0);
      this.vwGasCloud.Margin = new Padding(5);
      this.vwGasCloud.MultiSelect = false;
      this.vwGasCloud.Name = "vwGasCloud";
      this.vwGasCloud.Scrollable = false;
      this.vwGasCloud.ShowItemToolTips = true;
      this.vwGasCloud.Size = new Size(320, 145);
      this.vwGasCloud.TabIndex = 1;
      this.vwGasCloud.UseCompatibleStateImageBehavior = false;
      this.cpnSystem.BackColor = Color.FromArgb(32, 32, 48);
      this.cpnSystem.BackupSize = new Size(0, 0);
      this.cpnSystem.BorderColor = Color.Gray;
      this.cpnSystem.CaptionColorOne = Color.FromArgb(128, 128, 152);
      this.cpnSystem.CaptionColorTwo = Color.FromArgb(48, 48, 56);
      this.cpnSystem.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.cpnSystem.CaptionImage = (Image) componentResourceManager.GetObject("cpnSystem.CaptionImage");
      this.cpnSystem.CaptionSize = 20;
      this.cpnSystem.CaptionText = "Systems";
      this.cpnSystem.CaptionTextColor = Color.White;
      this.cpnSystem.Controls.Add((Control) this.vwSystem);
      this.cpnSystem.CornerStyle = CornerStyle.Normal;
      this.cpnSystem.DirectionCtrlColor = Color.LightGray;
      this.cpnSystem.DirectionCtrlHoverColor = Color.White;
      this.cpnSystem.Dock = DockStyle.Top;
      this.cpnSystem.ForeColor = Color.White;
      this.cpnSystem.Location = new Point(0, 0);
      this.cpnSystem.Name = "cpnSystem";
      this.cpnSystem.Size = new Size(320, 85);
      this.cpnSystem.State = ExtendedPanelState.Collapsed;
      this.cpnSystem.TabIndex = 0;
      this.vwSystem.BackColor = Color.FromArgb(32, 32, 48);
      this.vwSystem.BorderStyle = BorderStyle.None;
      this.vwSystem.Dock = DockStyle.Fill;
      this.vwSystem.Font = new Font("Verdana", 6.75f);
      this.vwSystem.ForeColor = Color.White;
      this.vwSystem.Location = new Point(0, 0);
      this.vwSystem.Margin = new Padding(5);
      this.vwSystem.MultiSelect = false;
      this.vwSystem.Name = "vwSystem";
      this.vwSystem.Scrollable = false;
      this.vwSystem.ShowItemToolTips = true;
      this.vwSystem.Size = new Size(320, 85);
      this.vwSystem.TabIndex = 1;
      this.vwSystem.UseCompatibleStateImageBehavior = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.FromArgb(32, 32, 48);
      this.Controls.Add((Control) this.MainPanel);
      this.Name = nameof (CollapsingHabitatTypeSelector);
      this.Size = new Size(320, 1640);
      this.AutoSizeChanged += new EventHandler(this.CollapsingHabitatTypeSelector_AutoSizeChanged);
      this.SizeChanged += new EventHandler(this.CollapsingHabitatTypeSelector_SizeChanged);
      this.Resize += new EventHandler(this.CollapsingHabitatTypeSelector_Resize);
      this.MainPanel.ResumeLayout(false);
      this.cpnClearItems.ResumeLayout(false);
      this.cpnEmpireExploration.ResumeLayout(false);
      this.cpnEmpireExploration.PerformLayout();
      this.cpnRuins.ResumeLayout(false);
      this.cpnPirates.ResumeLayout(false);
      this.cpnCreature.ResumeLayout(false);
      this.cpnCharacters.ResumeLayout(false);
      this.cpnCharacters.PerformLayout();
      this.cpnAliens.ResumeLayout(false);
      this.cpnAliens.PerformLayout();
      this.cpnColony.ResumeLayout(false);
      this.cpnColony.PerformLayout();
      this.cpnBuiltObject.ResumeLayout(false);
      this.cpnBuiltObject.PerformLayout();
      this.cpnAsteroid.ResumeLayout(false);
      this.cpnMoon.ResumeLayout(false);
      this.cpnPlanet.ResumeLayout(false);
      this.cpnStar.ResumeLayout(false);
      this.cpnGasCloud.ResumeLayout(false);
      this.cpnSystem.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    public CollapsingHabitatTypeSelector()
    {
      this.InitializeComponent();
      this.SetMainPanelSize();
      this.AttachEvents();
    }

    private void AttachEvents()
    {
      this.cpnAsteroid.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnBuiltObject.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnGasCloud.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnMoon.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnPlanet.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnStar.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnSystem.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnColony.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnAliens.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnCreature.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnPirates.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnRuins.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnEmpireExploration.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnClearItems.CaptionClicked += new EventHandler(this.OnCaptionClicked);
      this.cpnCharacters.CaptionClicked += new EventHandler(this.OnCaptionClicked);
    }

    private void CollapsingHabitatTypeSelector_Resize(object sender, EventArgs e) => this.SetMainPanelSize();

    private void SetMainPanelSize()
    {
      this.MainPanel.Location = new Point(0, 0);
      this.MainPanel.Size = this.Size;
    }

    private void CollapsingHabitatTypeSelector_SizeChanged(object sender, EventArgs e) => this.SetMainPanelSize();

    private void CollapsingHabitatTypeSelector_AutoSizeChanged(object sender, EventArgs e) => this.SetMainPanelSize();

    private void SetFonts()
    {
      Font font1 = new Font(this.Font.FontFamily, 15.33f, FontStyle.Regular, GraphicsUnit.Pixel);
      Font font2 = new Font(this.Font.FontFamily, 16.67f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.vwAsteroids.Font = font1;
      this.vwClearItems.Font = font1;
      this.vwCreature.Font = font1;
      this.vwGasCloud.Font = font1;
      this.vwMoon.Font = font1;
      this.vwPirates.Font = font1;
      this.vwRuins.Font = font1;
      this.vwPlanet.Font = font1;
      this.vwStar.Font = font1;
      this.vwSystem.Font = font1;
      this.cpnAliens.CaptionFont = font2;
      this.cpnAsteroid.CaptionFont = font2;
      this.cpnBuiltObject.CaptionFont = font2;
      this.cpnClearItems.CaptionFont = font2;
      this.cpnColony.CaptionFont = font2;
      this.cpnCreature.CaptionFont = font2;
      this.cpnEmpireExploration.CaptionFont = font2;
      this.cpnGasCloud.CaptionFont = font2;
      this.cpnMoon.CaptionFont = font2;
      this.cpnPirates.CaptionFont = font2;
      this.cpnRuins.CaptionFont = font2;
      this.cpnPlanet.CaptionFont = font2;
      this.cpnStar.CaptionFont = font2;
      this.cpnSystem.CaptionFont = font2;
      this.cpnCharacters.CaptionFont = font2;
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Races = (RaceList) null;
      this._Empires = (DistantWorlds.Types.EmpireList) null;
      this._PirateFactions = (DistantWorlds.Types.EmpireList) null;
      this._PlayerEmpire = (Empire) null;
      this._IndependentEmpire = (Empire) null;
      this.cmbPlaceAlienRace.ClearData();
      this.cmbPlaceColonyEmpire.ClearData();
      this.cmbPlaceShipDesign.ClearData();
      this.cmbPlaceShipEmpire.ClearData();
      this.cmbSetEmpireExploration.ClearData();
      this.cmbCharacters.ClearData();
      this._CharacterImageCache = (CharacterImageCache) null;
    }

    public void BindData(
      Control mouseWheelRefocusControl,
      Galaxy galaxy,
      RaceList races,
      Bitmap[] raceImages,
      DistantWorlds.Types.EmpireList empires,
      DistantWorlds.Types.EmpireList pirateFactions,
      Empire playerEmpire,
      Empire independentEmpire,
      Bitmap[] builtObjectImages,
      Bitmap[] habitatImages,
      Bitmap[] mapStarImages,
      Bitmap novaImage,
      Bitmap gasCloudImage,
      Bitmap[][] creatureImages,
      Bitmap ruinImage,
      Bitmap pirateImage,
      Bitmap debrisImage,
      Bitmap asteroidImage,
      Bitmap asteroidFieldImage,
      Bitmap eraseImage,
      Bitmap eraseColonyImage,
      Bitmap eraseAlienRaceImage,
      Bitmap eraseRuinsImage,
      Bitmap eraseAsteroidFieldImage,
      CharacterImageCache characterImageCache)
    {
      this.SetFonts();
      this._Galaxy = galaxy;
      this._Races = races;
      this._RaceImages = raceImages;
      this._Empires = empires;
      this._PirateFactions = pirateFactions;
      this._PlayerEmpire = playerEmpire;
      this._IndependentEmpire = independentEmpire;
      this._BuiltObjectImages = builtObjectImages;
      this._CharacterImageCache = characterImageCache;
      string str = Application.StartupPath + "\\";
      this.cpnAliens.CaptionText = TextResolver.GetText("Independent Alien Races");
      this.cpnAsteroid.CaptionText = TextResolver.GetText("Asteroids");
      this.cpnBuiltObject.CaptionText = TextResolver.GetText("Ships and Bases");
      this.cpnClearItems.CaptionText = TextResolver.GetText("Clear Items");
      this.cpnColony.CaptionText = TextResolver.GetText("Colonies");
      this.cpnCreature.CaptionText = TextResolver.GetText("Space Creatures");
      this.cpnEmpireExploration.CaptionText = TextResolver.GetText("Empire Exploration");
      this.cpnGasCloud.CaptionText = TextResolver.GetText("Gas Clouds");
      this.cpnMoon.CaptionText = TextResolver.GetText("Moons");
      this.cpnPirates.CaptionText = TextResolver.GetText("Pirates");
      this.cpnRuins.CaptionText = TextResolver.GetText("Ruins");
      this.cpnPlanet.CaptionText = TextResolver.GetText("Planets");
      this.cpnStar.CaptionText = TextResolver.GetText("Stars");
      this.cpnSystem.CaptionText = TextResolver.GetText("Systems");
      this.cpnCharacters.CaptionText = TextResolver.GetText("Characters");
      List<HabitatType> habitatTypes1 = new List<HabitatType>();
      habitatTypes1.Add(HabitatType.MainSequence);
      habitatTypes1.Add(HabitatType.RedGiant);
      habitatTypes1.Add(HabitatType.SuperGiant);
      habitatTypes1.Add(HabitatType.WhiteDwarf);
      habitatTypes1.Add(HabitatType.Neutron);
      Bitmap[] habitatTypeImages1 = new Bitmap[5]
      {
        mapStarImages[GalaxyImages.MapStarImageOffsetMainSequence],
        mapStarImages[GalaxyImages.MapStarImageOffsetRedGiant],
        mapStarImages[GalaxyImages.MapStarImageOffsetSuperGiant],
        mapStarImages[GalaxyImages.MapStarImageOffsetWhiteDwarf],
        mapStarImages[GalaxyImages.MapStarImageOffsetNeutron]
      };
      this.vwSystem.BindData(habitatTypes1, habitatTypeImages1, mouseWheelRefocusControl);
      this.cpnSystem.CheckDocking(0);
      this.cpnSystem.BackupSize = new Size(320, 85);
      this.cpnSystem.Size = new Size(320, 85);
      this.cpnSystem.CheckDocking(0);
      this.vwSystem.View = View.LargeIcon;
      if (this.cpnSystem.State == ExtendedPanelState.Collapsed)
        this.cpnSystem.Expand();
      this.vwSystem.Dock = DockStyle.None;
      this.vwSystem.Height = this.cpnSystem.Height - this.cpnSystem.CaptionSize;
      this.vwSystem.Location = new Point(0, this.cpnSystem.CaptionSize);
      this.vwSystem.Location = new Point(0, this.cpnSystem.CaptionSize);
      this.vwSystem.Size = new Size(this.cpnSystem.Width, this.cpnSystem.Height);
      List<HabitatType> habitatTypes2 = new List<HabitatType>();
      habitatTypes2.Add(HabitatType.Ammonia);
      habitatTypes2.Add(HabitatType.Argon);
      habitatTypes2.Add(HabitatType.CarbonDioxide);
      habitatTypes2.Add(HabitatType.Chlorine);
      habitatTypes2.Add(HabitatType.Helium);
      habitatTypes2.Add(HabitatType.Hydrogen);
      habitatTypes2.Add(HabitatType.NitrogenOxygen);
      habitatTypes2.Add(HabitatType.Oxygen);
      Bitmap[] habitatTypeImages2 = new Bitmap[8]
      {
        gasCloudImage,
        gasCloudImage,
        gasCloudImage,
        gasCloudImage,
        gasCloudImage,
        gasCloudImage,
        gasCloudImage,
        gasCloudImage
      };
      this.vwGasCloud.BindData(habitatTypes2, habitatTypeImages2, mouseWheelRefocusControl);
      this.cpnGasCloud.CheckDocking(0);
      this.cpnGasCloud.BackupSize = new Size(320, 145);
      this.vwGasCloud.View = View.LargeIcon;
      List<HabitatType> habitatTypes3 = new List<HabitatType>();
      habitatTypes3.Add(HabitatType.MainSequence);
      habitatTypes3.Add(HabitatType.RedGiant);
      habitatTypes3.Add(HabitatType.SuperGiant);
      habitatTypes3.Add(HabitatType.WhiteDwarf);
      habitatTypes3.Add(HabitatType.Neutron);
      habitatTypes3.Add(HabitatType.SuperNova);
      habitatTypes3.Add(HabitatType.BlackHole);
      Bitmap[] habitatTypeImages3 = new Bitmap[7]
      {
        mapStarImages[GalaxyImages.MapStarImageOffsetMainSequence],
        mapStarImages[GalaxyImages.MapStarImageOffsetRedGiant],
        mapStarImages[GalaxyImages.MapStarImageOffsetSuperGiant],
        mapStarImages[GalaxyImages.MapStarImageOffsetWhiteDwarf],
        mapStarImages[GalaxyImages.MapStarImageOffsetNeutron],
        novaImage,
        mapStarImages[GalaxyImages.MapStarImageOffsetBlackHole]
      };
      this.vwStar.BindData(habitatTypes3, habitatTypeImages3, mouseWheelRefocusControl);
      this.cpnStar.CheckDocking(0);
      this.cpnStar.BackupSize = new Size(320, 145);
      this.vwStar.View = View.LargeIcon;
      List<HabitatType> habitatTypes4 = new List<HabitatType>();
      habitatTypes4.Add(HabitatType.Continental);
      habitatTypes4.Add(HabitatType.MarshySwamp);
      habitatTypes4.Add(HabitatType.Desert);
      habitatTypes4.Add(HabitatType.Ocean);
      habitatTypes4.Add(HabitatType.Ice);
      habitatTypes4.Add(HabitatType.Volcanic);
      habitatTypes4.Add(HabitatType.BarrenRock);
      habitatTypes4.Add(HabitatType.GasGiant);
      habitatTypes4.Add(HabitatType.FrozenGasGiant);
      Bitmap[] habitatTypeImages4 = new Bitmap[9]
      {
        habitatImages[GalaxyImages.HabitatImageOffsetContinental],
        habitatImages[GalaxyImages.HabitatImageOffsetMarshySwamp],
        habitatImages[GalaxyImages.HabitatImageOffsetDesert],
        habitatImages[GalaxyImages.HabitatImageOffsetOcean],
        habitatImages[GalaxyImages.HabitatImageOffsetIce],
        habitatImages[GalaxyImages.HabitatImageOffsetVolcanic],
        habitatImages[GalaxyImages.HabitatImageOffsetBarrenRock],
        habitatImages[GalaxyImages.HabitatImageOffsetGasGiantAny],
        habitatImages[GalaxyImages.HabitatImageOffsetFrozenGasGiantAny]
      };
      this.vwPlanet.BindData(habitatTypes4, habitatTypeImages4, mouseWheelRefocusControl);
      this.cpnPlanet.CheckDocking(0);
      this.cpnPlanet.BackupSize = new Size(320, 145);
      this.vwPlanet.View = View.LargeIcon;
      List<HabitatType> habitatTypes5 = new List<HabitatType>();
      habitatTypes5.Add(HabitatType.Continental);
      habitatTypes5.Add(HabitatType.MarshySwamp);
      habitatTypes5.Add(HabitatType.Desert);
      habitatTypes5.Add(HabitatType.Ocean);
      habitatTypes5.Add(HabitatType.Ice);
      habitatTypes5.Add(HabitatType.Volcanic);
      habitatTypes5.Add(HabitatType.BarrenRock);
      Bitmap[] habitatTypeImages5 = new Bitmap[7]
      {
        habitatImages[GalaxyImages.HabitatImageOffsetContinental],
        habitatImages[GalaxyImages.HabitatImageOffsetMarshySwamp],
        habitatImages[GalaxyImages.HabitatImageOffsetDesert],
        habitatImages[GalaxyImages.HabitatImageOffsetOcean],
        habitatImages[GalaxyImages.HabitatImageOffsetIce],
        habitatImages[GalaxyImages.HabitatImageOffsetVolcanic],
        habitatImages[GalaxyImages.HabitatImageOffsetBarrenRock]
      };
      this.vwMoon.BindData(habitatTypes5, habitatTypeImages5, mouseWheelRefocusControl);
      this.cpnMoon.CheckDocking(0);
      this.cpnMoon.BackupSize = new Size(320, 145);
      this.vwMoon.View = View.LargeIcon;
      List<CreatureType> creatureTypes = new List<CreatureType>();
      creatureTypes.Add(CreatureType.RockSpaceSlug);
      creatureTypes.Add(CreatureType.DesertSpaceSlug);
      creatureTypes.Add(CreatureType.Kaltor);
      creatureTypes.Add(CreatureType.Ardilus);
      creatureTypes.Add(CreatureType.SilverMist);
      Bitmap[] creatureImages1 = new Bitmap[5]
      {
        new Bitmap((Image) creatureImages[0][0]),
        new Bitmap((Image) creatureImages[1][0]),
        new Bitmap((Image) creatureImages[2][0]),
        new Bitmap((Image) creatureImages[3][0]),
        new Bitmap((Image) creatureImages[4][0])
      };
      for (int index = 0; index < 5; ++index)
        creatureImages1[index].RotateFlip(RotateFlipType.Rotate270FlipNone);
      this.vwCreature.BindData(creatureTypes, creatureImages1, mouseWheelRefocusControl);
      this.cpnCreature.CheckDocking(0);
      this.cpnCreature.BackupSize = new Size(320, 85);
      this.vwCreature.View = View.LargeIcon;
      List<string> values1 = new List<string>();
      values1.Add(TextResolver.GetText("Asteroid"));
      values1.Add(TextResolver.GetText("Asteroid Field - Rock"));
      values1.Add(TextResolver.GetText("Asteroid Field - Metal"));
      values1.Add(TextResolver.GetText("Asteroid Field - Ice"));
      Bitmap[] images1 = new Bitmap[4]
      {
        asteroidImage,
        asteroidFieldImage,
        asteroidFieldImage,
        asteroidFieldImage
      };
      this.vwAsteroids.BindData(values1, images1, mouseWheelRefocusControl);
      this.cpnAsteroid.CheckDocking(0);
      this.cpnAsteroid.BackupSize = new Size(320, 85);
      this.vwAsteroids.View = View.LargeIcon;
      List<string> values2 = new List<string>();
      values2.Add(TextResolver.GetText("Pirate Faction"));
      for (int index = 0; index < races.Count; ++index)
      {
        if (races[index].CanBePirate)
          values2.Add(TextResolver.GetText("Pirate Faction") + ": " + races[index].Name);
      }
      int count = values2.Count;
      Bitmap[] images2 = new Bitmap[values2.Count];
      int index1 = 0;
      for (int index2 = 0; index2 < count; ++index2)
      {
        images2[index1] = pirateImage;
        ++index1;
      }
      this.vwPirates.BindData(values2, images2, mouseWheelRefocusControl);
      this.cpnPirates.CheckDocking(0);
      this.cpnPirates.BackupSize = new Size(320, 350);
      this.vwPirates.View = View.LargeIcon;
      List<string> values3 = new List<string>();
      values3.Add(TextResolver.GetText("Ruins"));
      values3.Add(TextResolver.GetText("Ruins: Special Government"));
      values3.Add(TextResolver.GetText("Ruins: Super Weapon"));
      values3.Add(TextResolver.GetText("Ruins: Sleeping Race"));
      values3.Add(TextResolver.GetText("Ruins: Refugees"));
      values3.Add(TextResolver.GetText("Ruins: Origins"));
      values3.Add(TextResolver.GetText("Ruins: Lost Ship"));
      values3.Add(TextResolver.GetText("Ruins: Lost Colony"));
      values3.Add(TextResolver.GetText("Debris Field"));
      int num1 = 8;
      int num2 = 1;
      Bitmap[] images3 = new Bitmap[values3.Count];
      int index3 = 0;
      for (int index4 = 0; index4 < num1; ++index4)
      {
        images3[index3] = ruinImage;
        ++index3;
      }
      for (int index5 = 0; index5 < num2; ++index5)
      {
        images3[index3] = debrisImage;
        ++index3;
      }
      this.vwRuins.BindData(values3, images3, mouseWheelRefocusControl);
      this.cpnRuins.CheckDocking(0);
      this.cpnRuins.BackupSize = new Size(320, 145);
      this.vwRuins.View = View.LargeIcon;
      List<string> values4 = new List<string>();
      values4.Add(TextResolver.GetText("Erase Items"));
      values4.Add(TextResolver.GetText("Erase Colony"));
      values4.Add(TextResolver.GetText("Erase Alien Race"));
      values4.Add(TextResolver.GetText("Erase Ruins"));
      values4.Add(TextResolver.GetText("Erase Asteroid Field"));
      Bitmap[] images4 = new Bitmap[5]
      {
        eraseImage,
        eraseColonyImage,
        eraseAlienRaceImage,
        eraseRuinsImage,
        eraseAsteroidFieldImage
      };
      this.vwClearItems.BindData(values4, images4, mouseWheelRefocusControl);
      this.cpnClearItems.CheckDocking(0);
      this.cpnClearItems.BackupSize = new Size(320, 95);
      this.vwClearItems.View = View.LargeIcon;
      this.SetEmpireData();
      this.cmbPlaceShipDesign.BindData((DesignList) null, new Bitmap[0], (Empire) null);
    }

    public Empire PlayerEmpire
    {
      get => this._PlayerEmpire;
      set
      {
        this._PlayerEmpire = value;
        this.SetEmpireData();
      }
    }

    public Empire IndependentEmpire
    {
      get => this._IndependentEmpire;
      set
      {
        this._IndependentEmpire = value;
        this.SetEmpireData();
      }
    }

    public DistantWorlds.Types.EmpireList Empires
    {
      get => this._Empires;
      set
      {
        this._Empires = value;
        this.SetEmpireData();
      }
    }

    public DistantWorlds.Types.EmpireList PirateFactions
    {
      get => this._PirateFactions;
      set
      {
        this._PirateFactions = value;
        this.SetEmpireData();
      }
    }

    private void SetEmpireData()
    {
      this.cmbPlaceColonyEmpire.BindData(this._PlayerEmpire, this._Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
      this.cmbPlaceShipEmpire.BindData(this._PlayerEmpire, this._Empires, this._PirateFactions, this._IndependentEmpire, true);
      this.cmbPlaceAlienRace.BindData(this.Font, this._Races, this._RaceImages);
      this.cmbSetEmpireExploration.BindData(this._PlayerEmpire, this._Empires, (DistantWorlds.Types.EmpireList) null, (Empire) null, false);
      this.cmbCharacterEmpire.BindData(this._PlayerEmpire, this._Empires, this._PirateFactions, (Empire) null, false);
    }

    private Bitmap SafeLoadImage(string imagePath, bool required) => this.SafeLoadImage(imagePath, required, int.MaxValue);

    private Bitmap SafeLoadImage(string imagePath, bool required, int maximumSize)
    {
      Bitmap bitmap = (Bitmap) null;
      if (!File.Exists(imagePath))
      {
        if (!required)
          return (Bitmap) null;
      }
      else
      {
        if (maximumSize < int.MaxValue)
        {
          if (new FileInfo(imagePath).Length > (long) maximumSize)
          {
            int num1 = maximumSize / 1024;
            int num2 = (int) MessageBox.Show(string.Format(TextResolver.GetText("Image too big"), (object) imagePath, (object) num1.ToString()), TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(-1);
          }
        }
        try
        {
          bitmap = new Bitmap(imagePath);
        }
        catch (Exception ex)
        {
          bitmap = (Bitmap) null;
        }
      }
      if (bitmap != null)
        return bitmap;
      int num = (int) MessageBox.Show(string.Format(TextResolver.GetText("Could not load required image"), (object) imagePath), TextResolver.GetText("Error loading file"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      Environment.Exit(-1);
      return (Bitmap) null;
    }

    private void CollapseAll()
    {
      this.cpnAsteroid.State = ExtendedPanelState.Collapsed;
      this.cpnBuiltObject.State = ExtendedPanelState.Collapsed;
      this.cpnGasCloud.State = ExtendedPanelState.Collapsed;
      this.cpnMoon.State = ExtendedPanelState.Collapsed;
      this.cpnPlanet.State = ExtendedPanelState.Collapsed;
      this.cpnStar.State = ExtendedPanelState.Collapsed;
      this.cpnSystem.State = ExtendedPanelState.Collapsed;
      this.cpnColony.State = ExtendedPanelState.Collapsed;
      this.cpnAliens.State = ExtendedPanelState.Collapsed;
      this.cpnCharacters.State = ExtendedPanelState.Collapsed;
      this.cpnCreature.State = ExtendedPanelState.Collapsed;
      this.cpnPirates.State = ExtendedPanelState.Collapsed;
      this.cpnRuins.State = ExtendedPanelState.Collapsed;
      this.cpnEmpireExploration.State = ExtendedPanelState.Collapsed;
      this.cpnClearItems.State = ExtendedPanelState.Collapsed;
    }

    private void OnCaptionClicked(object sender, EventArgs e)
    {
      if (!(sender is CaptionCtrl))
        return;
      this.OpenPanel((ExtendedPanel) ((Control) sender).Parent);
    }

    private void CheckAndCollapsePanel(ExtendedPanel panel)
    {
      if (panel.State == ExtendedPanelState.Collapsed || panel.State == ExtendedPanelState.Collapsing)
        return;
      panel.Collapse();
      panel.DoTopDock();
    }

    public void OpenPanel(ExtendedPanel panelToOpen)
    {
      this.SuspendLayout();
      if (panelToOpen != this.cpnAsteroid)
        this.CheckAndCollapsePanel(this.cpnAsteroid);
      if (panelToOpen != this.cpnBuiltObject)
        this.CheckAndCollapsePanel(this.cpnBuiltObject);
      if (panelToOpen != this.cpnGasCloud)
        this.CheckAndCollapsePanel(this.cpnGasCloud);
      if (panelToOpen != this.cpnMoon)
        this.CheckAndCollapsePanel(this.cpnMoon);
      if (panelToOpen != this.cpnPlanet)
        this.CheckAndCollapsePanel(this.cpnPlanet);
      if (panelToOpen != this.cpnStar)
        this.CheckAndCollapsePanel(this.cpnStar);
      if (panelToOpen != this.cpnSystem)
        this.CheckAndCollapsePanel(this.cpnSystem);
      if (panelToOpen != this.cpnColony)
        this.CheckAndCollapsePanel(this.cpnColony);
      if (panelToOpen != this.cpnAliens)
        this.CheckAndCollapsePanel(this.cpnAliens);
      if (panelToOpen != this.cpnCreature)
        this.CheckAndCollapsePanel(this.cpnCreature);
      if (panelToOpen != this.cpnPirates)
        this.CheckAndCollapsePanel(this.cpnPirates);
      if (panelToOpen != this.cpnRuins)
        this.CheckAndCollapsePanel(this.cpnRuins);
      if (panelToOpen != this.cpnEmpireExploration)
        this.CheckAndCollapsePanel(this.cpnEmpireExploration);
      if (panelToOpen != this.cpnClearItems)
        this.CheckAndCollapsePanel(this.cpnClearItems);
      if (panelToOpen != this.cpnCharacters)
        this.CheckAndCollapsePanel(this.cpnCharacters);
      this.ResumeLayout(false);
    }

    public CreatureType EditorCreatureType => this.EditMode == EditorMode.Creature ? this.vwCreature.SelectedCreatureType : CreatureType.Undefined;

    public HabitatType EditorHabitatType
    {
      get
      {
        switch (this.EditMode)
        {
          case EditorMode.System:
            return this.vwSystem.SelectedHabitatType;
          case EditorMode.GasCloud:
            return this.vwGasCloud.SelectedHabitatType;
          case EditorMode.Star:
            return this.vwStar.SelectedHabitatType;
          case EditorMode.Planet:
            return this.vwPlanet.SelectedHabitatType;
          case EditorMode.Moon:
            return this.vwMoon.SelectedHabitatType;
          default:
            return HabitatType.Undefined;
        }
      }
    }

    public Race EditorAlienRace => this.EditMode == EditorMode.AlienRace ? this.cmbPlaceAlienRace.SelectedRace : (Race) null;

    public Empire EditorEmpire
    {
      get
      {
        switch (this.EditMode)
        {
          case EditorMode.BuiltObject:
            return this.cmbPlaceShipEmpire.SelectedEmpire;
          case EditorMode.Colony:
            return this.cmbPlaceColonyEmpire.SelectedEmpire;
          case EditorMode.EmpireExploration:
            return this.cmbSetEmpireExploration.SelectedEmpire;
          case EditorMode.Character:
            return this.cmbCharacterEmpire.SelectedEmpire;
          default:
            return (Empire) null;
        }
      }
    }

    public Design EditorDesign => this.EditMode == EditorMode.BuiltObject ? this.cmbPlaceShipDesign.SelectedDesign : (Design) null;

    public EditorMode EditMode
    {
      get
      {
        ExtendedPanel selectedPanel = this.SelectedPanel;
        if (selectedPanel != null)
        {
          if (selectedPanel == this.cpnAliens && this.cmbPlaceAlienRace.SelectedRace != null)
            return EditorMode.AlienRace;
          if (selectedPanel == this.cpnAsteroid)
          {
            if (this.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid"))
              return EditorMode.Asteroid;
            if (this.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid Field - Rock") || this.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid Field - Ice") || this.vwAsteroids.SelectedValue == TextResolver.GetText("Asteroid Field - Metal"))
              return EditorMode.AsteroidField;
          }
          if (selectedPanel == this.cpnBuiltObject && this.cmbPlaceShipDesign.SelectedDesign != null)
            return EditorMode.BuiltObject;
          if (selectedPanel == this.cpnClearItems)
          {
            if (this.vwClearItems.SelectedValue == TextResolver.GetText("Erase Items"))
              return EditorMode.ClearItems;
            if (this.vwClearItems.SelectedValue == TextResolver.GetText("Erase Colony"))
              return EditorMode.ClearColony;
            if (this.vwClearItems.SelectedValue == TextResolver.GetText("Erase Alien Race"))
              return EditorMode.ClearAlienRace;
            if (this.vwClearItems.SelectedValue == TextResolver.GetText("Erase Ruins"))
              return EditorMode.ClearRuins;
            if (this.vwClearItems.SelectedValue == TextResolver.GetText("Erase Asteroid Field"))
              return EditorMode.ClearAsteroidField;
          }
          if (selectedPanel == this.cpnColony && this.cmbPlaceColonyEmpire.SelectedEmpire != null)
            return EditorMode.Colony;
          if (selectedPanel == this.cpnCharacters)
          {
            CharacterRole characterRole = CharacterRole.Undefined;
            Character selectedCharacter = this.cmbCharacters.GetSelectedCharacter(out characterRole);
            if (characterRole != CharacterRole.Undefined || selectedCharacter != null)
              return EditorMode.Character;
          }
          if (selectedPanel == this.cpnCreature && this.vwCreature.SelectedCreatureType != CreatureType.Undefined)
            return EditorMode.Creature;
          if (selectedPanel == this.cpnEmpireExploration && this.cmbSetEmpireExploration.SelectedEmpire != null)
            return EditorMode.EmpireExploration;
          if (selectedPanel == this.cpnGasCloud && this.vwGasCloud.SelectedHabitatType != HabitatType.Undefined)
            return EditorMode.GasCloud;
          if (selectedPanel == this.cpnMoon && this.vwMoon.SelectedHabitatType != HabitatType.Undefined)
            return EditorMode.Moon;
          if (selectedPanel == this.cpnRuins)
          {
            string selectedValue = this.vwRuins.SelectedValue;
            if (selectedValue == TextResolver.GetText("Ruins"))
              return EditorMode.Ruins;
            if (selectedValue == TextResolver.GetText("Ruins: Special Government"))
              return EditorMode.RuinsSpecialGovernment;
            if (selectedValue == TextResolver.GetText("Ruins: Super Weapon"))
              return EditorMode.RuinsSuperWeapon;
            if (selectedValue == TextResolver.GetText("Ruins: Sleeping Race"))
              return EditorMode.RuinsSleepingRace;
            if (selectedValue == TextResolver.GetText("Ruins: Refugees"))
              return EditorMode.RuinsRefugees;
            if (selectedValue == TextResolver.GetText("Ruins: Origins"))
              return EditorMode.RuinsOrigins;
            if (selectedValue == TextResolver.GetText("Ruins: Lost Ship"))
              return EditorMode.RuinsLostShip;
            if (selectedValue == TextResolver.GetText("Ruins: Lost Colony"))
              return EditorMode.RuinsLostColony;
            if (selectedValue == TextResolver.GetText("Debris Field"))
              return EditorMode.DebrisField;
          }
          if (selectedPanel == this.cpnPirates)
          {
            string text = TextResolver.GetText("Pirate Faction");
            string selectedValue = this.vwPirates.SelectedValue;
            if (!string.IsNullOrEmpty(selectedValue) && selectedValue.StartsWith(text))
            {
              if (selectedValue.Length > text.Length + 2)
                this.EditModeExtra = selectedValue.Substring(text.Length + 2, selectedValue.Length - (text.Length + 2)).Trim();
              return EditorMode.Pirates;
            }
          }
          if (selectedPanel == this.cpnPlanet && this.vwPlanet.SelectedHabitatType != HabitatType.Undefined)
            return EditorMode.Planet;
          if (selectedPanel == this.cpnStar && this.vwStar.SelectedHabitatType != HabitatType.Undefined)
            return EditorMode.Star;
          if (selectedPanel == this.cpnSystem && this.vwSystem.SelectedHabitatType != HabitatType.Undefined)
            return EditorMode.System;
        }
        return EditorMode.Undefined;
      }
    }

    public ExtendedPanel SelectedPanel
    {
      get
      {
        if (this.cpnSystem.State == ExtendedPanelState.Expanded)
          return this.cpnSystem;
        if (this.cpnStar.State == ExtendedPanelState.Expanded)
          return this.cpnStar;
        if (this.cpnGasCloud.State == ExtendedPanelState.Expanded)
          return this.cpnGasCloud;
        if (this.cpnPlanet.State == ExtendedPanelState.Expanded)
          return this.cpnPlanet;
        if (this.cpnMoon.State == ExtendedPanelState.Expanded)
          return this.cpnMoon;
        if (this.cpnAsteroid.State == ExtendedPanelState.Expanded)
          return this.cpnAsteroid;
        if (this.cpnBuiltObject.State == ExtendedPanelState.Expanded)
          return this.cpnBuiltObject;
        if (this.cpnColony.State == ExtendedPanelState.Expanded)
          return this.cpnColony;
        if (this.cpnAliens.State == ExtendedPanelState.Expanded)
          return this.cpnAliens;
        if (this.cpnCreature.State == ExtendedPanelState.Expanded)
          return this.cpnCreature;
        if (this.cpnPirates.State == ExtendedPanelState.Expanded)
          return this.cpnPirates;
        if (this.cpnRuins.State == ExtendedPanelState.Expanded)
          return this.cpnRuins;
        if (this.cpnEmpireExploration.State == ExtendedPanelState.Expanded)
          return this.cpnEmpireExploration;
        if (this.cpnClearItems.State == ExtendedPanelState.Expanded)
          return this.cpnClearItems;
        return this.cpnCharacters.State == ExtendedPanelState.Expanded ? this.cpnCharacters : (ExtendedPanel) null;
      }
    }

    private void cmbPlaceShipEmpire_SelectedIndexChanged(object sender, EventArgs e)
    {
      Empire selectedEmpire = this.cmbPlaceShipEmpire.SelectedEmpire;
      if (selectedEmpire != null)
        this.cmbPlaceShipDesign.BindData(selectedEmpire.Designs, this._BuiltObjectImages, this._IndependentEmpire);
      else
        this.cmbPlaceShipDesign.BindData((DesignList) null, this._BuiltObjectImages, this._IndependentEmpire);
    }

    private void cmbCharacterEmpire_SelectedIndexChanged(object sender, EventArgs e)
    {
      Empire selectedEmpire = this.cmbCharacterEmpire.SelectedEmpire;
      if (selectedEmpire != null)
      {
        if (selectedEmpire.DominantRace != null && selectedEmpire.DominantRace.AvailableCharacters != null)
          this.cmbCharacters.BindData(selectedEmpire, selectedEmpire.DominantRace.AvailableCharacters, this._CharacterImageCache, this._Galaxy, true, false);
        else
          this.cmbCharacters.BindData(selectedEmpire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
      }
      else
        this.cmbPlaceShipDesign.BindData((DesignList) null, this._BuiltObjectImages, this._IndependentEmpire);
    }

    protected override void OnMouseWheel(MouseEventArgs e) => base.OnMouseWheel(e);
  }
}
