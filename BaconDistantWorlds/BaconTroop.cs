// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconTroop
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;

namespace BaconDistantWorlds
{
  public static class BaconTroop
  {
    public static int MultiplyStrength(Troop troop)
    {
      int num = 1;
      if (troop != null && troop.Empire != null && troop.Empire.Name.Contains("Romulan"))
        num = 3;
      return num;
    }
  }
}
