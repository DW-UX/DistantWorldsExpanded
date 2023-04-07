// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterEventListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class CharacterEventListView : ListViewBase
    {
        private CharacterEventList _CharacterEvents;
        private Empire _Empire;
        private IContainer components;

        public CharacterEventListView()
        {
            this.InitializeComponent();
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Star Date");
            viewTextBoxColumn1.Name = "Star Date";
            viewTextBoxColumn1.ReadOnly = true;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 40;
            viewTextBoxColumn1.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Title");
            viewTextBoxColumn2.Name = "Title";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 160;
            viewTextBoxColumn2.FillWeight = 160f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public void SelectCharacterEvent(CharacterEvent characterEvent)
        {
            int num1 = -1;
            if (characterEvent == null)
                return;
            for (int index = 0; index < this._CharacterEvents.Count; ++index)
            {
                if (this._CharacterEvents[index] == characterEvent)
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

        public CharacterEvent SelectedCharacterEvent
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (CharacterEvent)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._CharacterEvents[index]; }
                else { return (CharacterEvent)null; }
            }
        }

        public void ClearData()
        {
            this._CharacterEvents = (CharacterEventList)null;
            this._Empire = (Empire)null;
        }

        public void BindData(CharacterEventList characterEvents, Empire empire)
        {
            characterEvents.Sort();
            characterEvents.Reverse();
            this._CharacterEvents = characterEvents;
            this._Empire = empire;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (characterEvents != null && characterEvents.Count > 0)
                this._Grid.Rows.Add(characterEvents.Count);
            if (characterEvents != null)
            {
                for (int index = 0; index < characterEvents.Count; ++index)
                {
                    DataGridViewRow row = this._Grid.Rows[index];
                    string str = Galaxy.ResolveStarDateDescription(characterEvents[index].StarDate);
                    row.Cells[0].Value = (object)str;
                    string title = string.Empty;
                    Galaxy.ResolveDescription(characterEvents[index], empire, out title);
                    row.Cells[1].Value = (object)title;
                    row.Cells[1].Tag = (object)index;
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

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

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new CharacterEventListView.SortableImageCell();
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
                this.CellTemplate = (DataGridViewCell)new CharacterEventListView.SortableImageNumberCell();
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
