// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DiplomaticRelationCell
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
  public class DiplomaticRelationCell : DataGridViewComboBoxCell
  {
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

    public override void InitializeEditingControl(
      int rowIndex,
      object initialFormattedValue,
      DataGridViewCellStyle dataGridViewCellStyle)
    {
      base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
      DiplomaticRelationTypeDropDown editingControl = this.DataGridView.EditingControl as DiplomaticRelationTypeDropDown;
      editingControl.Ignite();
      if (this.Value is DiplomaticRelationType)
      {
        editingControl.SetSelectedDiplomaticRelationType((object) (DiplomaticRelationType) this.Value);
      }
      else
      {
        if (!(this.Value is PirateRelationType))
          return;
        editingControl.SetSelectedDiplomaticRelationType((object) (PirateRelationType) this.Value);
      }
    }

    public override Type EditType => typeof (DiplomaticRelationTypeDropDown);

    public override Type ValueType
    {
      get
      {
        if (this.Value is DiplomaticRelationType)
          return typeof (DiplomaticRelationType);
        return this.Value is PirateRelationType ? typeof (PirateRelationType) : typeof (DiplomaticRelationType);
      }
    }

    public override object DefaultNewRowValue => (object) DiplomaticRelationType.NotMet;

    protected override void Paint(
      Graphics graphics,
      Rectangle clipBounds,
      Rectangle cellBounds,
      int rowIndex,
      DataGridViewElementStates elementState,
      object value,
      object formattedValue,
      string errorText,
      DataGridViewCellStyle cellStyle,
      DataGridViewAdvancedBorderStyle advancedBorderStyle,
      DataGridViewPaintParts paintParts)
    {
      if (this.DataGridView == null)
        return;
      base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon));
      Point currentCellAddress = this.DataGridView.CurrentCellAddress;
      if (currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == rowIndex && this.DataGridView.EditingControl != null || !DiplomaticRelationCell.PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
        return;
      Color color1;
      Color color2;
      if ((elementState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
      {
        color1 = cellStyle.SelectionBackColor;
        color2 = cellStyle.SelectionForeColor;
      }
      else if (rowIndex % 2 == 1 && this.DataGridView.AlternatingRowsDefaultCellStyle != null)
      {
        color1 = this.DataGridView.AlternatingRowsDefaultCellStyle.BackColor;
        color2 = this.DataGridView.AlternatingRowsDefaultCellStyle.ForeColor;
      }
      else
      {
        color1 = cellStyle.BackColor;
        color2 = cellStyle.ForeColor;
      }
      SolidBrush solidBrush1 = new SolidBrush(color1);
      graphics.FillRectangle((Brush) solidBrush1, cellBounds);
      SolidBrush solidBrush2 = new SolidBrush(color2);
      SizeF sizeF = graphics.MeasureString(formattedValue as string, cellStyle.Font);
      float x = (float) (cellBounds.X + 2);
      float num = (float) cellBounds.Y + (float) (((double) cellBounds.Height - (double) sizeF.Height) / 2.0);
      switch (value)
      {
        case DiplomaticRelationType diplomaticRelationType1:
          solidBrush2 = this.SelectBrush(diplomaticRelationType1);
          break;
        case PirateRelationType diplomaticRelationType2:
          solidBrush2 = this.SelectBrush(diplomaticRelationType2);
          break;
      }
      Font font = new Font("Verdana", 8.25f, FontStyle.Bold);
      graphics.DrawString(formattedValue as string, font, (Brush) solidBrush2, x, num + 1f);
    }

    private static bool PartPainted(
      DataGridViewPaintParts paintParts,
      DataGridViewPaintParts paintPart)
    {
      return (paintParts & paintPart) != DataGridViewPaintParts.None;
    }

    private SolidBrush SelectBrush(PirateRelationType diplomaticRelationType)
    {
      SolidBrush solidBrush = (SolidBrush) null;
      switch (diplomaticRelationType)
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

    protected override object GetFormattedValue(
      object value,
      int rowIndex,
      ref DataGridViewCellStyle cellStyle,
      TypeConverter valueTypeConverter,
      TypeConverter formattedValueTypeConverter,
      DataGridViewDataErrorContexts context)
    {
      cellStyle.BackColor = Color.FromArgb(32, 32, 40);
      switch (value)
      {
        case DiplomaticRelationType diplomaticRelationType1:
          SolidBrush solidBrush1 = this.SelectBrush(diplomaticRelationType1);
          cellStyle.ForeColor = solidBrush1.Color;
          return (object) Galaxy.ResolveDescription(diplomaticRelationType1);
        case PirateRelationType diplomaticRelationType2:
          SolidBrush solidBrush2 = this.SelectBrush(diplomaticRelationType2);
          cellStyle.ForeColor = solidBrush2.Color;
          string formattedValue = string.Empty;
          switch (diplomaticRelationType2)
          {
            case PirateRelationType.NotMet:
              formattedValue = TextResolver.GetText("DiplomaticRelationType NotMet");
              break;
            case PirateRelationType.None:
              formattedValue = TextResolver.GetText("None");
              break;
            case PirateRelationType.Protection:
              formattedValue = TextResolver.GetText("Pirate Protection");
              break;
          }
          return (object) formattedValue;
        case null:
          return (object) string.Empty;
        default:
          return (object) value.ToString();
      }
    }
  }
}
