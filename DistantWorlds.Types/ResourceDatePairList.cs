// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceDatePairList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceDatePairList : List<ResourceDatePair>
  {
    public void ClearResources(ComponentResourceList resourcesToClear)
    {
      ResourceDatePairList resourceDatePairList = new ResourceDatePairList();
      for (int index1 = 0; index1 < resourcesToClear.Count; ++index1)
      {
        for (int index2 = 0; index2 < this.Count; ++index2)
        {
          if ((int) this[index2].ResourceId == (int) resourcesToClear[index1].ResourceID)
            resourceDatePairList.Add(this[index2]);
        }
      }
      for (int index = 0; index < resourceDatePairList.Count; ++index)
        this.Remove(resourceDatePairList[index]);
    }

    public bool Contains(byte resourceId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if ((int) this[index].ResourceId == (int) resourceId)
          return true;
      }
      return false;
    }

    public void CheckAddResource(byte resourceId, long starDate)
    {
      if (this.Contains(resourceId))
        return;
      this.Add(new ResourceDatePair(resourceId, starDate));
    }

    public ResourceDatePairList GetResourcesOlderThanAge(long starDate, long age)
    {
      ResourceDatePairList resourcesOlderThanAge = new ResourceDatePairList();
      long num = starDate - age;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].StarDate <= num)
          resourcesOlderThanAge.Add(this[index]);
      }
      return resourcesOlderThanAge;
    }
  }
}
