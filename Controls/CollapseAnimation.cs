// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.CollapseAnimation
// Assembly: DistantWorlds.Controls, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C078528F-27D0-4E24-8047-8F4F72265A90
// Assembly location: H:\7\DistantWorlds.Controls.dll

using System;
using System.Threading;

namespace DistantWorlds.Controls
{
  internal class CollapseAnimation
  {
    protected int step;
    protected int minimum;
    protected int maximum;
    protected Thread thread;
    protected ManualResetEvent threadStart = new ManualResetEvent(false);

    public event NotifyAnimationEvent NotifyAnimation;

    public event NotifyAnimationFinishedEvent NotifyAnimationFinished;

    internal CollapseAnimation()
    {
    }

    internal CollapseAnimation(int step, int minimum, int maximum)
    {
      this.step = step;
      this.minimum = minimum;
      this.maximum = maximum;
    }

    public void Start()
    {
      if (this.step == 0)
        throw new InvalidOperationException("Step can not be zero!");
      if (this.minimum >= this.maximum)
        throw new InvalidOperationException("Invalid parameters");
      this.threadStart.Reset();
      this.thread = new Thread(new ThreadStart(this.Animate));
      this.thread.IsBackground = true;
      this.thread.Start();
      this.threadStart.WaitOne();
    }

    protected void Animate()
    {
      this.threadStart.Set();
      if (this.NotifyAnimation == null)
        return;
      if (this.step > 0)
      {
        while (this.maximum > this.minimum)
        {
          this.maximum -= this.step;
          if (this.maximum < this.minimum)
            this.maximum = this.minimum;
          this.NotifyAnimation((object) this, this.maximum);
          Thread.Sleep(20);
        }
        if (this.NotifyAnimationFinished == null)
          return;
        this.NotifyAnimationFinished((object) this);
      }
      else
      {
        while (this.maximum > this.minimum)
        {
          this.minimum -= this.step;
          if (this.maximum < this.minimum)
            this.minimum = this.maximum;
          this.NotifyAnimation((object) this, this.minimum);
          Thread.Sleep(20);
        }
        if (this.NotifyAnimationFinished == null)
          return;
        this.NotifyAnimationFinished((object) this);
      }
    }

    public int Step
    {
      get => this.step;
      set => this.step = value;
    }

    public int Minimum
    {
      get => this.minimum;
      set => this.minimum = value;
    }

    public int Maximum
    {
      get => this.maximum;
      set => this.maximum = value;
    }
  }
}
