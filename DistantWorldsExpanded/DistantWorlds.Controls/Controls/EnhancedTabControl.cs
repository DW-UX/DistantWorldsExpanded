// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EnhancedTabControl
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class EnhancedTabControl : TabControl
  {
    public EnhancedTabControl()
    {
      this.Font = new Font("Verdana", 8f, FontStyle.Regular);
      this.DrawMode = TabDrawMode.OwnerDrawFixed;
      this.DrawItem += new DrawItemEventHandler(this.tab_DrawItem);
    }

    protected override void OnPaintBackground(PaintEventArgs pevent) => base.OnPaintBackground(pevent);

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(64, 64, 80)), this.Bounds);
    }

    private void tab_DrawItem(object sender, DrawItemEventArgs e)
    {
      Color white = Color.White;
      Color color = Color.FromArgb(64, 64, 80);
      e.Graphics.FillRectangle((Brush) new SolidBrush(Color.FromArgb(128, 128, 144)), this.ClientRectangle);
      e.Graphics.FillRectangle((Brush) new SolidBrush(color), new Rectangle(3, 21, this.Width - 6, 5));
      for (int index = 0; index < this.TabCount; ++index)
      {
        string text = this.TabPages[index].Text;
        Rectangle tabRect = this.GetTabRect(index);
        DrawItemState state = DrawItemState.None;
        if (this.SelectedIndex == index)
          state = DrawItemState.Selected;
        this.DrawTabText((TabControl) this, new DrawItemEventArgs(e.Graphics, e.Font, tabRect, index, state, white, color), color, white, text);
      }
    }

    public void DrawTabText(
      TabControl tabControl,
      DrawItemEventArgs e,
      Color backColor,
      Color foreColor,
      string caption)
    {
      Font font = e.Font;
      if (e.Index == tabControl.SelectedIndex)
      {
        backColor = Color.FromArgb(48, 48, 64);
        foreColor = Color.Yellow;
        font = new Font(e.Font, FontStyle.Regular);
      }
      Brush brush1 = (Brush) new SolidBrush(foreColor);
      Rectangle rectangle = e.Bounds;
      Brush brush2 = (Brush) new SolidBrush(backColor);
      string text = tabControl.TabPages[e.Index].Text;
      StringFormat format = new StringFormat();
      format.Alignment = StringAlignment.Center;
      e.Graphics.FillRectangle(brush2, rectangle);
      e.Graphics.DrawRectangle(new Pen(Color.FromArgb(128, 128, 144)), rectangle);
      e.Graphics.FillRectangle(brush2, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - 1, rectangle.Width, 2));
      rectangle = new Rectangle(rectangle.X, rectangle.Y + 3, rectangle.Width, rectangle.Height - 3);
      e.Graphics.DrawString(caption, font, brush1, (RectangleF) rectangle, format);
      format.Dispose();
      if (e.Index == tabControl.SelectedIndex)
      {
        font.Dispose();
        brush2.Dispose();
      }
      else
      {
        brush2.Dispose();
        brush1.Dispose();
      }
    }
  }
}
