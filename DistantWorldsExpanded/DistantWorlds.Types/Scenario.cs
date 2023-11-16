// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.Scenario
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;
using System.Drawing;

namespace DistantWorlds.Types
{
  [Serializable]
  public class Scenario
  {
    private string _Title;
    private string _Summary;
    private string _Introduction;
    private Image _Image;
    private ScenarioStepList _Steps = new ScenarioStepList();

    public string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    public string Summary
    {
      get => this._Summary;
      set => this._Summary = value;
    }

    public string Introduction
    {
      get => this._Introduction;
      set => this._Introduction = value;
    }

    public Image Image
    {
      get => this._Image;
      set => this._Image = value;
    }

    public ScenarioStepList Steps
    {
      get => this._Steps;
      set => this._Steps = value;
    }
  }
}
