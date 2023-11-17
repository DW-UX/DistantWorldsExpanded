// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DataGridViewNumericUpDownCell
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class DataGridViewNumericUpDownCell : DataGridViewTextBoxCell
    {
        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapWidth = 100;
        private const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultRenderingBitmapHeight = 22;
        internal const int DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces = 0;
        internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement = 1M;
        internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum = 100M;
        internal const Decimal DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum = 0M;
        internal const bool DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator = false;
        private static readonly DataGridViewContentAlignment anyRight = DataGridViewContentAlignment.TopRight | DataGridViewContentAlignment.MiddleRight | DataGridViewContentAlignment.BottomRight;
        private static readonly DataGridViewContentAlignment anyCenter = DataGridViewContentAlignment.TopCenter | DataGridViewContentAlignment.MiddleCenter | DataGridViewContentAlignment.BottomCenter;
        private static Type defaultEditType;
        private static Type defaultValueType;
        [ThreadStatic]
        private static Bitmap renderingBitmap;
        [ThreadStatic]
        private static NumericUpDown paintingNumericUpDown;
        private int decimalPlaces;
        private Decimal increment;
        private Decimal minimum;
        private Decimal maximum;
        private bool thousandsSeparator;

        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern short VkKeyScan(char key);

        public DataGridViewNumericUpDownCell()
        {
            if (DataGridViewNumericUpDownCell.renderingBitmap == null)
                DataGridViewNumericUpDownCell.renderingBitmap = new Bitmap(100, 22);
            if (DataGridViewNumericUpDownCell.paintingNumericUpDown == null)
            {
                DataGridViewNumericUpDownCell.paintingNumericUpDown = new NumericUpDown();
                DataGridViewNumericUpDownCell.paintingNumericUpDown.BorderStyle = BorderStyle.None;
                DataGridViewNumericUpDownCell.paintingNumericUpDown.Maximum = 7922816251426433759354395034M;
                DataGridViewNumericUpDownCell.paintingNumericUpDown.Minimum = -7922816251426433759354395034M;
            }
            this.decimalPlaces = 0;
            this.increment = 1M;
            this.minimum = 0M;
            this.maximum = 100M;
            this.thousandsSeparator = false;
        }

        [DefaultValue(0)]
        public int DecimalPlaces
        {
            get => this.decimalPlaces;
            set
            {
                if (value < 0 || value > 99)
                    throw new ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.");
                if (this.decimalPlaces == value)
                    return;
                this.SetDecimalPlaces(this.RowIndex, value);
                this.OnCommonChange();
            }
        }

        private DataGridViewNumericUpDownEditingControl EditingNumericUpDown => this.DataGridView.EditingControl as DataGridViewNumericUpDownEditingControl;

        public override Type EditType => DataGridViewNumericUpDownCell.defaultEditType;

        public Decimal Increment
        {
            get => this.increment;
            set
            {
                if (value < 0M)
                    throw new ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.");
                this.SetIncrement(this.RowIndex, value);
            }
        }

        public Decimal Maximum
        {
            get => this.maximum;
            set
            {
                if (!(this.maximum != value))
                    return;
                this.SetMaximum(this.RowIndex, value);
                this.OnCommonChange();
            }
        }

        public Decimal Minimum
        {
            get => this.minimum;
            set
            {
                if (!(this.minimum != value))
                    return;
                this.SetMinimum(this.RowIndex, value);
                this.OnCommonChange();
            }
        }

        [DefaultValue(false)]
        public bool ThousandsSeparator
        {
            get => this.thousandsSeparator;
            set
            {
                if (this.thousandsSeparator == value)
                    return;
                this.SetThousandsSeparator(this.RowIndex, value);
                this.OnCommonChange();
            }
        }

        public override Type ValueType
        {
            get
            {
                Type valueType = base.ValueType;
                return valueType != (Type)null ? valueType : DataGridViewNumericUpDownCell.defaultValueType;
            }
        }

        public override object Clone()
        {
            if (base.Clone() is DataGridViewNumericUpDownCell numericUpDownCell)
            {
                numericUpDownCell.DecimalPlaces = this.DecimalPlaces;
                numericUpDownCell.Increment = this.Increment;
                numericUpDownCell.Maximum = this.Maximum;
                numericUpDownCell.Minimum = this.Minimum;
                numericUpDownCell.ThousandsSeparator = this.ThousandsSeparator;
                return numericUpDownCell;
            }
            return null;
        }

        private Decimal Constrain(Decimal value)
        {
            Debug.Assert(this.minimum <= this.maximum);
            if (value < this.minimum)
                value = this.minimum;
            if (value > this.maximum)
                value = this.maximum;
            return value;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public override void DetachEditingControl()
        {
            DataGridView dataGridView = this.DataGridView;
            if (dataGridView == null || dataGridView.EditingControl == null)
                throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
            if (dataGridView.EditingControl is NumericUpDown editingControl && editingControl.Controls[1] is TextBox control)
                control.ClearUndo();
            base.DetachEditingControl();
        }

        private Rectangle GetAdjustedEditingControlBounds(
          Rectangle editingControlBounds,
          DataGridViewCellStyle cellStyle)
        {
            ++editingControlBounds.X;
            editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 2);
            int num = cellStyle.Font.Height + 3;
            if (num < editingControlBounds.Height)
            {
                switch (cellStyle.Alignment)
                {
                    case DataGridViewContentAlignment.MiddleLeft:
                    case DataGridViewContentAlignment.MiddleCenter:
                    case DataGridViewContentAlignment.MiddleRight:
                        editingControlBounds.Y += (editingControlBounds.Height - num) / 2;
                        break;
                    case DataGridViewContentAlignment.BottomLeft:
                    case DataGridViewContentAlignment.BottomCenter:
                    case DataGridViewContentAlignment.BottomRight:
                        editingControlBounds.Y += editingControlBounds.Height - num;
                        break;
                }
            }
            return editingControlBounds;
        }

        protected override Rectangle GetErrorIconBounds(
          Graphics graphics,
          DataGridViewCellStyle cellStyle,
          int rowIndex)
        {
            Rectangle errorIconBounds = base.GetErrorIconBounds(graphics, cellStyle, rowIndex);
            errorIconBounds.X = this.DataGridView.RightToLeft != RightToLeft.Yes ? errorIconBounds.Left - 16 : errorIconBounds.Left + 16;
            return errorIconBounds;
        }

        protected override object GetFormattedValue(
          object value,
          int rowIndex,
          ref DataGridViewCellStyle cellStyle,
          TypeConverter valueTypeConverter,
          TypeConverter formattedValueTypeConverter,
          DataGridViewDataErrorContexts context)
        {
            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        protected override Size GetPreferredSize(
          Graphics graphics,
          DataGridViewCellStyle cellStyle,
          int rowIndex,
          Size constraintSize)
        {
            if (this.DataGridView == null)
                return new Size(-1, -1);
            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            if (constraintSize.Width == 0)
                preferredSize.Width += 24;
            return preferredSize;
        }

        public override void InitializeEditingControl(
          int rowIndex,
          object initialFormattedValue,
          DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (!(this.DataGridView.EditingControl is NumericUpDown editingControl))
                return;
            editingControl.BorderStyle = BorderStyle.None;
            editingControl.DecimalPlaces = this.DecimalPlaces;
            editingControl.Increment = this.Increment;
            editingControl.Maximum = this.Maximum;
            editingControl.Minimum = this.Minimum;
            editingControl.ThousandsSeparator = this.ThousandsSeparator;
            if (this.Value is long)
                editingControl.Value = (Decimal)(long)this.Value;
            else if (this.Value is float)
                editingControl.Value = (Decimal)(float)this.Value;
            else
                editingControl.Value = (Decimal)(double)this.Value;
        }

        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            Keys keys = Keys.None;
            string negativeSign = numberFormat.NegativeSign;
            if (!string.IsNullOrEmpty(negativeSign) && negativeSign.Length == 1)
                keys = (Keys)DataGridViewNumericUpDownCell.VkKeyScan(negativeSign[0]);
            return (char.IsDigit((char)e.KeyCode) || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9 || keys == e.KeyCode || Keys.Subtract == e.KeyCode) && !e.Shift && !e.Alt && !e.Control;
        }

        private new void OnCommonChange()
        {
            if (this.DataGridView == null || this.DataGridView.IsDisposed || this.DataGridView.Disposing)
                return;
            if (this.RowIndex == -1)
                this.DataGridView.InvalidateColumn(this.ColumnIndex);
            else
                this.DataGridView.UpdateCellValue(this.ColumnIndex, this.RowIndex);
        }

        private bool OwnsEditingNumericUpDown(int rowIndex) => rowIndex != -1 && this.DataGridView != null && this.DataGridView.EditingControl is DataGridViewNumericUpDownEditingControl editingControl && rowIndex == editingControl.EditingControlRowIndex;

        protected override void Paint(
          Graphics graphics,
          Rectangle clipBounds,
          Rectangle cellBounds,
          int rowIndex,
          DataGridViewElementStates cellState,
          object value,
          object formattedValue,
          string errorText,
          DataGridViewCellStyle cellStyle,
          DataGridViewAdvancedBorderStyle advancedBorderStyle,
          DataGridViewPaintParts paintParts)
        {
            if (this.DataGridView == null)
                return;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts & ~(DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon));
            Point currentCellAddress = this.DataGridView.CurrentCellAddress;
            if (currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == rowIndex && this.DataGridView.EditingControl != null)
                return;
            if (DataGridViewNumericUpDownCell.PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
            {
                Rectangle rectangle1 = this.BorderWidths(advancedBorderStyle);
                Rectangle rectangle2 = cellBounds;
                rectangle2.Offset(rectangle1.X, rectangle1.Y);
                rectangle2.Width -= rectangle1.Right;
                rectangle2.Height -= rectangle1.Bottom;
                if (cellStyle.Padding != Padding.Empty)
                {
                    if (this.DataGridView.RightToLeft == RightToLeft.Yes)
                        rectangle2.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
                    else
                        rectangle2.Offset(cellStyle.Padding.Left, cellStyle.Padding.Top);
                    rectangle2.Width -= cellStyle.Padding.Horizontal;
                    rectangle2.Height -= cellStyle.Padding.Vertical;
                }
                Color color1;
                Color color2;
                if ((cellState & DataGridViewElementStates.Selected) != DataGridViewElementStates.None)
                {
                    color1 = cellStyle.SelectionBackColor;
                    color2 = cellStyle.SelectionForeColor;
                }
                else
                {
                    color1 = cellStyle.BackColor;
                    color2 = cellStyle.ForeColor;
                }
                SolidBrush solidBrush1 = new SolidBrush(color1);
                graphics.FillRectangle((Brush)solidBrush1, cellBounds);
                SolidBrush solidBrush2 = new SolidBrush(color2);
                SizeF sizeF = graphics.MeasureString(formattedValue as string, cellStyle.Font);
                float x = (float)(cellBounds.X + 2);
                float num = (float)cellBounds.Y + (float)(((double)cellBounds.Height - (double)sizeF.Height) / 2.0);
                graphics.DrawString(formattedValue as string, cellStyle.Font, (Brush)solidBrush2, x, num + 1f);
            }
            if (!DataGridViewNumericUpDownCell.PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
                return;
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);
        }

        private static bool PartPainted(
          DataGridViewPaintParts paintParts,
          DataGridViewPaintParts paintPart)
        {
            return (paintParts & paintPart) != DataGridViewPaintParts.None;
        }

        public override void PositionEditingControl(
          bool setLocation,
          bool setSize,
          Rectangle cellBounds,
          Rectangle cellClip,
          DataGridViewCellStyle cellStyle,
          bool singleVerticalBorderAdded,
          bool singleHorizontalBorderAdded,
          bool isFirstDisplayedColumn,
          bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds = this.GetAdjustedEditingControlBounds(this.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow), cellStyle);
            this.DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            this.DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        internal void SetDecimalPlaces(int rowIndex, int value)
        {
            Debug.Assert(value >= 0 && value <= 99);
            this.decimalPlaces = value;
            if (!this.OwnsEditingNumericUpDown(rowIndex))
                return;
            this.EditingNumericUpDown.DecimalPlaces = value;
        }

        internal void SetIncrement(int rowIndex, Decimal value)
        {
            Debug.Assert(value >= 0M);
            this.increment = value;
            if (!this.OwnsEditingNumericUpDown(rowIndex))
                return;
            this.EditingNumericUpDown.Increment = value;
        }

        internal void SetMaximum(int rowIndex, Decimal value)
        {
            this.maximum = value;
            if (this.minimum > this.maximum)
                this.minimum = this.maximum;
            object obj = this.GetValue(rowIndex);
            if (obj != null)
            {
                Decimal num1 = Convert.ToDecimal(obj);
                Decimal num2 = this.Constrain(num1);
                if (num2 != num1)
                    this.SetValue(rowIndex, (object)num2);
            }
            Debug.Assert(this.maximum == value);
            if (!this.OwnsEditingNumericUpDown(rowIndex))
                return;
            this.EditingNumericUpDown.Maximum = value;
        }

        internal void SetMinimum(int rowIndex, Decimal value)
        {
            this.minimum = value;
            if (this.minimum > this.maximum)
                this.maximum = value;
            object obj = this.GetValue(rowIndex);
            if (obj != null)
            {
                Decimal num1 = Convert.ToDecimal(obj);
                Decimal num2 = this.Constrain(num1);
                if (num2 != num1)
                    this.SetValue(rowIndex, (object)num2);
            }
            Debug.Assert(this.minimum == value);
            if (!this.OwnsEditingNumericUpDown(rowIndex))
                return;
            this.EditingNumericUpDown.Minimum = value;
        }

        internal void SetThousandsSeparator(int rowIndex, bool value)
        {
            this.thousandsSeparator = value;
            if (!this.OwnsEditingNumericUpDown(rowIndex))
                return;
            this.EditingNumericUpDown.ThousandsSeparator = value;
        }

        public override string ToString() => "DataGridViewNumericUpDownCell { ColumnIndex=" + this.ColumnIndex.ToString((IFormatProvider)CultureInfo.CurrentCulture) + ", RowIndex=" + this.RowIndex.ToString((IFormatProvider)CultureInfo.CurrentCulture) + " }";

        internal static HorizontalAlignment TranslateAlignment(DataGridViewContentAlignment align)
        {
            if ((align & DataGridViewNumericUpDownCell.anyRight) != DataGridViewContentAlignment.NotSet)
                return HorizontalAlignment.Right;
            return (align & DataGridViewNumericUpDownCell.anyCenter) != DataGridViewContentAlignment.NotSet ? HorizontalAlignment.Center : HorizontalAlignment.Left;
        }

        static DataGridViewNumericUpDownCell()
        {
            //DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement = 1M;
            //DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum = 100M;
            //DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum = 0M;
            DataGridViewNumericUpDownCell.defaultEditType = typeof(DataGridViewNumericUpDownEditingControl);
            DataGridViewNumericUpDownCell.defaultValueType = typeof(Decimal);
        }
    }
}
