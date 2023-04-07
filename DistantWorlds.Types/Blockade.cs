// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Blockade
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Blockade
  {
    private Habitat _Colony;
    private BuiltObject _BuiltObject;
    private Empire _Initiator;
    private Empire _BlockadedEmpire;
    private long _DateInitiated;
    private bool _TargetIsColony;

    public Blockade(BuiltObject builtObject, Empire initiator, long dateInitiated)
    {
      this._BuiltObject = builtObject;
      this._TargetIsColony = false;
      this._Colony = (Habitat) null;
      this._BlockadedEmpire = this._BuiltObject.Empire;
      this._Initiator = initiator;
      this._DateInitiated = dateInitiated;
    }

    public Blockade(Habitat colony, Empire initiator, long dateInitiated)
    {
      this._Colony = colony;
      this._TargetIsColony = true;
      this._BuiltObject = (BuiltObject) null;
      this._BlockadedEmpire = this._Colony.Empire;
      this._Initiator = initiator;
      this._DateInitiated = dateInitiated;
    }

    public bool TargetIsColony => this._TargetIsColony;

    public Habitat Colony => this._Colony;

    public BuiltObject BuiltObject => this._BuiltObject;

    public Empire Initiator => this._Initiator;

    public Empire BlockadedEmpire => this._BlockadedEmpire;

    public long DateInitiated => this._DateInitiated;
  }
}
