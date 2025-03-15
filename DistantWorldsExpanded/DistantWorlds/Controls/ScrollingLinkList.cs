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
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class ScrollingLinkList : GradientPanel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private bool bool_0;

        private Cursor cursor_0;

        private List<LinkLabel> list_0;

        private List<double> list_1;

        //private int int_0;

        private double double_0;

        private object object_0;

        private object object_1;

        private double double_1;

        private Font font_0;

        private int int_1;

        private int int_2;

        private double double_2;

        private DateTime dateTime_0;

        private bool bool_1;

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
                Pen pen = new Pen(color_0);
                if (bool_6)
                {
                    pen = new Pen(Color.Black);
                }
                graphics_0.DrawPath(pen, path);
                pen.Dispose();
            }
            rectangle.X++;
            rectangle.Y++;
            rectangle.Width -= 2;
            rectangle.Height -= 2;
            Rectangle rectangle2 = rectangle;
            rectangle2.Height >>= 1;
            using (GraphicsPath path2 = smethod_3(rectangle, int_3 - 2, gradientPanel_0))
            {
                using Brush brush = new SolidBrush(color_0);
                graphics_0.FillPath(brush, path2);
            }
            if ((bool_3 || bool_4) && !bool_2)
            {
                using GraphicsPath path3 = smethod_3(rectangle, int_3 - 2, gradientPanel_0);
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
                using (GraphicsPath path4 = smethod_2(rectangle2, int_3 - 2, gradientPanel_0))
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
            using (GraphicsPath path5 = smethod_3(rectangle, int_3 - 1, gradientPanel_0))
            {
                Pen pen2 = new Pen(color_4);
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
            GraphicsPath graphicsPath = new GraphicsPath();
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
            GraphicsPath graphicsPath = new GraphicsPath();
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
            font_0 = new Font(Font.FontFamily, 19f, GraphicsUnit.Pixel);
            cursor_0 = defaultMouseCursor;
            Cursor = defaultMouseCursor;
            base.CurveMode = CornerCurveMode.TopLeft_BottomLeft;
            base.Curvature = 15;
            base.BorderWidth = 1;
            DoRegionClip();
            list_0 = new List<LinkLabel>();
            list_1 = new List<double>();
            double_1 = 0.0;
            dateTime_0 = galaxy_0.CurrentDateTime;
            double_2 = 0.0;
            bool_0 = true;
        }

        public ScrollingLinkList() : base()
        {
            
            list_0 = new List<LinkLabel>();
            list_1 = new List<double>();
            //int_0 = 4;
            double_0 = 20.0;
            object_0 = new object();
            object_1 = new object();
            font_0 = new Font("Verdana", 8.25f, FontStyle.Regular);
            int_2 = 5;
            dateTime_0 = DateTime.Now.ToUniversalTime();
            base.BorderColor = Color.FromArgb(67, 67, 77);
            base.BorderStyle = BorderStyle.FixedSingle;
            base.BorderWidth = 4;
            base.CurveMode = CornerCurveMode.TopLeft_BottomLeft;
            base.GradientMode = DistantWorlds.Controls.LinearGradientMode.Vertical;
            base.BackColor = Color.FromArgb(58, 58, 66);
            base.BackColor2 = Color.FromArgb(31, 31, 41);
            base.BackColor3 = Color.FromArgb(12, 12, 20);
            base.BorderWidth = 1;
        }

        public void ClearItems()
        {
            lock (object_0)
            {
                bool_1 = false;
                foreach (LinkLabel item in list_0)
                {
                    item.Cursor = null;
                    item.Parent = null;
                    if (item.Links != null && item.Links.Count > 0)
                    {
                        item.Links[0].LinkData = null;
                    }
                    item.LinkClicked -= main_0.method_249;
                    item.MouseEnter -= main_0.method_247;
                    item.MouseLeave -= main_0.method_248;
                    item.Dispose();
                }
                list_0.Clear();
                lock (object_1)
                {
                    double_1 = 0.0;
                }
                list_1 = new List<double>();
                if (galaxy_0 != null)
                {
                    dateTime_0 = galaxy_0.CurrentDateTime;
                }
                else
                {
                    dateTime_0 = DateTime.Now.ToUniversalTime();
                }
                double_2 = 0.0;
                bool_1 = true;
            }
        }

        [SpecialName]
        private double method_0()
        {
            lock (object_1)
            {
                return double_1;
            }
        }

        private double method_1(double double_3)
        {
            lock (object_1)
            {
                double_1 += double_3;
                return double_1 - double_3;
            }
        }

        private double method_2(double double_3)
        {
            lock (object_1)
            {
                double_1 -= double_3;
                return double_1 + double_3;
            }
        }

        public void AddItem(string text, object relatedObject)
        {
            if (!bool_0)
            {
                return;
            }
            lock (object_0)
            {
                bool_1 = false;
                LinkLabel linkLabel = new LinkLabel();
                string text3 = (linkLabel.Text = text.Replace("\n", " "));
                linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                linkLabel.LinkColor = Color.FromArgb(170, 170, 170);
                linkLabel.ActiveLinkColor = Color.Yellow;
                linkLabel.VisitedLinkColor = Color.FromArgb(200, 170, 170);
                linkLabel.BackColor = Color.Transparent;
                linkLabel.BorderStyle = BorderStyle.None;
                linkLabel.Font = font_0;
                linkLabel.TextAlign = ContentAlignment.MiddleCenter;
                linkLabel.LinkArea = new LinkArea(0, text.Length);
                linkLabel.Parent = this;
                linkLabel.Width = base.ClientRectangle.Width;
                linkLabel.Height = 18;
                if (linkLabel.Links != null && linkLabel.Links.Count > 0)
                {
                    linkLabel.Links[0].LinkData = relatedObject;
                }
                linkLabel.LinkClicked += main_0.method_249;
                linkLabel.MouseEnter += main_0.method_247;
                linkLabel.MouseLeave += main_0.method_248;
                linkLabel.Name = Guid.NewGuid().ToString();
                linkLabel.Cursor = cursor_0;
                int_1 = linkLabel.Height;
                double_2 = (double)int_1 * -1.0 + (double)int_2;
                double num = base.ClientRectangle.Height;
                num = (double)base.ClientRectangle.Height + method_1(int_1 + 1);
                linkLabel.Location = new Point(0, (int)num);
                list_0.Add(linkLabel);
                list_1.Add(num);
                bool_1 = true;
            }
        }

        private void method_3()
        {
            DateTime dateTime = ((galaxy_0 == null) ? dateTime_0 : galaxy_0.CurrentDateTime);
            if (bool_1)
            {
                bool_1 = false;
                double num = 0.0;
                num = method_0();
                if (num > 0.0)
                {
                    SuspendLayout();
                    List<int> list = new List<int>();
                    double totalSeconds = dateTime.Subtract(dateTime_0).TotalSeconds;
                    double num2 = double_0 * totalSeconds;
                    if (num2 > num)
                    {
                        num2 = num;
                    }
                    for (int i = 0; i < list_0.Count; i++)
                    {
                        LinkLabel linkLabel = list_0[i];
                        double num3 = list_1[i];
                        double num4 = num3 - num2;
                        list_1[i] = num4;
                        int num5 = (int)num4;
                        if (linkLabel.Top != num5)
                        {
                            linkLabel.Top = num5;
                        }
                        if (num4 < double_2)
                        {
                            list.Add(i);
                        }
                    }
                    method_2(num2);
                    if (list.Count > 0)
                    {
                        for (int num6 = list.Count - 1; num6 >= 0; num6--)
                        {
                            if (base.Controls.Contains(list_0[list[num6]]))
                            {
                                base.Controls.Remove(list_0[list[num6]]);
                                object linkData = list_0[list[num6]].Links[0].LinkData;
                                if (linkData != null)
                                {
                                    if (linkData is EmpireMessage empireMessage_)
                                    {
                                        main_0.method_244(empireMessage_);
                                    }
                                    else
                                    {
                                        main_0.method_245(linkData);
                                    }
                                    list_0[list[num6]].Links[0] = null;
                                    list_0[list[num6]].Links.Clear();
                                }
                                list_0[list[num6]].LinkClicked -= main_0.method_249;
                                list_0[list[num6]].MouseEnter -= main_0.method_247;
                                list_0[list[num6]].MouseLeave -= main_0.method_248;
                                list_0[list[num6]].Parent = null;
                                list_0[list[num6]].Cursor = null;
                                list_0.RemoveAt(list[num6]);
                            }
                            list_1.RemoveAt(list[num6]);
                        }
                    }
                    ResumeLayout();
                }
                bool_1 = true;
            }
            dateTime_0 = dateTime;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color color_ = Color.FromArgb(0, 0, 0);
            Color color_2 = Color.FromArgb(96, 96, 104);
            Color color_3 = Color.FromArgb(0, 0, 16);
            Color color_4 = Color.FromArgb(67, 67, 77);
            smethod_0(e.Graphics, base.ClientRectangle, bool_2: false, bool_3: false, bool_4: false, bool_5: true, color_3, color_, Color.FromArgb(48, 48, 128), color_2, color_4, 153f, 15, bool_6: true, Color.FromArgb(48, 48, 48), this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            method_3();
        }
    }
}
