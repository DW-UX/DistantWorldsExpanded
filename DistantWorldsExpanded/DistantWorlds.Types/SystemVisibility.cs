// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.SystemVisibility
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class SystemVisibility : ISerializable
  {
    public Habitat SystemStar;
    public SystemVisibilityStatus Status;
    public bool TotallyExplored;
    public bool IsRefuellingPoint;
    public int EmpireStrength;
    public bool FuelSourcesFinalized;
    [NonSerialized]
    private BuiltObjectList _Threats = new BuiltObjectList();
    [NonSerialized]
    private List<int> _ThreatLevels = new List<int>();
    [NonSerialized]
    private DateTime _LatestThreatEvaluation = DateTime.MinValue;
    [NonSerialized]
    private HabitatList _LinkSystemStars = new HabitatList();
    [NonSerialized]
    private HabitatList _ReciprocalLinkSystemStars = new HabitatList();

    public SystemVisibility()
    {
    }

    public SystemVisibility(SerializationInfo info, StreamingContext context)
      : this()
    {
      using (MemoryStream input = new MemoryStream((byte[]) info.GetValue("D", typeof (byte[]))))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          this.Status = (SystemVisibilityStatus) binaryReader.ReadByte();
          this.TotallyExplored = binaryReader.ReadBoolean();
          this.IsRefuellingPoint = binaryReader.ReadBoolean();
          this.EmpireStrength = binaryReader.ReadInt32();
          this.FuelSourcesFinalized = binaryReader.ReadBoolean();
          binaryReader.Close();
        }
      }
      this.SystemStar = (Habitat) info.GetValue("Star", typeof (Habitat));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          binaryWriter.Write((byte) this.Status);
          binaryWriter.Write(this.TotallyExplored);
          binaryWriter.Write(this.IsRefuellingPoint);
          binaryWriter.Write(this.EmpireStrength);
          binaryWriter.Write(this.FuelSourcesFinalized);
          binaryWriter.Flush();
          binaryWriter.Close();
          info.AddValue("D", (object) output.ToArray());
        }
      }
      info.AddValue("Star", (object) this.SystemStar);
    }

    public HabitatList ReciprocalLinkSystemStars
    {
      get => this._ReciprocalLinkSystemStars;
      set => this._ReciprocalLinkSystemStars = value;
    }

    public HabitatList LinkSystemStars
    {
      get => this._LinkSystemStars;
      set => this._LinkSystemStars = value;
    }

    public BuiltObjectList Threats
    {
      get => this._Threats;
      set => this._Threats = value;
    }

    public List<int> ThreatLevels
    {
      get => this._ThreatLevels;
      set => this._ThreatLevels = value;
    }

    public DateTime LatestThreatEvaluation
    {
      get => this._LatestThreatEvaluation;
      set => this._LatestThreatEvaluation = value;
    }
  }
}
