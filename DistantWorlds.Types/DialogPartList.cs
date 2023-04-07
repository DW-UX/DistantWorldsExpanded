// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DialogPartList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DialogPartList : List<DialogPart>
  {
    private Race _Race;

    public Race Race
    {
      get => this._Race;
      set => this._Race = value;
    }

    public DialogPart this[DialogPartType type]
    {
      get
      {
        foreach (DialogPart dialogPart in (List<DialogPart>) this)
        {
          if (dialogPart.Type == type)
            return dialogPart;
        }
        return (DialogPart) null;
      }
    }
  }
}
