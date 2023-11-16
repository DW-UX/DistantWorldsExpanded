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
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls
{
  [Designer("DistantWorlds.Controls.Design.ScreenPanelDocumentDesigner, DwUxDesignHelper", typeof(IRootDesigner))]
  [Designer("System.Windows.Forms.Design.ControlDesigner, System.Drawing.Design")]
  [DesignerCategory("UserControl")]
  public class ScreenPanel : BorderPanel
  {
    private IContainer components;
    public HeaderPanel pnlHeader;
    public GradientPanel pnlBody;

    private Point _headerDragAnchor;

    private bool _headerDragging;

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      pnlHeader = new HeaderPanel();
      pnlBody = new GradientPanel();
      SuspendLayout();
      pnlHeader.BackColor = Color.FromArgb(0, 0, 0);
      pnlHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      pnlHeader.ForeColor = Color.FromArgb(150, 150, 150);
      pnlHeader.Icon = null;
      pnlHeader.Location = new Point(0, 0);
      pnlHeader.Name = "pnlHeader";
      pnlHeader.Size = new Size(200, 100);
      pnlHeader.TabIndex = 0;
      pnlHeader.TitleText = null;
      pnlHeader.CloseButtonClicked += new EventHandler(pnlHeader_CloseButtonClicked);
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
      ResumeLayout(false);
    }

    public ScreenPanel()
    {
      InitializeComponent();
      DoubleBuffered = true;
      
      pnlHeader.MouseDown += (_, args) => HeaderDragStart(args);
      pnlHeader.MouseMove += (_, args) => HeaderDragMove(args);
      pnlHeader.MouseUp += (_, args) => HeaderDragEnd(args);
    }

    private void HeaderDragStart(MouseEventArgs args) {
      if (_headerDragging) {
        HeaderDragEnd(args);
        return;
      }

      if (args.Button != MouseButtons.Left)
        return; // didn't start
      
      _headerDragging = true;
      _headerDragAnchor = new(args.X, args.Y);
      pnlHeader.Capture = true;
    }

    private void HeaderDragEnd(MouseEventArgs args) {
      if (!_headerDragging) return;

      pnlHeader.Capture = false;
      _headerDragging = false;
    }

    private void HeaderDragMove(MouseEventArgs args) {
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


    public event EventHandler CloseButtonClicked;

    public override void SetFontCache(IFontCache fontCache)
    {
      base.SetFontCache(fontCache);
      pnlHeader.Font = Font;
      pnlHeader.SetFont();
      pnlBody.Font = Font;
    }

    protected override void OnVisibleChanged(EventArgs e) => base.OnVisibleChanged(e);

    protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

    [Category("Appearance")]
    [Description("The icon of the header.")]
    public virtual Image HeaderIcon
    {
      get => pnlHeader.Icon;
      set => pnlHeader.Icon = value;
    }

    [Category("Appearance")]
    [Description("The title of the header.")]
    public virtual string HeaderTitle
    {
      get => pnlHeader.TitleText;
      set
      {
        pnlHeader.TitleText = value;
        pnlHeader.Invalidate();
        pnlHeader.btnClose.Invalidate();
      }
    }

    public void DoLayout()
    {
      pnlBody.Visible = true;
      pnlBody.BringToFront();
      pnlHeader.Visible = true;
      pnlHeader.BringToFront();
      SuspendLayout();
      const int headerMarginX = 7;
      const int headerHeight = 51; // header bottom is 51 + 8 = 59
      const int headerMarginY = 8;
      const int halfHeaderMarginY = headerMarginY / 2;
      const int tripleHalfHeaderMarginY = halfHeaderMarginY * 3; 
      
      pnlHeader.Size = new Size(ClientRectangle.Width - headerMarginX*2, headerHeight);
      pnlHeader.Location = new Point(headerMarginX, headerMarginY);
      ReparentControl(this, pnlHeader);
      const int bodyMarginX = 8;
      pnlBody.Size = new Size(
        ClientRectangle.Width - bodyMarginX*2,
        ClientRectangle.Height - (pnlHeader.Height + tripleHalfHeaderMarginY));
      pnlBody.Location = new Point(bodyMarginX, headerHeight + headerMarginY);
      ReparentControl(this, pnlBody);
      ReparentControls();
      pnlHeader.DoLayout();
      ResumeLayout();
      Invalidate();
      pnlHeader.BringToFront();
    }

    private void ReparentControl(Control parent, Control child)
    {
      if (!parent.Controls.Contains(child))
        parent.Controls.Add(child);
      child.Parent = parent;
    }

    private void ReparentControls() {
      for (;;) {
        var reparented = 0;
        List<Control> controlList = new List<Control>();
        foreach (Control control in (ArrangedElementCollection)Controls) {
          if (control != pnlHeader && control != pnlBody) {
            ReparentControl(pnlBody, control);
            controlList.Add(control);
            reparented++;
          }
        }

        foreach (Control control in controlList) {
          if (Controls.Contains(control))
            Controls.Remove(control);
        }

        if (reparented == 0) break;
      }
    }

    private void pnlHeader_CloseButtonClicked(object sender, EventArgs e)
    {
      if (CloseButtonClicked == null)
        return;
      CloseButtonClicked(sender, e);
    }
  }
}
