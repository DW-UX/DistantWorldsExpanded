// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Plague
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Plague : IComparable<Plague>
  {
    public byte PlagueId;
    public string Name;
    public int PictureRef;
    public double MortalityRate;
    public int InfectionChance;
    public float Duration;
    public bool CanCompletelyEliminatePopulation;
    public int NaturalOccurrenceLevel;
    public string ExceptionRaceName;
    public double ExceptionMortalityRate;
    public int ExceptionInfectionChance;
    public float ExceptionDuration;
    public int SpecialFunctionCode;
    public string Description;
    public int LatestTechLevelUpdate;
    [NonSerialized]
    public float SortTag;

    public Plague(
      byte plagueId,
      string name,
      int pictureRef,
      double mortalityRate,
      int infectionChance,
      float duration)
    {
      this.PlagueId = plagueId;
      this.Name = name;
      this.PictureRef = pictureRef;
      this.MortalityRate = mortalityRate;
      this.InfectionChance = infectionChance;
      this.Duration = duration;
    }

    int IComparable<Plague>.CompareTo(Plague other) => (double) this.SortTag > 0.0 || (double) other.SortTag > 0.0 ? this.SortTag.CompareTo(other.SortTag) : this.PlagueId.CompareTo(other.PlagueId);
  }
}
