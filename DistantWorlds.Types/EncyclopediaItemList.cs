// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EncyclopediaItemList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EncyclopediaItemList : List<EncyclopediaItem>
  {
    public EncyclopediaItem this[string title]
    {
      get
      {
        foreach (EncyclopediaItem encyclopediaItem in (List<EncyclopediaItem>) this)
        {
          if (encyclopediaItem.Title.ToLower(CultureInfo.InvariantCulture) == title.ToLower(CultureInfo.InvariantCulture))
            return encyclopediaItem;
        }
        return (EncyclopediaItem) null;
      }
    }

    public EncyclopediaItemList GetItemsByCategory(EncyclopediaCategory category)
    {
      EncyclopediaItemList itemsByCategory = new EncyclopediaItemList();
      foreach (EncyclopediaItem encyclopediaItem in (List<EncyclopediaItem>) this)
      {
        if (encyclopediaItem.Category == category)
          itemsByCategory.Add(encyclopediaItem);
      }
      return itemsByCategory;
    }
  }
}
