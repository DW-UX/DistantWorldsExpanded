// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DataGridViewNumericUpDownEditingControl
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  internal class DataGridViewNumericUpDownEditingControl : NumericUpDown, IDataGridViewEditingControl
  {
    private DataGridView dataGridView;
    private bool valueChanged;
    private int rowIndex;

    [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    public DataGridViewNumericUpDownEditingControl() => this.TabStop = false;

    public virtual DataGridView EditingControlDataGridView
    {
      get => this.dataGridView;
      set => this.dataGridView = value;
    }

    public virtual object EditingControlFormattedValue
    {
      get => this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
      set => this.Text = (string) value;
    }

    public virtual int EditingControlRowIndex
    {
      get => this.rowIndex;
      set => this.rowIndex = value;
    }

    public virtual bool EditingControlValueChanged
    {
      get => this.valueChanged;
      set => this.valueChanged = value;
    }

    public virtual Cursor EditingPanelCursor => Cursors.Default;

    public virtual bool RepositionEditingControlOnValueChange => false;

    public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
    {
      this.Font = dataGridViewCellStyle.Font;
      if (dataGridViewCellStyle.BackColor.A < byte.MaxValue)
      {
        Color color = Color.FromArgb((int) byte.MaxValue, dataGridViewCellStyle.BackColor);
        this.BackColor = color;
        this.dataGridView.EditingPanel.BackColor = color;
      }
      else
        this.BackColor = dataGridViewCellStyle.BackColor;
      this.ForeColor = dataGridViewCellStyle.ForeColor;
      this.TextAlign = DataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment);
    }

    public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
    {
      switch (keyData & Keys.KeyCode)
      {
        case Keys.End:
        case Keys.Home:
          if (this.Controls[1] is TextBox control1 && control1.SelectionLength != control1.Text.Length)
            return true;
          break;
        case Keys.Left:
          if (this.Controls[1] is TextBox control2 && (this.RightToLeft == RightToLeft.No && (control2.SelectionLength != 0 || control2.SelectionStart != 0) || this.RightToLeft == RightToLeft.Yes && (control2.SelectionLength != 0 || control2.SelectionStart != control2.Text.Length)))
            return true;
          break;
        case Keys.Up:
          if (this.Value < this.Maximum)
            return true;
          break;
        case Keys.Right:
          if (this.Controls[1] is TextBox control3 && (this.RightToLeft == RightToLeft.No && (control3.SelectionLength != 0 || control3.SelectionStart != control3.Text.Length) || this.RightToLeft == RightToLeft.Yes && (control3.SelectionLength != 0 || control3.SelectionStart != 0)))
            return true;
          break;
        case Keys.Down:
          if (this.Value > this.Minimum)
            return true;
          break;
        case Keys.Delete:
          if (this.Controls[1] is TextBox control4 && (control4.SelectionLength > 0 || control4.SelectionStart < control4.Text.Length))
            return true;
          break;
      }
      return !dataGridViewWantsInputKey;
    }

    public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
    {
      bool userEdit = this.UserEdit;
      try
      {
        this.UserEdit = (context & DataGridViewDataErrorContexts.Display) == (DataGridViewDataErrorContexts) 0;
        return (object) this.Value.ToString((this.ThousandsSeparator ? "N" : "F") + this.DecimalPlaces.ToString());
      }
      finally
      {
        this.UserEdit = userEdit;
      }
    }

    public virtual void PrepareEditingControlForEdit(bool selectAll)
    {
      if (!(this.Controls[1] is TextBox control))
        return;
      if (selectAll)
        control.SelectAll();
      else
        control.SelectionStart = control.Text.Length;
    }

    private void NotifyDataGridViewOfValueChange()
    {
      if (this.valueChanged)
        return;
      this.valueChanged = true;
      this.dataGridView.NotifyCurrentCellDirty(true);
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      bool flag = false;
      if (char.IsDigit(e.KeyChar))
      {
        flag = true;
      }
      else
      {
        NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
        string decimalSeparator = numberFormat.NumberDecimalSeparator;
        string numberGroupSeparator = numberFormat.NumberGroupSeparator;
        string negativeSign = numberFormat.NegativeSign;
        if (!string.IsNullOrEmpty(decimalSeparator) && decimalSeparator.Length == 1)
          flag = (int) decimalSeparator[0] == (int) e.KeyChar;
        if (!flag && !string.IsNullOrEmpty(numberGroupSeparator) && numberGroupSeparator.Length == 1)
          flag = (int) numberGroupSeparator[0] == (int) e.KeyChar;
        if (!flag && !string.IsNullOrEmpty(negativeSign) && negativeSign.Length == 1)
          flag = (int) negativeSign[0] == (int) e.KeyChar;
      }
      if (!flag)
        return;
      this.NotifyDataGridViewOfValueChange();
    }

    protected override void OnValueChanged(EventArgs e)
    {
      base.OnValueChanged(e);
      if (!this.Focused)
        return;
      this.NotifyDataGridViewOfValueChange();
    }

    protected override bool ProcessKeyEventArgs(ref Message m)
    {
      if (!(this.Controls[1] is TextBox control))
        return base.ProcessKeyEventArgs(ref m);
      DataGridViewNumericUpDownEditingControl.SendMessage(control.Handle, m.Msg, m.WParam, m.LParam);
      return true;
    }
  }
}
