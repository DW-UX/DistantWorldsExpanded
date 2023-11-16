// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ListBase
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System.Drawing;
using System.Windows.Forms;

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

    public new Color BackColor
    {
      get => base.BackColor;
      set
      {
      }
    }

    public new Color ForeColor
    {
      get => base.ForeColor;
      set
      {
      }
    }

    public new int Height
    {
      get => base.Height;
      set
      {
      }
    }

    public new BorderStyle BorderStyle
    {
      get => base.BorderStyle;
      set
      {
      }
    }

    public new bool MultiColumn
    {
      get => base.MultiColumn;
      set
      {
      }
    }

    public new Font Font
    {
      get => base.Font;
      set
      {
      }
    }
  }
}
