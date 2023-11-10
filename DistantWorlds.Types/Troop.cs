// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Troop
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Troop : IComparable<Troop>, ISerializable
  {
    private string _Name;
    private TroopType _Type;
    private short _AttackStrength;
    private short _DefendStrength;
    private short _Size;
    private bool _Garrisoned;
    private float _Readiness;
    private Empire _Empire;
    private bool _AwaitingPickup;
    private bool _AtColony;
    private BuiltObject _BuiltObject;
    private Habitat _Colony;
    private Race _Race;
    private int _PictureRef;
    private float _MaintenanceMultiplier = 1f;

    public Troop()
    {
    }

    public Troop(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._Name = binaryReader.ReadString();
          this._Type = (TroopType) binaryReader.ReadByte();
          this._AttackStrength = binaryReader.ReadInt16();
          this._DefendStrength = binaryReader.ReadInt16();
          this._Size = binaryReader.ReadInt16();
          this._Garrisoned = binaryReader.ReadBoolean();
          this._Readiness = binaryReader.ReadSingle();
          this._AwaitingPickup = binaryReader.ReadBoolean();
          this._PictureRef = (int) binaryReader.ReadInt16();
          this._MaintenanceMultiplier = binaryReader.ReadSingle();
          binaryReader.Close();
        }
      }
      this._Empire = (Empire) info.GetValue("Em", typeof (Empire));
      this._BuiltObject = (BuiltObject) info.GetValue("BO", typeof (BuiltObject));
      this._Colony = (Habitat) info.GetValue("Ha", typeof (Habitat));
      this._Race = (Race) info.GetValue("Ra", typeof (Race));
      if (this.Colony == null)
        return;
      this._AtColony = true;
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this._Name);
          binaryWriter.Write((byte) this._Type);
          binaryWriter.Write(this._AttackStrength);
          binaryWriter.Write(this._DefendStrength);
          binaryWriter.Write(this._Size);
          binaryWriter.Write(this._Garrisoned);
          binaryWriter.Write(this._Readiness);
          binaryWriter.Write(this._AwaitingPickup);
          binaryWriter.Write((short) this._PictureRef);
          binaryWriter.Write(this._MaintenanceMultiplier);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("Em", (object) this._Empire);
      info.AddValue("BO", (object) this._BuiltObject);
      info.AddValue("Ha", (object) this._Colony);
      info.AddValue("Ra", (object) this._Race);
    }

    public Troop(
      string name,
      TroopType type,
      int attackStrength,
      int defendStrength,
      int size,
      float readiness,
      Empire empire,
      Race race)
    {
      this._Name = name;
      this._Type = type;
      this._AttackStrength = (short) attackStrength;
      this._DefendStrength = (short) defendStrength;
      this._Size = (short) size;
      this._Readiness = readiness;
      this._Empire = empire;
      this._Race = race;
      this._AwaitingPickup = false;
      this._MaintenanceMultiplier = 1f;
    }

    public int AttackStrength => (int) this._AttackStrength;

    public void SetAttackStrength(int attackStrength) => this._AttackStrength = (short) attackStrength;

    public int DefendStrength => (int) this._DefendStrength;

    public void SetDefendStrength(int defendStrength) => this._DefendStrength = (short) defendStrength;

    public int Size => (int) this._Size;

    public TroopType Type => this._Type;

    public bool Garrisoned
    {
      get => this._Garrisoned;
      set => this._Garrisoned = value;
    }

    public bool AtColony => this._AtColony;

    public BuiltObject BuiltObject
    {
      get => this._BuiltObject;
      set
      {
        this._BuiltObject = value;
        if (this._BuiltObject == null)
          return;
        this._AtColony = false;
        this._Colony = (Habitat) null;
      }
    }

    public Habitat Colony
    {
      get => this._Colony;
      set
      {
        this._Colony = value;
        if (this._Colony == null)
          return;
        this._AtColony = true;
        this._BuiltObject = (BuiltObject) null;
      }
    }

    public bool BeingRecruited => this._AtColony && this._Colony != null && this._Colony.TroopsToRecruit != null && this._Colony.TroopsToRecruit.Contains(this);

    public float Readiness
    {
      get => this._Readiness;
      set => this._Readiness = value;
    }

    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    public int PictureRef
    {
      get => this._PictureRef;
      set => this._PictureRef = value;
    }

    public float MaintenanceMultiplier
    {
      get => this._MaintenanceMultiplier;
      set => this._MaintenanceMultiplier = value;
    }

    public Empire Empire
    {
      get => this._Empire;
      set => this._Empire = value;
    }

    public Race Race
    {
      get => this._Race;
      set => this._Race = value;
    }

    public bool AwaitingPickup
    {
      get => this._AwaitingPickup;
      set => this._AwaitingPickup = value;
    }

    public double OverallAttackStrength => (double) this._AttackStrength * (double) this._Readiness;

    public double OverallDefendStrength => (double) this._DefendStrength * (double) this._Readiness;

    public double OverallDefendStrengthExcludeReadiness => (double) this._DefendStrength * 100.0;

    public int CompareTo(Troop other) => this.OverallAttackStrength.CompareTo(other.OverallAttackStrength);
  }
}
