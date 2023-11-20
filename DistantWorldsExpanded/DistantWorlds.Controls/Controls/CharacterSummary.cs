// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterSummary
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterSummary : GradientPanel
  {
    private IContainer components;
    private TextBox txtName;
    private BuiltObjectDropDown cmbBuiltObjects;
    private FleetDropDown cmbFleets;
    private FleetHabitatDropDown cmbFleetsHabitats;
    private HabitatDropDown cmbHabitats;
    private GlassButton btnTransfer;
    public CharacterSkillsTraitsProgress pnlCharacterSkillsTraits;
    public Panel pnlCharacterSkillsTraitsContainer;
    private FleetBuiltObjectHabitatDropdown cmbFleetBuiltObjectHabitats;
    private GlassButton btnChangeImage;
    private GlassButton btnEditSkillsTraits;
    private EmpireDropDown cmbEmpire;
    private CharacterDropDown cmbRole;
    private GlassButton btnChangeRole;
    private GlassButton btnChangeEmpire;
    private Galaxy _Galaxy;
    private Character _Character;
    private CharacterImageCache _CharacterImageCache;
    private Bitmap[] _LandscapeImages;
    private Bitmap _FrameImage;
    private Bitmap _SpaceImage;
    private Font _BoldFont;
    private Font _LargeFont;
    private Font _TitleFont;
    private Font _HugeFont;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private volatile bool _Initializing;
    public bool EditingCharacter;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.txtName = new TextBox();
      this.cmbBuiltObjects = new BuiltObjectDropDown();
      this.cmbFleets = new FleetDropDown();
      this.cmbFleetsHabitats = new FleetHabitatDropDown();
      this.cmbHabitats = new HabitatDropDown();
      this.btnTransfer = new GlassButton();
      this.pnlCharacterSkillsTraits = new CharacterSkillsTraitsProgress();
      this.pnlCharacterSkillsTraitsContainer = new Panel();
      this.cmbFleetBuiltObjectHabitats = new FleetBuiltObjectHabitatDropdown();
      this.btnChangeImage = new GlassButton();
      this.btnEditSkillsTraits = new GlassButton();
      this.cmbEmpire = new EmpireDropDown();
      this.cmbRole = new CharacterDropDown();
      this.btnChangeRole = new GlassButton();
      this.btnChangeEmpire = new GlassButton();
      this.SuspendLayout();
      this.txtName.Location = new Point(21, 52);
      this.txtName.Name = "txtName";
      this.txtName.Size = new Size(100, 20);
      this.txtName.TabIndex = 0;
      this.txtName.Enter += new EventHandler(this.txtName_Enter);
      this.txtName.KeyDown += new KeyEventHandler(this.txtName_KeyDown);
      this.txtName.Leave += new EventHandler(this.txtName_Leave);
      this.cmbBuiltObjects.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbBuiltObjects.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbBuiltObjects.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbBuiltObjects.FlatStyle = FlatStyle.Popup;
      this.cmbBuiltObjects.Font = new Font("Verdana", 8.25f);
      this.cmbBuiltObjects.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbBuiltObjects.FormattingEnabled = true;
      this.cmbBuiltObjects.Location = new Point(0, 0);
      this.cmbBuiltObjects.Name = "cmbBuiltObjects";
      this.cmbBuiltObjects.Size = new Size(121, 21);
      this.cmbBuiltObjects.TabIndex = 0;
      this.cmbFleets.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbFleets.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbFleets.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbFleets.FlatStyle = FlatStyle.Popup;
      this.cmbFleets.Font = new Font("Verdana", 8.25f);
      this.cmbFleets.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbFleets.FormattingEnabled = true;
      this.cmbFleets.Location = new Point(0, 0);
      this.cmbFleets.Name = "cmbFleets";
      this.cmbFleets.Size = new Size(121, 21);
      this.cmbFleets.TabIndex = 0;
      this.cmbFleetsHabitats.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbFleetsHabitats.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbFleetsHabitats.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbFleetsHabitats.FlatStyle = FlatStyle.Popup;
      this.cmbFleetsHabitats.Font = new Font("Verdana", 8.25f);
      this.cmbFleetsHabitats.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbFleetsHabitats.FormattingEnabled = true;
      this.cmbFleetsHabitats.Location = new Point(0, 0);
      this.cmbFleetsHabitats.Name = "cmbFleetsHabitats";
      this.cmbFleetsHabitats.Size = new Size(121, 21);
      this.cmbFleetsHabitats.TabIndex = 0;
      this.cmbHabitats.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbHabitats.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbHabitats.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbHabitats.FlatStyle = FlatStyle.Popup;
      this.cmbHabitats.Font = new Font("Verdana", 8.25f);
      this.cmbHabitats.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbHabitats.FormattingEnabled = true;
      this.cmbHabitats.Location = new Point(0, 0);
      this.cmbHabitats.Name = "cmbHabitats";
      this.cmbHabitats.Size = new Size(121, 21);
      this.cmbHabitats.TabIndex = 0;
      this.btnTransfer.BackColor = Color.FromArgb(0, 0, 0);
      this.btnTransfer.ClipBackground = false;
      this.btnTransfer.DelayFrameRefresh = false;
      this.btnTransfer.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnTransfer.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnTransfer.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnTransfer.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnTransfer.IntensifyColors = false;
      this.btnTransfer.Location = new Point(0, 0);
      this.btnTransfer.Name = "btnTransfer";
      this.btnTransfer.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnTransfer.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnTransfer.Size = new Size(75, 23);
      this.btnTransfer.TabIndex = 0;
      this.btnTransfer.Text = "Transfer";
      this.btnTransfer.TextColor = Color.FromArgb(120, 120, 120);
      this.btnTransfer.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnTransfer.ToggledOn = false;
      this.btnTransfer.Click += new EventHandler(this.btnTransfer_Click);
      this.pnlCharacterSkillsTraits.Font = new Font("Verdana", 8f);
      this.pnlCharacterSkillsTraits.Location = new Point(0, 0);
      this.pnlCharacterSkillsTraits.Name = "pnlCharacterSkillsTraits";
      this.pnlCharacterSkillsTraits.Size = new Size(200, 100);
      this.pnlCharacterSkillsTraits.TabIndex = 0;
      this.pnlCharacterSkillsTraits.Enter += new EventHandler(this.pnlCharacterSkillsTraits_Enter);
      this.pnlCharacterSkillsTraits.MouseEnter += new EventHandler(this.pnlCharacterSkillsTraits_MouseEnter);
      this.pnlCharacterSkillsTraitsContainer.Location = new Point(0, 0);
      this.pnlCharacterSkillsTraitsContainer.Name = "pnlCharacterSkillsTraitsContainer";
      this.pnlCharacterSkillsTraitsContainer.Size = new Size(200, 100);
      this.pnlCharacterSkillsTraitsContainer.TabIndex = 0;
      this.cmbFleetBuiltObjectHabitats.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbFleetBuiltObjectHabitats.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbFleetBuiltObjectHabitats.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbFleetBuiltObjectHabitats.FlatStyle = FlatStyle.Popup;
      this.cmbFleetBuiltObjectHabitats.Font = new Font("Verdana", 8.25f);
      this.cmbFleetBuiltObjectHabitats.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbFleetBuiltObjectHabitats.FormattingEnabled = true;
      this.cmbFleetBuiltObjectHabitats.Location = new Point(0, 0);
      this.cmbFleetBuiltObjectHabitats.Name = "cmbFleetBuiltObjectHabitats";
      this.cmbFleetBuiltObjectHabitats.Size = new Size(121, 21);
      this.cmbFleetBuiltObjectHabitats.TabIndex = 0;
      this.btnChangeImage.BackColor = Color.FromArgb(0, 0, 0);
      this.btnChangeImage.ClipBackground = false;
      this.btnChangeImage.DelayFrameRefresh = false;
      this.btnChangeImage.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnChangeImage.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnChangeImage.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnChangeImage.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnChangeImage.IntensifyColors = false;
      this.btnChangeImage.Location = new Point(0, 0);
      this.btnChangeImage.Name = "btnChangeImage";
      this.btnChangeImage.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnChangeImage.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnChangeImage.Size = new Size(75, 23);
      this.btnChangeImage.TabIndex = 0;
      this.btnChangeImage.Text = "Change Image";
      this.btnChangeImage.TextColor = Color.FromArgb(120, 120, 120);
      this.btnChangeImage.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnChangeImage.ToggledOn = false;
      this.btnChangeImage.Click += new EventHandler(this.btnChangeImage_Click);
      this.btnEditSkillsTraits.BackColor = Color.FromArgb(0, 0, 0);
      this.btnEditSkillsTraits.ClipBackground = false;
      this.btnEditSkillsTraits.DelayFrameRefresh = false;
      this.btnEditSkillsTraits.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnEditSkillsTraits.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnEditSkillsTraits.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnEditSkillsTraits.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnEditSkillsTraits.IntensifyColors = false;
      this.btnEditSkillsTraits.Location = new Point(0, 0);
      this.btnEditSkillsTraits.Name = "btnEditSkillsTraits";
      this.btnEditSkillsTraits.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnEditSkillsTraits.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnEditSkillsTraits.Size = new Size(75, 23);
      this.btnEditSkillsTraits.TabIndex = 0;
      this.btnEditSkillsTraits.Text = "Edit Skills and Traits";
      this.btnEditSkillsTraits.TextColor = Color.FromArgb(120, 120, 120);
      this.btnEditSkillsTraits.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnEditSkillsTraits.ToggledOn = false;
      this.btnEditSkillsTraits.Click += new EventHandler(this.btnEditSkillsTraits_Click);
      this.cmbEmpire.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbEmpire.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbEmpire.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbEmpire.FlatStyle = FlatStyle.Popup;
      this.cmbEmpire.Font = new Font("Verdana", 8.25f);
      this.cmbEmpire.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbEmpire.FormattingEnabled = true;
      this.cmbEmpire.Location = new Point(0, 0);
      this.cmbEmpire.Name = "cmbEmpire";
      this.cmbEmpire.Size = new Size(121, 21);
      this.cmbEmpire.TabIndex = 0;
      this.cmbRole.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbRole.FlatStyle = FlatStyle.Popup;
      this.cmbRole.Font = new Font("Verdana", 8.25f);
      this.cmbRole.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbRole.FormattingEnabled = true;
      this.cmbRole.Location = new Point(0, 0);
      this.cmbRole.Name = "cmbRole";
      this.cmbRole.Size = new Size(121, 21);
      this.cmbRole.TabIndex = 0;
      this.btnChangeRole.BackColor = Color.FromArgb(0, 0, 0);
      this.btnChangeRole.ClipBackground = false;
      this.btnChangeRole.DelayFrameRefresh = false;
      this.btnChangeRole.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnChangeRole.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnChangeRole.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnChangeRole.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnChangeRole.IntensifyColors = false;
      this.btnChangeRole.Location = new Point(0, 0);
      this.btnChangeRole.Name = "btnChangeRole";
      this.btnChangeRole.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnChangeRole.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnChangeRole.Size = new Size(75, 23);
      this.btnChangeRole.TabIndex = 0;
      this.btnChangeRole.Text = "Change Role";
      this.btnChangeRole.TextColor = Color.FromArgb(120, 120, 120);
      this.btnChangeRole.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnChangeRole.ToggledOn = false;
      this.btnChangeRole.Click += new EventHandler(this.btnChangeRole_Click);
      this.btnChangeEmpire.BackColor = Color.FromArgb(0, 0, 0);
      this.btnChangeEmpire.ClipBackground = false;
      this.btnChangeEmpire.DelayFrameRefresh = false;
      this.btnChangeEmpire.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Pixel);
      this.btnChangeEmpire.ForeColor = Color.FromArgb(150, 150, 150);
      this.btnChangeEmpire.GlowColor = Color.FromArgb(48, 48, 128);
      this.btnChangeEmpire.InnerBorderColor = Color.FromArgb(67, 67, 77);
      this.btnChangeEmpire.IntensifyColors = false;
      this.btnChangeEmpire.Location = new Point(0, 0);
      this.btnChangeEmpire.Name = "btnChangeEmpire";
      this.btnChangeEmpire.OuterBorderColor = Color.FromArgb(0, 0, 16);
      this.btnChangeEmpire.ShineColor = Color.FromArgb(112, 112, 128);
      this.btnChangeEmpire.Size = new Size(75, 23);
      this.btnChangeEmpire.TabIndex = 0;
      this.btnChangeEmpire.Text = "Change Empire";
      this.btnChangeEmpire.TextColor = Color.FromArgb(120, 120, 120);
      this.btnChangeEmpire.TextColor2 = Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue);
      this.btnChangeEmpire.ToggledOn = false;
      this.btnChangeEmpire.Click += new EventHandler(this.btnChangeEmpire_Click);
      this.Controls.Add((Control) this.txtName);
      this.Click += new EventHandler(this.CharacterSummary_Click);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public event EventHandler CharacterNameChanged;

    public event EventHandler CharacterTransferInitiated;

    public event EventHandler CharacterChangeImage;

    public event EventHandler CharacterEditSkillsTraits;

    public event EventHandler CharacterChangeEmpire;

    public event EventHandler CharacterChangeRole;

    public CharacterSummary()
    {
      this.InitializeComponent();
      this.Font = new Font("Verdana", 8f);
      this.SetFont(FontSize.Normal);
      this.InitializeFonts();
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Character = (Character) null;
      this._CharacterImageCache = (CharacterImageCache) null;
    }

    public Character Character => this._Character;

    public void InitializeImages(Bitmap[] builtObjectImages, Bitmap[] habitatImages)
    {
      this.cmbHabitats.InitializeImages(habitatImages);
      this.cmbFleets.InitializeImages(builtObjectImages);
      this.cmbFleetsHabitats.InitializeImages(builtObjectImages, habitatImages);
      this.cmbBuiltObjects.InitializeImages(builtObjectImages);
      this.cmbFleetBuiltObjectHabitats.InitializeImages(builtObjectImages, habitatImages);
    }

    private void InitializeFonts()
    {
      this._LargeFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Regular, GraphicsUnit.Pixel);
      this._BoldFont = new Font(this.Font.FontFamily, FontSize.Large, FontStyle.Bold, GraphicsUnit.Pixel);
      this._TitleFont = new Font(this.Font.FontFamily, FontSize.Title, FontStyle.Bold, GraphicsUnit.Pixel);
      this._HugeFont = new Font(this.Font.FontFamily, FontSize.Huge, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void BindData(
      Character character,
      Galaxy galaxy,
      CharacterImageCache characterImageCache,
      Bitmap[] landscapeImages,
      Bitmap frameImage,
      Bitmap spaceImage,
      bool editing)
    {
      this._Initializing = true;
      this.EditingCharacter = editing;
      this._Galaxy = galaxy;
      this._Character = character;
      this._CharacterImageCache = characterImageCache;
      this._LandscapeImages = landscapeImages;
      this._FrameImage = frameImage;
      this._SpaceImage = spaceImage;
      this.InitializeFonts();
      this.txtName.Size = new Size(120, 62);
      this.txtName.Location = new Point(270, 38);
      this.txtName.Multiline = true;
      this.txtName.BackColor = this.BackColor;
      this.txtName.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtName.BorderStyle = BorderStyle.None;
      this.txtName.Font = this._HugeFont;
      this.txtName.TextAlign = HorizontalAlignment.Center;
      if (this._Character != null)
        this.txtName.Text = this._Character.Name;
      else
        this.txtName.Text = string.Empty;
      Point point1 = new Point(13, 272);
      Size size1 = new Size(377, 270);
      Size size2 = new Size(357, 270);
      if (character != null && character.Role == CharacterRole.IntelligenceAgent && !editing)
      {
        size1 = new Size(377, 188);
        size2 = new Size(357, 188);
      }
      this.pnlCharacterSkillsTraits.Visible = true;
      if (!this.Controls.Contains((Control) this.pnlCharacterSkillsTraitsContainer))
        this.Controls.Add((Control) this.pnlCharacterSkillsTraitsContainer);
      if (!this.pnlCharacterSkillsTraitsContainer.Controls.Contains((Control) this.pnlCharacterSkillsTraits))
        this.pnlCharacterSkillsTraitsContainer.Controls.Add((Control) this.pnlCharacterSkillsTraits);
      this.pnlCharacterSkillsTraitsContainer.Location = point1;
      this.pnlCharacterSkillsTraitsContainer.Size = size1;
      this.pnlCharacterSkillsTraitsContainer.AutoScroll = true;
      this.pnlCharacterSkillsTraitsContainer.SetAutoScrollMargin(0, 0);
      this.pnlCharacterSkillsTraitsContainer.AutoScrollPosition = new Point(0, 0);
      this.pnlCharacterSkillsTraits.BringToFront();
      this.pnlCharacterSkillsTraits.Font = this.Font;
      this.pnlCharacterSkillsTraits.Location = new Point(0, 0);
      this.pnlCharacterSkillsTraits.Size = size2;
      this.pnlCharacterSkillsTraits.MaximumSize = new Size(360, 1000);
      this.pnlCharacterSkillsTraits.MinimumSize = size2;
      this.pnlCharacterSkillsTraits.AutoSize = true;
      this.pnlCharacterSkillsTraits.BindData(character);
      this.btnChangeImage.Size = new Size(125, 25);
      this.btnChangeImage.Location = new Point(268, 235);
      this.btnChangeImage.Text = TextResolver.GetText("Change Image") + "...";
      this.btnChangeImage.Font = this._BoldFont;
      this.btnChangeImage.Parent = (Control) this;
      this.btnChangeImage.BringToFront();
      Point point2 = new Point(85, 565);
      Size size3 = new Size(230, 22);
      this.btnTransfer.Text = TextResolver.GetText("Transfer to new location");
      this.btnTransfer.Size = new Size(230, 25);
      this.btnTransfer.Location = new Point(85, 595);
      this.btnTransfer.Font = this._BoldFont;
      this.btnTransfer.Parent = (Control) this;
      if (editing)
      {
        this.btnEditSkillsTraits.Visible = true;
        this.cmbRole.Visible = true;
        this.btnChangeRole.Visible = true;
        this.cmbEmpire.Visible = true;
        this.btnChangeEmpire.Visible = true;
        this.btnChangeImage.Visible = true;
        this.btnEditSkillsTraits.Parent = (Control) this;
        this.cmbRole.Parent = (Control) this;
        this.btnChangeRole.Parent = (Control) this;
        this.cmbEmpire.Parent = (Control) this;
        this.btnChangeEmpire.Parent = (Control) this;
        this.btnEditSkillsTraits.BringToFront();
        this.cmbRole.BringToFront();
        this.btnChangeRole.BringToFront();
        this.cmbEmpire.BringToFront();
        this.btnChangeEmpire.BringToFront();
        point2 = new Point(85, 598);
        size3 = new Size(230, 22);
        this.btnTransfer.Text = TextResolver.GetText("Change location immediately");
        this.btnTransfer.Location = new Point(85, 626);
        this.btnChangeImage.Size = new Size(125, 25);
        this.btnChangeImage.Location = new Point(268, 205);
        this.btnEditSkillsTraits.Size = new Size(125, 25);
        this.btnEditSkillsTraits.Location = new Point(268, 235);
        this.btnEditSkillsTraits.Text = TextResolver.GetText("Edit Skills and Traits");
        this.btnEditSkillsTraits.Font = this._BoldFont;
        this.cmbRole.Size = new Size(200, 21);
        this.cmbRole.Location = new Point(22, 544);
        Empire empire = (Empire) null;
        if (this._Character != null)
          empire = this._Character.Empire;
        this.cmbRole.BindData(empire, (CharacterList) null, this._CharacterImageCache, this._Galaxy, true, false);
        if (this._Character != null)
          this.cmbRole.SetSelectedCharacter(this._Character.Role);
        this.cmbRole.Font = this.Font;
        this.btnChangeRole.Size = new Size(150, 25);
        this.btnChangeRole.Location = new Point(227, 544);
        this.btnChangeRole.Text = TextResolver.GetText("Change Role");
        this.btnChangeRole.Font = this._BoldFont;
        this.cmbEmpire.Size = new Size(200, 21);
        this.cmbEmpire.Location = new Point(22, 571);
        this.cmbEmpire.BindData(galaxy.PlayerEmpire, galaxy.Empires, galaxy.PirateEmpires, (Empire) null, false);
        if (this._Character != null)
          this.cmbEmpire.SetSelectedEmpire(this._Character.Empire);
        this.cmbEmpire.Font = this.Font;
        this.btnChangeEmpire.Size = new Size(150, 25);
        this.btnChangeEmpire.Location = new Point(227, 571);
        this.btnChangeEmpire.Text = TextResolver.GetText("Change Empire");
        this.btnChangeEmpire.Font = this._BoldFont;
      }
      else
      {
        this.btnEditSkillsTraits.Visible = false;
        this.cmbRole.Visible = false;
        this.btnChangeRole.Visible = false;
        this.cmbEmpire.Visible = false;
        this.btnChangeEmpire.Visible = false;
        this.btnChangeImage.Visible = false;
      }
      this.cmbBuiltObjects.Size = size3;
      this.cmbBuiltObjects.Location = point2;
      this.cmbHabitats.Size = size3;
      this.cmbHabitats.Location = point2;
      this.cmbFleets.Size = size3;
      this.cmbFleets.Location = point2;
      this.cmbFleetsHabitats.Size = size3;
      this.cmbFleetsHabitats.Location = point2;
      this.cmbFleetBuiltObjectHabitats.Size = size3;
      this.cmbFleetBuiltObjectHabitats.Location = point2;
      this.cmbBuiltObjects.BringToFront();
      this.cmbHabitats.BringToFront();
      this.cmbFleets.BringToFront();
      this.cmbFleetsHabitats.BringToFront();
      this.cmbFleetBuiltObjectHabitats.BringToFront();
      this.cmbBuiltObjects.Parent = (Control) this;
      this.cmbHabitats.Parent = (Control) this;
      this.cmbFleets.Parent = (Control) this;
      this.cmbFleetsHabitats.Parent = (Control) this;
      this.cmbFleetBuiltObjectHabitats.Parent = (Control) this;
      this.cmbBuiltObjects.Font = this._LargeFont;
      this.cmbHabitats.Font = this._LargeFont;
      this.cmbFleets.Font = this._LargeFont;
      this.cmbFleetsHabitats.Font = this._LargeFont;
      this.cmbFleetBuiltObjectHabitats.Font = this._LargeFont;
      this.SetupTransferControls(character, galaxy);
      this._Initializing = false;
      this.Invalidate();
    }

    private void SetupTransferControls(Character character, Galaxy galaxy)
    {
      if (character != null)
      {
        this.cmbBuiltObjects.Visible = false;
        this.cmbHabitats.Visible = false;
        this.cmbFleets.Visible = false;
        this.cmbFleetsHabitats.Visible = false;
        this.cmbFleetBuiltObjectHabitats.Visible = false;
        switch (character.Role)
        {
          case CharacterRole.Leader:
            if (character.Empire != null)
              this.cmbHabitats.BindData(character.Empire.Capitals, galaxy);
            this.cmbHabitats.Visible = true;
            break;
          case CharacterRole.Ambassador:
            HabitatList habitats = new HabitatList();
            if (character.Empire != null && character.Empire.DiplomaticRelations != null)
            {
              for (int index = 0; index < character.Empire.DiplomaticRelations.Count; ++index)
              {
                DiplomaticRelation diplomaticRelation = character.Empire.DiplomaticRelations[index];
                if (diplomaticRelation != null && diplomaticRelation.Type != DiplomaticRelationType.NotMet && diplomaticRelation.Type != DiplomaticRelationType.War && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire != character.Empire && diplomaticRelation.OtherEmpire.Capital != null)
                {
                  Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(diplomaticRelation.OtherEmpire.Capital);
                  if (habitatSystemStar != null && character.Empire.CheckSystemExplored(habitatSystemStar))
                    habitats.Add(diplomaticRelation.OtherEmpire.Capital);
                }
              }
            }
            this.cmbHabitats.BindData(habitats, galaxy);
            this.cmbHabitats.Visible = true;
            break;
          case CharacterRole.ColonyGovernor:
            if (character.Empire != null)
              this.cmbHabitats.BindData(character.Empire.Colonies, galaxy);
            this.cmbHabitats.Visible = true;
            break;
          case CharacterRole.FleetAdmiral:
            if (character.Empire != null)
              this.cmbFleets.BindData(character.Empire.ShipGroups, false, galaxy);
            this.cmbFleets.Visible = true;
            break;
          case CharacterRole.TroopGeneral:
            if (character.Empire != null)
              this.cmbFleetsHabitats.BindData(character.Empire.ShipGroups, character.Empire.Colonies, false, galaxy);
            this.cmbFleetsHabitats.Visible = true;
            break;
          case CharacterRole.Scientist:
            if (character.Empire != null)
              this.cmbBuiltObjects.BindData(character.Empire.ResearchFacilities, galaxy, true);
            this.cmbBuiltObjects.Visible = true;
            break;
          case CharacterRole.PirateLeader:
            if (character.Empire != null && character.Empire.Colonies != null && character.Empire.BuiltObjects != null)
              this.cmbFleetBuiltObjectHabitats.BindData(character.Empire.ShipGroups, character.Empire.BuiltObjects.GetBuiltObjectsByRole(new List<BuiltObjectRole>()
              {
                BuiltObjectRole.Base
              }), character.Empire.Colonies.GetOwnedColonies(character.Empire), false, galaxy, Color.FromArgb(128, 0, 64));
            this.cmbFleetBuiltObjectHabitats.Visible = true;
            break;
          case CharacterRole.ShipCaptain:
            if (character.Empire != null)
            {
              List<BuiltObjectRole> roles = new List<BuiltObjectRole>()
              {
                BuiltObjectRole.Build,
                BuiltObjectRole.Exploration,
                BuiltObjectRole.Freight,
                BuiltObjectRole.Military,
                BuiltObjectRole.Passenger,
                BuiltObjectRole.Resource
              };
              this.cmbBuiltObjects.BindData(character.Empire.BuiltObjects.GetBuiltObjectsByRole(roles), galaxy, true, Color.FromArgb(128, 0, 64));
            }
            this.cmbBuiltObjects.Visible = true;
            break;
          default:
            this.cmbBuiltObjects.Visible = false;
            this.cmbHabitats.Visible = false;
            this.cmbFleets.Visible = false;
            this.cmbFleetsHabitats.Visible = false;
            if (!this.EditingCharacter)
              break;
            if (character.Empire != null && character.Empire.Colonies != null && character.Empire.BuiltObjects != null)
              this.cmbFleetBuiltObjectHabitats.BindData(new ShipGroupList(), character.Empire.BuiltObjects.GetBuiltObjectsByRole(new List<BuiltObjectRole>()
              {
                BuiltObjectRole.Base
              }), character.Empire.Colonies.GetOwnedColonies(character.Empire), false, galaxy);
            this.cmbFleetBuiltObjectHabitats.Visible = true;
            break;
        }
      }
      else
      {
        this.cmbBuiltObjects.Visible = false;
        this.cmbHabitats.Visible = false;
        this.cmbFleets.Visible = false;
        this.cmbFleetsHabitats.Visible = false;
        this.cmbFleetBuiltObjectHabitats.Visible = false;
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      this.DrawCharacter(e.Graphics, this._Character);
    }

    private void DrawCharacter(Graphics graphics, Character character)
    {
      if (character == null || this._Initializing)
        return;
      if (this._CharacterImageCache != null)
      {
        this._CharacterImageCache.ObtainCharacterImage(character);
        bool inTransitOrUnknown = false;
        if (character.Location == null || character.TransferDestination != null && (double) character.TransferTimeRemaining > 0.0 || character.Mission != null && character.Mission.Type != IntelligenceMissionType.CounterIntelligence && character.Mission.Type != IntelligenceMissionType.Undefined)
          inTransitOrUnknown = true;
        int width = 250;
        int height = 250;
        Bitmap planetCompositeImage = this.GenerateCharacterPlanetCompositeImage(character, width, height, this._FrameImage, 6, inTransitOrUnknown);
        Rectangle srcRect = new Rectangle(0, 0, planetCompositeImage.Width, planetCompositeImage.Height);
        Rectangle destRect = new Rectangle(10, 10, width, height);
        graphics.DrawImage((Image) planetCompositeImage, destRect, srcRect, GraphicsUnit.Pixel);
        planetCompositeImage.Dispose();
      }
      int y1 = 10;
      string text = Galaxy.ResolveDescription(character.Role);
      Point location = new Point(270 + (120 - (int) graphics.MeasureString(text, this._BoldFont, 120, StringFormat.GenericDefault).Width) / 2, y1);
      this.DrawStringWithDropShadow(graphics, text, this._BoldFont, location);
      int y2 = 108;
      string s = Galaxy.ResolveDescriptionCharacterTask(character, this._Galaxy);
      if (string.IsNullOrEmpty(s))
        s = Galaxy.ResolveCharacterLocationDescription(character);
      RectangleF layoutRectangle = new RectangleF(270f, (float) y2, 120f, 152f);
      graphics.DrawString(s, this._LargeFont, (Brush) this._WhiteBrush, layoutRectangle, StringFormat.GenericTypographic);
    }

    private Bitmap GenerateCharacterPlanetCompositeImage(
      Character character,
      int width,
      int height,
      Bitmap frameImage,
      int frameBorderWidth,
      bool inTransitOrUnknown)
    {
      Bitmap planetCompositeImage = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
      using (Graphics graphics = Graphics.FromImage((Image) planetCompositeImage))
      {
        if (character != null)
        {
          GraphicsHelper.SetGraphicsQualityToHigh(graphics);
          int targetWidth = width - frameBorderWidth * 2;
          int targetHeight = height - frameBorderWidth * 2;
          Bitmap bitmap = (Bitmap) null;
          int index = Galaxy.SelectCharacterLandscapeImageIndex(character);
          if (index >= 0 && this._LandscapeImages.Length > index)
            bitmap = this._LandscapeImages[index];
          if (bitmap == null || inTransitOrUnknown)
            bitmap = this._SpaceImage;
          Rectangle destRect = this.ResolveDrawFillRectangle(bitmap.Width, bitmap.Height, targetWidth, targetHeight);
          Rectangle srcRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
          destRect.Offset(frameBorderWidth, frameBorderWidth);
          graphics.DrawImage((Image) bitmap, destRect, srcRect, GraphicsUnit.Pixel);
          Bitmap characterImage = this._CharacterImageCache.ObtainCharacterImage(character);
          if (characterImage != null)
          {
            destRect = this.ResolveDrawFillRectangle(characterImage.Width, characterImage.Height, targetWidth, targetHeight);
            srcRect = new Rectangle(0, 0, characterImage.Width, characterImage.Height);
            destRect.Offset(frameBorderWidth, frameBorderWidth);
            graphics.DrawImage((Image) characterImage, destRect, srcRect, GraphicsUnit.Pixel);
          }
          if (frameImage != null)
          {
            destRect = new Rectangle(0, 0, width, height);
            srcRect = new Rectangle(0, 0, frameImage.Width, frameImage.Height);
            graphics.DrawImage((Image) frameImage, destRect, srcRect, GraphicsUnit.Pixel);
          }
        }
      }
      return planetCompositeImage;
    }

    private Rectangle ResolveDrawFillRectangle(
      int imageWidth,
      int imageHeight,
      int targetWidth,
      int targetHeight)
    {
      double num = Math.Max((double) targetWidth / (double) imageWidth, (double) targetHeight / (double) imageHeight);
      int width = (int) ((double) imageWidth * num);
      int height = (int) ((double) imageHeight * num);
      return new Rectangle((targetWidth - width) / 2, (targetHeight - height) / 2, width, height);
    }

    private void DrawStringWithDropShadow(
      Graphics graphics,
      string text,
      Font font,
      Point location)
    {
      location = new Point(location.X + 1, location.Y + 1);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, (PointF) location);
      location = new Point(location.X - 1, location.Y - 1);
      graphics.DrawString(text, font, (Brush) this._WhiteBrush, (PointF) location);
    }

    private void DrawStringWithDropShadowBounded(
      Graphics graphics,
      string text,
      Font font,
      Point location,
      SizeF size)
    {
      location = new Point(location.X + 1, location.Y + 1);
      PointF pointF = new PointF((float) location.X, (float) location.Y);
      RectangleF layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      graphics.DrawString(text, font, (Brush) this._BlackBrush, layoutRectangle, StringFormat.GenericTypographic);
      location = new Point(location.X - 1, location.Y - 1);
      pointF = new PointF((float) location.X, (float) location.Y);
      layoutRectangle = new RectangleF(pointF.X, pointF.Y, size.Width + 2f, size.Height + 2f);
      graphics.DrawString(text, font, (Brush) this._WhiteBrush, layoutRectangle, StringFormat.GenericTypographic);
    }

    private void txtName_Enter(object sender, EventArgs e)
    {
      this.txtName.BorderStyle = BorderStyle.FixedSingle;
      this.txtName.SelectAll();
    }

    private void txtName_Leave(object sender, EventArgs e)
    {
      this.txtName.BorderStyle = BorderStyle.None;
      if (this._Character != null)
        this._Character.Name = this.txtName.Text;
      if (this.CharacterNameChanged == null)
        return;
      this.CharacterNameChanged((object) this, new EventArgs());
    }

    private void CharacterSummary_Click(object sender, EventArgs e) => this.Focus();

    private void txtName_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.Focus();
    }

    private MessageBoxEx GenerateAutomationMessageBox(string automationTask)
    {
      MessageBoxEx messageBox = MessageBoxExManager.GetMessageBox(automationTask);
      if (messageBox == null)
      {
        messageBox = MessageBoxExManager.CreateMessageBox(automationTask, this._LargeFont);
        messageBox.AddButton(TextResolver.GetText("Leave automation on"), "On");
        messageBox.AddButton(TextResolver.GetText("Turn off automation"), "Off");
        messageBox.Caption = string.Format(TextResolver.GetText("Turn Off TASKNAME Automation?"), (object) automationTask);
        messageBox.Icon = MessageBoxExIcon.Question;
        messageBox.AllowSaveResponse = true;
        messageBox.SaveResponseText = TextResolver.GetText("Don't ask me again");
        string str = string.Format(TextResolver.GetText("Would you like to turn off automation"), (object) automationTask).Replace("\n", Environment.NewLine);
        messageBox.Text = str;
        messageBox.UseSavedResponse = true;
      }
      return messageBox;
    }

    private void btnTransfer_Click(object sender, EventArgs e)
    {
      if (this._Character == null)
        return;
      StellarObject destination = (StellarObject) null;
      switch (this._Character.Role)
      {
        case CharacterRole.Leader:
          destination = (StellarObject) this.cmbHabitats.SelectedHabitat;
          break;
        case CharacterRole.Ambassador:
          destination = (StellarObject) this.cmbHabitats.SelectedHabitat;
          break;
        case CharacterRole.ColonyGovernor:
          destination = (StellarObject) this.cmbHabitats.SelectedHabitat;
          break;
        case CharacterRole.FleetAdmiral:
          ShipGroup selectedFleet1 = this.cmbFleets.SelectedFleet;
          if (selectedFleet1 != null)
          {
            destination = (StellarObject) selectedFleet1.LeadShip;
            break;
          }
          break;
        case CharacterRole.TroopGeneral:
          ShipGroup selectedFleet2 = this.cmbFleetsHabitats.SelectedFleet;
          Habitat selectedHabitat = this.cmbFleetsHabitats.SelectedHabitat;
          destination = selectedFleet2 == null ? (StellarObject) selectedHabitat : (StellarObject) selectedFleet2.DetermineStrongestTroopTransport() ?? (StellarObject) selectedFleet2.LeadShip;
          break;
        case CharacterRole.IntelligenceAgent:
          if (this.EditingCharacter)
          {
            ShipGroup selectedFleet3 = this.cmbFleetBuiltObjectHabitats.SelectedFleet;
            destination = selectedFleet3 != null ? (StellarObject) selectedFleet3.DetermineStrongestTroopTransport() ?? (StellarObject) selectedFleet3.LeadShip : (StellarObject) this.cmbFleetBuiltObjectHabitats.SelectedBuiltObject ?? (StellarObject) this.cmbFleetBuiltObjectHabitats.SelectedHabitat;
            break;
          }
          break;
        case CharacterRole.Scientist:
          destination = (StellarObject) this.cmbBuiltObjects.SelectedBuiltObject;
          break;
        case CharacterRole.PirateLeader:
          ShipGroup selectedFleet4 = this.cmbFleetBuiltObjectHabitats.SelectedFleet;
          destination = selectedFleet4 != null ? (StellarObject) selectedFleet4.DetermineStrongestTroopTransport() ?? (StellarObject) selectedFleet4.LeadShip : (StellarObject) this.cmbFleetBuiltObjectHabitats.SelectedBuiltObject ?? (StellarObject) this.cmbFleetBuiltObjectHabitats.SelectedHabitat;
          break;
        case CharacterRole.ShipCaptain:
          destination = (StellarObject) this.cmbBuiltObjects.SelectedBuiltObject;
          break;
      }
      if (this.EditingCharacter)
      {
        if (destination == null || destination == this._Character.Location)
          return;
        this._Character.CompleteLocationTransfer(destination, this._Galaxy);
        if (this.CharacterTransferInitiated == null)
          return;
        this.CharacterTransferInitiated((object) this, new EventArgs());
      }
      else
      {
        if (this._Galaxy.PlayerEmpire.ControlCharacterLocations && this.GenerateAutomationMessageBox(TextResolver.GetText("Character Locations")).Show((IWin32Window) this).ToLower(CultureInfo.InvariantCulture) == "off")
          this._Galaxy.PlayerEmpire.ControlCharacterLocations = false;
        if (destination == null || destination == this._Character.Location || this._Character.TransferDestination != null)
          return;
        this._Character.TransferToNewLocation(destination, this._Galaxy);
        if (this.CharacterTransferInitiated == null)
          return;
        this.CharacterTransferInitiated((object) this, new EventArgs());
      }
    }

    private void pnlCharacterSkillsTraits_MouseEnter(object sender, EventArgs e) => this.pnlCharacterSkillsTraitsContainer.Focus();

    private void pnlCharacterSkillsTraits_Enter(object sender, EventArgs e) => this.pnlCharacterSkillsTraitsContainer.Focus();

    private void btnChangeImage_Click(object sender, EventArgs e)
    {
      if (this.CharacterChangeImage == null)
        return;
      this.CharacterChangeImage((object) this, new EventArgs());
    }

    private void btnEditSkillsTraits_Click(object sender, EventArgs e)
    {
      if (this.CharacterEditSkillsTraits == null)
        return;
      this.CharacterEditSkillsTraits((object) this, new EventArgs());
    }

    private void btnChangeEmpire_Click(object sender, EventArgs e)
    {
      if (this._Character == null)
        return;
      Empire selectedEmpire = this.cmbEmpire.SelectedEmpire;
      if (selectedEmpire == null || selectedEmpire == this._Character.Empire)
        return;
      this._Character.CompleteEmpireChange(selectedEmpire);
      this.Invalidate();
      if (this.CharacterChangeEmpire == null)
        return;
      this.CharacterChangeEmpire((object) this, new EventArgs());
    }

    private void btnChangeRole_Click(object sender, EventArgs e)
    {
      if (this._Character == null)
        return;
      CharacterRole characterRole = CharacterRole.Undefined;
      this.cmbRole.GetSelectedCharacter(out characterRole);
      if (characterRole == CharacterRole.Undefined || characterRole == this._Character.Role)
        return;
      this._Character.RemoveAllSkillsAndTraits();
      this._Character.Role = characterRole;
      this._CharacterImageCache.ClearCharacterImages(this._Character);
      this._Character.Empire.ApplyRandomCharacterSkillsTraits(this._Character, false);
      this.Invalidate();
      if (this.CharacterChangeRole == null)
        return;
      this.CharacterChangeRole((object) this, new EventArgs());
    }
  }
}
