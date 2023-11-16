// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CommandListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class CommandListView : ListViewBase
    {
        private Command[] _Commands;
        private IContainer components;

        public CommandListView()
        {
            this.InitializeComponent();
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = "Action";
            viewTextBoxColumn1.Name = "Action";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 130;
            viewTextBoxColumn1.FillWeight = 130f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = "Target";
            viewTextBoxColumn2.Name = "Target";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 160;
            viewTextBoxColumn2.FillWeight = 160f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public Command SelectedCommand
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Command)null;
                //index = -1;
                object tag = selectedRows[0].Cells[0].Tag;
                if (tag is int index)
                { return this._Commands[index]; }
                else { return (Command)null; }
            }
        }

        public void BindData(Command[] commands)
        {
            this._Commands = commands;
            this._Grid.Rows.Clear();
            if (commands == null)
                return;
            for (int index = 0; index < commands.Length; ++index)
            {
                this._Grid.Rows.Add();
                DataGridViewRow row = this._Grid.Rows[index];
                row.Cells[0].Value = (object)Galaxy.ResolveDescription(commands[index].Action);
                row.Cells[0].Tag = (object)index;
                if (commands[index].TargetBuiltObject != null && commands[index].TargetHabitat != null && commands[index].TargetShipGroup != null)
                {
                    if (commands[index].TargetBuiltObject != null)
                    {
                        BuiltObject targetBuiltObject = commands[index].TargetBuiltObject;
                        row.Cells[1].Value = (object)targetBuiltObject.Name;
                    }
                    else if (commands[index].TargetHabitat != null)
                    {
                        Habitat targetHabitat = commands[index].TargetHabitat;
                        row.Cells[1].Value = (object)targetHabitat.Name;
                    }
                }
                else
                    row.Cells[1].Value = (double)commands[index].Xpos <= -2000000000.0 ? (object)"(Unknown)" : (object)(commands[index].Xpos.ToString("#######0") + "," + commands[index].Ypos.ToString("#######0"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();
    }
}
