// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceFamilyList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceFamilyList : List<RaceFamily>
  {
    public RaceFamilyBiasList Biases = new RaceFamilyBiasList();

    public List<byte> GetIdsBySpecialFunctionCode(int specialFunctionCode)
    {
      List<byte> specialFunctionCode1 = new List<byte>();
      for (int index = 0; index < this.Count; ++index)
      {
        RaceFamily raceFamily = this[index];
        if (raceFamily != null && raceFamily.SpecialFunctionCode == specialFunctionCode)
          specialFunctionCode1.Add(raceFamily.RaceFamilyId);
      }
      return specialFunctionCode1;
    }

    public RaceFamily GetByName(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        RaceFamily byName = this[index];
        if (byName.Name == name)
          return byName;
      }
      return (RaceFamily) null;
    }

    private bool CheckSequentialIds(RaceFamilyList raceFamilies)
    {
      byte num = 0;
      for (int index = 0; index < raceFamilies.Count; ++index)
      {
        RaceFamily raceFamily = raceFamilies[index];
        if (raceFamily != null)
        {
          if ((int) raceFamily.RaceFamilyId != (int) num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      this.Clear();
      RaceFamilyList raceFamilyList = new RaceFamilyList();
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
              string str = streamReader.ReadLine();
              if (!string.IsNullOrEmpty(str) && str.Trim() != string.Empty && str.Trim().Substring(0, 1) != "'")
              {
                //if (raceFamilyList.Count > 30)
                //  throw new ApplicationException("Exceeded maximum race family count in " + filePath + ". Cannot define more than 30 race families.");
                int result1 = 0;
                int startIndex1 = 0;
                byte result2;
                int startIndex2;
                try
                {
                  int num2 = str.IndexOf(",", startIndex1);
                  if (num2 < 0)
                    throw new ApplicationException("Could not read RaceFamilyId at line " + num1.ToString() + " of file " + filePath);
                  if (!byte.TryParse(str.Substring(startIndex1, num2 - startIndex1).Trim(), out result2))
                    throw new ApplicationException("Could not read RaceFamilyId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num2 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read RaceFamilyId at line " + num1.ToString() + " of file " + filePath);
                }
                int num3;
                string name;
                int startIndex3;
                try
                {
                  num3 = str.IndexOf(",", startIndex2);
                  if (num3 < 0)
                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                  name = str.Substring(startIndex2, num3 - startIndex2).Trim();
                  startIndex3 = num3 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                }
                try
                {
                  if (!int.TryParse(str.Substring(startIndex3, str.Length - startIndex3).Trim(), out result1))
                    throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                  int num4 = num3 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                }
                RaceFamily raceFamily = new RaceFamily(result2, name, result1);
                raceFamilyList.Add(raceFamily);
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
      raceFamilyList.Sort();
      if (!this.CheckSequentialIds(raceFamilyList))
        throw new ApplicationException("Non-sequential Race Family IDs detected in file " + filePath + ". Race Family ID values must start at 0 (zero) and be sequential.");
      this.AddRange((IEnumerable<RaceFamily>) raceFamilyList);
      this.SetDefaultBiases();
    }

    private void SetDefaultBiases()
    {
      List<KeyValuePair<int, int>> biases = new List<KeyValuePair<int, int>>();
      for (int index = 0; index < this.Count; ++index)
        biases.Add(new KeyValuePair<int, int>((int) this[index].RaceFamilyId, 0));
      this.Biases = new RaceFamilyBiasList();
      this.Biases.SetInternal(biases);
    }

    public RaceFamilyList Clone()
    {
      RaceFamilyList raceFamilyList = new RaceFamilyList();
      raceFamilyList.AddRange((IEnumerable<RaceFamily>) this);
      raceFamilyList.Biases = this.Biases.Clone();
      return raceFamilyList;
    }
  }
}
