// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TutorialItemList
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class TutorialItemList : List<TutorialItem>
  {
    public TutorialItem this[string title]
    {
      get
      {
        foreach (TutorialItem tutorialItem in (List<TutorialItem>) this)
        {
          if (tutorialItem.Title == title)
            return tutorialItem;
        }
        return (TutorialItem) null;
      }
    }

    public TutorialItemList():base()
    {
      
      // ISSUE: explicit constructor call
    }
  }
}
