// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Types.TutorialItem
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using System;

namespace DistantWorlds.Types
{
  [Serializable]
  public class TutorialItem
  {
    private string string_0;
    private string string_1;
    private string string_2;
    private object object_0;
    private bool bool_0;
    private bool bool_1;
    private bool bool_2;
    private bool bool_3;
    private bool bool_4;
    private object object_1;
    private double double_0;
    private object object_2;
    private BorderPanel borderPanel_0;
    private string string_3;
    private object object_3;
    public int HighlightActionButtonNumber;
    public bool UnpauseGame;
    public string HighlightEmpireNavigationToolPanelTitle;

    public string Name
    {
      get => this.string_0;
      set => this.string_0 = value;
    }

    public string Title
    {
      get => this.string_1;
      set => this.string_1 = value;
    }

    public string Text
    {
      get => this.string_2;
      set => this.string_2 = value;
    }

    public object HighlightObject
    {
      get => this.object_0;
      set => this.object_0 = value;
    }

    public bool HighlightHabitatEmpire
    {
      get => this.bool_0;
      set => this.bool_0 = value;
    }

    public bool HighlightHabitatPopulationGraph
    {
      get => this.bool_1;
      set => this.bool_1 = value;
    }

    public bool HighlightHabitatDominantRace
    {
      get => this.bool_2;
      set => this.bool_2 = value;
    }

    public bool HighlightHabitatResources
    {
      get => this.bool_3;
      set => this.bool_3 = value;
    }

    public bool HighlightOpenEmpireNavigationToolPanel
    {
      get => this.bool_4;
      set => this.bool_4 = value;
    }

    public object ZoomScrollObject
    {
      get => this.object_1;
      set => this.object_1 = value;
    }

    public double ZoomLevel
    {
      get => this.double_0;
      set => this.double_0 = value;
    }

    public object SelectionObject
    {
      get => this.object_2;
      set => this.object_2 = value;
    }

    public BorderPanel OpenScreen
    {
      get => this.borderPanel_0;
      set => this.borderPanel_0 = value;
    }

    public string ScreenTabName
    {
      get => this.string_3;
      set => this.string_3 = value;
    }

    public object ListSelection
    {
      get => this.object_3;
      set => this.object_3 = value;
    }

    public TutorialItem():base()
    {
      Class7.VEFSJNszvZKMZ();
      // ISSUE: explicit constructor call
    }
  }
}
