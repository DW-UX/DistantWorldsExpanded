using DistantWorlds.Controls;
using DistantWorlds.Types;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistantWorlds.Mods
{
    public class ReturnDoubleModsArgs
    {
        public double Result { get; set; }
        public ReturnDoubleModsArgs()
        {
        }
    }
    public class DrawRangeModsArgs
    {
        public SpriteBatch SpriteBatch_2 { get; }
        public int Num14 { get; }
        public int Num16 { get; }
        public double ZoomLevel { get; }

        public DrawRangeModsArgs(SpriteBatch spriteBatch_2, int num14, int num16, double zoomLevel)
        {
            SpriteBatch_2 = spriteBatch_2;
            Num14 = num14;
            Num16 = num16;
            ZoomLevel = zoomLevel;
        }
    }
    public class DrawPathLineModsArgs
    {
        public MainView Mainview { get; }
        public SpriteBatch SpriteBatch_2 { get; }
        public BuiltObject BuiltObject_1 { get; }
        public int Int_11 { get; }
        public int Int_12 { get; }
        public int Int_13 { get; }
        public int Int_14 { get; }
        public double Double_15 { get; }
        public bool Bool_13 { get; }
        public Color Color_18 { get; }
        public int Int_15 { get; }

        public DrawPathLineModsArgs(MainView mainview,
                                                      SpriteBatch spriteBatch_2,
                                                      BuiltObject builtObject_1,
                                                      int int_11,
                                                      int int_12,
                                                      int int_13,
                                                      int int_14,
                                                      double double_15,
                                                      bool bool_13,
                                                      System.Drawing.Color color_18,
                                                      int int_15)
        {
            Mainview = mainview;
            SpriteBatch_2 = spriteBatch_2;
            BuiltObject_1 = builtObject_1;
            Int_11 = int_11;
            Int_12 = int_12;
            Int_13 = int_13;
            Int_14 = int_14;
            Double_15 = double_15;
            Bool_13 = bool_13;
            Color_18 = color_18;
            Int_15 = int_15;
        }
    }
    public class GetBackGroundImageModsArgs
    {
        public Image Result { get; set; }
        public GameOptions Options { get; }

        public GetBackGroundImageModsArgs(GameOptions options)
        {
            Options = options;
        }
    }
    public class GetStarDensityLabelsModsArgs
    {
        public string[] Result { get; set; }

        public GetStarDensityLabelsModsArgs()
        {
        }
    }
    public class GenericIntIntModsArgs
    {
        public int Result { get; set; }
        public int Value { get; }

        public GenericIntIntModsArgs(int value)
        {
            Value = value;
        }
    }
    public class GetRaceCountUserSelectedModsArgs
    {
        public int Result { get; set; }
        public int Value { get; }
        public RaceList RaceList { get; }

        public GetRaceCountUserSelectedModsArgs(int value, RaceList raceList_2)
        {
            Value = value;
            RaceList = raceList_2;
        }
    }
    public class SetUIMessagesImagesModsArgs
    {
        public Bitmap Result { get; set; }
        public Func<string, string, string, bool, Bitmap> LoadImageFunc { get; }
        public string UiMessagesRoot { get; }
        public string CustomUiMessagesRoot { get; }

        public SetUIMessagesImagesModsArgs(Func<string, string, string, bool, Bitmap> func, string uiMessagesRoot, string customUiMessagesRoot)
        {
            LoadImageFunc = func;
            UiMessagesRoot = uiMessagesRoot;
            CustomUiMessagesRoot = customUiMessagesRoot;
        }
    }
}
