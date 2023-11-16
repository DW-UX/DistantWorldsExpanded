// Decompiled with JetBrains decompiler
// Type: DistantWorlds.FastPerlinNoise
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;

namespace DistantWorlds
{
    public class FastPerlinNoise
    {
        private static int[][] int_0;

        private static int[][] int_1;

        private static int[] int_2;

        private static int[] int_3;

        private static int[][] int_4;

        private static double double_0;

        private static double double_1;

        static FastPerlinNoise()
        {
            
            int_0 = new int[12][]
            {
            new int[3] { 1, 1, 0 },
            new int[3] { -1, 1, 0 },
            new int[3] { 1, -1, 0 },
            new int[3] { -1, -1, 0 },
            new int[3] { 1, 0, 1 },
            new int[3] { -1, 0, 1 },
            new int[3] { 1, 0, -1 },
            new int[3] { -1, 0, -1 },
            new int[3] { 0, 1, 1 },
            new int[3] { 0, -1, 1 },
            new int[3] { 0, 1, -1 },
            new int[3] { 0, -1, -1 }
            };
            int_1 = new int[32][]
            {
            new int[4] { 0, 1, 1, 1 },
            new int[4] { 0, 1, 1, -1 },
            new int[4] { 0, 1, -1, 1 },
            new int[4] { 0, 1, -1, -1 },
            new int[4] { 0, -1, 1, 1 },
            new int[4] { 0, -1, 1, -1 },
            new int[4] { 0, -1, -1, 1 },
            new int[4] { 0, -1, -1, -1 },
            new int[4] { 1, 0, 1, 1 },
            new int[4] { 1, 0, 1, -1 },
            new int[4] { 1, 0, -1, 1 },
            new int[4] { 1, 0, -1, -1 },
            new int[4] { -1, 0, 1, 1 },
            new int[4] { -1, 0, 1, -1 },
            new int[4] { -1, 0, -1, 1 },
            new int[4] { -1, 0, -1, -1 },
            new int[4] { 1, 1, 0, 1 },
            new int[4] { 1, 1, 0, -1 },
            new int[4] { 1, -1, 0, 1 },
            new int[4] { 1, -1, 0, -1 },
            new int[4] { -1, 1, 0, 1 },
            new int[4] { -1, 1, 0, -1 },
            new int[4] { -1, -1, 0, 1 },
            new int[4] { -1, -1, 0, -1 },
            new int[4] { 1, 1, 1, 0 },
            new int[4] { 1, 1, -1, 0 },
            new int[4] { 1, -1, 1, 0 },
            new int[4] { 1, -1, -1, 0 },
            new int[4] { -1, 1, 1, 0 },
            new int[4] { -1, 1, -1, 0 },
            new int[4] { -1, -1, 1, 0 },
            new int[4] { -1, -1, -1, 0 }
            };
            int_2 = new int[256]
            {
            151, 160, 137, 91, 90, 15, 131, 13, 201, 95,
            96, 53, 194, 233, 7, 225, 140, 36, 103, 30,
            69, 142, 8, 99, 37, 240, 21, 10, 23, 190,
            6, 148, 247, 120, 234, 75, 0, 26, 197, 62,
            94, 252, 219, 203, 117, 35, 11, 32, 57, 177,
            33, 88, 237, 149, 56, 87, 174, 20, 125, 136,
            171, 168, 68, 175, 74, 165, 71, 134, 139, 48,
            27, 166, 77, 146, 158, 231, 83, 111, 229, 122,
            60, 211, 133, 230, 220, 105, 92, 41, 55, 46,
            245, 40, 244, 102, 143, 54, 65, 25, 63, 161,
            1, 216, 80, 73, 209, 76, 132, 187, 208, 89,
            18, 169, 200, 196, 135, 130, 116, 188, 159, 86,
            164, 100, 109, 198, 173, 186, 3, 64, 52, 217,
            226, 250, 124, 123, 5, 202, 38, 147, 118, 126,
            255, 82, 85, 212, 207, 206, 59, 227, 47, 16,
            58, 17, 182, 189, 28, 42, 223, 183, 170, 213,
            119, 248, 152, 2, 44, 154, 163, 70, 221, 153,
            101, 155, 167, 43, 172, 9, 129, 22, 39, 253,
            19, 98, 108, 110, 79, 113, 224, 232, 178, 185,
            112, 104, 218, 246, 97, 228, 251, 34, 242, 193,
            238, 210, 144, 12, 191, 179, 162, 241, 81, 51,
            145, 235, 249, 14, 239, 107, 49, 192, 214, 31,
            181, 199, 106, 157, 184, 84, 204, 176, 115, 121,
            50, 45, 127, 4, 150, 254, 138, 236, 205, 93,
            222, 114, 67, 29, 24, 72, 243, 141, 128, 195,
            78, 66, 215, 61, 156, 180
            };
            int_3 = new int[512];
            int[][] array = new int[64][];
            array[0] = new int[4] { 0, 1, 2, 3 };
            array[1] = new int[4] { 0, 1, 3, 2 };
            int[] array2 = (array[2] = new int[4]);
            array[3] = new int[4] { 0, 2, 3, 1 };
            int[] array3 = (array[4] = new int[4]);
            int[] array4 = (array[5] = new int[4]);
            int[] array5 = (array[6] = new int[4]);
            array[7] = new int[4] { 1, 2, 3, 0 };
            array[8] = new int[4] { 0, 2, 1, 3 };
            int[] array6 = (array[9] = new int[4]);
            array[10] = new int[4] { 0, 3, 1, 2 };
            array[11] = new int[4] { 0, 3, 2, 1 };
            int[] array7 = (array[12] = new int[4]);
            int[] array8 = (array[13] = new int[4]);
            int[] array9 = (array[14] = new int[4]);
            array[15] = new int[4] { 1, 3, 2, 0 };
            int[] array10 = (array[16] = new int[4]);
            int[] array11 = (array[17] = new int[4]);
            int[] array12 = (array[18] = new int[4]);
            int[] array13 = (array[19] = new int[4]);
            int[] array14 = (array[20] = new int[4]);
            int[] array15 = (array[21] = new int[4]);
            int[] array16 = (array[22] = new int[4]);
            int[] array17 = (array[23] = new int[4]);
            array[24] = new int[4] { 1, 2, 0, 3 };
            int[] array18 = (array[25] = new int[4]);
            array[26] = new int[4] { 1, 3, 0, 2 };
            int[] array19 = (array[27] = new int[4]);
            int[] array20 = (array[28] = new int[4]);
            int[] array21 = (array[29] = new int[4]);
            array[30] = new int[4] { 2, 3, 0, 1 };
            array[31] = new int[4] { 2, 3, 1, 0 };
            array[32] = new int[4] { 1, 0, 2, 3 };
            array[33] = new int[4] { 1, 0, 3, 2 };
            int[] array22 = (array[34] = new int[4]);
            int[] array23 = (array[35] = new int[4]);
            int[] array24 = (array[36] = new int[4]);
            array[37] = new int[4] { 2, 0, 3, 1 };
            int[] array25 = (array[38] = new int[4]);
            array[39] = new int[4] { 2, 1, 3, 0 };
            int[] array26 = (array[40] = new int[4]);
            int[] array27 = (array[41] = new int[4]);
            int[] array28 = (array[42] = new int[4]);
            int[] array29 = (array[43] = new int[4]);
            int[] array30 = (array[44] = new int[4]);
            int[] array31 = (array[45] = new int[4]);
            int[] array32 = (array[46] = new int[4]);
            int[] array33 = (array[47] = new int[4]);
            array[48] = new int[4] { 2, 0, 1, 3 };
            int[] array34 = (array[49] = new int[4]);
            int[] array35 = (array[50] = new int[4]);
            int[] array36 = (array[51] = new int[4]);
            array[52] = new int[4] { 3, 0, 1, 2 };
            array[53] = new int[4] { 3, 0, 2, 1 };
            int[] array37 = (array[54] = new int[4]);
            array[55] = new int[4] { 3, 1, 2, 0 };
            array[56] = new int[4] { 2, 1, 0, 3 };
            int[] array38 = (array[57] = new int[4]);
            int[] array39 = (array[58] = new int[4]);
            int[] array40 = (array[59] = new int[4]);
            array[60] = new int[4] { 3, 1, 0, 2 };
            int[] array41 = (array[61] = new int[4]);
            array[62] = new int[4] { 3, 2, 0, 1 };
            array[63] = new int[4] { 3, 2, 1, 0 };
            int_4 = array;
            double_0 = 0.5 * (Math.Sqrt(3.0) - 1.0);
            double_1 = (3.0 - Math.Sqrt(3.0)) / 6.0;
            for (int i = 0; i < 512; i++)
            {
                int_3[i] = int_2[i & 0xFF];
            }
        }

        private static int smethod_0(double double_2)
        {
            if (!(double_2 > 0.0))
            {
                return (int)double_2 - 1;
            }
            return (int)double_2;
        }

        private static double smethod_1(object object_0, double double_2, double double_3)
        {
            return (double)((int[])object_0)[0] * double_2 + (double)((int[])object_0)[1] * double_3;
        }

        private static double smethod_2(object object_0, double double_2, double double_3, double double_4)
        {
            return (double)((int[])object_0)[0] * double_2 + (double)((int[])object_0)[1] * double_3 + (double)((int[])object_0)[2] * double_4;
        }

        private static double smethod_3(object object_0, double double_2, double double_3, double double_4, double double_5)
        {
            return (double)((int[])object_0)[0] * double_2 + (double)((int[])object_0)[1] * double_3 + (double)((int[])object_0)[2] * double_4 + (double)((int[])object_0)[3] * double_5;
        }

        public static double noise(double xin, double yin)
        {
            double num = (xin + yin) * double_0;
            int num2 = smethod_0(xin + num);
            int num3 = smethod_0(yin + num);
            double num4 = (double)(num2 + num3) * double_1;
            double num5 = (double)num2 - num4;
            double num6 = (double)num3 - num4;
            double num7 = xin - num5;
            double num8 = yin - num6;
            int num9;
            int num10;
            if (num7 > num8)
            {
                num9 = 1;
                num10 = 0;
            }
            else
            {
                num9 = 0;
                num10 = 1;
            }
            double num11 = num7 - (double)num9 + double_1;
            double num12 = num8 - (double)num10 + double_1;
            double num13 = num7 - 1.0 + 2.0 * double_1;
            double num14 = num8 - 1.0 + 2.0 * double_1;
            int num15 = num2 & 0xFF;
            int num16 = num3 & 0xFF;
            int num17 = int_3[num15 + int_3[num16]] % 12;
            int num18 = int_3[num15 + num9 + int_3[num16 + num10]] % 12;
            int num19 = int_3[num15 + 1 + int_3[num16 + 1]] % 12;
            double num20 = 0.5 - num7 * num7 - num8 * num8;
            double num21;
            if (num20 < 0.0)
            {
                num21 = 0.0;
            }
            else
            {
                num20 *= num20;
                num21 = num20 * num20 * smethod_1(int_0[num17], num7, num8);
            }
            double num22 = 0.5 - num11 * num11 - num12 * num12;
            double num23;
            if (num22 < 0.0)
            {
                num23 = 0.0;
            }
            else
            {
                num22 *= num22;
                num23 = num22 * num22 * smethod_1(int_0[num18], num11, num12);
            }
            double num24 = 0.5 - num13 * num13 - num14 * num14;
            double num25;
            if (num24 < 0.0)
            {
                num25 = 0.0;
            }
            else
            {
                num24 *= num24;
                num25 = num24 * num24 * smethod_1(int_0[num19], num13, num14);
            }
            return 70.0 * (num21 + num23 + num25);
        }

        private static double smethod_4(double double_2, double double_3, double double_4)
        {
            return (1.0 - double_4) * double_2 + double_4 * double_3;
        }

        private static double smethod_5(double double_2)
        {
            return double_2 * double_2 * double_2 * (double_2 * (double_2 * 6.0 - 15.0) + 10.0);
        }

        public static double classicNoise(double x, double y, double z)
        {
            int num = smethod_0(x);
            int num2 = smethod_0(y);
            int num3 = smethod_0(z);
            x -= (double)num;
            y -= (double)num2;
            z -= (double)num3;
            num &= 0xFF;
            num2 &= 0xFF;
            num3 &= 0xFF;
            int num4 = int_3[num + int_3[num2 + int_3[num3]]] % 12;
            int num5 = int_3[num + int_3[num2 + int_3[num3 + 1]]] % 12;
            int num6 = int_3[num + int_3[num2 + 1 + int_3[num3]]] % 12;
            int num7 = int_3[num + int_3[num2 + 1 + int_3[num3 + 1]]] % 12;
            int num8 = int_3[num + 1 + int_3[num2 + int_3[num3]]] % 12;
            int num9 = int_3[num + 1 + int_3[num2 + int_3[num3 + 1]]] % 12;
            int num10 = int_3[num + 1 + int_3[num2 + 1 + int_3[num3]]] % 12;
            int num11 = int_3[num + 1 + int_3[num2 + 1 + int_3[num3 + 1]]] % 12;
            double double_ = smethod_2(int_0[num4], x, y, z);
            double double_2 = smethod_2(int_0[num8], x - 1.0, y, z);
            double double_3 = smethod_2(int_0[num6], x, y - 1.0, z);
            double double_4 = smethod_2(int_0[num10], x - 1.0, y - 1.0, z);
            double double_5 = smethod_2(int_0[num5], x, y, z - 1.0);
            double double_6 = smethod_2(int_0[num9], x - 1.0, y, z - 1.0);
            double double_7 = smethod_2(int_0[num7], x, y - 1.0, z - 1.0);
            double double_8 = smethod_2(int_0[num11], x - 1.0, y - 1.0, z - 1.0);
            double double_9 = smethod_5(x);
            double double_10 = smethod_5(y);
            double double_11 = smethod_5(z);
            double double_12 = smethod_4(double_, double_2, double_9);
            double double_13 = smethod_4(double_5, double_6, double_9);
            double double_14 = smethod_4(double_3, double_4, double_9);
            double double_15 = smethod_4(double_7, double_8, double_9);
            double double_16 = smethod_4(double_12, double_14, double_10);
            double double_17 = smethod_4(double_13, double_15, double_10);
            return smethod_4(double_16, double_17, double_11);
        }

        public FastPerlinNoise():base()
        {
            
        }
    }

}
