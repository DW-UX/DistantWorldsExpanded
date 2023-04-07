// Decompiled with JetBrains decompiler
// Type: DistantWorlds.PerlinNoise
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Drawing;

namespace DistantWorlds
{
    public class PerlinNoise
    {
        private FastPerlinNoise fastPerlinNoise_0;

        private int int_0;

        private int int_1;

        private int int_2;

        public PerlinNoise(int randomSeed):base()
        {
            Class7.VEFSJNszvZKMZ();
            fastPerlinNoise_0 = new FastPerlinNoise();
            Random random = new Random(randomSeed);
            int_0 = random.Next(1000, 10000);
            int_1 = random.Next(100000, 1000000);
            int_2 = random.Next(1000000000, 2000000000);
        }

        public void Render(Bitmap image, Rectangle rectangle)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            for (int i = rectangle.Top; i < rectangle.Bottom; i++)
            {
                for (int j = rectangle.Left; j < rectangle.Right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        byte alpha = (byte)(PerlinNoise2d(j, i) * 255.0);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                }
            }
            fastBitmap.Release();
        }

        public void ApplyNoiseToImageOpaque(Bitmap image, Rectangle rectangle, byte[][] noise, out int maxAlpha, out int int_3)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            maxAlpha = 0;
            int_3 = 0;
            int top = rectangle.Top;
            int bottom = rectangle.Bottom;
            int left = rectangle.Left;
            int right = rectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        double num = (double)(int)noise[j][i] / 96.0;
                        int a = pixel.A;
                        int val = (int)((double)(int)pixel.R * num);
                        int val2 = (int)((double)(int)pixel.G * num);
                        int val3 = (int)((double)(int)pixel.B * num);
                        a = Math.Max(0, Math.Min(255, a));
                        val = Math.Max(0, Math.Min(255, val));
                        val2 = Math.Max(0, Math.Min(255, val2));
                        val3 = Math.Max(0, Math.Min(255, val3));
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(a, val, val2, val3));
                        maxAlpha = Math.Max(maxAlpha, a);
                        int_3 = Math.Max(int_3, (val + val2 + val3 + a) / 4);
                    }
                }
            }
            fastBitmap.Release();
        }

        public void ApplyNoiseToImageTransparent(Bitmap image, Rectangle rectangle, byte[][] noise)
        {
            ApplyNoiseToImageTransparent(image, rectangle, noise, 255.0);
        }

        public void ApplyNoiseToImageTransparent(Bitmap image, Rectangle rectangle, byte[][] noise, double alphaFactor)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            int top = rectangle.Top;
            int bottom = rectangle.Bottom;
            int left = rectangle.Left;
            int right = rectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    double num = (double)(int)noise[j][i] / alphaFactor;
                    int val = (int)((double)(int)pixel.A * num);
                    val = Math.Max(0, Math.Min(255, val));
                    fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(val, pixel.R, pixel.G, pixel.B));
                }
            }
            fastBitmap.Release();
        }

        public void ApplyNoiseToImage(Bitmap image, Rectangle rectangle, byte[][] noise, out int maxAlpha, out int int_3)
        {
            ApplyNoiseToImage(image, rectangle, noise, out maxAlpha, out int_3, 1.0);
        }

        public void ApplyNoiseToImage(Bitmap image, Rectangle rectangle, byte[][] noise, out int maxAlpha, out int int_3, double alphaFactor)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            maxAlpha = 0;
            int_3 = 0;
            int top = rectangle.Top;
            int bottom = rectangle.Bottom;
            int left = rectangle.Left;
            int right = rectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        double num = (double)(int)noise[j][i] / 255.0;
                        double num2 = Math.Min(1.0, num / alphaFactor);
                        int num3 = (int)((double)(int)pixel.A * num2);
                        int num4 = (int)((double)(int)pixel.R * num);
                        int num5 = (int)((double)(int)pixel.G * num);
                        int num6 = (int)((double)(int)pixel.B * num);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(num3, num4, num5, num6));
                        maxAlpha = Math.Max(maxAlpha, num3);
                        int_3 = Math.Max(int_3, (num4 + num5 + num6 + num3) / 4);
                    }
                }
            }
            fastBitmap.Release();
        }

        public void IntensifyImageColor(Bitmap image, Rectangle rectangle, int maxAlpha, int alphaThreshhold, int maxColorSum, int colorThreshhold, double intensityFactor, double fadeFactor, int colorBoostAmount)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            int int_ = Math.Max(1, maxAlpha - alphaThreshhold);
            int int_2 = 255 - alphaThreshhold;
            int top = rectangle.Top;
            int bottom = rectangle.Bottom;
            int left = rectangle.Left;
            int right = rectangle.Right;
            for (int i = top; i < bottom; i++)
            {
                for (int j = left; j < right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0 && pixel.A > alphaThreshhold)
                    {
                        int alpha = method_2(pixel.A, alphaThreshhold, int_, int_2, intensityFactor, fadeFactor);
                        Color color = method_1(pixel, colorThreshhold, maxColorSum, colorBoostAmount);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, color.R, color.G, color.B));
                    }
                }
            }
            fastBitmap.Release();
        }

        private int method_0(int int_3, int int_4, int int_5)
        {
            if (int_3 < int_4)
            {
                double num = 1.0 + (double)(int_4 - int_3) / (double)int_4 * 1.0;
                int_5 = (int)((double)int_5 * num);
            }
            return Math.Min(255, Math.Max(0, int_3 + int_5));
        }

        private Color method_1(Color color_0, int int_3, int int_4, int int_5)
        {
            Color result = color_0;
            int num = (color_0.R + color_0.G + color_0.B + color_0.A) / 4;
            if (num > int_3)
            {
                int num2 = int_4 - int_3;
                int num3 = num - int_3;
                int num4 = (int)((double)int_5 * ((double)num3 / (double)num2));
                num4 /= 3;
                int red = method_0(color_0.R, int_3, num4);
                int green = method_0(color_0.G, int_3, num4);
                int blue = method_0(color_0.B, int_3, num4);
                result = Color.FromArgb(red, green, blue);
            }
            return result;
        }

        private int method_2(int int_3, int int_4, int int_5, int int_6, double double_0, double double_1)
        {
            int result = int_3;
            if (int_3 > int_4)
            {
                double num = (double)(int_3 - int_4) / (double)int_5;
                int val = int_4 + (int)(num * (double)int_6);
                result = Math.Min(255, val);
            }
            return result;
        }

        public byte[][] GeneratePerlinNoiseJagged(int size)
        {
            byte[][] array = new byte[size][];
            for (int i = 0; i < size; i++)
            {
                array[i] = new byte[size];
            }
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    array[k][j] = (byte)(PerlinNoise2d(k, j) * 255.0);
                }
            }
            return array;
        }

        public byte[,] GeneratePerlinNoise(int size)
        {
            byte[,] array = new byte[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    array[j, i] = (byte)(PerlinNoise2d(j, i) * 255.0);
                }
            }
            return array;
        }

        public void Render(Bitmap destinationImage, Bitmap sourceImage, Rectangle rectangle)
        {
            for (int i = rectangle.Top; i < rectangle.Bottom; i++)
            {
                for (int j = rectangle.Left; j < rectangle.Right; j++)
                {
                    Color pixel = sourceImage.GetPixel(j, i);
                    if (pixel.A > 0)
                    {
                        byte alpha = (byte)(PerlinNoise2d(j, i) * 255.0);
                        destinationImage.SetPixel(j, i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                }
            }
        }

        public double PerlinNoise2d(double x, double y)
        {
            double num = 0.0;
            double num2 = 0.03;
            double num3 = 0.75;
            double num4 = 7.0;
            double num5 = 1.0;
            double num6 = 0.4;
            double num7 = 1.0;
            for (int i = 0; (double)i < num4; i++)
            {
                num += method_3(x * num2, y * num2) * num5;
                num2 *= 2.0;
                num5 *= num3;
            }
            num = (num + num6) * num7;
            if (num < 0.0)
            {
                num = 0.0;
            }
            if (num > 1.0)
            {
                num = 1.0;
            }
            return num;
        }

        public void RenderTurbulence(Bitmap image, Rectangle rectangle)
        {
            FastBitmap fastBitmap = new FastBitmap(image);
            for (int i = rectangle.Top; i < rectangle.Bottom; i++)
            {
                for (int j = rectangle.Left; j < rectangle.Right; j++)
                {
                    Color pixel = fastBitmap.GetPixel(ref j, ref i);
                    if (pixel.A > 0)
                    {
                        byte alpha = (byte)(Turbulence(j, i, 7.0, 2.0956723, 0.5) * 255.0);
                        fastBitmap.SetPixel(ref j, ref i, Color.FromArgb(alpha, pixel.R, pixel.G, pixel.B));
                    }
                }
            }
            fastBitmap.Release();
        }

        public double Turbulence(double inputX, double inputY, double octaves, double lacunarity, double gain)
        {
            double num = 0.0;
            double num2 = 1.0;
            double num3 = 0.0;
            for (int i = 0; (double)i < octaves; i++)
            {
                num += num2 * Math.Abs(method_4((int)inputX, (int)inputY));
                num3 += num2;
                num2 *= gain;
                inputX *= lacunarity;
                inputY *= lacunarity;
            }
            return num / num3;
        }

        private double method_3(double double_0, double double_1)
        {
            double double_2 = method_4((int)double_0, (int)double_1);
            double double_3 = method_4((int)double_0 + 1, (int)double_1);
            double double_4 = method_4((int)double_0, (int)double_1 + 1);
            double double_5 = method_4((int)double_0 + 1, (int)double_1 + 1);
            double double_6 = method_5(double_2, double_3, double_0 - (double)(int)double_0);
            double double_7 = method_5(double_4, double_5, double_0 - (double)(int)double_0);
            return method_5(double_6, double_7, double_1 - (double)(int)double_1);
        }

        private double method_4(int int_3, int int_4)
        {
            int num = int_3 + int_4 * 57;
            num = (num << 13) ^ num;
            return 1.0 - (double)((num * (num * num * int_0 + int_1) + int_2) & 0x7FFFFFFF) / 1073741824.0;
        }

        private double method_5(double double_0, double double_1, double double_2)
        {
            double num = (1.0 - Math.Cos(double_2 * Math.PI)) * 0.5;
            return double_0 * (1.0 - num) + double_1 * num;
        }
    }
}
