// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconBuiltObjectMission
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

namespace BaconDistantWorlds
{
  public static class BaconBuiltObjectMission
  {
    public static void LoadMoreCargo(BuiltObjectMission mission, BuiltObject ship)
    {
      if (mission.Type != BuiltObjectMissionType.Transport || mission.Cargo == null)
        return;
      try
      {
        CommandQueue commandQueue = new CommandQueue();
        Point point = mission.ResolveTargetCoordinates(mission);
        Command command1 = (Command) null;
        Command command2 = (Command) null;
        Command command3 = (Command) null;
        Command command4 = (Command) null;
        Command command5 = (Command) null;
        Command command6 = (Command) null;
        Command command7 = (Command) null;
        Command command8 = (Command) null;
        Command command9 = (Command) null;
        Command command10 = (Command) null;
        Command command11 = (Command) null;
        Command command12 = (Command) null;
        Command command13 = (Command) null;
        CargoList cargo1 = mission.Cargo;
        int count = mission.Cargo.Count;
        int cargoCapacity = ship.CargoCapacity;
        if (mission.TargetBuiltObject != null)
        {
          command1 = new Command(CommandAction.ConditionalHyperTo, mission.TargetBuiltObject);
          command2 = new Command(CommandAction.SetParent, mission.TargetBuiltObject);
          command3 = new Command(CommandAction.MoveTo, mission.TargetBuiltObject);
          command4 = new Command(CommandAction.Dock, mission.TargetBuiltObject);
          command5 = new Command(CommandAction.Undock, mission.TargetBuiltObject);
          command6 = new Command(CommandAction.Attack, mission.TargetBuiltObject);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetBuiltObject);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetBuiltObject);
          command10 = new Command(CommandAction.Blockade, mission.TargetBuiltObject);
          command11 = new Command(CommandAction.Escort, mission.TargetBuiltObject);
          command12 = new Command(CommandAction.SprintTo, mission.TargetBuiltObject);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetBuiltObject);
        }
        else if (mission.TargetHabitat != null)
        {
          command1 = new Command(CommandAction.ConditionalHyperTo, mission.TargetHabitat);
          command2 = new Command(CommandAction.SetParent, mission.TargetHabitat);
          command3 = new Command(CommandAction.MoveTo, mission.TargetHabitat);
          command4 = new Command(CommandAction.Dock, mission.TargetHabitat);
          command5 = new Command(CommandAction.Undock, mission.TargetHabitat);
          command6 = new Command(CommandAction.Attack, mission.TargetHabitat);
          command7 = new Command(CommandAction.Bombard, mission.TargetHabitat);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetHabitat);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetHabitat);
          command10 = new Command(CommandAction.Blockade, mission.TargetHabitat);
          command11 = new Command(CommandAction.Escort, mission.TargetHabitat);
          command12 = new Command(CommandAction.SprintTo, mission.TargetHabitat);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetHabitat);
        }
        commandQueue.Enqueue(new Command(CommandAction.ClearParent));
        commandQueue.Enqueue(command1.Clone());
        commandQueue.Enqueue(command2.Clone());
        commandQueue.Enqueue(command3.Clone());
        commandQueue.Enqueue(command4.Clone());
        commandQueue.Enqueue(new Command(CommandAction.Refuel));
        if (mission.Cargo == null || mission.Cargo.Count <= 0)
        {
          if (mission.Population != null && mission.Population.Count > 0)
            commandQueue.Enqueue(new Command(CommandAction.Load, mission.Population.Clone()));
        }
        else
        {
          if (mission.SecondaryTargetBuiltObject == null || mission.SecondaryTargetBuiltObject != null && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.GasMiningStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.MiningStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.ConstructionShip && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.DefensiveBase && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.ResortBase && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.EnergyResearchStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.HighTechResearchStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.WeaponsResearchStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.MonitoringStation && mission.SecondaryTargetBuiltObject.SubRole != BuiltObjectSubRole.GasMiningStation)
          {
            foreach (Cargo cargo2 in (SyncList<Cargo>) cargo1)
              cargo2.Amount = cargoCapacity / count;
          }
          commandQueue.Enqueue(new Command(CommandAction.Load, mission.Cargo.Clone()));
        }
        commandQueue.Enqueue(command5.Clone());
        if (mission.SecondaryTargetHabitat != null)
        {
          Habitat secondaryTargetHabitat = mission.SecondaryTargetHabitat;
          Galaxy.CalculateDistanceStatic(secondaryTargetHabitat.Xpos, secondaryTargetHabitat.Ypos, (double) point.X, (double) point.Y);
          commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, secondaryTargetHabitat));
          commandQueue.Enqueue(new Command(CommandAction.SetParent, secondaryTargetHabitat));
          commandQueue.Enqueue(new Command(CommandAction.MoveTo, secondaryTargetHabitat));
          commandQueue.Enqueue(new Command(CommandAction.Dock, secondaryTargetHabitat));
          if (mission.Cargo != null && mission.Cargo.Count > 0)
            commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Cargo.Clone()));
          else if (mission.Population != null && mission.Population.Count > 0)
            commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Population.Clone()));
          commandQueue.Enqueue(new Command(CommandAction.Refuel));
          commandQueue.Enqueue(new Command(CommandAction.Undock, secondaryTargetHabitat));
          commandQueue.Enqueue(new Command(CommandAction.SetParent, secondaryTargetHabitat));
        }
        else
        {
          BuiltObject target = mission.SecondaryTargetBuiltObject != null ? mission.SecondaryTargetBuiltObject : throw new ApplicationException("Invalid mission target type");
          Galaxy.CalculateDistanceStatic(target.Xpos, target.Ypos, (double) point.X, (double) point.Y);
          commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, target));
          commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
          commandQueue.Enqueue(new Command(CommandAction.MoveTo, target));
          commandQueue.Enqueue(new Command(CommandAction.Dock, target));
          if (mission.Cargo != null && mission.Cargo.Count > 0)
            commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Cargo.Clone()));
          else if (mission.Population != null && mission.Population.Count > 0)
            commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Population.Clone()));
          commandQueue.Enqueue(new Command(CommandAction.Refuel));
          commandQueue.Enqueue(new Command(CommandAction.Undock, target));
          if (target.Role == BuiltObjectRole.Base)
            commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
        }
        commandQueue.Enqueue(mission.GenerateParkCommand());
        FieldInfo field = typeof (BuiltObjectMission).GetField("_Commands", BindingFlags.Instance | BindingFlags.NonPublic);
        if (!(field != (FieldInfo) null))
          return;
        field.SetValue((object) mission, (object) commandQueue);
      }
      catch (Exception ex)
      {
      }
    }

    public static void LoadMoreCargoConstructionShip(BuiltObjectMission mission, BuiltObject ship)
    {
    }

    public static CommandQueue ResolveCommandsForMission(
      BuiltObjectMission theThis,
      BuiltObjectMission mission,
      bool allowReprocessing,
      bool specifiedAsFleetMission,
      out bool couldResolveCommands)
    {
      couldResolveCommands = true;
      CommandQueue commandQueue = new CommandQueue();
      try
      {
        Point point = theThis.ResolveTargetCoordinates(mission);
        theThis._Galaxy.CalculateDistance(theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, (double) point.X, (double) point.Y);
        Command command1 = (Command) null;
        Command command2;
        Command command3;
        Command command4;
        Command command5;
        Command command6;
        Command command7;
        Command command8;
        Command command9;
        Command command10;
        Command command11;
        Command command12;
        Command command13;
        if (mission.TargetBuiltObject != null)
        {
          command2 = new Command(CommandAction.ConditionalHyperTo, mission.TargetBuiltObject);
          command3 = new Command(CommandAction.SetParent, mission.TargetBuiltObject);
          command4 = new Command(CommandAction.MoveTo, mission.TargetBuiltObject);
          command5 = new Command(CommandAction.Dock, mission.TargetBuiltObject);
          command6 = new Command(CommandAction.Undock, mission.TargetBuiltObject);
          command7 = new Command(CommandAction.Attack, mission.TargetBuiltObject);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetBuiltObject);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetBuiltObject);
          command10 = new Command(CommandAction.Blockade, mission.TargetBuiltObject);
          command11 = new Command(CommandAction.Escort, mission.TargetBuiltObject);
          command12 = new Command(CommandAction.SprintTo, mission.TargetBuiltObject);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetBuiltObject);
        }
        else if (mission.TargetHabitat != null)
        {
          command2 = new Command(CommandAction.ConditionalHyperTo, mission.TargetHabitat);
          command3 = new Command(CommandAction.SetParent, mission.TargetHabitat);
          command4 = new Command(CommandAction.MoveTo, mission.TargetHabitat);
          command5 = new Command(CommandAction.Dock, mission.TargetHabitat);
          command6 = new Command(CommandAction.Undock, mission.TargetHabitat);
          command7 = new Command(CommandAction.Attack, mission.TargetHabitat);
          command1 = new Command(CommandAction.Bombard, mission.TargetHabitat);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetHabitat);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetHabitat);
          command10 = new Command(CommandAction.Blockade, mission.TargetHabitat);
          command11 = new Command(CommandAction.Escort, mission.TargetHabitat);
          command12 = new Command(CommandAction.SprintTo, mission.TargetHabitat);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetHabitat);
        }
        else if (mission.TargetCreature != null)
        {
          command2 = new Command(CommandAction.ConditionalHyperTo, mission.TargetCreature);
          command3 = new Command(CommandAction.SetParent, mission.TargetCreature);
          command4 = new Command(CommandAction.MoveTo, mission.TargetCreature);
          command5 = new Command(CommandAction.Dock, mission.TargetCreature);
          command6 = new Command(CommandAction.Undock, mission.TargetCreature);
          command7 = new Command(CommandAction.Attack, mission.TargetCreature);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetCreature);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetCreature);
          command10 = new Command(CommandAction.Blockade, mission.TargetCreature);
          command11 = new Command(CommandAction.Escort, mission.TargetCreature);
          command12 = new Command(CommandAction.SprintTo, mission.TargetCreature);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetCreature);
        }
        else
        {
          command2 = new Command(CommandAction.ConditionalHyperTo, mission.TargetShipGroup);
          command3 = new Command(CommandAction.SetParent, mission.TargetShipGroup);
          command4 = new Command(CommandAction.MoveTo, mission.TargetShipGroup);
          command5 = new Command(CommandAction.Dock, mission.TargetShipGroup);
          command6 = new Command(CommandAction.Undock, mission.TargetShipGroup);
          command7 = new Command(CommandAction.Attack, mission.TargetShipGroup);
          command8 = new Command(CommandAction.ExtractResources, mission.TargetShipGroup);
          command9 = new Command(CommandAction.ImpulseTo, mission.TargetShipGroup);
          command10 = new Command(CommandAction.Blockade, mission.TargetShipGroup);
          command11 = new Command(CommandAction.Escort, mission.TargetShipGroup);
          command12 = new Command(CommandAction.SprintTo, mission.TargetShipGroup);
          command13 = new Command(CommandAction.SelectTargetToAttack, mission.TargetShipGroup);
        }
        object lockObject;
        switch (mission.Type)
        {
          case BuiltObjectMissionType.Explore:
            Habitat targetHabitat1 = mission.TargetHabitat;
            if (targetHabitat1 != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              if (targetHabitat1.Type == HabitatType.BlackHole)
              {
                double num1 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double num2 = (double) targetHabitat1.Diameter * 0.7 + Galaxy.Rnd.NextDouble() * 500.0;
                double x = targetHabitat1.Xpos + num2 * Math.Sin(num1);
                double y = targetHabitat1.Ypos + num2 * Math.Cos(num1);
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x, y));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, x, y));
              }
              else
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, targetHabitat1));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, targetHabitat1));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, targetHabitat1));
              }
              commandQueue.Enqueue(new Command(CommandAction.ScanArea));
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
              return commandQueue;
            }
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            return commandQueue;
          case BuiltObjectMissionType.Build:
          case BuiltObjectMissionType.BuildRepair:
            if (!theThis._BuiltObject.IsShipYard)
              return (CommandQueue) null;
            double x1;
            double y1;
            if (mission.TargetBuiltObject != null)
            {
              x1 = mission.TargetBuiltObject.Xpos;
              y1 = mission.TargetBuiltObject.Ypos;
            }
            else if (mission.TargetHabitat != null)
            {
              x1 = mission.TargetHabitat.Xpos;
              y1 = mission.TargetHabitat.Ypos;
            }
            else
            {
              if (mission.TargetCreature != null)
                throw new ApplicationException("Invalid build commmand location");
              x1 = mission.TargetShipGroup == null ? (double) mission.X : throw new ApplicationException("Invalid build commmand location");
              y1 = (double) mission.Y;
            }
            if (theThis._BuiltObject.SubRole == BuiltObjectSubRole.ConstructionShip && theThis._BuiltObject.Cargo != null)
            {
              foreach (Cargo cargo in (SyncList<Cargo>) theThis._BuiltObject.Cargo)
                cargo.Reserved = 0;
            }
            CargoList cargoItems = new CargoList();
            ComponentList componentList = new ComponentList();
            CargoList cargoList1 = (CargoList) null;
            if (mission.SecondaryTargetBuiltObject != null)
            {
              if (theThis._BuiltObject.Cargo != null)
                cargoList1 = theThis._BuiltObject.Cargo.Clone();
              foreach (BuiltObjectComponent component in (SyncList<BuiltObjectComponent>) mission.SecondaryTargetBuiltObject.Components)
              {
                if (component.Status == ComponentStatus.Unbuilt)
                {
                  bool flag = true;
                  if (cargoList1 != null)
                  {
                    int index = cargoList1.IndexOf((Component) component, theThis._BuiltObject.ActualEmpire);
                    if (index >= 0 && cargoList1[index].Amount > 0)
                    {
                      flag = false;
                      --cargoList1[index].Amount;
                    }
                  }
                  if (flag)
                    componentList.Add((Component) component);
                }
              }
            }
            else if (mission.Design != null)
              componentList.AddRange((IEnumerable<Component>) mission.Design.Components.Clone());
            if (componentList.Count > 0)
            {
              foreach (Component component in (SyncList<Component>) componentList)
              {
                foreach (ComponentResource requiredResource in (SyncList<ComponentResource>) component.RequiredResources)
                {
                  Cargo cargo = new Cargo(new Resource(requiredResource.ResourceID), (int) requiredResource.Quantity, theThis._BuiltObject.ActualEmpire);
                  cargoItems.Add(cargo);
                }
              }
            }
            if ((mission.TargetHabitat == null || mission.TargetHabitat.Empire != theThis._BuiltObject.ActualEmpire) && mission.Design != null && mission.Design.Role == BuiltObjectRole.Base)
            {
              CargoList cargoList2 = Galaxy.ResolveRetrofitResourcesForBase(theThis._BuiltObject.Empire);
              for (int index = 0; index < cargoList2.Count; ++index)
                cargoItems.Add(cargoList2[index]);
            }
            CargoList cargoList3 = new CargoList();
            for (int index1 = 0; index1 < cargoItems.Count; ++index1)
            {
              Cargo cargo = cargoItems[index1];
              if (cargo.CommodityResource != null)
              {
                Resource commodityResource = cargo.CommodityResource;
                int index2 = -1;
                if (theThis._BuiltObject.Cargo != null)
                  index2 = theThis._BuiltObject.Cargo.IndexOf(commodityResource, theThis._BuiltObject.ActualEmpire);
                if (index2 >= 0)
                {
                  cargo.Amount -= theThis._BuiltObject.Cargo[index2].Amount;
                  if (cargo.Amount <= 0)
                    cargoList3.Add(cargo);
                }
              }
            }
            foreach (Cargo cargo in (SyncList<Cargo>) cargoList3)
              cargoItems.Remove(cargo);
            if (cargoItems.Count > 0)
            {
              BuiltObject spaceportWithCargo = theThis._BuiltObject.ActualEmpire.FindNearestSpaceportWithCargo(x1, y1, cargoItems);
              if (spaceportWithCargo != null)
              {
                CargoList cargoList4 = new CargoList();
                foreach (Cargo cargo1 in (SyncList<Cargo>) cargoItems)
                {
                  Resource commodityResource = cargo1.CommodityResource;
                  int index = -1;
                  if (spaceportWithCargo.Cargo != null)
                    index = spaceportWithCargo.Cargo.IndexOf(commodityResource, theThis._BuiltObject.ActualEmpire);
                  if (index >= 0)
                  {
                    int num = Math.Min(cargo1.Amount, spaceportWithCargo.Cargo[index].Available);
                    if (num > 0)
                    {
                      Cargo cargo2 = new Cargo(commodityResource, num, theThis._BuiltObject.ActualEmpire, num);
                      cargoList4.Add(cargo2);
                      spaceportWithCargo.Cargo[index].Reserved += num;
                      Contract contract = new Contract((StellarObject) spaceportWithCargo, num, (int) commodityResource.ResourceID, -1, theThis._BuiltObject.ActualEmpire.EmpireId);
                      theThis._BuiltObject.ContractsToFulfill.Add(contract);
                    }
                  }
                }
                if (cargoList4.Count > 0)
                {
                  commandQueue.Enqueue(new Command(CommandAction.ClearParent));
                  commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, spaceportWithCargo));
                  commandQueue.Enqueue(new Command(CommandAction.SetParent, spaceportWithCargo));
                  commandQueue.Enqueue(new Command(CommandAction.MoveTo, spaceportWithCargo));
                  commandQueue.Enqueue(new Command(CommandAction.Dock, spaceportWithCargo));
                  commandQueue.Enqueue(new Command(CommandAction.Refuel));
                  commandQueue.Enqueue(new Command(CommandAction.Load, cargoList4));
                  commandQueue.Enqueue(new Command(CommandAction.Undock, spaceportWithCargo));
                }
              }
            }
            if (mission.TargetBuiltObject != null || mission.TargetHabitat != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              Command command14 = command2.Clone();
              if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
              {
                command14.TargetRelativeXpos = mission.X;
                command14.TargetRelativeYpos = mission.Y;
              }
              commandQueue.Enqueue(command14);
              commandQueue.Enqueue(command3.Clone());
              if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
              {
                Command command15 = new Command(CommandAction.MoveTo);
                if (mission.TargetBuiltObject != null)
                  command15.TargetBuiltObject = mission.TargetBuiltObject;
                else if (mission.TargetHabitat != null)
                  command15.TargetHabitat = mission.TargetHabitat;
                else
                  command15.TargetShipGroup = mission.TargetShipGroup;
                command15.TargetRelativeXpos = mission.X;
                command15.TargetRelativeYpos = mission.Y;
                commandQueue.Enqueue(command15);
                Command command16 = new Command(CommandAction.ImpulseTo);
                if (mission.TargetBuiltObject != null)
                  command16.TargetBuiltObject = mission.TargetBuiltObject;
                else if (mission.TargetHabitat != null)
                  command16.TargetHabitat = mission.TargetHabitat;
                else
                  command16.TargetShipGroup = mission.TargetShipGroup;
                command16.TargetRelativeXpos = mission.X;
                command16.TargetRelativeYpos = mission.Y;
                commandQueue.Enqueue(command16);
              }
              else
              {
                Command command17 = new Command(CommandAction.MoveTo);
                if (mission.TargetBuiltObject != null)
                  command17.TargetBuiltObject = mission.TargetBuiltObject;
                else if (mission.TargetHabitat != null)
                  command17.TargetHabitat = mission.TargetHabitat;
                else if (mission.TargetCreature != null)
                  command17.TargetCreature = mission.TargetCreature;
                else
                  command17.TargetShipGroup = mission.TargetShipGroup;
                command17.TargetRelativeXpos = 0.0f;
                command17.TargetRelativeYpos = 0.0f;
                commandQueue.Enqueue(command17);
                Command command18 = new Command(CommandAction.ImpulseTo);
                if (mission.TargetBuiltObject != null)
                  command18.TargetBuiltObject = mission.TargetBuiltObject;
                else if (mission.TargetHabitat != null)
                  command18.TargetHabitat = mission.TargetHabitat;
                else if (mission.TargetCreature != null)
                  command18.TargetCreature = mission.TargetCreature;
                else
                  command18.TargetShipGroup = mission.TargetShipGroup;
                command18.TargetRelativeXpos = 0.0f;
                command18.TargetRelativeYpos = 0.0f;
                commandQueue.Enqueue(command18);
              }
            }
            else if (mission.SecondaryTargetBuiltObject != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, mission.SecondaryTargetBuiltObject));
              if (mission.SecondaryTargetBuiltObject.ParentHabitat != null)
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetBuiltObject));
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, mission.SecondaryTargetBuiltObject));
              commandQueue.Enqueue(new Command(CommandAction.ImpulseTo, mission.SecondaryTargetBuiltObject));
              if (mission.SecondaryTargetBuiltObject.ParentHabitat != null)
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetBuiltObject.ParentHabitat));
            }
            else
            {
              theThis._Galaxy.CalculateDistance(theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, (double) mission.X, (double) mission.Y);
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) mission.X, (double) mission.Y));
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, (double) mission.X, (double) mission.Y));
              commandQueue.Enqueue(new Command(CommandAction.ImpulseTo, (double) mission.X, (double) mission.Y));
            }
            if (mission.SecondaryTargetBuiltObject != null || mission.SecondaryTargetHabitat != null || mission.SecondaryTargetCreature != null || mission.SecondaryTargetShipGroup != null)
            {
              if (mission.SecondaryTargetBuiltObject != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.Build, mission.SecondaryTargetBuiltObject));
                return commandQueue;
              }
              if (mission.SecondaryTargetHabitat != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.Build, mission.SecondaryTargetHabitat));
                return commandQueue;
              }
              if (mission.SecondaryTargetCreature != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.Build, mission.SecondaryTargetCreature));
                return commandQueue;
              }
              commandQueue.Enqueue(new Command(CommandAction.Build, mission.SecondaryTargetShipGroup));
              return commandQueue;
            }
            if (mission.Design != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.Build, mission.Design));
              if ((mission.TargetBuiltObject != null || mission.TargetHabitat != null) && (mission.TargetHabitat == null || mission.TargetHabitat.Category != HabitatCategoryType.Star) && mission.TargetHabitat.Category != HabitatCategoryType.GasCloud)
                commandQueue.Enqueue(theThis.GenerateParkCommand());
              return commandQueue;
            }
            couldResolveCommands = false;
            return commandQueue;
          case BuiltObjectMissionType.Transport:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            commandQueue.Enqueue(command5.Clone());
            commandQueue.Enqueue(new Command(CommandAction.Refuel));
            if (mission.Cargo == null || mission.Cargo.Count <= 0)
            {
              if (mission.Population != null && mission.Population.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Load, mission.Population.Clone()));
            }
            else
              commandQueue.Enqueue(new Command(CommandAction.Load, mission.Cargo.Clone()));
            commandQueue.Enqueue(command6.Clone());
            if (mission.SecondaryTargetHabitat != null)
            {
              Habitat secondaryTargetHabitat = mission.SecondaryTargetHabitat;
              theThis._Galaxy.CalculateDistance(secondaryTargetHabitat.Xpos, secondaryTargetHabitat.Ypos, (double) point.X, (double) point.Y);
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, secondaryTargetHabitat));
              commandQueue.Enqueue(new Command(CommandAction.SetParent, secondaryTargetHabitat));
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, secondaryTargetHabitat));
              commandQueue.Enqueue(new Command(CommandAction.Dock, secondaryTargetHabitat));
              if (mission.Cargo != null && mission.Cargo.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Cargo.Clone()));
              else if (mission.Population != null && mission.Population.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Population.Clone()));
              commandQueue.Enqueue(new Command(CommandAction.Refuel));
              commandQueue.Enqueue(new Command(CommandAction.Undock, secondaryTargetHabitat));
              commandQueue.Enqueue(new Command(CommandAction.SetParent, secondaryTargetHabitat));
            }
            else
            {
              BuiltObject target = mission.SecondaryTargetBuiltObject != null ? mission.SecondaryTargetBuiltObject : throw new ApplicationException("Invalid mission target type");
              theThis._Galaxy.CalculateDistance(target.Xpos, target.Ypos, (double) point.X, (double) point.Y);
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, target));
              commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, target));
              commandQueue.Enqueue(new Command(CommandAction.Dock, target));
              if (mission.Cargo != null && mission.Cargo.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Cargo.Clone()));
              else if (mission.Population != null && mission.Population.Count > 0)
                commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Population.Clone()));
              commandQueue.Enqueue(new Command(CommandAction.Refuel));
              commandQueue.Enqueue(new Command(CommandAction.Undock, target));
              if (target.Role == BuiltObjectRole.Base)
                commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
            }
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            return commandQueue;
          case BuiltObjectMissionType.Patrol:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            if (mission.TargetBuiltObject == null)
            {
              Habitat habitat1 = mission.TargetHabitat != null ? mission.TargetHabitat : throw new ApplicationException("Invalid mission target type");
              if (habitat1.Category == HabitatCategoryType.Star)
              {
                int index = theThis._Galaxy.Habitats.IndexOf(habitat1) + 1;
                Habitat habitat2 = theThis._Galaxy.Habitats[index];
                for (int iterationCount = 0; Galaxy.ConditionCheckLimit(habitat2.Parent != null, 500, ref iterationCount); habitat2 = theThis._Galaxy.Habitats[index])
                {
                  if (habitat2.Owner == theThis._BuiltObject.Empire)
                    commandQueue.Enqueue(new Command(CommandAction.MoveTo, habitat2));
                  ++index;
                  if (index >= theThis._Galaxy.Habitats.Count)
                    return commandQueue;
                }
                return commandQueue;
              }
              List<Command> commandList = new List<Command>();
              double num3 = Math.PI / 3.0;
              double num4 = 0.0;
              double num5 = (double) -Galaxy.PatrolOrbitDistance;
              double num6 = 0.0 + (double) Galaxy.PatrolOrbitDistance * Math.Sin(num3);
              double num7 = 0.0 + (double) Galaxy.PatrolOrbitDistance * Math.Cos(num3);
              double num8 = 0.0 - (double) Galaxy.PatrolOrbitDistance * Math.Sin(num3);
              double num9 = num7;
              Command command19 = new Command(CommandAction.MoveTo)
              {
                TargetRelativeXpos = (float) num4,
                TargetRelativeYpos = (float) num5
              };
              if (mission.TargetBuiltObject != null)
                command19.TargetBuiltObject = mission.TargetBuiltObject;
              else if (mission.TargetHabitat != null)
                command19.TargetHabitat = mission.TargetHabitat;
              else
                command19.TargetShipGroup = mission.TargetShipGroup != null ? mission.TargetShipGroup : throw new ApplicationException("Invalid patrol target");
              commandList.Add(command19);
              Command command20 = new Command(CommandAction.MoveTo)
              {
                TargetRelativeXpos = (float) num6,
                TargetRelativeYpos = (float) num7
              };
              if (mission.TargetBuiltObject != null)
                command20.TargetBuiltObject = mission.TargetBuiltObject;
              else if (mission.TargetHabitat != null)
                command20.TargetHabitat = mission.TargetHabitat;
              else if (mission.TargetShipGroup != null)
                command20.TargetShipGroup = mission.TargetShipGroup;
              commandList.Add(command20);
              Command command21 = new Command(CommandAction.MoveTo)
              {
                TargetRelativeXpos = (float) num8,
                TargetRelativeYpos = (float) num9
              };
              if (mission.TargetBuiltObject != null)
                command21.TargetBuiltObject = mission.TargetBuiltObject;
              else if (mission.TargetHabitat != null)
                command21.TargetHabitat = mission.TargetHabitat;
              else if (mission.TargetShipGroup != null)
                command21.TargetShipGroup = mission.TargetShipGroup;
              commandList.Add(command21);
              int num10 = Galaxy.Rnd.Next(11, 14);
              for (int index = 0; index < num10; ++index)
              {
                commandQueue.Enqueue(commandList[0]);
                commandQueue.Enqueue(commandList[1]);
                commandQueue.Enqueue(commandList[2]);
              }
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
              return commandQueue;
            }
            List<Command> commandList1 = new List<Command>();
            double num11 = Math.PI / 3.0;
            double num12 = 0.0;
            double num13 = (double) -Galaxy.PatrolOrbitDistance;
            double num14 = 0.0 + (double) Galaxy.PatrolOrbitDistance * Math.Sin(num11);
            double num15 = 0.0 + (double) Galaxy.PatrolOrbitDistance * Math.Cos(num11);
            double num16 = 0.0 - (double) Galaxy.PatrolOrbitDistance * Math.Sin(num11);
            double num17 = num15;
            Command command22 = new Command(CommandAction.MoveTo)
            {
              TargetRelativeXpos = (float) num12,
              TargetRelativeYpos = (float) num13
            };
            if (mission.TargetBuiltObject == null)
            {
              if (mission.TargetHabitat != null)
                command22.TargetHabitat = mission.TargetHabitat;
              else
                command22.TargetShipGroup = mission.TargetShipGroup != null ? mission.TargetShipGroup : throw new ApplicationException("Invalid patrol target");
            }
            else
              command22.TargetBuiltObject = mission.TargetBuiltObject;
            commandList1.Add(command22);
            Command command23 = new Command(CommandAction.MoveTo)
            {
              TargetRelativeXpos = (float) num14,
              TargetRelativeYpos = (float) num15
            };
            if (mission.TargetBuiltObject != null)
              command23.TargetBuiltObject = mission.TargetBuiltObject;
            else if (mission.TargetHabitat != null)
              command23.TargetHabitat = mission.TargetHabitat;
            else if (mission.TargetShipGroup != null)
              command23.TargetShipGroup = mission.TargetShipGroup;
            commandList1.Add(command23);
            Command command24 = new Command(CommandAction.MoveTo)
            {
              TargetRelativeXpos = (float) num16,
              TargetRelativeYpos = (float) num17
            };
            if (mission.TargetBuiltObject != null)
              command24.TargetBuiltObject = mission.TargetBuiltObject;
            else if (mission.TargetHabitat != null)
              command24.TargetHabitat = mission.TargetHabitat;
            else if (mission.TargetShipGroup != null)
              command24.TargetShipGroup = mission.TargetShipGroup;
            commandList1.Add(command24);
            int num18 = Galaxy.Rnd.Next(11, 14);
            for (int index = 0; index < num18; ++index)
            {
              commandQueue.Enqueue(commandList1[0]);
              commandQueue.Enqueue(commandList1[1]);
              commandQueue.Enqueue(commandList1[2]);
            }
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            return commandQueue;
          case BuiltObjectMissionType.Escort:
            if (mission.TargetHabitat != null)
              throw new ApplicationException("Invalid escort mission target type");
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command11.Clone());
            return commandQueue;
          case BuiltObjectMissionType.Rescue:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command12.Clone());
            commandQueue.Enqueue(command13.Clone());
            return commandQueue;
          case BuiltObjectMissionType.Blockade:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            commandQueue.Enqueue(command10.Clone());
            return commandQueue;
          case BuiltObjectMissionType.Attack:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            double x2 = -1.0;
            double y2 = -1.0;
            if (mission.TargetBuiltObject != null)
            {
              BuiltObject targetBuiltObject = mission.TargetBuiltObject;
              if (targetBuiltObject.Mission != null && targetBuiltObject.Mission.CheckCommandsForHyperjumpOrConditionalJump())
              {
                x2 = targetBuiltObject.Xpos;
                y2 = targetBuiltObject.Ypos;
              }
            }
            if (x2 >= 0.0 && y2 >= 0.0)
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x2, y2));
            else
              commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command7.Clone());
            commandQueue.Enqueue(new Command(CommandAction.EvaluateThreats));
            commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            return commandQueue;
          case BuiltObjectMissionType.Escape:
            if (theThis._BuiltObject.DockedAt != null)
              commandQueue.Enqueue(new Command(CommandAction.Undock));
            double num19 = 0.0;
            double num20;
            if (theThis._BuiltObject.Attackers.Count <= 0)
            {
              if (mission.TargetBuiltObject == null && mission.TargetCreature == null)
                return commandQueue;
              StellarObject stellarObject = (StellarObject) null;
              if (mission.TargetBuiltObject != null)
                stellarObject = (StellarObject) mission.TargetBuiltObject;
              if (mission.TargetCreature != null)
                stellarObject = (StellarObject) mission.TargetCreature;
              if (stellarObject != null)
                num19 = Galaxy.DetermineAngle(theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, stellarObject.Xpos, stellarObject.Ypos);
              num20 = num19 + Math.PI;
            }
            else
            {
              for (int index = 0; index < theThis._BuiltObject.Attackers.Count; ++index)
                num19 += Galaxy.DetermineAngle(theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, theThis._BuiltObject.Attackers[index].Xpos, theThis._BuiltObject.Attackers[index].Ypos);
              num20 = num19 / (double) theThis._BuiltObject.Attackers.Count + Math.PI;
            }
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            bool flag1 = true;
            double num21 = (double) Galaxy.EscapeHyperDistance;
            if (theThis._BuiltObject.Empire != null && theThis._BuiltObject.Empire.Research != null)
            {
              Component latestComponent = theThis._BuiltObject.Empire.Research.GetLatestComponent(ComponentType.HyperDrive);
              if ((latestComponent == null || latestComponent.Value1 < 3000) && theThis._BuiltObject.WarpSpeed <= 3000)
              {
                if (theThis._BuiltObject.WarpSpeed <= 0)
                {
                  num21 = 600.0;
                }
                else
                {
                  num21 = 20000.0;
                  BuiltObject nearestSpacePort = theThis._Galaxy.FastFindNearestSpacePort(theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, theThis._BuiltObject.ActualEmpire);
                  if (nearestSpacePort != null && nearestSpacePort.Attackers.Count <= 0 && nearestSpacePort.NearestSystemStar == theThis._BuiltObject.NearestSystemStar && theThis._Galaxy.CalculateDistance(nearestSpacePort.Xpos, nearestSpacePort.Ypos, theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos) > 3000.0)
                  {
                    commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, nearestSpacePort));
                    commandQueue.Enqueue(new Command(CommandAction.SetParent, nearestSpacePort));
                    commandQueue.Enqueue(theThis.GenerateParkCommand());
                    flag1 = false;
                  }
                }
              }
              else if (theThis._BuiltObject.WarpSpeed <= 0)
                num21 = 600.0;
            }
            if (flag1)
            {
              double num22 = num21 * 0.75 + num21 * (Galaxy.Rnd.NextDouble() / 2.0);
              double x3 = theThis._BuiltObject.Xpos + Math.Cos(num20) * num22;
              double y3 = theThis._BuiltObject.Ypos + Math.Sin(num20) * num22;
              theThis.EnsureCoordsInGalaxy(ref x3, ref y3);
              if (num21 > 2000.0)
                commandQueue.Enqueue(new Command(CommandAction.HyperTo, x3, y3));
              else
                commandQueue.Enqueue(new Command(CommandAction.SprintTo, x3, y3));
              commandQueue.Enqueue(new Command(CommandAction.ClearAttackers));
              if (theThis._BuiltObject.Role == BuiltObjectRole.Freight || theThis._BuiltObject.Role == BuiltObjectRole.Colony)
              {
                if (theThis._BuiltObject.WarpSpeed > 0)
                {
                  if (theThis._BuiltObject.ActualEmpire != null)
                  {
                    Habitat colonyNotInSystem = theThis._Galaxy.FastFindNearestColonyNotInSystem((double) (int) theThis._BuiltObject.Xpos, (double) (int) theThis._BuiltObject.Ypos, theThis._BuiltObject.ActualEmpire, 0, theThis._BuiltObject.NearestSystemStar);
                    if (colonyNotInSystem != null)
                    {
                      double distance = theThis._Galaxy.CalculateDistance(colonyNotInSystem.Xpos, colonyNotInSystem.Ypos, theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos);
                      if (distance <= (double) Galaxy.MaxSolarSystemSize * 2.1 || distance >= (double) Galaxy.SectorSize * 3.0 || !theThis._BuiltObject.WithinFuelRange(colonyNotInSystem.Xpos, colonyNotInSystem.Ypos, 0.0))
                        return commandQueue;
                      int num23 = 0;
                      if (theThis._BuiltObject.Empire != null && theThis._BuiltObject.Empire.SystemVisibility != null && theThis._BuiltObject.Empire.SystemVisibility.Count > colonyNotInSystem.SystemIndex)
                      {
                        SystemVisibility systemVisibility = theThis._BuiltObject.Empire.SystemVisibility[colonyNotInSystem.SystemIndex];
                        if (systemVisibility != null && systemVisibility.Threats != null)
                          num23 = systemVisibility.Threats.CalculateAttackingFirepowerNearEmpireTargets(theThis._BuiltObject.Empire);
                      }
                      if (num23 <= 0)
                      {
                        commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, colonyNotInSystem));
                        commandQueue.Enqueue(new Command(CommandAction.SetParent, colonyNotInSystem));
                        commandQueue.Enqueue(theThis.GenerateParkCommand());
                      }
                    }
                    return commandQueue;
                  }
                  if (theThis._BuiltObject.Empire == theThis._Galaxy.IndependentEmpire)
                  {
                    double angle = (double) theThis._Galaxy.SelectRandomHeading();
                    double distance = 2000000.0 + Galaxy.Rnd.NextDouble() * 2000000.0;
                    double x4;
                    double y4;
                    theThis._BuiltObject.Empire.ObtainCoordinatesFromPoint(angle, theThis._BuiltObject.Xpos, theThis._BuiltObject.Ypos, distance, out x4, out y4);
                    Habitat nearestColony = theThis._Galaxy.FindNearestColony(x4, y4, (Empire) null, 0, true);
                    if (nearestColony != null)
                    {
                      commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, nearestColony));
                      commandQueue.Enqueue(new Command(CommandAction.SetParent, nearestColony));
                      commandQueue.Enqueue(theThis.GenerateParkCommand());
                    }
                  }
                }
                return commandQueue;
              }
              commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            }
            return commandQueue;
          case BuiltObjectMissionType.Retire:
            if (theThis._BuiltObject.Role != BuiltObjectRole.Base)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(command2.Clone());
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(command4.Clone());
              commandQueue.Enqueue(command5.Clone());
              commandQueue.Enqueue(new Command(CommandAction.Scrap));
              return commandQueue;
            }
            commandQueue.Enqueue(new Command(CommandAction.Scrap));
            return commandQueue;
          case BuiltObjectMissionType.Retrofit:
            if (mission.Design == null && !specifiedAsFleetMission)
            {
              couldResolveCommands = false;
              lock (lockObject = theThis._LockObject)
              {
                commandQueue.Clear();
                return commandQueue;
              }
            }
            else
            {
              if (theThis._BuiltObject.Role == BuiltObjectRole.Base)
              {
                commandQueue.Enqueue(new Command(CommandAction.Retrofit));
                return commandQueue;
              }
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(command2.Clone());
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(command4.Clone());
              commandQueue.Enqueue(command5.Clone());
              commandQueue.Enqueue(new Command(CommandAction.Retrofit, mission.Design));
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(theThis.GenerateParkCommand());
              return commandQueue;
            }
          case BuiltObjectMissionType.Colonize:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            commandQueue.Enqueue(command9.Clone());
            commandQueue.Enqueue(new Command(CommandAction.Colonize, mission.TargetHabitat));
            return commandQueue;
          case BuiltObjectMissionType.Waypoint:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            if (mission.TargetBuiltObject == null && mission.TargetHabitat == null && mission.TargetShipGroup == null)
            {
              if ((double) mission.X > 0.0 && (double) mission.Y > 0.0)
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) mission.X, (double) mission.Y));
              }
              else
              {
                couldResolveCommands = false;
                lock (lockObject = theThis._LockObject)
                {
                  commandQueue.Clear();
                  return commandQueue;
                }
              }
            }
            else
              commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            return commandQueue;
          case BuiltObjectMissionType.Hold:
            commandQueue.Enqueue(new Command(CommandAction.Hold, theThis.StarDate));
            return commandQueue;
          case BuiltObjectMissionType.WaitAndAttack:
          case BuiltObjectMissionType.WaitAndBombard:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            if (mission.SecondaryTargetBuiltObject == null)
            {
              if (mission.SecondaryTargetHabitat != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(new Command(CommandAction.Dock, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(new Command(CommandAction.Refuel));
                commandQueue.Enqueue(new Command(CommandAction.Undock, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetHabitat));
                commandQueue.Enqueue(theThis.GenerateParkCommand());
              }
              else if (mission.SecondaryTargetCreature != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, mission.SecondaryTargetCreature));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetCreature));
              }
              else if (mission.SecondaryTargetShipGroup != null)
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, mission.SecondaryTargetShipGroup));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetShipGroup));
              }
              else
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) mission.X, (double) mission.Y));
                double x5;
                double y5;
                theThis._Galaxy.SelectRelativeParkingPoint(200.0, out x5, out y5);
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, (double) mission.X + x5, (double) mission.Y + y5));
              }
            }
            else
            {
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, mission.SecondaryTargetBuiltObject));
              commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetBuiltObject));
              if (mission.SecondaryTargetBuiltObject.IsRefuellingDepot)
              {
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, mission.SecondaryTargetBuiltObject));
                commandQueue.Enqueue(new Command(CommandAction.Dock, mission.SecondaryTargetBuiltObject));
                commandQueue.Enqueue(new Command(CommandAction.Refuel));
                commandQueue.Enqueue(new Command(CommandAction.Undock, mission.SecondaryTargetBuiltObject));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, mission.SecondaryTargetBuiltObject));
              }
              commandQueue.Enqueue(theThis.GenerateParkCommand());
            }
            commandQueue.Enqueue(new Command(CommandAction.HoldSyncFleet, theThis.StarDate));
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            if (mission.TargetShipGroup != null)
            {
              Point actualFleetLocation = mission.TargetShipGroup.DetermineActualFleetLocation();
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) actualFleetLocation.X, (double) actualFleetLocation.Y));
            }
            else
              commandQueue.Enqueue(command2.Clone());
            if (mission.Type == BuiltObjectMissionType.Attack || mission.Type == BuiltObjectMissionType.WaitAndAttack)
              commandQueue.Enqueue(command7.Clone());
            else if (mission.Type == BuiltObjectMissionType.Bombard || mission.Type == BuiltObjectMissionType.WaitAndBombard)
              commandQueue.Enqueue(command1.Clone());
            commandQueue.Enqueue(new Command(CommandAction.EvaluateThreats));
            commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            break;
          case BuiltObjectMissionType.MoveAndWait:
            if (mission.TargetBuiltObject == null && mission.TargetHabitat == null && mission.TargetShipGroup == null && mission.TargetCreature == null && (double) mission.X < 0.0 && (double) mission.Y < 0.0)
              throw new ApplicationException("Mission target cannot be null");
            if ((double) mission.X > 0.0 && (double) mission.Y > 0.0)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) mission.X, (double) mission.Y));
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, (double) mission.X, (double) mission.Y));
              commandQueue.Enqueue(new Command(CommandAction.Hold, theThis.StarDate));
              return commandQueue;
            }
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            if (mission.TargetBuiltObject != null && mission.TargetBuiltObject.Role != BuiltObjectRole.Base)
            {
              commandQueue.Enqueue(new Command(CommandAction.MoveTo, mission.TargetBuiltObject));
            }
            else
            {
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(theThis.GenerateParkCommand());
            }
            commandQueue.Enqueue(new Command(CommandAction.Hold, theThis.StarDate));
            return commandQueue;
          case BuiltObjectMissionType.Refuel:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            commandQueue.Enqueue(command5.Clone());
            commandQueue.Enqueue(new Command(CommandAction.Refuel));
            commandQueue.Enqueue(command6.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            return commandQueue;
          case BuiltObjectMissionType.ExtractResources:
            if (mission.TargetHabitat != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(command2.Clone());
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(command4.Clone());
              commandQueue.Enqueue(command9.Clone());
              commandQueue.Enqueue(command8.Clone());
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              BuiltObject target = (BuiltObject) null;
              double num24 = 536870911.0;
              if (theThis._BuiltObject != null)
              {
                Empire actualEmpire = theThis._BuiltObject.ActualEmpire;
                if (actualEmpire != null && actualEmpire.SpacePorts != null)
                {
                  foreach (BuiltObject spacePort in (SyncList<BuiltObject>) actualEmpire.SpacePorts)
                  {
                    if (spacePort != null && spacePort.IsSpacePort)
                    {
                      double distance = theThis._Galaxy.CalculateDistance(spacePort.Xpos, spacePort.Ypos, (double) point.X, (double) point.Y);
                      if (distance < num24)
                      {
                        target = spacePort;
                        num24 = distance;
                      }
                    }
                  }
                }
              }
              if (target == null)
              {
                couldResolveCommands = false;
                lock (lockObject = theThis._LockObject)
                {
                  commandQueue.Clear();
                  return commandQueue;
                }
              }
              else
              {
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, target));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, target));
                commandQueue.Enqueue(new Command(CommandAction.Dock, target));
                commandQueue.Enqueue(new Command(CommandAction.Unload));
                commandQueue.Enqueue(new Command(CommandAction.Refuel));
                commandQueue.Enqueue(new Command(CommandAction.Undock, target));
                commandQueue.Enqueue(new Command(CommandAction.SetParent, target));
                commandQueue.Enqueue(theThis.GenerateParkCommand());
              }
            }
            return commandQueue;
          case BuiltObjectMissionType.LoadTroops:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            commandQueue.Enqueue(command5.Clone());
            commandQueue.Enqueue(new Command(CommandAction.Load, mission.Troops));
            commandQueue.Enqueue(new Command(CommandAction.Refuel));
            commandQueue.Enqueue(command6.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            return commandQueue;
          case BuiltObjectMissionType.UnloadTroops:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(command4.Clone());
            commandQueue.Enqueue(command5.Clone());
            commandQueue.Enqueue(new Command(CommandAction.Unload, mission.Troops));
            commandQueue.Enqueue(new Command(CommandAction.Refuel));
            commandQueue.Enqueue(command6.Clone());
            commandQueue.Enqueue(command3.Clone());
            commandQueue.Enqueue(theThis.GenerateParkCommand());
            return commandQueue;
          case BuiltObjectMissionType.Deploy:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command3.Clone());
            Command command25 = command4.Clone();
            if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
            {
              command25.TargetRelativeXpos = mission.X;
              command25.TargetRelativeYpos = mission.Y;
            }
            commandQueue.Enqueue(command25);
            commandQueue.Enqueue(new Command(CommandAction.Deploy));
            return commandQueue;
          case BuiltObjectMissionType.Undeploy:
            commandQueue.Enqueue(new Command(CommandAction.Undeploy));
            return commandQueue;
          case BuiltObjectMissionType.Repair:
            if (theThis._BuiltObject.Role != BuiltObjectRole.Base)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              commandQueue.Enqueue(command2.Clone());
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(command4.Clone());
              commandQueue.Enqueue(command5.Clone());
              commandQueue.Enqueue(new Command(CommandAction.Repair));
              commandQueue.Enqueue(command3.Clone());
              commandQueue.Enqueue(theThis.GenerateParkCommand());
              return commandQueue;
            }
            commandQueue.Enqueue(new Command(CommandAction.Repair));
            return commandQueue;
          case BuiltObjectMissionType.Move:
            if (mission.TargetBuiltObject != null || mission.TargetHabitat != null || mission.TargetCreature != null || mission.TargetShipGroup != null)
            {
              commandQueue.Enqueue(new Command(CommandAction.ClearParent));
              if (mission.TargetHabitat != null && mission.TargetHabitat.Type == HabitatType.BlackHole)
              {
                Habitat targetHabitat2 = mission.TargetHabitat;
                if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
                {
                  double x6 = targetHabitat2.Xpos + (double) mission.X;
                  double y6 = targetHabitat2.Ypos + (double) mission.Y;
                  commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x6, y6));
                  commandQueue.Enqueue(new Command(CommandAction.MoveTo, x6, y6));
                  return commandQueue;
                }
                double num25 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double num26 = (double) targetHabitat2.Diameter * 0.7 + Galaxy.Rnd.NextDouble() * 500.0;
                double x7 = targetHabitat2.Xpos + num26 * Math.Sin(num25);
                double y7 = targetHabitat2.Ypos + num26 * Math.Cos(num25);
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x7, y7));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, x7, y7));
                return commandQueue;
              }
              if (allowReprocessing && mission.TargetHabitat != null && mission.TargetHabitat.Category == HabitatCategoryType.Star)
              {
                Habitat targetHabitat3 = mission.TargetHabitat;
                if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
                {
                  double x8 = targetHabitat3.Xpos + (double) mission.X;
                  double y8 = targetHabitat3.Ypos + (double) mission.Y;
                  commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x8, y8));
                  commandQueue.Enqueue(new Command(CommandAction.MoveTo, x8, y8));
                  return commandQueue;
                }
                double num27 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                int num28 = (int) targetHabitat3.Diameter;
                if (targetHabitat3.Type == HabitatType.SuperNova)
                  num28 = (int) targetHabitat3.Diameter / 50;
                double num29 = (double) num28 * 0.63 + Galaxy.Rnd.NextDouble() * 300.0;
                double x9 = targetHabitat3.Xpos + num29 * Math.Sin(num27);
                double y9 = targetHabitat3.Ypos + num29 * Math.Cos(num27);
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x9, y9));
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, x9, y9));
                return commandQueue;
              }
              commandQueue.Enqueue(command2.Clone());
              if (mission.TargetBuiltObject != null && mission.TargetBuiltObject.Role != BuiltObjectRole.Base)
              {
                commandQueue.Enqueue(new Command(CommandAction.MoveTo, mission.TargetBuiltObject));
                return commandQueue;
              }
              commandQueue.Enqueue(command3.Clone());
              Command command26 = command4.Clone();
              if ((double) mission.X > -2000000000.0 && (double) mission.Y > -2000000000.0)
              {
                command26.TargetRelativeXpos = mission.X;
                command26.TargetRelativeYpos = mission.Y;
              }
              commandQueue.Enqueue(command26);
              return commandQueue;
            }
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, (double) point.X, (double) point.Y));
            commandQueue.Enqueue(new Command(CommandAction.MoveTo, (double) point.X, (double) point.Y));
            return commandQueue;
          case BuiltObjectMissionType.Bombard:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            commandQueue.Enqueue(command2.Clone());
            commandQueue.Enqueue(command1.Clone());
            commandQueue.Enqueue(new Command(CommandAction.EvaluateThreats));
            commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            return commandQueue;
          case BuiltObjectMissionType.Capture:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            double x10 = -1.0;
            double y10 = -1.0;
            if (mission.TargetBuiltObject != null)
            {
              BuiltObject targetBuiltObject = mission.TargetBuiltObject;
              if (targetBuiltObject.Mission != null && targetBuiltObject.Mission.CheckCommandsForHyperjumpOrConditionalJump())
              {
                x10 = targetBuiltObject.Xpos;
                y10 = targetBuiltObject.Ypos;
              }
              if (x10 >= 0.0 && y10 >= 0.0)
                commandQueue.Enqueue(new Command(CommandAction.ConditionalHyperTo, x10, y10));
              else
                commandQueue.Enqueue(command2.Clone());
              commandQueue.Enqueue(new Command(CommandAction.Capture, targetBuiltObject));
              commandQueue.Enqueue(new Command(CommandAction.EvaluateThreats));
              commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            }
            return commandQueue;
          case BuiltObjectMissionType.Reinforce:
            return commandQueue;
          case BuiltObjectMissionType.Raid:
            commandQueue.Enqueue(new Command(CommandAction.ClearParent));
            StellarObject target1 = (StellarObject) null;
            if (mission.TargetBuiltObject == null)
            {
              if (mission.TargetHabitat != null && mission.TargetHabitat.Population != null && mission.TargetHabitat.Population.Count > 0 && mission.TargetHabitat.Empire != theThis._BuiltObject.Empire)
                target1 = (StellarObject) mission.TargetHabitat;
            }
            else if (mission.TargetBuiltObject.Role == BuiltObjectRole.Base && mission.TargetBuiltObject.Empire != theThis._BuiltObject.Empire)
              target1 = (StellarObject) mission.TargetBuiltObject;
            if (target1 != null)
            {
              commandQueue.Enqueue(command2.Clone());
              if (target1 is BuiltObject)
                commandQueue.Enqueue(new Command(CommandAction.Raid, (BuiltObject) target1));
              else if (target1 is Habitat)
                commandQueue.Enqueue(new Command(CommandAction.Raid, (Habitat) target1));
              commandQueue.Enqueue(new Command(CommandAction.EvaluateThreats));
              commandQueue.Enqueue(new Command(CommandAction.ReassignMission));
            }
            return commandQueue;
          default:
            return commandQueue;
        }
      }
      catch (Exception ex)
      {
        if (BaconBuiltObject.myMain != null)
          BaconBuiltObject.myMain._Game.Galaxy.Pause();
        return commandQueue;
      }
      return commandQueue;
    }
  }
}
