// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Animation
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;

namespace DistantWorlds
{
    [Serializable]
    public class Animation
    {
        private Bitmap[] bitmap_0;

        private Texture2D[] texture2D_0;

        private double double_0;

        private double double_1;

        private DateTime dateTime_0;

        private double WjktOqguy;

        private int int_0;

        private int int_1;

        private Color color_0;

        private int taNyJukrT;

        private int int_2;

        public bool DisposeTexturesWhenComplete;

        public Bitmap[] Images
        {
            get
            {
                return bitmap_0;
            }
            set
            {
                bitmap_0 = value;
            }
        }

        public Texture2D[] Textures
        {
            get
            {
                return texture2D_0;
            }
            set
            {
                texture2D_0 = value;
            }
        }

        public double Xpos
        {
            get
            {
                return double_0;
            }
            set
            {
                double_0 = value;
            }
        }

        public double Ypos
        {
            get
            {
                return double_1;
            }
            set
            {
                double_1 = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return dateTime_0;
            }
            set
            {
                dateTime_0 = value;
            }
        }

        public double RotationAngle
        {
            get
            {
                return WjktOqguy;
            }
            set
            {
                WjktOqguy = value;
            }
        }

        public int Width
        {
            get
            {
                return int_0;
            }
            set
            {
                int_0 = value;
            }
        }

        public int Height
        {
            get
            {
                return int_1;
            }
            set
            {
                int_1 = value;
            }
        }

        public Color TintColor
        {
            get
            {
                return color_0;
            }
            set
            {
                color_0 = value;
            }
        }

        public int FramesPerSecond
        {
            get
            {
                return taNyJukrT;
            }
            set
            {
                taNyJukrT = value;
            }
        }

        public int CurrentFrame
        {
            get
            {
                return int_2;
            }
            set
            {
                int_2 = value;
            }
        }

        public Animation(Bitmap[] images, DateTime startTime, int framesPerSecond, double x, double y, int width, int height) : 
            this(images, startTime, framesPerSecond, x, y, width, height, 0.0, Color.Empty)
        {
            Class7.VEFSJNszvZKMZ();
        }

        public Animation(Bitmap[] images, DateTime startTime, int framesPerSecond, double x, double y, int width, int height, double rotationAngle, Color tintColor)
        {
            Class7.VEFSJNszvZKMZ();
            DisposeTexturesWhenComplete = true;
            bitmap_0 = images;
            dateTime_0 = startTime;
            taNyJukrT = framesPerSecond;
            double_0 = x;
            double_1 = y;
            int_0 = width;
            int_1 = height;
            WjktOqguy = rotationAngle;
            color_0 = tintColor;
        }

        public Animation(Texture2D[] textures, DateTime startTime, int framesPerSecond, double x, double y, int width, int height) : this(textures, startTime, framesPerSecond, x, y, width, height, 0.0, Color.Empty)
        {
            Class7.VEFSJNszvZKMZ();
        }

        public Animation(Texture2D[] textures, DateTime startTime, int framesPerSecond, double x, double y, int width, int height, double rotationAngle, Color tintColor)
        {
            Class7.VEFSJNszvZKMZ();
            DisposeTexturesWhenComplete = true;
            texture2D_0 = textures;
            dateTime_0 = startTime;
            taNyJukrT = framesPerSecond;
            double_0 = x;
            double_1 = y;
            int_0 = width;
            int_1 = height;
            WjktOqguy = rotationAngle;
            color_0 = tintColor;
        }

        public void Teardown()
        {
            if (!DisposeTexturesWhenComplete || texture2D_0 == null || texture2D_0.Length <= 0)
            {
                return;
            }
            for (int i = 0; i < texture2D_0.Length; i++)
            {
                if (texture2D_0[i] != null)
                {
                    texture2D_0[i].Dispose();
                    texture2D_0[i] = null;
                }
            }
        }
    }

}
