// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EncyclopediaItemChangedEventArgs
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using DistantWorlds.Types;
using System;

namespace DistantWorlds.Controls
{
  public class EncyclopediaItemChangedEventArgs : EventArgs
  {
    public EncyclopediaItem Item;

    public EncyclopediaItemChangedEventArgs(EncyclopediaItem item) => this.Item = item;
  }
}
