// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.CustomBomberForm
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
  public class CustomBomberForm : Form
  {
    private FighterSpecification currentDesign = new FighterSpecification();
    private List<Tuple<FighterSpecification, float, float, short>> allDesignTuples = new List<Tuple<FighterSpecification, float, float, short>>();
    private static Empire playerEmpire;
    private static short targetingBonus = 0;
    private static short countermeasuresBonus = 0;
    private static short sizeIncrease = 0;
    public static short weaponMountLevels = 0;
    private static short shieldIncreaseLevels = 0;
    private static short engineIncreaseLevels = 0;
    private static short shieldRegenLevels = 0;
    private static float ammoMultiplier = 1f;
    private static float costMultiplier = 1f;
    public static Main main;
    public static int DesignPictureFamilyIndex = 0;
    private IContainer components = (IContainer) null;
    private Label label_FighterName;
    private Label label_WeaponMounts;
    private Label label_Shield;
    private Label label_Engine;
    private Label label_Targeting;
    private Label label_Countermeasures;
    private Label label_Size;
    private TextBox size_textBox;
    private TextBox engineOutput_textBox;
    private TextBox shieldLayers_textBox;
    private TextBox weapon_textBox;
    private ComboBox targeting_comboBox;
    private ComboBox countermeasures_comboBox;
    private TextBox bomberName_textBox;
    private Label label_shieldRegen;
    private TextBox shieldRegen_textBox;
    private Button createBomber_button;
    private Button weaponMountPlusOne_button;
    private Button weaponMountMinusOne_button;
    private Button shieldLayersMinusOne_button;
    private Button shieldLayersPlusOne_button;
    private Button shieldRechargeMinusOne_button;
    private Button shieldRechargePlusOne__button;
    private Button engineMinusOne_button;
    private Button enginePlusOne__button;
    private CheckBox bomberType_checkBox;
    private Button extraAmmoMinusOne_button;
    private Button extraAmmoPlusOne_button;
    private TextBox extraAmmo_textBox;
    private Label label_ExtraAmmo;
    private PictureBox bomberPictureBox;
    private Button imageSelectBack_button;
    private Button imageSelectNext_button;
    private Label label_Cost;
    private TextBox cost_textBox;

    public CustomBomberForm()
    {
      if (BaconBuiltObject.myMain == null)
        return;
      CustomBomberForm.playerEmpire = BaconBuiltObject.myMain._Game.PlayerEmpire;
      CustomBomberForm.main = BaconBuiltObject.myMain;
      this.InitializeComponent();
      this.InitMore();
    }

    public CustomBomberForm(Main theMain)
    {
      if (theMain == null)
        return;
      CustomBomberForm.main = theMain;
      CustomBomberForm.playerEmpire = BaconBuiltObject.myMain._Game.PlayerEmpire;
      this.InitializeComponent();
      this.InitMore();
    }

    public FighterSpecification IdentifyLatestTorpedoBomberSpecification()
    {
      FighterSpecification fighterSpecification = (FighterSpecification) null;
      for (int index = 0; index < CustomBomberForm.playerEmpire.Research.ResearchedFighters.Count; ++index)
      {
        if (CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].Type == FighterType.Bomber && CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].WeaponType == ComponentType.WeaponTorpedo && (fighterSpecification == null || CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].TechLevel > fighterSpecification.TechLevel))
          fighterSpecification = CustomBomberForm.playerEmpire.Research.ResearchedFighters[index];
      }
      return fighterSpecification;
    }

    public FighterSpecification IdentifyLatestMissileBomberSpecification()
    {
      FighterSpecification fighterSpecification = (FighterSpecification) null;
      for (int index = 0; index < CustomBomberForm.playerEmpire.Research.ResearchedFighters.Count; ++index)
      {
        if (CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].Type == FighterType.Bomber && CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].WeaponType == ComponentType.WeaponMissile && (fighterSpecification == null || CustomBomberForm.playerEmpire.Research.ResearchedFighters[index].TechLevel > fighterSpecification.TechLevel))
          fighterSpecification = CustomBomberForm.playerEmpire.Research.ResearchedFighters[index];
      }
      return fighterSpecification;
    }

    private void InitMore()
    {
      CustomBomberForm.targetingBonus = (short) 0;
      CustomBomberForm.countermeasuresBonus = (short) 0;
      CustomBomberForm.sizeIncrease = (short) 0;
      CustomBomberForm.weaponMountLevels = (short) 0;
      CustomBomberForm.shieldIncreaseLevels = (short) 0;
      CustomBomberForm.engineIncreaseLevels = (short) 0;
      CustomBomberForm.shieldRegenLevels = (short) 0;
      CustomBomberForm.ammoMultiplier = 0.0f;
      CustomBomberForm.costMultiplier = 1f;
      CustomBomberForm.DesignPictureFamilyIndex = 0;
      this.targeting_comboBox.SelectedIndex = 0;
      this.countermeasures_comboBox.SelectedIndex = 0;
      this.bomberName_textBox.Text = "";
      this.cost_textBox.Text = (BaconFighter.fighterBuildCost * 10).ToString();
      this.allDesignTuples = BaconBuiltObject.GetCustomFighterDesigns(CustomBomberForm.playerEmpire);
      int pictureFamilyIndex = CustomBomberForm.playerEmpire.DesignPictureFamilyIndex;
      if (CustomBomberForm.playerEmpire.DominantRace != null)
      {
        CustomBomberForm.DesignPictureFamilyIndex = CustomBomberForm.playerEmpire.PirateEmpireBaseHabitat == null ? CustomBomberForm.playerEmpire.DominantRace.DesignPictureFamilyIndex : CustomBomberForm.playerEmpire.DominantRace.DesignPictureFamilyIndexPirates;
        this.bomberPictureBox.Image = (Image) new Bitmap((Image) CustomBomberForm.main.bitmap_6[CustomBomberForm.DesignPictureFamilyIndex * 2 + 1]);
        this.bomberName_textBox.Update();
      }
      this.Show();
    }

    private void createBomber_button_Click(object sender, EventArgs e)
    {
      FighterSpecification fighterSpecification = new FighterSpecification();
      FighterSpecification original = this.IdentifyLatestMissileBomberSpecification();
      if (original == null || this.bomberType_checkBox.Checked)
        original = this.IdentifyLatestTorpedoBomberSpecification();
      this.currentDesign = CustomBomberForm.CloneFighterSpecification(original);
      this.currentDesign.Name = this.bomberName_textBox.Text == "" ? "Custom" : this.bomberName_textBox.Text;
      this.currentDesign.TopSpeed += (short) ((int) CustomBomberForm.engineIncreaseLevels * (int) this.currentDesign.TopSpeed / 10);
      this.currentDesign.AccelerationRate += (float) ((double) CustomBomberForm.engineIncreaseLevels * (double) this.currentDesign.AccelerationRate / 10.0);
      this.currentDesign.ShieldsCapacity += (short) ((int) CustomBomberForm.shieldIncreaseLevels * (int) this.currentDesign.ShieldsCapacity);
      this.currentDesign.ShieldRechargeRate += (float) ((double) CustomBomberForm.shieldRegenLevels * (double) this.currentDesign.ShieldRechargeRate / 2.0);
      this.currentDesign.WeaponDamage += (short) ((int) CustomBomberForm.weaponMountLevels * (int) this.currentDesign.WeaponDamage);
      this.currentDesign.Size += CustomBomberForm.sizeIncrease;
      this.currentDesign.TargettingModifier += (short) ((int) CustomBomberForm.targetingBonus + (int) CustomBomberForm.sizeIncrease / 2);
      this.currentDesign.CountermeasureModifier += (short) ((int) CustomBomberForm.countermeasuresBonus - (int) CustomBomberForm.sizeIncrease / 2);
      if ((double) this.currentDesign.SortTag == 0.0)
        ;
      this.currentDesign.SortTag = (float) (CustomBomberForm.DesignPictureFamilyIndex * 2 + 1);
      BaconBuiltObject.AddCustomFighterDesign(Tuple.Create<FighterSpecification, float, float, short>(this.currentDesign, CustomBomberForm.costMultiplier, CustomBomberForm.ammoMultiplier + 1f, (short) (CustomBomberForm.DesignPictureFamilyIndex * 2 + 1)), CustomBomberForm.playerEmpire);
      int num = (int) MessageBox.Show(this.bomberName_textBox.Text + " created." + Environment.NewLine + "To use, select a carrier or fleet and use !bomber=[bomber name] to assign the new bomber.");
      BaconMain.customBomberFormOpen = false;
      this.Close();
    }

    private void UpdateSize(short change)
    {
      CustomBomberForm.sizeIncrease += change;
      this.size_textBox.Text = CustomBomberForm.sizeIncrease.ToString();
    }

    private void UpdateCost()
    {
      float num1 = (float) this.countermeasures_comboBox.SelectedIndex * 0.2f;
      float num2 = (float) this.targeting_comboBox.SelectedIndex * 0.2f;
      this.cost_textBox.Text = Math.Round((double) (BaconFighter.fighterBuildCost * (10 + (int) CustomBomberForm.sizeIncrease)) * ((double) CustomBomberForm.costMultiplier + (double) num2 + (double) num1)).ToString();
    }

    private void weaponMountMinusOne_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.weaponMountLevels;
      this.weapon_textBox.Text = CustomBomberForm.weaponMountLevels.ToString();
      this.UpdateSize((short) -5);
      this.UpdateCost();
      if (CustomBomberForm.weaponMountLevels >= (short) 1)
        return;
      this.weaponMountMinusOne_button.Enabled = false;
    }

    private void weaponMountPlusOne_button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.weaponMountLevels;
      this.weapon_textBox.Text = CustomBomberForm.weaponMountLevels.ToString();
      this.UpdateSize((short) 5);
      this.UpdateCost();
      this.weaponMountMinusOne_button.Enabled = true;
    }

    private void shieldLayersMinusOne_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.shieldIncreaseLevels;
      this.shieldLayers_textBox.Text = CustomBomberForm.shieldIncreaseLevels.ToString();
      this.UpdateSize((short) -3);
      this.UpdateCost();
      if (CustomBomberForm.shieldIncreaseLevels >= (short) 1)
        return;
      this.shieldLayersMinusOne_button.Enabled = false;
    }

    private void shieldLayersPlusOne_button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.shieldIncreaseLevels;
      this.shieldLayers_textBox.Text = CustomBomberForm.shieldIncreaseLevels.ToString();
      this.UpdateSize((short) 3);
      this.UpdateCost();
      this.shieldLayersMinusOne_button.Enabled = true;
    }

    private void shieldRechargeMinusOne_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.shieldRegenLevels;
      this.shieldRegen_textBox.Text = CustomBomberForm.shieldRegenLevels.ToString();
      CustomBomberForm.costMultiplier -= 0.3f;
      if (CustomBomberForm.shieldRegenLevels < (short) 1)
        this.shieldRechargeMinusOne_button.Enabled = false;
      this.UpdateCost();
    }

    private void shieldRechargePlusOne__button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.shieldRegenLevels;
      this.shieldRegen_textBox.Text = CustomBomberForm.shieldRegenLevels.ToString();
      CustomBomberForm.costMultiplier += 0.3f;
      this.shieldRechargeMinusOne_button.Enabled = true;
      this.UpdateCost();
    }

    private void engineMinusOne_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.engineIncreaseLevels;
      this.engineOutput_textBox.Text = CustomBomberForm.engineIncreaseLevels.ToString();
      this.UpdateSize((short) -2);
      this.UpdateCost();
      if (CustomBomberForm.engineIncreaseLevels >= (short) 1)
        return;
      this.engineMinusOne_button.Enabled = false;
    }

    private void enginePlusOne__button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.engineIncreaseLevels;
      this.engineOutput_textBox.Text = CustomBomberForm.engineIncreaseLevels.ToString();
      this.UpdateSize((short) 2);
      this.UpdateCost();
      this.engineMinusOne_button.Enabled = true;
    }

    private void extraAmmoMinusOne_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.ammoMultiplier;
      this.extraAmmo_textBox.Text = CustomBomberForm.ammoMultiplier.ToString();
      this.UpdateSize((short) -2);
      this.UpdateCost();
      if ((double) CustomBomberForm.ammoMultiplier >= 1.0)
        return;
      this.extraAmmoMinusOne_button.Enabled = false;
    }

    private void extraAmmoPlusOne_button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.ammoMultiplier;
      this.extraAmmo_textBox.Text = CustomBomberForm.ammoMultiplier.ToString();
      this.UpdateSize((short) 2);
      this.UpdateCost();
      this.extraAmmoMinusOne_button.Enabled = true;
    }

    private void targeting_comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        CustomBomberForm.targetingBonus = short.Parse(this.targeting_comboBox.SelectedItem.ToString());
        this.UpdateCost();
      }
      catch (Exception ex)
      {
      }
    }

    private void countermeasures_comboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        CustomBomberForm.countermeasuresBonus = short.Parse(this.countermeasures_comboBox.SelectedItem.ToString());
        this.UpdateCost();
      }
      catch (Exception ex)
      {
      }
    }

    private void CustomBomberForm_FormClosed(object sender, FormClosedEventArgs e) => BaconMain.customBomberFormOpen = false;

    public static FighterSpecification CloneFighterSpecification(FighterSpecification original) => new FighterSpecification()
    {
      AccelerationRate = original.AccelerationRate,
      CountermeasureModifier = original.CountermeasureModifier,
      DamageRepairRate = original.DamageRepairRate,
      EnergyCapacity = original.EnergyCapacity,
      EnergyRechargeRate = original.EnergyRechargeRate,
      EngineExhaustImageIndex = original.EngineExhaustImageIndex,
      FighterSpecificationId = original.FighterSpecificationId,
      Name = original.Name,
      ShieldRechargeRate = original.ShieldRechargeRate,
      ShieldsCapacity = original.ShieldsCapacity,
      Size = original.Size,
      SortTag = original.SortTag,
      TargettingModifier = original.TargettingModifier,
      TechLevel = original.TechLevel,
      TopSpeed = original.TopSpeed,
      TopSpeedEnergyConsumptionRate = original.TopSpeedEnergyConsumptionRate,
      TurnRate = original.TurnRate,
      Type = original.Type,
      WeaponDamage = original.WeaponDamage,
      WeaponDamageLoss = original.WeaponDamageLoss,
      WeaponEnergyRequired = original.WeaponEnergyRequired,
      WeaponFireRate = original.WeaponFireRate,
      WeaponImageIndex = original.WeaponImageIndex,
      WeaponRange = original.WeaponRange,
      WeaponSoundEffectFilename = original.WeaponSoundEffectFilename,
      WeaponSpeed = original.WeaponSpeed,
      WeaponType = original.WeaponType
    };

    private void imageSelectBack_button_Click(object sender, EventArgs e)
    {
      --CustomBomberForm.DesignPictureFamilyIndex;
      if (CustomBomberForm.DesignPictureFamilyIndex < 1)
        CustomBomberForm.DesignPictureFamilyIndex = CustomBomberForm.main._Game.Galaxy.Races.Count - 1;
      Bitmap bitmap = new Bitmap((Image) CustomBomberForm.main.bitmap_6[CustomBomberForm.DesignPictureFamilyIndex * 2 + 1]);
      this.bomberPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.bomberPictureBox.Image = (Image) bitmap;
      this.bomberName_textBox.Update();
    }

    private void imageSelectNext_button_Click(object sender, EventArgs e)
    {
      ++CustomBomberForm.DesignPictureFamilyIndex;
      if (CustomBomberForm.DesignPictureFamilyIndex > CustomBomberForm.main._Game.Galaxy.Races.Count - 1)
        CustomBomberForm.DesignPictureFamilyIndex = 1;
      Bitmap bitmap = new Bitmap((Image) CustomBomberForm.main.bitmap_6[CustomBomberForm.DesignPictureFamilyIndex * 2 + 1]);
      this.bomberPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      this.bomberPictureBox.Image = (Image) bitmap;
      this.bomberName_textBox.Update();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label_FighterName = new Label();
      this.label_WeaponMounts = new Label();
      this.label_Shield = new Label();
      this.label_Engine = new Label();
      this.label_Targeting = new Label();
      this.label_Countermeasures = new Label();
      this.label_Size = new Label();
      this.size_textBox = new TextBox();
      this.engineOutput_textBox = new TextBox();
      this.shieldLayers_textBox = new TextBox();
      this.weapon_textBox = new TextBox();
      this.targeting_comboBox = new ComboBox();
      this.countermeasures_comboBox = new ComboBox();
      this.bomberName_textBox = new TextBox();
      this.label_shieldRegen = new Label();
      this.shieldRegen_textBox = new TextBox();
      this.createBomber_button = new Button();
      this.weaponMountPlusOne_button = new Button();
      this.weaponMountMinusOne_button = new Button();
      this.shieldLayersMinusOne_button = new Button();
      this.shieldLayersPlusOne_button = new Button();
      this.shieldRechargeMinusOne_button = new Button();
      this.shieldRechargePlusOne__button = new Button();
      this.engineMinusOne_button = new Button();
      this.enginePlusOne__button = new Button();
      this.bomberType_checkBox = new CheckBox();
      this.extraAmmoMinusOne_button = new Button();
      this.extraAmmoPlusOne_button = new Button();
      this.extraAmmo_textBox = new TextBox();
      this.label_ExtraAmmo = new Label();
      this.bomberPictureBox = new PictureBox();
      this.imageSelectBack_button = new Button();
      this.imageSelectNext_button = new Button();
      this.label_Cost = new Label();
      this.cost_textBox = new TextBox();
      ((ISupportInitialize) this.bomberPictureBox).BeginInit();
      this.SuspendLayout();
      this.label_FighterName.AutoSize = true;
      this.label_FighterName.Location = new Point(50, 39);
      this.label_FighterName.Name = "label_FighterName";
      this.label_FighterName.Size = new Size(77, 13);
      this.label_FighterName.TabIndex = 0;
      this.label_FighterName.Text = "Bomber Name:";
      this.label_WeaponMounts.AutoSize = true;
      this.label_WeaponMounts.Location = new Point(50, 119);
      this.label_WeaponMounts.Name = "label_WeaponMounts";
      this.label_WeaponMounts.Size = new Size(116, 13);
      this.label_WeaponMounts.TabIndex = 1;
      this.label_WeaponMounts.Text = "Extra Weapon Mounts:";
      this.label_Shield.AutoSize = true;
      this.label_Shield.Location = new Point(50, 160);
      this.label_Shield.Name = "label_Shield";
      this.label_Shield.Size = new Size(93, 13);
      this.label_Shield.TabIndex = 2;
      this.label_Shield.Text = "Extra Shield layers";
      this.label_Engine.AutoSize = true;
      this.label_Engine.Location = new Point(50, 238);
      this.label_Engine.Name = "label_Engine";
      this.label_Engine.Size = new Size(102, 13);
      this.label_Engine.TabIndex = 3;
      this.label_Engine.Text = "Extra Engine Output";
      this.label_Targeting.AutoSize = true;
      this.label_Targeting.Location = new Point(50, 307);
      this.label_Targeting.Name = "label_Targeting";
      this.label_Targeting.Size = new Size(88, 13);
      this.label_Targeting.TabIndex = 4;
      this.label_Targeting.Text = "Targeting Bonus:";
      this.label_Countermeasures.AutoSize = true;
      this.label_Countermeasures.Location = new Point(50, 337);
      this.label_Countermeasures.Name = "label_Countermeasures";
      this.label_Countermeasures.Size = new Size(92, 13);
      this.label_Countermeasures.TabIndex = 5;
      this.label_Countermeasures.Text = "Countermeasures:";
      this.label_Size.AutoSize = true;
      this.label_Size.Location = new Point(50, 80);
      this.label_Size.Name = "label_Size";
      this.label_Size.Size = new Size(115, 13);
      this.label_Size.TabIndex = 6;
      this.label_Size.Text = "Bomber increased Size";
      this.size_textBox.Location = new Point(174, 77);
      this.size_textBox.Name = "size_textBox";
      this.size_textBox.ReadOnly = true;
      this.size_textBox.Size = new Size(53, 20);
      this.size_textBox.TabIndex = 7;
      this.engineOutput_textBox.Location = new Point(174, 230);
      this.engineOutput_textBox.Name = "engineOutput_textBox";
      this.engineOutput_textBox.ReadOnly = true;
      this.engineOutput_textBox.Size = new Size(53, 20);
      this.engineOutput_textBox.TabIndex = 8;
      this.shieldLayers_textBox.Location = new Point(174, 153);
      this.shieldLayers_textBox.Name = "shieldLayers_textBox";
      this.shieldLayers_textBox.ReadOnly = true;
      this.shieldLayers_textBox.Size = new Size(53, 20);
      this.shieldLayers_textBox.TabIndex = 9;
      this.weapon_textBox.Location = new Point(174, 112);
      this.weapon_textBox.Name = "weapon_textBox";
      this.weapon_textBox.ReadOnly = true;
      this.weapon_textBox.Size = new Size(53, 20);
      this.weapon_textBox.TabIndex = 10;
      this.targeting_comboBox.FormattingEnabled = true;
      this.targeting_comboBox.Items.AddRange(new object[6]
      {
        (object) "0",
        (object) "5",
        (object) "10",
        (object) "15",
        (object) "20",
        (object) "25"
      });
      this.targeting_comboBox.Location = new Point(174, 298);
      this.targeting_comboBox.Name = "targeting_comboBox";
      this.targeting_comboBox.Size = new Size(53, 21);
      this.targeting_comboBox.TabIndex = 11;
      this.targeting_comboBox.SelectedIndexChanged += new EventHandler(this.targeting_comboBox_SelectedIndexChanged);
      this.countermeasures_comboBox.FormattingEnabled = true;
      this.countermeasures_comboBox.Items.AddRange(new object[6]
      {
        (object) "0",
        (object) "5",
        (object) "10",
        (object) "15",
        (object) "20",
        (object) "25"
      });
      this.countermeasures_comboBox.Location = new Point(174, 334);
      this.countermeasures_comboBox.Name = "countermeasures_comboBox";
      this.countermeasures_comboBox.Size = new Size(53, 21);
      this.countermeasures_comboBox.TabIndex = 12;
      this.countermeasures_comboBox.SelectedIndexChanged += new EventHandler(this.countermeasures_comboBox_SelectedIndexChanged);
      this.bomberName_textBox.Location = new Point(174, 36);
      this.bomberName_textBox.Name = "bomberName_textBox";
      this.bomberName_textBox.Size = new Size(137, 20);
      this.bomberName_textBox.TabIndex = 13;
      this.label_shieldRegen.AutoSize = true;
      this.label_shieldRegen.Location = new Point(50, 201);
      this.label_shieldRegen.Name = "label_shieldRegen";
      this.label_shieldRegen.Size = new Size(113, 13);
      this.label_shieldRegen.TabIndex = 14;
      this.label_shieldRegen.Text = "Extra Shield Recharge";
      this.shieldRegen_textBox.Location = new Point(174, 194);
      this.shieldRegen_textBox.Name = "shieldRegen_textBox";
      this.shieldRegen_textBox.ReadOnly = true;
      this.shieldRegen_textBox.Size = new Size(53, 20);
      this.shieldRegen_textBox.TabIndex = 15;
      this.createBomber_button.Location = new Point(53, 402);
      this.createBomber_button.Name = "createBomber_button";
      this.createBomber_button.Size = new Size(75, 23);
      this.createBomber_button.TabIndex = 16;
      this.createBomber_button.Text = "Create";
      this.createBomber_button.UseVisualStyleBackColor = true;
      this.createBomber_button.Click += new EventHandler(this.createBomber_button_Click);
      this.weaponMountPlusOne_button.Location = new Point(290, 109);
      this.weaponMountPlusOne_button.Name = "weaponMountPlusOne_button";
      this.weaponMountPlusOne_button.Size = new Size(50, 23);
      this.weaponMountPlusOne_button.TabIndex = 17;
      this.weaponMountPlusOne_button.Text = "+1";
      this.weaponMountPlusOne_button.UseVisualStyleBackColor = true;
      this.weaponMountPlusOne_button.Click += new EventHandler(this.weaponMountPlusOne_button_Click);
      this.weaponMountMinusOne_button.Enabled = false;
      this.weaponMountMinusOne_button.Location = new Point(240, 110);
      this.weaponMountMinusOne_button.Name = "weaponMountMinusOne_button";
      this.weaponMountMinusOne_button.Size = new Size(44, 23);
      this.weaponMountMinusOne_button.TabIndex = 18;
      this.weaponMountMinusOne_button.Text = "-1";
      this.weaponMountMinusOne_button.UseVisualStyleBackColor = true;
      this.weaponMountMinusOne_button.Click += new EventHandler(this.weaponMountMinusOne_button_Click);
      this.shieldLayersMinusOne_button.Enabled = false;
      this.shieldLayersMinusOne_button.Location = new Point(240, 150);
      this.shieldLayersMinusOne_button.Name = "shieldLayersMinusOne_button";
      this.shieldLayersMinusOne_button.Size = new Size(44, 23);
      this.shieldLayersMinusOne_button.TabIndex = 20;
      this.shieldLayersMinusOne_button.Text = "-100%";
      this.shieldLayersMinusOne_button.UseVisualStyleBackColor = true;
      this.shieldLayersMinusOne_button.Click += new EventHandler(this.shieldLayersMinusOne_button_Click);
      this.shieldLayersPlusOne_button.Location = new Point(290, 149);
      this.shieldLayersPlusOne_button.Name = "shieldLayersPlusOne_button";
      this.shieldLayersPlusOne_button.Size = new Size(50, 23);
      this.shieldLayersPlusOne_button.TabIndex = 19;
      this.shieldLayersPlusOne_button.Text = "+100%";
      this.shieldLayersPlusOne_button.UseVisualStyleBackColor = true;
      this.shieldLayersPlusOne_button.Click += new EventHandler(this.shieldLayersPlusOne_button_Click);
      this.shieldRechargeMinusOne_button.Enabled = false;
      this.shieldRechargeMinusOne_button.Location = new Point(240, 192);
      this.shieldRechargeMinusOne_button.Name = "shieldRechargeMinusOne_button";
      this.shieldRechargeMinusOne_button.Size = new Size(44, 23);
      this.shieldRechargeMinusOne_button.TabIndex = 22;
      this.shieldRechargeMinusOne_button.Text = "-50%";
      this.shieldRechargeMinusOne_button.UseVisualStyleBackColor = true;
      this.shieldRechargeMinusOne_button.Click += new EventHandler(this.shieldRechargeMinusOne_button_Click);
      this.shieldRechargePlusOne__button.Location = new Point(290, 191);
      this.shieldRechargePlusOne__button.Name = "shieldRechargePlusOne__button";
      this.shieldRechargePlusOne__button.Size = new Size(50, 23);
      this.shieldRechargePlusOne__button.TabIndex = 21;
      this.shieldRechargePlusOne__button.Text = "+50%";
      this.shieldRechargePlusOne__button.UseVisualStyleBackColor = true;
      this.shieldRechargePlusOne__button.Click += new EventHandler(this.shieldRechargePlusOne__button_Click);
      this.engineMinusOne_button.Enabled = false;
      this.engineMinusOne_button.Location = new Point(240, 227);
      this.engineMinusOne_button.Name = "engineMinusOne_button";
      this.engineMinusOne_button.Size = new Size(44, 23);
      this.engineMinusOne_button.TabIndex = 24;
      this.engineMinusOne_button.Text = "-10%";
      this.engineMinusOne_button.UseVisualStyleBackColor = true;
      this.engineMinusOne_button.Click += new EventHandler(this.engineMinusOne_button_Click);
      this.enginePlusOne__button.Location = new Point(290, 226);
      this.enginePlusOne__button.Name = "enginePlusOne__button";
      this.enginePlusOne__button.Size = new Size(50, 23);
      this.enginePlusOne__button.TabIndex = 23;
      this.enginePlusOne__button.Text = "+10%";
      this.enginePlusOne__button.UseVisualStyleBackColor = true;
      this.enginePlusOne__button.Click += new EventHandler(this.enginePlusOne__button_Click);
      this.bomberType_checkBox.AutoSize = true;
      this.bomberType_checkBox.Location = new Point(53, 368);
      this.bomberType_checkBox.Name = "bomberType_checkBox";
      this.bomberType_checkBox.Size = new Size(105, 17);
      this.bomberType_checkBox.TabIndex = 25;
      this.bomberType_checkBox.Text = "Torpedo Bomber";
      this.bomberType_checkBox.UseVisualStyleBackColor = true;
      this.extraAmmoMinusOne_button.Enabled = false;
      this.extraAmmoMinusOne_button.Location = new Point(240, 260);
      this.extraAmmoMinusOne_button.Name = "extraAmmoMinusOne_button";
      this.extraAmmoMinusOne_button.Size = new Size(44, 23);
      this.extraAmmoMinusOne_button.TabIndex = 29;
      this.extraAmmoMinusOne_button.Text = "-10%";
      this.extraAmmoMinusOne_button.UseVisualStyleBackColor = true;
      this.extraAmmoMinusOne_button.Click += new EventHandler(this.extraAmmoMinusOne_button_Click);
      this.extraAmmoPlusOne_button.Location = new Point(290, 259);
      this.extraAmmoPlusOne_button.Name = "extraAmmoPlusOne_button";
      this.extraAmmoPlusOne_button.Size = new Size(50, 23);
      this.extraAmmoPlusOne_button.TabIndex = 28;
      this.extraAmmoPlusOne_button.Text = "+10%";
      this.extraAmmoPlusOne_button.UseVisualStyleBackColor = true;
      this.extraAmmoPlusOne_button.Click += new EventHandler(this.extraAmmoPlusOne_button_Click);
      this.extraAmmo_textBox.Location = new Point(174, 263);
      this.extraAmmo_textBox.Name = "extraAmmo_textBox";
      this.extraAmmo_textBox.ReadOnly = true;
      this.extraAmmo_textBox.Size = new Size(53, 20);
      this.extraAmmo_textBox.TabIndex = 27;
      this.label_ExtraAmmo.AutoSize = true;
      this.label_ExtraAmmo.Location = new Point(50, 271);
      this.label_ExtraAmmo.Name = "label_ExtraAmmo";
      this.label_ExtraAmmo.Size = new Size(63, 13);
      this.label_ExtraAmmo.TabIndex = 26;
      this.label_ExtraAmmo.Text = "Extra Ammo";
      this.bomberPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
      this.bomberPictureBox.Location = new Point(428, 48);
      this.bomberPictureBox.Name = "bomberPictureBox";
      this.bomberPictureBox.Size = new Size(370, 307);
      this.bomberPictureBox.TabIndex = 30;
      this.bomberPictureBox.TabStop = false;
      this.imageSelectBack_button.Location = new Point(518, 19);
      this.imageSelectBack_button.Name = "imageSelectBack_button";
      this.imageSelectBack_button.Size = new Size(75, 23);
      this.imageSelectBack_button.TabIndex = 31;
      this.imageSelectBack_button.Text = "Previous";
      this.imageSelectBack_button.UseVisualStyleBackColor = true;
      this.imageSelectBack_button.Click += new EventHandler(this.imageSelectBack_button_Click);
      this.imageSelectNext_button.Location = new Point(609, 19);
      this.imageSelectNext_button.Name = "imageSelectNext_button";
      this.imageSelectNext_button.Size = new Size(75, 23);
      this.imageSelectNext_button.TabIndex = 32;
      this.imageSelectNext_button.Text = "Next";
      this.imageSelectNext_button.UseVisualStyleBackColor = true;
      this.imageSelectNext_button.Click += new EventHandler(this.imageSelectNext_button_Click);
      this.label_Cost.AutoSize = true;
      this.label_Cost.Location = new Point(237, 80);
      this.label_Cost.Name = "label_Cost";
      this.label_Cost.Size = new Size(28, 13);
      this.label_Cost.TabIndex = 33;
      this.label_Cost.Text = "Cost";
      this.cost_textBox.Location = new Point(271, 73);
      this.cost_textBox.Name = "cost_textBox";
      this.cost_textBox.ReadOnly = true;
      this.cost_textBox.Size = new Size(53, 20);
      this.cost_textBox.TabIndex = 34;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Window;
      this.ClientSize = new Size(800, 450);
      this.Controls.Add((Control) this.cost_textBox);
      this.Controls.Add((Control) this.label_Cost);
      this.Controls.Add((Control) this.imageSelectNext_button);
      this.Controls.Add((Control) this.imageSelectBack_button);
      this.Controls.Add((Control) this.bomberPictureBox);
      this.Controls.Add((Control) this.extraAmmoMinusOne_button);
      this.Controls.Add((Control) this.extraAmmoPlusOne_button);
      this.Controls.Add((Control) this.extraAmmo_textBox);
      this.Controls.Add((Control) this.label_ExtraAmmo);
      this.Controls.Add((Control) this.bomberType_checkBox);
      this.Controls.Add((Control) this.engineMinusOne_button);
      this.Controls.Add((Control) this.enginePlusOne__button);
      this.Controls.Add((Control) this.shieldRechargeMinusOne_button);
      this.Controls.Add((Control) this.shieldRechargePlusOne__button);
      this.Controls.Add((Control) this.shieldLayersMinusOne_button);
      this.Controls.Add((Control) this.shieldLayersPlusOne_button);
      this.Controls.Add((Control) this.weaponMountMinusOne_button);
      this.Controls.Add((Control) this.weaponMountPlusOne_button);
      this.Controls.Add((Control) this.createBomber_button);
      this.Controls.Add((Control) this.shieldRegen_textBox);
      this.Controls.Add((Control) this.label_shieldRegen);
      this.Controls.Add((Control) this.bomberName_textBox);
      this.Controls.Add((Control) this.countermeasures_comboBox);
      this.Controls.Add((Control) this.targeting_comboBox);
      this.Controls.Add((Control) this.weapon_textBox);
      this.Controls.Add((Control) this.shieldLayers_textBox);
      this.Controls.Add((Control) this.engineOutput_textBox);
      this.Controls.Add((Control) this.size_textBox);
      this.Controls.Add((Control) this.label_Size);
      this.Controls.Add((Control) this.label_Countermeasures);
      this.Controls.Add((Control) this.label_Targeting);
      this.Controls.Add((Control) this.label_Engine);
      this.Controls.Add((Control) this.label_Shield);
      this.Controls.Add((Control) this.label_WeaponMounts);
      this.Controls.Add((Control) this.label_FighterName);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (CustomBomberForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Custom Bomber";
      this.FormClosed += new FormClosedEventHandler(this.CustomBomberForm_FormClosed);
      ((ISupportInitialize) this.bomberPictureBox).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
