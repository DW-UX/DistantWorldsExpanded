// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.RuinPanel
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
  public class RuinPanel : UserControl
  {
    private Ruin _Ruin;
    private Bitmap[] _RuinImages;
    protected IFontCache _FontCache;
    private float _FontSize = FontSize.Normal;
    private bool _FontIsBold;
    private IContainer components;
    private Label lblTitle;
    private Label lblName;
    private GroupBox grpBonuses;
    private Label lblExplorationBonus;
    private Label lblResearchBonus;
    private Label lblDevelopmentBonus;
    private NumericUpDown numExplorationBonus;
    private NumericUpDown numResearchBonus;
    private NumericUpDown numDevelopmentBonus;
    private TextBox txtName;
    private PictureBox picImage;
    private NumericUpDown numMoneyBonus;
    private Label lblMoneyBonus;
    private CheckBox chkCreatureSwarm;
    private CheckBox chkPirateAmbush;
    private HScrollBar scrImage;

    public virtual void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public void SetFont(float pointSize) => this.SetFont(pointSize, false);

    public void SetFont(float pointSize, bool isBold)
    {
      this._FontSize = pointSize;
      this._FontIsBold = isBold;
      if (this._FontCache == null)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
    }

    public RuinPanel() => this.InitializeComponent();

    private void LayoutPanel()
    {
      this.lblName.Location = new Point(5, 25);
      this.txtName.Location = new Point(47, 23);
      this.picImage.Location = new Point(205, 0);
      this.scrImage.Size = new Size(50, 12);
      this.scrImage.Location = new Point(205, 38);
      this.picImage.SendToBack();
      this.grpBonuses.Size = new Size(254, 116);
      this.grpBonuses.Location = new Point(3, 50);
      this.lblDevelopmentBonus.Location = new Point(5, 17);
      this.numDevelopmentBonus.Location = new Point(84, 15);
      this.lblResearchBonus.Location = new Point(5, 42);
      this.numResearchBonus.Location = new Point(84, 40);
      this.lblExplorationBonus.Location = new Point(5, 67);
      this.numExplorationBonus.Location = new Point(84, 65);
      this.lblMoneyBonus.Location = new Point(5, 92);
      this.numMoneyBonus.Location = new Point(84, 90);
      this.chkCreatureSwarm.Location = new Point(149, 18);
      this.chkPirateAmbush.Location = new Point(149, 43);
      this.LocalizeControlText();
    }

    private void LocalizeControlText()
    {
      this.lblDevelopmentBonus.Text = TextResolver.GetText("Development");
      this.lblExplorationBonus.Text = TextResolver.GetText("Exploration");
      this.lblMoneyBonus.Text = TextResolver.GetText("Money");
      this.lblName.Text = TextResolver.GetText("Name");
      this.lblResearchBonus.Text = TextResolver.GetText("Research");
      this.lblTitle.Text = TextResolver.GetText("Ruins");
      this.grpBonuses.Text = TextResolver.GetText("Bonuses");
      this.chkCreatureSwarm.Text = TextResolver.GetText("Creature Swarm");
      this.chkPirateAmbush.Text = TextResolver.GetText("Pirate Ambush");
    }

    public void ClearData() => this._Ruin = (Ruin) null;

    public void BindData(Ruin ruin, Bitmap[] ruinImages)
    {
      this._Ruin = ruin;
      this._RuinImages = ruinImages;
      this.LayoutPanel();
      this.picImage.SizeMode = PictureBoxSizeMode.Zoom;
      if (this._Ruin != null)
      {
        this.txtName.Text = this._Ruin.Name;
        this.numDevelopmentBonus.Value = (Decimal) (this._Ruin.DevelopmentBonus * 100.0);
        this.numResearchBonus.Value = (Decimal) this._Ruin.ResearchBonus;
        this.numExplorationBonus.Value = (Decimal) this._Ruin.MapSystemReveal;
        this.numMoneyBonus.Value = (Decimal) this._Ruin.MoneyBonus;
        this.picImage.Image = (Image) this._RuinImages[this._Ruin.PictureRef];
        this.chkCreatureSwarm.Checked = false;
        this.chkPirateAmbush.Checked = false;
        if (this._Ruin.Type == RuinType.PirateAmbush)
          this.chkPirateAmbush.Checked = true;
        else if (this._Ruin.Type == RuinType.CreatureSwarm)
          this.chkCreatureSwarm.Checked = true;
      }
      else
      {
        this.txtName.Text = string.Empty;
        this.numDevelopmentBonus.Value = 0M;
        this.numResearchBonus.Value = 0M;
        this.numExplorationBonus.Value = 0M;
        this.numMoneyBonus.Value = 0M;
        this.picImage.Image = (Image) null;
        this.chkCreatureSwarm.Checked = false;
        this.chkPirateAmbush.Checked = false;
      }
      this.lblTitle.Font = this._FontCache.GenerateFont(FontSize.Large, true);
    }

    public Ruin GetRuin()
    {
      if (this._Ruin != null)
      {
        this._Ruin.Name = this.txtName.Text;
        this._Ruin.DevelopmentBonus = !this.numDevelopmentBonus.Enabled ? 0.0 : (double) (this.numDevelopmentBonus.Value / 100M);
        this._Ruin.ResearchBonus = !this.numResearchBonus.Enabled ? 0 : (int) this.numResearchBonus.Value;
        this._Ruin.MapSystemReveal = !this.numExplorationBonus.Enabled ? 0 : (int) this.numExplorationBonus.Value;
        this._Ruin.MoneyBonus = !this.numMoneyBonus.Enabled ? 0.0 : (double) this.numMoneyBonus.Value;
        this._Ruin.Type = !this.chkCreatureSwarm.Checked ? (!this.chkPirateAmbush.Checked ? RuinType.Standard : RuinType.PirateAmbush) : RuinType.CreatureSwarm;
      }
      return this._Ruin;
    }

    private void chkCreatureSwarm_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkCreatureSwarm.Checked)
      {
        this.numDevelopmentBonus.Enabled = false;
        this.numExplorationBonus.Enabled = false;
        this.numMoneyBonus.Enabled = false;
        this.numResearchBonus.Enabled = false;
        this.chkPirateAmbush.Enabled = false;
      }
      else
      {
        this.numDevelopmentBonus.Enabled = true;
        this.numExplorationBonus.Enabled = true;
        this.numMoneyBonus.Enabled = true;
        this.numResearchBonus.Enabled = true;
        this.chkPirateAmbush.Enabled = true;
      }
    }

    private void chkPirateAmbush_CheckedChanged(object sender, EventArgs e)
    {
      if (this.chkPirateAmbush.Checked)
      {
        this.numDevelopmentBonus.Enabled = false;
        this.numExplorationBonus.Enabled = false;
        this.numMoneyBonus.Enabled = false;
        this.numResearchBonus.Enabled = false;
        this.chkCreatureSwarm.Enabled = false;
      }
      else
      {
        this.numDevelopmentBonus.Enabled = true;
        this.numExplorationBonus.Enabled = true;
        this.numMoneyBonus.Enabled = true;
        this.numResearchBonus.Enabled = true;
        this.chkCreatureSwarm.Enabled = true;
      }
    }

    public void ImageScroll(int newValue)
    {
      if (this._Ruin == null)
        return;
      int num = newValue;
      if (num >= this._RuinImages.Length)
        num = this._RuinImages.Length - 1;
      else if (num < 0)
        num = 0;
      this._Ruin.PictureRef = num;
      this.picImage.Image = (Image) this._RuinImages[this._Ruin.PictureRef];
      this.picImage.Refresh();
    }

    private void scrImage_Scroll(object sender, ScrollEventArgs e) => this.ImageScroll(e.NewValue);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.lblTitle = new Label();
      this.lblName = new Label();
      this.grpBonuses = new GroupBox();
      this.chkPirateAmbush = new CheckBox();
      this.chkCreatureSwarm = new CheckBox();
      this.numMoneyBonus = new NumericUpDown();
      this.lblMoneyBonus = new Label();
      this.numExplorationBonus = new NumericUpDown();
      this.numResearchBonus = new NumericUpDown();
      this.numDevelopmentBonus = new NumericUpDown();
      this.lblExplorationBonus = new Label();
      this.lblResearchBonus = new Label();
      this.lblDevelopmentBonus = new Label();
      this.txtName = new TextBox();
      this.picImage = new PictureBox();
      this.scrImage = new HScrollBar();
      this.grpBonuses.SuspendLayout();
      this.numMoneyBonus.BeginInit();
      this.numExplorationBonus.BeginInit();
      this.numResearchBonus.BeginInit();
      this.numDevelopmentBonus.BeginInit();
      ((ISupportInitialize) this.picImage).BeginInit();
      this.SuspendLayout();
      this.lblTitle.AutoSize = true;
      this.lblTitle.Font = new Font("Verdana", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lblTitle.Location = new Point(0, 0);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new Size(53, 18);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "Ruins";
      this.lblName.AutoSize = true;
      this.lblName.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblName.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblName.Location = new Point(5, 25);
      this.lblName.Name = "lblName";
      this.lblName.Size = new Size(40, 13);
      this.lblName.TabIndex = 1;
      this.lblName.Text = "Name";
      this.grpBonuses.BackColor = Color.Transparent;
      this.grpBonuses.Controls.Add((Control) this.chkPirateAmbush);
      this.grpBonuses.Controls.Add((Control) this.chkCreatureSwarm);
      this.grpBonuses.Controls.Add((Control) this.numMoneyBonus);
      this.grpBonuses.Controls.Add((Control) this.lblMoneyBonus);
      this.grpBonuses.Controls.Add((Control) this.numExplorationBonus);
      this.grpBonuses.Controls.Add((Control) this.numResearchBonus);
      this.grpBonuses.Controls.Add((Control) this.numDevelopmentBonus);
      this.grpBonuses.Controls.Add((Control) this.lblExplorationBonus);
      this.grpBonuses.Controls.Add((Control) this.lblResearchBonus);
      this.grpBonuses.Controls.Add((Control) this.lblDevelopmentBonus);
      this.grpBonuses.Font = new Font("Verdana", 9f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.grpBonuses.ForeColor = Color.FromArgb(170, 170, 170);
      this.grpBonuses.Location = new Point(3, 50);
      this.grpBonuses.Name = "grpBonuses";
      this.grpBonuses.Size = new Size(254, 116);
      this.grpBonuses.TabIndex = 2;
      this.grpBonuses.TabStop = false;
      this.grpBonuses.Text = "Bonuses";
      this.chkPirateAmbush.AutoSize = true;
      this.chkPirateAmbush.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.chkPirateAmbush.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkPirateAmbush.Location = new Point(152, 43);
      this.chkPirateAmbush.Name = "chkPirateAmbush";
      this.chkPirateAmbush.Size = new Size(109, 17);
      this.chkPirateAmbush.TabIndex = 11;
      this.chkPirateAmbush.Text = "Pirate Ambush";
      this.chkPirateAmbush.UseVisualStyleBackColor = true;
      this.chkPirateAmbush.CheckedChanged += new EventHandler(this.chkPirateAmbush_CheckedChanged);
      this.chkCreatureSwarm.AutoSize = true;
      this.chkCreatureSwarm.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.chkCreatureSwarm.ForeColor = Color.FromArgb(170, 170, 170);
      this.chkCreatureSwarm.Location = new Point(152, 18);
      this.chkCreatureSwarm.Name = "chkCreatureSwarm";
      this.chkCreatureSwarm.Size = new Size(121, 17);
      this.chkCreatureSwarm.TabIndex = 10;
      this.chkCreatureSwarm.Text = "Creature Swarm";
      this.chkCreatureSwarm.UseVisualStyleBackColor = true;
      this.chkCreatureSwarm.CheckedChanged += new EventHandler(this.chkCreatureSwarm_CheckedChanged);
      this.numMoneyBonus.BackColor = Color.FromArgb(48, 48, 64);
      this.numMoneyBonus.BorderStyle = BorderStyle.FixedSingle;
      this.numMoneyBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.numMoneyBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.numMoneyBonus.Location = new Point(84, 90);
      this.numMoneyBonus.Maximum = new Decimal(new int[4]
      {
        20000,
        0,
        0,
        0
      });
      this.numMoneyBonus.Name = "numMoneyBonus";
      this.numMoneyBonus.Size = new Size(60, 21);
      this.numMoneyBonus.TabIndex = 9;
      this.lblMoneyBonus.AutoSize = true;
      this.lblMoneyBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblMoneyBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblMoneyBonus.Location = new Point(5, 92);
      this.lblMoneyBonus.Name = "lblMoneyBonus";
      this.lblMoneyBonus.Size = new Size(44, 13);
      this.lblMoneyBonus.TabIndex = 8;
      this.lblMoneyBonus.Text = "Money";
      this.numExplorationBonus.BackColor = Color.FromArgb(48, 48, 64);
      this.numExplorationBonus.BorderStyle = BorderStyle.FixedSingle;
      this.numExplorationBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.numExplorationBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.numExplorationBonus.Location = new Point(84, 65);
      this.numExplorationBonus.Maximum = new Decimal(new int[4]
      {
        20,
        0,
        0,
        0
      });
      this.numExplorationBonus.Name = "numExplorationBonus";
      this.numExplorationBonus.Size = new Size(60, 21);
      this.numExplorationBonus.TabIndex = 7;
      this.numResearchBonus.BackColor = Color.FromArgb(48, 48, 64);
      this.numResearchBonus.BorderStyle = BorderStyle.FixedSingle;
      this.numResearchBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.numResearchBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.numResearchBonus.Location = new Point(84, 40);
      this.numResearchBonus.Maximum = new Decimal(new int[4]
      {
        150000,
        0,
        0,
        0
      });
      this.numResearchBonus.Name = "numResearchBonus";
      this.numResearchBonus.Size = new Size(60, 21);
      this.numResearchBonus.TabIndex = 6;
      this.numDevelopmentBonus.BackColor = Color.FromArgb(48, 48, 64);
      this.numDevelopmentBonus.BorderStyle = BorderStyle.FixedSingle;
      this.numDevelopmentBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.numDevelopmentBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.numDevelopmentBonus.Location = new Point(84, 15);
      this.numDevelopmentBonus.Maximum = new Decimal(new int[4]
      {
        50,
        0,
        0,
        0
      });
      this.numDevelopmentBonus.Name = "numDevelopmentBonus";
      this.numDevelopmentBonus.Size = new Size(60, 21);
      this.numDevelopmentBonus.TabIndex = 5;
      this.lblExplorationBonus.AutoSize = true;
      this.lblExplorationBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblExplorationBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblExplorationBonus.Location = new Point(5, 67);
      this.lblExplorationBonus.Name = "lblExplorationBonus";
      this.lblExplorationBonus.Size = new Size(71, 13);
      this.lblExplorationBonus.TabIndex = 4;
      this.lblExplorationBonus.Text = "Exploration";
      this.lblResearchBonus.AutoSize = true;
      this.lblResearchBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblResearchBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblResearchBonus.Location = new Point(5, 42);
      this.lblResearchBonus.Name = "lblResearchBonus";
      this.lblResearchBonus.Size = new Size(60, 13);
      this.lblResearchBonus.TabIndex = 3;
      this.lblResearchBonus.Text = "Research";
      this.lblDevelopmentBonus.AutoSize = true;
      this.lblDevelopmentBonus.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.lblDevelopmentBonus.ForeColor = Color.FromArgb(170, 170, 170);
      this.lblDevelopmentBonus.Location = new Point(5, 17);
      this.lblDevelopmentBonus.Name = "lblDevelopmentBonus";
      this.lblDevelopmentBonus.Size = new Size(83, 13);
      this.lblDevelopmentBonus.TabIndex = 2;
      this.lblDevelopmentBonus.Text = "Development";
      this.txtName.BackColor = Color.FromArgb(48, 48, 64);
      this.txtName.BorderStyle = BorderStyle.FixedSingle;
      this.txtName.ForeColor = Color.FromArgb(170, 170, 170);
      this.txtName.Location = new Point(47, 23);
      this.txtName.Name = "txtName";
      this.txtName.Size = new Size(150, 21);
      this.txtName.TabIndex = 3;
      this.picImage.BackColor = Color.Transparent;
      this.picImage.Location = new Point(205, 0);
      this.picImage.Name = "picImage";
      this.picImage.Size = new Size(50, 50);
      this.picImage.TabIndex = 4;
      this.picImage.TabStop = false;
      this.scrImage.Location = new Point(66, 4);
      this.scrImage.Name = "scrImage";
      this.scrImage.Size = new Size(80, 17);
      this.scrImage.TabIndex = 5;
      this.scrImage.Scroll += new ScrollEventHandler(this.scrImage_Scroll);
      this.AutoScaleMode = AutoScaleMode.None;
      this.BackColor = Color.DimGray;
      this.Controls.Add((Control) this.scrImage);
      this.Controls.Add((Control) this.picImage);
      this.Controls.Add((Control) this.txtName);
      this.Controls.Add((Control) this.grpBonuses);
      this.Controls.Add((Control) this.lblName);
      this.Controls.Add((Control) this.lblTitle);
      this.Font = new Font("Verdana", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.ForeColor = Color.White;
      this.Name = nameof (RuinPanel);
      this.Size = new Size(260, 170);
      this.grpBonuses.ResumeLayout(false);
      this.grpBonuses.PerformLayout();
      this.numMoneyBonus.EndInit();
      this.numExplorationBonus.EndInit();
      this.numResearchBonus.EndInit();
      this.numDevelopmentBonus.EndInit();
      ((ISupportInitialize) this.picImage).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
