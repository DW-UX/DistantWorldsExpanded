// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.GameEventListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class GameEventListView : ListViewBase
    {
        private IContainer components;
        private GameEventList _GameEvents;

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

        public GameEventListView()
        {
            this.InitializeComponent();
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Title");
            viewTextBoxColumn1.Name = "Title";
            viewTextBoxColumn1.ReadOnly = true;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 120;
            viewTextBoxColumn1.FillWeight = 120f;
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
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Text");
            viewTextBoxColumn3.Name = "Text";
            viewTextBoxColumn3.ReadOnly = true;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(string);
            viewTextBoxColumn3.Width = 250;
            viewTextBoxColumn3.FillWeight = 250f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
        }

        public void SelectGameEvent(GameEvent gameEvent)
        {
            int num1 = -1;
            if (gameEvent == null)
                return;
            for (int index = 0; index < this._GameEvents.Count; ++index)
            {
                if (this._GameEvents[index] == gameEvent)
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

        public GameEvent SelectedGameEvent
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (GameEvent)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index && index >= 0 && index < this._GameEvents.Count)
                { return this._GameEvents[index]; }
                else { return (GameEvent)null; }
            }
        }

        public void ClearData() => this._GameEvents = (GameEventList)null;

        public void BindData(GameEventList gameEvents)
        {
            this._GameEvents = gameEvents;
            this._Grid.ReadOnly = true;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (gameEvents != null && gameEvents.Count > 0)
                this._Grid.Rows.Add(gameEvents.Count);
            if (gameEvents != null)
            {
                for (int index = 0; index < gameEvents.Count; ++index)
                {
                    GameEvent gameEvent = gameEvents[index];
                    if (gameEvent != null)
                    {
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Cells[0].Value = !string.IsNullOrEmpty(gameEvent.Title) ? (object)gameEvent.Title : (object)("(" + TextResolver.GetText("No Title Set") + ")");
                        row.Cells[1].Value = (object)Galaxy.ResolveDescription(gameEvent);
                        row.Cells[1].Tag = (object)index;
                        row.Cells[2].Value = !string.IsNullOrEmpty(gameEvent.Description) ? (object)gameEvent.Description : (object)("(" + TextResolver.GetText("No Description Set") + ")");
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
                this.CellTemplate = (DataGridViewCell)new GameEventListView.SortableImageCell();
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
                this.CellTemplate = (DataGridViewCell)new GameEventListView.SortableImageNumberCell();
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
