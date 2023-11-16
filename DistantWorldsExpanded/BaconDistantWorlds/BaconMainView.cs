// Decompiled with JetBrains decompiler
// Type: BaconDistantWorlds.BaconMainView
// Assembly: BaconDistantWorlds, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BEEE04E-F6B8-4927-96B6-91C74B502CC0
// Assembly location: H:\7\BaconDistantWorlds.dll

using DistantWorlds;
using DistantWorlds.Controls;
using DistantWorlds.Types;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Drawing;
using System.Linq;

namespace BaconDistantWorlds
{
    public static class BaconMainView
    {
        public static void method_253(
          MainView mainview,
          SpriteBatch spriteBatch_2,
          BuiltObject builtObject_1,
          int int_11,
          int int_12,
          int int_13,
          int int_14,
          double double_15,
          bool bool_13,
          Color color_18,
          int int_15)
        {
            try
            {
                if (builtObject_1 == null || builtObject_1.HasBeenDestroyed || builtObject_1.Role == BuiltObjectRole.Base || builtObject_1.TopSpeed <= (short)0 || builtObject_1.WarpSpeed <= 0 || (double)builtObject_1.CurrentSpeed <= (double)builtObject_1.TopSpeed && !builtObject_1.HyperjumpPrepare || builtObject_1.Mission == null || builtObject_1.Mission.Type == BuiltObjectMissionType.Undefined || !bool_13 && builtObject_1.ShipGroup != null && builtObject_1.ShipGroup.LeadShip != builtObject_1)
                    return;
                //var temp = builtObject_1.Mission.ShowAllCommands().Last();
                //if (temp != null && temp.Action == CommandAction.MoveTo && temp.Xpos == (float)-2.00000013E+09 && temp.TargetBuiltObject == null && temp.TargetCreature == null && 
                //    temp.TargetHabitat == null && temp.TargetShipGroup == null)
                //    return;

                Point point = builtObject_1.Mission.ResolveTargetCoordinatesCurrentCommand();

                if (point.IsEmpty )
                {
                    if (builtObject_1.ParentBuiltObject != null)
                    { point = new Point((int)builtObject_1.ParentBuiltObject.Xpos, (int)builtObject_1.ParentBuiltObject.Ypos); }
                    else if (builtObject_1.ParentHabitat != null)
                    { point = new Point((int)builtObject_1.ParentHabitat.Xpos, (int)builtObject_1.ParentHabitat.Ypos); }
                }
                int num1 = (int)((double)(point.X - int_13) / double_15);
                int num2 = (int)((double)(point.Y - int_14) / double_15);
                if ((Math.Abs((double)point.X - builtObject_1.Xpos) + Math.Abs((double)point.Y - builtObject_1.Ypos)) * (1500.0 / double_15) > 40000.0)
                    XnaDrawingHelper.DrawLine(spriteBatch_2, int_11, int_12, num1, num2, color_18, int_15, true, mainview.texture2D_35);
            }
            catch (Exception ex)
            {
                if (BaconBuiltObject.myMain != null)
                    BaconBuiltObject.myMain._Game.Galaxy.Pause();
            }
        }
    }
}
