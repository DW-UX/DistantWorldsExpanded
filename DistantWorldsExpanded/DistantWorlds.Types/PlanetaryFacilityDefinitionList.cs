// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlanetaryFacilityDefinitionList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PlanetaryFacilityDefinitionList : SyncList<PlanetaryFacilityDefinition>
  {
    private bool CheckSequentialIds(PlanetaryFacilityDefinitionList definitions)
    {
      int num = 0;
      for (int index = 0; index < definitions.Count; ++index)
      {
        PlanetaryFacilityDefinition definition = definitions[index];
        if (definition != null)
        {
          if (definition.PlanetaryFacilityDefinitionId != num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      this.Clear();
      PlanetaryFacilityDefinitionList facilityDefinitionList = new PlanetaryFacilityDefinitionList();
      int num1 = 0;
      try
      {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
          using (StreamReader streamReader = new StreamReader((Stream) fileStream))
          {
            while (!streamReader.EndOfStream)
            {
              ++num1;
              string str1 = streamReader.ReadLine();
              if (!string.IsNullOrEmpty(str1) && str1.Trim() != string.Empty && str1.Trim().Substring(0, 1) != "'")
              {
                //if (facilityDefinitionList.Count > 100)
                //  throw new ApplicationException("Exceeded maximum planetary facility count in " + filePath + ". Cannot define more than 100 facilities.");
                int result1 = 0;
                string empty1 = string.Empty;
                int result2 = 0;
                double result3 = 0.0;
                double result4 = 0.0;
                int result5 = 0;
                int result6 = 0;
                int result7 = 0;
                string empty2 = string.Empty;
                int startIndex1 = 0;
                int startIndex2;
                try
                {
                  int num2 = str1.IndexOf(",", startIndex1);
                  if (num2 < 0)
                    throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex1, num2 - startIndex1).Trim(), out result1))
                    throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num2 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read FacilityId at line " + num1.ToString() + " of file " + filePath);
                }
                string name;
                int startIndex3;
                try
                {
                  int num3 = str1.IndexOf(",", startIndex2);
                  if (num3 < 0)
                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                  name = str1.Substring(startIndex2, num3 - startIndex2).Trim();
                  startIndex3 = num3 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                }
                PlanetaryFacilityType type;
                int startIndex4;
                try
                {
                  int num4 = str1.IndexOf(",", startIndex3);
                  if (num4 < 0)
                    throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                  int result8;
                  if (!int.TryParse(str1.Substring(startIndex3, num4 - startIndex3).Trim(), out result8))
                    throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                  switch (result8)
                  {
                    case 0:
                      type = PlanetaryFacilityType.TroopTrainingCenter;
                      break;
                    case 1:
                      type = PlanetaryFacilityType.RoboticTroopFoundry;
                      break;
                    case 2:
                      type = PlanetaryFacilityType.CloningFacility;
                      break;
                    case 3:
                      type = PlanetaryFacilityType.PlanetaryShield;
                      break;
                    case 4:
                      type = PlanetaryFacilityType.IonCannon;
                      break;
                    case 5:
                      type = PlanetaryFacilityType.RegionalCapital;
                      break;
                    case 6:
                      type = PlanetaryFacilityType.FortifiedBunker;
                      break;
                    case 7:
                      type = PlanetaryFacilityType.TerraformingFacility;
                      break;
                    case 8:
                      type = PlanetaryFacilityType.Wonder;
                      break;
                    case 9:
                      type = PlanetaryFacilityType.PirateBase;
                      break;
                    case 10:
                      type = PlanetaryFacilityType.PirateFortress;
                      break;
                    case 11:
                      type = PlanetaryFacilityType.ArmoredFactory;
                      break;
                    case 12:
                      type = PlanetaryFacilityType.SpyAcademy;
                      break;
                    case 13:
                      type = PlanetaryFacilityType.ScienceAcademy;
                      break;
                    case 14:
                      type = PlanetaryFacilityType.NavalAcademy;
                      break;
                    case 15:
                      type = PlanetaryFacilityType.MilitaryAcademy;
                      break;
                    case 16:
                      type = PlanetaryFacilityType.PirateCriminalNetwork;
                      break;
                    default:
                      throw new ApplicationException("Invalid Type at line " + num1.ToString() + " of file " + filePath);
                  }
                  startIndex4 = num4 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                }
                WonderType wonderType;
                int startIndex5;
                try
                {
                  int num5 = str1.IndexOf(",", startIndex4);
                  if (num5 < 0)
                    throw new ApplicationException("Could not read WonderType at line " + num1.ToString() + " of file " + filePath);
                  int result9;
                  if (!int.TryParse(str1.Substring(startIndex4, num5 - startIndex4).Trim(), out result9))
                    throw new ApplicationException("Could not read WonderType at line " + num1.ToString() + " of file " + filePath);
                  switch (result9)
                  {
                    case 0:
                      wonderType = WonderType.Undefined;
                      break;
                    case 1:
                      wonderType = WonderType.EmpirePopulationGrowth;
                      break;
                    case 2:
                      wonderType = WonderType.EmpireHappiness;
                      break;
                    case 3:
                      wonderType = WonderType.EmpireResearchWeapons;
                      break;
                    case 4:
                      wonderType = WonderType.EmpireResearchEnergy;
                      break;
                    case 5:
                      wonderType = WonderType.EmpireResearchHighTech;
                      break;
                    case 6:
                      wonderType = WonderType.EmpireIncome;
                      break;
                    case 7:
                      wonderType = WonderType.ColonyPopulationGrowth;
                      break;
                    case 8:
                      wonderType = WonderType.ColonyHappiness;
                      break;
                    case 9:
                      wonderType = WonderType.ColonyDefense;
                      break;
                    case 10:
                      wonderType = WonderType.ColonyConstructionSpeed;
                      break;
                    case 11:
                      wonderType = WonderType.ColonyIncome;
                      break;
                    case 12:
                      wonderType = WonderType.RaceAchievement;
                      break;
                    default:
                      throw new ApplicationException("Invalid WonderType at line " + num1.ToString() + " of file " + filePath);
                  }
                  startIndex5 = num5 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WonderType at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex6;
                try
                {
                  int num6 = str1.IndexOf(",", startIndex5);
                  if (num6 < 0)
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex5, num6 - startIndex5).Trim(), out result2))
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  if (result2 < 0 || result2 > (int) short.MaxValue)
                    throw new ApplicationException("Invalid PictureRef at line " + num1.ToString() + " of file " + filePath + ". PictureRef must be within range of 0 to 32767.");
                  startIndex6 = num6 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex7;
                try
                {
                  int num7 = str1.IndexOf(",", startIndex6);
                  if (num7 < 0)
                    throw new ApplicationException("Could not read BuildCost at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex6, num7 - startIndex6).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                    throw new ApplicationException("Could not read BuildCost at line " + num1.ToString() + " of file " + filePath);
                  startIndex7 = num7 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read BuildCost at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex8;
                try
                {
                  int num8 = str1.IndexOf(",", startIndex7);
                  if (num8 < 0)
                    throw new ApplicationException("Could not read MaintenanceCost at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex7, num8 - startIndex7).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result4))
                    throw new ApplicationException("Could not read MaintenanceCost at line " + num1.ToString() + " of file " + filePath);
                  startIndex8 = num8 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read MaintenanceCost at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex9;
                try
                {
                  int num9 = str1.IndexOf(",", startIndex8);
                  if (num9 < 0)
                    throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex8, num9 - startIndex8).Trim(), out result5))
                    throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                  startIndex9 = num9 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Value1 at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex10;
                try
                {
                  int num10 = str1.IndexOf(",", startIndex9);
                  if (num10 < 0)
                    throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex9, num10 - startIndex9).Trim(), out result6))
                    throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                  startIndex10 = num10 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Value2 at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex11;
                try
                {
                  int num11 = str1.IndexOf(",", startIndex10);
                  if (num11 < 0)
                    throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex10, num11 - startIndex10).Trim(), out result7))
                    throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                  startIndex11 = num11 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Value3 at line " + num1.ToString() + " of file " + filePath);
                }
                string str2;
                try
                {
                  str2 = str1.Substring(startIndex11, str1.Length - startIndex11).Trim();
                }
                catch
                {
                  throw new ApplicationException("Could not read Description at line " + num1.ToString() + " of file " + filePath);
                }
                facilityDefinitionList.Add(new PlanetaryFacilityDefinition(result1, name, type, (short) result2, result3, result5, result6, result7)
                {
                  WonderType = wonderType,
                  Maintenance = result4,
                  Description = str2
                });
              }
            }
          }
        }
      }
      catch (ApplicationException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error at line " + num1.ToString() + " reading file " + filePath);
      }
      facilityDefinitionList.Sort();
      if (!this.CheckSequentialIds(facilityDefinitionList))
        throw new ApplicationException("Non-sequential Planetary Facility IDs detected in file " + filePath + ". Planetary Facility ID values must start at 0 (zero) and be sequential.");
      this.AddRange((IEnumerable<PlanetaryFacilityDefinition>) facilityDefinitionList);
    }

    public PlanetaryFacilityDefinition FindFacilityByType(PlanetaryFacilityType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == type)
          return this[index];
      }
      return (PlanetaryFacilityDefinition) null;
    }

    public PlanetaryFacilityDefinition FindWonderByType(WonderType type)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.Wonder && this[index].WonderType == type)
          return this[index];
      }
      return (PlanetaryFacilityDefinition) null;
    }

    public PlanetaryFacilityDefinitionList GetWonders()
    {
      PlanetaryFacilityDefinitionList wonders = new PlanetaryFacilityDefinitionList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Type == PlanetaryFacilityType.Wonder)
          wonders.Add(this[index]);
      }
      return wonders;
    }

    public int IndexById(int planetaryFacilityDefinitionId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].PlanetaryFacilityDefinitionId == planetaryFacilityDefinitionId)
          return index;
      }
      return -1;
    }

    public PlanetaryFacilityDefinition GetById(int planetaryFacilityDefinitionId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].PlanetaryFacilityDefinitionId == planetaryFacilityDefinitionId)
          return this[index];
      }
      return (PlanetaryFacilityDefinition) null;
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

    public PlanetaryFacilityDefinitionList Clone()
    {
      PlanetaryFacilityDefinitionList facilityDefinitionList = new PlanetaryFacilityDefinitionList();
      facilityDefinitionList.AddRange((IEnumerable<PlanetaryFacilityDefinition>) this);
      return facilityDefinitionList;
    }
  }
}
