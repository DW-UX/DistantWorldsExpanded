// Decompiled with JetBrains decompiler
// Type: DistantWorlds.SelectionToolStripRenderer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class SelectionToolStripRenderer : ToolStripRenderer
    {
        public SelectionToolStripRenderer():base()
        {
            
        }

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);
            toolStrip.AllowDrop = false;
            toolStrip.AutoSize = true;
            toolStrip.CanOverflow = true;
            toolStrip.ShowItemToolTips = false;
            toolStrip.Font = new Font("Verdana", 8f);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (!e.Item.Selected && !e.Item.Pressed)
            {
                e.ArrowColor = Color.FromArgb(170, 170, 170);
            }
            else
            {
                e.ArrowColor = Color.Yellow;
            }
            base.OnRenderArrow(e);
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Pen pen = new Pen(Color.DarkGray);
            e.Graphics.DrawLine(pen, 4, e.Item.Bounds.Height / 2, e.Item.Bounds.Width - 8, e.Item.Bounds.Height / 2);
            pen.Dispose();
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.AffectedBounds, Color.FromArgb(32, 32, 40), Color.FromArgb(64, 64, 80), 0f);
            e.Graphics.FillRectangle(linearGradientBrush, e.AffectedBounds);
            linearGradientBrush.Dispose();
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected && !e.Item.Pressed)
            {
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.Item.Bounds, Color.FromArgb(32, 32, 40), Color.FromArgb(64, 64, 80), 0f);
                e.Graphics.FillRectangle(linearGradientBrush, e.Item.Bounds);
                linearGradientBrush.Dispose();
            }
            else
            {
                Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect, Color.FromArgb(80, 80, 96), Color.FromArgb(160, 160, 192), 0f);
                e.Graphics.FillRectangle(linearGradientBrush2, rect);
                linearGradientBrush2.Dispose();
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                e.TextColor = Color.Yellow;
            }
            else if (!e.Item.Enabled)
            {
                e.TextColor = Color.DarkGray;
            }
            else
            {
                e.TextColor = Color.FromArgb(170, 170, 170);
            }
            base.OnRenderItemText(e);
        }
    }
}
