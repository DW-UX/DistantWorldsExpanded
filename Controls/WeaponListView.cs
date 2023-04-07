// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.WeaponListView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class WeaponListView : ListViewBase
    {
        private IContainer components;
        private WeaponList _Weapons;
        private int _DamageGraphWidth = 270;
        private int _DamageGraphHeight = 45;
        private int _RowHeight = 45;

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent() => this.components = (IContainer)new System.ComponentModel.Container();

        public int RowHeight
        {
            get => this._RowHeight;
            set => this._RowHeight = value;
        }

        public int DamageGraphWidth
        {
            get => this._DamageGraphWidth;
            set => this._DamageGraphWidth = value;
        }

        public int DamageGraphHeight
        {
            get => this._DamageGraphHeight;
            set => this._DamageGraphHeight = value;
        }

        public WeaponListView()
        {
            this.InitializeComponent();
            DataGridViewImageColumn gridViewImageColumn1 = new DataGridViewImageColumn();
            gridViewImageColumn1.Description = "Picture";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "Picture";
            gridViewImageColumn1.ReadOnly = true;
            gridViewImageColumn1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn1.ValueType = typeof(Image);
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn1.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn1);
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
            viewTextBoxColumn2.HeaderText = TextResolver.GetText("Speed");
            viewTextBoxColumn2.Name = "Speed";
            viewTextBoxColumn2.ReadOnly = false;
            viewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn2.ValueType = typeof(int);
            viewTextBoxColumn2.Width = 40;
            viewTextBoxColumn2.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn2);
            DataGridViewTextBoxColumn viewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn3.HeaderText = TextResolver.GetText("Energy");
            viewTextBoxColumn3.Name = "EnergyRequired";
            viewTextBoxColumn3.ReadOnly = false;
            viewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn3.ValueType = typeof(int);
            viewTextBoxColumn3.Width = 40;
            viewTextBoxColumn3.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn3);
            DataGridViewTextBoxColumn viewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            viewTextBoxColumn4.HeaderText = TextResolver.GetText("Rate");
            viewTextBoxColumn4.Name = "FireRate";
            viewTextBoxColumn4.ReadOnly = false;
            viewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.Automatic;
            viewTextBoxColumn4.ValueType = typeof(int);
            viewTextBoxColumn4.Width = 40;
            viewTextBoxColumn4.FillWeight = 40f;
            this._Grid.Columns.Add((DataGridViewColumn)viewTextBoxColumn4);
            DataGridViewImageColumn gridViewImageColumn2 = new DataGridViewImageColumn();
            gridViewImageColumn2.Description = TextResolver.GetText("Damage");
            gridViewImageColumn2.HeaderText = TextResolver.GetText("Damage");
            gridViewImageColumn2.Name = "DamageGraph";
            gridViewImageColumn2.ReadOnly = true;
            gridViewImageColumn2.ImageLayout = DataGridViewImageCellLayout.Zoom;
            gridViewImageColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            gridViewImageColumn2.ValueType = typeof(Image);
            gridViewImageColumn2.Width = 260;
            gridViewImageColumn2.FillWeight = 30f;
            this._Grid.Columns.Add((DataGridViewColumn)gridViewImageColumn2);
        }

        public Weapon SelectedWeapon
        {
            get
            {
                DataGridViewSelectedRowCollection selectedRows = this._Grid.SelectedRows;
                if (selectedRows.Count != 1)
                    return (Weapon)null;
                //index = -1;
                object tag = selectedRows[0].Cells[2].Tag;
                if (tag is int index)
                { return this._Weapons[index]; }
                else { return (Weapon)null; }
            }
        }

        private Bitmap GenerateDamageGraph(Weapon weapon)
        {
            double num1 = (double)weapon.DamageLoss * ((double)weapon.Range / 100.0);
            int num2 = (int)((double)weapon.RawDamage - num1);
            double num3 = (double)this._DamageGraphHeight / 2.0 / 50.0;
            double num4 = (double)this._DamageGraphWidth / 990.0;
            double num5 = (double)(int)((double)this._DamageGraphHeight / 2.0);
            double num6 = (double)weapon.RawDamage * num3 / 2.0;
            double num7 = (double)num2 * num3 / 2.0;
            int x = (int)((double)weapon.Range * num4);
            Bitmap damageGraph = new Bitmap(this._DamageGraphWidth, this._DamageGraphHeight, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)damageGraph);
            graphics.CompositingQuality = CompositingQuality.HighSpeed;
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.FillRectangle((Brush)new SolidBrush(Color.Transparent), 0, 0, this._DamageGraphWidth, this._DamageGraphHeight);
            Point[] points = new Point[4]
            {
        new Point(0, (int) (num5 - num6)),
        new Point(0, (int) (num5 + num6)),
        new Point(x, (int) (num5 + num7)),
        new Point(x, (int) (num5 - num7))
            };
            SolidBrush solidBrush = new SolidBrush(Color.Red);
            graphics.FillPolygon((Brush)solidBrush, points);
            return damageGraph;
        }

        public void ClearData() => this._Weapons = (WeaponList)null;

        public void BindData(WeaponList weapons, Bitmap[] componentImages)
        {
            this._Weapons = weapons;
            this._Grid.Rows.Clear();
            if (weapons != null)
            {
                int index1 = 0;
                for (int index2 = 0; index2 < weapons.Count; ++index2)
                {
                    if (weapons[index2].Component.Type != ComponentType.AssaultPod)
                    {
                        this._Grid.Rows.Add();
                        DataGridViewRow row = this._Grid.Rows[index1];
                        row.Height = this._RowHeight;
                        row.Cells[0].Value = (object)componentImages[weapons[index2].Component.PictureRef];
                        row.Cells[1].Value = (object)weapons[index2].Component.Name;
                        row.Cells[2].Value = (object)weapons[index2].Speed;
                        row.Cells[2].Tag = (object)index2;
                        row.Cells[3].Value = (object)weapons[index2].EnergyRequired;
                        row.Cells[4].Value = (object)weapons[index2].FireRate;
                        double num1 = (double)weapons[index2].DamageLoss * ((double)weapons[index2].Range / 100.0);
                        int num2 = (int)((double)weapons[index2].RawDamage - num1);
                        row.Cells[5].Value = (object)this.GenerateDamageGraph(weapons[index2]);
                        row.Cells[5].ToolTipText = TextResolver.GetText("Range") + ": " + weapons[index2].Range.ToString() + ", " + TextResolver.GetText("Maximum Damage") + ": " + weapons[index2].RawDamage.ToString() + ", " + TextResolver.GetText("Minimum Damage") + ": " + num2.ToString();
                        ++index1;
                    }
                }
            }
            this.RememberSorting();
        }
    }
}
