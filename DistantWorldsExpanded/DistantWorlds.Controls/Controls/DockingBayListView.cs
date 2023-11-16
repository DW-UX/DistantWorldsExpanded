// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.DockingBayListView
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
    public class DockingBayListView : ListViewBase
    {
        private DockingBayList _DockingBays;
        private IContainer components;

        public DockingBayListView()
        {
            this.InitializeComponent();
            this._Grid.RowTemplate.Height = 25;
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
            gridViewImageColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn3.ImageLayout = DataGridViewImageCellLayout.Zoom;
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
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Command");
            viewTextBoxColumn2.Name = "ShipCommand";
            viewTextBoxColumn2.ReadOnly = true;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(string);
            viewTextBoxColumn2.Width = 80;
            viewTextBoxColumn2.FillWeight = 80f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
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

        public DockingBay SelectedDockingBay
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (DockingBay)null;
                //index = -1;
                object tag = selectedRows[0].Cells[0].Tag;
                if (tag is int index)
                { return this._DockingBays[index]; }
                else { return (DockingBay)null; }
            }
        }

        public void ClearData() => this._DockingBays = (DockingBayList)null;

        public void BindData(
          DockingBayList dockingBays,
          Bitmap[] componentImages,
          Bitmap[] builtObjectImages)
        {
            this._DockingBays = dockingBays;
            this._Grid.Rows.Clear();
            if (dockingBays != null)
            {
                for (int index = 0; index < dockingBays.Count; ++index)
                {
                    DockingBay dockingBay = dockingBays[index];
                    if (dockingBay != null)
                    {
                        this._Grid.Rows.Add();
                        DataGridViewRow row = this._Grid.Rows[index];
                        row.Height = 25;
                        row.Cells[0].Value = (object)componentImages[Galaxy.ComponentDefinitionsStatic[(int)dockingBay.ParentComponentId].PictureRef];
                        row.Cells[0].ToolTipText = Galaxy.ComponentDefinitionsStatic[(int)dockingBay.ParentComponentId].Name;
                        row.Cells[0].Tag = (object)index;
                        if (dockingBay.DockedShip != null)
                        {
                            row.Cells[1].Value = (object)dockingBay.DockedShip.Empire.SmallFlagPicture;
                            row.Cells[1].ToolTipText = dockingBay.DockedShip.Empire.Name;
                            Bitmap bitmap = this.PrepareBuiltObjectImage(builtObjectImages[dockingBay.DockedShip.PictureRef], dockingBay.DockedShip.Empire.MainColor, dockingBay.DockedShip.Empire.SecondaryColor, 1, 1);
                            bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            row.Cells[2].Value = (object)bitmap;
                            row.Cells[3].Value = (object)dockingBay.DockedShip.Name;
                            row.Cells[4].Value = dockingBay.DockedShip.Mission == null || dockingBay.DockedShip.Mission.Type == BuiltObjectMissionType.Undefined ? (object)string.Empty : (object)dockingBay.DockedShip.Mission.ShowCurrentCommand().Action.ToString();
                        }
                        else
                        {
                            row.Cells[1].Value = (object)null;
                            row.Cells[2].Value = (object)null;
                            row.Cells[3].Value = (object)string.Empty;
                            row.Cells[4].Value = (object)string.Empty;
                        }
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
