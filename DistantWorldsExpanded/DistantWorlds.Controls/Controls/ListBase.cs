// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ListBase
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DistantWorlds.Controls
{
  public abstract class ListBase : ListBox
  {
    public ListBase()
    {
      base.BackColor = Color.FromArgb(0, 0, 72);
      base.ForeColor = Color.White;
      base.Font = new Font("Verdana", 7.5f, FontStyle.Regular, GraphicsUnit.Pixel);
      this.MultiColumn = false;
      this.BorderStyle = BorderStyle.FixedSingle;
      this.Height = 20;
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Color BackColor
    {
      get => base.BackColor;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Color ForeColor
    {
      get => base.ForeColor;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new int Height
    {
      get => base.Height;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new BorderStyle BorderStyle
    {
      get => base.BorderStyle;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new bool MultiColumn
    {
      get => base.MultiColumn;
      set
      {
      }
    }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public new Font Font
    {
      get => base.Font;
      set
      {
      }
    }
  }
}
