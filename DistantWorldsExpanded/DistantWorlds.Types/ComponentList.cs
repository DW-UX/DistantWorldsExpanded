// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentList
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
  public class ComponentList : SyncList<Component>, ISerializable
  {
    public ComponentList()
    {
    }

    public ComponentList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 2;
          for (int index = 0; index < num; ++index)
          {
            int componentID = (int) binaryReader.ReadInt16();
            if (componentID >= 0)
              this.Add(new Component(componentID));
            else
              this.Add((Component) null);
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
              if (this[index] != null)
                binaryWriter.Write((short) this[index].ComponentID);
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

    public new bool Contains(Component containedComponent)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].ComponentID == containedComponent.ComponentID)
          return true;
      }
      return false;
    }

    public ComponentList Clone()
    {
      ComponentList componentList = new ComponentList();
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Component component = new Component(this[index].ComponentID);
          componentList.Add(component);
        }
      }
      return componentList;
    }

    public Component GetFirstByType(ComponentType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          return this[index];
      }
      return (Component) null;
    }

    public Component GetFirstByCategory(ComponentCategoryType category)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category)
          return this[index];
      }
      return (Component) null;
    }

    public Component GetFirstWeaponOrFighter()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        Component firstWeaponOrFighter = this[index];
        if (firstWeaponOrFighter != null)
        {
          switch (firstWeaponOrFighter.Category)
          {
            case ComponentCategoryType.WeaponBeam:
            case ComponentCategoryType.WeaponTorpedo:
            case ComponentCategoryType.WeaponArea:
            case ComponentCategoryType.Fighter:
            case ComponentCategoryType.WeaponSuperBeam:
            case ComponentCategoryType.WeaponSuperArea:
            case ComponentCategoryType.WeaponSuperTorpedo:
              return firstWeaponOrFighter;
            case ComponentCategoryType.WeaponIon:
              switch (firstWeaponOrFighter.Type)
              {
                case ComponentType.WeaponIonCannon:
                case ComponentType.WeaponIonPulse:
                  return firstWeaponOrFighter;
                default:
                  continue;
              }
            case ComponentCategoryType.WeaponGravity:
              switch (firstWeaponOrFighter.Type)
              {
                case ComponentType.WeaponGravityBeam:
                case ComponentType.WeaponAreaGravity:
                  return firstWeaponOrFighter;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
      }
      return (Component) null;
    }

    public ComponentList GetByType(ComponentType type)
    {
      ComponentList byType = new ComponentList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          byType.Add(this[index]);
      }
      return byType;
    }

    public int CountByType(ComponentType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          ++num;
      }
      return num;
    }

    public ComponentList GetByCategory(ComponentCategoryType category)
    {
      ComponentList byCategory = new ComponentList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category)
          byCategory.Add(this[index]);
      }
      return byCategory;
    }

    public int CountByCategory(ComponentCategoryType category)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category)
          ++num;
      }
      return num;
    }

    public void RemoveAllByComponentCategory(ComponentCategoryType category)
    {
      ComponentList componentList = new ComponentList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Category == category)
          componentList.Add(this[index]);
      }
      for (int index = 0; index < componentList.Count; ++index)
        this.Remove(componentList[index]);
    }

    public bool RemoveByComponentId(Component component)
    {
      if (component != null)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ComponentID == component.ComponentID)
          {
            this.RemoveAt(index);
            return true;
          }
        }
      }
      return false;
    }

    public int IndexById(Component component)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ComponentID == component.ComponentID)
            return index;
        }
      }
      return -1;
    }

    public int LastIndexById(Component component)
    {
      lock (this._LockObject)
      {
        for (int index = this.Count - 1; index >= 0; --index)
        {
          if (this[index].ComponentID == component.ComponentID)
            return index;
        }
      }
      return -1;
    }

    public ComponentList GetDistinctComponentList()
    {
      ComponentList distinctComponentList = new ComponentList();
      bool[] flagArray = new bool[Galaxy.ComponentDefinitionsStatic.Length];
      for (int index = 0; index < this.Count; ++index)
      {
        Component component = this[index];
        if (component != null && !flagArray[component.ComponentID])
        {
          distinctComponentList.Add(component);
          flagArray[component.ComponentID] = true;
        }
      }
      return distinctComponentList;
    }

    public int[] ResolveComponentCountsByType()
    {
      int[] numArray = new int[Galaxy.ComponentDefinitionsStatic.Length];
      for (int index = 0; index < this.Count; ++index)
      {
        Component component = this[index];
        if (component != null)
          ++numArray[component.ComponentID];
      }
      return numArray;
    }

    public List<ComponentType> ResolveComponentTypes()
    {
      List<ComponentType> componentTypeList = new List<ComponentType>();
      for (int index = 0; index < this.Count; ++index)
      {
        Component component = this[index];
        if (component != null && !componentTypeList.Contains(component.Type))
          componentTypeList.Add(component.Type);
      }
      return componentTypeList;
    }

    public ComponentList Diff(ComponentList components)
    {
      ComponentList componentList1 = this.Clone();
      ComponentList componentList2 = components.Clone();
      for (int index1 = componentList1.Count - 1; index1 >= 0; --index1)
      {
        int index2 = componentList2.IndexById(componentList1[index1]);
        if (index2 >= 0)
        {
          componentList2.RemoveAt(index2);
          componentList1.RemoveAt(index1);
        }
      }
      return componentList2;
    }
  }
}
