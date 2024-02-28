// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.FighterSpecificationList
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
  public class FighterSpecificationList : SyncList<FighterSpecification>
  {
    public FighterSpecification GetById(int fighterSpecificationId)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        FighterSpecification byId = this[index];
        if (byId != null && byId.FighterSpecificationId == fighterSpecificationId)
          return byId;
      }
      return (FighterSpecification) null;
    }

    private bool CheckSequentialIds(FighterSpecificationList fighters)
    {
      int num = 0;
      for (int index = 0; index < fighters.Count; ++index)
      {
        FighterSpecification fighter = fighters[index];
        if (fighter != null)
        {
          if (fighter.FighterSpecificationId != num)
            return false;
          ++num;
        }
      }
      return true;
    }

    public void LoadFromFile(string filePath)
    {
      this.Clear();
      FighterSpecificationList specificationList = new FighterSpecificationList();
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
                //if (specificationList.Count > 50)
                //  throw new ApplicationException("Exceeded maximum fighter count in " + filePath + ". Cannot define more than 50 fighters.");
                byte result1 = 0;
                string empty1 = string.Empty;
                short num2 = 10;
                string empty2 = string.Empty;
                int startIndex1 = 0;
                int startIndex2;
                try
                {
                  int num3 = str1.IndexOf(",", startIndex1);
                  if (num3 < 0)
                    throw new ApplicationException("Could not read FighterId at line " + num1.ToString() + " of file " + filePath);
                  if (!byte.TryParse(str1.Substring(startIndex1, num3 - startIndex1).Trim(), out result1))
                    throw new ApplicationException("Could not read FighterId at line " + num1.ToString() + " of file " + filePath);
                  startIndex2 = num3 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read FighterId at line " + num1.ToString() + " of file " + filePath);
                }
                string str2;
                int startIndex3;
                try
                {
                  int num4 = str1.IndexOf(",", startIndex2);
                  if (num4 < 0)
                    throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                  str2 = str1.Substring(startIndex2, num4 - startIndex2).Trim();
                  startIndex3 = num4 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Name at line " + num1.ToString() + " of file " + filePath);
                }
                FighterType fighterType;
                int startIndex4;
                try
                {
                  int num5 = str1.IndexOf(",", startIndex3);
                  if (num5 < 0)
                    throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                  int result2;
                  if (!int.TryParse(str1.Substring(startIndex3, num5 - startIndex3).Trim(), out result2))
                    throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                  switch (result2)
                  {
                    case 0:
                      fighterType = FighterType.Interceptor;
                      break;
                    case 1:
                      fighterType = FighterType.Bomber;
                      break;
                    default:
                      throw new ApplicationException("Invalid Type at line " + num1.ToString() + " of file " + filePath + ". Type should be either 0 (Interceptor) or 1 (Bomber).");
                  }
                  startIndex4 = num5 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read Type at line " + num1.ToString() + " of file " + filePath);
                }
                double result3;
                int startIndex5;
                try
                {
                  int num6 = str1.IndexOf(",", startIndex4);
                  if (num6 < 0)
                    throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                  if (!double.TryParse(str1.Substring(startIndex4, num6 - startIndex4).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result3))
                    throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                  startIndex5 = num6 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TechLevel at line " + num1.ToString() + " of file " + filePath);
                }
                short result4;
                int startIndex6;
                try
                {
                  int num7 = str1.IndexOf(",", startIndex5);
                  if (num7 < 0)
                    throw new ApplicationException("Could not read EnergyCapacity at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex5, num7 - startIndex5).Trim(), out result4))
                    throw new ApplicationException("Could not read EnergyCapacity at line " + num1.ToString() + " of file " + filePath);
                  startIndex6 = num7 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read EnergyCapacity at line " + num1.ToString() + " of file " + filePath);
                }
                float result5;
                int startIndex7;
                try
                {
                  int num8 = str1.IndexOf(",", startIndex6);
                  if (num8 < 0)
                    throw new ApplicationException("Could not read EnergyRechargeRate at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex6, num8 - startIndex6).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result5))
                    throw new ApplicationException("Could not read EnergyRechargeRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex7 = num8 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read EnergyRechargeRate at line " + num1.ToString() + " of file " + filePath);
                }
                short result6;
                int startIndex8;
                try
                {
                  int num9 = str1.IndexOf(",", startIndex7);
                  if (num9 < 0)
                    throw new ApplicationException("Could not read TopSpeed at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex7, num9 - startIndex7).Trim(), out result6))
                    throw new ApplicationException("Could not read TopSpeed at line " + num1.ToString() + " of file " + filePath);
                  startIndex8 = num9 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TopSpeed at line " + num1.ToString() + " of file " + filePath);
                }
                float result7;
                int startIndex9;
                try
                {
                  int num10 = str1.IndexOf(",", startIndex8);
                  if (num10 < 0)
                    throw new ApplicationException("Could not read TopSpeedEnergyConsumptionRate at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex8, num10 - startIndex8).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result7))
                    throw new ApplicationException("Could not read TopSpeedEnergyConsumptionRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex9 = num10 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TopSpeedEnergyConsumptionRate at line " + num1.ToString() + " of file " + filePath);
                }
                float result8;
                int startIndex10;
                try
                {
                  int num11 = str1.IndexOf(",", startIndex9);
                  if (num11 < 0)
                    throw new ApplicationException("Could not read AccelerationRate at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex9, num11 - startIndex9).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result8))
                    throw new ApplicationException("Could not read AccelerationRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex10 = num11 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read AccelerationRate at line " + num1.ToString() + " of file " + filePath);
                }
                float result9;
                int startIndex11;
                try
                {
                  int num12 = str1.IndexOf(",", startIndex10);
                  if (num12 < 0)
                    throw new ApplicationException("Could not read TurnRate at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex10, num12 - startIndex10).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result9))
                    throw new ApplicationException("Could not read TurnRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex11 = num12 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TurnRate at line " + num1.ToString() + " of file " + filePath);
                }
                int result10;
                int startIndex12;
                try
                {
                  int num13 = str1.IndexOf(",", startIndex11);
                  if (num13 < 0)
                    throw new ApplicationException("Could not read EngineExhaustImageIndex at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex11, num13 - startIndex11).Trim(), out result10))
                    throw new ApplicationException("Could not read EngineExhaustImageIndex at line " + num1.ToString() + " of file " + filePath);
                  startIndex12 = num13 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read EngineExhaustImageIndex at line " + num1.ToString() + " of file " + filePath);
                }
                short result11;
                int startIndex13;
                try
                {
                  int num14 = str1.IndexOf(",", startIndex12);
                  if (num14 < 0)
                    throw new ApplicationException("Could not read ShieldsCapacity at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex12, num14 - startIndex12).Trim(), out result11))
                    throw new ApplicationException("Could not read ShieldsCapacity at line " + num1.ToString() + " of file " + filePath);
                  startIndex13 = num14 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ShieldsCapacity at line " + num1.ToString() + " of file " + filePath);
                }
                float result12;
                int startIndex14;
                try
                {
                  int num15 = str1.IndexOf(",", startIndex13);
                  if (num15 < 0)
                    throw new ApplicationException("Could not read ShieldRechargeRate at line " + num1.ToString() + " of file " + filePath);
                  if (!float.TryParse(str1.Substring(startIndex13, num15 - startIndex13).Trim(), NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result12))
                    throw new ApplicationException("Could not read ShieldRechargeRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex14 = num15 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read ShieldRechargeRate at line " + num1.ToString() + " of file " + filePath);
                }
                short result13;
                int startIndex15;
                try
                {
                  int num16 = str1.IndexOf(",", startIndex14);
                  if (num16 < 0)
                    throw new ApplicationException("Could not read DamageRepairRate at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex14, num16 - startIndex14).Trim(), out result13))
                    throw new ApplicationException("Could not read DamageRepairRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex15 = num16 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read DamageRepairRate at line " + num1.ToString() + " of file " + filePath);
                }
                short result14;
                int startIndex16;
                try
                {
                  int num17 = str1.IndexOf(",", startIndex15);
                  if (num17 < 0)
                    throw new ApplicationException("Could not read CountermeasureModifier at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex15, num17 - startIndex15).Trim(), out result14))
                    throw new ApplicationException("Could not read CountermeasureModifier at line " + num1.ToString() + " of file " + filePath);
                  startIndex16 = num17 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read CountermeasureModifier at line " + num1.ToString() + " of file " + filePath);
                }
                short result15;
                int startIndex17;
                try
                {
                  int num18 = str1.IndexOf(",", startIndex16);
                  if (num18 < 0)
                    throw new ApplicationException("Could not read TargettingModifier at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex16, num18 - startIndex16).Trim(), out result15))
                    throw new ApplicationException("Could not read TargettingModifier at line " + num1.ToString() + " of file " + filePath);
                  startIndex17 = num18 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read TargettingModifier at line " + num1.ToString() + " of file " + filePath);
                }
                ComponentType componentType;
                int startIndex18;
                try
                {
                  int num19 = str1.IndexOf(",", startIndex17);
                  if (num19 < 0)
                    throw new ApplicationException("Could not read WeaponType at line " + num1.ToString() + " of file " + filePath);
                  byte result16;
                  if (!byte.TryParse(str1.Substring(startIndex17, num19 - startIndex17).Trim(), out result16))
                    throw new ApplicationException("Could not read WeaponType at line " + num1.ToString() + " of file " + filePath);
                  switch (result16)
                  {
                    case 0:
                      componentType = ComponentType.WeaponBeam;
                      break;
                    case 1:
                      componentType = ComponentType.WeaponTorpedo;
                      break;
                    case 2:
                      componentType = ComponentType.WeaponMissile;
                      break;
                    default:
                      throw new ApplicationException("Invalid WeaponType at line " + num1.ToString() + " of file " + filePath + ". Should be 0 (beam), 1 (torpedo) or 2 (missile).");
                  }
                  startIndex18 = num19 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponType at line " + num1.ToString() + " of file " + filePath);
                }
                int result17;
                int startIndex19;
                try
                {
                  int num20 = str1.IndexOf(",", startIndex18);
                  if (num20 < 0)
                    throw new ApplicationException("Could not read WeaponImageIndex at line " + num1.ToString() + " of file " + filePath);
                  if (!int.TryParse(str1.Substring(startIndex18, num20 - startIndex18).Trim(), out result17))
                    throw new ApplicationException("Could not read WeaponImageIndex at line " + num1.ToString() + " of file " + filePath);
                  startIndex19 = num20 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponImageIndex at line " + num1.ToString() + " of file " + filePath);
                }
                short result18;
                int startIndex20;
                try
                {
                  int num21 = str1.IndexOf(",", startIndex19);
                  if (num21 < 0)
                    throw new ApplicationException("Could not read WeaponDamage at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex19, num21 - startIndex19).Trim(), out result18))
                    throw new ApplicationException("Could not read WeaponDamage at line " + num1.ToString() + " of file " + filePath);
                  startIndex20 = num21 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponDamage at line " + num1.ToString() + " of file " + filePath);
                }
                short result19;
                int startIndex21;
                try
                {
                  int num22 = str1.IndexOf(",", startIndex20);
                  if (num22 < 0)
                    throw new ApplicationException("Could not read WeaponRange at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex20, num22 - startIndex20).Trim(), out result19))
                    throw new ApplicationException("Could not read WeaponRange at line " + num1.ToString() + " of file " + filePath);
                  startIndex21 = num22 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponRange at line " + num1.ToString() + " of file " + filePath);
                }
                short result20;
                int startIndex22;
                try
                {
                  int num23 = str1.IndexOf(",", startIndex21);
                  if (num23 < 0)
                    throw new ApplicationException("Could not read WeaponEnergyRequired at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex21, num23 - startIndex21).Trim(), out result20))
                    throw new ApplicationException("Could not read WeaponEnergyRequired at line " + num1.ToString() + " of file " + filePath);
                  startIndex22 = num23 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponEnergyRequired at line " + num1.ToString() + " of file " + filePath);
                }
                short result21;
                int startIndex23;
                try
                {
                  int num24 = str1.IndexOf(",", startIndex22);
                  if (num24 < 0)
                    throw new ApplicationException("Could not read WeaponSpeed at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex22, num24 - startIndex22).Trim(), out result21))
                    throw new ApplicationException("Could not read WeaponSpeed at line " + num1.ToString() + " of file " + filePath);
                  startIndex23 = num24 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponSpeed at line " + num1.ToString() + " of file " + filePath);
                }
                short result22;
                int startIndex24;
                try
                {
                  int num25 = str1.IndexOf(",", startIndex23);
                  if (num25 < 0)
                    throw new ApplicationException("Could not read WeaponDamageLoss at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex23, num25 - startIndex23).Trim(), out result22))
                    throw new ApplicationException("Could not read WeaponDamageLoss at line " + num1.ToString() + " of file " + filePath);
                  startIndex24 = num25 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponDamageLoss at line " + num1.ToString() + " of file " + filePath);
                }
                int num26;
                short result23;
                int startIndex25;
                try
                {
                  num26 = str1.IndexOf(",", startIndex24);
                  if (num26 < 0)
                    throw new ApplicationException("Could not read WeaponFireRate at line " + num1.ToString() + " of file " + filePath);
                  if (!short.TryParse(str1.Substring(startIndex24, num26 - startIndex24).Trim(), out result23))
                    throw new ApplicationException("Could not read WeaponFireRate at line " + num1.ToString() + " of file " + filePath);
                  startIndex25 = num26 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponFireRate at line " + num1.ToString() + " of file " + filePath);
                }
                string str3;
                try
                {
                  str3 = str1.Substring(startIndex25, str1.Length - startIndex25).Trim();
                  int num27 = num26 + 1;
                }
                catch
                {
                  throw new ApplicationException("Could not read WeaponSoundEffectFilename at line " + num1.ToString() + " of file " + filePath);
                }
                specificationList.Add(new FighterSpecification()
                {
                  FighterSpecificationId = (int) result1,
                  Name = str2,
                  Type = fighterType,
                  Size = num2,
                  TechLevel = result3,
                  EnergyCapacity = result4,
                  EnergyRechargeRate = result5,
                  TopSpeed = result6,
                  TopSpeedEnergyConsumptionRate = result7,
                  AccelerationRate = result8,
                  TurnRate = result9,
                  EngineExhaustImageIndex = result10,
                  ShieldsCapacity = result11,
                  ShieldRechargeRate = result12,
                  DamageRepairRate = result13,
                  CountermeasureModifier = result14,
                  TargettingModifier = result15,
                  WeaponType = componentType,
                  WeaponImageIndex = result17,
                  WeaponDamage = result18,
                  WeaponRange = result19,
                  WeaponEnergyRequired = result20,
                  WeaponSpeed = result21,
                  WeaponDamageLoss = result22,
                  WeaponFireRate = result23,
                  WeaponSoundEffectFilename = str3
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
      specificationList.Sort();
      if (!this.CheckSequentialIds(specificationList))
        throw new ApplicationException("Non-sequential Fighter IDs detected in file " + filePath + ". Fighter ID values must start at 0 (zero) and be sequential.");
      this.AddRange((IEnumerable<FighterSpecification>) specificationList);
    }

    public FighterSpecificationList Clone()
    {
      FighterSpecificationList specificationList = new FighterSpecificationList();
      specificationList.AddRange((IEnumerable<FighterSpecification>) this);
      return specificationList;
    }
  }
}
