// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class HabitatListView : ListViewBase
    {
        private HabitatList _Habitats;
        private static Bitmap[] _Images;
        private static Bitmap[] _FacilityImages;
        private static Bitmap _CapitalImage;
        private static Bitmap _RegionalCapitalImage;
        private static Bitmap _ApprovalSmileImage;
        private static Bitmap _ApprovalNeutralImage;
        private static Bitmap _ApprovalSadImage;
        private static Bitmap _ApprovalAngryImage;
        private IContainer components;

        public HabitatListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = (DataGridViewImageColumn)new HabitatListView.SortableImageColumn();
            gridViewImageColumn1.Description = "Empire";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Empire";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn1.ValueType = typeof(string);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            gridViewImageColumn1.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
            gridViewImageColumn2.Description = "Picture";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "Picture";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Normal;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn2.ValueType = typeof(Image);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Name");
            viewTextBoxColumn1.Name = "Name";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 130;
            viewTextBoxColumn1.FillWeight = 130f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Type");
            viewTextBoxColumn2.Name = "Type";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 80;
            viewTextBoxColumn2.FillWeight = 80f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("System");
            viewTextBoxColumn3.Name = "System";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(string);
            viewTextBoxColumn3.Width = 80;
            viewTextBoxColumn3.FillWeight = 80f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewImageColumn gridViewImageColumn3 = (DataGridViewImageColumn)new HabitatListView.SortableImageColumn();
            gridViewImageColumn3.Description = TextResolver.GetText("Facilities");
            gridViewImageColumn3.HeaderText = TextResolver.GetText("Facilities");
            gridViewImageColumn3.Name = "Facilities";
            gridViewImageColumn3.ReadOnly = true;
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn3.ValueType = typeof(string);
            gridViewImageColumn3.Width = 30;
            gridViewImageColumn3.FillWeight = 30f;
            gridViewImageColumn3.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Quality");
            viewTextBoxColumn4.Name = "Quality";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(float);
            viewTextBoxColumn4.Width = 35;
            viewTextBoxColumn4.FillWeight = 35f;
            viewTextBoxColumn4.DefaultCellStyle.Format = "0%";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn5.HeaderText = TextResolver.GetText("Culture");
            viewTextBoxColumn5.Name = "DevelopmentLevel";
            viewTextBoxColumn5.ReadOnly = false;
            viewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn5.ValueType = typeof(int);
            viewTextBoxColumn5.Width = 35;
            viewTextBoxColumn5.FillWeight = 35f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn5);
            DataGridViewTextBoxColumn viewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn6.HeaderText = TextResolver.GetText("Population Abbreviation");
            viewTextBoxColumn6.Name = "TotalPopulation";
            viewTextBoxColumn6.ReadOnly = false;
            viewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn6.ValueType = typeof(long);
            viewTextBoxColumn6.Width = 45;
            viewTextBoxColumn6.FillWeight = 45f;
            viewTextBoxColumn6.DefaultCellStyle.Format = "0,,M";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn6);
            DataGridViewImageColumn gridViewImageColumn4 = (DataGridViewImageColumn)new HabitatListView.SortableImageNumberColumn();
            gridViewImageColumn4.Description = "Approval";
            gridViewImageColumn4.HeaderText = "";
            gridViewImageColumn4.Name = "Approval";
            gridViewImageColumn4.ReadOnly = true;
            gridViewImageColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            gridViewImageColumn4.ValueType = typeof(int);
            gridViewImageColumn4.Width = 30;
            gridViewImageColumn4.FillWeight = 30f;
            gridViewImageColumn4.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn4);
            DataGridViewTextBoxColumn viewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn7.HeaderText = TextResolver.GetText("Value");
            viewTextBoxColumn7.Name = "StrategicValue";
            viewTextBoxColumn7.ReadOnly = false;
            viewTextBoxColumn7.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn7.ValueType = typeof(double);
            viewTextBoxColumn7.Width = 45;
            viewTextBoxColumn7.FillWeight = 45f;
            viewTextBoxColumn7.DefaultCellStyle.Format = "####K";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn7);
            DataGridViewTextBoxColumn viewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn8.HeaderText = TextResolver.GetText("Tax");
            viewTextBoxColumn8.Name = "TaxRate";
            viewTextBoxColumn8.ReadOnly = false;
            viewTextBoxColumn8.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn8.ValueType = typeof(double);
            viewTextBoxColumn8.Width = 35;
            viewTextBoxColumn8.FillWeight = 35f;
            viewTextBoxColumn8.DefaultCellStyle.Format = "#0%;-#0%;0%";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn8);
            DataGridViewTextBoxColumn viewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn9.HeaderText = TextResolver.GetText("Revenue");
            viewTextBoxColumn9.Name = "AnnualRevenue";
            viewTextBoxColumn9.ReadOnly = false;
            viewTextBoxColumn9.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn9.ValueType = typeof(double);
            viewTextBoxColumn9.Width = 45;
            viewTextBoxColumn9.FillWeight = 45f;
            viewTextBoxColumn9.DefaultCellStyle.Format = "0,K";
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn9);
        }

        public void SelectHabitat(Habitat habitat)
        {
            int num1 = -1;
            if (habitat == null)
                return;
            for (int index = 0; index < this._Habitats.Count; ++index)
            {
                if (this._Habitats[index] == habitat)
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
                object tag = this._Grid.Rows[index].Cells[2].Tag;
                if (tag != null && tag is int num2 && num2 == num1)
                {
                    this._Grid.Rows[index].Selected = true;
                    this._Grid.FirstDisplayedScrollingRowIndex = index;
                    break;
                }
            }
        }

        public Habitat SelectedHabitat
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Habitat)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._Habitats[index]; }
                else { return (Habitat)null; }
            }
        }

        public static void ClearImages()
        {
            GraphicsHelper.DisposeImageArray(HabitatListView._Images);
            GraphicsHelper.DisposeImageArray(HabitatListView._FacilityImages);
        }

        public static void InitializeImages(
          Bitmap[] habitatImages,
          Bitmap[] facilityImages,
          Bitmap capitalImage,
          Bitmap regionalCapitalImage,
          Bitmap approvalSmileImage,
          Bitmap approvalNeutralImage,
          Bitmap approvalSadImage,
          Bitmap approvalAngryImage)
        {
            HabitatListView.ClearImages();
            HabitatListView._Images = new Bitmap[habitatImages.Length];
            HabitatListView._FacilityImages = new Bitmap[facilityImages.Length];
            HabitatListView._CapitalImage = capitalImage;
            HabitatListView._RegionalCapitalImage = regionalCapitalImage;
            HabitatListView._ApprovalSmileImage = approvalSmileImage;
            HabitatListView._ApprovalNeutralImage = approvalNeutralImage;
            HabitatListView._ApprovalSadImage = approvalSadImage;
            HabitatListView._ApprovalAngryImage = approvalAngryImage;
            for (int index = 0; index < habitatImages.Length; ++index)
            {
                int width1 = habitatImages[index].Width;
                int height1 = habitatImages[index].Height;
                double num = (double)height1 / 18.0;
                int width2 = (int)((double)width1 / num);
                int height2 = (int)((double)height1 / num);
                Bitmap bitmap = new Bitmap(width2, height2, PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage((Image)bitmap);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image)habitatImages[index], new Rectangle(0, 0, width2, height2));
                graphics.Dispose();
                HabitatListView._Images[index] = bitmap;
            }
            for (int index = 0; index < facilityImages.Length; ++index)
            {
                int width3 = facilityImages[index].Width;
                int height3 = facilityImages[index].Height;
                double num = (double)height3 / 18.0;
                int width4 = (int)((double)width3 / num);
                int height4 = (int)((double)height3 / num);
                Bitmap bitmap = new Bitmap(width4, height4, PixelFormat.Format32bppPArgb);
                Graphics graphics = Graphics.FromImage((Image)bitmap);
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.DrawImage((Image)facilityImages[index], new Rectangle(0, 0, width4, height4));
                graphics.Dispose();
                HabitatListView._FacilityImages[index] = bitmap;
            }
        }

        private Bitmap CreateCapitalFlagImage(Bitmap flag, Bitmap capitalImage)
        {
            Bitmap capitalFlagImage = new Bitmap(flag.Width + capitalImage.Width, capitalImage.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)capitalFlagImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rect = new Rectangle(0, 3, flag.Width, flag.Height);
            graphics.DrawImage((Image)flag, rect);
            rect = new Rectangle(flag.Width, 0, capitalImage.Width, capitalImage.Height);
            graphics.DrawImage((Image)capitalImage, rect);
            return capitalFlagImage;
        }

        public void ClearData() => this._Habitats = (HabitatList)null;

        public void BindData(HabitatList habitats)
        {
            this._Habitats = habitats;
            this._Grid.SuspendLayout();
            this._Grid.Rows.Clear();
            if (habitats != null && habitats.Count > 0)
                this._Grid.Rows.Add(habitats.Count);
            if (habitats != null)
            {
                for (int index1 = 0; index1 < habitats.Count; ++index1)
                {
                    Habitat habitat = habitats[index1];
                    if (habitat != null)
                    {
                        DataGridViewRow row = this._Grid.Rows[index1];
                        if (habitat.Empire != null)
                        {
                            Bitmap flag = habitat.Empire.SmallFlagPicture;
                            if (habitat.Empire.Capital == habitat)
                                flag = this.CreateCapitalFlagImage(flag, HabitatListView._CapitalImage);
                            else if (habitat.Empire.Capitals != null && habitat.Empire.Capitals.Contains(habitat))
                                flag = this.CreateCapitalFlagImage(flag, HabitatListView._RegionalCapitalImage);
                            ((HabitatListView.SortableImageCell)row.Cells[0]).ScaledImage = flag;
                            row.Cells[0].Value = (object)habitat.Empire.Name;
                            row.Cells[0].ToolTipText = habitat.Empire.Name;
                        }
                        row.Cells[1].Value = (object)HabitatListView._Images[(int)habitat.PictureRef];
                        row.Cells[2].Value = (object)habitat.Name;
                        row.Cells[2].Tag = (object)index1;
                        string str1 = Galaxy.ResolveDescription(habitat.Type) + " " + Galaxy.ResolveDescription(habitat.Category);
                        row.Cells[3].Value = (object)str1;
                        string str2 = string.Empty;
                        switch (habitat.Category)
                        {
                            case HabitatCategoryType.Star:
                            case HabitatCategoryType.GasCloud:
                                str2 = habitat.Name;
                                break;
                            case HabitatCategoryType.Planet:
                            case HabitatCategoryType.Asteroid:
                                if (habitat.Parent != null)
                                {
                                    str2 = habitat.Parent.Name;
                                    break;
                                }
                                break;
                            case HabitatCategoryType.Moon:
                                if (habitat.Parent != null && habitat.Parent.Parent != null)
                                {
                                    str2 = habitat.Parent.Parent.Name;
                                    break;
                                }
                                break;
                        }
                        row.Cells[4].Value = (object)str2;
                        Bitmap bitmap = (Bitmap)null;
                        string str3 = string.Empty;
                        if (habitat.Facilities != null && habitat.Facilities.Count > 0)
                        {
                            bitmap = HabitatListView._FacilityImages[(int)habitat.Facilities[0].PictureRef];
                            for (int index2 = 0; index2 < habitat.Facilities.Count; ++index2)
                            {
                                string str4 = str3 + habitat.Facilities[index2].Name;
                                if ((double)habitat.Facilities[index2].ConstructionProgress < 1.0)
                                    str4 = str4 + " (" + habitat.Facilities[index2].ConstructionProgress.ToString("0%") + ")";
                                str3 = str4 + ", ";
                            }
                            str3 = str3.Substring(0, str3.Length - 2);
                        }
                      ((HabitatListView.SortableImageCell)row.Cells[5]).ScaledImage = bitmap;
                        row.Cells[5].Value = (object)str3;
                        row.Cells[5].ToolTipText = str3;
                        row.Cells[6].Value = (object)habitat.Quality;
                        row.Cells[7].Value = (object)habitat.DevelopmentLevel;
                        if (habitat.Population != null)
                        {
                            long totalAmount = habitat.Population.TotalAmount;
                            row.Cells[8].Value = (object)totalAmount;
                        }
                        else
                            row.Cells[8].Value = (object)0;
                        ((HabitatListView.SortableImageNumberCell)row.Cells[9]).ScaledImage = this.SelectApprovalImage(habitat);
                        row.Cells[9].Value = (object)(int)habitat.EmpireApprovalRating;
                        string str5 = ((int)habitat.EmpireApprovalRating).ToString();
                        if (habitat.Rebelling)
                            str5 = str5 + " (" + TextResolver.GetText("Rebelling").ToUpper(CultureInfo.InvariantCulture) + ")";
                        row.Cells[9].ToolTipText = str5;
                        double num = (double)habitat.StrategicValue / 1000.0;
                        if (num < 1.0)
                            num = 1.0;
                        row.Cells[10].Value = (object)num;
                        row.Cells[11].Value = (object)habitat.TaxRate;
                        row.Cells[12].Value = (object)habitat.AnnualRevenue;
                    }
                }
            }
            this.RememberSorting();
            this._Grid.ResumeLayout();
        }

        private Bitmap SelectApprovalImage(Habitat habitat)
        {
            double empireApprovalRating = habitat.EmpireApprovalRating;
            Bitmap bitmap = empireApprovalRating <= 15.0 ? (empireApprovalRating <= 0.0 ? (empireApprovalRating <= -15.0 ? HabitatListView._ApprovalAngryImage : HabitatListView._ApprovalSadImage) : HabitatListView._ApprovalNeutralImage) : HabitatListView._ApprovalSmileImage;
            if (habitat.Rebelling)
                bitmap = HabitatListView._ApprovalAngryImage;
            return bitmap;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        private class SortableImageColumn : DataGridViewImageColumn
        {
            public SortableImageColumn()
            {
                this.CellTemplate = (DataGridViewCell)new HabitatListView.SortableImageCell();
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
                this.CellTemplate = (DataGridViewCell)new HabitatListView.SortableImageNumberCell();
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
