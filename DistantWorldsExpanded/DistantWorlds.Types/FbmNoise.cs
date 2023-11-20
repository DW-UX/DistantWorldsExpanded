// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FbmNoise
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  public class FbmNoise
  {
    private int _Seed;
    private float PI = 3.14159274f;
    private float HALF_PI = 1.57079637f;
    private float TWO_PI = 6.28318548f;
    private float LOGHALF = -0.6931472f;
    private float LOGHALFI = -1.442695f;
    private float DELTA = 1E-06f;
    private int MAX_DIMENSIONS = 4;
    private int MAX_OCTAVES = 128;

    public FbmNoise(int seed) => this._Seed = seed;

    public void ApplyNoiseToImageTransparent(Bitmap image, Rectangle rectangle, byte[][] noise) => this.ApplyNoiseToImageTransparent(image, rectangle, noise, (double) byte.MaxValue);

    public void ApplyNoiseToImageTransparent(
      Bitmap image,
      Rectangle rectangle,
      byte[][] noise,
      double alphaFactor)
    {
      FastBitmap fastBitmap = new FastBitmap(image);
      int top = rectangle.Top;
      int bottom = rectangle.Bottom;
      int left = rectangle.Left;
      int right = rectangle.Right;
      int length1 = noise.Length;
      int length2 = noise[0].Length;
      for (int Y = top; Y < bottom; ++Y)
      {
        for (int X = left; X < right; ++X)
        {
          Color pixel = fastBitmap.GetPixel(X, Y);
          if (pixel.A > (byte) 0)
          {
            int index1 = X % length1;
            int index2 = Y % length2;
            double num = (double) noise[index1][index2] / alphaFactor;
            int a = (int) (double) pixel.A;
            int val2_1 = (int) ((double) pixel.R * num);
            int val2_2 = (int) ((double) pixel.G * num);
            int val2_3 = (int) ((double) pixel.B * num);
            int alpha = Math.Max(0, Math.Min((int) byte.MaxValue, a));
            int red = Math.Max(0, Math.Min((int) byte.MaxValue, val2_1));
            int green = Math.Max(0, Math.Min((int) byte.MaxValue, val2_2));
            int blue = Math.Max(0, Math.Min((int) byte.MaxValue, val2_3));
            fastBitmap.SetPixel(ref X, ref Y, Color.FromArgb(alpha, red, green, blue));
          }
        }
      }
      fastBitmap.Dispose();
    }

    public byte[][] EmphasizeNoise(byte[][] noise, double emphasisFactor)
    {
      int length1 = noise.Length;
      int length2 = noise[0].Length;
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          byte num = noise[index1][index2];
          byte val2 = num > (byte) 127 ? (byte) ((double) sbyte.MaxValue + Math.Min(128.0, ((double) num - (double) sbyte.MaxValue) * emphasisFactor)) : (byte) ((double) sbyte.MaxValue - Math.Min((double) sbyte.MaxValue, ((double) sbyte.MaxValue - (double) num) * emphasisFactor));
          noise[index1][index2] = Math.Min(byte.MaxValue, Math.Max((byte) 0, val2));
        }
      }
      return noise;
    }

    public byte[][] TileNoise(byte[][] noise, int edgeSize)
    {
      int length1 = noise.Length;
      int length2 = noise[0].Length;
      double x = 0.95;
      Random random = new Random(this._Seed);
      for (int index1 = 0; index1 < length1; ++index1)
      {
        int index2 = index1;
        int num1 = (int) noise[index2][0];
        for (int index3 = 0; index3 < edgeSize; ++index3)
        {
          int index4 = length2 - 1 - index3;
          int num2 = (int) noise[index1][index4];
          int num3 = (int) ((double) (num1 - num2) * Math.Pow(x, (double) (index3 + 1)));
          noise[index1][index4] = (byte) Math.Min((int) byte.MaxValue, Math.Max(0, num2 + num3));
          num1 = (int) noise[index2][index4];
        }
      }
      for (int index5 = 0; index5 < length2; ++index5)
      {
        int index6 = index5;
        int num4 = (int) noise[0][index6];
        for (int index7 = 0; index7 < edgeSize; ++index7)
        {
          int index8 = length1 - 1 - index7;
          int num5 = (int) noise[index8][index5];
          int num6 = (int) ((double) (num4 - num5) * Math.Pow(x, (double) (index7 + 1)));
          noise[index8][index5] = (byte) Math.Min((int) byte.MaxValue, Math.Max(0, num5 + num6));
          num4 = (int) noise[index8][index6];
        }
      }
      return noise;
    }

    public byte[][] MakeNoise(int size, double roughness, double lacunarity)
    {
      byte[][] numArray = new byte[size][];
      for (int index = 0; index < size; ++index)
        numArray[index] = new byte[size];
      FbmNoise.CFractal cfractal = new FbmNoise.CFractal(this._Seed, (float) roughness, (float) lacunarity);
      float num1 = (float) size / 2f;
      for (int index1 = 0; index1 < size; ++index1)
      {
        for (int index2 = 0; index2 < size; ++index2)
        {
          float x = (float) index2 / num1;
          float y = (float) index1 / num1;
          float num2 = cfractal.fBm(ref x, ref y, 8);
          numArray[index2][index1] = (byte) Math.Max(0.0f, Math.Min((float) byte.MaxValue, (float) (((double) num2 + 1.0) * 128.0)));
        }
      }
      return numArray;
    }

    public byte[][] MakeTurbulence(int size, double roughness, double lacunarity)
    {
      byte[][] numArray = new byte[size][];
      for (int index = 0; index < size; ++index)
        numArray[index] = new byte[size];
      FbmNoise.CFractal cfractal = new FbmNoise.CFractal(this._Seed, (float) roughness, (float) lacunarity);
      double num1 = (double) size / 2.0;
      for (int index1 = 0; index1 < size; ++index1)
      {
        for (int index2 = 0; index2 < size; ++index2)
        {
          float x = (float) index2 / 100f;
          float y = (float) index1 / 100f;
          float num2 = cfractal.Turbulence(ref x, ref y, 8);
          numArray[index2][index1] = (byte) Math.Max(0.0f, Math.Min((float) byte.MaxValue, (float) (((double) num2 + 1.0) * 128.0)));
        }
      }
      return numArray;
    }

    public byte[][] MakeHeteroFractal(int size, double roughness, double lacunarity, float offset)
    {
      byte[][] numArray = new byte[size][];
      for (int index = 0; index < size; ++index)
        numArray[index] = new byte[size];
      FbmNoise.CFractal cfractal = new FbmNoise.CFractal(this._Seed, (float) roughness, (float) lacunarity);
      double num1 = (double) size / 2.0;
      for (int index1 = 0; index1 < size; ++index1)
      {
        for (int index2 = 0; index2 < size; ++index2)
        {
          float x = (float) ((double) index2 / 150.0 + 2.0);
          float y = (float) ((double) index1 / 150.0 + 2.0);
          float num2 = cfractal.Heterofractal(ref x, ref y, 6, offset);
          numArray[index2][index1] = (byte) Math.Max(0.0f, Math.Min((float) byte.MaxValue, num2 * (float) byte.MaxValue));
        }
      }
      return numArray;
    }

    public byte[][] MakeRidgedMultifractal(
      int size,
      double roughness,
      double lacunarity,
      float offset,
      float gain,
      float xOffset,
      float yOffset)
    {
      byte[][] numArray = new byte[size][];
      for (int index = 0; index < size; ++index)
        numArray[index] = new byte[size];
      FbmNoise.CFractal cfractal = new FbmNoise.CFractal(this._Seed, (float) roughness, (float) lacunarity);
      double num1 = (double) size / 2.0;
      for (int index1 = 0; index1 < size; ++index1)
      {
        for (int index2 = 0; index2 < size; ++index2)
        {
          float x = (float) index2 / 40f + xOffset;
          float y = (float) index1 / 40f + yOffset;
          float num2 = cfractal.RidgedMultifractal(ref x, ref y, 3, offset, gain);
          numArray[index2][index1] = (byte) Math.Max(0.0f, Math.Min((float) byte.MaxValue, num2 * (float) byte.MaxValue));
        }
      }
      return numArray;
    }

    private class Helpers
    {
      public static float PI = 3.14159274f;
      public static float HALF_PI = 1.57079637f;
      public static float TWO_PI = 6.28318548f;
      public static float LOGHALF = -0.6931472f;
      public static float LOGHALFI = -1.442695f;
      public static float DELTA = 1E-06f;
      public static int MAX_DIMENSIONS = 4;
      public static int MAX_OCTAVES = 128;

      public static float Square(float a) => a * a;

      public static float Min(float a, float b) => (double) a >= (double) b ? b : a;

      public static float Max(float a, float b) => (double) a <= (double) b ? b : a;

      public static float Abs(float a) => (double) a >= 0.0 ? a : -a;

      public static float Clamp(float a, float b, float x)
      {
        if ((double) x < (double) a)
          return a;
        return (double) x <= (double) b ? x : b;
      }

      public static float Lerp(float a, float b, float x) => a + x * (b - a);

      public static float Cubic(float a) => (float) ((double) a * (double) a * (3.0 - 2.0 * (double) a));

      public static float Boxstep(float a, float b, float x) => FbmNoise.Helpers.Clamp(0.0f, 1f, (float) (((double) x - (double) a) / ((double) b - (double) a)));

      public static float Gamma(float a, float g) => (float) Math.Pow((double) a, 1.0 / (double) g);

      public static float Bias(float a, float b) => (float) Math.Pow((double) a, Math.Log10((double) b) * (double) FbmNoise.Helpers.LOGHALFI);

      public static float Expose(float l, float k) => (float) (1.0 - Math.Exp(-(double) l * (double) k));

      public static void SWAP(ref byte a, ref byte b)
      {
        byte num = a;
        a = b;
        b = num;
      }

      public static float CLAMP(float a, float b, float x)
      {
        if ((double) x < (double) a)
          return a;
        return (double) x <= (double) b ? x : b;
      }

      public static float LERP(float a, float b, float x) => a + x * (b - a);

      public static float Gain(float a, float b)
      {
        if ((double) a <= (double) FbmNoise.Helpers.DELTA)
          return 0.0f;
        if ((double) a >= 1.0 - (double) FbmNoise.Helpers.DELTA)
          return 1f;
        float y = (float) Math.Log10(1.0 - (double) b) * FbmNoise.Helpers.LOGHALFI;
        return (double) a < 0.5 ? (float) Math.Pow(2.0 * (double) a, (double) y) * 0.5f : (float) (1.0 - Math.Pow(2.0 * (1.0 - (double) a), (double) y) * 0.5);
      }

      public static float Smoothstep(float a, float b, float x)
      {
        if ((double) x <= (double) a)
          return 0.0f;
        return (double) x >= (double) b ? 1f : FbmNoise.Helpers.Cubic((float) (((double) x - (double) a) / ((double) b - (double) a)));
      }

      public static float Mod(float a, float b)
      {
        a -= (float) (int) ((double) a / (double) b) * b;
        if ((double) a < 0.0)
          a += b;
        return a;
      }

      public static void Normalize(float[] f, int n)
      {
        float d = 0.0f;
        for (int index = 0; index < n; ++index)
          d += f[index] * f[index];
        float num = (float) (1.0 / Math.Sqrt((double) d));
        for (int index = 0; index < n; ++index)
          f[index] *= num;
      }
    }

    private class CRandom
    {
      private Random _Rnd;

      public CRandom() => this._Rnd = new Random();

      public CRandom(int nSeed) => this.Init(nSeed);

      public void Init(int nSeed) => this._Rnd = new Random(nSeed);

      public double Random() => this._Rnd.NextDouble();

      public double RandomD(double dMin, double dMax)
      {
        double val2 = dMax - dMin;
        double val1 = val2 * this.Random();
        return dMin + Math.Min(val1, val2);
      }

      public uint RandomI(int nMin, int nMax)
      {
        int val2 = nMax - nMin;
        int val1 = (int) (((double) val2 + 1.0) * this.Random());
        return (uint) (nMin + Math.Min(val1, val2));
      }
    }

    public abstract class CNoise
    {
      private float PI = 3.14159274f;
      private float HALF_PI = 1.57079637f;
      private float TWO_PI = 6.28318548f;
      private float LOGHALF = -0.6931472f;
      private float LOGHALFI = -1.442695f;
      private float DELTA = 1E-06f;
      private int MAX_DIMENSIONS = 4;
      private int MAX_OCTAVES = 128;
      protected int m_nDimensions;
      protected byte[] m_nMap = new byte[256];
      protected float[][] m_nBuffer = new float[256][];

      protected float Lattice(int ix, float fx) => this.Lattice(ix, fx, 0, 0.0f, 0, 0.0f, 0, 0.0f);

      protected float Lattice(int ix, float fx, int iy, float fy) => this.Lattice(ix, fx, iy, fy, 0, 0.0f, 0, 0.0f);

      protected float Lattice(int ix, float fx, int iy, float fy, int iz, float fz) => this.Lattice(ix, fx, iy, fy, iz, fz, 0, 0.0f);

      protected float Lattice(
        int ix,
        float fx,
        int iy,
        float fy,
        int iz,
        float fz,
        int iw,
        float fw)
      {
        int[] numArray1 = new int[4]{ ix, iy, iz, iw };
        float[] numArray2 = new float[4]{ fx, fy, fz, fw };
        int index1 = 0;
        for (int index2 = 0; index2 < this.m_nDimensions; ++index2)
          index1 = (int) this.m_nMap[index1 + numArray1[index2] & (int) byte.MaxValue];
        float num = 0.0f;
        for (int index3 = 0; index3 < this.m_nDimensions; ++index3)
          num += this.m_nBuffer[index1][index3] * numArray2[index3];
        return num;
      }

      protected float LatticeOptimized(int ix, float fx, int iy, float fy)
      {
        int n = (int) this.m_nMap[(int) this.m_nMap[0 + ix & (int) byte.MaxValue] + iy & (int) byte.MaxValue];
        return this.m_nBuffer[n][0] * fx + this.m_nBuffer[n][1] * fy;
      }

      public CNoise()
      {
      }

      public CNoise(int nSeed) => this.Init(nSeed);

      public void Init(int nSeed)
      {
        this.m_nDimensions = 2;
        FbmNoise.CRandom crandom = new FbmNoise.CRandom(nSeed);
        int index1;
        for (index1 = 0; index1 < 256; ++index1)
        {
          this.m_nMap[index1] = (byte) index1;
          this.m_nBuffer[index1] = new float[256];
          for (int index2 = 0; index2 < this.m_nDimensions; ++index2)
            this.m_nBuffer[index1][index2] = (float) crandom.RandomD(-0.5, 0.5);
          FbmNoise.Helpers.Normalize(this.m_nBuffer[index1], this.m_nDimensions);
        }
        while (--index1 > 0)
        {
          int index3 = (int) crandom.RandomI(0, (int) byte.MaxValue);
          FbmNoise.Helpers.SWAP(ref this.m_nMap[index1], ref this.m_nMap[index3]);
        }
      }

      public float Noise(ref float x, ref float y)
      {
        int ix = (int) Math.Floor((double) x);
        int iy = (int) Math.Floor((double) y);
        float fx = x - (float) ix;
        float fy = y - (float) iy;
        float x1 = (float) ((double) fx * (double) fx * (3.0 - 2.0 * (double) fx));
        float x2 = (float) ((double) fy * (double) fy * (3.0 - 2.0 * (double) fy));
        return FbmNoise.Helpers.CLAMP(-0.99999f, 0.99999f, FbmNoise.Helpers.Lerp(FbmNoise.Helpers.Lerp(this.LatticeOptimized(ix, fx, iy, fy), this.LatticeOptimized(ix + 1, fx - 1f, iy, fy), x1), FbmNoise.Helpers.Lerp(this.LatticeOptimized(ix, fx, iy + 1, fy - 1f), this.LatticeOptimized(ix + 1, fx - 1f, iy + 1, fy - 1f), x1), x2));
      }
    }

    public class CFractal : FbmNoise.CNoise
    {
      private float PI = 3.14159274f;
      private float HALF_PI = 1.57079637f;
      private float TWO_PI = 6.28318548f;
      private float LOGHALF = -0.6931472f;
      private float LOGHALFI = -1.442695f;
      private float DELTA = 1E-06f;
      private int MAX_OCTAVES = 128;
      private int MAX_DIMENSIONS = 4;
      protected float m_fH;
      protected float m_fLacunarity;
      protected float[] m_fExponent = new float[128];

      public CFractal()
      {
      }

      public CFractal(int nSeed, float fH, float fLacunarity) => this.Init(nSeed, fH, fLacunarity);

      public void Init(int nSeed, float fH, float fLacunarity)
      {
        this.Init(nSeed);
        this.m_fH = fH;
        this.m_fLacunarity = fLacunarity;
        float x = 1f;
        for (int index = 0; index < this.MAX_OCTAVES; ++index)
        {
          this.m_fExponent[index] = (float) Math.Pow((double) x, -(double) this.m_fH);
          x *= this.m_fLacunarity;
        }
      }

      public float fBm(ref float x, ref float y, int fOctaves)
      {
        float x1 = 0.0f;
        float x2 = x;
        float y1 = y;
        for (int index = 0; index < fOctaves; ++index)
        {
          x1 += this.Noise(ref x2, ref y1) * this.m_fExponent[index];
          x2 *= this.m_fLacunarity;
          y1 *= this.m_fLacunarity;
        }
        return FbmNoise.Helpers.CLAMP(-0.99999f, 0.99999f, x1);
      }

      public float Turbulence(ref float x, ref float y, int fOctaves)
      {
        float x1 = 0.0f;
        float x2 = x;
        float y1 = y;
        for (int index = 0; index < fOctaves; ++index)
        {
          x1 += Math.Abs(this.Noise(ref x2, ref y1)) * this.m_fExponent[index];
          x2 *= this.m_fLacunarity;
          y1 *= this.m_fLacunarity;
        }
        return FbmNoise.Helpers.CLAMP(-0.99999f, 0.99999f, x1);
      }

      public float Heterofractal(ref float x, ref float y, int fOctaves, float fOffset)
      {
        float x1 = this.Noise(ref x, ref y) + fOffset;
        float num1 = x;
        float num2 = y;
        float x2 = num1 * this.m_fLacunarity;
        float y1 = num2 * this.m_fLacunarity;
        for (int index = 1; index < fOctaves; ++index)
        {
          x1 += (this.Noise(ref x2, ref y1) + fOffset) * this.m_fExponent[index] * x1;
          x2 *= this.m_fLacunarity;
          y1 *= this.m_fLacunarity;
        }
        return FbmNoise.Helpers.CLAMP(-0.99999f, 0.99999f, x1);
      }

      public float RidgedMultifractal(
        ref float x,
        ref float y,
        int fOctaves,
        float fOffset,
        float fGain)
      {
        float num1 = fOffset - Math.Abs(this.Noise(ref x, ref y));
        float num2 = num1 * num1;
        float x1 = num2;
        float x2 = x;
        float y1 = y;
        for (int index = 1; index < fOctaves; ++index)
        {
          float num3 = FbmNoise.Helpers.CLAMP(0.0f, 1f, num2 * fGain);
          float num4 = fOffset - Math.Abs(this.Noise(ref x2, ref y1));
          num2 = num4 * num4 * num3;
          x1 += num2 * this.m_fExponent[index];
        }
        return FbmNoise.Helpers.CLAMP(-0.99999f, 0.99999f, x1);
      }
    }
  }
}
