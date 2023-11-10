// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ExtendedPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls
{
  public class ExtendedPanel : CornerCtrl
  {
    private bool firstTimeVisible;
    private bool moveable;
    private bool backupMoveable;
    private int backupHeight;
    private int backupWidth;
    private int step = 20;
    private int captionSize;
    private AnchorStyles backupAnchor;
    private Animation animation = Animation.Yes;
    private ExtendedPanelState state;
    private DirectionStyle captionAlign = DirectionStyle.Up;
    private CaptionCtrl captionCtrl;
    private CollapseAnimation collapseAnimation;
    private NotifyAnimationCallback callbackNotifyAnimation;
    private NotifyAnimationFinishedCallback callbackNotifyAnimationFinished;
    private List<Control> visibleControls = new List<Control>();
    private object dummy = (object) 1;
    private IContainer components;

    public event EventHandler CaptionClicked;

    public Size BackupSize
    {
      set
      {
        this.backupHeight = value.Height;
        this.backupWidth = value.Width;
      }
      get => new Size(this.backupWidth, this.backupHeight);
    }

    public ExtendedPanel()
    {
      this.InitializeComponent();
      this.captionCtrl.SetStyleChangedHandler(new DirectionCtrlStyleChangedEvent(this.CollapsingHandler));
      if (this.moveable)
        this.captionCtrl.Dragging += new DistantWorlds.Controls.CaptionDraggingEvent(this.CaptionDraggingEvent);
      this.callbackNotifyAnimation = new NotifyAnimationCallback(this.SetSizeCallback);
      this.callbackNotifyAnimationFinished = new NotifyAnimationFinishedCallback(this.AnimationFinished);
      this.SetDefaults();
    }

    public void SetDefaults()
    {
      this.BorderStyle = BorderStyle.None;
      this.captionCtrl.BorderStyle = BorderStyle.None;
      this.BackColor = Color.FromArgb(32, 32, 48);
      this.ForeColor = Color.FromArgb(170, 170, 170);
      this.captionCtrl.DirectionCtrlColor = Color.LightGray;
      this.captionCtrl.DirectionCtrlHoverColor = Color.White;
      this.captionCtrl.CornerStyle = CornerStyle.Normal;
      this.CornerStyle = CornerStyle.Normal;
      this.CaptionAlign = DirectionStyle.Up;
      this.captionCtrl.Dock = DockStyle.Top;
      this.CaptionBrush = BrushType.Gradient;
      this.CaptionColorOne = Color.FromArgb(144, 144, 154);
      this.CaptionColorTwo = Color.FromArgb(56, 56, 64);
      this.CaptionFont = new Font("Verdana", 10f, FontStyle.Bold);
      this.CaptionSize = 20;
      this.CaptionTextColor = Color.White;
    }

    public void Collapse()
    {
      if (this.state != ExtendedPanelState.Expanded)
        return;
      DirectionStyle oldStyle = DirectionStyle.Up;
      DirectionStyle directionStyle = DirectionStyle.Down;
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          oldStyle = DirectionStyle.Right;
          directionStyle = DirectionStyle.Left;
          break;
        case DirectionStyle.Down:
          oldStyle = DirectionStyle.Down;
          directionStyle = DirectionStyle.Up;
          break;
        case DirectionStyle.Left:
          oldStyle = DirectionStyle.Left;
          directionStyle = DirectionStyle.Right;
          break;
      }
      this.captionCtrl.SetDirectionStyle(directionStyle);
      this.CollapsingHandler((object) this, new ChangeStyleEventArgs(oldStyle, directionStyle));
    }

    public void Expand()
    {
      if (this.state != ExtendedPanelState.Collapsed)
        throw new InvalidOperationException("The control has to be in an expanded state for calling collapsing!");
      DirectionStyle oldStyle = DirectionStyle.Down;
      DirectionStyle directionStyle = DirectionStyle.Up;
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          oldStyle = DirectionStyle.Left;
          directionStyle = DirectionStyle.Right;
          break;
        case DirectionStyle.Down:
          oldStyle = DirectionStyle.Up;
          directionStyle = DirectionStyle.Down;
          break;
        case DirectionStyle.Left:
          oldStyle = DirectionStyle.Right;
          directionStyle = DirectionStyle.Left;
          break;
      }
      this.captionCtrl.SetDirectionStyle(directionStyle);
      this.CollapsingHandler((object) this, new ChangeStyleEventArgs(oldStyle, directionStyle));
    }

    public void DoTopDock()
    {
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control != this.captionCtrl)
        {
          switch (control)
          {
            case HabitatTypeIconView _:
            case CreatureTypeIconView _:
            case GenericIconView _:
              if (control.Location.Y < this.captionCtrl.Height)
                control.Top = this.captionSize;
              control.Height = this.ClientRectangle.Height - this.captionSize;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void CheckDocking(int oldCaptionSize)
    {
      int num = this.captionSize - oldCaptionSize;
      if (num == 0)
        return;
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          this.SuspendLayout();
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control != this.captionCtrl)
              control.Left -= num;
          }
          this.ResumeLayout(false);
          break;
        case DirectionStyle.Down:
          this.SuspendLayout();
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control != this.captionCtrl)
            {
              control.Top -= num;
              control.Height += num;
            }
          }
          this.ResumeLayout(false);
          break;
        case DirectionStyle.Left:
          this.SuspendLayout();
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control != this.captionCtrl)
              control.Left += num;
          }
          this.ResumeLayout(false);
          break;
        case DirectionStyle.Up:
          this.SuspendLayout();
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control != this.captionCtrl)
            {
              control.Top += num;
              control.Height -= num;
            }
          }
          this.ResumeLayout(false);
          break;
      }
    }

    private void ShowControls()
    {
      if (this.state != ExtendedPanelState.Collapsed)
        return;
      lock (this.dummy)
      {
        if (this.visibleControls.Count > 0)
        {
          while (this.visibleControls.Count > 0)
          {
            this.visibleControls[this.visibleControls.Count - 1].Visible = true;
            this.visibleControls.RemoveAt(this.visibleControls.Count - 1);
          }
        }
        else
        {
          foreach (Control control in (ArrangedElementCollection) this.Controls)
          {
            if (control != this.captionCtrl && control.Visible)
            {
              this.visibleControls.Add(control);
              control.Visible = false;
            }
          }
        }
      }
    }

    private void SetSizeCallback(int size)
    {
      this.SuspendLayout();
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          int num1 = this.Width - size;
          Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X + num1, this.Location.Y, size, this.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_SHOWWINDOW);
          break;
        case DirectionStyle.Down:
          int num2 = this.Height - size;
          Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X, this.Location.Y + num2, this.Width, size, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_SHOWWINDOW);
          break;
        case DirectionStyle.Left:
          this.Width = size;
          if (this.Width < this.captionCtrl.Width)
          {
            this.Width = this.captionCtrl.Width;
            break;
          }
          break;
        case DirectionStyle.Up:
          this.Height = size;
          break;
      }
      this.DoTopDock();
      this.ResumeLayout(false);
    }

    private void AnimationFinished()
    {
      if (this.captionAlign == DirectionStyle.Right || this.captionAlign == DirectionStyle.Down)
        this.Anchor = this.backupAnchor;
      if (this.captionAlign == DirectionStyle.Down)
      {
        Win32Wrapper.SetWindowPos(this.captionCtrl.Handle, IntPtr.Zero, 0, this.Height - this.captionCtrl.Height, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW | Win32Wrapper.FlagsSetWindowPos.SWP_HIDEWINDOW);
        this.captionCtrl.Parent = (Control) this;
        this.captionCtrl.Visible = true;
        this.moveable = this.backupMoveable;
      }
      else if (this.captionAlign == DirectionStyle.Right)
      {
        Win32Wrapper.SetWindowPos(this.captionCtrl.Handle, IntPtr.Zero, this.Width - this.captionCtrl.Width, 0, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER | Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW | Win32Wrapper.FlagsSetWindowPos.SWP_HIDEWINDOW);
        this.captionCtrl.Parent = (Control) this;
        this.captionCtrl.Visible = true;
        this.moveable = this.backupMoveable;
      }
      this.SetState();
      this.DoTopDock();
    }

    private void SetState()
    {
      if (this.captionCtrl.Width >= this.captionCtrl.Height)
      {
        if (this.captionCtrl.Height == this.Height)
          this.state = ExtendedPanelState.Collapsed;
        else
          this.state = ExtendedPanelState.Expanded;
      }
      else if (this.captionCtrl.Width == this.Width)
        this.state = ExtendedPanelState.Collapsed;
      else
        this.state = ExtendedPanelState.Expanded;
    }

    private void SetCaptionControl(bool flag)
    {
      if (flag && this.state == ExtendedPanelState.Expanded)
        this.captionCtrl.SetDirectionStyle(this.captionAlign);
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          if (flag)
          {
            this.captionCtrl.Width = this.captionSize;
            this.captionCtrl.Location = new Point(this.Width - this.captionCtrl.Width, 0);
            this.captionCtrl.Dock = DockStyle.Right;
          }
          if (this.captionCtrl.Height == this.Height)
            break;
          this.captionCtrl.Height = this.Height;
          break;
        case DirectionStyle.Down:
          if (flag)
          {
            this.captionCtrl.Height = this.captionSize;
            this.captionCtrl.Location = new Point(0, this.Height - this.captionCtrl.Height);
            this.captionCtrl.Dock = DockStyle.Bottom;
          }
          if (this.Width == this.captionCtrl.Width)
            break;
          this.captionCtrl.Width = this.Width;
          break;
        case DirectionStyle.Left:
          if (flag)
          {
            this.captionCtrl.Width = this.captionSize;
            this.captionCtrl.Location = new Point(0, 0);
            this.captionCtrl.Dock = DockStyle.Left;
          }
          if (this.captionCtrl.Height == this.Height)
            break;
          this.captionCtrl.Height = this.Height;
          break;
        case DirectionStyle.Up:
          if (flag)
          {
            this.captionCtrl.Height = this.captionSize;
            this.captionCtrl.Location = new Point(0, 0);
            this.captionCtrl.Dock = DockStyle.Top;
          }
          if (this.Width == this.captionCtrl.Width)
            break;
          this.captionCtrl.Width = this.Width;
          break;
      }
    }

    private void ChangeCaptionParent()
    {
      this.captionCtrl.Parent = this.Parent;
      this.captionCtrl.Location = new Point(this.Location.X + this.Width - this.captionCtrl.Width, this.Location.Y + this.Height - this.captionCtrl.Height);
      Win32Wrapper.SetWindowPos(this.Handle, this.captionCtrl.Handle, 0, 0, 0, 0, Win32Wrapper.FlagsSetWindowPos.SWP_NOSIZE | Win32Wrapper.FlagsSetWindowPos.SWP_NOMOVE | Win32Wrapper.FlagsSetWindowPos.SWP_NOREDRAW);
      this.backupMoveable = this.moveable;
      this.moveable = false;
    }

    protected override void InitializeGraphicPath()
    {
      this.cornerSquare = this.captionCtrl.Height > this.captionCtrl.Width ? (int) ((double) this.captionCtrl.Height * 0.05000000074505806) : (int) ((double) this.captionCtrl.Width * 0.05000000074505806);
      base.InitializeGraphicPath();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
      if (this.graphicPath != null)
        return;
      this.InitializeGraphicPath();
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      if (this.captionSize == 0)
      {
        this.captionSize = this.Height * 20 / 100;
        this.CheckDocking(0);
      }
      this.SetCaptionControl(this.state != ExtendedPanelState.Collapsing && this.state != ExtendedPanelState.Expanding);
      this.Refresh();
    }

    protected override void WndProc(ref Message m)
    {
      if (!this.DesignMode && this.firstTimeVisible && m.Msg == 24)
      {
        this.firstTimeVisible = false;
        this.backupHeight = this.Height;
        this.backupWidth = this.Width;
        switch (this.captionAlign)
        {
          case DirectionStyle.Right:
            Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X + this.Width - this.captionCtrl.Location.X, this.Location.Y, this.captionCtrl.Width, this.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER);
            this.captionCtrl.SetDirectionStyle(DirectionStyle.Left);
            break;
          case DirectionStyle.Down:
            Win32Wrapper.SetWindowPos(this.Handle, IntPtr.Zero, this.Location.X, this.Location.Y + this.captionCtrl.Location.Y, this.Width, this.captionCtrl.Height, Win32Wrapper.FlagsSetWindowPos.SWP_NOZORDER);
            this.captionCtrl.SetDirectionStyle(DirectionStyle.Up);
            break;
          case DirectionStyle.Left:
            this.Width = this.captionCtrl.Width;
            this.captionCtrl.SetDirectionStyle(DirectionStyle.Right);
            break;
          case DirectionStyle.Up:
            this.Height = this.captionCtrl.Height;
            this.captionCtrl.SetDirectionStyle(DirectionStyle.Down);
            break;
        }
        this.captionCtrl.Location = new Point(0, 0);
      }
      base.WndProc(ref m);
    }

    [DefaultValue(false)]
    [Description("Sets/Gets whether this control can be moved by dragging")]
    [Category("Behavior")]
    [Browsable(true)]
    public bool Moveable
    {
      get => this.moveable;
      set
      {
        if (value == this.moveable)
          return;
        this.moveable = value;
        if (this.moveable)
        {
          if (this.captionCtrl.IsDraggingEnabled())
            return;
          this.captionCtrl.Dragging += new DistantWorlds.Controls.CaptionDraggingEvent(this.CaptionDraggingEvent);
        }
        else
        {
          if (!this.captionCtrl.IsDraggingEnabled())
            return;
          this.captionCtrl.Dragging -= new DistantWorlds.Controls.CaptionDraggingEvent(this.CaptionDraggingEvent);
        }
      }
    }

    [DefaultValue(Animation.Yes)]
    [Browsable(true)]
    [Category("Behavior")]
    [Description("Sets/Gets whether collapsing or expanding process is being animated")]
    public Animation Animation
    {
      get => this.animation;
      set => this.animation = value;
    }

    [Category("Caption")]
    [Description("Set/Get where the caption for this panel is positioned")]
    [DefaultValue(DirectionStyle.Up)]
    public DirectionStyle CaptionAlign
    {
      get => this.captionAlign;
      set
      {
        if (value == this.captionAlign)
          return;
        this.captionAlign = value;
        this.SetCaptionControl(true);
        this.captionCtrl.Refresh();
      }
    }

    [Description("Set/Get the text to be displayed in the caption of this control")]
    [DefaultValue("Caption")]
    [Category("Caption")]
    public string CaptionText
    {
      get => this.captionCtrl.CaptionText;
      set
      {
        this.captionCtrl.CaptionText = value;
        this.captionCtrl.Refresh();
      }
    }

    [DefaultValue(null)]
    [Category("Caption")]
    [Description("Get/Set the image to be displayed in the caption of this control")]
    public Image CaptionImage
    {
      get => this.captionCtrl.CaptionIcon;
      set
      {
        this.captionCtrl.CaptionIcon = value;
        this.captionCtrl.Refresh();
      }
    }

    [Description("Get/Set the brush type used in filling the caption")]
    [DefaultValue(BrushType.Gradient)]
    [Category("Caption")]
    public BrushType CaptionBrush
    {
      get => this.captionCtrl.BrushType;
      set => this.captionCtrl.BrushType = value;
    }

    [Description("Get/Set the color of the text displayed in the caption")]
    [Category("Caption")]
    public Color CaptionTextColor
    {
      get => this.captionCtrl.TextColor;
      set => this.captionCtrl.TextColor = value;
    }

    [Category("Caption")]
    [Description("Get/Set the starting color for the gradient brush if brush type is chose to be gradient")]
    public Color CaptionColorOne
    {
      get => this.captionCtrl.ColorOne;
      set => this.captionCtrl.ColorOne = value;
    }

    [Category("Caption")]
    [Description("Get/Set the ending color for the gradient brush if brush type is chose to be gradient")]
    public Color CaptionColorTwo
    {
      get => this.captionCtrl.ColorTwo;
      set => this.captionCtrl.ColorTwo = value;
    }

    [Description("Get/Set the font used for drawing the caption text")]
    [Category("Caption")]
    public Font CaptionFont
    {
      get => this.captionCtrl.CaptionFont;
      set => this.captionCtrl.CaptionFont = value;
    }

    [Category("Appearance")]
    [DefaultValue(ExtendedPanelState.Expanded)]
    [Description("Returns the state of this panel")]
    public ExtendedPanelState State
    {
      get => this.state;
      [DesignOnly(true)] set
      {
        if (value == ExtendedPanelState.Collapsing || value == ExtendedPanelState.Expanding)
          return;
        this.state = value;
        if (value != ExtendedPanelState.Collapsed)
          return;
        this.firstTimeVisible = true;
      }
    }

    [Category("Caption")]
    [DefaultValue(0)]
    [Description("Set/Get the percent of the caption part of this control 1 to 100")]
    public int CaptionSize
    {
      get => this.captionSize;
      set
      {
        if (value < 0)
          throw new ArgumentException("Need a value greater or equal to 0 ");
        if (this.state != ExtendedPanelState.Expanded)
          throw new InvalidOperationException("You can set the caption size while the control is fully expanded");
        if (value == this.captionSize)
          return;
        int captionSize = this.captionSize;
        this.captionSize = value;
        this.SetCaptionControl(true);
        this.CheckDocking(captionSize);
        this.captionCtrl.Refresh();
        this.Refresh();
      }
    }

    [Description("Get/Set the step used for animating collasping/expanding")]
    [Category("Caption")]
    [DefaultValue(20)]
    public int AnimationStep
    {
      get => this.step;
      set => this.step = value >= 1 ? value : throw new ArgumentException("Need a value greater or equal to 1");
    }

    [Browsable(false)]
    public Color DirectionCtrlColor
    {
      get => this.captionCtrl.DirectionCtrlColor;
      set => this.captionCtrl.DirectionCtrlColor = value;
    }

    [Browsable(false)]
    public Color DirectionCtrlHoverColor
    {
      get => this.captionCtrl.DirectionCtrlHoverColor;
      set => this.captionCtrl.DirectionCtrlHoverColor = value;
    }

    private void CollapsingHandler(object sender, ChangeStyleEventArgs e)
    {
      if (this.captionAlign == DirectionStyle.Right || this.captionAlign == DirectionStyle.Down)
      {
        this.backupAnchor = this.Anchor;
        this.Anchor |= AnchorStyles.Left;
        this.Anchor |= AnchorStyles.Top;
        this.Anchor &= ~AnchorStyles.Right;
        this.Anchor &= ~AnchorStyles.Bottom;
      }
      if (this.collapseAnimation == null)
      {
        this.collapseAnimation = new CollapseAnimation();
        this.collapseAnimation.NotifyAnimation += new NotifyAnimationEvent(this.OnNotifyAnimationEvent);
        this.collapseAnimation.NotifyAnimationFinished += new NotifyAnimationFinishedEvent(this.OnNotifyAnimationFinished);
        if (this.backupHeight == 0)
          this.backupHeight = this.Height;
        if (this.backupWidth == 0)
          this.backupWidth = this.Width;
      }
      switch (this.captionAlign)
      {
        case DirectionStyle.Right:
          if (e.Old == DirectionStyle.Right)
          {
            this.ChangeCaptionParent();
            this.backupHeight = this.Height;
            this.backupWidth = this.Width;
            this.collapseAnimation.Maximum = this.Width;
            this.collapseAnimation.Minimum = this.captionCtrl.Width;
            this.collapseAnimation.Step = this.animation != Animation.Yes ? this.Width - this.captionCtrl.Width : this.step;
            break;
          }
          this.ChangeCaptionParent();
          this.collapseAnimation.Maximum = this.backupWidth;
          this.collapseAnimation.Minimum = this.captionCtrl.Width;
          this.collapseAnimation.Step = this.animation != Animation.Yes ? -(this.backupWidth - this.captionCtrl.Width) : -this.step;
          break;
        case DirectionStyle.Down:
          if (e.Old == DirectionStyle.Down)
          {
            this.ChangeCaptionParent();
            this.backupHeight = this.Height;
            this.backupWidth = this.Width;
            this.collapseAnimation.Maximum = this.Height;
            this.collapseAnimation.Minimum = this.captionCtrl.Height;
            this.collapseAnimation.Step = this.animation != Animation.Yes ? this.Height - this.captionCtrl.Height : this.step;
            break;
          }
          this.ChangeCaptionParent();
          this.collapseAnimation.Maximum = this.backupHeight;
          this.collapseAnimation.Minimum = this.captionCtrl.Height;
          this.collapseAnimation.Step = this.animation != Animation.Yes ? -(this.backupHeight - this.captionCtrl.Height) : -this.step;
          break;
        case DirectionStyle.Left:
          if (e.Old == DirectionStyle.Left)
          {
            this.backupHeight = this.Height;
            this.backupWidth = this.Width;
            this.collapseAnimation.Maximum = this.Width;
            this.collapseAnimation.Minimum = this.captionCtrl.Width;
            this.collapseAnimation.Step = this.animation != Animation.Yes ? this.Width - this.captionCtrl.Width : this.step;
            break;
          }
          this.collapseAnimation.Maximum = this.backupWidth;
          this.collapseAnimation.Minimum = this.captionCtrl.Width;
          this.collapseAnimation.Step = this.animation != Animation.Yes ? -(this.backupWidth - this.captionCtrl.Width) : -this.step;
          break;
        case DirectionStyle.Up:
          if (e.Old == DirectionStyle.Up)
          {
            this.backupHeight = this.Height;
            this.backupWidth = this.Width;
            this.collapseAnimation.Maximum = this.Height;
            this.collapseAnimation.Minimum = this.captionCtrl.Height;
            this.collapseAnimation.Step = this.animation != Animation.Yes ? this.Height - this.captionCtrl.Height : this.step;
            break;
          }
          this.collapseAnimation.Maximum = this.backupHeight;
          this.collapseAnimation.Minimum = this.captionCtrl.Height;
          this.collapseAnimation.Step = this.animation != Animation.Yes ? -(this.backupHeight - this.captionCtrl.Height) : -this.step;
          break;
      }
      this.SetState();
      if (this.state == ExtendedPanelState.Collapsed)
        this.state = ExtendedPanelState.Expanding;
      else if (this.state == ExtendedPanelState.Expanded)
        this.state = ExtendedPanelState.Collapsing;
      this.collapseAnimation.Start();
    }

    private void OnNotifyAnimationEvent(object sender, int size) => this.Invoke((Delegate) this.callbackNotifyAnimation, (object) size);

    private void OnNotifyAnimationFinished(object sender) => this.Invoke((Delegate) this.callbackNotifyAnimationFinished);

    private void CaptionDraggingEvent(object sender, CaptionDraggingEventArgs e)
    {
      if (!this.moveable)
        return;
      this.Location = new Point(this.Location.X - e.Width, this.Location.Y - e.Height);
    }

    private void captionCtrl_Click(object sender, EventArgs e) => this.CaptionClicked(sender, e);

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.captionCtrl = new CaptionCtrl();
      this.SuspendLayout();
      this.captionCtrl.BackColor = Color.Transparent;
      this.captionCtrl.BorderColor = Color.Gray;
      this.captionCtrl.BrushType = BrushType.Gradient;
      this.captionCtrl.CaptionFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.captionCtrl.CaptionIcon = (Image) null;
      this.captionCtrl.CaptionText = "Caption";
      this.captionCtrl.ColorOne = Color.White;
      this.captionCtrl.ColorTwo = Color.FromArgb(155, (int) byte.MaxValue, 165, 0);
      this.captionCtrl.DirectionCtrlColor = Color.DarkGray;
      this.captionCtrl.DirectionCtrlHoverColor = Color.Orange;
      this.captionCtrl.Location = new Point(0, 0);
      this.captionCtrl.Name = "captionCtrl";
      this.captionCtrl.Size = new Size(200, 100);
      this.captionCtrl.TabIndex = 0;
      this.captionCtrl.TextColor = Color.Black;
      this.captionCtrl.Click += new EventHandler(this.captionCtrl_Click);
      this.Controls.Add((Control) this.captionCtrl);
      this.ResumeLayout(false);
    }
  }
}
