// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CharacterTroopListIconView
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
    public class CharacterTroopListIconView : ListView
    {
        private Empire _LocationEmpire;
        private CharacterList _Characters;
        private CharacterList _InvadingCharacters;
        private TroopList _Troops;
        private TroopList _TroopsToRecruit;
        private TroopList _InvadingTroops;
        private static ImageList _ImageList;
        private static int _FadedTroopsImageOffset;
        private static int _InfantryImageOffset;
        private static int _ArmoredImageOffset;
        private static int _ArtilleryImageOffset;
        private static int _SpecialForcesImageOffset;
        private static int _PirateRaiderImageOffset;
        private static int _CharacterImageOffset;
        private static List<string> _CharacterImageKeys = new List<string>();
        protected IFontCache _FontCache;
        private float _FontSize = 6.75f;
        private bool _FontIsBold;
        private Font _BoldFont;

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

        public CharacterTroopListIconView()
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

        public object SelectedObject
        {
            get
            {
                ListView.SelectedListViewItemCollection selectedItems = this.SelectedItems;
                if (selectedItems.Count != 1)
                    return (object)null;
                //num1 = -1;
                object tag = selectedItems[0].Tag;
                if (tag is int num1)
                {
                    int num2 = 0;
                    int count = this._Characters.Count;
                    if (num1 >= num2 && num1 < count)
                        return (object)this._Characters[num1 - num2];
                    int num3 = count;
                    int num4 = count + this._TroopsToRecruit.Count;
                    if (num1 >= num3 && num1 < num4)
                        return (object)this._TroopsToRecruit[num1 - num3];
                    int num5 = num4;
                    int num6 = num4 + this._Troops.Count;
                    if (num1 >= num5 && num1 < num6)
                        return (object)this._Troops[num1 - num5];
                    int num7 = num6;
                    int num8 = num6 + this._InvadingTroops.Count;
                    if (num1 >= num7 && num1 < num8)
                        return (object)this._InvadingTroops[num1 - num7];
                    int num9 = num8;
                    int num10 = num8 + this._InvadingCharacters.Count;
                    return num1 >= num9 && num1 < num10 ? (object)this._InvadingCharacters[num1 - num9] : (object)null;
                }
                else
                {
                    return (object)null;
                }

            }
        }

        public int SelectedIndex
        {
            get
            {
                ListView.SelectedListViewItemCollection selectedItems = this.SelectedItems;
                return selectedItems.Count == 1 ? this.Items.IndexOf(selectedItems[0]) : -1;
            }
        }

        public void SetSelectedItem(int index)
        {
            if (index < 0 || index >= this.Items.Count)
                return;
            this.Items[index].Selected = true;
        }

        private static Bitmap PrescaleImage(Bitmap originalBitmap, int width, int height)
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

        public void Kickstart()
        {
            this._BoldFont = this._FontCache.GenerateFont(this._FontSize, true);
            this.LargeImageList = CharacterTroopListIconView._ImageList;
        }

        public static void InitializeImages(
          Bitmap[] troopImagesInfantry,
          Bitmap[] troopImagesArmored,
          Bitmap[] troopImagesArtillery,
          Bitmap[] troopImagesSpecialForces,
          Bitmap[] troopImagesPirateRaider)
        {
            if (CharacterTroopListIconView._ImageList != null)
            {
                if (CharacterTroopListIconView._ImageList.Images != null)
                {
                    foreach (Image image in CharacterTroopListIconView._ImageList.Images)
                        image.Dispose();
                    CharacterTroopListIconView._ImageList.Images.Clear();
                }
                CharacterTroopListIconView._ImageList.Dispose();
            }
            CharacterTroopListIconView._CharacterImageKeys.Clear();
            CharacterTroopListIconView._ImageList = new ImageList();
            CharacterTroopListIconView._ImageList.ColorDepth = ColorDepth.Depth32Bit;
            int num = 56;
            CharacterTroopListIconView._ImageList.ImageSize = new Size(num, num);
            CharacterTroopListIconView._InfantryImageOffset = 0;
            CharacterTroopListIconView._ArmoredImageOffset = CharacterTroopListIconView._InfantryImageOffset + troopImagesInfantry.Length;
            CharacterTroopListIconView._ArtilleryImageOffset = CharacterTroopListIconView._ArmoredImageOffset + troopImagesArmored.Length;
            CharacterTroopListIconView._SpecialForcesImageOffset = CharacterTroopListIconView._ArtilleryImageOffset + troopImagesArtillery.Length;
            CharacterTroopListIconView._PirateRaiderImageOffset = CharacterTroopListIconView._SpecialForcesImageOffset + troopImagesSpecialForces.Length;
            for (int index = 0; index < troopImagesInfantry.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesInfantry[index], num);
                Bitmap image = CharacterTroopListIconView.PrescaleImage(troopImagesInfantry[index], size.Width, size.Height);
                Bitmap bitmap = CharacterTroopListIconView.MakeImageSquare(image, num);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            for (int index = 0; index < troopImagesArmored.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesArmored[index], num);
                Bitmap image = CharacterTroopListIconView.PrescaleImage(troopImagesArmored[index], size.Width, size.Height);
                Bitmap bitmap = CharacterTroopListIconView.MakeImageSquare(image, num);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            for (int index = 0; index < troopImagesArtillery.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesArtillery[index], num);
                Bitmap image = CharacterTroopListIconView.PrescaleImage(troopImagesArtillery[index], size.Width, size.Height);
                Bitmap bitmap = CharacterTroopListIconView.MakeImageSquare(image, num);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            for (int index = 0; index < troopImagesSpecialForces.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesSpecialForces[index], num);
                Bitmap image = CharacterTroopListIconView.PrescaleImage(troopImagesSpecialForces[index], size.Width, size.Height);
                Bitmap bitmap = CharacterTroopListIconView.MakeImageSquare(image, num);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            for (int index = 0; index < troopImagesPirateRaider.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesPirateRaider[index], num);
                Bitmap image = CharacterTroopListIconView.PrescaleImage(troopImagesPirateRaider[index], size.Width, size.Height);
                Bitmap bitmap = CharacterTroopListIconView.MakeImageSquare(image, num);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image.Dispose();
            }
            CharacterTroopListIconView._FadedTroopsImageOffset = CharacterTroopListIconView._ImageList.Images.Count;
            for (int index = 0; index < troopImagesInfantry.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesInfantry[index], num);
                Bitmap image1 = CharacterTroopListIconView.PrescaleImage(troopImagesInfantry[index], size.Width, size.Height);
                Bitmap image2 = CharacterTroopListIconView.MakeImageSquare(image1, num);
                Bitmap bitmap = CharacterTroopListIconView.FadeImage(image2, 0.35f);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image1.Dispose();
                image2.Dispose();
            }
            for (int index = 0; index < troopImagesArmored.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesArmored[index], num);
                Bitmap image3 = CharacterTroopListIconView.PrescaleImage(troopImagesArmored[index], size.Width, size.Height);
                Bitmap image4 = CharacterTroopListIconView.MakeImageSquare(image3, num);
                Bitmap bitmap = CharacterTroopListIconView.FadeImage(image4, 0.35f);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image3.Dispose();
                image4.Dispose();
            }
            for (int index = 0; index < troopImagesArtillery.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesArtillery[index], num);
                Bitmap image5 = CharacterTroopListIconView.PrescaleImage(troopImagesArtillery[index], size.Width, size.Height);
                Bitmap image6 = CharacterTroopListIconView.MakeImageSquare(image5, num);
                Bitmap bitmap = CharacterTroopListIconView.FadeImage(image6, 0.35f);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image5.Dispose();
                image6.Dispose();
            }
            for (int index = 0; index < troopImagesSpecialForces.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesSpecialForces[index], num);
                Bitmap image7 = CharacterTroopListIconView.PrescaleImage(troopImagesSpecialForces[index], size.Width, size.Height);
                Bitmap image8 = CharacterTroopListIconView.MakeImageSquare(image7, num);
                Bitmap bitmap = CharacterTroopListIconView.FadeImage(image8, 0.35f);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image7.Dispose();
                image8.Dispose();
            }
            for (int index = 0; index < troopImagesPirateRaider.Length; ++index)
            {
                Size size = CharacterTroopListIconView.ResolveImageSize(troopImagesPirateRaider[index], num);
                Bitmap image9 = CharacterTroopListIconView.PrescaleImage(troopImagesPirateRaider[index], size.Width, size.Height);
                Bitmap image10 = CharacterTroopListIconView.MakeImageSquare(image9, num);
                Bitmap bitmap = CharacterTroopListIconView.FadeImage(image10, 0.35f);
                CharacterTroopListIconView._ImageList.Images.Add((Image)bitmap);
                image9.Dispose();
                image10.Dispose();
            }
            CharacterTroopListIconView._CharacterImageOffset = CharacterTroopListIconView._ImageList.Images.Count;
        }

        private static Bitmap MakeImageSquare(Bitmap image, int size)
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

        private static Size ResolveImageSize(Bitmap image, int troopMaximumSize)
        {
            int width;
            int height;
            if (image.Width > image.Height)
            {
                double num = (double)image.Height / (double)image.Width;
                width = troopMaximumSize;
                height = (int)((double)troopMaximumSize * num);
            }
            else
            {
                double num = (double)image.Width / (double)image.Height;
                height = troopMaximumSize;
                width = (int)((double)troopMaximumSize * num);
            }
            return new Size(width, height);
        }

        private static Bitmap FadeImage(Bitmap image, float transparencyLevel)
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
            this._Characters = (CharacterList)null;
            this._InvadingCharacters = (CharacterList)null;
            this._Troops = (TroopList)null;
            this._TroopsToRecruit = (TroopList)null;
            this._InvadingTroops = (TroopList)null;
        }

        public void BindData(
          Empire locationEmpire,
          CharacterList characters,
          CharacterList invadingCharacters,
          TroopList troops,
          TroopList troopsToRecruit,
          TroopList invadingTroops,
          CharacterImageCache characterImageCache)
        {
            this._LocationEmpire = locationEmpire;
            this._Characters = characters != null ? characters : new CharacterList();
            this._InvadingCharacters = invadingCharacters != null ? invadingCharacters : new CharacterList();
            this._Troops = troops != null ? troops : new TroopList();
            this._TroopsToRecruit = troopsToRecruit != null ? troopsToRecruit : new TroopList();
            this._InvadingTroops = invadingTroops != null ? invadingTroops : new TroopList();
            this.SuspendLayout();
            List<ListViewItem> listViewItemList = new List<ListViewItem>();
            this.Items.Clear();
            int indexStart1 = 0;
            List<ListViewItem> characterItems1 = this.GenerateCharacterItems(this._Characters, indexStart1, CharacterTroopListIconView._CharacterImageOffset, "", characterImageCache);
            if (characterItems1.Count > 0)
                this.Items.AddRange(characterItems1.ToArray());
            int indexStart2 = indexStart1 + characterItems1.Count;
            List<ListViewItem> troopItems1 = this.GenerateTroopItems(this._TroopsToRecruit, indexStart2, CharacterTroopListIconView._FadedTroopsImageOffset, TextResolver.GetText("Recruiting") + " - ");
            if (troopItems1.Count > 0)
                this.Items.AddRange(troopItems1.ToArray());
            int indexStart3 = indexStart2 + troopItems1.Count;
            List<ListViewItem> troopItems2 = this.GenerateTroopItems(this._Troops, indexStart3, 0, "");
            if (troopItems2.Count > 0)
                this.Items.AddRange(troopItems2.ToArray());
            int indexStart4 = indexStart3 + troopItems2.Count;
            List<ListViewItem> troopItems3 = this.GenerateTroopItems(this._InvadingTroops, indexStart4, 0, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " - ");
            if (troopItems3.Count > 0)
                this.Items.AddRange(troopItems3.ToArray());
            List<ListViewItem> characterItems2 = this.GenerateCharacterItems(this._InvadingCharacters, indexStart4 + troopItems3.Count, CharacterTroopListIconView._CharacterImageOffset, TextResolver.GetText("Invading").ToUpper(CultureInfo.InvariantCulture) + " - ", characterImageCache);
            if (characterItems2.Count > 0)
                this.Items.AddRange(characterItems2.ToArray());
            this.ResumeLayout();
        }

        private Bitmap AddCacheCharacterImage(
          Character character,
          CharacterImageCache characterImageCache,
          out int imageIndex)
        {
            imageIndex = 0;
            string characterImageKey = characterImageCache.ObtainCharacterImageKey(character);
            int num = -1;
            for (int index = 0; index < CharacterTroopListIconView._CharacterImageKeys.Count; ++index)
            {
                if (CharacterTroopListIconView._CharacterImageKeys[index] == characterImageKey)
                {
                    num = index;
                    break;
                }
            }
            if (num < 0)
            {
                Bitmap originalBitmap = characterImageCache.ObtainCharacterImage(character);
                if (originalBitmap != null && originalBitmap.PixelFormat != PixelFormat.Undefined)
                {
                    Bitmap image = CharacterTroopListIconView.PrescaleImage(originalBitmap, this.LargeImageList.ImageSize.Width, this.LargeImageList.ImageSize.Height);
                    Bitmap bitmap = image;
                    originalBitmap = CharacterTroopListIconView.MakeImageSquare(image, this.LargeImageList.ImageSize.Width);
                    bitmap.Dispose();
                }
                CharacterTroopListIconView._CharacterImageKeys.Add(characterImageKey);
                imageIndex = CharacterTroopListIconView._CharacterImageKeys.Count - 1;
                return originalBitmap;
            }
            imageIndex = num;
            return (Bitmap)null;
        }

        private List<ListViewItem> GenerateCharacterItems(
          CharacterList characters,
          int indexStart,
          int imageOffset,
          string textPrefix,
          CharacterImageCache characterImageCache)
        {
            List<ListViewItem> characterItems = new List<ListViewItem>();
            if (characters != null)
            {
                for (int index = 0; index < characters.Count; ++index)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Text = textPrefix + characters[index].Name;
                    listViewItem.Tag = (object)(index + indexStart);
                    string str1 = Galaxy.ResolveDescription(characters[index].Role) + "\n";
                    if (characters[index].Empire != null && characters[index].Empire != this._LocationEmpire)
                        str1 = str1 + characters[index].Empire.Name + "\n";
                    string str2 = str1 + Galaxy.ResolveCharacterDescription(characters[index]);
                    if (characters[index].Mission != null)
                        str2 = str2 + "\n" + TextResolver.GetText("Mission") + ": " + Galaxy.ResolveDescription(characters[index].Mission.Type);
                    listViewItem.ToolTipText = str2;
                    int imageIndex = -1;
                    Bitmap bitmap = this.AddCacheCharacterImage(characters[index], characterImageCache, out imageIndex);
                    if (bitmap != null)
                        this.LargeImageList.Images.Add((Image)bitmap);
                    listViewItem.ImageIndex = imageOffset + imageIndex;
                    if (characters[index].Empire != null && characters[index].Empire != this._LocationEmpire)
                    {
                        listViewItem.ForeColor = characters[index].Empire.MainColor;
                        listViewItem.Font = this._BoldFont;
                    }
                    characterItems.Add(listViewItem);
                }
            }
            return characterItems;
        }

        private List<ListViewItem> GenerateTroopItems(
          TroopList troops,
          int indexStart,
          int imageOffset,
          string textPrefix)
        {
            List<ListViewItem> troopItems = new List<ListViewItem>();
            if (troops != null)
            {
                for (int index = 0; index < troops.Count; ++index)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    string empty = string.Empty;
                    if (troops[index].Garrisoned)
                        empty += "* ";
                    string str1 = empty + textPrefix + troops[index].Name + " (" + Galaxy.ResolveTroopStrengthDescription(troops[index]) + ", " + troops[index].OverallAttackStrength.ToString("####0") + ", " + troops[index].OverallDefendStrength.ToString("####0") + ")";
                    listViewItem.Text = str1;
                    listViewItem.Tag = (object)(index + indexStart);
                    string str2 = string.Empty + TextResolver.GetText("Readiness") + ": " + troops[index].Readiness.ToString("##0") + "\n" + TextResolver.GetText("Attack Strength") + ": " + troops[index].AttackStrength.ToString("##0") + "\n" + TextResolver.GetText("Overall Attack Strength") + ": " + troops[index].OverallAttackStrength.ToString("####0") + "\n" + TextResolver.GetText("Defend Strength") + ": " + troops[index].DefendStrength.ToString("##0") + "\n" + TextResolver.GetText("Overall Defend Strength") + ": " + troops[index].OverallDefendStrength.ToString("####0");
                    if (troops[index].Garrisoned)
                        str2 = str2 + "\n" + TextResolver.GetText("Garrisoned").ToUpper(CultureInfo.InvariantCulture);
                    listViewItem.ToolTipText = str2;
                    int num = 0;
                    switch (troops[index].Type)
                    {
                        case TroopType.Infantry:
                            num = CharacterTroopListIconView._InfantryImageOffset + troops[index].PictureRef + imageOffset;
                            break;
                        case TroopType.Armored:
                            num = CharacterTroopListIconView._ArmoredImageOffset + troops[index].PictureRef + imageOffset;
                            break;
                        case TroopType.Artillery:
                            num = CharacterTroopListIconView._ArtilleryImageOffset + troops[index].PictureRef + imageOffset;
                            break;
                        case TroopType.SpecialForces:
                            num = CharacterTroopListIconView._SpecialForcesImageOffset + troops[index].PictureRef + imageOffset;
                            break;
                        case TroopType.PirateRaider:
                            num = CharacterTroopListIconView._PirateRaiderImageOffset + troops[index].PictureRef + imageOffset;
                            break;
                    }
                    listViewItem.ImageIndex = num;
                    if (troops[index].Garrisoned)
                        listViewItem.BackColor = Color.FromArgb(0, 192, 0);
                    troopItems.Add(listViewItem);
                }
            }
            return troopItems;
        }
    }
}
