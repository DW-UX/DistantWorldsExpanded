// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class CharacterListView : ListViewBase
    {
        private Galaxy _Galaxy;
        private CharacterList _Characters;
        private Bitmap _BlankImage;
        private CharacterImageCache _CharacterImageCache;
        private IContainer components;

        public event EventHandler CharacterChanged;

        public event EventHandler CharacterDoubleClicked;

        public CharacterListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Image";
            gridViewImageColumn.HeaderText = string.Empty;
            gridViewImageColumn.Name = "Image";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 30;
            gridViewImageColumn.FillWeight = 30f;
            gridViewImageColumn.ValuesAreIcons = false;
            gridViewImageColumn.Image = (Image)this._BlankImage;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 140;
            viewTextBoxColumn1.FillWeight = 140f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Role");
            viewTextBoxColumn2.Name = "Role";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(double);
            viewTextBoxColumn2.Width = 100;
            viewTextBoxColumn2.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Location");
            viewTextBoxColumn3.Name = "Location";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(double);
            viewTextBoxColumn3.Width = 150;
            viewTextBoxColumn3.FillWeight = 150f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Mission");
            viewTextBoxColumn4.Name = "Mission";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(string);
            viewTextBoxColumn4.Width = 270;
            viewTextBoxColumn4.FillWeight = 270f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            this._Grid.SelectionChanged += new EventHandler(this._Grid_SelectionChanged);
            this._Grid.CellDoubleClick += new DataGridViewCellEventHandler(this._Grid_CellDoubleClick);
        }

        private void _Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) => this.CharacterDoubleClicked(sender, (EventArgs)e);

        private void _Grid_SelectionChanged(object sender, EventArgs e) => this.CharacterChanged(sender, e);

        public Character SelectedCharacter
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Character)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index && index >= 0 && index < this._Characters.Count)
                { return this._Characters[index]; }
                else { return (Character)null; }
            }
        }

        public void SelectCharacter(Character characterToSelect)
        {
            int num1 = -1;
            if (characterToSelect == null)
                return;
            for (int index = 0; index < this._Characters.Count; ++index)
            {
                if (this._Characters[index] == characterToSelect)
                {
                    num1 = index;
                    break;
                }
            }
            if (num1 < 0)
                return;
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

        public void ClearData()
        {
            this._Galaxy = (Galaxy)null;
            this._Characters = (CharacterList)null;
        }

        public void BindData(
          CharacterImageCache characterImageCache,
          CharacterList characters,
          Galaxy galaxy,
          Bitmap blankImage)
        {
            this._BlankImage = blankImage;
            this.SetFont(FontSize.Normal);
            this._Galaxy = galaxy;
            this._Characters = characters;
            this._Grid.Rows.Clear();
            string text = TextResolver.GetText("Double-click to move to location");
            if (characters != null)
            {
                for (int index = 0; index < characters.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Height = 40;
                    row.Cells[0].Value = characterImageCache == null ? (object)this._BlankImage : (object)characterImageCache.ObtainCharacterImageSmall(characters[index]);
                    row.Cells[1].Value = (object)characters[index].Name;
                    row.Cells[1].Tag = (object)index;
                    row.Cells[1].Style.WrapMode = DataGridViewTriState.True;
                    row.Cells[2].Value = (object)Galaxy.ResolveDescription(characters[index].Role);
                    row.Cells[2].Style.WrapMode = DataGridViewTriState.True;
                    row.Cells[3].Value = characters[index].Location == null ? (object)("(" + TextResolver.GetText("None") + ")") : (object)Galaxy.ResolveCharacterLocationDescription(characters[index]);
                    row.Cells[3].Style.WrapMode = DataGridViewTriState.True;
                    string str = Galaxy.ResolveDescriptionCharacterTask(characters[index], galaxy);
                    row.Cells[4].Value = (object)str;
                    row.Cells[4].Style.WrapMode = DataGridViewTriState.True;
                    row.Cells[0].ToolTipText = text;
                    row.Cells[1].ToolTipText = text;
                    row.Cells[2].ToolTipText = text;
                    row.Cells[3].ToolTipText = text;
                    row.Cells[4].ToolTipText = text;
                }
            }
            this.RememberSorting();
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
