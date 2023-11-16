using BaconDistantWorlds;
using DistantWorlds.Types;
using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public partial class MainView
    {
        private System.Drawing.Color method_75(Habitat habitat_1)
        {
            if (habitat_1 != null && habitat_1.Category == HabitatCategoryType.Star)
            {
                int mapPictureRef = habitat_1.MapPictureRef;
                switch (habitat_1.Type)
                {
                    case HabitatType.MainSequence:
                        //return (habitat_1.MapPictureRef - main_0.int_6) switch
                        return (habitat_1.MapPictureRef) switch
                        {
                            0 => System.Drawing.Color.FromArgb(255, 255, 200),
                            1 => System.Drawing.Color.FromArgb(255, 248, 160),
                            2 => System.Drawing.Color.FromArgb(255, 232, 96),
                            3 => System.Drawing.Color.FromArgb(255, 200, 48),
                            4 => System.Drawing.Color.FromArgb(255, 192, 0),
                            _ => System.Drawing.Color.FromArgb(255, 176, 0),
                        };
                    case HabitatType.RedGiant:
                        mapPictureRef = habitat_1.MapPictureRef - main_0.DiZcdsnvl0;
                        return System.Drawing.Color.FromArgb(255, 32, 0);
                    case HabitatType.SuperGiant:
                        mapPictureRef = habitat_1.MapPictureRef - main_0.int_7;
                        return System.Drawing.Color.FromArgb(255, 64, 0);
                    case HabitatType.WhiteDwarf:
                        return (habitat_1.MapPictureRef - main_0.int_8) switch
                        {
                            0 => System.Drawing.Color.FromArgb(128, 232, 255),
                            1 => System.Drawing.Color.FromArgb(176, 255, 255),
                            _ => System.Drawing.Color.FromArgb(224, 255, 255),
                        };
                    case HabitatType.Neutron:
                        if (habitat_1.MapPictureRef - main_0.int_9 == 0)
                        {
                            return System.Drawing.Color.FromArgb(80, 80, 255);
                        }
                        return System.Drawing.Color.FromArgb(96, 208, 255);
                }
            }
            return System.Drawing.Color.FromArgb(255, 255, 144);
        }

        private void method_76()
        {
            int num = main_0.int_13 + main_0.int_21;
            int num2 = main_0.int_14 + main_0.vhadzRiecM;
            if (main_0._Game == null || main_0._Game.Galaxy == null)
            {
                return;
            }
            bool fadeCivilianShips = false;
            if (main_0.gameOptions_0 != null)
            {
                fadeCivilianShips = main_0.gameOptions_0.MapOverlayFadeCivilianShips;
            }
            Empire empire = main_0._Game.PlayerEmpire;
            if (main_0.empire_1 != null)
            {
                empire = main_0.empire_1;
            }
            DateTime currentDateTime = main_0._Game.Galaxy.CurrentDateTime;
            long currentStarDate = main_0._Game.Galaxy.CurrentStarDate;
            DateTime now = DateTime.Now;
            list_4.Clear();
            list_3.Clear();
            int num3 = (int)((double)base.ClientRectangle.Width * main_0.double_0);
            num3 += Galaxy.MaxSolarSystemSize * 2;
            BuiltObjectList builtObjectsAtLocation = galaxy_0.GetBuiltObjectsAtLocation(num, num2, num3);
            FighterList fightersForBuiltObjects = galaxy_0.GetFightersForBuiltObjects(builtObjectsAtLocation);
            Habitat habitat = null;
            if (main_0.int_28 >= 0)
            {
                habitat = galaxy_0.Habitats[main_0.int_28];
            }
            if (main_0.double_0 < 150.0)
            {
                GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.NebulaCloud);
                GalaxyLocationList galaxyLocationList2 = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.SuperNova);
                bool flag = false;
                if (galaxyLocationList.Count > 0)
                {
                    for (int i = 0; i < galaxyLocationList.Count; i++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[i];
                        if (galaxyLocation.Effect == GalaxyLocationEffectType.LightningDamage)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (galaxyLocationList.Count <= 0 && galaxyLocationList2.Count <= 0 && !flag)
                {
                    color_15 = System.Drawing.Color.Empty;
                }
                else
                {
                    if (galaxyLocationList2.Count > 0)
                    {
                        color_15 = method_29();
                    }
                    else if (galaxyLocationList.Count > 0)
                    {
                        if (!bool_7)
                        {
                            color_15 = method_122(num, num2);
                        }
                        else
                        {
                            color_15 = System.Drawing.Color.Empty;
                        }
                    }
                    if (flag)
                    {
                        System.Drawing.Color color = method_28(currentDateTime);
                        if (!color.IsEmpty)
                        {
                            color_15 = color;
                        }
                    }
                    if (color_15 != System.Drawing.Color.Empty)
                    {
                        double num4 = 1.0;
                        num4 = ((!(main_0.double_0 < 75.0)) ? ((double)(int)color_15.A / 255.0 * ((150.0 - main_0.double_0) / 167.0 + 0.35)) : ((double)(int)color_15.A / 255.0 * ((main_0.double_0 + 30.0) / 167.0 + 0.35)));
                        int red = (int)((double)(int)color_15.R * num4);
                        int green = (int)((double)(int)color_15.G * num4);
                        int blue = (int)((double)(int)color_15.B * num4);
                        color_15 = System.Drawing.Color.FromArgb(255, red, green, blue);
                    }
                }
                if (color_15 != System.Drawing.Color.Empty)
                {
                    XnaDrawingHelper.FillRectangle(spriteBatch_0, base.ClientRectangle, color_15);
                }
            }
            if (habitat == null)
            {
                habitat = galaxy_0.FastFindNearestSystem(num, num2);
            }
            SystemInfo systemInfo = galaxy_0.Systems[habitat.SystemIndex];
            Empire empire2 = null;
            if (systemInfo.DominantEmpire != null)
            {
                empire2 = systemInfo.DominantEmpire.Empire;
            }
            bool flag2 = false;
            double num5 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, num, num2);
            double num6 = num5 + double_9 * main_0.double_0;
            if ((int)num6 > Galaxy.MaxSolarSystemSize + 500)
            {
                flag2 = true;
            }
            if (flag2)
            {
                BuiltObject builtObject = galaxy_0.FastFindNearestLongRangeScanner(num, num2, empire);
                if (builtObject != null)
                {
                    double num7 = galaxy_0.CalculateDistance(builtObject.Xpos, builtObject.Ypos, num, num2);
                    num6 = num7 + double_9 * main_0.double_0;
                    if ((int)num6 < builtObject.SensorLongRange)
                    {
                        flag2 = false;
                    }
                }
            }
            bool flag3 = false;
            SystemVisibilityStatus systemVisibilityStatus = SystemVisibilityStatus.Undefined;
            if (habitat != null)
            {
                systemVisibilityStatus = empire.CheckSystemVisibilityStatus(habitat.SystemIndex);
                System.Drawing.Rectangle rectangle;
                if (main_0._Game.GodMode)
                {
                    systemVisibilityStatus = SystemVisibilityStatus.Visible;
                    rectangle = default(System.Drawing.Rectangle);
                    //SystemVisibilityStatus systemVisibilityStatus2 = SystemVisibilityStatus.Visible;
                    goto IL_05ae;
                }
                rectangle = default(System.Drawing.Rectangle);
                switch (systemVisibilityStatus)
                {
                    case SystemVisibilityStatus.Explored:
                        break;
                    case SystemVisibilityStatus.Visible:
                        goto IL_05ae;
                    default:
                        goto IL_05fc;
                }
                flag3 = true;
                if (flag2)
                {
                    rectangle = method_34((int)habitat.Xpos, (int)habitat.Ypos, Galaxy.MaxSolarSystemSize * 2 + 1000, Galaxy.MaxSolarSystemSize * 2 + 1000, main_0.double_0);
                    graphicsPath_2.AddEllipse(rectangle);
                    graphicsPath_0.AddEllipse(rectangle);
                }
                else
                {
                    rectangle = new System.Drawing.Rectangle(0, 0, base.Width, base.Height);
                    graphicsPath_0.AddRectangle(rectangle);
                }
            }
            goto IL_05fc;
        IL_05fc:
            bool flag4 = false;
            for (int j = 0; j < builtObjectsAtLocation.Count; j++)
            {
                BuiltObject builtObject2 = builtObjectsAtLocation[j];
                if (builtObject2 == null || builtObject2.Empire != empire)
                {
                    continue;
                }
                int num8 = Galaxy.ThreatRange;
                if (builtObject2.SensorProximityArrayRange > num8)
                {
                    num8 = builtObject2.SensorProximityArrayRange;
                }
                if (builtObject2.SensorLongRange > num8)
                {
                    num8 = builtObject2.SensorLongRange;
                }
                if (habitat != null)
                {
                    double num9 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num9 - num8 > Galaxy.MaxSolarSystemSize + 500)
                    {
                        System.Drawing.Rectangle rectangle2 = method_34((int)builtObject2.Xpos, (int)builtObject2.Ypos, num8 * 2, num8 * 2, main_0.double_0);
                        if (rectangle2.Width > 8 && method_151(base.ClientRectangle, rectangle2))
                        {
                            graphicsPath_2.AddEllipse(rectangle2);
                            graphicsPath_1.AddEllipse(rectangle2);
                            flag3 = false;
                        }
                    }
                    if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored && (int)num9 - num8 <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                    continue;
                }
                System.Drawing.Rectangle rectangle3 = method_34((int)builtObject2.Xpos, (int)builtObject2.Ypos, num8 * 2, num8 * 2, main_0.double_0);
                if (rectangle3.Width > 8 && method_151(base.ClientRectangle, rectangle3))
                {
                    graphicsPath_2.AddEllipse(rectangle3);
                    graphicsPath_1.AddEllipse(rectangle3);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    double num10 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject2.Xpos, builtObject2.Ypos);
                    if ((int)num10 - num8 <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                }
            }
            for (int k = 0; k < empire.LongRangeScanners.Count; k++)
            {
                BuiltObject builtObject3 = empire.LongRangeScanners[k];
                if (builtObject3 == null)
                {
                    continue;
                }
                int sensorLongRange = builtObject3.SensorLongRange;
                if (habitat != null)
                {
                    double num11 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                    if ((int)num11 - sensorLongRange <= Galaxy.MaxSolarSystemSize + 500 && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                    {
                        flag4 = true;
                        System.Drawing.Rectangle rectangle4 = method_34((int)builtObject3.Xpos, (int)builtObject3.Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                        if (rectangle4.Width > 8 && method_151(base.ClientRectangle, rectangle4))
                        {
                            flag3 = ((!rectangle4.Contains(base.ClientRectangle)) ? true : false);
                            graphicsPath_2.AddEllipse(rectangle4);
                            graphicsPath_1.AddEllipse(rectangle4);
                        }
                    }
                    continue;
                }
                System.Drawing.Rectangle rectangle5 = method_34((int)builtObject3.Xpos, (int)builtObject3.Ypos, sensorLongRange * 2, sensorLongRange * 2, main_0.double_0);
                if (rectangle5.Width > 8 && method_151(base.ClientRectangle, rectangle5))
                {
                    flag3 = ((!rectangle5.Contains(base.ClientRectangle)) ? true : false);
                    graphicsPath_2.AddEllipse(rectangle5);
                    graphicsPath_1.AddEllipse(rectangle5);
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Unexplored)
                {
                    double num12 = galaxy_0.CalculateDistance(habitat.Xpos, habitat.Ypos, builtObject3.Xpos, builtObject3.Ypos);
                    if ((int)num12 + sensorLongRange <= Galaxy.MaxSolarSystemSize + 500)
                    {
                        flag4 = true;
                    }
                }
            }
            int int_;
            int int_2;
            if (main_0.int_28 >= 0)
            {
                if (main_0.double_0 < 500.0)
                {
                    for (int l = 0; l < builtObjectsAtLocation.Count; l++)
                    {
                        BuiltObject builtObject4 = builtObjectsAtLocation[l];
                        if (builtObject4 == null)
                        {
                            continue;
                        }
                        if (builtObject4.HyperDenyActive)
                        {
                            int num13 = (int)((builtObject4.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num14 = (int)((builtObject4.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num13 >= -1000 && num13 <= base.Width + 1000 && num14 >= -1000 && num14 <= base.Height + 1000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject4)))
                            {
                                int num15 = (int)((double)builtObject4.WeaponHyperDenyRange / main_0.double_0);
                                XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num13 - num15, num14 - num15, num15 * 2, num15 * 2), spriteBatch: spriteBatch_0, texture: this.texture2D_4[0], rotationAngle: 0f, tintColor: System.Drawing.Color.FromArgb(128, 255, 255, 255));
                            }
                        }
                        else
                        {
                            if (builtObject4.HyperStopRange <= 0 || !(main_0.double_0 < 50.0))
                            {
                                continue;
                            }
                            int num16 = (int)((builtObject4.Xpos - (double)num) / main_0.double_0) + base.Width / 2;
                            int num17 = (int)((builtObject4.Ypos - (double)num2) / main_0.double_0) + base.Height / 2;
                            if (num16 >= -3000 && num16 <= base.Width + 3000 && num17 >= -3000 && num17 <= base.Height + 3000 && (main_0._Game.GodMode || empire.IsObjectVisibleToThisEmpire(builtObject4)))
                            {
                                int num18 = (int)((double)builtObject4.HyperStopRange / main_0.double_0);
                                System.Drawing.Rectangle area = new System.Drawing.Rectangle(num16 - num18, num17 - num18, num18 * 2, num18 * 2);
                                int val = 255 - (int)(230.0 * (main_0.double_0 / 50.0));
                                val = Math.Min(255, Math.Max(0, val));
                                System.Drawing.Color color_ = System.Drawing.Color.FromArgb(val, 255, 32, 64);
                                System.Drawing.Color color_2 = System.Drawing.Color.FromArgb(val, 64, 32, 192);
                                System.Drawing.Color color2 = method_214(color_, color_2, currentDateTime);
                                float num19 = 1f;
                                if (main_0.double_0 < 1.2)
                                {
                                    num19 = 4f;
                                }
                                else if (main_0.double_0 < 3.0)
                                {
                                    num19 = 3f;
                                }
                                else if (main_0.double_0 < 9.0)
                                {
                                    num19 = 2f;
                                }
                                XnaDrawingHelper.DrawCircle(spriteBatch_0, area, 120, color2, (int)num19, dashed: true);
                            }
                        }
                    }
                    GalaxyLocationList galaxyLocationList3 = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(num, num2, double_9 * main_0.double_0, GalaxyLocationType.RestrictedArea);
                    for (int m = 0; m < galaxyLocationList3.Count; m++)
                    {
                        int num20 = (int)((double)galaxyLocationList3[m].Width / 2.0 / main_0.double_0);
                        int num21 = (int)((double)galaxyLocationList3[m].Width / 2.0);
                        int num22 = num21 * -1;
                        int num23 = (int)(((double)(galaxyLocationList3[m].Xpos + (float)num21) - (double)num) / main_0.double_0) + base.Width / 2;
                        int num24 = (int)(((double)(galaxyLocationList3[m].Ypos + (float)num21) - (double)num2) / main_0.double_0) + base.Height / 2;
                        if (num23 >= num22 && num23 <= base.Width + num21 && num24 >= num22 && num24 <= base.Height + num21)
                        {
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num23 - num20, num24 - num20, num20 * 2, num20 * 2), spriteBatch: spriteBatch_0, texture: this.texture2D_4[0], rotationAngle: 0f, tintColor: System.Drawing.Color.FromArgb(128, 255, 255, 255));
                        }
                    }
                }
                if (main_0.double_0 < BaconMain.backgroundStarsAtZoomLevel)
                {
                    if (bool_9)
                    {
                        method_108(spriteBatch_0);
                    }
                    else
                    {
                        method_46();
                        method_106(solidBrush_9.Color, starFieldItemList_3, rectangle_4, this.int_4, 38, spriteBatch_0);
                        method_106(solidBrush_8.Color, starFieldItemList_2, rectangle_3, this.int_3, 20, spriteBatch_0);
                        method_106(solidBrush_7.Color, starFieldItemList_1, this.rectangle_2, this.int_2, 11, spriteBatch_0);
                    }
                    method_102(spriteBatch_0, solidBrush_6, starFieldItemList_0, rectangle_1, int_1, 6);
                }
                if (systemVisibilityStatus != SystemVisibilityStatus.Explored && systemVisibilityStatus != SystemVisibilityStatus.Visible)
                {
                    if (flag4 && main_0.double_0 >= 5.0)
                    {
                        int num25 = (int)((galaxy_0.Habitats[main_0.int_28].Xpos - (double)num) / main_0.double_0) + base.ClientRectangle.Width / 2;
                        int num26 = (int)((galaxy_0.Habitats[main_0.int_28].Ypos - (double)num2) / main_0.double_0) + base.ClientRectangle.Height / 2;
                        for (int n = main_0.int_28; n <= main_0.int_29; n++)
                        {
                            if (n < galaxy_0.Habitats.Count)
                            {
                                Habitat habitat2 = galaxy_0.Habitats[n];
                                if (habitat2 != null && habitat2.Category == HabitatCategoryType.Planet && empire.IsObjectVisibleToThisEmpire(habitat2))
                                {
                                    int num27 = (int)((double)habitat2.OrbitDistance / main_0.double_0);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, num25 - num27, num26 - num27, num27 * 2, num27 * 2, main_0.pen_1.Color, (int)main_0.pen_1.Width, 100);
                                }
                            }
                        }
                    }
                }
                else if (main_0.double_0 >= 5.0)
                {
                    int num28 = (int)((galaxy_0.Habitats[main_0.int_28].Xpos - (double)num) / main_0.double_0) + base.ClientRectangle.Width / 2;
                    int num29 = (int)((galaxy_0.Habitats[main_0.int_28].Ypos - (double)num2) / main_0.double_0) + base.ClientRectangle.Height / 2;
                    for (int num30 = main_0.int_28; num30 <= main_0.int_29; num30++)
                    {
                        if (num30 < galaxy_0.Habitats.Count)
                        {
                            Habitat habitat3 = galaxy_0.Habitats[num30];
                            if (habitat3 != null && habitat3.Category == HabitatCategoryType.Planet)
                            {
                                int num31 = (int)((double)habitat3.OrbitDistance / main_0.double_0);
                                XnaDrawingHelper.DrawCircle(spriteBatch_0, num28 - num31, num29 - num31, num31 * 2, num31 * 2, main_0.pen_1.Color, (int)main_0.pen_1.Width, 100);
                            }
                        }
                    }
                }
                if (main_0.double_0 < 500.0)
                {
                    double num32 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                    double num33 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                    for (int num34 = main_0.int_28; num34 <= main_0.int_29; num34++)
                    {
                        if ((systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num34 > main_0.int_28 && !flag4) || num34 >= galaxy_0.Habitats.Count)
                        {
                            continue;
                        }
                        Habitat habitat4 = galaxy_0.Habitats[num34];
                        if (habitat4 == null || (flag4 && systemVisibilityStatus == SystemVisibilityStatus.Unexplored && habitat4.Category != 0 && habitat4.Category != HabitatCategoryType.GasCloud && !empire.IsObjectVisibleToThisEmpire(habitat4)))
                        {
                            continue;
                        }
                        double double_ = main_0.double_0;
                        switch (habitat4.Category)
                        {
                            case HabitatCategoryType.Planet:
                                double_ = num32;
                                break;
                            case HabitatCategoryType.Moon:
                                double_ = num33;
                                break;
                        }
                        Texture2D texture2D_ = method_58(num34 - main_0.int_28);
                        lVxRufUsger(texture2D_, habitat4.Diameter, double_);
                        method_59(habitat4, double_, 4, out int_, out int_2);
                        int num35 = 4;
                        if (habitat4.Category == HabitatCategoryType.Asteroid)
                        {
                            num35 = 1;
                        }
                        System.Drawing.Rectangle rectangle6 = method_35((int)habitat4.Xpos, (int)habitat4.Ypos, int_, int_2, double_, num35);
                        int num36 = rectangle6.X;
                        int num37 = rectangle6.Y;
                        int_ = rectangle6.Width;
                        int_2 = rectangle6.Height;
                        int num38 = -20;
                        int num39 = base.Width + 20;
                        int num40 = -20;
                        int num41 = base.Height + 20;
                        if (habitat4.HasRings || habitat4.Category == HabitatCategoryType.Star)
                        {
                            num38 = -500;
                            num39 = base.Width + 500;
                            num40 = -500;
                            num41 = base.Height + 500;
                        }
                        if (num36 + int_ >= num38 && num36 <= num39 && num37 + int_2 >= num40 && num37 <= num41)
                        {
                            Bitmap bitmap = null;
                            if ((habitat4.Category == HabitatCategoryType.Moon || habitat4.Category == HabitatCategoryType.Planet) && list_5.Count > num34 - main_0.int_28 && list_5[num34 - main_0.int_28] != null)
                            {
                                DateTime value = list_7[num34 - main_0.int_28];
                                double num42 = (double)habitat4.OrbitDistance / 100.0;
                                if (habitat4.Category == HabitatCategoryType.Moon)
                                {
                                    num42 = (double)habitat4.OrbitDistance / 10.0;
                                }
                                if (habitat4.Damage > 0f)
                                {
                                    num42 /= 10.0;
                                }
                                if (now.Subtract(value).TotalSeconds > num42)
                                {
                                    if (list_5.Count > num34 - main_0.int_28)
                                    {
                                        if (list_5[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_5[num34 - main_0.int_28]);
                                        }
                                        list_5[num34 - main_0.int_28] = null;
                                    }
                                    if (list_6.Count > num34 - main_0.int_28)
                                    {
                                        if (list_6[num34 - main_0.int_28] != null)
                                        {
                                            method_22(list_6[num34 - main_0.int_28]);
                                        }
                                        list_6[num34 - main_0.int_28] = null;
                                    }
                                    if (list_8.Count > num34 - main_0.int_28)
                                    {
                                        if (list_8[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_8[num34 - main_0.int_28]);
                                        }
                                        list_8[num34 - main_0.int_28] = null;
                                    }
                                    if (list_7.Count > num34 - main_0.int_28)
                                    {
                                        list_7[num34 - main_0.int_28] = DateTime.MinValue;
                                    }
                                }
                            }
                            if (habitat4.Category != HabitatCategoryType.GasCloud)
                            {
                                if (list_5.Count <= num34 - main_0.int_28)
                                {
                                    method_59(habitat4, double_, num35, out int_, out int_2);
                                    int num43 = num34 - main_0.int_28 - list_5.Count;
                                    for (int num44 = 0; num44 < num43; num44++)
                                    {
                                        list_5.Add(null);
                                        list_6.Add(null);
                                        list_8.Add(null);
                                        list_7.Add(DateTime.MinValue);
                                    }
                                    if (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy)
                                    {
                                        bitmap = null;
                                        bool bool_ = false;
                                        Texture2D item = method_78(habitat4, bool_13: true, out bitmap, out bool_);
                                        list_5.Add(bitmap);
                                        list_8.Add(null);
                                        list_6.Add(item);
                                        if (bool_)
                                        {
                                            double num45 = (double)habitat4.OrbitDistance / 100.0;
                                            if (habitat4.Category == HabitatCategoryType.Moon)
                                            {
                                                num45 = (double)habitat4.OrbitDistance / 10.0;
                                            }
                                            int num46 = (int)(num45 * 1000.0);
                                            int num47 = 200 + Galaxy.Rnd.Next(0, 100);
                                            int num48 = Math.Max(0, num46 - num47);
                                            long ticks = num48 * 10000L;
                                            DateTime item2 = now.Subtract(new TimeSpan(ticks));
                                            list_7.Add(item2);
                                        }
                                        else
                                        {
                                            list_7.Add(now);
                                        }
                                    }
                                }
                                else if (list_5[num34 - main_0.int_28] == null)
                                {
                                    method_59(habitat4, double_, num35, out int_, out int_2);
                                    if (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy)
                                    {
                                        bitmap = null;
                                        bool bool_2 = false;
                                        Texture2D value2 = method_78(habitat4, bool_13: false, out bitmap, out bool_2);
                                        if (list_5.Count > num34 - main_0.int_28)
                                        {
                                            if (list_5[num34 - main_0.int_28] != null)
                                            {
                                                method_21(list_5[num34 - main_0.int_28]);
                                            }
                                            list_5[num34 - main_0.int_28] = bitmap;
                                        }
                                        if (list_6.Count > num34 - main_0.int_28)
                                        {
                                            if (list_6[num34 - main_0.int_28] != null)
                                            {
                                                method_22(list_6[num34 - main_0.int_28]);
                                            }
                                            list_6[num34 - main_0.int_28] = value2;
                                        }
                                        if (list_8.Count > num34 - main_0.int_28 && list_8[num34 - main_0.int_28] != null)
                                        {
                                            method_21(list_8[num34 - main_0.int_28]);
                                        }
                                        if (list_7.Count > num34 - main_0.int_28)
                                        {
                                            if (bool_2)
                                            {
                                                double num49 = (double)habitat4.OrbitDistance / 100.0;
                                                if (habitat4.Category == HabitatCategoryType.Moon)
                                                {
                                                    num49 = (double)habitat4.OrbitDistance / 10.0;
                                                }
                                                int num50 = (int)(num49 * 1000.0);
                                                int num51 = 200 + Galaxy.Rnd.Next(0, 100);
                                                int num52 = Math.Max(0, num50 - num51);
                                                long ticks2 = num52 * 10000L;
                                                DateTime value3 = now.Subtract(new TimeSpan(ticks2));
                                                list_7[num34 - main_0.int_28] = value3;
                                            }
                                            else
                                            {
                                                list_7[num34 - main_0.int_28] = now;
                                            }
                                        }
                                    }
                                }
                            }
                            if (habitat4.Category != HabitatCategoryType.GasCloud && (habitat4.Explosion == null || !habitat4.Explosion.ExplosionWillDestroy))
                            {
                                if (habitat4.Category == HabitatCategoryType.Star)
                                {
                                    int_ = habitat4.Diameter;
                                    int_2 = habitat4.Diameter;
                                }
                                else
                                {
                                    bitmap = list_5[num34 - main_0.int_28];
                                    if (bitmap != null && bitmap.PixelFormat != 0)
                                    {
                                        float num53 = (float)bitmap.Width / (float)bitmap.Height;
                                        int_ = (int)((double)habitat4.Diameter * (double)num53);
                                        int_2 = (int)(double)habitat4.Diameter;
                                    }
                                }
                                rectangle6 = method_35((int)habitat4.Xpos, (int)habitat4.Ypos, int_, int_2, double_, num35);
                                num36 = rectangle6.X;
                                num37 = rectangle6.Y;
                                int_ = rectangle6.Width;
                                int_2 = rectangle6.Height;
                            }
                            if (habitat4.Type == HabitatType.BlackHole)
                            {
                                double totalSeconds = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                this.double_4 += totalSeconds * 0.4;
                                Texture2D texture2D = texture2D_12[0];
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                new System.Drawing.Rectangle(0, 0, 10, 10);
                                double num54 = this.double_4 / 7.0;
                                System.Drawing.Color color_3 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_4 = System.Drawing.Color.DarkGray;
                                System.Drawing.Color color_5 = System.Drawing.Color.DarkGray;
                                switch (habitat4.HabitatIndex % 4)
                                {
                                    case 0:
                                        color_3 = System.Drawing.Color.FromArgb(0, 0, 216);
                                        color_4 = System.Drawing.Color.FromArgb(0, 0, 138);
                                        color_5 = System.Drawing.Color.FromArgb(0, 0, 112);
                                        break;
                                    case 1:
                                        color_3 = System.Drawing.Color.FromArgb(84, 54, 138);
                                        color_4 = System.Drawing.Color.FromArgb(56, 30, 84);
                                        color_5 = System.Drawing.Color.FromArgb(48, 20, 60);
                                        break;
                                    case 2:
                                        color_3 = System.Drawing.Color.FromArgb(54, 32, 84);
                                        color_4 = System.Drawing.Color.FromArgb(38, 24, 60);
                                        color_5 = System.Drawing.Color.FromArgb(32, 20, 48);
                                        break;
                                    case 3:
                                        color_3 = System.Drawing.Color.FromArgb(84, 30, 96);
                                        color_4 = System.Drawing.Color.FromArgb(62, 20, 72);
                                        color_5 = System.Drawing.Color.FromArgb(56, 16, 64);
                                        break;
                                }
                                int num55 = (int)((double)int_ * 0.07);
                                int num56 = (int)((double)int_2 * 0.07);
                                int num57 = num36 + (int_ - num55) / 2;
                                int num58 = num37 + (int_2 - num56) / 2;
                                if (color_15 == System.Drawing.Color.Empty)
                                {
                                    using (new SolidBrush(System.Drawing.Color.Black))
                                    {
                                    }
                                }
                                num55 = (int)((double)int_ * 1.0);
                                num56 = (int)((double)int_2 * 1.0);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                num54 = this.double_4 / 5.3;
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, num54, color_5);
                                num55 = (int)((double)int_ * 0.44);
                                num56 = (int)((double)int_2 * 0.44);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                num54 = this.double_4 / 2.3;
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, num54, color_4);
                                num55 = (int)((double)int_ * 0.2);
                                num56 = (int)((double)int_2 * 0.2);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                new System.Drawing.Rectangle(0, 0, texture2D.Width, texture2D.Height);
                                new System.Drawing.Rectangle(num57, num58, num55, num56);
                                method_118(spriteBatch_0, num57, num58, num55, num56, texture2D, currentDateTime, this.double_4, color_3);
                                double_5 += totalSeconds * 0.5;
                                num55 = (int)((double)int_ * 0.08);
                                num56 = (int)((double)int_2 * 0.08);
                                num57 = num36 + (int_ - num55) / 2;
                                num58 = num37 + (int_2 - num56) / 2;
                                method_117(spriteBatch_0, num57, num58, num55, num56, texture2D_13, currentDateTime, 15, 0.0, 0.0, System.Drawing.Color.Empty);
                                dateTime_2 = currentDateTime;
                            }
                            else if (habitat4.Category == HabitatCategoryType.GasCloud)
                            {
                                DrawGasCloudToMainXna(spriteBatch_0, bitmap_2, habitat4, num36, num37, int_, int_2);
                            }
                            else if (habitat4.Type != HabitatType.MainSequence && habitat4.Type != HabitatType.RedGiant && habitat4.Type != HabitatType.SuperGiant && habitat4.Type != HabitatType.WhiteDwarf && habitat4.Type != HabitatType.Neutron)
                            {
                                if (habitat4.Type != HabitatType.SuperNova && list_6.Count > num34 - main_0.int_28 && list_6[num34 - main_0.int_28] != null)
                                {
                                    XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num36, num37, int_, int_2), spriteBatch: spriteBatch_0, texture: list_6[num34 - main_0.int_28], rotationAngle: 0f);
                                }
                            }
                            else if (main_0.double_0 < method_60(habitat4.Type))
                            {
                                int num59 = (int)((double)int_ * 0.98);
                                int num60 = (int)((double)int_2 * 0.98);
                                int int_3 = num36 + (int_ - num59) / 2;
                                int int_4 = num37 + (int_2 - num60) / 2;
                                System.Drawing.Color color_6 = method_120(main_0.bitmap_196[habitat4.MapPictureRef]);
                                System.Drawing.Color color3 = method_65(color_6, 1.0);
                                System.Drawing.Color color4 = method_65(color_6, 1.0);
                                System.Drawing.Color baseColor = method_65(color_6, 1.0);
                                color3 = method_64(color_6, 0.99);
                                color4 = method_64(color_6, 0.85);
                                color4 = System.Drawing.Color.FromArgb(128, color4);
                                baseColor = System.Drawing.Color.FromArgb(0, baseColor);
                                double totalSeconds2 = currentDateTime.Subtract(dateTime_2).TotalSeconds;
                                this.double_4 += totalSeconds2 * 0.02;
                                System.Drawing.Rectangle rectangle7 = new System.Drawing.Rectangle(num36, num37, int_, int_2);
                                int num61 = (int)((double)int_ * 0.195);
                                rectangle7.Inflate(num61, num61);
                                double_5 -= totalSeconds2 * 0.02;
                                float num62 = (float)double_5 * -1f;
                                method_65(color_6, 1.0);
                                System.Drawing.Color color_7 = System.Drawing.Color.FromArgb(255, method_65(color_6, 1.2));
                                method_118(spriteBatch_0, int_3, int_4, num59, num60, texture2D_14[2], currentDateTime, num62, color_7);
                                System.Drawing.Color color_8 = System.Drawing.Color.FromArgb(96, method_65(color_6, 1.0));
                                method_118(spriteBatch_0, int_3, int_4, num59, num60, texture2D_14[0], currentDateTime, double_5, color_8);
                                double num63 = 1.65;
                                Texture2D[] texture2D_2 = texture2D_15;
                                int int_5 = 15;
                                if (habitat4.Type == HabitatType.Neutron)
                                {
                                    num63 = 2.3;
                                    texture2D_2 = texture2D_16;
                                    int_5 = 12;
                                }
                                int num64 = (int)((double)int_ * num63);
                                int num65 = (int)((double)int_2 * num63);
                                int num66 = (num64 - int_) / 2;
                                int num67 = (num65 - int_2) / 2;
                                int int_6 = num36 - num66;
                                int int_7 = num37 - num67;
                                System.Drawing.Color color5 = color3;
                                color5 = method_64(color_6, -0.15);
                                color5 = System.Drawing.Color.FromArgb(192, color5);
                                color5 = System.Drawing.Color.FromArgb(160, method_64(color_6, 0.3));
                                color5 = System.Drawing.Color.FromArgb(240, method_65(color_6, 1.2));
                                method_117(spriteBatch_0, int_6, int_7, num64, num65, texture2D_2, currentDateTime, int_5, 0.0, 0.0, color5);
                            }
                            else if (list_6.Count > num34 - main_0.int_28 && list_6[num34 - main_0.int_28] != null)
                            {
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, list_6[num34 - main_0.int_28], num36, num37, int_, int_2, 0f);
                            }
                            if (habitat4.IsBlockaded)
                            {
                                System.Drawing.Rectangle rectangle8 = method_81(num36 - 14, num37 - 14, int_ + 28, int_2 + 28);
                                method_99(spriteBatch_0, rectangle8.X, rectangle8.Y, rectangle8.Width, rectangle8.Height);
                            }
                            if (main_0.int_34 < 2 && main_0.double_0 < 100.0 && !habitat4.HasBeenDestroyed)
                            {
                                int num68 = 28;
                                num68 = (int)(28.0 / main_0.double_0);
                                num68 = Math.Max(6, num68);
                                int num69 = num68 * 2;
                                if (habitat4.Owner != null && habitat4.Owner != main_0._Game.Galaxy.IndependentEmpire)
                                {
                                    System.Drawing.Color color6 = habitat4.Owner.MainColor;
                                    if (habitat4.Owner.PirateEmpireBaseHabitat != null && habitat4.Owner.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))
                                    {
                                        color6 = System.Drawing.Color.FromArgb(48, 48, 48);
                                    }
                                    System.Drawing.Rectangle area2 = method_81(num36 - num68, num37 - num68, int_ + num69, int_2 + num69);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, area2, 60, color6, 2);
                                }
                                else if (habitat4.Owner == main_0._Game.Galaxy.IndependentEmpire && habitat4.Population != null && habitat4.Population.Count > 0)
                                {
                                    System.Drawing.Rectangle area3 = method_81(num36 - num68, num37 - num68, int_ + num69, int_2 + num69);
                                    XnaDrawingHelper.DrawCircle(spriteBatch_0, area3, 60, color_1, 2);
                                }
                                else if (habitat4.Category == HabitatCategoryType.Planet)
                                {
                                    int val2 = (int)(48.0 * ((main_0.double_0 - 5.0) / 100.0));
                                    val2 = Math.Max(0, Math.Min(val2, 255));
                                    XnaDrawingHelper.DrawCircle(area: new System.Drawing.Rectangle(num36 - num68, num37 - num68, int_ + num69, int_2 + num69), spriteBatch: spriteBatch_0, sideCount: 60, color: System.Drawing.Color.FromArgb(val2, 255, 255, 255), lineThickness: 2);
                                }
                            }
                            if (main_0.double_0 > 1.1)
                            {
                            }
                            if (habitat4 == main_0._Game.SelectedObject)
                            {
                                method_212(spriteBatch_0, num36 - 10, num37 - 10, num36 + int_ + 10, num37 + int_2 + 10);
                            }
                            if (habitat_0 != null && habitat4 == habitat_0 && !bool_4 && !this.bool_2 && !bool_1 && !bool_3)
                            {
                                method_197(rectangle_5: new System.Drawing.Rectangle(num36 - 10, num37 - (10 + (int_ - int_2) / 2), int_ + 20, int_2 + 20), spriteBatch_2: spriteBatch_0);
                            }
                            method_92(habitat4, num36 + int_ / 2, num37 + int_2 / 2, bool_13: true);
                        }
                        else if ((num36 + int_ < -800 || num36 > base.Width + 800 || num37 + int_2 < -800 || num37 > base.Height + 800) && list_5.Count > num34 - main_0.int_28)
                        {
                            list_5[num34 - main_0.int_28] = null;
                            list_6[num34 - main_0.int_28] = null;
                            list_8[num34 - main_0.int_28] = null;
                            list_7[num34 - main_0.int_28] = DateTime.MinValue;
                        }
                        ref System.Drawing.Rectangle reference = ref rectangle_0[num34 - main_0.int_28];
                        reference = new System.Drawing.Rectangle(num36 - 2, num37 - 2, int_ + 4, int_2 + 4);
                        method_173(spriteBatch_0, habitat4, num36, num37, int_, int_2, currentDateTime);
                        method_169(spriteBatch_0, habitat4, currentDateTime, num36 + int_ / 2, num37 + int_2 / 2);
                        if (habitat4.Explosions != null && habitat4.Explosions.Count > 0)
                        {
                            method_180(spriteBatch_0, habitat4, double_);
                        }
                        if (habitat4.Explosion != null)
                        {
                            method_187(spriteBatch_0, habitat4, double_);
                        }
                    }
                }
            }
            animationSystem_0.DoAnimationsXna(spriteBatch_0, currentDateTime);
            if (main_0.double_0 < 500.0)
            {
                double maxWidth = 0.0;
                double num70 = main_0.CalculateShipZoomFactor(main_0.double_0, out maxWidth);
                BuiltObjectList builtObjectList = new BuiltObjectList();
                FighterList fighterList = new FighterList();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                for (int num71 = 0; num71 < builtObjectsAtLocation.Count; num71++)
                {
                    BuiltObject builtObject5 = builtObjectsAtLocation[num71];
                    if (builtObject5 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData = main_0.builtObjectImageCache_0.FastGetImageData(builtObject5.PictureRef);
                    if (builtObjectImageData == null || builtObjectImageData.Image == null)
                    {
                        continue;
                    }
                    int num72 = builtObjectImageData.Image.Width;
                    int num73 = builtObjectImageData.Image.Height;
                    num72 = (int)((double)num72 / num70);
                    num73 = (int)((double)num73 / num70);
                    num72 = Math.Min(num72, (int)maxWidth);
                    num73 = Math.Min(num73, (int)maxWidth);
                    int num74 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - num72 / 2 + base.Width / 2;
                    int num75 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - num73 / 2 + base.Height / 2;
                    if (num74 + num72 < -50 || num74 - num72 > base.Width + 50 || num75 + num73 < -50 || num75 - num73 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(builtObject5)))
                    {
                        continue;
                    }
                    builtObjectList.Add(builtObject5);
                    bool flag5 = false;
                    if (builtObject5.TargetSpeedChanged)
                    {
                        flag5 = true;
                        builtObject5.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap2 = null;
                    Bitmap bitmap3 = null;
                    int num76 = list_14.IndexOf(builtObject5.BuiltObjectID);
                    if (num76 >= 0)
                    {
                        if (list_15[num76] == BuiltObjectImageSize.Small && builtObjectImageData.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            bitmap2 = ((builtObject5.Empire == null) ? main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0) : main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, allowPreRotate: false, 1.0));
                            list_9[num76] = bitmap2;
                        }
                        list_15[num76] = builtObjectImageData.ImageSize;
                        if (method_71(builtObject5))
                        {
                            if (list_10[num76] != null)
                            {
                                method_21(list_10[num76]);
                            }
                            list_10[num76] = method_73(builtObject5, list_9[num76], builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: false);
                            if (list_11[num76] != null)
                            {
                                method_22(list_11[num76]);
                            }
                            list_11[num76] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_10[num76]);
                        }
                        bitmap2 = list_10[num76];
                        _ = list_9[num76];
                        bitmap3 = list_12[num76];
                        if (flag5)
                        {
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, list_9[num76]);
                            if (list_12[num76] != null)
                            {
                                method_21(list_12[num76]);
                            }
                            list_12[num76] = bitmap3;
                            if (list_13[num76] != null)
                            {
                                method_22(list_13[num76]);
                            }
                            list_13[num76] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3);
                            flag5 = false;
                        }
                    }
                    else
                    {
                        if (builtObject5.Empire != null)
                        {
                            bitmap2 = main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, builtObject5.Empire.MainColor, builtObject5.Empire.SecondaryColor, allowPreRotate: false, 1.0);
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, bitmap2);
                        }
                        else
                        {
                            bitmap2 = main_0.PrepareBuiltObjectImageNEW(builtObject5, builtObjectImageData, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0);
                            bitmap3 = main_0.PrepareEngineExhaust(builtObject5, bitmap2);
                        }
                        list_14.Add(builtObject5.BuiltObjectID);
                        list_9.Add(bitmap2);
                        bitmap2 = method_73(builtObject5, bitmap2, builtObjectImageData.Image.Size, builtObjectImageData.MaskImage, builtObjectImageData.LightPoints, bool_13: false);
                        list_10.Add(bitmap2);
                        list_11.Add(XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap2));
                        list_12.Add(bitmap3);
                        list_13.Add(XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3));
                        list_15.Add(builtObjectImageData.ImageSize);
                    }
                    num72 = bitmap2.Width;
                    num73 = bitmap2.Height;
                    num72 = Math.Min(num72, (int)maxWidth);
                    num73 = Math.Min(num73, (int)maxWidth);
                    num74 = (int)((double)((int)builtObject5.Xpos - num) / main_0.double_0) - num72 / 2 + base.Width / 2;
                    num75 = (int)((double)((int)builtObject5.Ypos - num2) / main_0.double_0) - num73 / 2 + base.Height / 2;
                    if (num74 + num72 >= -50 && num74 - num72 <= base.Width + 50 && num75 + num73 >= -50 && num75 - num73 <= base.Height + 50)
                    {
                    }
                }
                for (int num77 = 0; num77 < builtObjectList.Count; num77++)
                {
                    BuiltObject builtObject6 = builtObjectList[num77];
                    if (builtObject6 == null)
                    {
                        continue;
                    }
                    BuiltObjectImageData builtObjectImageData2 = main_0.builtObjectImageCache_0.FastGetImageData(builtObject6.PictureRef);
                    if (builtObjectImageData2 == null)
                    {
                        continue;
                    }
                    Bitmap bitmap4 = null;
                    Bitmap bitmap5 = null;
                    Bitmap bitmap6 = null;
                    Texture2D texture2D2 = null;
                    Texture2D texture2D3 = null;
                    int num78 = list_14.IndexOf(builtObject6.BuiltObjectID);
                    if (num78 >= 0)
                    {
                        bool flag6 = false;
                        if (list_15[num78] == BuiltObjectImageSize.Small && builtObjectImageData2.ImageSize == BuiltObjectImageSize.Fullsize)
                        {
                            bitmap4 = ((builtObject6.Empire == null) ? main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, System.Drawing.Color.Gray, System.Drawing.Color.Gray, allowPreRotate: false, 1.0) : main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, builtObject6.Empire.MainColor, builtObject6.Empire.SecondaryColor, allowPreRotate: false, 1.0));
                            list_9[num78] = bitmap4;
                            flag6 = true;
                        }
                        list_15[num78] = builtObjectImageData2.ImageSize;
                        if (flag6 || method_71(builtObject6))
                        {
                            if (list_10[num78] != null)
                            {
                                method_21(list_10[num78]);
                            }
                            list_10[num78] = method_73(builtObject6, list_9[num78], builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: false);
                            if (list_11[num78] != null)
                            {
                                method_22(list_11[num78]);
                            }
                            list_11[num78] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_10[num78]);
                        }
                        bitmap4 = list_10[num78];
                        texture2D2 = list_11[num78];
                        bitmap5 = list_9[num78];
                        bitmap6 = list_12[num78];
                        texture2D3 = list_13[num78];
                    }
                    else
                    {
                        bitmap4 = main_0.PrepareBuiltObjectImageNEW(builtObject6, builtObjectImageData2, builtObject6.Empire.MainColor, builtObject6.Empire.SecondaryColor, allowPreRotate: false, 1.0);
                        bitmap6 = main_0.PrepareEngineExhaust(builtObject6, bitmap4);
                        list_14.Add(builtObject6.BuiltObjectID);
                        list_9.Add(bitmap4);
                        bitmap5 = bitmap4;
                        bitmap4 = method_73(builtObject6, bitmap4, builtObjectImageData2.Image.Size, builtObjectImageData2.MaskImage, builtObjectImageData2.LightPoints, bool_13: false);
                        texture2D2 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap4);
                        texture2D3 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap6);
                        list_10.Add(bitmap4);
                        list_11.Add(texture2D2);
                        list_12.Add(bitmap6);
                        list_13.Add(texture2D3);
                        list_15.Add(builtObjectImageData2.ImageSize);
                    }
                    int val3 = bitmap4.Width;
                    int val4 = bitmap4.Height;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num79 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num80 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    if (num79 + val3 < -50 || num79 - val3 > base.Width + 50 || num80 + val4 < -50 || num80 - val4 > base.Height + 50)
                    {
                        continue;
                    }
                    int num81 = bitmap5.Width;
                    int num82 = bitmap5.Height;
                    int num83 = num79 + (val3 - num81) / 2;
                    int num84 = num80 + (val4 - num82) / 2;
                    Size size = method_77(builtObject6, builtObjectImageData2, main_0.double_0);
                    num83 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - num81 / 2 + base.Width / 2;
                    num84 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - num82 / 2 + base.Height / 2;
                    num81 = size.Width;
                    num82 = size.Height;
                    double totalSeconds3 = currentDateTime.TimeOfDay.TotalSeconds;
                    double num85 = (double)(builtObject6.BuiltObjectID % 20) / 10.0;
                    totalSeconds3 += num85;
                    double num86 = totalSeconds3 % (double_7 + double_8);
                    if (num86 < double_7)
                    {
                        if (!builtObject6.LightsOn)
                        {
                            builtObject6.LightsOn = true;
                            builtObject6.LightChanged = true;
                        }
                    }
                    else if (builtObject6.LightsOn)
                    {
                        builtObject6.LightsOn = false;
                        builtObject6.LightChanged = true;
                    }
                    int num87 = (int)((double)num81 * 1.3);
                    int num88 = (int)((double)num82 * 1.3);
                    val3 = num81;
                    val4 = num82;
                    val3 = Math.Min(val3, (int)maxWidth);
                    val4 = Math.Min(val4, (int)maxWidth);
                    int num89 = (int)((double)((int)builtObject6.Xpos - num) / main_0.double_0) - val3 / 2 + base.Width / 2;
                    int num90 = (int)((double)((int)builtObject6.Ypos - num2) / main_0.double_0) - val4 / 2 + base.Height / 2;
                    int num91 = num89 - (num87 - val3) / 2;
                    int num92 = num90 - (num88 - val4) / 2;
                    if (!builtObject6.HasBeenDestroyed)
                    {
                        if (main_0.int_34 < 2 && builtObject6.Empire != null)
                        {
                            bool flag7 = true;
                            if (main_0.double_0 > 20.0)
                            {
                                if (builtObject6.Role != BuiltObjectRole.Base && builtObject6.Owner == null && empire2 == empire)
                                {
                                    flag7 = false;
                                }
                            }
                            else if (main_0.double_0 > 35.0 && builtObject6.Role != BuiltObjectRole.Base && empire2 == empire)
                            {
                                flag7 = false;
                            }
                            if (flag7)
                            {
                                int num93 = 8;
                                if (builtObject6.Role == BuiltObjectRole.Base)
                                {
                                    num93 = 10;
                                }
                                int num94 = Math.Max(num93, num81);
                                int num95 = Math.Max(num93, num82);
                                Math.Max(0, num93 - num81);
                                Math.Max(0, num93 - num82);
                                int num96 = num89 - (num94 - val3) / 2;
                                int num97 = num90 - (num95 - val4) / 2;
                                int num98 = Math.Max(num93, num81);
                                int num99 = Math.Max(num93, num82);
                                System.Drawing.Color color7 = ResolveShipSymbolColor(builtObject6);
                                if (builtObject6.Role != BuiltObjectRole.Base && builtObject6.Empire != galaxy_0.IndependentEmpire)
                                {
                                    color7 = System.Drawing.Color.FromArgb(Math.Min(255, color7.A + 48), color7.R, color7.G, color7.B);
                                }
                                bool galaxyLevel = false;
                                if (main_0.double_0 > 10.0)
                                {
                                    galaxyLevel = true;
                                }
                                DrawShipSymbolXna(spriteBatch_0, builtObject6, color7, num96, num97, num94, num95, num98, num99, galaxyLevel, currentDateTime);
                            }
                        }
                        System.Drawing.Rectangle destination4 = new System.Drawing.Rectangle(num89, num90, num81, num82);
                        double num100 = (double)texture2D2.Width / (double)list_9[num78].Width;
                        int num101 = (int)((double)(float)list_9[num78].Width * (num100 - 1.0) / 2.0);
                        int num102 = (int)((double)(float)list_9[num78].Height * (num100 - 1.0) / 2.0);
                        destination4.Inflate(num101, num102);
                        if (texture2D3 != null && builtObject6.TargetSpeed > 0)
                        {
                            float rotationAngle = builtObject6.Heading;
                            float num103 = (float)texture2D3.Width / (float)list_9[num78].Width;
                            int num104 = (int)((float)num81 * num103);
                            int num105 = (int)((float)num82 * num103);
                            int num106 = num89 - (num104 - num81) / 2;
                            int num107 = num90 - (num105 - num82) / 2;
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num106, num107, num104, num105), spriteBatch: spriteBatch_0, texture: texture2D3, rotationAngle: rotationAngle);
                        }
                        DrawBuiltObjectToMainXna(spriteBatch_0, texture2D2, builtObject6, destination4, fadeCivilianShips);
                        if (builtObject6.IsBlockaded)
                        {
                            method_99(spriteBatch_0, destination4.X, destination4.Y, destination4.Width, destination4.Height);
                        }
                        if (main_0._Game.SelectedObject is ShipGroup)
                        {
                            ShipGroup shipGroup = (ShipGroup)main_0._Game.SelectedObject;
                            if (builtObject6.ShipGroup == shipGroup)
                            {
                                method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                            }
                        }
                        else if (main_0._Game.SelectedObject is BuiltObjectList)
                        {
                            BuiltObjectList builtObjectList2 = (BuiltObjectList)main_0._Game.SelectedObject;
                            if (builtObjectList2.Contains(builtObject6))
                            {
                                method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                            }
                        }
                        else if (builtObject6 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num91, num92, num91 + num87, num92 + num88);
                        }
                        if (builtObject6.LastIonStrike > DateTime.MinValue)
                        {
                            TimeSpan timeSpan = currentDateTime.Subtract(builtObject6.LastIonStrike);
                            if (timeSpan.TotalMilliseconds < 1400.0)
                            {
                                if (!builtObject6.IonStrikeSoundPlayed)
                                {
                                    builtObject6.IonStrikeSoundPlayed = true;
                                    double double_2 = 0.0;
                                    double double_3 = 0.0;
                                    method_90(num89, num90, main_0.double_0, out double_2, out double_3);
                                    main_0.method_0(main_0.EffectsPlayer.ResolveIonStrike(double_2, double_3));
                                }
                                int num108 = (int)((double)num81 * 1.4);
                                int num109 = currentDateTime.Second / 3;
                                Bitmap bitmap7 = lightningGenerator_0.GenerateLightning(builtObject6.BuiltObjectID + num109, num108);
                                Texture2D texture2D4 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap7);
                                int num110 = num89 - (num108 - num81) / 2;
                                int num111 = num90 - (num108 - num82) / 2;
                                System.Drawing.Rectangle destination6 = new System.Drawing.Rectangle(num110, num111, num108, num108);
                                new System.Drawing.Rectangle(0, 0, bitmap7.Width, bitmap7.Height);
                                double num112 = 1.0 - timeSpan.TotalMilliseconds % 250.0 / 250.0 * 0.95;
                                System.Drawing.Color white = System.Drawing.Color.White;
                                if (num112 < 1.0)
                                {
                                    white = System.Drawing.Color.FromArgb((int)(num112 * 255.0), 255, 255, 255);
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D4, destination6, 0f, white);
                                }
                                else
                                {
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D4, destination6, 0f, white);
                                }
                                method_21(bitmap7);
                                method_22(texture2D4);
                            }
                            else
                            {
                                builtObject6.LastIonStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject6.ShieldAreaRechargeStartTime > DateTime.MinValue && builtObject6.ShieldAreaRechargeTarget != null)
                        {
                            int int_8 = num83 + num81 / 2;
                            int int_9 = num84 + num82 / 2;
                            int int_10 = (int)((double)((int)builtObject6.ShieldAreaRechargeTarget.Xpos - num) / main_0.double_0) + base.Width / 2;
                            int int_11 = (int)((double)((int)builtObject6.ShieldAreaRechargeTarget.Ypos - num2) / main_0.double_0) + base.Height / 2;
                            method_94(spriteBatch_0, currentDateTime, builtObject6, int_8, int_9, num81 / 2, int_10, int_11);
                            if (builtObject6.ShieldAreaRechargeStartTime.Subtract(currentDateTime).TotalSeconds > 3.0)
                            {
                                builtObject6.ShieldAreaRechargeStartTime = DateTime.MinValue;
                                builtObject6.ShieldAreaRechargeTarget = null;
                            }
                        }
                        if (builtObject6.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject6.LastShieldStrike).TotalMilliseconds < 200.0 && (double)builtObject6.LastShieldStrikeDirection > double.MinValue)
                            {
                                double num113 = 1.0;
                                int num114 = (int)(num113 * (double)num81);
                                int num115 = (int)(num113 * (double)num82);
                                int num116 = num89 - (num114 - num81) / 2;
                                int num117 = num90 - (num115 - num82) / 2;
                                System.Drawing.Rectangle destination7 = new System.Drawing.Rectangle(num116, num117, num114, num115);
                                float rotationAngle2 = (float)((double)builtObject6.LastShieldStrikeDirection - Math.PI / 2.0);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_6, destination7, rotationAngle2);
                            }
                            else
                            {
                                builtObject6.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (builtObject6.LastTractorStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(builtObject6.LastTractorStrike).TotalMilliseconds < 2000.0 && (double)builtObject6.LastTractorStrikeDirection > double.MinValue)
                            {
                                double num118 = 1.0;
                                int num119 = (int)(num118 * (double)num81);
                                int num120 = (int)(num118 * (double)num82);
                                int int_12 = num89 - (num119 - num81) / 2;
                                int int_13 = num90 - (num120 - num82) / 2;
                                new System.Drawing.Rectangle(int_12, int_13, num119, num120);
                                float num121 = (float)((double)builtObject6.LastTractorStrikeDirection - Math.PI / 2.0);
                                method_117(spriteBatch_0, int_12, int_13, num119, num120, texture2D_34, currentDateTime, 10, num121, 0.0, System.Drawing.Color.White);
                            }
                            else
                            {
                                builtObject6.LastTractorStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0)
                        {
                            if (main_0.int_34 < 1 && builtObject6.InBattle)
                            {
                                if (builtObject6.ShieldsCapacity > 0)
                                {
                                    int int_14 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                    int num122 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                    method_194(spriteBatch_0, int_14, num122 - 8, bitmap5.Width, builtObject6.ShieldsCapacity, (int)builtObject6.CurrentShields);
                                }
                                if (builtObject6.AssaultDefenseValue > 0 || builtObject6.AssaultAttackValue > 0)
                                {
                                    if (builtObject6.AssaultAttackValue > 0)
                                    {
                                        int int_15 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                        int num123 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                        int assaultDefenseValueFixed = builtObject6.AssaultDefenseValueFixed;
                                        int int_16 = Math.Max(0, builtObject6.AssaultDefenseValue - assaultDefenseValueFixed);
                                        method_195(spriteBatch_0, int_15, num123 - 4, bitmap5.Width, builtObject6.AssaultDefenseValueDefault, assaultDefenseValueFixed, int_16, builtObject6.AssaultAttackValue);
                                    }
                                    else if (builtObject6.AssaultDefenseValueDefault > 0)
                                    {
                                        Math.Min(1f, Math.Max(0f, (float)builtObject6.AssaultDefenseValue / (float)builtObject6.AssaultDefenseValueDefault));
                                        int int_17 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                        int num124 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                        int assaultDefenseValueFixed2 = builtObject6.AssaultDefenseValueFixed;
                                        int int_18 = Math.Max(0, builtObject6.AssaultDefenseValue - assaultDefenseValueFixed2);
                                        method_195(spriteBatch_0, int_17, num124 - 4, bitmap5.Width, builtObject6.AssaultDefenseValueDefault, assaultDefenseValueFixed2, int_18, builtObject6.AssaultAttackValue);
                                    }
                                }
                                if (builtObject6.AssaultAttackValue > 0 && texture2D_25 != null)
                                {
                                    int num125 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - texture2D_25.Width / 2 + base.Width / 2;
                                    int num126 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - texture2D_25.Height / 2 + base.Height / 2;
                                    System.Drawing.Rectangle destination8 = new System.Drawing.Rectangle(num125, num126, texture2D_25.Width, texture2D_25.Height);
                                    System.Drawing.Color tintColor = method_214(System.Drawing.Color.FromArgb(255, 0, 0), System.Drawing.Color.FromArgb(255, 255, 0), currentDateTime);
                                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_25, destination8, 0f, tintColor);
                                }
                            }
                            if (main_0.int_34 < 2 && builtObject6.ShipGroup != null && builtObject6.ShipGroup.LeadShip == builtObject6)
                            {
                                int num127 = (int)((builtObject6.Xpos - (double)num) / main_0.double_0) - bitmap5.Width / 2 + base.Width / 2;
                                int num128 = (int)((builtObject6.Ypos - (double)num2) / main_0.double_0) - bitmap5.Height / 2 + base.Height / 2;
                                method_191(spriteBatch_0, num127 + (int)(double)bitmap5.Width + 2 - main_0.bitmap_43.Width, num128 - 2);
                            }
                        }
                        if ((builtObject_0 != null && builtObject6 == builtObject_0) || (shipGroup_0 != null && builtObject6.ShipGroup == shipGroup_0))
                        {
                            method_203(rectangle_5: new System.Drawing.Rectangle(num91, num92, num87, num88), spriteBatch_2: spriteBatch_0);
                        }
                    }
                    method_97(builtObject6, num79, num80, currentStarDate, bool_13: true);
                    method_95(builtObject6, num79, num80, bool_13: true);
                    if (builtObject6.Explosions.Count > 0)
                    {
                        method_183(spriteBatch_0, builtObject6, num70);
                    }
                    if (builtObject6.Weapons.Count > 0)
                    {
                        method_167(spriteBatch_0, builtObject6, currentDateTime, num89, num90);
                    }
                }
                animationSystem_1.DoAnimationsXna(spriteBatch_0, currentDateTime);
                for (int num129 = 0; num129 < fightersForBuiltObjects.Count; num129++)
                {
                    Fighter fighter = fightersForBuiltObjects[num129];
                    if (fighter == null)
                    {
                        continue;
                    }
                    if (fighter.PictureRef < 0 || fighter.PictureRef > main_0.bitmap_6.Length)
                    {
                        fighter.PictureRef = 0;
                        if (fighter.Empire != null && fighter.Empire.DominantRace != null)
                        {
                            fighter.PictureRef = (short)ShipImageHelper.ResolveNewFighterImageIndex(fighter.Empire.DominantRace, isPirates: false);
                        }
                    }
                    int num130 = main_0.bitmap_6[fighter.PictureRef].Width;
                    int num131 = main_0.bitmap_6[fighter.PictureRef].Height;
                    num130 = (int)((double)num130 / num70);
                    num131 = (int)((double)num131 / num70);
                    num130 = Math.Min(num130, (int)maxWidth);
                    num131 = Math.Min(num131, (int)maxWidth);
                    int num132 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num130 / 2 + base.Width / 2;
                    int num133 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num131 / 2 + base.Height / 2;
                    if (num132 + num130 < -50 || num132 - num130 > base.Width + 50 || num133 + num131 < -50 || num133 - num131 > base.Height + 50 || (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(fighter)) || fighter.OnboardCarrier)
                    {
                        continue;
                    }
                    fighterList.Add(fighter);
                    bool flag8 = false;
                    if (fighter.TargetSpeedChanged)
                    {
                        flag8 = true;
                        fighter.TargetSpeedChanged = false;
                    }
                    Bitmap bitmap8 = null;
                    Bitmap bitmap9 = null;
                    Texture2D texture2D5 = null;
                    Texture2D texture2D6 = null;
                    int num134 = list_21.IndexOf(fighter.FighterID);
                    if (num134 >= 0)
                    {
                        if (method_68(fighter))
                        {
                            if (list_17[num134] != null)
                            {
                                method_21(list_17[num134]);
                            }
                            list_17[num134] = method_70(fighter, list_16[num134], bool_13: false);
                            if (list_18[num134] != null)
                            {
                                method_22(list_18[num134]);
                            }
                            list_18[num134] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_17[num134]);
                        }
                        bitmap8 = list_17[num134];
                        texture2D5 = list_18[num134];
                        _ = list_16[num134];
                        bitmap9 = list_19[num134];
                        texture2D6 = list_20[num134];
                        if (flag8)
                        {
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, list_16[num134]);
                            if (list_19[num134] != null)
                            {
                                method_21(list_19[num134]);
                            }
                            list_19[num134] = bitmap9;
                            if (list_20[num134] != null)
                            {
                                method_22(list_20[num134]);
                            }
                            texture2D6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap9);
                            list_20[num134] = texture2D6;
                            flag8 = false;
                        }
                    }
                    else
                    {
                        double size2 = (double)main_0.int_5[fighter.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        if (fighter.Empire != null)
                        {
                            bitmap8 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], fighter.Empire.MainColor, fighter.Empire.SecondaryColor, size2, fighter.Size, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, bitmap8);
                        }
                        else
                        {
                            bitmap8 = main_0.PrepareFighterImage(fighter, main_0.bitmap_6[fighter.PictureRef], System.Drawing.Color.Gray, System.Drawing.Color.Gray, size2, fighter.Size, main_0.double_0);
                            bitmap9 = main_0.PrepareEngineExhaust(fighter, bitmap8);
                        }
                        list_21.Add(fighter.FighterID);
                        list_16.Add(bitmap8);
                        bitmap8 = method_70(fighter, bitmap8, bool_13: false);
                        list_17.Add(bitmap8);
                        list_19.Add(bitmap9);
                        texture2D5 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap8);
                        texture2D6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap9);
                        list_18.Add(texture2D5);
                        list_20.Add(texture2D6);
                    }
                    num130 = bitmap8.Width;
                    num131 = bitmap8.Height;
                    num130 = Math.Min(num130, (int)maxWidth);
                    num131 = Math.Min(num131, (int)maxWidth);
                    num132 = (int)((double)((int)fighter.Xpos - num) / main_0.double_0) - num130 / 2 + base.Width / 2;
                    num133 = (int)((double)((int)fighter.Ypos - num2) / main_0.double_0) - num131 / 2 + base.Height / 2;
                    if (num132 + num130 >= -50 && num132 - num130 <= base.Width + 50 && num133 + num131 >= -50 && num133 - num131 <= base.Height + 50)
                    {
                    }
                }
                for (int num135 = 0; num135 < fighterList.Count; num135++)
                {
                    Fighter fighter2 = fighterList[num135];
                    if (fighter2 == null)
                    {
                        continue;
                    }
                    Bitmap bitmap10 = null;
                    Bitmap bitmap11 = null;
                    Bitmap bitmap12 = null;
                    Texture2D texture2D7 = null;
                    Texture2D texture2D8 = null;
                    int num136 = list_21.IndexOf(fighter2.FighterID);
                    if (num136 >= 0)
                    {
                        if (method_68(fighter2))
                        {
                            if (list_17[num136] != null)
                            {
                                method_21(list_17[num136]);
                            }
                            list_17[num136] = method_70(fighter2, list_16[num136], bool_13: false);
                            if (list_18[num136] != null)
                            {
                                method_22(list_18[num136]);
                            }
                            list_18[num136] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, list_17[num136]);
                        }
                        bitmap10 = list_17[num136];
                        texture2D7 = list_18[num136];
                        bitmap11 = list_16[num136];
                        bitmap12 = list_19[num136];
                        texture2D8 = list_20[num136];
                    }
                    else
                    {
                        double size3 = (double)main_0.int_5[fighter2.PictureRef] / Galaxy.BuiltObjectDrawResizeFactor;
                        bitmap10 = main_0.PrepareFighterImage(fighter2, main_0.bitmap_6[fighter2.PictureRef], fighter2.Empire.MainColor, fighter2.Empire.SecondaryColor, size3, fighter2.Size, main_0.double_0);
                        bitmap12 = main_0.PrepareEngineExhaust(fighter2, bitmap10);
                        list_21.Add(fighter2.FighterID);
                        list_16.Add(bitmap10);
                        bitmap11 = bitmap10;
                        bitmap10 = method_70(fighter2, bitmap10, bool_13: false);
                        list_17.Add(bitmap10);
                        list_19.Add(bitmap12);
                        texture2D7 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap10);
                        texture2D8 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap12);
                        list_18.Add(texture2D7);
                        list_20.Add(texture2D8);
                    }
                    int val5 = bitmap10.Width;
                    int val6 = bitmap10.Height;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num137 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num138 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    if (num137 + val5 < -50 || num137 - val5 > base.Width + 50 || num138 + val6 < -50 || num138 - val6 > base.Height + 50)
                    {
                        continue;
                    }
                    int num139 = bitmap11.Width;
                    int num140 = bitmap11.Height;
                    Size size4 = new Size(num139, num140);
                    Bitmap bitmap13 = main_0.bitmap_6[fighter2.PictureRef];
                    if (bitmap13 != null && bitmap13.PixelFormat != 0)
                    {
                        size4 = kgxRubsAau3(fighter2, main_0.int_5[fighter2.PictureRef], bitmap13, main_0.double_0);
                    }
                    num139 = size4.Width;
                    num140 = size4.Height;
                    Texture2D toDraw = texture2D7;
                    int num141 = (int)((double)num139 * 1.3);
                    int num142 = (int)((double)num140 * 1.3);
                    val5 = num139;
                    val6 = num140;
                    val5 = Math.Min(val5, (int)maxWidth);
                    val6 = Math.Min(val6, (int)maxWidth);
                    int num143 = (int)((double)((int)fighter2.Xpos - num) / main_0.double_0) - val5 / 2 + base.Width / 2;
                    int num144 = (int)((double)((int)fighter2.Ypos - num2) / main_0.double_0) - val6 / 2 + base.Height / 2;
                    int num145 = num143 - (num141 - val5) / 2;
                    int num146 = num144 - (num142 - val6) / 2;
                    if (!fighter2.HasBeenDestroyed)
                    {
                        if (main_0.int_34 >= 2)
                        {
                        }
                        if (texture2D8 != null && fighter2.TargetSpeed > 0f)
                        {
                            float rotationAngle3 = fighter2.Heading;
                            float num147 = (float)texture2D8.Width / (float)texture2D7.Width;
                            int num148 = (int)((float)num139 * num147);
                            int num149 = (int)((float)num140 * num147);
                            int num150 = num143 - (num148 - num139) / 2;
                            int num151 = num144 - (num149 - num140) / 2;
                            XnaDrawingHelper.DrawTexture(destination: new System.Drawing.Rectangle(num150, num151, num148, num149), spriteBatch: spriteBatch_0, texture: texture2D8, rotationAngle: rotationAngle3);
                        }
                        DrawFighterToMainXna(destination: new System.Drawing.Rectangle(num143, num144, num139, num140), spriteBatch: spriteBatch_0, ToDraw: toDraw, fighter: fighter2);
                        if (fighter2 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num145, num146, num145 + num141, num146 + num142);
                        }
                        if (fighter2.LastShieldStrike > DateTime.MinValue)
                        {
                            if (currentDateTime.Subtract(fighter2.LastShieldStrike).TotalMilliseconds < 200.0 && (double)fighter2.LastShieldStrikeDirection > double.MinValue)
                            {
                                double num152 = 1.0;
                                int num153 = (int)(num152 * (double)num139);
                                int num154 = (int)(num152 * (double)num140);
                                int num155 = num143 - (num153 - num139) / 2;
                                int num156 = num144 - (num154 - num140) / 2;
                                System.Drawing.Rectangle destination11 = new System.Drawing.Rectangle(num155, num156, num153, num154);
                                float rotationAngle4 = (float)((double)fighter2.LastShieldStrikeDirection - Math.PI / 2.0);
                                XnaDrawingHelper.DrawTexture(spriteBatch_0, texture2D_6, destination11, rotationAngle4);
                            }
                            else
                            {
                                fighter2.LastShieldStrike = DateTime.MinValue;
                            }
                        }
                        if (main_0.double_0 <= 3.0 && main_0.int_34 < 1 && fighter2.InBattle && fighter2.Specification.ShieldsCapacity > 0)
                        {
                            int int_19 = (int)((fighter2.Xpos - (double)num) / main_0.double_0) - bitmap11.Width / 2 + base.Width / 2;
                            int num157 = (int)((fighter2.Ypos - (double)num2) / main_0.double_0) - bitmap11.Height / 2 + base.Height / 2;
                            method_194(spriteBatch_0, int_19, num157 - 8, bitmap11.Width, fighter2.Specification.ShieldsCapacity, (int)fighter2.CurrentShields);
                        }
                    }
                    if (fighter2.Explosions.Count > 0)
                    {
                        method_184(spriteBatch_0, fighter2, num70);
                    }
                    if (fighter2.Weapons.Count > 0)
                    {
                        method_165(spriteBatch_0, fighter2, currentDateTime, num143, num144);
                    }
                }
            }
            if (main_0.double_0 < 500.0)
            {
                double maxWidth2 = 0.0;
                double num158 = main_0.CalculateCreatureZoomFactor(main_0.double_0, out maxWidth2);
                CreatureList creatureList = new CreatureList();
                new Keyboard();
                _ = MouseHelper.GetCursorPosition().X;
                _ = MouseHelper.GetCursorPosition().Y;
                if (num5 <= (double)(Galaxy.MaxSolarSystemSize + 5000))
                {
                    creatureList.AddRange(ListHelper.ToArrayThreadSafe(galaxy_0.Systems[habitat.SystemIndex].Creatures));
                }
                else
                {
                    GalaxyLocationList galaxyLocationList4 = galaxy_0.DetermineGalaxyLocationsAtPoint(num, num2, GalaxyLocationType.RestrictedArea);
                    for (int num159 = 0; num159 < galaxyLocationList4.Count; num159++)
                    {
                        GalaxyLocation galaxyLocation2 = galaxyLocationList4[num159];
                        creatureList.AddRange(ListHelper.ToArrayThreadSafe(galaxyLocation2.RelatedCreatures));
                        for (int num160 = 0; num160 < galaxyLocation2.RelatedCreatures.Count; num160++)
                        {
                            Creature creature = galaxyLocation2.RelatedCreatures[num160];
                            creature.DoTasks(currentDateTime);
                        }
                    }
                }
                for (int num161 = 0; num161 < creatureList.Count; num161++)
                {
                    Creature creature2 = creatureList[num161];
                    if (creature2 == null)
                    {
                        continue;
                    }
                    int num162 = main_0.bitmap_10[creature2.PictureRef][0].Width;
                    int num163 = main_0.bitmap_10[creature2.PictureRef][0].Height;
                    num162 = (int)((double)num162 / num158);
                    num163 = (int)((double)num163 / num158);
                    num162 = Math.Min(num162, (int)maxWidth2);
                    num163 = Math.Min(num163, (int)maxWidth2);
                    int num164 = (int)((creature2.Xpos - ((double)num + (double)num162 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Width / 2;
                    int num165 = (int)((creature2.Ypos - ((double)num2 + (double)num163 * main_0.double_0 / 2.0)) / main_0.double_0) + base.Height / 2;
                    if (num164 + num162 < -50 || num164 - num162 > base.Width + 50 || num165 + num163 < -50 || num165 - num163 > base.Height + 50)
                    {
                        continue;
                    }
                    if (creature2.CurrentSpeed <= (float)creature2.MovementSpeed && creature2.NearestSystemStar != habitat)
                    {
                        creature2.PromptSystemCheck = true;
                    }
                    if (!main_0._Game.GodMode && !empire.IsObjectVisibleToThisEmpire(creature2))
                    {
                        continue;
                    }
                    int num166 = list_24.IndexOf(creature2.CreatureID);
                    Bitmap[] array;
                    Bitmap[] array2;
                    if (num166 >= 0)
                    {
                        array = list_22[num166];
                        array2 = list_23[num166];
                    }
                    else
                    {
                        int pictureRef = creature2.PictureRef;
                        int num167 = pictureRef + 5;
                        double size5 = (double)main_0.sveqhmNacy[pictureRef] / Galaxy.CreatureDrawResizeFactor;
                        array = new Bitmap[main_0.bitmap_10[pictureRef].Length];
                        for (int num168 = 0; num168 < main_0.bitmap_10[pictureRef].Length; num168++)
                        {
                            array[num168] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[pictureRef][num168], size5, creature2.Size, allowPreRotate: false);
                        }
                        size5 = (double)main_0.sveqhmNacy[num167] / Galaxy.CreatureDrawResizeFactor;
                        array2 = new Bitmap[main_0.bitmap_10[num167].Length];
                        for (int num169 = 0; num169 < main_0.bitmap_10[num167].Length; num169++)
                        {
                            array2[num169] = main_0.PrepareCreatureImage(creature2, main_0.bitmap_10[num167][num169], size5, creature2.Size, allowPreRotate: false);
                        }
                        list_24.Add(creature2.CreatureID);
                        list_22.Add(array);
                        list_23.Add(array2);
                    }
                    num162 = array[0].Width;
                    num163 = array[0].Height;
                    num162 = (int)((double)num162 / num158);
                    num163 = (int)((double)num163 / num158);
                    num162 = Math.Min(num162, (int)maxWidth2);
                    num163 = Math.Min(num163, (int)maxWidth2);
                    num164 = (int)((creature2.Xpos - (double)(num + (int)((double)num162 * num158 / 2.0))) / main_0.double_0) + base.Width / 2;
                    num165 = (int)((creature2.Ypos - (double)(num2 + (int)((double)num163 * num158 / 2.0))) / main_0.double_0) + base.Height / 2;
                    if (num164 + num162 < -50 || num164 - num162 > base.Width + 50 || num165 + num163 < -50 || num165 - num163 > base.Height + 50)
                    {
                        continue;
                    }
                    Bitmap bitmap14 = null;
                    Texture2D texture2D9 = null;
                    Texture2D[] texture2D_3 = null;
                    Texture2D[] texture2D_4 = null;
                    bool flag9 = false;
                    if (creature2.Damage > 0.0)
                    {
                        bitmap14 = main_0.method_108(creature2, array[0], main_0.bitmap_11[creature2.PictureRef][0]);
                        texture2D9 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap14);
                        method_21(bitmap14);
                        flag9 = true;
                    }
                    else
                    {
                        bitmap14 = array[0];
                        switch (creature2.Type)
                        {
                            case CreatureType.Kaltor:
                                texture2D9 = main_0.texture2D_0[2][0];
                                texture2D_3 = main_0.texture2D_0[2];
                                texture2D_4 = main_0.texture2D_0[7];
                                break;
                            case CreatureType.RockSpaceSlug:
                                texture2D9 = main_0.texture2D_0[0][0];
                                texture2D_3 = main_0.texture2D_0[0];
                                texture2D_4 = main_0.texture2D_0[5];
                                break;
                            case CreatureType.DesertSpaceSlug:
                                texture2D9 = main_0.texture2D_0[1][0];
                                texture2D_3 = main_0.texture2D_0[1];
                                texture2D_4 = main_0.texture2D_0[6];
                                break;
                            case CreatureType.Ardilus:
                                texture2D9 = main_0.texture2D_0[3][0];
                                texture2D_3 = main_0.texture2D_0[3];
                                texture2D_4 = main_0.texture2D_0[8];
                                break;
                            case CreatureType.SilverMist:
                                texture2D9 = main_0.texture2D_0[4][0];
                                texture2D_3 = main_0.texture2D_0[4];
                                texture2D_4 = main_0.texture2D_0[9];
                                break;
                        }
                    }
                    int num170 = bitmap14.Width;
                    int num171 = bitmap14.Height;
                    int num172 = (bitmap14.Width - num170) / 2;
                    int num173 = (bitmap14.Height - num171) / 2;
                    num172 = (int)((double)num172 / num158);
                    num173 = (int)((double)num173 / num158);
                    int num174 = (int)((double)num162 * 1.5);
                    int num175 = (int)((double)num163 * 1.5);
                    int num176 = num164 - (num174 - num162) / 2;
                    int num177 = num165 - (num175 - num163) / 2;
                    if (!creature2.HasBeenDestroyed)
                    {
                        if (creature2.CurrentSpeed > 0f)
                        {
                            if (creature2.Damage > 0.0)
                            {
                                method_114(creature2, spriteBatch_0, num164 - num172, num165 - num173, num162, num163, array, array2, currentDateTime, 10, creature2.CurrentHeading);
                            }
                            else
                            {
                                method_113(creature2, spriteBatch_0, num164 - num172, num165 - num173, num162, num163, texture2D_3, texture2D_4, currentDateTime, 10, creature2.CurrentHeading);
                            }
                        }
                        else
                        {
                            DrawCreatureToMainXna(spriteBatch_0, creature2, texture2D9, num164 - num172, num165 - num173, num162, num163, creature2.CurrentHeading);
                        }
                        _ = ((int)((double)texture2D9.Width / num158) - num162) / 2;
                        _ = ((int)((double)texture2D9.Height / num158) - num163) / 2;
                        if (creature2 == main_0._Game.SelectedObject)
                        {
                            method_212(spriteBatch_0, num176, num177, num176 + num174, num177 + num175);
                        }
                    }
                    list_4.Add(new System.Drawing.Rectangle(num164 - 7, num165 - 7, (int)((double)texture2D9.Width / main_0.double_0) + 14, (int)((double)texture2D9.Height / main_0.double_0) + 14));
                    if (flag9)
                    {
                        method_22(texture2D9);
                    }
                }
            }
            if (Control.MouseButtons == MouseButtons.Left && main_0.method_207(MouseHelper.GetCursorPosition()) == null && !main_0.itemListCollectionPanel_0.Area.Contains(MouseHelper.GetCursorPosition()) && (main_0.int_15 != main_0.int_32 || main_0.int_16 != main_0.int_33))
            {
                int num178 = Math.Min(main_0.int_15, main_0.int_32);
                int num179 = Math.Min(main_0.int_16, main_0.int_33);
                int num180 = Math.Abs(main_0.int_15 - main_0.int_32);
                int num181 = Math.Abs(main_0.int_16 - main_0.int_33);
                num178 = base.Width / 2 + (int)((double)(num178 - num) / main_0.double_0);
                num179 = base.Height / 2 + (int)((double)(num179 - num2) / main_0.double_0);
                num180 = (int)((double)num180 / main_0.double_0);
                num181 = (int)((double)num181 / main_0.double_0);
                XnaDrawingHelper.DrawRectangle(spriteBatch_0, new System.Drawing.Rectangle(num178, num179, num180, num181), main_0.pen_1.Color, (int)main_0.pen_1.Width);
            }
            if (main_0.double_0 < 500.0)
            {
                double num182 = main_0.CalculatePlanetZoomFactor(main_0.double_0);
                double num183 = main_0.CalculateMoonZoomFactor(main_0.double_0);
                for (int num184 = main_0.int_28; num184 <= main_0.int_29; num184++)
                {
                    if ((systemVisibilityStatus == SystemVisibilityStatus.Unexplored && num184 > main_0.int_28 && !flag4) || num184 >= galaxy_0.Habitats.Count)
                    {
                        continue;
                    }
                    Habitat habitat5 = galaxy_0.Habitats[num184];
                    if (habitat5 == null)
                    {
                        continue;
                    }
                    bool flag10 = false;
                    if (flag4)
                    {
                        if (!empire.IsObjectVisibleToThisEmpire(habitat5))
                        {
                            continue;
                        }
                        flag10 = true;
                    }
                    double double_4 = main_0.double_0;
                    switch (habitat5.Category)
                    {
                        case HabitatCategoryType.Planet:
                            double_4 = num182;
                            break;
                        case HabitatCategoryType.Moon:
                            double_4 = num183;
                            break;
                    }
                    Bitmap bitmap15 = method_57(num184 - main_0.int_28);
                    if (habitat5.Category == HabitatCategoryType.GasCloud)
                    {
                        bitmap15 = bitmap_2;
                        method_59(habitat5, double_4, 0, out int_, out int_2);
                    }
                    if (bitmap15 == null)
                    {
                        bitmap15 = main_0.habitatImageCache_0.ObtainImageSmall(habitat5);
                    }
                    int_ = habitat5.Diameter;
                    int_2 = habitat5.Diameter;
                    if (bitmap15 != null && bitmap15.PixelFormat != 0)
                    {
                        double num185 = (double)bitmap15.Width / (double)bitmap15.Height;
                        if (bitmap15.Width > bitmap15.Height)
                        {
                            int_ = (int)((double)habitat5.Diameter * num185);
                            int_2 = habitat5.Diameter;
                        }
                        else
                        {
                            int_ = habitat5.Diameter;
                            int_2 = (int)((double)habitat5.Diameter / num185);
                        }
                    }
                    int int_20 = 4;
                    if (habitat5.Category == HabitatCategoryType.Asteroid)
                    {
                        int_20 = 1;
                    }
                    System.Drawing.Rectangle rectangle9 = method_35((int)habitat5.Xpos, (int)habitat5.Ypos, int_, int_2, double_4, int_20);
                    int num36 = rectangle9.X;
                    int num37 = rectangle9.Y;
                    int_ = rectangle9.Width;
                    int_2 = rectangle9.Height;
                    if (num36 + int_ < -20 || num36 > base.Width + 20 || num37 + int_2 < -20 || num37 > base.Height + 20)
                    {
                        continue;
                    }
                    bool flag11 = false;
                    bool flag12 = false;
                    bool flag13 = false;
                    if ((main_0.int_34 < 2 && systemVisibilityStatus != SystemVisibilityStatus.Unexplored) || flag10)
                    {
                        if (!habitat5.HasBeenDestroyed)
                        {
                            if (main_0.double_0 < 2.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag12 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Category == HabitatCategoryType.Planet)
                                {
                                    flag11 = true;
                                }
                            }
                            else if (main_0.double_0 < 10.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Category == HabitatCategoryType.Planet)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                            else if (main_0.double_0 < 40.0)
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.BasesAtHabitat != null && habitat5.BasesAtHabitat.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                            else
                            {
                                if (habitat5.Owner != null && habitat5.Population != null && habitat5.Population.Count > 0)
                                {
                                    flag11 = true;
                                }
                                if (habitat5.Ruin != null)
                                {
                                    flag13 = true;
                                }
                            }
                        }
                        if (main_0.int_34 == 2)
                        {
                            flag12 = false;
                            flag11 = false;
                            flag13 = false;
                        }
                    }
                    if (flag12)
                    {
                        System.Drawing.Rectangle rectangle10 = method_81(num36, num37, int_, int_2);
                        Texture2D texture2D10 = null;
                        int num186 = list_0.IndexOf(habitat5.HabitatIndex);
                        if (num186 >= 0 && list_2.Count > num186)
                        {
                            texture2D10 = list_2[num186];
                        }
                        if (texture2D10 == null)
                        {
                            Bitmap bitmap16 = method_89(habitat5);
                            texture2D10 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap16);
                            method_21(bitmap16);
                            list_2.Add(texture2D10);
                            list_0.Add(habitat5.HabitatIndex);
                        }
                        method_86(spriteBatch_0, habitat5, rectangle10.X, rectangle10.Y, rectangle10.Width, rectangle10.Height, texture2D10);
                    }
                    else if (flag11 || flag13)
                    {
                        method_84(spriteBatch_0, habitat5, num36, num37, int_, int_2, main_0.double_0, flag11, flag13);
                    }
                }
            }
            method_91(num, num2, main_0.double_0);
            if (main_0.double_0 < 100.0)
            {
                if (flag2)
                {
                }
                if (systemVisibilityStatus == SystemVisibilityStatus.Explored && flag3)
                {
                    XnaDrawingHelper.DrawPolygon(spriteBatch_0, point_0, System.Drawing.Color.FromArgb(20, 20, 20), 1);
                }
            }
            graphicsPath_0.Reset();
            graphicsPath_1.Reset();
            graphicsPath_2.Reset();
            graphicsPath_0.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_1.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            graphicsPath_2.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
            dateTime_2 = currentDateTime;
            return;
        IL_05ae:
            if (flag2)
            {
                System.Drawing.Rectangle rectangle = method_34((int)habitat.Xpos, (int)habitat.Ypos, Galaxy.MaxSolarSystemSize * 2 + 1000, Galaxy.MaxSolarSystemSize * 2 + 1000, main_0.double_0);
                graphicsPath_2.AddEllipse(rectangle);
            }
            goto IL_05fc;
        }

        private Size method_77(BuiltObject builtObject_1, BuiltObjectImageData builtObjectImageData_0, double double_15)
        {
            return main_0.DetermineBuiltObjectSizeNEW(builtObject_1, builtObjectImageData_0, double_15);
        }

        private Size kgxRubsAau3(Fighter fighter_0, int int_11, Bitmap bitmap_7, double double_15)
        {
            double size = (double)int_11 / Galaxy.BuiltObjectDrawResizeFactor;
            int targetSize = (int)((double)fighter_0.Size / (double_15 * double_15));
            return main_0.DetermineBuiltObjectSize(bitmap_7, size, targetSize);
        }

        private Texture2D method_78(Habitat habitat_1, bool bool_13, out Bitmap bitmap_7, out bool bool_14)
        {
            bitmap_7 = null;
            bool_14 = false;
            try
            {
                if (habitat_1.Category == HabitatCategoryType.Asteroid && main_0.bool_6 && main_0.long_0 >= 4080218929L && (main_0.int_1 > 6 || (main_0.int_1 == 6 && main_0.int_2 >= 1)))
                {
                    int num = habitat_1.PictureRef - int_0;
                    if (num >= 0 && num < bitmap_0.Length)
                    {
                        bitmap_7 = bitmap_0[num];
                        return texture2D_0[num];
                    }
                }
                Bitmap bitmap = null;
                bitmap = ((!bool_13) ? main_0.habitatImageCache_0.ObtainImage(habitat_1) : main_0.habitatImageCache_0.FastGetImage(habitat_1.PictureRef, out bool_14));
                if (bitmap != null && bitmap.PixelFormat != 0)
                {
                    bitmap.SetResolution(72f, 72f);
                    Bitmap bitmap_8 = null;
                    if (habitat_1.HasRings)
                    {
                        bitmap_8 = GetRescaledGasGiantRigns(habitat_1, bitmap.Width);
                    }
                    if (habitat_1.Category != HabitatCategoryType.Planet && habitat_1.Category != HabitatCategoryType.Moon)
                    {
                        bitmap_7 = bitmap;
                    }
                    else
                    {
                        Bitmap bitmap2 = new Bitmap(bitmap);
                        bitmap2.SetResolution(bitmap.HorizontalResolution, bitmap.VerticalResolution);
                        bool bool_15 = false;
                        Bitmap bitmap3 = bitmap2;
                        bitmap2 = method_52(habitat_1, bitmap2, out bool_15);
                        if (bool_15)
                        {
                            bitmap3.Dispose();
                        }
                        bitmap2 = method_51(habitat_1, bitmap2);
                        bitmap2 = GetPlanetShadow(habitat_1, bitmap2);
                        bitmap2 = method_50(habitat_1, bitmap2);
                        bitmap_7 = CombinePlaneWithRings(habitat_1, bitmap2, bitmap_8);
                        bitmap2.Dispose();
                    }
                    return XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_7);
                }
            }
            catch (Exception)
            {
                //BaconMain.CatchMethod_78(this);
            }
            return null;
        }

        protected override void OnCreateControl()
        {
            if (!base.DesignMode)
            {
                class1_0 = Class1.smethod_0(base.Handle, base.ClientSize.Width, base.ClientSize.Height);
                serviceContainer_0.AddService((IGraphicsDeviceService)class1_0);
            }
            base.OnCreateControl();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (class1_0 != null)
            {
                _ = base.ClientSize;
                class1_0.method_1(base.ClientSize.Width, base.ClientSize.Height);
            }
            base.OnSizeChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (spriteBatch_0 != null && !spriteBatch_0.IsDisposed)
            {
                spriteBatch_0.Dispose();
            }
            if (spriteBatch_1 != null && !spriteBatch_1.IsDisposed)
            {
                spriteBatch_1.Dispose();
            }
            if (class1_0 != null)
            {
                class1_0.method_0(disposing);
                class1_0 = null;
            }
            base.Dispose(disposing);
        }

        public void ResetRendering()
        {
            bool_12 = false;
        }

        public void DisposePirateFlagTextures()
        {
            XnaDrawingHelper.DisposeTextureArray(texture2D_21);
        }

        private void method_79()
        {
            if (bool_12)
            {
                return;
            }
            presentationParameters_0 = new PresentationParameters();
            presentationParameters_0.BackBufferWidth = base.Width;
            presentationParameters_0.BackBufferHeight = base.Height;
            presentationParameters_0.BackBufferFormat = SurfaceFormat.Color;
            presentationParameters_0.DepthStencilFormat = DepthFormat.Depth24;
            presentationParameters_0.MultiSampleCount = 4;
            presentationParameters_0.PresentationInterval = PresentInterval.Default;
            presentationParameters_0.IsFullScreen = false;
            presentationParameters_0.DeviceWindowHandle = base.Handle;
            spriteBatch_0 = new SpriteBatch(GraphicsDevice);
            spriteBatch_1 = new SpriteBatch(GraphicsDevice);
            XnaDrawingHelper.Initialize(GraphicsDevice);
            for (int i = 0; i < main_0.bitmap_10.Length; i++)
            {
                for (int j = 0; j < main_0.bitmap_10[i].Length; j++)
                {
                    if (main_0.texture2D_0[i][j] == null)
                    {
                        main_0.texture2D_0[i][j] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_10[i][j]);
                    }
                }
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_12, ref texture2D_1);
            method_80(main_0.bitmap_12);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_13, ref texture2D_2);
            method_80(main_0.bitmap_13);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.edLqkLkgAx, ref texture2D_3);
            method_80(main_0.edLqkLkgAx);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_14, ref texture2D_4);
            method_80(main_0.bitmap_14);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_15, ref texture2D_5);
            method_80(main_0.bitmap_15);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_18, ref texture2D_7);
            method_80(main_0.bitmap_18);
            for (int k = 0; k < texture2D_8.Length; k++)
            {
                XnaDrawingHelper.DisposeTextureArray(texture2D_8[k]);
                texture2D_8[k] = new Texture2D[main_0.bitmap_19[k].Length];
                for (int l = 0; l < texture2D_8[k].Length; l++)
                {
                    texture2D_8[k][l] = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_19[k][l]);
                }
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_199, ref texture2D_12);
            method_80(main_0.bitmap_199);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_200, ref texture2D_13);
            method_80(main_0.bitmap_200);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_204, ref texture2D_14);
            method_80(main_0.bitmap_204);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_201, ref texture2D_15);
            method_80(main_0.bitmap_201);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_202, ref texture2D_16);
            method_80(main_0.bitmap_202);
            texture2D_18 = new Texture2D[main_0.bitmap_196.Length];
            for (int m = 0; m < texture2D_18.Length; m++)
            {
                texture2D_18[m] = XnaDrawingHelper.BitmapToTexture(GraphicsDevice, main_0.bitmap_196[m]);
            }
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_197, ref texture2D_19);
            method_80(main_0.bitmap_197);
            texture2D_20 = new Texture2D[main_0.bitmap_206.Length];
            for (int n = 0; n < texture2D_20.Length; n++)
            {
                texture2D_20[n] = XnaDrawingHelper.BitmapToTexture(GraphicsDevice, main_0.bitmap_206[n]);
            }
            XnaDrawingHelper.DisposeTextureArray(texture2D_21);
            texture2D_21 = new Texture2D[255];
            texture2D_6 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_17);
            texture2D_17 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_188);
            texture2D_22 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_49);
            texture2D_23 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_52);
            texture2D_24 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_4);
            texture2D_25 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_88);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_215, ref texture2D_34);
            method_80(main_0.bitmap_215);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_211, ref texture2D_32);
            method_80(main_0.bitmap_211);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_210, ref texture2D_31);
            method_80(main_0.bitmap_210);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_212, ref texture2D_33);
            method_80(main_0.bitmap_212);
            bitmap_0 = main_0.habitatImageCache_0.GetAsteroidImages();
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, bitmap_0, ref texture2D_0);
            for (int num = 0; num < main_0.list_3.Count; num++)
            {
                if (list_26.Count > num)
                {
                    Texture2D[] textures = list_26[num];
                    XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.list_3[num], ref textures);
                    list_26[num] = textures;
                }
                else
                {
                    list_26.Add(XnaDrawingHelper.ConvertBitmapsToTextures(GraphicsDevice, main_0.list_3[num]));
                }
                method_80(main_0.list_3[num]);
            }
            for (int num2 = 0; num2 < main_0.list_4.Count; num2++)
            {
                if (list_27.Count > num2)
                {
                    Texture2D[] textures2 = list_27[num2];
                    XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.list_4[num2], ref textures2);
                    list_27[num2] = textures2;
                }
                else
                {
                    list_27.Add(XnaDrawingHelper.ConvertBitmapsToTextures(GraphicsDevice, main_0.list_4[num2]));
                }
                method_80(main_0.list_4[num2]);
            }
            texture2D_35 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_105);
            texture2D_36 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_110);
            texture2D_37 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_111);
            texture2D_38 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_112);
            texture2D_39 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_113);
            texture2D_40 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_114);
            texture2D_41 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.VnciZycUss);
            texture2D_42 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_115);
            texture2D_43 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_116);
            texture2D_44 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_117);
            texture2D_45 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_118);
            texture2D_46 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_119);
            texture2D_47 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_120);
            texture2D_48 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_121);
            texture2D_49 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_122);
            texture2D_50 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_123);
            texture2D_51 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_124);
            texture2D_52 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_125);
            Bitmap bitmap = GraphicsHelper.ScaleImage(main_0.bitmap_44, 20, 20, 1f);
            Bitmap bitmap2 = GraphicsHelper.ScaleImage(main_0.bitmap_44, 14, 14, 1f);
            texture2D_9 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
            texture2D_27 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap2);
            Bitmap bitmap3 = GraphicsHelper.ScaleImage(main_0.bitmap_43, 20, 20, 1f);
            Bitmap bitmap4 = GraphicsHelper.ScaleImage(main_0.bitmap_43, 14, 14, 1f);
            texture2D_10 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap3);
            texture2D_28 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap4);
            Bitmap bitmap5 = GraphicsHelper.ScaleImage(main_0.bitmap_55, 20, 20, 1f);
            Bitmap bitmap6 = GraphicsHelper.ScaleImage(main_0.bitmap_55, 14, 14, 1f);
            texture2D_11 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap5);
            texture2D_29 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap6);
            Bitmap[] images = GraphicsHelper.ScaleImages(main_0.bitmap_9, 40, 40);
            Bitmap[] images2 = GraphicsHelper.ScaleImages(main_0.bitmap_9, 22, 22);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, images, ref texture2D_26);
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, images2, ref texture2D_30);
            Texture2D[] textures3 = main_0.texture2D_1;
            XnaDrawingHelper.ConvertBitmapsToTexturesIfExist(GraphicsDevice, main_0.bitmap_20, ref textures3);
            main_0.texture2D_1 = textures3;
            method_80(main_0.bitmap_20);
            bool_12 = true;
        }

        private void method_80(Bitmap[] bitmap_7)
        {
            for (int i = 0; i < bitmap_7.Length; i++)
            {
                bitmap_7[i]?.Dispose();
            }
        }

        private System.Drawing.Rectangle method_81(int int_11, int int_12, int int_13, int int_14)
        {
            int num = int_13 - int_14;
            if (num != 0)
            {
                int_11 += num / 2;
            }
            int num2 = Math.Min(int_13, int_14);
            return new System.Drawing.Rectangle(int_11, int_12, num2, num2);
        }

        private Bitmap method_82(Habitat habitat_1, double double_15, bool bool_13, bool bool_14)
        {
            Font font = font_3;
            if (double_15 < 3.0)
            {
                font = font_0;
            }
            SizeF sizeF;
            using (Graphics graphics = CreateGraphics())
            {
                method_177(graphics);
                sizeF = graphics.MeasureString(habitat_1.Name, font, 200, StringFormat.GenericDefault);
            }
            if (!bool_13)
            {
                sizeF = new SizeF(1f, sizeF.Height);
            }
            int num = (int)sizeF.Width;
            int num2 = (int)sizeF.Height;
            int num3 = num + 2;
            int num4 = num2 + 2;
            int num5 = 5;
            int num6 = 12;
            if (bool_14)
            {
                num3 += num6 * 2;
            }
            Bitmap bitmap = new Bitmap(num3, num4, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap);
            int num7 = num3 / 2 - num / 2;
            int num8 = num4 / 2 - num2 / 2;
            System.Drawing.Point point_ = new System.Drawing.Point(num7, num8);
            SolidBrush solidBrush = new SolidBrush(color_1);
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                solidBrush = new SolidBrush(habitat_1.Empire.MainColor);
            }
            if (bool_13)
            {
                method_267(graphics2, habitat_1.Name, font, point_, solidBrush);
            }
            solidBrush.Dispose();
            if (bool_14)
            {
                int num9 = num3 / 2 + num / 2 + 1;
                int num10 = num8 + 1;
                if (main_0.double_0 > 3.0)
                {
                    num6 = 8;
                    num5 = 4;
                }
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num9, num10 + (int)((double)num6 * 0.85), num5, num5);
                graphics2.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num9 + num6, num10 + (int)((double)num6 * 0.85), num5, num5);
                graphics2.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num9 + num6 / 2, num10, num5, num5);
                graphics2.FillEllipse(brush, rect);
                return bitmap;
            }
            return bitmap;
        }

        private void method_83(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7)
        {
            int num = int_11 + int_13 - bitmap_7.Width / 2;
            int num2 = int_12 + int_14 / 2 - bitmap_7.Height / 2;
            graphics_0.DrawImageUnscaled(bitmap_7, num, num2);
        }

        private void method_84(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, bool bool_14)
        {
            SpriteFont spriteFont = spriteFont_0;
            if (double_15 < 3.0)
            {
                spriteFont = spriteFont_3;
            }
            Vector2 vector = spriteFont.MeasureString(habitat_1.Name);
            if (!bool_13)
            {
                vector = new Vector2(1f, vector.Y);
            }
            int num = (int)vector.X;
            int num2 = (int)vector.Y;
            int num3 = int_11 + int_13 - num / 2;
            int num4 = int_12 + int_14 / 2 - num2 / 2;
            System.Drawing.Point point = new System.Drawing.Point(num3, num4);
            System.Drawing.Color mainColor = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                mainColor = habitat_1.Empire.MainColor;
            }
            if (bool_13)
            {
                XnaDrawingHelper.DrawStringDropShadow(spriteBatch_2, habitat_1.Name, spriteFont, mainColor, point);
            }
            if (bool_14)
            {
                int num5 = int_11 + int_13 + num / 2 + 1;
                int num6 = num4 + 1;
                int num7 = 5;
                int num8 = 12;
                if (main_0.double_0 > 3.0)
                {
                    num8 = 8;
                    num7 = 4;
                }
                using (new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255)))
                {
                    System.Drawing.Rectangle rectangle = new System.Drawing.Rectangle(num5, num6 + (int)((double)num8 * 0.85), num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                    rectangle = new System.Drawing.Rectangle(num5 + num8, num6 + (int)((double)num8 * 0.85), num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                    rectangle = new System.Drawing.Rectangle(num5 + num8 / 2, num6, num7, num7);
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle, System.Drawing.Color.White);
                }
            }
        }

        private void method_85(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, double double_15, bool bool_13, bool bool_14)
        {
            Font font = font_3;
            if (double_15 < 3.0)
            {
                font = font_0;
            }
            SizeF sizeF = graphics_0.MeasureString(habitat_1.Name, font, 200, StringFormat.GenericDefault);
            if (!bool_13)
            {
                sizeF = new SizeF(1f, sizeF.Height);
            }
            int num = (int)sizeF.Width;
            int num2 = (int)sizeF.Height;
            int num3 = int_11 + int_13 - num / 2;
            int num4 = int_12 + int_14 / 2 - num2 / 2;
            System.Drawing.Point point_ = new System.Drawing.Point(num3, num4);
            SolidBrush solidBrush = new SolidBrush(color_1);
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                solidBrush = new SolidBrush(habitat_1.Empire.MainColor);
            }
            if (bool_13)
            {
                method_267(graphics_0, habitat_1.Name, font, point_, solidBrush);
            }
            solidBrush.Dispose();
            if (bool_14)
            {
                int num5 = int_11 + int_13 + num / 2 + 1;
                int num6 = num4 + 1;
                int num7 = 5;
                int num8 = 12;
                if (main_0.double_0 > 3.0)
                {
                    num8 = 8;
                    num7 = 4;
                }
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(255, 255, 255));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(num5, num6 + (int)((double)num8 * 0.85), num7, num7);
                graphics_0.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num5 + num8, num6 + (int)((double)num8 * 0.85), num7, num7);
                graphics_0.FillEllipse(brush, rect);
                rect = new System.Drawing.Rectangle(num5 + num8 / 2, num6, num7, num7);
                graphics_0.FillEllipse(brush, rect);
            }
        }

        private void method_86(SpriteBatch spriteBatch_2, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Texture2D texture2D_61)
        {
            int num = int_11 + int_13 - texture2D_61.Width / 2;
            int num2 = int_12 + int_14 / 2 - texture2D_61.Height / 2;
            XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, num, num2, 0f, 1f);
        }

        private void method_87(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7)
        {
            int num = int_11 + int_13 - bitmap_7.Width / 2;
            int num2 = int_12 + int_14 / 2 - bitmap_7.Height / 2;
            System.Drawing.Point point = new System.Drawing.Point(num, num2);
            graphics_0.DrawImageUnscaled(bitmap_7, point);
        }

        private void method_88(Graphics graphics_0, Habitat habitat_1, int int_11, int int_12, int int_13, int int_14)
        {
            SizeF sizeF = graphics_0.MeasureString(habitat_1.Name, font_0, 300);
            int num = (int)sizeF.Width;
            _ = sizeF.Height;
            int num2 = 0;
            System.Drawing.Color mainColor = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                if (habitat_1.Empire.Capital == habitat_1 || habitat_1.Empire.Capitals.Contains(habitat_1))
                {
                    num2 = 16;
                }
                mainColor = habitat_1.Empire.MainColor;
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population != null && habitat_1.Population.Count > 0)
            {
                num3 = 20;
                num4 = 20;
            }
            bool flag = false;
            if (galaxy_0.PlayerEmpire.ResourceMap != null)
            {
                flag = galaxy_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_1);
            }
            int num5 = 0;
            if (habitat_1.Resources != null && habitat_1.Resources.Count > 0 && flag)
            {
                num5 = 16 * habitat_1.Resources.Count;
            }
            int num6 = num2 + num;
            int num7 = num3 + num4 + 6 + num5;
            int num8 = Math.Max(num6, num7);
            int num9 = 36;
            int num10 = num8 + 18;
            int num11 = int_11 + int_13 - num10 / 2;
            int num12 = int_12 + int_14 / 2 - 18;
            int num13 = 9;
            int num14 = 18;
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            List<System.Drawing.Point> list2 = new List<System.Drawing.Point>();
            list.Add(new System.Drawing.Point(num11, num12 + 18));
            list.Add(new System.Drawing.Point(num11 + 9, num12));
            list.Add(new System.Drawing.Point(num11 + num10 - 9, num12));
            list.Add(new System.Drawing.Point(num11 + num10, num12 + 18));
            list2.AddRange(list);
            list.Add(new System.Drawing.Point(num11 + num10 - 9, num12 + 36));
            list.Add(new System.Drawing.Point(num11 + 9, num12 + 36));
            using (Pen pen = new Pen(mainColor, 1f))
            {
                graphics_0.FillPolygon(solidBrush_2, list.ToArray());
                graphics_0.FillPolygon(new SolidBrush(pen.Color), list2.ToArray());
                graphics_0.DrawPolygon(pen, list.ToArray());
            }
            List<System.Drawing.Point> list3 = new List<System.Drawing.Point>();
            new List<System.Drawing.Point>();
            List<System.Drawing.Point> list4 = new List<System.Drawing.Point>();
            int num15 = 2;
            list3.Add(new System.Drawing.Point(num11 - 1, num12 + num14));
            list3.Add(new System.Drawing.Point(num11 + num13 - 1, num12 - 1));
            list3.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 - 1));
            list4.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 - 1));
            list4.Add(new System.Drawing.Point(num11 + num10 + 1, num12 + num14));
            list4.Add(new System.Drawing.Point(num11 + num10 - num13 + 1, num12 + num9 + 1));
            list4.Add(new System.Drawing.Point(num11 + num13 - 1, num12 + num9 + 1));
            list4.Add(new System.Drawing.Point(num11 - 1, num12 + num14));
            System.Drawing.Color color = ControlPaint.Dark(mainColor);
            System.Drawing.Color color2 = ControlPaint.Light(mainColor);
            using (Pen pen2 = new Pen(color2, 2f))
            {
                graphics_0.DrawLines(pen2, list3.ToArray());
            }
            using (Pen pen3 = new Pen(color, num15))
            {
                graphics_0.DrawLines(pen3, list4.ToArray());
            }
            int num16 = num11 + num13 + (num8 - num6) / 2;
            int num17 = num12 + 2;
            using (Pen pen4 = new Pen(mainColor, 1f))
            {
                if (num2 > 0)
                {
                    if (habitat_1.Empire != null && habitat_1.Empire.Capital == habitat_1)
                    {
                        graphics_0.DrawImage(main_0.bitmap_44, new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    else
                    {
                        graphics_0.DrawImage(main_0.bitmap_43, new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    if (bool_4)
                    {
                        method_199(new System.Drawing.Rectangle(num16 - 5, num17 - 5, 25, 25), graphics_0);
                    }
                    num16 += num2;
                }
                method_266(graphics_0, habitat_1.Name, font_0, new System.Drawing.Point(num16, num17));
                num16 = num11 + num13 + 3 + (num8 - num7) / 2;
                num17 = num12 + num14 + 2;
                graphics_0.DrawLine(pen4, num11, num12 + num14, num11 + num10, num12 + num14);
                if (num3 > 0)
                {
                    if (habitat_1.Population != null && habitat_1.Population.DominantRace != null)
                    {
                        graphics_0.DrawImage(main_0.raceImageCache_0.GetRaceImage(habitat_1.Population.DominantRace.PictureRef), new System.Drawing.Rectangle(num16, num17, 15, 15));
                    }
                    if (bool_3)
                    {
                        method_201(new System.Drawing.Rectangle(num16 - 5, num17 - 5, 25, 25), graphics_0);
                    }
                    num16 += 17;
                }
                if (num4 > 0)
                {
                    SmoothingMode smoothingMode = graphics_0.SmoothingMode;
                    graphics_0.SmoothingMode = SmoothingMode.None;
                    method_155(habitat_1, num16, num17, graphics_0);
                    graphics_0.SmoothingMode = smoothingMode;
                    num16 += num4;
                }
                num16 += 4;
                graphics_0.DrawLine(pen4, num16, num12 + num14, num16, num12 + num9);
                num16 += 5;
            }
            if (num5 <= 0 || !flag)
            {
                return;
            }
            int num18 = num16;
            num17++;
            HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                Bitmap bitmap = main_0._uiResourcesBitmaps[habitatResourceList[i].PictureRef];
                double num19 = (double)bitmap.Width / (double)bitmap.Height;
                int num20 = 0;
                int num21 = 0;
                if (num19 < 1.0)
                {
                    num20 = (int)(12.0 * num19);
                    num21 = 12;
                }
                else
                {
                    num20 = 12;
                    num21 = (int)(12.0 / num19);
                }
                Size size = new Size(num20, num21);
                System.Drawing.Point location = new System.Drawing.Point(num16, num17 + (12 - num21) / 2);
                graphics_0.DrawImage(bitmap, new System.Drawing.Rectangle(location, size));
                num16 += size.Width + 2;
            }
            if (bool_1)
            {
                method_198(new System.Drawing.Rectangle(num18 - 5, num17 - 5, num16 - num18 + 10, 22), graphics_0);
            }
        }

        private Bitmap method_89(Habitat habitat_1)
        {
            SizeF sizeF;
            using (Graphics graphics = CreateGraphics())
            {
                method_177(graphics);
                sizeF = graphics.MeasureString(habitat_1.Name, font_0, 300);
            }
            int num = (int)sizeF.Width;
            _ = sizeF.Height;
            int num2 = 0;
            System.Drawing.Color color = color_1;
            if (habitat_1.Empire != null && habitat_1.Empire != galaxy_0.IndependentEmpire)
            {
                if (habitat_1.Empire.Capital == habitat_1 || habitat_1.Empire.Capitals.Contains(habitat_1))
                {
                    num2 = 16;
                }
                color = ((habitat_1.Empire.PirateEmpireBaseHabitat == null) ? habitat_1.Empire.MainColor : ((!(habitat_1.Empire.MainColor == System.Drawing.Color.FromArgb(1, 1, 1))) ? habitat_1.Empire.MainColor : System.Drawing.Color.FromArgb(48, 48, 48)));
            }
            int num3 = 0;
            int num4 = 0;
            if (habitat_1.Population != null && habitat_1.Population.Count > 0)
            {
                num3 = 20;
                num4 = 20;
            }
            bool flag = false;
            if (galaxy_0.PlayerEmpire.ResourceMap != null)
            {
                flag = galaxy_0.PlayerEmpire.ResourceMap.CheckResourcesKnown(habitat_1);
            }
            int num5 = 0;
            if (habitat_1.Resources != null && habitat_1.Resources.Count > 0 && flag)
            {
                num5 = 16 * habitat_1.Resources.Count;
            }
            int num6 = num2 + num;
            int num7 = num3 + num4 + 6 + num5;
            int num8 = Math.Max(num6, num7);
            int num9 = 36;
            int num10 = num8 + 18;
            int num11 = num10 + 2;
            int num12 = num11 / 2 - num10 / 2;
            int num13 = 1;
            Bitmap bitmap = new Bitmap(num11, 38, PixelFormat.Format32bppPArgb);
            using Graphics graphics2 = Graphics.FromImage(bitmap);
            method_177(graphics2);
            int num14 = num9 / 4;
            int num15 = (int)((double)num9 * 0.5);
            List<System.Drawing.Point> list = new List<System.Drawing.Point>();
            List<System.Drawing.Point> list2 = new List<System.Drawing.Point>();
            list.Add(new System.Drawing.Point(num12, num13 + num15));
            list.Add(new System.Drawing.Point(num12 + num14, num13));
            list.Add(new System.Drawing.Point(num12 + num10 - num14, num13));
            list.Add(new System.Drawing.Point(num12 + num10, num13 + num15));
            list2.AddRange(list);
            list.Add(new System.Drawing.Point(num12 + num10 - num14, num13 + num9));
            list.Add(new System.Drawing.Point(num12 + num14, num13 + num9));
            using (Pen pen = new Pen(color, 1f))
            {
                graphics2.FillPolygon(solidBrush_2, list.ToArray());
                graphics2.FillPolygon(new SolidBrush(pen.Color), list2.ToArray());
                graphics2.DrawPolygon(pen, list.ToArray());
            }
            List<System.Drawing.Point> list3 = new List<System.Drawing.Point>();
            new List<System.Drawing.Point>();
            List<System.Drawing.Point> list4 = new List<System.Drawing.Point>();
            int num16 = 2;
            list3.Add(new System.Drawing.Point(num12 - 1, num13 + num15));
            list3.Add(new System.Drawing.Point(num12 + num14 - 1, num13 - 1));
            list3.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 - 1));
            list4.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 - 1));
            list4.Add(new System.Drawing.Point(num12 + num10 + 1, num13 + num15));
            list4.Add(new System.Drawing.Point(num12 + num10 - num14 + 1, num13 + num9 + 1));
            list4.Add(new System.Drawing.Point(num12 + num14 - 1, num13 + num9 + 1));
            list4.Add(new System.Drawing.Point(num12 - 1, num13 + num15));
            System.Drawing.Color color2 = ControlPaint.Dark(color);
            System.Drawing.Color color3 = ControlPaint.Light(color);
            using (Pen pen2 = new Pen(color3, 2f))
            {
                graphics2.DrawLines(pen2, list3.ToArray());
            }
            using (Pen pen3 = new Pen(color2, num16))
            {
                graphics2.DrawLines(pen3, list4.ToArray());
            }
            int num17 = num12 + num14 + (num8 - num6) / 2;
            int num18 = num13 + 2;
            using (Pen pen4 = new Pen(color, 1f))
            {
                if (num2 > 0)
                {
                    if (habitat_1.Empire != null && habitat_1.Empire.Capital == habitat_1)
                    {
                        graphics2.DrawImage(main_0.bitmap_44, new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    else
                    {
                        graphics2.DrawImage(main_0.bitmap_43, new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    if (bool_4)
                    {
                        method_199(new System.Drawing.Rectangle(num17 - 5, num18 - 5, 25, 25), graphics2);
                    }
                    num17 += num2;
                }
                method_266(graphics2, habitat_1.Name, font_0, new System.Drawing.Point(num17, num18));
                num17 = num12 + num14 + 3 + (num8 - num7) / 2;
                num18 = num13 + num15 + 2;
                graphics2.DrawLine(pen4, num12, num13 + num15, num12 + num10, num13 + num15);
                if (num3 > 0)
                {
                    if (habitat_1.Population != null && habitat_1.Population.DominantRace != null)
                    {
                        graphics2.DrawImage(main_0.raceImageCache_0.GetRaceImage(habitat_1.Population.DominantRace.PictureRef), new System.Drawing.Rectangle(num17, num18, 15, 15));
                    }
                    if (bool_3)
                    {
                        method_201(new System.Drawing.Rectangle(num17 - 5, num18 - 5, 25, 25), graphics2);
                    }
                    num17 += 17;
                }
                if (num4 > 0)
                {
                    SmoothingMode smoothingMode = graphics2.SmoothingMode;
                    graphics2.SmoothingMode = SmoothingMode.None;
                    method_155(habitat_1, num17, num18, graphics2);
                    graphics2.SmoothingMode = smoothingMode;
                    num17 += num4;
                }
                num17 += 4;
                graphics2.DrawLine(pen4, num17, num13 + num15, num17, num13 + num9);
                num17 += 5;
            }
            if (num5 > 0)
            {
                if (flag)
                {
                    int num19 = num17;
                    num18++;
                    HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
                    for (int i = 0; i < habitatResourceList.Count; i++)
                    {
                        Bitmap bitmap2 = main_0._uiResourcesBitmaps[habitatResourceList[i].PictureRef];
                        double num20 = (double)bitmap2.Width / (double)bitmap2.Height;
                        int num21 = 0;
                        int num22 = 0;
                        if (num20 < 1.0)
                        {
                            num21 = (int)(12.0 * num20);
                            num22 = 12;
                        }
                        else
                        {
                            num21 = 12;
                            num22 = (int)(12.0 / num20);
                        }
                        Size size = new Size(num21, num22);
                        System.Drawing.Point location = new System.Drawing.Point(num17, num18 + (12 - num22) / 2);
                        graphics2.DrawImage(bitmap2, new System.Drawing.Rectangle(location, size));
                        num17 += size.Width + 2;
                    }
                    if (bool_1)
                    {
                        method_198(new System.Drawing.Rectangle(num19 - 5, num18 - 5, num17 - num19 + 10, 22), graphics2);
                        return bitmap;
                    }
                    return bitmap;
                }
                return bitmap;
            }
            return bitmap;
        }

        private void method_90(int int_11, int int_12, double double_15, out double double_16, out double double_17)
        {
            double num = (double)base.ClientRectangle.Width / 2.0;
            double y = (double)base.ClientRectangle.Height / 2.0;
            double_16 = ((double)int_11 - num) / num;
            int num2 = (int)main_0._Game.Galaxy.CalculateDistance(num, y, 0.0, 0.0);
            num2 = (int)((double)num2 * 1.5);
            int num3 = (int)main_0._Game.Galaxy.CalculateDistance(num, y, int_11, int_12);
            double val = (double)(num2 - num3) / (double)num2;
            val = Math.Max(0.02, val);
            double num4 = Math.Max(1.0, Math.Sqrt(double_15));
            double_17 = val / num4;
            if (double_15 > 50.0)
            {
                double_17 = 0.0;
            }
        }

        private void method_91(double double_15, double double_16, double double_17)
        {
            bool flag = false;
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (currentStarDate <= long_0)
            {
                return;
            }
            if (double_17 < 100.0)
            {
                double range = double_17 * double_9;
                GalaxyLocationList galaxyLocationList = galaxy_0.DetermineGalaxyLocationsInRangeAtPoint(double_15, double_16, range, GalaxyLocationType.RestrictedArea);
                if (galaxyLocationList != null && galaxyLocationList.Count > 0)
                {
                    for (int i = 0; i < galaxyLocationList.Count; i++)
                    {
                        GalaxyLocation galaxyLocation = galaxyLocationList[i];
                        if (galaxyLocation.SoundScheme >= 0)
                        {
                            flag = true;
                            double balance = 0.0;
                            double distance = 200.0;
                            int nextEffectOffset = 8000;
                            SoundEffectRequest soundEffectRequest = main_0.EffectsPlayer.ResolveAmbientEffect(galaxyLocation.SoundScheme, balance, distance, out nextEffectOffset);
                            if (soundEffectRequest != null)
                            {
                                main_0.method_0(soundEffectRequest);
                            }
                            long_0 = currentStarDate + nextEffectOffset;
                        }
                    }
                }
            }
            if (flag && main_0.musicPlayer_0.IsPlaying && !main_0.musicPlayer_0.IsInitiatingFade && main_0.double_0 < 100.0)
            {
                main_0.musicPlayer_0.FadePause();
            }
            else if (main_0._Game.Galaxy.TimeState == GalaxyTimeState.Running && (!flag || main_0.double_0 >= 100.0) && (!main_0.musicPlayer_0.IsPlaying || main_0.musicPlayer_0.ActualVolume <= 0.0) && !main_0.musicPlayer_1.IsPlaying && !main_0.musicPlayer_0.IsInitiatingFade)
            {
                main_0.musicPlayer_0.FadeResume();
            }
        }

        private void method_92(Habitat habitat_1, int int_11, int int_12, bool bool_13)
        {
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (habitat_1.Category == HabitatCategoryType.Star)
            {
                if (currentStarDate > habitat_1.NextSoundTime)
                {
                    double double_ = 0.0;
                    double double_2 = 0.0;
                    method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                    SoundEffectRequest soundEffectRequest = main_0.EffectsPlayer.ResolveStar(habitat_1.Type, double_, double_2);
                    if (soundEffectRequest != null)
                    {
                        main_0.method_0(soundEffectRequest);
                    }
                    habitat_1.NextSoundTime = currentStarDate + 4200L;
                }
            }
            else
            {
                if ((habitat_1.Category != HabitatCategoryType.Planet && habitat_1.Category != HabitatCategoryType.Moon) || currentStarDate <= habitat_1.NextSoundTime || habitat_1.ConstructionQueue == null || habitat_1.ConstructionQueue.ConstructionYards == null || habitat_1.ConstructionQueue.ConstructionYards.CountUnderConstruction <= 0 || !galaxy_0.PlayerEmpire.IsObjectVisibleToThisEmpire(habitat_1))
                {
                    return;
                }
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_3, out double_4);
                main_0.method_0(main_0.EffectsPlayer.ResolveConstruction(double_3, double_4));
                habitat_1.NextSoundTime = currentStarDate + 4100L;
                int val = (int)Math.Sqrt(habitat_1.Diameter * 10);
                BuiltObject shipUnderConstruction = habitat_1.ConstructionQueue.ConstructionYards[0].ShipUnderConstruction;
                double num = 0.0;
                double num2 = 0.0;
                if (shipUnderConstruction != null)
                {
                    val = (int)Math.Sqrt(shipUnderConstruction.Size * 15);
                    if ((shipUnderConstruction.ParentBuiltObject != null || shipUnderConstruction.ParentHabitat != null) && shipUnderConstruction.ParentOffsetX > -2000000001.0 && shipUnderConstruction.ParentOffsetY > -2000000001.0)
                    {
                        num = shipUnderConstruction.ParentOffsetX;
                        num2 = shipUnderConstruction.ParentOffsetY;
                    }
                }
                val = Math.Min(100, val);
                double num3 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double double_5 = 0.0;
                double double_6 = 0.0;
                method_96((int)((double)val * 0.5), num3, out double_5, out double_6);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 30, habitat_1.Xpos + num + double_5, habitat_1.Ypos + num2 + double_6, val, val, num3, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 30, habitat_1.Xpos + num + double_5, habitat_1.Ypos + num2 + double_6, val, val, num3, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
            }
        }

        private void method_93(Graphics graphics_0, DateTime dateTime_5, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            if (builtObject_1.ShieldAreaRechargeTarget == null || !(builtObject_1.ShieldAreaRechargeStartTime > DateTime.MinValue))
            {
                return;
            }
            if (builtObject_1.ShieldAreaRechargeTarget.CurrentSpeed >= (float)builtObject_1.ShieldAreaRechargeTarget.WarpSpeed && builtObject_1.ShieldAreaRechargeTarget.WarpSpeed > 0)
            {
                builtObject_1.ShieldAreaRechargeStartTime = DateTime.MinValue;
                builtObject_1.ShieldAreaRechargeTarget = null;
            }
            else
            {
                if (!(dateTime_5 > builtObject_1.ShieldAreaRechargeStartTime))
                {
                    return;
                }
                System.Drawing.Color color = method_214(System.Drawing.Color.FromArgb(56, 80, 32, 192), System.Drawing.Color.FromArgb(8, 32, 192, 255), dateTime_5);
                System.Drawing.Point pt = new System.Drawing.Point(int_11, int_12);
                System.Drawing.Point pt2 = new System.Drawing.Point(int_14, int_15);
                if (pt.X == pt2.X && pt.Y == pt2.Y)
                {
                    pt2 = new System.Drawing.Point(int_14 + 1, int_15 + 1);
                }
                using SolidBrush brush = new SolidBrush(color);
                using Pen pen = new Pen(brush);
                pen.Width = int_13;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
                graphics_0.DrawLine(pen, pt, pt2);
                graphics_0.SmoothingMode = SmoothingMode.None;
            }
        }

        private void method_94(SpriteBatch spriteBatch_2, DateTime dateTime_5, BuiltObject builtObject_1, int int_11, int int_12, int int_13, int int_14, int int_15)
        {
            if (builtObject_1.ShieldAreaRechargeTarget == null || !(builtObject_1.ShieldAreaRechargeStartTime > DateTime.MinValue))
            {
                return;
            }
            if (builtObject_1.ShieldAreaRechargeTarget.CurrentSpeed >= (float)builtObject_1.ShieldAreaRechargeTarget.WarpSpeed && builtObject_1.ShieldAreaRechargeTarget.WarpSpeed > 0)
            {
                builtObject_1.ShieldAreaRechargeStartTime = DateTime.MinValue;
                builtObject_1.ShieldAreaRechargeTarget = null;
            }
            else if (dateTime_5 > builtObject_1.ShieldAreaRechargeStartTime)
            {
                System.Drawing.Color color = method_214(System.Drawing.Color.FromArgb(56, 80, 32, 192), System.Drawing.Color.FromArgb(8, 32, 192, 255), dateTime_5);
                System.Drawing.Point start = new System.Drawing.Point(int_11, int_12);
                System.Drawing.Point end = new System.Drawing.Point(int_14, int_15);
                if (start.X == end.X && start.Y == end.Y)
                {
                    end = new System.Drawing.Point(int_14 + 1, int_15 + 1);
                }
                XnaDrawingHelper.DrawLineCustomTexture(spriteBatch_2, start, end, texture2D_17, color, int_13);
            }
        }

        private void method_95(BuiltObject builtObject_1, int int_11, int int_12, bool bool_13)
        {
            long currentStarDate = galaxy_0.CurrentStarDate;
            if (builtObject_1.DoingConstruction && currentStarDate > builtObject_1.NextSoundTimeConstruction)
            {
                double double_ = 0.0;
                double double_2 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_, out double_2);
                main_0.method_0(main_0.EffectsPlayer.ResolveConstruction(double_, double_2));
                builtObject_1.NextSoundTimeConstruction = currentStarDate + 4100L;
                int val = (int)Math.Sqrt(builtObject_1.Size * 15);
                val = Math.Min(100, val);
                double num = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_96((int)((double)val * 1.0), num, out double_3, out double_4);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(texture2D_33, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_3, builtObject_1.Ypos + double_4, val, val, num, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.bitmap_212, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_3, builtObject_1.Ypos + double_4, val, val, num, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
            }
            if (builtObject_1.DoingMining && currentStarDate > builtObject_1.NextSoundTimeMining)
            {
                double double_5 = 0.0;
                double double_6 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_5, out double_6);
                main_0.method_0(main_0.EffectsPlayer.ResolveMining(double_5, double_6));
                builtObject_1.NextSoundTimeMining = currentStarDate + 3000L;
                if (builtObject_1.ParentHabitat != null && (builtObject_1.ParentHabitat.Category == HabitatCategoryType.Asteroid || builtObject_1.ParentHabitat.Type == HabitatType.BarrenRock || builtObject_1.ParentHabitat.Type == HabitatType.Volcanic))
                {
                    int val2 = (int)Math.Sqrt(builtObject_1.Size * 15);
                    val2 = Math.Min(60, val2);
                    double num2 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
                    double double_7 = 0.0;
                    double double_8 = 0.0;
                    method_96((int)((double)val2 * 0.8), num2, out double_7, out double_8);
                    if (bool_13)
                    {
                        DistantWorlds.Animation animation3 = new DistantWorlds.Animation(texture2D_31, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, val2, val2, num2, System.Drawing.Color.Empty);
                        animation3.DisposeTexturesWhenComplete = false;
                        animationSystem_0.AddAnimation(animation3);
                    }
                    else
                    {
                        DistantWorlds.Animation animation4 = new DistantWorlds.Animation(main_0.bitmap_210, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, val2, val2, num2, System.Drawing.Color.Empty);
                        animationSystem_0.AddAnimation(animation4);
                    }
                }
            }
            if (!builtObject_1.DoingGasMining || currentStarDate <= builtObject_1.NextSoundTimeGasMining)
            {
                return;
            }
            double double_9 = 0.0;
            double double_10 = 0.0;
            method_90(int_11, int_12, main_0.double_0, out double_9, out double_10);
            main_0.method_0(main_0.EffectsPlayer.ResolveGasMining(double_9, double_10));
            builtObject_1.NextSoundTimeGasMining = currentStarDate + 5600L;
            int val3 = (int)Math.Sqrt(builtObject_1.Size * 15);
            val3 = Math.Min(80, val3);
            double num3 = Galaxy.Rnd.NextDouble() * Math.PI * 2.0;
            double double_11 = 0.0;
            double double_12 = 0.0;
            method_96((int)((double)val3 * 0.7), num3, out double_11, out double_12);
            System.Drawing.Color tintColor = System.Drawing.Color.Empty;
            if (builtObject_1.ParentHabitat != null && builtObject_1.ParentHabitat.Resources != null && builtObject_1.ParentHabitat.Resources.Count > 0)
            {
                Resource resource = null;
                int num4 = 0;
                HabitatResourceList habitatResourceList = builtObject_1.ParentHabitat.Resources.Clone();
                for (int i = 0; i < habitatResourceList.Count; i++)
                {
                    HabitatResource habitatResource = habitatResourceList[i];
                    if (habitatResource.Group == ResourceGroup.Gas && habitatResource.Abundance > num4)
                    {
                        num4 = habitatResource.Abundance;
                        resource = habitatResource;
                    }
                }
                if (resource != null)
                {
                    tintColor = resource.Name switch
                    {
                        "Argon" => System.Drawing.Color.FromArgb(255, 255, 0),
                        "Caslon" => System.Drawing.Color.FromArgb(0, 0, 255),
                        "Helium" => System.Drawing.Color.FromArgb(204, 0, 51),
                        "Hydrogen" => System.Drawing.Color.FromArgb(160, 0, 0),
                        "Krypton" => System.Drawing.Color.FromArgb(0, 255, 0),
                        "Tyderios" => System.Drawing.Color.FromArgb(255, 0, 255),
                        _ => System.Drawing.Color.FromArgb(204, 0, 51),
                    };
                }
            }
            if (bool_13)
            {
                DistantWorlds.Animation animation5 = new DistantWorlds.Animation(texture2D_32, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_11, builtObject_1.Ypos + double_12, val3, val3, num3, tintColor);
                animation5.DisposeTexturesWhenComplete = false;
                animationSystem_0.AddAnimation(animation5);
            }
            else
            {
                DistantWorlds.Animation animation6 = new DistantWorlds.Animation(main_0.bitmap_211, galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_11, builtObject_1.Ypos + double_12, val3, val3, num3, tintColor);
                animationSystem_0.AddAnimation(animation6);
            }
        }

        private void method_96(int int_11, double double_15, out double double_16, out double double_17)
        {
            double num = (double)int_11 / 2.0;
            double_16 = Math.Cos(double_15) * num;
            double_17 = Math.Sin(double_15) * num;
        }

        private void method_97(BuiltObject builtObject_1, int int_11, int int_12, long long_1, bool bool_13)
        {
            long num = builtObject_1.HyperjumpCountdown - long_1;
            if (num > 0L && builtObject_1.HyperEnterStartAnimation && num < 800L && builtObject_1.CanHyperJump)
            {
                int num2 = (int)Math.Sqrt(builtObject_1.Size * 30);
                double double_ = 0.0;
                double double_2 = 0.0;
                method_96((int)((double)num2 * 0.7), (double)builtObject_1.TargetHeading + Math.PI, out double_, out double_2);
                if (bool_13)
                {
                    DistantWorlds.Animation animation = new DistantWorlds.Animation(list_26[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_, builtObject_1.Ypos + double_2, num2, num2, (double)builtObject_1.TargetHeading * -1.0, System.Drawing.Color.Empty);
                    animation.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation);
                }
                else
                {
                    DistantWorlds.Animation animation2 = new DistantWorlds.Animation(main_0.list_3[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_, builtObject_1.Ypos + double_2, num2, num2, builtObject_1.TargetHeading, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation2);
                }
                builtObject_1.HyperEnterStartAnimation = false;
            }
            if (builtObject_1.HyperjumpAboutToEnter && !builtObject_1.HyperjumpAboutToEnterSoundPlayed)
            {
                double double_3 = 0.0;
                double double_4 = 0.0;
                method_90(int_11, int_12, main_0.double_0, out double_3, out double_4);
                main_0.method_0(main_0.EffectsPlayer.ResolveHyperjumpEntry(double_3, double_4));
                builtObject_1.HyperjumpAboutToEnterSoundPlayed = true;
            }
            if (!builtObject_1.HyperjumpJustExited)
            {
                return;
            }
            double double_5 = 0.0;
            double double_6 = 0.0;
            method_90(int_11, int_12, main_0.double_0, out double_5, out double_6);
            main_0.method_0(main_0.EffectsPlayer.ResolveHyperjumpExit(double_5, double_6));
            if (builtObject_1.HyperExitStartAnimation)
            {
                int num3 = (int)Math.Sqrt(builtObject_1.Size * 30);
                double double_7 = 0.0;
                double double_8 = 0.0;
                method_96((int)((double)num3 * 0.7), (double)builtObject_1.TargetHeading + Math.PI, out double_7, out double_8);
                if (bool_13)
                {
                    DistantWorlds.Animation animation3 = new DistantWorlds.Animation(list_27[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, num3, num3, (double)builtObject_1.TargetHeading * -1.0, System.Drawing.Color.Empty);
                    animation3.DisposeTexturesWhenComplete = false;
                    animationSystem_0.AddAnimation(animation3);
                }
                else
                {
                    DistantWorlds.Animation animation4 = new DistantWorlds.Animation(main_0.list_4[builtObject_1.Design.HyperDriveIndex], galaxy_0.CurrentDateTime, 30, builtObject_1.Xpos + double_7, builtObject_1.Ypos + double_8, num3, num3, builtObject_1.TargetHeading, System.Drawing.Color.Empty);
                    animationSystem_0.AddAnimation(animation4);
                }
            }
            builtObject_1.HyperExitStartAnimation = false;
        }

        private void method_98(int int_11, int int_12, int int_13, int int_14, Graphics graphics_0)
        {
            using Pen pen = new Pen(System.Drawing.Color.Orange, 2f);
            pen.DashStyle = DashStyle.Dash;
            graphics_0.DrawEllipse(pen, int_11, int_12, int_13, int_14);
        }

        private void method_99(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14)
        {
            XnaDrawingHelper.DrawCircle(spriteBatch_2, int_11, int_12, int_13, int_14, System.Drawing.Color.Orange, 2, 100, dashed: true);
        }

        private void method_100(Habitat habitat_1, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 0;
            HabitatResourceList habitatResourceList = habitat_1.Resources.Clone();
            for (int i = 0; i < habitatResourceList.Count; i++)
            {
                HabitatResource habitatResource = habitatResourceList[i];
                Bitmap image = bitmap_6[habitatResource.PictureRef];
                int num2 = 0;
                int num3 = 0;
                if (habitat_1.Diameter >= 30)
                {
                    switch (num)
                    {
                        case 0:
                            num2 = -19;
                            num3 = 3;
                            break;
                        case 1:
                            num2 = -37;
                            num3 = 3;
                            break;
                        case 2:
                            num2 = -19;
                            num3 = 21;
                            break;
                        case 3:
                            num2 = -37;
                            num3 = 21;
                            break;
                        case 4:
                            num2 = -19;
                            num3 = 39;
                            break;
                    }
                }
                else
                {
                    switch (num)
                    {
                        case 0:
                            num2 = 2;
                            num3 = -18;
                            break;
                        case 1:
                            num2 = -16;
                            num3 = -18;
                            break;
                        case 2:
                            num2 = 2;
                            num3 = 0;
                            break;
                        case 3:
                            num2 = -34;
                            num3 = -18;
                            break;
                        case 4:
                            num2 = 2;
                            num3 = 18;
                            break;
                    }
                }
                System.Drawing.Point point = new System.Drawing.Point(int_11 + num2, int_12 + num3);
                graphics_0.DrawImageUnscaled(image, point);
                num++;
            }
            if (habitat_0 != null && habitat_1 == habitat_0 && bool_1)
            {
                System.Drawing.Rectangle rectangle_ = new System.Drawing.Rectangle(int_11 - 41, int_12, 41, 41);
                method_198(rectangle_, graphics_0);
            }
        }

        private StarFieldItemList method_101(int int_11, int int_12, int int_13)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StarFieldItemList starFieldItemList = new StarFieldItemList();
            for (int i = 0; i < int_12; i++)
            {
                StarFieldItem item = default(StarFieldItem);
                item.X = random.Next(0, int_11);
                item.Y = random.Next(0, int_11);
                item.Size = int_13;
                starFieldItemList.Add(item);
            }
            return starFieldItemList;
        }

        private void method_102(SpriteBatch spriteBatch_2, Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12)
        {
            int num = (int)((double)main_0.int_13 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            float num7 = method_45();
            new Microsoft.Xna.Framework.Color(1f, 1f, 1f, num7);
            int alpha = Math.Max(0, Math.Min(255, (int)(num7 * 255f)));
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            if (num3 < 0)
            {
                num8 = num3 + int_11;
                num9 = int_11;
            }
            if (num4 > int_11)
            {
                num8 = 0;
                num9 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num10 = num5 + int_11;
                num11 = int_11;
            }
            if (num6 > int_11)
            {
                num10 = 0;
                num11 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num8, num5, num9 - num8, num6 - num5);
            new System.Drawing.Rectangle(num3, num10, num4 - num3, num11 - num10);
            new System.Drawing.Rectangle(num8, num10, num9 - num8, num11 - num10);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num12 = starFieldItemList_4[i].X - num3;
                int num13 = starFieldItemList_4[i].Y - num5;
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                if (num13 > int_11)
                {
                    num13 -= int_11;
                }
                else if (num13 < 0)
                {
                    num13 += int_11;
                }
                rectangle_5[i].X = num12;
                rectangle_5[i].Y = num13;
                int num14 = 128;
                int num15 = Galaxy.Rnd.Next(0, 127);
                int num16 = Galaxy.Rnd.Next(0, 127);
                int num17 = Galaxy.Rnd.Next(0, 127);
                using (new SolidBrush(System.Drawing.Color.FromArgb(128 + num15, 128 + num16, 128 + num17)))
                {
                    int num18 = i % texture2D_19.Length;
                    Texture2D texture = texture2D_19[num18];
                    XnaDrawingHelper.DrawTexture(spriteBatch_0, texture, rectangle_5[i], 0f, System.Drawing.Color.FromArgb(alpha, num14 + num15, num14 + num16, num14 + num17));
                }
            }
        }

        private void method_103(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
                int num13 = Galaxy.Rnd.Next(0, 72);
                int num14 = Galaxy.Rnd.Next(0, 72);
                int num15 = Galaxy.Rnd.Next(0, 72);
                using SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(136 + num13, 136 + num14, 136 + num15));
                graphics_0.FillEllipse(brush, rectangle_5[i]);
            }
        }

        private void method_104(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
            }
            if (rectangle_5.Length > 0)
            {
                graphics_0.FillRectangles(brush_0, rectangle_5);
            }
        }

        private void method_105(Brush brush_0, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, Graphics graphics_0)
        {
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            //if (int_11 > int_11)
            //{
            //    num3 = 0;
            //    num4 = int_11 - int_11;
            //}
            if (num2 < 0)
            {
                num5 = num2 + int_11;
                num6 = int_11;
            }
            //if (int_11 > int_11)
            //{
            //    num5 = 0;
            //    num6 = int_11 - int_11;
            //}
            new System.Drawing.Rectangle(num, num2, int_11 - num, int_11 - num2);
            new System.Drawing.Rectangle(num3, num2, num4 - num3, int_11 - num2);
            new System.Drawing.Rectangle(num, num5, int_11 - num, num6 - num5);
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num7 = starFieldItemList_4[i].X - num;
                int num8 = starFieldItemList_4[i].Y - num2;
                if (num7 > int_11)
                {
                    num7 -= int_11;
                }
                else if (num7 < 0)
                {
                    num7 += int_11;
                }
                if (num8 > int_11)
                {
                    num8 -= int_11;
                }
                else if (num8 < 0)
                {
                    num8 += int_11;
                }
                rectangle_5[i].X = num7;
                rectangle_5[i].Y = num8;
            }
            using ImageAttributes imageAttr = method_220(((SolidBrush)brush_0).Color);
            for (int j = 0; j < rectangle_5.Length; j++)
            {
                int num9 = j % main_0.bitmap_198.Length;
                Bitmap bitmap = main_0.bitmap_198[num9];
                graphics_0.DrawImage(bitmap, rectangle_5[j], 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttr);
            }
        }

        private void method_106(System.Drawing.Color color_18, StarFieldItemList starFieldItemList_4, System.Drawing.Rectangle[] rectangle_5, int int_11, int int_12, SpriteBatch spriteBatch_2)
        {
            int num = (int)((double)main_0.int_13 / main_0.double_0 / (double)int_12) % int_11;
            int num2 = (int)((double)main_0.int_14 / main_0.double_0 / (double)int_12) % int_11;
            int num3 = num - base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num4 = num + base.ClientRectangle.Width / 2 + (int)((double)main_0.int_21 / main_0.double_0);
            int num5 = num2 - base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num6 = num2 + base.ClientRectangle.Width / 2 + (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            if (num3 < 0)
            {
                num7 = num3 + int_11;
                num8 = int_11;
            }
            if (num4 > int_11)
            {
                num7 = 0;
                num8 = num4 - int_11;
            }
            if (num5 < 0)
            {
                num9 = num5 + int_11;
                num10 = int_11;
            }
            if (num6 > int_11)
            {
                num9 = 0;
                num10 = num6 - int_11;
            }
            new System.Drawing.Rectangle(num3, num5, num4 - num3, num6 - num5);
            new System.Drawing.Rectangle(num7, num5, num8 - num7, num6 - num5);
            new System.Drawing.Rectangle(num3, num9, num4 - num3, num10 - num9);
            new System.Drawing.Rectangle(num7, num9, num8 - num7, num10 - num9);
            for (int i = 0; i < starFieldItemList_4.Count; i++)
            {
                int num11 = starFieldItemList_4[i].X - num3;
                int num12 = starFieldItemList_4[i].Y - num5;
                if (num11 > int_11)
                {
                    num11 -= int_11;
                }
                else if (num11 < 0)
                {
                    num11 += int_11;
                }
                if (num12 > int_11)
                {
                    num12 -= int_11;
                }
                else if (num12 < 0)
                {
                    num12 += int_11;
                }
                rectangle_5[i].X = num11;
                rectangle_5[i].Y = num12;
            }
            if (rectangle_5.Length > 0)
            {
                for (int j = 0; j < rectangle_5.Length; j++)
                {
                    XnaDrawingHelper.FillRectangle(spriteBatch_2, rectangle_5[j], color_18);
                }
            }
        }

        private void method_107(int int_11, int int_12, out Texture2D texture2D_61, out Texture2D texture2D_62, out Texture2D texture2D_63)
        {
            texture2D_61 = null;
            texture2D_62 = null;
            texture2D_63 = null;
            method_46();
            using (Bitmap bitmap = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics_ = Graphics.FromImage(bitmap))
                {
                    method_175(graphics_);
                    method_105(solidBrush_9, starFieldItemList_3, rectangle_4, int_4, 38, graphics_);
                }
                texture2D_61 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap, useAlternateBuffer: true);
            }
            using (Bitmap bitmap2 = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb))
            {
                using (Graphics graphics_2 = Graphics.FromImage(bitmap2))
                {
                    method_175(graphics_2);
                    method_105(solidBrush_8, starFieldItemList_2, rectangle_3, int_3, 20, graphics_2);
                }
                texture2D_62 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap2, useAlternateBuffer: true);
            }
            using Bitmap bitmap3 = new Bitmap(int_11, int_12, PixelFormat.Format32bppArgb);
            using (Graphics graphics_3 = Graphics.FromImage(bitmap3))
            {
                method_175(graphics_3);
                method_105(solidBrush_7, starFieldItemList_1, rectangle_2, int_2, 11, graphics_3);
            }
            texture2D_63 = XnaDrawingHelper.FastBitmapToTexture(class1_0.GraphicsDevice, bitmap3, useAlternateBuffer: true);
        }

        private void method_108(SpriteBatch spriteBatch_2)
        {
            float float_ = method_45();
            int num = (int)((double)main_0.int_21 / main_0.double_0);
            int num2 = (int)((double)main_0.vhadzRiecM / main_0.double_0);
            int int_ = num + (int)((double)main_0.int_13 / main_0.double_0 / 38.0) % this.int_4;
            int int_2 = num2 + (int)((double)main_0.int_14 / main_0.double_0 / 38.0) % this.int_4;
            method_110(spriteBatch_2, texture2D_53, int_, int_2, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
            int int_3 = (int)((double)main_0.int_13 / main_0.double_0 / 20.0) % this.int_3;
            int int_4 = (int)((double)main_0.int_14 / main_0.double_0 / 20.0) % this.int_3;
            method_110(spriteBatch_2, texture2D_54, int_3, int_4, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
            int int_5 = (int)((double)main_0.int_13 / main_0.double_0 / 11.0) % this.int_2;
            int int_6 = (int)((double)main_0.int_14 / main_0.double_0 / 11.0) % this.int_2;
            method_110(spriteBatch_2, texture2D_55, int_5, int_6, base.ClientRectangle.Width, base.ClientRectangle.Height, float_);
        }

        private void method_109(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14)
        {
            method_110(spriteBatch_2, texture2D_61, int_11, int_12, int_13, int_14, 1f);
        }

        private void method_110(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14, float float_0)
        {
            if (texture2D_61 == null || texture2D_61.IsDisposed)
            {
                return;
            }
            int num = 0;
            int num2 = 0;
            int val = Math.Min(int_13 - int_11, texture2D_61.Width - int_11);
            int val2 = Math.Min(int_14 - int_12, texture2D_61.Height - int_12);
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color(1f, 1f, 1f, float_0);
            int num3 = int_11;
            while (num < int_13)
            {
                int num4 = int_12;
                while (num2 < int_14)
                {
                    Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(num3, num4, val, val2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(num, num2, value.Width, value.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle, value, color);
                    num2 += Math.Max(1, val2);
                    num4 = 0;
                    val2 = Math.Min(int_14 - num2, texture2D_61.Height);
                }
                num2 = 0;
                num += Math.Max(1, val);
                num3 = 0;
                val = Math.Min(int_13 - num, texture2D_61.Width);
                val2 = Math.Min(int_14 - 0, texture2D_61.Height - int_12);
            }
        }

        private void method_111(SpriteBatch spriteBatch_2, Texture2D texture2D_61, int int_11, int int_12, int int_13, int int_14)
        {
            if (texture2D_61 == null || texture2D_61.IsDisposed)
            {
                return;
            }
            int num = int_11 + int_13;
            int num2 = int_12 + int_14;
            int num3 = Math.Min(texture2D_61.Width - int_11, num);
            int num4 = Math.Min(texture2D_61.Height - int_12, num2);
            Microsoft.Xna.Framework.Rectangle value = new Microsoft.Xna.Framework.Rectangle(int_11, int_12, num3, num4);
            Microsoft.Xna.Framework.Rectangle destinationRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, value.Width, value.Height);
            spriteBatch_2.Draw(texture2D_61, destinationRectangle, value, Microsoft.Xna.Framework.Color.White);
            num -= texture2D_61.Width;
            num2 -= texture2D_61.Height;
            if (num > 0)
            {
                Microsoft.Xna.Framework.Rectangle value2 = new Microsoft.Xna.Framework.Rectangle(0, int_12, num, num4);
                Microsoft.Xna.Framework.Rectangle destinationRectangle2 = new Microsoft.Xna.Framework.Rectangle(value.Width, 0, value2.Width, value2.Height);
                spriteBatch_2.Draw(texture2D_61, destinationRectangle2, value2, Microsoft.Xna.Framework.Color.White);
                if (num2 > 0)
                {
                    Microsoft.Xna.Framework.Rectangle value3 = new Microsoft.Xna.Framework.Rectangle(int_11, 0, num3, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle3 = new Microsoft.Xna.Framework.Rectangle(0, value.Height, value3.Width, value3.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle3, value3, Microsoft.Xna.Framework.Color.White);
                    Microsoft.Xna.Framework.Rectangle value4 = new Microsoft.Xna.Framework.Rectangle(0, 0, num, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle4 = new Microsoft.Xna.Framework.Rectangle(value.Width, value.Height, value4.Width, value4.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle4, value4, Microsoft.Xna.Framework.Color.White);
                }
            }
            else if (num2 > 0)
            {
                Microsoft.Xna.Framework.Rectangle value5 = new Microsoft.Xna.Framework.Rectangle(int_11, 0, num3, num2);
                Microsoft.Xna.Framework.Rectangle destinationRectangle5 = new Microsoft.Xna.Framework.Rectangle(0, value.Height, value5.Width, value5.Height);
                spriteBatch_2.Draw(texture2D_61, destinationRectangle5, value5, Microsoft.Xna.Framework.Color.White);
                if (num > 0)
                {
                    Microsoft.Xna.Framework.Rectangle value6 = new Microsoft.Xna.Framework.Rectangle(0, int_12, num, num4);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle6 = new Microsoft.Xna.Framework.Rectangle(value.Width, 0, value6.Width, value6.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle6, value6, Microsoft.Xna.Framework.Color.White);
                    Microsoft.Xna.Framework.Rectangle value7 = new Microsoft.Xna.Framework.Rectangle(0, 0, num, num2);
                    Microsoft.Xna.Framework.Rectangle destinationRectangle7 = new Microsoft.Xna.Framework.Rectangle(value.Width, value.Height, value7.Width, value7.Height);
                    spriteBatch_2.Draw(texture2D_61, destinationRectangle7, value7, Microsoft.Xna.Framework.Color.White);
                }
            }
        }

        private void method_112(Creature creature_0, Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, Bitmap[] bitmap_8, DateTime dateTime_5, int int_15, double double_15)
        {
            Bitmap[] array = bitmap_7;
            int num = 0;
            if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && bitmap_8 != null && bitmap_8.Length > 0)
            {
                array = bitmap_8;
                num = 5;
            }
            int num2 = (int)((double)array.Length / (double)int_15 * 1000.0);
            int num3 = Math.Max(1, array.Length - 1);
            int num4 = num2 / num3;
            long num5 = dateTime_5.Ticks / 10000L;
            int num6 = (int)(num5 % num2);
            int num7 = num6 / num4;
            Bitmap bitmap_9 = array[num7];
            bitmap_9 = main_0.method_108(creature_0, bitmap_9, main_0.bitmap_11[creature_0.PictureRef + num][num7]);
            float num8 = bitmap_9.Width;
            float num9 = bitmap_9.Height;
            Bitmap bitmap = null;
            bitmap = ((double_15 == 0.0) ? new Bitmap(bitmap_9) : method_218(bitmap_9, (float)double_15, GraphicsQuality.Medium));
            if (creature_0.Damage > 0.0)
            {
                method_21(bitmap_9);
            }
            float num10 = (float)int_13 / num8;
            float num11 = (float)bitmap.Width * num10;
            float num12 = (float)bitmap.Height * num10;
            float num13 = (num11 - num8) / 2f;
            float num14 = (num12 - num9) / 2f;
            num13 *= num10;
            num14 *= num10;
            RectangleF destRect = new RectangleF(int_11, int_12, num11, num12);
            RectangleF srcRect = new RectangleF(0f, 0f, bitmap.Width, bitmap.Height);
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
            graphics_0.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void method_113(Creature creature_0, SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D[] texture2D_61, Texture2D[] texture2D_62, DateTime dateTime_5, int int_15, double double_15)
        {
            if (creature_0 != null && texture2D_61 != null)
            {
                Texture2D[] array = texture2D_61;
                if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && texture2D_62 != null && texture2D_62.Length > 0)
                {
                    array = texture2D_62;
                }
                int num = (int)((double)array.Length / (double)int_15 * 1000.0);
                int num2 = Math.Max(1, array.Length - 1);
                int num3 = num / num2;
                long num4 = dateTime_5.Ticks / 10000L;
                int num5 = (int)(num4 % num);
                int num6 = num5 / num3;
                Texture2D texture = array[num6];
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture, destination, (float)double_15);
            }
        }

        private void method_114(Creature creature_0, SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, Bitmap[] bitmap_8, DateTime dateTime_5, int int_15, double double_15)
        {
            if (creature_0 == null || bitmap_7 == null)
            {
                return;
            }
            Bitmap[] array = bitmap_7;
            int num = 0;
            if (creature_0.CurrentTarget != null && creature_0.DistanceToTarget <= 40.0 && bitmap_8 != null && bitmap_8.Length > 0)
            {
                array = bitmap_8;
                num = 5;
            }
            int num2 = (int)((double)array.Length / (double)int_15 * 1000.0);
            int num3 = Math.Max(1, array.Length - 1);
            int num4 = num2 / num3;
            long num5 = dateTime_5.Ticks / 10000L;
            int num6 = (int)(num5 % num2);
            int num7 = num6 / num4;
            Bitmap bitmap = array[num7];
            Texture2D texture2D = null;
            if (bitmap != null && bitmap.PixelFormat != 0)
            {
                bool flag = false;
                if (creature_0.Damage > 0.0)
                {
                    bitmap = main_0.method_108(creature_0, bitmap, main_0.bitmap_11[creature_0.PictureRef + num][num7]);
                    flag = true;
                }
                texture2D = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap);
                if (flag)
                {
                    method_21(bitmap);
                }
            }
            if (texture2D != null)
            {
                System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, (float)double_15);
                method_22(texture2D);
            }
        }

        private void method_115(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7)
        {
            method_116(graphics_0, int_11, int_12, int_13, int_14, bitmap_7, DateTime.Now, int_6, 0.0, 0.0, System.Drawing.Color.Empty);
        }

        private void method_116(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap[] bitmap_7, DateTime dateTime_5, int int_15, double double_15, double double_16, System.Drawing.Color color_18)
        {
            int num = (int)((double)bitmap_7.Length / (double)int_15 * 1000.0);
            int num2 = Math.Max(1, bitmap_7.Length - 1);
            int num3 = num / num2;
            long num4 = dateTime_5.Ticks / 10000L;
            int num5 = (int)(num4 % num);
            int num6 = num5 / num3;
            Bitmap bitmap = bitmap_7[num6];
            ImageAttributes imageAttributes = null;
            if (color_18 != System.Drawing.Color.Empty)
            {
                imageAttributes = method_221(color_18);
            }
            float num7 = bitmap.Width;
            float num8 = bitmap.Height;
            Bitmap bitmap2 = null;
            if (double_15 != 0.0)
            {
                bitmap2 = method_217(bitmap, (float)double_15);
            }
            else if (double_16 != 0.0)
            {
                double num9 = double_16 / (1000.0 / (double)num3);
                double num10 = (double)num6 * num9;
                bitmap2 = method_217(bitmap, (float)num10);
            }
            else
            {
                bitmap2 = new Bitmap(bitmap);
            }
            float num11 = ((float)bitmap2.Width - num7) / 2f;
            float num12 = ((float)bitmap2.Height - num8) / 2f;
            RectangleF srcRect = new RectangleF(num11, num12, num7, num8);
            RectangleF destRect = new RectangleF(int_11, int_12, int_13, int_14);
            graphics_0.InterpolationMode = InterpolationMode.Low;
            graphics_0.SmoothingMode = SmoothingMode.None;
            graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
            if (imageAttributes == null)
            {
                graphics_0.DrawImage(bitmap2, destRect, srcRect, GraphicsUnit.Pixel);
            }
            else
            {
                System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle((int)destRect.X, (int)destRect.Y, (int)destRect.Width, (int)destRect.Height);
                graphics_0.DrawImage(bitmap2, destRect2, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
            }
            if (bitmap2 != null)
            {
                method_21(bitmap2);
            }
            graphics_0.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            graphics_0.CompositingQuality = CompositingQuality.HighQuality;
        }

        private void method_117(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D[] texture2D_61, DateTime dateTime_5, int int_15, double double_15, double double_16, System.Drawing.Color color_18)
        {
            int num = (int)((double)texture2D_61.Length / (double)int_15 * 1000.0);
            int num2 = Math.Max(1, texture2D_61.Length - 1);
            int num3 = num / num2;
            long num4 = dateTime_5.Ticks / 10000L;
            int num5 = (int)(num4 % num);
            int num6 = num5 / num3;
            Texture2D texture2D = texture2D_61[num6];
            _ = (float)int_13 / (float)texture2D.Width;
            float rotationAngle = (float)double_15;
            if (double_16 != 0.0)
            {
                double num7 = double_16 / (1000.0 / (double)num3);
                rotationAngle = (float)((double)num6 * num7);
            }
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
            if (color_18 == System.Drawing.Color.Empty)
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, rotationAngle);
            }
            else
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D, destination, rotationAngle, color_18);
            }
        }

        private void method_118(SpriteBatch spriteBatch_2, int int_11, int int_12, int int_13, int int_14, Texture2D texture2D_61, DateTime dateTime_5, double double_15, System.Drawing.Color color_18)
        {
            System.Drawing.Rectangle destination = new System.Drawing.Rectangle(int_11, int_12, int_13, int_14);
            if (color_18 == System.Drawing.Color.Empty)
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, destination, (float)double_15);
            }
            else
            {
                XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, destination, (float)double_15, color_18);
            }
        }

        private void method_119(Graphics graphics_0, int int_11, int int_12, int int_13, int int_14, Bitmap bitmap_7, DateTime dateTime_5, double double_15, System.Drawing.Color color_18)
        {
            ImageAttributes imageAttributes = null;
            if (color_18 != System.Drawing.Color.Empty)
            {
                imageAttributes = method_219(color_18);
            }
            float num = bitmap_7.Width;
            float num2 = bitmap_7.Height;
            Bitmap bitmap = null;
            bitmap = ((double_15 == 0.0) ? new Bitmap(bitmap_7) : method_217(bitmap_7, (float)double_15));
            float num3 = ((float)bitmap.Width - num) / 2f;
            float num4 = ((float)bitmap.Height - num2) / 2f;
            RectangleF srcRect = new RectangleF(num3, num4, num, num2);
            RectangleF destRect = new RectangleF(int_11, int_12, int_13, int_14);
            if (imageAttributes == null)
            {
                graphics_0.DrawImage(bitmap, destRect, srcRect, GraphicsUnit.Pixel);
            }
            else
            {
                System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle((int)destRect.X, (int)destRect.Y, (int)destRect.Width, (int)destRect.Height);
                graphics_0.DrawImage(bitmap, destRect2, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, imageAttributes);
                imageAttributes.Dispose();
            }
            if (bitmap != null)
            {
                method_21(bitmap);
            }
        }

        public void PrepareGalaxyBackdrop()
        {
            if (main_0.bool_23)
            {
                return;
            }
            float float_ = 2f;
            RectangleF rectangleF_ = method_126(1);
            RectangleF rect = method_123(rectangleF_, 1.2f);
            float num = main_0.rectangleF_0.Width / 3f;
            if (main_0.rectangleF_0.Contains(rect) && !(rectangleF_.Width < num) && main_0.bitmap_179 != null && main_0.bitmap_179.PixelFormat != 0)
            {
                return;
            }
            lock (main_0.object_4)
            {
                RectangleF rectangleF_2 = method_123(rectangleF_, float_);
                if (!(rectangleF_2.Width > (float)main_0.bitmap_176.Width) && rectangleF_2.Width <= (float)base.Width)
                {
                    main_0.bool_12 = false;
                    RectangleF srcRect = method_124(rectangleF_2, new System.Drawing.Rectangle(0, 0, main_0.bitmap_176.Width, main_0.bitmap_176.Height));
                    main_0.rectangleF_0 = rectangleF_2;
                    float num2 = rectangleF_2.Width;
                    float num3 = rectangleF_2.Height;
                    _ = num2 / rectangleF_2.Width;
                    _ = num3 / rectangleF_2.Height;
                    float num4 = 0f;
                    float num5 = 0f;
                    float num6 = rectangleF_2.Width;
                    float num7 = rectangleF_2.Height;
                    if (rectangleF_2.X < 0f)
                    {
                        num4 = rectangleF_2.X * -1f;
                        num6 -= num4;
                    }
                    if (rectangleF_2.Y < 0f)
                    {
                        num5 = rectangleF_2.Y * -1f;
                        num7 -= num5;
                    }
                    RectangleF destRect = new RectangleF(num4, num5, num6, num7);
                    num2 = Math.Max(1f, num2);
                    num3 = Math.Max(1f, num3);
                    Bitmap bitmap = new Bitmap((int)num2, (int)num3, PixelFormat.Format32bppPArgb);
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                            graphics.SmoothingMode = SmoothingMode.None;
                        }
                        else
                        {
                            graphics.CompositingQuality = CompositingQuality.HighSpeed;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.None;
                        }
                        graphics.DrawImage(main_0.bitmap_178, destRect, srcRect, GraphicsUnit.Pixel);
                        if (main_0.ZoomStatus == ZoomStatus.Stabilizing || main_0.ZoomStatus == ZoomStatus.Stable || (bool_11 && !main_0.gameOptions_0.CleanGalaxyView))
                        {
                            using ImageAttributes imageAttr = method_236(0.25);
                            RectangleF rectangleF_3 = method_125();
                            RectangleF rectangleF = method_123(rectangleF_3, float_);
                            System.Drawing.Rectangle galaxySection = new System.Drawing.Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height);
                            double systemInfluenceSizeFactor = 1.0 + main_0.double_0 / 10000.0;
                            Bitmap bitmap2 = null;
                            if (main_0.gameOptions_0.MapOverlayEmpireTerritory)
                            {
                                bitmap2 = EmpireTerritory.CalculateEmpireTerritoryGrid(main_0._Game.Galaxy, galaxySection, (int)num2, (int)num3, galaxy_0.PlayerEmpire, main_0._Game.GodMode);
                                bitmap2 = GraphicsHelper.SmoothImage(bitmap2);
                            }
                            else
                            {
                                bitmap2 = EmpireTerritory.CalculateEmpireSystemTerritory(main_0._Game.Galaxy, galaxySection, (int)num2, (int)num3, galaxy_0.PlayerEmpire, main_0._Game.GodMode, bitmap_4, systemInfluenceSizeFactor);
                            }
                            System.Drawing.Rectangle destRect2 = new System.Drawing.Rectangle(0, 0, (int)num2, (int)num3);
                            graphics.DrawImage(bitmap2, destRect2, 0, 0, bitmap2.Width, bitmap2.Height, GraphicsUnit.Pixel, imageAttr);
                            method_21(bitmap2);
                        }
                        if (main_0.gameOptions_0.MapOverlayLongRangeScanners && !main_0.bool_23)
                        {
                            bool flag = false;
                            Bitmap image = new Bitmap((int)num2, (int)num3, PixelFormat.Format32bppPArgb);
                            using (Graphics graphics2 = Graphics.FromImage(image))
                            {
                                method_175(graphics2);
                                graphics2.Clear(System.Drawing.Color.Transparent);
                                RectangleF rectangleF2 = method_123(method_125(), float_);
                                float num8 = rectangleF2.Width / rectangleF_2.Width;
                                Empire playerEmpire = galaxy_0.PlayerEmpire;
                                for (int i = 0; i < playerEmpire.LongRangeScanners.Count; i++)
                                {
                                    BuiltObject builtObject = playerEmpire.LongRangeScanners[i];
                                    if (builtObject == null)
                                    {
                                        continue;
                                    }
                                    int sensorLongRange = builtObject.SensorLongRange;
                                    if (builtObject.Role != BuiltObjectRole.Base && builtObject.CurrentSpeed != 0f)
                                    {
                                        continue;
                                    }
                                    int num9 = (int)builtObject.Xpos - sensorLongRange;
                                    int num10 = (int)builtObject.Xpos + sensorLongRange;
                                    int num11 = (int)builtObject.Ypos - sensorLongRange;
                                    int num12 = (int)builtObject.Ypos + sensorLongRange;
                                    RectangleF rect2 = new RectangleF(num9, num11, sensorLongRange * 2, sensorLongRange * 2);
                                    if (!rectangleF2.IntersectsWith(rect2))
                                    {
                                        continue;
                                    }
                                    flag = true;
                                    float num13 = (float)(((double)num9 - (double)rectangleF2.Left) / (double)num8);
                                    float num14 = (float)(((double)num10 - (double)rectangleF2.Left) / (double)num8);
                                    float num15 = (float)(((double)num11 - (double)rectangleF2.Top) / (double)num8);
                                    float num16 = (float)(((double)num12 - (double)rectangleF2.Top) / (double)num8);
                                    RectangleF rectangleF3 = new RectangleF(num13, num15, num14 - num13, num16 - num15);
                                    rectangleF3.Inflate(rectangleF3.Width * 0.1f, rectangleF3.Width * 0.1f);
                                    System.Drawing.Color white = System.Drawing.Color.White;
                                    using ImageAttributes imageAttr2 = method_221(white);
                                    if (!main_0.bool_23)
                                    {
                                        graphics2.DrawImage(main_0.bitmap_188, new System.Drawing.Rectangle((int)rectangleF3.X, (int)rectangleF3.Y, (int)rectangleF3.Width, (int)rectangleF3.Height), 0, 0, main_0.bitmap_188.Width, main_0.bitmap_188.Height, GraphicsUnit.Pixel, imageAttr2);
                                    }
                                }
                            }
                            if (flag && !main_0.bool_23)
                            {
                                using ImageAttributes imageAttr3 = method_236(0.13);
                                method_175(graphics);
                                graphics.DrawImage(image, new System.Drawing.Rectangle(0, 0, (int)num2, (int)num3), 0, 0, (int)num2, (int)num3, GraphicsUnit.Pixel, imageAttr3);
                            }
                        }
                    }
                    Bitmap bitmap_ = main_0.bitmap_179;
                    main_0.bitmap_179 = bitmap;
                    method_21(bitmap_);
                    Texture2D texture2D_ = main_0.texture2D_2;
                    main_0.texture2D_2 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_179);
                    method_22(texture2D_);
                    FadeGalaxyBackground();
                }
                else
                {
                    main_0.bool_12 = true;
                }
            }
        }

        private System.Drawing.Color method_120(Bitmap bitmap_7)
        {
            int int_ = bitmap_7.Width / 2;
            int int_2 = bitmap_7.Height / 2;
            return method_121(bitmap_7, int_, int_2);
        }

        private System.Drawing.Color method_121(Bitmap bitmap_7, int int_11, int int_12)
        {
            return bitmap_7.GetPixel(int_11, int_12);
        }

        private System.Drawing.Color method_122(double double_15, double double_16)
        {
            System.Drawing.Color result = System.Drawing.Color.Empty;
            if (main_0.bitmap_182 != null && main_0.bitmap_182.Width > 0 && main_0.bitmap_182.Height > 0)
            {
                double num = (double)Galaxy.SizeX / (double)main_0.bitmap_182.Width;
                int val = (int)(double_15 / num);
                int val2 = (int)(double_16 / num);
                val = Math.Min(main_0.bitmap_182.Width - 1, Math.Max(val, 0));
                val2 = Math.Min(main_0.bitmap_182.Height - 1, Math.Max(val2, 0));
                System.Drawing.Color pixel = main_0.bitmap_182.GetPixel(val, val2);
                if (pixel.R != 0 && pixel.G != 0 && pixel.B != 0)
                {
                    result = pixel;
                }
                int red = Math.Min(255, result.R * 2);
                int green = Math.Min(255, result.G * 2);
                int blue = Math.Min(255, result.B * 2);
                result = System.Drawing.Color.FromArgb(red, green, blue);
            }
            return result;
        }

        public void FadeGalaxyNebulae()
        {
            if (main_0.double_0 < 210.0 && main_0.double_0 >= 70.0 && main_0.bitmap_183 != null)
            {
                float val = (float)((main_0.double_0 - 70.0) / 210.0);
                val = Math.Min(Math.Max(val, 0f), 1f);
                main_0.bitmap_184 = method_16(main_0.bitmap_183, val);
            }
            else
            {
                main_0.bitmap_184 = main_0.bitmap_183;
            }
        }

        public void FadeSectorBackground()
        {
            Bitmap bitmap = null;
            if (main_0.double_0 < 210.0 && main_0.double_0 >= 70.0 && main_0.bitmap_185 != null && main_0.bitmap_185.PixelFormat != 0)
            {
                float val = (float)((main_0.double_0 - 70.0) / 210.0);
                val = Math.Min(Math.Max(val, 0f), 1f);
                bitmap = method_16(main_0.bitmap_185, val);
            }
            else
            {
                bitmap = main_0.bitmap_185;
            }
            Bitmap bitmap_ = main_0.bitmap_186;
            main_0.bitmap_186 = bitmap;
            method_21(bitmap_);
            if (bool_11 && bool_12 && GraphicsDevice != null)
            {
                Texture2D texture2D = main_0.texture2D_4;
                main_0.texture2D_4 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, main_0.bitmap_186);
                if (texture2D != null && !texture2D.IsDisposed)
                {
                    method_22(texture2D);
                }
            }
        }

        public void FadeGalaxyBackground()
        {
            if (!bool_11)
            {
                Bitmap bitmap = null;
                if (main_0.double_0 < 350.0 && main_0.double_0 >= 70.0 && main_0.bitmap_179 != null && main_0.bitmap_179.PixelFormat != 0)
                {
                    float val = (float)((main_0.double_0 - 70.0) / 210.0);
                    val = Math.Min(Math.Max(val, 0f), 1f);
                    bitmap = method_16(main_0.bitmap_179, val);
                }
                else
                {
                    bitmap = main_0.bitmap_179;
                }
                Bitmap fyKcynWgNv = main_0.FyKcynWgNv;
                main_0.FyKcynWgNv = bitmap;
                method_21(fyKcynWgNv);
            }
        }

        private RectangleF method_123(RectangleF rectangleF_1, float float_0)
        {
            float num = rectangleF_1.Width * float_0;
            float num2 = rectangleF_1.Height * float_0;
            float num3 = rectangleF_1.X - (num - rectangleF_1.Width) / 2f;
            float num4 = rectangleF_1.Y - (num2 - rectangleF_1.Height) / 2f;
            return new RectangleF(num3, num4, num, num2);
        }

        private RectangleF method_124(RectangleF rectangleF_1, RectangleF rectangleF_2)
        {
            if (rectangleF_1.X < rectangleF_2.X)
            {
                float num = rectangleF_2.X - rectangleF_1.X;
                rectangleF_1.Width -= num;
                rectangleF_1.X = rectangleF_2.X;
            }
            if (rectangleF_1.Y < rectangleF_2.Y)
            {
                float num2 = rectangleF_2.Y - rectangleF_1.Y;
                rectangleF_1.Height -= num2;
                rectangleF_1.Y = rectangleF_2.Y;
            }
            if (rectangleF_1.Width > rectangleF_2.Width)
            {
                _ = rectangleF_1.Width;
                _ = rectangleF_2.Width;
                rectangleF_1.Width = rectangleF_2.Width;
            }
            if (rectangleF_1.Height > rectangleF_2.Height)
            {
                _ = rectangleF_1.Height;
                _ = rectangleF_2.Height;
                rectangleF_1.Height = rectangleF_2.Height;
            }
            return rectangleF_1;
        }

        private RectangleF method_125()
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            return new RectangleF(num3, num4, num, num2);
        }

        private RectangleF method_126(int int_11)
        {
            return method_127(int_11, main_0.bitmap_176.Width);
        }

        private RectangleF method_127(int int_11, int int_12)
        {
            RectangleF rectangleF = method_125();
            float num = (float)int_12 / (float)int_11;
            float num2 = num / (float)Galaxy.SizeX;
            float num3 = rectangleF.Width * num2;
            float num4 = rectangleF.Height * num2;
            float num5 = rectangleF.X * num2;
            float num6 = rectangleF.Y * num2;
            return new RectangleF(num5, num6, num3, num4);
        }

        private System.Drawing.Rectangle method_128()
        {
            RectangleF rectangleF = method_125();
            float num = rectangleF.Left / (float)Galaxy.SizeX;
            float num2 = rectangleF.Width / (float)Galaxy.SizeX;
            float num3 = rectangleF.Top / (float)Galaxy.SizeY;
            float num4 = rectangleF.Height / (float)Galaxy.SizeY;
            RectangleF rectangleF2 = new RectangleF(num, num3, num2, num4);
            float num5 = (float)base.ClientRectangle.Width * (float)main_0.double_0;
            float num6 = (float)Galaxy.SizeX / num5 * (float)base.ClientRectangle.Width;
            int num7 = 0;
            int num8 = base.ClientRectangle.Right;
            int num9 = 0;
            int num10 = base.ClientRectangle.Bottom;
            if (rectangleF2.Left < 0f)
            {
                num7 = (int)(Math.Abs(rectangleF2.Left) * num6);
            }
            if (rectangleF2.Right > 1f)
            {
                num8 = (int)((float)base.ClientRectangle.Width - (rectangleF2.Right - 1f) * num6);
            }
            if (rectangleF2.Top < 0f)
            {
                num9 = (int)(Math.Abs(rectangleF2.Top) * num6);
            }
            if (rectangleF2.Bottom > 1f)
            {
                num10 = (int)((float)base.ClientRectangle.Height - (rectangleF2.Bottom - 1f) * num6);
            }
            int num11 = num8 - num7;
            int num12 = num10 - num9;
            return new System.Drawing.Rectangle(num7, num9, num11, num12);
        }

        private System.Drawing.Rectangle method_129(Texture2D texture2D_61)
        {
            RectangleF rectangleF = method_125();
            float num = rectangleF.Left / (float)Galaxy.SizeX;
            float num2 = rectangleF.Width / (float)Galaxy.SizeX;
            float num3 = rectangleF.Top / (float)Galaxy.SizeY;
            float num4 = rectangleF.Height / (float)Galaxy.SizeY;
            RectangleF rectangleF2 = new RectangleF(num, num3, num2, num4);
            float num5 = (float)base.ClientRectangle.Width * (float)main_0.double_0;
            _ = num5 / ((float)Galaxy.SizeX / (float)texture2D_61.Width);
            int num6 = 0;
            int num7 = texture2D_61.Width;
            int num8 = 0;
            int num9 = texture2D_61.Height;
            if (rectangleF2.Left > 0f)
            {
                num6 = (int)(rectangleF2.Left * (float)texture2D_61.Width);
            }
            if (rectangleF2.Right < 1f)
            {
                num7 = (int)(rectangleF2.Right * (float)texture2D_61.Width);
            }
            if (rectangleF2.Top > 0f)
            {
                num8 = (int)(rectangleF2.Top * (float)texture2D_61.Height);
            }
            if (rectangleF2.Bottom < 1f)
            {
                num9 = (int)(rectangleF2.Bottom * (float)texture2D_61.Height);
            }
            int num10 = num7 - num6;
            int num11 = num9 - num8;
            return new System.Drawing.Rectangle(num6, num8, num10, num11);
        }

        private RectangleF method_130(int int_11)
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            float float_ = main_0.float_1;
            float num5 = float_ / 2f;
            float num6 = (float)main_0.bitmap_176.Width / (float)int_11;
            num6 *= float_;
            float num7 = num6 / (float)Galaxy.SizeX;
            float num8 = (float)main_0.bitmap_176.Width / (float)int_11;
            num8 *= num5;
            float num9 = num8 / (float)Galaxy.SizeX;
            float num10 = num * num9;
            float num11 = num2 * num9;
            float num12 = num3 * num7;
            float num13 = num4 * num7;
            return new RectangleF(num12, num13, num10, num11);
        }

        private RectangleF method_131(int int_11)
        {
            float num = (float)base.Width * (float)main_0.double_0;
            float num2 = (float)base.Height * (float)main_0.double_0;
            float num3 = (float)main_0.int_13 - num / 2f;
            float num4 = (float)main_0.int_14 - num2 / 2f;
            float ugecxqhvjP = main_0.UgecxqhvjP;
            float num5 = ugecxqhvjP / 2f;
            float num6 = (float)main_0.bitmap_182.Width / (float)int_11;
            num6 *= ugecxqhvjP;
            float num7 = num6 / (float)Galaxy.SizeX;
            float num8 = (float)main_0.bitmap_182.Width / (float)int_11;
            num8 *= num5;
            float num9 = num8 / (float)Galaxy.SizeX;
            float num10 = num * num9;
            float num11 = num2 * num9;
            float num12 = num3 * num7;
            float num13 = num4 * num7;
            return new RectangleF(num12, num13, num10, num11);
        }

        private void method_132(Graphics graphics_0, SpriteBatch spriteBatch_2, Texture2D texture2D_61)
        {
            PrepareGalaxyBackdrop();
            RectangleF srcRect = method_126(1);
            if (main_0.bool_12)
            {
                srcRect = method_126(2);
                if (bool_11)
                {
                    if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                    {
                        method_249(srcRect);
                        System.Drawing.Rectangle source = method_129(texture2D_61);
                        System.Drawing.Rectangle destination = method_128();
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source, destination);
                    }
                    return;
                }
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_0.SmoothingMode = SmoothingMode.None;
                if (main_0.bitmap_177 != null && main_0.bitmap_177.PixelFormat != 0)
                {
                    graphics_0.DrawImage(main_0.bitmap_177, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                }
                method_177(graphics_0);
                return;
            }
            if (main_0.double_0 <= 2500.0)
            {
                if (!bool_11)
                {
                    method_147();
                    srcRect.Offset(main_0.rectangleF_2.X * -1f, main_0.rectangleF_2.Y * -1f);
                }
                if (bool_11)
                {
                    if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                    {
                        System.Drawing.Rectangle source2 = method_129(texture2D_61);
                        System.Drawing.Rectangle destination2 = method_128();
                        XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source2, destination2);
                    }
                    return;
                }
                if (main_0.ZoomStatus != ZoomStatus.Stabilizing && main_0.ZoomStatus != 0)
                {
                    method_175(graphics_0);
                }
                else
                {
                    graphics_0.InterpolationMode = InterpolationMode.Bilinear;
                    graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics_0.SmoothingMode = SmoothingMode.None;
                }
                graphics_0.DrawImage(main_0.bitmap_186, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                method_177(graphics_0);
                return;
            }
            srcRect.Offset(main_0.rectangleF_0.X * -1f, main_0.rectangleF_0.Y * -1f);
            if (bool_11)
            {
                if (bool_12 && spriteBatch_2 != null && texture2D_61 != null && !texture2D_61.IsDisposed)
                {
                    System.Drawing.Rectangle source3 = method_129(texture2D_61);
                    System.Drawing.Rectangle destination3 = method_128();
                    XnaDrawingHelper.DrawTexture(spriteBatch_2, texture2D_61, source3, destination3);
                }
            }
            else
            {
                graphics_0.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics_0.CompositingQuality = CompositingQuality.HighSpeed;
                graphics_0.SmoothingMode = SmoothingMode.None;
                graphics_0.DrawImage(main_0.bitmap_179, base.ClientRectangle, srcRect, GraphicsUnit.Pixel);
                method_177(graphics_0);
            }
        }

        private void method_133()
        {
            RectangleF rectangleF_ = method_174(main_0.bitmap_176, 1.0);
            double double_ = Galaxy.SizeX / 2;
            double double_2 = Galaxy.SizeY / 2;
            RectangleF rectangleF_2 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_, 1.0);
            RectangleF rectangleF_3 = method_174(main_0.bitmap_176, main_0.float_1);
            RectangleF rectangleF_4 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_3, 1.0);
            RectangleF rect = method_123(rectangleF_4, 1.2f);
            if (!main_0.rectangleF_2.Contains(rect) || main_0.bitmap_179 == null || main_0.bool_15)
            {
                float val = ((float)main_0.double_0 - 1200f) / 800f;
                val = Math.Min(1f, Math.Max(0f, val));
                double double_3 = 1.0;
                RectangleF rectangleF_5 = default(RectangleF);
                main_0.bitmap_185 = method_146(1, main_0.bitmap_176, rectangleF_2, Galaxy.SizeX, 500, 200f, 20f, val, main_0.bitmap_176, out double_3, out rectangleF_5, bool_13: false);
                main_0.float_1 = (float)double_3;
                method_174(main_0.bitmap_176, main_0.float_1);
                float num = 0f;
                if (rectangleF_2.X > 0f)
                {
                    num = rectangleF_2.X * main_0.float_1;
                }
                float num2 = 0f;
                if (rectangleF_2.Y > 0f)
                {
                    num2 = rectangleF_2.Y * main_0.float_1;
                }
                float num3 = rectangleF_2.Width * main_0.float_1;
                float num4 = rectangleF_2.Height * main_0.float_1;
                RectangleF rectangleF_6 = new RectangleF(num, num2, num3, num4);
                main_0.rectangleF_2 = rectangleF_6;
                main_0.float_1 = (float)double_3;
                main_0.bool_15 = false;
                FadeSectorBackground();
            }
        }

        private void method_134()
        {
            RectangleF rectangleF_ = method_174(main_0.bitmap_182, 1.0);
            double double_ = Galaxy.SizeX / 2;
            double double_2 = Galaxy.SizeY / 2;
            RectangleF rectangleF_2 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_, 1.0);
            RectangleF rectangleF_3 = method_174(main_0.bitmap_182, main_0.UgecxqhvjP);
            RectangleF rectangleF_4 = method_139(Galaxy.SizeX, double_, double_2, rectangleF_3, 1.0);
            RectangleF rect = method_123(rectangleF_4, 1.2f);
            if (!main_0.rectangleF_1.Contains(rect) || main_0.bitmap_183 == null || main_0.bool_13)
            {
                float val = ((float)main_0.double_0 - 1200f) / 800f;
                val = Math.Min(1f, Math.Max(0f, val));
                double double_3 = 1.0;
                RectangleF rectangleF_5 = default(RectangleF);
                Bitmap bitmap_ = main_0.bitmap_183;
                main_0.bitmap_183 = method_146(1, main_0.bitmap_182, rectangleF_2, Galaxy.SizeX, 500, 200f, 20f, val, main_0.bitmap_182, out double_3, out rectangleF_5, bool_13: true);
                method_21(bitmap_);
                main_0.UgecxqhvjP = (float)double_3;
                method_174(main_0.bitmap_182, main_0.UgecxqhvjP);
                float num = 0f;
                if (rectangleF_2.X > 0f)
                {
                    num = rectangleF_2.X * main_0.UgecxqhvjP;
                }
                float num2 = 0f;
                if (rectangleF_2.Y > 0f)
                {
                    num2 = rectangleF_2.Y * main_0.UgecxqhvjP;
                }
                float num3 = rectangleF_2.Width * main_0.UgecxqhvjP;
                float num4 = rectangleF_2.Height * main_0.UgecxqhvjP;
                RectangleF rectangleF_6 = new RectangleF(num, num2, num3, num4);
                main_0.rectangleF_1 = rectangleF_6;
                main_0.UgecxqhvjP = (float)double_3;
                main_0.bool_13 = false;
                FadeGalaxyNebulae();
            }
        }

        private Bitmap method_135(Habitat habitat_1)
        {
            int num = -1;
            switch (habitat_1.Type)
            {
                case HabitatType.Hydrogen:
                    num = 6;
                    break;
                case HabitatType.Helium:
                    num = 0;
                    break;
                case HabitatType.Argon:
                    num = 2;
                    break;
                case HabitatType.Ammonia:
                    num = 4;
                    break;
                case HabitatType.CarbonDioxide:
                    num = 7;
                    break;
                case HabitatType.Oxygen:
                    num = 1;
                    break;
                case HabitatType.NitrogenOxygen:
                    num = 3;
                    break;
                case HabitatType.Chlorine:
                    num = 5;
                    break;
            }
            int num2 = Math.Min(100, (int)habitat_1.Diameter);
            nebulaCloudGenerator_1.GenerateNebulaBackdrop(habitat_1.HabitatIndex, out var cloudImages, out var _, 255, num + 20, num2, num2, transparentBackground: true, isGasCloud: true, useLowQuality: false);
            sectorCloudGenerator_0 = new SectorCloudGenerator(habitat_1.HabitatIndex, 4);
            return cloudImages[0];
        }

        private void method_136(Habitat habitat_1)
        {
            if (bitmap_2 == null)
            {
                bitmap_2 = method_135(habitat_1);
                texture2D_56 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_2);
            }
            RectangleF rectangleF_ = method_174(bitmap_2, 1.0);
            RectangleF rectangleF = method_137(habitat_1, rectangleF_);
            RectangleF rectangleF_2 = method_174(bitmap_2, double_3);
            RectangleF rectangleF_3 = method_137(habitat_1, rectangleF_2);
            RectangleF rectangleF_4 = method_123(rectangleF_3, 1.2f);
            rectangleF_4 = method_143(rectangleF_4, bitmap_2, double_3);
            if (rectangleF == System.Drawing.Rectangle.Empty)
            {
                if (bitmap_3 != null && bitmap_3.PixelFormat != 0)
                {
                    method_21(bitmap_3);
                }
                bitmap_3 = null;
                if (texture2D_57 != null && !texture2D_57.IsDisposed)
                {
                    method_22(texture2D_57);
                }
                texture2D_57 = null;
                rectangleF_0 = System.Drawing.Rectangle.Empty;
                return;
            }
            rectangleF_4 = method_123(rectangleF_4, 0.99f);
            if (bool_6 || !rectangleF_0.Contains(rectangleF_4) || bitmap_3 == null)
            {
                rectangleF = method_123(rectangleF, 2f);
                rectangleF = method_142(rectangleF, bitmap_2);
                RectangleF rectangleF_5 = default(RectangleF);
                bitmap_3 = method_145(habitat_1.HabitatIndex, bitmap_2, rectangleF, habitat_1.Diameter, out double_3, out rectangleF_5, bool_13: true);
                texture2D_57 = XnaDrawingHelper.FastBitmapToTexture(GraphicsDevice, bitmap_3);
                rectangleF_ = method_174(bitmap_2, double_3);
                float num = 0f;
                if (rectangleF.X > 0f)
                {
                    num = rectangleF.X * (float)double_3;
                }
                float num2 = 0f;
                if (rectangleF.Y > 0f)
                {
                    num2 = rectangleF.Y * (float)double_3;
                }
                float num3 = rectangleF.Width * (float)double_3;
                float num4 = rectangleF.Height * (float)double_3;
                RectangleF rectangleF2 = (rectangleF_0 = new RectangleF(num, num2, num3, num4));
                bool_6 = false;
            }
        }

        private RectangleF method_137(Habitat habitat_1, RectangleF rectangleF_1)
        {
            return method_138(habitat_1, rectangleF_1, 1.0);
        }

        private RectangleF method_138(Habitat habitat_1, RectangleF rectangleF_1, double double_15)
        {
            return method_139(habitat_1.Diameter, habitat_1.Xpos, habitat_1.Ypos, rectangleF_1, double_15);
        }

        private RectangleF method_139(int int_11, double double_15, double double_16, RectangleF rectangleF_1, double double_17)
        {
            float num = rectangleF_1.Height / rectangleF_1.Width;
            float num2 = int_11;
            float num3 = num2 * num;
            System.Drawing.Rectangle rectangle = method_34((int)double_15, (int)double_16, (int)num2, (int)num3, main_0.double_0);
            float num4 = rectangle.X;
            float num5 = rectangle.Y;
            float num6 = rectangle.Width;
            float num7 = rectangle.Height;
            float num8 = 0f;
            float num9 = 0f;
            float num10 = num6;
            float num11 = num7;
            if (num4 + num6 > (float)base.Width)
            {
                num10 -= num4 + num6 - (float)base.Width;
            }
            if (num5 + num7 > (float)base.Height)
            {
                num11 -= num5 + num7 - (float)base.Height;
            }
            if (num4 < 0f)
            {
                num6 += num4;
                num10 += num4;
                num4 = 0f - num4;
                num8 = num4;
            }
            if (num5 < 0f)
            {
                num7 += num5;
                num11 += num5;
                num5 = 0f - num5;
                num9 = num5;
            }
            float num12 = (float)int_11 / (float)main_0.double_0 / (rectangleF_1.Width * (float)double_17);
            num8 /= num12;
            num9 /= num12;
            num10 /= num12;
            num11 /= num12;
            RectangleF result = new RectangleF(num8, num9, num10, num11);
            if (num10 <= 0f || num11 <= 0f)
            {
                return System.Drawing.Rectangle.Empty;
            }
            return result;
        }

        private RectangleF method_140(Habitat habitat_1, Bitmap bitmap_7)
        {
            return method_141(habitat_1.Diameter, habitat_1.Xpos, habitat_1.Ypos, bitmap_7);
        }

        private RectangleF method_141(int int_11, double double_15, double double_16, Bitmap bitmap_7)
        {
            double num = (double)bitmap_7.Height / (double)bitmap_7.Width;
            int int_12 = (int)((double)int_11 * num);
            System.Drawing.Rectangle rectangle = method_34((int)double_15, (int)double_16, int_11, int_12, main_0.double_0);
            int num2 = rectangle.X;
            int num3 = rectangle.Y;
            int num4 = rectangle.Width;
            int num5 = rectangle.Height;
            if (num2 + num4 > base.Width)
            {
                num4 -= num2 + num4 - base.Width;
            }
            if (num3 + num5 > base.Height)
            {
                num5 -= num3 + num5 - base.Height;
            }
            if (num2 < 0)
            {
                num4 += num2;
                num2 = 0;
            }
            if (num3 < 0)
            {
                num5 += num3;
                num3 = 0;
            }
            RectangleF result = new RectangleF(num2, num3, num4, num5);
            if (num4 <= 0 || num5 <= 0)
            {
                return System.Drawing.Rectangle.Empty;
            }
            return result;
        }

        private RectangleF method_142(RectangleF rectangleF_1, Bitmap bitmap_7)
        {
            return method_143(rectangleF_1, bitmap_7, 1.0);
        }

        private RectangleF method_143(RectangleF rectangleF_1, Bitmap bitmap_7, double double_15)
        {
            int num = (int)((double)bitmap_7.Width * double_15);
            int num2 = (int)((double)bitmap_7.Height * double_15);
            if (rectangleF_1.Right >= (float)num)
            {
                rectangleF_1.Width -= rectangleF_1.Right - (float)num;
            }
            if (rectangleF_1.Bottom >= (float)num2)
            {
                rectangleF_1.Height -= rectangleF_1.Bottom - (float)num2;
            }
            if (rectangleF_1.X < 0f)
            {
                rectangleF_1.Width += rectangleF_1.X;
                rectangleF_1.X = 0f;
            }
            if (rectangleF_1.Y < 0f)
            {
                rectangleF_1.Height += rectangleF_1.Y;
                rectangleF_1.Y = 0f;
            }
            return rectangleF_1;
        }

        private Bitmap method_144(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, out double double_15, out RectangleF rectangleF_2)
        {
            return method_146(int_11, bitmap_7, rectangleF_1, int_12, 400, 0.1f, 10f, 0f, null, out double_15, out rectangleF_2, bool_13: false);
        }

        private Bitmap method_145(int int_11, Bitmap bitmap_7, RectangleF rectangleF_1, int int_12, out double double_15, out RectangleF rectangleF_2, bool bool_13)
        {
            return method_146(int_11, bitmap_7, rectangleF_1, int_12, 400, 0.1f, 10f, 0f, null, out double_15, out rectangleF_2, bool_13);
        }

    }
}
