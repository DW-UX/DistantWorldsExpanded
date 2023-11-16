// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.LabelledTrackBar
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class LabelledTrackBar : GradientPanel
  {
    private ColorSlider _Slider = new ColorSlider();
    private int _LabelWidth = 100;
    private string _LabelText;
    private int _LinkWidth = 100;
    private string _LinkText;
    private string[] _Labels = new string[0];
    private List<SmoothLabel> _LabelControls = new List<SmoothLabel>();
    private LinkLabel _LinkLabel = new LinkLabel();
    private int _SliderOffset = 30;
    private int _SliderHeight = 22;
    private int _Padding = 5;

    public event EventHandler LinkClicked;

    public event EventHandler ValueChanged;

    public int LabelWidth
    {
      get => this._LabelWidth;
      set => this._LabelWidth = value;
    }

    public string LabelText
    {
      get => this._LabelText;
      set => this._LabelText = value;
    }

    public int LinkWidth
    {
      get => this._LinkWidth;
      set => this._LinkWidth = value;
    }

    public string LinkText
    {
      get => this._LinkText;
      set
      {
        this._LinkText = value;
        if (!string.IsNullOrEmpty(this._LinkText))
        {
          this._LinkLabel.MaximumSize = new Size(this._LinkWidth, this.Height);
          this._LinkLabel.Size = new Size(this._LinkWidth, this.Height);
          this._LinkLabel.Text = this._LinkText;
          this._LinkLabel.LinkColor = Color.Yellow;
          this._LinkLabel.ForeColor = Color.Yellow;
          this._LinkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
          this._LinkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this._LinkLabel_LinkClicked);
          this._LinkLabel.Parent = (Control) this;
          if (!this.Controls.Contains((Control) this._LinkLabel))
            this.Controls.Add((Control) this._LinkLabel);
          this._LinkLabel.Visible = true;
          this._LinkLabel.BringToFront();
          this._LinkLabel.TextAlign = ContentAlignment.MiddleLeft;
        }
        else if (this._LinkLabel != null)
        {
          this._LinkLabel.Text = string.Empty;
          this._LinkLabel.LinkClicked -= new LinkLabelLinkClickedEventHandler(this._LinkLabel_LinkClicked);
        }
        this.DoLayout();
      }
    }

    private void _LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (this.LinkClicked == null)
        return;
      this.LinkClicked(sender, (EventArgs) e);
    }

    public LabelledTrackBar()
    {
      this.Setup();
      this._Slider.Value = 0;
    }

    public void SetLabels(string[] labels)
    {
      if (labels != null && labels.Length > 1)
      {
        this._Labels = labels;
        this._Slider.Minimum = 0;
        this._Slider.Maximum = this._Labels.Length - 1;
        this._Slider.SmallChange = 1U;
        this._Slider.LargeChange = 1U;
        foreach (SmoothLabel labelControl in this._LabelControls)
        {
          if (this.Controls.Contains((Control) labelControl))
          {
            this.Controls.Remove((Control) labelControl);
            labelControl.Parent = (Control) null;
            labelControl.Dispose();
          }
        }
        this._LabelControls.Clear();
        foreach (string label in this._Labels)
        {
          SmoothLabel smoothLabel = new SmoothLabel();
          smoothLabel.Text = label;
          smoothLabel.TextAlign = ContentAlignment.TopCenter;
          smoothLabel.Font = this.Font;
          smoothLabel.ForeColor = this.ForeColor;
          smoothLabel.Parent = (Control) this;
          this._LabelControls.Add(smoothLabel);
          this.Controls.Add((Control) smoothLabel);
        }
      }
      this.Invalidate();
    }

    public void Setup() => this.Setup(25);

    public void Setup(int sliderOffset)
    {
      if (!this.Controls.Contains((Control) this._Slider))
      {
        this.Controls.Add((Control) this._Slider);
        this._Slider.Parent = (Control) this;
      }
      this._Padding = 3;
      this.Curvature = 10;
      this.GradientMode = LinearGradientMode.Vertical;
      this.BorderStyle = BorderStyle.FixedSingle;
      this.BorderWidth = 1;
      this.BackColor2 = Color.FromArgb(36, 35, 40);
      this._Slider.BackColor = Color.Transparent;
      this._Slider.BorderRoundRectSize = new Size(2, 2);
      this._Slider.BarInnerColor = Color.FromArgb(64, 64, 72);
      this._Slider.BarOuterColor = Color.FromArgb(32, 32, 40);
      this._Slider.BarPenColor = Color.FromArgb(16, 16, 24);
      this._Slider.ElapsedInnerColor = Color.FromArgb(80, 80, 96);
      this._Slider.ElapsedOuterColor = Color.FromArgb(48, 48, 64);
      this._Slider.ThumbInnerColor = Color.FromArgb(80, 80, 96);
      this._Slider.ThumbOuterColor = Color.FromArgb(48, 48, 64);
      this._Slider.ThumbPenColor = Color.FromArgb(32, 32, 40);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.Margin = new Padding(0);
      this.Padding = new Padding(0);
      this._Slider.Margin = new Padding(0);
      this._Slider.Padding = new Padding(0);
      this._LinkWidth = 100;
      this._SliderOffset = sliderOffset;
      this._Slider.Minimum = 0;
      this._Slider.Maximum = 100;
      this._Slider.MouseWheelBarPartitions = 1;
      this._Slider.SmallChange = 1U;
      this._Slider.ThumbRoundRectSize = new Size(3, 3);
      this._Slider.ThumbSize = 10;
      this._Slider.Font = this.Font;
      this._Slider.BorderRoundRectSize = new Size(3, 3);
      this._Slider.ValueChanged -= new EventHandler(this._Slider_ValueChanged);
      this._Slider.ValueChanged += new EventHandler(this._Slider_ValueChanged);
      this.DoLayout();
    }

    private void _Slider_ValueChanged(object sender, EventArgs e)
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new EventArgs());
    }

    public int Value
    {
      get => this._Slider.Value;
      set
      {
        value = Math.Max(this._Slider.Minimum, Math.Min(value, this._Slider.Maximum));
        this._Slider.Value = value;
      }
    }

    private void DoLayout()
    {
      this._Slider.Size = new Size(Math.Max(1, this.Width - (this._Padding * 2 + this._LabelWidth + this._SliderOffset * 2 + this._LinkWidth)), this._SliderHeight);
      this._Slider.Location = new Point(this._Padding + this._LabelWidth + this._SliderOffset, this.Height - (this._SliderHeight + this._Padding));
    }

    public new Size Size
    {
      get => base.Size;
      set
      {
        base.Size = value;
        this.DoLayout();
      }
    }

    public int SliderOffset
    {
      get => this._SliderOffset;
      set
      {
        this._SliderOffset = value;
        this.DoLayout();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      base.OnPaint(e);
      if (!string.IsNullOrEmpty(this._LabelText))
      {
        Font font = new Font(this.Font, FontStyle.Bold);
        SizeF sizeF = e.Graphics.MeasureString(this._LabelText, font, new SizeF((float) this._LabelWidth, (float) this.Height), StringFormat.GenericDefault);
        RectangleF layoutRectangle = new RectangleF((float) (((double) this._LabelWidth - (double) sizeF.Width) / 2.0), (float) (((double) this.Height - (double) sizeF.Height) / 2.0), sizeF.Width + 1f, sizeF.Height + 1f);
        e.Graphics.DrawString(this._LabelText, font, (Brush) new SolidBrush(this.ForeColor), layoutRectangle, StringFormat.GenericDefault);
      }
      if (this._LinkLabel != null && !string.IsNullOrEmpty(this._LinkLabel.Text))
      {
        this._LinkLabel.Location = new Point(this.Width - (this._LinkWidth + this._Padding), (this.Height - this._LinkLabel.Height) / 2);
        this._LinkLabel.Visible = true;
        this._LinkLabel.BringToFront();
      }
      int width = this._Slider.Width / (this._Labels.Length - 1);
      int padding = this._Padding;
      for (int index = 0; index < this._Labels.Length; ++index)
      {
        int x = this._Padding + this._LabelWidth + this._SliderOffset - 1 + this._Slider.ThumbSize / 2 + (int) ((double) (this._Slider.Width - this._Slider.ThumbSize) * ((double) index / (double) (this._Labels.Length - 1)));
        e.Graphics.FillRectangle((Brush) new SolidBrush(this._Slider.BarInnerColor), x, this.Height - (this._SliderHeight + this._Padding), 2, 7);
        SizeF sizeF = e.Graphics.MeasureString(this._Labels[index], this.Font, width, StringFormat.GenericDefault);
        PointF pointF = new PointF((float) x - sizeF.Width / 2f, (float) padding);
        RectangleF rectangleF = new RectangleF(pointF.X, pointF.Y, sizeF.Width, sizeF.Height);
        this._LabelControls[index].Size = new Size((int) ((double) sizeF.Width + 1.0), (int) ((double) sizeF.Height + 1.0));
        this._LabelControls[index].Location = new Point((int) pointF.X, (int) pointF.Y);
      }
    }
  }
}
