// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Hotspot
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Hotspot
  {
    private Rectangle _Region;
    private string _HoverMessage;
    private object _RelatedObject;
    private bool _Hovered;

    public Hotspot(Rectangle region, object relatedObject)
    {
      this._Region = region;
      this._RelatedObject = relatedObject;
    }

    public Hotspot(Rectangle region, object relatedObject, string message)
    {
      this._Region = region;
      this._RelatedObject = relatedObject;
      this._HoverMessage = message;
    }

    public Rectangle Region
    {
      get => this._Region;
      set => this._Region = value;
    }

    public string HoverMessage
    {
      get => this._HoverMessage;
      set => this._HoverMessage = value;
    }

    public bool Hovered
    {
      get => this._Hovered;
      set => this._Hovered = value;
    }

    public object RelatedObject
    {
      get => this._RelatedObject;
      set
      {
        switch (value)
        {
          case BuiltObject _:
          case Fighter _:
          case Habitat _:
          case Creature _:
          case ShipGroup _:
          case Resource _:
          case Design _:
            this._RelatedObject = value;
            break;
        }
      }
    }
  }
}
