// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentResourceList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ComponentResourceList : SyncList<ComponentResource>, ISerializable
  {
    public ComponentResourceList()
    {
    }

    public ComponentResourceList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 4;
          for (int index = 0; index < num; ++index)
          {
            int resourceId = (int) binaryReader.ReadInt16();
            short quantity = binaryReader.ReadInt16();
            if (resourceId >= 0 && resourceId <= (int) byte.MaxValue)
              this.Add(new ComponentResource((byte) resourceId, quantity));
            else
              this.Add((ComponentResource) null);
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
              {
                binaryWriter.Write((short) this[index].ResourceID);
                binaryWriter.Write(this[index].Quantity);
              }
              else
              {
                binaryWriter.Write((short) -1);
                binaryWriter.Write((short) 0);
              }
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

    public new void Add(ComponentResource resource)
    {
      lock (this._LockObject)
      {
        foreach (Resource resource1 in (SyncList<ComponentResource>) this)
        {
          if ((int) resource1.ResourceID == (int) resource.ResourceID)
            throw new ApplicationException("Can't add the same resource twice to a component");
        }
        base.Add(resource);
      }
    }

    public void AddWithoutCheck(ComponentResource resource) => base.Add(resource);

    public new int IndexOf(ComponentResource resource)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if ((int) this[index].ResourceID == (int) resource.ResourceID)
            return index;
        }
      }
      return -1;
    }

    public new bool Contains(ComponentResource resource)
    {
      foreach (Resource resource1 in (SyncList<ComponentResource>) this)
      {
        if ((int) resource1.ResourceID == (int) resource.ResourceID)
          return true;
      }
      return false;
    }
  }
}
