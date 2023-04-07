// Decompiled with JetBrains decompiler
// Type: DistantWorlds.EventPing
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Drawing;

namespace DistantWorlds
{
    [Serializable]
    public class EventPing
    {
        private Point point_0;

        private object object_0;

        public Point Point
        {
            get
            {
                return point_0;
            }
            set
            {
                point_0 = value;
            }
        }

        public object Object
        {
            get
            {
                return object_0;
            }
            set
            {
                object_0 = value;
            }
        }

        public EventPing(int x, int y, object data):base()
        {
            Class7.VEFSJNszvZKMZ();
            point_0 = new Point(x, y);
            object_0 = data;
        }
    }

}
