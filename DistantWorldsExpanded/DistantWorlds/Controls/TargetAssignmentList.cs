// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.TargetAssignmentList
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class TargetAssignmentList
    {
        public class TargetClickedEventArgs : EventArgs
        {
            public PrioritizedTarget Target;

            public MouseButtons ButtonClicked;

            public TargetClickedEventArgs(PrioritizedTarget target, MouseButtons buttonClicked):base()
            {
                
                Target = target;
                ButtonClicked = buttonClicked;
            }
        }

        public bool Visible;

        private bool bool_0;

        public bool Expanded;

        private EventHandler<TargetClickedEventArgs> eventHandler_0;

        private object object_0;

        private PrioritizedTargetList prioritizedTargetList_0;

        private List<double> list_0;

        private List<Bitmap> list_1;

        private int int_0;

        private int int_1;

        private Main main_0;

        private Empire empire_0;

        private ShipGroup shipGroup_0;

        private DateTime dateTime_0;

        private Rectangle rectangle_0;

        private int int_2;

        //private double double_0;

        private int int_3;

        private float float_0;

        private int int_4;

        private int int_5;

        private PrioritizedTarget prioritizedTarget_0;

        private ShipGroup shipGroup_1;

        private bool bool_1;

        private bool bool_2;

        private bool bool_3;

        private PrioritizedTarget prioritizedTarget_1;

        private double double_1;

        //проверить что переменная не используется
        private ShipGroupList shipGroupList_0;
        //проверить что переменная не используется
        private PrioritizedTarget prioritizedTarget_2;

        private Bitmap[] bitmap_0;

        private Bitmap[] bitmap_1;

        private Bitmap[] bitmap_2;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private Font font_0;

        private Font font_1;

        private Font font_2;

        protected IFontCache _FontCache;

        //private float float_1;

        //private bool bool_4;

        public PrioritizedTargetList Targets => prioritizedTargetList_0;

        public int PageOffset => int_1;

        public int PageSize => int_0;

        public PrioritizedTarget HoveredTarget => prioritizedTarget_0;

        public ShipGroup HoveredFleet => shipGroup_1;

        public event EventHandler<TargetClickedEventArgs> TargetClicked
        {
            add
            {
                EventHandler<TargetClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<TargetClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<TargetClickedEventArgs> value2 = (EventHandler<TargetClickedEventArgs>)Delegate.Combine(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
            remove
            {
                EventHandler<TargetClickedEventArgs> eventHandler = eventHandler_0;
                EventHandler<TargetClickedEventArgs> eventHandler2;
                do
                {
                    eventHandler2 = eventHandler;
                    EventHandler<TargetClickedEventArgs> value2 = (EventHandler<TargetClickedEventArgs>)Delegate.Remove(eventHandler2, value);
                    eventHandler = Interlocked.CompareExchange(ref eventHandler_0, value2, eventHandler2);
                }
                while ((object)eventHandler != eventHandler2);
            }
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
        }

        private void method_0()
        {
            if (_FontCache != null)
            {
                font_0 = _FontCache.GenerateFont(16.67f, isBold: false);
                font_1 = _FontCache.GenerateFont(16.67f, isBold: true);
                font_2 = _FontCache.GenerateFont(22.67f, isBold: true);
            }
        }

        private void method_1()
        {
            method_2(bitmap_1);
            method_2(bitmap_2);
            method_2(bitmap_0);
        }

        private void method_2(Bitmap[] bitmap_3)
        {
            if (bitmap_3 == null)
            {
                return;
            }
            for (int i = 0; i < bitmap_3.Length; i++)
            {
                if (bitmap_3[i] != null)
                {
                    bitmap_3[i].Dispose();
                    bitmap_3[i] = null;
                }
            }
        }

        public void InitializeImages(Bitmap[] builtObjectImages, Bitmap[] habitatImages, Bitmap[] raceImages)
        {
            double num = 0.0;
            int num2 = 0;
            method_1();
            bitmap_2 = new Bitmap[builtObjectImages.Length];
            for (int i = 0; i < builtObjectImages.Length; i++)
            {
                num = (double)builtObjectImages[i].Width / (double)builtObjectImages[i].Height;
                num2 = (int)((double)int_3 * num);
                bitmap_2[i] = method_4(builtObjectImages[i], num2, int_3, float_0);
                bitmap_2[i].RotateFlip(RotateFlipType.Rotate270FlipNone);
            }
            bitmap_1 = new Bitmap[habitatImages.Length];
            for (int j = 0; j < habitatImages.Length; j++)
            {
                num = (double)habitatImages[j].Width / (double)habitatImages[j].Height;
                num2 = (int)((double)int_3 * num);
                bitmap_1[j] = method_4(habitatImages[j], num2, int_3, float_0);
            }
            bitmap_0 = new Bitmap[raceImages.Length];
            for (int k = 0; k < raceImages.Length; k++)
            {
                num = (double)raceImages[k].Width / (double)raceImages[k].Height;
                num2 = (int)((double)int_3 * num);
                bitmap_0[k] = method_4(raceImages[k], num2, int_3, float_0);
            }
        }

        private Bitmap method_3(Bitmap bitmap_3, int int_6, int int_7, float float_2)
        {
            Bitmap bitmap = null;
            double val = (double)int_6 / (double)bitmap_3.Width;
            double val2 = (double)int_7 / (double)bitmap_3.Height;
            double num = Math.Min(val, val2);
            if (num < 1.0)
            {
                int width = (int)((double)bitmap_3.Width * num);
                int height = (int)((double)bitmap_3.Height * num);
                ImageAttributes imageAttr = main_0.method_20(float_2);
                bitmap = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
                using Graphics graphics = Graphics.FromImage(bitmap);
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                Rectangle rectangle = new Rectangle(0, 0, bitmap_3.Width, bitmap_3.Height);
                Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawImage(bitmap_3, destRect, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
                return bitmap;
            }
            return new Bitmap(bitmap_3);
        }

        private Bitmap method_4(Bitmap bitmap_3, int int_6, int int_7, float float_2)
        {
            return method_5(bitmap_3, int_6, int_7, float_2, bool_5: false);
        }

        private Bitmap method_5(Bitmap bitmap_3, int int_6, int int_7, float float_2, bool bool_5)
        {
            if (int_6 < 1)
            {
                int_6 = 1;
            }
            if (int_7 < 1)
            {
                int_7 = 1;
            }
            ImageAttributes imageAttr = main_0.method_20(float_2);
            Bitmap bitmap = new Bitmap(int_6, int_7, PixelFormat.Format32bppPArgb);
            using Graphics graphics = Graphics.FromImage(bitmap);
            if (bool_5)
            {
                graphics.InterpolationMode = InterpolationMode.Bilinear;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.SmoothingMode = SmoothingMode.None;
            }
            else
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
            }
            Rectangle rectangle = new Rectangle(0, 0, bitmap_3.Width, bitmap_3.Height);
            Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            graphics.DrawImage(bitmap_3, destRect, 0, 0, rectangle.Width, rectangle.Height, GraphicsUnit.Pixel, imageAttr);
            return bitmap;
        }

        public void Initialize(Main parentForm, Rectangle area)
        {
            method_0();
            main_0 = parentForm;
            rectangle_0 = area;
            int_2 = 46;
            //double_0 = 20.0;
            int_0 = (area.Height - (int_4 + 5 + int_5)) / int_2;
            Reset();
        }

        public void Reset()
        {
            prioritizedTargetList_0.Clear();
            list_0.Clear();
            if (list_1 == null)
            {
                return;
            }
            for (int i = 0; i < list_1.Count; i++)
            {
                if (list_1[i] != null)
                {
                    list_1[i].Dispose();
                }
            }
            list_1.Clear();
        }

        public bool DetectHoverElement(Point point)
        {
            bool_1 = false;
            bool_3 = false;
            bool_2 = false;
            ShipGroup shipGroup = null;
            PrioritizedTarget prioritizedTarget = null;
            bool flag = false;
            if (rectangle_0.Contains(point))
            {
                _ = point.X;
                _ = rectangle_0.X;
                int num = point.Y - rectangle_0.Y;
                if (num < int_4)
                {
                    bool_1 = true;
                    return true;
                }
                if (point.Y > rectangle_0.Bottom - int_5)
                {
                    int num2 = rectangle_0.X + rectangle_0.Width / 2;
                    if (point.X < num2)
                    {
                        bool_3 = true;
                        flag = true;
                    }
                    else
                    {
                        bool_2 = true;
                        flag = true;
                    }
                }
                else
                {
                    PrioritizedTarget prioritizedTarget_ = null;
                    if (method_6(point, out prioritizedTarget_) && prioritizedTarget_ != null)
                    {
                        prioritizedTarget = prioritizedTarget_;
                        int missionQueueIndex = -1;
                        ShipGroup shipGroup2 = ResolveAssignedFleet(prioritizedTarget_, out missionQueueIndex);
                        if (shipGroup2 != null)
                        {
                            shipGroup = shipGroup2;
                        }
                        flag = true;
                    }
                }
            }
            if (prioritizedTarget != prioritizedTarget_0)
            {
                if (prioritizedTarget_0 != null)
                {
                    if (prioritizedTarget_0.Target is ShipGroup)
                    {
                        ShipGroup shipGroup3 = (ShipGroup)prioritizedTarget_0.Target;
                        main_0.method_245(shipGroup3.LeadShip);
                    }
                    else
                    {
                        main_0.method_245(prioritizedTarget_0.Target);
                    }
                }
                prioritizedTarget_0 = prioritizedTarget;
                if (prioritizedTarget_0 != null)
                {
                    if (prioritizedTarget_0.Target is ShipGroup)
                    {
                        ShipGroup shipGroup4 = (ShipGroup)prioritizedTarget_0.Target;
                        main_0.method_243(shipGroup4.LeadShip);
                    }
                    else
                    {
                        main_0.method_243(prioritizedTarget_0.Target);
                    }
                }
            }
            shipGroup_1 = shipGroup;
            if (flag)
            {
                main_0.string_17 = string.Format(TextResolver.GetText("Enemy Targets: cycle fleets (X key) and click to assign attack"), "F");
            }
            return flag;
        }

        public bool CheckHover(Point point)
        {
            return method_6(point, out prioritizedTarget_0);
        }

        private bool method_6(Point point_0, out PrioritizedTarget prioritizedTarget_3)
        {
            prioritizedTarget_3 = null;
            if (!Visible)
            {
                return false;
            }
            if (rectangle_0.Contains(point_0))
            {
                for (int i = int_1; i < list_0.Count && i < int_1 + int_0; i++)
                {
                    int num = (i - int_1) * int_2;
                    int num2 = num + rectangle_0.Top + int_4 + 5;
                    if (point_0.Y >= num2 && point_0.Y < num2 + (int_2 - 5))
                    {
                        prioritizedTarget_3 = prioritizedTargetList_0[i];
                        return true;
                    }
                }
            }
            if (prioritizedTarget_0 != null)
            {
                if (prioritizedTarget_0.Target is ShipGroup)
                {
                    ShipGroup shipGroup = (ShipGroup)prioritizedTarget_0.Target;
                    main_0.method_245(shipGroup.LeadShip);
                }
                else
                {
                    main_0.method_245(prioritizedTarget_0.Target);
                }
            }
            prioritizedTarget_0 = null;
            return false;
        }

        public bool CheckClick(Point point, MouseButtons buttonClicked)
        {
            if (!Visible)
            {
                return false;
            }
            PrioritizedTarget prioritizedTarget_ = null;
            if (DetectHoverElement(point))
            {
                if (bool_1)
                {
                    Expanded = !Expanded;
                    return true;
                }
                if (bool_3)
                {
                    int_1 -= int_0;
                    int_1 = Math.Max(0, int_1);
                    return true;
                }
                if (bool_2)
                {
                    int_1 += int_0;
                    int num = (int)((double)prioritizedTargetList_0.Count / (double)int_0 - 0.01) * int_0 - 1;
                    int_1 = Math.Min(num + 1, int_1);
                    return true;
                }
            }
            method_6(point, out prioritizedTarget_);
            if ((prioritizedTarget_ != null || shipGroup_1 != null) && eventHandler_0 != null)
            {
                eventHandler_0(this, new TargetClickedEventArgs(prioritizedTarget_, buttonClicked));
                return true;
            }
            return false;
        }

        public void BindTargets(Empire playerEmpire, ShipGroup selectedFleet, PrioritizedTargetList targets, int pageOffset, bool hideAssignedTargets)
        {
            lock (object_0)
            {
                if (prioritizedTargetList_0 != null && prioritizedTargetList_0.CheckEquivalent(targets))
                {
                    return;
                }
                Reset();
                empire_0 = playerEmpire;
                bool_0 = !hideAssignedTargets;
                shipGroup_0 = selectedFleet;
                if (int_1 < targets.Count)
                {
                    Math.Min(targets.Count, int_1 + int_0);
                    for (int i = 0; i < targets.Count; i++)
                    {
                        if (hideAssignedTargets)
                        {
                            int missionQueueIndex = 0;
                            ShipGroup shipGroup = ResolveAssignedFleet(targets[i], out missionQueueIndex);
                            if (shipGroup != null)
                            {
                                continue;
                            }
                        }
                        prioritizedTargetList_0.Add(targets[i]);
                        list_1.Add(method_4(targets[i].Empire.LargeFlagPicture, 50, 30, 0.5f));
                        double item = 0.0;
                        if (list_0 != null && list_0.Count > 0)
                        {
                            item = list_0[list_0.Count - 1] + (double)int_2;
                        }
                        list_0.Add(item);
                        prioritizedTarget_1 = targets[i];
                        double_1 = 1.0;
                    }
                }
                dateTime_0 = main_0._Game.Galaxy.CurrentDateTime;
            }
        }

        public ShipGroup ResolveAssignedFleet(PrioritizedTarget target, out int missionQueueIndex)
        {
            missionQueueIndex = 0;
            int num = 0;
            while (true)
            {
                if (num < empire_0.ShipGroups.Count)
                {
                    if (empire_0.ShipGroups[num].Mission != null && (empire_0.ShipGroups[num].Mission.Type == BuiltObjectMissionType.Attack || empire_0.ShipGroups[num].Mission.Type == BuiltObjectMissionType.Bombard || empire_0.ShipGroups[num].Mission.Type == BuiltObjectMissionType.WaitAndAttack || empire_0.ShipGroups[num].Mission.Type == BuiltObjectMissionType.WaitAndBombard) && empire_0.ShipGroups[num].Mission.Target == target.Target)
                    {
                        break;
                    }
                    if (empire_0.ShipGroups[num].SubsequentMissions != null && empire_0.ShipGroups[num].SubsequentMissions.Count > 0)
                    {
                        for (int i = 0; i < empire_0.ShipGroups[num].SubsequentMissions.Count; i++)
                        {
                            if (empire_0.ShipGroups[num].SubsequentMissions[i] != null && (empire_0.ShipGroups[num].SubsequentMissions[i].Type == BuiltObjectMissionType.Attack || empire_0.ShipGroups[num].SubsequentMissions[i].Type == BuiltObjectMissionType.Bombard || empire_0.ShipGroups[num].SubsequentMissions[i].Type == BuiltObjectMissionType.WaitAndAttack || empire_0.ShipGroups[num].SubsequentMissions[i].Type == BuiltObjectMissionType.WaitAndBombard) && empire_0.ShipGroups[num].SubsequentMissions[i].Target == target.Target)
                            {
                                missionQueueIndex = i + 1;
                                return empire_0.ShipGroups[num];
                            }
                        }
                    }
                    num++;
                    continue;
                }
                return null;
            }
            return empire_0.ShipGroups[num];
        }

        private string method_7(PrioritizedTarget prioritizedTarget_3)
        {
            string text = string.Empty;
            bool flag = false;
            if (prioritizedTarget_3.Target is Habitat)
            {
                Habitat habitat = (Habitat)prioritizedTarget_3.Target;
                if (!(flag = empire_0.IsObjectVisibleToThisEmpire(habitat)))
                {
                    text = text + TextResolver.GetText("Estimated") + ": ";
                }
                int num = 0;
                if (flag)
                {
                    Galaxy.DetermineHabitatSystemStar(habitat);
                    num += main_0._Game.Galaxy.DetermineDefendingFirepower(habitat, habitat.Empire);
                }
                else if (habitat.BasesAtHabitat != null)
                {
                    for (int i = 0; i < habitat.BasesAtHabitat.Count; i++)
                    {
                        num += habitat.BasesAtHabitat[i].FirepowerRaw;
                    }
                }
                text += num.ToString(TextResolver.GetText("firepower format"));
                if (habitat.Troops != null)
                {
                    if (flag)
                    {
                        string text2 = text;
                        text = text2 + ", " + habitat.Troops.Count + " " + TextResolver.GetText("troops");
                    }
                    else
                    {
                        text = text + ", ? " + TextResolver.GetText("troops");
                    }
                }
                if (habitat.DefensiveFortressBonus > 0)
                {
                    text = text + ", " + TextResolver.GetText("Planetary Facility Fortified Bunker");
                }
            }
            else if (prioritizedTarget_3.Target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)prioritizedTarget_3.Target;
                flag = empire_0.IsObjectVisibleToThisEmpire(builtObject);
                int num2 = 0;
                if (flag && builtObject.NearestSystemStar != null)
                {
                    num2 = main_0._Game.Galaxy.DetermineDefendingStrength(builtObject, builtObject.Empire);
                }
                else
                {
                    if (!flag)
                    {
                        text = text + TextResolver.GetText("Estimated") + ": ";
                    }
                    num2 = builtObject.FirepowerRaw;
                }
                text += num2.ToString(TextResolver.GetText("firepower format"));
            }
            else if (prioritizedTarget_3.Target is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)prioritizedTarget_3.Target;
                flag = empire_0.IsObjectVisibleToThisEmpire(shipGroup.LeadShip);
                string text3 = text;
                text = text3 + shipGroup.Ships.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ", " + shipGroup.TotalFirepower.ToString(TextResolver.GetText("firepower format"));
            }
            return text;
        }

        private Bitmap method_8(PrioritizedTarget prioritizedTarget_3)
        {
            Bitmap result = null;
            if (prioritizedTarget_3.Target is Habitat)
            {
                Habitat habitat = (Habitat)prioritizedTarget_3.Target;
                result = bitmap_1[habitat.PictureRef];
            }
            else if (prioritizedTarget_3.Target is BuiltObject)
            {
                BuiltObject builtObject = (BuiltObject)prioritizedTarget_3.Target;
                result = bitmap_2[builtObject.PictureRef];
            }
            else if (prioritizedTarget_3.Target is ShipGroup)
            {
                ShipGroup shipGroup = (ShipGroup)prioritizedTarget_3.Target;
                result = bitmap_2[shipGroup.LeadShip.PictureRef];
            }
            return result;
        }

        private ShipGroup method_9(Point point_0, Rectangle rectangle_1, int int_6, ShipGroupList shipGroupList_1)
        {
            if (rectangle_1.Contains(point_0))
            {
                int num = (point_0.Y - rectangle_1.Top) / int_6 - 1;
                if (num >= 0 && num < shipGroupList_1.Count)
                {
                    return shipGroupList_1[num];
                }
            }
            return null;
        }

        private void method_10(Graphics graphics_0, Point point_0, int int_6, PrioritizedTarget prioritizedTarget_3)
        {
            if (empire_0.ShipGroups == null || empire_0.ShipGroups.Count <= 0)
            {
                return;
            }
            int num = 15;
            ShipGroupList shipGroupList = shipGroupList_0;
            int height = (shipGroupList.Count + 1) * 15;
            Rectangle rectangle = new Rectangle(point_0, new Size(int_6, height));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(192, 0, 0, 0)))
            {
                graphics_0.FillRectangle(brush, rectangle);
            }
            ShipGroup shipGroup = method_9(MouseHelper.GetCursorPosition(), rectangle, num, shipGroupList);
            int x = point_0.X;
            int y = point_0.Y;
            using SolidBrush brush_ = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            method_12(point_0: new Point(x, y), graphics_0: graphics_0, string_0: TextResolver.GetText("Select a fleet below to attack this target (closest fleets listed first)"), font_3: font_0, brush_0: brush_);
            y += num;
            for (int i = 0; i < shipGroupList.Count; i++)
            {
                ShipGroup shipGroup2 = shipGroupList[i];
                Point point = new Point(x, y);
                if (shipGroup2 == shipGroup)
                {
                    using SolidBrush brush2 = new SolidBrush(Color.FromArgb(192, 255, 255, 0));
                    graphics_0.FillRectangle(brush2, new Rectangle(point, new Size(int_6, num)));
                }
                string name = shipGroup2.Name;
                string text = Galaxy.ResolveDescription(empire_0, shipGroup2.Mission);
                name = ((shipGroup2.Mission == null || shipGroup2.Mission.Type == BuiltObjectMissionType.Undefined) ? (name + " (" + TextResolver.GetText("No mission") + ")") : (name + " (" + text + ")"));
                string text2 = shipGroup2.Ships.Count + " " + TextResolver.GetText("Ships").ToLower(CultureInfo.InvariantCulture) + ", ";
                text2 = text2 + shipGroup2.TotalFirepower.ToString(TextResolver.GetText("firepower format")) + ", ";
                string text3 = text2;
                text2 = text3 + shipGroup2.TotalFighterCount + " " + TextResolver.GetText("Fighters").ToLower(CultureInfo.InvariantCulture) + ", ";
                text2 = text2 + shipGroup2.TotalTroopCount + " " + TextResolver.GetText("troops");
                name = name + " - " + text2;
                method_12(graphics_0, name, font_0, point, brush_);
                y += num;
            }
        }

        public void DrawTargets(Graphics graphics)
        {
            if (main_0 == null || main_0._Game == null || empire_0 == null || !Visible)
            {
                return;
            }
            main_0.method_112(graphics, GraphicsQuality.Low);
            graphics.SetClip(new Rectangle(rectangle_0.X, rectangle_0.Y - 3, rectangle_0.Width, rectangle_0.Height + 3));
            Color color = Color.FromArgb(36, 255, 255, 255);
            if (bool_1)
            {
                color = Color.FromArgb(80, 255, 255, 255);
            }
            using (SolidBrush brush = new SolidBrush(color))
            {
                Rectangle rect = new Rectangle(rectangle_0.X, rectangle_0.Y, rectangle_0.Width - 1, int_4);
                graphics.FillRectangle(brush, rect);
                using (Pen pen = new Pen(Color.FromArgb(64, 255, 255, 255)))
                {
                    graphics.DrawRectangle(pen, rect);
                }
                string empty = string.Empty;
                Font font_ = font_0;
                empty = ((prioritizedTargetList_0 == null || prioritizedTargetList_0.Count <= 0) ? (empty + "(" + TextResolver.GetText("No targets") + ")") : (empty + prioritizedTargetList_0.Count + " " + TextResolver.GetText("Enemy Targets")));
                empty = ((!Expanded) ? (empty + "   (" + TextResolver.GetText("click to expand") + ")") : (empty + "   (" + TextResolver.GetText("click to collapse") + ")"));
                method_12(graphics, empty, font_, new Point(rectangle_0.X, rectangle_0.Y), solidBrush_2);
            }
            if (Expanded)
            {
                int num = -1;
                for (int i = int_1; i < prioritizedTargetList_0.Count && i < int_1 + int_0; i++)
                {
                    int num2 = (i - int_1) * int_2;
                    if (prioritizedTargetList_0[i] == prioritizedTarget_2 && shipGroupList_0 != null)
                    {
                        num = rectangle_0.Y + num2 + int_4 + int_2;
                    }
                    int missionQueueIndex = 0;
                    ShipGroup shipGroup = ResolveAssignedFleet(prioritizedTargetList_0[i], out missionQueueIndex);
                    int num3 = 32;
                    if (prioritizedTargetList_0[i] == prioritizedTarget_0)
                    {
                        num3 += 80;
                    }
                    else if (double_1 > 0.0 && prioritizedTargetList_0[i] == prioritizedTarget_1)
                    {
                        double num4 = double_1;
                        if (num4 > 100.0)
                        {
                            num4 = 200.0 - num4;
                        }
                        num3 += (int)(num4 * 1.7);
                    }
                    Rectangle rect2 = new Rectangle(rectangle_0.X, rectangle_0.Y + num2 + int_4 + 5, rectangle_0.Width - 1, int_2 - 5);
                    Color color2 = Color.FromArgb(num3, prioritizedTargetList_0[i].Empire.MainColor);
                    Color color3 = prioritizedTargetList_0[i].Empire.MainColor;
                    if (prioritizedTargetList_0[i].Empire.PirateEmpireBaseHabitat != null)
                    {
                        color2 = Color.FromArgb(num3, 96, 96, 96);
                        color3 = Color.FromArgb(170, 170, 170);
                    }
                    if (shipGroup != null)
                    {
                        color3 = Color.FromArgb(80, color3.R, color3.G, color3.B);
                    }
                    using (SolidBrush brush2 = new SolidBrush(color2))
                    {
                        graphics.FillRectangle(brush2, rect2);
                    }
                    if (prioritizedTargetList_0[i] == prioritizedTarget_0)
                    {
                        int alpha = Math.Min(255, num3 + 64);
                        using Pen pen2 = new Pen(Color.FromArgb(alpha, prioritizedTargetList_0[i].Empire.MainColor));
                        graphics.DrawRectangle(pen2, rect2);
                    }
                    graphics.DrawImage(list_1[i], rect2.X + 5, rect2.Y + 5);
                    Bitmap bitmap = method_8(prioritizedTargetList_0[i]);
                    int num5 = 0;
                    if (bitmap != null)
                    {
                        graphics.DrawImage(bitmap, rect2.X + 59, rect2.Y + 5);
                        num5 = bitmap.Width + 3;
                    }
                    main_0.method_112(graphics, GraphicsQuality.Medium);
                    Point point_ = new Point(rect2.X + 59 + num5, rect2.Y + 3);
                    using (SolidBrush brush_ = new SolidBrush(color3))
                    {
                        SizeF sizeF_ = new SizeF(200f, 20f);
                        string name = prioritizedTargetList_0[i].Name;
                        SizeF sizeF = graphics.MeasureString(name, font_1, 200, StringFormat.GenericTypographic);
                        method_13(graphics, name, font_1, point_, brush_, sizeF_);
                        method_13(point_0: new Point(point_.X + (int)(sizeF.Width + 6f), point_.Y + 1), graphics_0: graphics, string_0: "(" + prioritizedTargetList_0[i].Empire.Name + ")", font_3: font_0, brush_0: brush_, sizeF_0: sizeF_);
                    }
                    Point point = new Point(rect2.X + 59 + num5, rect2.Y + 22);
                    string s = method_7(prioritizedTargetList_0[i]);
                    Color color4 = Color.FromArgb(170, 170, 170);
                    if (shipGroup != null)
                    {
                        color4 = Color.FromArgb(96, 170, 170, 170);
                    }
                    using (SolidBrush brush3 = new SolidBrush(color4))
                    {
                        graphics.DrawString(s, font_0, brush3, point);
                    }
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    if (shipGroup != null)
                    {
                        string text = string.Format(TextResolver.GetText("FLEET attacking"), shipGroup.Name);
                        SizeF sizeF2 = graphics.MeasureString(text, font_2, rectangle_0.Width, StringFormat.GenericTypographic);
                        string text2 = "(" + TextResolver.GetText("right-click to cancel").ToLower(CultureInfo.InvariantCulture) + ")";
                        SizeF sizeF3 = graphics.MeasureString(text2, font_0, rectangle_0.Width, StringFormat.GenericTypographic);
                        sizeF3 = new SizeF(sizeF3.Width, sizeF3.Height - 4f);
                        int num6 = (rect2.Width - (int)sizeF2.Width) / 2;
                        int num7 = (rect2.Height - (int)(sizeF2.Height + sizeF3.Height)) / 2;
                        Point point_3 = new Point(rect2.X + num6, rect2.Y + num7);
                        int num8 = (rect2.Width - (int)sizeF3.Width) / 2;
                        int num9 = num7 + (int)sizeF2.Height - 4;
                        Point point_4 = new Point(rect2.X + num8, rect2.Y + num9);
                        using SolidBrush brush_2 = new SolidBrush(Color.FromArgb(255, 220, 220, 220));
                        method_12(graphics, text, font_2, point_3, brush_2);
                        method_12(graphics, text2, font_0, point_4, brush_2);
                    }
                }
                if (prioritizedTargetList_0 != null && prioritizedTargetList_0.Count > 0 && int_1 >= int_0)
                {
                    Color color5 = Color.FromArgb(36, 255, 255, 255);
                    if (bool_3)
                    {
                        color5 = Color.FromArgb(80, 255, 255, 255);
                    }
                    using SolidBrush brush4 = new SolidBrush(color5);
                    Rectangle rect3 = new Rectangle(rectangle_0.X, rectangle_0.Bottom - int_5, rectangle_0.Width / 2 - 1, int_5 - 1);
                    graphics.FillRectangle(brush4, rect3);
                    using (Pen pen3 = new Pen(Color.FromArgb(64, 255, 255, 255)))
                    {
                        graphics.DrawRectangle(pen3, rect3);
                    }
                    string text3 = string.Empty;
                    if (prioritizedTargetList_0 != null && prioritizedTargetList_0.Count > 0 && int_1 >= int_0)
                    {
                        string text4 = text3;
                        text3 = text4 + "< " + TextResolver.GetText("Previous") + " " + int_0;
                    }
                    method_12(graphics, text3, font_0, new Point(rectangle_0.X, rectangle_0.Bottom - int_5), solidBrush_2);
                }
                int num10 = (int)((double)prioritizedTargetList_0.Count / (double)int_0 - 0.01) * int_0 - 1;
                if (prioritizedTargetList_0 != null && prioritizedTargetList_0.Count > 0 && int_1 <= num10)
                {
                    Color color6 = Color.FromArgb(36, 255, 255, 255);
                    if (bool_2)
                    {
                        color6 = Color.FromArgb(80, 255, 255, 255);
                    }
                    using SolidBrush brush5 = new SolidBrush(color6);
                    Rectangle rect4 = new Rectangle(rectangle_0.X + rectangle_0.Width / 2, rectangle_0.Bottom - int_5, rectangle_0.Width / 2 - 1, int_5 - 1);
                    graphics.FillRectangle(brush5, rect4);
                    using (Pen pen4 = new Pen(Color.FromArgb(64, 255, 255, 255)))
                    {
                        graphics.DrawRectangle(pen4, rect4);
                    }
                    string text5 = string.Empty;
                    if (prioritizedTargetList_0 != null && prioritizedTargetList_0.Count > 0 && int_1 <= num10)
                    {
                        string text6 = text5;
                        text5 = text6 + TextResolver.GetText("Next") + " " + int_0 + " >";
                    }
                    method_12(graphics, text5, font_0, new Point(rectangle_0.X + rectangle_0.Width / 2, rectangle_0.Bottom - int_5), solidBrush_2);
                }
                if (num >= 0)
                {
                    method_10(graphics, new Point(rectangle_0.X, num), rectangle_0.Width, prioritizedTarget_2);
                }
            }
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.SetClip(main_0.ClientRectangle);
            main_0.method_112(graphics, GraphicsQuality.High);
        }

        private void method_11(Graphics graphics_0, string string_0, Font font_3, Point point_0)
        {
            method_12(graphics_0, string_0, font_3, point_0, solidBrush_0);
        }

        private void method_12(Graphics graphics_0, string string_0, Font font_3, Point point_0, Brush brush_0)
        {
            method_13(graphics_0, string_0, font_3, point_0, brush_0, SizeF.Empty);
        }

        private void method_13(Graphics graphics_0, string string_0, Font font_3, Point point_0, Brush brush_0, SizeF sizeF_0)
        {
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(layoutRectangle: new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height), s: string_0, font: font_3, brush: solidBrush_1, format: StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                RectangleF layoutRectangle2 = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_3, brush_0, layoutRectangle2, StringFormat.GenericTypographic);
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_3, solidBrush_1, point_0);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_3, brush_0, point_0);
            }
        }

        public TargetAssignmentList():base()
        {
            
            Visible = true;
            bool_0 = true;
            Expanded = true;
            object_0 = new object();
            prioritizedTargetList_0 = new PrioritizedTargetList();
            list_0 = new List<double>();
            list_1 = new List<Bitmap>();
            int_0 = 5;
            dateTime_0 = DateTime.MinValue;
            rectangle_0 = new Rectangle(10, 100, 200, 50);
            int_2 = 46;
            //double_0 = 20.0;
            int_3 = 30;
            float_0 = 0.6f;
            int_4 = 17;
            int_5 = 17;
            solidBrush_0 = new SolidBrush(Color.White);
            solidBrush_1 = new SolidBrush(Color.Black);
            solidBrush_2 = new SolidBrush(Color.FromArgb(170, 170, 170));
            font_0 = new Font("Verdana", 8f);
            font_1 = new Font("Verdana", 10f, FontStyle.Bold);
            font_2 = new Font("Verdana", 12f, FontStyle.Bold);
        }
    }
}
