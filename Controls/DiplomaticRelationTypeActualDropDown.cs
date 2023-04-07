// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomaticRelationTypeActualDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DiplomaticRelationTypeActualDropDown : ComboBox
  {
    private SolidBrush _MutualDefenseBrush = new SolidBrush(Color.FromArgb(64, 64, 232));
    private SolidBrush _ProtectorateBrush = new SolidBrush(Color.FromArgb(112, 112, (int) byte.MaxValue));
    private SolidBrush _FreeTradeBrush = new SolidBrush(Color.FromArgb(0, (int) byte.MaxValue, 0));
    private SolidBrush _NoneBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
    private SolidBrush _SubjugatedBrush = new SolidBrush(Color.Yellow);
    private SolidBrush _TradeSanctionsBrush = new SolidBrush(Color.Orange);
    private SolidBrush _WarBrush = new SolidBrush(Color.FromArgb((int) byte.MaxValue, 0, 0));
    private SolidBrush _NotMetBrush = new SolidBrush(Color.Tan);
    private List<DiplomaticRelationType> _DiplomaticRelationTypes;

    public DiplomaticRelationTypeActualDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void ClearData() => this._DiplomaticRelationTypes = (List<DiplomaticRelationType>) null;

    public List<DiplomaticRelationType> GetDiplomaticRelationTypes() => this._DiplomaticRelationTypes;

    public void BindData(
      List<DiplomaticRelationType> diplomaticRelationTypes)
    {
      this._DiplomaticRelationTypes = diplomaticRelationTypes;
      this.Items.Clear();
      if (this._DiplomaticRelationTypes == null)
        return;
      for (int index = 0; index < this._DiplomaticRelationTypes.Count; ++index)
        this.Items.Add((object) this._DiplomaticRelationTypes[index]);
    }

    public DiplomaticRelationType SelectedDiplomaticRelationType
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex < 0 || this._DiplomaticRelationTypes == null || selectedIndex < 0 || selectedIndex >= this._DiplomaticRelationTypes.Count ? DiplomaticRelationType.None : this._DiplomaticRelationTypes[selectedIndex];
      }
    }

    public void SetSelectedDiplomaticRelationType(DiplomaticRelationType diplomaticRelationType)
    {
      int num = -1;
      if (this._DiplomaticRelationTypes != null)
        num = this._DiplomaticRelationTypes.IndexOf(diplomaticRelationType);
      this.SelectedIndex = num;
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 21;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      string empty = string.Empty;
      if (this._DiplomaticRelationTypes != null && this._DiplomaticRelationTypes.Count > 0 && e.Index >= 0)
      {
        int index = e.Index;
        SolidBrush solidBrush = (SolidBrush) null;
        switch (this._DiplomaticRelationTypes[index])
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
        }
        string s = Galaxy.ResolveDescription(this._DiplomaticRelationTypes[index]);
        e.Graphics.DrawString(s, font, (Brush) solidBrush, point);
      }
      e.DrawFocusRectangle();
    }
  }
}
