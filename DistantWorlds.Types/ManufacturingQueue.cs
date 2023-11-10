// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ManufacturingQueue
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ManufacturingQueue
  {
    [NonSerialized]
    private object _LockObject = new object();
    private Galaxy _Galaxy;
    private BuiltObject _ParentBuiltObject;
    private Habitat _ParentHabitat;
    private ManufacturerList _ManufacturerList;
    private ComponentList _ComponentWaitQueue;
    private DateTime _LastProcessed;
    [OptionalField]
    private DateTime _LastProcessedLong = DateTime.MinValue;
    [OptionalField]
    private DateTime _LastResourceShortageNotification = DateTime.MinValue;
    private int _SlotsAvailableWeapons = -1;
    private int _SlotsAvailableHighTech = -1;
    private int _SlotsAvailableEnergy = -1;
    [OptionalField]
    public ResourceDatePairList DeficientResources = new ResourceDatePairList();

    public ManufacturingQueue(Habitat habitat, Galaxy galaxy)
    {
      this._Galaxy = galaxy;
      this._ParentBuiltObject = (BuiltObject) null;
      this._ParentHabitat = habitat;
      this.Redefine(habitat);
      this._LastProcessed = this._Galaxy.CurrentDateTime;
    }

    public ManufacturingQueue(BuiltObject builtObject, Galaxy galaxy)
    {
      this._Galaxy = galaxy;
      this._ParentBuiltObject = builtObject;
      this._ParentHabitat = (Habitat) null;
      this.Redefine(builtObject);
      this._LastProcessed = this._Galaxy.CurrentDateTime;
    }

    private void UpdateSlotAvailability()
    {
      this._SlotsAvailableEnergy = 0;
      this._SlotsAvailableWeapons = 0;
      this._SlotsAvailableHighTech = 0;
      foreach (Manufacturer manufacturer in (SyncList<Manufacturer>) this._ManufacturerList)
      {
        if (manufacturer.Component == null)
        {
          switch (manufacturer.Industry)
          {
            case IndustryType.Weapon:
              ++this._SlotsAvailableWeapons;
              continue;
            case IndustryType.Energy:
              ++this._SlotsAvailableEnergy;
              continue;
            case IndustryType.HighTech:
              ++this._SlotsAvailableHighTech;
              continue;
            default:
              continue;
          }
        }
      }
    }

    public void Redefine(Habitat habitat)
    {
      if (this._ManufacturerList == null)
        this._ManufacturerList = new ManufacturerList();
      if (this._ComponentWaitQueue == null)
        this._ComponentWaitQueue = new ComponentList();
      this._ManufacturerList.Add(new Manufacturer(-1, new BuiltObjectComponent(62, ComponentStatus.Normal), IndustryType.Weapon, 24000));
      this._ManufacturerList.Add(new Manufacturer(-1, new BuiltObjectComponent(63, ComponentStatus.Normal), IndustryType.Energy, 24000));
      this._ManufacturerList.Add(new Manufacturer(-1, new BuiltObjectComponent(64, ComponentStatus.Normal), IndustryType.HighTech, 24000));
      this.UpdateSlotAvailability();
    }

    public bool Redefine(BuiltObject builtObject) => this.Redefine(builtObject, false);

    public bool Redefine(BuiltObject builtObject, bool forceSingleManufacturerOfEachType)
    {
      if (this._ManufacturerList == null)
        this._ManufacturerList = new ManufacturerList();
      if (this._ComponentWaitQueue == null)
        this._ComponentWaitQueue = new ComponentList();
      bool flag = false;
      for (int index = 0; index < this._ParentBuiltObject.Components.Count; ++index)
      {
        BuiltObjectComponent component = this._ParentBuiltObject.Components[index];
        if (component.Category == ComponentCategoryType.Manufacturer)
        {
          int byComponentIndex = this._ManufacturerList.FindManufacturerByComponentIndex(index);
          if (component.Status == ComponentStatus.Damaged || component.Status == ComponentStatus.Unbuilt)
          {
            if (byComponentIndex >= 0)
            {
              if (this._ManufacturerList[byComponentIndex].Component != null)
              {
                this._ComponentWaitQueue.Add(this._ManufacturerList[byComponentIndex].Component);
                this._ManufacturerList[byComponentIndex].Component = (Component) null;
              }
              this._ManufacturerList.RemoveAt(byComponentIndex);
            }
          }
          else
          {
            flag = true;
            if (byComponentIndex < 0)
            {
              IndustryType industry = IndustryType.Energy;
              switch (component.Type)
              {
                case ComponentType.ManufacturerWeaponsPlant:
                  industry = IndustryType.Weapon;
                  break;
                case ComponentType.ManufacturerEnergyPlant:
                  industry = IndustryType.Energy;
                  break;
                case ComponentType.ManufacturerHighTechPlant:
                  industry = IndustryType.HighTech;
                  break;
              }
              this._ManufacturerList.Add(new Manufacturer(index, component, industry, component.Value1));
            }
          }
        }
      }
      if (!flag && forceSingleManufacturerOfEachType)
      {
        Manufacturer manufacturer1 = new Manufacturer(-1, new BuiltObjectComponent(62, ComponentStatus.Normal), IndustryType.Weapon, 5000);
        Manufacturer manufacturer2 = new Manufacturer(-1, new BuiltObjectComponent(63, ComponentStatus.Normal), IndustryType.Energy, 5000);
        Manufacturer manufacturer3 = new Manufacturer(-1, new BuiltObjectComponent(64, ComponentStatus.Normal), IndustryType.HighTech, 5000);
        this._ManufacturerList.Add(manufacturer1);
        this._ManufacturerList.Add(manufacturer2);
        this._ManufacturerList.Add(manufacturer3);
        flag = true;
      }
      this.UpdateSlotAvailability();
      if (!flag)
      {
        this._ManufacturerList = (ManufacturerList) null;
        this._ComponentWaitQueue = (ComponentList) null;
      }
      return flag;
    }

    private void ProcessWaitQueue(CargoList parentCargo, Empire empire, long starDate)
    {
      ComponentList componentList = new ComponentList();
      if (this._ComponentWaitQueue.Count > 0 && parentCargo != null && (this._SlotsAvailableEnergy > 0 || this._SlotsAvailableHighTech > 0 || this._SlotsAvailableWeapons > 0))
      {
        bool[] flagArray = new bool[Galaxy.ComponentDefinitionsStatic.Length];
        for (int index = 0; index < flagArray.Length; ++index)
          flagArray[index] = true;
        int[] resourceAmounts = new int[this._Galaxy.ResourceSystem.Resources.Count];
        for (int index = 0; index < resourceAmounts.Length; ++index)
          resourceAmounts[index] = -1;
        for (int index = 0; index < this._ComponentWaitQueue.Count; ++index)
        {
          Component componentWait = this._ComponentWaitQueue[index];
          if (componentWait != null && flagArray[componentWait.ComponentID])
          {
            int num = 0;
            switch (componentWait.Industry)
            {
              case IndustryType.Weapon:
                num = this._SlotsAvailableWeapons;
                break;
              case IndustryType.Energy:
                num = this._SlotsAvailableEnergy;
                break;
              case IndustryType.HighTech:
                num = this._SlotsAvailableHighTech;
                break;
            }
            if (num > 0)
            {
              ComponentResourceList manufacturingResources = (ComponentResourceList) null;
              byte deficientResourceId = byte.MaxValue;
              if (parentCargo.GetResourcesForManufacturing(componentWait, empire, out manufacturingResources, out deficientResourceId, ref resourceAmounts))
              {
                this.DeficientResources.ClearResources(manufacturingResources);
                if (this._ManufacturerList.AddComponentToManufacture(componentWait))
                {
                  foreach (ComponentResource componentResource in (SyncList<ComponentResource>) manufacturingResources)
                  {
                    if (componentResource != null)
                    {
                      Resource resource = new Resource(componentResource.ResourceID);
                      Cargo cargo = parentCargo.GetCargo(resource, empire);
                      if (cargo != null)
                      {
                        cargo.Amount -= (int) componentResource.Quantity;
                        cargo.Reserved -= (int) componentResource.Quantity;
                        if (cargo.Amount <= 0 && cargo.Reserved <= 0)
                          parentCargo.Remove(cargo);
                      }
                    }
                  }
                  componentList.Add(componentWait);
                  switch (componentWait.Industry)
                  {
                    case IndustryType.Weapon:
                      --this._SlotsAvailableWeapons;
                      continue;
                    case IndustryType.Energy:
                      --this._SlotsAvailableEnergy;
                      continue;
                    case IndustryType.HighTech:
                      --this._SlotsAvailableHighTech;
                      continue;
                    default:
                      continue;
                  }
                }
              }
              else if (deficientResourceId != byte.MaxValue)
              {
                flagArray[componentWait.ComponentID] = false;
                this.DeficientResources.CheckAddResource(deficientResourceId, starDate);
              }
            }
            else
            {
              flagArray[componentWait.ComponentID] = false;
              if (this._SlotsAvailableEnergy <= 0 && this._SlotsAvailableHighTech <= 0 && this._SlotsAvailableWeapons <= 0)
                break;
            }
          }
        }
        foreach (Component component in (SyncList<Component>) componentList)
          this._ComponentWaitQueue.Remove(component);
      }
      if (this._ComponentWaitQueue.Count > 0)
        return;
      this.DeficientResources.Clear();
    }

    private void ProcessManufacturing(
      TimeSpan timePassed,
      long starDate,
      CargoList parentCargo,
      Empire parentEmpire,
      ref ComponentList completedComponents)
    {
      int num = Galaxy.Rnd.Next(0, this._ManufacturerList.Count);
      for (int index = num; index < this._ManufacturerList.Count; ++index)
        this.ProcessSingleManufacturer(this._ManufacturerList[index], timePassed, starDate, parentCargo, parentEmpire, ref completedComponents);
      for (int index = 0; index < num; ++index)
        this.ProcessSingleManufacturer(this._ManufacturerList[index], timePassed, starDate, parentCargo, parentEmpire, ref completedComponents);
    }

    private void ProcessSingleManufacturer(
      Manufacturer manufacturer,
      TimeSpan timePassed,
      long starDate,
      CargoList parentCargo,
      Empire parentEmpire,
      ref ComponentList completedComponents)
    {
      if (manufacturer.Component != null)
      {
        double num = timePassed.TotalMilliseconds / 1000.0 * ((double) manufacturer.ManufacturingSpeed / 1000.0);
        manufacturer.Progress += (float) num;
        int iterationCount = 0;
        while (Galaxy.ConditionCheckLimit(manufacturer.Component != null && (double) manufacturer.Progress >= (double) manufacturer.Component.Size, 1000, ref iterationCount))
        {
          completedComponents.Add(manufacturer.Component);
          Cargo cargo = new Cargo(manufacturer.Component, 1, parentEmpire, 1);
          parentCargo.Add(cargo);
          IndustryType industry = manufacturer.Component.Industry;
          manufacturer.Progress -= (float) manufacturer.Component.Size;
          manufacturer.Component = (Component) null;
          switch (industry)
          {
            case IndustryType.Weapon:
              ++this._SlotsAvailableWeapons;
              break;
            case IndustryType.Energy:
              ++this._SlotsAvailableEnergy;
              break;
            case IndustryType.HighTech:
              ++this._SlotsAvailableHighTech;
              break;
          }
          this.ProcessWaitQueue(parentCargo, parentEmpire, starDate);
        }
      }
      else
        manufacturer.Progress = 0.0f;
    }

    public ComponentList DoManufacturing(Galaxy galaxy, DateTime tempNow, long starDate)
    {
      if (this._LockObject == null)
        this._LockObject = new object();
      lock (this._LockObject)
      {
        ComponentList completedComponents = new ComponentList();
        TimeSpan timePassed = tempNow.Subtract(this._LastProcessed);
        CargoList cargo;
        Empire empire;
        if (this._ParentBuiltObject != null)
        {
          cargo = this._ParentBuiltObject.Cargo;
          empire = this._ParentBuiltObject.Empire;
        }
        else
        {
          cargo = this._ParentHabitat.Cargo;
          empire = this._ParentHabitat.Empire;
        }
        if (cargo != null)
        {
          this.ProcessWaitQueue(cargo, empire, starDate);
          this.ProcessManufacturing(timePassed, starDate, cargo, empire, ref completedComponents);
          this.ProcessWaitQueue(cargo, empire, starDate);
        }
        if (tempNow.Subtract(this._LastProcessedLong).TotalSeconds > 120.0)
        {
          this.NotifyResourceShortages(starDate, tempNow);
          this._LastProcessedLong = tempNow;
        }
        this._LastProcessed = tempNow;
        return completedComponents;
      }
    }

    private void NotifyResourceShortages(long starDate, DateTime date)
    {
      if (date.Subtract(this._LastResourceShortageNotification).TotalSeconds <= 600.0)
        return;
      ResourceDatePairList resourcesOlderThanAge = this.DeficientResources.GetResourcesOlderThanAge(starDate, (long) ((double) Galaxy.RealSecondsInGalacticYear * 1000.0 * 0.6));
      if (resourcesOlderThanAge.Count <= 0)
        return;
      Empire recipientEmpire = (Empire) null;
      StellarObject subject = (StellarObject) null;
      if (this._ParentBuiltObject != null && this._ParentBuiltObject.ActualEmpire != null && this._ParentBuiltObject.ActualEmpire != this._Galaxy.IndependentEmpire)
      {
        recipientEmpire = this._ParentBuiltObject.ActualEmpire;
        subject = (StellarObject) this._ParentBuiltObject;
      }
      else if (this._ParentHabitat != null && this._ParentHabitat.Empire != null && this._ParentHabitat.Empire != this._Galaxy.IndependentEmpire)
      {
        recipientEmpire = this._ParentHabitat.Empire;
        subject = (StellarObject) this._ParentHabitat;
      }
      if (recipientEmpire == null || subject == null)
        return;
      string empty = string.Empty;
      for (int index = 0; index < resourcesOlderThanAge.Count; ++index)
      {
        if (index > 0)
          empty += ", ";
        empty += new Resource(resourcesOlderThanAge[index].ResourceId).Name;
      }
      string description = string.Format(TextResolver.GetText("Construction Resource Shortage Message"), (object) subject.Name, (object) empty);
      recipientEmpire.SendMessageToEmpire(recipientEmpire, EmpireMessageType.ConstructionResourceShortage, (object) subject, description);
      this._LastResourceShortageNotification = date;
    }

    public ManufacturerList Manufacturers => this._ManufacturerList;

    public ComponentList ComponentWaitQueue => this._ComponentWaitQueue;

    public bool AddComponentToManufacture(Component component)
    {
      if (!this._ManufacturerList.CanBuildComponent(component))
        return false;
      this._ComponentWaitQueue.Add(component);
      return true;
    }

    public void Clear()
    {
      this._ComponentWaitQueue.Clear();
      CargoList cargo1;
      Empire empire;
      if (this._ParentBuiltObject != null)
      {
        cargo1 = this._ParentBuiltObject.Cargo;
        empire = this._ParentBuiltObject.Empire;
      }
      else
      {
        cargo1 = this._ParentHabitat.Cargo;
        empire = this._ParentHabitat.Empire;
      }
      foreach (Manufacturer manufacturer in (SyncList<Manufacturer>) this._ManufacturerList)
      {
        if (manufacturer.Component != null)
        {
          Cargo cargo2 = new Cargo(manufacturer.Component, 1, empire, 1);
          cargo1.Add(cargo2);
        }
        manufacturer.Progress = 0.0f;
        manufacturer.Component = (Component) null;
      }
      this.DeficientResources.Clear();
      this.UpdateSlotAvailability();
    }
  }
}
