// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ManufacturerList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ManufacturerList : SyncList<Manufacturer>, ISerializable
  {
    public ManufacturerList()
    {
    }

    protected ManufacturerList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num1 = buffer.Length / 13;
          for (int index = 0; index < num1; ++index)
          {
            short componentID1 = binaryReader.ReadInt16();
            short builtObjectComponentIndex = binaryReader.ReadInt16();
            short componentID2 = binaryReader.ReadInt16();
            IndustryType industry = (IndustryType) binaryReader.ReadByte();
            float num2 = binaryReader.ReadSingle();
            short manufacturingSpeed = binaryReader.ReadInt16();
            Manufacturer manufacturer = new Manufacturer((int) builtObjectComponentIndex, new BuiltObjectComponent((int) componentID2, ComponentStatus.Normal), industry, (int) manufacturingSpeed);
            manufacturer.Progress = num2;
            if (componentID1 >= (short) 0)
              manufacturer.Component = new Component((int) componentID1);
            if (manufacturer != null)
              this.Add(manufacturer);
          }
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this.Count > 0)
      {
        using (MemoryStream output = new MemoryStream())
        {
          using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
          {
            for (int index = 0; index < this.Count; ++index)
            {
              if (this[index].Component != null)
                binaryWriter.Write((short) this[index].Component.ComponentID);
              else
                binaryWriter.Write((short) -1);
              binaryWriter.Write((short) this[index].ParentBuiltObjectComponentIndex);
              if (this[index].ParentBuiltObjectComponent != null)
                binaryWriter.Write((short) this[index].ParentBuiltObjectComponent.ComponentID);
              else
                binaryWriter.Write((short) -1);
              binaryWriter.Write((byte) this[index].Industry);
              binaryWriter.Write(this[index].Progress);
              binaryWriter.Write((short) this[index].ManufacturingSpeed);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
          }
        }
      }
      else
        info.AddValue("D", (object) new byte[0]);
    }

    public bool CanBuildComponent(Component component)
    {
      IndustryType industry = component.Industry;
      foreach (Manufacturer manufacturer in (SyncList<Manufacturer>) this)
      {
        if (manufacturer.Industry == industry)
          return true;
      }
      return false;
    }

    public bool IsManufacturerAvailable
    {
      get
      {
        foreach (Manufacturer manufacturer in (SyncList<Manufacturer>) this)
        {
          if (manufacturer.Component == null)
            return true;
        }
        return false;
      }
    }

    public int CanAddComponentNow(Component component)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Component == null && this[index].Industry == component.Industry)
            return index;
        }
      }
      return -1;
    }

    public bool AddComponentToManufacture(Component component)
    {
      foreach (Manufacturer manufacturer in (SyncList<Manufacturer>) this)
      {
        if (manufacturer.Industry == component.Industry && manufacturer.Component == null)
        {
          manufacturer.Component = component;
          return true;
        }
      }
      return false;
    }

    public int FindManufacturerByComponentIndex(int componentIndex)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (componentIndex == this[index].ParentBuiltObjectComponentIndex)
            return index;
        }
      }
      return -1;
    }
  }
}
