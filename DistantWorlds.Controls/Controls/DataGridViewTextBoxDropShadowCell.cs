// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DataGridViewTextBoxDropShadowCell
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DataGridViewTextBoxDropShadowCell : DataGridViewTextBoxCell
  {
    private bool _UseDropShadow = true;
    private Color _DropShadowColor = Color.FromArgb(170, 170, 170);
    private int _Amount;
    private int _MaximumAmount;

    public Color DropShadowColor
    {
      get => this._DropShadowColor;
      set => this._DropShadowColor = value;
    }

    public bool UseDropShadow
    {
      get => this._UseDropShadow;
      set => this._UseDropShadow = value;
    }

    public int Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    public int MaximumAmount
    {
      get => this._MaximumAmount;
      set => this._MaximumAmount = value;
    }

    private static bool PartPainted(
      DataGridViewPaintParts paintParts,
      DataGridViewPaintParts paintPart)
    {
      return (paintParts & paintPart) != DataGridViewPaintParts.None;
    }

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
      if (!this._UseDropShadow)
      {
        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
      }
      else
      {
        Point currentCellAddress = this.DataGridView.CurrentCellAddress;
        if (currentCellAddress.X == this.ColumnIndex && currentCellAddress.Y == rowIndex && this.DataGridView.EditingControl != null)
          return;
        if (DataGridViewTextBoxDropShadowCell.PartPainted(paintParts, DataGridViewPaintParts.ContentForeground))
        {
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
          graphics.FillRectangle((Brush) solidBrush1, cellBounds);
          if (this._Amount > 0 && this._MaximumAmount > 0)
          {
            int width = Math.Max(1, (int) ((double) this._Amount * ((double) (cellBounds.Width - 4) / (double) this._MaximumAmount)) - 4);
            Rectangle rect = new Rectangle(cellBounds.X, cellBounds.Y + 3, width, cellBounds.Height - 6);
            Color color1_1 = Color.FromArgb(32, (int) color2.R, (int) color2.G, (int) color2.B);
            Color color2_1 = Color.FromArgb(128, (int) color2.R, (int) color2.G, (int) color2.B);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Rectangle(rect.X - 1, rect.Y, rect.Width + 2, rect.Height), color1_1, color2_1, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            graphics.FillRectangle((Brush) linearGradientBrush, rect);
          }
          SolidBrush solidBrush2 = new SolidBrush(color2);
          SizeF sizeF = graphics.MeasureString(formattedValue as string, cellStyle.Font);
          float x = (float) (cellBounds.X + 2);
          float num = (float) cellBounds.Y + (float) (((double) cellBounds.Height - (double) sizeF.Height) / 2.0);
          Font font = new Font(cellStyle.Font.FontFamily, 17f, FontStyle.Bold, GraphicsUnit.Pixel);
          SolidBrush solidBrush3 = new SolidBrush(this.SelectComplimentaryShadowColor(color2, color1));
          graphics.DrawString(formattedValue as string, font, (Brush) solidBrush3, x + 1f, num + 2f, StringFormat.GenericTypographic);
          graphics.DrawString(formattedValue as string, font, (Brush) solidBrush2, x, num + 1f, StringFormat.GenericTypographic);
        }
        if (!DataGridViewTextBoxDropShadowCell.PartPainted(paintParts, DataGridViewPaintParts.ErrorIcon))
          return;
        base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, DataGridViewPaintParts.ErrorIcon);
      }
    }

    private Color SelectComplimentaryShadowColor(Color textColor, Color backColor)
    {
      Color color = Color.FromArgb(224, 224, 224);
      color = Color.Black;
      if (textColor.ToArgb() == Color.FromArgb(1, 1, 1).ToArgb())
        color = Color.FromArgb(128, 128, 128);
      return Galaxy.DetermineContrastDropShadowColor(textColor);
    }
  }
}
