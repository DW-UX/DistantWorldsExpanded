// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconStart
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Types;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BaconDistantWorlds
{
  public static class BaconStart
  {
    public static Start baconStart = (Start) null;
    public static int lowStarCount = 100;
    public static int lowIndependentLifeValue = 150;

    public static void InitializeMore(Start start, GameOptions options)
    {
      BaconMain.settingsInitialized = false;
      BaconStart.baconStart = start;
      BaconBuiltObject.myMain = start.main_0;
      Image image = (Image) null;
      try
      {
        image = Image.FromFile(Application.StartupPath + "\\Customization\\" + options.CustomizationSetName + "\\images\\customBackgroundImage.jpg");
      }
      catch (Exception ex)
      {
      }
      if (image == null)
        return;
      ((Control) BaconStart.baconStart).BackgroundImage = image;
    }

    public static void LoadGame(Start start)
    {
      BaconMain.settingsInitialized = false;
      BaconStart.baconStart = start;
      BaconBuiltObject.myMain = start.main_0;
    }

    public static int method_60(int int_1)
    {
      if (BaconMain.settingsInitialized)
        BaconMain.settingsInitialized = false;
      int num = 400;
      switch (int_1)
      {
        case 0:
          num = 100;
          break;
        case 1:
          num = 250;
          break;
        case 2:
          num = 400;
          break;
        case 3:
          num = 700;
          break;
        case 4:
          num = 1000;
          break;
        case 5:
          num = 1400;
          break;
        case 6:
          num = 2000;
          break;
      }
      if (num == 100)
        num = BaconStart.lowStarCount;
      return num;
    }

    public static int method_61(int int_1, RaceList raceList_2)
    {
      int num = 10;
      switch (int_1)
      {
        case 0:
          num = 12;
          break;
        case 1:
          num = 15;
          break;
        case 2:
          num = 18;
          break;
        case 3:
          num = Math.Max(raceList_2.Count<Race>(), 20);
          break;
        case 4:
          num = Math.Max(raceList_2.Count<Race>(), 20);
          break;
        case 5:
          num = Math.Max(raceList_2.Count<Race>(), 20);
          break;
        case 6:
          num = Math.Max(raceList_2.Count<Race>(), 20);
          break;
      }
      return num;
    }

    public static void method_37(Start start)
    {
      string text = TextResolver.GetText("stars");
      start.tbarStartNewGameTheGalaxyStarDensity.SetLabels(new string[7]
      {
        TextResolver.GetText("Dwarf") + "\n100 " + text,
        TextResolver.GetText("Tiny") + "\n250 " + text,
        TextResolver.GetText("Small") + "\n400 " + text,
        TextResolver.GetText("Standard") + "\n700 " + text,
        TextResolver.GetText("Large") + "\n1000 " + text,
        TextResolver.GetText("Huge") + "\n1400 " + text,
        "Bacon big\n2000 " + text
      });
    }

    public static int OverrideLowIndependentLifeValue(int sliderValue)
    {
      int num = 400;
      switch (sliderValue)
      {
        case 0:
          num = 150;
          break;
        case 1:
          num = 250;
          break;
        case 2:
          num = 400;
          break;
        case 3:
          num = 700;
          break;
        case 4:
          num = 1000;
          break;
      }
      if (num == 150)
        num = BaconStart.lowIndependentLifeValue;
      return num;
    }
  }
}
