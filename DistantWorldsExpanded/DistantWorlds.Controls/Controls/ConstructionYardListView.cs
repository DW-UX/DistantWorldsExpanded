// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ConstructionYardListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ConstructionYardListView : ListViewBase
    {
        private ConstructionYardList _ConstructionYards;
        private IContainer components;

        public ConstructionYardList ConstructionYards => this._ConstructionYards;

        public ConstructionYardListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "ComponentPicture";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "ComponentPicture";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
            DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
            gridViewImageColumn2.Description = "ShipEmpire";
            gridViewImageColumn2.HeaderText = "";
            gridViewImageColumn2.Name = "ShipEmpire";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn2.ValueType = typeof(Image);
            gridViewImageColumn2.Width = 30;
            gridViewImageColumn2.FillWeight = 30f;
            gridViewImageColumn2.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
            DataGridViewImageColumn gridViewImageColumn3 = new DataGridViewImageColumn();
            gridViewImageColumn3.Description = "ShipPicture";
            gridViewImageColumn3.HeaderText = "";
            gridViewImageColumn3.Name = "ShipPicture";
            gridViewImageColumn3.ReadOnly = true;
            gridViewImageColumn3.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn3.ValueType = typeof(Image);
            gridViewImageColumn3.Width = 30;
            gridViewImageColumn3.FillWeight = 30f;
            gridViewImageColumn3.DefaultCellStyle.NullValue = (object)null;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn1.HeaderText = TextResolver.GetText("Ship");
            viewTextBoxColumn1.Name = "ShipName";
            viewTextBoxColumn1.ReadOnly = false;
            viewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn1.ValueType = typeof(string);
            viewTextBoxColumn1.Width = 120;
            viewTextBoxColumn1.FillWeight = 120f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn1);
            DataGridViewTextBoxColumn viewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Progress");
            viewTextBoxColumn2.Name = "Progress";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(double);
            viewTextBoxColumn2.Width = 40;
            viewTextBoxColumn2.DefaultCellStyle.Format = "p";
            viewTextBoxColumn2.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Speed");
            viewTextBoxColumn3.Name = "Speed";
            viewTextBoxColumn3.ReadOnly = true;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 35;
            viewTextBoxColumn3.FillWeight = 35f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
        }

        public ConstructionYard SelectedConstructionYard
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (ConstructionYard)null;
                //index = -1;
                object tag = selectedRows[0].Cells[0].Tag;
                if (tag is int index)
                { return this._ConstructionYards[index]; }
                else { return (ConstructionYard)null; }
            }
        }

        private Bitmap PrepareBuiltObjectImage(
          Bitmap image,
          Color mainColor,
          Color secondaryColor,
          int size,
          int targetSize)
        {
            double num = Math.Sqrt((double)targetSize / (double)size);
            int width = (int)((double)image.Width * num);
            int height = (int)((double)image.Height * num);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage((Image)image, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public void ClearData() => this._ConstructionYards = (ConstructionYardList)null;

        public void BindData(
          Galaxy galaxy,
          ConstructionYardList constructionYards,
          Bitmap[] componentImages,
          Bitmap[] builtObjectImages)
        {
            this._ConstructionYards = constructionYards;
            this._Grid.Rows.Clear();
            if (constructionYards != null && constructionYards.Count > 0)
                this._Grid.Rows.Add(constructionYards.Count);
            if (constructionYards != null)
            {
                for (int index = 0; index < constructionYards.Count; ++index)
                {
                    if (constructionYards[index].ComponentId >= (short)0)
                    {
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Cells[0].Value = (object)componentImages[Galaxy.ComponentDefinitionsStatic[(int)constructionYards[index].ComponentId].PictureRef];
                        row.Cells[0].ToolTipText = Galaxy.ComponentDefinitionsStatic[(int)constructionYards[index].ComponentId].Name;
                        row.Cells[0].Tag = (object)index;
                        if (constructionYards[index].ShipUnderConstruction != null)
                        {
                            if (constructionYards[index].ShipUnderConstruction.Empire != null)
                            {
                                row.Cells[1].Value = (object)constructionYards[index].ShipUnderConstruction.Empire.SmallFlagPicture;
                                row.Cells[1].ToolTipText = constructionYards[index].ShipUnderConstruction.Empire.Name;
                                Bitmap builtObjectImage = builtObjectImages[constructionYards[index].ShipUnderConstruction.PictureRef];
                                if (constructionYards[index].ShipUnderConstruction.RetrofitDesign != null)
                                    builtObjectImage = builtObjectImages[constructionYards[index].ShipUnderConstruction.RetrofitDesign.PictureRef];
                                Bitmap bitmap = this.PrepareBuiltObjectImage(builtObjectImage, constructionYards[index].ShipUnderConstruction.Empire.MainColor, constructionYards[index].ShipUnderConstruction.Empire.SecondaryColor, 1, 1);
                                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                row.Cells[2].Value = (object)bitmap;
                            }
                            else
                            {
                                Bitmap builtObjectImage = builtObjectImages[constructionYards[index].ShipUnderConstruction.PictureRef];
                                if (constructionYards[index].ShipUnderConstruction.RetrofitDesign != null)
                                    builtObjectImage = builtObjectImages[constructionYards[index].ShipUnderConstruction.RetrofitDesign.PictureRef];
                                Bitmap bitmap = this.PrepareBuiltObjectImage(builtObjectImage, Color.Gray, Color.Gray, 1, 1);
                                bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                row.Cells[2].Value = (object)bitmap;
                            }
                            row.Cells[3].Value = (object)constructionYards[index].ShipUnderConstruction.Name;
                            if (constructionYards[index].ShipUnderConstruction.RetrofitDesign != null)
                            {
                                BuiltObject underConstruction = constructionYards[index].ShipUnderConstruction;
                                Design design = underConstruction.Design;
                                Design retrofitDesign = underConstruction.RetrofitDesign;
                                int num1 = design.Components.Diff(retrofitDesign.Components).Count + retrofitDesign.Components.Diff(design.Components).Count / 4;
                                int num2 = 0;
                                int num3 = 0;
                                if (constructionYards[index].RetrofitComponentsToBeBuilt != null)
                                    num2 = constructionYards[index].RetrofitComponentsToBeBuilt.Count;
                                if (constructionYards[index].RetrofitComponentsToBeScrapped != null)
                                    num3 = constructionYards[index].RetrofitComponentsToBeScrapped.Count;
                                double num4 = 1.0 - (double)(num2 + num3 / 4) / (double)num1;
                                row.Cells[4].Value = (object)num4;
                            }
                            else
                            {
                                double num = 1.0 - (double)constructionYards[index].ShipUnderConstruction.UnbuiltOrDamagedComponentCount / (double)constructionYards[index].ShipUnderConstruction.Components.Count;
                                row.Cells[4].Value = (object)num;
                            }
                        }
                        else
                        {
                            row.Cells[1].Value = (object)null;
                            row.Cells[2].Value = (object)null;
                            row.Cells[3].Value = (object)string.Empty;
                            row.Cells[4].Value = (object)0.0;
                        }
                        row.Cells[5].Value = (object)constructionYards[index].ConstructionSpeed;
                    }
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
