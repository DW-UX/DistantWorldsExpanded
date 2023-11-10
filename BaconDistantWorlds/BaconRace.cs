// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconRace
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;

namespace BaconDistantWorlds
{
  public static class BaconRace
  {
    public static double myCivilianShipSizeMultiplier = 3.0;
    public static double myMilitaryShipSizeMultiplier = 3.0;

    public static double MilitaryShipSizeMultiplier(Empire empire)
    {
      double num = 1.0;
      if (empire != null && empire.Name.Contains("Romulan"))
        num = BaconRace.myMilitaryShipSizeMultiplier;
      return num;
    }

    public static double CivilianShipSizeMultiplier(Empire empire)
    {
      double num = 1.0;
      if (empire != null && empire.Name.Contains("Romulan"))
        num = BaconRace.myCivilianShipSizeMultiplier;
      return num;
    }
  }
}
