// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.GalaxyResourceMap
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class GalaxyResourceMap
  {
    internal byte[] _ResourcesKnown;
    private Galaxy _Galaxy;

    public void InitializeFlags(int habitatCount, Galaxy galaxy)
    {
      this._ResourcesKnown = new byte[habitatCount / 8 + 1];
      this._Galaxy = galaxy;
    }

    public void MergeMap(byte[] map)
    {
      if (map.Length != this._ResourcesKnown.Length)
        throw new ApplicationException("Cannot merge maps: they are different sizes");
      for (int index = 0; index < map.Length; ++index)
        this._ResourcesKnown[index] |= map[index];
    }

    public void SetSystemResourcesKnown(Habitat habitat, bool known)
    {
      Habitat habitatSystemStar = Galaxy.DetermineHabitatSystemStar(habitat);
      if (habitatSystemStar == null || habitatSystemStar.SystemIndex < 0 || habitatSystemStar.SystemIndex >= this._Galaxy.Systems.Count)
        return;
      this.SetResourcesKnown(habitatSystemStar, known);
      SystemInfo system = this._Galaxy.Systems[habitatSystemStar.SystemIndex];
      if (system == null || system.Habitats == null)
        return;
      for (int index = 0; index < system.Habitats.Count; ++index)
      {
        Habitat habitat1 = system.Habitats[index];
        if (habitat1 != null)
          this.SetResourcesKnown(habitat1, known);
      }
    }

    public bool CheckResourcesKnownRaw(int habitatIndex)
    {
      int index = habitatIndex / 8;
      int num = habitatIndex % 8;
      if (index < this._ResourcesKnown.Length)
      {
        switch (num)
        {
          case 0:
            return ((int) this._ResourcesKnown[index] & 1) != 0;
          case 1:
            return ((int) this._ResourcesKnown[index] & 2) != 0;
          case 2:
            return ((int) this._ResourcesKnown[index] & 4) != 0;
          case 3:
            return ((int) this._ResourcesKnown[index] & 8) != 0;
          case 4:
            return ((int) this._ResourcesKnown[index] & 16) != 0;
          case 5:
            return ((int) this._ResourcesKnown[index] & 32) != 0;
          case 6:
            return ((int) this._ResourcesKnown[index] & 64) != 0;
          case 7:
            return ((int) this._ResourcesKnown[index] & 128) != 0;
        }
      }
      return false;
    }

    public bool CheckResourcesKnownRaw(Habitat habitat)
    {
      int index = habitat.HabitatIndex / 8;
      int num = habitat.HabitatIndex % 8;
      if (index < this._ResourcesKnown.Length)
      {
        switch (num)
        {
          case 0:
            return ((int) this._ResourcesKnown[index] & 1) != 0;
          case 1:
            return ((int) this._ResourcesKnown[index] & 2) != 0;
          case 2:
            return ((int) this._ResourcesKnown[index] & 4) != 0;
          case 3:
            return ((int) this._ResourcesKnown[index] & 8) != 0;
          case 4:
            return ((int) this._ResourcesKnown[index] & 16) != 0;
          case 5:
            return ((int) this._ResourcesKnown[index] & 32) != 0;
          case 6:
            return ((int) this._ResourcesKnown[index] & 64) != 0;
          case 7:
            return ((int) this._ResourcesKnown[index] & 128) != 0;
        }
      }
      return false;
    }

    public bool CheckResourcesKnown(Habitat habitat)
    {
      if (!this._Galaxy._Reindexing)
      {
        int index = habitat.HabitatIndex / 8;
        int num = habitat.HabitatIndex % 8;
        if (index < this._ResourcesKnown.Length)
        {
          switch (num)
          {
            case 0:
              return ((int) this._ResourcesKnown[index] & 1) != 0;
            case 1:
              return ((int) this._ResourcesKnown[index] & 2) != 0;
            case 2:
              return ((int) this._ResourcesKnown[index] & 4) != 0;
            case 3:
              return ((int) this._ResourcesKnown[index] & 8) != 0;
            case 4:
              return ((int) this._ResourcesKnown[index] & 16) != 0;
            case 5:
              return ((int) this._ResourcesKnown[index] & 32) != 0;
            case 6:
              return ((int) this._ResourcesKnown[index] & 64) != 0;
            case 7:
              return ((int) this._ResourcesKnown[index] & 128) != 0;
          }
        }
      }
      return false;
    }

    public void SetResourcesKnownRaw(int habitatIndex, bool known)
    {
      int index = habitatIndex / 8;
      int num = habitatIndex % 8;
      bool flag = this.CheckResourcesKnownRaw(habitatIndex);
      if (index >= this._ResourcesKnown.Length || flag == known)
        return;
      switch (num)
      {
        case 0:
          this._ResourcesKnown[index] ^= (byte) 1;
          break;
        case 1:
          this._ResourcesKnown[index] ^= (byte) 2;
          break;
        case 2:
          this._ResourcesKnown[index] ^= (byte) 4;
          break;
        case 3:
          this._ResourcesKnown[index] ^= (byte) 8;
          break;
        case 4:
          this._ResourcesKnown[index] ^= (byte) 16;
          break;
        case 5:
          this._ResourcesKnown[index] ^= (byte) 32;
          break;
        case 6:
          this._ResourcesKnown[index] ^= (byte) 64;
          break;
        case 7:
          this._ResourcesKnown[index] ^= (byte) 128;
          break;
      }
    }

    public void SetResourcesKnownRaw(Habitat habitat, bool known)
    {
      int index = habitat.HabitatIndex / 8;
      int num = habitat.HabitatIndex % 8;
      bool flag = this.CheckResourcesKnownRaw(habitat);
      if (index >= this._ResourcesKnown.Length || flag == known)
        return;
      switch (num)
      {
        case 0:
          this._ResourcesKnown[index] ^= (byte) 1;
          break;
        case 1:
          this._ResourcesKnown[index] ^= (byte) 2;
          break;
        case 2:
          this._ResourcesKnown[index] ^= (byte) 4;
          break;
        case 3:
          this._ResourcesKnown[index] ^= (byte) 8;
          break;
        case 4:
          this._ResourcesKnown[index] ^= (byte) 16;
          break;
        case 5:
          this._ResourcesKnown[index] ^= (byte) 32;
          break;
        case 6:
          this._ResourcesKnown[index] ^= (byte) 64;
          break;
        case 7:
          this._ResourcesKnown[index] ^= (byte) 128;
          break;
      }
    }

    public void SetResourcesKnown(Habitat habitat, bool known)
    {
      if (this._Galaxy._Reindexing)
        return;
      int index = habitat.HabitatIndex / 8;
      int num = habitat.HabitatIndex % 8;
      bool flag = this.CheckResourcesKnown(habitat);
      if (index >= this._ResourcesKnown.Length || flag == known)
        return;
      switch (num)
      {
        case 0:
          this._ResourcesKnown[index] ^= (byte) 1;
          break;
        case 1:
          this._ResourcesKnown[index] ^= (byte) 2;
          break;
        case 2:
          this._ResourcesKnown[index] ^= (byte) 4;
          break;
        case 3:
          this._ResourcesKnown[index] ^= (byte) 8;
          break;
        case 4:
          this._ResourcesKnown[index] ^= (byte) 16;
          break;
        case 5:
          this._ResourcesKnown[index] ^= (byte) 32;
          break;
        case 6:
          this._ResourcesKnown[index] ^= (byte) 64;
          break;
        case 7:
          this._ResourcesKnown[index] ^= (byte) 128;
          break;
      }
    }
  }
}
