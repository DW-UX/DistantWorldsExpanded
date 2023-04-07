// Decompiled with JetBrains decompiler
// Type: DistantWorlds.SoundEffectRequest
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;

namespace DistantWorlds
{
    [Serializable]
    public class SoundEffectRequest
    {
        private double double_0;

        private double double_1;

        private int int_0;

        private string string_0;

        public double Volume
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

        public double Balance
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

        public int Frequency
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

        public string Filename
        {
            get
            {
                return string_0;
            }
            set
            {
                string_0 = value;
            }
        }

        public SoundEffectRequest():base()
        {
            Class7.VEFSJNszvZKMZ();
        }
    }
}
