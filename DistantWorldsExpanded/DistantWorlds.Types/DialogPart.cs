// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DialogPart
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DialogPart
  {
    private DialogPartType _Type;
    private string _Dialog;

    public DialogPart(DialogPartType type, string dialog)
    {
      this._Type = type;
      this._Dialog = dialog;
    }

    public DialogPartType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public string Dialog
    {
      get => this._Dialog;
      set => this._Dialog = value;
    }
  }
}
