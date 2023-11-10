// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconManufacturingQueue
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;

namespace BaconDistantWorlds
{
  public static class BaconManufacturingQueue
  {
    public static void NotifyResourceShortage(
      ManufacturingQueue queue,
      long starDate,
      DateTime date,
      Empire recipientEmpire,
      StellarObject subject)
    {
      try
      {
        foreach (ResourceDatePair deficientResource in (List<ResourceDatePair>) queue.DeficientResources)
        {
          ComponentResource componentResource = new ComponentResource(deficientResource.ResourceId, (short) 1000);
          if (subject is Habitat)
            subject.Cargo.Add(new Cargo((Resource) componentResource, 1000, recipientEmpire));
          else if (subject is BuiltObject)
            subject.Cargo.Add(new Cargo((Resource) componentResource, 1000, recipientEmpire));
        }
      }
      catch (Exception ex)
      {
      }
    }

    public static void GiveResourcesThatAreDeficient(Main main)
    {
      try
      {
        object selectedObject = BaconBuiltObject.myMain._Game.SelectedObject;
        switch (selectedObject)
        {
          case Habitat _:
            Habitat subject1 = selectedObject as Habitat;
            BaconManufacturingQueue.NotifyResourceShortage(subject1.ManufacturingQueue, main._Game.Galaxy.CurrentStarDate, DateTime.Now, subject1.Empire, (StellarObject) subject1);
            break;
          case BuiltObject _:
            BuiltObject subject2 = selectedObject as BuiltObject;
            BaconManufacturingQueue.NotifyResourceShortage(subject2.ManufacturingQueue, main._Game.Galaxy.CurrentStarDate, DateTime.Now, subject2.Owner, (StellarObject) subject2);
            break;
        }
      }
      catch (Exception ex)
      {
      }
    }
  }
}
