// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ShipGroupList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ShipGroupList : SyncList<ShipGroup>
  {
    public ShipGroupList DetermineFleetsTravellingToLocation(
      double x,
      double y,
      double acceptableRange)
    {
      int num1 = (int) (x - acceptableRange);
      int num2 = (int) (x + acceptableRange);
      int num3 = (int) (y - acceptableRange);
      int num4 = (int) (y + acceptableRange);
      ShipGroupList travellingToLocation = new ShipGroupList();
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup.Mission != null && shipGroup.Mission.Type != BuiltObjectMissionType.Undefined)
        {
          int xpos = (int) shipGroup.LeadShip.Xpos;
          int ypos = (int) shipGroup.LeadShip.Ypos;
          if (xpos <= num1 || xpos >= num2 || ypos <= num3 || ypos >= num4)
          {
            Point point = shipGroup.Mission.ResolveTargetCoordinates(shipGroup.Mission);
            if (point.X > num1 && point.X < num2 && point.Y > num3 && point.Y < num4)
              travellingToLocation.Add(shipGroup);
          }
        }
      }
      return travellingToLocation;
    }

    public int CountLargeFleets()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ShipTargetAmount >= 10)
          ++num;
      }
      return num;
    }

    public ShipGroup IdentifyLargestFleet()
    {
      ShipGroup shipGroup = (ShipGroup) null;
      for (int index = 0; index < this.Count; ++index)
      {
        if (shipGroup == null || this[index].Ships.Count > shipGroup.Ships.Count)
          shipGroup = this[index];
      }
      return shipGroup;
    }

    public int CountTotalFirepower()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
        num += this[index].TotalFirepower;
      return num;
    }

    public int CountTotalOverallStrengthFactor()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
        num += this[index].TotalOverallStrengthFactor;
      return num;
    }

    public ShipGroupList ResolveFleetsWithAttackTarget(
      object attackTarget,
      out double nearestDistance)
    {
      nearestDistance = double.MaxValue;
      switch (attackTarget)
      {
        case BuiltObject _:
          return this.ResolveFleetsWithAttackTarget((StellarObject) attackTarget, out nearestDistance);
        case Habitat _:
          return this.ResolveFleetsWithAttackTarget((StellarObject) attackTarget, out nearestDistance);
        case Creature _:
          return this.ResolveFleetsWithAttackTarget((StellarObject) attackTarget, out nearestDistance);
        case ShipGroup _:
          return this.ResolveFleetsWithAttackTarget((ShipGroup) attackTarget, out nearestDistance);
        default:
          return new ShipGroupList();
      }
    }

    public ShipGroupList ResolveFleetsWithAttackTarget(StellarObject attackTarget)
    {
      ShipGroupList shipGroupList = new ShipGroupList();
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Mission != null && (shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack || shipGroup.Mission.Type == BuiltObjectMissionType.Bombard || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndBombard || shipGroup.Mission.Type == BuiltObjectMissionType.Capture || shipGroup.Mission.Type == BuiltObjectMissionType.Raid))
        {
          switch (attackTarget)
          {
            case BuiltObject _:
              BuiltObject builtObject = (BuiltObject) attackTarget;
              if (shipGroup.Mission.TargetBuiltObject == builtObject)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            case Habitat _:
              Habitat habitat = (Habitat) attackTarget;
              if (shipGroup.Mission.TargetHabitat == habitat)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            case Creature _:
              Creature creature = (Creature) attackTarget;
              if (shipGroup.Mission.TargetCreature == creature)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return shipGroupList;
    }

    public ShipGroupList ResolveFleetsWithAttackTarget(ShipGroup attackTarget)
    {
      ShipGroupList shipGroupList = new ShipGroupList();
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Mission != null && (shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack) && shipGroup.Mission.TargetShipGroup == attackTarget)
          shipGroupList.Add(shipGroup);
      }
      return shipGroupList;
    }

    public ShipGroupList ResolveFleetsWithAttackTarget(
      StellarObject attackTarget,
      out double nearestDistance)
    {
      ShipGroupList shipGroupList = new ShipGroupList();
      nearestDistance = double.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Mission != null && (shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack || shipGroup.Mission.Type == BuiltObjectMissionType.Bombard || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndBombard || shipGroup.Mission.Type == BuiltObjectMissionType.Capture || shipGroup.Mission.Type == BuiltObjectMissionType.Raid))
        {
          switch (attackTarget)
          {
            case BuiltObject _:
              BuiltObject builtObject = (BuiltObject) attackTarget;
              if (shipGroup.Mission.TargetBuiltObject == builtObject)
              {
                shipGroupList.Add(shipGroup);
                double distanceStatic = Galaxy.CalculateDistanceStatic(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, builtObject.Xpos, builtObject.Ypos);
                if (distanceStatic < nearestDistance)
                {
                  nearestDistance = distanceStatic;
                  continue;
                }
                continue;
              }
              continue;
            case Habitat _:
              Habitat habitat = (Habitat) attackTarget;
              if (shipGroup.Mission.TargetHabitat == habitat)
              {
                shipGroupList.Add(shipGroup);
                double distanceStatic = Galaxy.CalculateDistanceStatic(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, habitat.Xpos, habitat.Ypos);
                if (distanceStatic < nearestDistance)
                {
                  nearestDistance = distanceStatic;
                  continue;
                }
                continue;
              }
              continue;
            case Creature _:
              Creature creature = (Creature) attackTarget;
              if (shipGroup.Mission.TargetCreature == creature)
              {
                shipGroupList.Add(shipGroup);
                double distanceStatic = Galaxy.CalculateDistanceStatic(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, creature.Xpos, creature.Ypos);
                if (distanceStatic < nearestDistance)
                {
                  nearestDistance = distanceStatic;
                  continue;
                }
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return shipGroupList;
    }

    public ShipGroupList ResolveFleetsWithAttackTarget(
      ShipGroup attackTarget,
      out double nearestDistance)
    {
      ShipGroupList shipGroupList = new ShipGroupList();
      nearestDistance = double.MaxValue;
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Mission != null && (shipGroup.Mission.Type == BuiltObjectMissionType.Attack || shipGroup.Mission.Type == BuiltObjectMissionType.WaitAndAttack) && shipGroup.Mission.TargetShipGroup == attackTarget)
        {
          shipGroupList.Add(shipGroup);
          if (attackTarget.LeadShip != null)
          {
            double distanceStatic = Galaxy.CalculateDistanceStatic(shipGroup.LeadShip.Xpos, shipGroup.LeadShip.Ypos, attackTarget.LeadShip.Xpos, attackTarget.LeadShip.Ypos);
            if (distanceStatic < nearestDistance)
              nearestDistance = distanceStatic;
          }
        }
      }
      return shipGroupList;
    }

    public ShipGroupList ResolveFleetsWithWaitTarget(StellarObject waitTarget)
    {
      ShipGroupList shipGroupList = new ShipGroupList();
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        if (shipGroup != null && shipGroup.LeadShip != null && shipGroup.Mission != null && shipGroup.Mission.Type == BuiltObjectMissionType.MoveAndWait)
        {
          switch (waitTarget)
          {
            case BuiltObject _:
              BuiltObject builtObject = (BuiltObject) waitTarget;
              if (shipGroup.Mission.TargetBuiltObject == builtObject)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            case Habitat _:
              Habitat habitat = (Habitat) waitTarget;
              if (shipGroup.Mission.TargetHabitat == habitat)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            case Creature _:
              Creature creature = (Creature) waitTarget;
              if (shipGroup.Mission.TargetCreature == creature)
              {
                shipGroupList.Add(shipGroup);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return shipGroupList;
    }

    //public void ClearSortTags()
    //{
    //  for (int index = 0; index < this.Count; ++index)
    //  {
    //    ShipGroup shipGroup = this[index];
    //    if (shipGroup != null)
    //      shipGroup.SortTag = 0.0;
    //  }
    //}

    public ShipGroupList OrderByName()
    {
      ShipGroupList shipGroupList1 = new ShipGroupList();
      ShipGroupList shipGroupList2 = new ShipGroupList();
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.Count; ++index)
      {
        ShipGroup shipGroup = this[index];
        stringList.Add(shipGroup.Name);
        shipGroupList2.Add(shipGroup);
      }
      ShipGroup[] array = shipGroupList2.ToArray();
      Array.Sort<string, ShipGroup>(stringList.ToArray(), array);
      shipGroupList1.AddRange((IEnumerable<ShipGroup>) array);
      return shipGroupList1;
    }
  }
}
