// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.HabitatResourceList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class HabitatResourceList : SyncList<HabitatResource>, ISerializable
  {
    private sbyte[] _Index;

    public HabitatResourceList()
    {
      this._Index = new sbyte[100];
      this.ClearIndexes();
    }

    protected HabitatResourceList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("HR", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 3;
          for (int index = 0; index < num; ++index)
            this.Add(new HabitatResource(binaryReader.ReadByte(), (int) binaryReader.ReadInt16()));
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
              binaryWriter.Write(this[index].ResourceID);
              binaryWriter.Write(this[index].Abundance);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("HR", (object) output.ToArray());
          }
          else
            info.AddValue("HR", (object) new byte[0]);
        }
      }
    }

    public HabitatResourceList Clone()
    {
      HabitatResourceList habitatResourceList = new HabitatResourceList();
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          HabitatResource habitatResource = this[index];
          if (habitatResource != null)
            habitatResourceList.Add(new HabitatResource(habitatResource.ResourceID, (int) habitatResource.Abundance));
        }
      }
      return habitatResourceList;
    }

    private void ClearIndexes()
    {
      if (this._Index == null || this._Index.Length <= 0)
        return;
      for (int index = 0; index < this._Index.Length; ++index)
        this._Index[index] = (sbyte) -1;
    }

    public new void Clear()
    {
      lock (this._LockObject)
      {
        base.Clear();
        this._Index = new sbyte[100];
        this.ClearIndexes();
      }
    }

    public new bool Remove(HabitatResource habitatResource) => throw new NotImplementedException();

    public new void RemoveAt(int index)
    {
      lock (this._LockObject)
      {
        if (index >= this.Count)
          return;
        this._Index[(int) this[index].ResourceID] = (sbyte) -1;
        base.RemoveAt(index);
      }
    }

    public int Add(HabitatResource habitatResource)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if ((int) this[index].ResourceID == (int) habitatResource.ResourceID)
            return -1;
        }
        this._Index[(int) habitatResource.ResourceID] = (sbyte) this.Count;
        base.Add(habitatResource);
        return this.Count - 1;
      }
    }

    public int IndexOf(byte resourceId, int startIndex)
    {
      int num = -1;
      if ((int) resourceId >= this._Index.Length)
        return -1;
      lock (this._LockObject)
        num = (int) this._Index[(int) resourceId];
      return num >= startIndex ? num : -1;
    }

    public bool HasFuelResources()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.IsFuel)
          return true;
      }
      return false;
    }

    public bool HasSuperLuxuryResources()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.IsRestrictedResource)
          return true;
      }
      return false;
    }

    public bool HasLuxuryResources()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.IsLuxuryResource)
          return true;
      }
      return false;
    }

    public int CountGasResources()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.Group == ResourceGroup.Gas)
          ++num;
      }
      return num;
    }

    public int CountMineralResources()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.Group == ResourceGroup.Mineral)
          ++num;
      }
      return num;
    }

    public int CountLuxuryResources()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.Group == ResourceGroup.Luxury)
          ++num;
      }
      return num;
    }

    public int CountColonyManufacturedResources()
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.ColonyManufacturingLevel > 0)
          ++num;
      }
      return num;
    }

    public HabitatResourceList GetColonyManufacturedResources()
    {
      HabitatResourceList manufacturedResources = new HabitatResourceList();
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.ColonyManufacturingLevel > 0)
          manufacturedResources.Add(habitatResource);
      }
      return manufacturedResources;
    }

    public bool ContainsName(string resourceName)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.Name == resourceName)
          return true;
      }
      return false;
    }

    public bool ContainsId(byte resourceId) => this.IndexOf(resourceId, 0) >= 0;

    public bool ContainsGroup(ResourceGroup resourceGroup)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        HabitatResource habitatResource = this[index];
        if (habitatResource != null && habitatResource.Group == resourceGroup)
          return true;
      }
      return false;
    }
  }
}
