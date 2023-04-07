// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ResearchProgress
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class ResearchProgress : GradientPanel
  {
    private Galaxy _Galaxy;
    private Empire _Empire;
    private Bitmap[] _ComponentImages;
    private Bitmap _CrashProgramImage;
    private Bitmap _CrashProgramImageDisabled;
    private ComponentCategoryType _CrashProgramCategory;
    private string _CrashProgramHoverMessage;
    private ComponentCategoryType _HoverCategory;
    private int _RowHeight = 14;
    private int _TopMargin = 10;
    private int _LeftMargin = 10;
    private SolidBrush _WhiteBrush = new SolidBrush(Color.FromArgb(170, 170, 170));
    private SolidBrush _BlackBrush = new SolidBrush(Color.Black);
    private SolidBrush _RedBrush = new SolidBrush(Color.Red);
    private SolidBrush _GreenBrush = new SolidBrush(Color.Green);
    private Font _BoldFont;
    private Font _TitleFont;
    private List<LinkLabel> _ComponentLinks = new List<LinkLabel>();
    private IContainer components;

    public event LinkLabelLinkClickedEventHandler NextComponentClicked;

    public event EventHandler<ResearchProgress.CrashProgramEventArgs> CrashProgramInitiated;

    public ResearchProgress()
    {
      this.InitializeComponent();
      this.Font = new Font("Verdana", 8f);
      this.SetFont(15.33f);
      this._BoldFont = new Font(this.Font, FontStyle.Bold);
      this._TitleFont = new Font(this.Font.FontFamily, this.Font.Size + 2f, FontStyle.Bold, GraphicsUnit.Pixel);
    }

    public void ClearData()
    {
      this._Galaxy = (Galaxy) null;
      this._Empire = (Empire) null;
      this._CrashProgramCategory = ComponentCategoryType.Undefined;
    }

    public void Ignite(
      Galaxy galaxy,
      Empire empire,
      Bitmap[] componentImages,
      Bitmap crashProgramImage,
      Bitmap crashProgramImageDisabled,
      ComponentCategoryType crashProgramCategory)
    {
    }

    internal void _LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (this.NextComponentClicked == null)
        return;
      this.NextComponentClicked((object) this, e);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.MouseMove += new MouseEventHandler(this.ResearchProgress_MouseMove);
      this.MouseClick += new MouseEventHandler(this.ResearchProgress_MouseClick);
      this.ResumeLayout(false);
    }

    private void DoClickProcessing()
    {
    }

    private void DoHoverProcessing()
    {
    }

    private void ResearchProgress_MouseClick(object sender, MouseEventArgs e) => this.DoClickProcessing();

    private void ResearchProgress_MouseMove(object sender, MouseEventArgs e) => this.DoHoverProcessing();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    public class CrashProgramEventArgs : EventArgs
    {
      public ComponentCategoryType Category;

      public CrashProgramEventArgs(ComponentCategoryType category) => this.Category = category;
    }
  }
}
