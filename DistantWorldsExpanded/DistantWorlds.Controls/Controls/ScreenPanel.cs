// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ScreenPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls {

  [Designer("DistantWorlds.Controls.Design.ScreenPanelDocumentDesigner, DwUxDesignHelper", typeof(IRootDesigner))]
  //[Designer("DistantWorlds.Controls.Design.GenericControlDocumentDesigner, DwUxDesignHelper", typeof(IRootDesigner))]
  [DesignerCategory("UserControl")]
  public partial class ScreenPanel : BorderPanel {

    public HeaderPanel pnlHeader;

    public GradientPanel pnlBody;

    private Point _headerDragAnchor;

    private bool _headerDragging;

    protected override void Dispose(bool disposing) {
      base.Dispose(disposing);
    }

    private HashSet<Control>? _headerAndBodyControls = null;

    private HashSet<Control> HeaderAndBodyControls
      => _headerAndBodyControls ??= new() { pnlHeader, pnlBody };

    protected override ControlCollection CreateControlsInstance()
      => new BodyControlCollection(this);

    private void InitializeComponent() {
      SuspendLayout();
      DoubleBuffered = true;
      pnlHeader = new HeaderPanel();
      pnlBody = new GradientPanel();
      pnlHeader.BackColor = Color.FromArgb(0, 0, 0);
      pnlHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      pnlHeader.ForeColor = Color.FromArgb(150, 150, 150);
      pnlHeader.Icon = null;
      pnlHeader.Location = new Point(0, 0);
      pnlHeader.Name = "pnlHeader";
      pnlHeader.Size = new Size(200, 56);
      pnlHeader.TabIndex = 0;
      pnlHeader.TitleText = null;
      pnlHeader.Dock = DockStyle.Top;
      pnlHeader.CloseButtonClicked += OnHeaderCloseButtonClicked;
      pnlHeader.MouseDown += OnHeaderDragStart;
      pnlHeader.MouseMove += OnHeaderDragMove;
      pnlHeader.MouseUp += OnHeaderDragEnd;
      pnlHeader.MouseEnter += OnHeaderMouseEnter;
      pnlHeader.MouseLeave += OnHeaderMouseLeave;
      pnlBody.AutoSize = true;
      pnlBody.MinimumSize = new Size(300, 200);
      pnlBody.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      pnlBody.BackColor = Color.FromArgb(39, 40, 44);
      pnlBody.BackColor2 = Color.FromArgb(22, 21, 26);
      pnlBody.BackColor3 = Color.FromArgb(51, 54, 61);
      pnlBody.BorderColor = Color.FromArgb(67, 67, 77);
      pnlBody.BorderStyle = BorderStyle.FixedSingle;
      pnlBody.BorderWidth = 2;
      pnlBody.Curvature = 20;
      pnlBody.CurveMode = CornerCurveMode.BottomRight_BottomLeft;
      pnlBody.GradientMode = LinearGradientMode.Vertical;
      pnlBody.Location = new Point(0, 0);
      pnlBody.Margin = new Padding(0);
      pnlBody.Name = "pnlBody";
      pnlBody.Size = new Size(200, 100);
      pnlBody.TabIndex = 0;
      pnlBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      Controls.Add(pnlHeader);
      Controls.Add(pnlBody);
      ResumeLayout(false);
    }

    private void OnHeaderMouseEnter(object sender, EventArgs e) {
      pnlHeader.Hovered = true;
    }

    private void OnHeaderMouseLeave(object sender, EventArgs e) {
      pnlHeader.Hovered = false;
    }

    static ScreenPanel() {
      //Debugger.Launch();
    }

    public ScreenPanel() {
      InitializeComponent();
      Visible = false;
    }

    public event EventHandler CloseButtonClicked;

    public override void SetFontCache(IFontCache fontCache) {
      base.SetFontCache(fontCache);
      pnlHeader.Font = Font;
      pnlHeader.SetFont();
      pnlBody.Font = Font;
    }

    [Category("Appearance")]
    [Description("The icon of the header.")]
    public Image HeaderIcon {
      get => pnlHeader.Icon;
      set {
        pnlHeader.Icon = value;
        UpdateHeader();
      }
    }

    private void UpdateHeader() {
      if (InvokeRequired) {
        _dlgUpdateHeaderInternal
          ??= UpdateHeaderInternal;
        Invoke(_dlgUpdateHeaderInternal);
      }
      else
        UpdateHeaderInternal();
    }

    private Action _dlgUpdateHeaderInternal;

    protected override void InitLayout() {
      base.InitLayout();

      if (DesignMode)
        Visible = true;

      UpdateHeader();

      AutoResize();
    }

    private bool _autoResizing;

    private void AutoResize() {
      _autoResizing = true;
      var headerSize = pnlHeader.ClientSize;
      var bodySize = pnlBody.ClientSize;
      Size = new(
        bodySize.Width,
        bodySize.Height
      );
    }

    protected override void OnSizeChanged(EventArgs e) {
      base.OnSizeChanged(e);
      if (!_autoResizing)
        AutoResize();
      else
        _autoResizing = false;
    }

    protected override void OnLayout(LayoutEventArgs levent) {
      base.OnLayout(levent);
      //UpdateHeaderHeight();

      AutoResize();
    }

    private void UpdateHeaderInternal() {
      var headerFontHeight = pnlHeader.Font.Height;
      var headerIconHeight = pnlHeader.Icon?.Height ?? 0;
      var headerHeight = Math.Max(headerFontHeight, headerIconHeight);
      var padding = (int)Math.Floor(headerHeight * 0.2);
      var doublePadding = padding * 2;
      pnlHeader.Height = headerHeight + padding;
      pnlBody.Padding = new(padding, headerHeight + doublePadding, padding, padding);
      pnlHeader.pbIcon.Size = new(headerHeight, headerHeight);
      pnlHeader.btnClose.Size = new(headerHeight, headerHeight);
      var squaredSidePadding = headerHeight + doublePadding;
      pnlHeader.lblCaption.Padding = new(
        squaredSidePadding,
        padding,
        squaredSidePadding,
        doublePadding
      );
      pnlHeader.Invalidate();
      //pnlHeader.btnClose.Invalidate();
      pnlBody.Invalidate();
    }

    [Category("Appearance")]
    [Description("The title of the header.")]
    public string HeaderTitle {
      get => pnlHeader.TitleText;
      set {
        pnlHeader.TitleText = value;
        UpdateHeader();
      }
    }

    public void DoLayout() {
      //pnlBody.AutoSize = false;
      //pnlBody.AutoSize = true;
      //pnlBody.PerformLayout();
    }

    private void OnHeaderCloseButtonClicked(object sender, EventArgs e)
      => CloseButtonClicked?.Invoke(sender, e);

    private void OnHeaderDragStart(object sender, MouseEventArgs args) {
      if (_headerDragging) {
        OnHeaderDragEnd(sender, args);
        return;
      }

      if (args.Button != MouseButtons.Left)
        return; // didn't start

      _headerDragging = true;
      _headerDragAnchor = new(args.X, args.Y);
      pnlHeader.Capture = true;
      pnlHeader.Pressed = true;
    }

    private void OnHeaderDragEnd(object sender, MouseEventArgs args) {
      if (!_headerDragging) return;

      pnlHeader.Capture = false;
      _headerDragging = false;
      pnlHeader.Pressed = false;
    }

    private void OnHeaderDragMove(object sender, MouseEventArgs args) {
      if (!_headerDragging) return;

      var scale = 96f / DeviceDpi;

      // move the panel with the delta
      var delta = new Point(
        args.Location.X - _headerDragAnchor.X,
        args.Location.Y - _headerDragAnchor.Y
      );

      Location = new(Location.X + delta.X, Location.Y + delta.Y);

      //_headerDragCursor = args.Location;
    }

  }

}