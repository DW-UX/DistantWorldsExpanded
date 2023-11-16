// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceFamilyBiasList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceFamilyBiasList
  {
    private List<KeyValuePair<int, int>> _Biases = new List<KeyValuePair<int, int>>();
    public bool Populated;

    public RaceFamilyBiasList() => this.Clear();

    public void Clear()
    {
      this._Biases = new List<KeyValuePair<int, int>>();
      this.Populated = false;
    }

    public static void LoadFromFile(string filePath, ref RaceFamilyList raceFamilies)
    {
      int num1 = 0;
      try
      {
        if (!File.Exists(filePath))
          return;
        FileStream fileStream = File.OpenRead(filePath);
        StreamReader streamReader = new StreamReader((Stream) fileStream);
        while (!streamReader.EndOfStream)
        {
          ++num1;
          string empty1 = string.Empty;
          string str = streamReader.ReadLine();
          if (!string.IsNullOrEmpty(str) && str.Trim() != string.Empty && str.Trim().Substring(0, 1) != "'")
          {
            int startIndex1 = 0;
            int num2 = str.IndexOf(",", startIndex1);
            int result1 = -1;
            if (num2 < 0)
              throw new ApplicationException("Could not read RaceFamilyId number at line " + num1.ToString() + " of file " + filePath);
            if (!int.TryParse(str.Substring(startIndex1, num2 - startIndex1).Trim(), out result1))
              throw new ApplicationException("Could not read RaceFamilyId number at line " + num1.ToString() + " of file " + filePath);
            int startIndex2 = num2 + 1;
            int num3 = str.IndexOf(",", startIndex2);
            if (num3 < 0)
              throw new ApplicationException("Could not read Race Family Name at line " + num1.ToString() + " of file " + filePath);
            str.Substring(startIndex2, num3 - startIndex2).Trim();
            int startIndex3 = num3 + 1;
            List<int> intList = new List<int>();
            int num4 = 1;
            while (num3 >= 0)
            {
              num3 = str.IndexOf(",", startIndex3);
              string empty2 = string.Empty;
              string s = (num3 < 0 ? str.Substring(startIndex3, str.Length - startIndex3) : str.Substring(startIndex3, num3 - startIndex3)).Trim();
              int result2 = 0;
              if (int.TryParse(s, out result2))
              {
                int num5 = Math.Max(-50, Math.Min(result2, 50));
                intList.Add(num5);
                if (intList.Count > raceFamilies.Count)
                  throw new ApplicationException("More bias values than race families at line " + num1.ToString() + " in file " + filePath);
                startIndex3 = num3 + 1;
                ++num4;
              }
              else
                throw new ApplicationException("Could not read Bias Value " + num4.ToString() + " at line " + num1.ToString() + " of file " + filePath);
            }
            if (intList.Count != raceFamilies.Count)
              throw new ApplicationException("Wrong number of bias values at line " + num1.ToString() + " in file " + filePath + ". Number of bias values should match number of race families, i.e. columns should match rows.");
            if (result1 >= 0 && result1 < raceFamilies.Count)
            {
              List<KeyValuePair<int, int>> biases = new List<KeyValuePair<int, int>>();
              for (int index = 0; index < intList.Count; ++index)
                biases.Add(new KeyValuePair<int, int>(result1, intList[index]));
              raceFamilies[result1].Biases.SetInternal(biases);
            }
          }
        }
        streamReader.Close();
        fileStream.Close();
      }
      catch (ApplicationException ex)
      {
        throw;
      }
      catch (Exception ex)
      {
        throw new ApplicationException("Error at line " + num1.ToString() + " reading file " + filePath);
      }
    }

    public int GetBias(int raceFamilyId)
    {
      for (int index = 0; index < this._Biases.Count; ++index)
      {
        if (this._Biases[index].Key == raceFamilyId)
          return this._Biases[index].Value;
      }
      return 0;
    }

    internal void SetInternal(List<KeyValuePair<int, int>> biases)
    {
      this._Biases = biases;
      this.Populated = true;
    }

    public RaceFamilyBiasList Clone()
    {
      List<KeyValuePair<int, int>> biases = new List<KeyValuePair<int, int>>();
      for (int index = 0; index < this._Biases.Count; ++index)
        biases.Add(this._Biases[index]);
      RaceFamilyBiasList raceFamilyBiasList = new RaceFamilyBiasList();
      raceFamilyBiasList.SetInternal(biases);
      return raceFamilyBiasList;
    }
  }
}
