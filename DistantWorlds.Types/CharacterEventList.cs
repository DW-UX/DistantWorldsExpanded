// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterEventList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CharacterEventList : List<CharacterEvent>
  {
    [OptionalField]
    private object _LockObject = new object();

    public new void Add(CharacterEvent characterEvent)
    {
      if (this._LockObject == null)
        this._LockObject = new object();
      lock (this._LockObject)
        base.Add(characterEvent);
    }

    public void AddRange(CharacterEventList characterEvents)
    {
      if (this._LockObject == null)
        this._LockObject = new object();
      lock (this._LockObject)
        this.AddRange((IEnumerable<CharacterEvent>) characterEvents);
    }

    public void Remove(CharacterEvent characterEvent)
    {
      if (this._LockObject == null)
        this._LockObject = new object();
      lock (this._LockObject)
        base.Remove(characterEvent);
    }

    public new void RemoveAt(int index)
    {
      if (this._LockObject == null)
        this._LockObject = new object();
      lock (this._LockObject)
        base.RemoveAt(index);
    }

    public CharacterEventList ObtainPublicEvents()
    {
      CharacterEventList publicEvents = new CharacterEventList();
      for (int index = 0; index < this.Count; ++index)
      {
        CharacterEvent characterEvent = this[index];
        if (characterEvent != null && Galaxy.DetermineCharacterEventIsPublic(characterEvent.Type))
          publicEvents.Add(characterEvent);
      }
      return publicEvents;
    }

    public int CountEventsByType(CharacterEventType eventType)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        CharacterEvent characterEvent = this[index];
        if (characterEvent != null && characterEvent.Type == eventType)
          ++num;
      }
      return num;
    }

    public long GetDateOfMostRecentEventByType(CharacterEventType eventType)
    {
      this.Sort();
      this.Reverse();
      for (int index = 0; index < this.Count; ++index)
      {
        CharacterEvent characterEvent = this[index];
        if (characterEvent != null && characterEvent.Type == eventType)
          return characterEvent.StarDate;
      }
      return long.MinValue;
    }

    public CharacterEvent GetMostRecentEventByType(CharacterEventType eventType)
    {
      this.Sort();
      this.Reverse();
      for (int index = 0; index < this.Count; ++index)
      {
        CharacterEvent recentEventByType = this[index];
        if (recentEventByType != null && recentEventByType.Type == eventType)
          return recentEventByType;
      }
      return (CharacterEvent) null;
    }
  }
}
