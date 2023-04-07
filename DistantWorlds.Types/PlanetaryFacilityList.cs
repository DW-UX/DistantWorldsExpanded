// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlanetaryFacilityList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.IO;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PlanetaryFacilityList : SyncList<PlanetaryFacility>, ISerializable
  {
    public PlanetaryFacilityList()
    {
    }

    public PlanetaryFacilityList(SerializationInfo info, StreamingContext context)
      : this()
    {
      byte[] buffer = (byte[]) info.GetValue("D", typeof (byte[]));
      if (buffer == null || buffer.Length <= 0)
        return;
      using (MemoryStream input = new MemoryStream(buffer))
      {
        using (BinaryReader binaryReader = new BinaryReader((Stream) input))
        {
          int num = buffer.Length / 6;
          for (int index = 0; index < num; ++index)
          {
            int planetaryFacilityDefinitionId = (int) binaryReader.ReadInt16();
            float constructionProgress = binaryReader.ReadSingle();
            if (planetaryFacilityDefinitionId >= 0)
              this.Add(new PlanetaryFacility(planetaryFacilityDefinitionId, constructionProgress));
            else
              this.Add((PlanetaryFacility) null);
          }
        }
      }
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      using (MemoryStream output = new MemoryStream())
      {
        using (BinaryWriter binaryWriter = new BinaryWriter((Stream) output))
        {
          if (this.Count > 0)
          {
            for (int index = 0; index < this.Count; ++index)
            {
              if (this[index] != null)
              {
                binaryWriter.Write((short) this[index].PlanetaryFacilityDefinitionId);
                binaryWriter.Write(this[index].ConstructionProgress);
              }
              else
              {
                binaryWriter.Write((short) -1);
                binaryWriter.Write(-1f);
              }
            }
            binaryWriter.Flush();
            binaryWriter.Close();
            info.AddValue("D", (object) output.ToArray());
          }
          else
            info.AddValue("D", (object) new byte[0]);
        }
      }
    }

    public PlanetaryFacility FindWonderByType(WonderType wonderType)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.Wonder && this[index].WonderType == wonderType)
          return this[index];
      }
      return (PlanetaryFacility) null;
    }

    public int CountWonderByType(WonderType wonderType)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.Wonder && this[index].WonderType == wonderType)
          ++num;
      }
      return num;
    }

    public int CountCompletedWonderByType(WonderType wonderType, int value2)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.Wonder && this[index].WonderType == wonderType && (double) this[index].ConstructionProgress >= 1.0 && this[index].Value2 == value2)
          ++num;
      }
      return num;
    }

    public bool CheckPirateBasesPresent()
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.PirateBase || this[index].Type == PlanetaryFacilityType.PirateFortress)
          return true;
      }
      return false;
    }

    public PlanetaryFacility FindByType(PlanetaryFacilityType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          return this[index];
      }
      return (PlanetaryFacility) null;
    }

    public int CountCompletedByType(PlanetaryFacilityType type, int value2)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type && (double) this[index].ConstructionProgress >= 1.0 && this[index].Value2 == value2)
          ++num;
      }
      return num;
    }

    public PlanetaryFacility FindCompletedByType(PlanetaryFacilityType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type && (double) this[index].ConstructionProgress >= 1.0)
          return this[index];
      }
      return (PlanetaryFacility) null;
    }

    public PlanetaryFacility FindBestCompletedPirateFacility(bool includeCriminalNetwork)
    {
      PlanetaryFacility completedPirateFacility = (PlanetaryFacility) null;
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacility planetaryFacility = this[index];
        if (planetaryFacility != null && (double) planetaryFacility.ConstructionProgress >= 1.0)
        {
          switch (planetaryFacility.Type)
          {
            case PlanetaryFacilityType.PirateBase:
            case PlanetaryFacilityType.PirateFortress:
              if (completedPirateFacility == null || planetaryFacility.Value2 > completedPirateFacility.Value2)
              {
                completedPirateFacility = planetaryFacility;
                continue;
              }
              continue;
            case PlanetaryFacilityType.PirateCriminalNetwork:
              if (includeCriminalNetwork && (completedPirateFacility == null || planetaryFacility.Value2 > completedPirateFacility.Value2))
              {
                completedPirateFacility = planetaryFacility;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return completedPirateFacility;
    }

    public PlanetaryFacility FindBestPirateFacility(bool includeCriminalNetwork)
    {
      PlanetaryFacility bestPirateFacility = (PlanetaryFacility) null;
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacility planetaryFacility = this[index];
        if (planetaryFacility != null)
        {
          switch (planetaryFacility.Type)
          {
            case PlanetaryFacilityType.PirateBase:
            case PlanetaryFacilityType.PirateFortress:
              if (bestPirateFacility == null || planetaryFacility.Value2 > bestPirateFacility.Value2)
              {
                bestPirateFacility = planetaryFacility;
                continue;
              }
              continue;
            case PlanetaryFacilityType.PirateCriminalNetwork:
              if (includeCriminalNetwork && (bestPirateFacility == null || planetaryFacility.Value2 > bestPirateFacility.Value2))
              {
                bestPirateFacility = planetaryFacility;
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return bestPirateFacility;
    }

    public PlanetaryFacility SelectRandomFacility(PlanetaryFacilityType excludeType)
    {
      PlanetaryFacility planetaryFacility = (PlanetaryFacility) null;
      if (this.Count > 0)
      {
        int num = 0;
        for (planetaryFacility = this[Galaxy.Rnd.Next(0, this.Count)]; (planetaryFacility == null || planetaryFacility.Type == excludeType) && num < 10; ++num)
          planetaryFacility = this[Galaxy.Rnd.Next(0, this.Count)];
        if (planetaryFacility != null && planetaryFacility.Type == excludeType)
          planetaryFacility = (PlanetaryFacility) null;
      }
      return planetaryFacility;
    }

    public double CalculateAnnualMaintenance()
    {
      double annualMaintenance = 0.0;
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacility planetaryFacility = this[index];
        if (planetaryFacility != null && (double) planetaryFacility.ConstructionProgress >= 1.0)
        {
          double maintenance = planetaryFacility.Maintenance;
          annualMaintenance += maintenance;
        }
      }
      return annualMaintenance;
    }

    public int CumulateValue1ByType(PlanetaryFacilityType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          num += this[index].Value1;
      }
      return num;
    }

    public int CumulateValue1ByTypeCompleted(PlanetaryFacilityType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type && (double) this[index].ConstructionProgress >= 1.0)
          num += this[index].Value1;
      }
      return num;
    }

    public int CountByType(PlanetaryFacilityType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          ++num;
      }
      return num;
    }

    public int CountCompletedByType(PlanetaryFacilityType type)
    {
      int num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type && (double) this[index].ConstructionProgress >= 1.0)
          ++num;
      }
      return num;
    }

    public PlanetaryFacility GetById(int planetaryFacilityDefinitionId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        PlanetaryFacility byId = this[index];
        if (byId != null && byId.PlanetaryFacilityDefinitionId == planetaryFacilityDefinitionId)
          return byId;
      }
      return (PlanetaryFacility) null;
    }
  }
}
