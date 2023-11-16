// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PopulationListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class PopulationListView : ListViewBase
    {
        private IContainer components;
        private PopulationList _Populations;
        private Bitmap[] _RaceImages;
        private Bitmap _BlankImage;
        private bool _Editable;
        private Habitat _Colony;

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

        public PopulationListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.Description = "Picture";
            gridViewImageColumn.HeaderText = "";
            gridViewImageColumn.Name = "Picture";
            gridViewImageColumn.ReadOnly = true;
            gridViewImageColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.Width = 40;
            gridViewImageColumn.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 160;
            viewTextBoxColumn1.FillWeight = 160f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewNumericUpDownColumn numericUpDownColumn = new DataGridViewNumericUpDownColumn();
            numericUpDownColumn.HeaderText = TextResolver.GetText("Amount");
            numericUpDownColumn.Name = "Amount";
            numericUpDownColumn.ReadOnly = true;
            numericUpDownColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            numericUpDownColumn.Minimum = 1000000M;
            numericUpDownColumn.Maximum = 20000000000M;
            numericUpDownColumn.Increment = 10000000M;
            numericUpDownColumn.ValueType = typeof(long);
            numericUpDownColumn.Width = 60;
            numericUpDownColumn.FillWeight = 60f;
            numericUpDownColumn.DefaultCellStyle.Format = "0,,M";
            this._Grid.Columns.Add((DataGridViewColumn)numericUpDownColumn);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Growth");
            viewTextBoxColumn2.Name = "GrowthRate";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            viewTextBoxColumn2.ValueType = typeof(double);
            viewTextBoxColumn2.Width = 60;
            viewTextBoxColumn2.FillWeight = 60f;
            viewTextBoxColumn2.DefaultCellStyle.Format = "+#0.0%;-#0.0%;0.0%";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
        }

        public DistantWorlds.Types.Population SelectedPopulation
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (DistantWorlds.Types.Population)null;
                //index = -1;
                object tag = selectedRows[0].Cells[1].Tag;
                if (tag is int index)
                { return this._Populations[index]; }
                else
                {
                    return (DistantWorlds.Types.Population)null;
                }
            }
        }

        public PopulationList Population
        {
            get
            {
                if (!this._Editable)
                    return this._Populations;
                for (int index1 = 0; index1 < this._Grid.Rows.Count; ++index1)
                {
                    //index2 = -1;
                    object tag = this._Grid.Rows[index1].Cells[1].Tag;
                    if (tag is int index2)
                    {
                        if (index2 >= 0)
                        {
                            long num = (long)this._Grid.Rows[index1].Cells["Amount"].Value;
                            this._Populations[index2].Amount = num;
                        }
                    }
                }
                return this._Populations;
            }
        }

        private void ClearImages() => this.ClearImageArray(this._RaceImages);

        private void ClearImageArray(Bitmap[] imageArray)
        {
            if (imageArray == null)
                return;
            for (int index = 0; index < imageArray.Length; ++index)
            {
                if (imageArray[index] != null)
                {
                    imageArray[index].Dispose();
                    imageArray[index] = (Bitmap)null;
                }
            }
        }

        public void InitializeImages(Bitmap[] raceImages)
        {
            this.ClearImages();
            this._RaceImages = new Bitmap[raceImages.Length];
            for (int index = 0; index < raceImages.Length; ++index)
                this._RaceImages[index] = this.PrescaleImage(raceImages[index], 40, 40);
            this._BlankImage = new Bitmap(10, 10, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)this._BlankImage);
            graphics.FillRectangle((Brush)new SolidBrush(Color.Transparent), 0, 0, 10, 10);
            graphics.Dispose();
        }

        public void ClearData() => this._Populations = (PopulationList)null;

        public void BindData(PopulationList populations) => this.BindData(populations, false);

        public void BindData(PopulationList populations, bool editable) => this.BindData(populations, editable, (Habitat)null);

        public void BindData(PopulationList populations, bool editable, Habitat colony)
        {
            this._Populations = populations;
            if (this._Populations != null)
            {
                this._Populations.Sort();
                this._Populations.Reverse();
            }
            this._Editable = editable;
            this._Colony = colony;
            this._Grid.ReadOnly = !editable;
            if (editable)
                this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
            this._Grid.Columns["Amount"].ReadOnly = !editable;
            this._Grid.Columns["Name"].ReadOnly = true;
            this._Grid.Rows.Clear();
            if (populations != null)
            {
                for (int index = 0; index < populations.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    row.Height = 45;
                    row.Cells[0].Value = (object)this._RaceImages[populations[index].Race.PictureRef];
                    string str = populations[index].Race.Name;
                    if (this._Colony != null && this._Colony.Empire != null && this._Colony.Empire.DominantRace != null)
                    {
                        ColonyPopulationPolicy colonyPopulationPolicy = ColonyPopulationPolicy.Assimilate;
                        if (populations[index].Race != this._Colony.Empire.DominantRace)
                        {
                            colonyPopulationPolicy = this._Colony.ColonyPopulationPolicy;
                            if ((int)populations[index].Race.FamilyId == (int)this._Colony.Empire.DominantRace.FamilyId)
                                colonyPopulationPolicy = this._Colony.ColonyPopulationPolicyRaceFamily;
                        }
                        if (populations[index].Race != this._Colony.Empire.DominantRace)
                        {
                            row.Cells[1].Style.WrapMode = DataGridViewTriState.True;
                            str = str + "\n        (" + Galaxy.ResolveDescription(colonyPopulationPolicy) + ")";
                        }
                    }
                    row.Cells[1].Value = (object)str;
                    row.Cells[1].Tag = (object)index;
                    row.Cells[2].Value = (object)populations[index].Amount;
                    row.Cells[2].ReadOnly = !editable;
                    row.Cells[3].Value = (object)(float)((double)populations[index].GrowthRate - 1.0);
                }
                this._Grid.Rows.Add();
                DataGridViewRow row1 = this._Grid.Rows[populations.Count];
                row1.Height = 45;
                row1.Cells[0].Value = (object)this._BlankImage;
                row1.Cells[1].Value = (object)TextResolver.GetText("TOTAL");
                row1.Cells[1].Tag = (object)-1;
                row1.Cells[2].Value = (object)populations.TotalAmount;
                row1.Cells[2].ReadOnly = true;
                row1.Cells[3].Value = (object)(populations.OverallGrowthRate - 1.0);
            }
            this.RememberSorting();
        }
    }
}
