// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.PlanetaryFacilityListIconView
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class PlanetaryFacilityListIconView : ListView
    {
        private Galaxy _Galaxy;
        private PlanetaryFacilityList _Facilities;
        protected IFontCache _FontCache;
        private float _FontSize = 6.75f;
        private bool _FontIsBold;

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

        public PlanetaryFacilityListIconView()
        {
            this.View = View.LargeIcon;
            this.BackColor = Color.FromArgb(48, 48, 64);
            this.BorderStyle = BorderStyle.None;
            this.Font = new Font("Verdana", 6.75f, FontStyle.Regular);
            this.SetFont(12.67f);
            this.ForeColor = Color.FromArgb(170, 170, 170);
            this.MultiSelect = false;
            this.Scrollable = true;
            this.AutoArrange = true;
            this.ShowItemToolTips = true;
        }

        public PlanetaryFacility SelectedFacility
        {
            get
            {
                ListView.SelectedListViewItemCollection selectedItems = this.SelectedItems;
                if (selectedItems.Count != 1)
                    return (PlanetaryFacility)null;
                //index = -1;
                object tag = selectedItems[0].Tag;
                if (tag is int index && index >= 0 && index < this._Facilities.Count)
                { return this._Facilities[index]; }
                else
                { return (PlanetaryFacility)null; }
            }
        }

        private Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
        {
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawImage((Image)originalBitmap, new Rectangle(0, 0, width, height));
            graphics.Dispose();
            return bitmap;
        }

        public void InitializeImages(Bitmap[] facilityImages)
        {
            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            int num = 96;
            imageList.ImageSize = new Size(num, num);
            for (int index = 0; index < facilityImages.Length; ++index)
            {
                Size size = this.ResolveImageSize(facilityImages[index], num);
                Bitmap image = this.PrescaleImage(facilityImages[index], size.Width, size.Height);
                Bitmap bitmap = this.MakeImageSquare(image, num);
                imageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            for (int index = 0; index < facilityImages.Length; ++index)
            {
                Size size = this.ResolveImageSize(facilityImages[index], num);
                Bitmap image1 = this.PrescaleImage(facilityImages[index], size.Width, size.Height);
                Bitmap image2 = this.MakeImageSquare(image1, num);
                Bitmap bitmap = this.FadeImage(image2, 0.35f);
                imageList.Images.Add((Image)bitmap);
                image1.Dispose();
                image2.Dispose();
            }
            this.LargeImageList = imageList;
        }

        private Bitmap MakeImageSquare(Bitmap image, int size)
        {
            Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle srcRect = new Rectangle(0, 0, image.Width, image.Height);
            Rectangle destRect = new Rectangle((size - image.Width) / 2, (size - image.Height) / 2, image.Width, image.Height);
            graphics.DrawImage((Image)image, destRect, srcRect, GraphicsUnit.Pixel);
            return bitmap;
        }

        private Size ResolveImageSize(Bitmap image, int facilityMaximumSize)
        {
            int width;
            int height;
            if (image.Width > image.Height)
            {
                double num = (double)image.Height / (double)image.Width;
                width = facilityMaximumSize;
                height = (int)((double)facilityMaximumSize * num);
            }
            else
            {
                double num = (double)image.Width / (double)image.Height;
                height = facilityMaximumSize;
                width = (int)((double)facilityMaximumSize * num);
            }
            return new Size(width, height);
        }

        private Bitmap FadeImage(Bitmap image, float transparencyLevel)
        {
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            Rectangle destRect = new Rectangle(0, 0, image.Width, image.Height);
            ColorMatrix newColorMatrix = new ColorMatrix(new float[5][]
            {
        new float[5]{ 1f, 0.0f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 1f, 0.0f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 1f, 0.0f, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, transparencyLevel, 0.0f },
        new float[5]{ 0.0f, 0.0f, 0.0f, 0.0f, 1f }
            });
            ImageAttributes imageAttrs = new ImageAttributes();
            imageAttrs.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage((Image)image, destRect, 0.0f, 0.0f, (float)image.Width, (float)image.Height, GraphicsUnit.Pixel, imageAttrs);
            return bitmap;
        }

        private Bitmap FadeImage(Bitmap image)
        {
            SolidBrush solidBrush1 = new SolidBrush(Color.FromArgb(192, 48, 48, 64));
            SolidBrush solidBrush2 = new SolidBrush(Color.FromArgb((int)byte.MaxValue, 48, 48, 64));
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage((Image)bitmap);
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            graphics.FillRectangle((Brush)solidBrush2, rect);
            graphics.DrawImage((Image)image, rect);
            graphics.FillRectangle((Brush)solidBrush1, rect);
            bitmap.GetPixel(0, 0);
            return bitmap;
        }

        public void ClearData()
        {
            this._Galaxy = (Galaxy)null;
            this._Facilities = (PlanetaryFacilityList)null;
        }

        public void BindData(Galaxy galaxy, PlanetaryFacilityList facilities, Habitat colony)
        {
            this._Galaxy = galaxy;
            this._Facilities = facilities;
            if (this._Facilities == null)
                this._Facilities = new PlanetaryFacilityList();
            this.SuspendLayout();
            List<ListViewItem> listViewItemList = new List<ListViewItem>();
            this.Items.Clear();
            int num1 = 0;
            List<ListViewItem> facilityItems = this.GenerateFacilityItems(this._Facilities, colony);
            if (facilityItems.Count > 0)
                this.Items.AddRange(facilityItems.ToArray());
            int num2 = num1 + facilityItems.Count;
            this.ResumeLayout();
        }

        private List<ListViewItem> GenerateFacilityItems(
          PlanetaryFacilityList facilities,
          Habitat colony)
        {
            List<ListViewItem> facilityItems = new List<ListViewItem>();
            if (facilities != null)
            {
                for (int index = 0; index < facilities.Count; ++index)
                {
                    PlanetaryFacility facility = facilities[index];
                    if (facility != null)
                    {
                        ListViewItem listViewItem = new ListViewItem();
                        string str1 = facility.Name;
                        string str2 = string.Empty;
                        if (colony != null)
                        {
                            Empire empire = colony.CheckFacilityOwner(facility);
                            if (empire != colony.Owner && empire != null)
                                str2 = string.Format(TextResolver.GetText("Pirate Facility Owner Description"), (object)empire.Name);
                        }
                        if (!string.IsNullOrEmpty(str2))
                            str1 = str1 + " (" + str2 + ")";
                        listViewItem.Text = str1;
                        listViewItem.Tag = (object)index;
                        int num = 0;
                        string str3 = string.Empty;
                        if (!string.IsNullOrEmpty(str2))
                            str3 = str3 + str2.ToUpper(CultureInfo.InvariantCulture) + "\n";
                        if ((double)facility.ConstructionProgress < 1.0)
                        {
                            str3 = (facility.ConstructionProgress * 100f).ToString("0") + "% " + TextResolver.GetText("Complete").ToLower(CultureInfo.InvariantCulture);
                            if (this.LargeImageList != null && this.LargeImageList.Images != null && this.LargeImageList.Images.Count > 0)
                                num = this.LargeImageList.Images.Count / 2;
                        }
                        if (facility.Type == PlanetaryFacilityType.Wonder)
                        {
                            if (!string.IsNullOrEmpty(str3))
                                str3 += "\n";
                            str3 += this._Galaxy.ResolveWonderDescription(Galaxy.PlanetaryFacilityDefinitionsStatic[facility.PlanetaryFacilityDefinitionId]);
                        }
                        if (facility.Maintenance > 0.0)
                        {
                            if (!string.IsNullOrEmpty(str3))
                                str3 += "\n";
                            str3 = str3 + TextResolver.GetText("Facility Maintenance Cost") + ": " + facility.Maintenance.ToString("#,###,##0") + " " + TextResolver.GetText("credits");
                            if ((double)facility.ConstructionProgress < 1.0)
                                str3 = str3 + " (" + TextResolver.GetText("when completed") + ")";
                        }
                        listViewItem.ToolTipText = str3;
                        listViewItem.ImageIndex = (int)facility.PictureRef + num;
                        facilityItems.Add(listViewItem);
                    }
                }
            }
            return facilityItems;
        }
    }
}
