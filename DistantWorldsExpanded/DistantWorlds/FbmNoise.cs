// Decompiled with JetBrains decompiler
// Type: DistantWorlds.FbmNoise
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;

namespace DistantWorlds
{
    public class FbmNoise
    {
        private class Class2
        {
            public static float float_0;

            public static float float_1;

            public static float float_2;

            public static float float_3;

            public static float float_4;

            public static float float_5;

            public static int int_0;

            public static int int_1;

            public static float smethod_0(float float_6)
            {
                return float_6 * float_6;
            }

            public static float smethod_1(float float_6, float float_7)
            {
                if (!(float_6 < float_7))
                {
                    return float_7;
                }
                return float_6;
            }

            public static float smethod_2(float float_6, float float_7)
            {
                if (!(float_6 > float_7))
                {
                    return float_7;
                }
                return float_6;
            }

            public static float smethod_3(float float_6)
            {
                if (!(float_6 < 0f))
                {
                    return float_6;
                }
                return 0f - float_6;
            }

            public static float smethod_4(float float_6, float float_7, float float_8)
            {
                if (!(float_8 < float_6))
                {
                    if (!(float_8 > float_7))
                    {
                        return float_8;
                    }
                    return float_7;
                }
                return float_6;
            }

            public static float smethod_5(float float_6, float float_7, float float_8)
            {
                return float_6 + float_8 * (float_7 - float_6);
            }

            public static float smethod_6(float float_6)
            {
                return float_6 * float_6 * (3f - 2f * float_6);
            }

            public static float smethod_7(float float_6, float float_7, float float_8)
            {
                return smethod_4(0f, 1f, (float_8 - float_6) / (float_7 - float_6));
            }

            public static float smethod_8(float float_6, float float_7)
            {
                return (float)Math.Pow(float_6, 1f / float_7);
            }

            public static float smethod_9(float float_6, float float_7)
            {
                return (float)Math.Pow(float_6, Math.Log10(float_7) * (double)float_4);
            }

            public static float smethod_10(float float_6, float float_7)
            {
                return (float)(1.0 - Math.Exp((0f - float_6) * float_7));
            }

            public static void smethod_11(ref byte byte_0, ref byte byte_1)
            {
                byte b = byte_0;
                byte_0 = byte_1;
                byte_1 = b;
            }

            public static float smethod_12(float float_6, float float_7, float float_8)
            {
                if (!(float_8 < float_6))
                {
                    if (!(float_8 > float_7))
                    {
                        return float_8;
                    }
                    return float_7;
                }
                return float_6;
            }

            public static float smethod_13(float float_6, float float_7, float float_8)
            {
                return float_6 + float_8 * (float_7 - float_6);
            }

            public static float smethod_14(float float_6, float float_7)
            {
                if (float_6 <= float_5)
                {
                    return 0f;
                }
                if (float_6 >= 1f - float_5)
                {
                    return 1f;
                }
                float num = (float)(Math.Log10(1f - float_7) * (double)float_4);
                if ((double)float_6 < 0.5)
                {
                    return (float)Math.Pow(2f * float_6, num) * 0.5f;
                }
                return (float)(1.0 - Math.Pow(2f * (1f - float_6), num) * 0.5);
            }

            public static float smethod_15(float float_6, float float_7, float float_8)
            {
                if (float_8 <= float_6)
                {
                    return 0f;
                }
                if (float_8 >= float_7)
                {
                    return 1f;
                }
                return smethod_6((float_8 - float_6) / (float_7 - float_6));
            }

            public static float smethod_16(float float_6, float float_7)
            {
                float_6 -= (float)(int)(float_6 / float_7) * float_7;
                if (float_6 < 0f)
                {
                    float_6 += float_7;
                }
                return float_6;
            }

            public static void smethod_17(object object_0, int int_2)
            {
                float num = 0f;
                for (int i = 0; i < int_2; i++)
                {
                    num += ((float[])object_0)[i] * ((float[])object_0)[i];
                }
                num = (float)(1.0 / Math.Sqrt(num));
                for (int j = 0; j < int_2; j++)
                {
                    ((float[])object_0)[j] *= num;
                }
            }

            public Class2() : base()
            {
                
            }

            static Class2()
            {
                
                float_0 = (float)Math.PI;
                float_1 = (float)Math.PI / 2f;
                float_2 = (float)Math.PI * 2f;
                float_3 = -0.6931472f;
                float_4 = -1.442695f;
                float_5 = 1E-06f;
                int_0 = 4;
                int_1 = 128;
            }
        }

        private class Class3
        {
            private Random random_0;

            public Class3():base()
            {
                
                random_0 = new Random();
            }

            public Class3(int int_0) : base()
            {
                
                method_0(int_0);
            }

            public void method_0(int int_0)
            {
                random_0 = new Random(int_0);
            }

            public double method_1()
            {
                return random_0.NextDouble();
            }

            public double method_2(double double_0, double double_1)
            {
                double num = double_1 - double_0;
                double val = num * method_1();
                return double_0 + Math.Min(val, num);
            }

            public uint method_3(int int_0, int int_1)
            {
                int num = int_1 - int_0;
                int val = (int)(((double)num + 1.0) * method_1());
                return (uint)(int_0 + Math.Min(val, num));
            }
        }

        public abstract class CNoise
        {
            //private float float_0;

            //private float float_1;

            //private float float_2;

            //private float float_3;

            //private float float_4;

            //private float float_5;

            //private int int_0;

            //private int int_1;

            protected int m_nDimensions;

            protected byte[] m_nMap;

            protected float[][] m_nBuffer;

            protected float Lattice(int ix, float fx)
            {
                return Lattice(ix, fx, 0, 0f, 0, 0f, 0, 0f);
            }

            protected float Lattice(int ix, float fx, int iy, float fy)
            {
                return Lattice(ix, fx, iy, fy, 0, 0f, 0, 0f);
            }

            protected float Lattice(int ix, float fx, int iy, float fy, int iz, float fz)
            {
                return Lattice(ix, fx, iy, fy, iz, fz, 0, 0f);
            }

            protected float Lattice(int ix, float fx, int iy, float fy, int iz, float fz, int iw, float fw)
            {
                int[] array = new int[4] { ix, iy, iz, iw };
                float[] array2 = new float[4] { fx, fy, fz, fw };
                int num = 0;
                for (int i = 0; i < m_nDimensions; i++)
                {
                    num = m_nMap[(num + array[i]) & 0xFF];
                }
                float num2 = 0f;
                for (int j = 0; j < m_nDimensions; j++)
                {
                    num2 += m_nBuffer[num][j] * array2[j];
                }
                return num2;
            }

            protected float LatticeOptimized(int ix, float fx, int iy, float fy)
            {
                int num = 0;
                num = m_nMap[(0 + ix) & 0xFF];
                num = m_nMap[(num + iy) & 0xFF];
                float num2 = m_nBuffer[num][0] * fx;
                return num2 + m_nBuffer[num][1] * fy;
            }

            public CNoise():base()
            {
                
                //float_0 = (float)Math.PI;
                //float_1 = (float)Math.PI / 2f;
                //float_2 = (float)Math.PI * 2f;
                //float_3 = -0.6931472f;
                //float_4 = -1.442695f;
                //float_5 = 1E-06f;
                //int_0 = 4;
                //int_1 = 128;
                m_nMap = new byte[256];
                m_nBuffer = new float[256][];
            }

            public CNoise(int nSeed):base()
            {
                
                //float_0 = (float)Math.PI;
                //float_1 = (float)Math.PI / 2f;
                //float_2 = (float)Math.PI * 2f;
                //float_3 = -0.6931472f;
                //float_4 = -1.442695f;
                //float_5 = 1E-06f;
                //int_0 = 4;
                //int_1 = 128;
                m_nMap = new byte[256];
                m_nBuffer = new float[256][];
                Init(nSeed);
            }

            public void Init(int nSeed)
            {
                m_nDimensions = 2;
                Class3 @class = new Class3(nSeed);
                int i;
                for (i = 0; i < 256; i++)
                {
                    m_nMap[i] = (byte)i;
                    m_nBuffer[i] = new float[256];
                    for (int j = 0; j < m_nDimensions; j++)
                    {
                        m_nBuffer[i][j] = (float)@class.method_2(-0.5, 0.5);
                    }
                    Class2.smethod_17(m_nBuffer[i], m_nDimensions);
                }
                while (--i > 0)
                {
                    int j = (int)@class.method_3(0, 255);
                    Class2.smethod_11(ref m_nMap[i], ref m_nMap[j]);
                }
            }

            public float Noise(ref float x, ref float y)
            {
                int num = (int)Math.Floor(x);
                int num2 = (int)Math.Floor(y);
                float num3 = x - (float)num;
                float num4 = y - (float)num2;
                float float_ = num3 * num3 * (3f - 2f * num3);
                float float_2 = num4 * num4 * (3f - 2f * num4);
                float float_3 = Class2.smethod_5(Class2.smethod_5(LatticeOptimized(num, num3, num2, num4), LatticeOptimized(num + 1, num3 - 1f, num2, num4), float_), Class2.smethod_5(LatticeOptimized(num, num3, num2 + 1, num4 - 1f), LatticeOptimized(num + 1, num3 - 1f, num2 + 1, num4 - 1f), float_), float_2);
                return Class2.smethod_12(-0.99999f, 0.99999f, float_3);
            }
        }

        public class CFractal : CNoise
        {
            //private float float_6;

            //private float float_7;

            //private float float_8;

            //private float float_9;

            //private float float_10;

            //private float float_11;

            private int int_2;

            //private int int_3;

            protected float m_fH;

            protected float m_fLacunarity;

            protected float[] m_fExponent;

            public CFractal() : base()
            {
                
                //float_6 = (float)Math.PI;
                //float_7 = (float)Math.PI / 2f;
                //float_8 = (float)Math.PI * 2f;
                //float_9 = -0.6931472f;
                //float_10 = -1.442695f;
                //float_11 = 1E-06f;
                int_2 = 128;
                //int_3 = 4;
                m_fExponent = new float[128];
            }

            public CFractal(int nSeed, float fH, float fLacunarity) :base()
            {
                
                //float_6 = (float)Math.PI;
                //float_7 = (float)Math.PI / 2f;
                //float_8 = (float)Math.PI * 2f;
                //float_9 = -0.6931472f;
                //float_10 = -1.442695f;
                //float_11 = 1E-06f;
                //int_2 = 128;
                //int_3 = 4;
                m_fExponent = new float[128];
                Init(nSeed, fH, fLacunarity);
            }

            public void Init(int nSeed, float fH, float fLacunarity)
            {
                Init(nSeed);
                m_fH = fH;
                m_fLacunarity = fLacunarity;
                float num = 1f;
                for (int i = 0; i < int_2; i++)
                {
                    m_fExponent[i] = (float)Math.Pow(num, 0f - m_fH);
                    num *= m_fLacunarity;
                }
            }

            public float fBm(ref float x, ref float y, int fOctaves)
            {
                float num = 0f;
                float x2 = x;
                float y2 = y;
                for (int i = 0; i < fOctaves; i++)
                {
                    num += Noise(ref x2, ref y2) * m_fExponent[i];
                    x2 *= m_fLacunarity;
                    y2 *= m_fLacunarity;
                }
                return Class2.smethod_12(-0.99999f, 0.99999f, num);
            }

            public float Turbulence(ref float x, ref float y, int fOctaves)
            {
                float num = 0f;
                float x2 = x;
                float y2 = y;
                for (int i = 0; i < fOctaves; i++)
                {
                    num += Math.Abs(Noise(ref x2, ref y2)) * m_fExponent[i];
                    x2 *= m_fLacunarity;
                    y2 *= m_fLacunarity;
                }
                return Class2.smethod_12(-0.99999f, 0.99999f, num);
            }

            public float Heterofractal(ref float x, ref float y, int fOctaves, float fOffset)
            {
                float num = Noise(ref x, ref y) + fOffset;
                float num2 = x;
                float num3 = y;
                num2 *= m_fLacunarity;
                num3 *= m_fLacunarity;
                for (int i = 1; i < fOctaves; i++)
                {
                    num += (Noise(ref num2, ref num3) + fOffset) * m_fExponent[i] * num;
                    num2 *= m_fLacunarity;
                    num3 *= m_fLacunarity;
                }
                return Class2.smethod_12(-0.99999f, 0.99999f, num);
            }

            public float RidgedMultifractal(ref float x, ref float y, int fOctaves, float fOffset, float fGain)
            {
                float num = fOffset - Math.Abs(Noise(ref x, ref y));
                num *= num;
                float num2 = num;
                float x2 = x;
                float y2 = y;
                for (int i = 1; i < fOctaves; i++)
                {
                    float num3 = Class2.smethod_12(0f, 1f, num * fGain);
                    num = fOffset - Math.Abs(Noise(ref x2, ref y2));
                    num *= num;
                    num *= num3;
                    num2 += num * m_fExponent[i];
                }
                return Class2.smethod_12(-0.99999f, 0.99999f, num2);
            }
        }

        private int int_0;

        //private float float_0;

        //private float float_1;

        //private float float_2;

        //private float float_3;

        //private float float_4;

        //private float float_5;

        //private int int_1;

        //private int int_2;

        public FbmNoise(int seed):base()
        {
            
            //float_0 = (float)Math.PI;
            //float_1 = (float)Math.PI / 2f;
            //float_2 = (float)Math.PI * 2f;
            //float_3 = -0.6931472f;
            //float_4 = -1.442695f;
            //float_5 = 1E-06f;
            //int_1 = 4;
            //int_2 = 128;
            int_0 = seed;
        }

        public byte[][] MakeNoise(int size, double roughness, double lacunarity)
        {
            byte[][] array = new byte[size][];
            for (int i = 0; i < size; i++)
            {
                array[i] = new byte[size];
            }
            CFractal cFractal = new CFractal(int_0, (float)roughness, (float)lacunarity);
            float num = (float)size / 5f;
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    float x = (float)k / num;
                    float y = (float)j / num;
                    float num2 = cFractal.fBm(ref x, ref y, 8);
                    array[k][j] = (byte)Math.Max(0f, Math.Min(255f, (num2 + 1f) * 128f));
                }
            }
            return array;
        }

        public byte[][] MakeTurbulence(int size, double roughness, double lacunarity)
        {
            byte[][] array = new byte[size][];
            for (int i = 0; i < size; i++)
            {
                array[i] = new byte[size];
            }
            CFractal cFractal = new CFractal(int_0, (float)roughness, (float)lacunarity);
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    float x = (float)k / 100f;
                    float y = (float)j / 100f;
                    float num = cFractal.Turbulence(ref x, ref y, 8);
                    array[k][j] = (byte)Math.Max(0f, Math.Min(255f, (num + 1f) * 128f));
                }
            }
            return array;
        }

        public byte[][] MakeHeteroFractal(int size, double roughness, double lacunarity, float offset)
        {
            byte[][] array = new byte[size][];
            for (int i = 0; i < size; i++)
            {
                array[i] = new byte[size];
            }
            CFractal cFractal = new CFractal(int_0, (float)roughness, (float)lacunarity);
            for (int j = 0; j < size; j++)
            {
                for (int k = 0; k < size; k++)
                {
                    float x = (float)k / 150f + 2f;
                    float y = (float)j / 150f + 2f;
                    float num = cFractal.Heterofractal(ref x, ref y, 6, offset);
                    array[k][j] = (byte)Math.Max(0f, Math.Min(255f, num * 255f));
                }
            }
            return array;
        }

        public void MakeRidgedMultifractal(ref byte[][] buffer, int size, double roughness, double lacunarity, float offset, float gain, float xOffset, float yOffset, double scaleFactor)
        {
            float num = 900f / (float)scaleFactor;
            CFractal cFractal = new CFractal(int_0, (float)roughness, (float)lacunarity);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float x = (float)j / num + xOffset;
                    float y = (float)i / num + yOffset;
                    float num2 = cFractal.RidgedMultifractal(ref x, ref y, 3, offset, gain);
                    buffer[j][i] = (byte)Math.Max(0f, Math.Min(255f, num2 * 255f));
                }
            }
        }
    }

}
