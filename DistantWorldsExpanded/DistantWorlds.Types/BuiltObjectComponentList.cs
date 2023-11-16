// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BuiltObjectComponentList
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
  public class BuiltObjectComponentList : SyncList<BuiltObjectComponent>, ISerializable
  {
    public BuiltObjectComponentList()
    {
    }

    public BuiltObjectComponentList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num1 = buffer.Length / 4;
          for (int index = 0; index < num1; ++index)
          {
            byte componentID = binaryReader.ReadByte();
            byte num2 = binaryReader.ReadByte();
            short num3 = binaryReader.ReadInt16();
            this.Add(new BuiltObjectComponent((int) componentID, (ComponentStatus) num2)
            {
              BuiltObjectComponentId = num3
            });
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
              binaryWriter.Write((byte) this[index].ComponentID);
              binaryWriter.Write((byte) this[index].Status);
              binaryWriter.Write(this[index].BuiltObjectComponentId);
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

    public BuiltObjectComponent this[
      ComponentCategoryType componentCategory,
      ComponentStatus status]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          BuiltObjectComponent builtObjectComponent = this[index];
          if (builtObjectComponent != null && builtObjectComponent.Category == componentCategory && builtObjectComponent.Status == status)
            return builtObjectComponent;
        }
        return (BuiltObjectComponent) null;
      }
    }

    public BuiltObjectComponent this[ComponentType componentType, ComponentStatus status]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          BuiltObjectComponent builtObjectComponent = this[index];
          if (builtObjectComponent != null && builtObjectComponent.Type == componentType && builtObjectComponent.Status == status)
            return builtObjectComponent;
        }
        return (BuiltObjectComponent) null;
      }
    }

    public int FindNextUnbuiltComponent(int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        for (int index = startIndex; index < this.Count; ++index)
        {
          if (this[index].Status == ComponentStatus.Unbuilt)
            return index;
        }
      }
      return -1;
    }

    public int FindNextUnbuiltOrDamagedComponent(int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        for (int index = startIndex; index < this.Count; ++index)
        {
          if (this[index].Status == ComponentStatus.Damaged || this[index].Status == ComponentStatus.Unbuilt)
            return index;
        }
      }
      return -1;
    }

    public int FindNextBuiltComponent(int startIndex)
    {
      if (startIndex >= this.Count)
        return -1;
      lock (this._LockObject)
      {
        for (int index = startIndex; index < this.Count; ++index)
        {
          if (this[index].Status == ComponentStatus.Normal)
            return index;
        }
      }
      return -1;
    }

    public int UnbuiltOrDamagedComponentCount
    {
      get
      {
        int damagedComponentCount = 0;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].Status == ComponentStatus.Damaged || this[index].Status == ComponentStatus.Unbuilt)
              ++damagedComponentCount;
          }
        }
        return damagedComponentCount;
      }
    }

    public int UnbuiltComponentCount
    {
      get
      {
        int unbuiltComponentCount = 0;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].Status == ComponentStatus.Unbuilt)
              ++unbuiltComponentCount;
          }
        }
        return unbuiltComponentCount;
      }
    }

    public int DamagedComponentCount
    {
      get
      {
        int damagedComponentCount = 0;
        lock (this._LockObject)
        {
          for (int index = 0; index < this.Count; ++index)
          {
            if (this[index].Status == ComponentStatus.Damaged)
              ++damagedComponentCount;
          }
        }
        return damagedComponentCount;
      }
    }

    public BuiltObjectComponent FindComponentByBuiltObjectComponentId(short builtObjectComponentId)
    {
      if (builtObjectComponentId >= (short) 0)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if ((int) this[index].BuiltObjectComponentId == (int) builtObjectComponentId)
            return this[index];
        }
      }
      return (BuiltObjectComponent) null;
    }

    private short FindHighestBuiltObjectComponentId()
    {
      short objectComponentId = -1;
      for (int index = 0; index < this.Count; ++index)
      {
        if ((int) this[index].BuiltObjectComponentId > (int) objectComponentId)
          objectComponentId = this[index].BuiltObjectComponentId;
      }
      return objectComponentId;
    }

    public new void Add(BuiltObjectComponent component)
    {
      base.Add(component);
      if (component.BuiltObjectComponentId >= (short) 0)
        return;
      component.BuiltObjectComponentId = (short) ((int) this.FindHighestBuiltObjectComponentId() + 1);
    }

    public new void Remove(BuiltObjectComponent component) => base.Remove(component);

    public new void RemoveAt(int index) => base.RemoveAt(index);

    public new void AddRange(IEnumerable<BuiltObjectComponent> components) => throw new NotImplementedException("Do not use this method");

    public int IndexByIdAndStatus(int componentId, ComponentStatus status)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].ComponentID == componentId && this[index].Status == status)
            return index;
        }
      }
      return -1;
    }

    public ComponentList ResolveComponentList()
    {
      ComponentList componentList = new ComponentList();
      foreach (Component component1 in (SyncList<BuiltObjectComponent>) this)
      {
        Component component2 = new Component(component1.ComponentID);
        componentList.Add(component2);
      }
      return componentList;
    }

    public bool ContainsComponentId(int componentId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        BuiltObjectComponent builtObjectComponent = this[index];
        if (builtObjectComponent != null && builtObjectComponent.ComponentID == componentId)
          return true;
      }
      return false;
    }

    public int CountNormalComponentsByType(ComponentType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        BuiltObjectComponent builtObjectComponent = this[index];
        if (builtObjectComponent != null && builtObjectComponent.Type == type && builtObjectComponent.Status == ComponentStatus.Normal)
          ++num;
      }
      return num;
    }

    public int CountNormalComponentsByCategory(ComponentCategoryType category)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        BuiltObjectComponent builtObjectComponent = this[index];
        if (builtObjectComponent != null && builtObjectComponent.Category == category && builtObjectComponent.Status == ComponentStatus.Normal)
          ++num;
      }
      return num;
    }
  }
}
