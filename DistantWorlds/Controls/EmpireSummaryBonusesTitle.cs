// Decompiled with JetBrains decompiler
// Type: DistantWorlds.Controls.EmpireSummaryBonusesTitle
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using DistantWorlds.Types;
using System.Drawing;
using System.Windows.Forms;

namespace DistantWorlds.Controls
{
    public class EmpireSummaryBonusesTitle : GradientPanel
    {
        public EmpireSummaryBonuses _BonusesPanel;

        public Panel _Container;

        private int int_0;

        private int int_1;

        private int int_2;

        private SolidBrush solidBrush_0;

        private SolidBrush solidBrush_1;

        private SolidBrush solidBrush_2;

        private SolidBrush solidBrush_3;

        private Font font_0;

        private Font font_1;

        public EmpireSummaryBonusesTitle():base()
        {
            
            _BonusesPanel = new EmpireSummaryBonuses();
            _Container = new Panel();
            int_0 = 15;
            int_1 = 10;
            int_2 = 10;
            solidBrush_0 = new SolidBrush(Color.FromArgb(170, 170, 170));
            solidBrush_1 = new SolidBrush(Color.Black);
            solidBrush_2 = new SolidBrush(Color.Red);
            solidBrush_3 = new SolidBrush(Color.Green);
            Font = new Font("Verdana", 8f);
            SetFont(16.67f);
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        public void ClearData()
        {
            _BonusesPanel.ClearData();
        }

        public void Ignite(Main parentForm, Galaxy galaxy, Empire empire, CharacterImageCache characterImageCache)
        {
            if (!base.Controls.Contains(_Container))
            {
                base.Controls.Add(_Container);
            }
            _Container.Size = new Size(base.Width - 20, base.Height - (int_1 * 2 + int_0 + 10));
            _Container.Location = new Point(int_2, int_1 + int_0 + 10);
            _Container.AutoScroll = true;
            _Container.SetAutoScrollMargin(0, 0);
            _Container.AutoScrollPosition = new Point(0, 0);
            if (!_Container.Controls.Contains(_BonusesPanel))
            {
                _Container.Controls.Add(_BonusesPanel);
            }
            _Container.Visible = true;
            _Container.BringToFront();
            _BonusesPanel.Size = new Size(_Container.Width - 20, _Container.Height - 20);
            _BonusesPanel.Location = new Point(0, 0);
            _BonusesPanel.MaximumSize = new Size(_Container.Width - 20, 1000);
            _BonusesPanel.MinimumSize = new Size(_Container.Width - 20, _Container.Height - 20);
            _BonusesPanel.AutoSize = true;
            _BonusesPanel.Visible = true;
            _BonusesPanel.BringToFront();
            _BonusesPanel.Font = Font;
            _BonusesPanel.Ignite(parentForm, galaxy, empire, characterImageCache);
            font_0 = new Font(Font, FontStyle.Bold);
            font_1 = new Font(Font.FontFamily, Font.Size + 3f, FontStyle.Bold, GraphicsUnit.Pixel);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            method_0(e.Graphics);
        }

        private void method_0(Graphics graphics_0)
        {
            int num = int_1;
            new SizeF(base.Width - (int_0 + 5 + int_2 * 2), base.Height);
            method_1(graphics_0, TextResolver.GetText("Bonuses"), font_1, new Point(int_2, num));
        }

        private void method_1(Graphics graphics_0, string string_0, Font font_2, Point point_0)
        {
            method_2(graphics_0, string_0, font_2, point_0, solidBrush_0);
        }

        private void method_2(Graphics graphics_0, string string_0, Font font_2, Point point_0, SolidBrush solidBrush_4)
        {
            point_0 = new Point(point_0.X + 1, point_0.Y + 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
            point_0 = new Point(point_0.X - 1, point_0.Y - 1);
            graphics_0.DrawString(string_0, font_2, solidBrush_4, point_0, StringFormat.GenericTypographic);
        }

        private void method_3(Graphics graphics_0, string string_0, Font font_2, Point point_0, Brush brush_0, SizeF sizeF_0, out int int_3)
        {
            int_3 = 0;
            if (sizeF_0 != SizeF.Empty)
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(layoutRectangle: new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height), s: string_0, font: font_2, brush: solidBrush_1, format: StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                RectangleF layoutRectangle2 = new RectangleF(point_0.X, point_0.Y, sizeF_0.Width, sizeF_0.Height);
                graphics_0.DrawString(string_0, font_2, brush_0, layoutRectangle2, StringFormat.GenericTypographic);
                int_3 = (int)graphics_0.MeasureString(string_0, font_2, (int)sizeF_0.Width, StringFormat.GenericTypographic).Height;
            }
            else
            {
                point_0 = new Point(point_0.X + 1, point_0.Y + 1);
                graphics_0.DrawString(string_0, font_2, solidBrush_1, point_0, StringFormat.GenericTypographic);
                point_0 = new Point(point_0.X - 1, point_0.Y - 1);
                graphics_0.DrawString(string_0, font_2, brush_0, point_0, StringFormat.GenericTypographic);
            }
        }
    }
}
