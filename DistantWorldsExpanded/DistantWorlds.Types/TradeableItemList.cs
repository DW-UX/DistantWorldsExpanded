// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TradeableItemList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class TradeableItemList : SyncList<TradeableItem>
  {
    public int TotalValue
    {
      get
      {
        int totalValue = 0;
        foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>) this)
          totalValue += tradeableItem.Value;
        return totalValue;
      }
    }

    public TradeableItemList Clone()
    {
      TradeableItemList tradeableItemList = new TradeableItemList();
      foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>) this)
        tradeableItemList.Add(tradeableItem.Clone());
      return tradeableItemList;
    }

    public bool CheckIdentical(TradeableItemList otherItems)
    {
      if (otherItems == null || otherItems.Count != this.Count)
        return false;
      for (int index1 = 0; index1 < otherItems.Count; ++index1)
      {
        TradeableItem otherItem = otherItems[index1];
        int index2 = this.IndexOf(otherItem);
        if (index2 < 0)
          return false;
        TradeableItem tradeableItem = this[index2];
        if (otherItem.Type == TradeableItemType.Money)
        {
          double num1 = 0.0;
          if (otherItem.Item is double)
            num1 = (double) otherItem.Item;
          double num2 = 0.0;
          if (tradeableItem.Item is double)
            num2 = (double) tradeableItem.Item;
          if (num1 != num2)
            return false;
        }
      }
      return true;
    }

    public override string ToString() => this.BuildTradeableItemsDescription(this, false);

    private string BuildTradeableItemsDescription(TradeableItemList items, bool showValues)
    {
      string str = string.Empty;
      foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>) items)
      {
        str = tradeableItem.Type != TradeableItemType.GalaxyMap ? (tradeableItem.Type != TradeableItemType.TerritoryMap ? str + tradeableItem.ToString(showValues) : str + TextResolver.GetText("Trade Description OUR TERRITORY MAP")) : str + TextResolver.GetText("Trade Description OUR GALAXY MAP");
        str += ", ";
      }
      if (!string.IsNullOrEmpty(str))
        str = str.Substring(0, str.Length - 2);
      return str;
    }

    public int FindAnyGovernmentStyle()
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Type == TradeableItemType.AdoptGovernmentStyle)
            return index;
        }
      }
      return -1;
    }

    public TradeableItemList ExtractHighOrderedItemsByType(TradeableItemType type) => this.ExtractHighOrderedItemsByType(new TradeableItemType[1]
    {
      type
    });

    public TradeableItemList ExtractHighOrderedItemsByType(TradeableItemType[] types)
    {
      TradeableItemList orderedItemsByType = new TradeableItemList();
      if (types != null && types.Length > 0)
      {
        foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>) this)
        {
          for (int index = 0; index < types.Length; ++index)
          {
            if (tradeableItem.Type == types[index])
              orderedItemsByType.Add(tradeableItem);
          }
        }
        orderedItemsByType.Sort();
        orderedItemsByType.Reverse();
      }
      return orderedItemsByType;
    }

    public bool ContainsType(TradeableItemType type)
    {
      foreach (TradeableItem tradeableItem in (SyncList<TradeableItem>) this)
      {
        if (tradeableItem.Type == type)
          return true;
      }
      return false;
    }

    public new int IndexOf(TradeableItem tradeableItem)
    {
      Habitat habitat = (Habitat) null;
      GovernmentAttributes governmentAttributes = (GovernmentAttributes) null;
      BuiltObject builtObject = (BuiltObject) null;
      ResearchNode researchNode = (ResearchNode) null;
      Empire empire = (Empire) null;
      GalaxyLocation galaxyLocation = (GalaxyLocation) null;
      switch (tradeableItem.Type)
      {
        case TradeableItemType.Money:
          double num = (double) tradeableItem.Item;
          break;
        case TradeableItemType.Colony:
        case TradeableItemType.SystemMap:
        case TradeableItemType.IndependentColonyLocation:
          habitat = (Habitat) tradeableItem.Item;
          break;
        case TradeableItemType.Base:
          builtObject = (BuiltObject) tradeableItem.Item;
          break;
        case TradeableItemType.TerritoryMap:
        case TradeableItemType.GalaxyMap:
        case TradeableItemType.ThreatenWar:
        case TradeableItemType.DeclareWarOther:
        case TradeableItemType.ThreatenTradeSanctions:
        case TradeableItemType.InitiateTradeSanctionsOther:
        case TradeableItemType.EndWar:
        case TradeableItemType.EndWarOther:
        case TradeableItemType.LiftTradeSanctions:
        case TradeableItemType.LiftTradeSanctionsOther:
        case TradeableItemType.ContactEmpire:
          empire = (Empire) tradeableItem.Item;
          break;
        case TradeableItemType.AdoptGovernmentStyle:
          governmentAttributes = (GovernmentAttributes) tradeableItem.Item;
          break;
        case TradeableItemType.ResearchProject:
          researchNode = (ResearchNode) tradeableItem.Item;
          break;
        case TradeableItemType.SecretLocation:
          if (tradeableItem.Item is GalaxyLocation)
          {
            galaxyLocation = (GalaxyLocation) tradeableItem.Item;
            break;
          }
          if (tradeableItem.Item is Habitat)
          {
            habitat = (Habitat) tradeableItem.Item;
            break;
          }
          break;
      }
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          TradeableItem tradeableItem1 = this[index];
          switch (tradeableItem.Type)
          {
            case TradeableItemType.Money:
              if (tradeableItem1.Type == TradeableItemType.Money)
                return index;
              break;
            case TradeableItemType.Colony:
              if (tradeableItem1.Type == TradeableItemType.Colony && (Habitat) tradeableItem1.Item == habitat)
                return index;
              break;
            case TradeableItemType.Base:
              if (tradeableItem1.Type == TradeableItemType.Base && (BuiltObject) tradeableItem1.Item == builtObject)
                return index;
              break;
            case TradeableItemType.TerritoryMap:
              if (tradeableItem1.Type == TradeableItemType.TerritoryMap)
                return index;
              break;
            case TradeableItemType.GalaxyMap:
              if (tradeableItem1.Type == TradeableItemType.GalaxyMap)
                return index;
              break;
            case TradeableItemType.AdoptGovernmentStyle:
              if (tradeableItem1.Type == TradeableItemType.AdoptGovernmentStyle && ((GovernmentAttributes) tradeableItem1.Item).GovernmentId == governmentAttributes.GovernmentId)
                return index;
              break;
            case TradeableItemType.ThreatenWar:
              if (tradeableItem1.Type == TradeableItemType.ThreatenWar)
                return index;
              break;
            case TradeableItemType.DeclareWarOther:
              if (tradeableItem1.Type == TradeableItemType.DeclareWarOther && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.ThreatenTradeSanctions:
              if (tradeableItem1.Type == TradeableItemType.ThreatenTradeSanctions)
                return index;
              break;
            case TradeableItemType.InitiateTradeSanctionsOther:
              if (tradeableItem1.Type == TradeableItemType.InitiateTradeSanctionsOther && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.EndWar:
              if (tradeableItem1.Type == TradeableItemType.EndWar && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.EndWarOther:
              if (tradeableItem1.Type == TradeableItemType.EndWarOther && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.LiftTradeSanctions:
              if (tradeableItem1.Type == TradeableItemType.LiftTradeSanctions && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.LiftTradeSanctionsOther:
              if (tradeableItem1.Type == TradeableItemType.LiftTradeSanctionsOther && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.ResearchProject:
              if (tradeableItem1.Type == TradeableItemType.ResearchProject && ((ResearchNode) tradeableItem1.Item).ResearchNodeId == researchNode.ResearchNodeId)
                return index;
              break;
            case TradeableItemType.ContactEmpire:
              if (tradeableItem1.Type == TradeableItemType.ContactEmpire && (Empire) tradeableItem1.Item == empire)
                return index;
              break;
            case TradeableItemType.SecretLocation:
              if (tradeableItem1.Type == TradeableItemType.SecretLocation)
              {
                if (tradeableItem1.Item is GalaxyLocation)
                {
                  if ((GalaxyLocation) tradeableItem1.Item == galaxyLocation)
                    return index;
                  break;
                }
                if (tradeableItem1.Item is Habitat && (Habitat) tradeableItem1.Item == habitat)
                  return index;
                break;
              }
              break;
            case TradeableItemType.SystemMap:
              if (tradeableItem1.Type == TradeableItemType.SystemMap && (Habitat) tradeableItem1.Item == habitat)
                return index;
              break;
            case TradeableItemType.IndependentColonyLocation:
              if (tradeableItem1.Type == TradeableItemType.IndependentColonyLocation && (Habitat) tradeableItem1.Item == habitat)
                return index;
              break;
          }
        }
      }
      return -1;
    }
  }
}
