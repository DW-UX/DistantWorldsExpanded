// Decompiled with JetBrains decompiler
// Type: DistantWorlds.ItemListPanelList
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Collections.Generic;

namespace DistantWorlds
{
    [Serializable]
    public class ItemListPanelList : List<ItemListPanel>
    {
        public ItemListPanel this[string title]
        {
            get
            {
                int num = 0;
                while (true)
                {
                    if (num < base.Count)
                    {
                        if (base[num].TitleText == title)
                        {
                            break;
                        }
                        num++;
                        continue;
                    }
                    return null;
                }
                return base[num];
            }
        }

        public ItemListPanelList() : base()
        {
            
        }
    }
}
