// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.ScrollingLinkList
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ScrollingLinkList : GradientPanel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private bool bool_0;

        private Cursor cursor_0;

        private double double_0;

        private Lock object_0;

        private Font font_0;

        private DateTime dateTime_0;

        // Number of LinkLabels (visible rows). The labels are created once and never moved afterwards.
        private const int SlotCount = 4;

        private struct SlotData
        {
            public string Text;
            public object Data;
            public bool Visited;
        }

        private LinkLabel[] slots;                                   // [0] = top row (oldest), [SlotCount-1] = bottom row (newest)
        private int shownCount;                                      // rows in use; they are always the bottom 'shownCount' slots
        private readonly Queue<SlotData> pending = new Queue<SlotData>();   // added, but not yet released to a slot (guarded by object_0)
        private readonly List<SlotData> scratch = new List<SlotData>();
        private double releaseCredit;                                // game seconds collected towards releasing the next pending item
        private bool clearRequested;                                 // ClearItems() was called, slots are reset on the next method_3()
        private int rowPitch;                                        // label height + 1px gap

        private int ctrlName = 0;
        public void ClearData()
        {
            galaxy_0 = null;
            ClearItems();
        }

        private static void smethod_0(Graphics graphics_0, Rectangle rectangle_0, bool bool_2, bool bool_3, bool bool_4, bool bool_5, Color color_0, Color color_1, Color color_2, Color color_3, Color color_4, float float_0, int int_3, bool bool_6, Color color_5, GradientPanel gradientPanel_0)
        {
            SmoothingMode smoothingMode = graphics_0.SmoothingMode;
            graphics_0.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectangle = rectangle_0;
            rectangle.Width--;
            rectangle.Height--;
            using (GraphicsPath path = smethod_3(rectangle, int_3, gradientPanel_0))
            {
                Pen pen;
                if (bool_6)
                {
                    pen = new Pen(Color.Black);
                }
                else
                { pen= new Pen(color_0);}
                graphics_0.DrawPath(pen, path);
                pen.Dispose();
            }
            rectangle.X++;
            rectangle.Y++;
            rectangle.Width -= 2;
            rectangle.Height -= 2;
            Rectangle rectangle2 = rectangle;
            rectangle2.Height >>= 1;
            using (GraphicsPath path2 = smethod_3(rectangle, int_3 - UiScale.S(2), gradientPanel_0))
            {
                using Brush brush = new SolidBrush(color_0);
                graphics_0.FillPath(brush, path2);
            }
            if ((bool_3 || bool_4) && !bool_2)
            {
                using GraphicsPath path3 = smethod_3(rectangle, int_3 - UiScale.S(2), gradientPanel_0);
                graphics_0.SetClip(path3, CombineMode.Intersect);
                using (GraphicsPath graphicsPath = smethod_1(rectangle))
                {
                    using PathGradientBrush pathGradientBrush = new PathGradientBrush(graphicsPath);
                    int alpha = (int)(178f * float_0 + 0.5f);
                    RectangleF bounds = graphicsPath.GetBounds();
                    pathGradientBrush.CenterPoint = new PointF((bounds.Left + bounds.Right) / 2f, (bounds.Top + bounds.Bottom) / 2f);
                    pathGradientBrush.CenterColor = Color.FromArgb(alpha, color_2);
                    pathGradientBrush.SurroundColors = new Color[1] { Color.FromArgb(0, color_2) };
                    graphics_0.FillPath(pathGradientBrush, graphicsPath);
                }
                graphics_0.ResetClip();
            }
            if (rectangle2.Width > 0 && rectangle2.Height > 0)
            {
                rectangle2.Height++;
                using (GraphicsPath path4 = smethod_2(rectangle2, int_3 - UiScale.S(2), gradientPanel_0))
                {
                    rectangle2.Height++;
                    int num = 153;
                    if (bool_2 || !bool_5)
                    {
                        num = (int)(0.4f * (float)num + 0.5f);
                    }
                    using LinearGradientBrush brush2 = new LinearGradientBrush(rectangle2, Color.FromArgb(num, color_3), Color.FromArgb(num / 3, color_3), System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    graphics_0.FillPath(brush2, path4);
                }
                rectangle2.Height -= 2;
            }
            using (GraphicsPath path5 = smethod_3(rectangle, int_3 - UiScale.S(1), gradientPanel_0))
            {
                using Pen pen2 = new Pen(color_4);
                graphics_0.DrawPath(pen2, path5);
            }
            graphics_0.SmoothingMode = smoothingMode;
        }

        private static GraphicsPath smethod_1(Rectangle rectangle_0)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            RectangleF rect = rectangle_0;
            rect.X -= rect.Width * 0.35f;
            rect.Y -= rect.Height * 0.15f;
            rect.Width *= 1.7f;
            rect.Height *= 2.3f;
            graphicsPath.AddEllipse(rect);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        private static GraphicsPath smethod_2(Rectangle rectangle_0, int int_3, GradientPanel gradientPanel_0)
        {
            _ = rectangle_0.Left;
            _ = rectangle_0.Top;
            _ = rectangle_0.Width;
            _ = rectangle_0.Height;
            int num = int_3 << 1;
            return smethod_5(rectangle_0.Left, rectangle_0.Top, rectangle_0.Width, rectangle_0.Height, num, int_3, 0f, gradientPanel_0);
        }

        private static GraphicsPath smethod_3(Rectangle rectangle_0, int int_3, GradientPanel gradientPanel_0)
        {
            RectangleF rectangleF_ = new RectangleF(rectangle_0.X, rectangle_0.Y, rectangle_0.Width, rectangle_0.Height);
            return smethod_4(rectangleF_, int_3, gradientPanel_0);
        }

        private static GraphicsPath smethod_4(RectangleF rectangleF_0, float float_0, GradientPanel gradientPanel_0)
        {
            _ = rectangleF_0.Left;
            _ = rectangleF_0.Top;
            _ = rectangleF_0.Width;
            _ = rectangleF_0.Height;
            float float_ = float_0 * 2f;
            return smethod_5(rectangleF_0.Left, rectangleF_0.Top, rectangleF_0.Width, rectangleF_0.Height, float_, float_0, 0f, gradientPanel_0);
        }

        private static GraphicsPath smethod_5(float float_0, float float_1, float float_2, float float_3, float float_4, float float_5, float float_6, GradientPanel gradientPanel_0)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            bool flag = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            switch (gradientPanel_0.CurveMode)
            {
                case CornerCurveMode.TopLeft_BottomLeft:
                    flag = true;
                    flag2 = false;
                    flag3 = false;
                    flag4 = true;
                    break;
                case CornerCurveMode.None:
                    flag = false;
                    flag2 = false;
                    flag3 = false;
                    flag4 = false;
                    break;
            }
            if (flag)
            {
                graphicsPath.AddArc(float_0, float_1, float_4, float_4, 180f, 90f);
                if (flag2)
                {
                    graphicsPath.AddLine(float_0 + float_5, float_1 + float_6, float_0 + float_2 - float_5, float_1 + float_6);
                }
                else
                {
                    graphicsPath.AddLine(float_0 + (float_5 + float_6), float_1 + float_6, float_0 + float_2, float_1 + float_6);
                }
            }
            else if (flag2)
            {
                graphicsPath.AddLine(float_0, float_1 + float_6, float_0 + float_2 - (float_5 + float_6), float_1 + float_6);
            }
            else
            {
                graphicsPath.AddLine(float_0, float_1 + float_6, float_0 + float_2, float_1 + float_6);
            }
            if (flag2)
            {
                graphicsPath.AddArc(float_0 + float_2 - (float_4 + float_6), float_1, float_4, float_4, 270f, 90f);
                if (flag3)
                {
                    graphicsPath.AddLine(float_0 + float_2 - float_6, float_1 + float_5, float_0 + float_2 - float_6, float_1 + float_3 - float_5);
                }
                else
                {
                    graphicsPath.AddLine(float_0 + float_2 - float_6, float_1 + (float_5 + float_6), float_0 + float_2 - float_6, float_1 + float_3);
                }
            }
            else if (flag3)
            {
                graphicsPath.AddLine(float_0 + float_2 - float_6, float_1, float_0 + float_2 - float_6, float_1 + float_3 - (float_5 + float_6));
            }
            else
            {
                graphicsPath.AddLine(float_0 + float_2 - float_6, float_1, float_0 + float_2 - float_6, float_1 + float_3);
            }
            if (flag3)
            {
                graphicsPath.AddArc(float_0 + float_2 - (float_4 + float_6), float_1 + float_3 - (float_4 + float_6), float_4, float_4, 0f, 90f);
                if (flag4)
                {
                    graphicsPath.AddLine(float_0 + float_2 - float_5, float_1 + float_3 - float_6, float_0 + float_5, float_1 + float_3 - float_6);
                }
                else
                {
                    graphicsPath.AddLine(float_0, float_1 + float_3 - float_6, float_0 + (float_5 - float_6), float_1 + float_3 - float_6);
                }
            }
            else if (flag4)
            {
                graphicsPath.AddLine(float_0 + float_2 - (float_5 + float_6), float_1 + float_3 - float_6, float_0, float_1 + float_3 - float_6);
            }
            else
            {
                graphicsPath.AddLine(float_0 + float_2, float_1 + float_3 - float_6, float_0, float_1 + float_3 - float_6);
            }
            if (flag4)
            {
                graphicsPath.AddArc(float_0, float_1 + float_3 - (float_4 + float_6), float_4, float_4, 90f, 90f);
                if (flag)
                {
                    graphicsPath.AddLine(float_0 + float_6, float_1 + float_3 - float_5, float_0 + float_6, float_1 + float_5);
                }
                else
                {
                    graphicsPath.AddLine(float_0 + float_6, float_1, float_0 + float_6, float_1 + float_3 - (float_5 + float_6));
                }
            }
            else if (flag)
            {
                graphicsPath.AddLine(float_0 + float_6, float_1 + (float_5 + float_6), float_0 + float_6, float_1 + float_3);
            }
            else
            {
                graphicsPath.AddLine(float_0 + float_6, float_1 + float_3, float_0 + float_6, float_1);
            }
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public void KickStart(Main parentForm, Galaxy galaxy, Cursor defaultMouseCursor)
        {
            main_0 = parentForm;
            galaxy_0 = galaxy;
            font_0 = UiScale.NewFont(Font.FontFamily, 19f, GraphicsUnit.Pixel);
            cursor_0 = defaultMouseCursor;
            Cursor = defaultMouseCursor;
            base.CurveMode = CornerCurveMode.TopLeft_BottomLeft;
            base.Curvature = UiScale.S(15);
            base.BorderWidth = UiScale.S(1);
            DoRegionClip();
            CreateSlots();
            ClearItems();
            bool_0 = true;
        }

        public ScrollingLinkList() : base()
        {

            double_0 = 20.0;
            object_0 = new Lock();
            font_0 = UiScale.NewFont("Verdana", 8.25f, FontStyle.Regular);
            dateTime_0 = DateTime.Now.ToUniversalTime();
            base.BorderColor = Color.FromArgb(67, 67, 77);
            base.BorderStyle = BorderStyle.FixedSingle;
            base.BorderWidth = UiScale.S(4);
            base.CurveMode = CornerCurveMode.TopLeft_BottomLeft;
            base.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            base.BackColor = Color.FromArgb(58, 58, 66);
            base.BackColor2 = Color.FromArgb(31, 31, 41);
            base.BackColor3 = Color.FromArgb(12, 12, 20);
            base.BorderWidth = UiScale.S(1);
        }

        // Seconds of game time between two messages becoming visible
        // (same pace as the old scrolling: double_0 pixels per second, one row = rowPitch pixels).
        private double ReleaseInterval => rowPitch / double_0;

        private void CreateSlots()
        {
            if (slots != null)
            {
                return;
            }
            slots = new LinkLabel[SlotCount];
            for (int i = 0; i < SlotCount; i++)
            {
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                linkLabel.LinkColor = Color.FromArgb(170, 170, 170);
                linkLabel.ActiveLinkColor = Color.Yellow;
                linkLabel.VisitedLinkColor = Color.FromArgb(200, 170, 170);
                linkLabel.BackColor = Color.Transparent;
                linkLabel.BorderStyle = BorderStyle.None;
                linkLabel.Font = font_0;
                linkLabel.TextAlign = ContentAlignment.MiddleCenter;
                linkLabel.Height = UiScale.S(18);
                linkLabel.Name = Guid.NewGuid().ToString();
                linkLabel.Cursor = cursor_0;
                linkLabel.Visible = false;
                linkLabel.LinkClicked += main_0.method_249;
                linkLabel.MouseEnter += main_0.method_247;
                linkLabel.MouseLeave += main_0.method_248;
                linkLabel.Parent = this;
                slots[i] = linkLabel;
            }
            LayoutSlots();
        }

        // Rows are bottom-aligned, same resting positions as before: top = ClientHeight - rowPitch * (rows below + 1)
        private void LayoutSlots()
        {
            if (slots == null)
            {
                return;
            }
            int height = UiScale.S(18);
            rowPitch = height + UiScale.S(1);
            int width = base.ClientRectangle.Width;
            int bottom = base.ClientRectangle.Height;
            for (int i = 0; i < SlotCount; i++)
            {
                slots[i].SetBounds(0, bottom - (SlotCount - i) * rowPitch, width, height);
            }
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            LayoutSlots();
        }

        public void ClearItems()
        {
            DateTime dateTime = (galaxy_0 != null) ? galaxy_0.CurrentDateTime : DateTime.Now.ToUniversalTime();
            lock (object_0)
            {
                pending.Clear();
                releaseCredit = ReleaseInterval;
                clearRequested = true;
                dateTime_0 = dateTime;
            }
            // Labels are only touched on the UI thread: reset them now if we are on it,
            // otherwise the next method_3() does it.
            if (!InvokeRequired)
            {
                method_3();
            }
        }

        // Can be called from any thread: it only queues the data. The labels are updated by method_3().
        public void AddItem(string text, object relatedObject)
        {
            if (!bool_0 || string.IsNullOrEmpty(text))
            {
                return;
            }
            SlotData item = new SlotData
            {
                Text = text.Replace("\n", " "),
                Data = relatedObject
            };
            lock (object_0)
            {
                pending.Enqueue(item);
            }
        }

        // Called on every tick (UI thread). Releases queued items one by one at the old scroll pace;
        // each release shifts the texts/link data of the 5 labels up by one row. No control is moved.
        public void method_3()
        {
            DateTime dateTime = (galaxy_0 == null) ? dateTime_0 : galaxy_0.CurrentDateTime;
            List<object> expired = null;
            lock (object_0)
            {
                double elapsed = dateTime.Subtract(dateTime_0).TotalSeconds;
                dateTime_0 = dateTime;
                if (slots == null)
                {
                    return;
                }
                if (clearRequested)
                {
                    clearRequested = false;
                    ResetSlots();
                }
                double interval = ReleaseInterval;
                if (pending.Count == 0)
                {
                    // idle: the next added item is shown on the very next tick
                    releaseCredit = interval;
                    return;
                }
                if (elapsed > 0.0)
                {
                    releaseCredit += elapsed;
                }
                int due = 0;
                while (due < pending.Count && releaseCredit >= interval)
                {
                    releaseCredit -= interval;
                    due++;
                }
                if (due == 0)
                {
                    return;
                }
                expired = ShowNext(due);
                if (pending.Count == 0)
                {
                    releaseCredit = interval;
                }
            }
            // Items pushed out at the top (outside the lock, the handlers may call AddItem)
            if (expired != null)
            {
                foreach (object linkData in expired)
                {
                    if (linkData is EmpireMessage empireMessage_)
                    {
                        main_0.method_244(empireMessage_);
                    }
                    else
                    {
                        main_0.method_245(linkData);
                    }
                }
            }
        }

        // Moves 'count' items from the pending queue into the slots.
        // Returns link data of the items that left the list at the top (null if none).
        private List<object> ShowNext(int count)
        {
            // current rows (oldest first) followed by the new ones
            scratch.Clear();
            for (int i = SlotCount - shownCount; i < SlotCount; i++)
            {
                scratch.Add(ReadSlot(slots[i]));
            }
            for (int i = 0; i < count; i++)
            {
                scratch.Add(pending.Dequeue());
            }

            // everything above the last SlotCount items has scrolled out
            int drop = Math.Max(0, scratch.Count - SlotCount);
            List<object> expired = null;
            for (int i = 0; i < drop; i++)
            {
                if (scratch[i].Data != null)
                {
                    expired ??= new List<object>();
                    expired.Add(scratch[i].Data);
                }
            }

            // write the rest bottom-aligned
            int keep = scratch.Count - drop;
            int firstUsed = SlotCount - keep;
            for (int i = 0; i < SlotCount; i++)
            {
                if (i < firstUsed)
                {
                    HideSlot(slots[i]);
                }
                else
                {
                    WriteSlot(slots[i], scratch[drop + i - firstUsed]);
                }
            }
            shownCount = keep;
            scratch.Clear();
            return expired;
        }

        private void ResetSlots()
        {
            for (int i = 0; i < SlotCount; i++)
            {
                HideSlot(slots[i]);
            }
            shownCount = 0;
        }

        private static SlotData ReadSlot(LinkLabel label)
        {
            SlotData result = new SlotData { Text = label.Text };
            if (label.Links.Count > 0)
            {
                result.Data = label.Links[0].LinkData;
                result.Visited = label.Links[0].Visited;
            }
            return result;
        }

        private static void WriteSlot(LinkLabel label, SlotData slotData)
        {
            if (label.Links.Count > 0)
            {
                LinkLabel.Link current = label.Links[0];
                if (label.Text == slotData.Text && ReferenceEquals(current.LinkData, slotData.Data) && current.Visited == slotData.Visited)
                {
                    return; // row already shows exactly this
                }
            }
            label.Links.Clear();
            label.Text = slotData.Text;
            label.LinkArea = new LinkArea(0, slotData.Text.Length);
            LinkLabel.Link link = label.Links[0];
            link.LinkData = slotData.Data;
            link.Visited = slotData.Visited;
            label.Visible = true;
        }

        private static void HideSlot(LinkLabel label)
        {
            if (label.Text.Length == 0 && label.Links.Count == 0)
            {
                return; // already empty
            }
            label.Links.Clear(); // also drops the reference to the related object
            label.Text = string.Empty;
            label.Visible = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color color_ = Color.FromArgb(0, 0, 0);
            Color color_2 = Color.FromArgb(96, 96, 104);
            Color color_3 = Color.FromArgb(0, 0, 16);
            Color color_4 = Color.FromArgb(67, 67, 77);
            smethod_0(e.Graphics, base.ClientRectangle, bool_2: false, bool_3: false, bool_4: false, bool_5: true, color_3, color_, Color.FromArgb(48, 48, 128), color_2, color_4, 153f, UiScale.S(15), bool_6: true, Color.FromArgb(48, 48, 48), this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //method_3();
        }
    }
}