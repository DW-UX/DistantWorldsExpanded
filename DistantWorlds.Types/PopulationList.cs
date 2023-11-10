// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.PopulationList
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class PopulationList : SyncList<Population>
  {
    private long _TotalAmount;

    public PopulationList Clone()
    {
      PopulationList populationList = new PopulationList();
      foreach (Population population in (SyncList<Population>) this)
        populationList.Add(new Population(population.Race, population.Amount));
      populationList.RecalculateTotalAmount();
      return populationList;
    }

    public Population this[Race race]
    {
      get
      {
        for (int index = 0; index < this.Count; ++index)
        {
          Population population = this[index];
          if (population.Race == race)
            return population;
        }
        return (Population) null;
      }
    }

    public int Add(Population population)
    {
      lock (this._LockObject)
      {
        for (int index = 0; index < this.Count; ++index)
        {
          if (this[index].Race.Name == population.Race.Name)
          {
            this[index].Amount += population.Amount;
            return -1;
          }
        }
        base.Add(population);
        return this.Count - 1;
      }
    }

    public void RecalculateTotalAmount()
    {
      long num = 0;
      for (int index = 0; index < this.Count; ++index)
      {
        Population population = this[index];
        num += population.Amount;
      }
      this._TotalAmount = num;
    }

    public long TotalAmount
    {
      get => this._TotalAmount;
      set => this._TotalAmount = value;
    }

    public double OverallGrowthRate
    {
      get
      {
        double num = 0.0;
        for (int index = 0; index < this.Count; ++index)
        {
          Population population = this[index];
          num += (double) population.Amount * ((double) population.GrowthRate - 1.0);
        }
        return 1.0 + num / (double) this.TotalAmount;
      }
    }

    public Race DominantRace
    {
      get
      {
        long num1 = 0;
        Race dominantRace = (Race) null;
        for (int index = 0; index < this.Count; ++index)
        {
          Population population = this[index];
          long num2 = population.Amount * (long) population.Race.IntelligenceLevel;
          if (num2 > num1)
          {
            dominantRace = population.Race;
            num1 = num2;
          }
        }
        return dominantRace;
      }
    }
  }
}
