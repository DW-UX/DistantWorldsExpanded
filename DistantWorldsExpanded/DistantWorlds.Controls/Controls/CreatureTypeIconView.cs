// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CreatureTypeIconView
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
    public class CreatureTypeIconView : ListView
    {
        private const int LVM_FIRST = 4096;
        private List<CreatureType> _CreatureTypes;
        private Bitmap[] _CreatureImages;
        protected IFontCache _FontCache;
        private float _FontSize = 12.67f;
        private bool _FontIsBold;
        private Control _MouseWheelRefocusControl;

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr handle, int wMsg, int wParam, int lParam);

        public virtual void SetFontCache(IFontCache fontCache)
        {
            this._FontCache = fontCache;
            if ((double)this._FontSize <= 0.0)
                return;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public void SetFont(float pointSize) => this.SetFont(pointSize, false);

        public void SetFont(float pointSize, bool isBold)
        {
            this._FontSize = pointSize;
            this._FontIsBold = isBold;
            if (this._FontCache == null)
                return;
            this.Font = this._FontCache.GenerateFont(this._FontSize, this._FontIsBold);
        }

        public CreatureTypeIconView()
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
            CreatureTypeIconView.SendMessage(listView.Handle, 4149, 0, lParam);
        }

        private void SetDefaults()
        {
            this.View = View.LargeIcon;
            this.BackColor = Color.FromArgb(32, 32, 48);
            this.BorderStyle = BorderStyle.None;
            this.SetFont(12.67f);
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
          List<CreatureType> creatureTypes,
          Bitmap[] creatureImages,
          Control mouseWheelRefocusControl)
        {
            this.SetDefaults();
            this._MouseWheelRefocusControl = mouseWheelRefocusControl;
            this._CreatureTypes = creatureTypes;
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(32, 32);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.TransparentColor = Color.Black;
            this._CreatureImages = new Bitmap[creatureImages.Length];
            for (int index = 0; index < creatureImages.Length; ++index)
            {
                int width = 32;
                double num = (double)width / (double)creatureImages[index].Width;
                int height = (int)((double)creatureImages[index].Height * num);
                this._CreatureImages[index] = this.PrescaleImage(creatureImages[index], 32, 32, width, height);
                imageList.Images.Add((Image)this._CreatureImages[index]);
            }
            this.LargeImageList = imageList;
            this.SmallImageList = imageList;
            this.SuspendLayout();
            List<ListViewItem> listViewItemList = new List<ListViewItem>();
            this.Items.Clear();
            if (this._CreatureTypes != null)
            {
                for (int index = 0; index < this._CreatureTypes.Count; ++index)
                    listViewItemList.Add(new ListViewItem()
                    {
                        Text = Galaxy.ResolveDescription(this._CreatureTypes[index]),
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

        public CreatureType SelectedCreatureType
        {
            get
            {
                ListView.SelectedListViewItemCollection selectedItems = this.SelectedItems;
                if (selectedItems.Count != 1)
                    return CreatureType.Undefined;
                //index = -1;
                object tag = selectedItems[0].Tag;
                if (tag is int index)
                { return this._CreatureTypes[index]; }
                else { return CreatureType.Undefined; }
            }
        }
    }
}
