// Decompiled with JetBrains decompiler
// Type: DistantWorlds.RelatedEncyclopediaItemsBox
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Controls;
using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class RelatedEncyclopediaItemsBox : GradientPanel
    {
        private IContainer icontainer_0;

        private Main ahfGeFqstf;

        private Start start_0;

        //private bool bool_0;

        private EncyclopediaItemList encyclopediaItemList_0;

        private List<LinkLabel> list_0;

        private Font font_0;

        private Font font_1;

        private SolidBrush solidBrush_0;

        private int int_0;

        private DateTime dateTime_0;

        public EncyclopediaItemList Items
        {
            get
            {
                return encyclopediaItemList_0;
            }
            set
            {
                encyclopediaItemList_0 = value;
                method_2();
                Invalidate();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && icontainer_0 != null)
            {
                icontainer_0.Dispose();
            }
            base.Dispose(disposing);
        }

        private void method_0()
        {
            icontainer_0 = new Container();
        }

        private void method_1()
        {
            font_0 = new Font(Font.FontFamily, 16.67f, FontStyle.Regular, GraphicsUnit.Pixel);
            font_1 = new Font(Font.FontFamily, 18.67f, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public void KickStart(Main parentForm)
        {
            method_1();
            ahfGeFqstf = parentForm;
            start_0 = null;
            //bool_0 = true;
            ClearItems();
        }

        public void KickStart(Start parentForm)
        {
            method_1();
            start_0 = parentForm;
            ahfGeFqstf = null;
            //bool_0 = true;
            ClearItems();
        }

        public RelatedEncyclopediaItemsBox():base()
        {
            
            list_0 = new List<LinkLabel>();
            font_0 = new Font("Verdana", 9f, FontStyle.Regular);
            font_1 = new Font("Verdana", 10f, FontStyle.Bold);
            solidBrush_0 = new SolidBrush(Color.White);
            int_0 = 10;
            dateTime_0 = DateTime.Now.ToUniversalTime();
            base.BorderColor = Color.FromArgb(0, 0, 255);
            base.BorderStyle = BorderStyle.FixedSingle;
            base.BorderWidth = 2;
            base.CurveMode = CornerCurveMode.All;
            base.Curvature = 20;
            base.GradientMode = LinearGradientMode.Vertical;
            base.BackColor = Color.FromArgb(8, 8, 32);
            base.BackColor2 = Color.Navy;
        }

        public void ClearItems()
        {
            if (list_0 == null)
            {
                return;
            }
            foreach (LinkLabel item in list_0)
            {
                if (ahfGeFqstf != null)
                {
                    item.LinkClicked -= ahfGeFqstf.method_466;
                }
                else if (start_0 != null)
                {
                    item.LinkClicked -= start_0.method_134;
                }
                item.Links[0].LinkData = null;
                if (item.Parent.Controls.Contains(item))
                {
                    item.Parent.Controls.Remove(item);
                }
                item.Parent = null;
                item.Dispose();
            }
            list_0.Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawItems(e.Graphics);
        }

        private void method_2()
        {
            int num = int_0;
            int num2 = int_0;
            num2 += (int)((double)int_0 * 2.0);
            num = int_0;
            ClearItems();
            if (encyclopediaItemList_0 == null)
            {
                return;
            }
            for (int i = 0; i < encyclopediaItemList_0.Count; i++)
            {
                EncyclopediaItem encyclopediaItem = encyclopediaItemList_0[i];
                LinkLabel linkLabel = new LinkLabel();
                if (i < encyclopediaItemList_0.Count - 1)
                {
                    linkLabel.Text = encyclopediaItem.Title + ",";
                    linkLabel.LinkArea = new LinkArea(0, linkLabel.Text.Length - 1);
                }
                else
                {
                    linkLabel.Text = encyclopediaItem.Title;
                    linkLabel.LinkArea = new LinkArea(0, linkLabel.Text.Length);
                }
                linkLabel.Height += 4;
                linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
                linkLabel.LinkColor = Color.Yellow;
                linkLabel.ForeColor = Color.Yellow;
                linkLabel.ActiveLinkColor = Color.Orange;
                linkLabel.VisitedLinkColor = Color.White;
                linkLabel.BackColor = Color.Transparent;
                linkLabel.BorderStyle = BorderStyle.None;
                linkLabel.Font = font_0;
                linkLabel.TextAlign = ContentAlignment.MiddleLeft;
                linkLabel.Parent = this;
                linkLabel.Links[0].LinkData = encyclopediaItem;
                if (ahfGeFqstf != null)
                {
                    linkLabel.LinkClicked += ahfGeFqstf.method_466;
                }
                else if (start_0 != null)
                {
                    linkLabel.LinkClicked += start_0.method_134;
                }
                linkLabel.Name = Guid.NewGuid().ToString();
                linkLabel.Width = linkLabel.PreferredWidth;
                int num3 = num + (linkLabel.Width + int_0);
                if (num3 > base.ClientRectangle.Width - int_0)
                {
                    num2 += linkLabel.Height - 7;
                    num = int_0;
                }
                linkLabel.Location = new Point(num, num2);
                linkLabel.Visible = true;
                num += linkLabel.Width + int_0;
                list_0.Add(linkLabel);
            }
        }

        public void DrawItems(Graphics graphics)
        {
            int num = int_0;
            int num2 = int_0;
            graphics.DrawString(TextResolver.GetText("Related Topics"), font_1, solidBrush_0, num, num2);
        }
    }
}
