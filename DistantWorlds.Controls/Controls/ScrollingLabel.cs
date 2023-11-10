// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ScrollingLabel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Timers;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ScrollingLabel : Panel
  {
    private object _LockObject = new object();
    private volatile bool _CanPaint = true;
    private List<Control> _Controls = new List<Control>();
    private List<int> _ControlTops = new List<int>();
    private List<double> _ScrollingControlTops = new List<double>();
    private System.Timers.Timer _Timer = new System.Timers.Timer();
    private double _ScrollSpeed = 20.0;
    private double _SecondsPerTimerInterval = 0.05;
    private Font _DefaultFont = new Font("Verdana", 12f, FontStyle.Bold);
    private Color _DefaultTextColor = Color.Yellow;
    private int _DefaultSpacerHeight = 20;
    public ScrollingLabel.DoScrollDelegate DoScrollMethod;
    private Image _BackgroundImage;
    private bool _ShowBackground;

    public override Image BackgroundImage
    {
      get => this._BackgroundImage;
      set
      {
        this._BackgroundImage = value;
        if (this._BackgroundImage == null)
        {
          this._ShowBackground = false;
          this.BackColor = Color.Transparent;
        }
        else
        {
          this._ShowBackground = true;
          this.BackColor = Color.Black;
        }
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this._CanPaint || !this._ShowBackground)
        return;
      e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
      e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
      e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
      e.Graphics.SmoothingMode = SmoothingMode.None;
      e.Graphics.DrawImage(this._BackgroundImage, 0, 0);
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      if (!this._CanPaint || this._ShowBackground)
        return;
      base.OnPaintBackground(e);
    }

    public ScrollingLabel()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
      this.SetStyle(ControlStyles.Opaque, true);
      this.SetStyle(ControlStyles.ResizeRedraw, false);
      this.UpdateStyles();
      this._Timer.AutoReset = true;
      this._Timer.Interval = this._SecondsPerTimerInterval * 1000.0;
      this._Timer.Elapsed += new ElapsedEventHandler(this._Timer_Elapsed);
      this.DoScrollMethod = new ScrollingLabel.DoScrollDelegate(this.DoScroll);
    }

    ~ScrollingLabel()
    {
      this._Timer.Stop();
      this._Timer.Elapsed -= new ElapsedEventHandler(this._Timer_Elapsed);
      this.DoScrollMethod = (ScrollingLabel.DoScrollDelegate) null;
    }

    private void _Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      this._Timer.Stop();
      if (this.DoScrollMethod != null)
        this.Invoke((Delegate) this.DoScrollMethod);
      this._Timer.Start();
    }

    private void DoScroll()
    {
      lock (this._LockObject)
      {
        this._CanPaint = false;
        double num = this._SecondsPerTimerInterval * this._ScrollSpeed;
        for (int index1 = 0; index1 < this._ScrollingControlTops.Count; ++index1)
        {
          List<double> scrollingControlTops;
          int index2;
          (scrollingControlTops = this._ScrollingControlTops)[index2 = index1] = scrollingControlTops[index2] - num;
        }
        this.SuspendLayout();
        bool flag = false;
        for (int index = 0; index < this._Controls.Count; ++index)
        {
          if (this.CheckControlInView(this._Controls[index], this._ScrollingControlTops[index], this.Size))
          {
            flag = true;
            int x = (this.Width - this._Controls[index].Width) / 2;
            this._Controls[index].Location = new Point(x, (int) this._ScrollingControlTops[index]);
            this._Controls[index].Visible = true;
          }
          else
            this._Controls[index].Visible = false;
        }
        if (!flag)
        {
          this.StopScroll();
          this.ResetControlPositions();
          this.StartScroll();
        }
        this.ResumeLayout();
        this._CanPaint = true;
      }
    }

    private void ResetControlPositions()
    {
      for (int index = 0; index < this._ControlTops.Count; ++index)
        this._ScrollingControlTops[index] = (double) this._ControlTops[index];
    }

    private bool CheckControlInView(Control control, double topPosition, Size viewSize)
    {
      int num1 = (int) (topPosition + (double) control.Height);
      int num2 = (int) topPosition;
      return num1 >= 0 && num2 <= viewSize.Height;
    }

    public void SetScrollPosition(double position)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this._ScrollingControlTops.Count; ++index)
          this._ScrollingControlTops[index] = (double) this._ControlTops[index] - position;
      }
    }

    public void StartScroll() => this._Timer.Start();

    public void StopScroll() => this._Timer.Stop();

    public double ScrollSpeed
    {
      get => this._ScrollSpeed;
      set => this._ScrollSpeed = value;
    }

    public new Font DefaultFont
    {
      get => this._DefaultFont;
      set => this._DefaultFont = value;
    }

    public Color DefaultTextColor
    {
      get => this._DefaultTextColor;
      set => this._DefaultTextColor = value;
    }

    public void ClearAll()
    {
      this._Timer.Stop();
      lock (this._LockObject)
      {
        this._Controls.Clear();
        this._ControlTops.Clear();
        this._ScrollingControlTops.Clear();
        this.Controls.Clear();
      }
    }

    private void AddControl(Control control)
    {
      lock (this._LockObject)
      {
        if (control is Label)
        {
          using (Graphics graphics = this.CreateGraphics())
          {
            SizeF sizeF = graphics.MeasureString(control.Text, control.Font, this.Width, StringFormat.GenericDefault);
            Size size = new Size((int) sizeF.Width + 1, (int) sizeF.Height + 1);
            control.MinimumSize = size;
            control.Size = size;
            ((Label) control).TextAlign = ContentAlignment.MiddleCenter;
          }
        }
        control.Visible = false;
        int currentTopPosition = this.CalculateCurrentTopPosition();
        this._ControlTops.Add(currentTopPosition);
        this._ScrollingControlTops.Add((double) currentTopPosition);
        this._Controls.Add(control);
        this.Controls.Add(control);
      }
    }

    public void AddText(string text) => this.AddText(text, this._DefaultFont, this._DefaultTextColor);

    public void AddText(string text, Font font, Color color)
    {
      DropLabel dropLabel = new DropLabel();
      dropLabel.Text = text;
      dropLabel.BackColor = Color.Transparent;
      dropLabel.ForeColor = color;
      dropLabel.Font = font;
      this.AddControl((Control) dropLabel);
    }

    public void AddSpacer() => this.AddSpacer(this._DefaultSpacerHeight);

    public void AddSpacer(int height)
    {
      Panel panel = new Panel();
      panel.Size = new Size(0, height);
      panel.BackColor = Color.Transparent;
      panel.BorderStyle = BorderStyle.None;
      this.AddControl((Control) panel);
    }

    public void AddImage(Image image)
    {
      PictureBox pictureBox = new PictureBox();
      pictureBox.Image = image;
      pictureBox.Size = new Size(image.Width, image.Height);
      pictureBox.BackColor = Color.Transparent;
      pictureBox.BorderStyle = BorderStyle.None;
      this.AddControl((Control) pictureBox);
    }

    private int CalculateCurrentTopPosition()
    {
      int currentTopPosition = 0;
      foreach (Control control in this._Controls)
        currentTopPosition += control.Height;
      return currentTopPosition;
    }

    public delegate void DoScrollDelegate();
  }
}
