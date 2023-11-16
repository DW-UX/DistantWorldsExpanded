// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.BasicStopWatch
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Runtime.InteropServices;

namespace DistantWorlds.Types
{
  [Serializable]
  public class BasicStopWatch
  {
    public static readonly long Frequency;
    public static readonly bool IsHighResolution;
    private long _Elapsed;
    private bool _IsRunning;
    private long _StartTimeStamp;
    private static readonly double _TickFrequency;
    private double _TimeSpeed = 1.0;

    [DllImport("kernel32.dll")]
    private static extern bool QueryPerformanceFrequency(out long lpFrequency);

    [DllImport("kernel32.dll")]
    private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

    static BasicStopWatch()
    {
      if (!BasicStopWatch.QueryPerformanceFrequency(out BasicStopWatch.Frequency))
      {
        BasicStopWatch.IsHighResolution = false;
        BasicStopWatch.Frequency = 10000000L;
        BasicStopWatch._TickFrequency = 1.0;
      }
      else
      {
        BasicStopWatch.IsHighResolution = true;
        BasicStopWatch._TickFrequency = 10000000.0;
        BasicStopWatch._TickFrequency /= (double) BasicStopWatch.Frequency;
      }
    }

    public BasicStopWatch() => this.Reset();

    public double TimeSpeed
    {
      get => this._TimeSpeed;
      set
      {
        if (value == this._TimeSpeed)
          return;
        this._TimeSpeed = value;
      }
    }

    private long GetElapsedDateTimeTicks()
    {
      long rawElapsedTicks = this.GetRawElapsedTicks();
      return BasicStopWatch.IsHighResolution ? (long) ((double) rawElapsedTicks * BasicStopWatch._TickFrequency) : rawElapsedTicks;
    }

    private long GetRawElapsedTicks()
    {
      long elapsed = this._Elapsed;
      if (this._IsRunning)
      {
        long num = (long) ((double) (BasicStopWatch.GetTimestamp() - this._StartTimeStamp) * this._TimeSpeed);
        elapsed += num;
      }
      return elapsed;
    }

    public static long GetTimestamp()
    {
      if (!BasicStopWatch.IsHighResolution)
        return DateTime.UtcNow.Ticks;
      long lpPerformanceCount = 0;
      BasicStopWatch.QueryPerformanceCounter(out lpPerformanceCount);
      return lpPerformanceCount;
    }

    public void Reset()
    {
      this._Elapsed = 0L;
      this._IsRunning = false;
      this._StartTimeStamp = 0L;
    }

    public void Start()
    {
      if (this._IsRunning)
        return;
      this._StartTimeStamp = BasicStopWatch.GetTimestamp();
      this._IsRunning = true;
    }

    public static BasicStopWatch StartNew()
    {
      BasicStopWatch basicStopWatch = new BasicStopWatch();
      basicStopWatch.Start();
      return basicStopWatch;
    }

    public void Stop()
    {
      if (!this._IsRunning)
        return;
      this._Elapsed += (long) ((double) (BasicStopWatch.GetTimestamp() - this._StartTimeStamp) * this._TimeSpeed);
      this._IsRunning = false;
    }

    public TimeSpan Elapsed => new TimeSpan(this.GetElapsedDateTimeTicks());

    public long ElapsedMilliseconds => this.GetElapsedDateTimeTicks() / 10000L;

    public long ElapsedTicks => this.GetRawElapsedTicks();

    public bool IsRunning => this._IsRunning;
  }
}
