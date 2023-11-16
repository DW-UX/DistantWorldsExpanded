// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.WeaponList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;

namespace DistantWorlds.Types
{
  [Serializable]
  public class WeaponList : SyncList<Weapon>
  {
    public WeaponList GetAllPlanetDestroyerWeapons()
    {
      WeaponList destroyerWeapons = new WeaponList();
      for (int index = 0; index < this.Count; ++index)
      {
        Weapon weapon = this[index];
        if (weapon != null && weapon.IsPlanetDestroyer)
          destroyerWeapons.Add(weapon);
      }
      return destroyerWeapons;
    }

    public int CalculateRawDamageOfWeaponsAboveRange(int range)
    {
      int weaponsAboveRange = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Weapon weapon = this[index];
        if (weapon != null && weapon.Range >= range)
          weaponsAboveRange += weapon.RawDamage;
      }
      return weaponsAboveRange;
    }

    public bool QuickCompareEquivalent(WeaponList weapons)
    {
      if (weapons == null || this.Count != weapons.Count)
        return false;
      for (int index = 0; index < this.Count; ++index)
      {
        if (this[index].Component.ComponentID != weapons[index].Component.ComponentID)
          return false;
      }
      return true;
    }

    public WeaponList DetermineWeaponsNotInSuppliedList(WeaponList weapons)
    {
      WeaponList notInSuppliedList = new WeaponList();
      notInSuppliedList.AddRange((IEnumerable<Weapon>) this);
      for (int index1 = 0; index1 < weapons.Count; ++index1)
      {
        int index2 = -1;
        for (int index3 = 0; index3 < notInSuppliedList.Count; ++index3)
        {
          if (notInSuppliedList[index3].Component.ComponentID == weapons[index1].Component.ComponentID)
          {
            index2 = index3;
            break;
          }
        }
        if (index2 >= 0)
          notInSuppliedList.RemoveAt(index2);
        if (notInSuppliedList.Count <= 0)
          break;
      }
      return notInSuppliedList;
    }

    public void RemoveAndResetFirstMatchingWeaponById(Weapon weaponToRemove)
    {
      if (weaponToRemove == null)
        return;
      int index1 = -1;
      for (int index2 = 0; index2 < this.Count; ++index2)
      {
        Weapon weapon = this[index2];
        if (weapon != null && weapon.Component.ComponentID == weaponToRemove.Component.ComponentID)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0 || index1 >= this.Count)
        return;
      this[index1].Reset();
      this.RemoveAt(index1);
    }
  }
}
