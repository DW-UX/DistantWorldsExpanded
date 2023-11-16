// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DesignImageScalingModeDropDown
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class DesignImageScalingModeDropDown : ComboBox
  {
    private List<DesignImageScalingMode> _DesignImageScalingModes = new List<DesignImageScalingMode>();

    public DesignImageScalingModeDropDown()
    {
      this.DrawMode = DrawMode.OwnerDrawFixed;
      this.DropDownStyle = ComboBoxStyle.DropDownList;
      this.FlatStyle = FlatStyle.Popup;
      this.BackColor = Color.FromArgb(48, 48, 64);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Font = new Font("Verdana", 8.25f);
    }

    public void Ignite()
    {
      if (this._DesignImageScalingModes.Count > 0)
        return;
      this._DesignImageScalingModes.Add(DesignImageScalingMode.None);
      this._DesignImageScalingModes.Add(DesignImageScalingMode.Absolute);
      this._DesignImageScalingModes.Add(DesignImageScalingMode.Scaled);
      this.Items.Clear();
      for (int index = 0; index < this._DesignImageScalingModes.Count; ++index)
        this.Items.Add((object) this._DesignImageScalingModes[index]);
    }

    public void SetSelectedScalingMode(DesignImageScalingMode scalingMode)
    {
      int num = -1;
      if (this._DesignImageScalingModes != null)
      {
        for (int index = 0; index < this._DesignImageScalingModes.Count; ++index)
        {
          if (this._DesignImageScalingModes[index] == scalingMode)
          {
            num = index;
            break;
          }
        }
      }
      this.SelectedIndex = num;
    }

    public DesignImageScalingMode SelectedScalingMode
    {
      get
      {
        int selectedIndex = this.SelectedIndex;
        return selectedIndex >= 0 ? this._DesignImageScalingModes[selectedIndex] : DesignImageScalingMode.None;
      }
    }

    protected override void OnMeasureItem(MeasureItemEventArgs e)
    {
      base.OnMeasureItem(e);
      e.ItemHeight = 19;
    }

    protected override void OnDrawItem(DrawItemEventArgs e)
    {
      base.OnDrawItem(e);
      e.DrawBackground();
      PointF point = new PointF(0.0f, (float) (e.Bounds.Y + 1));
      Font font = this.Font;
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
      {
        int num = 0;
        if (this._DesignImageScalingModes != null)
          num = this._DesignImageScalingModes.Count;
        if (this._DesignImageScalingModes != null)
        {
          if (e.Index >= 0)
          {
            if (e.Index < num)
              e.Graphics.DrawString(Galaxy.ResolveDescription(this._DesignImageScalingModes[e.Index]), font, (Brush) solidBrush, point);
          }
        }
      }
      e.DrawFocusRectangle();
    }
  }
}
