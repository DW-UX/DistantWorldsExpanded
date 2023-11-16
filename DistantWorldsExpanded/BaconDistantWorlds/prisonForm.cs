// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.prisonForm
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

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
  public class prisonForm : Form
  {
    private IContainer components = (IContainer) null;
    private DataGridView prisonersDataGridView;
    private Button ransomButton;
    private Button closeButton;
    private DataGridViewTextBoxColumn spyNameColumn;
    private DataGridViewTextBoxColumn spyNationalityColumn;
    private DataGridViewTextBoxColumn spyLocationColumn;
    private DataGridViewTextBoxColumn ransomColumn;
    private Button executeButton;

    public prisonForm(Main main)
    {
      try
      {
        this.InitializeComponent();
        List<Character> characterList1 = (List<Character>) null;
        if (main._Game.PlayerEmpire.Capital != null && main._Game.PlayerEmpire.Capital.BaconValues != null && main._Game.PlayerEmpire.Capital.BaconValues.ContainsKey("capturedSpies"))
          characterList1 = (List<Character>) main._Game.PlayerEmpire.Capital.BaconValues["capturedSpies"];
        if (main._Game.PlayerEmpire.PirateEmpireBaseHabitat != null && main._Game.PlayerEmpire.BuiltObjects[0].BaconValues != null && main._Game.PlayerEmpire.BuiltObjects[0].BaconValues.ContainsKey("capturedSpies"))
          characterList1 = (List<Character>) main._Game.PlayerEmpire.BuiltObjects[0].BaconValues["capturedSpies"];
        if (characterList1 == null)
          return;
        int index = 0;
        List<Character> characterList2 = new List<Character>();
        foreach (Character character in characterList1)
        {
          if (character.Mission == null)
            characterList2.Add(character);
          else if (character.Mission.TargetEmpire == null)
            characterList2.Add(character);
          else if (character.Mission.TargetEmpire.PirateEmpireBaseHabitat == null && character.Mission.TargetEmpire.Capital == null)
            characterList2.Add(character);
          else if (character.Mission.TargetEmpire.PirateEmpireBaseHabitat != null && character.Mission.TargetEmpire.BuiltObjects.Count < 1)
            characterList2.Add(character);
        }
        if (characterList2.Count > 0)
        {
          foreach (Character character in characterList2)
            characterList1.Remove(character);
        }
        foreach (Character character in characterList1)
        {
          DataGridViewRow dataGridViewRow = new DataGridViewRow();
          object[] objArray = new object[4]
          {
            (object) character.Name,
            (object) character.Empire.Name,
            (object) (character.Mission.TargetEmpire.PirateEmpireBaseHabitat != null ? character.Mission.TargetEmpire.BuiltObjects[0].Name : character.Mission.TargetEmpire.Capital.Name),
            (object) BaconCharacter.GetCharacterValue(character)
          };
          this.prisonersDataGridView.Rows.Add(dataGridViewRow);
          this.prisonersDataGridView.Rows[index].SetValues(objArray);
          ++index;
        }
        this.Show();
      }
      catch (Exception ex)
      {
        BaconMain.prisonFormOpen = false;
      }
      finally
      {
        BaconMain.prisonFormOpen = false;
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      BaconMain.prisonFormOpen = false;
      this.Close();
    }

    private void ransomButton_Click(object sender, EventArgs e)
    {
      DataGridViewRow selectedRow = (DataGridViewRow) null;
      if (BaconBuiltObject.myMain == null)
        return;
      if (this.prisonersDataGridView.SelectedRows != null && this.prisonersDataGridView.SelectedRows.Count > 0)
        selectedRow = this.prisonersDataGridView.SelectedRows[0];
      else if (this.prisonersDataGridView.SelectedCells != null && this.prisonersDataGridView.SelectedCells.Count > 0)
        selectedRow = this.prisonersDataGridView.Rows[this.prisonersDataGridView.SelectedCells[0].RowIndex];
      if (selectedRow == null)
      {
        BaconMain.prisonFormOpen = false;
      }
      else
      {
        List<Character> source = (List<Character>) null;
        if (BaconBuiltObject.myMain._Game.PlayerEmpire.Capital != null && BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues != null && BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues.ContainsKey("capturedSpies"))
          source = (List<Character>) BaconBuiltObject.myMain._Game.PlayerEmpire.Capital.BaconValues["capturedSpies"];
        if (BaconBuiltObject.myMain._Game.PlayerEmpire.PirateEmpireBaseHabitat != null && BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues != null && BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues.ContainsKey("capturedSpies"))
          source = (List<Character>) BaconBuiltObject.myMain._Game.PlayerEmpire.BuiltObjects[0].BaconValues["capturedSpies"];
        Character character1 = source.FirstOrDefault<Character>((Func<Character, bool>) (x => x.Name == selectedRow.Cells[0].Value.ToString()));
        if (character1 == null)
          return;
        int characterValue = BaconCharacter.GetCharacterValue(character1);
        Empire empire;
        string description;
        if (character1.Empire == BaconBuiltObject.myMain._Game.PlayerEmpire)
        {
          if (BaconBuiltObject.myMain._Game.PlayerEmpire.Name.Contains("Romulan"))
            characterValue /= 100;
          empire = BaconCharacter.GetSpyTargetEmpire(character1);
          description = character1.Name + " has been released for a payment of " + characterValue.ToString() + " credits.";
        }
        else
        {
          empire = BaconBuiltObject.myMain._Game.PlayerEmpire;
          description = character1.Empire.Name + " have paid " + characterValue.ToString() + " credits for their agent.";
        }
        try
        {
          if (!prisonForm.EmpireHasEnoughMoneyToRansomSpyBack(character1.Empire, characterValue))
          {
            BaconMain.prisonFormOpen = false;
            this.Close();
            BaconBuiltObject.ShowMessageBox(BaconBuiltObject.myMain, "You do not have enough money to ransom your spy back." + Environment.NewLine + "Maybe you should take out a loan.", "Financial Status Report");
          }
          else
          {
            character1.Empire.StateMoney -= Math.Min(Math.Max(character1.Empire.StateMoney, 0.0), (double) characterValue);
            empire.StateMoney += (double) characterValue;
            if (character1.Empire.Characters == null)
              character1.Empire.Characters = new CharacterList();
            character1.Empire.Characters.Add(character1);
            if (character1.Empire.Capital != null)
            {
              if (character1.Empire.Capital.Characters == null)
                character1.Empire.Capital.Characters = new CharacterList();
              character1.Empire.Capital.Characters.Add(character1);
              character1.Location = (StellarObject) character1.Empire.Capital;
            }
            else if (character1.Empire.BuiltObjects.Any<BuiltObject>())
              character1.Location = (StellarObject) character1.Empire.BuiltObjects[0];
            source.Remove(character1);
            IntelligenceMission intelligenceMission = new IntelligenceMission(character1.Empire, character1, BaconBuiltObject.myMain._Game.Galaxy.CurrentStarDate)
            {
              TimeLength = (long) (Galaxy.RealSecondsInGalacticYear * 1000 / 4)
            };
            character1.Mission = intelligenceMission;
            BaconBuiltObject.myMain._Game.PlayerEmpire.SendMessageToEmpire(BaconBuiltObject.myMain._Game.PlayerEmpire, EmpireMessageType.Undefined, (object) null, description, Point.Empty, "ransom");
            CharacterEvent character2 = BaconCharacter.AddEventToCharacter("Ransomed", character1.Name + " ransomed for " + characterValue.ToString() + " credits.", character1);
            character1.EventHistory.Add(character2);
          }
        }
        catch (Exception ex)
        {
        }
        finally
        {
          BaconMain.prisonFormOpen = false;
          this.Close();
        }
      }
    }

    public static bool EmpireHasEnoughMoneyToRansomSpyBack(Empire empire, int ransom) => empire != BaconBuiltObject.myMain._Game.PlayerEmpire || empire.StateMoney >= (double) ransom;

    private void executeButton_Click(object sender, EventArgs e)
    {
      BaconMain.prisonFormOpen = false;
      this.Close();
    }

    private void prisonForm_FormClosed(object sender, FormClosedEventArgs e) => BaconMain.prisonFormOpen = false;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.prisonersDataGridView = new DataGridView();
      this.spyNameColumn = new DataGridViewTextBoxColumn();
      this.spyNationalityColumn = new DataGridViewTextBoxColumn();
      this.spyLocationColumn = new DataGridViewTextBoxColumn();
      this.ransomColumn = new DataGridViewTextBoxColumn();
      this.ransomButton = new Button();
      this.closeButton = new Button();
      this.executeButton = new Button();
      ((ISupportInitialize) this.prisonersDataGridView).BeginInit();
      this.SuspendLayout();
      this.prisonersDataGridView.AllowUserToAddRows = false;
      this.prisonersDataGridView.AllowUserToDeleteRows = false;
      this.prisonersDataGridView.AllowUserToResizeRows = false;
      this.prisonersDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.prisonersDataGridView.Columns.AddRange((DataGridViewColumn) this.spyNameColumn, (DataGridViewColumn) this.spyNationalityColumn, (DataGridViewColumn) this.spyLocationColumn, (DataGridViewColumn) this.ransomColumn);
      this.prisonersDataGridView.Location = new Point(30, 67);
      this.prisonersDataGridView.Name = "prisonersDataGridView";
      this.prisonersDataGridView.Size = new Size(600, 250);
      this.prisonersDataGridView.TabIndex = 0;
      this.spyNameColumn.HeaderText = "Name";
      this.spyNameColumn.Name = "spyNameColumn";
      this.spyNameColumn.ReadOnly = true;
      this.spyNameColumn.Width = 150;
      this.spyNationalityColumn.HeaderText = "Empire";
      this.spyNationalityColumn.Name = "spyNationalityColumn";
      this.spyNationalityColumn.ReadOnly = true;
      this.spyNationalityColumn.Width = 150;
      this.spyLocationColumn.HeaderText = "Location";
      this.spyLocationColumn.Name = "spyLocationColumn";
      this.spyLocationColumn.ReadOnly = true;
      this.spyLocationColumn.Width = 150;
      this.ransomColumn.HeaderText = "Ransom";
      this.ransomColumn.Name = "ransomColumn";
      this.ransomColumn.ReadOnly = true;
      this.ransomColumn.Width = 150;
      this.ransomButton.Location = new Point(178, 332);
      this.ransomButton.Name = "ransomButton";
      this.ransomButton.Size = new Size(75, 23);
      this.ransomButton.TabIndex = 1;
      this.ransomButton.Text = "Ransom";
      this.ransomButton.UseVisualStyleBackColor = true;
      this.ransomButton.Click += new EventHandler(this.ransomButton_Click);
      this.closeButton.Location = new Point(388, 332);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new Size(75, 23);
      this.closeButton.TabIndex = 2;
      this.closeButton.Text = "Close";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new EventHandler(this.closeButton_Click);
      this.executeButton.Location = new Point(277, 332);
      this.executeButton.Name = "executeButton";
      this.executeButton.Size = new Size(75, 23);
      this.executeButton.TabIndex = 3;
      this.executeButton.Text = "Execute";
      this.executeButton.UseVisualStyleBackColor = true;
      this.executeButton.Visible = false;
      this.executeButton.Click += new EventHandler(this.executeButton_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(673, 366);
      this.Controls.Add((Control) this.executeButton);
      this.Controls.Add((Control) this.closeButton);
      this.Controls.Add((Control) this.ransomButton);
      this.Controls.Add((Control) this.prisonersDataGridView);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (prisonForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Prisoners";
      this.FormClosed += new FormClosedEventHandler(this.prisonForm_FormClosed);
      ((ISupportInitialize) this.prisonersDataGridView).EndInit();
      this.ResumeLayout(false);
    }
  }
}
