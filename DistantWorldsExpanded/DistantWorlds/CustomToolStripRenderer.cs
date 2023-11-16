// Decompiled with JetBrains decompiler
// Type: DistantWorlds.CustomToolStripRenderer
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class CustomToolStripRenderer : ToolStripRenderer
    {
        private Font font_0;

        public CustomToolStripRenderer(Font font):base()
        {
            
            font_0 = font;
        }

        protected override void Initialize(ToolStrip toolStrip)
        {
            base.Initialize(toolStrip);
            toolStrip.AllowDrop = false;
            toolStrip.AutoSize = true;
            toolStrip.CanOverflow = true;
            toolStrip.ShowItemToolTips = false;
            toolStrip.Font = font_0;
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
            Pen pen = new Pen(Color.Gray);
            e.Graphics.DrawLine(pen, 4, e.Item.Bounds.Height / 2, e.Item.Bounds.Width - 8, e.Item.Bounds.Height / 2);
            pen.Dispose();
        }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.AffectedBounds, Color.FromArgb(16, 16, 24), Color.FromArgb(56, 56, 72), 0f);
            e.Graphics.FillRectangle(linearGradientBrush, e.AffectedBounds);
            linearGradientBrush.Dispose();
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected && !e.Item.Pressed)
            {
                LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.Item.Bounds, Color.FromArgb(16, 16, 24), Color.FromArgb(56, 56, 72), 0f);
                e.Graphics.FillRectangle(linearGradientBrush, e.Item.Bounds);
                linearGradientBrush.Dispose();
            }
            else
            {
                Rectangle rect = new Rectangle(0, 0, e.Item.Width, e.Item.Height);
                LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect, Color.FromArgb(64, 64, 80), Color.FromArgb(128, 128, 144), 0f);
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
            else
            {
                e.TextColor = Color.FromArgb(170, 170, 170);
            }
            base.OnRenderItemText(e);
        }
    }

}
