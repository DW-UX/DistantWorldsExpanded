// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CargoList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CargoList : SyncList<Cargo>, ISerializable
  {
    private List<short[]> _EmpireIndexers = new List<short[]>();
    private List<int> _EmpireIds = new List<int>();
    private byte[] _ItemExists = new byte[73];

    public bool GetExists(Resource resource)
    {
      int index = (int) resource.ResourceID / 8;
      switch ((int) resource.ResourceID % 8)
      {
        case 0:
          return ((int) this._ItemExists[index] & 1) != 0;
        case 1:
          return ((int) this._ItemExists[index] & 2) != 0;
        case 2:
          return ((int) this._ItemExists[index] & 4) != 0;
        case 3:
          return ((int) this._ItemExists[index] & 8) != 0;
        case 4:
          return ((int) this._ItemExists[index] & 16) != 0;
        case 5:
          return ((int) this._ItemExists[index] & 32) != 0;
        case 6:
          return ((int) this._ItemExists[index] & 64) != 0;
        case 7:
          return ((int) this._ItemExists[index] & 128) != 0;
        default:
          return false;
      }
    }

    private void SetExists(Resource resource, bool exists)
    {
      int index = (int) resource.ResourceID / 8;
      int num = (int) resource.ResourceID % 8;
      if (this.GetExists(resource) == exists)
        return;
      switch (num)
      {
        case 0:
          this._ItemExists[index] ^= (byte) 1;
          break;
        case 1:
          this._ItemExists[index] ^= (byte) 2;
          break;
        case 2:
          this._ItemExists[index] ^= (byte) 4;
          break;
        case 3:
          this._ItemExists[index] ^= (byte) 8;
          break;
        case 4:
          this._ItemExists[index] ^= (byte) 16;
          break;
        case 5:
          this._ItemExists[index] ^= (byte) 32;
          break;
        case 6:
          this._ItemExists[index] ^= (byte) 64;
          break;
        case 7:
          this._ItemExists[index] ^= (byte) 128;
          break;
      }
    }

    public bool GetExists(Component component)
    {
      int index = (80 + component.ComponentID) / 8;
      switch ((80 + component.ComponentID) % 8)
      {
        case 0:
          return ((int) this._ItemExists[index] & 1) != 0;
        case 1:
          return ((int) this._ItemExists[index] & 2) != 0;
        case 2:
          return ((int) this._ItemExists[index] & 4) != 0;
        case 3:
          return ((int) this._ItemExists[index] & 8) != 0;
        case 4:
          return ((int) this._ItemExists[index] & 16) != 0;
        case 5:
          return ((int) this._ItemExists[index] & 32) != 0;
        case 6:
          return ((int) this._ItemExists[index] & 64) != 0;
        case 7:
          return ((int) this._ItemExists[index] & 128) != 0;
        default:
          return false;
      }
    }

    private void SetExists(Component component, bool exists)
    {
      int index = (80 + component.ComponentID) / 8;
      int num = (80 + component.ComponentID) % 8;
      if (this.GetExists(component) == exists)
        return;
      switch (num)
      {
        case 0:
          this._ItemExists[index] ^= (byte) 1;
          break;
        case 1:
          this._ItemExists[index] ^= (byte) 2;
          break;
        case 2:
          this._ItemExists[index] ^= (byte) 4;
          break;
        case 3:
          this._ItemExists[index] ^= (byte) 8;
          break;
        case 4:
          this._ItemExists[index] ^= (byte) 16;
          break;
        case 5:
          this._ItemExists[index] ^= (byte) 32;
          break;
        case 6:
          this._ItemExists[index] ^= (byte) 64;
          break;
        case 7:
          this._ItemExists[index] ^= (byte) 128;
          break;
      }
    }

    private int GetIndexForResource(int resourceId, int empireId)
    {
      int index = this._EmpireIds.IndexOf(empireId);
      return index >= 0 && index < this._EmpireIndexers.Count ? (int) this._EmpireIndexers[index][resourceId] - 1 : -1;
    }

    private int GetIndexForComponent(int componentId, int empireId)
    {
      int index = this._EmpireIds.IndexOf(empireId);
      return index >= 0 ? (int) this._EmpireIndexers[index][80 + componentId] - 1 : -1;
    }

    private void SetIndexForResource(int resourceId, int empireId, int index)
    {
      if (index > 32766)
        throw new ApplicationException("Cargo index out of range, must be less than 32767");
      int index1 = this._EmpireIds.IndexOf(empireId);
      if (index1 < 0)
      {
        this._EmpireIndexers.Add(new short[80 + Galaxy.ComponentDefinitionsStatic.Length]);
        this._EmpireIds.Add(empireId);
        index1 = this._EmpireIds.Count - 1;
      }
      this._EmpireIndexers[index1][resourceId] = (short) (index + 1);
    }

    private void SetIndexForComponent(int componentId, int empireId, int index)
    {
      if (index > 32766)
        throw new ApplicationException("Cargo index out of range, must be less than 32767");
      int index1 = this._EmpireIds.IndexOf(empireId);
      if (index1 < 0)
      {
        this._EmpireIndexers.Add(new short[80 + Galaxy.ComponentDefinitionsStatic.Length]);
        this._EmpireIds.Add(empireId);
        index1 = this._EmpireIds.Count - 1;
      }
      this._EmpireIndexers[index1][80 + componentId] = (short) (index + 1);
    }

    public new void Add(Cargo cargo)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].EmpireId == cargo.EmpireId)
          {
            if (cargo.CommodityComponent != null)
            {
              if (this[index].CommodityComponent != null && this[index].CommodityComponent.ComponentID == cargo.CommodityComponent.ComponentID)
              {
                this[index].Amount += cargo.Amount;
                this[index].Reserved += cargo.Reserved;
                this.SetExists(cargo.CommodityComponent, true);
                return;
              }
            }
            else if (cargo.CommodityResource != null && this[index].CommodityResource != null && (int) this[index].CommodityResource.ResourceID == (int) cargo.CommodityResource.ResourceID)
            {
              this[index].Amount += cargo.Amount;
              this[index].Reserved += cargo.Reserved;
              this.SetExists(cargo.CommodityResource, true);
              return;
            }
          }
        }
        int count = this.Count;
        base.Add(cargo);
        if (cargo.CommodityIsComponent)
        {
          this.SetIndexForComponent(cargo.Component.ComponentID, cargo.EmpireId, count);
          this.SetExists(cargo.Component, true);
        }
        else
        {
          if (!cargo.CommodityIsResource)
            return;
          this.SetIndexForResource((int) cargo.Resource.ResourceID, cargo.EmpireId, count);
          this.SetExists(cargo.Resource, true);
        }
      }
    }

    public new void Remove(Cargo cargo)
    {
      lock (this._LockObject)
      {
        int aboveIndex = -1;
        if (cargo.CommodityIsComponent)
        {
          aboveIndex = this.IndexOf(cargo.Component, cargo.EmpireId);
          if (aboveIndex < 0)
            return;
          int num = this._EmpireIds.IndexOf(cargo.EmpireId);
          if (num >= 0)
          {
            this._EmpireIndexers[num][80 + cargo.Component.ComponentID] = (short) 0;
            this.CheckRemoveEmpireIndexes(cargo.EmpireId, num);
          }
        }
        else if (cargo.CommodityIsResource)
        {
          aboveIndex = this.IndexOf(cargo.Resource, cargo.EmpireId);
          if (aboveIndex < 0)
            return;
          int num = this._EmpireIds.IndexOf(cargo.EmpireId);
          if (num >= 0)
          {
            this._EmpireIndexers[num][(int) cargo.Resource.ResourceID] = (short) 0;
            this.CheckRemoveEmpireIndexes(cargo.EmpireId, num);
          }
        }
        this.DecrementIndexes(aboveIndex);
        base.Remove(cargo);
        if (cargo.CommodityIsComponent)
        {
          this.SetExists(cargo.Component, this.CheckAnyEmpireHasComponent(cargo.Component));
        }
        else
        {
          if (!cargo.CommodityIsResource)
            return;
          this.SetExists(cargo.Resource, this.CheckAnyEmpireHasResource(cargo.Resource));
        }
      }
    }

    private bool CheckAnyEmpireHasResource(Resource resource)
    {
      for (int index = 0; index < this._EmpireIds.Count; ++index)
      {
        if (this.GetIndexForResource((int) resource.ResourceID, this._EmpireIds[index]) >= 0)
          return true;
      }
      return false;
    }

    private bool CheckAnyEmpireHasComponent(Component component)
    {
      for (int index = 0; index < this._EmpireIds.Count; ++index)
      {
        if (this.GetIndexForComponent(component.ComponentID, this._EmpireIds[index]) >= 0)
          return true;
      }
      return false;
    }

    private void CheckRemoveEmpireIndexes(int empireId, int empireIndex)
    {
      bool flag = false;
      for (int index = 0; index < this._EmpireIndexers[empireIndex].Length; ++index)
      {
        if (this._EmpireIndexers[empireIndex][index] != (short) 0)
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this._EmpireIndexers.RemoveAt(empireIndex);
      this._EmpireIds.Remove(empireId);
    }

    public new void RemoveAt(int index)
    {
      lock (this._LockObject)
      {
        if (index >= this.Count || index < 0)
          return;
        Cargo cargo = this[index];
        if (cargo.CommodityIsComponent)
        {
          int num = this._EmpireIds.IndexOf(cargo.EmpireId);
          if (num >= 0)
          {
            this._EmpireIndexers[num][80 + cargo.Component.ComponentID] = (short) 0;
            this.CheckRemoveEmpireIndexes(cargo.EmpireId, num);
          }
        }
        else if (cargo.CommodityIsResource)
        {
          int num = this._EmpireIds.IndexOf(cargo.EmpireId);
          if (num >= 0)
          {
            this._EmpireIndexers[num][(int) cargo.Resource.ResourceID] = (short) 0;
            this.CheckRemoveEmpireIndexes(cargo.EmpireId, num);
          }
        }
        this.DecrementIndexes(index);
        base.RemoveAt(index);
        if (cargo.CommodityIsComponent)
        {
          this.SetExists(cargo.Component, this.CheckAnyEmpireHasComponent(cargo.Component));
        }
        else
        {
          if (!cargo.CommodityIsResource)
            return;
          this.SetExists(cargo.Resource, this.CheckAnyEmpireHasResource(cargo.Resource));
        }
      }
    }

    private void DecrementIndexes(int aboveIndex)
    {
      short num = (short) (aboveIndex + 1);
      for (int index1 = 0; index1 < this._EmpireIndexers.Count; ++index1)
      {
        for (int index2 = 0; index2 < this._EmpireIndexers[index1].Length; ++index2)
        {
          if ((int) this._EmpireIndexers[index1][index2] > (int) num)
            --this._EmpireIndexers[index1][index2];
        }
      }
    }

    public CargoList()
    {
    }

    protected CargoList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 15;
          for (int index = 0; index < num; ++index)
          {
            int amount = binaryReader.ReadInt32();
            int reserved = binaryReader.ReadInt32();
            binaryReader.ReadBoolean();
            binaryReader.ReadBoolean();
            int empireId = (int) binaryReader.ReadByte();
            int componentID = (int) binaryReader.ReadInt16();
            int resourceId = (int) binaryReader.ReadInt16();
            Cargo cargo = (Cargo) null;
            if (componentID >= 0)
              cargo = new Cargo(new Component(componentID), amount, empireId, reserved);
            else if (resourceId >= 0)
              cargo = new Cargo(new Resource((byte) resourceId), amount, empireId, reserved);
            if (cargo != null)
              this.Add(cargo);
          }
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this.Count > 0)
          {
            for (int index = 0; index < this.Count; ++index)
            {
              binaryWriter.Write(this[index].Amount);
              binaryWriter.Write(this[index].Reserved);
              binaryWriter.Write(this[index].CommodityIsComponent);
              binaryWriter.Write(this[index].CommodityIsResource);
              binaryWriter.Write((byte) this[index].EmpireId);
              if (this[index].Component != null)
                binaryWriter.Write((short) this[index].Component.ComponentID);
              else
                binaryWriter.Write((short) -1);
              if (this[index].Resource != null)
                binaryWriter.Write((short) this[index].Resource.ResourceID);
              else
                binaryWriter.Write((short) -1);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
          }
          else
            info.AddValue("D", (object) new byte[0]);
        }
      }
    }

    public new void Clear()
    {
      lock (this._LockObject)
      {
        base.Clear();
        this._EmpireIndexers = new List<short[]>();
        this._EmpireIds = new List<int>();
        this._ItemExists = new byte[73];
      }
    }

    public Cargo GetHighestAvailableResource(
      Empire empire,
      BuiltObject spaceport,
      out int highestAvailable)
    {
      Cargo availableResource = (Cargo) null;
      highestAvailable = 0;
      if (empire != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Cargo cargo = this[index];
          if (cargo != null && cargo.EmpireId == empire.EmpireId && cargo.CommodityIsResource && cargo.CommodityResource != null)
          {
            int num = cargo.Available - Galaxy.CalculateResourceLevel(cargo.CommodityResource, spaceport);
            if (availableResource == null || num > highestAvailable)
            {
              availableResource = cargo;
              highestAvailable = num;
            }
          }
        }
      }
      return availableResource;
    }

    public int GetTotalUnitsAvailable(Empire empire, ResourceList resources)
    {
      int totalUnitsAvailable = 0;
      if (empire != null)
      {
        foreach (Cargo cargo in (SyncList<Cargo>) this)
        {
          if (cargo.EmpireId == empire.EmpireId && cargo.CommodityResource != null && resources.Contains(cargo.CommodityResource))
            totalUnitsAvailable += cargo.Available;
        }
      }
      return totalUnitsAvailable;
    }

    public int GetTotalUnitsAvailable(Empire empire, HabitatResourceList resources)
    {
      int totalUnitsAvailable = 0;
      if (empire != null)
      {
        foreach (Cargo cargo in (SyncList<Cargo>) this)
        {
          if (cargo.EmpireId == empire.EmpireId && cargo.CommodityResource != null && resources.ContainsId(cargo.CommodityResource.ResourceID))
            totalUnitsAvailable += cargo.Available;
        }
      }
      return totalUnitsAvailable;
    }

    public int GetTotalUnitsAvailable(Empire empire)
    {
      int totalUnitsAvailable = 0;
      if (empire != null)
      {
        foreach (Cargo cargo in (SyncList<Cargo>) this)
        {
          if (cargo.EmpireId == empire.EmpireId)
            totalUnitsAvailable += cargo.Available;
        }
      }
      return totalUnitsAvailable;
    }

    public int GetTotalUnits(Empire empire)
    {
      int totalUnits = 0;
      if (empire != null)
      {
        foreach (Cargo cargo in (SyncList<Cargo>) this)
        {
          if (cargo.EmpireId == empire.EmpireId)
            totalUnits += cargo.Amount;
        }
      }
      return totalUnits;
    }

    public int TotalUnits
    {
      get
      {
        int totalUnits = 0;
        foreach (Cargo cargo in (SyncList<Cargo>) this)
          totalUnits += cargo.Amount;
        return totalUnits;
      }
    }

    public int GetTotalResourceAmount(Resource resource, int empireId)
    {
      int index = this.IndexOf(resource, empireId);
      return index >= 0 ? this[index].Amount : 0;
    }

    public int GetTotalResourceAvailable(Resource resource, int empireId)
    {
      int index = this.IndexOf(resource, empireId);
      return index >= 0 ? this[index].Available : 0;
    }

    public bool GetResourcesForManufacturing(
      Component componentToBeManufactured,
      Empire empire,
      out ComponentResourceList manufacturingResources,
      out byte deficientResourceId,
      ref int[] resourceAmounts)
    {
      manufacturingResources = new ComponentResourceList();
      deficientResourceId = byte.MaxValue;
      foreach (ComponentResource requiredResource in (SyncList<ComponentResource>) componentToBeManufactured.RequiredResources)
      {
        if (resourceAmounts[(int) requiredResource.ResourceID] == -1)
        {
          if (this.GetExists((Resource) requiredResource))
          {
            int index = this.IndexOf((Resource) requiredResource, empire);
            if (index >= 0)
            {
              Cargo cargo = this[index];
              resourceAmounts[(int) requiredResource.ResourceID] = cargo.Amount;
              if (cargo.Amount >= (int) requiredResource.Quantity)
              {
                manufacturingResources.AddWithoutCheck(requiredResource);
              }
              else
              {
                deficientResourceId = requiredResource.ResourceID;
                return false;
              }
            }
            else
            {
              resourceAmounts[(int) requiredResource.ResourceID] = 0;
              deficientResourceId = requiredResource.ResourceID;
              return false;
            }
          }
          else
          {
            resourceAmounts[(int) requiredResource.ResourceID] = 0;
            deficientResourceId = requiredResource.ResourceID;
            return false;
          }
        }
        else if (resourceAmounts[(int) requiredResource.ResourceID] >= (int) requiredResource.Quantity)
          manufacturingResources.Add(requiredResource);
        else
          deficientResourceId = requiredResource.ResourceID;
      }
      for (int index = 0; index < manufacturingResources.Count; ++index)
        resourceAmounts[(int) manufacturingResources[index].ResourceID] -= (int) manufacturingResources[index].Quantity;
      return true;
    }

    public int IndexOf(ResourceGroup resourceGroup, Empire empire, int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        if (empire != null)
        {
          for (int index = startIndex; index < this.Count; ++index)
          {
            if (this[index].CommodityResource != null && this[index].CommodityResource.Group == resourceGroup && this[index].EmpireId == empire.EmpireId)
              return index;
          }
        }
      }
      return -1;
    }

    public int ResourceGroupCount(ResourceGroup resourceGroup, Empire empire)
    {
      int num = 0;
      lock (this._LockObject)
      {
        if (empire != null)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].CommodityResource != null && this[index].CommodityResource.Group == resourceGroup && this[index].EmpireId == empire.EmpireId && this[index].Amount > 0)
              ++num;
          }
        }
      }
      return num;
    }

    public int RestrictedResourceCount(Empire empire)
    {
      int num = 0;
      lock (this._LockObject)
      {
        if (empire != null)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].CommodityResource != null && this[index].CommodityResource.IsRestrictedResource && this[index].EmpireId == empire.EmpireId && this[index].Amount > 0)
              ++num;
          }
        }
      }
      return num;
    }

    public int IndexOf(Component component)
    {
      if (component == null)
        return -1;
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].CommodityComponent != null && this[index].CommodityComponent.ComponentID == component.ComponentID)
            return index;
        }
      }
      return -1;
    }

    public Cargo GetCargo(Component component, Empire empire)
    {
      if (empire == null)
        return (Cargo) null;
      int index = this.IndexOf(component, empire.EmpireId);
      return index >= 0 ? this[index] : (Cargo) null;
    }

    public int IndexOf(Component component, Empire empire) => empire != null ? this.IndexOf(component, empire.EmpireId) : -1;

    public int IndexOf(Component component, int empireId)
    {
      if (component == null || empireId < 0)
        return -1;
      lock (this._LockObject)
        return this.GetIndexForComponent(component.ComponentID, empireId);
    }

    public int IndexOf(Resource resource)
    {
      if (resource == null)
        return -1;
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].CommodityResource != null && (int) this[index].CommodityResource.ResourceID == (int) resource.ResourceID)
            return index;
        }
      }
      return -1;
    }

    public int IndexOf(byte resourceId)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].CommodityResource != null && (int) this[index].CommodityResource.ResourceID == (int) resourceId)
            return index;
        }
      }
      return -1;
    }

    public Cargo GetCargo(Resource resource, Empire empire)
    {
      if (empire == null)
        return (Cargo) null;
      int index = this.IndexOf(resource, empire.EmpireId);
      return index >= 0 ? this[index] : (Cargo) null;
    }

    public int IndexOf(Resource resource, Empire empire) => empire != null ? this.IndexOf(resource, empire.EmpireId) : -1;

    public int IndexOf(Resource resource, int empireId)
    {
      if (resource == null || empireId < 0)
        return -1;
      lock (this._LockObject)
        return this.GetIndexForResource((int) resource.ResourceID, empireId);
    }

    public Cargo GetCargoOptimized(Resource resource, int empireId)
    {
      if (resource == null || empireId < 0)
        return (Cargo) null;
      int indexForResource = this.GetIndexForResource((int) resource.ResourceID, empireId);
      if (indexForResource < 0)
        return (Cargo) null;
      Cargo cargo = this[indexForResource];
      if (cargo == null)
        return (Cargo) null;
      return cargo.Resource != null && (int) cargo.Resource.ResourceID == (int) resource.ResourceID && cargo.EmpireId == empireId ? cargo : this.GetCargo(resource, empireId);
    }

    public Cargo GetCargo(Resource resource, int empireId)
    {
      if (resource == null || empireId < 0)
        return (Cargo) null;
      lock (this._LockObject)
      {
        int indexForResource = this.GetIndexForResource((int) resource.ResourceID, empireId);
        return indexForResource >= 0 ? this[indexForResource] : (Cargo) null;
      }
    }

    public Cargo GetCargo(Component component, int empireId)
    {
      if (component == null || empireId < 0)
        return (Cargo) null;
      lock (this._LockObject)
      {
        int indexForComponent = this.GetIndexForComponent(component.ComponentID, empireId);
        return indexForComponent >= 0 ? this[indexForComponent] : (Cargo) null;
      }
    }

    public CargoList Clone()
    {
      CargoList cargoList = new CargoList();
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Cargo cargo1 = (Cargo) null;
          Cargo cargo2 = this[index];
          if (this[index].CommodityResource != null)
            cargo1 = new Cargo(cargo2.CommodityResource, cargo2.Amount, cargo2.EmpireId, cargo2.Reserved);
          else if (this[index].CommodityComponent != null)
            cargo1 = new Cargo(cargo2.CommodityComponent, cargo2.Amount, cargo2.EmpireId, cargo2.Reserved);
          cargoList.Add(cargo1);
        }
      }
      return cargoList;
    }
  }
}
