// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ShipAction
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ShipAction
  {
    private BuiltObjectMissionType _MissionType;
    private ShipActionType _ActionType;
    private object _Target;
    private object _Target2;
    private Point _Position;
    private Design _Design;
    private bool _IsSubsequentAction;
    public bool Enabled = true;
    public string Hint;
    public string ExtraData;

    public ShipAction(BuiltObjectMissionType missionType, object target, object target2)
    {
      this._MissionType = missionType;
      this._ActionType = ShipActionType.Undefined;
      this._Target = target;
      this._Target2 = target2;
      this._Position = new Point(0, 0);
      this._Design = (Design) null;
    }

    public ShipAction(ShipActionType actionType, object target)
    {
      this._MissionType = BuiltObjectMissionType.Undefined;
      this._ActionType = actionType;
      this._Target = target;
      this._Target2 = (object) null;
      this._Position = new Point(0, 0);
      this._Design = (Design) null;
    }

    public ShipAction(
      BuiltObjectMissionType missionType,
      object target,
      Point offset,
      Design design)
    {
      this._MissionType = missionType;
      this._ActionType = ShipActionType.Undefined;
      this._Target = target;
      this._Target2 = (object) null;
      this._Position = offset;
      this._Design = design;
    }

    public ShipAction(BuiltObjectMissionType missionType, object target)
    {
      this._MissionType = missionType;
      this._ActionType = ShipActionType.Undefined;
      this._Target = target;
      this._Target2 = (object) null;
      this._Position = new Point(0, 0);
      this._Design = (Design) null;
    }

    public BuiltObjectMissionType MissionType => this._MissionType;

    public void SetMissionType(BuiltObjectMissionType missionType) => this._MissionType = missionType;

    public ShipActionType ActionType
    {
      get => this._ActionType;
      set => this._ActionType = value;
    }

    public object Target
    {
      get => this._Target;
      set => this._Target = value;
    }

    public object Target2
    {
      get => this._Target2;
      set => this._Target2 = value;
    }

    public Point Position
    {
      get => this._Position;
      set => this._Position = value;
    }

    public Design Design
    {
      get => this._Design;
      set => this._Design = value;
    }

    public bool IsSubsequentAction
    {
      get => this._IsSubsequentAction;
      set => this._IsSubsequentAction = value;
    }

    public ShipAction Clone() => new ShipAction(this._MissionType, this._Target, this._Position, this._Design)
    {
      ActionType = this._ActionType,
      IsSubsequentAction = this._IsSubsequentAction
    };
  }
}
