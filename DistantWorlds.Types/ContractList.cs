// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ContractList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ContractList : SyncList<Contract>
  {
    public Contract GetContractForCargoWithRemainingPickup(Cargo cargo)
    {
      if (cargo != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Contract withRemainingPickup = this[index];
          if (withRemainingPickup != null && withRemainingPickup.BuyerEmpireId == cargo.EmpireId && withRemainingPickup.AmountToFulfill - withRemainingPickup.AmountPickedUp > 0)
          {
            if (cargo.CommodityIsResource)
            {
              if (withRemainingPickup.ResourceId == (int) cargo.Resource.ResourceID)
                return withRemainingPickup;
            }
            else if (cargo.CommodityIsComponent && withRemainingPickup.ComponentId == cargo.Component.ComponentID)
              return withRemainingPickup;
          }
        }
      }
      return (Contract) null;
    }

    public Contract GetContractForCargoWithRemainingDelivery(Cargo cargo)
    {
      if (cargo != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Contract remainingDelivery = this[index];
          if (remainingDelivery != null && remainingDelivery.BuyerEmpireId == cargo.EmpireId && remainingDelivery.AmountToFulfill - remainingDelivery.AmountDelivered > 0)
          {
            if (cargo.CommodityIsResource)
            {
              if (remainingDelivery.ResourceId == (int) cargo.Resource.ResourceID)
                return remainingDelivery;
            }
            else if (cargo.CommodityIsComponent && remainingDelivery.ComponentId == cargo.Component.ComponentID)
              return remainingDelivery;
          }
        }
      }
      return (Contract) null;
    }
  }
}
