// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DataGridViewTextBoxDropShadowColumn
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DataGridViewTextBoxDropShadowColumn : DataGridViewColumn
  {
    public DataGridViewTextBoxDropShadowColumn()
      : base((DataGridViewCell) new DataGridViewTextBoxDropShadowCell())
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override DataGridViewCell CellTemplate
    {
      get => base.CellTemplate;
      set
      {
        DataGridViewTextBoxDropShadowCell boxDropShadowCell = value as DataGridViewTextBoxDropShadowCell;
        base.CellTemplate = value == null || boxDropShadowCell != null ? value : throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewTextBoxDropShadowElements.DataGridViewTextBoxDropShadowCell or derive from it.");
      }
    }

    public int MaximumAmount
    {
      get => this.TextBoxDropShadowCellTemplate != null ? this.TextBoxDropShadowCellTemplate.MaximumAmount : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.TextBoxDropShadowCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.TextBoxDropShadowCellTemplate.MaximumAmount = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewTextBoxDropShadowCell cell)
            cell.MaximumAmount = value;
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    public bool UseDropShadow
    {
      get => this.TextBoxDropShadowCellTemplate != null ? this.TextBoxDropShadowCellTemplate.UseDropShadow : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.TextBoxDropShadowCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.TextBoxDropShadowCellTemplate.UseDropShadow = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewTextBoxDropShadowCell cell)
            cell.UseDropShadow = value;
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    private DataGridViewTextBoxDropShadowCell TextBoxDropShadowCellTemplate => (DataGridViewTextBoxDropShadowCell) this.CellTemplate;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(100);
      stringBuilder.Append("DataGridViewTextBoxDropShadowColumn { Name=");
      stringBuilder.Append(this.Name);
      stringBuilder.Append(", Index=");
      stringBuilder.Append(this.Index.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      stringBuilder.Append(" }");
      return stringBuilder.ToString();
    }
  }
}
