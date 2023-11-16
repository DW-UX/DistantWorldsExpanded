// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CornerCtrl
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls
{
  public class CornerCtrl : BufferPaintingCtrl
  {
    protected CornerStyle cornerStyle = CornerStyle.Rounded;
    protected int cornerSquare;
    protected Color borderColor = Color.Gray;
    protected GraphicsPath graphicPath;
    protected GraphicsPath regionPath;

    protected virtual void InitializeGraphicPath()
    {
      if (this.graphicPath != null)
      {
        this.graphicPath.Dispose();
        this.graphicPath = (GraphicsPath) null;
      }
      if (this.regionPath != null)
      {
        this.regionPath.Dispose();
        this.regionPath = (GraphicsPath) null;
      }
      this.graphicPath = new GraphicsPath();
      this.regionPath = new GraphicsPath();
      switch (this.cornerStyle)
      {
        case CornerStyle.Normal:
          this.graphicPath.AddLine(0, 0, this.Width - 1, 0);
          this.regionPath.AddLine(0, 0, this.Width, 0);
          this.graphicPath.AddLine(this.Width - 1, 0, this.Width - 1, this.Height - 1);
          this.regionPath.AddLine(this.Width, 0, this.Width, this.Height);
          this.graphicPath.AddLine(this.Width - 1, this.Height - 1, 0, this.Height - 1);
          this.regionPath.AddLine(this.Width, this.Height, 0, this.Height);
          this.graphicPath.AddLine(0, this.Height - 1, 0, 0);
          this.regionPath.AddLine(0, this.Height, 0, 0);
          break;
        case CornerStyle.Rounded:
          this.graphicPath.AddArc(0, 0, this.cornerSquare, this.cornerSquare, 180f, 90f);
          this.regionPath.AddArc(0, 0, this.cornerSquare, this.cornerSquare, 180f, 90f);
          this.graphicPath.AddLine(this.cornerSquare - this.cornerSquare / 2, 0, this.Width - this.cornerSquare + this.cornerSquare / 2 - 1, 0);
          this.regionPath.AddLine(this.cornerSquare - this.cornerSquare / 2, 0, this.Width - this.cornerSquare + this.cornerSquare / 2, 0);
          this.graphicPath.AddArc(this.Width - this.cornerSquare - 1, 0, this.cornerSquare, this.cornerSquare, -90f, 90f);
          this.regionPath.AddArc(this.Width - this.cornerSquare, 0, this.cornerSquare, this.cornerSquare, -90f, 90f);
          this.graphicPath.AddLine(this.Width - 1, this.cornerSquare - this.cornerSquare / 2, this.Width - 1, this.Height - this.cornerSquare + this.cornerSquare / 2);
          this.regionPath.AddLine(this.Width, this.cornerSquare - this.cornerSquare / 2, this.Width, this.Height - this.cornerSquare + this.cornerSquare / 2);
          this.graphicPath.AddArc(this.Width - this.cornerSquare - 1, this.Height - 1 - this.cornerSquare, this.cornerSquare, this.cornerSquare, 0.0f, 90f);
          this.regionPath.AddArc(this.Width - this.cornerSquare, this.Height - this.cornerSquare, this.cornerSquare, this.cornerSquare, 0.0f, 90f);
          this.graphicPath.AddLine(this.cornerSquare - this.cornerSquare / 2, this.Height - 1, this.Width - this.cornerSquare + this.cornerSquare / 2, this.Height - 1);
          this.regionPath.AddLine(this.cornerSquare - this.cornerSquare / 2, this.Height, this.Width - this.cornerSquare + this.cornerSquare / 2, this.Height);
          this.graphicPath.AddArc(0, this.Height - this.cornerSquare - 1, this.cornerSquare, this.cornerSquare, 90f, 90f);
          this.regionPath.AddArc(0, this.Height - this.cornerSquare, this.cornerSquare, this.cornerSquare, 90f, 90f);
          this.graphicPath.AddLine(0, this.cornerSquare - this.cornerSquare / 2, 0, this.Height - this.cornerSquare + this.cornerSquare / 2);
          this.regionPath.AddLine(0, this.cornerSquare - this.cornerSquare / 2, 0, this.Height - this.cornerSquare + this.cornerSquare / 2);
          break;
        default:
          throw new ApplicationException("Unrecognized style for rendering the corners");
      }
    }

    public override void Refresh()
    {
      if (this.graphicPath != null)
      {
        this.graphicPath.Dispose();
        this.InitializeGraphicPath();
      }
      base.Refresh();
    }

    [Description("Set/Get the color used to draw the borders")]
    [Category("Behavior")]
    public Color BorderColor
    {
      get => this.borderColor;
      set
      {
        this.borderColor = value;
        this.Refresh();
      }
    }

    [Description("Set/Get the style used for rendering the corners")]
    [DefaultValue(CornerStyle.Rounded)]
    [Category("Behavior")]
    public CornerStyle CornerStyle
    {
      get => this.cornerStyle;
      set
      {
        if (value == this.cornerStyle)
          return;
        this.cornerStyle = value;
        foreach (Control control in (ArrangedElementCollection) this.Controls)
        {
          if (control is CornerCtrl)
          {
            (control as CornerCtrl).CornerStyle = this.cornerStyle;
            control.Refresh();
          }
        }
        this.Refresh();
      }
    }
  }
}
