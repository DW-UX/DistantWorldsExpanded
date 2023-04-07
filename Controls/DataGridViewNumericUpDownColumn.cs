// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DataGridViewNumericUpDownColumn
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
  public class DataGridViewNumericUpDownColumn : DataGridViewColumn
  {
    public DataGridViewNumericUpDownColumn()
      : base((DataGridViewCell) new DataGridViewNumericUpDownCell())
    {
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override DataGridViewCell CellTemplate
    {
      get => base.CellTemplate;
      set
      {
        DataGridViewNumericUpDownCell numericUpDownCell = value as DataGridViewNumericUpDownCell;
        base.CellTemplate = value == null || numericUpDownCell != null ? value : throw new InvalidCastException("Value provided for CellTemplate must be of type DataGridViewNumericUpDownElements.DataGridViewNumericUpDownCell or derive from it.");
      }
    }

    [Description("Indicates the number of decimal places to display.")]
    [Category("Appearance")]
    [DefaultValue(0)]
    public int DecimalPlaces
    {
      get => this.NumericUpDownCellTemplate != null ? this.NumericUpDownCellTemplate.DecimalPlaces : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.NumericUpDownCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.NumericUpDownCellTemplate.DecimalPlaces = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewNumericUpDownCell cell)
            cell.SetDecimalPlaces(rowIndex, value);
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    [Category("Data")]
    [Description("Indicates the amount to increment or decrement on each button click.")]
    public Decimal Increment
    {
      get => this.NumericUpDownCellTemplate != null ? this.NumericUpDownCellTemplate.Increment : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.NumericUpDownCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.NumericUpDownCellTemplate.Increment = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewNumericUpDownCell cell)
            cell.SetIncrement(rowIndex, value);
        }
      }
    }

    private bool ShouldSerializeIncrement() => !this.Increment.Equals(1M);

    [Description("Indicates the maximum value for the numeric up-down cells.")]
    [Category("Data")]
    [RefreshProperties(RefreshProperties.All)]
    public Decimal Maximum
    {
      get => this.NumericUpDownCellTemplate != null ? this.NumericUpDownCellTemplate.Maximum : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.NumericUpDownCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.NumericUpDownCellTemplate.Maximum = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewNumericUpDownCell cell)
            cell.SetMaximum(rowIndex, value);
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    private bool ShouldSerializeMaximum() => !this.Maximum.Equals(100M);

    [Description("Indicates the minimum value for the numeric up-down cells.")]
    [RefreshProperties(RefreshProperties.All)]
    [Category("Data")]
    public Decimal Minimum
    {
      get => this.NumericUpDownCellTemplate != null ? this.NumericUpDownCellTemplate.Minimum : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.NumericUpDownCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.NumericUpDownCellTemplate.Minimum = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewNumericUpDownCell cell)
            cell.SetMinimum(rowIndex, value);
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    private bool ShouldSerializeMinimum() => !this.Minimum.Equals(0M);

    [Category("Data")]
    [DefaultValue(false)]
    [Description("Indicates whether the thousands separator will be inserted between every three decimal digits.")]
    public bool ThousandsSeparator
    {
      get => this.NumericUpDownCellTemplate != null ? this.NumericUpDownCellTemplate.ThousandsSeparator : throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
      set
      {
        if (this.NumericUpDownCellTemplate == null)
          throw new InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.");
        this.NumericUpDownCellTemplate.ThousandsSeparator = value;
        if (this.DataGridView == null)
          return;
        DataGridViewRowCollection rows = this.DataGridView.Rows;
        int count = rows.Count;
        for (int rowIndex = 0; rowIndex < count; ++rowIndex)
        {
          if (rows.SharedRow(rowIndex).Cells[this.Index] is DataGridViewNumericUpDownCell cell)
            cell.SetThousandsSeparator(rowIndex, value);
        }
        this.DataGridView.InvalidateColumn(this.Index);
      }
    }

    private DataGridViewNumericUpDownCell NumericUpDownCellTemplate => (DataGridViewNumericUpDownCell) this.CellTemplate;

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder(100);
      stringBuilder.Append("DataGridViewNumericUpDownColumn { Name=");
      stringBuilder.Append(this.Name);
      stringBuilder.Append(", Index=");
      stringBuilder.Append(this.Index.ToString((IFormatProvider) CultureInfo.CurrentCulture));
      stringBuilder.Append(" }");
      return stringBuilder.ToString();
    }
  }
}
