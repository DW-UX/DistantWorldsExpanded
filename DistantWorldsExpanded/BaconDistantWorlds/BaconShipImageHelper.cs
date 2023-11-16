// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconShipImageHelper
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;

namespace BaconDistantWorlds
{
  public static class BaconShipImageHelper
  {
    public static int ResolveNewFighterImageIndex(
      FighterSpecification fighterSpec,
      Race empireRace,
      bool isPirates)
    {
      if ((double) fighterSpec.SortTag > 0.0)
        return (int) fighterSpec.SortTag;
      int num = 0;
      if (empireRace != null)
      {
        num = empireRace.DesignPictureFamilyIndex;
        if (isPirates && empireRace.DesignPictureFamilyIndexPirates > 0)
          num = empireRace.DesignPictureFamilyIndexPirates;
      }
      int fighterImageCount = ShipImageHelper.ShipSetFighterImageCount;
      return num * fighterImageCount;
    }

    public static int ResolveNewBomberImageIndex(
      FighterSpecification fighterSpec,
      Race empireRace,
      bool isPirates)
    {
      if ((double) fighterSpec.SortTag > 0.0)
        return (int) fighterSpec.SortTag;
      int num = 0;
      if (empireRace != null)
      {
        num = empireRace.DesignPictureFamilyIndex;
        if (isPirates && empireRace.DesignPictureFamilyIndexPirates > 0)
          num = empireRace.DesignPictureFamilyIndexPirates;
      }
      int fighterImageCount = ShipImageHelper.ShipSetFighterImageCount;
      return num * fighterImageCount + 1;
    }
  }
}
