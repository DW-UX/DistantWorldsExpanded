// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameEndEventArgs
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GameEndEventArgs : EventArgs
  {
    private Empire _VictorEmpire;
    private GameEndOutcome _OutcomeForPlayer;
    private string _Description;
    public int Code;

    public GameEndEventArgs(
      Empire victorEmpire,
      GameEndOutcome outcomeForPlayer,
      string description,
      int code)
    {
      this._VictorEmpire = victorEmpire;
      this._OutcomeForPlayer = outcomeForPlayer;
      this._Description = description;
      this.Code = code;
    }

    public string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    public Empire VictorEmpire
    {
      get => this._VictorEmpire;
      set => this._VictorEmpire = value;
    }

    public GameEndOutcome OutcomeForPlayer
    {
      get => this._OutcomeForPlayer;
      set => this._OutcomeForPlayer = value;
    }
  }
}
