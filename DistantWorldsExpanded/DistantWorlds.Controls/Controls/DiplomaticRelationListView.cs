// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomaticRelationListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DiplomaticRelationListView : UserControl
  {
    private Game _Game;
    private Empire _Empire;
    private Empire _SelectedEmpire;
    private DiplomaticRelationList _DiplomaticRelations;
    private DistantWorlds.Types.EmpireList _Empires;
    private int _RelationViewWidth;
    private int _RowTemplateHeight = 30;
    private Bitmap _TreatyImage;
    private Bitmap _AgentImage;
    private Bitmap _RefuelImage;
    private Bitmap _MiningImage;
    private bool _RaiseEvents = true;
    private IFontCache _FontCache;
    private float _FontSize = 15.33f;
    private bool _FontIsBold;
    private Font _TinyFont;
    private IContainer components;
    private EmpireListView _EmpireListView;
    private System.Windows.Forms.Panel _Relations;

    public event DiplomaticRelationListView.RelationDoubleClickedEventHandler _RelationDoubleClicked;

    public void SetFontCache(IFontCache fontCache)
    {
      this._FontCache = fontCache;
      if ((double) this._FontSize <= 0.0)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
      this._EmpireListView.SetFont(this._FontSize, this._FontIsBold);
    }

    public void SetFont(float pixelSize) => this.SetFont(pixelSize, false);

    public void SetFont(float pixelSize, bool isBold)
    {
      this._FontSize = pixelSize;
      this._FontIsBold = isBold;
      if (this._FontCache == null)
        return;
      this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
      this._EmpireListView.SetFont(pixelSize, isBold);
    }

    public DiplomaticRelationListView()
    {
      this._Empires = new DistantWorlds.Types.EmpireList();
      this.InitializeComponent();
      this._Relations.BackColor = Color.FromArgb(48, 48, 64);
      this._RelationViewWidth = 110;
      this._EmpireListView.RowTemplateHeight = this._RowTemplateHeight;
      this._EmpireListView.Grid.ColumnHeadersVisible = false;
      this.Width = 300;
      this.Height = 200;
      this.LayoutControls();
      this.SelectionChanged = new EventHandler(this.OnSelectionChanged);
      this._Relations.Paint += new PaintEventHandler(this._Relations_Paint);
    }

    private void _Relations_Paint(object sender, PaintEventArgs e) => this.DrawRelations(e.Graphics);

    private void LayoutControls()
    {
      this._Relations.Width = this._RelationViewWidth + 1;
      this._EmpireListView.Width = this.Width - this._RelationViewWidth;
      this._Relations.Height = this.Height;
      this._EmpireListView.Height = this.Height;
      this._EmpireListView.Location = new Point(0, 0);
      this._EmpireListView.BorderStyle = BorderStyle.None;
      this._EmpireListView.Grid.BorderStyle = BorderStyle.None;
      Color color = Color.FromArgb(60, 60, 72);
      DataGridViewCellStyle defaultCellStyle1 = this._EmpireListView.Grid.AlternatingRowsDefaultCellStyle;
      defaultCellStyle1.BackColor = color;
      this._EmpireListView.Grid.AlternatingRowsDefaultCellStyle = defaultCellStyle1;
      DataGridViewCellStyle defaultCellStyle2 = this._EmpireListView.Grid.DefaultCellStyle;
      defaultCellStyle2.BackColor = color;
      this._EmpireListView.Grid.DefaultCellStyle = defaultCellStyle2;
      this._EmpireListView.Grid.BackgroundColor = color;
      this._Relations.Location = new Point(this._EmpireListView.Width, 0);
      this._Relations.BackColor = color;
      this.Invalidate();
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Height
    {
      get => base.Height;
      set => base.Height = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Width
    {
      get => base.Width;
      set => base.Width = value;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RelationViewWidth
    {
      get => this._RelationViewWidth;
      set
      {
        this._RelationViewWidth = value <= this.Width ? value : throw new ArgumentException("Cannot set RelationViewWidth to be wider than the parent DiplomaticRelationListView control");
        this.LayoutControls();
      }
    }

    public void ClearData()
    {
      this._Game = (Game) null;
      this._Empire = (Empire) null;
      this._SelectedEmpire = (Empire) null;
      this._DiplomaticRelations = (DiplomaticRelationList) null;
      this._Empires = (DistantWorlds.Types.EmpireList) null;
    }

    public void BindData(
      Game game,
      Empire empire,
      Font tinyFont,
      RaceImageCache raceImageCache,
      Bitmap treatyImage,
      Bitmap agentImage,
      Bitmap refuelImage,
      Bitmap miningImage)
    {
      this._RaiseEvents = false;
      this._EmpireListView.Grid.AllowUserToResizeColumns = false;
      this._TinyFont = tinyFont;
      this._Game = game;
      this._Empire = empire;
      this._SelectedEmpire = this._Empire;
      this._DiplomaticRelations = (DiplomaticRelationList) null;
      this._Empires = (DistantWorlds.Types.EmpireList) null;
      this._TreatyImage = treatyImage;
      this._AgentImage = agentImage;
      if (this._RefuelImage != null)
        this._RefuelImage.Dispose();
      if (this._MiningImage != null)
        this._MiningImage.Dispose();
      this._RefuelImage = GraphicsHelper.ScaleLimitImage(refuelImage, 16, 16, 1f);
      this._MiningImage = GraphicsHelper.ScaleLimitImage(miningImage, 16, 16, 1f);
      this._EmpireListView.Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      if (this._Empire != null)
      {
        this._Empire.ClearInvalidDiplomaticRelations();
        this._DiplomaticRelations = this._Empire.DiplomaticRelations;
        this._Empires = new DistantWorlds.Types.EmpireList();
        this._Empires.Add(this._Empire);
        for (int index = 0; index < this._DiplomaticRelations.Count; ++index)
        {
          DiplomaticRelation diplomaticRelation = this._DiplomaticRelations[index];
          if (diplomaticRelation.Type != DiplomaticRelationType.NotMet && diplomaticRelation.OtherEmpire != null && diplomaticRelation.OtherEmpire != this._Game.Galaxy.IndependentEmpire)
            this._Empires.Add(diplomaticRelation.OtherEmpire);
        }
        if (this._Empire.PirateRelations != null)
        {
          for (int index = 0; index < this._Empire.PirateRelations.Count; ++index)
          {
            PirateRelation pirateRelation = this._Empire.PirateRelations[index];
            if (pirateRelation.Type != PirateRelationType.NotMet && pirateRelation.OtherEmpire != null && pirateRelation.OtherEmpire != this._Game.Galaxy.IndependentEmpire)
              this._Empires.Add(pirateRelation.OtherEmpire);
          }
        }
      }
      this._EmpireListView.BindData(this._Empires, raceImageCache);
      this._RaiseEvents = true;
      this.OnSelectionChanged((object) this, new EventArgs());
      this.Invalidate();
    }

    protected override void OnInvalidated(InvalidateEventArgs e)
    {
      base.OnInvalidated(e);
      this._Relations.Invalidate();
    }

    public void DrawRelations(Graphics graphics)
    {
      int scrollingRowIndex = this._EmpireListView.FirstDisplayedScrollingRowIndex;
      int num1 = 0;
      int rowTemplateHeight = this._EmpireListView.RowTemplateHeight;
      int width1 = this._Relations.Width;
      Pen pen1 = (Pen) null;
      Pen pen2 = new Pen(Color.FromArgb(96, 96, 96), 30f);
      Pen pen3 = new Pen(Color.FromArgb(60, 60, 72), 3f);
      Pen pen4 = new Pen(Color.FromArgb(64, 64, 232), 3f);
      Pen pen5 = new Pen(Color.FromArgb(112, 112, (int) byte.MaxValue), 3f);
      Pen pen6 = new Pen(Color.FromArgb(0, (int) byte.MaxValue, 0), 3f);
      Pen pen7 = new Pen(Color.FromArgb(128, 128, 128), 3f);
      Pen pen8 = new Pen(Color.Yellow, 3f);
      Pen pen9 = new Pen(Color.Yellow, 3f);
      Pen pen10 = new Pen(Color.Orange, 3f);
      Pen pen11 = new Pen(Color.FromArgb((int) byte.MaxValue, 0, 0), 3f);
      Pen pen12 = new Pen(Color.Tan, 3f);
      Pen pen13 = new Pen(Color.FromArgb(160, 160, (int) byte.MaxValue), 3f);
      Pen pen14 = new Pen(Color.FromArgb((int) byte.MaxValue, 160, 160), 3f);
      Pen pen15 = new Pen(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), 3f);
      graphics.Clear(this._Relations.BackColor);
      if (this._Empires == null || scrollingRowIndex > this._Empires.Count || scrollingRowIndex < 0)
        return;
      int num2 = scrollingRowIndex;
      for (int index1 = scrollingRowIndex; index1 < this._Empires.Count; ++index1)
      {
        int tag = (int) this._EmpireListView.Rows[index1].Cells[2].Tag;
        int num3 = 4 + rowTemplateHeight / 2 + (num1 + rowTemplateHeight * (num2 - scrollingRowIndex));
        if (this._SelectedEmpire != null && this._SelectedEmpire.PirateEmpireBaseHabitat != null && this._Empires[tag] != null)
        {
          switch (this._SelectedEmpire.ObtainPirateRelation(this._Empires[tag]).Type)
          {
            case PirateRelationType.None:
              pen1 = pen7;
              break;
            case PirateRelationType.Protection:
              pen1 = pen13;
              break;
          }
          if (pen1 != null)
          {
            graphics.DrawLine(pen1, 0, num3, width1, num3);
            Rectangle rect = new Rectangle(0, num3 - 1, 10, 3);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, pen3.Color, pen1.Color, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            graphics.FillRectangle((Brush) linearGradientBrush, rect);
            int y = num3 - 13;
            string str = this._SelectedEmpire.ResolveFeelingDescription(this._Empires[tag].ObtainPirateRelation(this._SelectedEmpire));
            graphics.MeasureString(str, this._EmpireListView.Font);
            graphics.DrawString(str, this._EmpireListView.Font, pen1.Brush, (PointF) new Point(2, y));
          }
        }
        else if (this._Empires[tag].PirateEmpireBaseHabitat != null)
        {
          if (this._SelectedEmpire == this._Game.PlayerEmpire)
          {
            pen1 = (Pen) null;
            switch (this._Empires[tag].ObtainPirateRelation(this._SelectedEmpire).Type)
            {
              case PirateRelationType.None:
                pen1 = pen7;
                break;
              case PirateRelationType.Protection:
                pen1 = pen13;
                break;
            }
            if (pen1 != null)
            {
              graphics.DrawLine(pen1, 0, num3, width1, num3);
              Rectangle rect = new Rectangle(0, num3 - 1, 10, 3);
              LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, pen3.Color, pen1.Color, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
              graphics.FillRectangle((Brush) linearGradientBrush, rect);
              int y = num3 - 13;
              string str = this._SelectedEmpire.ResolveFeelingDescription(this._Empires[tag].ObtainPirateRelation(this._SelectedEmpire));
              graphics.MeasureString(str, this._EmpireListView.Font);
              graphics.DrawString(str, this._EmpireListView.Font, pen1.Brush, (PointF) new Point(2, y));
            }
          }
        }
        else
        {
          DiplomaticRelation diplomaticRelation1 = this._DiplomaticRelations[this._Empires[tag]];
          int y1;
          if (diplomaticRelation1 != null)
          {
            switch (diplomaticRelation1.Type)
            {
              case DiplomaticRelationType.NotMet:
                pen1 = pen3;
                break;
              case DiplomaticRelationType.None:
                pen1 = pen7;
                break;
              case DiplomaticRelationType.FreeTradeAgreement:
                pen1 = pen6;
                break;
              case DiplomaticRelationType.MutualDefensePact:
                pen1 = pen4;
                break;
              case DiplomaticRelationType.SubjugatedDominion:
                pen1 = pen9;
                break;
              case DiplomaticRelationType.Protectorate:
                pen1 = pen5;
                break;
              case DiplomaticRelationType.TradeSanctions:
                pen1 = pen10;
                break;
              case DiplomaticRelationType.War:
                pen1 = pen11;
                break;
              case DiplomaticRelationType.Truce:
                pen1 = pen8;
                break;
            }
            if (diplomaticRelation1.Type != DiplomaticRelationType.SubjugatedDominion && diplomaticRelation1.Type != DiplomaticRelationType.Protectorate && diplomaticRelation1.Type != DiplomaticRelationType.NotMet)
            {
              graphics.DrawLine(pen1, 0, num3, width1, num3);
              Rectangle rect = new Rectangle(0, num3 - 1, 10, 3);
              LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, pen3.Color, pen1.Color, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
              graphics.FillRectangle((Brush) linearGradientBrush, rect);
            }
            else
            {
              SolidBrush solidBrush = new SolidBrush(pen1.Color);
              Point[] points = new Point[4];
              if (diplomaticRelation1.Initiator == diplomaticRelation1.ThisEmpire)
              {
                points[0] = new Point(0, num3 - 1);
                points[1] = new Point(0, num3 + 1);
                points[2] = new Point(width1, num3 + 7);
                points[3] = new Point(width1, num3 - 1);
              }
              else
              {
                points[0] = new Point(0, num3 - 1);
                points[1] = new Point(0, num3 + 7);
                points[2] = new Point(width1, num3 + 1);
                points[3] = new Point(width1, num3 - 1);
              }
              graphics.SmoothingMode = SmoothingMode.AntiAlias;
              graphics.FillPolygon((Brush) solidBrush, points);
              graphics.SmoothingMode = SmoothingMode.None;
            }
            y1 = num3 - 13;
            string str1 = ((int) (diplomaticRelation1.NormalizedAnnualTradeValue / 1000.0)).ToString("######0K");
            if (diplomaticRelation1.TradeBonus > 0.0)
              str1 = str1 + " (+" + diplomaticRelation1.TradeBonus.ToString("##0%") + ")";
            SizeF sizeF = graphics.MeasureString(str1, this._EmpireListView.Font);
            int x1 = (this._RelationViewWidth - (int) sizeF.Width) / 2 - 10;
            graphics.DrawString(str1, this._EmpireListView.Font, pen1.Brush, (PointF) new Point(x1, y1));
            string str2 = this._Empire.ResolveFeelingDescription(this._Empires[tag].EmpireEvaluations[this._SelectedEmpire]);
            sizeF = graphics.MeasureString(str2, this._EmpireListView.Font);
            int x2 = 2;
            graphics.DrawString(str2, this._EmpireListView.Font, pen1.Brush, (PointF) new Point(x2, y1));
          }
          else
            y1 = num3 - 13;
          if (diplomaticRelation1 != null && diplomaticRelation1.OtherEmpire != null && diplomaticRelation1.OtherEmpire.DiplomaticRelations != null)
          {
            string highestAllianceName = diplomaticRelation1.OtherEmpire.DiplomaticRelations.GetHighestAllianceName();
            if (!string.IsNullOrEmpty(highestAllianceName))
            {
              using (SolidBrush brush = new SolidBrush(Color.FromArgb(170, 170, 170)))
              {
                int width2 = 180;
                int x = (this._RelationViewWidth - width2) / 2;
                int y2 = y1 + 12;
                GraphicsHelper.DrawStringWithDropShadowCentered(graphics, highestAllianceName, this._TinyFont, brush, new Rectangle(x, y2, width2, 15));
              }
            }
          }
          for (int index2 = 0; index2 < this._Game.PlayerEmpire.Characters.Count; ++index2)
          {
            Character character = this._Game.PlayerEmpire.Characters[index2];
            if (character.Role == CharacterRole.IntelligenceAgent && character.Mission != null && character.Mission.TargetEmpire == this._Empires[tag] && character.Mission.Type == IntelligenceMissionType.DeepCover && character.Mission.Outcome == IntelligenceMissionOutcome.SucceedNotDetect)
            {
              Rectangle rect = new Rectangle(this._RelationViewWidth - 72, y1 - 4, this._AgentImage.Width, this._AgentImage.Height);
              graphics.DrawImage((Image) this._AgentImage, rect);
            }
          }
          bool flag1 = false;
          bool flag2 = false;
          bool flag3 = false;
          bool flag4 = false;
          if (diplomaticRelation1 != null)
          {
            flag2 = diplomaticRelation1.MilitaryRefuelingToOther;
            flag4 = diplomaticRelation1.MiningRightsToOther;
            DiplomaticRelation diplomaticRelation2 = diplomaticRelation1.OtherEmpire.ObtainDiplomaticRelation(diplomaticRelation1.ThisEmpire);
            flag1 = diplomaticRelation2.MilitaryRefuelingToOther;
            flag3 = diplomaticRelation2.MiningRightsToOther;
          }
          if (flag1 || flag2)
          {
            int num4 = this._RelationViewWidth - 50;
            if (flag1)
              graphics.DrawImage((Image) diplomaticRelation1.ThisEmpire.SmallFlagPicture, num4 - 2, y1 - 3);
            if (flag2)
              graphics.DrawImage((Image) diplomaticRelation1.OtherEmpire.SmallFlagPicture, num4 - 2, y1 + 7);
            Rectangle rect = new Rectangle(num4 + 6, y1 - 1, this._RefuelImage.Width, this._RefuelImage.Height);
            graphics.DrawImage((Image) this._RefuelImage, rect);
          }
          if (flag3 || flag4)
          {
            int num5 = this._RelationViewWidth - 23;
            if (flag3)
              graphics.DrawImage((Image) diplomaticRelation1.ThisEmpire.SmallFlagPicture, num5 - 2, y1 - 3);
            if (flag4)
              graphics.DrawImage((Image) diplomaticRelation1.OtherEmpire.SmallFlagPicture, num5 - 2, y1 + 7);
            Rectangle rect = new Rectangle(num5 + 7, y1 - 1, this._MiningImage.Width, this._MiningImage.Height);
            graphics.DrawImage((Image) this._MiningImage, rect);
          }
        }
        if (this._Empires[tag] == this._SelectedEmpire)
        {
          int num6 = rowTemplateHeight / 2 + (num1 + rowTemplateHeight * (num2 - scrollingRowIndex));
          graphics.DrawLine(pen2, 0, num6, width1, num6);
        }
        ++num2;
      }
    }

    private void LoadDiplomaticRelations(Empire empire)
    {
      this._DiplomaticRelations = empire.DiplomaticRelations;
      this._Empires = new DistantWorlds.Types.EmpireList();
      for (int index = 0; index < this._DiplomaticRelations.Count; ++index)
        this._Empires.Add(this._DiplomaticRelations[index].OtherEmpire);
    }

    public Empire SelectedEmpire => this._EmpireListView.SelectedEmpire;

    public void SelectEmpire(Empire empireToSelect)
    {
      int num1 = -1;
      if (empireToSelect == null)
        return;
      for (int index = 0; index < this._Empires.Count; ++index)
      {
        if (this._Empires[index] == empireToSelect)
        {
          num1 = index;
          break;
        }
      }
      if (num1 < 0)
        return;
      for (int index = 0; index < this._EmpireListView.Grid.Rows.Count; ++index)
      {
        object tag = this._EmpireListView.Grid.Rows[index].Cells[2].Tag;
        if (tag != null && tag is int num2 && num2 == num1)
        {
          this._EmpireListView.Grid.Rows[index].Selected = true;
          this._EmpireListView.Grid.FirstDisplayedScrollingRowIndex = index;
          break;
        }
      }
    }

    private void _EmpireListView_Scrolled(object sender, ScrollEventArgs e) => this.Invalidate();

    public event EventHandler SelectionChanged;

    public void OnSelectionChanged(object sender, EventArgs e)
    {
      this._SelectedEmpire = this._EmpireListView.SelectedEmpire;
      if (this._SelectedEmpire == null)
        this._SelectedEmpire = this._Empire;
      this._DiplomaticRelations = this._SelectedEmpire.DiplomaticRelations;
      this.Invalidate();
    }

    private void _EmpireListView_SelectionChanged(object sender, EventArgs e)
    {
      if (!this._RaiseEvents)
        return;
      this.SelectionChanged(sender, e);
    }

    private void _EmpireListView_Sorted(object sender, EventArgs e) => this.Invalidate();

    private void _Relations_MouseClick(object sender, MouseEventArgs e) => this._EmpireListView.SelectEmpire(this.ResolveClickedEmpire(e.X, e.Y));

    private Empire ResolveClickedEmpire(int x, int y)
    {
      int height = this._EmpireListView.Grid.Rows[0].Height;
      int index = this._EmpireListView.Grid.FirstDisplayedScrollingRowIndex + y / height;
      return index < this._EmpireListView.Empires.Count ? this._EmpireListView.Empires[index] : (Empire) null;
    }

    private void _Relations_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      Empire sender1 = this.ResolveClickedEmpire(e.X, e.Y);
      if (this._RelationDoubleClicked == null)
        return;
      this._RelationDoubleClicked((object) sender1, new EventArgs());
    }

    private void _EmpireListView_RowDoubleClick(object sender, EventArgs e)
    {
      Empire selectedEmpire = this._EmpireListView.SelectedEmpire;
      if (this._RelationDoubleClicked == null)
        return;
      this._RelationDoubleClicked((object) selectedEmpire, new EventArgs());
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this._Relations = new Panel();
      this._EmpireListView = new EmpireListView();
      this.SuspendLayout();
      this._Relations.Location = new Point(3, 3);
      this._Relations.Name = "_Relations";
      this._Relations.Size = new Size(99, 100);
      this._Relations.TabIndex = 1;
      this._Relations.MouseClick += new MouseEventHandler(this._Relations_MouseClick);
      this._Relations.MouseDoubleClick += new MouseEventHandler(this._Relations_MouseDoubleClick);
      this._EmpireListView.Location = new Point(108, 3);
      this._EmpireListView.Name = "_EmpireListView";
      this._EmpireListView.RowTemplateHeight = 20;
      this._EmpireListView.Size = new Size(100, 100);
      this._EmpireListView.SoundsEnabled = false;
      this._EmpireListView.TabIndex = 0;
      this._EmpireListView.SelectionChanged += new EventHandler(this._EmpireListView_SelectionChanged);
      this._EmpireListView.Scrolled += new ScrollEventHandler(this._EmpireListView_Scrolled);
      this._EmpireListView.Sorted += new EventHandler(this._EmpireListView_Sorted);
      this._EmpireListView.RowDoubleClick += new EventHandler(this._EmpireListView_RowDoubleClick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this._Relations);
      this.Controls.Add((Control) this._EmpireListView);
      this.Name = nameof (DiplomaticRelationListView);
      this.Size = new Size(216, 109);
      this.ResumeLayout(false);
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public EmpireListView EmpireListView
    {
      get => this._EmpireListView;
      set => this._EmpireListView = value;
    }

    public delegate void RelationDoubleClickedEventHandler(object sender, EventArgs e);
  }
}
