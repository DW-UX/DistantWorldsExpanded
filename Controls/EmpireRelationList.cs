// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireRelationList
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireRelationList : ListViewBase
    {
        private DiplomaticRelationList _Relations;
        private PirateRelationList _PirateRelations;
        private Bitmap[] _RaceImages;
        private SolidBrush _MutualDefenseBrush = new SolidBrush(Color.FromArgb(64, 64, 232));
        private SolidBrush _ProtectorateBrush = new SolidBrush(Color.FromArgb(112, 112, (int)byte.MaxValue));
        private SolidBrush _FreeTradeBrush = new SolidBrush(Color.FromArgb(0, (int)byte.MaxValue, 0));
        private SolidBrush _NoneBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
        private SolidBrush _TruceBrush = new SolidBrush(Color.Yellow);
        private SolidBrush _SubjugatedBrush = new SolidBrush(Color.Yellow);
        private SolidBrush _TradeSanctionsBrush = new SolidBrush(Color.Orange);
        private SolidBrush _WarBrush = new SolidBrush(Color.FromArgb((int)byte.MaxValue, 0, 0));
        private SolidBrush _NotMetBrush = new SolidBrush(Color.Tan);
        private SolidBrush _PirateProtectionBrush = new SolidBrush(Color.FromArgb(160, 160, (int)byte.MaxValue));

        public EmpireRelationList()
        {
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = (DataGridViewImageColumn)new EmpireRelationList.SortableImageColumn();
            gridViewImageColumn2.Description = "Race";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "Race";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn2.ValueType = typeof(string);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn = new DataGridViewTextBoxColumn();
            viewTextBoxColumn.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn.Name = "Name";
            viewTextBoxColumn.ReadOnly = false;
            viewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn.ValueType = typeof(string);
            viewTextBoxColumn.Width = 135;
            viewTextBoxColumn.FillWeight = 135f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn);
            DiplomaticRelationColumn diplomaticRelationColumn = new DiplomaticRelationColumn();
            diplomaticRelationColumn.HeaderText = TextResolver.GetText("Relationship");
            diplomaticRelationColumn.Name = "Relation";
            diplomaticRelationColumn.ReadOnly = false;
            diplomaticRelationColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            diplomaticRelationColumn.ValueType = typeof(object);
            diplomaticRelationColumn.Width = 100;
            diplomaticRelationColumn.FillWeight = 100f;
            this._Grid.Columns.Add((DataGridViewColumn)diplomaticRelationColumn);
            DataGridViewNumericUpDownColumn numericUpDownColumn = new DataGridViewNumericUpDownColumn();
            numericUpDownColumn.HeaderText = TextResolver.GetText("Bias");
            numericUpDownColumn.Name = "Bias";
            numericUpDownColumn.ReadOnly = false;
            numericUpDownColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            numericUpDownColumn.Minimum = -50M;
            numericUpDownColumn.Maximum = 50M;
            numericUpDownColumn.ValueType = typeof(double);
            numericUpDownColumn.Width = 60;
            numericUpDownColumn.FillWeight = 60f;
            this._Grid.Columns.Add((DataGridViewColumn)numericUpDownColumn);
        }

        public void FinalizeRelations(Galaxy galaxy)
        {
            for (int index = 0; index < this._Relations.Count; ++index)
            {
                int relationRowByIndex = this.GetRelationRowByIndex(index);
                if (relationRowByIndex >= 0)
                {
                    DiplomaticRelationType newDiplomaticRelationType = (DiplomaticRelationType)this._Grid.Rows[relationRowByIndex].Cells[3].Value;
                    if (newDiplomaticRelationType != this._Relations[index].Type)
                        this._Relations[index].ThisEmpire.ChangeDiplomaticRelation(this._Relations[index], newDiplomaticRelationType, true, this._Relations[index].Locked, this._Relations[index].AllianceName);
                    EmpireEvaluation empireEvaluation = this._Relations[index].ThisEmpire.ObtainEmpireEvaluation(this._Relations[index].OtherEmpire);
                    double num = (double)this._Grid.Rows[relationRowByIndex].Cells[4].Value;
                    if (empireEvaluation.Bias != num)
                        empireEvaluation.Bias = num;
                }
            }
            for (int index = 0; index < this._PirateRelations.Count; ++index)
            {
                int relationRowByIndex = this.GetPirateRelationRowByIndex(index);
                if (relationRowByIndex >= 0)
                {
                    PirateRelationType relationType = (PirateRelationType)this._Grid.Rows[relationRowByIndex].Cells[3].Value;
                    if (relationType != this._PirateRelations[index].Type)
                        this._PirateRelations[index].ThisEmpire.ChangePirateRelation(this._PirateRelations[index].OtherEmpire, relationType, galaxy.CurrentStarDate);
                }
            }
        }

        private int GetRelationRowByIndex(int relationIndex)
        {
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                if (this._Grid.Rows[index].Cells[3].Value is DiplomaticRelationType)
                {
                    object tag = this._Grid.Rows[index].Cells[2].Tag;
                    if (tag != null && tag is int num && num == relationIndex)
                        return index;
                }
            }
            return -1;
        }

        private int GetPirateRelationRowByIndex(int relationIndex)
        {
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                if (this._Grid.Rows[index].Cells[3].Value is PirateRelationType)
                {
                    object tag = this._Grid.Rows[index].Cells[2].Tag;
                    if (tag != null && tag is int num && num == relationIndex)
                        return index;
                }
            }
            return -1;
        }

        public DiplomaticRelation SelectedRelation
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (DiplomaticRelation)null;
                //index = -1;
                if (!(selectedRows[0].Cells[3].Value is DiplomaticRelationType))
                    return (DiplomaticRelation)null;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._Relations[index]; }
                else { return (DiplomaticRelation)null; }
            }
        }

        public PirateRelation SelectedPirateRelation
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (PirateRelation)null;
                //index = -1;
                if (!(selectedRows[0].Cells[3].Value is PirateRelationType))
                    return (PirateRelation)null;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._PirateRelations[index]; }
                else { return (PirateRelation)null; }
            }
        }

        private void SetSelectedRelation(DiplomaticRelation relation)
        {
            int num1 = -1;
            for (int index = 0; index < this._Relations.Count; ++index)
            {
                if (this._Relations[index].ThisEmpire == relation.ThisEmpire && this._Relations[index].OtherEmpire == relation.OtherEmpire)
                {
                    num1 = index;
                    break;
                }
            }
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                if (this._Grid.Rows[index].Cells[3].Value is DiplomaticRelationType)
                {
                    object tag = this._Grid.Rows[index].Cells[2].Tag;
                    if (tag != null && tag is int num2 && num2 == num1)
                    {
                        this._Grid.Rows[index].Selected = true;
                        this._Grid.FirstDisplayedScrollingRowIndex = index;
                        break;
                    }
                }
            }
        }

        private void SetSelectedRelation(PirateRelation relation)
        {
            int num1 = -1;
            for (int index = 0; index < this._Relations.Count; ++index)
            {
                if (this._Relations[index].ThisEmpire == relation.ThisEmpire && this._Relations[index].OtherEmpire == relation.OtherEmpire)
                {
                    num1 = index;
                    break;
                }
            }
            for (int index = 0; index < this._Grid.Rows.Count; ++index)
            {
                if (this._Grid.Rows[index].Cells[3].Value is PirateRelationType)
                {
                    object tag = this._Grid.Rows[index].Cells[2].Tag;
                    if (tag != null && tag is int num2 && num2 == num1)
                    {
                        this._Grid.Rows[index].Selected = true;
                        this._Grid.FirstDisplayedScrollingRowIndex = index;
                        break;
                    }
                }
            }
        }

        private SolidBrush SelectBrush(PirateRelationType pirateRelationType)
        {
            SolidBrush solidBrush = (SolidBrush)null;
            switch (pirateRelationType)
            {
                case PirateRelationType.NotMet:
                    solidBrush = this._NotMetBrush;
                    break;
                case PirateRelationType.None:
                    solidBrush = this._NoneBrush;
                    break;
                case PirateRelationType.Protection:
                    solidBrush = this._PirateProtectionBrush;
                    break;
            }
            return solidBrush;
        }

        private SolidBrush SelectBrush(DiplomaticRelationType diplomaticRelationType)
        {
            SolidBrush solidBrush = (SolidBrush)null;
            switch (diplomaticRelationType)
            {
                case DiplomaticRelationType.NotMet:
                    solidBrush = this._NotMetBrush;
                    break;
                case DiplomaticRelationType.None:
                    solidBrush = this._NoneBrush;
                    break;
                case DiplomaticRelationType.FreeTradeAgreement:
                    solidBrush = this._FreeTradeBrush;
                    break;
                case DiplomaticRelationType.MutualDefensePact:
                    solidBrush = this._MutualDefenseBrush;
                    break;
                case DiplomaticRelationType.SubjugatedDominion:
                    solidBrush = this._SubjugatedBrush;
                    break;
                case DiplomaticRelationType.Protectorate:
                    solidBrush = this._ProtectorateBrush;
                    break;
                case DiplomaticRelationType.TradeSanctions:
                    solidBrush = this._TradeSanctionsBrush;
                    break;
                case DiplomaticRelationType.War:
                    solidBrush = this._WarBrush;
                    break;
                case DiplomaticRelationType.Truce:
                    solidBrush = this._TruceBrush;
                    break;
            }
            return solidBrush;
        }

        public void ClearData()
        {
            this._Relations = (DiplomaticRelationList)null;
            this._PirateRelations = (PirateRelationList)null;
        }

        public void BindData(
          DiplomaticRelationList relations,
          PirateRelationList pirateRelations,
          Bitmap[] raceImages)
        {
            this._Relations = relations;
            this._PirateRelations = pirateRelations;
            this._RaceImages = new Bitmap[raceImages.Length];
            for (int index = 0; index < raceImages.Length; ++index)
                this._RaceImages[index] = this.PrescaleImage(raceImages[index], 30, 30);
            this._Grid.DataError += new DataGridViewDataErrorEventHandler(this._Grid_DataError);
            this._Grid.ReadOnly = false;
            this._Grid.EditMode = DataGridViewEditMode.EditOnEnter;
            this._Grid.Columns["Relation"].ReadOnly = false;
            this._Grid.Columns["Bias"].ReadOnly = false;
            this._Grid.Rows.Clear();
            if (relations != null)
            {
                for (int index = 0; index < relations.Count; ++index)
                {
                    this._Grid.Rows.Add();
                    DataGridViewRow row = this._Grid.Rows[index];
                    Empire thisEmpire = relations[index].ThisEmpire;
                    Empire otherEmpire = relations[index].OtherEmpire;
                    row.Cells[0].Value = (object)otherEmpire.SmallFlagPicture;
                    ((EmpireRelationList.SortableImageCell)row.Cells[1]).ScaledImage = this._RaceImages[otherEmpire.DominantRace.PictureRef];
                    row.Cells[1].Value = (object)otherEmpire.DominantRace.Name;
                    row.Cells[1].ToolTipText = otherEmpire.DominantRace.Name;
                    row.Cells[2].Value = (object)otherEmpire.Name;
                    row.Cells[2].ReadOnly = true;
                    row.Cells[2].Tag = (object)index;
                    row.Cells[3].Value = (object)relations[index].Type;
                    SolidBrush solidBrush = this.SelectBrush(relations[index].Type);
                    row.Cells[3].Style.ForeColor = solidBrush.Color;
                    row.Cells[3].ReadOnly = false;
                    double bias = thisEmpire.ObtainEmpireEvaluation(otherEmpire).Bias;
                    row.Cells[4].Style.Format = "+#0;-#0;0";
                    row.Cells[4].Value = (object)bias;
                    row.Cells[4].ReadOnly = false;
                }
            }
            if (pirateRelations == null)
                return;
            for (int index = 0; index < pirateRelations.Count; ++index)
            {
                this._Grid.Rows.Add();
                DataGridViewRow row = this._Grid.Rows[index + relations.Count];
                Empire thisEmpire = pirateRelations[index].ThisEmpire;
                Empire otherEmpire = pirateRelations[index].OtherEmpire;
                row.Cells[0].Value = (object)otherEmpire.SmallFlagPicture;
                ((EmpireRelationList.SortableImageCell)row.Cells[1]).ScaledImage = this._RaceImages[otherEmpire.DominantRace.PictureRef];
                row.Cells[1].Value = (object)otherEmpire.DominantRace.Name;
                row.Cells[1].ToolTipText = otherEmpire.DominantRace.Name;
                row.Cells[2].Value = (object)otherEmpire.Name;
                row.Cells[2].ReadOnly = true;
                row.Cells[2].Tag = (object)index;
                row.Cells[3].Value = (object)pirateRelations[index].Type;
                SolidBrush solidBrush = this.SelectBrush(pirateRelations[index].Type);
                row.Cells[3].Style.ForeColor = solidBrush.Color;
                row.Cells[3].ReadOnly = false;
                double num = 0.0;
                row.Cells[4].Style.Format = "+#0;-#0;0";
                row.Cells[4].Value = (object)num;
                row.Cells[4].ReadOnly = true;
            }
        }

        private void _Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new EmpireRelationList.SortableImageCell();
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
    }
}
