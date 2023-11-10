// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconBuiltObjectImageCache
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BaconDistantWorlds
{
    public static class BaconBuiltObjectImageCache
    {
        private static int overallIndex = -1;
        public static List<string> shipPictures = new List<string>();

        public static void AddMoreImages(
          BuiltObjectImageCache imageCache,
          int index,
          string regularPath,
          string modPath)
        {
            List<string> stringList = new List<string>()
              {
                "Escort",
                "Frigate",
                "Destroyer",
                "Cruiser",
                "CapitalShip",
                "TroopTransport",
                "Carrier",
                "ResupplyShip",
                "ExplorationShip",
                "SmallFreighter",
                "MediumFreighter",
                "LargeFreighter",
                "ColonyShip",
                "PassengerShip",
                "ConstructionShip",
                "GasMiningShip",
                "MiningShip",
                "GasMiningStation",
                "MiningStation",
                "SmallSpacePort",
                "MediumSpacePort",
                "LargeSpacePort",
                "ResortBase",
                "GenericBase"
              };

            List<Task<string>> taskList = new List<Task<string>>();

            if (BaconBuiltObjectImageCache.overallIndex == -1)
                BaconBuiltObjectImageCache.overallIndex = index;
            foreach (string str1 in stringList)
            {
                for (int index1 = 0; index1 < 5; ++index1)
                {
                    string str2 = index1 != 0 ? index1.ToString() : "";
                    try
                    {
                        if (File.Exists(regularPath + str1 + str2 + ".png") || File.Exists(modPath + str1 + str2 + ".png") || File.Exists(regularPath + str1 + str2 + ".bmp") || File.Exists(modPath + str1 + str2 + ".bmp"))
                        {
                            int localIndex = BaconBuiltObjectImageCache.overallIndex++;
                            //string str3 = imageCache.CheckLoadSmallImage(BaconBuiltObjectImageCache.overallIndex, regularPath + str1 + str2, modPath + str1 + str2);
                            //if (str3 != null)
                            //    imageCache._Filepaths.Add(str3);
                            taskList.Add(Task.Run(() => imageCache.CheckLoadSmallImage(localIndex, regularPath + str1 + str2, modPath + str1 + str2)));
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }

            Task.WaitAll(taskList.ToArray());
            foreach (var item in taskList)
            {
                if (item.Result != null)
                { imageCache._Filepaths.Add(item.Result); }
            }
            BaconBuiltObjectImageCache.shipPictures = imageCache._Filepaths;
        }
    }
}
