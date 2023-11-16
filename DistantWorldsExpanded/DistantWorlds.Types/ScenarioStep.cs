// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.ScenarioStep
// Assembly: DistantWorlds.Types, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: C87DBA0E-BD3A-46BA-A8F0-EE9F5E5721E2
// Assembly location: H:\7\DistantWorlds.Types.dll

using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class ScenarioStep
  {
    private ScenarioStepList _ParentSteps = new ScenarioStepList();
    private CompletionType _ParentStepsCompletionType;
    private CompletionType _ObjectiveCompletionType;
    private ScenarioObjectiveList _Objectives = new ScenarioObjectiveList();
    private ScenarioObjectiveList _ObjectivesOtherEmpire = new ScenarioObjectiveList();
    private ScenarioResultList _Results = new ScenarioResultList();
    private string _Title;
    private string _Description;

    public ScenarioStepList ParentSteps
    {
      get => this._ParentSteps;
      set => this._ParentSteps = value;
    }

    public CompletionType ObjectiveCompletionType
    {
      get => this._ObjectiveCompletionType;
      set => this._ObjectiveCompletionType = value;
    }

    public ScenarioObjectiveList Objectives
    {
      get => this._Objectives;
      set => this._Objectives = value;
    }

    public ScenarioObjectiveList ObjectivesOtherEmpire
    {
      get => this._ObjectivesOtherEmpire;
      set => this._ObjectivesOtherEmpire = value;
    }

    public ScenarioResultList Results
    {
      get => this._Results;
      set => this._Results = value;
    }

    public string Title
    {
      get => this._Title;
      set => this._Title = value;
    }

    public string Description
    {
      get => this._Description;
      set => this._Description = value;
    }
  }
}
