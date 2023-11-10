// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.RaceBiasList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DistantWorlds.Types
{
  [Serializable]
  public class RaceBiasList
  {
    private List<KeyValuePair<string, int>> _Biases = new List<KeyValuePair<string, int>>();
    [OptionalField]
    public bool Populated;

    public RaceBiasList() => this.Clear();

    public void Clear()
    {
      this._Biases = new List<KeyValuePair<string, int>>();
      this.Populated = false;
    }

    public void LoadBiases(RaceList races, List<int> biases)
    {
      this.Clear();
      if (races == null || biases == null || races.Count != biases.Count)
        return;
      for (int index = 0; index < races.Count; ++index)
        this._Biases.Add(new KeyValuePair<string, int>(races[index].Name, biases[index]));
      this.Populated = true;
    }

    public int GetBias(Race race) => race != null ? this.GetBias(race.Name) : 0;

    public int GetBias(string raceName)
    {
      for (int index = 0; index < this._Biases.Count; ++index)
      {
        if (this._Biases[index].Key == raceName)
          return this._Biases[index].Value;
      }
      return 0;
    }

    public void SetBias(string raceName, int value)
    {
      int index1 = -1;
      for (int index2 = 0; index2 < this._Biases.Count; ++index2)
      {
        if (this._Biases[index2].Key == raceName)
        {
          index1 = index2;
          break;
        }
      }
      if (index1 < 0)
        return;
      this._Biases.RemoveAt(index1);
      this._Biases.Add(new KeyValuePair<string, int>(raceName, value));
    }
  }
}
