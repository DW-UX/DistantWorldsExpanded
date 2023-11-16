// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceList : SyncList<Resource>, ISerializable
  {
    public ResourceList()
    {
    }

    protected ResourceList(SerializationInfo info, StreamingContext context)
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
            short resourceId = binaryReader.ReadInt16();
            if (resourceId >= (short) 0)
              this.Add(new Resource((byte) resourceId));
            else
              this.Add((Resource) null);
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
              if (this[index] != null)
                binaryWriter.Write((short) this[index].ResourceID);
              else
                binaryWriter.Write((short) -1);
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

    public bool ContainsGroup(ResourceGroup group)
    {
      foreach (Resource resource in (SyncList<Resource>) this)
      {
        if (resource.Group == group)
          return true;
      }
      return false;
    }

    public new bool Contains(Resource resource)
    {
      foreach (Resource resource1 in (SyncList<Resource>) this)
      {
        if ((int) resource1.ResourceID == (int) resource.ResourceID)
          return true;
      }
      return false;
    }

    public int IndexOf(byte resourceId)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if ((int) this[index].ResourceID == (int) resourceId)
            return index;
        }
      }
      return -1;
    }
  }
}
