// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EventActionListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EventActionListView : ListViewBase
    {
        private IContainer components;
        private EventActionList _EventActions;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = (IContainer)new System.ComponentModel.Container();
            this.AutoScaleMode = AutoScaleMode.Font;
        }

        public EventActionListView()
        {
            this.InitializeComponent();
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn1.Name = "Type";
            viewTextBoxColumn1.ReadOnly = true;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 150;
            viewTextBoxColumn1.FillWeight = 150f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Description");
            viewTextBoxColumn2.Name = "Description";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 250;
            viewTextBoxColumn2.FillWeight = 250f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public void SelectEventAction(EventAction eventAction)
        {
            int num1 = -1;
            if (eventAction == null)
                return;
            for (int index = 0; index < this._EventActions.Count; ++index)
            {
                if (this._EventActions[index] == eventAction)
                {
                    num1 = index;
                    break;
                }
            }
            if (num1 < 0)
                return;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
                this._Grid.Rows[index].Selected = false;
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                object tag = this._Grid.Rows[index].Cells[1].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        public EventAction SelectedEventAction
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (EventAction)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._EventActions[index]; }
                else { return (EventAction)null; }
            }
        }

        public EventActionList GetActions() => this._EventActions;

        public void ClearData() => this._EventActions = (EventActionList)null;

        public void BindData(EventActionList eventActions)
        {
            this._EventActions = eventActions;
            this._Grid.ReadOnly = true;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (eventActions != null && eventActions.Count > 0)
                this._Grid.Rows.Add(eventActions.Count);
            if (eventActions != null)
            {
                for (int index = 0; index < eventActions.Count; ++index)
                {
                    EventAction eventAction = eventActions[index];
                    if (eventAction != null)
                    {
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Cells[0].Value = (object)Galaxy.ResolveDescription(eventAction.Type);
                        row.Cells[0].ToolTipText = TextResolver.GetText("Double-click to edit");
                        row.Cells[1].Value = (object)Galaxy.ResolveDescription(eventAction);
                        row.Cells[1].Tag = (object)index;
                        row.Cells[1].ToolTipText = TextResolver.GetText("Double-click to edit");
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new EventActionListView.SortableImageCell();
                this.ValueType = typeof(string);
            }
        }

        private class SortableImageCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageCell() => this.ValueType = typeof(string);

            public Bitmap ScaledImage
            {
                get => this._Bitmap;
                set => this._Bitmap = value;
            }

            protected override object GetFormattedValue(
              object value,
              int rowIndex,
              ref DataGridViewCellStyle cellStyle,
              TypeConverter valueTypeConverter,
              TypeConverter formattedValueTypeConverter,
              DataGridViewDataErrorContexts context)
            {
                return (object)this.ScaledImage;
            }

            public override object DefaultNewRowValue => (object)string.Empty;
        }

        private class SortableImageNumberColumn : DataGridViewImageColumn
        {
            public SortableImageNumberColumn()
            {
                this.CellTemplate = (DataGridViewCell)new EventActionListView.SortableImageNumberCell();
                this.ValueType = typeof(int);
            }
        }

        private class SortableImageNumberCell : DataGridViewImageCell
        {
            private Bitmap _Bitmap;

            public SortableImageNumberCell() => this.ValueType = typeof(int);

            public Bitmap ScaledImage
            {
                get => this._Bitmap;
                set => this._Bitmap = value;
            }

            protected override object GetFormattedValue(
              object value,
              int rowIndex,
              ref DataGridViewCellStyle cellStyle,
              TypeConverter valueTypeConverter,
              TypeConverter formattedValueTypeConverter,
              DataGridViewDataErrorContexts context)
            {
                return (object)this.ScaledImage;
            }

            public override object DefaultNewRowValue => (object)0;
        }
    }
}
