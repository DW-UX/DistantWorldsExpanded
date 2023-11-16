// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ForceStructureProjection
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ForceStructureProjection : IComparable<ForceStructureProjection>
  {
    private long _ProjectionDate;
    private BuiltObjectSubRole _SubRole;
    private int _Amount;

    public ForceStructureProjection(BuiltObjectSubRole subRole, int amount, long projectionDate)
    {
      this._SubRole = subRole;
      this._Amount = amount;
      this._ProjectionDate = projectionDate;
    }

    public long ProjectionDate => this._ProjectionDate;

    public BuiltObjectSubRole SubRole => this._SubRole;

    public int Amount
    {
      get => this._Amount;
      set => this._Amount = value;
    }

    private int ObtainWeighting(ForceStructureProjection forceStructureProjection)
    {
      int weighting;
      switch (forceStructureProjection.SubRole)
      {
        case BuiltObjectSubRole.Escort:
          weighting = 20;
          break;
        case BuiltObjectSubRole.Frigate:
          weighting = 19;
          break;
        case BuiltObjectSubRole.Destroyer:
          weighting = 18;
          break;
        case BuiltObjectSubRole.Cruiser:
          weighting = 16;
          break;
        case BuiltObjectSubRole.CapitalShip:
          weighting = 15;
          break;
        case BuiltObjectSubRole.TroopTransport:
          weighting = 14;
          break;
        case BuiltObjectSubRole.Carrier:
          weighting = 17;
          break;
        case BuiltObjectSubRole.ResupplyShip:
          weighting = 21;
          break;
        case BuiltObjectSubRole.ExplorationShip:
          weighting = 13;
          break;
        case BuiltObjectSubRole.SmallFreighter:
          weighting = 8;
          break;
        case BuiltObjectSubRole.MediumFreighter:
          weighting = 7;
          break;
        case BuiltObjectSubRole.LargeFreighter:
          weighting = 6;
          break;
        case BuiltObjectSubRole.ColonyShip:
          weighting = 9;
          break;
        case BuiltObjectSubRole.ConstructionShip:
          weighting = 1;
          break;
        case BuiltObjectSubRole.GasMiningShip:
          weighting = 4;
          break;
        case BuiltObjectSubRole.MiningShip:
          weighting = 4;
          break;
        case BuiltObjectSubRole.GasMiningStation:
          weighting = 2;
          break;
        case BuiltObjectSubRole.MiningStation:
          weighting = 2;
          break;
        case BuiltObjectSubRole.SmallSpacePort:
          weighting = 12;
          break;
        case BuiltObjectSubRole.MediumSpacePort:
          weighting = 11;
          break;
        case BuiltObjectSubRole.LargeSpacePort:
          weighting = 10;
          break;
        default:
          weighting = 100;
          break;
      }
      return weighting;
    }

    public int CompareTo(ForceStructureProjection other) => this.ObtainWeighting(this).CompareTo(this.ObtainWeighting(other));
  }
}
