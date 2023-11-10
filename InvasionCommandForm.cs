// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.InvasionCommandForm
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using BaconDistantWorlds.Properties;
using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
  public class InvasionCommandForm : Form
  {
    public Habitat planet = (Habitat) null;
    public bool wasPaused = false;
    public int[] currentGuess = new int[4];
    public int[] enemyTactics = new int[4];
    public int currentGuessNumber = 1;
    public int maxNumberOfGusses = 10;
    private int mostRedPegs = 0;
    private bool isPlayerInvading = false;
    public List<Character> generalsInBattle = new List<Character>();
    public string advice = "";
    public string victoryMessage = "Success! You have determined the strategy being employed by the enemy commander. No doubt this will provide a huge benefit on the field.";
    public string defeatMessage = "You were not able to determine the enemy commander's strategy.";
    private IContainer components = (IContainer) null;
    private PictureBox tradePartnerPicture;
    private Label planetNamelabel;
    private Label EnemyFormationLabel;
    private Label leftlabel;
    private Label actualEnemyTacticLeftLabel;
    private ComboBox leftFlankComboBox;
    private ComboBox mainBodyComboBox;
    private ComboBox rightFlankComboBox;
    private ComboBox reservesComboBox;
    private Button guessButton;
    private Label mainBodyLabel;
    private Label rightlabel;
    private Label reserveLabel;
    private Label actualEnemyTacticMainLabel;
    private Label actualEnemyTacticRightLabel;
    private Label actualEnemyTacticReserveLabel;
    private Label label1_1;
    private Label label2_1;
    private Label label3_1;
    private Label label5_1;
    private Label label4_1;
    private Label label10_1;
    private Label label9_1;
    private Label label8_1;
    private Label label7_1;
    private Label label6_1;
    private Label label10_2;
    private Label label9_2;
    private Label label8_2;
    private Label label7_2;
    private Label label6_2;
    private Label label5_2;
    private Label label4_2;
    private Label label3_2;
    private Label label2_2;
    private Label label1_2;
    private Label label10_3;
    private Label label9_3;
    private Label label8_3;
    private Label label7_3;
    private Label label6_3;
    private Label label5_3;
    private Label label4_3;
    private Label label3_3;
    private Label label2_3;
    private Label label1_3;
    private Label label10_4;
    private Label label9_4;
    private Label label8_4;
    private Label label7_4;
    private Label label6_4;
    private Label label5_4;
    private Label label4_4;
    private Label label3_4;
    private Label label2_4;
    private Label label1_4;
    private PictureBox pictureBox1_gold;
    private Label label1_gold;
    private Label label1_silver;
    private PictureBox pictureBox1_silver;
    private PictureBox pictureBox2_silver;
    private Label label2_silver;
    private Label label2_gold;
    private PictureBox pictureBox2_gold;
    private PictureBox pictureBox3_silver;
    private Label label3_silver;
    private Label label3_gold;
    private PictureBox pictureBox3_gold;
    private PictureBox pictureBox4_silver;
    private Label label4_silver;
    private Label label4_gold;
    private PictureBox pictureBox4_gold;
    private PictureBox pictureBox5_silver;
    private Label label5_silver;
    private Label label5_gold;
    private PictureBox pictureBox5_gold;
    private PictureBox pictureBox6_silver;
    private Label label6_silver;
    private Label label6_gold;
    private PictureBox pictureBox6_gold;
    private PictureBox pictureBox7_silver;
    private Label label7_silver;
    private Label label7_gold;
    private PictureBox pictureBox7_gold;
    private PictureBox pictureBox8_silver;
    private Label label8_silver;
    private Label label8_gold;
    private PictureBox pictureBox8_gold;
    private PictureBox pictureBox9_silver;
    private Label label9_silver;
    private Label label9_gold;
    private PictureBox pictureBox9_gold;
    private PictureBox pictureBox10_silver;
    private Label label10_silver;
    private Label label10_gold;
    private PictureBox pictureBox10_gold;
    private PictureBox battlefieldPicture;
    private TextBox generalsInBattleTextBox;
    private TextBox generalAdviceTextBox;
    private Button seekAdviceButton;
    private ProgressBar battleProgressBar;
    private ToolTip seekAdviceToolTip;

    public InvasionCommandForm(Main main, Habitat invasionPlanet)
    {
      try
      {
        this.planet = invasionPlanet;
        BaconMain.invasionCommandFormOpen = true;
        this.isPlayerInvading = this.planet.InvadingTroops[0].Empire == BaconBuiltObject.myMain._Game.PlayerEmpire;
        this.InitializeComponent();
        this.ClearAllLabels();
        this.leftFlankComboBox.SelectedItem = (object) 0;
        this.leftFlankComboBox.Text = Enum.GetName(typeof (TacticType), (object) 0);
        this.mainBodyComboBox.SelectedItem = (object) 0;
        this.mainBodyComboBox.Text = Enum.GetName(typeof (TacticType), (object) 0);
        this.rightFlankComboBox.SelectedItem = (object) 0;
        this.rightFlankComboBox.Text = Enum.GetName(typeof (TacticType), (object) 0);
        this.reservesComboBox.SelectedItem = (object) 0;
        this.reservesComboBox.Text = Enum.GetName(typeof (TacticType), (object) 0);
        this.battleProgressBar.ForeColor = Color.Red;
        if (this.isPlayerInvading)
        {
          this.generalsInBattle = this.planet.InvadingCharacters.Where<Character>((Func<Character, bool>) (x => x.Empire == main._Game.PlayerEmpire && x.Role == CharacterRole.TroopGeneral)).ToList<Character>();
          this.generalsInBattle = this.generalsInBattle.OrderBy<Character, CharacterSkill>((Func<Character, CharacterSkill>) (x => x.Skills.FirstOrDefault<CharacterSkill>((Func<CharacterSkill, bool>) (y => y.Type == CharacterSkillType.TroopGroundAttack)))).ToList<Character>();
        }
        else
        {
          this.generalsInBattle = this.planet.Characters.Where<Character>((Func<Character, bool>) (x => x.Empire == main._Game.PlayerEmpire && x.Role == CharacterRole.TroopGeneral)).ToList<Character>();
          this.generalsInBattle = this.generalsInBattle.OrderBy<Character, CharacterSkill>((Func<Character, CharacterSkill>) (x => x.Skills.FirstOrDefault<CharacterSkill>((Func<CharacterSkill, bool>) (y => y.Type == CharacterSkillType.TroopGroundDefense)))).ToList<Character>();
        }
        if (this.generalsInBattle.Count == 0)
        {
          this.generalAdviceTextBox.Hide();
          this.generalsInBattleTextBox.Hide();
          this.seekAdviceButton.Hide();
        }
        else
        {
          string str = "";
          foreach (Character character in this.generalsInBattle)
            str = str + character.Name + Environment.NewLine;
          this.generalsInBattleTextBox.Text = str;
        }
        Random random = new Random();
        int num1 = random.Next(0, 6);
        int num2 = random.Next(0, 6);
        int num3 = random.Next(0, 6);
        int num4 = random.Next(0, 6);
        this.enemyTactics[0] = num1;
        this.enemyTactics[1] = num2;
        this.enemyTactics[2] = num3;
        this.enemyTactics[3] = num4;
        this.planetNamelabel.Text = this.planet.Name;
        this.SizeLabelFont(this.planetNamelabel);
        string empty = string.Empty;
        Bitmap bitmap = (Bitmap) null;
        if (this.planet != null)
        {
          bool smallImageSupplied = false;
          bitmap = new Bitmap((Image) BaconBuiltObject.myMain.habitatImageCache_0.FastGetImage((int) this.planet.PictureRef, out smallImageSupplied));
        }
        if (bitmap != null)
          this.tradePartnerPicture.Image = (Image) bitmap;
        this.wasPaused = main._Game.Galaxy.TimeState == GalaxyTimeState.Paused;
        if (this.wasPaused)
          return;
        main._Game.Galaxy.Pause();
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private void SizeLabelFont(Label lbl)
    {
      string text = lbl.Text;
      if (text.Length <= 0)
        return;
      int emSize1 = 100;
      int num1 = lbl.DisplayRectangle.Width - 3;
      int num2 = lbl.DisplayRectangle.Height - 3;
      using (Graphics graphics = lbl.CreateGraphics())
      {
        for (int emSize2 = 1; emSize2 <= 100; ++emSize2)
        {
          using (Font font = new Font(lbl.Font.FontFamily, (float) emSize2))
          {
            SizeF sizeF = graphics.MeasureString(text, font);
            if ((double) sizeF.Width > (double) num1 || (double) sizeF.Height > (double) num2)
            {
              emSize1 = emSize2 - 1;
              break;
            }
          }
        }
      }
      lbl.Font = new Font(lbl.Font.FontFamily, (float) emSize1);
    }

    private void InvasionCommandForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.currentGuessNumber >= 1)
        this.ApplyCombatModifiers();
      BaconMain.invasionCommandFormOpen = false;
      if (this.wasPaused || BaconBuiltObject.myMain == null)
        return;
      BaconBuiltObject.myMain._Game.Galaxy.Resume();
    }

    private void leftFlankComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.currentGuess[0] = this.leftFlankComboBox.SelectedIndex;

    private void mainBodyComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.currentGuess[1] = this.mainBodyComboBox.SelectedIndex;

    private void rightFlankComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.currentGuess[2] = this.rightFlankComboBox.SelectedIndex;

    private void reservesComboBox_SelectedIndexChanged(object sender, EventArgs e) => this.currentGuess[3] = this.reservesComboBox.SelectedIndex;

    private void guessButton_Click(object sender, EventArgs e)
    {
      this.AddPlayerGuess();
      if (this.mostRedPegs == 4)
      {
        this.RevealEnemyStrategy();
        BaconBuiltObject.ShowMessageBox(BaconBuiltObject.myMain, this.victoryMessage, "Success");
        BaconMain.invasionCommandFormOpen = false;
        this.Close();
      }
      else if (this.currentGuessNumber > 9)
      {
        this.RevealEnemyStrategy();
        BaconBuiltObject.ShowMessageBox(BaconBuiltObject.myMain, this.defeatMessage, "Defeat");
        BaconMain.invasionCommandFormOpen = false;
        this.Close();
      }
      ++this.currentGuessNumber;
      this.battleProgressBar.PerformStep();
    }

    public void RevealEnemyStrategy()
    {
      Control control1 = ((IEnumerable<Control>) this.Controls.Find("actualEnemyTacticLeftLabel", true)).FirstOrDefault<Control>();
      if (control1 != null && control1 is Label)
      {
        control1.Text = Enum.GetName(typeof (TacticType), (object) this.enemyTactics[0]);
        control1.Show();
      }
      Control control2 = ((IEnumerable<Control>) this.Controls.Find("actualEnemyTacticMainLabel", true)).FirstOrDefault<Control>();
      if (control2 != null && control2 is Label)
      {
        control2.Text = Enum.GetName(typeof (TacticType), (object) this.enemyTactics[1]);
        control2.Show();
      }
      Control control3 = ((IEnumerable<Control>) this.Controls.Find("actualEnemyTacticRightLabel", true)).FirstOrDefault<Control>();
      if (control3 != null && control3 is Label)
      {
        control3.Text = Enum.GetName(typeof (TacticType), (object) this.enemyTactics[2]);
        control3.Show();
      }
      Control control4 = ((IEnumerable<Control>) this.Controls.Find("actualEnemyTacticReserveLabel", true)).FirstOrDefault<Control>();
      if (control4 != null && control4 is Label)
      {
        control4.Text = Enum.GetName(typeof (TacticType), (object) this.enemyTactics[3]);
        control4.Show();
      }
      this.Refresh();
    }

    public void AddHeaderStringForThisGuess()
    {
    }

    public void AddPlayerGuess()
    {
      this.FillInGuessLabels();
      this.AnalyzeGuess();
    }

    public void FillInGuessLabels()
    {
      Control control1 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_1", true)).FirstOrDefault<Control>();
      if (control1 != null && control1 is Label)
      {
        control1.Text = Enum.GetName(typeof (TacticType), (object) this.currentGuess[0]);
        control1.Show();
      }
      Control control2 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_2", true)).FirstOrDefault<Control>();
      if (control2 != null && control2 is Label)
      {
        control2.Text = Enum.GetName(typeof (TacticType), (object) this.currentGuess[1]);
        control2.Show();
      }
      Control control3 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_3", true)).FirstOrDefault<Control>();
      if (control3 != null && control3 is Label)
      {
        control3.Text = Enum.GetName(typeof (TacticType), (object) this.currentGuess[2]);
        control3.Show();
      }
      Control control4 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_4", true)).FirstOrDefault<Control>();
      if (control4 == null || !(control4 is Label))
        return;
      control4.Text = Enum.GetName(typeof (TacticType), (object) this.currentGuess[3]);
      control4.Show();
    }

    public void AnalyzeGuess()
    {
      int goldStars = 0;
      int silverStars = 0;
      int[] numArray1 = new int[4]{ -1, -1, -1, -1 };
      int[] numArray2 = new int[4]{ -1, -1, -1, -1 };
      for (int index = 0; index < this.enemyTactics.Length; ++index)
      {
        if (this.enemyTactics[index] == this.currentGuess[index])
        {
          ++goldStars;
          numArray1[index] = 1;
          numArray2[index] = 1;
        }
      }
      for (int index1 = 0; index1 < this.currentGuess.Length; ++index1)
      {
        for (int index2 = 0; index2 < this.enemyTactics.Length; ++index2)
        {
          if (numArray1[index1] != 1 && numArray2[index2] != 1 && this.currentGuess[index1] == this.enemyTactics[index2])
          {
            ++silverStars;
            numArray1[index1] = 1;
            numArray2[index2] = 1;
            break;
          }
        }
      }
      if (goldStars > this.mostRedPegs)
        this.mostRedPegs = goldStars;
      this.FillInStars(goldStars, silverStars);
    }

    public void FillInStars(int goldStars, int silverStars)
    {
      if (goldStars > 0)
      {
        Control control1 = ((IEnumerable<Control>) this.Controls.Find("pictureBox" + this.currentGuessNumber.ToString() + "_gold", true)).FirstOrDefault<Control>();
        if (control1 != null && control1 is PictureBox)
          control1.Show();
        Control control2 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_gold", true)).FirstOrDefault<Control>();
        if (control2 != null && control2 is Label)
        {
          control2.Text = goldStars.ToString();
          control2.Show();
        }
      }
      if (silverStars <= 0)
        return;
      Control control3 = ((IEnumerable<Control>) this.Controls.Find("pictureBox" + this.currentGuessNumber.ToString() + "_silver", true)).FirstOrDefault<Control>();
      if (control3 != null && control3 is PictureBox)
        control3.Show();
      Control control4 = ((IEnumerable<Control>) this.Controls.Find("label" + this.currentGuessNumber.ToString() + "_silver", true)).FirstOrDefault<Control>();
      if (control4 != null && control4 is Label)
      {
        control4.Text = silverStars.ToString();
        control4.Show();
      }
    }

    public void ApplyCombatModifiers()
    {
      try
      {
        float num = BaconMain.invasionStrategyResult + (float) Math.Max(0, Math.Min(5, 10 - this.currentGuessNumber)) * BaconMain.invasionStrategyRemainingGuesses;
        List<Troop> troopList = new List<Troop>();
        List<Troop> invadingTroops = (List<Troop>) this.planet.InvadingTroops;
        if (this.mostRedPegs == 4)
        {
          if (this.isPlayerInvading)
          {
            foreach (Troop troop in invadingTroops)
              troop.Readiness *= num;
          }
          else
          {
            foreach (Troop troop in invadingTroops)
              troop.Readiness /= num;
          }
        }
        else if (this.isPlayerInvading)
        {
          foreach (Troop troop in invadingTroops)
            troop.Readiness /= num;
        }
        else
        {
          foreach (Troop troop in invadingTroops)
            troop.Readiness *= num;
        }
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    public void ClearAllLabels()
    {
      for (int index1 = 1; index1 <= this.maxNumberOfGusses; ++index1)
      {
        for (int index2 = 1; index2 <= 4; ++index2)
        {
          Control control1 = ((IEnumerable<Control>) this.Controls.Find("label" + index1.ToString() + "_" + index2.ToString(), true)).FirstOrDefault<Control>();
          if (control1 is Label)
            (control1 as Label).Text = "";
          Control control2 = ((IEnumerable<Control>) this.Controls.Find("label" + index1.ToString() + "_gold", true)).FirstOrDefault<Control>();
          if (control2 is Label)
            (control2 as Label).Text = "";
          Control control3 = ((IEnumerable<Control>) this.Controls.Find("label" + index1.ToString() + "_silver", true)).FirstOrDefault<Control>();
          if (control3 is Label)
            (control3 as Label).Text = "";
          Control control4 = ((IEnumerable<Control>) this.Controls.Find("pictureBox" + index1.ToString() + "_gold", true)).FirstOrDefault<Control>();
          if (control4 != null && control4 is PictureBox)
            control4.Hide();
          Control control5 = ((IEnumerable<Control>) this.Controls.Find("pictureBox" + index1.ToString() + "_silver", true)).FirstOrDefault<Control>();
          if (control5 != null && control5 is PictureBox)
            control5.Hide();
        }
      }
    }

    private void seekAdviceButton_Click(object sender, EventArgs e)
    {
      Character general = this.generalsInBattle[this.generalsInBattle.Count - 1];
      this.DisplayAdvice(general);
      this.SendGeneralHome(general);
      if (this.generalsInBattle.Count >= 1)
        return;
      this.seekAdviceButton.Hide();
    }

    public void DisplayAdvice(Character general)
    {
      int num = new Random().Next(0, 6);
      if (num == this.enemyTactics[0] || num == this.enemyTactics[1] || num == this.enemyTactics[2] || num == this.enemyTactics[3])
      {
        TextBox generalAdviceTextBox = this.generalAdviceTextBox;
        generalAdviceTextBox.Text = generalAdviceTextBox.Text + "General " + general.Name + " believes the enemy is using " + Enum.GetName(typeof (TacticType), (object) num) + "." + Environment.NewLine;
      }
      else
      {
        TextBox generalAdviceTextBox = this.generalAdviceTextBox;
        generalAdviceTextBox.Text = generalAdviceTextBox.Text + "General " + general.Name + " believes the enemy is NOT using " + Enum.GetName(typeof (TacticType), (object) num) + "." + Environment.NewLine;
      }
    }

    public void SendGeneralHome(Character general)
    {
      StellarObject destination = general.Empire.Capital == null ? (StellarObject) general.Empire.BuiltObjects[0] : (StellarObject) general.Empire.Capital;
      general.TransferToNewLocation(destination, BaconBuiltObject.myMain._Game.Galaxy);
      this.generalsInBattle.Remove(general);
      if (this.isPlayerInvading)
        this.planet.InvadingCharacters.Remove(general);
      else
        this.planet.Characters.Remove(general);
      general.Location = (StellarObject) null;
      this.RefreshGeneralsInBattleTextBox();
    }

    public void RefreshGeneralsInBattleTextBox()
    {
      string str = "";
      foreach (Character character in this.generalsInBattle)
        str = str + character.Name + Environment.NewLine;
      this.generalsInBattleTextBox.Text = str;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      this.planetNamelabel = new Label();
      this.EnemyFormationLabel = new Label();
      this.leftlabel = new Label();
      this.actualEnemyTacticLeftLabel = new Label();
      this.leftFlankComboBox = new ComboBox();
      this.mainBodyComboBox = new ComboBox();
      this.rightFlankComboBox = new ComboBox();
      this.reservesComboBox = new ComboBox();
      this.guessButton = new Button();
      this.mainBodyLabel = new Label();
      this.rightlabel = new Label();
      this.reserveLabel = new Label();
      this.actualEnemyTacticMainLabel = new Label();
      this.actualEnemyTacticRightLabel = new Label();
      this.actualEnemyTacticReserveLabel = new Label();
      this.label1_1 = new Label();
      this.label2_1 = new Label();
      this.label3_1 = new Label();
      this.label5_1 = new Label();
      this.label4_1 = new Label();
      this.label10_1 = new Label();
      this.label9_1 = new Label();
      this.label8_1 = new Label();
      this.label7_1 = new Label();
      this.label6_1 = new Label();
      this.label10_2 = new Label();
      this.label9_2 = new Label();
      this.label8_2 = new Label();
      this.label7_2 = new Label();
      this.label6_2 = new Label();
      this.label5_2 = new Label();
      this.label4_2 = new Label();
      this.label3_2 = new Label();
      this.label2_2 = new Label();
      this.label1_2 = new Label();
      this.label10_3 = new Label();
      this.label9_3 = new Label();
      this.label8_3 = new Label();
      this.label7_3 = new Label();
      this.label6_3 = new Label();
      this.label5_3 = new Label();
      this.label4_3 = new Label();
      this.label3_3 = new Label();
      this.label2_3 = new Label();
      this.label1_3 = new Label();
      this.label10_4 = new Label();
      this.label9_4 = new Label();
      this.label8_4 = new Label();
      this.label7_4 = new Label();
      this.label6_4 = new Label();
      this.label5_4 = new Label();
      this.label4_4 = new Label();
      this.label3_4 = new Label();
      this.label2_4 = new Label();
      this.label1_4 = new Label();
      this.label1_silver = new Label();
      this.label1_gold = new Label();
      this.pictureBox1_gold = new PictureBox();
      this.tradePartnerPicture = new PictureBox();
      this.pictureBox1_silver = new PictureBox();
      this.pictureBox2_silver = new PictureBox();
      this.label2_silver = new Label();
      this.label2_gold = new Label();
      this.pictureBox2_gold = new PictureBox();
      this.pictureBox3_silver = new PictureBox();
      this.label3_silver = new Label();
      this.label3_gold = new Label();
      this.pictureBox3_gold = new PictureBox();
      this.pictureBox4_silver = new PictureBox();
      this.label4_silver = new Label();
      this.label4_gold = new Label();
      this.pictureBox4_gold = new PictureBox();
      this.pictureBox5_silver = new PictureBox();
      this.label5_silver = new Label();
      this.label5_gold = new Label();
      this.pictureBox5_gold = new PictureBox();
      this.pictureBox6_silver = new PictureBox();
      this.label6_silver = new Label();
      this.label6_gold = new Label();
      this.pictureBox6_gold = new PictureBox();
      this.pictureBox7_silver = new PictureBox();
      this.label7_silver = new Label();
      this.label7_gold = new Label();
      this.pictureBox7_gold = new PictureBox();
      this.pictureBox8_silver = new PictureBox();
      this.label8_silver = new Label();
      this.label8_gold = new Label();
      this.pictureBox8_gold = new PictureBox();
      this.pictureBox9_silver = new PictureBox();
      this.label9_silver = new Label();
      this.label9_gold = new Label();
      this.pictureBox9_gold = new PictureBox();
      this.pictureBox10_silver = new PictureBox();
      this.label10_silver = new Label();
      this.label10_gold = new Label();
      this.pictureBox10_gold = new PictureBox();
      this.battlefieldPicture = new PictureBox();
      this.generalsInBattleTextBox = new TextBox();
      this.generalAdviceTextBox = new TextBox();
      this.seekAdviceButton = new Button();
      this.battleProgressBar = new ProgressBar();
      this.seekAdviceToolTip = new ToolTip(this.components);
      ((ISupportInitialize) this.pictureBox1_gold).BeginInit();
      ((ISupportInitialize) this.tradePartnerPicture).BeginInit();
      ((ISupportInitialize) this.pictureBox1_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox2_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox2_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox3_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox3_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox4_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox4_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox5_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox5_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox6_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox6_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox7_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox7_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox8_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox8_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox9_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox9_gold).BeginInit();
      ((ISupportInitialize) this.pictureBox10_silver).BeginInit();
      ((ISupportInitialize) this.pictureBox10_gold).BeginInit();
      ((ISupportInitialize) this.battlefieldPicture).BeginInit();
      this.SuspendLayout();
      this.planetNamelabel.Font = new Font("Microsoft Sans Serif", 27.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.planetNamelabel.Location = new Point(-5, 304);
      this.planetNamelabel.Name = "planetNamelabel";
      this.planetNamelabel.Size = new Size(371, 42);
      this.planetNamelabel.TabIndex = 17;
      this.planetNamelabel.Text = "Planet name here";
      this.EnemyFormationLabel.AutoSize = true;
      this.EnemyFormationLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.EnemyFormationLabel.Location = new Point(476, 38);
      this.EnemyFormationLabel.Name = "EnemyFormationLabel";
      this.EnemyFormationLabel.Size = new Size(185, 26);
      this.EnemyFormationLabel.TabIndex = 20;
      this.EnemyFormationLabel.Text = "Enemy Formation";
      this.leftlabel.AutoSize = true;
      this.leftlabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.leftlabel.Location = new Point(367, 71);
      this.leftlabel.Name = "leftlabel";
      this.leftlabel.Size = new Size(100, 26);
      this.leftlabel.TabIndex = 21;
      this.leftlabel.Text = "Left flank";
      this.actualEnemyTacticLeftLabel.AutoSize = true;
      this.actualEnemyTacticLeftLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.actualEnemyTacticLeftLabel.Location = new Point(391, 106);
      this.actualEnemyTacticLeftLabel.Name = "actualEnemyTacticLeftLabel";
      this.actualEnemyTacticLeftLabel.Size = new Size(48, 26);
      this.actualEnemyTacticLeftLabel.TabIndex = 22;
      this.actualEnemyTacticLeftLabel.Text = "???";
      this.leftFlankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.leftFlankComboBox.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.leftFlankComboBox.FormattingEnabled = true;
      this.leftFlankComboBox.Items.AddRange(new object[6]
      {
        (object) "Advance",
        (object) "Defend",
        (object) "Withdraw",
        (object) "Feign",
        (object) "Bombard",
        (object) "Encircle"
      });
      this.leftFlankComboBox.Location = new Point(367, 156);
      this.leftFlankComboBox.Name = "leftFlankComboBox";
      this.leftFlankComboBox.Size = new Size(110, 33);
      this.leftFlankComboBox.TabIndex = 23;
      this.leftFlankComboBox.SelectedIndexChanged += new EventHandler(this.leftFlankComboBox_SelectedIndexChanged);
      this.mainBodyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.mainBodyComboBox.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.mainBodyComboBox.FormattingEnabled = true;
      this.mainBodyComboBox.Items.AddRange(new object[6]
      {
        (object) "Advance",
        (object) "Defend",
        (object) "Withdraw",
        (object) "Feign",
        (object) "Bombard",
        (object) "Encircle"
      });
      this.mainBodyComboBox.Location = new Point(481, 156);
      this.mainBodyComboBox.Name = "mainBodyComboBox";
      this.mainBodyComboBox.Size = new Size(110, 33);
      this.mainBodyComboBox.TabIndex = 24;
      this.mainBodyComboBox.SelectedIndexChanged += new EventHandler(this.mainBodyComboBox_SelectedIndexChanged);
      this.rightFlankComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.rightFlankComboBox.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.rightFlankComboBox.FormattingEnabled = true;
      this.rightFlankComboBox.Items.AddRange(new object[6]
      {
        (object) "Advance",
        (object) "Defend",
        (object) "Withdraw",
        (object) "Feign",
        (object) "Bombard",
        (object) "Encircle"
      });
      this.rightFlankComboBox.Location = new Point(595, 156);
      this.rightFlankComboBox.Name = "rightFlankComboBox";
      this.rightFlankComboBox.Size = new Size(110, 33);
      this.rightFlankComboBox.TabIndex = 25;
      this.rightFlankComboBox.SelectedIndexChanged += new EventHandler(this.rightFlankComboBox_SelectedIndexChanged);
      this.reservesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.reservesComboBox.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.reservesComboBox.FormattingEnabled = true;
      this.reservesComboBox.Items.AddRange(new object[6]
      {
        (object) "Advance",
        (object) "Defend",
        (object) "Withdraw",
        (object) "Feign",
        (object) "Bombard",
        (object) "Encircle"
      });
      this.reservesComboBox.Location = new Point(710, 156);
      this.reservesComboBox.Name = "reservesComboBox";
      this.reservesComboBox.Size = new Size(110, 33);
      this.reservesComboBox.TabIndex = 26;
      this.reservesComboBox.SelectedIndexChanged += new EventHandler(this.reservesComboBox_SelectedIndexChanged);
      this.guessButton.Location = new Point(830, 156);
      this.guessButton.Name = "guessButton";
      this.guessButton.Size = new Size(108, 23);
      this.guessButton.TabIndex = 27;
      this.guessButton.Text = "Evaluate Tactics";
      this.guessButton.UseVisualStyleBackColor = true;
      this.guessButton.Click += new EventHandler(this.guessButton_Click);
      this.mainBodyLabel.AutoSize = true;
      this.mainBodyLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.mainBodyLabel.Location = new Point(489, 71);
      this.mainBodyLabel.Name = "mainBodyLabel";
      this.mainBodyLabel.Size = new Size(65, 26);
      this.mainBodyLabel.TabIndex = 28;
      this.mainBodyLabel.Text = "Main ";
      this.rightlabel.AutoSize = true;
      this.rightlabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.rightlabel.Location = new Point(590, 71);
      this.rightlabel.Name = "rightlabel";
      this.rightlabel.Size = new Size(115, 26);
      this.rightlabel.TabIndex = 29;
      this.rightlabel.Text = "Right flank";
      this.reserveLabel.AutoSize = true;
      this.reserveLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.reserveLabel.Location = new Point(711, 71);
      this.reserveLabel.Name = "reserveLabel";
      this.reserveLabel.Size = new Size(93, 26);
      this.reserveLabel.TabIndex = 30;
      this.reserveLabel.Text = "Reserve";
      this.actualEnemyTacticMainLabel.AutoSize = true;
      this.actualEnemyTacticMainLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.actualEnemyTacticMainLabel.Location = new Point(489, 106);
      this.actualEnemyTacticMainLabel.Name = "actualEnemyTacticMainLabel";
      this.actualEnemyTacticMainLabel.Size = new Size(48, 26);
      this.actualEnemyTacticMainLabel.TabIndex = 31;
      this.actualEnemyTacticMainLabel.Text = "???";
      this.actualEnemyTacticRightLabel.AutoSize = true;
      this.actualEnemyTacticRightLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.actualEnemyTacticRightLabel.Location = new Point(613, 106);
      this.actualEnemyTacticRightLabel.Name = "actualEnemyTacticRightLabel";
      this.actualEnemyTacticRightLabel.Size = new Size(48, 26);
      this.actualEnemyTacticRightLabel.TabIndex = 32;
      this.actualEnemyTacticRightLabel.Text = "???";
      this.actualEnemyTacticReserveLabel.AutoSize = true;
      this.actualEnemyTacticReserveLabel.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.actualEnemyTacticReserveLabel.Location = new Point(711, 106);
      this.actualEnemyTacticReserveLabel.Name = "actualEnemyTacticReserveLabel";
      this.actualEnemyTacticReserveLabel.Size = new Size(48, 26);
      this.actualEnemyTacticReserveLabel.TabIndex = 33;
      this.actualEnemyTacticReserveLabel.Text = "???";
      this.label1_1.AutoSize = true;
      this.label1_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_1.Location = new Point(367, 209);
      this.label1_1.Name = "label1_1";
      this.label1_1.Size = new Size(70, 26);
      this.label1_1.TabIndex = 34;
      this.label1_1.Text = "label1";
      this.label2_1.AutoSize = true;
      this.label2_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_1.Location = new Point(367, 251);
      this.label2_1.Name = "label2_1";
      this.label2_1.Size = new Size(70, 26);
      this.label2_1.TabIndex = 35;
      this.label2_1.Text = "label1";
      this.label3_1.AutoSize = true;
      this.label3_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_1.Location = new Point(367, 293);
      this.label3_1.Name = "label3_1";
      this.label3_1.Size = new Size(70, 26);
      this.label3_1.TabIndex = 36;
      this.label3_1.Text = "label1";
      this.label5_1.AutoSize = true;
      this.label5_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_1.Location = new Point(367, 377);
      this.label5_1.Name = "label5_1";
      this.label5_1.Size = new Size(70, 26);
      this.label5_1.TabIndex = 38;
      this.label5_1.Text = "label4";
      this.label4_1.AutoSize = true;
      this.label4_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_1.Location = new Point(367, 335);
      this.label4_1.Name = "label4_1";
      this.label4_1.Size = new Size(70, 26);
      this.label4_1.TabIndex = 37;
      this.label4_1.Text = "label1";
      this.label10_1.AutoSize = true;
      this.label10_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_1.Location = new Point(368, 587);
      this.label10_1.Name = "label10_1";
      this.label10_1.Size = new Size(70, 26);
      this.label10_1.TabIndex = 43;
      this.label10_1.Text = "label3";
      this.label9_1.AutoSize = true;
      this.label9_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_1.Location = new Point(368, 545);
      this.label9_1.Name = "label9_1";
      this.label9_1.Size = new Size(70, 26);
      this.label9_1.TabIndex = 42;
      this.label9_1.Text = "label1";
      this.label8_1.AutoSize = true;
      this.label8_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_1.Location = new Point(367, 503);
      this.label8_1.Name = "label8_1";
      this.label8_1.Size = new Size(70, 26);
      this.label8_1.TabIndex = 41;
      this.label8_1.Text = "label1";
      this.label7_1.AutoSize = true;
      this.label7_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_1.Location = new Point(367, 461);
      this.label7_1.Name = "label7_1";
      this.label7_1.Size = new Size(70, 26);
      this.label7_1.TabIndex = 40;
      this.label7_1.Text = "label8";
      this.label6_1.AutoSize = true;
      this.label6_1.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_1.Location = new Point(367, 419);
      this.label6_1.Name = "label6_1";
      this.label6_1.Size = new Size(70, 26);
      this.label6_1.TabIndex = 39;
      this.label6_1.Text = "label1";
      this.label10_2.AutoSize = true;
      this.label10_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_2.Location = new Point(477, 587);
      this.label10_2.Name = "label10_2";
      this.label10_2.Size = new Size(70, 26);
      this.label10_2.TabIndex = 53;
      this.label10_2.Text = "label3";
      this.label9_2.AutoSize = true;
      this.label9_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_2.Location = new Point(477, 545);
      this.label9_2.Name = "label9_2";
      this.label9_2.Size = new Size(70, 26);
      this.label9_2.TabIndex = 52;
      this.label9_2.Text = "label1";
      this.label8_2.AutoSize = true;
      this.label8_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_2.Location = new Point(476, 503);
      this.label8_2.Name = "label8_2";
      this.label8_2.Size = new Size(70, 26);
      this.label8_2.TabIndex = 51;
      this.label8_2.Text = "label1";
      this.label7_2.AutoSize = true;
      this.label7_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_2.Location = new Point(476, 461);
      this.label7_2.Name = "label7_2";
      this.label7_2.Size = new Size(70, 26);
      this.label7_2.TabIndex = 50;
      this.label7_2.Text = "label8";
      this.label6_2.AutoSize = true;
      this.label6_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_2.Location = new Point(476, 419);
      this.label6_2.Name = "label6_2";
      this.label6_2.Size = new Size(70, 26);
      this.label6_2.TabIndex = 49;
      this.label6_2.Text = "label1";
      this.label5_2.AutoSize = true;
      this.label5_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_2.Location = new Point(476, 377);
      this.label5_2.Name = "label5_2";
      this.label5_2.Size = new Size(70, 26);
      this.label5_2.TabIndex = 48;
      this.label5_2.Text = "label4";
      this.label4_2.AutoSize = true;
      this.label4_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_2.Location = new Point(476, 335);
      this.label4_2.Name = "label4_2";
      this.label4_2.Size = new Size(70, 26);
      this.label4_2.TabIndex = 47;
      this.label4_2.Text = "label1";
      this.label3_2.AutoSize = true;
      this.label3_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_2.Location = new Point(476, 293);
      this.label3_2.Name = "label3_2";
      this.label3_2.Size = new Size(70, 26);
      this.label3_2.TabIndex = 46;
      this.label3_2.Text = "label1";
      this.label2_2.AutoSize = true;
      this.label2_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_2.Location = new Point(476, 251);
      this.label2_2.Name = "label2_2";
      this.label2_2.Size = new Size(70, 26);
      this.label2_2.TabIndex = 45;
      this.label2_2.Text = "label1";
      this.label1_2.AutoSize = true;
      this.label1_2.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_2.Location = new Point(476, 209);
      this.label1_2.Name = "label1_2";
      this.label1_2.Size = new Size(70, 26);
      this.label1_2.TabIndex = 44;
      this.label1_2.Text = "label1";
      this.label10_3.AutoSize = true;
      this.label10_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_3.Location = new Point(591, 587);
      this.label10_3.Name = "label10_3";
      this.label10_3.Size = new Size(70, 26);
      this.label10_3.TabIndex = 63;
      this.label10_3.Text = "label3";
      this.label9_3.AutoSize = true;
      this.label9_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_3.Location = new Point(591, 545);
      this.label9_3.Name = "label9_3";
      this.label9_3.Size = new Size(70, 26);
      this.label9_3.TabIndex = 62;
      this.label9_3.Text = "label1";
      this.label8_3.AutoSize = true;
      this.label8_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_3.Location = new Point(590, 503);
      this.label8_3.Name = "label8_3";
      this.label8_3.Size = new Size(70, 26);
      this.label8_3.TabIndex = 61;
      this.label8_3.Text = "label1";
      this.label7_3.AutoSize = true;
      this.label7_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_3.Location = new Point(590, 461);
      this.label7_3.Name = "label7_3";
      this.label7_3.Size = new Size(70, 26);
      this.label7_3.TabIndex = 60;
      this.label7_3.Text = "label8";
      this.label6_3.AutoSize = true;
      this.label6_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_3.Location = new Point(590, 419);
      this.label6_3.Name = "label6_3";
      this.label6_3.Size = new Size(70, 26);
      this.label6_3.TabIndex = 59;
      this.label6_3.Text = "label1";
      this.label5_3.AutoSize = true;
      this.label5_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_3.Location = new Point(590, 377);
      this.label5_3.Name = "label5_3";
      this.label5_3.Size = new Size(70, 26);
      this.label5_3.TabIndex = 58;
      this.label5_3.Text = "label4";
      this.label4_3.AutoSize = true;
      this.label4_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_3.Location = new Point(590, 335);
      this.label4_3.Name = "label4_3";
      this.label4_3.Size = new Size(70, 26);
      this.label4_3.TabIndex = 57;
      this.label4_3.Text = "label1";
      this.label3_3.AutoSize = true;
      this.label3_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_3.Location = new Point(590, 293);
      this.label3_3.Name = "label3_3";
      this.label3_3.Size = new Size(70, 26);
      this.label3_3.TabIndex = 56;
      this.label3_3.Text = "label1";
      this.label2_3.AutoSize = true;
      this.label2_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_3.Location = new Point(590, 251);
      this.label2_3.Name = "label2_3";
      this.label2_3.Size = new Size(70, 26);
      this.label2_3.TabIndex = 55;
      this.label2_3.Text = "label1";
      this.label1_3.AutoSize = true;
      this.label1_3.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_3.Location = new Point(590, 209);
      this.label1_3.Name = "label1_3";
      this.label1_3.Size = new Size(70, 26);
      this.label1_3.TabIndex = 54;
      this.label1_3.Text = "label1";
      this.label10_4.AutoSize = true;
      this.label10_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_4.Location = new Point(710, 587);
      this.label10_4.Name = "label10_4";
      this.label10_4.Size = new Size(70, 26);
      this.label10_4.TabIndex = 73;
      this.label10_4.Text = "label3";
      this.label9_4.AutoSize = true;
      this.label9_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_4.Location = new Point(710, 545);
      this.label9_4.Name = "label9_4";
      this.label9_4.Size = new Size(70, 26);
      this.label9_4.TabIndex = 72;
      this.label9_4.Text = "label1";
      this.label8_4.AutoSize = true;
      this.label8_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_4.Location = new Point(709, 503);
      this.label8_4.Name = "label8_4";
      this.label8_4.Size = new Size(70, 26);
      this.label8_4.TabIndex = 71;
      this.label8_4.Text = "label1";
      this.label7_4.AutoSize = true;
      this.label7_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_4.Location = new Point(709, 461);
      this.label7_4.Name = "label7_4";
      this.label7_4.Size = new Size(70, 26);
      this.label7_4.TabIndex = 70;
      this.label7_4.Text = "label8";
      this.label6_4.AutoSize = true;
      this.label6_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_4.Location = new Point(709, 419);
      this.label6_4.Name = "label6_4";
      this.label6_4.Size = new Size(70, 26);
      this.label6_4.TabIndex = 69;
      this.label6_4.Text = "label1";
      this.label5_4.AutoSize = true;
      this.label5_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_4.Location = new Point(709, 377);
      this.label5_4.Name = "label5_4";
      this.label5_4.Size = new Size(70, 26);
      this.label5_4.TabIndex = 68;
      this.label5_4.Text = "label4";
      this.label4_4.AutoSize = true;
      this.label4_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_4.Location = new Point(709, 335);
      this.label4_4.Name = "label4_4";
      this.label4_4.Size = new Size(70, 26);
      this.label4_4.TabIndex = 67;
      this.label4_4.Text = "label1";
      this.label3_4.AutoSize = true;
      this.label3_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_4.Location = new Point(709, 293);
      this.label3_4.Name = "label3_4";
      this.label3_4.Size = new Size(70, 26);
      this.label3_4.TabIndex = 66;
      this.label3_4.Text = "label1";
      this.label2_4.AutoSize = true;
      this.label2_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_4.Location = new Point(709, 251);
      this.label2_4.Name = "label2_4";
      this.label2_4.Size = new Size(70, 26);
      this.label2_4.TabIndex = 65;
      this.label2_4.Text = "label1";
      this.label1_4.AutoSize = true;
      this.label1_4.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_4.Location = new Point(709, 209);
      this.label1_4.Name = "label1_4";
      this.label1_4.Size = new Size(70, 26);
      this.label1_4.TabIndex = 64;
      this.label1_4.Text = "label1";
      this.label1_silver.AutoSize = true;
      this.label1_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_silver.Location = new Point(909, 209);
      this.label1_silver.Name = "label1_silver";
      this.label1_silver.Size = new Size(36, 26);
      this.label1_silver.TabIndex = 76;
      this.label1_silver.Text = "g1";
      this.label1_gold.AutoSize = true;
      this.label1_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1_gold.Location = new Point(825, 209);
      this.label1_gold.Name = "label1_gold";
      this.label1_gold.Size = new Size(36, 26);
      this.label1_gold.TabIndex = 75;
      this.label1_gold.Text = "g1";
      this.pictureBox1_gold.Image = (Image) Resources.star___gold;
      this.pictureBox1_gold.Location = new Point(867, 199);
      this.pictureBox1_gold.Name = "pictureBox1_gold";
      this.pictureBox1_gold.Size = new Size(36, 36);
      this.pictureBox1_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1_gold.TabIndex = 74;
      this.pictureBox1_gold.TabStop = false;
      this.tradePartnerPicture.Location = new Point(2, 12);
      this.tradePartnerPicture.Name = "tradePartnerPicture";
      this.tradePartnerPicture.Size = new Size(280, 280);
      this.tradePartnerPicture.SizeMode = PictureBoxSizeMode.StretchImage;
      this.tradePartnerPicture.TabIndex = 18;
      this.tradePartnerPicture.TabStop = false;
      this.pictureBox1_silver.Image = (Image) Resources.star_silver;
      this.pictureBox1_silver.Location = new Point(951, 199);
      this.pictureBox1_silver.Name = "pictureBox1_silver";
      this.pictureBox1_silver.Size = new Size(36, 36);
      this.pictureBox1_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox1_silver.TabIndex = 77;
      this.pictureBox1_silver.TabStop = false;
      this.pictureBox2_silver.Image = (Image) Resources.star_silver;
      this.pictureBox2_silver.Location = new Point(951, 241);
      this.pictureBox2_silver.Name = "pictureBox2_silver";
      this.pictureBox2_silver.Size = new Size(36, 36);
      this.pictureBox2_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2_silver.TabIndex = 81;
      this.pictureBox2_silver.TabStop = false;
      this.label2_silver.AutoSize = true;
      this.label2_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_silver.Location = new Point(909, 251);
      this.label2_silver.Name = "label2_silver";
      this.label2_silver.Size = new Size(36, 26);
      this.label2_silver.TabIndex = 80;
      this.label2_silver.Text = "g1";
      this.label2_gold.AutoSize = true;
      this.label2_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2_gold.Location = new Point(825, 251);
      this.label2_gold.Name = "label2_gold";
      this.label2_gold.Size = new Size(36, 26);
      this.label2_gold.TabIndex = 79;
      this.label2_gold.Text = "g1";
      this.pictureBox2_gold.Image = (Image) Resources.star___gold;
      this.pictureBox2_gold.Location = new Point(867, 241);
      this.pictureBox2_gold.Name = "pictureBox2_gold";
      this.pictureBox2_gold.Size = new Size(36, 36);
      this.pictureBox2_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox2_gold.TabIndex = 78;
      this.pictureBox2_gold.TabStop = false;
      this.pictureBox3_silver.Image = (Image) Resources.star_silver;
      this.pictureBox3_silver.Location = new Point(951, 283);
      this.pictureBox3_silver.Name = "pictureBox3_silver";
      this.pictureBox3_silver.Size = new Size(36, 36);
      this.pictureBox3_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox3_silver.TabIndex = 85;
      this.pictureBox3_silver.TabStop = false;
      this.label3_silver.AutoSize = true;
      this.label3_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_silver.Location = new Point(909, 293);
      this.label3_silver.Name = "label3_silver";
      this.label3_silver.Size = new Size(36, 26);
      this.label3_silver.TabIndex = 84;
      this.label3_silver.Text = "g1";
      this.label3_gold.AutoSize = true;
      this.label3_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3_gold.Location = new Point(825, 293);
      this.label3_gold.Name = "label3_gold";
      this.label3_gold.Size = new Size(36, 26);
      this.label3_gold.TabIndex = 83;
      this.label3_gold.Text = "g1";
      this.pictureBox3_gold.Image = (Image) Resources.star___gold;
      this.pictureBox3_gold.Location = new Point(867, 283);
      this.pictureBox3_gold.Name = "pictureBox3_gold";
      this.pictureBox3_gold.Size = new Size(36, 36);
      this.pictureBox3_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox3_gold.TabIndex = 82;
      this.pictureBox3_gold.TabStop = false;
      this.pictureBox4_silver.Image = (Image) Resources.star_silver;
      this.pictureBox4_silver.Location = new Point(951, 325);
      this.pictureBox4_silver.Name = "pictureBox4_silver";
      this.pictureBox4_silver.Size = new Size(36, 36);
      this.pictureBox4_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox4_silver.TabIndex = 89;
      this.pictureBox4_silver.TabStop = false;
      this.label4_silver.AutoSize = true;
      this.label4_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_silver.Location = new Point(909, 335);
      this.label4_silver.Name = "label4_silver";
      this.label4_silver.Size = new Size(36, 26);
      this.label4_silver.TabIndex = 88;
      this.label4_silver.Text = "g1";
      this.label4_gold.AutoSize = true;
      this.label4_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4_gold.Location = new Point(825, 335);
      this.label4_gold.Name = "label4_gold";
      this.label4_gold.Size = new Size(36, 26);
      this.label4_gold.TabIndex = 87;
      this.label4_gold.Text = "g1";
      this.pictureBox4_gold.Image = (Image) Resources.star___gold;
      this.pictureBox4_gold.Location = new Point(867, 325);
      this.pictureBox4_gold.Name = "pictureBox4_gold";
      this.pictureBox4_gold.Size = new Size(36, 36);
      this.pictureBox4_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox4_gold.TabIndex = 86;
      this.pictureBox4_gold.TabStop = false;
      this.pictureBox5_silver.Image = (Image) Resources.star_silver;
      this.pictureBox5_silver.Location = new Point(951, 367);
      this.pictureBox5_silver.Name = "pictureBox5_silver";
      this.pictureBox5_silver.Size = new Size(36, 36);
      this.pictureBox5_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox5_silver.TabIndex = 93;
      this.pictureBox5_silver.TabStop = false;
      this.label5_silver.AutoSize = true;
      this.label5_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_silver.Location = new Point(909, 377);
      this.label5_silver.Name = "label5_silver";
      this.label5_silver.Size = new Size(36, 26);
      this.label5_silver.TabIndex = 92;
      this.label5_silver.Text = "g1";
      this.label5_gold.AutoSize = true;
      this.label5_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5_gold.Location = new Point(825, 377);
      this.label5_gold.Name = "label5_gold";
      this.label5_gold.Size = new Size(36, 26);
      this.label5_gold.TabIndex = 91;
      this.label5_gold.Text = "g1";
      this.pictureBox5_gold.Image = (Image) Resources.star___gold;
      this.pictureBox5_gold.Location = new Point(867, 367);
      this.pictureBox5_gold.Name = "pictureBox5_gold";
      this.pictureBox5_gold.Size = new Size(36, 36);
      this.pictureBox5_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox5_gold.TabIndex = 90;
      this.pictureBox5_gold.TabStop = false;
      this.pictureBox6_silver.Image = (Image) Resources.star_silver;
      this.pictureBox6_silver.Location = new Point(951, 409);
      this.pictureBox6_silver.Name = "pictureBox6_silver";
      this.pictureBox6_silver.Size = new Size(36, 36);
      this.pictureBox6_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox6_silver.TabIndex = 97;
      this.pictureBox6_silver.TabStop = false;
      this.label6_silver.AutoSize = true;
      this.label6_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_silver.Location = new Point(909, 419);
      this.label6_silver.Name = "label6_silver";
      this.label6_silver.Size = new Size(36, 26);
      this.label6_silver.TabIndex = 96;
      this.label6_silver.Text = "g1";
      this.label6_gold.AutoSize = true;
      this.label6_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label6_gold.Location = new Point(825, 419);
      this.label6_gold.Name = "label6_gold";
      this.label6_gold.Size = new Size(36, 26);
      this.label6_gold.TabIndex = 95;
      this.label6_gold.Text = "g1";
      this.pictureBox6_gold.Image = (Image) Resources.star___gold;
      this.pictureBox6_gold.Location = new Point(867, 409);
      this.pictureBox6_gold.Name = "pictureBox6_gold";
      this.pictureBox6_gold.Size = new Size(36, 36);
      this.pictureBox6_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox6_gold.TabIndex = 94;
      this.pictureBox6_gold.TabStop = false;
      this.pictureBox7_silver.Image = (Image) Resources.star_silver;
      this.pictureBox7_silver.Location = new Point(951, 451);
      this.pictureBox7_silver.Name = "pictureBox7_silver";
      this.pictureBox7_silver.Size = new Size(36, 36);
      this.pictureBox7_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox7_silver.TabIndex = 101;
      this.pictureBox7_silver.TabStop = false;
      this.label7_silver.AutoSize = true;
      this.label7_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_silver.Location = new Point(909, 461);
      this.label7_silver.Name = "label7_silver";
      this.label7_silver.Size = new Size(36, 26);
      this.label7_silver.TabIndex = 100;
      this.label7_silver.Text = "g1";
      this.label7_gold.AutoSize = true;
      this.label7_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label7_gold.Location = new Point(825, 461);
      this.label7_gold.Name = "label7_gold";
      this.label7_gold.Size = new Size(36, 26);
      this.label7_gold.TabIndex = 99;
      this.label7_gold.Text = "g1";
      this.pictureBox7_gold.Image = (Image) Resources.star___gold;
      this.pictureBox7_gold.Location = new Point(867, 451);
      this.pictureBox7_gold.Name = "pictureBox7_gold";
      this.pictureBox7_gold.Size = new Size(36, 36);
      this.pictureBox7_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox7_gold.TabIndex = 98;
      this.pictureBox7_gold.TabStop = false;
      this.pictureBox8_silver.Image = (Image) Resources.star_silver;
      this.pictureBox8_silver.Location = new Point(951, 493);
      this.pictureBox8_silver.Name = "pictureBox8_silver";
      this.pictureBox8_silver.Size = new Size(36, 36);
      this.pictureBox8_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox8_silver.TabIndex = 105;
      this.pictureBox8_silver.TabStop = false;
      this.label8_silver.AutoSize = true;
      this.label8_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_silver.Location = new Point(909, 503);
      this.label8_silver.Name = "label8_silver";
      this.label8_silver.Size = new Size(36, 26);
      this.label8_silver.TabIndex = 104;
      this.label8_silver.Text = "g1";
      this.label8_gold.AutoSize = true;
      this.label8_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label8_gold.Location = new Point(825, 503);
      this.label8_gold.Name = "label8_gold";
      this.label8_gold.Size = new Size(36, 26);
      this.label8_gold.TabIndex = 103;
      this.label8_gold.Text = "g1";
      this.pictureBox8_gold.Image = (Image) Resources.star___gold;
      this.pictureBox8_gold.Location = new Point(867, 493);
      this.pictureBox8_gold.Name = "pictureBox8_gold";
      this.pictureBox8_gold.Size = new Size(36, 36);
      this.pictureBox8_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox8_gold.TabIndex = 102;
      this.pictureBox8_gold.TabStop = false;
      this.pictureBox9_silver.Image = (Image) Resources.star_silver;
      this.pictureBox9_silver.Location = new Point(951, 535);
      this.pictureBox9_silver.Name = "pictureBox9_silver";
      this.pictureBox9_silver.Size = new Size(36, 36);
      this.pictureBox9_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox9_silver.TabIndex = 109;
      this.pictureBox9_silver.TabStop = false;
      this.label9_silver.AutoSize = true;
      this.label9_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_silver.Location = new Point(909, 545);
      this.label9_silver.Name = "label9_silver";
      this.label9_silver.Size = new Size(36, 26);
      this.label9_silver.TabIndex = 108;
      this.label9_silver.Text = "g1";
      this.label9_gold.AutoSize = true;
      this.label9_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9_gold.Location = new Point(825, 545);
      this.label9_gold.Name = "label9_gold";
      this.label9_gold.Size = new Size(36, 26);
      this.label9_gold.TabIndex = 107;
      this.label9_gold.Text = "g1";
      this.pictureBox9_gold.Image = (Image) Resources.star___gold;
      this.pictureBox9_gold.Location = new Point(867, 535);
      this.pictureBox9_gold.Name = "pictureBox9_gold";
      this.pictureBox9_gold.Size = new Size(36, 36);
      this.pictureBox9_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox9_gold.TabIndex = 106;
      this.pictureBox9_gold.TabStop = false;
      this.pictureBox10_silver.Image = (Image) Resources.star_silver;
      this.pictureBox10_silver.Location = new Point(951, 577);
      this.pictureBox10_silver.Name = "pictureBox10_silver";
      this.pictureBox10_silver.Size = new Size(36, 36);
      this.pictureBox10_silver.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox10_silver.TabIndex = 113;
      this.pictureBox10_silver.TabStop = false;
      this.label10_silver.AutoSize = true;
      this.label10_silver.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_silver.Location = new Point(909, 587);
      this.label10_silver.Name = "label10_silver";
      this.label10_silver.Size = new Size(36, 26);
      this.label10_silver.TabIndex = 112;
      this.label10_silver.Text = "g1";
      this.label10_gold.AutoSize = true;
      this.label10_gold.Font = new Font("Microsoft Sans Serif", 16f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label10_gold.Location = new Point(825, 587);
      this.label10_gold.Name = "label10_gold";
      this.label10_gold.Size = new Size(36, 26);
      this.label10_gold.TabIndex = 111;
      this.label10_gold.Text = "g1";
      this.pictureBox10_gold.Image = (Image) Resources.star___gold;
      this.pictureBox10_gold.Location = new Point(867, 577);
      this.pictureBox10_gold.Name = "pictureBox10_gold";
      this.pictureBox10_gold.Size = new Size(36, 36);
      this.pictureBox10_gold.SizeMode = PictureBoxSizeMode.StretchImage;
      this.pictureBox10_gold.TabIndex = 110;
      this.pictureBox10_gold.TabStop = false;
      this.battlefieldPicture.Image = (Image) Resources.AlienBattlefield01;
      this.battlefieldPicture.Location = new Point(2, 364);
      this.battlefieldPicture.Name = "battlefieldPicture";
      this.battlefieldPicture.Size = new Size(1050, 276);
      this.battlefieldPicture.SizeMode = PictureBoxSizeMode.StretchImage;
      this.battlefieldPicture.TabIndex = 114;
      this.battlefieldPicture.TabStop = false;
      this.generalsInBattleTextBox.Location = new Point(12, 409);
      this.generalsInBattleTextBox.Multiline = true;
      this.generalsInBattleTextBox.Name = "generalsInBattleTextBox";
      this.generalsInBattleTextBox.ReadOnly = true;
      this.generalsInBattleTextBox.ScrollBars = ScrollBars.Vertical;
      this.generalsInBattleTextBox.Size = new Size(199, 75);
      this.generalsInBattleTextBox.TabIndex = 115;
      this.generalAdviceTextBox.Location = new Point(12, 503);
      this.generalAdviceTextBox.Multiline = true;
      this.generalAdviceTextBox.Name = "generalAdviceTextBox";
      this.generalAdviceTextBox.ReadOnly = true;
      this.generalAdviceTextBox.ScrollBars = ScrollBars.Vertical;
      this.generalAdviceTextBox.Size = new Size(199, 110);
      this.generalAdviceTextBox.TabIndex = 116;
      this.seekAdviceButton.Location = new Point(13, 377);
      this.seekAdviceButton.Name = "seekAdviceButton";
      this.seekAdviceButton.Size = new Size(105, 23);
      this.seekAdviceButton.TabIndex = 117;
      this.seekAdviceButton.Text = "Seek Advice";
      this.seekAdviceToolTip.SetToolTip((Control) this.seekAdviceButton, "This will get the advice of a general.\r\nThe general will not be available for\r\nthe remainder of the battle.");
      this.seekAdviceButton.UseVisualStyleBackColor = true;
      this.seekAdviceButton.Click += new EventHandler(this.seekAdviceButton_Click);
      this.battleProgressBar.Location = new Point(373, 13);
      this.battleProgressBar.Name = "battleProgressBar";
      this.battleProgressBar.Size = new Size(431, 23);
      this.battleProgressBar.Style = ProgressBarStyle.Continuous;
      this.battleProgressBar.TabIndex = 118;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackgroundImageLayout = ImageLayout.Zoom;
      this.ClientSize = new Size(1049, 639);
      this.Controls.Add((Control) this.battleProgressBar);
      this.Controls.Add((Control) this.seekAdviceButton);
      this.Controls.Add((Control) this.generalAdviceTextBox);
      this.Controls.Add((Control) this.generalsInBattleTextBox);
      this.Controls.Add((Control) this.pictureBox10_silver);
      this.Controls.Add((Control) this.label10_silver);
      this.Controls.Add((Control) this.label10_gold);
      this.Controls.Add((Control) this.pictureBox10_gold);
      this.Controls.Add((Control) this.pictureBox9_silver);
      this.Controls.Add((Control) this.label9_silver);
      this.Controls.Add((Control) this.label9_gold);
      this.Controls.Add((Control) this.pictureBox9_gold);
      this.Controls.Add((Control) this.pictureBox8_silver);
      this.Controls.Add((Control) this.label8_silver);
      this.Controls.Add((Control) this.label8_gold);
      this.Controls.Add((Control) this.pictureBox8_gold);
      this.Controls.Add((Control) this.pictureBox7_silver);
      this.Controls.Add((Control) this.label7_silver);
      this.Controls.Add((Control) this.label7_gold);
      this.Controls.Add((Control) this.pictureBox7_gold);
      this.Controls.Add((Control) this.pictureBox6_silver);
      this.Controls.Add((Control) this.label6_silver);
      this.Controls.Add((Control) this.label6_gold);
      this.Controls.Add((Control) this.pictureBox6_gold);
      this.Controls.Add((Control) this.pictureBox5_silver);
      this.Controls.Add((Control) this.label5_silver);
      this.Controls.Add((Control) this.label5_gold);
      this.Controls.Add((Control) this.pictureBox5_gold);
      this.Controls.Add((Control) this.pictureBox4_silver);
      this.Controls.Add((Control) this.label4_silver);
      this.Controls.Add((Control) this.label4_gold);
      this.Controls.Add((Control) this.pictureBox4_gold);
      this.Controls.Add((Control) this.pictureBox3_silver);
      this.Controls.Add((Control) this.label3_silver);
      this.Controls.Add((Control) this.label3_gold);
      this.Controls.Add((Control) this.pictureBox3_gold);
      this.Controls.Add((Control) this.pictureBox2_silver);
      this.Controls.Add((Control) this.label2_silver);
      this.Controls.Add((Control) this.label2_gold);
      this.Controls.Add((Control) this.pictureBox2_gold);
      this.Controls.Add((Control) this.pictureBox1_silver);
      this.Controls.Add((Control) this.label1_silver);
      this.Controls.Add((Control) this.label1_gold);
      this.Controls.Add((Control) this.pictureBox1_gold);
      this.Controls.Add((Control) this.label10_4);
      this.Controls.Add((Control) this.label9_4);
      this.Controls.Add((Control) this.label8_4);
      this.Controls.Add((Control) this.label7_4);
      this.Controls.Add((Control) this.label6_4);
      this.Controls.Add((Control) this.label5_4);
      this.Controls.Add((Control) this.label4_4);
      this.Controls.Add((Control) this.label3_4);
      this.Controls.Add((Control) this.label2_4);
      this.Controls.Add((Control) this.label1_4);
      this.Controls.Add((Control) this.label10_3);
      this.Controls.Add((Control) this.label9_3);
      this.Controls.Add((Control) this.label8_3);
      this.Controls.Add((Control) this.label7_3);
      this.Controls.Add((Control) this.label6_3);
      this.Controls.Add((Control) this.label5_3);
      this.Controls.Add((Control) this.label4_3);
      this.Controls.Add((Control) this.label3_3);
      this.Controls.Add((Control) this.label2_3);
      this.Controls.Add((Control) this.label1_3);
      this.Controls.Add((Control) this.label10_2);
      this.Controls.Add((Control) this.label9_2);
      this.Controls.Add((Control) this.label8_2);
      this.Controls.Add((Control) this.label7_2);
      this.Controls.Add((Control) this.label6_2);
      this.Controls.Add((Control) this.label5_2);
      this.Controls.Add((Control) this.label4_2);
      this.Controls.Add((Control) this.label3_2);
      this.Controls.Add((Control) this.label2_2);
      this.Controls.Add((Control) this.label1_2);
      this.Controls.Add((Control) this.label10_1);
      this.Controls.Add((Control) this.label9_1);
      this.Controls.Add((Control) this.label8_1);
      this.Controls.Add((Control) this.label7_1);
      this.Controls.Add((Control) this.label6_1);
      this.Controls.Add((Control) this.label5_1);
      this.Controls.Add((Control) this.label4_1);
      this.Controls.Add((Control) this.label3_1);
      this.Controls.Add((Control) this.label2_1);
      this.Controls.Add((Control) this.label1_1);
      this.Controls.Add((Control) this.actualEnemyTacticReserveLabel);
      this.Controls.Add((Control) this.actualEnemyTacticRightLabel);
      this.Controls.Add((Control) this.actualEnemyTacticMainLabel);
      this.Controls.Add((Control) this.reserveLabel);
      this.Controls.Add((Control) this.rightlabel);
      this.Controls.Add((Control) this.mainBodyLabel);
      this.Controls.Add((Control) this.guessButton);
      this.Controls.Add((Control) this.reservesComboBox);
      this.Controls.Add((Control) this.rightFlankComboBox);
      this.Controls.Add((Control) this.mainBodyComboBox);
      this.Controls.Add((Control) this.leftFlankComboBox);
      this.Controls.Add((Control) this.actualEnemyTacticLeftLabel);
      this.Controls.Add((Control) this.leftlabel);
      this.Controls.Add((Control) this.EnemyFormationLabel);
      this.Controls.Add((Control) this.tradePartnerPicture);
      this.Controls.Add((Control) this.planetNamelabel);
      this.Controls.Add((Control) this.battlefieldPicture);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (InvasionCommandForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Invasion Command";
      this.FormClosing += new FormClosingEventHandler(this.InvasionCommandForm_FormClosing);
      ((ISupportInitialize) this.pictureBox1_gold).EndInit();
      ((ISupportInitialize) this.tradePartnerPicture).EndInit();
      ((ISupportInitialize) this.pictureBox1_silver).EndInit();
      ((ISupportInitialize) this.pictureBox2_silver).EndInit();
      ((ISupportInitialize) this.pictureBox2_gold).EndInit();
      ((ISupportInitialize) this.pictureBox3_silver).EndInit();
      ((ISupportInitialize) this.pictureBox3_gold).EndInit();
      ((ISupportInitialize) this.pictureBox4_silver).EndInit();
      ((ISupportInitialize) this.pictureBox4_gold).EndInit();
      ((ISupportInitialize) this.pictureBox5_silver).EndInit();
      ((ISupportInitialize) this.pictureBox5_gold).EndInit();
      ((ISupportInitialize) this.pictureBox6_silver).EndInit();
      ((ISupportInitialize) this.pictureBox6_gold).EndInit();
      ((ISupportInitialize) this.pictureBox7_silver).EndInit();
      ((ISupportInitialize) this.pictureBox7_gold).EndInit();
      ((ISupportInitialize) this.pictureBox8_silver).EndInit();
      ((ISupportInitialize) this.pictureBox8_gold).EndInit();
      ((ISupportInitialize) this.pictureBox9_silver).EndInit();
      ((ISupportInitialize) this.pictureBox9_gold).EndInit();
      ((ISupportInitialize) this.pictureBox10_silver).EndInit();
      ((ISupportInitialize) this.pictureBox10_gold).EndInit();
      ((ISupportInitialize) this.battlefieldPicture).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
