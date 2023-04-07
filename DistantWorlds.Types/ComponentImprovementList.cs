// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ComponentImprovementList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ComponentImprovementList : SyncList<ComponentImprovement>
  {
    public ComponentImprovementList()
    {
    }

    public ComponentImprovementList(ComponentList components)
    {
      if (components == null)
        return;
      for (int index = 0; index < components.Count; ++index)
      {
        Component component = components[index];
        if (component != null)
          this.Add(new ComponentImprovement(component));
      }
    }

    public bool ContainsComponent(Component component)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        ComponentImprovement componentImprovement = this[index];
        if (componentImprovement != null && componentImprovement.ImprovedComponent != null && componentImprovement.ImprovedComponent.ComponentID == component.ComponentID)
          return true;
      }
      return false;
    }
  }
}
