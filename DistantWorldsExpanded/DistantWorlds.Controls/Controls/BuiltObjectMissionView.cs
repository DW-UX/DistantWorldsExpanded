// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.BuiltObjectMissionView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
  public class BuiltObjectMissionView : UserControl
  {
    private IContainer components;
    private CommandListView CommandListView;
    private Label lblMission;
    private BuiltObject _BuiltObject;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.CommandListView = new CommandListView();
      this.lblMission = new Label();
      this.SuspendLayout();
      this.CommandListView.Location = new Point(6, 44);
      this.CommandListView.Name = "CommandListView";
      this.CommandListView.Size = new Size(100, 68);
      this.CommandListView.TabIndex = 0;
      this.lblMission.AutoSize = true;
      this.lblMission.Location = new Point(3, 9);
      this.lblMission.Name = "lblMission";
      this.lblMission.Size = new Size(35, 13);
      this.lblMission.TabIndex = 1;
      this.lblMission.Text = "label1";
      this.Controls.Add((Control) this.lblMission);
      this.Controls.Add((Control) this.CommandListView);
      this.Name = "BuiltObjectMissionView";
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public BuiltObjectMissionView()
    {
      this.InitializeComponent();
      this.BackColor = Color.FromArgb(0, 0, 64);
      this.BorderStyle = BorderStyle.None;
      this.Font = new Font("Verdana", 6.75f, FontStyle.Regular);
      this.ForeColor = Color.White;
      this.lblMission.Font = new Font("Verdana", 6.75f, FontStyle.Bold);
      this.lblMission.Location = new Point(5, 5);
      this.CommandListView.Location = new Point(5, 20);
      this.CommandListView.Size = new Size(this.Width - 10, this.Height - 25);
      this.CommandListView.BringToFront();
    }

    public void BindData(BuiltObject builtObject)
    {
      this.CommandListView.Size = new Size(this.Width - 10, this.Height - 25);
      this._BuiltObject = builtObject;
      if (builtObject != null)
      {
        if (builtObject.Mission != null)
        {
          this.lblMission.Text = Galaxy.ResolveDescription(builtObject.Empire, builtObject.Mission);
          this.CommandListView.BindData(builtObject.Mission.ShowAllCommands());
        }
        else
        {
          this.lblMission.Text = "(" + TextResolver.GetText("None") + ")";
          this.CommandListView.BindData((Command[]) null);
        }
      }
      else
      {
        this.lblMission.Text = "(" + TextResolver.GetText("None") + ")";
        this.CommandListView.BindData((Command[]) null);
      }
    }
  }
}
