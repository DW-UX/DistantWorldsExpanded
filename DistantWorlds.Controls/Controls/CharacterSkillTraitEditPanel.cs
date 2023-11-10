// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterSkillTraitEditPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class CharacterSkillTraitEditPanel : UserControl
  {
    private IContainer components;
    private CharacterSkillTypeDropDown cmbSkill1;
    private CharacterTraitTypeDropDown cmbTrait1;
    private NumericUpDown numSkill1;
    private SmoothLabel lblSkill1;
    private SmoothLabel lblSkill2;
    private NumericUpDown numSkill2;
    private CharacterSkillTypeDropDown cmbSkill2;
    private SmoothLabel lblSkill3;
    private NumericUpDown numSkill3;
    private CharacterSkillTypeDropDown cmbSkill3;
    private SmoothLabel lblSkill4;
    private NumericUpDown numSkill4;
    private CharacterSkillTypeDropDown cmbSkill4;
    private SmoothLabel lblTrait1;
    private SmoothLabel lblTrait2;
    private CharacterTraitTypeDropDown cmbTrait2;
    private SmoothLabel lblTrait3;
    private CharacterTraitTypeDropDown cmbTrait3;
    private SmoothLabel lblTrait4;
    private CharacterTraitTypeDropDown cmbTrait4;
    private CheckBox chkBonusesKnown;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.numSkill1 = new NumericUpDown();
      this.numSkill2 = new NumericUpDown();
      this.numSkill3 = new NumericUpDown();
      this.numSkill4 = new NumericUpDown();
      this.lblTrait4 = new SmoothLabel();
      this.cmbTrait4 = new CharacterTraitTypeDropDown();
      this.lblTrait3 = new SmoothLabel();
      this.cmbTrait3 = new CharacterTraitTypeDropDown();
      this.lblTrait2 = new SmoothLabel();
      this.cmbTrait2 = new CharacterTraitTypeDropDown();
      this.lblTrait1 = new SmoothLabel();
      this.lblSkill4 = new SmoothLabel();
      this.cmbSkill4 = new CharacterSkillTypeDropDown();
      this.lblSkill3 = new SmoothLabel();
      this.cmbSkill3 = new CharacterSkillTypeDropDown();
      this.lblSkill2 = new SmoothLabel();
      this.cmbSkill2 = new CharacterSkillTypeDropDown();
      this.lblSkill1 = new SmoothLabel();
      this.cmbTrait1 = new CharacterTraitTypeDropDown();
      this.cmbSkill1 = new CharacterSkillTypeDropDown();
      this.chkBonusesKnown = new CheckBox();
      this.numSkill1.BeginInit();
      this.numSkill2.BeginInit();
      this.numSkill3.BeginInit();
      this.numSkill4.BeginInit();
      this.SuspendLayout();
      this.numSkill1.BackColor = Color.FromArgb(48, 48, 64);
      this.numSkill1.ForeColor = Color.FromArgb(170, 170, 170);
      this.numSkill1.Location = new Point(276, 41);
      this.numSkill1.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.numSkill1.Name = "numSkill1";
      this.numSkill1.Size = new Size(60, 20);
      this.numSkill1.TabIndex = 2;
      this.numSkill2.BackColor = Color.FromArgb(48, 48, 64);
      this.numSkill2.ForeColor = Color.FromArgb(170, 170, 170);
      this.numSkill2.Location = new Point(276, 69);
      this.numSkill2.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.numSkill2.Name = "numSkill2";
      this.numSkill2.Size = new Size(60, 20);
      this.numSkill2.TabIndex = 5;
      this.numSkill3.BackColor = Color.FromArgb(48, 48, 64);
      this.numSkill3.ForeColor = Color.FromArgb(170, 170, 170);
      this.numSkill3.Location = new Point(276, 97);
      this.numSkill3.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.numSkill3.Name = "numSkill3";
      this.numSkill3.Size = new Size(60, 20);
      this.numSkill3.TabIndex = 8;
      this.numSkill4.BackColor = Color.FromArgb(48, 48, 64);
      this.numSkill4.ForeColor = Color.FromArgb(170, 170, 170);
      this.numSkill4.Location = new Point(276, 125);
      this.numSkill4.Minimum = new Decimal(new int[4]
      {
        100,
        0,
        0,
        int.MinValue
      });
      this.numSkill4.Name = "numSkill4";
      this.numSkill4.Size = new Size(60, 20);
      this.numSkill4.TabIndex = 11;
      this.lblTrait4.AutoSize = true;
      this.lblTrait4.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTrait4.Location = new Point(13, 257);
      this.lblTrait4.Name = "lblTrait4";
      this.lblTrait4.Size = new Size(37, 13);
      this.lblTrait4.TabIndex = 19;
      this.lblTrait4.Text = "Trait 4";
      this.cmbTrait4.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTrait4.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbTrait4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTrait4.FlatStyle = FlatStyle.Popup;
      this.cmbTrait4.Font = new Font("Verdana", 8.25f);
      this.cmbTrait4.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTrait4.FormattingEnabled = true;
      this.cmbTrait4.Location = new Point(75, 254);
      this.cmbTrait4.Name = "cmbTrait4";
      this.cmbTrait4.Size = new Size(180, 22);
      this.cmbTrait4.TabIndex = 18;
      this.lblTrait3.AutoSize = true;
      this.lblTrait3.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTrait3.Location = new Point(13, 229);
      this.lblTrait3.Name = "lblTrait3";
      this.lblTrait3.Size = new Size(37, 13);
      this.lblTrait3.TabIndex = 17;
      this.lblTrait3.Text = "Trait 3";
      this.cmbTrait3.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTrait3.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbTrait3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTrait3.FlatStyle = FlatStyle.Popup;
      this.cmbTrait3.Font = new Font("Verdana", 8.25f);
      this.cmbTrait3.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTrait3.FormattingEnabled = true;
      this.cmbTrait3.Location = new Point(75, 226);
      this.cmbTrait3.Name = "cmbTrait3";
      this.cmbTrait3.Size = new Size(180, 22);
      this.cmbTrait3.TabIndex = 16;
      this.lblTrait2.AutoSize = true;
      this.lblTrait2.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTrait2.Location = new Point(13, 201);
      this.lblTrait2.Name = "lblTrait2";
      this.lblTrait2.Size = new Size(37, 13);
      this.lblTrait2.TabIndex = 15;
      this.lblTrait2.Text = "Trait 2";
      this.cmbTrait2.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTrait2.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbTrait2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTrait2.FlatStyle = FlatStyle.Popup;
      this.cmbTrait2.Font = new Font("Verdana", 8.25f);
      this.cmbTrait2.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTrait2.FormattingEnabled = true;
      this.cmbTrait2.Location = new Point(75, 198);
      this.cmbTrait2.Name = "cmbTrait2";
      this.cmbTrait2.Size = new Size(180, 22);
      this.cmbTrait2.TabIndex = 14;
      this.lblTrait1.AutoSize = true;
      this.lblTrait1.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblTrait1.Location = new Point(13, 173);
      this.lblTrait1.Name = "lblTrait1";
      this.lblTrait1.Size = new Size(37, 13);
      this.lblTrait1.TabIndex = 13;
      this.lblTrait1.Text = "Trait 1";
      this.lblSkill4.AutoSize = true;
      this.lblSkill4.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblSkill4.Location = new Point(13, (int) sbyte.MaxValue);
      this.lblSkill4.Name = "lblSkill4";
      this.lblSkill4.Size = new Size(35, 13);
      this.lblSkill4.TabIndex = 12;
      this.lblSkill4.Text = "Skill 4";
      this.cmbSkill4.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbSkill4.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSkill4.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkill4.FlatStyle = FlatStyle.Popup;
      this.cmbSkill4.Font = new Font("Verdana", 8.25f);
      this.cmbSkill4.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbSkill4.FormattingEnabled = true;
      this.cmbSkill4.Location = new Point(75, 123);
      this.cmbSkill4.Name = "cmbSkill4";
      this.cmbSkill4.Size = new Size(180, 22);
      this.cmbSkill4.TabIndex = 10;
      this.lblSkill3.AutoSize = true;
      this.lblSkill3.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblSkill3.Location = new Point(13, 99);
      this.lblSkill3.Name = "lblSkill3";
      this.lblSkill3.Size = new Size(35, 13);
      this.lblSkill3.TabIndex = 9;
      this.lblSkill3.Text = "Skill 3";
      this.cmbSkill3.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbSkill3.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSkill3.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkill3.FlatStyle = FlatStyle.Popup;
      this.cmbSkill3.Font = new Font("Verdana", 8.25f);
      this.cmbSkill3.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbSkill3.FormattingEnabled = true;
      this.cmbSkill3.Location = new Point(75, 95);
      this.cmbSkill3.Name = "cmbSkill3";
      this.cmbSkill3.Size = new Size(180, 22);
      this.cmbSkill3.TabIndex = 7;
      this.lblSkill2.AutoSize = true;
      this.lblSkill2.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblSkill2.Location = new Point(13, 71);
      this.lblSkill2.Name = "lblSkill2";
      this.lblSkill2.Size = new Size(35, 13);
      this.lblSkill2.TabIndex = 6;
      this.lblSkill2.Text = "Skill 2";
      this.cmbSkill2.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbSkill2.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSkill2.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkill2.FlatStyle = FlatStyle.Popup;
      this.cmbSkill2.Font = new Font("Verdana", 8.25f);
      this.cmbSkill2.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbSkill2.FormattingEnabled = true;
      this.cmbSkill2.Location = new Point(75, 67);
      this.cmbSkill2.Name = "cmbSkill2";
      this.cmbSkill2.Size = new Size(180, 22);
      this.cmbSkill2.TabIndex = 4;
      this.lblSkill1.AutoSize = true;
      this.lblSkill1.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblSkill1.Location = new Point(13, 43);
      this.lblSkill1.Name = "lblSkill1";
      this.lblSkill1.Size = new Size(35, 13);
      this.lblSkill1.TabIndex = 3;
      this.lblSkill1.Text = "Skill 1";
      this.cmbTrait1.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbTrait1.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbTrait1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbTrait1.FlatStyle = FlatStyle.Popup;
      this.cmbTrait1.Font = new Font("Verdana", 8.25f);
      this.cmbTrait1.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbTrait1.FormattingEnabled = true;
      this.cmbTrait1.Location = new Point(75, 170);
      this.cmbTrait1.Name = "cmbTrait1";
      this.cmbTrait1.Size = new Size(180, 22);
      this.cmbTrait1.TabIndex = 1;
      this.cmbSkill1.BackColor = Color.FromArgb(48, 48, 64);
      this.cmbSkill1.DrawMode = DrawMode.OwnerDrawFixed;
      this.cmbSkill1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.cmbSkill1.FlatStyle = FlatStyle.Popup;
      this.cmbSkill1.Font = new Font("Verdana", 8.25f);
      this.cmbSkill1.ForeColor = Color.FromArgb(170, 170, 170);
      this.cmbSkill1.FormattingEnabled = true;
      this.cmbSkill1.Location = new Point(75, 39);
      this.cmbSkill1.Name = "cmbSkill1";
      this.cmbSkill1.Size = new Size(180, 22);
      this.cmbSkill1.TabIndex = 0;
      this.chkBonusesKnown.AutoSize = true;
      this.chkBonusesKnown.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkBonusesKnown.Location = new Point(75, 10);
      this.chkBonusesKnown.Name = "chkBonusesKnown";
      this.chkBonusesKnown.Size = new Size(86, 17);
      this.chkBonusesKnown.TabIndex = 20;
      this.chkBonusesKnown.Text = "Skills Known";
      this.chkBonusesKnown.UseVisualStyleBackColor = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = Color.Transparent;
      this.Controls.Add((Control) this.chkBonusesKnown);
      this.Controls.Add((Control) this.lblTrait4);
      this.Controls.Add((Control) this.cmbTrait4);
      this.Controls.Add((Control) this.lblTrait3);
      this.Controls.Add((Control) this.cmbTrait3);
      this.Controls.Add((Control) this.lblTrait2);
      this.Controls.Add((Control) this.cmbTrait2);
      this.Controls.Add((Control) this.lblTrait1);
      this.Controls.Add((Control) this.lblSkill4);
      this.Controls.Add((Control) this.numSkill4);
      this.Controls.Add((Control) this.cmbSkill4);
      this.Controls.Add((Control) this.lblSkill3);
      this.Controls.Add((Control) this.numSkill3);
      this.Controls.Add((Control) this.cmbSkill3);
      this.Controls.Add((Control) this.lblSkill2);
      this.Controls.Add((Control) this.numSkill2);
      this.Controls.Add((Control) this.cmbSkill2);
      this.Controls.Add((Control) this.lblSkill1);
      this.Controls.Add((Control) this.numSkill1);
      this.Controls.Add((Control) this.cmbTrait1);
      this.Controls.Add((Control) this.cmbSkill1);
      this.Name = nameof (CharacterSkillTraitEditPanel);
      this.Size = new Size(350, 285);
      this.numSkill1.EndInit();
      this.numSkill2.EndInit();
      this.numSkill3.EndInit();
      this.numSkill4.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public CharacterSkillTraitEditPanel() => this.InitializeComponent();

    public void SetControls(CharacterRole role)
    {
      switch (role)
      {
        case CharacterRole.Leader:
        case CharacterRole.Scientist:
        case CharacterRole.PirateLeader:
          this.chkBonusesKnown.Checked = true;
          break;
        default:
          this.chkBonusesKnown.Checked = false;
          break;
      }
      this.cmbSkill1.BindData(role, true);
      this.cmbSkill2.BindData(role, true);
      this.cmbSkill3.BindData(role, true);
      this.cmbSkill4.BindData(role, true);
      this.numSkill1.Value = 0M;
      this.numSkill2.Value = 0M;
      this.numSkill3.Value = 0M;
      this.numSkill4.Value = 0M;
      this.cmbTrait1.BindData(role, true);
      this.cmbTrait2.BindData(role, true);
      this.cmbTrait3.BindData(role, true);
      this.cmbTrait4.BindData(role, true);
    }

    public void BindData(Character character)
    {
      if (character == null)
        return;
      this.SetControls(character.Role);
      this.chkBonusesKnown.Checked = character.BonusesKnown;
      for (int index = 0; index < character.Skills.Count; ++index)
      {
        if (character.Skills[index].Type != CharacterSkillType.Undefined)
        {
          switch (index)
          {
            case 0:
              this.cmbSkill1.SetSelectedSkill(character.Skills[index].Type);
              this.numSkill1.Value = (Decimal) character.Skills[index].Level;
              continue;
            case 1:
              this.cmbSkill2.SetSelectedSkill(character.Skills[index].Type);
              this.numSkill2.Value = (Decimal) character.Skills[index].Level;
              continue;
            case 2:
              this.cmbSkill3.SetSelectedSkill(character.Skills[index].Type);
              this.numSkill3.Value = (Decimal) character.Skills[index].Level;
              continue;
            case 3:
              this.cmbSkill4.SetSelectedSkill(character.Skills[index].Type);
              this.numSkill4.Value = (Decimal) character.Skills[index].Level;
              continue;
            default:
              continue;
          }
        }
      }
      for (int index = 0; index < character.Traits.Count; ++index)
      {
        if (character.Traits[index] != CharacterTraitType.Undefined)
        {
          switch (index)
          {
            case 0:
              this.cmbTrait1.SetSelectedTrait(character.Traits[index]);
              continue;
            case 1:
              this.cmbTrait2.SetSelectedTrait(character.Traits[index]);
              continue;
            case 2:
              this.cmbTrait3.SetSelectedTrait(character.Traits[index]);
              continue;
            case 3:
              this.cmbTrait4.SetSelectedTrait(character.Traits[index]);
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void UnbindData(ref Character character)
    {
      if (character == null)
        return;
      character.RemoveAllSkillsAndTraits();
      character.BonusesKnown = this.chkBonusesKnown.Checked;
      this.AddSkillIfPresent(character, this.cmbSkill1.SelectedSkill, (int) this.numSkill1.Value);
      this.AddSkillIfPresent(character, this.cmbSkill2.SelectedSkill, (int) this.numSkill2.Value);
      this.AddSkillIfPresent(character, this.cmbSkill3.SelectedSkill, (int) this.numSkill3.Value);
      this.AddSkillIfPresent(character, this.cmbSkill4.SelectedSkill, (int) this.numSkill4.Value);
      this.AddTraitIfPresent(character, this.cmbTrait1.SelectedTrait);
      this.AddTraitIfPresent(character, this.cmbTrait2.SelectedTrait);
      this.AddTraitIfPresent(character, this.cmbTrait3.SelectedTrait);
      this.AddTraitIfPresent(character, this.cmbTrait4.SelectedTrait);
    }

    private void AddTraitIfPresent(Character character, CharacterTraitType traitType)
    {
      if (character == null || traitType == CharacterTraitType.Undefined)
        return;
      character.AddTrait(traitType, true, (Galaxy) null);
    }

    private void AddSkillIfPresent(Character character, CharacterSkillType skillType, int value)
    {
      if (character == null || skillType == CharacterSkillType.Undefined)
        return;
      character.AddSkill(skillType, value, (Galaxy) null);
    }
  }
}
