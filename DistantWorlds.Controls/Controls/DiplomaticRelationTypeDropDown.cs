// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomaticRelationTypeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DiplomaticRelationTypeDropDown : DataGridViewComboBoxEditingControl
  {
    private List<object> _DiplomaticRelationTypes;
    private List<object> _PirateRelationTypes;
    public bool PirateRelationMode;
    private SolidBrush _MutualDefenseBrush = new SolidBrush(Color.FromArgb(64, 64, 232));
    private SolidBrush _ProtectorateBrush = new SolidBrush(Color.FromArgb(112, 112, (int) byte.MaxValue));
    private SolidBrush _FreeTradeBrush = new SolidBrush(Color.FromArgb(0, (int) byte.MaxValue, 0));
    private SolidBrush _NoneBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
    private SolidBrush _TruceBrush = new SolidBrush(Color.Yellow);
    private SolidBrush _SubjugatedBrush = new SolidBrush(Color.Violet);
    private SolidBrush _TradeSanctionsBrush = new SolidBrush(Color.Orange);
    private SolidBrush _WarBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0));
    private SolidBrush _NotMetBrush = new SolidBrush(Color.Tan);
    private SolidBrush _PirateProtectionBrush = new SolidBrush(Color.FromArgb(160, 160, (int) byte.MaxValue));

    public DiplomaticRelationTypeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Ignite();
    }

    public void Ignite()
    {
      this.Items.Clear();
      this._DiplomaticRelationTypes = this.ResolveDiplomaticRelationTypes();
      this._PirateRelationTypes = this.ResolvePirateRelationTypes();
    }

    private void PopulateItemsDiplomaticRelations()
    {
      this.Items.Clear();
      for (int index = 0; index < this._DiplomaticRelationTypes.Count; ++index)
        this.Items.Add(this._DiplomaticRelationTypes[index]);
    }

    private void PopulateItemsPirateRelations()
    {
      this.Items.Clear();
      for (int index = 0; index < this._PirateRelationTypes.Count; ++index)
        this.Items.Add(this._PirateRelationTypes[index]);
    }

    private List<object> ResolveDiplomaticRelationTypes() => new List<object>()
    {
      (object) DiplomaticRelationType.NotMet,
      (object) DiplomaticRelationType.MutualDefensePact,
      (object) DiplomaticRelationType.Protectorate,
      (object) DiplomaticRelationType.FreeTradeAgreement,
      (object) DiplomaticRelationType.None,
      (object) DiplomaticRelationType.SubjugatedDominion,
      (object) DiplomaticRelationType.TradeSanctions,
      (object) DiplomaticRelationType.War
    };

    private List<object> ResolvePirateRelationTypes() => new List<object>()
    {
      (object) PirateRelationType.NotMet,
      (object) PirateRelationType.None,
      (object) PirateRelationType.Protection
    };

    public void SetSelectedDiplomaticRelationType(object diplomaticRelationType)
    {
      int num = -1;
      switch (diplomaticRelationType)
      {
        case DiplomaticRelationType _:
          this.PirateRelationMode = false;
          this.PopulateItemsDiplomaticRelations();
          for (int index = 0; index < this._DiplomaticRelationTypes.Count; ++index)
          {
            if (this._DiplomaticRelationTypes[index] == diplomaticRelationType)
            {
              num = index;
              break;
            }
          }
          break;
        case PirateRelationType _:
          this.PirateRelationMode = true;
          this.PopulateItemsPirateRelations();
          for (int index = 0; index < this._PirateRelationTypes.Count; ++index)
          {
            if (this._PirateRelationTypes[index] == diplomaticRelationType)
            {
              num = index;
              break;
            }
          }
          break;
      }
      this.SelectedIndex = num;
    }

    public object SelectedDiplomaticRelationType
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return this.PirateRelationMode ? (selectedIndex >= 0 ? this._PirateRelationTypes[selectedIndex] : (object) PirateRelationType.NotMet) : (selectedIndex >= 0 ? this._DiplomaticRelationTypes[selectedIndex] : (object) DiplomaticRelationType.NotMet);
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    private SolidBrush SelectBrush(PirateRelationType pirateRelationType)
    {
      SolidBrush solidBrush = (SolidBrush) null;
      switch (pirateRelationType)
      {
        case PirateRelationType.NotMet:
          solidBrush = this._NotMetBrush;
          break;
        case PirateRelationType.None:
          solidBrush = this._NoneBrush;
          break;
        case PirateRelationType.Protection:
          solidBrush = this._PirateProtectionBrush;
          break;
      }
      return solidBrush;
    }

    private SolidBrush SelectBrush(DiplomaticRelationType diplomaticRelationType)
    {
      SolidBrush solidBrush = (SolidBrush) null;
      switch (diplomaticRelationType)
      {
        case DiplomaticRelationType.NotMet:
          solidBrush = this._NotMetBrush;
          break;
        case DiplomaticRelationType.None:
          solidBrush = this._NoneBrush;
          break;
        case DiplomaticRelationType.FreeTradeAgreement:
          solidBrush = this._FreeTradeBrush;
          break;
        case DiplomaticRelationType.MutualDefensePact:
          solidBrush = this._MutualDefenseBrush;
          break;
        case DiplomaticRelationType.SubjugatedDominion:
          solidBrush = this._SubjugatedBrush;
          break;
        case DiplomaticRelationType.Protectorate:
          solidBrush = this._ProtectorateBrush;
          break;
        case DiplomaticRelationType.TradeSanctions:
          solidBrush = this._TradeSanctionsBrush;
          break;
        case DiplomaticRelationType.War:
          solidBrush = this._WarBrush;
          break;
        case DiplomaticRelationType.Truce:
          solidBrush = this._TruceBrush;
          break;
      }
      return solidBrush;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      SolidBrush solidBrush1 = new SolidBrush(this.ForeColor);
      if (this.PirateRelationMode)
      {
        if (this._PirateRelationTypes != null && e.Index >= 0 && e.Index < this._PirateRelationTypes.Count)
        {
          PirateRelationType pirateRelationType = (PirateRelationType) this._PirateRelationTypes[e.Index];
          SolidBrush solidBrush2 = this.SelectBrush(pirateRelationType);
          string s = string.Empty;
          switch (pirateRelationType)
          {
            case PirateRelationType.NotMet:
              s = TextResolver.GetText("DiplomaticRelationType NotMet");
              break;
            case PirateRelationType.None:
              s = TextResolver.GetText("None");
              break;
            case PirateRelationType.Protection:
              s = TextResolver.GetText("Pirate Protection");
              break;
          }
          e.Graphics.DrawString(s, font, (Brush) solidBrush2, point);
        }
      }
      else if (this._DiplomaticRelationTypes != null && e.Index >= 0 && e.Index < this._DiplomaticRelationTypes.Count)
      {
        SolidBrush solidBrush3 = this.SelectBrush((DiplomaticRelationType) this._DiplomaticRelationTypes[e.Index]);
        e.Graphics.DrawString(Galaxy.ResolveDescription((DiplomaticRelationType) this._DiplomaticRelationTypes[e.Index]), font, (Brush) solidBrush3, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
