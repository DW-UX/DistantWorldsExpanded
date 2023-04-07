// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ScreenPanel
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace DistantWorlds.Controls
{
  public class ScreenPanel : BorderPanel
  {
    private IContainer components;
    public HeaderPanel pnlHeader;
    public GradientPanel pnlBody;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pnlHeader = new HeaderPanel();
      this.pnlBody = new GradientPanel();
      this.SuspendLayout();
      this.pnlHeader.BackColor = Color.FromArgb(0, 0, 0);
      this.pnlHeader.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
      this.pnlHeader.ForeColor = Color.FromArgb(150, 150, 150);
      this.pnlHeader.Icon = (Image) null;
      this.pnlHeader.Location = new Point(0, 0);
      this.pnlHeader.Name = "pnlHeader";
      this.pnlHeader.Size = new Size(200, 100);
      this.pnlHeader.TabIndex = 0;
      this.pnlHeader.TitleText = (string) null;
      this.pnlHeader.CloseButtonClicked += new EventHandler(this.pnlHeader_CloseButtonClicked);
      this.pnlBody.BackColor = Color.FromArgb(39, 40, 44);
      this.pnlBody.BackColor2 = Color.FromArgb(22, 21, 26);
      this.pnlBody.BackColor3 = Color.FromArgb(51, 54, 61);
      this.pnlBody.BorderColor = Color.FromArgb(67, 67, 77);
      this.pnlBody.BorderStyle = BorderStyle.FixedSingle;
      this.pnlBody.BorderWidth = 2;
      this.pnlBody.Curvature = 20;
      this.pnlBody.CurveMode = CornerCurveMode.BottomRight_BottomLeft;
      this.pnlBody.GradientMode = LinearGradientMode.Vertical;
      this.pnlBody.Location = new Point(0, 0);
      this.pnlBody.Margin = new Padding(0);
      this.pnlBody.Name = "pnlBody";
      this.pnlBody.Size = new Size(200, 100);
      this.pnlBody.TabIndex = 0;
      this.ResumeLayout(false);
    }

    public ScreenPanel()
    {
      this.InitializeComponent();
      this.DoubleBuffered = true;
    }

    public event EventHandler CloseButtonClicked;

    public override void SetFontCache(IFontCache fontCache)
    {
      base.SetFontCache(fontCache);
      this.pnlHeader.Font = this.Font;
      this.pnlHeader.SetFont();
      this.pnlBody.Font = this.Font;
    }

    protected override void OnVisibleChanged(EventArgs e) => base.OnVisibleChanged(e);

    protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

    [Category("Appearance")]
    [Description("The icon of the header.")]
    public virtual Image HeaderIcon
    {
      get => this.pnlHeader.Icon;
      set => this.pnlHeader.Icon = value;
    }

    [Category("Appearance")]
    [Description("The title of the header.")]
    public virtual string HeaderTitle
    {
      get => this.pnlHeader.TitleText;
      set
      {
        this.pnlHeader.TitleText = value;
        this.pnlHeader.Invalidate();
        this.pnlHeader.btnClose.Invalidate();
      }
    }

    public void DoLayout()
    {
      this.pnlBody.Visible = true;
      this.pnlBody.BringToFront();
      this.pnlHeader.Visible = true;
      this.pnlHeader.BringToFront();
      this.SuspendLayout();
      this.pnlHeader.Size = new Size(this.ClientRectangle.Width - 14, 51);
      this.pnlHeader.Location = new Point(7, 8);
      this.ReparentControl((Control) this, (Control) this.pnlHeader);
      this.pnlBody.Size = new Size(this.ClientRectangle.Width - 15, this.ClientRectangle.Height - (this.pnlHeader.Height + 12));
      this.pnlBody.Location = new Point(8, 56);
      this.ReparentControl((Control) this, (Control) this.pnlBody);
      this.ReparentControls();
      this.ReparentControls();
      this.ReparentControls();
      this.ReparentControls();
      this.ReparentControls();
      this.pnlHeader.DoLayout();
      this.ResumeLayout();
      this.Invalidate();
      this.pnlHeader.BringToFront();
    }

    private void ReparentControl(Control parent, Control child)
    {
      if (!parent.Controls.Contains(child))
        parent.Controls.Add(child);
      child.Parent = parent;
    }

    private void ReparentControls()
    {
      List<Control> controlList = new List<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Controls)
      {
        if (control != this.pnlHeader && control != this.pnlBody)
        {
          this.ReparentControl((Control) this.pnlBody, control);
          controlList.Add(control);
        }
      }
      foreach (Control control in controlList)
      {
        if (this.Controls.Contains(control))
          this.Controls.Remove(control);
      }
    }

    private void pnlHeader_CloseButtonClicked(object sender, EventArgs e)
    {
      if (this.CloseButtonClicked == null)
        return;
      this.CloseButtonClicked(sender, e);
    }
  }
}
