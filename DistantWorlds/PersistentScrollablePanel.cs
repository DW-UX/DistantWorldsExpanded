// Decompiled with JetBrains decompiler
// Type: DistantWorlds.PersistentScrollablePanel
// Assembly: DistantWorlds, Version=1.9.5.12, Culture=neutral, PublicKeyToken=null
// MVID: DFB67E2D-B390-4FC8-9690-CA3C0824704F
// Assembly location: F:\SteamLibrary\steamapps\common\Distant Worlds Universe\DistantWorlds - Copy-Unpacked.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DistantWorlds
{
    public class PersistentScrollablePanel : Panel
    {
        internal class Class4 : Panel
        {
            protected override CreateParams CreateParams
            {
                get
                {
                    CreateParams createParams = base.CreateParams;
                    createParams.ExStyle |= 32;
                    return createParams;
                }
            }

            public Class4():base()
            {
                Class7.VEFSJNszvZKMZ();
                SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
                SetStyle(ControlStyles.UserPaint, value: true);
                SetStyle(ControlStyles.SupportsTransparentBackColor, value: true);
                SetStyle(ControlStyles.Opaque, value: true);
                UpdateStyles();
                BackColor = Color.Transparent;
            }
        }

        private IContainer icontainer_0;

        private Class4 _InternalPanel;

        private string string_0;

        private string string_1;

        private Image image_0;

        private int int_0;

        private bool bool_0;

        private Image image_1;

        private Rectangle rectangle_0;

        private int int_1;

        public string Title
        {
            get
            {
                return string_0;
            }
            set
            {
                Graphics graphics = _InternalPanel.CreateGraphics();
                graphics.Clear(_InternalPanel.BackColor);
                string_0 = value;
                method_1();
                method_2();
            }
        }

        public override string Text
        {
            get
            {
                return string_1;
            }
            set
            {
                Graphics graphics = _InternalPanel.CreateGraphics();
                graphics.Clear(_InternalPanel.BackColor);
                string_1 = value;
                method_1();
                method_2();
            }
        }

        public Image Picture
        {
            get
            {
                return image_0;
            }
            set
            {
                image_0 = value;
                if (image_0 != null)
                {
                    int_0 = image_0.Width;
                }
                else
                {
                    int_0 = 0;
                }
            }
        }

        public int PictureSize
        {
            get
            {
                return int_0;
            }
            set
            {
                int_0 = value;
            }
        }

        public bool AlignPictureRight
        {
            get
            {
                return bool_0;
            }
            set
            {
                bool_0 = value;
            }
        }

        public Image SmallPicture
        {
            get
            {
                return image_1;
            }
            set
            {
                image_1 = value;
            }
        }

        public Rectangle SmallPictureLocation
        {
            get
            {
                return rectangle_0;
            }
            set
            {
                rectangle_0 = value;
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
            _InternalPanel = new Class4();
            SuspendLayout();
            _InternalPanel.Location = new Point(0, 0);
            _InternalPanel.Name = "_InternalPanel";
            _InternalPanel.Size = new Size(200, 100);
            _InternalPanel.TabIndex = 0;
            ResumeLayout(performLayout: false);
        }

        public PersistentScrollablePanel():base()
        {
            Class7.VEFSJNszvZKMZ();
            _InternalPanel = new Class4();
            method_0();
            Graphics graphics = _InternalPanel.CreateGraphics();
            graphics.Clear(_InternalPanel.BackColor);
            AutoScroll = true;
            base.AutoScrollMargin = new Size(0, 0);
            base.Padding = new Padding(0);
            base.Margin = new Padding(0);
            SetStyle(ControlStyles.AllPaintingInWmPaint, value: true);
            SetStyle(ControlStyles.UserPaint, value: true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
            UpdateStyles();
            base.Controls.Add(_InternalPanel);
            _InternalPanel.Paint += _InternalPanel_Paint;
            _InternalPanel.BorderStyle = BorderStyle.None;
            _InternalPanel.Location = new Point(0, 0);
            _InternalPanel.Size = new Size(base.Size.Width, base.Size.Height);
            _InternalPanel.BringToFront();
        }

        public void Reset()
        {
            _InternalPanel.Size = new Size(base.ClientRectangle.Size.Width, base.ClientRectangle.Size.Height);
            int_1 = _InternalPanel.Size.Height;
            method_1();
            Graphics graphics = _InternalPanel.CreateGraphics();
            graphics.Clear(_InternalPanel.BackColor);
        }

        public void Clear()
        {
            string_0 = string.Empty;
            image_0 = null;
            int_0 = 0;
            image_1 = null;
            rectangle_0 = new Rectangle(0, 0, 0, 0);
            bool_0 = false;
            Text = string.Empty;
        }

        private void method_1()
        {
            Graphics graphics = _InternalPanel.CreateGraphics();
            Font font = new Font(Font.FontFamily, Font.Size + 1.5f, FontStyle.Bold);
            _InternalPanel.Font = Font;
            SizeF sizeF = new SizeF(0f, 0f);
            SizeF sizeF2 = new SizeF(0f, 0f);
            sizeF = graphics.MeasureString(string_0, font);
            if (!string.IsNullOrEmpty(string_0))
            {
                sizeF.Height += 6f;
            }
            sizeF2 = graphics.MeasureString(string_1, Font);
            if (!string.IsNullOrEmpty(string_1))
            {
                sizeF2.Height += 12f;
            }
            if ((int)sizeF.Height + (int)sizeF2.Height > _InternalPanel.Height)
            {
                if (int_1 == 0)
                {
                    int_1 = _InternalPanel.Height;
                }
                _InternalPanel.Height = (int)sizeF.Height + (int)sizeF2.Height;
                _InternalPanel.Width = base.ClientRectangle.Width;
                return;
            }
            int num = (int)sizeF.Height + (int)sizeF2.Height;
            if (num <= int_1)
            {
                num = int_1;
                _InternalPanel.Width = base.ClientRectangle.Width;
            }
            _InternalPanel.Height = num;
        }

        private void _InternalPanel_Paint(object sender, PaintEventArgs e)
        {
            method_2();
        }

        internal void method_2()
        {
            Graphics graphics = CreateGraphics();
            PaintEventArgs pevent = new PaintEventArgs(graphics, new Rectangle(0, 0, base.Width, base.Height));
            OnPaintBackground(pevent);
            Graphics graphics2 = _InternalPanel.CreateGraphics();
            Font font = new Font(Font.FontFamily, Font.Size + 1.5f, FontStyle.Bold);
            _InternalPanel.Font = Font;
            base.AutoScrollMargin = new Size(0, 0);
            graphics2.SmoothingMode = SmoothingMode.AntiAlias;
            graphics2.InterpolationMode = InterpolationMode.High;
            graphics2.CompositingQuality = CompositingQuality.HighQuality;
            if (image_0 != null)
            {
                Rectangle rectangle = default(Rectangle);
                int num = _InternalPanel.ClientRectangle.Width - 6;
                int num2 = _InternalPanel.ClientRectangle.Height - 6;
                double num3 = (double)image_0.Width / (double)image_0.Height;
                int num4;
                int num5;
                if (image_0.Width > image_0.Height)
                {
                    num4 = (int)((double)int_0 * num3);
                    num5 = int_0;
                }
                else
                {
                    num4 = int_0;
                    num5 = (int)((double)int_0 / num3);
                }
                double num6;
                if (num5 > num2)
                {
                    num6 = (double)num2 / (double)num5;
                    if ((double)num4 * num6 > (double)num)
                    {
                        num6 *= (double)num / ((double)num4 * num6);
                    }
                }
                else if (num4 > num)
                {
                    num6 = (double)num / (double)num4;
                    if ((double)num5 * num6 > (double)num2)
                    {
                        num6 *= (double)num2 / ((double)num5 * num6);
                    }
                }
                else
                {
                    num6 = 1.0;
                }
                int num7;
                int num8;
                int num9;
                if (bool_0)
                {
                    num7 = num - (int)((double)num4 * num6);
                    num8 = (num2 - (int)((double)num5 * num6)) / 2;
                    num9 = (int)((double)num4 * num6);
                    if (num - num7 - num9 / 2 < num2 / 2)
                    {
                        num7 = num - (num2 / 2 + num9 / 2);
                    }
                }
                else
                {
                    num7 = (num - (int)((double)num4 * num6)) / 2;
                    num8 = (num2 - (int)((double)num5 * num6)) / 2;
                    num9 = (int)((double)num4 * num6);
                }
                graphics2.DrawImage(rect: new Rectangle(num7 + 3, num8 + 3, num9, (int)((double)num5 * num6)), image: image_0);
                if (image_1 != null)
                {
                    graphics2.DrawImage(image_1, rectangle_0);
                }
            }
            int num10 = 6;
            if (!string.IsNullOrEmpty(string_0))
            {
                graphics2.DrawString(string_0, font, new SolidBrush(Color.Black), new PointF(7f, num10 + 1));
                graphics2.DrawString(string_0, font, new SolidBrush(Color.White), new PointF(6f, num10));
                num10 += (int)graphics2.MeasureString(string_0, font).Height + 6;
            }
            graphics2.DrawString(string_1, Font, new SolidBrush(Color.Black), new PointF(7f, num10 + 1));
            graphics2.DrawString(string_1, Font, new SolidBrush(Color.White), new PointF(6f, num10));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            method_2();
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, new Rectangle(new Point(0, 0), base.Size));
            }
        }
    }
}
