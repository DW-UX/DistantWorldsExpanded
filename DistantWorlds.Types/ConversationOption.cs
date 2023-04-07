// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ConversationOption
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ConversationOption
  {
    private DialogPartType _Type;
    private string _Text;
    private object _RelatedInfo;
    private double _Cost;
    private Empire _Initiator;
    public DialogPartType ReroutedType;

    public ConversationOption(DialogPartType type, string text, Empire initiator)
    {
      this._Type = type;
      this._Text = text;
      this._RelatedInfo = (object) null;
      this._Cost = 0.0;
      this._Initiator = initiator;
    }

    public ConversationOption(
      DialogPartType type,
      string text,
      object relatedInfo,
      double cost,
      Empire initiator)
    {
      this._Type = type;
      this._Text = text;
      this._RelatedInfo = relatedInfo;
      this._Cost = cost;
      this._Initiator = initiator;
    }

    public DialogPartType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public string Text
    {
      get => this._Text;
      set => this._Text = value;
    }

    public object RelatedInfo
    {
      get => this._RelatedInfo;
      set => this._RelatedInfo = value;
    }

    public double Cost
    {
      get => this._Cost;
      set => this._Cost = value;
    }

    public Empire Initiator
    {
      get => this._Initiator;
      set => this._Initiator = value;
    }
  }
}
