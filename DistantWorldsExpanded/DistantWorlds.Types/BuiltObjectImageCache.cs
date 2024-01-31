// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectImageCache
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using BaconDistantWorlds;

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DistantWorlds.Types
{
    public class BuiltObjectImageCache
    {
        private object _LockObject = new object();

        public List<string> _Filepaths = new List<string>();

        private Dictionary<int, BuiltObjectImageData> _ImageData = new Dictionary<int, BuiltObjectImageData>();

        private Dictionary<int, DateTime> _LastUsage = new Dictionary<int, DateTime>();

        private ConcurrentDictionary<int, BuiltObjectImageData> _ImageDataSmall = new ConcurrentDictionary<int, BuiltObjectImageData>();

        private int _ImageSmallSize = 30;

        private string _ApplicationStartupPath;

        private string _CustomizationSetName;

        public event EventHandler LoadProgress;

        private void DoLoadProgress()
        {
            if (this.LoadProgress != null)
            {
                this.LoadProgress(this, new EventArgs());
            }
        }

        public void Initialize(string applicationStartupPath, string customizationSetName)
        {
            _ApplicationStartupPath = applicationStartupPath;
            _CustomizationSetName = customizationSetName;
            ClearImageCache(_ImageData);
            ClearImageCache(_ImageDataSmall);
            GenerateBuiltObjectImageFilepaths();
        }

        public BuiltObjectImageData ObtainImageDataSmall(BuiltObject builtObject)
        {
            if (builtObject != null)
            {
                return ObtainImageDataSmall(builtObject.PictureRef);
            }
            return null;
        }

        public BuiltObjectImageData ObtainImageDataSmall(int pictureRef)
        {
            //BuiltObjectImageData builtObjectImageData = null;
            BuiltObjectImageData obj;
            if (_ImageDataSmall.TryGetValue(pictureRef, out obj))
            {
                //if (obj is BuiltObjectImageData)
                //{
                return obj;
                //}
                //CacheImageSmall(pictureRef);
                //return (BuiltObjectImageData)_ImageDataSmall[pictureRef];
            }
            else
            {
                CacheImageSmall(pictureRef);
                return _ImageDataSmall[pictureRef];
            }
        }

        public BuiltObjectImageData ObtainImageData(BuiltObject builtObject)
        {
            if (builtObject != null)
            {
                return ObtainImageData(builtObject.PictureRef);
            }
            return null;
        }

        public BuiltObjectImageData ObtainImageData(int pictureRef)
        {
            //BuiltObjectImageData builtObjectImageData = null;
            BuiltObjectImageData builtObjectImageData;
            if (_ImageData.TryGetValue(pictureRef, out builtObjectImageData) && builtObjectImageData != null)
            {
                //if (obj is BuiltObjectImageData)
                //{
                //builtObjectImageData = (BuiltObjectImageData)obj;
                _LastUsage[pictureRef] = DateTime.Now;
                //}
                //else
                //{
                //    CacheImage(pictureRef);
                //    builtObjectImageData = (BuiltObjectImageData)_ImageData[pictureRef];
                //}
            }
            else
            {
                CacheImage(pictureRef);
                builtObjectImageData = _ImageData[pictureRef];
            }
            return builtObjectImageData;
        }

        public BuiltObjectImageData FastGetImageData(int pictureRef)
        {
            BuiltObjectImageData result = null;
            _LastUsage[pictureRef] = DateTime.Now;
            BuiltObjectImageData obj;
            if (_ImageData.TryGetValue(pictureRef, out obj))
            {
                result = obj;
            }
            else
            {
                //obj = _ImageDataSmall[pictureRef];
                //if (obj != null)
                if (_ImageDataSmall.TryGetValue(pictureRef, out obj))
                {
                    result = obj;
                }
                _ImageData[pictureRef] = obj;
                LoadImageDataDelayed(pictureRef);
            }
            return result;
        }

        //private void LoadImageDataDelayedDelegate(object key)
        //{
        //    if (key != null && key is int)
        //    {
        //        int pictureRef = (int)key;
        //        CacheImage(pictureRef);
        //    }
        //}

        public void LoadImageDataDelayed(int pictureRef)
        {
            //ThreadPool.QueueUserWorkItem(LoadImageDataDelayedDelegate, pictureRef);
            Task.Factory.StartNew(() => CacheImage(pictureRef));
        }

        public void ClearOldImages()
        {
            ClearOldImages(60);
        }

        public void ClearOldImages(int maximumAgeInSeconds)
        {
            DateTime dateTime = DateTime.Now.Subtract(new TimeSpan(0, 0, maximumAgeInSeconds));
            List<int> list = new List<int>();
            lock (_LockObject)
            {
                foreach (int key in _ImageData.Keys)
                {
                    if (_LastUsage[key] > dateTime)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData;
                    if (_ImageData.TryGetValue(key, out builtObjectImageData))
                    {
                        //if (obj is Bitmap)
                        //{
                        //    ((Bitmap)obj).Dispose();
                        //}
                        //else if (obj is BuiltObjectImageData)
                        //{
                        //BuiltObjectImageData builtObjectImageData = (BuiltObjectImageData)obj;
                        if (builtObjectImageData != null)
                        {
                            if (builtObjectImageData.Image != null && builtObjectImageData.Image.PixelFormat != 0)
                            {
                                builtObjectImageData.Image.Dispose();
                            }
                            if (builtObjectImageData.MaskImage != null && builtObjectImageData.MaskImage.PixelFormat != 0)
                            {
                                builtObjectImageData.MaskImage.Dispose();
                            }
                            builtObjectImageData.Image = null;
                            builtObjectImageData.MaskImage = null;
                            builtObjectImageData.ThrusterLocations = null;
                            builtObjectImageData.LightPoints = null;
                        }
                        //}
                    }
                    list.Add(key);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    _ImageData.Remove(list[i]);
                    _LastUsage.Remove(list[i]);
                }
            }
        }

        public Bitmap[] GetImagesSmall()
        {
            List<Bitmap> list = new List<Bitmap>();
            BuiltObjectImageData builtObjectImageData;
            for (int i = 0; i < _Filepaths.Count; i++)
            {
                if (_ImageDataSmall.TryGetValue(i, out builtObjectImageData))
                {
                    //BuiltObjectImageData builtObjectImageData = (BuiltObjectImageData)obj;
                    if (builtObjectImageData != null)
                    {
                        list.Add(builtObjectImageData.Image);
                    }
                }
            }
            return list.ToArray();
        }

        public Bitmap ObtainImageSmall(BuiltObject builtObject)
        {
            Bitmap result = null;
            if (builtObject != null)
            {
                result = ObtainImageSmall(builtObject.PictureRef);
            }
            return result;
        }

        public Bitmap ObtainImageSmall(int pictureRef)
        {
            //Bitmap bitmap = null;
            BuiltObjectImageData builtObjectImageData;
            if (_ImageDataSmall.TryGetValue(pictureRef, out builtObjectImageData))
            {
                //if (obj is BuiltObjectImageData)
                //{
                //BuiltObjectImageData builtObjectImageData = (BuiltObjectImageData)obj;
                if (builtObjectImageData != null)
                {
                    return builtObjectImageData.Image;
                }
                return CacheImageSmall(pictureRef);
                //}
                //return CacheImageSmall(pictureRef);
            }
            return CacheImageSmall(pictureRef);
        }

        public Bitmap ObtainImage(BuiltObject builtObject)
        {
            Bitmap result = null;
            if (builtObject != null)
            {
                result = ObtainImage(builtObject.PictureRef);
            }
            return result;
        }

        public Bitmap ObtainImage(int pictureRef)
        {
            Bitmap bitmap = null;
            BuiltObjectImageData builtObjectImageData;
            if (_ImageData.TryGetValue(pictureRef, out builtObjectImageData))
            {
                //if (obj is BuiltObjectImageData)
                //{
                //BuiltObjectImageData builtObjectImageData = (BuiltObjectImageData)obj;
                if (builtObjectImageData != null)
                {
                    bitmap = builtObjectImageData.Image;
                    if (bitmap.PixelFormat == PixelFormat.Undefined)
                    {
                        bitmap = CacheImage(pictureRef);
                    }
                    else
                    {
                        _LastUsage[pictureRef] = DateTime.Now;
                    }
                }
                else
                {
                    bitmap = CacheImage(pictureRef);
                }
                //}
                //else
                //{
                //    bitmap = CacheImage(pictureRef);
                //}
            }
            else
            {
                bitmap = CacheImage(pictureRef);
            }
            return bitmap;
        }

        //private Bitmap CacheImageSmall(BuiltObject builtObject)
        //{
        //    Bitmap result = null;
        //    if (builtObject != null)
        //    {
        //        result = CacheImageSmall(builtObject.PictureRef);
        //    }
        //    return result;
        //}

        private Bitmap CacheImageSmall(int pictureRef)
        {
            string filepath = ResolveImageFilename(pictureRef);
            return LoadImageDataSmall(pictureRef, filepath);
        }

        //private Bitmap CacheImage(BuiltObject builtObject)
        //{
        //    Bitmap result = null;
        //    if (builtObject != null)
        //    {
        //        result = CacheImage(builtObject.PictureRef);
        //    }
        //    return result;
        //}

        private Bitmap CacheImage(int pictureRef)
        {
            string filepath = ResolveImageFilename(pictureRef);
            Bitmap bitmap = LoadImageData(pictureRef, filepath);
            if (bitmap != null)
            {
                _LastUsage[pictureRef] = DateTime.Now;
            }
            return bitmap;
        }

        public string ResolveImageFilename(BuiltObject builtObject)
        {
            string result = string.Empty;
            if (builtObject != null)
            {
                result = ResolveImageFilename(builtObject.PictureRef);
            }
            return result;
        }

        public string ResolveImageFilename(int pictureRef)
        {
            string result = string.Empty;
            if (pictureRef >= 0 && pictureRef < _Filepaths.Count)
            {
                result = _Filepaths[pictureRef];
            }
            return result;
        }

        //private Bitmap LoadImage(BuiltObject builtObject)
        //{
        //    Bitmap result = null;
        //    if (builtObject != null && builtObject.PictureRef >= 0)
        //    {
        //        result = LoadImage(builtObject.PictureRef);
        //    }
        //    return result;
        //}

        //private Bitmap LoadImage(int pictureRef)
        //{
        //    string filepath = ResolveImageFilename(pictureRef);
        //    return LoadImage(filepath);
        //}

        //private Bitmap LoadImage(string filepath)
        //{
        //    Bitmap result = null;
        //    if (File.Exists(filepath))
        //    {
        //        result = SafeLoadImage(filepath);
        //    }
        //    return result;
        //}

        private Bitmap LoadImageData(int key, string filepath)
        {
            Bitmap result = null;
            if (File.Exists(filepath))
            {
                int imageSize = 0;
                Bitmap maskImage = null;
                List<Rectangle> thrusterLocations = new List<Rectangle>();
                List<Point> lightPoints = new List<Point>();
                result = LoadSingleBuiltObjectImage(filepath, 1.0, out imageSize, out maskImage, out thrusterLocations, out lightPoints);
                BuiltObjectImageData value = new BuiltObjectImageData(result, maskImage, imageSize, thrusterLocations, lightPoints, BuiltObjectImageSize.Fullsize);
                lock (_LockObject)
                {
                    _ImageData[key] = value;
                    return result;
                }
            }
            return result;
        }

        private Bitmap SafeLoadImage(string imagePath)
        {
            Bitmap result = null;
            if (File.Exists(imagePath))
            {
                try
                {
                    return GraphicsHelper.LoadImageFromFilePath(imagePath);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return result;
        }

        private Bitmap LoadImageDataSmall(int key, string filepath)
        {
            Bitmap result = null;
            if (File.Exists(filepath))
            {
                int imageSize = 0;
                Bitmap maskImage = null;
                List<Rectangle> thrusterLocations = new List<Rectangle>();
                List<Point> lightPoints = new List<Point>();
                result = LoadSingleBuiltObjectImageSmall(filepath, 1.0, out imageSize, out maskImage, out thrusterLocations, out lightPoints);
                BuiltObjectImageData value = new BuiltObjectImageData(result, maskImage, imageSize, thrusterLocations, lightPoints, BuiltObjectImageSize.Small);
                lock (_LockObject)
                {
                    _ImageDataSmall[key] = value;
                    return result;
                }
            }
            return result;
        }

        private Bitmap LoadSingleBuiltObjectImageSmall(string filepath, double scaleFactor, out int imageSize, out Bitmap maskImage, out List<Rectangle> thrusterLocations, out List<Point> lightPoints)
        {
            Bitmap bitmap = null;
            imageSize = 0;
            maskImage = null;
            thrusterLocations = new List<Rectangle>();
            lightPoints = new List<Point>();
            Color color = Color.FromArgb(0, 0, 0, 0);
            bool flag = false;
            if (File.Exists(filepath))
            {
                bitmap = SafeLoadImage(filepath);
                if (filepath.ToLower(CultureInfo.InvariantCulture).Substring(filepath.Length - 4, 4) == ".png")
                {
                    flag = true;
                }
                else
                {
                    color = Color.Black;
                }
                Bitmap bitmap2 = CropImageContent(bitmap, 4, color);
                bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                thrusterLocations = ScanForThrusterLocations(bitmap2, Color.Blue, 20);
                lightPoints = ScanForColorPoints(bitmap2, Color.Yellow, 20);
                Bitmap bitmap3 = ScaleBuiltObjectImageData(bitmap2, ref thrusterLocations, ref lightPoints, _ImageSmallSize, _ImageSmallSize);
                if (!flag)
                {
                    maskImage = ScanForOutline(bitmap3, color, Color.Black);
                    bitmap3.MakeTransparent(Color.Black);
                    imageSize = DetermineImageContentSize(bitmap3);
                }
                else
                {
                    maskImage = ScanForOutlineAndDetermineContentSize(bitmap3, color, Color.Black, out imageSize);
                }
                bitmap.Dispose();
                bitmap2.Dispose();
                return bitmap3;
            }
            return null;
        }

        public string CheckLoadSmallImage(int index, string fullPathWithoutSuffix, string fullPathWithoutSuffixCustom)
        {
            Bitmap bitmap = null;
            string empty = string.Empty;
            if (File.Exists(fullPathWithoutSuffixCustom + ".png") || File.Exists(fullPathWithoutSuffixCustom + ".bmp"))
            {
                fullPathWithoutSuffix = fullPathWithoutSuffixCustom;
            }
            Color color = Color.FromArgb(0, 0, 0, 0);
            bool flag = false;
            if (File.Exists(fullPathWithoutSuffix + ".png"))
            {
                bitmap = SafeLoadImage(fullPathWithoutSuffix + ".png");
                empty = fullPathWithoutSuffix + ".png";
                flag = true;
            }
            else
            {
                bitmap = SafeLoadImage(fullPathWithoutSuffix + ".bmp");
                empty = fullPathWithoutSuffix + ".bmp";
                color = Color.Black;
            }
            if (bitmap == null)
            {
                throw new ApplicationException("Could not find required image: " + fullPathWithoutSuffix + ".png");
            }
            int num = 0;
            Bitmap bitmap2 = null;
            List<Rectangle> list = new List<Rectangle>();
            List<Point> list2 = new List<Point>();
            Bitmap bitmap3 = CropImageContent(bitmap, 4, color);
            bitmap3.RotateFlip(RotateFlipType.Rotate90FlipNone);
            list = ScanForThrusterLocations(bitmap3, Color.Blue, 20);
            list2 = ScanForColorPoints(bitmap3, Color.Yellow, 20);
            Bitmap bitmap4 = ScaleBuiltObjectImageData(bitmap3, ref list, ref list2, _ImageSmallSize, _ImageSmallSize);
            bitmap2 = ScanForOutline(bitmap4, color, Color.Black);
            if (!flag)
            {
                bitmap4.MakeTransparent(Color.Black);
            }
            num = DetermineImageContentSize(bitmap4);
            BuiltObjectImageData value = new BuiltObjectImageData(bitmap4, bitmap2, num, list, list2, BuiltObjectImageSize.Small);
            //_ImageDataSmall[index] = value;
            _ImageDataSmall.AddOrUpdate(index, value, (key, oldValue) => throw new ApplicationException("Key duplicate on shipset dictionary, shoud not be posible"));
            //index++;
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                bitmap.Dispose();
                bitmap = null;
            }
            if (bitmap3 != null && bitmap3.PixelFormat != 0)
            {
                bitmap3.Dispose();
                bitmap3 = null;
            }
            return empty;
        }

        private Bitmap ScaleBuiltObjectImageData(Bitmap image, ref List<Rectangle> thrusterLocations, ref List<Point> lightPoints, int maxWidth, int maxHeight)
        {
            double num = Math.Min((double)maxWidth / (double)image.Width, (double)maxHeight / (double)image.Height);
            for (int i = 0; i < thrusterLocations.Count; i++)
            {
                Rectangle rectangle = thrusterLocations[i];
                Rectangle value = new Rectangle((int)((double)rectangle.X * num), (int)((double)rectangle.Y * num), (int)((double)rectangle.Width * num), (int)((double)rectangle.Height * num));
                thrusterLocations[i] = value;
            }
            for (int j = 0; j < lightPoints.Count; j++)
            {
                Point point = lightPoints[j];
                Point value2 = new Point((int)((double)point.X * num), (int)((double)point.Y * num));
                lightPoints[j] = value2;
            }
            Bitmap result = GraphicsHelper.ScaleLimitImage(image, (int)((double)image.Width * num), (int)((double)image.Height * num), 1f);
            image.Dispose();
            return result;
        }

        private Bitmap LoadSingleBuiltObjectImage(string filepath, double scaleFactor, out int imageSize, out Bitmap maskImage, out List<Rectangle> thrusterLocations, out List<Point> lightPoints)
        {
            Bitmap bitmap = null;
            imageSize = 0;
            maskImage = null;
            thrusterLocations = new List<Rectangle>();
            lightPoints = new List<Point>();
            Color color = Color.FromArgb(0, 0, 0, 0);
            bool flag = false;
            if (File.Exists(filepath))
            {
                bitmap = SafeLoadImage(filepath);
                if (filepath.ToLower(CultureInfo.InvariantCulture).Substring(filepath.Length - 4, 4) == ".png")
                {
                    flag = true;
                }
                else
                {
                    color = Color.Black;
                }
                if (bitmap == null)
                {
                    return null;
                }
                Bitmap bitmap2 = CropImageContent(bitmap, 4, color);
                bitmap2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                thrusterLocations = ScanForThrusterLocations(bitmap2, Color.Blue, 20);
                lightPoints = ScanForColorPoints(bitmap2, Color.Yellow, 20);
                if (!flag)
                {
                    maskImage = ScanForOutline(bitmap2, color, Color.Black);
                    bitmap2.MakeTransparent(Color.Black);
                    imageSize = DetermineImageContentSize(bitmap2);
                }
                else
                {
                    maskImage = ScanForOutlineAndDetermineContentSize(bitmap2, color, Color.Black, out imageSize);
                }
                bitmap.Dispose();
                return bitmap2;
            }
            return null;
        }

        private int DetermineImageContentSize(Bitmap image)
        {
            int num = 0;
            _ = image.Width;
            _ = image.Height;
            int num2 = Color.Black.ToArgb();
            int num3 = Color.FromArgb(0, 0, 0, 0).ToArgb();
            int num4 = Color.FromArgb(0, 255, 255, 255).ToArgb();
            FastBitmap fastBitmap = new FastBitmap(image);
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.A > 0 && pixel.ToArgb() != num2 && pixel.ToArgb() != num3 && pixel.ToArgb() != num4)
                    {
                        num++;
                    }
                }
            }
            fastBitmap.Dispose();
            return num;
        }

        private Bitmap CropImageContent(Bitmap image, int padding, Color backColor)
        {
            int num = 0;
            int num2 = image.Width - 1;
            int num3 = 0;
            int num4 = image.Height - 1;
            int num5 = Color.Black.ToArgb();
            int num6 = Color.FromArgb(0, 0, 0, 0).ToArgb();
            int num7 = Color.FromArgb(0, 255, 255, 255).ToArgb();
            FastBitmap fastBitmap = new FastBitmap(image);
            int width = image.Width;
            int height = image.Height;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.A > 0 && pixel.ToArgb() != num5 && pixel.ToArgb() != num6 && pixel.ToArgb() != num7)
                    {
                        num = i;
                        i = image.Width;
                        break;
                    }
                }
            }
            for (int X = width - 1; X >= 0; X--)
            {
                for (int k = 0; k < height; k++)
                {
                    Color pixel2 = fastBitmap.GetPixel(ref X, ref k);
                    if (pixel2.A > 0 && pixel2.ToArgb() != num5 && pixel2.ToArgb() != num6 && pixel2.ToArgb() != num7)
                    {
                        num2 = X;
                        X = -1;
                        break;
                    }
                }
            }
            for (int l = 0; l < height; l++)
            {
                for (int m = 0; m < width; m++)
                {
                    Color pixel3 = fastBitmap.GetPixel(ref m, ref l);
                    if (pixel3.A > 0 && pixel3.ToArgb() != num5 && pixel3.ToArgb() != num6 && pixel3.ToArgb() != num7)
                    {
                        num3 = l;
                        l = image.Height;
                        break;
                    }
                }
            }
            for (int Y = height - 1; Y >= 0; Y--)
            {
                for (int n = 0; n < width; n++)
                {
                    Color pixel4 = fastBitmap.GetPixel(ref n, ref Y);
                    if (pixel4.A > 0 && pixel4.ToArgb() != num5 && pixel4.ToArgb() != num6 && pixel4.ToArgb() != num7)
                    {
                        num4 = Y;
                        Y = -1;
                        break;
                    }
                }
            }
            fastBitmap.Dispose();
            num3 -= padding;
            num4 += padding;
            num -= padding;
            num2 += padding;
            int num8 = num4 - num3;
            int num9 = num2 - num;
            int num10;
            int num11;
            int num12;
            int num13;
            if (num8 > num9)
            {
                num10 = num + num9 / 2 - num8 / 2;
                _ = 0;
                num11 = num10 + num8;
                num12 = num3;
                num13 = num4;
            }
            else
            {
                num12 = num3 + num8 / 2 - num9 / 2;
                _ = 0;
                num13 = num12 + num9;
                num10 = num;
                num11 = num2;
            }
            Bitmap bitmap = new Bitmap(num11 - num10 + 1, num13 - num12 + 1, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(backColor);
            Rectangle rect = new Rectangle(num10 * -1, num12 * -1, image.Width, image.Height);
            graphics.DrawImage(image, rect);
            return bitmap;
        }

        private List<Rectangle> ScanForThrusterLocations(Bitmap image, Color thrusterColor, int maximumLocationCount)
        {
            List<Rectangle> list = new List<Rectangle>();
            List<Point> list2 = new List<Point>();
            List<Color> list3 = new List<Color>();
            FastBitmap fastBitmap = new FastBitmap(image);
            int width = image.Width;
            int height = image.Height;
            Color transparent = Color.Transparent;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref i, ref j);
                    if (pixel.B != thrusterColor.B || pixel.R != thrusterColor.R || pixel.G != thrusterColor.G)
                    {
                        continue;
                    }
                    int num = j;
                    int num2 = j;
                    transparent = SampleForReplacementColor(fastBitmap, i, j);
                    list3.Add(transparent);
                    list2.Add(new Point(i, j));
                    if (j < image.Height - 1)
                    {
                        j++;
                        num2 = image.Height - 1;
                        for (; j < image.Height; j++)
                        {
                            pixel = fastBitmap.GetPixel(ref i, ref j);
                            if (pixel.R != thrusterColor.R || pixel.G != thrusterColor.G || pixel.B != thrusterColor.B)
                            {
                                num2 = j - 1;
                                break;
                            }
                            transparent = SampleForReplacementColor(fastBitmap, i, j);
                            list3.Add(transparent);
                            list2.Add(new Point(i, j));
                        }
                    }
                    list.Add(new Rectangle(i, num, 0, num2 - num + 1));
                    if (list.Count >= maximumLocationCount)
                    {
                        fastBitmap.Dispose();
                        return list;
                    }
                }
            }
            fastBitmap.Dispose();
            for (int k = 0; k < list2.Count; k++)
            {
                image.SetPixel(list2[k].X, list2[k].Y, list3[k]);
            }
            return list;
        }

        private Color SampleForReplacementColor(FastBitmap image, int x, int y)
        {
            int X = Math.Max(0, x - 1);
            if (x == 0)
            {
                X = 1;
            }
            int X2 = Math.Min(image.Bitmap.Width - 1, x + 1);
            if (x == image.Bitmap.Width - 1)
            {
                X2 = image.Bitmap.Width - 2;
            }
            Color pixel = image.GetPixel(ref X, ref y);
            Color pixel2 = image.GetPixel(ref X2, ref y);
            int alpha = (pixel.A + pixel2.A) / 2;
            int red = (pixel.R + pixel2.R) / 2;
            int green = (pixel.G + pixel2.G) / 2;
            int blue = (pixel.B + pixel2.B) / 2;
            return Color.FromArgb(alpha, red, green, blue);
        }

        private List<Point> ScanForColorPoints(Bitmap image, Color scanColor, int maximumPointCount)
        {
            List<Point> list = new List<Point>();
            List<Point> list2 = new List<Point>();
            List<Color> list3 = new List<Color>();
            FastBitmap fastBitmap = new FastBitmap(image);
            int width = image.Width;
            int height = image.Height;
            Color transparent = Color.Transparent;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.R == scanColor.R && pixel.G == scanColor.G && pixel.B == scanColor.B)
                    {
                        list.Add(new Point(j, i));
                        transparent = SampleForReplacementColor(fastBitmap, j, i);
                        list3.Add(transparent);
                        list2.Add(new Point(j, i));
                        if (list.Count >= maximumPointCount)
                        {
                            fastBitmap.Dispose();
                            return list;
                        }
                    }
                }
            }
            fastBitmap.Dispose();
            for (int k = 0; k < list2.Count; k++)
            {
                image.SetPixel(list2[k].X, list2[k].Y, list3[k]);
            }
            return list;
        }

        //private Bitmap ScanForOutlineAlpha(Bitmap image, Color replacementColor)
        //{
        //    FastBitmap fastBitmap = new FastBitmap(image);
        //    Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
        //    FastBitmap fastBitmap2 = new FastBitmap(bitmap);
        //    for (int i = 0; i < image.Height; i++)
        //    {
        //        for (int j = 0; j < image.Width; j++)
        //        {
        //            if (fastBitmap.GetPixel(ref j, ref i).A > 192)
        //            {
        //                fastBitmap2.SetPixel(ref j, ref i, Color.Transparent);
        //            }
        //            else
        //            {
        //                fastBitmap2.SetPixel(ref j, ref i, replacementColor);
        //            }
        //        }
        //    }
        //    fastBitmap2.Dispose();
        //    fastBitmap.Dispose();
        //    return bitmap;
        //}

        private Bitmap ScanForOutlineAndDetermineContentSize(Bitmap image, Color opaqueColor, Color replacementColor, out int opaqueCount)
        {
            opaqueCount = 0;
            int num = 0;
            int width = image.Width;
            int num2 = 0;
            int height = image.Height;
            int num3 = Color.Black.ToArgb();
            int num4 = Color.FromArgb(0, 0, 0, 0).ToArgb();
            int num5 = Color.FromArgb(0, 255, 255, 255).ToArgb();
            FastBitmap fastBitmap = new FastBitmap(image);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            FastBitmap fastBitmap2 = new FastBitmap(bitmap);
            int num6 = opaqueColor.ToArgb();
            for (int i = num; i < height; i++)
            {
                for (int j = num2; j < width; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0 && pixel.ToArgb() != num3 && pixel.ToArgb() != num4 && pixel.ToArgb() != num5)
                    {
                        opaqueCount++;
                    }
                    if (pixel.ToArgb() != num6)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, Color.Transparent);
                    }
                    else
                    {
                        fastBitmap2.SetPixel(ref j, ref i, replacementColor);
                    }
                }
            }
            fastBitmap2.Dispose();
            fastBitmap.Dispose();
            return bitmap;
        }

        private Bitmap ScanForOutline(Bitmap image, Color opaqueColor, Color replacementColor)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppPArgb);
            FastBitmap fastBitmap2 = new FastBitmap(bitmap);
            int num = opaqueColor.ToArgb();
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (fastBitmap.GetPixel(ref j, ref i).ToArgb() != num)
                    {
                        fastBitmap2.SetPixel(ref j, ref i, Color.Transparent);
                    }
                    else
                    {
                        fastBitmap2.SetPixel(ref j, ref i, replacementColor);
                    }
                }
            }
            fastBitmap2.Dispose();
            fastBitmap.Dispose();
            return bitmap;
        }

        //private Bitmap GenerateSmallImage(Bitmap fullSizeImage)
        //{
        //    Bitmap result = null;
        //    if (fullSizeImage != null)
        //    {
        //        result = GraphicsHelper.ScaleLimitImage(fullSizeImage, _ImageSmallSize, _ImageSmallSize, 1f);
        //    }
        //    return result;
        //}

        private void GenerateBuiltObjectImageFilepaths()
        {
            List<Task<string>> taskList = new List<Task<string>>();
            _Filepaths.Clear();
            string origShipsFolder = _ApplicationStartupPath + "\\images\\units\\ships\\";
            string modShipsFolder = _ApplicationStartupPath + "\\customization\\" + _CustomizationSetName + "\\images\\units\\ships\\";
            int index = 0;
            taskList.Add(Task.Run(() => CheckLoadSmallImage(0, origShipsFolder + "other\\planetdestroyer", modShipsFolder + "other\\planetdestroyer")));
            index++;
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text + "other\\planetdestroyer", text2 + "other\\planetdestroyer"));
            string otherShipsFolder = origShipsFolder + "other\\";
            string customOtherShipsFolder = modShipsFolder + "other\\";
            string origMinorBases = otherShipsFolder + "minorsets\\bases\\";
            string customMinorBases = customOtherShipsFolder + "minorsets\\bases\\";
            for (int i = 0; i < 2; i++)
            {
                int localIndex = index++;
                int localI = i;
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text5 + "base_" + i, text6 + "base_" + i));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex, origMinorBases + "base_" + localI, customMinorBases + "base_" + localI)));
            }
            string text7 = otherShipsFolder + "minorsets\\";
            string text8 = customOtherShipsFolder + "minorsets\\";
            for (int j = 0; j < 7; j++)
            {
                string text9 = text7 + "family" + j + "\\";
                string text10 = text8 + "family" + j + "\\";
                int localIndex1 = index++;
                int localIndex2 = index++;
                int localIndex3 = index++;
                int localIndex4 = index++;
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text9 + "military_small", text10 + "military_small"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text9 + "military_medium", text10 + "military_medium"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text9 + "military_large", text10 + "military_large"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text9 + "colonyship", text10 + "colonyship"));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex1, text9 + "military_small", text10 + "military_small")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex2, text9 + "military_medium", text10 + "military_medium")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex3, text9 + "military_large", text10 + "military_large")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex4, text9 + "colonyship", text10 + "colonyship")));
            }
            string text11 = otherShipsFolder + "majorsets\\";
            string text12 = customOtherShipsFolder + "majorsets\\";
            for (int k = 0; k < 4; k++)
            {
                string text13 = text11;
                string text14 = text12;
                switch (k)
                {
                    case 0:
                        text13 += "Shakturi\\";
                        text14 += "Shakturi\\";
                        break;
                    case 1:
                        text13 += "ShakturiAllies\\";
                        text14 += "ShakturiAllies\\";
                        break;
                    case 2:
                        text13 += "AncientHelpers\\";
                        text14 += "AncientHelpers\\";
                        break;
                    case 3:
                        text13 += "FreedomAlliance\\";
                        text14 += "FreedomAlliance\\";
                        break;
                }
                int localIndex1 = index++;
                int localIndex2 = index++;
                int localIndex3 = index++;
                int localIndex4 = index++;
                int localIndex5 = index++;
                int localIndex6 = index++;
                int localIndex7 = index++;
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "frigate", text14 + "frigate"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "destroyer", text14 + "destroyer"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "cruiser", text14 + "cruiser"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "capitalship", text14 + "capitalship"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "trooptransport", text14 + "trooptransport"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "base_0", text14 + "base_0"));
                //_Filepaths.Add(CheckLoadSmallImage(ref index, text13 + "base_1", text14 + "base_1"));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex1, text13 + "frigate", text14 + "frigate")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex2, text13 + "destroyer", text14 + "destroyer")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex3, text13 + "cruiser", text14 + "cruiser")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex4, text13 + "capitalship", text14 + "capitalship")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex5, text13 + "trooptransport", text14 + "trooptransport")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex6, text13 + "base_0", text14 + "base_0")));
                taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex7, text13 + "base_1", text14 + "base_1")));
                if (k == ShipImageHelper.FreedomAllianceFamily)
                {
                    string text15 = text13 + "aged\\";
                    string text16 = text14 + "aged\\";
                    //_Filepaths.Add(CheckLoadSmallImage(ref index, text15 + "frigate", text16 + "frigate"));
                    //_Filepaths.Add(CheckLoadSmallImage(ref index, text15 + "destroyer", text16 + "destroyer"));
                    //_Filepaths.Add(CheckLoadSmallImage(ref index, text15 + "cruiser", text16 + "cruiser"));
                    //_Filepaths.Add(CheckLoadSmallImage(ref index, text15 + "capitalship", text16 + "capitalship"));
                    //_Filepaths.Add(CheckLoadSmallImage(ref index, text15 + "trooptransport", text16 + "trooptransport"));
                    int localIndex8 = index++;
                    int localIndex9 = index++;
                    int localIndex10 = index++;
                    int localIndex11 = index++;
                    int localIndex12 = index++;
                    taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex8, text15 + "frigate", text16 + "frigate")));
                    taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex9, text15 + "destroyer", text16 + "destroyer")));
                    taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex10, text15 + "cruiser", text16 + "cruiser")));
                    taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex11, text15 + "capitalship", text16 + "capitalship")));
                    taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex12, text15 + "trooptransport", text16 + "trooptransport")));
                }
            }
            string text17 = otherShipsFolder + "majorsets\\PhantomPirates\\";
            string text18 = customOtherShipsFolder + "majorsets\\PhantomPirates\\";
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "escort", text18 + "escort"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "frigate", text18 + "frigate"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "destroyer", text18 + "destroyer"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "cruiser", text18 + "cruiser"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "capitalship", text18 + "capitalship"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "carrier", text18 + "carrier"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "defensivebase", text18 + "defensivebase"));
            //_Filepaths.Add(CheckLoadSmallImage(ref index, text17 + "homebase", text18 + "homebase"));
            int localIndex13 = index++;
            int localIndex14 = index++;
            int localIndex15 = index++;
            int localIndex16 = index++;
            int localIndex17 = index++;
            int localIndex18 = index++;
            int localIndex19 = index++;
            int localIndex20 = index++;
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex13, text17 + "escort", text18 + "escort")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex14, text17 + "frigate", text18 + "frigate")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex15, text17 + "destroyer", text18 + "destroyer")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex16, text17 + "cruiser", text18 + "cruiser")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex17, text17 + "capitalship", text18 + "capitalship")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex18, text17 + "carrier", text18 + "carrier")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex19, text17 + "defensivebase", text18 + "defensivebase")));
            taskList.Add(Task.Run(() => CheckLoadSmallImage(localIndex20, text17 + "homebase", text18 + "homebase")));

            Task.WaitAll(taskList.ToArray());
            foreach (var item in taskList)
            {
                if (item.IsFaulted)
                { throw item.Exception; }
                else
                { _Filepaths.Add(item.Result); }
            }

            //int num = 0;
            //int num2 = 500;
            //while (true)
            //{
            //    string text19 = origShipsFolder + "family" + num + "\\";
            //    string text20 = customShipsFolder + "family" + num + "\\";
            //    if (num < num2 && (Directory.Exists(text19) || Directory.Exists(text20)))
            //    {
            //        switch (num)
            //        {
            //            case 3:
            //            case 8:
            //            case 13:
            //            case 18:
            //                DoLoadProgress();
            //                break;
            //        }
            //        BaconBuiltObjectImageCache.AddMoreImages(this, index, text19, text20);
            //        num++;
            //        continue;
            //    }
            //    break;
            //}
            List<DirectoryInfo> origDir = new DirectoryInfo(origShipsFolder).EnumerateDirectories("family*").ToList();
            List<int> familyNumbers = null;
            if (!string.IsNullOrEmpty(_CustomizationSetName))
            {
                DirectoryInfo modDir = new DirectoryInfo(modShipsFolder);
                List<DirectoryInfo> modDirFamilyList = new List<DirectoryInfo>();
                if (modDir.Exists)
                {
                    modDirFamilyList = modDir.EnumerateDirectories("family*").ToList();
                }
                familyNumbers = origDir.Union(modDirFamilyList).Select(x =>
                {
                    int.TryParse(x.Name.Substring(6), out int res);
                    return res;
                }).Distinct().ToList();
            }
            else
            {
                familyNumbers = origDir.Select(x =>
                {
                    int.TryParse(x.Name.Substring(6), out int res);
                    return res;
                }).Distinct().ToList();
            }
            familyNumbers.Sort();
            int idx = 0;
            foreach (var num in familyNumbers)
            {
                string text19 = origShipsFolder + "family" + num + "\\";
                string text20 = modShipsFolder + "family" + num + "\\";
                if (Directory.Exists(text19) || Directory.Exists(text20))
                {
                    switch (idx)
                    {
                        case 3:
                        case 8:
                        case 13:
                        case 18:
                            DoLoadProgress();
                            break;
                    }
                    BaconBuiltObjectImageCache.AddMoreImages(this, index, text19, text20);
                    idx++;
                }
            }
        }

        private void ClearImageCache(ConcurrentDictionary<int, BuiltObjectImageData> images)
        {
            lock (_LockObject)
            {
                foreach (var item in images)
                {
                    if (item.Value == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (item.Value.Image != null && item.Value.Image.PixelFormat != 0)
                        {
                            item.Value.Image.Dispose();
                        }
                        if (item.Value.MaskImage != null && item.Value.MaskImage.PixelFormat != 0)
                        {
                            item.Value.MaskImage.Dispose();
                        }
                        item.Value.Image = null;
                        item.Value.MaskImage = null;
                        item.Value.ThrusterLocations = null;
                        item.Value.LightPoints = null;
                    }
                    //}
                }
                images.Clear();
            }
        }
        private void ClearImageCache(Dictionary<int, BuiltObjectImageData> images)
        {
            lock (_LockObject)
            {
                foreach (var item in images)
                {
                    if (item.Value == null)
                    {
                        continue;
                    }
                    else
                    {
                        if (item.Value.Image != null && item.Value.Image.PixelFormat != 0)
                        {
                            item.Value.Image.Dispose();
                        }
                        if (item.Value.MaskImage != null && item.Value.MaskImage.PixelFormat != 0)
                        {
                            item.Value.MaskImage.Dispose();
                        }
                        item.Value.Image = null;
                        item.Value.MaskImage = null;
                        item.Value.ThrusterLocations = null;
                        item.Value.LightPoints = null;
                    }
                    //}
                }
                images.Clear();
            }
        }

        public void ClearCache()
        {
            ClearImageCache(_ImageData);
        }
    }
}
