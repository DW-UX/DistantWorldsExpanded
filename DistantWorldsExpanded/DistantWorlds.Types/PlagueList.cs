// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PlagueList
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
  public class PlagueList : SyncList<Plague>
  {
    public bool ContainsById(byte plagueId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if ((int) this[index].PlagueId == (int) plagueId)
          return true;
      }
      return false;
    }

    public Plague GetFirstBySpecialFunctionCode(int specialFunctionCode)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].SpecialFunctionCode == specialFunctionCode)
          return this[index];
      }
      return (Plague) null;
    }

    private bool CheckSequentialIds(PlagueList plagues)
    {
      byte num = 0;
      for (int index = 0; index < plagues.Count; ++index)
      {
        Plague plague = plagues[index];
        if (plague != null)
        {
          if ((int) plague.PlagueId != (int) num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      this.Clear();
      PlagueList plagueList = new PlagueList();
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
                //if (plagueList.Count > 50)
                //  throw new ApplicationException("Exceeded maximum plague count in " + filePath + ". Cannot define more than 50 plagues.");
                byte result1 = 0;
                string empty1 = string.Empty;
                int result2 = 0;
                double result3 = 0.0;
                int result4 = 0;
                float result5 = 0.0f;
                bool flag = false;
                int result6 = 0;
                string empty2 = string.Empty;
                double result7 = 0.0;
                int result8 = 0;
                float result9 = 0.0f;
                int result10 = 0;
                string empty3 = string.Empty;
                int startIndex1 = 0;
                int startIndex2;
                try
                {
                  int num2 = str1.IndexOf(",", startIndex1);
                  if (num2 < 0)
                    throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
                  if (!byte.TryParse(str1.Substring(startIndex1, num2 - startIndex1).Trim(), out result1))
                    throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num2 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read PlagueId at line " + num1.ToString() + " of file " + filePath);
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
                int startIndex4;
                try
                {
                  int num4 = str1.IndexOf(",", startIndex3);
                  if (num4 < 0)
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex3, num4 - startIndex3).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
                    throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                  startIndex4 = num4 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read PictureRef at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex5;
                try
                {
                  int num5 = str1.IndexOf(",", startIndex4);
                  if (num5 < 0)
                    throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex4, num5 - startIndex4).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                    throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                  if (result3 < 0.001 || result3 > 5.0)
                    throw new ApplicationException("Invalid MortalityRate. Should be in range 0.001 to 5.0. Line " + num1.ToString() + " of file " + filePath);
                  startIndex5 = num5 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read MortalityRate at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex6;
                try
                {
                  int num6 = str1.IndexOf(",", startIndex5);
                  if (num6 < 0)
                    throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex5, num6 - startIndex5).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result4))
                    throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                  startIndex6 = num6 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read InfectionChance at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex7;
                try
                {
                  int num7 = str1.IndexOf(",", startIndex6);
                  if (num7 < 0)
                    throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex6, num7 - startIndex6).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
                    throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                  startIndex7 = num7 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Length at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex8;
                try
                {
                  int num8 = str1.IndexOf(",", startIndex7);
                  if (num8 < 0)
                    throw new ApplicationException("Could not read NaturalOccurrenceLevel at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex7, num8 - startIndex7).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result6))
                    throw new ApplicationException("Could not read NaturalOccurrenceLevel at line " + num1.ToString() + " of file " + filePath);
                  startIndex8 = num8 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read NaturalOccurrenceLevel at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex9;
                try
                {
                  int num9 = str1.IndexOf(",", startIndex8);
                  string empty4 = string.Empty;
                  switch ((num9 < 0 ? str1.Substring(startIndex8, str1.Length - startIndex8) : str1.Substring(startIndex8, num9 - startIndex8)).Trim().ToLower(CultureInfo.InvariantCulture))
                  {
                    case "y":
                      flag = true;
                      break;
                    case "n":
                      flag = false;
                      break;
                  }
                  startIndex9 = num9 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read CanCompletelyEliminatePopulation at line " + num1.ToString() + " of file " + filePath);
                }
                string str2;
                int startIndex10;
                try
                {
                  int num10 = str1.IndexOf(",", startIndex9);
                  if (num10 < 0)
                    throw new ApplicationException("Could not read ExceptionRaceName at line " + num1.ToString() + " of file " + filePath);
                  str2 = str1.Substring(startIndex9, num10 - startIndex9).Trim();
                  startIndex10 = num10 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ExceptionRaceName at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex11;
                try
                {
                  int num11 = str1.IndexOf(",", startIndex10);
                  if (num11 < 0)
                    throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex10, num11 - startIndex10).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result7))
                    throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                  if (!string.IsNullOrWhiteSpace(str2) && (result7 < 0.001 || result7 > 5.0))
                    throw new ApplicationException("Invalid ExceptionMortalityRate. Should be in range 0.001 to 5.0. Line " + num1.ToString() + " of file " + filePath);
                  startIndex11 = num11 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ExceptionMortalityRate at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex12;
                try
                {
                  int num12 = str1.IndexOf(",", startIndex11);
                  if (num12 < 0)
                    throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex11, num12 - startIndex11).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result8))
                    throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                  startIndex12 = num12 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ExceptionInfectionChance at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex13;
                try
                {
                  int num13 = str1.IndexOf(",", startIndex12);
                  if (num13 < 0)
                    throw new ApplicationException("Could not read ExceptionLength at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex12, num13 - startIndex12).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result9))
                    throw new ApplicationException("Could not read ExceptionLength at line " + num1.ToString() + " of file " + filePath);
                  startIndex13 = num13 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ExceptionLength at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex14;
                try
                {
                  int num14 = str1.IndexOf(",", startIndex13);
                  if (num14 < 0)
                    throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex13, num14 - startIndex13).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result10))
                    throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                  startIndex14 = num14 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                }
                string str3;
                try
                {
                  str3 = str1.Substring(startIndex14, str1.Length - startIndex14).Trim();
                }
                catch
                {
                  throw new ApplicationException("Could not read Description at line " + num1.ToString() + " of file " + filePath);
                }
                plagueList.Add(new Plague(result1, name, result2, result3, result4, result5)
                {
                  CanCompletelyEliminatePopulation = flag,
                  NaturalOccurrenceLevel = result6,
                  ExceptionRaceName = str2,
                  ExceptionMortalityRate = result7,
                  ExceptionInfectionChance = result8,
                  ExceptionDuration = result9,
                  SpecialFunctionCode = result10,
                  Description = str3
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
      plagueList.Sort();
      if (!this.CheckSequentialIds(plagueList))
        throw new ApplicationException("Non-sequential Plague IDs detected in file " + filePath + ". Plague ID values must start at 0 (zero) and be sequential.");
      this.AddRange((IEnumerable<Plague>) plagueList);
    }

    public PlagueList Clone()
    {
      PlagueList plagueList = new PlagueList();
      plagueList.AddRange((IEnumerable<Plague>) this);
      return plagueList;
    }
  }
}
