// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DockingBayList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DockingBayList : SyncList<DockingBay>, ISerializable
  {
    public DockingBayList()
    {
    }

    public DockingBayList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer != null && buffer.Length > 0)
      {
        using (MemoryStream input = new MemoryStream(buffer))
        {
          using (BinaryReader binaryReader = new BinaryReader((Stream) input))
          {
            int num = buffer.Length / 8;
            for (int index = 0; index < num; ++index)
            {
              int capacity = binaryReader.ReadInt32();
              this.Add(new DockingBay((int) binaryReader.ReadInt16(), binaryReader.ReadInt16(), capacity));
            }
          }
        }
      }
      BuiltObjectList builtObjectList = (BuiltObjectList) info.GetValue("DckShps", typeof (BuiltObjectList));
      if (builtObjectList == null)
        return;
      for (int index = 0; index < builtObjectList.Count; ++index)
        this[index].DockedShip = builtObjectList[index];
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
              binaryWriter.Write(this[index].Capacity);
              binaryWriter.Write(this[index].ParentComponentId);
              binaryWriter.Write(this[index].ParentBuiltObjectComponentId);
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
            BuiltObjectList builtObjectList = new BuiltObjectList();
            for (int index = 0; index < this.Count; ++index)
              builtObjectList.Add(this[index].DockedShip);
            info.AddValue("DckShps", (object) builtObjectList);
          }
          else
          {
            info.AddValue("D", (object) new byte[0]);
            info.AddValue("DckShps", (object) null);
          }
        }
      }
    }

    public int CountDocked
    {
      get
      {
        int countDocked = 0;
        foreach (DockingBay dockingBay in (SyncList<DockingBay>) this)
        {
          if (dockingBay.DockedShip != null)
            ++countDocked;
        }
        return countDocked;
      }
    }

    public int IndexOf(BuiltObjectComponent builtObjectComponent)
    {
      lock (this._LockObject)
      {
        if (builtObjectComponent != null)
        {
          if (builtObjectComponent.BuiltObjectComponentId >= (short) 0)
          {
            for (int index = 0; index < this.Count; ++index)
            {
              if ((int) builtObjectComponent.BuiltObjectComponentId == (int) this[index].ParentBuiltObjectComponentId)
                return index;
            }
          }
        }
      }
      return -1;
        }
        public DockingBay GetObjIndexOf(BuiltObjectComponent builtObjectComponent)
        {
            lock (this._LockObject)
            {
                if (builtObjectComponent != null)
                {
                    if (builtObjectComponent.BuiltObjectComponentId >= (short)0)
                    {
                        for (int index = 0; index < this.Count; ++index)
                        {
                            if ((int)builtObjectComponent.BuiltObjectComponentId == (int)this[index].ParentBuiltObjectComponentId)
                                return this[index];
                        }
                    }
                }
            }
            return null;
        }

        public int IndexOf(BuiltObject builtObject)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (builtObject == this[index].DockedShip)
            return index;
        }
      }
      return -1;
    }
  }
}
