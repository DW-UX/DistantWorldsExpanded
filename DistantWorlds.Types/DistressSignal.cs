// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.DistressSignal
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class DistressSignal : IComparable<DistressSignal>
  {
    private BuiltObject _SourceBuiltObject;
    private Habitat _SourceHabitat;
    private long _Date;
    private DistressSignalType _Type;
    private Empire _Attacker;
    public int AttackStrength;

    public DistressSignal(BuiltObject source, DistressSignalType type, long date)
    {
      this._SourceBuiltObject = source;
      this._SourceHabitat = (Habitat) null;
      this._Type = type;
      this._Date = date;
    }

    public DistressSignal(Habitat source, DistressSignalType type, long date)
    {
      this._SourceBuiltObject = (BuiltObject) null;
      this._SourceHabitat = source;
      this._Type = type;
      this._Date = date;
    }

    public Empire Attacker
    {
      get => this._Attacker;
      set => this._Attacker = value;
    }

    public long Date
    {
      get => this._Date;
      set => this._Date = value;
    }

    public DistressSignalType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public object Source
    {
      get
      {
        if (this._SourceBuiltObject != null)
          return (object) this._SourceBuiltObject;
        return this._SourceHabitat != null ? (object) this._SourceHabitat : (object) null;
      }
    }

    int IComparable<DistressSignal>.CompareTo(DistressSignal other) => this.Date.CompareTo(other.Date);
  }
}
