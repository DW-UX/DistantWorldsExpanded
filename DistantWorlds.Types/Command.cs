// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Command
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Command : ISerializable
  {
    private CommandAction _Action;
    private float _Xpos;
    private float _Ypos;
    private float _TargetRelativeXpos;
    private float _TargetRelativeYpos;
    private long _StarDate;
    private StellarObject _TargetStellarObject;
    private ShipGroup _TargetShipGroup;
    private CargoList _Commodities;
    private Design _Design;
    private TroopList _Troops;
    private PopulationList _Population;

    public Command()
    {
    }

    public Command(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._Action = (CommandAction) binaryReader.ReadByte();
          this._Xpos = binaryReader.ReadSingle();
          this._Ypos = binaryReader.ReadSingle();
          this._TargetRelativeXpos = binaryReader.ReadSingle();
          this._TargetRelativeYpos = binaryReader.ReadSingle();
          this._StarDate = binaryReader.ReadInt64();
          binaryReader.Close();
        }
      }
      this._TargetStellarObject = (StellarObject) info.GetValue("StO", typeof (StellarObject));
      this._TargetShipGroup = (ShipGroup) info.GetValue("SG", typeof (ShipGroup));
      this._Commodities = (CargoList) info.GetValue("Cg", typeof (CargoList));
      this._Design = (Design) info.GetValue("De", typeof (Design));
      this._Troops = (TroopList) info.GetValue("Tr", typeof (TroopList));
      this._Population = (PopulationList) info.GetValue("Po", typeof (PopulationList));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write((byte) this._Action);
          binaryWriter.Write(this._Xpos);
          binaryWriter.Write(this._Ypos);
          binaryWriter.Write(this._TargetRelativeXpos);
          binaryWriter.Write(this._TargetRelativeYpos);
          binaryWriter.Write(this._StarDate);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("StO", (object) this._TargetStellarObject);
      info.AddValue("SG", (object) this._TargetShipGroup);
      info.AddValue("Cg", (object) this._Commodities);
      info.AddValue("De", (object) this._Design);
      info.AddValue("Tr", (object) this._Troops);
      info.AddValue("Po", (object) this._Population);
    }

    public Command Clone() => new Command(this._Action, this._Xpos, this._Ypos, this._TargetRelativeXpos, this._TargetRelativeYpos, this._StarDate, this._TargetStellarObject, this._TargetShipGroup, this._Commodities, this._Design, this._Troops, this._Population);

    public Command(
      CommandAction action,
      float x,
      float y,
      float relativeX,
      float relativeY,
      long starDate,
      StellarObject targetStellarObject,
      ShipGroup targetShipGroup,
      CargoList cargoList,
      Design design,
      TroopList troops,
      PopulationList population)
    {
      this._Action = action;
      this._Xpos = x;
      this._Ypos = y;
      this._TargetRelativeXpos = relativeX;
      this._TargetRelativeYpos = relativeY;
      this._StarDate = starDate;
      this._TargetStellarObject = targetStellarObject;
      this._TargetShipGroup = targetShipGroup;
      this._Commodities = cargoList;
      this._Design = design;
      this._Troops = troops;
      this._Population = population;
    }

    public Command(CommandAction action)
    {
      this._Action = action;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, CargoList cargoList)
    {
      this._Action = action;
      this._Commodities = cargoList;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, BuiltObject target)
    {
      this._Action = action;
      this.TargetBuiltObject = target;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, Habitat target)
    {
      this._Action = action;
      this.TargetHabitat = target;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, ShipGroup target)
    {
      this._Action = action;
      this.TargetShipGroup = target;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, Creature target)
    {
      this._Action = action;
      this.TargetCreature = target;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, long starDate)
    {
      this._Action = action;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = starDate;
    }

    public Command(CommandAction action, Design design)
    {
      this._Action = action;
      this._Design = design;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, TroopList troops)
    {
      this._Action = action;
      this._Troops = troops;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, PopulationList population)
    {
      this._Action = action;
      this._Population = population;
      this._Xpos = -2.00000013E+09f;
      this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public Command(CommandAction action, double x, double y)
    {
      this._Action = action;
      this._Xpos = (float) x;
      this._Ypos = (float) y;
      if (x <= -2000000001.0)
        this._Xpos = -2.00000013E+09f;
      if (y <= -2000000001.0)
        this._Ypos = -2.00000013E+09f;
      this._StarDate = -1L;
    }

    public float Xpos
    {
      get => this._Xpos;
      set => this._Xpos = value;
    }

    public float Ypos
    {
      get => this._Ypos;
      set => this._Ypos = value;
    }

    public float TargetRelativeXpos
    {
      get => this._TargetRelativeXpos;
      set
      {
        this._TargetRelativeXpos = value;
        if ((double) value > -2000000000.0)
          return;
        this._TargetRelativeXpos = -2.00000013E+09f;
      }
    }

    public float TargetRelativeYpos
    {
      get => this._TargetRelativeYpos;
      set
      {
        this._TargetRelativeYpos = value;
        if ((double) value > -2000000000.0)
          return;
        this._TargetRelativeYpos = -2.00000013E+09f;
      }
    }

    public CommandAction Action
    {
      get => this._Action;
      set => this._Action = value;
    }

    public CargoList Commodities
    {
      get => this._Commodities;
      set => this._Commodities = value;
    }

    public TroopList Troops
    {
      get => this._Troops;
      set => this._Troops = value;
    }

    public PopulationList Population
    {
      get => this._Population;
      set => this._Population = value;
    }

    public Design Design
    {
      get => this._Design;
      set => this._Design = value;
    }

    public long StarDate
    {
      get => this._StarDate;
      set => this._StarDate = value;
    }

    public BuiltObject TargetBuiltObject
    {
      get => this._TargetStellarObject is BuiltObject ? (BuiltObject) this._TargetStellarObject : (BuiltObject) null;
      set
      {
        this._TargetStellarObject = (StellarObject) value;
        this._TargetShipGroup = (ShipGroup) null;
      }
    }

    public Creature TargetCreature
    {
      get => this._TargetStellarObject is Creature ? (Creature) this._TargetStellarObject : (Creature) null;
      set
      {
        this._TargetStellarObject = (StellarObject) value;
        this._TargetShipGroup = (ShipGroup) null;
      }
    }

    public Habitat TargetHabitat
    {
      get => this._TargetStellarObject is Habitat ? (Habitat) this._TargetStellarObject : (Habitat) null;
      set
      {
        this._TargetStellarObject = (StellarObject) value;
        this._TargetShipGroup = (ShipGroup) null;
      }
    }

    public ShipGroup TargetShipGroup
    {
      get => this._TargetShipGroup;
      set
      {
        this._TargetStellarObject = (StellarObject) null;
        this._TargetShipGroup = value;
      }
    }
  }
}
