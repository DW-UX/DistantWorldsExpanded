// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TradeableItem
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class TradeableItem : IComparable<TradeableItem>
  {
    private TradeableItemType _Type;
    private object _Item;
    private int _Value;
    public bool ShowSecretLocationNames = true;

    public TradeableItem(TradeableItemType type, object item, int value)
    {
      this._Type = type;
      this._Item = item;
      this._Value = value;
    }

    public TradeableItemType Type => this._Type;

    public object Item => this._Item;

    public int Value => this._Value;

    public TradeableItem Clone() => new TradeableItem(this.Type, this.Item, this.Value);

    public override string ToString() => this.ToString(true);

    public string ToString(bool showValue)
    {
      string str1 = string.Empty;
      switch (this.Type)
      {
        case TradeableItemType.Money:
          double num = (double) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description Money"), (object) num.ToString("###,###,###,##0"));
          break;
        case TradeableItemType.Colony:
          Habitat habitat1 = (Habitat) this.Item;
          Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat1);
          str1 = string.Format(TextResolver.GetText("Trade Description Colony NAME PLANETTYPE SYSTEMNAME"), (object) habitat1.Name, (object) Galaxy.ResolveDescription(habitat1.Type), (object) habitatSystemStar.Name);
          break;
        case TradeableItemType.Base:
          BuiltObject builtObject = (BuiltObject) this.Item;
          string str2 = string.Empty;
          if (builtObject.NearestSystemStar != null)
            str2 = builtObject.NearestSystemStar.Name;
          string str3 = Galaxy.ResolveSectorDescriptionStatic(builtObject.Xpos, builtObject.Ypos);
          str1 = string.Format(TextResolver.GetText("Trade Description Base With Sector"), (object) builtObject.Name, (object) str2, (object) str3);
          break;
        case TradeableItemType.TerritoryMap:
          str1 = TextResolver.GetText("Trade Description Territory Map");
          break;
        case TradeableItemType.GalaxyMap:
          str1 = TextResolver.GetText("Trade Description Galaxy Map");
          break;
        case TradeableItemType.AdoptGovernmentStyle:
          GovernmentAttributes governmentAttributes = (GovernmentAttributes) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description AdoptGovernmentStyle"), (object) governmentAttributes.Name);
          break;
        case TradeableItemType.ThreatenWar:
          str1 = TextResolver.GetText("Trade Description Threaten War");
          break;
        case TradeableItemType.DeclareWarOther:
          Empire empire1 = (Empire) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description Declare War Other"), (object) empire1.Name);
          break;
        case TradeableItemType.ThreatenTradeSanctions:
          str1 = TextResolver.GetText("Trade Description Threaten Trade Sanctions");
          break;
        case TradeableItemType.InitiateTradeSanctionsOther:
          Empire empire2 = (Empire) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description Trade Sanctions Other"), (object) empire2.Name);
          break;
        case TradeableItemType.EndWar:
          str1 = TextResolver.GetText("Trade Description End War You");
          break;
        case TradeableItemType.EndWarOther:
          Empire empire3 = (Empire) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description End War Other"), (object) empire3.Name);
          break;
        case TradeableItemType.LiftTradeSanctions:
          str1 = TextResolver.GetText("Trade Description Lift Trade Sanctions You");
          break;
        case TradeableItemType.LiftTradeSanctionsOther:
          Empire empire4 = (Empire) this.Item;
          str1 = string.Format(TextResolver.GetText("Trade Description Lift Trade Sanctions Other"), (object) empire4.Name);
          break;
        case TradeableItemType.ResearchProject:
          str1 = "" + ((ResearchNode) this.Item).Name;
          break;
        case TradeableItemType.ContactEmpire:
          if (this.Item is Empire)
          {
            str1 = ((Empire) this.Item).Name;
            break;
          }
          break;
        case TradeableItemType.SecretLocation:
          if (this.ShowSecretLocationNames)
          {
            if (this.Item is GalaxyLocation)
            {
              str1 = ((GalaxyLocation) this.Item).Name;
              break;
            }
            if (this.Item is Habitat)
            {
              str1 = ((StellarObject) this.Item).Name;
              break;
            }
            break;
          }
          str1 = TextResolver.GetText("Secret Location");
          break;
        case TradeableItemType.SystemMap:
          if (this.Item is Habitat)
          {
            Habitat habitat2 = (Habitat) this.Item;
            str1 = string.Format(TextResolver.GetText("Trade Description System Map"), (object) habitat2.Name);
            break;
          }
          break;
        case TradeableItemType.IndependentColonyLocation:
          if (this.Item is Habitat)
          {
            str1 = ((StellarObject) this.Item).Name;
            break;
          }
          break;
      }
      if (showValue && this.Type != TradeableItemType.Money && this.Type != TradeableItemType.TerritoryMap && this.Type != TradeableItemType.GalaxyMap && this.Type != TradeableItemType.ThreatenWar && this.Type != TradeableItemType.ThreatenTradeSanctions)
        str1 = str1 + " (" + this.Value.ToString("###,###,###,##0") + ")";
      return str1;
    }

    int IComparable<TradeableItem>.CompareTo(TradeableItem other) => this.Value.CompareTo(other.Value);
  }
}
