// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Number
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public struct Number
  {
    private int _value;
    private string _rep;

    internal Number(int value, string rep)
    {
      this._value = value;
      this._rep = rep;
    }

    public int Value => this._value;

    public string Rep => this._rep;
  }
}
