// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResearchAreaList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResearchAreaList : SyncList<ResearchArea>, ISerializable
  {
    public ResearchAreaList()
    {
    }

    protected ResearchAreaList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      BinaryReader binaryReader = new BinaryReader((Stream) new MemoryStream(buffer));
      int num1 = buffer.Length / 7;
      for (int index = 0; index < num1; ++index)
      {
        ComponentCategoryType componentCategory = (ComponentCategoryType) binaryReader.ReadByte();
        float num2 = binaryReader.ReadSingle();
        short componentID = binaryReader.ReadInt16();
        ResearchArea researchArea = new ResearchArea(componentCategory);
        researchArea.TechPoints = num2;
        if (componentID >= (short) 0)
          researchArea.LatestComponent = new Component((int) componentID);
        this.Add(researchArea);
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this.Count > 0)
      {
        MemoryStream output = new MemoryStream();
        BinaryWriter binaryWriter = new BinaryWriter((Stream) output);
        for (int index = 0; index < this.Count; ++index)
        {
          binaryWriter.Write((byte) this[index].Category);
          binaryWriter.Write(this[index].TechPoints);
          if (this[index].LatestComponent != null)
            binaryWriter.Write((short) this[index].LatestComponent.ComponentID);
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

    public int IndexOf(ComponentCategoryType componentCategoryType)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Category == componentCategoryType)
            return index;
        }
      }
      return -1;
    }

    public ResearchArea GetByCategory(ComponentCategoryType componentCategoryType) => this[(int) (componentCategoryType - (byte) 1)];
  }
}
