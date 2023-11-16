// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HabitatTypeIconView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class HabitatTypeIconView : ListView
    {
        private const int LVM_FIRST = 4096;
        private List<HabitatType> _HabitatTypes = new List<HabitatType>();
        private Bitmap[] _HabitatTypeImages;
        private Control _MouseWheelRefocusControl;

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr handle, int wMsg, int wParam, int lParam);

        public HabitatTypeIconView()
        {
            this.SetDefaults();
            this.VisibleChanged += new EventHandler(this.listView_VisibleChanged);
        }

        private void listView_VisibleChanged(object sender, EventArgs e)
        {
            this.VisibleChanged -= new EventHandler(this.listView_VisibleChanged);
            this.ListViewSetSpacing((ListView)this, 64, 60);
        }

        public void ListViewSetSpacing(ListView listView, int width, int height)
        {
            int lParam = height * 65536 + (width & (int)ushort.MaxValue);
            HabitatTypeIconView.SendMessage(listView.Handle, 4149, 0, lParam);
        }

        private void SetDefaults()
        {
            this.View = View.LargeIcon;
            this.BackColor = Color.FromArgb(32, 32, 48);
            this.BorderStyle = BorderStyle.None;
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.MultiSelect = false;
            this.Scrollable = false;
            this.AutoArrange = true;
            this.ShowItemToolTips = true;
            this.View = View.LargeIcon;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this._MouseWheelRefocusControl != null)
            {
                this._MouseWheelRefocusControl.Focus();
                this._MouseWheelRefocusControl.Select();
            }
            else
                base.OnMouseWheel(e);
        }

        public void BindData(
          List<HabitatType> habitatTypes,
          Bitmap[] habitatTypeImages,
          Control mouseWheelRefocusControl)
        {
            this.SetDefaults();
            this._MouseWheelRefocusControl = mouseWheelRefocusControl;
            this._HabitatTypes = habitatTypes;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.TransparentColor = Color.Black;
            this._HabitatTypeImages = new Bitmap[habitatTypeImages.Length];
            for (int index = 0; index < habitatTypeImages.Length; ++index)
            {
                Bitmap habitatTypeImage = habitatTypeImages[index];
                if (habitatTypeImage != null && habitatTypeImage.PixelFormat != PixelFormat.Undefined)
                {
                    int width = 32;
                    double num = (double)width / (double)habitatTypeImage.Width;
                    int height = (int)((double)habitatTypeImage.Height * num);
                    this._HabitatTypeImages[index] = this.PrescaleImage(habitatTypeImage, 32, 32, width, height);
                    imageList.Images.Add((Image)this._HabitatTypeImages[index]);
                }
            }
            this.LargeImageList = imageList;
            this.SmallImageList = imageList;
            this.SuspendLayout();
            List<ListViewItem> listViewItemList = new List<ListViewItem>();
            this.Items.Clear();
            if (this._HabitatTypes != null)
            {
                for (int index = 0; index < this._HabitatTypes.Count; ++index)
                    listViewItemList.Add(new ListViewItem()
                    {
                        Text = Galaxy.ResolveDescription(this._HabitatTypes[index]),
                        Tag = (object)index,
                        ImageIndex = index
                    });
            }
            if (listViewItemList.Count > 0)
                this.Items.AddRange(listViewItemList.ToArray());
            this.ResumeLayout();
        }

        private Bitmap PrescaleImage(
          Bitmap originalBitmap,
          int imageWidth,
          int imageHeight,
          int width,
          int height)
        {
            Bitmap bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int x = (imageWidth - width) / 2;
            int y = (imageHeight - height) / 2;
            graphics.DrawImage((Image)originalBitmap, new Rectangle(x, y, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public HabitatType SelectedHabitatType
        {
            get
            {
                ListView.SelectedListViewItemCollection selectedItems = this.SelectedItems;
                if (selectedItems.Count != 1)
                    return HabitatType.Undefined;
                //index = -1;
                object tag = selectedItems[0].Tag;
                if (tag is int index)
                { return this._HabitatTypes[index]; }
                else { return HabitatType.Undefined; }
            }
        }
    }
}
