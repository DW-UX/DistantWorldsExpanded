// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GameSummary
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GameSummary : IComparable<GameSummary>
  {
    public int GalaxyStarCount;
    public double DifficultyLevel;
    public Race PlayerRace;
    public string PlayerGovernmentName;
    public string PlayerEmpireName;
    public Color PlayerMainColor;
    public int PlayerScore;
    [OptionalField]
    public bool PlayerVictory;
    public AchievementList PlayerAchievements = new AchievementList();

    int IComparable<GameSummary>.CompareTo(GameSummary other) => this.PlayerScore.CompareTo(other.PlayerScore);
  }
}
