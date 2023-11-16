// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconBuiltObjectList
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System.Linq;

namespace BaconDistantWorlds
{
  public static class BaconBuiltObjectList
  {
    public static BuiltObject FindShortestConstructionWaitQueue(
      BuiltObjectList bol,
      BuiltObject ship,
      out double shortestWaitQueueTime,
      bool includeVerySmallYards,
      int maximumQueueDepth)
    {
      shortestWaitQueueTime = double.MaxValue;
      BuiltObject constructionWaitQueue = (BuiltObject) null;
      int num1 = 2;
      BuiltObject builtObject1 = (BuiltObject) null;
      for (int index = 0; index < bol.Count<BuiltObject>(); ++index)
      {
        BuiltObject builtObject2 = bol[index];
        if (builtObject2 != null && builtObject2.IsSpacePort && builtObject2.IsShipYard)
        {
          bool flag1 = true;
          int num2 = 0;
          if (builtObject2.ConstructionQueue != null && builtObject2.ConstructionQueue.ConstructionYards != null)
            num2 = builtObject2.ConstructionQueue.ConstructionYards.Count<ConstructionYard>();
          if (!includeVerySmallYards && builtObject2.ExtractionGas > (short) 0 && num2 <= 1)
            flag1 = false;
          if (flag1 && builtObject2.Empire.CanBuildBuiltObject(ship) && !builtObject2.Name.StartsWith("--") && (builtObject2.ConstructionQueue.ConstructionWaitQueue.Count<BuiltObject>() + (num2 - 1)) / num2 <= maximumQueueDepth)
          {
            double num3 = double.MaxValue;
            if (builtObject2.ConstructionQueue != null)
              num3 = builtObject2.ConstructionQueue.EstimateCurrentWaitQueueTime();
            if (num3 < shortestWaitQueueTime)
            {
              bool flag2 = false;
              if (builtObject2.ManufacturingQueue != null && builtObject2.ManufacturingQueue.DeficientResources != null && builtObject2.ManufacturingQueue.DeficientResources.Count<ResourceDatePair>() > num1)
                flag2 = true;
              if (!flag2)
              {
                shortestWaitQueueTime = num3;
                constructionWaitQueue = builtObject2;
              }
              else
                builtObject1 = builtObject2;
            }
          }
        }
      }
      if (constructionWaitQueue == null && builtObject1 != null)
        constructionWaitQueue = builtObject1;
      return constructionWaitQueue;
    }
  }
}
