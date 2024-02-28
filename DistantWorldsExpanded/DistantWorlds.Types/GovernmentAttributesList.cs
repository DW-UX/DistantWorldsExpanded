// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GovernmentAttributesList
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
  public class GovernmentAttributesList : SyncList<GovernmentAttributes>
  {
    public GovernmentBiasList Biases = new GovernmentBiasList();

    public GovernmentAttributesList GetByIds(List<int> governmentIds)
    {
      GovernmentAttributesList byIds = new GovernmentAttributesList();
      for (int index = 0; index < this.Count; ++index)
      {
        if (governmentIds.Contains(this[index].GovernmentId))
          byIds.Add(this[index]);
      }
      return byIds;
    }

    public GovernmentAttributes GetFirstByAvailability(int availability)
    {
      GovernmentAttributesList byAvailability = this.GetByAvailability(availability);
      return byAvailability != null && byAvailability.Count > 0 ? byAvailability[0] : (GovernmentAttributes) null;
    }

    public GovernmentAttributesList GetByAvailability(int availability)
    {
      GovernmentAttributesList byAvailability = new GovernmentAttributesList();
      for (int index = 0; index < this.Count; ++index)
      {
        GovernmentAttributes governmentAttributes = this[index];
        if (governmentAttributes.Availability == availability)
          byAvailability.Add(governmentAttributes);
      }
      return byAvailability;
    }

    public GovernmentAttributesList GetBySpecialFunctionCode(int specialFunctionCode)
    {
      GovernmentAttributesList specialFunctionCode1 = new GovernmentAttributesList();
      for (int index = 0; index < this.Count; ++index)
      {
        GovernmentAttributes governmentAttributes = this[index];
        if (governmentAttributes.SpecialFunctionCode == specialFunctionCode)
          specialFunctionCode1.Add(governmentAttributes);
      }
      return specialFunctionCode1;
    }

    public GovernmentAttributes GetByName(string name)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        GovernmentAttributes byName = this[index];
        if (byName.Name == name)
          return byName;
      }
      return (GovernmentAttributes) null;
    }

    private bool CheckSequentialIds(GovernmentAttributesList governments)
    {
      byte num = 0;
      for (int index = 0; index < governments.Count; ++index)
      {
        GovernmentAttributes government = governments[index];
        if (government != null)
        {
          if (government.GovernmentId != (int) num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      this.Clear();
      GovernmentAttributesList governmentAttributesList = new GovernmentAttributesList();
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
                //if (governmentAttributesList.Count > 60)
                //  throw new ApplicationException("Exceeded maximum government count in " + filePath + ". Cannot define more than 60 governments.");
                double result1 = 1.0;
                double result2 = 1.0;
                double result3 = 1.0;
                double result4 = 1.0;
                double result5 = 1.0;
                double result6 = 1.0;
                List<string> empireNameAdjectives = new List<string>();
                List<string> empireNameNouns = new List<string>();
                int startIndex1 = 0;
                int result7;
                int startIndex2;
                try
                {
                  int num2 = str1.IndexOf(",", startIndex1);
                  if (num2 < 0)
                    throw new ApplicationException("Could not read GovernmentId at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex1, num2 - startIndex1).Trim(), out result7))
                    throw new ApplicationException("Could not read GovernmentId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num2 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read GovernmentId at line " + num1.ToString() + " of file " + filePath);
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
                double result8;
                int startIndex4;
                try
                {
                  int num4 = str1.IndexOf(",", startIndex3);
                  if (num4 < 0)
                    throw new ApplicationException("Could not read Corruption at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex3, num4 - startIndex3).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result8))
                    throw new ApplicationException("Could not read Corruption at line " + num1.ToString() + " of file " + filePath);
                  if (result8 < 0.0 || result8 > 3.0)
                    throw new ApplicationException("Invalid Corruption value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex4 = num4 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Corruption at line " + num1.ToString() + " of file " + filePath);
                }
                double result9;
                int startIndex5;
                try
                {
                  int num5 = str1.IndexOf(",", startIndex4);
                  if (num5 < 0)
                    throw new ApplicationException("Could not read WarWeariness at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex4, num5 - startIndex4).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result9))
                    throw new ApplicationException("Could not read WarWeariness at line " + num1.ToString() + " of file " + filePath);
                  if (result9 < 0.0 || result9 > 3.0)
                    throw new ApplicationException("Invalid War Weariness value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex5 = num5 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WarWeariness at line " + num1.ToString() + " of file " + filePath);
                }
                double result10;
                int startIndex6;
                try
                {
                  int num6 = str1.IndexOf(",", startIndex5);
                  if (num6 < 0)
                    throw new ApplicationException("Could not read MaintenanceCosts at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex5, num6 - startIndex5).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result10))
                    throw new ApplicationException("Could not read MaintenanceCosts at line " + num1.ToString() + " of file " + filePath);
                  if (result10 < 0.0 || result10 > 3.0)
                    throw new ApplicationException("Invalid Maintenance Costs value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex6 = num6 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read MaintenanceCosts at line " + num1.ToString() + " of file " + filePath);
                }
                double result11;
                int startIndex7;
                try
                {
                  int num7 = str1.IndexOf(",", startIndex6);
                  if (num7 < 0)
                    throw new ApplicationException("Could not read ApprovalRating at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex6, num7 - startIndex6).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result11))
                    throw new ApplicationException("Could not read ApprovalRating at line " + num1.ToString() + " of file " + filePath);
                  if (result11 < 0.0 || result11 > 3.0)
                    throw new ApplicationException("Invalid Approval Rating value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex7 = num7 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ApprovalRating at line " + num1.ToString() + " of file " + filePath);
                }
                double result12;
                int startIndex8;
                try
                {
                  int num8 = str1.IndexOf(",", startIndex7);
                  if (num8 < 0)
                    throw new ApplicationException("Could not read PopulationGrowth at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex7, num8 - startIndex7).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result12))
                    throw new ApplicationException("Could not read PopulationGrowth at line " + num1.ToString() + " of file " + filePath);
                  if (result12 < 0.0 || result12 > 3.0)
                    throw new ApplicationException("Invalid Population Growth value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex8 = num8 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read PopulationGrowth at line " + num1.ToString() + " of file " + filePath);
                }
                double result13;
                int startIndex9;
                try
                {
                  int num9 = str1.IndexOf(",", startIndex8);
                  if (num9 < 0)
                    throw new ApplicationException("Could not read ResearchSpeed at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex8, num9 - startIndex8).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result13))
                    throw new ApplicationException("Could not read ResearchSpeed at line " + num1.ToString() + " of file " + filePath);
                  if (result13 < 0.0 || result13 > 3.0)
                    throw new ApplicationException("Invalid Research Speed value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex9 = num9 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ResearchSpeed at line " + num1.ToString() + " of file " + filePath);
                }
                double result14;
                int startIndex10;
                try
                {
                  int num10 = str1.IndexOf(",", startIndex9);
                  if (num10 < 0)
                    throw new ApplicationException("Could not read TroopRecruitment at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex9, num10 - startIndex9).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result14))
                    throw new ApplicationException("Could not read TroopRecruitment at line " + num1.ToString() + " of file " + filePath);
                  if (result14 < 0.0 || result14 > 3.0)
                    throw new ApplicationException("Invalid Troop Recruitment value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex10 = num10 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TroopRecruitment at line " + num1.ToString() + " of file " + filePath);
                }
                double result15;
                int startIndex11;
                try
                {
                  int num11 = str1.IndexOf(",", startIndex10);
                  if (num11 < 0)
                    throw new ApplicationException("Could not read TradeBonus at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex10, num11 - startIndex10).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result15))
                    throw new ApplicationException("Could not read TradeBonus at line " + num1.ToString() + " of file " + filePath);
                  if (result15 < 0.0 || result15 > 3.0)
                    throw new ApplicationException("Invalid Trade Bonus value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex11 = num11 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TradeBonus at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex12;
                try
                {
                  int num12 = str1.IndexOf(",", startIndex11);
                  if (num12 < 0)
                    throw new ApplicationException("Could not read LeaderReplacementLikeliness at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex11, num12 - startIndex11).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result1))
                    throw new ApplicationException("Could not read LeaderReplacementLikeliness at line " + num1.ToString() + " of file " + filePath);
                  if (result1 < 0.0 || result1 > 3.0)
                    throw new ApplicationException("Invalid Leader Replacement Likeliness value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex12 = num12 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read LeaderReplacementLikeliness at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex13;
                try
                {
                  int num13 = str1.IndexOf(",", startIndex12);
                  if (num13 < 0)
                    throw new ApplicationException("Could not read LeaderReplacementDisruptionLevel at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex12, num13 - startIndex12).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result2))
                    throw new ApplicationException("Could not read LeaderReplacementDisruptionLevel at line " + num1.ToString() + " of file " + filePath);
                  if (result2 < 0.0 || result2 > 3.0)
                    throw new ApplicationException("Invalid Leader Replacement Disruption Level value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex13 = num13 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read LeaderReplacementDisruptionLevel at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex14;
                try
                {
                  int num14 = str1.IndexOf(",", startIndex13);
                  if (num14 < 0)
                    throw new ApplicationException("Could not read LeaderReplacementBoost at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex13, num14 - startIndex13).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                    throw new ApplicationException("Could not read LeaderReplacementBoost at line " + num1.ToString() + " of file " + filePath);
                  if (result3 < 0.0 || result3 > 3.0)
                    throw new ApplicationException("Invalid Leader Replacement Boost Level value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex14 = num14 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read LeaderReplacementBoost at line " + num1.ToString() + " of file " + filePath);
                }
                int result16;
                int startIndex15;
                try
                {
                  int num15 = str1.IndexOf(",", startIndex14);
                  if (num15 < 0)
                    throw new ApplicationException("Could not read LeaderReplacementCharacterPool at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex14, num15 - startIndex14).Trim(), out result16))
                    throw new ApplicationException("Could not read LeaderReplacementCharacterPool at line " + num1.ToString() + " of file " + filePath);
                  if (result16 < 0 || result16 > 3)
                    throw new ApplicationException("Invalid Leader Replacement Character Pool value (must be between 0 and 3) at line " + num1.ToString() + " of file " + filePath);
                  startIndex15 = num15 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read LeaderReplacementCharacterPool at line " + num1.ToString() + " of file " + filePath);
                }
                int result17;
                int startIndex16;
                try
                {
                  int num16 = str1.IndexOf(",", startIndex15);
                  if (num16 < 0)
                    throw new ApplicationException("Could not read LeaderReplacementTypicalManner at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex15, num16 - startIndex15).Trim(), out result17))
                    throw new ApplicationException("Could not read LeaderReplacementTypicalManner at line " + num1.ToString() + " of file " + filePath);
                  if (result17 < 0 || result17 > 2)
                    throw new ApplicationException("Invalid Leader Replacement Typical Manner value (must be between 0 and 2) at line " + num1.ToString() + " of file " + filePath);
                  startIndex16 = num16 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read LeaderReplacementTypicalManner at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex17;
                try
                {
                  int num17 = str1.IndexOf(",", startIndex16);
                  if (num17 < 0)
                    throw new ApplicationException("Could not read Stability at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex16, num17 - startIndex16).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result4))
                    throw new ApplicationException("Could not read Stability at line " + num1.ToString() + " of file " + filePath);
                  if (result4 < 0.0 || result4 > 3.0)
                    throw new ApplicationException("Invalid Stability value (must be between 0 and 3.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex17 = num17 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Stability at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex18;
                try
                {
                  int num18 = str1.IndexOf(",", startIndex17);
                  if (num18 < 0)
                    throw new ApplicationException("Could not read ConcernForOwnReputation at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex17, num18 - startIndex17).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
                    throw new ApplicationException("Could not read ConcernForOwnReputation at line " + num1.ToString() + " of file " + filePath);
                  if (result5 < 0.0 || result5 > 2.0)
                    throw new ApplicationException("Invalid Concern For Own Reputation value (must be between 0 and 2.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex18 = num18 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ConcernForOwnReputation at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex19;
                try
                {
                  int num19 = str1.IndexOf(",", startIndex18);
                  if (num19 < 0)
                    throw new ApplicationException("Could not read ImportanceOfOthersReputations at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex18, num19 - startIndex18).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result6))
                    throw new ApplicationException("Could not read ImportanceOfOthersReputations at line " + num1.ToString() + " of file " + filePath);
                  if (result6 < 0.0 || result6 > 2.0)
                    throw new ApplicationException("Invalid Importance Of Others Reputations value (must be between 0 and 2.0) at line " + num1.ToString() + " of file " + filePath);
                  startIndex19 = num19 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ImportanceOfOthersReputations at line " + num1.ToString() + " of file " + filePath);
                }
                int result18;
                int startIndex20;
                try
                {
                  int num20 = str1.IndexOf(",", startIndex19);
                  if (num20 < 0)
                    throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex19, num20 - startIndex19).Trim(), out result18))
                    throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                  if (result18 < 0 || result18 > 1)
                    throw new ApplicationException("Invalid Special Function Code value (must be between 0 and 1) at line " + num1.ToString() + " of file " + filePath);
                  startIndex20 = num20 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read SpecialFunctionCode at line " + num1.ToString() + " of file " + filePath);
                }
                int result19;
                int startIndex21;
                try
                {
                  int num21 = str1.IndexOf(",", startIndex20);
                  if (num21 < 0)
                    throw new ApplicationException("Could not read Availability at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex20, num21 - startIndex20).Trim(), out result19))
                    throw new ApplicationException("Could not read Availability at line " + num1.ToString() + " of file " + filePath);
                  if (result19 < 0 || result19 > 3)
                    throw new ApplicationException("Invalid Availability value (must be between 0 and 3) at line " + num1.ToString() + " of file " + filePath);
                  startIndex21 = num21 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Availability at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex22;
                try
                {
                  int num22 = str1.IndexOf(",", startIndex21);
                  if (num22 < 0)
                    throw new ApplicationException("Could not read Empire Name Adjective 1 at line " + num1.ToString() + " of file " + filePath);
                  string str2 = str1.Substring(startIndex21, num22 - startIndex21).Trim();
                  if (!string.IsNullOrEmpty(str2))
                    empireNameAdjectives.Add(str2);
                  startIndex22 = num22 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Empire Name Adjective 1 at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex23;
                try
                {
                  int num23 = str1.IndexOf(",", startIndex22);
                  if (num23 < 0)
                    throw new ApplicationException("Could not read Empire Name Adjective 2 at line " + num1.ToString() + " of file " + filePath);
                  string str3 = str1.Substring(startIndex22, num23 - startIndex22).Trim();
                  if (!string.IsNullOrEmpty(str3))
                    empireNameAdjectives.Add(str3);
                  startIndex23 = num23 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Empire Name Adjective 2 at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex24;
                try
                {
                  int num24 = str1.IndexOf(",", startIndex23);
                  if (num24 < 0)
                    throw new ApplicationException("Could not read Empire Name Adjective 3 at line " + num1.ToString() + " of file " + filePath);
                  string str4 = str1.Substring(startIndex23, num24 - startIndex23).Trim();
                  if (!string.IsNullOrEmpty(str4))
                    empireNameAdjectives.Add(str4);
                  startIndex24 = num24 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Empire Name Adjective 3 at line " + num1.ToString() + " of file " + filePath);
                }
                int startIndex25;
                try
                {
                  int num25 = str1.IndexOf(",", startIndex24);
                  if (num25 < 0)
                    throw new ApplicationException("Could not read Empire Name Adjective 4 at line " + num1.ToString() + " of file " + filePath);
                  string str5 = str1.Substring(startIndex24, num25 - startIndex24).Trim();
                  if (!string.IsNullOrEmpty(str5))
                    empireNameAdjectives.Add(str5);
                  startIndex25 = num25 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Empire Name Adjective 4 at line " + num1.ToString() + " of file " + filePath);
                }
                int num26;
                int startIndex26;
                try
                {
                  num26 = str1.IndexOf(",", startIndex25);
                  if (num26 < 0)
                    throw new ApplicationException("Could not read Empire Name Adjective 5 at line " + num1.ToString() + " of file " + filePath);
                  string str6 = str1.Substring(startIndex25, num26 - startIndex25).Trim();
                  if (!string.IsNullOrEmpty(str6))
                    empireNameAdjectives.Add(str6);
                  startIndex26 = num26 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Empire Name Adjective 5 at line " + num1.ToString() + " of file " + filePath);
                }
                for (; num26 >= 0; num26 = startIndex26 >= str1.Length ? -1 : str1.IndexOf(",", startIndex26))
                {
                  try
                  {
                    int num27 = str1.IndexOf(",", startIndex26);
                    if (num27 >= 0)
                    {
                      string str7 = str1.Substring(startIndex26, num27 - startIndex26).Trim();
                      if (!string.IsNullOrEmpty(str7))
                        empireNameNouns.Add(str7);
                      startIndex26 = num27 + 1;
                    }
                  }
                  catch
                  {
                    throw new ApplicationException("Could not read Empire Name Noun " + (empireNameNouns.Count + 1).ToString() + " at line " + num1.ToString() + " of file " + filePath);
                  }
                }
                GovernmentAttributes governmentAttributes = new GovernmentAttributes(result7, name, result8, result9, result10, result11, result12, result13, result14, result15, result1, result2, result3, result16, result17, result4, result5, result6, result18, result19, empireNameAdjectives, empireNameNouns);
                governmentAttributesList.Add(governmentAttributes);
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
      governmentAttributesList.Sort();
      if (!this.CheckSequentialIds(governmentAttributesList))
        throw new ApplicationException("Non-sequential Government IDs detected in file " + filePath + ". Government ID values must start at 0 (zero) and be sequential.");
      this.AddRange((IEnumerable<GovernmentAttributes>) governmentAttributesList);
      this.SetDefaultBiases();
    }

    private void SetDefaultBiases()
    {
      List<KeyValuePair<int, int>> biases = new List<KeyValuePair<int, int>>();
      for (int index = 0; index < this.Count; ++index)
        biases.Add(new KeyValuePair<int, int>(this[index].GovernmentId, 0));
      this.Biases = new GovernmentBiasList();
      this.Biases.SetInternal(biases);
    }

    public GovernmentAttributesList Clone()
    {
      GovernmentAttributesList governmentAttributesList = new GovernmentAttributesList();
      governmentAttributesList.AddRange((IEnumerable<GovernmentAttributes>) this);
      governmentAttributesList.Biases = this.Biases.Clone();
      return governmentAttributesList;
    }
  }
}
