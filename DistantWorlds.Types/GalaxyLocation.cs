// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyLocation
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxyLocation : ISerializable, IComparable<GalaxyLocation>
  {
    private float _Xpos;
    private float _Ypos;
    private float _Width;
    private float _Height;
    private string _Name;
    private bool _ShowName;
    private GalaxyLocationType _Type;
    private GalaxyLocationEffectType _Effect;
    private short _PictureRef;
    private string _Message;
    private double _EffectAmount;
    private int _EffectRandomSeed;
    private GalaxyLocationShape _Shape;
    private short _SoundScheme = -1;
    private BuiltObject _RelatedBuiltObject;
    private Race _RelatedRace;
    private CreatureList _RelatedCreatures = new CreatureList();

    protected GalaxyLocation(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this._Xpos = binaryReader.ReadSingle();
          this._Ypos = binaryReader.ReadSingle();
          this._Width = binaryReader.ReadSingle();
          this._Height = binaryReader.ReadSingle();
          this._Name = binaryReader.ReadString();
          this._ShowName = binaryReader.ReadBoolean();
          this._Type = (GalaxyLocationType) binaryReader.ReadByte();
          this._Effect = (GalaxyLocationEffectType) binaryReader.ReadByte();
          this._PictureRef = binaryReader.ReadInt16();
          this._Message = binaryReader.ReadString();
          this._EffectAmount = binaryReader.ReadDouble();
          this._EffectRandomSeed = binaryReader.ReadInt32();
          this._Shape = (GalaxyLocationShape) binaryReader.ReadByte();
          this._SoundScheme = binaryReader.ReadInt16();
          binaryReader.Close();
        }
      }
      this._RelatedBuiltObject = (BuiltObject) info.GetValue("BO", typeof (BuiltObject));
      this._RelatedRace = (Race) info.GetValue("Ra", typeof (Race));
      this._RelatedCreatures = (CreatureList) info.GetValue("CrL", typeof (CreatureList));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write(this._Xpos);
          binaryWriter.Write(this._Ypos);
          binaryWriter.Write(this._Width);
          binaryWriter.Write(this._Height);
          binaryWriter.Write(this._Name);
          binaryWriter.Write(this._ShowName);
          binaryWriter.Write((byte) this._Type);
          binaryWriter.Write((byte) this._Effect);
          binaryWriter.Write(this._PictureRef);
          binaryWriter.Write(this._Message);
          binaryWriter.Write(this._EffectAmount);
          binaryWriter.Write(this._EffectRandomSeed);
          binaryWriter.Write((byte) this._Shape);
          binaryWriter.Write(this._SoundScheme);
          binaryWriter.Flush();
          binaryWriter.Close();
          byte[] array = output.ToArray();
          info.AddValue("D", (object) array);
        }
      }
      info.AddValue("BO", (object) this._RelatedBuiltObject);
      info.AddValue("Ra", (object) this._RelatedRace);
      info.AddValue("CrL", (object) this._RelatedCreatures);
    }

    public GalaxyLocation(
      string name,
      GalaxyLocationType type,
      double x,
      double y,
      double width,
      double height,
      int pictureRef)
    {
      this._Name = name;
      this._ShowName = false;
      this._Type = type;
      this._Effect = GalaxyLocationEffectType.None;
      this._Message = string.Empty;
      this._Xpos = (float) x;
      this._Ypos = (float) y;
      this._Width = (float) width;
      this._Height = (float) height;
      this._PictureRef = (short) pictureRef;
      this._RelatedBuiltObject = (BuiltObject) null;
      this._EffectAmount = 0.0;
    }

    public void ResolveLocationCenter(out double x, out double y)
    {
      x = (double) this.Xpos + (double) this.Width / 2.0;
      y = (double) this.Ypos + (double) this.Height / 2.0;
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

    public float Width
    {
      get => this._Width;
      set => this._Width = value;
    }

    public float Height
    {
      get => this._Height;
      set => this._Height = value;
    }

    public string Name
    {
      get => this._Name;
      set => this._Name = value;
    }

    public bool ShowName
    {
      get => this._ShowName;
      set => this._ShowName = value;
    }

    public short PictureRef
    {
      get => this._PictureRef;
      set => this._PictureRef = value;
    }

    public GalaxyLocationType Type
    {
      get => this._Type;
      set => this._Type = value;
    }

    public GalaxyLocationShape Shape
    {
      get => this._Shape;
      set => this._Shape = value;
    }

    public GalaxyLocationEffectType Effect
    {
      get => this._Effect;
      set => this._Effect = value;
    }

    public double EffectAmount
    {
      get => this._EffectAmount;
      set => this._EffectAmount = value;
    }

    public int EffectRandomSeed
    {
      get => this._EffectRandomSeed;
      set => this._EffectRandomSeed = value;
    }

    public string Message
    {
      get => this._Message;
      set => this._Message = value;
    }

    public BuiltObject RelatedBuiltObject
    {
      get => this._RelatedBuiltObject;
      set => this._RelatedBuiltObject = value;
    }

    public Race RelatedRace
    {
      get => this._RelatedRace;
      set => this._RelatedRace = value;
    }

    public CreatureList RelatedCreatures
    {
      get => this._RelatedCreatures;
      set => this._RelatedCreatures = value;
    }

    public short SoundScheme
    {
      get => this._SoundScheme;
      set => this._SoundScheme = value;
    }

    int IComparable<GalaxyLocation>.CompareTo(GalaxyLocation other) => this.Name.CompareTo(other.Name);
  }
}
