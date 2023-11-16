// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxySummary
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;
using System.IO;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxySummary : IComparable<GalaxySummary>
  {
    public int StarCount;
    public int SectorWidth;
    public int SectorHeight;
    public GalaxyShape Shape;
    public bool GeneratedResources;
    public bool GeneratedRuins;
    public bool GeneratedAbandonedShips;
    public bool GeneratedSpecialLocations;
    public bool GeneratedEmpires;
    public bool GeneratedPirates;
    public bool GeneratedShipsAndBases;
    public bool GeneratedSpaceCreatures;
    public string Title;
    public string Description;
    [NonSerialized]
    public string Filename = string.Empty;
    [NonSerialized]
    public string Filepath = string.Empty;
    public static readonly string ThemeNameSuffixIntro = "#######THEMENAME#######";
    [NonSerialized]
    public string ThemeName = string.Empty;
    public EmpireSummaryList EmpireSummaries = new EmpireSummaryList();

    public static string ResolveThemeNameFromTitle(string title, out string actualTruncatedTitle)
    {
      actualTruncatedTitle = title;
      string str = string.Empty;
      int length1 = title.IndexOf(GalaxySummary.ThemeNameSuffixIntro, 0);
      if (length1 >= 0)
      {
        int length2 = title.Length - (length1 + GalaxySummary.ThemeNameSuffixIntro.Length);
        if (length2 > 0)
        {
          str = title.Substring(length1 + GalaxySummary.ThemeNameSuffixIntro.Length, length2);
          actualTruncatedTitle = title.Substring(0, length1);
        }
      }
      return str;
    }

    public static string BuildTitleWithThemeName(string title, string themeName)
    {
      if (!string.IsNullOrEmpty(themeName))
        title = title + GalaxySummary.ThemeNameSuffixIntro + themeName;
      return title;
    }

    public static GalaxySummary ReadGalaxySummary(Stream stream)
    {
      GalaxySummary galaxySummary = new GalaxySummary();
      BinaryReader binaryReader = new BinaryReader(stream);
      galaxySummary.StarCount = binaryReader.ReadInt32();
      galaxySummary.SectorWidth = binaryReader.ReadInt32();
      galaxySummary.SectorHeight = binaryReader.ReadInt32();
      galaxySummary.Shape = (GalaxyShape) binaryReader.ReadInt32();
      galaxySummary.GeneratedResources = binaryReader.ReadBoolean();
      galaxySummary.GeneratedRuins = binaryReader.ReadBoolean();
      galaxySummary.GeneratedAbandonedShips = binaryReader.ReadBoolean();
      galaxySummary.GeneratedSpecialLocations = binaryReader.ReadBoolean();
      galaxySummary.GeneratedEmpires = binaryReader.ReadBoolean();
      galaxySummary.GeneratedPirates = binaryReader.ReadBoolean();
      galaxySummary.GeneratedShipsAndBases = binaryReader.ReadBoolean();
      galaxySummary.GeneratedSpaceCreatures = binaryReader.ReadBoolean();
      galaxySummary.Title = binaryReader.ReadString();
      galaxySummary.Description = binaryReader.ReadString();
      string actualTruncatedTitle = galaxySummary.Title;
      string str = GalaxySummary.ResolveThemeNameFromTitle(galaxySummary.Title, out actualTruncatedTitle);
      galaxySummary.ThemeName = str;
      galaxySummary.Title = actualTruncatedTitle;
      int num = binaryReader.ReadInt32();
      for (int index = 0; index < num; ++index)
      {
        EmpireSummary empireSummary = new EmpireSummary();
        empireSummary.EmpireId = binaryReader.ReadInt32();
        empireSummary.Name = binaryReader.ReadString();
        empireSummary.GovernmentId = binaryReader.ReadInt32();
        empireSummary.RaceName = binaryReader.ReadString();
        empireSummary.IsPirateFaction = binaryReader.ReadBoolean();
        empireSummary.PiratePlayStyle = (PiratePlayStyle) binaryReader.ReadByte();
        empireSummary.ColonyCount = binaryReader.ReadInt32();
        empireSummary.SystemCount = binaryReader.ReadInt32();
        empireSummary.PopulationAmount = binaryReader.ReadInt64();
        empireSummary.SpaceportCount = binaryReader.ReadInt32();
        empireSummary.Money = binaryReader.ReadDouble();
        empireSummary.Cashflow = binaryReader.ReadDouble();
        int alpha1 = (int) binaryReader.ReadByte();
        int red1 = (int) binaryReader.ReadByte();
        int green1 = (int) binaryReader.ReadByte();
        int blue1 = (int) binaryReader.ReadByte();
        empireSummary.MainColor = Color.FromArgb(alpha1, red1, green1, blue1);
        int alpha2 = (int) binaryReader.ReadByte();
        int red2 = (int) binaryReader.ReadByte();
        int green2 = (int) binaryReader.ReadByte();
        int blue2 = (int) binaryReader.ReadByte();
        empireSummary.SecondaryColor = Color.FromArgb(alpha2, red2, green2, blue2);
        empireSummary.FlagIndex = binaryReader.ReadInt32();
        empireSummary.Description = binaryReader.ReadString();
        galaxySummary.EmpireSummaries.Add(empireSummary);
      }
      return galaxySummary;
    }

    public static void WriteGalaxySummary(Stream stream, GalaxySummary summary, string themeName)
    {
      BinaryWriter binaryWriter = new BinaryWriter(stream);
      binaryWriter.Write(summary.StarCount);
      binaryWriter.Write(summary.SectorWidth);
      binaryWriter.Write(summary.SectorHeight);
      binaryWriter.Write((int) summary.Shape);
      binaryWriter.Write(summary.GeneratedResources);
      binaryWriter.Write(summary.GeneratedRuins);
      binaryWriter.Write(summary.GeneratedAbandonedShips);
      binaryWriter.Write(summary.GeneratedSpecialLocations);
      binaryWriter.Write(summary.GeneratedEmpires);
      binaryWriter.Write(summary.GeneratedPirates);
      binaryWriter.Write(summary.GeneratedShipsAndBases);
      binaryWriter.Write(summary.GeneratedSpaceCreatures);
      summary.Title = GalaxySummary.BuildTitleWithThemeName(summary.Title, themeName);
      binaryWriter.Write(summary.Title);
      binaryWriter.Write(summary.Description);
      binaryWriter.Write(summary.EmpireSummaries.Count);
      for (int index = 0; index < summary.EmpireSummaries.Count; ++index)
      {
        EmpireSummary empireSummary = summary.EmpireSummaries[index];
        if (empireSummary != null)
        {
          binaryWriter.Write(empireSummary.EmpireId);
          binaryWriter.Write(empireSummary.Name);
          binaryWriter.Write(empireSummary.GovernmentId);
          binaryWriter.Write(empireSummary.RaceName);
          binaryWriter.Write(empireSummary.IsPirateFaction);
          binaryWriter.Write((byte) empireSummary.PiratePlayStyle);
          binaryWriter.Write(empireSummary.ColonyCount);
          binaryWriter.Write(empireSummary.SystemCount);
          binaryWriter.Write(empireSummary.PopulationAmount);
          binaryWriter.Write(empireSummary.SpaceportCount);
          binaryWriter.Write(empireSummary.Money);
          binaryWriter.Write(empireSummary.Cashflow);
          binaryWriter.Write(empireSummary.MainColor.A);
          binaryWriter.Write(empireSummary.MainColor.R);
          binaryWriter.Write(empireSummary.MainColor.G);
          binaryWriter.Write(empireSummary.MainColor.B);
          binaryWriter.Write(empireSummary.SecondaryColor.A);
          binaryWriter.Write(empireSummary.SecondaryColor.R);
          binaryWriter.Write(empireSummary.SecondaryColor.G);
          binaryWriter.Write(empireSummary.SecondaryColor.B);
          binaryWriter.Write(empireSummary.FlagIndex);
          binaryWriter.Write(empireSummary.Description);
        }
      }
    }

    int IComparable<GalaxySummary>.CompareTo(GalaxySummary other)
    {
      if (!string.IsNullOrEmpty(this.Title))
      {
        if (!string.IsNullOrEmpty(other.Title))
          return this.Title.CompareTo(other.Title);
        return !string.IsNullOrEmpty(other.Filename) ? this.Title.CompareTo(other.Filename) : this.StarCount.CompareTo(other.StarCount);
      }
      if (string.IsNullOrEmpty(this.Filename))
        return this.StarCount.CompareTo(other.StarCount);
      if (!string.IsNullOrEmpty(other.Title))
        return this.Filename.CompareTo(other.Title);
      return !string.IsNullOrEmpty(other.Filename) ? this.Filename.CompareTo(other.Filename) : this.StarCount.CompareTo(other.StarCount);
    }
  }
}
