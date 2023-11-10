// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.EventActionExecutionPackage
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class EventActionExecutionPackage
  {
    public EventAction Action;
    public GameEvent GameEvent;
    public Empire TriggerEmpire;

    public EventActionExecutionPackage(
      EventAction action,
      GameEvent gameEvent,
      Empire triggerEmpire)
    {
      this.Action = action;
      this.GameEvent = gameEvent;
      this.TriggerEmpire = triggerEmpire;
    }
  }
}
