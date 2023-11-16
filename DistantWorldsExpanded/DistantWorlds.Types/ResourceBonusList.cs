// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ResourceBonusList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ResourceBonusList : List<ResourceBonus>
  {
    public double GetBonusTotalByEffectType(ColonyResourceEffect effectType)
    {
      double totalByEffectType = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceBonus resourceBonus = this[index];
        if (resourceBonus != null && resourceBonus.Effect == effectType)
          totalByEffectType += resourceBonus.Value;
      }
      return totalByEffectType;
    }

    public ResourceBonus GetBonusByEffectType(ColonyResourceEffect effectType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceBonus bonusByEffectType = this[index];
        if (bonusByEffectType != null && bonusByEffectType.Effect == effectType)
          return bonusByEffectType;
      }
      return (ResourceBonus) null;
    }

    public ResourceBonus GetBonusByResourceType(byte resourceId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceBonus bonusByResourceType = this[index];
        if (bonusByResourceType != null && (int) bonusByResourceType.ResourceId == (int) resourceId)
          return bonusByResourceType;
      }
      return (ResourceBonus) null;
    }

    public ResourceList ResolveResources()
    {
      ResourceList resourceList = new ResourceList();
      for (int index = 0; index < this.Count; ++index)
      {
        ResourceBonus resourceBonus = this[index];
        if (resourceBonus != null)
        {
          Resource resource = new Resource(resourceBonus.ResourceId);
          resourceList.Add(resource);
        }
      }
      return resourceList;
    }
  }
}
