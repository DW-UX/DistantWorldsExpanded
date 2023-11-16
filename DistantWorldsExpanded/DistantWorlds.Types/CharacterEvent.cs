// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.CharacterEvent
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class CharacterEvent : IComparable<CharacterEvent>
  {
    public CharacterEventType Type;
    public object EventData;
    public long StarDate;

    public CharacterEvent(CharacterEventType type, object eventData, long starDate)
    {
      switch (type)
      {
        case CharacterEventType.CharacterStart:
        case CharacterEventType.CharacterTransferLocation:
          if (eventData is Character)
          {
            Character character = (Character) eventData;
            if (character != null)
            {
              eventData = (object) new object[2]
              {
                (object) character,
                (object) character.Location
              };
              break;
            }
            break;
          }
          break;
      }
      this.Type = type;
      this.EventData = eventData;
      this.StarDate = starDate;
    }

    int IComparable<CharacterEvent>.CompareTo(CharacterEvent other) => this.StarDate.CompareTo(other.StarDate);
  }
}
