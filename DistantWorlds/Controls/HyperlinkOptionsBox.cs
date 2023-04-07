// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.HyperlinkOptionsBox
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class HyperlinkOptionsBox : Panel
    {
        private Main main_0;

        private Galaxy galaxy_0;

        private bool bool_0;

        private Cursor cursor_0;

        private volatile bool bool_1;

        private List<LinkLabel> list_0;

        private List<double> list_1;

        private int int_0;

        private Font font_0;

        private int int_1;

        private int int_2;

        private double double_0;

        private DateTime dateTime_0;

        protected IFontCache _FontCache;

        private float float_0;

        private bool bool_2;

        public bool Clearing => bool_1;

        public void ClearData()
        {
            galaxy_0 = null;
            ClearItems();
        }

        public void KickStart(Main parentForm, Galaxy galaxy, Cursor defaultMouseCursor)
        {
            int_2 = 5;
            cursor_0 = defaultMouseCursor;
            Cursor = defaultMouseCursor;
            font_0 = _FontCache.GenerateFont(18.67f, isBold: false);
            main_0 = parentForm;
            galaxy_0 = galaxy;
            bool_0 = true;
            ClearItems();
        }

        public HyperlinkOptionsBox() : base()
        {
            Class7.VEFSJNszvZKMZ();
            list_0 = new List<LinkLabel>();
            list_1 = new List<double>();
            int_0 = 5;
            font_0 = new Font("Verdana", 9f, FontStyle.Regular);
            int_2 = 12;
            dateTime_0 = DateTime.Now.ToUniversalTime();
            float_0 = 15.33f;
            SetFont(16.67f);
            base.BorderStyle = BorderStyle.None;
            BackColor = Color.FromArgb(64, 0, 0, 0);
        }

        public virtual void SetFontCache(IFontCache fontCache)
        {
            _FontCache = fontCache;
            if (float_0 > 0f)
            {
                Font = _FontCache.GenerateFont(float_0, bool_2);
            }
        }

        public void SetFont(float pixelSize)
        {
            SetFont(pixelSize, isBold: false);
        }

        public void SetFont(float pixelSize, bool isBold)
        {
            float_0 = pixelSize;
            bool_2 = isBold;
            if (_FontCache != null)
            {
                Font = _FontCache.GenerateFont(float_0, bool_2);
            }
        }

        public void ClearItems()
        {
            bool_1 = true;
            foreach (LinkLabel item in list_0)
            {
                if (item.Links != null && item.Links.Count > 0)
                {
                    item.Links[0].LinkData = null;
                }
                item.LinkClicked -= main_0.method_241;
                item.MouseEnter -= main_0.method_239;
                item.MouseLeave -= main_0.method_240;
                item.Dispose();
            }
            list_0.Clear();
            list_1.Clear();
            int_1 = 0;
            bool_1 = false;
        }

        public Rectangle ResolveLinkBounds(LinkLabel linkLabel)
        {
            Rectangle result = Rectangle.Empty;
            if (list_0 != null && list_0.Contains(linkLabel))
            {
                Point point = PointToScreen(linkLabel.Location);
                result = new Rectangle(point.X, point.Y, linkLabel.Bounds.Width, linkLabel.Bounds.Height);
            }
            return result;
        }

        public LinkLabel ResolveHoveredLink(Point point)
        {
            Point pt = PointToClient(point);
            if (list_0 != null)
            {
                for (int i = 0; i < list_0.Count; i++)
                {
                    if (list_0[i].Bounds.Contains(pt))
                    {
                        return list_0[i];
                    }
                }
            }
            return null;
        }

        public void AddItem(string text, object relatedObject)
        {
            if (!bool_0)
            {
                return;
            }
            LinkLabel linkLabel = new LinkLabel();
            linkLabel.Width = base.ClientRectangle.Width - int_2 * 2;
            linkLabel.Font = font_0;
            linkLabel.Text = text;
            linkLabel.Height += 2;
            linkLabel.LinkBehavior = LinkBehavior.HoverUnderline;
            linkLabel.LinkColor = Color.Yellow;
            linkLabel.ActiveLinkColor = Color.Orange;
            linkLabel.VisitedLinkColor = Color.White;
            linkLabel.BackColor = Color.Transparent;
            linkLabel.BorderStyle = BorderStyle.None;
            linkLabel.TextAlign = ContentAlignment.MiddleCenter;
            linkLabel.Parent = this;
            linkLabel.Links[0].LinkData = relatedObject;
            linkLabel.LinkClicked += main_0.method_241;
            linkLabel.MouseEnter += main_0.method_239;
            linkLabel.MouseLeave += main_0.method_240;
            linkLabel.Name = Guid.NewGuid().ToString();
            linkLabel.Cursor = cursor_0;
            double num = (double)int_1 + (double)int_2;
            Graphics graphics = linkLabel.CreateGraphics();
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            SizeF sizeF = graphics.MeasureString(text, font_0, base.ClientRectangle.Width - int_2 * 2, StringFormat.GenericDefault);
            linkLabel.Location = new Point(int_2, (int)num);
            linkLabel.Visible = true;
            linkLabel.Height = (int)sizeF.Height + 2;
            int_1 += (int)sizeF.Height + 5;
            list_0.Add(linkLabel);
            list_1.Add(num);
            double num2 = 0.0;
            foreach (LinkLabel item in list_0)
            {
                num2 += (double)(item.Height + 5);
            }
            num2 -= 5.0;
            int num3 = (base.ClientRectangle.Height - int_2 * 2 - (int)num2) / 2;
            for (int i = 0; i < list_0.Count; i++)
            {
                list_0[i].Top = (int)list_1[i] + num3;
            }
        }
    }
}
